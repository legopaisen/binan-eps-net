namespace Modules.Reports
{
    partial class frmCertification
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCertification));
            this.cmbCert = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.an1 = new Modules.ARN.AN();
            this.txtCertNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFSIC = new System.Windows.Forms.TextBox();
            this.dtpCertNo = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpFSIC = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // cmbCert
            // 
            this.cmbCert.FormattingEnabled = true;
            this.cmbCert.Items.AddRange(new object[] {
            "ANNUAL INSPECTION",
            "OCCUPANCY"});
            this.cmbCert.Location = new System.Drawing.Point(14, 31);
            this.cmbCert.Name = "cmbCert";
            this.cmbCert.Size = new System.Drawing.Size(217, 23);
            this.cmbCert.TabIndex = 0;
            this.cmbCert.SelectedIndexChanged += new System.EventHandler(this.cmbCert_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select Certificate:";
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(52, 247);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(86, 33);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(145, 247);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(86, 33);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "AN";
            // 
            // btnSearch
            // 
            this.btnSearch.ForeColor = System.Drawing.Color.Aqua;
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(197, 77);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(35, 30);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // an1
            // 
            this.an1.GetCode = "";
            this.an1.GetSeries = "";
            this.an1.GetTaxYear = "";
            this.an1.Location = new System.Drawing.Point(14, 77);
            this.an1.Name = "an1";
            this.an1.Size = new System.Drawing.Size(176, 41);
            this.an1.TabIndex = 2;
            this.an1.Load += new System.EventHandler(this.an1_Load);
            // 
            // txtCertNo
            // 
            this.txtCertNo.Enabled = false;
            this.txtCertNo.Location = new System.Drawing.Point(89, 118);
            this.txtCertNo.Name = "txtCertNo";
            this.txtCertNo.Size = new System.Drawing.Size(97, 23);
            this.txtCertNo.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "No.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 179);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "FSIC No.";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // txtFSIC
            // 
            this.txtFSIC.Enabled = false;
            this.txtFSIC.Location = new System.Drawing.Point(89, 176);
            this.txtFSIC.Name = "txtFSIC";
            this.txtFSIC.Size = new System.Drawing.Size(97, 23);
            this.txtFSIC.TabIndex = 8;
            this.txtFSIC.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // dtpCertNo
            // 
            this.dtpCertNo.Enabled = false;
            this.dtpCertNo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCertNo.Location = new System.Drawing.Point(89, 147);
            this.dtpCertNo.Name = "dtpCertNo";
            this.dtpCertNo.Size = new System.Drawing.Size(97, 23);
            this.dtpCertNo.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 153);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 15);
            this.label5.TabIndex = 11;
            this.label5.Text = "Date Issued";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 211);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 15);
            this.label6.TabIndex = 13;
            this.label6.Text = "Date Issued";
            // 
            // dtpFSIC
            // 
            this.dtpFSIC.Enabled = false;
            this.dtpFSIC.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFSIC.Location = new System.Drawing.Point(89, 205);
            this.dtpFSIC.Name = "dtpFSIC";
            this.dtpFSIC.Size = new System.Drawing.Size(97, 23);
            this.dtpFSIC.TabIndex = 12;
            // 
            // frmCertification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Aqua;
            this.ClientSize = new System.Drawing.Size(239, 286);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dtpFSIC);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtpCertNo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtFSIC);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCertNo);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.an1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbCert);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCertification";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Certification";
            this.Load += new System.EventHandler(this.frmCertification_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbCert;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        public ARN.AN an1;
        public System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtCertNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFSIC;
        private System.Windows.Forms.DateTimePicker dtpCertNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpFSIC;
    }
}