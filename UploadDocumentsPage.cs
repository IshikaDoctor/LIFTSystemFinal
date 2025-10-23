using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LIFT_System
{
    public partial class UploadDocumentsPage : Form
    {
        
        private readonly string _applicationId;         // passed from FundingDetailsPage
        private readonly string _studentId;             // from Session.LoggedInID
        private string _saveRoot;                       // ./UploadedFiles/<applicationId>/


        public UploadDocumentsPage(string applicationId)
        {
            InitializeComponent();
            _applicationId = applicationId ?? "APP-UNKNOWN";
            _studentId = string.IsNullOrWhiteSpace(Session.LoggedInID) ? "UNKNOWN" : Session.LoggedInID.Trim();

            // project folder + UploadedFiles/<applicationId>/
            string projectRoot = AppDomain.CurrentDomain.BaseDirectory; // bin/Debug/...; still fine for now
            _saveRoot = Path.Combine(projectRoot, "UploadedFiles", _applicationId);
            Directory.CreateDirectory(_saveRoot);

            // clear any previous labels
            
        }

        private void UploadDocumentsPage_Load(object sender, EventArgs e)
        {

        }

        private void btnUploadRegistration_Click(object sender, EventArgs e)
        {
            // PDF only for Proof of Registration
            if (PickAndSave("PDF files (*.pdf)|*.pdf", out string savedPath))
            {
                lblExampleRegistration.Text = Path.GetFileName(savedPath);
            }

        }

        private void btnUploadFeeStatement_Click(object sender, EventArgs e)
        {
            // PDF only for Fee Statement
            if (PickAndSave("PDF files (*.pdf)|*.pdf", out string savedPath))
            {
                lblExampleFee.Text = Path.GetFileName(savedPath);
            }
        }

        private void btnUploadidDocument_Click(object sender, EventArgs e)
        {
            // ID can be image or PDF
            if (PickAndSave("Images/PDF (*.jpg;*.jpeg;*.png;*.pdf)|*.jpg;*.jpeg;*.png;*.pdf", out string savedPath))
            {
                lblExampleID.Text = Path.GetFileName(savedPath);
            }
        }

        private bool PickAndSave(string filter, out string savedPath)
        {
            savedPath = null;

            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = filter;
                ofd.Title = "Select file to upload";
                ofd.Multiselect = false;
                ofd.CheckFileExists = true;
                ofd.CheckPathExists = true;

                if (ofd.ShowDialog() != DialogResult.OK) return false;

                try
                {
                    string srcPath = ofd.FileName;
                    string originalName = Path.GetFileName(srcPath);
                    string renamed = $"{_studentId}_{_applicationId}_{originalName}";
                    string destPath = Path.Combine(_saveRoot, renamed);

                    // copy (overwrite if the user re-uploads)
                    File.Copy(srcPath, destPath, true);

                    savedPath = destPath;
                    MessageBox.Show($"Saved:\n{destPath}", "Uploaded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not save file: " + ex.Message, "Upload error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        private void btnBuck_Click(object sender, EventArgs e)
        {
            CreateFundingApplicationPage createFundingApplicationPage = new CreateFundingApplicationPage();
            createFundingApplicationPage.Show();
            this.Hide();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            SubmitApplicationPage page = new SubmitApplicationPage(_applicationId);
            page.Show();
            this.Hide();
        }
    }
}
