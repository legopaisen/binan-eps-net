namespace Modules.Transactions
{
    partial class frmSearchLot
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSearchLot));
            this.label1 = new System.Windows.Forms.Label();
            this.txtLotPIN = new System.Windows.Forms.TextBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.txtMI = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.PIN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OWN_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OWN_LN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OWN_FN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OWN_MI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OWN_ADDR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PropLot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PropBlk = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PINBrgy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PINDist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Lot PIN";
            // 
            // txtLotPIN
            // 
            this.txtLotPIN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLotPIN.Location = new System.Drawing.Point(124, 21);
            this.txtLotPIN.Name = "txtLotPIN";
            this.txtLotPIN.Size = new System.Drawing.Size(223, 23);
            this.txtLotPIN.TabIndex = 1;
            // 
            // txtLastName
            // 
            this.txtLastName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLastName.Location = new System.Drawing.Point(124, 48);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(295, 23);
            this.txtLastName.TabIndex = 2;
            // 
            // txtFirstName
            // 
            this.txtFirstName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFirstName.Location = new System.Drawing.Point(124, 75);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(223, 23);
            this.txtFirstName.TabIndex = 3;
            // 
            // txtMI
            // 
            this.txtMI.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMI.Location = new System.Drawing.Point(389, 75);
            this.txtMI.MaxLength = 1;
            this.txtMI.Name = "txtMI";
            this.txtMI.Size = new System.Drawing.Size(30, 23);
            this.txtMI.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(47, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Last Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(47, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "First Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(355, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "M.I.";
            // 
            // btnClear
            // 
            this.btnClear.ForeColor = System.Drawing.Color.Aqua;
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.Location = new System.Drawing.Point(389, 18);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(30, 26);
            this.btnClear.TabIndex = 6;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.ForeColor = System.Drawing.Color.Aqua;
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(353, 18);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(30, 26);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dgvList
            // 
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PIN,
            this.OWN_CODE,
            this.OWN_LN,
            this.OWN_FN,
            this.OWN_MI,
            this.OWN_ADDR,
            this.PropLot,
            this.PropBlk,
            this.PINBrgy,
            this.PINDist});
            this.dgvList.Location = new System.Drawing.Point(7, 113);
            this.dgvList.Name = "dgvList";
            this.dgvList.RowHeadersVisible = false;
            this.dgvList.Size = new System.Drawing.Size(535, 167);
            this.dgvList.TabIndex = 7;
            this.dgvList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellClick);
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(415, 299);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(67, 26);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(488, 299);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(67, 26);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PaleTurquoise;
            this.groupBox1.Controls.Add(this.dgvList);
            this.groupBox1.Controls.Add(this.txtLotPIN);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtMI);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtFirstName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtLastName);
            this.groupBox1.Location = new System.Drawing.Point(12, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(549, 292);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // PIN
            // 
            this.PIN.HeaderText = "PIN";
            this.PIN.Name = "PIN";
            // 
            // OWN_CODE
            // 
            this.OWN_CODE.HeaderText = "Account No";
            this.OWN_CODE.Name = "OWN_CODE";
            this.OWN_CODE.Width = 80;
            // 
            // OWN_LN
            // 
            this.OWN_LN.HeaderText = "Last Name";
            this.OWN_LN.Name = "OWN_LN";
            // 
            // OWN_FN
            // 
            this.OWN_FN.HeaderText = "First Name";
            this.OWN_FN.Name = "OWN_FN";
            // 
            // OWN_MI
            // 
            this.OWN_MI.HeaderText = "MI";
            this.OWN_MI.Name = "OWN_MI";
            this.OWN_MI.Width = 50;
            // 
            // OWN_ADDR
            // 
            this.OWN_ADDR.HeaderText = "Owner\'s Address";
            this.OWN_ADDR.Name = "OWN_ADDR";
            // 
            // PropLot
            // 
            this.PropLot.HeaderText = "PIN Lot No";
            this.PropLot.Name = "PropLot";
            // 
            // PropBlk
            // 
            this.PropBlk.HeaderText = "PIN Blk No";
            this.PropBlk.Name = "PropBlk";
            // 
            // PINBrgy
            // 
            this.PINBrgy.HeaderText = "PIN Brgy";
            this.PINBrgy.Name = "PINBrgy";
            // 
            // PINDist
            // 
            this.PINDist.HeaderText = "PIN Dist";
            this.PINDist.Name = "PINDist";
            // 
            // frmSearchLot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.ClientSize = new System.Drawing.Size(573, 333);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSearchLot";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Search Lot";
            this.Load += new System.EventHandler(this.frmSearchLot_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLotPIN;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.TextBox txtMI;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn PIN;
        private System.Windows.Forms.DataGridViewTextBoxColumn OWN_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn OWN_LN;
        private System.Windows.Forms.DataGridViewTextBoxColumn OWN_FN;
        private System.Windows.Forms.DataGridViewTextBoxColumn OWN_MI;
        private System.Windows.Forms.DataGridViewTextBoxColumn OWN_ADDR;
        private System.Windows.Forms.DataGridViewTextBoxColumn PropLot;
        private System.Windows.Forms.DataGridViewTextBoxColumn PropBlk;
        private System.Windows.Forms.DataGridViewTextBoxColumn PINBrgy;
        private System.Windows.Forms.DataGridViewTextBoxColumn PINDist;
    }
}