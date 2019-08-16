namespace Modules.Billing
{
    partial class frmBilling
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBilling));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtBillNo = new System.Windows.Forms.TextBox();
            this.txtAppDate = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtOccupancy = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.grpAddlFee = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtProjOwn = new System.Windows.Forms.TextBox();
            this.txtProjLoc = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtProjDesc = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.an1 = new Modules.ARN.AN();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvPermit = new System.Windows.Forms.DataGridView();
            this.Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Permit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpAssessment = new System.Windows.Forms.GroupBox();
            this.grpAddFees = new System.Windows.Forms.GroupBox();
            this.txtAddlAmt = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtAddlFees = new System.Windows.Forms.TextBox();
            this.btnAddlAdd = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCompute = new System.Windows.Forms.Button();
            this.dgvParameter = new System.Windows.Forms.DataGridView();
            this.Para = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtAmtDue = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dgvAssessment = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Fees = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FeesCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Means = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Area = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cumulative = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Scope = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPermit)).BeginInit();
            this.grpAssessment.SuspendLayout();
            this.grpAddFees.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParameter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAssessment)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtStatus);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtBillNo);
            this.groupBox1.Controls.Add(this.txtAppDate);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtOccupancy);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.grpAddlFee);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtProjOwn);
            this.groupBox1.Controls.Add(this.txtProjLoc);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtProjDesc);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.an1);
            this.groupBox1.Location = new System.Drawing.Point(217, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(670, 152);
            this.groupBox1.TabIndex = 100;
            this.groupBox1.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(342, 120);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 15);
            this.label11.TabIndex = 120;
            this.label11.Text = "Status";
            this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtStatus
            // 
            this.txtStatus.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtStatus.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStatus.Location = new System.Drawing.Point(389, 117);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(60, 23);
            this.txtStatus.TabIndex = 119;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Blue;
            this.label10.Location = new System.Drawing.Point(449, 17);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(73, 26);
            this.label10.TabIndex = 118;
            this.label10.Text = "Bill No.";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtBillNo
            // 
            this.txtBillNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBillNo.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBillNo.ForeColor = System.Drawing.Color.Blue;
            this.txtBillNo.Location = new System.Drawing.Point(528, 14);
            this.txtBillNo.Name = "txtBillNo";
            this.txtBillNo.ReadOnly = true;
            this.txtBillNo.Size = new System.Drawing.Size(134, 33);
            this.txtBillNo.TabIndex = 117;
            // 
            // txtAppDate
            // 
            this.txtAppDate.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAppDate.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAppDate.Location = new System.Drawing.Point(558, 117);
            this.txtAppDate.Name = "txtAppDate";
            this.txtAppDate.ReadOnly = true;
            this.txtAppDate.Size = new System.Drawing.Size(104, 23);
            this.txtAppDate.TabIndex = 116;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(455, 120);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 15);
            this.label7.TabIndex = 11;
            this.label7.Text = "Application Date";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtOccupancy
            // 
            this.txtOccupancy.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtOccupancy.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOccupancy.Location = new System.Drawing.Point(558, 88);
            this.txtOccupancy.Name = "txtOccupancy";
            this.txtOccupancy.ReadOnly = true;
            this.txtOccupancy.Size = new System.Drawing.Size(104, 23);
            this.txtOccupancy.TabIndex = 114;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(455, 91);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 15);
            this.label6.TabIndex = 11;
            this.label6.Text = "Occupancy";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // grpAddlFee
            // 
            this.grpAddlFee.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.grpAddlFee.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpAddlFee.Location = new System.Drawing.Point(558, 59);
            this.grpAddlFee.Name = "grpAddlFee";
            this.grpAddlFee.ReadOnly = true;
            this.grpAddlFee.Size = new System.Drawing.Size(104, 23);
            this.grpAddlFee.TabIndex = 112;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(455, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 15);
            this.label5.TabIndex = 11;
            this.label5.Text = "Scope of Work";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtProjOwn
            // 
            this.txtProjOwn.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtProjOwn.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProjOwn.Location = new System.Drawing.Point(87, 117);
            this.txtProjOwn.Name = "txtProjOwn";
            this.txtProjOwn.ReadOnly = true;
            this.txtProjOwn.Size = new System.Drawing.Size(242, 23);
            this.txtProjOwn.TabIndex = 115;
            // 
            // txtProjLoc
            // 
            this.txtProjLoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtProjLoc.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProjLoc.Location = new System.Drawing.Point(87, 88);
            this.txtProjLoc.Name = "txtProjLoc";
            this.txtProjLoc.ReadOnly = true;
            this.txtProjLoc.Size = new System.Drawing.Size(362, 23);
            this.txtProjLoc.TabIndex = 113;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "Owner Name";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtProjDesc
            // 
            this.txtProjDesc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtProjDesc.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProjDesc.Location = new System.Drawing.Point(87, 59);
            this.txtProjDesc.Name = "txtProjDesc";
            this.txtProjDesc.ReadOnly = true;
            this.txtProjDesc.Size = new System.Drawing.Size(362, 23);
            this.txtProjDesc.TabIndex = 111;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Location";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnClear
            // 
            this.btnClear.ForeColor = System.Drawing.Color.Aqua;
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.Location = new System.Drawing.Point(335, 18);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(30, 26);
            this.btnClear.TabIndex = 2;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Description";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnSearch
            // 
            this.btnSearch.ForeColor = System.Drawing.Color.Aqua;
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(299, 18);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(30, 26);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Application No.";
            // 
            // an1
            // 
            this.an1.GetCode = "";
            this.an1.GetMonth = "";
            this.an1.GetSeries = "";
            this.an1.GetTaxYear = "";
            this.an1.Location = new System.Drawing.Point(106, 17);
            this.an1.Name = "an1";
            this.an1.Size = new System.Drawing.Size(187, 35);
            this.an1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvPermit);
            this.groupBox2.Location = new System.Drawing.Point(12, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(201, 151);
            this.groupBox2.TabIndex = 101;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Permit Checklist";
            // 
            // dgvPermit
            // 
            this.dgvPermit.AllowUserToAddRows = false;
            this.dgvPermit.AllowUserToResizeRows = false;
            this.dgvPermit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPermit.ColumnHeadersVisible = false;
            this.dgvPermit.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Select,
            this.Permit,
            this.Code});
            this.dgvPermit.Location = new System.Drawing.Point(6, 22);
            this.dgvPermit.Name = "dgvPermit";
            this.dgvPermit.ReadOnly = true;
            this.dgvPermit.RowHeadersVisible = false;
            this.dgvPermit.Size = new System.Drawing.Size(187, 122);
            this.dgvPermit.TabIndex = 3;
            this.dgvPermit.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPermit_CellContentClick);
            // 
            // Select
            // 
            this.Select.HeaderText = " ";
            this.Select.Name = "Select";
            this.Select.ReadOnly = true;
            this.Select.Width = 30;
            // 
            // Permit
            // 
            this.Permit.HeaderText = "Permit";
            this.Permit.Name = "Permit";
            this.Permit.ReadOnly = true;
            this.Permit.Width = 150;
            // 
            // Code
            // 
            this.Code.HeaderText = "Code";
            this.Code.Name = "Code";
            this.Code.ReadOnly = true;
            this.Code.Visible = false;
            // 
            // grpAssessment
            // 
            this.grpAssessment.Controls.Add(this.grpAddFees);
            this.grpAssessment.Controls.Add(this.groupBox4);
            this.grpAssessment.Controls.Add(this.txtAmtDue);
            this.grpAssessment.Controls.Add(this.label8);
            this.grpAssessment.Controls.Add(this.dgvAssessment);
            this.grpAssessment.Location = new System.Drawing.Point(12, 162);
            this.grpAssessment.Name = "grpAssessment";
            this.grpAssessment.Size = new System.Drawing.Size(875, 319);
            this.grpAssessment.TabIndex = 102;
            this.grpAssessment.TabStop = false;
            this.grpAssessment.Text = "Assessment";
            // 
            // grpAddFees
            // 
            this.grpAddFees.Controls.Add(this.txtAddlAmt);
            this.grpAddFees.Controls.Add(this.label13);
            this.grpAddFees.Controls.Add(this.label12);
            this.grpAddFees.Controls.Add(this.txtAddlFees);
            this.grpAddFees.Controls.Add(this.btnAddlAdd);
            this.grpAddFees.Location = new System.Drawing.Point(604, 221);
            this.grpAddFees.Name = "grpAddFees";
            this.grpAddFees.Size = new System.Drawing.Size(263, 90);
            this.grpAddFees.TabIndex = 119;
            this.grpAddFees.TabStop = false;
            this.grpAddFees.Text = "Additional Fees";
            // 
            // txtAddlAmt
            // 
            this.txtAddlAmt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAddlAmt.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddlAmt.Location = new System.Drawing.Point(78, 52);
            this.txtAddlAmt.Name = "txtAddlAmt";
            this.txtAddlAmt.ReadOnly = true;
            this.txtAddlAmt.Size = new System.Drawing.Size(89, 23);
            this.txtAddlAmt.TabIndex = 123;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(20, 55);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(52, 15);
            this.label13.TabIndex = 122;
            this.label13.Text = "Amount:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 25);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(63, 15);
            this.label12.TabIndex = 121;
            this.label12.Text = "Fees Desc:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtAddlFees
            // 
            this.txtAddlFees.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAddlFees.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddlFees.Location = new System.Drawing.Point(78, 22);
            this.txtAddlFees.Name = "txtAddlFees";
            this.txtAddlFees.ReadOnly = true;
            this.txtAddlFees.Size = new System.Drawing.Size(177, 23);
            this.txtAddlFees.TabIndex = 121;
            // 
            // btnAddlAdd
            // 
            this.btnAddlAdd.Location = new System.Drawing.Point(173, 49);
            this.btnAddlAdd.Name = "btnAddlAdd";
            this.btnAddlAdd.Size = new System.Drawing.Size(82, 27);
            this.btnAddlAdd.TabIndex = 118;
            this.btnAddlAdd.Text = "Add";
            this.btnAddlAdd.UseVisualStyleBackColor = true;
            this.btnAddlAdd.Click += new System.EventHandler(this.btnAddlAdd_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.txtAmount);
            this.groupBox4.Controls.Add(this.btnOk);
            this.groupBox4.Controls.Add(this.btnCompute);
            this.groupBox4.Controls.Add(this.dgvParameter);
            this.groupBox4.Location = new System.Drawing.Point(604, 22);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(263, 193);
            this.groupBox4.TabIndex = 102;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Parameters";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(95, 131);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 15);
            this.label9.TabIndex = 117;
            this.label9.Text = "Amount Due";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtAmount
            // 
            this.txtAmount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAmount.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.Location = new System.Drawing.Point(174, 128);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.ReadOnly = true;
            this.txtAmount.Size = new System.Drawing.Size(81, 23);
            this.txtAmount.TabIndex = 117;
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(173, 157);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(82, 27);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "Add";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCompute
            // 
            this.btnCompute.Location = new System.Drawing.Point(6, 128);
            this.btnCompute.Name = "btnCompute";
            this.btnCompute.Size = new System.Drawing.Size(82, 27);
            this.btnCompute.TabIndex = 4;
            this.btnCompute.Text = "Compute";
            this.btnCompute.UseVisualStyleBackColor = true;
            this.btnCompute.Click += new System.EventHandler(this.btnCompute_Click);
            // 
            // dgvParameter
            // 
            this.dgvParameter.AllowUserToAddRows = false;
            this.dgvParameter.AllowUserToResizeRows = false;
            this.dgvParameter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvParameter.ColumnHeadersVisible = false;
            this.dgvParameter.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Para,
            this.Value});
            this.dgvParameter.Location = new System.Drawing.Point(6, 22);
            this.dgvParameter.Name = "dgvParameter";
            this.dgvParameter.RowHeadersVisible = false;
            this.dgvParameter.Size = new System.Drawing.Size(249, 100);
            this.dgvParameter.TabIndex = 3;
            this.dgvParameter.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvParameter_CellClick);
            // 
            // Para
            // 
            this.Para.HeaderText = "Parameter";
            this.Para.Name = "Para";
            this.Para.ReadOnly = true;
            // 
            // Value
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            this.Value.DefaultCellStyle = dataGridViewCellStyle2;
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            // 
            // txtAmtDue
            // 
            this.txtAmtDue.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAmtDue.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmtDue.Location = new System.Drawing.Point(442, 288);
            this.txtAmtDue.Name = "txtAmtDue";
            this.txtAmtDue.ReadOnly = true;
            this.txtAmtDue.Size = new System.Drawing.Size(150, 23);
            this.txtAmtDue.TabIndex = 118;
            this.txtAmtDue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(330, 291);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(106, 15);
            this.label8.TabIndex = 117;
            this.label8.Text = "Total Amount Due";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // dgvAssessment
            // 
            this.dgvAssessment.AllowUserToAddRows = false;
            this.dgvAssessment.AllowUserToResizeColumns = false;
            this.dgvAssessment.AllowUserToResizeRows = false;
            this.dgvAssessment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAssessment.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn1,
            this.Fees,
            this.FeesCode,
            this.Means,
            this.Unit,
            this.Area,
            this.Cumulative,
            this.UnitValue,
            this.Amount,
            this.Category,
            this.Scope});
            this.dgvAssessment.Location = new System.Drawing.Point(6, 26);
            this.dgvAssessment.Name = "dgvAssessment";
            this.dgvAssessment.RowHeadersVisible = false;
            this.dgvAssessment.Size = new System.Drawing.Size(586, 259);
            this.dgvAssessment.TabIndex = 4;
            this.dgvAssessment.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAssessment_CellContentClick);
            this.dgvAssessment.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAssessment_CellLeave);
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.HeaderText = " ";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Width = 30;
            // 
            // Fees
            // 
            this.Fees.HeaderText = "Fees Description";
            this.Fees.Name = "Fees";
            this.Fees.Width = 250;
            // 
            // FeesCode
            // 
            this.FeesCode.HeaderText = "Code";
            this.FeesCode.Name = "FeesCode";
            this.FeesCode.Visible = false;
            this.FeesCode.Width = 70;
            // 
            // Means
            // 
            this.Means.HeaderText = "Means";
            this.Means.Name = "Means";
            this.Means.Visible = false;
            // 
            // Unit
            // 
            this.Unit.HeaderText = "Unit";
            this.Unit.Name = "Unit";
            // 
            // Area
            // 
            this.Area.HeaderText = "Area";
            this.Area.Name = "Area";
            this.Area.Visible = false;
            // 
            // Cumulative
            // 
            this.Cumulative.HeaderText = "Cumulative";
            this.Cumulative.Name = "Cumulative";
            this.Cumulative.Visible = false;
            // 
            // UnitValue
            // 
            this.UnitValue.HeaderText = "Unit Value";
            this.UnitValue.Name = "UnitValue";
            // 
            // Amount
            // 
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            // 
            // Category
            // 
            this.Category.HeaderText = "Category";
            this.Category.Name = "Category";
            this.Category.Visible = false;
            // 
            // Scope
            // 
            this.Scope.HeaderText = "Scope";
            this.Scope.Name = "Scope";
            this.Scope.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(578, 490);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 27);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save Billing";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Enabled = false;
            this.btnPrint.Location = new System.Drawing.Point(684, 490);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(100, 27);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.Text = "Print OP";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(790, 490);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 27);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmBilling
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 525);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grpAssessment);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBilling";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Billing";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBilling_FormClosing);
            this.Load += new System.EventHandler(this.frmBilling_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPermit)).EndInit();
            this.grpAssessment.ResumeLayout(false);
            this.grpAssessment.PerformLayout();
            this.grpAddFees.ResumeLayout(false);
            this.grpAddFees.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParameter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAssessment)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button btnClear;
        public System.Windows.Forms.Button btnSearch;
        public System.Windows.Forms.TextBox txtProjLoc;
        public System.Windows.Forms.TextBox txtProjDesc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtProjOwn;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox grpAddlFee;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox txtAppDate;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox txtOccupancy;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        public ARN.AN an1;
        public System.Windows.Forms.DataGridView dgvPermit;
        public System.Windows.Forms.DataGridView dgvAssessment;
        public System.Windows.Forms.TextBox txtAmtDue;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox4;
        public System.Windows.Forms.DataGridView dgvParameter;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fees;
        private System.Windows.Forms.DataGridViewTextBoxColumn FeesCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Means;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Area;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cumulative;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Category;
        private System.Windows.Forms.DataGridViewTextBoxColumn Scope;
        public System.Windows.Forms.Button btnOk;
        public System.Windows.Forms.Button btnCompute;
        private System.Windows.Forms.DataGridViewTextBoxColumn Para;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Select;
        private System.Windows.Forms.DataGridViewTextBoxColumn Permit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox txtBillNo;
        public System.Windows.Forms.Button btnPrint;
        public System.Windows.Forms.GroupBox grpAssessment;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.TextBox txtStatus;
        public System.Windows.Forms.Button btnSave;
        public System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.Button btnAddlAdd;
        public System.Windows.Forms.TextBox txtAddlAmt;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        public System.Windows.Forms.TextBox txtAddlFees;
        public System.Windows.Forms.GroupBox grpAddFees;
    }
}