using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LIFT_System
{
    public partial class StudentEditStudentDetailsPage : Form
    {
        private string studentNo;

        private readonly OleDbConnection conn =
            new OleDbConnection(
                @"Provider=Microsoft.ACE.OLEDB.16.0;Data Source=C:\Users\User\OneDrive - University of Witwatersrand\Desktop\LIFT.accdb;");

        public StudentEditStudentDetailsPage(string studentNo)
        {
            InitializeComponent();
            this.studentNo = studentNo;
            LoadStudentDetails();
        }

        private void LoadStudentDetails()
        {
            try
            {
                string sql = "SELECT * FROM Student WHERE StudentNo = ?";
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.Parameters.AddWithValue("@p1", studentNo);

                conn.Open();
                OleDbDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    txtFirstName.Text = rdr["StudentFirstName"].ToString();
                    txtLastName.Text = rdr["StudentLastName"].ToString();
                    dtpDOB.Value = Convert.ToDateTime(rdr["StudentDOB"]);
                    txtDegree.Text = rdr["StudentDegreeProgram"].ToString();
                    txtInstitution.Text = rdr["StudentInstituition"].ToString();
                    txtEmail.Text = rdr["StudentContactInfo"].ToString();
                    txtPassword.Text = rdr["StudentPassword"].ToString();
                    txtFMTG.Text = rdr["StudentFinancialMeansTestGrade"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading student details: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }


        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtDegree.Text) ||
                string.IsNullOrWhiteSpace(txtInstitution.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                string.IsNullOrWhiteSpace(txtFMTG.Text))
            {
                MessageBox.Show("All fields are required.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Invalid email format.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtFMTG.Text, out _))
            {
                MessageBox.Show("Financial Means Test Grade must be numeric.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string sql =
                    "UPDATE Student SET StudentFirstName = ?, StudentLastName = ?, StudentDOB = ?, " +
                    "StudentDegreeProgram = ?, StudentInstituition = ?, StudentContactInfo = ?, " +
                    "StudentPassword = ?, StudentFinancialMeansTestGrade = ? WHERE StudentNo = ?";

                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.Parameters.AddWithValue("@p1", txtFirstName.Text.Trim());
                cmd.Parameters.AddWithValue("@p2", txtLastName.Text.Trim());
                cmd.Parameters.AddWithValue("@p3", dtpDOB.Value.ToString("yyyy/MM/dd"));
                cmd.Parameters.AddWithValue("@p4", txtDegree.Text.Trim());
                cmd.Parameters.AddWithValue("@p5", txtInstitution.Text.Trim());
                cmd.Parameters.AddWithValue("@p6", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@p7", txtPassword.Text.Trim());
                cmd.Parameters.AddWithValue("@p8", txtFMTG.Text.Trim());
                cmd.Parameters.AddWithValue("@p9", studentNo);

                conn.Open();
                cmd.ExecuteNonQuery();

                MessageBox.Show("Student details updated successfully.",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                StudentDashboardPage studentDashboardPage = new StudentDashboardPage();
                studentDashboardPage.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving changes: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            StudentDashboardPage studentDashboardPage = new StudentDashboardPage();
            studentDashboardPage.Show();
            this.Hide();
        }
    }
}
