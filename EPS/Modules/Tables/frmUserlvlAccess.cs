using Common.DataConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Modules.Tables
{
    public partial class frmUserlvlAccess : Form
    {
        public frmUserlvlAccess()
        {
            InitializeComponent();
        }

        private void frmUserlvlAccess_Load(object sender, EventArgs e)
        {
            //AFM 20200803 (s)
            ClearALL();
            btnGrant.Enabled = false;
            btnRevoke.Enabled = false;
            OracleResultSet result = new OracleResultSet();
            result.Query = "select * from rights where right_code not like 'COL-%' order by right_code, right_desc";
            if(result.Execute())
                while(result.Read())
                    dgUserAcc.Rows.Add(false,result.GetString("right_desc"), result.GetString("right_code"));

            result.Close();

            result.Query = "select * from user_level";
            if (result.Execute())
                while (result.Read())
                    cboDesignation.Items.Add(result.GetString("user_level"));

            result.Close();

            dgUserAcc.Enabled = false;
            dgUserAcc.PerformLayout(); //fix for scrollbar not resizing or updating for some pc
            //AFM 20200803 (e)
        }

        private void ClearALL()
        {
            dgUserAcc.Rows.Clear();
            cboDesignation.Items.Clear();
        }

        private void cboDesignation_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void cboDesignation_SelectedIndexChanged(object sender, EventArgs e) //AFM 20200803
        {
            OracleResultSet result = new OracleResultSet();
            int iCnt = -1;
            string sMod = string.Empty;
            bool hasVal = false;
            result.Query = "select UR.*, RI.right_desc from user_level_rights UR, rights RI where UR.MODULE_CODE = RI.RIGHT_CODE and user_level = '" + cboDesignation.SelectedItem.ToString().Trim() + "' AND RI.RIGHT_CODE NOT LIKE 'COL-%' order by UR.module_code, RI.right_desc";
            if (result.Execute())
                while (result.Read())
                {
                    hasVal = true;
                    iCnt++;
                    sMod = result.GetString("module_code").Trim();
                    if (dgUserAcc[2, iCnt].Value.ToString().Trim() == sMod)
                        dgUserAcc[0, iCnt].Value = 1;
                    else
                    {
                        dgUserAcc[0, iCnt].Value = 0;
                        continue;
                    }
                }
            if(hasVal == false)
            {
                foreach (DataGridViewRow row in dgUserAcc.Rows)
                {
                    row.Cells[0].Value = false;
                }
            }
            result.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGrant_Click(object sender, EventArgs e) //AFM 20200803
        {
            foreach (DataGridViewRow row in dgUserAcc.Rows)
            {
                row.Cells[0].Value = true;
            }
        }

        private void btnRevoke_Click(object sender, EventArgs e) //AFM 20200803
        {
            foreach (DataGridViewRow row in dgUserAcc.Rows)
            {
                row.Cells[0].Value = false;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e) //AFM 20200803
        {
            if (btnEdit.Text == "Edit")
            {
                dgUserAcc.Enabled = true;
                btnEdit.Text = "Save";
                btnGrant.Enabled = true;
                btnRevoke.Enabled = true;
            }
            else if(btnEdit.Text == "Save")
            {
                if (MessageBox.Show("Are you sure you want to Save?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                { 
                    OracleResultSet rSet2 = new OracleResultSet();
                    string userlvl = cboDesignation.SelectedItem.ToString().Trim();

                    rSet2.Query = "DELETE FROM USER_LEVEL_RIGHTS WHERE USER_LEVEL = '" + userlvl + "' and module_code not like 'COL%'";
                    if (rSet2.ExecuteNonQuery() == 0)
                    { }
                    rSet2.Close();

                    foreach (DataGridViewRow row in dgUserAcc.Rows)
                    {
                        if(Convert.ToBoolean(row.Cells[0].Value) == true)
                        { 
                            rSet2.Query = "INSERT INTO USER_LEVEL_RIGHTS VALUES ('"+ userlvl + "', '"+ row.Cells[2].Value +"')";
                            if (rSet2.ExecuteNonQuery() == 0)
                            { }
                        }
                    }
                    rSet2.Close();
                    btnEdit.Text = "Edit";

                    dgUserAcc.Enabled = false;
                    btnGrant.Enabled = false;
                    btnRevoke.Enabled = false;

                    MessageBox.Show("Successfully Saved!");
                }
            }
        }

        private void dgUserAcc_CellClick(object sender, DataGridViewCellEventArgs e) //AFM 20200803
        {
            try
            { 
            if (Convert.ToBoolean(dgUserAcc.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) == false)
                dgUserAcc.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = true;
            else
                dgUserAcc.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = false;
            }
            catch { }
        }
    }
}
