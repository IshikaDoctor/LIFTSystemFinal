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
    public partial class SearchStudentRecordPage : Form
    {
        // === Database connection ===
        private readonly OleDbConnection conn =
            new OleDbConnection(
                @"Provider=Microsoft.ACE.OLEDB.16.0;Data Source=C:\Users\User\OneDrive - University of Witwatersrand\Desktop\LIFT.accdb;");

        public SearchStudentRecordPage()
        {
            InitializeComponent();
        }


        // ===== Back Button =====
        private void btnBackToDashboard_Click(object sender, EventArgs e)
        {
            AdministratorDashboardPage adminPage = new AdministratorDashboardPage();
            adminPage.Show();
            this.Hide();
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            string searchBy = cboSearchTypes?.Text?.Trim() ?? "";
            string searchTerm = txtEnteredSearchTerm?.Text?.Trim() ?? "";

            // Validation
            if (string.IsNullOrWhiteSpace(searchBy))
            {
                MessageBox.Show("Please select a search option.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                MessageBox.Show("Please enter a search term.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Build SQL query
            string sql = "";
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;

            if (searchBy == "Name")
            {
                sql = "SELECT StudentNo, StudentFirstName, StudentLastName, StudentInstituition " +
                      "FROM Student WHERE StudentFirstName LIKE ? OR StudentLastName LIKE ?";
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@p1", "%" + searchTerm + "%");
                cmd.Parameters.AddWithValue("@p2", "%" + searchTerm + "%");
            }
            else if (searchBy == "ID")
            {
                sql = "SELECT StudentNo, StudentFirstName, StudentLastName, StudentInstituition " +
                      "FROM Student WHERE StudentNo = ?";
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@p1", searchTerm);
            }
            else if (searchBy == "Institution")
            {
                sql = "SELECT StudentNo, StudentFirstName, StudentLastName, StudentInstituition " +
                      "FROM Student WHERE StudentInstitution LIKE ?";
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@p1", "%" + searchTerm + "%");
            }

            try
            {
                conn.Open();
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    dgvApplicants.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("No matching student records found.",
                        "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvApplicants.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State != ConnectionState.Closed) conn.Close();
            }
        }

        private void dataGridViewResults_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ignore header row
            {
                // Get selected StudentNo from first column
                string studentNo = dgvApplicants.Rows[e.RowIndex].Cells["StudentNo"].Value.ToString();

                // Pass StudentNo to new form
                AdminEditStudentDetailsPage detailsPage = new AdminEditStudentDetailsPage(studentNo);
                detailsPage.Show();
                this.Hide();
            }
        }
    }
}
