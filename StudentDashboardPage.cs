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
    public partial class StudentDashboardPage : Form
    {
        public StudentDashboardPage()
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

        private void btnUpdateProfile_Click(object sender, EventArgs e)
        {
            StudentEditStudentDetailsPage editPage = new StudentEditStudentDetailsPage(Session.LoggedInID);
            editPage.Show();
            this.Hide();
        }

        private void btnCreateFunding_Click(object sender, EventArgs e)
        {
            CreateFundingApplicationPage applicationPage = new CreateFundingApplicationPage(); 
            applicationPage.Show();
            this.Close();
        }

        private void btnFunding_Click(object sender, EventArgs e)
        {
            StudentFundingApplicationForm form = new StudentFundingApplicationForm();
            form.Show();
            this.Hide();
        }
    }
}
