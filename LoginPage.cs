using System;
using System.Data;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace LIFT_System
{
    public partial class LoginPage : Form
    {
        private readonly OleDbConnection conn =
            new OleDbConnection(
                @"Provider=Microsoft.ACE.OLEDB.16.0;Data Source=C:\Users\User\OneDrive - University of Witwatersrand\Desktop\LIFT.accdb;");

        public LoginPage()
        {
            InitializeComponent();

            // Optional: ensure the role dropdown starts blank
            if (cbRole != null) cbRole.SelectedIndex = -1;
        }

        // ===== Login button =====
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string role = cbRole?.Text?.Trim() ?? "";
            string email = (txtEmail?.Text ?? "").Trim();
            string password = txtPassword?.Text ?? "";

            // --- Basic validation ---
            if (string.IsNullOrWhiteSpace(role))
            {
                MessageBox.Show("Please select a Role.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbRole?.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Please enter your Email Address.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail?.Focus();
                return;
            }

            // lenient email check
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Please enter a valid Email Address.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail?.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter your Password.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword?.Focus();
                return;
            }

            // --- Map Role -> table/columns (matches your screenshots) ---
            string table, idCol, firstCol, lastCol, emailCol, passCol;

            try
            {
                GetRoleMapping(role, out table, out idCol, out firstCol, out lastCol, out emailCol, out passCol);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Role", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Parameterized SELECT (OleDb uses ? placeholders in order)
            string sql = $"SELECT TOP 1 [{idCol}], [{firstCol}], [{lastCol}] " +
                         $"FROM [{table}] WHERE [{emailCol}] = ? AND [{passCol}] = ?";

            try
            {
                using (var cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@p1", email);
                    cmd.Parameters.AddWithValue("@p2", password);

                    conn.Open();
                    using (var rdr = cmd.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (rdr != null && rdr.Read())
                        {
                            int id = Convert.ToInt32(rdr[idCol]);
                            string fname = Convert.ToString(rdr[firstCol]) ?? "";
                            string lname = Convert.ToString(rdr[lastCol]) ?? "";
                            string full = (fname + " " + lname).Trim();

                            MessageBox.Show($"Welcome, {full}.", "Login successful",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            //here to store a session
                            Session.LoggedInID = id.ToString();
                            Session.LoggedInRole = role;
                            Session.LoggedInName = full;


                            if (role == "Student")
                            {
                                StudentDashboardPage studentDashboardPage = new StudentDashboardPage(); 
                                studentDashboardPage.Show();
                                this.Hide();
                            }else if (role == "Donor")
                            {
                                DonorDashboardPage donorDashboardPage = new DonorDashboardPage();
                                donorDashboardPage.Show();
                                this.Hide();
                            }
                            else
                            {
                                AdministratorDashboardPage administratorDashboardPage = new AdministratorDashboardPage();
                                administratorDashboardPage.Show();
                                this.Hide();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid email or password for the selected role.",
                                "Login failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
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

        /// <summary>
        /// Maps UI role to the actual table and column names in your Access schema.
        /// </summary>
        private static void GetRoleMapping(
            string role,
            out string table, out string idCol, out string firstCol, out string lastCol,
            out string emailCol, out string passCol)
        {
            switch (role)
            {
                case "Student":
                    table = "Student";
                    idCol = "StudentNo";
                    firstCol = "StudentFirstName";
                    lastCol = "StudentLastName";
                    emailCol = "StudentContactInfo"; // email lives here
                    passCol = "StudentPassword";
                    break;

                case "Donor":
                    table = "Donor";
                    idCol = "DonorID";
                    firstCol = "DonorFirstName";
                    lastCol = "DonorLastName";
                    emailCol = "DonorContactInfo";
                    passCol = "DonorPassword";
                    break;

                case "Administrator":
                    table = "Administrator";
                    idCol = "AdministratorID";
                    firstCol = "AdministratorFirstName";
                    lastCol = "AdministratorLastName";
                    emailCol = "AdministratorContactInfo";
                    passCol = "AdministratorPassword";
                    break;

                default:
                    throw new ArgumentException("Unknown role selected.");
            }
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            SignUpPage signUpPage = new SignUpPage();
            signUpPage.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
