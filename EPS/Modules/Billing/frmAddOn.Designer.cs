namespace Modules.Billing
{
    partial class frmAddOn
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddOn));
            this.grpAddOn = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.bntOk = new System.Windows.Forms.Button();
            this.grpAddonAmt = new System.Windows.Forms.GroupBox();
            this.txtAddOnAmt = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.txtAddOnFeesDesc = new System.Windows.Forms.TextBox();
            this.btnAddOnAmt = new System.Windows.Forms.Button();
            this.txtAddOnTotAmTDue = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.dgvAddOnFees = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpAddOn.SuspendLayout();
            this.grpAddonAmt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAddOnFees)).BeginInit();
            this.SuspendLayout();
            // 
            // grpAddOn
            // 
            this.grpAddOn.Controls.Add(this.btnCancel);
            this.grpAddOn.Controls.Add(this.bntOk);
            this.grpAddOn.Controls.Add(this.grpAddonAmt);
            this.grpAddOn.Controls.Add(this.txtAddOnTotAmTDue);
            this.grpAddOn.Controls.Add(this.label14);
            this.grpAddOn.Controls.Add(this.dgvAddOnFees);
            this.grpAddOn.Location = new System.Drawing.Point(3, 3);
            this.grpAddOn.Name = "grpAddOn";
            this.grpAddOn.Size = new System.Drawing.Size(750, 158);
            this.grpAddOn.TabIndex = 122;
            this.grpAddOn.TabStop = false;
            this.grpAddOn.Text = "Add-on";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(658, 131);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(86, 23);
            this.btnCancel.TabIndex = 125;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // bntOk
            // 
            this.bntOk.Location = new System.Drawing.Point(567, 131);
            this.bntOk.Name = "bntOk";
            this.bntOk.Size = new System.Drawing.Size(86, 23);
            this.bntOk.TabIndex = 126;
            this.bntOk.Text = "Ok";
            this.bntOk.UseVisualStyleBackColor = true;
            this.bntOk.Click += new System.EventHandler(this.bntOk_Click);
            // 
            // grpAddonAmt
            // 
            this.grpAddonAmt.Controls.Add(this.txtAddOnAmt);
            this.grpAddonAmt.Controls.Add(this.label16);
            this.grpAddonAmt.Controls.Add(this.label17);
            this.grpAddonAmt.Controls.Add(this.txtAddOnFeesDesc);
            this.grpAddonAmt.Controls.Add(this.btnAddOnAmt);
            this.grpAddonAmt.Location = new System.Drawing.Point(519, 19);
            this.grpAddonAmt.Name = "grpAddonAmt";
            this.grpAddonAmt.Size = new System.Drawing.Size(225, 78);
            this.grpAddonAmt.TabIndex = 124;
            this.grpAddonAmt.TabStop = false;
            this.grpAddonAmt.Text = "Amount";
            // 
            // txtAddOnAmt
            // 
            this.txtAddOnAmt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAddOnAmt.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddOnAmt.Location = new System.Drawing.Point(67, 45);
            this.txtAddOnAmt.Name = "txtAddOnAmt";
            this.txtAddOnAmt.ReadOnly = true;
            this.txtAddOnAmt.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtAddOnAmt.Size = new System.Drawing.Size(152, 23);
            this.txtAddOnAmt.TabIndex = 123;
            this.txtAddOnAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAddOnAmt.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtAddOnAmt_KeyUp);
            this.txtAddOnAmt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAddOnAmt_KeyDown);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(17, 48);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(47, 13);
            this.label16.TabIndex = 122;
            this.label16.Text = "Amount:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(8, 22);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(57, 13);
            this.label17.TabIndex = 121;
            this.label17.Text = "Fees Desc:";
            this.label17.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtAddOnFeesDesc
            // 
            this.txtAddOnFeesDesc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAddOnFeesDesc.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddOnFeesDesc.Location = new System.Drawing.Point(67, 19);
            this.txtAddOnFeesDesc.Name = "txtAddOnFeesDesc";
            this.txtAddOnFeesDesc.ReadOnly = true;
            this.txtAddOnFeesDesc.Size = new System.Drawing.Size(152, 23);
            this.txtAddOnFeesDesc.TabIndex = 121;
            // 
            // btnAddOnAmt
            // 
            this.btnAddOnAmt.Location = new System.Drawing.Point(148, 42);
            this.btnAddOnAmt.Name = "btnAddOnAmt";
            this.btnAddOnAmt.Size = new System.Drawing.Size(70, 23);
            this.btnAddOnAmt.TabIndex = 118;
            this.btnAddOnAmt.Text = "Override";
            this.btnAddOnAmt.UseVisualStyleBackColor = true;
            this.btnAddOnAmt.Visible = false;
            this.btnAddOnAmt.Click += new System.EventHandler(this.btnAddOnAmt_Click);
            // 
            // txtAddOnTotAmTDue
            // 
            this.txtAddOnTotAmTDue.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAddOnTotAmTDue.Enabled = false;
            this.txtAddOnTotAmTDue.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddOnTotAmTDue.Location = new System.Drawing.Point(378, 128);
            this.txtAddOnTotAmTDue.Name = "txtAddOnTotAmTDue";
            this.txtAddOnTotAmTDue.ReadOnly = true;
            this.txtAddOnTotAmTDue.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtAddOnTotAmTDue.Size = new System.Drawing.Size(129, 23);
            this.txtAddOnTotAmTDue.TabIndex = 122;
            this.txtAddOnTotAmTDue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(288, 131);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(84, 15);
            this.label14.TabIndex = 121;
            this.label14.Text = "Total Amount ";
            this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // dgvAddOnFees
            // 
            this.dgvAddOnFees.AllowUserToAddRows = false;
            this.dgvAddOnFees.AllowUserToResizeColumns = false;
            this.dgvAddOnFees.AllowUserToResizeRows = false;
            this.dgvAddOnFees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAddOnFees.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn2,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11});
            this.dgvAddOnFees.Location = new System.Drawing.Point(5, 19);
            this.dgvAddOnFees.MultiSelect = false;
            this.dgvAddOnFees.Name = "dgvAddOnFees";
            this.dgvAddOnFees.RowHeadersVisible = false;
            this.dgvAddOnFees.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAddOnFees.Size = new System.Drawing.Size(502, 103);
            this.dgvAddOnFees.TabIndex = 120;
            // 
            // dataGridViewCheckBoxColumn2
            // 
            this.dataGridViewCheckBoxColumn2.HeaderText = " ";
            this.dataGridViewCheckBoxColumn2.Name = "dataGridViewCheckBoxColumn2";
            this.dataGridViewCheckBoxColumn2.Width = 30;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Fees Description";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 200;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Code";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Visible = false;
            this.dataGridViewTextBoxColumn2.Width = 70;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Means";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Visible = false;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Unit";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Visible = false;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Area";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Visible = false;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Cumulative";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Visible = false;
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            this.dataGridViewTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTextBoxColumn7.HeaderText = "Unit Value";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            this.dataGridViewTextBoxColumn8.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn8.HeaderText = "Amount";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "Category";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.Visible = false;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.HeaderText = "Scope";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.Visible = false;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.HeaderText = "OrigAmt";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.Visible = false;
            // 
            // frmAddOn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(758, 166);
            this.Controls.Add(this.grpAddOn);
            this.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddOn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add-On";
            this.Load += new System.EventHandler(this.frmAddOn_Load);
            this.grpAddOn.ResumeLayout(false);
            this.grpAddOn.PerformLayout();
            this.grpAddonAmt.ResumeLayout(false);
            this.grpAddonAmt.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAddOnFees)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpAddOn;
        public System.Windows.Forms.GroupBox grpAddonAmt;
        public System.Windows.Forms.TextBox txtAddOnAmt;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        public System.Windows.Forms.TextBox txtAddOnFeesDesc;
        public System.Windows.Forms.Button btnAddOnAmt;
        public System.Windows.Forms.TextBox txtAddOnTotAmTDue;
        private System.Windows.Forms.Label label14;
        public System.Windows.Forms.DataGridView dgvAddOnFees;
        public System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.Button bntOk;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
    }
}