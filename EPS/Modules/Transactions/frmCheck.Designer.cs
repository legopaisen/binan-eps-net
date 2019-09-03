namespace Modules.Transactions
{
    partial class frmCheck
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCheck));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grpCheckInfo = new System.Windows.Forms.GroupBox();
            this.grpBankInfo = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBankCode = new System.Windows.Forms.TextBox();
            this.txtBank = new System.Windows.Forms.TextBox();
            this.txtBankAdd = new System.Windows.Forms.TextBox();
            this.txtBankBranch = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtAcctNo = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.dtpCheckDate = new System.Windows.Forms.DateTimePicker();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.txtMI = new System.Windows.Forms.TextBox();
            this.txtCheckNo = new System.Windows.Forms.TextBox();
            this.txtCheckAmt = new System.Windows.Forms.TextBox();
            this.txtDebit = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnAdd = new System.Windows.Forms.Button();
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.grpCheckInfo.SuspendLayout();
            this.grpBankInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.txtBankBranch);
            this.groupBox1.Controls.Add(this.txtBankAdd);
            this.groupBox1.Controls.Add(this.txtBank);
            this.groupBox1.Controls.Add(this.txtBankCode);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(14, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(540, 115);
            this.groupBox1.TabIndex = 100;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bank Information";
            // 
            // grpCheckInfo
            // 
            this.grpCheckInfo.Controls.Add(this.txtDebit);
            this.grpCheckInfo.Controls.Add(this.txtCheckAmt);
            this.grpCheckInfo.Controls.Add(this.txtCheckNo);
            this.grpCheckInfo.Controls.Add(this.dtpCheckDate);
            this.grpCheckInfo.Controls.Add(this.label12);
            this.grpCheckInfo.Controls.Add(this.label11);
            this.grpCheckInfo.Controls.Add(this.label10);
            this.grpCheckInfo.Controls.Add(this.label9);
            this.grpCheckInfo.Location = new System.Drawing.Point(289, 135);
            this.grpCheckInfo.Name = "grpCheckInfo";
            this.grpCheckInfo.Size = new System.Drawing.Size(265, 140);
            this.grpCheckInfo.TabIndex = 100;
            this.grpCheckInfo.TabStop = false;
            this.grpCheckInfo.Text = "Check Info";
            // 
            // grpBankInfo
            // 
            this.grpBankInfo.Controls.Add(this.txtMI);
            this.grpBankInfo.Controls.Add(this.txtFirstName);
            this.grpBankInfo.Controls.Add(this.txtLastName);
            this.grpBankInfo.Controls.Add(this.txtAcctNo);
            this.grpBankInfo.Controls.Add(this.label8);
            this.grpBankInfo.Controls.Add(this.label7);
            this.grpBankInfo.Controls.Add(this.label6);
            this.grpBankInfo.Controls.Add(this.label5);
            this.grpBankInfo.Location = new System.Drawing.Point(14, 135);
            this.grpBankInfo.Name = "grpBankInfo";
            this.grpBankInfo.Size = new System.Drawing.Size(269, 140);
            this.grpBankInfo.TabIndex = 100;
            this.grpBankInfo.TabStop = false;
            this.grpBankInfo.Text = "Bank Account Info";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Code:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Bank:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "Address:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(292, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "Branch:";
            // 
            // txtBankCode
            // 
            this.txtBankCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBankCode.Location = new System.Drawing.Point(79, 30);
            this.txtBankCode.Name = "txtBankCode";
            this.txtBankCode.Size = new System.Drawing.Size(107, 23);
            this.txtBankCode.TabIndex = 0;
            // 
            // txtBank
            // 
            this.txtBank.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBank.Location = new System.Drawing.Point(79, 57);
            this.txtBank.Name = "txtBank";
            this.txtBank.ReadOnly = true;
            this.txtBank.Size = new System.Drawing.Size(207, 23);
            this.txtBank.TabIndex = 3;
            // 
            // txtBankAdd
            // 
            this.txtBankAdd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBankAdd.Location = new System.Drawing.Point(79, 84);
            this.txtBankAdd.Name = "txtBankAdd";
            this.txtBankAdd.ReadOnly = true;
            this.txtBankAdd.Size = new System.Drawing.Size(451, 23);
            this.txtBankAdd.TabIndex = 5;
            // 
            // txtBankBranch
            // 
            this.txtBankBranch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBankBranch.Location = new System.Drawing.Point(347, 57);
            this.txtBankBranch.Name = "txtBankBranch";
            this.txtBankBranch.ReadOnly = true;
            this.txtBankBranch.Size = new System.Drawing.Size(183, 23);
            this.txtBankBranch.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 15);
            this.label5.TabIndex = 2;
            this.label5.Text = "Acct. No:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 15);
            this.label6.TabIndex = 2;
            this.label6.Text = "Last Name:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 15);
            this.label7.TabIndex = 2;
            this.label7.Text = "First Name:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(43, 111);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 15);
            this.label8.TabIndex = 2;
            this.label8.Text = "M.I.:";
            // 
            // txtAcctNo
            // 
            this.txtAcctNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAcctNo.Location = new System.Drawing.Point(79, 26);
            this.txtAcctNo.Name = "txtAcctNo";
            this.txtAcctNo.Size = new System.Drawing.Size(107, 23);
            this.txtAcctNo.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(23, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 15);
            this.label9.TabIndex = 3;
            this.label9.Text = "Check No:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(31, 56);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 15);
            this.label10.TabIndex = 3;
            this.label10.Text = "Amount:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 85);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 15);
            this.label11.TabIndex = 3;
            this.label11.Text = "Debit/Credit:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(13, 116);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 15);
            this.label12.TabIndex = 3;
            this.label12.Text = "Check Date:";
            // 
            // dtpCheckDate
            // 
            this.dtpCheckDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCheckDate.Location = new System.Drawing.Point(89, 108);
            this.dtpCheckDate.Name = "dtpCheckDate";
            this.dtpCheckDate.Size = new System.Drawing.Size(92, 23);
            this.dtpCheckDate.TabIndex = 12;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(394, 281);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(77, 28);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Ok";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(477, 281);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(77, 28);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtLastName
            // 
            this.txtLastName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLastName.Location = new System.Drawing.Point(79, 53);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(184, 23);
            this.txtLastName.TabIndex = 7;
            // 
            // txtFirstName
            // 
            this.txtFirstName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFirstName.Location = new System.Drawing.Point(79, 80);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(184, 23);
            this.txtFirstName.TabIndex = 8;
            // 
            // txtMI
            // 
            this.txtMI.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMI.Location = new System.Drawing.Point(79, 108);
            this.txtMI.MaxLength = 1;
            this.txtMI.Name = "txtMI";
            this.txtMI.Size = new System.Drawing.Size(28, 23);
            this.txtMI.TabIndex = 9;
            // 
            // txtCheckNo
            // 
            this.txtCheckNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCheckNo.Location = new System.Drawing.Point(89, 26);
            this.txtCheckNo.Name = "txtCheckNo";
            this.txtCheckNo.Size = new System.Drawing.Size(166, 23);
            this.txtCheckNo.TabIndex = 10;
            // 
            // txtCheckAmt
            // 
            this.txtCheckAmt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCheckAmt.Location = new System.Drawing.Point(89, 53);
            this.txtCheckAmt.Name = "txtCheckAmt";
            this.txtCheckAmt.Size = new System.Drawing.Size(166, 23);
            this.txtCheckAmt.TabIndex = 11;
            this.txtCheckAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCheckAmt.Leave += new System.EventHandler(this.txtCheckAmt_Leave);
            // 
            // txtDebit
            // 
            this.txtDebit.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDebit.Location = new System.Drawing.Point(89, 80);
            this.txtDebit.Name = "txtDebit";
            this.txtDebit.ReadOnly = true;
            this.txtDebit.Size = new System.Drawing.Size(166, 23);
            this.txtDebit.TabIndex = 16;
            this.txtDebit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnSearch
            // 
            this.btnSearch.ForeColor = System.Drawing.Color.Aqua;
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(192, 27);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(30, 26);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(228, 27);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(41, 26);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // frmCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.ClientSize = new System.Drawing.Size(564, 318);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grpBankInfo);
            this.Controls.Add(this.grpCheckInfo);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCheck";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Check Detials";
            this.Load += new System.EventHandler(this.frmCheck_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpCheckInfo.ResumeLayout(false);
            this.grpCheckInfo.PerformLayout();
            this.grpBankInfo.ResumeLayout(false);
            this.grpBankInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtBankBranch;
        private System.Windows.Forms.TextBox txtBankAdd;
        private System.Windows.Forms.TextBox txtBank;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpCheckInfo;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox grpBankInfo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ToolTip toolTip2;
        public System.Windows.Forms.TextBox txtMI;
        public System.Windows.Forms.TextBox txtFirstName;
        public System.Windows.Forms.TextBox txtLastName;
        public System.Windows.Forms.TextBox txtBankCode;
        public System.Windows.Forms.DateTimePicker dtpCheckDate;
        public System.Windows.Forms.TextBox txtAcctNo;
        public System.Windows.Forms.TextBox txtDebit;
        public System.Windows.Forms.TextBox txtCheckAmt;
        public System.Windows.Forms.TextBox txtCheckNo;
    }
}