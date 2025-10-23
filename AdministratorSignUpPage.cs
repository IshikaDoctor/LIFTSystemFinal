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
    public partial class AdministratorSignUpPage : Form
    {
        private readonly OleDbConnection conn =
            new OleDbConnection(
                @"Provider=Microsoft.ACE.OLEDB.16.0;Data Source=C:\Users\User\OneDrive - University of Witwatersrand\Desktop\LIFT.accdb;");
        public AdministratorSignUpPage()
        {
            InitializeComponent();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            if (!ValidateAdminInputs(out string first, out string last,
                                     out string role, out string email, out string password))
                return;

            if (AdminEmailExists(email))
            {
                MessageBox.Show("An account with this email already exists.", "Duplicate",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            string sql = @"INSERT INTO [Administrator]
                           ([AdministratorFirstName],[AdministratorLastName],
                            [AdministratorRole],[AdministratorContactInfo],
                            [AdministratorPassword])
                           VALUES (?,?,?,?,?)";

            try
            {
                using (var cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@p1", first);
                    cmd.Parameters.AddWithValue("@p2", last);
                    cmd.Parameters.AddWithValue("@p3", role);
                    cmd.Parameters.AddWithValue("@p4", email);
                    cmd.Parameters.AddWithValue("@p5", password);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    if (rows == 1)
                    {
                        MessageBox.Show("Administrator account created successfully.", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        new LoginPage().Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("No record was created. Please try again.",
                            "Insert failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error while creating administrator:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { if (conn.State != ConnectionState.Closed) conn.Close(); }
        }

        private bool AdminEmailExists(string email)
        {
            string sql = "SELECT COUNT(*) FROM [Administrator] WHERE UCASE([AdministratorContactInfo]) = UCASE(?)";
            try
            {
                using (var c = new OleDbConnection(conn.ConnectionString))
                using (var cmd = new OleDbCommand(sql, c))
                {
                    cmd.Parameters.AddWithValue("@p1", email);
                    c.Open();
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking existing administrator: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
        }

        private bool ValidateAdminInputs(out string first, out string last,
                                         out string role, out string email, out string password)
        {
            first = txtFirstName.Text.Trim();
            last = txtLastName.Text.Trim();
            role = cbRole.Text.Trim();
            email = txtEmail.Text.Trim();
            password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(first))
            { MessageBox.Show("Enter first name."); txtFirstName.Focus(); return false; }

            if (string.IsNullOrWhiteSpace(last))
            { MessageBox.Show("Enter last name."); txtLastName.Focus(); return false; }

            if (string.IsNullOrWhiteSpace(role))
            { MessageBox.Show("Select administrator role."); cbRole.Focus(); return false; }

            if (string.IsNullOrWhiteSpace(email) ||
                !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            { MessageBox.Show("Enter a valid email address."); txtEmail.Focus(); return false; }

            if (!IsStrongPassword(password, out string why))
            { MessageBox.Show("Invalid password: " + why, "Password", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtPassword.Focus(); return false; }

            return true;
        }

        private bool IsStrongPassword(string pwd, out string reason)
        {
            reason = "";
            if (string.IsNullOrEmpty(pwd) || pwd.Length < 8) { reason = "must be at least 8 characters."; return false; }
            if (pwd.Contains(" ")) { reason = "must not contain spaces."; return false; }
            if (!Regex.IsMatch(pwd, "[A-Z]")) { reason = "needs at least one uppercase letter."; return false; }
            if (!Regex.IsMatch(pwd, "[a-z]")) { reason = "needs at least one lowercase letter."; return false; }
            if (!Regex.IsMatch(pwd, "[0-9]")) { reason = "needs at least one number."; return false; }
            if (!Regex.IsMatch(pwd, @"[^A-Za-z0-9]")) { reason = "needs at least one symbol."; return false; }
            return true;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            SignUpPage signUpPage = new SignUpPage();
            signUpPage.Show();
            this.Close();

        }
    }
}
