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
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtBankBranch = new System.Windows.Forms.TextBox();
            this.txtBankAdd = new System.Windows.Forms.TextBox();
            this.txtBank = new System.Windows.Forms.TextBox();
            this.txtBankCode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.grpCheckInfo = new System.Windows.Forms.GroupBox();
            this.btnSearchChk = new System.Windows.Forms.Button();
            this.txtDebit = new System.Windows.Forms.TextBox();
            this.txtCheckAmt = new System.Windows.Forms.TextBox();
            this.txtCheckNo = new System.Windows.Forms.TextBox();
            this.dtpCheckDate = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.grpTotal = new System.Windows.Forms.GroupBox();
            this.txtTotTaxDue = new System.Windows.Forms.TextBox();
            this.txtTotCashAmt = new System.Windows.Forms.TextBox();
            this.txtTotChkAmt = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.dgvCheck = new System.Windows.Forms.DataGridView();
            this.ChkNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChkAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Bank = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Branch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BankCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChkType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpChkType = new System.Windows.Forms.GroupBox();
            this.dtAccepted = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.rdoPostal = new System.Windows.Forms.RadioButton();
            this.rdoManager = new System.Windows.Forms.RadioButton();
            this.rdoPersonal = new System.Windows.Forms.RadioButton();
            this.rdoCashier = new System.Windows.Forms.RadioButton();
            this.btnDelete = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.grpCheckInfo.SuspendLayout();
            this.grpTotal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCheck)).BeginInit();
            this.grpChkType.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.txtBankBranch);
            this.groupBox1.Controls.Add(this.txtBankAdd);
            this.groupBox1.Controls.Add(this.txtBank);
            this.groupBox1.Controls.Add(this.txtBankCode);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 181);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(540, 115);
            this.groupBox1.TabIndex = 100;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bank Information";
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
            // txtBankBranch
            // 
            this.txtBankBranch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBankBranch.Location = new System.Drawing.Point(347, 57);
            this.txtBankBranch.Name = "txtBankBranch";
            this.txtBankBranch.ReadOnly = true;
            this.txtBankBranch.Size = new System.Drawing.Size(183, 23);
            this.txtBankBranch.TabIndex = 4;
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
            // txtBank
            // 
            this.txtBank.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBank.Location = new System.Drawing.Point(79, 57);
            this.txtBank.Name = "txtBank";
            this.txtBank.ReadOnly = true;
            this.txtBank.Size = new System.Drawing.Size(207, 23);
            this.txtBank.TabIndex = 3;
            // 
            // txtBankCode
            // 
            this.txtBankCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBankCode.Location = new System.Drawing.Point(79, 30);
            this.txtBankCode.Name = "txtBankCode";
            this.txtBankCode.Size = new System.Drawing.Size(107, 23);
            this.txtBankCode.TabIndex = 0;
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "Address:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Bank Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Code:";
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(12, 448);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(77, 28);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // grpCheckInfo
            // 
            this.grpCheckInfo.Controls.Add(this.btnSearchChk);
            this.grpCheckInfo.Controls.Add(this.txtDebit);
            this.grpCheckInfo.Controls.Add(this.txtCheckAmt);
            this.grpCheckInfo.Controls.Add(this.txtCheckNo);
            this.grpCheckInfo.Controls.Add(this.dtpCheckDate);
            this.grpCheckInfo.Controls.Add(this.label12);
            this.grpCheckInfo.Controls.Add(this.label11);
            this.grpCheckInfo.Controls.Add(this.label10);
            this.grpCheckInfo.Controls.Add(this.label9);
            this.grpCheckInfo.Location = new System.Drawing.Point(12, 302);
            this.grpCheckInfo.Name = "grpCheckInfo";
            this.grpCheckInfo.Size = new System.Drawing.Size(265, 140);
            this.grpCheckInfo.TabIndex = 100;
            this.grpCheckInfo.TabStop = false;
            this.grpCheckInfo.Text = "Check Info";
            // 
            // btnSearchChk
            // 
            this.btnSearchChk.ForeColor = System.Drawing.Color.Aqua;
            this.btnSearchChk.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchChk.Image")));
            this.btnSearchChk.Location = new System.Drawing.Point(192, 22);
            this.btnSearchChk.Name = "btnSearchChk";
            this.btnSearchChk.Size = new System.Drawing.Size(30, 26);
            this.btnSearchChk.TabIndex = 6;
            this.btnSearchChk.UseVisualStyleBackColor = true;
            this.btnSearchChk.Visible = false;
            this.btnSearchChk.Click += new System.EventHandler(this.btnSearchChk_Click);
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
            // txtCheckNo
            // 
            this.txtCheckNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCheckNo.Location = new System.Drawing.Point(89, 26);
            this.txtCheckNo.Name = "txtCheckNo";
            this.txtCheckNo.Size = new System.Drawing.Size(97, 23);
            this.txtCheckNo.TabIndex = 10;
            // 
            // dtpCheckDate
            // 
            this.dtpCheckDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCheckDate.Location = new System.Drawing.Point(89, 108);
            this.dtpCheckDate.Name = "dtpCheckDate";
            this.dtpCheckDate.Size = new System.Drawing.Size(92, 23);
            this.dtpCheckDate.TabIndex = 12;
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
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 85);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 15);
            this.label11.TabIndex = 3;
            this.label11.Text = "Debit/Credit:";
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
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(23, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 15);
            this.label9.TabIndex = 3;
            this.label9.Text = "Check No:";
            // 
            // grpTotal
            // 
            this.grpTotal.Controls.Add(this.txtTotTaxDue);
            this.grpTotal.Controls.Add(this.txtTotCashAmt);
            this.grpTotal.Controls.Add(this.txtTotChkAmt);
            this.grpTotal.Controls.Add(this.label7);
            this.grpTotal.Controls.Add(this.label6);
            this.grpTotal.Controls.Add(this.label5);
            this.grpTotal.Enabled = false;
            this.grpTotal.Location = new System.Drawing.Point(283, 302);
            this.grpTotal.Name = "grpTotal";
            this.grpTotal.Size = new System.Drawing.Size(269, 140);
            this.grpTotal.TabIndex = 100;
            this.grpTotal.TabStop = false;
            this.grpTotal.Text = "Payment Info";
            // 
            // txtTotTaxDue
            // 
            this.txtTotTaxDue.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTotTaxDue.Location = new System.Drawing.Point(107, 85);
            this.txtTotTaxDue.Name = "txtTotTaxDue";
            this.txtTotTaxDue.Size = new System.Drawing.Size(152, 23);
            this.txtTotTaxDue.TabIndex = 19;
            this.txtTotTaxDue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotCashAmt
            // 
            this.txtTotCashAmt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTotCashAmt.Location = new System.Drawing.Point(107, 56);
            this.txtTotCashAmt.Name = "txtTotCashAmt";
            this.txtTotCashAmt.Size = new System.Drawing.Size(152, 23);
            this.txtTotCashAmt.TabIndex = 18;
            this.txtTotCashAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotChkAmt
            // 
            this.txtTotChkAmt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTotChkAmt.Location = new System.Drawing.Point(107, 25);
            this.txtTotChkAmt.Name = "txtTotChkAmt";
            this.txtTotChkAmt.Size = new System.Drawing.Size(152, 23);
            this.txtTotChkAmt.TabIndex = 17;
            this.txtTotChkAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 88);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 15);
            this.label7.TabIndex = 2;
            this.label7.Text = "Total Tax Due";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 15);
            this.label6.TabIndex = 2;
            this.label6.Text = "Total Cash Amt";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 15);
            this.label5.TabIndex = 2;
            this.label5.Text = "Total Check Amt";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(475, 448);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(77, 28);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Ok";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(475, 448);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(77, 28);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dgvCheck
            // 
            this.dgvCheck.AllowUserToAddRows = false;
            this.dgvCheck.AllowUserToDeleteRows = false;
            this.dgvCheck.AllowUserToOrderColumns = true;
            this.dgvCheck.AllowUserToResizeRows = false;
            this.dgvCheck.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCheck.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ChkNo,
            this.ChkAmt,
            this.Bank,
            this.Branch,
            this.BankCode,
            this.ChkType});
            this.dgvCheck.Location = new System.Drawing.Point(12, 8);
            this.dgvCheck.MultiSelect = false;
            this.dgvCheck.Name = "dgvCheck";
            this.dgvCheck.ReadOnly = true;
            this.dgvCheck.RowHeadersVisible = false;
            this.dgvCheck.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCheck.Size = new System.Drawing.Size(540, 93);
            this.dgvCheck.TabIndex = 101;
            this.dgvCheck.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCheck_CellClick);
            // 
            // ChkNo
            // 
            this.ChkNo.HeaderText = "Check No";
            this.ChkNo.Name = "ChkNo";
            this.ChkNo.ReadOnly = true;
            // 
            // ChkAmt
            // 
            this.ChkAmt.HeaderText = "Check Amt";
            this.ChkAmt.Name = "ChkAmt";
            this.ChkAmt.ReadOnly = true;
            // 
            // Bank
            // 
            this.Bank.HeaderText = "Bank";
            this.Bank.Name = "Bank";
            this.Bank.ReadOnly = true;
            // 
            // Branch
            // 
            this.Branch.HeaderText = "Branch";
            this.Branch.Name = "Branch";
            this.Branch.ReadOnly = true;
            // 
            // BankCode
            // 
            this.BankCode.HeaderText = "Bank Code";
            this.BankCode.Name = "BankCode";
            this.BankCode.ReadOnly = true;
            // 
            // ChkType
            // 
            this.ChkType.HeaderText = "ChkType";
            this.ChkType.Name = "ChkType";
            this.ChkType.ReadOnly = true;
            this.ChkType.Visible = false;
            // 
            // grpChkType
            // 
            this.grpChkType.Controls.Add(this.dtAccepted);
            this.grpChkType.Controls.Add(this.label8);
            this.grpChkType.Controls.Add(this.rdoPostal);
            this.grpChkType.Controls.Add(this.rdoManager);
            this.grpChkType.Controls.Add(this.rdoPersonal);
            this.grpChkType.Controls.Add(this.rdoCashier);
            this.grpChkType.Location = new System.Drawing.Point(12, 101);
            this.grpChkType.Name = "grpChkType";
            this.grpChkType.Size = new System.Drawing.Size(540, 74);
            this.grpChkType.TabIndex = 101;
            this.grpChkType.TabStop = false;
            this.grpChkType.Text = "Check Type";
            // 
            // dtAccepted
            // 
            this.dtAccepted.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtAccepted.Location = new System.Drawing.Point(442, 22);
            this.dtAccepted.Name = "dtAccepted";
            this.dtAccepted.Size = new System.Drawing.Size(92, 23);
            this.dtAccepted.TabIndex = 103;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(404, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 15);
            this.label8.TabIndex = 102;
            this.label8.Text = "Date";
            // 
            // rdoPostal
            // 
            this.rdoPostal.AutoSize = true;
            this.rdoPostal.Location = new System.Drawing.Point(133, 47);
            this.rdoPostal.Name = "rdoPostal";
            this.rdoPostal.Size = new System.Drawing.Size(137, 19);
            this.rdoPostal.TabIndex = 3;
            this.rdoPostal.TabStop = true;
            this.rdoPostal.Text = "Postal/Money Order";
            this.rdoPostal.UseVisualStyleBackColor = true;
            this.rdoPostal.CheckedChanged += new System.EventHandler(this.rdoPostal_CheckedChanged);
            // 
            // rdoManager
            // 
            this.rdoManager.AutoSize = true;
            this.rdoManager.Location = new System.Drawing.Point(133, 22);
            this.rdoManager.Name = "rdoManager";
            this.rdoManager.Size = new System.Drawing.Size(118, 19);
            this.rdoManager.TabIndex = 2;
            this.rdoManager.TabStop = true;
            this.rdoManager.Text = "Manager\'s Check";
            this.rdoManager.UseVisualStyleBackColor = true;
            this.rdoManager.CheckedChanged += new System.EventHandler(this.rdoManager_CheckedChanged);
            // 
            // rdoPersonal
            // 
            this.rdoPersonal.AutoSize = true;
            this.rdoPersonal.Location = new System.Drawing.Point(16, 47);
            this.rdoPersonal.Name = "rdoPersonal";
            this.rdoPersonal.Size = new System.Drawing.Size(109, 19);
            this.rdoPersonal.TabIndex = 1;
            this.rdoPersonal.TabStop = true;
            this.rdoPersonal.Text = "Personal Check";
            this.rdoPersonal.UseVisualStyleBackColor = true;
            this.rdoPersonal.CheckedChanged += new System.EventHandler(this.rdoPersonal_CheckedChanged);
            // 
            // rdoCashier
            // 
            this.rdoCashier.AutoSize = true;
            this.rdoCashier.Location = new System.Drawing.Point(16, 22);
            this.rdoCashier.Name = "rdoCashier";
            this.rdoCashier.Size = new System.Drawing.Size(111, 19);
            this.rdoCashier.TabIndex = 0;
            this.rdoCashier.TabStop = true;
            this.rdoCashier.Text = "Cashier\'s Check";
            this.rdoCashier.UseVisualStyleBackColor = true;
            this.rdoCashier.CheckedChanged += new System.EventHandler(this.rdoCashier_CheckedChanged);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(95, 448);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(77, 28);
            this.btnDelete.TabIndex = 102;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // frmCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.ClientSize = new System.Drawing.Size(564, 486);
            this.ControlBox = false;
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.grpChkType);
            this.Controls.Add(this.dgvCheck);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.grpTotal);
            this.Controls.Add(this.grpCheckInfo);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCheck";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Check Detials";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCheck_FormClosing);
            this.Load += new System.EventHandler(this.frmCheck_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpCheckInfo.ResumeLayout(false);
            this.grpCheckInfo.PerformLayout();
            this.grpTotal.ResumeLayout(false);
            this.grpTotal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCheck)).EndInit();
            this.grpChkType.ResumeLayout(false);
            this.grpChkType.PerformLayout();
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
        private System.Windows.Forms.GroupBox grpCheckInfo;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox grpTotal;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ToolTip toolTip2;
        public System.Windows.Forms.DateTimePicker dtpCheckDate;
        public System.Windows.Forms.TextBox txtDebit;
        public System.Windows.Forms.TextBox txtCheckAmt;
        public System.Windows.Forms.TextBox txtCheckNo;
        private System.Windows.Forms.DataGridView dgvCheck;
        public System.Windows.Forms.TextBox txtBankCode;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button btnSearchChk;
        public System.Windows.Forms.TextBox txtTotTaxDue;
        public System.Windows.Forms.TextBox txtTotCashAmt;
        public System.Windows.Forms.TextBox txtTotChkAmt;
        private System.Windows.Forms.GroupBox grpChkType;
        public System.Windows.Forms.DateTimePicker dtAccepted;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RadioButton rdoPostal;
        private System.Windows.Forms.RadioButton rdoManager;
        private System.Windows.Forms.RadioButton rdoPersonal;
        private System.Windows.Forms.RadioButton rdoCashier;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChkNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChkAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Bank;
        private System.Windows.Forms.DataGridViewTextBoxColumn Branch;
        private System.Windows.Forms.DataGridViewTextBoxColumn BankCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChkType;
        public System.Windows.Forms.Button btnDelete;
    }
}