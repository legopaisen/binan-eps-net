namespace Modules.SearchAccount
{
    partial class frmSearchDOLE
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSearchDOLE));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.bntList = new System.Windows.Forms.Button();
            this.txtApplicant = new System.Windows.Forms.TextBox();
            this.txtBillNo = new System.Windows.Forms.TextBox();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.lblBillNo = new System.Windows.Forms.Label();
            this.BILL_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.APPLICANT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(339, 265);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 25);
            this.btnCancel.TabIndex = 25;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(332, 65);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(90, 25);
            this.btnClear.TabIndex = 22;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(243, 265);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(90, 25);
            this.btnOk.TabIndex = 24;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // bntList
            // 
            this.bntList.Location = new System.Drawing.Point(236, 65);
            this.bntList.Name = "bntList";
            this.bntList.Size = new System.Drawing.Size(90, 25);
            this.bntList.TabIndex = 2;
            this.bntList.Text = "List";
            this.bntList.UseVisualStyleBackColor = true;
            this.bntList.Click += new System.EventHandler(this.bntList_Click);
            // 
            // txtApplicant
            // 
            this.txtApplicant.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtApplicant.Location = new System.Drawing.Point(96, 36);
            this.txtApplicant.Name = "txtApplicant";
            this.txtApplicant.Size = new System.Drawing.Size(326, 23);
            this.txtApplicant.TabIndex = 1;
            // 
            // txtBillNo
            // 
            this.txtBillNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBillNo.Location = new System.Drawing.Point(96, 9);
            this.txtBillNo.Name = "txtBillNo";
            this.txtBillNo.Size = new System.Drawing.Size(88, 23);
            this.txtBillNo.TabIndex = 0;
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToDeleteRows = false;
            this.dgvList.AllowUserToResizeColumns = false;
            this.dgvList.AllowUserToResizeRows = false;
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BILL_NO,
            this.APPLICANT});
            this.dgvList.Location = new System.Drawing.Point(12, 96);
            this.dgvList.Name = "dgvList";
            this.dgvList.ReadOnly = true;
            this.dgvList.RowHeadersVisible = false;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvList.Size = new System.Drawing.Size(417, 163);
            this.dgvList.TabIndex = 23;
            this.dgvList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 15);
            this.label2.TabIndex = 14;
            this.label2.Text = "Applicant";
            // 
            // lblBillNo
            // 
            this.lblBillNo.AutoSize = true;
            this.lblBillNo.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBillNo.Location = new System.Drawing.Point(20, 13);
            this.lblBillNo.Name = "lblBillNo";
            this.lblBillNo.Size = new System.Drawing.Size(42, 15);
            this.lblBillNo.TabIndex = 15;
            this.lblBillNo.Text = "Bill No";
            // 
            // BILL_NO
            // 
            this.BILL_NO.HeaderText = "BILL NO";
            this.BILL_NO.Name = "BILL_NO";
            this.BILL_NO.ReadOnly = true;
            this.BILL_NO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // APPLICANT
            // 
            this.APPLICANT.HeaderText = "APPLICANT";
            this.APPLICANT.Name = "APPLICANT";
            this.APPLICANT.ReadOnly = true;
            this.APPLICANT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.APPLICANT.Width = 400;
            // 
            // frmSearchDOLE
            // 
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.ClientSize = new System.Drawing.Size(441, 297);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.bntList);
            this.Controls.Add(this.txtApplicant);
            this.Controls.Add(this.txtBillNo);
            this.Controls.Add(this.dgvList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblBillNo);
            this.Font = new System.Drawing.Font("Calibri", 9.75F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSearchDOLE";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Search DOLE";
            this.Load += new System.EventHandler(this.frmSearchDOLE_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button bntList;
        private System.Windows.Forms.TextBox txtApplicant;
        private System.Windows.Forms.TextBox txtBillNo;
        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblBillNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn BILL_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn APPLICANT;
    }
}