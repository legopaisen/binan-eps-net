namespace Modules.Utilities.Forms
{
    partial class frmScheduleDelete
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoRevAcct = new System.Windows.Forms.RadioButton();
            this.rdoSubCat = new System.Windows.Forms.RadioButton();
            this.rdoSubsidiary = new System.Windows.Forms.RadioButton();
            this.txtRevAcct = new System.Windows.Forms.TextBox();
            this.txtSubCat = new System.Windows.Forms.TextBox();
            this.txtSubsidiary = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(177, 156);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(97, 31);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(73, 156);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(97, 31);
            this.btnDelete.TabIndex = 19;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtSubsidiary);
            this.groupBox1.Controls.Add(this.txtSubCat);
            this.groupBox1.Controls.Add(this.txtRevAcct);
            this.groupBox1.Controls.Add(this.rdoSubsidiary);
            this.groupBox1.Controls.Add(this.rdoSubCat);
            this.groupBox1.Controls.Add(this.rdoRevAcct);
            this.groupBox1.Location = new System.Drawing.Point(14, 14);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(260, 134);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            // 
            // rdoRevAcct
            // 
            this.rdoRevAcct.AutoSize = true;
            this.rdoRevAcct.Location = new System.Drawing.Point(7, 26);
            this.rdoRevAcct.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rdoRevAcct.Name = "rdoRevAcct";
            this.rdoRevAcct.Size = new System.Drawing.Size(134, 22);
            this.rdoRevAcct.TabIndex = 0;
            this.rdoRevAcct.TabStop = true;
            this.rdoRevAcct.Text = "Revenue Account";
            this.rdoRevAcct.UseVisualStyleBackColor = true;
            this.rdoRevAcct.CheckedChanged += new System.EventHandler(this.rdoRevAcct_CheckedChanged);
            // 
            // rdoSubCat
            // 
            this.rdoSubCat.AutoSize = true;
            this.rdoSubCat.Location = new System.Drawing.Point(7, 60);
            this.rdoSubCat.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rdoSubCat.Name = "rdoSubCat";
            this.rdoSubCat.Size = new System.Drawing.Size(102, 22);
            this.rdoSubCat.TabIndex = 2;
            this.rdoSubCat.TabStop = true;
            this.rdoSubCat.Text = "Subcategory";
            this.rdoSubCat.UseVisualStyleBackColor = true;
            this.rdoSubCat.CheckedChanged += new System.EventHandler(this.rdoSubCat_CheckedChanged);
            // 
            // rdoSubsidiary
            // 
            this.rdoSubsidiary.AutoSize = true;
            this.rdoSubsidiary.Location = new System.Drawing.Point(7, 95);
            this.rdoSubsidiary.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rdoSubsidiary.Name = "rdoSubsidiary";
            this.rdoSubsidiary.Size = new System.Drawing.Size(90, 22);
            this.rdoSubsidiary.TabIndex = 4;
            this.rdoSubsidiary.TabStop = true;
            this.rdoSubsidiary.Text = "Subsidiary";
            this.rdoSubsidiary.UseVisualStyleBackColor = true;
            this.rdoSubsidiary.CheckedChanged += new System.EventHandler(this.rdoSubsidiary_CheckedChanged);
            // 
            // txtRevAcct
            // 
            this.txtRevAcct.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRevAcct.Location = new System.Drawing.Point(147, 22);
            this.txtRevAcct.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRevAcct.MaxLength = 2;
            this.txtRevAcct.Name = "txtRevAcct";
            this.txtRevAcct.ReadOnly = true;
            this.txtRevAcct.Size = new System.Drawing.Size(101, 26);
            this.txtRevAcct.TabIndex = 1;
            // 
            // txtSubCat
            // 
            this.txtSubCat.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSubCat.Location = new System.Drawing.Point(147, 55);
            this.txtSubCat.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSubCat.MaxLength = 4;
            this.txtSubCat.Name = "txtSubCat";
            this.txtSubCat.ReadOnly = true;
            this.txtSubCat.Size = new System.Drawing.Size(101, 26);
            this.txtSubCat.TabIndex = 3;
            // 
            // txtSubsidiary
            // 
            this.txtSubsidiary.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSubsidiary.Location = new System.Drawing.Point(147, 90);
            this.txtSubsidiary.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSubsidiary.MaxLength = 6;
            this.txtSubsidiary.Name = "txtSubsidiary";
            this.txtSubsidiary.ReadOnly = true;
            this.txtSubsidiary.Size = new System.Drawing.Size(101, 26);
            this.txtSubsidiary.TabIndex = 5;
            // 
            // frmScheduleDelete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.ClientSize = new System.Drawing.Size(296, 197);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDelete);
            this.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmScheduleDelete";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Delete Schedule";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtSubsidiary;
        private System.Windows.Forms.TextBox txtSubCat;
        private System.Windows.Forms.TextBox txtRevAcct;
        private System.Windows.Forms.RadioButton rdoSubsidiary;
        private System.Windows.Forms.RadioButton rdoSubCat;
        private System.Windows.Forms.RadioButton rdoRevAcct;
    }
}