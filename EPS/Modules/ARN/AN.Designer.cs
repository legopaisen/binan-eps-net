namespace Modules.ARN
{
    partial class AN
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
            this.txtCode = new System.Windows.Forms.TextBox();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.txtSeries = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtCode
            // 
            this.txtCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCode.Location = new System.Drawing.Point(3, 3);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(41, 20);
            this.txtCode.TabIndex = 0;
            this.txtCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCode.TextChanged += new System.EventHandler(this.txtCode_TextChanged);
            // 
            // txtYear
            // 
            this.txtYear.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtYear.Location = new System.Drawing.Point(50, 3);
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(39, 20);
            this.txtYear.TabIndex = 1;
            this.txtYear.Text = "2019";
            this.txtYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtYear.TextChanged += new System.EventHandler(this.txtYear_TextChanged);
            this.txtYear.Leave += new System.EventHandler(this.txtYear_Leave);
            // 
            // txtSeries
            // 
            this.txtSeries.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSeries.Location = new System.Drawing.Point(95, 3);
            this.txtSeries.MaxLength = 4;
            this.txtSeries.Name = "txtSeries";
            this.txtSeries.Size = new System.Drawing.Size(39, 20);
            this.txtSeries.TabIndex = 2;
            this.txtSeries.Text = "0001";
            this.txtSeries.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSeries.Leave += new System.EventHandler(this.txtSeries_Leave);
            // 
            // AN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtSeries);
            this.Controls.Add(this.txtYear);
            this.Controls.Add(this.txtCode);
            this.Name = "AN";
            this.Size = new System.Drawing.Size(158, 28);
            this.Load += new System.EventHandler(this.AN_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.TextBox txtYear;
        private System.Windows.Forms.TextBox txtSeries;
    }
}
