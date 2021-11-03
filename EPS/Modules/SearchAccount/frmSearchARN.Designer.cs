namespace Modules.SearchAccount
{
    partial class frmSearchARN
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSearchARN));
            this.txtProjDesc = new System.Windows.Forms.TextBox();
            this.lblBrgy = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLotMI = new System.Windows.Forms.TextBox();
            this.txtLotFirstName = new System.Windows.Forms.TextBox();
            this.txtLotLastName = new System.Windows.Forms.TextBox();
            this.lblLotMI = new System.Windows.Forms.Label();
            this.lblLotFN = new System.Windows.Forms.Label();
            this.lblLotLN = new System.Windows.Forms.Label();
            this.grpBox1 = new System.Windows.Forms.GroupBox();
            this.grpBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtOwnMI = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtOwnLastName = new System.Windows.Forms.TextBox();
            this.txtOwnFirstName = new System.Windows.Forms.TextBox();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.bntList = new System.Windows.Forms.Button();
            this.cmbBrgy = new MultiColumnComboBoxDemo.MultiColumnComboBox();
            this.arn1 = new Modules.ARN.AN();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtProjStreet = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtProjVill = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtProjBlkNo = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtProjHseNo = new System.Windows.Forms.TextBox();
            this.txtProjLotNo = new System.Windows.Forms.TextBox();
            this.grpBox1.SuspendLayout();
            this.grpBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtProjDesc
            // 
            this.txtProjDesc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtProjDesc.Location = new System.Drawing.Point(103, 47);
            this.txtProjDesc.Name = "txtProjDesc";
            this.txtProjDesc.Size = new System.Drawing.Size(286, 23);
            this.txtProjDesc.TabIndex = 2;
            // 
            // lblBrgy
            // 
            this.lblBrgy.AutoSize = true;
            this.lblBrgy.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBrgy.Location = new System.Drawing.Point(395, 51);
            this.lblBrgy.Name = "lblBrgy";
            this.lblBrgy.Size = new System.Drawing.Size(60, 15);
            this.lblBrgy.TabIndex = 0;
            this.lblBrgy.Text = "Barangay:";
            this.lblBrgy.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(24, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Description:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(65, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "AN:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtLotMI
            // 
            this.txtLotMI.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLotMI.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLotMI.Location = new System.Drawing.Point(273, 48);
            this.txtLotMI.Name = "txtLotMI";
            this.txtLotMI.Size = new System.Drawing.Size(27, 23);
            this.txtLotMI.TabIndex = 6;
            // 
            // txtLotFirstName
            // 
            this.txtLotFirstName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLotFirstName.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLotFirstName.Location = new System.Drawing.Point(76, 48);
            this.txtLotFirstName.Name = "txtLotFirstName";
            this.txtLotFirstName.Size = new System.Drawing.Size(164, 23);
            this.txtLotFirstName.TabIndex = 5;
            // 
            // txtLotLastName
            // 
            this.txtLotLastName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLotLastName.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLotLastName.Location = new System.Drawing.Point(76, 22);
            this.txtLotLastName.Name = "txtLotLastName";
            this.txtLotLastName.Size = new System.Drawing.Size(224, 23);
            this.txtLotLastName.TabIndex = 4;
            // 
            // lblLotMI
            // 
            this.lblLotMI.AutoSize = true;
            this.lblLotMI.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLotMI.Location = new System.Drawing.Point(245, 51);
            this.lblLotMI.Name = "lblLotMI";
            this.lblLotMI.Size = new System.Drawing.Size(27, 15);
            this.lblLotMI.TabIndex = 0;
            this.lblLotMI.Text = "M.I.";
            // 
            // lblLotFN
            // 
            this.lblLotFN.AutoSize = true;
            this.lblLotFN.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLotFN.Location = new System.Drawing.Point(7, 51);
            this.lblLotFN.Name = "lblLotFN";
            this.lblLotFN.Size = new System.Drawing.Size(67, 15);
            this.lblLotFN.TabIndex = 0;
            this.lblLotFN.Text = "First Name";
            // 
            // lblLotLN
            // 
            this.lblLotLN.AutoSize = true;
            this.lblLotLN.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLotLN.Location = new System.Drawing.Point(7, 25);
            this.lblLotLN.Name = "lblLotLN";
            this.lblLotLN.Size = new System.Drawing.Size(65, 15);
            this.lblLotLN.TabIndex = 0;
            this.lblLotLN.Text = "Last Name";
            // 
            // grpBox1
            // 
            this.grpBox1.Controls.Add(this.lblLotLN);
            this.grpBox1.Controls.Add(this.lblLotFN);
            this.grpBox1.Controls.Add(this.txtLotMI);
            this.grpBox1.Controls.Add(this.lblLotMI);
            this.grpBox1.Controls.Add(this.txtLotLastName);
            this.grpBox1.Controls.Add(this.txtLotFirstName);
            this.grpBox1.Font = new System.Drawing.Font("Calibri", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBox1.Location = new System.Drawing.Point(12, 77);
            this.grpBox1.Name = "grpBox1";
            this.grpBox1.Size = new System.Drawing.Size(312, 80);
            this.grpBox1.TabIndex = 0;
            this.grpBox1.TabStop = false;
            this.grpBox1.Text = "Lot Owner Information";
            // 
            // grpBox2
            // 
            this.grpBox2.Controls.Add(this.label6);
            this.grpBox2.Controls.Add(this.label7);
            this.grpBox2.Controls.Add(this.txtOwnMI);
            this.grpBox2.Controls.Add(this.label8);
            this.grpBox2.Controls.Add(this.txtOwnLastName);
            this.grpBox2.Controls.Add(this.txtOwnFirstName);
            this.grpBox2.Font = new System.Drawing.Font("Calibri", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBox2.Location = new System.Drawing.Point(330, 77);
            this.grpBox2.Name = "grpBox2";
            this.grpBox2.Size = new System.Drawing.Size(312, 80);
            this.grpBox2.TabIndex = 0;
            this.grpBox2.TabStop = false;
            this.grpBox2.Text = "Project Owner Information";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(7, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 15);
            this.label6.TabIndex = 0;
            this.label6.Text = "Last Name";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(7, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 15);
            this.label7.TabIndex = 0;
            this.label7.Text = "First Name";
            // 
            // txtOwnMI
            // 
            this.txtOwnMI.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtOwnMI.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOwnMI.Location = new System.Drawing.Point(276, 48);
            this.txtOwnMI.Name = "txtOwnMI";
            this.txtOwnMI.Size = new System.Drawing.Size(27, 23);
            this.txtOwnMI.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(248, 51);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(27, 15);
            this.label8.TabIndex = 0;
            this.label8.Text = "M.I.";
            // 
            // txtOwnLastName
            // 
            this.txtOwnLastName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtOwnLastName.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOwnLastName.Location = new System.Drawing.Point(79, 22);
            this.txtOwnLastName.Name = "txtOwnLastName";
            this.txtOwnLastName.Size = new System.Drawing.Size(224, 23);
            this.txtOwnLastName.TabIndex = 7;
            // 
            // txtOwnFirstName
            // 
            this.txtOwnFirstName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtOwnFirstName.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOwnFirstName.Location = new System.Drawing.Point(79, 48);
            this.txtOwnFirstName.Name = "txtOwnFirstName";
            this.txtOwnFirstName.Size = new System.Drawing.Size(164, 23);
            this.txtOwnFirstName.TabIndex = 8;
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToDeleteRows = false;
            this.dgvList.AllowUserToResizeRows = false;
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Location = new System.Drawing.Point(12, 317);
            this.dgvList.MultiSelect = false;
            this.dgvList.Name = "dgvList";
            this.dgvList.ReadOnly = true;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvList.Size = new System.Drawing.Size(630, 183);
            this.dgvList.TabIndex = 12;
            this.dgvList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellClick);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(452, 506);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(90, 25);
            this.btnOk.TabIndex = 13;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(548, 506);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 25);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(548, 286);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(90, 25);
            this.btnClear.TabIndex = 11;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // bntList
            // 
            this.bntList.Location = new System.Drawing.Point(452, 286);
            this.bntList.Name = "bntList";
            this.bntList.Size = new System.Drawing.Size(90, 25);
            this.bntList.TabIndex = 10;
            this.bntList.Text = "List";
            this.bntList.UseVisualStyleBackColor = true;
            this.bntList.Click += new System.EventHandler(this.bntList_Click);
            // 
            // cmbBrgy
            // 
            this.cmbBrgy.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbBrgy.FormattingEnabled = true;
            this.cmbBrgy.Location = new System.Drawing.Point(463, 47);
            this.cmbBrgy.Name = "cmbBrgy";
            this.cmbBrgy.Size = new System.Drawing.Size(167, 24);
            this.cmbBrgy.TabIndex = 3;
            // 
            // arn1
            // 
            this.arn1.GetCode = "";
            this.arn1.GetSeries = "";
            this.arn1.GetTaxYear = "";
            this.arn1.Location = new System.Drawing.Point(103, 12);
            this.arn1.Name = "arn1";
            this.arn1.Size = new System.Drawing.Size(183, 31);
            this.arn1.TabIndex = 15;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtProjStreet);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtProjVill);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txtProjBlkNo);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.txtProjHseNo);
            this.groupBox2.Controls.Add(this.txtProjLotNo);
            this.groupBox2.Font = new System.Drawing.Font("Calibri", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 163);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(630, 117);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Project Location";
            // 
            // txtProjStreet
            // 
            this.txtProjStreet.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtProjStreet.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProjStreet.Location = new System.Drawing.Point(369, 22);
            this.txtProjStreet.Name = "txtProjStreet";
            this.txtProjStreet.Size = new System.Drawing.Size(224, 23);
            this.txtProjStreet.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(325, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "Street";
            // 
            // txtProjVill
            // 
            this.txtProjVill.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtProjVill.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProjVill.Location = new System.Drawing.Point(369, 49);
            this.txtProjVill.Name = "txtProjVill";
            this.txtProjVill.Size = new System.Drawing.Size(224, 23);
            this.txtProjVill.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(326, 52);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 15);
            this.label9.TabIndex = 7;
            this.label9.Text = "Subd.";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(21, 25);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 15);
            this.label10.TabIndex = 0;
            this.label10.Text = "Hse No.";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(23, 52);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 15);
            this.label11.TabIndex = 0;
            this.label11.Text = "Lot No.";
            // 
            // txtProjBlkNo
            // 
            this.txtProjBlkNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtProjBlkNo.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProjBlkNo.Location = new System.Drawing.Point(76, 77);
            this.txtProjBlkNo.Name = "txtProjBlkNo";
            this.txtProjBlkNo.Size = new System.Drawing.Size(224, 23);
            this.txtProjBlkNo.TabIndex = 6;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(27, 80);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(45, 15);
            this.label12.TabIndex = 0;
            this.label12.Text = "Blk No.";
            // 
            // txtProjHseNo
            // 
            this.txtProjHseNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtProjHseNo.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProjHseNo.Location = new System.Drawing.Point(76, 22);
            this.txtProjHseNo.Name = "txtProjHseNo";
            this.txtProjHseNo.Size = new System.Drawing.Size(224, 23);
            this.txtProjHseNo.TabIndex = 4;
            // 
            // txtProjLotNo
            // 
            this.txtProjLotNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtProjLotNo.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProjLotNo.Location = new System.Drawing.Point(76, 49);
            this.txtProjLotNo.Name = "txtProjLotNo";
            this.txtProjLotNo.Size = new System.Drawing.Size(224, 23);
            this.txtProjLotNo.TabIndex = 5;
            // 
            // frmSearchARN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.ClientSize = new System.Drawing.Size(654, 541);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.arn1);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.bntList);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.dgvList);
            this.Controls.Add(this.grpBox2);
            this.Controls.Add(this.grpBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbBrgy);
            this.Controls.Add(this.lblBrgy);
            this.Controls.Add(this.txtProjDesc);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSearchARN";
            this.Text = "Search ARN";
            this.Load += new System.EventHandler(this.frmSearchARN_Load);
            this.grpBox1.ResumeLayout(false);
            this.grpBox1.PerformLayout();
            this.grpBox2.ResumeLayout(false);
            this.grpBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtProjDesc;
        public MultiColumnComboBoxDemo.MultiColumnComboBox cmbBrgy;
        private System.Windows.Forms.Label lblBrgy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLotMI;
        private System.Windows.Forms.TextBox txtLotFirstName;
        private System.Windows.Forms.TextBox txtLotLastName;
        private System.Windows.Forms.Label lblLotMI;
        private System.Windows.Forms.Label lblLotFN;
        private System.Windows.Forms.Label lblLotLN;
        private System.Windows.Forms.GroupBox grpBox1;
        private System.Windows.Forms.GroupBox grpBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtOwnMI;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtOwnLastName;
        private System.Windows.Forms.TextBox txtOwnFirstName;
        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button bntList;
        private ARN.AN arn1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtProjStreet;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtProjVill;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtProjBlkNo;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtProjHseNo;
        private System.Windows.Forms.TextBox txtProjLotNo;
    }
}