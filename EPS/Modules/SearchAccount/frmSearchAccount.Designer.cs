namespace Modules.SearchAccount
{
    partial class frmSearchAccount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSearchAccount));
            this.cmbEngrType = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.bntList = new System.Windows.Forms.Button();
            this.txtMI = new System.Windows.Forms.TextBox();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.txtAcctNo = new System.Windows.Forms.TextBox();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.lblEngrType = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblAcctNo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbEngrType
            // 
            this.cmbEngrType.FormattingEnabled = true;
            this.cmbEngrType.Location = new System.Drawing.Point(255, 10);
            this.cmbEngrType.Name = "cmbEngrType";
            this.cmbEngrType.Size = new System.Drawing.Size(163, 23);
            this.cmbEngrType.TabIndex = 17;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(337, 296);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 25);
            this.btnCancel.TabIndex = 25;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(336, 97);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(90, 25);
            this.btnClear.TabIndex = 22;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(241, 296);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(90, 25);
            this.btnOk.TabIndex = 24;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // bntList
            // 
            this.bntList.Location = new System.Drawing.Point(241, 97);
            this.bntList.Name = "bntList";
            this.bntList.Size = new System.Drawing.Size(90, 25);
            this.bntList.TabIndex = 21;
            this.bntList.Text = "List";
            this.bntList.UseVisualStyleBackColor = true;
            this.bntList.Click += new System.EventHandler(this.bntList_Click);
            // 
            // txtMI
            // 
            this.txtMI.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMI.Location = new System.Drawing.Point(386, 66);
            this.txtMI.Name = "txtMI";
            this.txtMI.Size = new System.Drawing.Size(33, 23);
            this.txtMI.TabIndex = 20;
            // 
            // txtFirstName
            // 
            this.txtFirstName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFirstName.Location = new System.Drawing.Point(93, 66);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(259, 23);
            this.txtFirstName.TabIndex = 19;
            // 
            // txtLastName
            // 
            this.txtLastName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLastName.Location = new System.Drawing.Point(93, 39);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(326, 23);
            this.txtLastName.TabIndex = 18;
            // 
            // txtAcctNo
            // 
            this.txtAcctNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAcctNo.Location = new System.Drawing.Point(93, 12);
            this.txtAcctNo.Name = "txtAcctNo";
            this.txtAcctNo.Size = new System.Drawing.Size(88, 23);
            this.txtAcctNo.TabIndex = 16;
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToResizeRows = false;
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Location = new System.Drawing.Point(9, 127);
            this.dgvList.Name = "dgvList";
            this.dgvList.ReadOnly = true;
            this.dgvList.Size = new System.Drawing.Size(417, 163);
            this.dgvList.TabIndex = 23;
            this.dgvList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellClick);
            this.dgvList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellContentClick);
            // 
            // lblEngrType
            // 
            this.lblEngrType.AutoSize = true;
            this.lblEngrType.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEngrType.Location = new System.Drawing.Point(185, 15);
            this.lblEngrType.Name = "lblEngrType";
            this.lblEngrType.Size = new System.Drawing.Size(62, 15);
            this.lblEngrType.TabIndex = 11;
            this.lblEngrType.Text = "Engr. Type";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(358, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 15);
            this.label4.TabIndex = 12;
            this.label4.Text = "M.I.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(17, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 13;
            this.label3.Text = "First Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(17, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 15);
            this.label2.TabIndex = 14;
            this.label2.Text = "Last Name";
            // 
            // lblAcctNo
            // 
            this.lblAcctNo.AutoSize = true;
            this.lblAcctNo.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAcctNo.Location = new System.Drawing.Point(17, 16);
            this.lblAcctNo.Name = "lblAcctNo";
            this.lblAcctNo.Size = new System.Drawing.Size(73, 15);
            this.lblAcctNo.TabIndex = 15;
            this.lblAcctNo.Text = "Account No.";
            // 
            // frmSearchAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.ClientSize = new System.Drawing.Size(441, 331);
            this.Controls.Add(this.cmbEngrType);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.bntList);
            this.Controls.Add(this.txtMI);
            this.Controls.Add(this.txtFirstName);
            this.Controls.Add(this.txtLastName);
            this.Controls.Add(this.txtAcctNo);
            this.Controls.Add(this.dgvList);
            this.Controls.Add(this.lblEngrType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblAcctNo);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSearchAccount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Search Account";
            this.Load += new System.EventHandler(this.frmSearchAccount_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbEngrType;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button bntList;
        private System.Windows.Forms.TextBox txtMI;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.TextBox txtAcctNo;
        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.Label lblEngrType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblAcctNo;
    }
}