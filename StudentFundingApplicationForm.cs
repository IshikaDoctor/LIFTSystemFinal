using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LIFT_System
{
    public partial class StudentFundingApplicationForm : Form
    {
        private readonly OleDbConnection conn =
           new OleDbConnection(
               @"Provider=Microsoft.ACE.OLEDB.16.0;Data Source=C:\Users\User\OneDrive - University of Witwatersrand\Desktop\LIFT.accdb;");


        public StudentFundingApplicationForm()
        {
            InitializeComponent();
            dgvMyApps.ReadOnly = true;
            dgvMyApps.AllowUserToAddRows = false;
            dgvMyApps.AllowUserToDeleteRows = false;
            dgvMyApps.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMyApps.MultiSelect = false;
            dgvMyApps.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // show all headers nicely
            dgvMyApps.AutoGenerateColumns = true; // <-- show all columns from table
        }
        
        

        private void LoadMyApplications()
        {
            try
            {
                if (!int.TryParse(Session.LoggedInID, out int studentNo))
                {
                    MessageBox.Show("Your Student ID in session is not numeric.", "Data",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string sql = @"SELECT *
                               FROM Funding_Application
                               WHERE StudentNo = ?
                               ORDER BY FundingApplicationID DESC";

                using (var da = new OleDbDataAdapter(sql, conn))
                {
                    da.SelectCommand.Parameters.Add("@p1", OleDbType.Integer).Value = studentNo;

                    var dt = new DataTable();
                    da.Fill(dt);
                    dgvMyApps.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading your applications:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            StudentDashboardPage studentDashboardPage = new StudentDashboardPage();
            studentDashboardPage.Show();
            this.Hide();
        }

        private void StudentFundingApplicationForm_Load(object sender, EventArgs e)
        {
            // Must be a student
            if (string.IsNullOrWhiteSpace(Session.LoggedInID) || Session.LoggedInRole != "Student")
            {
                MessageBox.Show("You must be logged in as a Student to view your applications.",
                    "Access", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            // Load data
            LoadMyApplications();
        }
    }
}
