namespace LIFT_System
{
    partial class SearchStudentRecordPage
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
            this.btnBackToDashboard = new System.Windows.Forms.Button();
            this.dgvApplicants = new System.Windows.Forms.DataGridView();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblHeading = new System.Windows.Forms.Label();
            this.lblValidation = new System.Windows.Forms.Label();
            this.txtEnteredSearchTerm = new System.Windows.Forms.TextBox();
            this.cboSearchTypes = new System.Windows.Forms.ComboBox();
            this.lblSearchTerm = new System.Windows.Forms.Label();
            this.lblSearchBy = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvApplicants)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBackToDashboard
            // 
            this.btnBackToDashboard.BackColor = System.Drawing.Color.RosyBrown;
            this.btnBackToDashboard.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBackToDashboard.Location = new System.Drawing.Point(13, 597);
            this.btnBackToDashboard.Margin = new System.Windows.Forms.Padding(4);
            this.btnBackToDashboard.Name = "btnBackToDashboard";
            this.btnBackToDashboard.Size = new System.Drawing.Size(271, 43);
            this.btnBackToDashboard.TabIndex = 38;
            this.btnBackToDashboard.Text = "Back to Dashboard";
            this.btnBackToDashboard.UseVisualStyleBackColor = false;
            this.btnBackToDashboard.Click += new System.EventHandler(this.btnBackToDashboard_Click);
            // 
            // dgvApplicants
            // 
            this.dgvApplicants.AllowUserToAddRows = false;
            this.dgvApplicants.AllowUserToDeleteRows = false;
            this.dgvApplicants.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvApplicants.Location = new System.Drawing.Point(42, 336);
            this.dgvApplicants.Margin = new System.Windows.Forms.Padding(4);
            this.dgvApplicants.Name = "dgvApplicants";
            this.dgvApplicants.ReadOnly = true;
            this.dgvApplicants.RowHeadersWidth = 51;
            this.dgvApplicants.Size = new System.Drawing.Size(1006, 239);
            this.dgvApplicants.TabIndex = 37;
            this.dgvApplicants.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewResults_CellDoubleClick);
            this.dgvApplicants.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewResults_CellDoubleClick);
            this.dgvApplicants.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewResults_CellDoubleClick);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(461, 273);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(188, 55);
            this.btnSearch.TabIndex = 36;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click_1);
            // 
            // lblHeading
            // 
            this.lblHeading.AutoSize = true;
            this.lblHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.Location = new System.Drawing.Point(384, 21);
            this.lblHeading.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(265, 39);
            this.lblHeading.TabIndex = 34;
            this.lblHeading.Text = "Search Student";
            // 
            // lblValidation
            // 
            this.lblValidation.AutoSize = true;
            this.lblValidation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValidation.ForeColor = System.Drawing.Color.Black;
            this.lblValidation.Location = new System.Drawing.Point(192, 229);
            this.lblValidation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblValidation.Name = "lblValidation";
            this.lblValidation.Size = new System.Drawing.Size(606, 17);
            this.lblValidation.TabIndex = 33;
            this.lblValidation.Text = "Tip: For ID, use a numeric value like 2025002. For Name, enter first or last name" +
    ".)";
            // 
            // txtEnteredSearchTerm
            // 
            this.txtEnteredSearchTerm.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEnteredSearchTerm.Location = new System.Drawing.Point(554, 182);
            this.txtEnteredSearchTerm.Margin = new System.Windows.Forms.Padding(4);
            this.txtEnteredSearchTerm.Name = "txtEnteredSearchTerm";
            this.txtEnteredSearchTerm.Size = new System.Drawing.Size(234, 36);
            this.txtEnteredSearchTerm.TabIndex = 32;
            // 
            // cboSearchTypes
            // 
            this.cboSearchTypes.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSearchTypes.FormattingEnabled = true;
            this.cboSearchTypes.Items.AddRange(new object[] {
            "Name",
            "ID",
            "Institution"});
            this.cboSearchTypes.Location = new System.Drawing.Point(554, 113);
            this.cboSearchTypes.Margin = new System.Windows.Forms.Padding(4);
            this.cboSearchTypes.Name = "cboSearchTypes";
            this.cboSearchTypes.Size = new System.Drawing.Size(234, 37);
            this.cboSearchTypes.TabIndex = 31;
            // 
            // lblSearchTerm
            // 
            this.lblSearchTerm.AutoSize = true;
            this.lblSearchTerm.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchTerm.Location = new System.Drawing.Point(194, 182);
            this.lblSearchTerm.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSearchTerm.Name = "lblSearchTerm";
            this.lblSearchTerm.Size = new System.Drawing.Size(166, 29);
            this.lblSearchTerm.TabIndex = 30;
            this.lblSearchTerm.Text = "Search Term:";
            // 
            // lblSearchBy
            // 
            this.lblSearchBy.AutoSize = true;
            this.lblSearchBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchBy.Location = new System.Drawing.Point(194, 121);
            this.lblSearchBy.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSearchBy.Name = "lblSearchBy";
            this.lblSearchBy.Size = new System.Drawing.Size(137, 29);
            this.lblSearchBy.TabIndex = 29;
            this.lblSearchBy.Text = "Search By:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(672, 579);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(376, 17);
            this.label1.TabIndex = 39;
            this.label1.Text = "Tip: Double click on the record you wish to change";
            // 
            // SearchStudentRecordPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 653);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBackToDashboard);
            this.Controls.Add(this.dgvApplicants);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.lblValidation);
            this.Controls.Add(this.txtEnteredSearchTerm);
            this.Controls.Add(this.cboSearchTypes);
            this.Controls.Add(this.lblSearchTerm);
            this.Controls.Add(this.lblSearchBy);
            this.Name = "SearchStudentRecordPage";
            this.Text = "Search Student Record Page";
            ((System.ComponentModel.ISupportInitialize)(this.dgvApplicants)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBackToDashboard;
        private System.Windows.Forms.DataGridView dgvApplicants;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.Label lblValidation;
        private System.Windows.Forms.TextBox txtEnteredSearchTerm;
        private System.Windows.Forms.ComboBox cboSearchTypes;
        private System.Windows.Forms.Label lblSearchTerm;
        private System.Windows.Forms.Label lblSearchBy;
        private System.Windows.Forms.Label label1;
    }
}