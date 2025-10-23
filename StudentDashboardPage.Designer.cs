namespace LIFT_System
{
    partial class StudentDashboardPage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnUpdateProfile = new System.Windows.Forms.Button();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.btnCreateFunding = new System.Windows.Forms.Button();
            this.btnFunding = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnUpdateProfile
            // 
            this.btnUpdateProfile.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnUpdateProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateProfile.Location = new System.Drawing.Point(377, 299);
            this.btnUpdateProfile.Name = "btnUpdateProfile";
            this.btnUpdateProfile.Size = new System.Drawing.Size(260, 100);
            this.btnUpdateProfile.TabIndex = 0;
            this.btnUpdateProfile.Text = "Update Profile";
            this.btnUpdateProfile.UseVisualStyleBackColor = false;
            this.btnUpdateProfile.Click += new System.EventHandler(this.btnUpdateProfile_Click);
            // 
            // btnLogOut
            // 
            this.btnLogOut.BackColor = System.Drawing.Color.RosyBrown;
            this.btnLogOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogOut.Location = new System.Drawing.Point(442, 518);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(140, 60);
            this.btnLogOut.TabIndex = 1;
            this.btnLogOut.Text = "Log Out";
            this.btnLogOut.UseVisualStyleBackColor = false;
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            // 
            // btnCreateFunding
            // 
            this.btnCreateFunding.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnCreateFunding.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateFunding.Location = new System.Drawing.Point(694, 299);
            this.btnCreateFunding.Name = "btnCreateFunding";
            this.btnCreateFunding.Size = new System.Drawing.Size(260, 100);
            this.btnCreateFunding.TabIndex = 2;
            this.btnCreateFunding.Text = "Create Funding Applicatoin";
            this.btnCreateFunding.UseVisualStyleBackColor = false;
            this.btnCreateFunding.Click += new System.EventHandler(this.btnCreateFunding_Click);
            // 
            // btnFunding
            // 
            this.btnFunding.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnFunding.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFunding.Location = new System.Drawing.Point(54, 299);
            this.btnFunding.Name = "btnFunding";
            this.btnFunding.Size = new System.Drawing.Size(260, 100);
            this.btnFunding.TabIndex = 3;
            this.btnFunding.Text = "View Funding Applications";
            this.btnFunding.UseVisualStyleBackColor = false;
            this.btnFunding.Click += new System.EventHandler(this.btnFunding_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(370, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(310, 39);
            this.label1.TabIndex = 4;
            this.label1.Text = "Student Dashboard";
            // 
            // StudentDashboardPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 653);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnFunding);
            this.Controls.Add(this.btnCreateFunding);
            this.Controls.Add(this.btnLogOut);
            this.Controls.Add(this.btnUpdateProfile);
            this.Name = "StudentDashboardPage";
            this.Text = "Student Dashboard Page";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUpdateProfile;
        private System.Windows.Forms.Button btnLogOut;
        private System.Windows.Forms.Button btnCreateFunding;
        private System.Windows.Forms.Button btnFunding;
        private System.Windows.Forms.Label label1;
    }
}