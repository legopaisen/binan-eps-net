namespace Modules.Transactions
{
    partial class frmBldgDates
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBldgDates));
            this.txtPIN = new System.Windows.Forms.TextBox();
            this.txtBldgNo = new System.Windows.Forms.TextBox();
            this.txtBldgName = new System.Windows.Forms.TextBox();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.txtArea = new System.Windows.Forms.TextBox();
            this.txtCost = new System.Windows.Forms.TextBox();
            this.txtStoreys = new System.Windows.Forms.TextBox();
            this.txtUnits = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txtAssVal = new System.Windows.Forms.TextBox();
            this.cmbMaterials = new MultiColumnComboBoxDemo.MultiColumnComboBox();
            this.btnBldgUnits = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPIN
            // 
            this.txtPIN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPIN.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPIN.Location = new System.Drawing.Point(386, 14);
            this.txtPIN.Name = "txtPIN";
            this.txtPIN.Size = new System.Drawing.Size(171, 23);
            this.txtPIN.TabIndex = 2;
            this.txtPIN.Visible = false;
            // 
            // txtBldgNo
            // 
            this.txtBldgNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBldgNo.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBldgNo.Location = new System.Drawing.Point(121, 14);
            this.txtBldgNo.Name = "txtBldgNo";
            this.txtBldgNo.ReadOnly = true;
            this.txtBldgNo.Size = new System.Drawing.Size(170, 23);
            this.txtBldgNo.TabIndex = 1;
            // 
            // txtBldgName
            // 
            this.txtBldgName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBldgName.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBldgName.Location = new System.Drawing.Point(121, 40);
            this.txtBldgName.Name = "txtBldgName";
            this.txtBldgName.Size = new System.Drawing.Size(436, 23);
            this.txtBldgName.TabIndex = 3;
            // 
            // txtHeight
            // 
            this.txtHeight.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtHeight.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHeight.Location = new System.Drawing.Point(476, 122);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtHeight.Size = new System.Drawing.Size(81, 23);
            this.txtHeight.TabIndex = 11;
            this.txtHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtArea
            // 
            this.txtArea.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtArea.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtArea.Location = new System.Drawing.Point(281, 93);
            this.txtArea.Name = "txtArea";
            this.txtArea.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtArea.Size = new System.Drawing.Size(81, 23);
            this.txtArea.TabIndex = 8;
            this.txtArea.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtArea.Leave += new System.EventHandler(this.txtArea_Leave);
            // 
            // txtCost
            // 
            this.txtCost.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCost.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCost.Location = new System.Drawing.Point(121, 67);
            this.txtCost.Name = "txtCost";
            this.txtCost.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtCost.Size = new System.Drawing.Size(170, 23);
            this.txtCost.TabIndex = 4;
            this.txtCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCost.Leave += new System.EventHandler(this.txtCost_Leave);
            // 
            // txtStoreys
            // 
            this.txtStoreys.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtStoreys.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStoreys.Location = new System.Drawing.Point(121, 93);
            this.txtStoreys.Name = "txtStoreys";
            this.txtStoreys.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtStoreys.Size = new System.Drawing.Size(51, 23);
            this.txtStoreys.TabIndex = 7;
            this.txtStoreys.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtUnits
            // 
            this.txtUnits.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUnits.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUnits.Location = new System.Drawing.Point(390, 67);
            this.txtUnits.Name = "txtUnits";
            this.txtUnits.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtUnits.Size = new System.Drawing.Size(95, 23);
            this.txtUnits.TabIndex = 5;
            this.txtUnits.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRemove);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.dgvList);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 164);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(568, 197);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Approved Permits and Proposed Dates of Construction/Installation ";
            // 
            // btnRemove
            // 
            this.btnRemove.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.Image = ((System.Drawing.Image)(resources.GetObject("btnRemove.Image")));
            this.btnRemove.Location = new System.Drawing.Point(519, 165);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(30, 26);
            this.btnRemove.TabIndex = 21;
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(488, 165);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(30, 26);
            this.btnAdd.TabIndex = 20;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToResizeRows = false;
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Location = new System.Drawing.Point(6, 22);
            this.dgvList.Name = "dgvList";
            this.dgvList.RowHeadersVisible = false;
            this.dgvList.Size = new System.Drawing.Size(556, 140);
            this.dgvList.TabIndex = 12;
            this.dgvList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellClick);
            this.dgvList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellContentClick);
            this.dgvList.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellEndEdit);
            this.dgvList.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvList_CellValidating);
            this.dgvList.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dgvList_Scroll);
            this.dgvList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvList_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Building No.:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Building Name:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "Estimated Cost:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "No of Storeys:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "Materials Used:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(178, 96);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 15);
            this.label6.TabIndex = 0;
            this.label6.Text = "Total Floor Area:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(323, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 15);
            this.label7.TabIndex = 0;
            this.label7.Text = "Land Pin:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.label7.Visible = false;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(310, 70);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(74, 15);
            this.label16.TabIndex = 0;
            this.label16.Text = "No. of Units:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(368, 96);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(93, 15);
            this.label17.TabIndex = 0;
            this.label17.Text = "Assessed Value:";
            this.label17.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(377, 125);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(93, 15);
            this.label18.TabIndex = 0;
            this.label18.Text = "Height in meter:";
            this.label18.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtAssVal
            // 
            this.txtAssVal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAssVal.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAssVal.Location = new System.Drawing.Point(476, 93);
            this.txtAssVal.Name = "txtAssVal";
            this.txtAssVal.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtAssVal.Size = new System.Drawing.Size(81, 23);
            this.txtAssVal.TabIndex = 9;
            this.txtAssVal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAssVal.Leave += new System.EventHandler(this.txtAssVal_Leave);
            // 
            // cmbMaterials
            // 
            this.cmbMaterials.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbMaterials.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMaterials.FormattingEnabled = true;
            this.cmbMaterials.Location = new System.Drawing.Point(121, 121);
            this.cmbMaterials.Name = "cmbMaterials";
            this.cmbMaterials.Size = new System.Drawing.Size(241, 24);
            this.cmbMaterials.TabIndex = 10;
            this.cmbMaterials.Leave += new System.EventHandler(this.cmbMaterials_Leave);
            // 
            // btnBldgUnits
            // 
            this.btnBldgUnits.Location = new System.Drawing.Point(491, 67);
            this.btnBldgUnits.Name = "btnBldgUnits";
            this.btnBldgUnits.Size = new System.Drawing.Size(66, 23);
            this.btnBldgUnits.TabIndex = 6;
            this.btnBldgUnits.Text = "Units";
            this.btnBldgUnits.UseVisualStyleBackColor = true;
            this.btnBldgUnits.Click += new System.EventHandler(this.btnBldgUnits_Click);
            // 
            // frmBldgDates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Aqua;
            this.ClientSize = new System.Drawing.Size(592, 364);
            this.ControlBox = false;
            this.Controls.Add(this.btnBldgUnits);
            this.Controls.Add(this.cmbMaterials);
            this.Controls.Add(this.txtAssVal);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCost);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtStoreys);
            this.Controls.Add(this.txtBldgNo);
            this.Controls.Add(this.txtUnits);
            this.Controls.Add(this.txtBldgName);
            this.Controls.Add(this.txtPIN);
            this.Controls.Add(this.txtHeight);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtArea);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmBldgDates";
            this.Text = "Building Information  ";
            this.Load += new System.EventHandler(this.frmBldgDates_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button btnBldgUnits;
        public MultiColumnComboBoxDemo.MultiColumnComboBox cmbMaterials;
        public System.Windows.Forms.TextBox txtPIN;
        public System.Windows.Forms.TextBox txtBldgNo;
        public System.Windows.Forms.TextBox txtBldgName;
        public System.Windows.Forms.TextBox txtHeight;
        public System.Windows.Forms.TextBox txtArea;
        public System.Windows.Forms.TextBox txtCost;
        public System.Windows.Forms.TextBox txtStoreys;
        public System.Windows.Forms.TextBox txtUnits;
        public System.Windows.Forms.DataGridView dgvList;
        public System.Windows.Forms.TextBox txtAssVal;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolTip toolTip2;
        public System.Windows.Forms.Button btnRemove;
        public System.Windows.Forms.Button btnAdd;
    }
}