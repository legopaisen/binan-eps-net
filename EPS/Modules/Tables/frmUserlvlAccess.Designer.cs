﻿namespace Modules.Tables
{
    partial class frmUserlvlAccess
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUserlvlAccess));
            this.label1 = new System.Windows.Forms.Label();
            this.cboDesignation = new System.Windows.Forms.ComboBox();
            this.dgUserAcc = new System.Windows.Forms.DataGridView();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnRevoke = new System.Windows.Forms.Button();
            this.btnGrant = new System.Windows.Forms.Button();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.module_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgUserAcc)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Designation";
            // 
            // cboDesignation
            // 
            this.cboDesignation.FormattingEnabled = true;
            this.cboDesignation.Location = new System.Drawing.Point(99, 12);
            this.cboDesignation.Name = "cboDesignation";
            this.cboDesignation.Size = new System.Drawing.Size(251, 31);
            this.cboDesignation.TabIndex = 1;
            this.cboDesignation.SelectedIndexChanged += new System.EventHandler(this.cboDesignation_SelectedIndexChanged);
            this.cboDesignation.SelectedValueChanged += new System.EventHandler(this.cboDesignation_SelectedValueChanged);
            // 
            // dgUserAcc
            // 
            this.dgUserAcc.AllowUserToAddRows = false;
            this.dgUserAcc.AllowUserToDeleteRows = false;
            this.dgUserAcc.AllowUserToResizeRows = false;
            this.dgUserAcc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgUserAcc.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.module_code});
            this.dgUserAcc.Location = new System.Drawing.Point(12, 47);
            this.dgUserAcc.MultiSelect = false;
            this.dgUserAcc.Name = "dgUserAcc";
            this.dgUserAcc.ReadOnly = true;
            this.dgUserAcc.RowHeadersVisible = false;
            this.dgUserAcc.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgUserAcc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgUserAcc.Size = new System.Drawing.Size(339, 356);
            this.dgUserAcc.TabIndex = 2;
            this.dgUserAcc.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgUserAcc_CellClick);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(259, 454);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(92, 34);
            this.btnExit.TabIndex = 9;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(161, 454);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(92, 34);
            this.btnEdit.TabIndex = 10;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnRevoke
            // 
            this.btnRevoke.Location = new System.Drawing.Point(259, 414);
            this.btnRevoke.Name = "btnRevoke";
            this.btnRevoke.Size = new System.Drawing.Size(92, 34);
            this.btnRevoke.TabIndex = 11;
            this.btnRevoke.Text = "Revoke All";
            this.btnRevoke.UseVisualStyleBackColor = true;
            this.btnRevoke.Click += new System.EventHandler(this.btnRevoke_Click);
            // 
            // btnGrant
            // 
            this.btnGrant.Location = new System.Drawing.Point(161, 414);
            this.btnGrant.Name = "btnGrant";
            this.btnGrant.Size = new System.Drawing.Size(92, 34);
            this.btnGrant.TabIndex = 12;
            this.btnGrant.Text = "Grant All";
            this.btnGrant.UseVisualStyleBackColor = true;
            this.btnGrant.Click += new System.EventHandler(this.btnGrant_Click);
            // 
            // Column1
            // 
            this.Column1.HeaderText = " ";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column1.Width = 50;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Module Permissions";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 260;
            // 
            // module_code
            // 
            this.module_code.HeaderText = "Module Code";
            this.module_code.Name = "module_code";
            this.module_code.ReadOnly = true;
            this.module_code.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // frmUserlvlAccess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Aqua;
            this.ClientSize = new System.Drawing.Size(362, 494);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnRevoke);
            this.Controls.Add(this.btnGrant);
            this.Controls.Add(this.dgUserAcc);
            this.Controls.Add(this.cboDesignation);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Calibri", 11.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUserlvlAccess";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User level Access";
            this.Load += new System.EventHandler(this.frmUserlvlAccess_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgUserAcc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboDesignation;
        private System.Windows.Forms.DataGridView dgUserAcc;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnRevoke;
        private System.Windows.Forms.Button btnGrant;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn module_code;
    }
}