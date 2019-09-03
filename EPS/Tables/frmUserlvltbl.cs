using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Amellar.Common.DataConnector;
using Common.StringUtilities;

namespace Tables
{
    public partial class frmUserlvltbl : Form
    {
        public frmUserlvltbl()
        {
            InitializeComponent();
        }

        private void frmUserlvltbl_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void EnableControl(bool blnEnable)
        {
            dgULT.Enabled = blnEnable;
            txtUlvl.ReadOnly = blnEnable;
            btnAdd.Enabled = blnEnable;
            btnEdit.Enabled = blnEnable;
            btnDelete.Enabled = blnEnable;
            btnExit.Enabled = blnEnable;
        }

        private void dgULT_SelectionChanged(object sender, EventArgs e)
        {
            if (dgULT.RowCount >= 1)
            {
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                txtUlvl.Text = dgULT.CurrentRow.Cells[0].Value?.ToString();
            }
        }

        private void ClearFields()
        {
            foreach (var c in this.Controls)
            {
                if (c is TextBox)
                    ((TextBox)c).Text = "";
            }
        }

        private void LoadData()
        {
            dgULT.Rows.Clear();
            OracleResultSet pSet = new OracleResultSet();
            pSet.Query = "select user_level from user_level";
            if (pSet.Execute())
            {
                while (pSet.Read())
                    dgULT.Rows.Add(pSet.GetString(0), pSet.GetString(1), pSet.GetString(2));
            }
            pSet.Close();
            dgULT.ClearSelection();
            ClearFields();
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
        }

        private bool ValidateData()
        {
            if (txtUlvl.Text.Trim() == "")
            {
                MessageBox.Show("User Level field requires an entry.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return true; 
            }

            OracleResultSet pSet = new OracleResultSet();
            pSet.Query = "select * from user_level where user_level = '" + StringUtilities.HandleApostrophe(txtUlvl.Text.Trim()) + "'";
            if (pSet.Execute())
                if (pSet.Read())
                {
                    MessageBox.Show("Duplicate record.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return true;
                }
            pSet.Close();

            return false;
        }

        private bool CheckIfThereAreUsersOfThisUserLevel()
        {
            OracleResultSet pSet = new OracleResultSet();
            pSet.Query = "select user_level from users where user_level = '" + StringUtilities.HandleApostrophe(txtUlvl.Text.Trim()) + "'";
            if (pSet.Execute())
                if (pSet.Read())
                {
                    MessageBox.Show("This USER LEVEL record is in use.\nDeletion denied.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return true;
                }
            pSet.Close();
            return false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Add")
            {
                ClearFields();
                EnableControl(false);
                btnAdd.Enabled = true;
                btnExit.Enabled = true;
                btnAdd.Text = "Save";
                btnExit.Text = "Cancel";
            }
            else //Save
            {
                if (ValidateData())
                    return;

                OracleResultSet pSet = new OracleResultSet();
                pSet.Query = "insert into user_level values ";
                pSet.Query += "('" + StringUtilities.HandleApostrophe(txtUlvl.Text.Trim()) + "')";
                pSet.ExecuteNonQuery();

                //pApp->AuditTrail("SS-A", "CONFIG", "Subj Code : " + m_sSubjCode);
                MessageBox.Show("User level successfully saved.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnExit.PerformClick();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEdit.Text == "Edit")
            {
                EnableControl(false);
                btnEdit.Enabled = true;
                btnExit.Enabled = true;
                btnEdit.Text = "Update";
                btnExit.Text = "Cancel";
                txtUlvl.Focus();
            }
            else //Update
            {
                if (ValidateData())
                    return;

                if (MessageBox.Show("Are you sure you want to save the changes?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    OracleResultSet pSet = new OracleResultSet();
                    pSet.Query = "update user_level set user_level = '";
                    pSet.Query += StringUtilities.HandleApostrophe(txtUlvl.Text.Trim()) + "'";
                    pSet.Query += "where user_level = '" + dgULT.CurrentRow.Cells[0].Value?.ToString() + "'";
                    pSet.ExecuteNonQuery();
                    
                    pSet.Query = "update user_level_rights set user_level = '";
                    pSet.Query += StringUtilities.HandleApostrophe(txtUlvl.Text.Trim()) + "'";
                    pSet.Query += "where user_level = '" + dgULT.CurrentRow.Cells[0].Value?.ToString() + "'";
                    pSet.ExecuteNonQuery();

                    pSet.Query = "update users set user_level = '";
                    pSet.Query += StringUtilities.HandleApostrophe(txtUlvl.Text.Trim()) + "'";
                    pSet.Query += "where user_level = '" + dgULT.CurrentRow.Cells[0].Value?.ToString() + "'";
                    pSet.ExecuteNonQuery();

                    MessageBox.Show("Changes successfully saved.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnExit.PerformClick();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgULT.RowCount <= 0)
                return;

            if (txtUlvl.Text.Trim() == "")
            {
                MessageBox.Show("Please select the record to delete.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (CheckIfThereAreUsersOfThisUserLevel())
                return;

            if (MessageBox.Show("Are you sure you want to delete?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                OracleResultSet pSet = new OracleResultSet();
                pSet.Query = "delete from user_level where user_level = '" + dgULT.CurrentRow.Cells[0].Value?.ToString() + "'";
                pSet.ExecuteNonQuery();

                pSet.Query = "delete from user_level_rights where user_level = '" + dgULT.CurrentRow.Cells[0].Value?.ToString() + "'";
                pSet.ExecuteNonQuery();

                pSet.Query = "update users set user_level = '' where user_level = '" + dgULT.CurrentRow.Cells[0].Value?.ToString() + "'";
                pSet.ExecuteNonQuery();

                pSet.Query = "delete from users_rights where user_code in (select user_code from users where user_level = '')";
                pSet.ExecuteNonQuery();

                MessageBox.Show("User level successfully deleted!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnExit.Text = "Cancel";
                btnExit.Enabled = true;
                btnExit.PerformClick();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (btnExit.Text == "Cancel")
            {
                LoadData();
                ClearFields();
                EnableControl(true);
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                btnAdd.Text = "Add";
                btnEdit.Text = "Edit";
                btnExit.Text = "Exit";
            }
            else //Exit
            {
                this.Close();
            }
        }

    }
}
