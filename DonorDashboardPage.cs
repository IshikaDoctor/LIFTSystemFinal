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
    public partial class DonorDashboardPage : Form
    {
        public DonorDashboardPage()
        {
            InitializeComponent();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Session.LoggedInID = null;
            Session.LoggedInRole = null;
            Session.LoggedInName = null;

            LoginPage loginPage = new LoginPage();
            loginPage.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EditDonorDetailsPage editDonor  = new EditDonorDetailsPage();
            editDonor.Show();
            this.Hide();
        }

        private void btnAllocated_Click(object sender, EventArgs e)
        {
            DonorsAllocatedStudentsPage donorsAllocatedStudentsPage = new DonorsAllocatedStudentsPage();
            donorsAllocatedStudentsPage.Show();
            this.Hide();
        }
    }
}
