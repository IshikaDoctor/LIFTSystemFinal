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
    public partial class EditAdminDetailsPage : Form
    {
        private readonly OleDbConnection conn =
           new OleDbConnection(
               @"Provider=Microsoft.ACE.OLEDB.16.0;Data Source=C:\Users\User\OneDrive - University of Witwatersrand\Desktop\LIFT.accdb;");

        private int _adminId;

        // keep originals to detect “no changes”
        private string _origFirst = "";
        private string _origLast = "";
        private string _origPass = "";
        private string _origRole = "";

        private static readonly string[] AllowedRoles = new[]
        {
            "Relationship Manager",
            "Processing Officer",
            "Reports Analyst"
        };

        public EditAdminDetailsPage()
        {
            InitializeComponent();
            InitForm();
            LoadAdminFromSession();
        }
        private void InitForm()
        {
            cbRole.Items.Clear();
            cbRole.Items.AddRange(AllowedRoles);
            cbRole.SelectedIndex = -1;

            if (txtPassword != null) txtPassword.MaxLength = 100;

            // optional: mask password while typing
            // txtPassword.UseSystemPasswordChar = true;
        }

        private void LoadAdminFromSession()
        {
            if (Session.LoggedInRole != "Administrator")
            {
                MessageBox.Show("Only administrators can edit administrator details.",
                    "Access", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            if (!int.TryParse(Session.LoggedInID, out _adminId))
            {
                MessageBox.Show("Invalid Administrator ID in session.",
                    "Session", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            try
            {
                conn.Open();
                string sql = @"SELECT AdministratorFirstName, AdministratorLastName,
                                      AdministratorPassword, AdministratorRole
                               FROM Administrator
                               WHERE AdministratorID = ?";

                using (var cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.Add("@p1", OleDbType.Integer).Value = _adminId;

                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (rdr != null && rdr.Read())
                        {
                            _origFirst = Convert.ToString(rdr["AdministratorFirstName"]) ?? "";
                            _origLast = Convert.ToString(rdr["AdministratorLastName"]) ?? "";
                            _origPass = Convert.ToString(rdr["AdministratorPassword"]) ?? "";
                            _origRole = Convert.ToString(rdr["AdministratorRole"]) ?? "";

                            txtFirstName.Text = _origFirst;
                            txtLastName.Text = _origLast;
                            txtPassword.Text = _origPass;

                            int idx = cbRole.FindStringExact(_origRole);
                            cbRole.SelectedIndex = idx; // -1 if not found
                        }
                        else
                        {
                            MessageBox.Show("Administrator record not found.",
                                "Not found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading administrator details: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State != System.Data.ConnectionState.Closed) conn.Close();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            AdministratorDashboardPage administratorDashboardPage = new AdministratorDashboardPage();  
            administratorDashboardPage.Show();
            this.Hide();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string first = (txtFirstName?.Text ?? "").Trim();
            string last = (txtLastName?.Text ?? "").Trim();
            string pass = (txtPassword?.Text ?? "").Trim();
            string role = (cbRole?.Text ?? "").Trim();

            // --- required ---
            if (string.IsNullOrWhiteSpace(first) ||
                string.IsNullOrWhiteSpace(last) ||
                string.IsNullOrWhiteSpace(pass) ||
                string.IsNullOrWhiteSpace(role))
            {
                MessageBox.Show("All fields are required.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- names: letters / space / - / ' ---
            var nameRe = new Regex(@"^[A-Za-z' -]+$");
            if (!nameRe.IsMatch(first))
            {
                MessageBox.Show("First name contains invalid characters.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFirstName.Focus();
                return;
            }
            if (!nameRe.IsMatch(last))
            {
                MessageBox.Show("Last name contains invalid characters.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLastName.Focus();
                return;
            }

            // --- password strength (very basic) ---
            if (pass.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            // --- role must be one of allowed ---
            bool roleOK = Array.Exists(AllowedRoles, r => r.Equals(role, StringComparison.OrdinalIgnoreCase));
            if (!roleOK)
            {
                MessageBox.Show("Please choose a valid role from the list.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbRole.Focus();
                return;
            }

            // --- nothing changed? ---
            if (first == _origFirst && last == _origLast && pass == _origPass && role == _origRole)
            {
                MessageBox.Show("No changes to save.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // --- save ---
            try
            {
                conn.Open();
                string sql = @"UPDATE Administrator
                               SET AdministratorFirstName = ?,
                                   AdministratorLastName  = ?,
                                   AdministratorPassword  = ?,
                                   AdministratorRole      = ?
                               WHERE AdministratorID = ?";

                using (var cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.Add("@p1", OleDbType.VarWChar).Value = first;
                    cmd.Parameters.Add("@p2", OleDbType.VarWChar).Value = last;
                    cmd.Parameters.Add("@p3", OleDbType.VarWChar).Value = pass;  // (hash later if needed)
                    cmd.Parameters.Add("@p4", OleDbType.VarWChar).Value = role;
                    cmd.Parameters.Add("@p5", OleDbType.Integer).Value = _adminId;

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        // update originals so subsequent edits compare correctly
                        _origFirst = first; _origLast = last; _origPass = pass; _origRole = role;

                        MessageBox.Show("Administrator profile updated.",
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No changes saved (record not found).",
                            "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving administrator details: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State != System.Data.ConnectionState.Closed) conn.Close();
            }
        }
    }
}
