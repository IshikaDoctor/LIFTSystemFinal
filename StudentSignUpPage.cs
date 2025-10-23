using System;
using System.Data;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace LIFT_System
{
    public partial class StudentSignUpPage : Form
    {
        // Keep the connection string, but don't reuse the same OleDbConnection instance
        private const string ConnString =
            @"Provider=Microsoft.ACE.OLEDB.16.0;Data Source=C:\Users\User\OneDrive - University of Witwatersrand\Desktop\LIFT.accdb;";

        public StudentSignUpPage()
        {
            InitializeComponent();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            if (!ValidateStudentInputs(out string first, out string last, out DateTime dob,
                                       out string email, out string inst, out string degree,
                                       out int meansGrade, out string password))
                return;

            // 1) Duplicate check (case-insensitive)
            if (StudentEmailExists(email))
            {
                MessageBox.Show("An account with this email already exists.", "Duplicate",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            // 2) INSERT — COLUMN NAMES FIXED to match your table EXACTLY:
            //    StudentInstitution, StudentFinancialMeansTestGrad
            string sql = @"INSERT INTO [Student]
               ([StudentFirstName],[StudentLastName],[StudentDOB],
                [StudentContactInfo],[StudentInstituition],[StudentDegreeProgram],
                [StudentFinancialMeansTestGrade],[StudentPassword])
               VALUES (?,?,?,?,?,?,?,?)";

            try
            {
                using (var conn = new OleDbConnection(ConnString))
                using (var cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@p1", first);
                    cmd.Parameters.AddWithValue("@p2", last);
                    cmd.Parameters.AddWithValue("@p3", dob);          // Date/Time
                    cmd.Parameters.AddWithValue("@p4", email);
                    cmd.Parameters.AddWithValue("@p5", inst);
                    cmd.Parameters.AddWithValue("@p6", degree);
                    cmd.Parameters.AddWithValue("@p7", meansGrade);   // Number/Integer
                    cmd.Parameters.AddWithValue("@p8", password);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();

                    if (rows == 1)
                    {
                        MessageBox.Show("Student account created successfully.", "Success",
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
                MessageBox.Show("Database error while creating student:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------- Helpers ----------

        private bool StudentEmailExists(string email)
        {
            const string sql =
                "SELECT COUNT(*) FROM [Student] WHERE UCASE([StudentContactInfo]) = UCASE(?)";

            try
            {
                using (var conn = new OleDbConnection(ConnString))
                using (var cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@p1", email);
                    conn.Open();
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                // Surface the real problem so we don't get “mystery duplicates”
                MessageBox.Show("Error while checking if email exists:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true; // fail closed to avoid duplicates
            }
        }

        private bool ValidateStudentInputs(out string first, out string last, out DateTime dob,
                                           out string email, out string institution,
                                           out string degree, out int fmGrade, out string password)
        {
            first = txtFirstName.Text.Trim();
            last = txtLastName.Text.Trim();
            dob = dtpDOB.Value.Date;
            email = txtEmail.Text.Trim();
            institution = txtInstitution.Text.Trim();
            degree = txtDegree.Text.Trim();
            password = txtPassword.Text;   // don’t Trim passwords; we validate spaces
            fmGrade = 0;

            if (string.IsNullOrWhiteSpace(first))
            { MessageBox.Show("Enter first name."); txtFirstName.Focus(); return false; }

            if (string.IsNullOrWhiteSpace(last))
            { MessageBox.Show("Enter last name."); txtLastName.Focus(); return false; }

            if (dob > DateTime.Today)
            { MessageBox.Show("Date of birth cannot be in the future."); dtpDOB.Focus(); return false; }

            if (string.IsNullOrWhiteSpace(email) ||
                !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            { MessageBox.Show("Enter a valid email address."); txtEmail.Focus(); return false; }

            if (string.IsNullOrWhiteSpace(institution))
            { MessageBox.Show("Enter institution."); txtInstitution.Focus(); return false; }

            if (string.IsNullOrWhiteSpace(degree))
            { MessageBox.Show("Enter degree."); txtDegree.Focus(); return false; }

            if (!int.TryParse(txtFMTG.Text.Trim(), out fmGrade) || fmGrade < 0 || fmGrade > 100)
            { MessageBox.Show("Enter a valid Financial Means Test Grade (0–100)."); txtFMTG.Focus(); return false; }

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

        private void label6_Click(object sender, EventArgs e) { }
        
        private void label4_Click(object sender, EventArgs e) { }

        private void btnBack_Click(object sender, EventArgs e)
        {
            SignUpPage signUpPage = new SignUpPage();
            signUpPage.Show();
            this.Close();
        }
    }
}
