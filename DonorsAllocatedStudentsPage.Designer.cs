namespace LIFT_System
{
    partial class DonorsAllocatedStudentsPage
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
            this.label1 = new System.Windows.Forms.Label();
            this.dgvFundedStudents = new System.Windows.Forms.DataGridView();
            this.btnBack = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFundedStudents)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(260, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(472, 39);
            this.label1.TabIndex = 0;
            this.label1.Text = "List of Students Being Funded";
            // 
            // dgvFundedStudents
            // 
            this.dgvFundedStudents.AllowUserToAddRows = false;
            this.dgvFundedStudents.AllowUserToDeleteRows = false;
            this.dgvFundedStudents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFundedStudents.Location = new System.Drawing.Point(33, 122);
            this.dgvFundedStudents.Name = "dgvFundedStudents";
            this.dgvFundedStudents.ReadOnly = true;
            this.dgvFundedStudents.RowHeadersWidth = 51;
            this.dgvFundedStudents.RowTemplate.Height = 24;
            this.dgvFundedStudents.Size = new System.Drawing.Size(998, 365);
            this.dgvFundedStudents.TabIndex = 1;
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.RosyBrown;
            this.btnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.Location = new System.Drawing.Point(42, 572);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(97, 51);
            this.btnBack.TabIndex = 2;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // DonorsAllocatedStudentsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 653);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.dgvFundedStudents);
            this.Controls.Add(this.label1);
            this.Name = "DonorsAllocatedStudentsPage";
            this.Text = "Donors Allocated Students Page";
            this.Load += new System.EventHandler(this.DonorsAllocatedStudentsPage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFundedStudents)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvFundedStudents;
        private System.Windows.Forms.Button btnBack;
    }
}