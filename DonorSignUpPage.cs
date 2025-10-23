using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace LIFT_System
{
    public partial class DonorSignUpPage : Form
    {
        private readonly OleDbConnection conn =
            new OleDbConnection(
                @"Provider=Microsoft.ACE.OLEDB.16.0;Data Source=C:\Users\User\OneDrive - University of Witwatersrand\Desktop\LIFT.accdb;");

        public DonorSignUpPage()
        {
            InitializeComponent();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            if (!ValidateDonorInputs(out string first, out string last, out string type,
                                     out string email, out string pref, out decimal balance,
                                     out string password))
                return;

            // === 1) Duplicate-email check ===
            if (DonorEmailExists(email))
            {
                MessageBox.Show("An account with this email already exists.", "Duplicate",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            // === 2) Insert ===
            string sql = @"INSERT INTO [Donor]
                           ([DonorFirstName],[DonorLastName],[DonorType],
                            [DonorContactInfo],[DonorContactFundingPreference],
                            [DonorContactAvailableBalance],[DonorPassword])
                           VALUES (?,?,?,?,?,?,?)";

            try
            {
                using (var cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@p1", first);
                    cmd.Parameters.AddWithValue("@p2", last);
                    cmd.Parameters.AddWithValue("@p3", type);
                    cmd.Parameters.AddWithValue("@p4", email);
                    cmd.Parameters.AddWithValue("@p5", pref);
                    cmd.Parameters.AddWithValue("@p6", balance);
                    cmd.Parameters.AddWithValue("@p7", password);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    if (rows == 1)
                    {
                        MessageBox.Show("Donor account created successfully.", "Success",
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
                MessageBox.Show("Database error while creating donor:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { if (conn.State != ConnectionState.Closed) conn.Close(); }
        }

        private bool DonorEmailExists(string email)
        {
            string sql = "SELECT COUNT(*) FROM [Donor] WHERE UCASE([DonorContactInfo]) = UCASE(?)";
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
                MessageBox.Show("Error checking existing donor: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true; // fail closed: treat as existing to prevent duplicate
            }
        }

        private bool ValidateDonorInputs(out string first, out string last, out string type,
                                         out string email, out string pref, out decimal balance,
                                         out string password)
        {
            first = txtFirstName.Text.Trim();
            last = txtLastName.Text.Trim();
            type = cbType.Text.Trim();
            email = txtEmail.Text.Trim();
            pref = txtFundingPref.Text.Trim();
            password = txtPassword.Text; // don't Trim passwords
            balance = 0m;

            if (string.IsNullOrWhiteSpace(first))
            { MessageBox.Show("Enter first name."); txtFirstName.Focus(); return false; }

            if (string.IsNullOrWhiteSpace(last))
            { MessageBox.Show("Enter last name."); txtLastName.Focus(); return false; }

            if (string.IsNullOrWhiteSpace(type))
            { MessageBox.Show("Select donor type."); cbType.Focus(); return false; }

            if (string.IsNullOrWhiteSpace(email) ||
                !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            { MessageBox.Show("Enter a valid email address."); txtEmail.Focus(); return false; }

            if (string.IsNullOrWhiteSpace(pref))
            { MessageBox.Show("Enter funding preferences."); txtFundingPref.Focus(); return false; }

            if (!decimal.TryParse(txtBal.Text.Trim(), out balance) || balance < 0)
            { MessageBox.Show("Enter a valid non-negative available balance."); txtBal.Focus(); return false; }

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

        private void btnBack_Click(object sender, EventArgs e)
        {
            SignUpPage signUpPage = new SignUpPage();
            signUpPage.Show();
            this.Close();
        }
    }
}
