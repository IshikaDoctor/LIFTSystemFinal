using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LIFT_System
{
    public partial class SubmitApplicationPage : Form
    {
        private readonly string _applicationId; // AutoNumber from DB, passed in
        private readonly OleDbConnection conn =
            new OleDbConnection(
                @"Provider=Microsoft.ACE.OLEDB.16.0;Data Source=C:\Users\User\OneDrive - University of Witwatersrand\Desktop\LIFT.accdb;");


        public SubmitApplicationPage(string applicationId)
        {
            InitializeComponent();
            _applicationId = applicationId ?? "";
            LoadSummary();
            LoadUploadedFiles();
        }

        private void LoadSummary()
        {
            if (string.IsNullOrWhiteSpace(_applicationId))
            {
                MessageBox.Show("Missing Application ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                conn.Open();
                // Only reading columns you show on screen
                string sql = @"SELECT FundingApplicationType, FundingApplicationAmount, FundingApplicationNote,
                                      FundingApplicationStatus
                               FROM Funding_Application
                               WHERE FundingApplicationID = ?";

                using (var cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.Add("@p1", OleDbType.Integer).Value = int.Parse(_applicationId);

                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (rdr != null && rdr.Read())
                        {
                            string type = Convert.ToString(rdr["FundingApplicationType"]);
                            decimal amt = 0m;
                            if (rdr["FundingApplicationAmount"] != DBNull.Value)
                                amt = Convert.ToDecimal(rdr["FundingApplicationAmount"], CultureInfo.InvariantCulture);
                            string notes = Convert.ToString(rdr["FundingApplicationNote"]) ?? "";
                            string status = Convert.ToString(rdr["FundingApplicationStatus"]) ?? "";

                            lblEnteredID.Text = $"APP-{_applicationId}";
                            lblEnteredType.Text = type;
                            lblEnteredAmount.Text = amt.ToString("0.00", CultureInfo.InvariantCulture);
                            lblNotes.Text = string.IsNullOrWhiteSpace(notes) ? "None" : notes;
                            // You can also show status somewhere if you add a label for it
                        }
                        else
                        {
                            MessageBox.Show("Application not found.", "Not Found",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading application summary:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State != System.Data.ConnectionState.Closed) conn.Close();
            }
        }

        // ===== List uploaded files from ./UploadedFiles/<APPID>/ =====
        private void LoadUploadedFiles()
        {
            try
            {
                string root = AppDomain.CurrentDomain.BaseDirectory;
                string folder = Path.Combine(root, "UploadedFiles", _applicationId);

                lblDocReg.Text = "";
                lblDocFee.Text = "";
                lblDocID.Text = "";

                if (!Directory.Exists(folder)) return;

                var files = Directory.GetFiles(folder).Select(Path.GetFileName).ToList();
                // If you want a simple mapping, just show first three with ✓
                if (files.Count > 0) lblDocReg.Text = files[0] + " ✓";
                if (files.Count > 1) lblDocFee.Text = files[1] + " ✓";
                if (files.Count > 2) lblDocID.Text = files[2] + " ✓";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading uploaded documents:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // Go back to Upload page, keep same app id
            var upload = new UploadDocumentsPage(_applicationId);
            upload.Show();
            this.Hide();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!chkConfirm.Checked)
            {
                MessageBox.Show("Please confirm the information is accurate.",
                    "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Flip status from Draft -> Pending (record already exists)
            try
            {
                conn.Open();
                string sql = @"UPDATE Funding_Application
                               SET FundingApplicationStatus = ?
                               WHERE FundingApplicationID = ?";

                using (var cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.Add("@p1", OleDbType.VarWChar).Value = "Pending";
                    cmd.Parameters.Add("@p2", OleDbType.Integer).Value = int.Parse(_applicationId);
                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("Application submitted. Status is now Pending.",
                            "Submitted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Navigate anywhere you prefer
                        var dash = new StudentDashboardPage();
                        dash.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("No record updated. Please check the Application ID.",
                            "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating application status:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State != System.Data.ConnectionState.Closed) conn.Close();
            }
        }

        private void lblDocumentsHeading_Click(object sender, EventArgs e)
        {

        }

        private void lblEnteredDocument3_Click(object sender, EventArgs e)
        {

        }

        private void lblEnteredDocument2_Click(object sender, EventArgs e)
        {

        }

        private void lblEnteredDocument1_Click(object sender, EventArgs e)
        {

        }

        private void chkConfirm_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
