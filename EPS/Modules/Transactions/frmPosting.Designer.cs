namespace Modules.Transactions
{
    partial class frmPosting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPosting));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grpDetails = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtProjDesc = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtProjLoc = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtProjOwn = new System.Windows.Forms.TextBox();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.txtOrNo = new System.Windows.Forms.TextBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdoCash = new System.Windows.Forms.RadioButton();
            this.rdoCheck = new System.Windows.Forms.RadioButton();
            this.txtAmt = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTeller = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtBillNo = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.label11 = new System.Windows.Forms.Label();
            this.txtCash = new System.Windows.Forms.TextBox();
            this.txtAmtTendered = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fees = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtDebit = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtChange = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.arn1 = new Modules.ARN.ARN();
            this.chkOthers = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.grpDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkOthers);
            this.groupBox1.Controls.Add(this.txtProjOwn);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtProjLoc);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtProjDesc);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.arn1);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(16, 8);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(568, 157);
            this.groupBox1.TabIndex = 111;
            this.groupBox1.TabStop = false;
            // 
            // grpDetails
            // 
            this.grpDetails.Controls.Add(this.label14);
            this.grpDetails.Controls.Add(this.label13);
            this.grpDetails.Controls.Add(this.label12);
            this.grpDetails.Controls.Add(this.label11);
            this.grpDetails.Controls.Add(this.txtChange);
            this.grpDetails.Controls.Add(this.txtDebit);
            this.grpDetails.Controls.Add(this.txtAmtTendered);
            this.grpDetails.Controls.Add(this.txtCash);
            this.grpDetails.Controls.Add(this.txtBillNo);
            this.grpDetails.Controls.Add(this.label10);
            this.grpDetails.Controls.Add(this.label9);
            this.grpDetails.Controls.Add(this.btnSave);
            this.grpDetails.Controls.Add(this.label8);
            this.grpDetails.Controls.Add(this.label7);
            this.grpDetails.Controls.Add(this.label6);
            this.grpDetails.Controls.Add(this.txtTeller);
            this.grpDetails.Controls.Add(this.txtMemo);
            this.grpDetails.Controls.Add(this.txtAmt);
            this.grpDetails.Controls.Add(this.groupBox3);
            this.grpDetails.Controls.Add(this.label5);
            this.grpDetails.Controls.Add(this.dtpDate);
            this.grpDetails.Controls.Add(this.txtOrNo);
            this.grpDetails.Controls.Add(this.dgvList);
            this.grpDetails.Enabled = false;
            this.grpDetails.Location = new System.Drawing.Point(16, 167);
            this.grpDetails.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpDetails.Name = "grpDetails";
            this.grpDetails.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpDetails.Size = new System.Drawing.Size(568, 438);
            this.grpDetails.TabIndex = 112;
            this.grpDetails.TabStop = false;
            // 
            // btnClear
            // 
            this.btnClear.ForeColor = System.Drawing.Color.Aqua;
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.Location = new System.Drawing.Point(312, 19);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(30, 26);
            this.btnClear.TabIndex = 2;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.ForeColor = System.Drawing.Color.Aqua;
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(276, 19);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(30, 26);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 18);
            this.label1.TabIndex = 14;
            this.label1.Text = "AN:";
            // 
            // txtProjDesc
            // 
            this.txtProjDesc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtProjDesc.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProjDesc.Location = new System.Drawing.Point(148, 65);
            this.txtProjDesc.Name = "txtProjDesc";
            this.txtProjDesc.ReadOnly = true;
            this.txtProjDesc.Size = new System.Drawing.Size(399, 23);
            this.txtProjDesc.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 18);
            this.label2.TabIndex = 18;
            this.label2.Text = "Project Description:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 18);
            this.label3.TabIndex = 18;
            this.label3.Text = "Project Location:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtProjLoc
            // 
            this.txtProjLoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtProjLoc.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProjLoc.Location = new System.Drawing.Point(148, 94);
            this.txtProjLoc.Name = "txtProjLoc";
            this.txtProjLoc.ReadOnly = true;
            this.txtProjLoc.Size = new System.Drawing.Size(399, 23);
            this.txtProjLoc.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 18);
            this.label4.TabIndex = 18;
            this.label4.Text = "Project Owner:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtProjOwn
            // 
            this.txtProjOwn.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtProjOwn.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProjOwn.Location = new System.Drawing.Point(148, 123);
            this.txtProjOwn.Name = "txtProjOwn";
            this.txtProjOwn.ReadOnly = true;
            this.txtProjOwn.Size = new System.Drawing.Size(399, 23);
            this.txtProjOwn.TabIndex = 19;
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToDeleteRows = false;
            this.dgvList.AllowUserToResizeRows = false;
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Code,
            this.Fees,
            this.Amount});
            this.dgvList.Location = new System.Drawing.Point(7, 17);
            this.dgvList.Name = "dgvList";
            this.dgvList.ReadOnly = true;
            this.dgvList.RowHeadersVisible = false;
            this.dgvList.Size = new System.Drawing.Size(424, 218);
            this.dgvList.TabIndex = 3;
            this.dgvList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellClick);
            this.dgvList.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellLeave);
            this.dgvList.Leave += new System.EventHandler(this.dgvList_Leave);
            // 
            // txtOrNo
            // 
            this.txtOrNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtOrNo.Location = new System.Drawing.Point(446, 147);
            this.txtOrNo.Name = "txtOrNo";
            this.txtOrNo.Size = new System.Drawing.Size(113, 26);
            this.txtOrNo.TabIndex = 5;
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(446, 94);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(111, 26);
            this.dtpDate.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(443, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 18);
            this.label5.TabIndex = 20;
            this.label5.Text = "OR No:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdoCheck);
            this.groupBox3.Controls.Add(this.rdoCash);
            this.groupBox3.Location = new System.Drawing.Point(437, 180);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(122, 76);
            this.groupBox3.TabIndex = 113;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Payment Mode";
            // 
            // rdoCash
            // 
            this.rdoCash.AutoSize = true;
            this.rdoCash.Location = new System.Drawing.Point(23, 25);
            this.rdoCash.Name = "rdoCash";
            this.rdoCash.Size = new System.Drawing.Size(55, 22);
            this.rdoCash.TabIndex = 6;
            this.rdoCash.TabStop = true;
            this.rdoCash.Text = "Cash";
            this.rdoCash.UseVisualStyleBackColor = true;
            this.rdoCash.CheckedChanged += new System.EventHandler(this.rdoCash_CheckedChanged);
            // 
            // rdoCheck
            // 
            this.rdoCheck.AutoSize = true;
            this.rdoCheck.Location = new System.Drawing.Point(23, 48);
            this.rdoCheck.Name = "rdoCheck";
            this.rdoCheck.Size = new System.Drawing.Size(63, 22);
            this.rdoCheck.TabIndex = 7;
            this.rdoCheck.TabStop = true;
            this.rdoCheck.Text = "Check";
            this.rdoCheck.UseVisualStyleBackColor = true;
            this.rdoCheck.CheckedChanged += new System.EventHandler(this.rdoCheck_CheckedChanged);
            // 
            // txtAmt
            // 
            this.txtAmt.Location = new System.Drawing.Point(317, 241);
            this.txtAmt.Name = "txtAmt";
            this.txtAmt.ReadOnly = true;
            this.txtAmt.Size = new System.Drawing.Size(113, 26);
            this.txtAmt.TabIndex = 22;
            this.txtAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(188, 244);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(123, 18);
            this.label6.TabIndex = 23;
            this.label6.Text = "Total Amount Due:";
            // 
            // txtMemo
            // 
            this.txtMemo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMemo.Location = new System.Drawing.Point(7, 270);
            this.txtMemo.Multiline = true;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(194, 121);
            this.txtMemo.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 249);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 18);
            this.label7.TabIndex = 23;
            this.label7.Text = "Memo:";
            // 
            // txtTeller
            // 
            this.txtTeller.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTeller.Location = new System.Drawing.Point(58, 397);
            this.txtTeller.Name = "txtTeller";
            this.txtTeller.Size = new System.Drawing.Size(143, 26);
            this.txtTeller.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 400);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 18);
            this.label8.TabIndex = 23;
            this.label8.Text = "Teller:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(451, 395);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(108, 28);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(467, 612);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(108, 28);
            this.btnExit.TabIndex = 11;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(445, 73);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 18);
            this.label9.TabIndex = 25;
            this.label9.Text = "OR Date:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(443, 17);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 18);
            this.label10.TabIndex = 114;
            this.label10.Text = "Billing No:";
            // 
            // txtBillNo
            // 
            this.txtBillNo.Location = new System.Drawing.Point(446, 38);
            this.txtBillNo.Name = "txtBillNo";
            this.txtBillNo.ReadOnly = true;
            this.txtBillNo.Size = new System.Drawing.Size(113, 26);
            this.txtBillNo.TabIndex = 115;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(270, 272);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 18);
            this.label11.TabIndex = 117;
            this.label11.Text = "Cash:";
            // 
            // txtCash
            // 
            this.txtCash.Location = new System.Drawing.Point(317, 269);
            this.txtCash.Name = "txtCash";
            this.txtCash.ReadOnly = true;
            this.txtCash.Size = new System.Drawing.Size(113, 26);
            this.txtCash.TabIndex = 8;
            this.txtCash.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCash.Leave += new System.EventHandler(this.txtCash_Leave);
            // 
            // txtAmtTendered
            // 
            this.txtAmtTendered.Location = new System.Drawing.Point(317, 297);
            this.txtAmtTendered.Name = "txtAmtTendered";
            this.txtAmtTendered.ReadOnly = true;
            this.txtAmtTendered.Size = new System.Drawing.Size(113, 26);
            this.txtAmtTendered.TabIndex = 9;
            this.txtAmtTendered.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(207, 300);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(104, 18);
            this.label12.TabIndex = 117;
            this.label12.Text = "Amt. Tendered:";
            // 
            // Code
            // 
            this.Code.HeaderText = "Code";
            this.Code.Name = "Code";
            this.Code.Width = 70;
            // 
            // Fees
            // 
            this.Fees.HeaderText = "Fees";
            this.Fees.Name = "Fees";
            this.Fees.Width = 230;
            // 
            // Amount
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = "0";
            this.Amount.DefaultCellStyle = dataGridViewCellStyle3;
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            this.Amount.Width = 120;
            // 
            // txtDebit
            // 
            this.txtDebit.Location = new System.Drawing.Point(317, 325);
            this.txtDebit.Name = "txtDebit";
            this.txtDebit.ReadOnly = true;
            this.txtDebit.Size = new System.Drawing.Size(113, 26);
            this.txtDebit.TabIndex = 10;
            this.txtDebit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(221, 329);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(90, 18);
            this.label13.TabIndex = 117;
            this.label13.Text = "Debit/Credit:";
            // 
            // txtChange
            // 
            this.txtChange.Location = new System.Drawing.Point(317, 353);
            this.txtChange.Name = "txtChange";
            this.txtChange.ReadOnly = true;
            this.txtChange.Size = new System.Drawing.Size(113, 26);
            this.txtChange.TabIndex = 11;
            this.txtChange.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(248, 356);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(58, 18);
            this.label14.TabIndex = 117;
            this.label14.Text = "Change:";
            // 
            // arn1
            // 
            //this.arn1.GetCode = "";
            this.arn1.GetLGUCode = "";
            //this.arn1.GetMonth = "";
            this.arn1.GetDistCode = "";
            this.arn1.GetSeries = "";
            this.arn1.GetTaxYear = "";
            this.arn1.Location = new System.Drawing.Point(58, 15);
            this.arn1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.arn1.Name = "arn1";
            this.arn1.Size = new System.Drawing.Size(210, 36);
            this.arn1.TabIndex = 0;
            // 
            // chkOthers
            // 
            this.chkOthers.AutoSize = true;
            this.chkOthers.Location = new System.Drawing.Point(382, 19);
            this.chkOthers.Name = "chkOthers";
            this.chkOthers.Size = new System.Drawing.Size(135, 22);
            this.chkOthers.TabIndex = 20;
            this.chkOthers.Text = "Other Certificates";
            this.chkOthers.UseVisualStyleBackColor = true;
            this.chkOthers.CheckedChanged += new System.EventHandler(this.chkOthers_CheckedChanged);
            // 
            // frmPosting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(597, 646);
            this.Controls.Add(this.grpDetails);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPosting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Posting";
            this.Load += new System.EventHandler(this.frmPosting_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpDetails.ResumeLayout(false);
            this.grpDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox grpDetails;
        public ARN.ARN arn1;
        public System.Windows.Forms.Button btnClear;
        public System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtProjDesc;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtProjOwn;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtProjLoc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.TextBox txtOrNo;
        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rdoCheck;
        private System.Windows.Forms.RadioButton rdoCash;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTeller;
        private System.Windows.Forms.TextBox txtMemo;
        private System.Windows.Forms.TextBox txtAmt;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtBillNo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtAmtTendered;
        private System.Windows.Forms.TextBox txtCash;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtChange;
        private System.Windows.Forms.TextBox txtDebit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fees;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.CheckBox chkOthers;
    }
}