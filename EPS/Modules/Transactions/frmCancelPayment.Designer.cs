namespace Modules.Transactions
{
    partial class frmCancelPayment
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtARN = new System.Windows.Forms.TextBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtORNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBillNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMI = new System.Windows.Forms.TextBox();
            this.dgView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.grpDatePosted = new System.Windows.Forms.GroupBox();
            this.chkInclude = new System.Windows.Forms.CheckBox();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.grpFields = new System.Windows.Forms.GroupBox();
            this.lblTotalRec = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).BeginInit();
            this.grpDatePosted.SuspendLayout();
            this.grpFields.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "ARN";
            // 
            // txtARN
            // 
            this.txtARN.Location = new System.Drawing.Point(71, 20);
            this.txtARN.Name = "txtARN";
            this.txtARN.Size = new System.Drawing.Size(197, 22);
            this.txtARN.TabIndex = 0;
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(71, 48);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(197, 22);
            this.txtLastName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "Bill No.";
            // 
            // txtORNo
            // 
            this.txtORNo.Location = new System.Drawing.Point(71, 132);
            this.txtORNo.Name = "txtORNo";
            this.txtORNo.Size = new System.Drawing.Size(197, 22);
            this.txtORNo.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 14);
            this.label3.TabIndex = 6;
            this.label3.Text = "O.R. No.";
            // 
            // txtBillNo
            // 
            this.txtBillNo.Location = new System.Drawing.Point(71, 104);
            this.txtBillNo.Name = "txtBillNo";
            this.txtBillNo.Size = new System.Drawing.Size(197, 22);
            this.txtBillNo.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 14);
            this.label4.TabIndex = 4;
            this.label4.Text = "Last name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 14);
            this.label5.TabIndex = 9;
            this.label5.Text = "First name";
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(71, 76);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(133, 22);
            this.txtFirstName.TabIndex = 2;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(387, 352);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 25);
            this.btnConfirm.TabIndex = 10;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(468, 352);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 25);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(210, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 14);
            this.label6.TabIndex = 13;
            this.label6.Text = "M.I.";
            // 
            // txtMI
            // 
            this.txtMI.Location = new System.Drawing.Point(241, 76);
            this.txtMI.Name = "txtMI";
            this.txtMI.Size = new System.Drawing.Size(27, 22);
            this.txtMI.TabIndex = 3;
            // 
            // dgView
            // 
            this.dgView.AllowUserToAddRows = false;
            this.dgView.AllowUserToDeleteRows = false;
            this.dgView.AllowUserToResizeRows = false;
            this.dgView.BackgroundColor = System.Drawing.Color.White;
            this.dgView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column8,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7});
            this.dgView.Location = new System.Drawing.Point(12, 184);
            this.dgView.MultiSelect = false;
            this.dgView.Name = "dgView";
            this.dgView.ReadOnly = true;
            this.dgView.RowHeadersVisible = false;
            this.dgView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgView.Size = new System.Drawing.Size(531, 162);
            this.dgView.TabIndex = 14;
            this.dgView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgView_CellClick);
            this.dgView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgView_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "ARN";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Acct_Code";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Visible = false;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Last name";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 120;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "First name";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "M.I.";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 30;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Bill No.";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "O.R. No.";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Posted Date";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(301, 130);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(109, 25);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(416, 130);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(109, 25);
            this.btnClear.TabIndex = 9;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "MM/dd/yyyy";
            this.dtpFrom.Enabled = false;
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(69, 45);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(171, 22);
            this.dtpFrom.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(7, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 14);
            this.label7.TabIndex = 17;
            this.label7.Text = "Date From";
            // 
            // grpDatePosted
            // 
            this.grpDatePosted.Controls.Add(this.chkInclude);
            this.grpDatePosted.Controls.Add(this.dtpTo);
            this.grpDatePosted.Controls.Add(this.label8);
            this.grpDatePosted.Controls.Add(this.dtpFrom);
            this.grpDatePosted.Controls.Add(this.label7);
            this.grpDatePosted.ForeColor = System.Drawing.Color.Blue;
            this.grpDatePosted.Location = new System.Drawing.Point(274, 14);
            this.grpDatePosted.Name = "grpDatePosted";
            this.grpDatePosted.Size = new System.Drawing.Size(246, 110);
            this.grpDatePosted.TabIndex = 18;
            this.grpDatePosted.TabStop = false;
            this.grpDatePosted.Text = "Date Posted";
            // 
            // chkInclude
            // 
            this.chkInclude.AutoSize = true;
            this.chkInclude.Location = new System.Drawing.Point(6, 20);
            this.chkInclude.Name = "chkInclude";
            this.chkInclude.Size = new System.Drawing.Size(96, 18);
            this.chkInclude.TabIndex = 20;
            this.chkInclude.Text = "Include Date";
            this.chkInclude.UseVisualStyleBackColor = true;
            this.chkInclude.CheckedChanged += new System.EventHandler(this.chkInclude_CheckedChanged);
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "MM/dd/yyyy";
            this.dtpTo.Enabled = false;
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(69, 73);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(171, 22);
            this.dtpTo.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(7, 76);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 14);
            this.label8.TabIndex = 19;
            this.label8.Text = "Date To";
            // 
            // grpFields
            // 
            this.grpFields.Controls.Add(this.txtARN);
            this.grpFields.Controls.Add(this.btnClear);
            this.grpFields.Controls.Add(this.btnSearch);
            this.grpFields.Controls.Add(this.grpDatePosted);
            this.grpFields.Controls.Add(this.label1);
            this.grpFields.Controls.Add(this.label2);
            this.grpFields.Controls.Add(this.txtLastName);
            this.grpFields.Controls.Add(this.label4);
            this.grpFields.Controls.Add(this.label6);
            this.grpFields.Controls.Add(this.txtBillNo);
            this.grpFields.Controls.Add(this.txtMI);
            this.grpFields.Controls.Add(this.label3);
            this.grpFields.Controls.Add(this.txtORNo);
            this.grpFields.Controls.Add(this.txtFirstName);
            this.grpFields.Controls.Add(this.label5);
            this.grpFields.Location = new System.Drawing.Point(12, 13);
            this.grpFields.Name = "grpFields";
            this.grpFields.Size = new System.Drawing.Size(531, 165);
            this.grpFields.TabIndex = 19;
            this.grpFields.TabStop = false;
            // 
            // lblTotalRec
            // 
            this.lblTotalRec.AutoSize = true;
            this.lblTotalRec.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalRec.Location = new System.Drawing.Point(9, 349);
            this.lblTotalRec.Name = "lblTotalRec";
            this.lblTotalRec.Size = new System.Drawing.Size(90, 15);
            this.lblTotalRec.TabIndex = 20;
            this.lblTotalRec.TabStop = true;
            this.lblTotalRec.Text = "Total Record(s)";
            // 
            // frmCancelPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(553, 382);
            this.ControlBox = false;
            this.Controls.Add(this.lblTotalRec);
            this.Controls.Add(this.grpFields);
            this.Controls.Add(this.dgView);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnConfirm);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximumSize = new System.Drawing.Size(569, 421);
            this.MinimumSize = new System.Drawing.Size(569, 421);
            this.Name = "frmCancelPayment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cancel Payment";
            this.Load += new System.EventHandler(this.frmCancelPayment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).EndInit();
            this.grpDatePosted.ResumeLayout(false);
            this.grpDatePosted.PerformLayout();
            this.grpFields.ResumeLayout(false);
            this.grpFields.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtARN;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtORNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBillNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtMI;
        private System.Windows.Forms.DataGridView dgView;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox grpDatePosted;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox grpFields;
        private System.Windows.Forms.CheckBox chkInclude;
        private System.Windows.Forms.LinkLabel lblTotalRec;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
    }
}

