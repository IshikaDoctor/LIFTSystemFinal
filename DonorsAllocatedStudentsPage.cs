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
    public partial class DonorsAllocatedStudentsPage : Form
    {
        private readonly OleDbConnection conn =
            new OleDbConnection(
                @"Provider=Microsoft.ACE.OLEDB.16.0;Data Source=C:\Users\User\OneDrive - University of Witwatersrand\Desktop\LIFT.accdb;");

        public DonorsAllocatedStudentsPage()
        {
            InitializeComponent();

            // Grid setup (safe defaults)
            dgvFundedStudents.ReadOnly = true;
            dgvFundedStudents.AllowUserToAddRows = false;
            dgvFundedStudents.AllowUserToDeleteRows = false;
            dgvFundedStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvFundedStudents.MultiSelect = false;
            dgvFundedStudents.AutoGenerateColumns = true;
            dgvFundedStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvFundedStudents.DataBindingComplete += DgvFundedStudents_DataBindingComplete;

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DonorDashboardPage donorDashboardPage = new DonorDashboardPage();
            donorDashboardPage.Show();
            this.Hide();
        }

        private void DonorsAllocatedStudentsPage_Load(object sender, EventArgs e)
        {
            // Only donors (and optionally admins) can see this
            if (string.IsNullOrWhiteSpace(Session.LoggedInID) ||
                (Session.LoggedInRole != "Donor" && Session.LoggedInRole != "Administrator"))
            {
                MessageBox.Show("You must be logged in as a Donor to view funded students.",
                    "Access", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            if (!int.TryParse(Session.LoggedInID, out var donorId))
            {
                MessageBox.Show("Invalid Donor ID in session.",
                    "Session", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            LoadFundedStudents(donorId);
        }

        private void LoadFundedStudents(int donorId)
        {
            try
            {
                // Minimal, bracketed, zero-alias SQL so Access can’t misparse anything
                var sql = @"
            SELECT
                [s].[StudentNo],
                [s].[StudentFirstName],
                [s].[StudentLastName],
                [s].[StudentInstituition],
                [s].[StudentDegreeProgram],
                [fa].[FundingAgreementAmount],
                [fa].[FundingAgreementStatus],
                [fa].[FundingAgreementDateCreated],
                [fa].[FundingAgreementID]
            FROM ([Funding_Agreement] AS fa
                  INNER JOIN [Student] AS s ON [fa].[StudentNo] = [s].[StudentNo])
            WHERE [fa].[DonorID] = ?
            ORDER BY [fa].[FundingAgreementDateCreated] DESC, [fa].[FundingAgreementID] DESC";

                using (var da = new OleDbDataAdapter(sql, conn))
                {
                    da.SelectCommand.Parameters.Add("@p1", OleDbType.Integer).Value = donorId;

                    var dt = new DataTable();
                    da.Fill(dt);
                    dgvFundedStudents.DataSource = dt;
                }

                // Nice headers & formatting AFTER binding (no SQL aliases needed)
                if (dgvFundedStudents.Columns.Contains("StudentNo"))
                {
                    dgvFundedStudents.Columns["StudentNo"].HeaderText = "Student #";
                    dgvFundedStudents.Columns["StudentNo"].FillWeight = 70;
                }
                if (dgvFundedStudents.Columns.Contains("StudentFirstName"))
                    dgvFundedStudents.Columns["StudentFirstName"].HeaderText = "First Name";
                if (dgvFundedStudents.Columns.Contains("StudentLastName"))
                    dgvFundedStudents.Columns["StudentLastName"].HeaderText = "Last Name";
                if (dgvFundedStudents.Columns.Contains("StudentInstituition"))
                    dgvFundedStudents.Columns["StudentInstituition"].HeaderText = "Institution";
                if (dgvFundedStudents.Columns.Contains("StudentDegreeProgram"))
                    dgvFundedStudents.Columns["StudentDegreeProgram"].HeaderText = "Degree";
                if (dgvFundedStudents.Columns.Contains("FundingAgreementAmount"))
                {
                    dgvFundedStudents.Columns["FundingAgreementAmount"].HeaderText = "Amount";
                    dgvFundedStudents.Columns["FundingAgreementAmount"].DefaultCellStyle.Format = "C2";
                }
                if (dgvFundedStudents.Columns.Contains("FundingAgreementStatus"))
                    dgvFundedStudents.Columns["FundingAgreementStatus"].HeaderText = "Status";
                if (dgvFundedStudents.Columns.Contains("FundingAgreementDateCreated"))
                {
                    dgvFundedStudents.Columns["FundingAgreementDateCreated"].HeaderText = "Date Created";
                    dgvFundedStudents.Columns["FundingAgreementDateCreated"].DefaultCellStyle.Format = "yyyy-MM-dd";
                }
                if (dgvFundedStudents.Columns.Contains("FundingAgreementID"))
                {
                    dgvFundedStudents.Columns["FundingAgreementID"].HeaderText = "Agreement ID";
                    dgvFundedStudents.Columns["FundingAgreementID"].FillWeight = 60;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading funded students:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Make the amount show as currency and set friendly column formats
        private void DgvFundedStudents_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dgvFundedStudents.Columns.Contains("Amount"))
            {
                dgvFundedStudents.Columns["Amount"].DefaultCellStyle.Format = "C2";
                dgvFundedStudents.Columns["Amount"].DefaultCellStyle.FormatProvider = CultureInfo.CurrentCulture;
            }
            if (dgvFundedStudents.Columns.Contains("Date Created"))
            {
                dgvFundedStudents.Columns["Date Created"].DefaultCellStyle.Format = "yyyy-MM-dd";
            }
            // Optional: narrow technical IDs
            if (dgvFundedStudents.Columns.Contains("Agreement ID"))
            {
                dgvFundedStudents.Columns["Agreement ID"].FillWeight = 60;
            }
            if (dgvFundedStudents.Columns.Contains("StudentNo"))
            {
                dgvFundedStudents.Columns["StudentNo"].HeaderText = "Student #";
                dgvFundedStudents.Columns["StudentNo"].FillWeight = 70;
            }
        }
    }
}
