namespace Modules.ARN
{
    partial class ARN
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtLGUCode = new System.Windows.Forms.TextBox();
            this.txtDistCode = new System.Windows.Forms.TextBox();
            this.txtTaxYear = new System.Windows.Forms.TextBox();
            this.txtSeries = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtLGUCode
            // 
            this.txtLGUCode.Location = new System.Drawing.Point(3, 3);
            this.txtLGUCode.MaxLength = 3;
            this.txtLGUCode.Name = "txtLGUCode";
            this.txtLGUCode.Size = new System.Drawing.Size(35, 20);
            this.txtLGUCode.TabIndex = 0;
            this.txtLGUCode.TextChanged += new System.EventHandler(this.txtLGUCode_TextChanged);
            // 
            // txtDistCode
            // 
            this.txtDistCode.Location = new System.Drawing.Point(42, 3);
            this.txtDistCode.MaxLength = 2;
            this.txtDistCode.Name = "txtDistCode";
            this.txtDistCode.Size = new System.Drawing.Size(22, 20);
            this.txtDistCode.TabIndex = 1;
            this.txtDistCode.TextChanged += new System.EventHandler(this.txtDistCode_TextChanged);
            this.txtDistCode.Leave += new System.EventHandler(this.txtDistCode_Leave);
            // 
            // txtTaxYear
            // 
            this.txtTaxYear.Location = new System.Drawing.Point(68, 3);
            this.txtTaxYear.MaxLength = 4;
            this.txtTaxYear.Name = "txtTaxYear";
            this.txtTaxYear.Size = new System.Drawing.Size(35, 20);
            this.txtTaxYear.TabIndex = 2;
            this.txtTaxYear.TextChanged += new System.EventHandler(this.txtTaxYear_TextChanged);
            this.txtTaxYear.Leave += new System.EventHandler(this.txtTaxYear_Leave);
            // 
            // txtSeries
            // 
            this.txtSeries.Location = new System.Drawing.Point(107, 3);
            this.txtSeries.MaxLength = 4;
            this.txtSeries.Name = "txtSeries";
            this.txtSeries.Size = new System.Drawing.Size(35, 20);
            this.txtSeries.TabIndex = 3;
            this.txtSeries.Leave += new System.EventHandler(this.txtSeries_Leave);
            // 
            // ARN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtSeries);
            this.Controls.Add(this.txtTaxYear);
            this.Controls.Add(this.txtDistCode);
            this.Controls.Add(this.txtLGUCode);
            this.Name = "ARN";
            this.Size = new System.Drawing.Size(148, 26);
            this.Load += new System.EventHandler(this.ARN_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLGUCode;
        private System.Windows.Forms.TextBox txtDistCode;
        private System.Windows.Forms.TextBox txtTaxYear;
        private System.Windows.Forms.TextBox txtSeries;
    }
}
