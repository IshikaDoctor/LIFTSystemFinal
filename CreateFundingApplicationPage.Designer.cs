namespace LIFT_System
{
    partial class CreateFundingApplicationPage
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
            this.lblHeading = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.lblAppId = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblApplicationStatus = new System.Windows.Forms.Label();
            this.lblNotes = new System.Windows.Forms.Label();
            this.lblRequestedAmt = new System.Windows.Forms.Label();
            this.lblApplicationType = new System.Windows.Forms.Label();
            this.lblFundingID = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblHeading
            // 
            this.lblHeading.AutoSize = true;
            this.lblHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.Location = new System.Drawing.Point(282, 9);
            this.lblHeading.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(484, 31);
            this.lblHeading.TabIndex = 35;
            this.lblHeading.Text = "Create Application - Funding Details";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(606, 436);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(67, 29);
            this.lblStatus.TabIndex = 31;
            this.lblStatus.Text = "Draft";
            // 
            // txtNotes
            // 
            this.txtNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotes.Location = new System.Drawing.Point(611, 375);
            this.txtNotes.Margin = new System.Windows.Forms.Padding(4);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(256, 36);
            this.txtNotes.TabIndex = 30;
            // 
            // txtAmount
            // 
            this.txtAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.Location = new System.Drawing.Point(611, 311);
            this.txtAmount.Margin = new System.Windows.Forms.Padding(4);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(256, 36);
            this.txtAmount.TabIndex = 29;
            // 
            // cbType
            // 
            this.cbType.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbType.FormattingEnabled = true;
            this.cbType.Items.AddRange(new object[] {
            "Tuiton",
            "Accomodation",
            "Study Materials",
            "Living Expenses",
            "Debt Relief"});
            this.cbType.Location = new System.Drawing.Point(611, 246);
            this.cbType.Margin = new System.Windows.Forms.Padding(4);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(256, 37);
            this.cbType.TabIndex = 28;
            // 
            // lblAppId
            // 
            this.lblAppId.AutoSize = true;
            this.lblAppId.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppId.Location = new System.Drawing.Point(606, 187);
            this.lblAppId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAppId.Name = "lblAppId";
            this.lblAppId.Size = new System.Drawing.Size(120, 29);
            this.lblAppId.TabIndex = 27;
            this.lblAppId.Text = "APP-xxxx";
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Location = new System.Drawing.Point(695, 580);
            this.btnNext.Margin = new System.Windows.Forms.Padding(4);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(314, 41);
            this.btnNext.TabIndex = 26;
            this.btnNext.Text = "Next: Upload Documents";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.RosyBrown;
            this.btnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.Location = new System.Drawing.Point(66, 580);
            this.btnBack.Margin = new System.Windows.Forms.Padding(4);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(143, 41);
            this.btnBack.TabIndex = 24;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // lblApplicationStatus
            // 
            this.lblApplicationStatus.AutoSize = true;
            this.lblApplicationStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApplicationStatus.Location = new System.Drawing.Point(183, 436);
            this.lblApplicationStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblApplicationStatus.Name = "lblApplicationStatus";
            this.lblApplicationStatus.Size = new System.Drawing.Size(322, 29);
            this.lblApplicationStatus.TabIndex = 23;
            this.lblApplicationStatus.Text = "Funding Application Status:";
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotes.Location = new System.Drawing.Point(183, 375);
            this.lblNotes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(86, 29);
            this.lblNotes.TabIndex = 22;
            this.lblNotes.Text = "Notes:";
            // 
            // lblRequestedAmt
            // 
            this.lblRequestedAmt.AutoSize = true;
            this.lblRequestedAmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRequestedAmt.Location = new System.Drawing.Point(183, 311);
            this.lblRequestedAmt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRequestedAmt.Name = "lblRequestedAmt";
            this.lblRequestedAmt.Size = new System.Drawing.Size(237, 29);
            this.lblRequestedAmt.TabIndex = 21;
            this.lblRequestedAmt.Text = "Requested Amount:";
            // 
            // lblApplicationType
            // 
            this.lblApplicationType.AutoSize = true;
            this.lblApplicationType.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApplicationType.Location = new System.Drawing.Point(183, 246);
            this.lblApplicationType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblApplicationType.Name = "lblApplicationType";
            this.lblApplicationType.Size = new System.Drawing.Size(306, 29);
            this.lblApplicationType.TabIndex = 20;
            this.lblApplicationType.Text = "Funding Application Type:";
            // 
            // lblFundingID
            // 
            this.lblFundingID.AutoSize = true;
            this.lblFundingID.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFundingID.Location = new System.Drawing.Point(183, 187);
            this.lblFundingID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFundingID.Name = "lblFundingID";
            this.lblFundingID.Size = new System.Drawing.Size(274, 29);
            this.lblFundingID.TabIndex = 19;
            this.lblFundingID.Text = "Funding Application ID:";
            // 
            // CreateFundingApplicationPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 653);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.cbType);
            this.Controls.Add(this.lblAppId);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.lblApplicationStatus);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.lblRequestedAmt);
            this.Controls.Add(this.lblApplicationType);
            this.Controls.Add(this.lblFundingID);
            this.Name = "CreateFundingApplicationPage";
            this.Text = "Create Funding Application Page";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.Label lblAppId;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label lblApplicationStatus;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.Label lblRequestedAmt;
        private System.Windows.Forms.Label lblApplicationType;
        private System.Windows.Forms.Label lblFundingID;
    }
}