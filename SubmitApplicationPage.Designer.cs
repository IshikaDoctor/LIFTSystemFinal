namespace LIFT_System
{
    partial class SubmitApplicationPage
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
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblDocID = new System.Windows.Forms.Label();
            this.lblDocFee = new System.Windows.Forms.Label();
            this.lblDocReg = new System.Windows.Forms.Label();
            this.lblDocumentsHeading = new System.Windows.Forms.Label();
            this.lblEnteredNotes = new System.Windows.Forms.Label();
            this.lblEnteredAmount = new System.Windows.Forms.Label();
            this.lblEnteredType = new System.Windows.Forms.Label();
            this.lblEnteredID = new System.Windows.Forms.Label();
            this.lblNotes = new System.Windows.Forms.Label();
            this.lblRequestedAmount = new System.Windows.Forms.Label();
            this.lblApplicationType = new System.Windows.Forms.Label();
            this.lblApplicationID = new System.Windows.Forms.Label();
            this.lblApplicationHeading = new System.Windows.Forms.Label();
            this.lblHeading = new System.Windows.Forms.Label();
            this.chkConfirm = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.Location = new System.Drawing.Point(642, 567);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(4);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(271, 43);
            this.btnSubmit.TabIndex = 34;
            this.btnSubmit.Text = "Submit Application";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.RosyBrown;
            this.btnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.Location = new System.Drawing.Point(104, 567);
            this.btnBack.Margin = new System.Windows.Forms.Padding(4);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(117, 43);
            this.btnBack.TabIndex = 33;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // lblDocID
            // 
            this.lblDocID.AutoSize = true;
            this.lblDocID.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocID.Location = new System.Drawing.Point(176, 446);
            this.lblDocID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDocID.Name = "lblDocID";
            this.lblDocID.Size = new System.Drawing.Size(71, 29);
            this.lblDocID.TabIndex = 32;
            this.lblDocID.Text = "xxx ✓";
            this.lblDocID.Click += new System.EventHandler(this.lblEnteredDocument3_Click);
            // 
            // lblDocFee
            // 
            this.lblDocFee.AutoSize = true;
            this.lblDocFee.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocFee.Location = new System.Drawing.Point(176, 406);
            this.lblDocFee.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDocFee.Name = "lblDocFee";
            this.lblDocFee.Size = new System.Drawing.Size(71, 29);
            this.lblDocFee.TabIndex = 31;
            this.lblDocFee.Text = "xxx ✓";
            this.lblDocFee.Click += new System.EventHandler(this.lblEnteredDocument2_Click);
            // 
            // lblDocReg
            // 
            this.lblDocReg.AutoSize = true;
            this.lblDocReg.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocReg.Location = new System.Drawing.Point(176, 360);
            this.lblDocReg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDocReg.Name = "lblDocReg";
            this.lblDocReg.Size = new System.Drawing.Size(71, 29);
            this.lblDocReg.TabIndex = 30;
            this.lblDocReg.Text = "xxx ✓";
            this.lblDocReg.Click += new System.EventHandler(this.lblEnteredDocument1_Click);
            // 
            // lblDocumentsHeading
            // 
            this.lblDocumentsHeading.AutoSize = true;
            this.lblDocumentsHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocumentsHeading.Location = new System.Drawing.Point(176, 316);
            this.lblDocumentsHeading.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDocumentsHeading.Name = "lblDocumentsHeading";
            this.lblDocumentsHeading.Size = new System.Drawing.Size(283, 29);
            this.lblDocumentsHeading.TabIndex = 29;
            this.lblDocumentsHeading.Text = "Uploaded Documents:";
            this.lblDocumentsHeading.Click += new System.EventHandler(this.lblDocumentsHeading_Click);
            // 
            // lblEnteredNotes
            // 
            this.lblEnteredNotes.AutoSize = true;
            this.lblEnteredNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnteredNotes.Location = new System.Drawing.Point(581, 259);
            this.lblEnteredNotes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEnteredNotes.Name = "lblEnteredNotes";
            this.lblEnteredNotes.Size = new System.Drawing.Size(49, 29);
            this.lblEnteredNotes.TabIndex = 28;
            this.lblEnteredNotes.Text = "xxx";
            // 
            // lblEnteredAmount
            // 
            this.lblEnteredAmount.AutoSize = true;
            this.lblEnteredAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnteredAmount.Location = new System.Drawing.Point(581, 222);
            this.lblEnteredAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEnteredAmount.Name = "lblEnteredAmount";
            this.lblEnteredAmount.Size = new System.Drawing.Size(49, 29);
            this.lblEnteredAmount.TabIndex = 27;
            this.lblEnteredAmount.Text = "xxx";
            // 
            // lblEnteredType
            // 
            this.lblEnteredType.AutoSize = true;
            this.lblEnteredType.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnteredType.Location = new System.Drawing.Point(581, 180);
            this.lblEnteredType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEnteredType.Name = "lblEnteredType";
            this.lblEnteredType.Size = new System.Drawing.Size(49, 29);
            this.lblEnteredType.TabIndex = 26;
            this.lblEnteredType.Text = "xxx";
            // 
            // lblEnteredID
            // 
            this.lblEnteredID.AutoSize = true;
            this.lblEnteredID.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnteredID.Location = new System.Drawing.Point(581, 135);
            this.lblEnteredID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEnteredID.Name = "lblEnteredID";
            this.lblEnteredID.Size = new System.Drawing.Size(49, 29);
            this.lblEnteredID.TabIndex = 25;
            this.lblEnteredID.Text = "xxx";
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotes.Location = new System.Drawing.Point(173, 259);
            this.lblNotes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(86, 29);
            this.lblNotes.TabIndex = 24;
            this.lblNotes.Text = "Notes:";
            // 
            // lblRequestedAmount
            // 
            this.lblRequestedAmount.AutoSize = true;
            this.lblRequestedAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRequestedAmount.Location = new System.Drawing.Point(173, 222);
            this.lblRequestedAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRequestedAmount.Name = "lblRequestedAmount";
            this.lblRequestedAmount.Size = new System.Drawing.Size(237, 29);
            this.lblRequestedAmount.TabIndex = 23;
            this.lblRequestedAmount.Text = "Requested Amount:";
            // 
            // lblApplicationType
            // 
            this.lblApplicationType.AutoSize = true;
            this.lblApplicationType.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApplicationType.Location = new System.Drawing.Point(173, 180);
            this.lblApplicationType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblApplicationType.Name = "lblApplicationType";
            this.lblApplicationType.Size = new System.Drawing.Size(306, 29);
            this.lblApplicationType.TabIndex = 22;
            this.lblApplicationType.Text = "Funding Application Type:";
            // 
            // lblApplicationID
            // 
            this.lblApplicationID.AutoSize = true;
            this.lblApplicationID.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApplicationID.Location = new System.Drawing.Point(173, 135);
            this.lblApplicationID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblApplicationID.Name = "lblApplicationID";
            this.lblApplicationID.Size = new System.Drawing.Size(274, 29);
            this.lblApplicationID.TabIndex = 21;
            this.lblApplicationID.Text = "Funding Application ID:";
            // 
            // lblApplicationHeading
            // 
            this.lblApplicationHeading.AutoSize = true;
            this.lblApplicationHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApplicationHeading.Location = new System.Drawing.Point(173, 93);
            this.lblApplicationHeading.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblApplicationHeading.Name = "lblApplicationHeading";
            this.lblApplicationHeading.Size = new System.Drawing.Size(282, 29);
            this.lblApplicationHeading.TabIndex = 20;
            this.lblApplicationHeading.Text = "Application Summary:";
            // 
            // lblHeading
            // 
            this.lblHeading.AutoSize = true;
            this.lblHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.Location = new System.Drawing.Point(200, 9);
            this.lblHeading.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(657, 39);
            this.lblHeading.TabIndex = 18;
            this.lblHeading.Text = "Create Application - Review and Submit";
            // 
            // chkConfirm
            // 
            this.chkConfirm.AutoSize = true;
            this.chkConfirm.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkConfirm.Location = new System.Drawing.Point(182, 493);
            this.chkConfirm.Margin = new System.Windows.Forms.Padding(4);
            this.chkConfirm.Name = "chkConfirm";
            this.chkConfirm.Size = new System.Drawing.Size(553, 33);
            this.chkConfirm.TabIndex = 35;
            this.chkConfirm.Text = "I Confirm the information provided is accurate.";
            this.chkConfirm.UseVisualStyleBackColor = true;
            this.chkConfirm.CheckedChanged += new System.EventHandler(this.chkConfirm_CheckedChanged);
            // 
            // SubmitApplicationPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 653);
            this.Controls.Add(this.chkConfirm);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.lblDocID);
            this.Controls.Add(this.lblDocFee);
            this.Controls.Add(this.lblDocReg);
            this.Controls.Add(this.lblDocumentsHeading);
            this.Controls.Add(this.lblEnteredNotes);
            this.Controls.Add(this.lblEnteredAmount);
            this.Controls.Add(this.lblEnteredType);
            this.Controls.Add(this.lblEnteredID);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.lblRequestedAmount);
            this.Controls.Add(this.lblApplicationType);
            this.Controls.Add(this.lblApplicationID);
            this.Controls.Add(this.lblApplicationHeading);
            this.Controls.Add(this.lblHeading);
            this.Name = "SubmitApplicationPage";
            this.Text = "Submit Application Page";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label lblDocID;
        private System.Windows.Forms.Label lblDocFee;
        private System.Windows.Forms.Label lblDocReg;
        private System.Windows.Forms.Label lblDocumentsHeading;
        private System.Windows.Forms.Label lblEnteredNotes;
        private System.Windows.Forms.Label lblEnteredAmount;
        private System.Windows.Forms.Label lblEnteredType;
        private System.Windows.Forms.Label lblEnteredID;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.Label lblRequestedAmount;
        private System.Windows.Forms.Label lblApplicationType;
        private System.Windows.Forms.Label lblApplicationID;
        private System.Windows.Forms.Label lblApplicationHeading;
        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.CheckBox chkConfirm;
    }
}