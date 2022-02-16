namespace Modules.Reports
{
    partial class frmCertificationBinan
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
            this.cmbCert = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCertNo = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFeePaid = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtORNo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dtPaid = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dtIssued = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.txtOwnersName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtProjectName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtCharacterOccupancy = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtGrp = new System.Windows.Forms.TextBox();
            this.cb1 = new System.Windows.Forms.CheckBox();
            this.cb2 = new System.Windows.Forms.CheckBox();
            this.cb3 = new System.Windows.Forms.CheckBox();
            this.cb4 = new System.Windows.Forms.CheckBox();
            this.txtSpecify = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtProfession = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtPermitNo = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtSignNo = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.dtPermitIssued = new System.Windows.Forms.DateTimePicker();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtOccupancyNo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "Select Certificate:";
            // 
            // cmbCert
            // 
            this.cmbCert.FormattingEnabled = true;
            this.cmbCert.Items.AddRange(new object[] {
            "ANNUAL INSPECTION",
            "OCCUPANCY"});
            this.cmbCert.Location = new System.Drawing.Point(146, 12);
            this.cmbCert.Name = "cmbCert";
            this.cmbCert.Size = new System.Drawing.Size(421, 21);
            this.cmbCert.TabIndex = 2;
            this.cmbCert.SelectedIndexChanged += new System.EventHandler(this.cmbCert_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "No.:";
            // 
            // txtCertNo
            // 
            this.txtCertNo.Enabled = false;
            this.txtCertNo.Location = new System.Drawing.Point(146, 42);
            this.txtCertNo.Name = "txtCertNo";
            this.txtCertNo.Size = new System.Drawing.Size(150, 20);
            this.txtCertNo.TabIndex = 10;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(482, 261);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(86, 33);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(389, 261);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(86, 33);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "FeePaid:";
            // 
            // txtFeePaid
            // 
            this.txtFeePaid.Enabled = false;
            this.txtFeePaid.Location = new System.Drawing.Point(146, 64);
            this.txtFeePaid.Name = "txtFeePaid";
            this.txtFeePaid.Size = new System.Drawing.Size(150, 20);
            this.txtFeePaid.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Official Receipt No.:";
            // 
            // txtORNo
            // 
            this.txtORNo.Enabled = false;
            this.txtORNo.Location = new System.Drawing.Point(146, 86);
            this.txtORNo.Name = "txtORNo";
            this.txtORNo.Size = new System.Drawing.Size(150, 20);
            this.txtORNo.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 115);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Date Paid";
            // 
            // dtPaid
            // 
            this.dtPaid.Enabled = false;
            this.dtPaid.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtPaid.Location = new System.Drawing.Point(146, 109);
            this.dtPaid.Name = "dtPaid";
            this.dtPaid.Size = new System.Drawing.Size(150, 20);
            this.dtPaid.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Date Issued";
            // 
            // dtIssued
            // 
            this.dtIssued.Enabled = false;
            this.dtIssued.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtIssued.Location = new System.Drawing.Point(146, 131);
            this.dtIssued.Name = "dtIssued";
            this.dtIssued.Size = new System.Drawing.Size(150, 20);
            this.dtIssued.TabIndex = 20;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 160);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "Owner\'s Name:";
            // 
            // txtOwnersName
            // 
            this.txtOwnersName.Enabled = false;
            this.txtOwnersName.Location = new System.Drawing.Point(146, 153);
            this.txtOwnersName.Name = "txtOwnersName";
            this.txtOwnersName.Size = new System.Drawing.Size(150, 20);
            this.txtOwnersName.TabIndex = 22;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 203);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 13);
            this.label8.TabIndex = 25;
            this.label8.Text = "Name of Project:";
            // 
            // txtProjectName
            // 
            this.txtProjectName.Enabled = false;
            this.txtProjectName.Location = new System.Drawing.Point(146, 197);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.Size = new System.Drawing.Size(150, 20);
            this.txtProjectName.TabIndex = 24;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 226);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(126, 13);
            this.label9.TabIndex = 27;
            this.label9.Text = "Character of Occupancy:";
            // 
            // txtCharacterOccupancy
            // 
            this.txtCharacterOccupancy.Enabled = false;
            this.txtCharacterOccupancy.Location = new System.Drawing.Point(146, 220);
            this.txtCharacterOccupancy.Name = "txtCharacterOccupancy";
            this.txtCharacterOccupancy.Size = new System.Drawing.Size(150, 20);
            this.txtCharacterOccupancy.TabIndex = 26;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(319, 50);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(39, 13);
            this.label10.TabIndex = 29;
            this.label10.Text = "Group:";
            // 
            // txtGrp
            // 
            this.txtGrp.Enabled = false;
            this.txtGrp.Location = new System.Drawing.Point(374, 47);
            this.txtGrp.Name = "txtGrp";
            this.txtGrp.Size = new System.Drawing.Size(194, 20);
            this.txtGrp.TabIndex = 28;
            // 
            // cb1
            // 
            this.cb1.AutoSize = true;
            this.cb1.Location = new System.Drawing.Point(322, 88);
            this.cb1.Name = "cb1";
            this.cb1.Size = new System.Drawing.Size(197, 17);
            this.cb1.TabIndex = 30;
            this.cb1.Text = "CERTIFICATION OF COMPLETION";
            this.cb1.UseVisualStyleBackColor = true;
            // 
            // cb2
            // 
            this.cb2.AutoSize = true;
            this.cb2.Location = new System.Drawing.Point(322, 112);
            this.cb2.Name = "cb2";
            this.cb2.Size = new System.Drawing.Size(245, 17);
            this.cb2.TabIndex = 31;
            this.cb2.Text = "DAILY CONSTRUCTION WORKS LOGBOOK";
            this.cb2.UseVisualStyleBackColor = true;
            // 
            // cb3
            // 
            this.cb3.AutoSize = true;
            this.cb3.Location = new System.Drawing.Point(322, 135);
            this.cb3.Name = "cb3";
            this.cb3.Size = new System.Drawing.Size(204, 17);
            this.cb3.TabIndex = 32;
            this.cb3.Text = "AS BUILT PLANS/SPECIFICATIONS";
            this.cb3.UseVisualStyleBackColor = true;
            // 
            // cb4
            // 
            this.cb4.AutoSize = true;
            this.cb4.Location = new System.Drawing.Point(322, 157);
            this.cb4.Name = "cb4";
            this.cb4.Size = new System.Drawing.Size(76, 17);
            this.cb4.TabIndex = 33;
            this.cb4.Text = "(SPECIFY)";
            this.cb4.UseVisualStyleBackColor = true;
            this.cb4.CheckedChanged += new System.EventHandler(this.cb4_CheckedChanged);
            // 
            // txtSpecify
            // 
            this.txtSpecify.Enabled = false;
            this.txtSpecify.Location = new System.Drawing.Point(404, 154);
            this.txtSpecify.Name = "txtSpecify";
            this.txtSpecify.Size = new System.Drawing.Size(163, 20);
            this.txtSpecify.TabIndex = 34;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(14, 181);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 13);
            this.label11.TabIndex = 36;
            this.label11.Text = "Profession:";
            // 
            // txtProfession
            // 
            this.txtProfession.Enabled = false;
            this.txtProfession.Location = new System.Drawing.Point(146, 175);
            this.txtProfession.Name = "txtProfession";
            this.txtProfession.Size = new System.Drawing.Size(150, 20);
            this.txtProfession.TabIndex = 35;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(317, 184);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 13);
            this.label12.TabIndex = 38;
            this.label12.Text = "Permit No.";
            // 
            // txtPermitNo
            // 
            this.txtPermitNo.Enabled = false;
            this.txtPermitNo.Location = new System.Drawing.Point(424, 180);
            this.txtPermitNo.Name = "txtPermitNo";
            this.txtPermitNo.Size = new System.Drawing.Size(143, 20);
            this.txtPermitNo.TabIndex = 37;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(317, 206);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(80, 13);
            this.label13.TabIndex = 40;
            this.label13.Text = "Sign Permit No.";
            // 
            // txtSignNo
            // 
            this.txtSignNo.Enabled = false;
            this.txtSignNo.Location = new System.Drawing.Point(424, 202);
            this.txtSignNo.Name = "txtSignNo";
            this.txtSignNo.Size = new System.Drawing.Size(143, 20);
            this.txtSignNo.TabIndex = 39;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(319, 231);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(96, 13);
            this.label14.TabIndex = 42;
            this.label14.Text = "Permit Date Issued";
            // 
            // dtPermitIssued
            // 
            this.dtPermitIssued.Enabled = false;
            this.dtPermitIssued.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtPermitIssued.Location = new System.Drawing.Point(424, 228);
            this.dtPermitIssued.Name = "dtPermitIssued";
            this.dtPermitIssued.Size = new System.Drawing.Size(143, 20);
            this.dtPermitIssued.TabIndex = 41;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(319, 72);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(29, 13);
            this.label15.TabIndex = 43;
            this.label15.Text = "Use:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(14, 250);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(51, 13);
            this.label16.TabIndex = 45;
            this.label16.Text = "Location:";
            // 
            // txtLocation
            // 
            this.txtLocation.Enabled = false;
            this.txtLocation.Location = new System.Drawing.Point(146, 244);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(150, 20);
            this.txtLocation.TabIndex = 44;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(14, 274);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(82, 13);
            this.label17.TabIndex = 47;
            this.label17.Text = "Occupancy No.";
            // 
            // txtOccupancyNo
            // 
            this.txtOccupancyNo.Enabled = false;
            this.txtOccupancyNo.Location = new System.Drawing.Point(146, 268);
            this.txtOccupancyNo.Name = "txtOccupancyNo";
            this.txtOccupancyNo.Size = new System.Drawing.Size(150, 20);
            this.txtOccupancyNo.TabIndex = 46;
            // 
            // frmCertificationBinan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Aqua;
            this.ClientSize = new System.Drawing.Size(579, 301);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.txtOccupancyNo);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtLocation);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.dtPermitIssued);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtSignNo);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtPermitNo);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtProfession);
            this.Controls.Add(this.txtSpecify);
            this.Controls.Add(this.cb4);
            this.Controls.Add(this.cb3);
            this.Controls.Add(this.cb2);
            this.Controls.Add(this.cb1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtGrp);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtCharacterOccupancy);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtProjectName);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtOwnersName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtIssued);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dtPaid);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtORNo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtFeePaid);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCertNo);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbCert);
            this.Name = "frmCertificationBinan";
            this.Text = "frmCertificationBinan";
            this.Load += new System.EventHandler(this.frmCertificationBinan_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbCert;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCertNo;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFeePaid;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtORNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtPaid;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtIssued;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtOwnersName;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtProfession;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtProjectName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtCharacterOccupancy;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtOccupancyNo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtGrp;
        private System.Windows.Forms.CheckBox cb1;
        private System.Windows.Forms.CheckBox cb2;
        private System.Windows.Forms.CheckBox cb3;
        private System.Windows.Forms.CheckBox cb4;
        private System.Windows.Forms.TextBox txtSpecify;
   
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtPermitNo;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtSignNo;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DateTimePicker dtPermitIssued;
        private System.Windows.Forms.Label label15;
       
    }
}