namespace Modules.Tables
{
    partial class frmRequirement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRequirement));
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.ReqID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Application = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblSubject = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.cmbApplication = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbFilter = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
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
            this.ReqID,
            this.Desc,
            this.Application});
            this.dgvList.Location = new System.Drawing.Point(12, 35);
            this.dgvList.Name = "dgvList";
            this.dgvList.ReadOnly = true;
            this.dgvList.RowHeadersVisible = false;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvList.Size = new System.Drawing.Size(386, 150);
            this.dgvList.TabIndex = 0;
            this.dgvList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellClick);
            // 
            // ReqID
            // 
            this.ReqID.HeaderText = "Req ID";
            this.ReqID.Name = "ReqID";
            this.ReqID.ReadOnly = true;
            this.ReqID.Width = 50;
            // 
            // Desc
            // 
            this.Desc.HeaderText = "Description";
            this.Desc.Name = "Desc";
            this.Desc.ReadOnly = true;
            this.Desc.Width = 230;
            // 
            // Application
            // 
            this.Application.HeaderText = "Application";
            this.Application.Name = "Application";
            this.Application.ReadOnly = true;
            // 
            // lblSubject
            // 
            this.lblSubject.AutoSize = true;
            this.lblSubject.Location = new System.Drawing.Point(12, 199);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(21, 18);
            this.lblSubject.TabIndex = 14;
            this.lblSubject.Text = "ID";
            // 
            // txtID
            // 
            this.txtID.Enabled = false;
            this.txtID.Location = new System.Drawing.Point(40, 196);
            this.txtID.Margin = new System.Windows.Forms.Padding(4);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(46, 26);
            this.txtID.TabIndex = 13;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 233);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(78, 18);
            this.label11.TabIndex = 16;
            this.label11.Text = "Description";
            // 
            // txtDesc
            // 
            this.txtDesc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDesc.Enabled = false;
            this.txtDesc.Location = new System.Drawing.Point(97, 230);
            this.txtDesc.Margin = new System.Windows.Forms.Padding(4);
            this.txtDesc.MaxLength = 500;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(298, 26);
            this.txtDesc.TabIndex = 15;
            // 
            // cmbApplication
            // 
            this.cmbApplication.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbApplication.FormattingEnabled = true;
            this.cmbApplication.Location = new System.Drawing.Point(177, 196);
            this.cmbApplication.Name = "cmbApplication";
            this.cmbApplication.Size = new System.Drawing.Size(161, 26);
            this.cmbApplication.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(93, 199);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 18);
            this.label1.TabIndex = 18;
            this.label1.Text = "Application";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(208, 274);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(92, 34);
            this.btnDelete.TabIndex = 21;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(110, 274);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(92, 34);
            this.btnEdit.TabIndex = 20;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(12, 274);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(92, 34);
            this.btnAdd.TabIndex = 19;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(306, 274);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(92, 34);
            this.btnExit.TabIndex = 22;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 18);
            this.label2.TabIndex = 24;
            this.label2.Text = "Filter";
            // 
            // cmbFilter
            // 
            this.cmbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilter.FormattingEnabled = true;
            this.cmbFilter.Location = new System.Drawing.Point(59, 3);
            this.cmbFilter.Name = "cmbFilter";
            this.cmbFilter.Size = new System.Drawing.Size(112, 26);
            this.cmbFilter.TabIndex = 23;
            this.cmbFilter.SelectedIndexChanged += new System.EventHandler(this.cmbFilter_SelectedIndexChanged);
            // 
            // frmRequirement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.ClientSize = new System.Drawing.Size(410, 325);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbFilter);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbApplication);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtDesc);
            this.Controls.Add(this.lblSubject);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.dgvList);
            this.Font = new System.Drawing.Font("Calibri", 11.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRequirement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Requirements Table";
            this.Load += new System.EventHandler(this.frmRequirement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.Label lblSubject;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.ComboBox cmbApplication;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReqID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn Application;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbFilter;
    }
}