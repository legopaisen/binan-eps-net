namespace Modules.Tables
{
    partial class frmModuleSetup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmModuleSetup));
            this.dgvModules = new System.Windows.Forms.DataGridView();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.lblSubject = new System.Windows.Forms.Label();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.module_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.module_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvModules)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvModules
            // 
            this.dgvModules.AllowUserToAddRows = false;
            this.dgvModules.AllowUserToDeleteRows = false;
            this.dgvModules.AllowUserToResizeColumns = false;
            this.dgvModules.AllowUserToResizeRows = false;
            this.dgvModules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvModules.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.module_code,
            this.module_desc});
            this.dgvModules.Location = new System.Drawing.Point(12, 12);
            this.dgvModules.MultiSelect = false;
            this.dgvModules.Name = "dgvModules";
            this.dgvModules.ReadOnly = true;
            this.dgvModules.RowHeadersVisible = false;
            this.dgvModules.RowTemplate.Height = 24;
            this.dgvModules.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvModules.Size = new System.Drawing.Size(404, 354);
            this.dgvModules.TabIndex = 0;
            this.dgvModules.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvModules_CellClick);
            // 
            // txtCode
            // 
            this.txtCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCode.Font = new System.Drawing.Font("Calibri", 11.25F);
            this.txtCode.Location = new System.Drawing.Point(118, 391);
            this.txtCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtCode.MaximumSize = new System.Drawing.Size(116, 40);
            this.txtCode.MinimumSize = new System.Drawing.Size(116, 30);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(116, 30);
            this.txtCode.TabIndex = 1;
            // 
            // lblSubject
            // 
            this.lblSubject.AutoSize = true;
            this.lblSubject.Font = new System.Drawing.Font("Calibri", 11.25F);
            this.lblSubject.Location = new System.Drawing.Point(12, 395);
            this.lblSubject.MaximumSize = new System.Drawing.Size(70, 23);
            this.lblSubject.MinimumSize = new System.Drawing.Size(49, 23);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(49, 23);
            this.lblSubject.TabIndex = 13;
            this.lblSubject.Text = "Code";
            // 
            // txtDesc
            // 
            this.txtDesc.Font = new System.Drawing.Font("Calibri", 11.25F);
            this.txtDesc.Location = new System.Drawing.Point(118, 425);
            this.txtDesc.Margin = new System.Windows.Forms.Padding(4);
            this.txtDesc.MaximumSize = new System.Drawing.Size(400, 30);
            this.txtDesc.MinimumSize = new System.Drawing.Size(116, 30);
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(298, 30);
            this.txtDesc.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 11.25F);
            this.label1.Location = new System.Drawing.Point(12, 427);
            this.label1.MaximumSize = new System.Drawing.Size(150, 23);
            this.label1.MinimumSize = new System.Drawing.Size(49, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 23);
            this.label1.TabIndex = 15;
            this.label1.Text = "Description";
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Calibri", 11.25F);
            this.btnAdd.Location = new System.Drawing.Point(12, 474);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(92, 34);
            this.btnAdd.TabIndex = 16;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Font = new System.Drawing.Font("Calibri", 11.25F);
            this.btnEdit.Location = new System.Drawing.Point(110, 474);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(92, 34);
            this.btnEdit.TabIndex = 17;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Calibri", 11.25F);
            this.btnDelete.Location = new System.Drawing.Point(226, 474);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(92, 34);
            this.btnDelete.TabIndex = 18;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Calibri", 11.25F);
            this.btnExit.Location = new System.Drawing.Point(324, 474);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(92, 34);
            this.btnExit.TabIndex = 19;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // module_code
            // 
            this.module_code.HeaderText = "Code";
            this.module_code.Name = "module_code";
            this.module_code.ReadOnly = true;
            this.module_code.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.module_code.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // module_desc
            // 
            this.module_desc.HeaderText = "Description";
            this.module_desc.Name = "module_desc";
            this.module_desc.ReadOnly = true;
            this.module_desc.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.module_desc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.module_desc.Width = 300;
            // 
            // frmModuleSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Aqua;
            this.ClientSize = new System.Drawing.Size(432, 520);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDesc);
            this.Controls.Add(this.lblSubject);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.dgvModules);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmModuleSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Module Setup";
            this.Load += new System.EventHandler(this.frmModuleSetup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvModules)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvModules;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label lblSubject;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.DataGridViewTextBoxColumn module_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn module_desc;
    }
}