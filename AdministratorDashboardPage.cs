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
    public partial class AdministratorDashboardPage : Form
    {
        public AdministratorDashboardPage()
        {
            InitializeComponent();
        }

        private void btnUpdateStudent_Click(object sender, EventArgs e)
        {
            SearchStudentRecordPage searchStudentRecordPage = new SearchStudentRecordPage();
            searchStudentRecordPage.Show();
            this.Hide();
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
            EditAdminDetailsPage editAdminDetailsPage = new EditAdminDetailsPage();
            editAdminDetailsPage.Show();
            this.Hide();
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            GenerateReportPage generateReportPage = new GenerateReportPage();
            generateReportPage.Show();
            this.Hide();
        }
    }
}
