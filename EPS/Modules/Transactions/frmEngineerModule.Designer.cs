namespace Modules.Transactions
{
    partial class frmEngineerModule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEngineerModule));
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNewAmt = new System.Windows.Forms.TextBox();
            this.txtOrigAmt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtProj = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.an1 = new Modules.ARN.AN();
            this.btnApprove = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.dgvFees = new System.Windows.Forms.DataGridView();
            this.arn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProjName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.feescode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFees)).BeginInit();
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
            this.arn,
            this.ProjName});
            this.dgvList.Location = new System.Drawing.Point(12, 12);
            this.dgvList.MultiSelect = false;
            this.dgvList.Name = "dgvList";
            this.dgvList.ReadOnly = true;
            this.dgvList.RowHeadersVisible = false;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvList.Size = new System.Drawing.Size(506, 132);
            this.dgvList.TabIndex = 0;
            this.dgvList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtProj);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.an1);
            this.groupBox1.Location = new System.Drawing.Point(12, 279);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(246, 94);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 15);
            this.label4.TabIndex = 21;
            this.label4.Text = "New Amount";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(49, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 15);
            this.label3.TabIndex = 20;
            this.label3.Text = "Orig Amt";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtNewAmt
            // 
            this.txtNewAmt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNewAmt.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNewAmt.Location = new System.Drawing.Point(110, 50);
            this.txtNewAmt.MaxLength = 40;
            this.txtNewAmt.Name = "txtNewAmt";
            this.txtNewAmt.Size = new System.Drawing.Size(135, 23);
            this.txtNewAmt.TabIndex = 19;
            this.txtNewAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtOrigAmt
            // 
            this.txtOrigAmt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtOrigAmt.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOrigAmt.Location = new System.Drawing.Point(110, 22);
            this.txtOrigAmt.MaxLength = 40;
            this.txtOrigAmt.Name = "txtOrigAmt";
            this.txtOrigAmt.Size = new System.Drawing.Size(135, 23);
            this.txtOrigAmt.TabIndex = 18;
            this.txtOrigAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 15);
            this.label2.TabIndex = 17;
            this.label2.Text = "Proj Name";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtProj
            // 
            this.txtProj.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtProj.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProj.Location = new System.Drawing.Point(76, 47);
            this.txtProj.MaxLength = 40;
            this.txtProj.Name = "txtProj";
            this.txtProj.Size = new System.Drawing.Size(159, 23);
            this.txtProj.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "AN:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // an1
            // 
            this.an1.GetCode = "";
            this.an1.GetSeries = "";
            this.an1.GetTaxYear = "";
            this.an1.Location = new System.Drawing.Point(35, 16);
            this.an1.Name = "an1";
            this.an1.Size = new System.Drawing.Size(167, 36);
            this.an1.TabIndex = 2;
            // 
            // btnApprove
            // 
            this.btnApprove.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApprove.Location = new System.Drawing.Point(290, 379);
            this.btnApprove.Name = "btnApprove";
            this.btnApprove.Size = new System.Drawing.Size(111, 26);
            this.btnApprove.TabIndex = 18;
            this.btnApprove.Text = "Approve";
            this.btnApprove.UseVisualStyleBackColor = true;
            this.btnApprove.Click += new System.EventHandler(this.btnApprove_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(407, 379);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(111, 26);
            this.btnExit.TabIndex = 19;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // dgvFees
            // 
            this.dgvFees.AllowUserToAddRows = false;
            this.dgvFees.AllowUserToDeleteRows = false;
            this.dgvFees.AllowUserToOrderColumns = true;
            this.dgvFees.AllowUserToResizeRows = false;
            this.dgvFees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFees.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.feescode,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.dgvFees.Location = new System.Drawing.Point(12, 150);
            this.dgvFees.Name = "dgvFees";
            this.dgvFees.ReadOnly = true;
            this.dgvFees.RowHeadersVisible = false;
            this.dgvFees.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFees.Size = new System.Drawing.Size(506, 132);
            this.dgvFees.TabIndex = 20;
            this.dgvFees.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFees_CellClick);
            // 
            // arn
            // 
            this.arn.HeaderText = "AN";
            this.arn.Name = "arn";
            this.arn.ReadOnly = true;
            this.arn.Width = 120;
            // 
            // ProjName
            // 
            this.ProjName.HeaderText = "ProjName";
            this.ProjName.Name = "ProjName";
            this.ProjName.ReadOnly = true;
            this.ProjName.Width = 370;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtOrigAmt);
            this.groupBox2.Controls.Add(this.txtNewAmt);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(267, 279);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(251, 94);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            // 
            // feescode
            // 
            this.feescode.HeaderText = "FeesCode";
            this.feescode.Name = "feescode";
            this.feescode.ReadOnly = true;
            this.feescode.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Fees Desc";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 300;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Orig Amount";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "New Amount";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // frmEngineerModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Aqua;
            this.ClientSize = new System.Drawing.Size(530, 415);
            this.Controls.Add(this.dgvFees);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnApprove);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvList);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Calibri", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEngineerModule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Engineer\'s Module";
            this.Load += new System.EventHandler(this.frmEngineerModule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFees)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.GroupBox groupBox1;
        public ARN.AN an1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnApprove;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtProj;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtNewAmt;
        public System.Windows.Forms.TextBox txtOrigAmt;
        private System.Windows.Forms.DataGridView dgvFees;
        private System.Windows.Forms.DataGridViewTextBoxColumn arn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProjName;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridViewTextBoxColumn feescode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    }
}