using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common.DataConnector;
using Common.StringUtilities;
using Modules.Utilities;

namespace Modules.Tables
{
    public partial class frmConfig : Form
    {
        public frmConfig()
        {
            InitializeComponent();
        }

        private string m_sObject = "";

        private void frmConfig_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            dgConfig.Rows.Clear();
            OracleResultSet pSet = new OracleResultSet();
            pSet.Query = "select * From config order by subj_code";
            if (pSet.Execute())
            {
                while (pSet.Read())
                    dgConfig.Rows.Add(pSet.GetString(0), pSet.GetString(1), pSet.GetString(2));
            }
            pSet.Close();
            dgConfig.ClearSelection();
            ClearFields();
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void ClearFields()
        {
            foreach (var c in this.Controls)
            {
                if (c is TextBox)
                    ((TextBox)c).Text = "";
            }
        }

        private void EnableControl(bool blnEnable)
        {
            txtSubject.ReadOnly = blnEnable;
            txtValue.ReadOnly = blnEnable;
            dgConfig.Enabled = blnEnable;
            btnAdd.Enabled = blnEnable;
            btnEdit.Enabled = blnEnable;
            btnDelete.Enabled = blnEnable;
            btnExit.Enabled = blnEnable;
            btnPrint.Enabled = blnEnable;
        }

        private void dgConfig_SelectionChanged(object sender, EventArgs e)
        {
            if (dgConfig.RowCount >= 1)
            {
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                txtSubject.Text = dgConfig.CurrentRow.Cells[1].Value?.ToString();
                txtValue.Text = dgConfig.CurrentRow.Cells[2].Value?.ToString();
            }
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
                txtSubject.Focus();
            }
            else //Save
            {
                if (txtSubject.Text.Trim() == "")
                {
                    MessageBox.Show("Subject field requires an entry.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (txtValue.Text.Trim() == "")
                {
                    MessageBox.Show("Value field requires an entry", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }


                if (MessageBox.Show("Are you sure you want to save?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int iCode = 0;
                    string sCode = "";
                    OracleResultSet pSet = new OracleResultSet();
                    pSet.Query = "select max(subj_code) from config";
                    if (pSet.Execute())
                        if (pSet.Read())
                        {
                            iCode = Convert.ToInt16(pSet.GetString(0));
                            iCode++;
                            sCode = iCode.ToString("00");
                        }
                        else
                            sCode = "01";
                    pSet.Close();

                    pSet.Query = "insert into config values ";
                    pSet.Query += "('" + sCode + "', ";
                    pSet.Query += " '" + StringUtilities.HandleApostrophe(txtSubject.Text.Trim()) + "', ";
                    pSet.Query += " '" + StringUtilities.HandleApostrophe(txtValue.Text.Trim()) + "','') ";
                    pSet.ExecuteNonQuery();

                    //pApp->AuditTrail("SS-A", "CONFIG", "Subj Code : " + m_sSubjCode);
                    if (Utilities.AuditTrail.InsertTrail("SS-A", "CONFIG", "Subj Desc/Value: " + txtSubject.Text.Trim() + "/" + txtValue.Text.Trim()) == 0)
                    {
                        MessageBox.Show("Failed to insert audit trail.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    MessageBox.Show("System setting successfully saved.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnExit.PerformClick();

                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEdit.Text == "Edit")
            {
                m_sObject = "Subj Code: " + dgConfig.CurrentRow.Cells[0].Value?.ToString();
                m_sObject += " / Old Desc:" + txtSubject.Text.Trim();
                m_sObject += " / Old Value:" + txtValue.Text.Trim();

                EnableControl(false);
                btnEdit.Enabled = true;
                btnExit.Enabled = true;
                btnEdit.Text = "Update";
                btnExit.Text = "Cancel";
                txtSubject.Focus();
            }
            else //Update
            {
                if (txtSubject.Text.Trim() == "")
                {
                    MessageBox.Show("Subject field requires an entry.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (txtValue.Text.Trim() == "")
                {
                    MessageBox.Show("Value field requires an entry", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (MessageBox.Show("Are you sure you want to save the changes?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    OracleResultSet pSet = new OracleResultSet();
                    pSet.Query = "update config set value_fld = '";
                    pSet.Query += StringUtilities.HandleApostrophe(txtValue.Text.Trim()) + "'";
                    pSet.Query += ", subj_desc = '" + StringUtilities.HandleApostrophe(txtSubject.Text.Trim()) + "' ";
                    pSet.Query += "where subj_code = '" + dgConfig.CurrentRow.Cells[0].Value?.ToString() + "'";
                    pSet.ExecuteNonQuery();

                    m_sObject += "New Desc: " + txtSubject.Text.Trim();
                    m_sObject += " / New Val: " + txtValue.Text.Trim();

                    //pApp->AuditTrail("SS-E", "CONFIG", m_sObj);
                    if (Utilities.AuditTrail.InsertTrail("SS-E", "CONFIG", m_sObject) == 0)
                    {
                        MessageBox.Show("Failed to insert audit trail.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    MessageBox.Show("Changes successfully saved.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnExit.PerformClick();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgConfig.RowCount <= 0)
                return;

            if (txtSubject.Text.Trim() == "")
            {
                MessageBox.Show("Please select the record to delete.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            m_sObject = "Subj Code: " + dgConfig.CurrentRow.Cells[0].Value?.ToString();
            m_sObject += " / Desc:" + txtSubject.Text.Trim();
            m_sObject += " / Value:" + txtValue.Text.Trim();

            if (MessageBox.Show("Are you sure you want to delete?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                OracleResultSet pSet = new OracleResultSet();
                pSet.Query = "delete from config where subj_code = '" + dgConfig.CurrentRow.Cells[0].Value?.ToString() + "'";
                pSet.ExecuteNonQuery();

                //pApp->AuditTrail("SS-D", "CONFIG", m_sSubjCode);
                if (Utilities.AuditTrail.InsertTrail("SS-D", "CONFIG", m_sObject) == 0)
                {
                    MessageBox.Show("Failed to insert audit trail.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("System setting successfully deleted!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnExit.Text = "Cancel";
                btnExit.Enabled = true;
                btnExit.PerformClick();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (btnExit.Text == "Cancel")
            {
                m_sObject = "";
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

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }
    }
}
