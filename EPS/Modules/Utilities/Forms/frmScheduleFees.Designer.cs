namespace Modules.Utilities.Forms
{
    partial class frmScheduleFees
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSubAcctDescNew = new System.Windows.Forms.TextBox();
            this.txtRevenueAcctNew = new System.Windows.Forms.TextBox();
            this.txtSubAcctDesc = new System.Windows.Forms.TextBox();
            this.txtRevenueAcct = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbRevenueAcct = new MultiColumnComboBoxDemo.MultiColumnComboBox();
            this.cmbSubAcctDesc = new MultiColumnComboBoxDemo.MultiColumnComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvCategory = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvMajorFees = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Subsidiaries = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Term = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Means = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Scope = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cumulative = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Area = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Structure = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvStruc = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvScope = new System.Windows.Forms.DataGridView();
            this.Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvSchedule = new System.Windows.Forms.DataGridView();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMajorFees)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStruc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvScope)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedule)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtSubAcctDescNew);
            this.groupBox1.Controls.Add(this.txtRevenueAcctNew);
            this.groupBox1.Controls.Add(this.txtSubAcctDesc);
            this.groupBox1.Controls.Add(this.txtRevenueAcct);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbType);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbRevenueAcct);
            this.groupBox1.Controls.Add(this.cmbSubAcctDesc);
            this.groupBox1.Location = new System.Drawing.Point(16, 7);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(766, 89);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // txtSubAcctDescNew
            // 
            this.txtSubAcctDescNew.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSubAcctDescNew.Location = new System.Drawing.Point(194, 51);
            this.txtSubAcctDescNew.Name = "txtSubAcctDescNew";
            this.txtSubAcctDescNew.Size = new System.Drawing.Size(376, 26);
            this.txtSubAcctDescNew.TabIndex = 7;
            this.txtSubAcctDescNew.Visible = false;
            // 
            // txtRevenueAcctNew
            // 
            this.txtRevenueAcctNew.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRevenueAcctNew.Location = new System.Drawing.Point(194, 18);
            this.txtRevenueAcctNew.Name = "txtRevenueAcctNew";
            this.txtRevenueAcctNew.Size = new System.Drawing.Size(376, 26);
            this.txtRevenueAcctNew.TabIndex = 3;
            this.txtRevenueAcctNew.Visible = false;
            // 
            // txtSubAcctDesc
            // 
            this.txtSubAcctDesc.Location = new System.Drawing.Point(141, 51);
            this.txtSubAcctDesc.Name = "txtSubAcctDesc";
            this.txtSubAcctDesc.ReadOnly = true;
            this.txtSubAcctDesc.Size = new System.Drawing.Size(47, 26);
            this.txtSubAcctDesc.TabIndex = 5;
            // 
            // txtRevenueAcct
            // 
            this.txtRevenueAcct.Location = new System.Drawing.Point(141, 18);
            this.txtRevenueAcct.Name = "txtRevenueAcct";
            this.txtRevenueAcct.ReadOnly = true;
            this.txtRevenueAcct.Size = new System.Drawing.Size(47, 26);
            this.txtRevenueAcct.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(588, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 18);
            this.label3.TabIndex = 3;
            this.label3.Text = "Type:";
            // 
            // cmbType
            // 
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(637, 18);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(121, 26);
            this.cmbType.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(47, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "Subcategory:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Revenue Account:";
            // 
            // cmbRevenueAcct
            // 
            this.cmbRevenueAcct.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbRevenueAcct.FormattingEnabled = true;
            this.cmbRevenueAcct.Location = new System.Drawing.Point(194, 17);
            this.cmbRevenueAcct.Name = "cmbRevenueAcct";
            this.cmbRevenueAcct.Size = new System.Drawing.Size(388, 27);
            this.cmbRevenueAcct.TabIndex = 2;
            this.cmbRevenueAcct.SelectedIndexChanged += new System.EventHandler(this.cmbRevenueAcct_SelectedIndexChanged);
            // 
            // cmbSubAcctDesc
            // 
            this.cmbSubAcctDesc.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbSubAcctDesc.FormattingEnabled = true;
            this.cmbSubAcctDesc.Location = new System.Drawing.Point(194, 51);
            this.cmbSubAcctDesc.Name = "cmbSubAcctDesc";
            this.cmbSubAcctDesc.Size = new System.Drawing.Size(388, 27);
            this.cmbSubAcctDesc.TabIndex = 6;
            this.cmbSubAcctDesc.SelectedIndexChanged += new System.EventHandler(this.cmbSubAcctDesc_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvCategory);
            this.groupBox2.Controls.Add(this.dgvMajorFees);
            this.groupBox2.Controls.Add(this.dgvStruc);
            this.groupBox2.Controls.Add(this.dgvScope);
            this.groupBox2.Location = new System.Drawing.Point(16, 95);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(766, 318);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // dgvCategory
            // 
            this.dgvCategory.AllowUserToAddRows = false;
            this.dgvCategory.AllowUserToDeleteRows = false;
            this.dgvCategory.AllowUserToResizeColumns = false;
            this.dgvCategory.AllowUserToResizeRows = false;
            this.dgvCategory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCategory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn2,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5});
            this.dgvCategory.Location = new System.Drawing.Point(200, 17);
            this.dgvCategory.Name = "dgvCategory";
            this.dgvCategory.RowHeadersVisible = false;
            this.dgvCategory.Size = new System.Drawing.Size(185, 138);
            this.dgvCategory.TabIndex = 9;
            // 
            // dataGridViewCheckBoxColumn2
            // 
            this.dataGridViewCheckBoxColumn2.HeaderText = " ";
            this.dataGridViewCheckBoxColumn2.Name = "dataGridViewCheckBoxColumn2";
            this.dataGridViewCheckBoxColumn2.Width = 30;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Category";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 150;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Code";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Visible = false;
            // 
            // dgvMajorFees
            // 
            this.dgvMajorFees.AllowUserToAddRows = false;
            this.dgvMajorFees.AllowUserToDeleteRows = false;
            this.dgvMajorFees.AllowUserToResizeRows = false;
            this.dgvMajorFees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMajorFees.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.Subsidiaries,
            this.Term,
            this.Means,
            this.Unit,
            this.Scope,
            this.Category,
            this.Cumulative,
            this.Area,
            this.Structure});
            this.dgvMajorFees.Location = new System.Drawing.Point(9, 161);
            this.dgvMajorFees.Name = "dgvMajorFees";
            this.dgvMajorFees.RowHeadersVisible = false;
            this.dgvMajorFees.Size = new System.Drawing.Size(749, 151);
            this.dgvMajorFees.TabIndex = 12;
            this.dgvMajorFees.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMajorFees_CellClick);
            this.dgvMajorFees.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMajorFees_CellEndEdit);
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Code";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 50;
            // 
            // Subsidiaries
            // 
            this.Subsidiaries.HeaderText = "Subsidiaries";
            this.Subsidiaries.Name = "Subsidiaries";
            this.Subsidiaries.Width = 200;
            // 
            // Term
            // 
            this.Term.HeaderText = "Term";
            this.Term.Name = "Term";
            // 
            // Means
            // 
            this.Means.HeaderText = "Means";
            this.Means.Name = "Means";
            // 
            // Unit
            // 
            this.Unit.HeaderText = "Unit";
            this.Unit.Name = "Unit";
            // 
            // Scope
            // 
            this.Scope.HeaderText = "Scope";
            this.Scope.Name = "Scope";
            // 
            // Category
            // 
            this.Category.HeaderText = "Category";
            this.Category.Name = "Category";
            // 
            // Cumulative
            // 
            this.Cumulative.HeaderText = "Cumulative";
            this.Cumulative.Name = "Cumulative";
            // 
            // Area
            // 
            this.Area.HeaderText = "Need Area";
            this.Area.Name = "Area";
            // 
            // Structure
            // 
            this.Structure.HeaderText = "Structure";
            this.Structure.Name = "Structure";
            // 
            // dgvStruc
            // 
            this.dgvStruc.AllowUserToAddRows = false;
            this.dgvStruc.AllowUserToDeleteRows = false;
            this.dgvStruc.AllowUserToResizeColumns = false;
            this.dgvStruc.AllowUserToResizeRows = false;
            this.dgvStruc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStruc.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn1,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.dgvStruc.Location = new System.Drawing.Point(391, 17);
            this.dgvStruc.Name = "dgvStruc";
            this.dgvStruc.RowHeadersVisible = false;
            this.dgvStruc.Size = new System.Drawing.Size(127, 138);
            this.dgvStruc.TabIndex = 10;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.HeaderText = " ";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Width = 30;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Structure";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 90;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Code";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Visible = false;
            // 
            // dgvScope
            // 
            this.dgvScope.AllowUserToAddRows = false;
            this.dgvScope.AllowUserToDeleteRows = false;
            this.dgvScope.AllowUserToResizeColumns = false;
            this.dgvScope.AllowUserToResizeRows = false;
            this.dgvScope.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvScope.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Select,
            this.Desc,
            this.Code});
            this.dgvScope.Location = new System.Drawing.Point(9, 17);
            this.dgvScope.Name = "dgvScope";
            this.dgvScope.RowHeadersVisible = false;
            this.dgvScope.Size = new System.Drawing.Size(185, 138);
            this.dgvScope.TabIndex = 8;
            // 
            // Select
            // 
            this.Select.HeaderText = " ";
            this.Select.Name = "Select";
            this.Select.Width = 30;
            // 
            // Desc
            // 
            this.Desc.HeaderText = "Scope of Work";
            this.Desc.Name = "Desc";
            this.Desc.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Desc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Desc.Width = 150;
            // 
            // Code
            // 
            this.Code.HeaderText = "Code";
            this.Code.Name = "Code";
            this.Code.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Code.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Code.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvSchedule);
            this.groupBox3.Location = new System.Drawing.Point(16, 414);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(629, 161);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Schedule";
            // 
            // dgvSchedule
            // 
            this.dgvSchedule.AllowUserToAddRows = false;
            this.dgvSchedule.AllowUserToDeleteRows = false;
            this.dgvSchedule.AllowUserToResizeColumns = false;
            this.dgvSchedule.AllowUserToResizeRows = false;
            this.dgvSchedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSchedule.Location = new System.Drawing.Point(9, 25);
            this.dgvSchedule.Name = "dgvSchedule";
            this.dgvSchedule.RowHeadersVisible = false;
            this.dgvSchedule.Size = new System.Drawing.Size(612, 126);
            this.dgvSchedule.TabIndex = 13;
            this.dgvSchedule.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSchedule_CellEndEdit);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(666, 553);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(97, 33);
            this.btnExit.TabIndex = 18;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(666, 519);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(97, 33);
            this.btnPrint.TabIndex = 17;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(666, 485);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(97, 33);
            this.btnDelete.TabIndex = 16;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(666, 451);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(97, 33);
            this.btnEdit.TabIndex = 15;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(666, 417);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(97, 33);
            this.btnAdd.TabIndex = 14;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // frmScheduleFees
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.ClientSize = new System.Drawing.Size(793, 592);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnDelete);
            this.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmScheduleFees";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Schedule of Fees";
            this.Load += new System.EventHandler(this.frmScheduleFees_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMajorFees)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStruc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvScope)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedule)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvStruc;
        private System.Windows.Forms.DataGridView dgvMajorFees;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView dgvScope;
        private System.Windows.Forms.DataGridView dgvSchedule;
        private System.Windows.Forms.TextBox txtSubAcctDesc;
        private System.Windows.Forms.TextBox txtRevenueAcct;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Select;
        private System.Windows.Forms.DataGridViewTextBoxColumn Desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.TextBox txtSubAcctDescNew;
        private System.Windows.Forms.TextBox txtRevenueAcctNew;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Subsidiaries;
        private System.Windows.Forms.DataGridViewTextBoxColumn Term;
        private System.Windows.Forms.DataGridViewTextBoxColumn Means;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Scope;
        private System.Windows.Forms.DataGridViewTextBoxColumn Category;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cumulative;
        private System.Windows.Forms.DataGridViewTextBoxColumn Area;
        private System.Windows.Forms.DataGridViewTextBoxColumn Structure;
        private System.Windows.Forms.DataGridView dgvCategory;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private MultiColumnComboBoxDemo.MultiColumnComboBox cmbRevenueAcct;
        private MultiColumnComboBoxDemo.MultiColumnComboBox cmbSubAcctDesc;
    }
}