using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LIFT_System
{
    public partial class SignUpPage : Form
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            LoginPage loginPage = new LoginPage();
            loginPage.Show();
            this.Hide();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {

            string role = cbRole.Text.Trim();

            if (string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Please select a role to continue.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbRole.Focus();
                return;
            }

            // Navigate based on role
            if (role == "Student")
            {
                StudentSignUpPage studentPage = new StudentSignUpPage();
                studentPage.Show();
                this.Hide(); // Keep signup accessible later
            }
            else if (role == "Donor")
            {
                DonorSignUpPage donorPage = new DonorSignUpPage();
                donorPage.Show();
                this.Hide();
            }
            else if (role == "Administrator")
            {
                AdministratorSignUpPage administratorPage = new AdministratorSignUpPage();
                administratorPage.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid role selected.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
