using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LIFT_System
{
    public partial class EditDonorDetailsPage : Form
    {
        private readonly OleDbConnection conn =
         new OleDbConnection(
             @"Provider=Microsoft.ACE.OLEDB.16.0;Data Source=C:\Users\User\OneDrive - University of Witwatersrand\Desktop\LIFT.accdb;");

        private int _donorId;

        // keep originals to detect “no changes”
        private string _origFirst = "";
        private string _origLast = "";
        private string _origPass = "";
        private string _origType = "";
        private string _origFundingPref = "";
        private decimal _origBalance = 0m;

        private static readonly string[] AllowedTypes = new[]
        {
            "Individual",
            "Organisation"
        };

        public EditDonorDetailsPage()
        {
            InitializeComponent();
            InitForm();
            LoadDonorFromSession();
        }
        private void InitForm()
        {
            // type drop-down
            cbType.Items.Clear();
            cbType.Items.AddRange(AllowedTypes);
            cbType.SelectedIndex = -1;

            if (txtPassword != null) txtPassword.MaxLength = 100;
            // txtPassword.UseSystemPasswordChar = true; // optional masking
        }

        private void LoadDonorFromSession()
        {
            // Allow Donor or Administrator (adjust if you want stricter)
            if (Session.LoggedInRole != "Donor" && Session.LoggedInRole != "Administrator")
            {
                MessageBox.Show("Only donors or administrators can edit donor details.",
                    "Access", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            if (!int.TryParse(Session.LoggedInID, out _donorId))
            {
                MessageBox.Show("Invalid Donor ID in session.",
                    "Session", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            try
            {
                conn.Open();
                string sql = @"
                    SELECT DonorFirstName,
                           DonorLastName,
                           DonorPassword,
                           DonorType,
                           DonorContactFundingPreference,
                           DonorContactAvailableBalance
                    FROM Donor
                    WHERE DonorID = ?";

                using (var cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.Add("@p1", OleDbType.Integer).Value = _donorId;

                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (rdr != null && rdr.Read())
                        {
                            _origFirst = Convert.ToString(rdr["DonorFirstName"]) ?? "";
                            _origLast = Convert.ToString(rdr["DonorLastName"]) ?? "";
                            _origPass = Convert.ToString(rdr["DonorPassword"]) ?? "";
                            _origType = Convert.ToString(rdr["DonorType"]) ?? "";
                            _origFundingPref = Convert.ToString(rdr["DonorContactFundingPreference"]) ?? "";
                            _origBalance = rdr["DonorContactAvailableBalance"] == DBNull.Value
                                ? 0m
                                : Convert.ToDecimal(rdr["DonorContactAvailableBalance"]);

                            // populate controls
                            txtFirstName.Text = _origFirst;
                            txtLastName.Text = _origLast;
                            txtPassword.Text = _origPass;
                            txtFundingPref.Text = _origFundingPref;
                            txtBal.Text = _origBalance.ToString("0.00", CultureInfo.InvariantCulture);

                            int idx = cbType.FindStringExact(_origType);
                            cbType.SelectedIndex = idx; // -1 if not found (forces validation)
                        }
                        else
                        {
                            MessageBox.Show("Donor record not found.",
                                "Not found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading donor details: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State != ConnectionState.Closed) conn.Close();
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DonorDashboardPage donorDashboardPage = new DonorDashboardPage();
            donorDashboardPage.Show();
            this.Hide();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string first = (txtFirstName?.Text ?? "").Trim();
            string last = (txtLastName?.Text ?? "").Trim();
            string pass = (txtPassword?.Text ?? "").Trim();
            string type = (cbType?.Text ?? "").Trim();
            string fundPref = (txtFundingPref?.Text ?? "").Trim();
            string balText = (txtBal?.Text ?? "").Trim();

            // --- required ---
            if (string.IsNullOrWhiteSpace(first) ||
                string.IsNullOrWhiteSpace(last) ||
                string.IsNullOrWhiteSpace(pass) ||
                string.IsNullOrWhiteSpace(type) ||
                string.IsNullOrWhiteSpace(balText))
            {
                MessageBox.Show("First Name, Last Name, Password, Type, and Available Balance are required.",
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

            // --- password strength (basic) ---
            if (pass.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            // --- type must be allowed ---
            bool typeOK = Array.Exists(AllowedTypes, t => t.Equals(type, StringComparison.OrdinalIgnoreCase));
            if (!typeOK)
            {
                MessageBox.Show("Please choose a valid donor type from the list.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbType.Focus();
                return;
            }

            // --- available balance numeric (Currency) ---
            // Accept both "1234.56" and "R 1,234.56" styles; normalize with CultureInfo
            if (!decimal.TryParse(balText,
                NumberStyles.AllowCurrencySymbol | NumberStyles.Number,
                CultureInfo.CurrentCulture, out decimal balance))
            {
                // try invariant as fallback
                if (!decimal.TryParse(balText,
                    NumberStyles.AllowCurrencySymbol | NumberStyles.Number,
                    CultureInfo.InvariantCulture, out balance))
                {
                    MessageBox.Show("Available Balance is not a valid number.",
                        "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtBal.Focus();
                    return;
                }
            }
            if (balance < 0m)
            {
                MessageBox.Show("Available Balance cannot be negative.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBal.Focus();
                return;
            }

            // --- nothing changed? ---
            if (first == _origFirst &&
                last == _origLast &&
                pass == _origPass &&
                type.Equals(_origType, StringComparison.OrdinalIgnoreCase) &&
                fundPref == _origFundingPref &&
                balance == _origBalance)
            {
                MessageBox.Show("No changes to save.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // --- save ---
            try
            {
                conn.Open();
                string sql = @"
                    UPDATE Donor
                    SET DonorFirstName = ?,
                        DonorLastName  = ?,
                        DonorPassword  = ?,
                        DonorType      = ?,
                        DonorContactFundingPreference = ?,
                        DonorContactAvailableBalance  = ?
                    WHERE DonorID = ?";

                using (var cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.Add("@p1", OleDbType.VarWChar).Value = first;
                    cmd.Parameters.Add("@p2", OleDbType.VarWChar).Value = last;
                    cmd.Parameters.Add("@p3", OleDbType.VarWChar).Value = pass; // hash later if needed
                    cmd.Parameters.Add("@p4", OleDbType.VarWChar).Value = type;
                    cmd.Parameters.Add("@p5", OleDbType.VarWChar).Value = (object)fundPref ?? DBNull.Value;
                    cmd.Parameters.Add("@p6", OleDbType.Currency).Value = balance; // Access Currency maps well to decimal
                    cmd.Parameters.Add("@p7", OleDbType.Integer).Value = _donorId;

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        // refresh originals
                        _origFirst = first;
                        _origLast = last;
                        _origPass = pass;
                        _origType = type;
                        _origFundingPref = fundPref;
                        _origBalance = balance;

                        MessageBox.Show("Donor profile updated.",
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
                MessageBox.Show("Error saving donor details: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State != ConnectionState.Closed) conn.Close();
            }
        }
    }
}
