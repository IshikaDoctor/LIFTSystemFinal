using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LIFT_System
{
    public partial class CreateFundingApplicationPage : Form
    {
        private readonly OleDbConnection conn =
            new OleDbConnection(
                @"Provider=Microsoft.ACE.OLEDB.16.0;Data Source=C:\Users\User\OneDrive - University of Witwatersrand\Desktop\LIFT.accdb;");

        private const string STATUS_DRAFT = "Draft";
        private long _newAppId = 0;

        public CreateFundingApplicationPage()
        {
            InitializeComponent();
            InitForm();
        }

        private void InitForm()
        {
            // Populate allowed types (match your dropdown)
            cbType.Items.Clear();
            cbType.Items.AddRange(new object[]
            {
                "Tuition",
                "Accommodation",
                "Study Materials",
                "Living Expenses",
                "Debt Relief"
            });
            cbType.SelectedIndex = -1;

            lblStatus.Text = STATUS_DRAFT;
            lblAppId.Text = "APP-xxxx";
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            StudentDashboardPage studentDashboardPage = new StudentDashboardPage();
            studentDashboardPage.Show();
            this.Hide();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            // --- Validate role / session ---
            if (string.IsNullOrWhiteSpace(Session.LoggedInID) || Session.LoggedInRole != "Student")
            {
                MessageBox.Show("You must be logged in as a Student to create a funding application.",
                    "Access", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // --- Gather inputs ---
            string type = cbType?.Text?.Trim() ?? "";
            string amountText = (txtAmount?.Text ?? "").Trim();
            string notes = (txtNotes?.Text ?? "").Trim();

            // --- Validation ---
            if (string.IsNullOrWhiteSpace(type))
            {
                MessageBox.Show("Please select a Funding Application Type.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbType?.Focus();
                return;
            }

            // Accept decimal with invariant culture; currency field in Access
            if (!decimal.TryParse(amountText, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal amount) || amount < 0m)
            {
                MessageBox.Show("Requested Amount must be numeric and ≥ 0.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmount?.Focus();
                return;
            }

            if (!long.TryParse(Session.LoggedInID, out long studentNoNumeric))
            {
                // If your StudentNo is text in Access, change the table column to Short Text and store as string instead.
                MessageBox.Show("Your account StudentNo is not in a numeric format. Please contact support.",
                    "Data Format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // --- Insert draft row and get AutoNumber ---
            try
            {
                conn.Open();

                // parse & validate first
                if (!int.TryParse(Session.LoggedInID, out int studentNo))   // Int32, not long
                {
                    MessageBox.Show("Your account StudentNo is not numeric. Please contact support.",
                        "Data Format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string insertSql = @"
        INSERT INTO Funding_Application
            (FundingApplicationType,
             FundingApplicationStatus,
             FundingApplicationSubmissionDate,
             StudentNo,
             FundingApplicationAmount,
             FundingApplicationNote)
        VALUES
            (?, ?, ?, ?, ?, ?)";

                using (var cmd = new OleDbCommand(insertSql, conn))
                {
                    // ORDER matters in OleDb
                    cmd.Parameters.Add("@p1", OleDbType.VarWChar).Value = type;
                    cmd.Parameters.Add("@p2", OleDbType.VarWChar).Value = STATUS_DRAFT;
                    cmd.Parameters.Add("@p3", OleDbType.Date).Value = DateTime.Now.ToString("yyyy/MM/dd"); // ✅ exact date format
                    cmd.Parameters.Add("@p4", OleDbType.Integer).Value = studentNo;  // ✅ logged-in student
                    cmd.Parameters.Add("@p5", OleDbType.Currency).Value = amount;
                    cmd.Parameters.Add("@p6", OleDbType.VarWChar).Value =
                        string.IsNullOrWhiteSpace(notes) ? "None" : notes; // ✅ save notes

                    cmd.ExecuteNonQuery();
                }

                // Retrieve last AutoNumber generated in this connection
                using (var idCmd = new OleDbCommand("SELECT @@IDENTITY", conn))
                {
                    object scalar = idCmd.ExecuteScalar();
                    if (scalar != null && long.TryParse(Convert.ToString(scalar), out long newId))
                    {
                        _newAppId = newId;
                        lblAppId.Text = $"APP-{_newAppId}";
                    }
                    else
                    {
                        throw new Exception("Could not retrieve new FundingApplicationID.");
                    }
                }

                // Navigate to Upload step, pass numeric ID as string
                UploadDocumentsPage uploadDocumentsPage = new UploadDocumentsPage(_newAppId.ToString());
                uploadDocumentsPage.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error while creating the application:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State != ConnectionState.Closed) conn.Close();
            }
        }
    }
}
