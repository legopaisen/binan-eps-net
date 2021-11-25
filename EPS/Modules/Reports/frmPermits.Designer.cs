namespace Modules.Reports
{
    partial class frmPermits
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPermits));
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.dtIssued = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.txtOr = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtEngr = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtProjCost = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtScope = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBrgy = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtStreet = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBlk = new System.Windows.Forms.TextBox();
            this.lblLot = new System.Windows.Forms.Label();
            this.txtLot = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtProjDesc = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.cmbFilter = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rdoWithPermit = new System.Windows.Forms.RadioButton();
            this.rdoWithoutPermit = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtTCT = new System.Windows.Forms.TextBox();
            this.txtOwnAddr = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtOwner = new System.Windows.Forms.TextBox();
            this.txtPermitCode = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPermitYear = new System.Windows.Forms.TextBox();
            this.txtPermitMonth = new System.Windows.Forms.TextBox();
            this.txtPermitSeries = new System.Windows.Forms.TextBox();
            this.btnEditPermit = new System.Windows.Forms.Button();
            this.an1 = new Modules.ARN.AN();
            this.label17 = new System.Windows.Forms.Label();
            this.txtFlrArea = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtStoreys = new System.Windows.Forms.TextBox();
            this.AN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProjDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProjOwner = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Lot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BlkNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Street = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.brgy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.City = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TCT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FloorArea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Storeys = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Scope = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProjCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Engr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PermitNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OwnerCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PermitCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateIssued = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToDeleteRows = false;
            this.dgvList.AllowUserToOrderColumns = true;
            this.dgvList.AllowUserToResizeRows = false;
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AN,
            this.ProjDesc,
            this.ProjOwner,
            this.Lot,
            this.BlkNo,
            this.Street,
            this.brgy,
            this.City,
            this.TCT,
            this.FloorArea,
            this.Storeys,
            this.Scope,
            this.ProjCost,
            this.Engr,
            this.OR,
            this.PermitNo,
            this.OwnerCode,
            this.PermitCode,
            this.DateIssued});
            this.dgvList.Location = new System.Drawing.Point(12, 34);
            this.dgvList.MultiSelect = false;
            this.dgvList.Name = "dgvList";
            this.dgvList.ReadOnly = true;
            this.dgvList.RowHeadersVisible = false;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvList.Size = new System.Drawing.Size(723, 203);
            this.dgvList.TabIndex = 0;
            this.dgvList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.txtStoreys);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.txtFlrArea);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.dtIssued);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtOr);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtEngr);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtProjCost);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtScope);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtCity);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtBrgy);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtStreet);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtBlk);
            this.groupBox1.Controls.Add(this.lblLot);
            this.groupBox1.Controls.Add(this.txtLot);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtProjDesc);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.an1);
            this.groupBox1.Location = new System.Drawing.Point(12, 243);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(723, 179);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Project Info";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(486, 19);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(32, 15);
            this.label16.TabIndex = 27;
            this.label16.Text = "Date";
            // 
            // dtIssued
            // 
            this.dtIssued.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtIssued.Location = new System.Drawing.Point(569, 16);
            this.dtIssued.Name = "dtIssued";
            this.dtIssued.Size = new System.Drawing.Size(146, 23);
            this.dtIssued.TabIndex = 26;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(486, 146);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(44, 15);
            this.label12.TabIndex = 25;
            this.label12.Text = "OR No.";
            // 
            // txtOr
            // 
            this.txtOr.Location = new System.Drawing.Point(569, 143);
            this.txtOr.Name = "txtOr";
            this.txtOr.Size = new System.Drawing.Size(146, 23);
            this.txtOr.TabIndex = 24;
            this.txtOr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(486, 82);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 15);
            this.label11.TabIndex = 23;
            this.label11.Text = "Professional";
            // 
            // txtEngr
            // 
            this.txtEngr.Location = new System.Drawing.Point(569, 79);
            this.txtEngr.Name = "txtEngr";
            this.txtEngr.Size = new System.Drawing.Size(146, 23);
            this.txtEngr.TabIndex = 22;
            this.txtEngr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(486, 117);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 15);
            this.label9.TabIndex = 21;
            this.label9.Text = "Proj Cost";
            // 
            // txtProjCost
            // 
            this.txtProjCost.Location = new System.Drawing.Point(569, 114);
            this.txtProjCost.Name = "txtProjCost";
            this.txtProjCost.Size = new System.Drawing.Size(146, 23);
            this.txtProjCost.TabIndex = 20;
            this.txtProjCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(486, 53);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 15);
            this.label8.TabIndex = 19;
            this.label8.Text = "Scope";
            // 
            // txtScope
            // 
            this.txtScope.Location = new System.Drawing.Point(569, 50);
            this.txtScope.Name = "txtScope";
            this.txtScope.Size = new System.Drawing.Size(146, 23);
            this.txtScope.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(285, 82);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(28, 15);
            this.label7.TabIndex = 17;
            this.label7.Text = "City";
            // 
            // txtCity
            // 
            this.txtCity.Location = new System.Drawing.Point(324, 79);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(156, 23);
            this.txtCity.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(285, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 15);
            this.label6.TabIndex = 15;
            this.label6.Text = "Brgy";
            // 
            // txtBrgy
            // 
            this.txtBrgy.Location = new System.Drawing.Point(324, 50);
            this.txtBrgy.Name = "txtBrgy";
            this.txtBrgy.Size = new System.Drawing.Size(156, 23);
            this.txtBrgy.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 140);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 15);
            this.label5.TabIndex = 13;
            this.label5.Text = "Street";
            // 
            // txtStreet
            // 
            this.txtStreet.Location = new System.Drawing.Point(74, 137);
            this.txtStreet.Name = "txtStreet";
            this.txtStreet.Size = new System.Drawing.Size(205, 23);
            this.txtStreet.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 15);
            this.label4.TabIndex = 11;
            this.label4.Text = "Blk No";
            // 
            // txtBlk
            // 
            this.txtBlk.Location = new System.Drawing.Point(74, 108);
            this.txtBlk.Name = "txtBlk";
            this.txtBlk.Size = new System.Drawing.Size(205, 23);
            this.txtBlk.TabIndex = 10;
            // 
            // lblLot
            // 
            this.lblLot.AutoSize = true;
            this.lblLot.Location = new System.Drawing.Point(9, 82);
            this.lblLot.Name = "lblLot";
            this.lblLot.Size = new System.Drawing.Size(41, 15);
            this.lblLot.TabIndex = 9;
            this.lblLot.Text = "Lot No";
            // 
            // txtLot
            // 
            this.txtLot.Location = new System.Drawing.Point(74, 79);
            this.txtLot.Name = "txtLot";
            this.txtLot.Size = new System.Drawing.Size(205, 23);
            this.txtLot.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "Proj Desc";
            // 
            // txtProjDesc
            // 
            this.txtProjDesc.Location = new System.Drawing.Point(74, 50);
            this.txtProjDesc.Name = "txtProjDesc";
            this.txtProjDesc.Size = new System.Drawing.Size(205, 23);
            this.txtProjDesc.TabIndex = 6;
            // 
            // btnClear
            // 
            this.btnClear.ForeColor = System.Drawing.Color.Aqua;
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.Location = new System.Drawing.Point(234, 16);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(26, 23);
            this.btnClear.TabIndex = 4;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.ForeColor = System.Drawing.Color.Aqua;
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(203, 16);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(26, 23);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "AN:";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(555, 517);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(87, 31);
            this.btnGenerate.TabIndex = 4;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(648, 517);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(87, 31);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // cmbFilter
            // 
            this.cmbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilter.FormattingEnabled = true;
            this.cmbFilter.Items.AddRange(new object[] {
            "PAID",
            "UNPAID"});
            this.cmbFilter.Location = new System.Drawing.Point(52, 6);
            this.cmbFilter.Name = "cmbFilter";
            this.cmbFilter.Size = new System.Drawing.Size(123, 23);
            this.cmbFilter.TabIndex = 6;
            this.cmbFilter.SelectedIndexChanged += new System.EventHandler(this.cmbFilter_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Filter";
            // 
            // rdoWithPermit
            // 
            this.rdoWithPermit.AutoSize = true;
            this.rdoWithPermit.Location = new System.Drawing.Point(297, 9);
            this.rdoWithPermit.Name = "rdoWithPermit";
            this.rdoWithPermit.Size = new System.Drawing.Size(91, 19);
            this.rdoWithPermit.TabIndex = 7;
            this.rdoWithPermit.TabStop = true;
            this.rdoWithPermit.Text = "With Permit";
            this.rdoWithPermit.UseVisualStyleBackColor = true;
            this.rdoWithPermit.CheckedChanged += new System.EventHandler(this.rdoWithPermit_CheckedChanged);
            // 
            // rdoWithoutPermit
            // 
            this.rdoWithoutPermit.AutoSize = true;
            this.rdoWithoutPermit.Location = new System.Drawing.Point(182, 9);
            this.rdoWithoutPermit.Name = "rdoWithoutPermit";
            this.rdoWithoutPermit.Size = new System.Drawing.Size(109, 19);
            this.rdoWithoutPermit.TabIndex = 8;
            this.rdoWithoutPermit.TabStop = true;
            this.rdoWithoutPermit.Text = "Without Permit";
            this.rdoWithoutPermit.UseVisualStyleBackColor = true;
            this.rdoWithoutPermit.CheckedChanged += new System.EventHandler(this.rdoWithoutPermit_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.txtTCT);
            this.groupBox2.Controls.Add(this.txtOwnAddr);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.txtOwner);
            this.groupBox2.Location = new System.Drawing.Point(12, 428);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(723, 83);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Owner Info";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(9, 54);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(47, 15);
            this.label15.TabIndex = 32;
            this.label15.Text = "TCT No.";
            // 
            // txtTCT
            // 
            this.txtTCT.Location = new System.Drawing.Point(92, 51);
            this.txtTCT.Name = "txtTCT";
            this.txtTCT.Size = new System.Drawing.Size(123, 23);
            this.txtTCT.TabIndex = 31;
            // 
            // txtOwnAddr
            // 
            this.txtOwnAddr.Location = new System.Drawing.Point(428, 22);
            this.txtOwnAddr.Name = "txtOwnAddr";
            this.txtOwnAddr.Size = new System.Drawing.Size(273, 23);
            this.txtOwnAddr.TabIndex = 30;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(371, 25);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(51, 15);
            this.label14.TabIndex = 29;
            this.label14.Text = "Address";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(9, 25);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(77, 15);
            this.label13.TabIndex = 27;
            this.label13.Text = "Owner Name";
            // 
            // txtOwner
            // 
            this.txtOwner.Location = new System.Drawing.Point(92, 22);
            this.txtOwner.Name = "txtOwner";
            this.txtOwner.Size = new System.Drawing.Size(273, 23);
            this.txtOwner.TabIndex = 26;
            // 
            // txtPermitCode
            // 
            this.txtPermitCode.Location = new System.Drawing.Point(513, 6);
            this.txtPermitCode.MaxLength = 3;
            this.txtPermitCode.Name = "txtPermitCode";
            this.txtPermitCode.Size = new System.Drawing.Size(40, 23);
            this.txtPermitCode.TabIndex = 22;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(446, 10);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 15);
            this.label10.TabIndex = 23;
            this.label10.Text = "Permit No";
            // 
            // txtPermitYear
            // 
            this.txtPermitYear.Location = new System.Drawing.Point(560, 6);
            this.txtPermitYear.MaxLength = 4;
            this.txtPermitYear.Name = "txtPermitYear";
            this.txtPermitYear.Size = new System.Drawing.Size(40, 23);
            this.txtPermitYear.TabIndex = 23;
            // 
            // txtPermitMonth
            // 
            this.txtPermitMonth.Location = new System.Drawing.Point(599, 6);
            this.txtPermitMonth.MaxLength = 2;
            this.txtPermitMonth.Name = "txtPermitMonth";
            this.txtPermitMonth.Size = new System.Drawing.Size(40, 23);
            this.txtPermitMonth.TabIndex = 24;
            // 
            // txtPermitSeries
            // 
            this.txtPermitSeries.Location = new System.Drawing.Point(645, 6);
            this.txtPermitSeries.MaxLength = 4;
            this.txtPermitSeries.Name = "txtPermitSeries";
            this.txtPermitSeries.Size = new System.Drawing.Size(40, 23);
            this.txtPermitSeries.TabIndex = 25;
            this.txtPermitSeries.Leave += new System.EventHandler(this.txtPermitSeries_Leave);
            // 
            // btnEditPermit
            // 
            this.btnEditPermit.Location = new System.Drawing.Point(691, 5);
            this.btnEditPermit.Name = "btnEditPermit";
            this.btnEditPermit.Size = new System.Drawing.Size(44, 25);
            this.btnEditPermit.TabIndex = 26;
            this.btnEditPermit.Text = "Edit";
            this.btnEditPermit.UseVisualStyleBackColor = true;
            this.btnEditPermit.Visible = false;
            this.btnEditPermit.Click += new System.EventHandler(this.btnEditPermit_Click);
            // 
            // an1
            // 
            this.an1.GetCode = "AN";
            this.an1.GetSeries = "";
            this.an1.GetTaxYear = "";
            this.an1.Location = new System.Drawing.Point(40, 13);
            this.an1.Name = "an1";
            this.an1.Size = new System.Drawing.Size(164, 33);
            this.an1.TabIndex = 2;
            this.an1.Load += new System.EventHandler(this.an1_Load);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(285, 117);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(94, 15);
            this.label17.TabIndex = 29;
            this.label17.Text = "Total Floor Area";
            // 
            // txtFlrArea
            // 
            this.txtFlrArea.Enabled = false;
            this.txtFlrArea.Location = new System.Drawing.Point(385, 114);
            this.txtFlrArea.Name = "txtFlrArea";
            this.txtFlrArea.Size = new System.Drawing.Size(95, 23);
            this.txtFlrArea.TabIndex = 28;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(285, 146);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(82, 15);
            this.label18.TabIndex = 31;
            this.label18.Text = "No. of Storeys";
            // 
            // txtStoreys
            // 
            this.txtStoreys.Enabled = false;
            this.txtStoreys.Location = new System.Drawing.Point(385, 143);
            this.txtStoreys.Name = "txtStoreys";
            this.txtStoreys.Size = new System.Drawing.Size(95, 23);
            this.txtStoreys.TabIndex = 30;
            // 
            // AN
            // 
            this.AN.HeaderText = "AN";
            this.AN.Name = "AN";
            this.AN.ReadOnly = true;
            // 
            // ProjDesc
            // 
            this.ProjDesc.HeaderText = "Proj Desc";
            this.ProjDesc.Name = "ProjDesc";
            this.ProjDesc.ReadOnly = true;
            // 
            // ProjOwner
            // 
            this.ProjOwner.HeaderText = "Proj Owner";
            this.ProjOwner.Name = "ProjOwner";
            this.ProjOwner.ReadOnly = true;
            // 
            // Lot
            // 
            this.Lot.HeaderText = "Lot";
            this.Lot.Name = "Lot";
            this.Lot.ReadOnly = true;
            // 
            // BlkNo
            // 
            this.BlkNo.HeaderText = "Blk No";
            this.BlkNo.Name = "BlkNo";
            this.BlkNo.ReadOnly = true;
            // 
            // Street
            // 
            this.Street.HeaderText = "Street";
            this.Street.Name = "Street";
            this.Street.ReadOnly = true;
            // 
            // brgy
            // 
            this.brgy.HeaderText = "Brgy";
            this.brgy.Name = "brgy";
            this.brgy.ReadOnly = true;
            // 
            // City
            // 
            this.City.HeaderText = "City";
            this.City.Name = "City";
            this.City.ReadOnly = true;
            // 
            // TCT
            // 
            this.TCT.HeaderText = "TCT No";
            this.TCT.Name = "TCT";
            this.TCT.ReadOnly = true;
            // 
            // FloorArea
            // 
            this.FloorArea.HeaderText = "Tota Floor Area";
            this.FloorArea.Name = "FloorArea";
            this.FloorArea.ReadOnly = true;
            // 
            // Storeys
            // 
            this.Storeys.HeaderText = "No. of Storeys";
            this.Storeys.Name = "Storeys";
            this.Storeys.ReadOnly = true;
            // 
            // Scope
            // 
            this.Scope.HeaderText = "Scope";
            this.Scope.Name = "Scope";
            this.Scope.ReadOnly = true;
            // 
            // ProjCost
            // 
            this.ProjCost.HeaderText = "Proj Cost";
            this.ProjCost.Name = "ProjCost";
            this.ProjCost.ReadOnly = true;
            // 
            // Engr
            // 
            this.Engr.HeaderText = "Professional In Charge";
            this.Engr.Name = "Engr";
            this.Engr.ReadOnly = true;
            // 
            // OR
            // 
            this.OR.HeaderText = "OR No";
            this.OR.Name = "OR";
            this.OR.ReadOnly = true;
            // 
            // PermitNo
            // 
            this.PermitNo.HeaderText = "Permit No";
            this.PermitNo.Name = "PermitNo";
            this.PermitNo.ReadOnly = true;
            // 
            // OwnerCode
            // 
            this.OwnerCode.HeaderText = "OwnerCode";
            this.OwnerCode.Name = "OwnerCode";
            this.OwnerCode.ReadOnly = true;
            this.OwnerCode.Visible = false;
            // 
            // PermitCode
            // 
            this.PermitCode.HeaderText = "PermitCode";
            this.PermitCode.Name = "PermitCode";
            this.PermitCode.ReadOnly = true;
            this.PermitCode.Visible = false;
            // 
            // DateIssued
            // 
            this.DateIssued.HeaderText = "Date Issued";
            this.DateIssued.Name = "DateIssued";
            this.DateIssued.ReadOnly = true;
            // 
            // frmPermits
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.ClientSize = new System.Drawing.Size(751, 560);
            this.Controls.Add(this.btnEditPermit);
            this.Controls.Add(this.txtPermitSeries);
            this.Controls.Add(this.txtPermitMonth);
            this.Controls.Add(this.txtPermitYear);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.rdoWithoutPermit);
            this.Controls.Add(this.txtPermitCode);
            this.Controls.Add(this.rdoWithPermit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbFilter);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvList);
            this.Font = new System.Drawing.Font("Calibri", 9.75F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPermits";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Permits";
            this.Load += new System.EventHandler(this.frmPermits_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvList;
        public ARN.AN an1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button btnClear;
        public System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBrgy;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtStreet;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBlk;
        private System.Windows.Forms.Label lblLot;
        private System.Windows.Forms.TextBox txtLot;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtProjDesc;
        private System.Windows.Forms.ComboBox cmbFilter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rdoWithPermit;
        private System.Windows.Forms.RadioButton rdoWithoutPermit;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtEngr;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtProjCost;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtScope;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtOr;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtOwnAddr;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtOwner;
        private System.Windows.Forms.TextBox txtPermitCode;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtPermitYear;
        private System.Windows.Forms.TextBox txtPermitMonth;
        private System.Windows.Forms.TextBox txtPermitSeries;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtTCT;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.DateTimePicker dtIssued;
        private System.Windows.Forms.Button btnEditPermit;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtStoreys;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtFlrArea;
        private System.Windows.Forms.DataGridViewTextBoxColumn AN;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProjDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProjOwner;
        private System.Windows.Forms.DataGridViewTextBoxColumn Lot;
        private System.Windows.Forms.DataGridViewTextBoxColumn BlkNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Street;
        private System.Windows.Forms.DataGridViewTextBoxColumn brgy;
        private System.Windows.Forms.DataGridViewTextBoxColumn City;
        private System.Windows.Forms.DataGridViewTextBoxColumn TCT;
        private System.Windows.Forms.DataGridViewTextBoxColumn FloorArea;
        private System.Windows.Forms.DataGridViewTextBoxColumn Storeys;
        private System.Windows.Forms.DataGridViewTextBoxColumn Scope;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProjCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn Engr;
        private System.Windows.Forms.DataGridViewTextBoxColumn OR;
        private System.Windows.Forms.DataGridViewTextBoxColumn PermitNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn OwnerCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn PermitCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateIssued;
    }
}