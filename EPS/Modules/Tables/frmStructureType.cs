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


namespace Modules.Tables
{
    public partial class frmStructureType : Form
    {
        public frmStructureType()
        {
            InitializeComponent();
        }

        private void frmStructureType_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            dgStruct.Rows.Clear();
            OracleResultSet pSet = new OracleResultSet();
            pSet.Query = "select * from struc_tbl order by struc_code";
            if (pSet.Execute())
            {
                while (pSet.Read())
                    dgStruct.Rows.Add(pSet.GetString(0), pSet.GetString(1));
            }
            pSet.Close();
            dgStruct.ClearSelection();
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
            txtType.ReadOnly = blnEnable;
            dgStruct.Enabled = blnEnable;
            btnAdd.Enabled = blnEnable;
            btnEdit.Enabled = blnEnable;
            btnDelete.Enabled = blnEnable;
            btnExit.Enabled = blnEnable;
        }

        private string GenerateCode()
        {
            int iCode = 0;
            string sCode = "";
            OracleResultSet pSet = new OracleResultSet();
           // pSet.Query = "select max(struc_code) from struc_tbl";
            pSet.Query = "select nvl(max(struc_code), 0) as max from struc_tbl"; //AFM 20200903
            if (pSet.Execute())
                if (pSet.Read())
                {
                    if(pSet.GetString("max") != "0") //AFM 20200903
                    { 
                        iCode = Convert.ToInt16(pSet.GetString(0));
                        iCode++;
                        sCode = iCode.ToString("00");
                    }
                    else
                        sCode = "01";
                }
                //else
                    //sCode = "01";
            pSet.Close();

            return sCode;
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
                txtCode.Text = GenerateCode();
                txtType.Focus();
            }
            else //Save
            {
                if (txtType.Text.Trim() == "")
                {
                    MessageBox.Show("Structure Type field requires an entry", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (MessageBox.Show("Are you sure you want to save?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string sCode = GenerateCode();

                    OracleResultSet pSet = new OracleResultSet();
                    pSet.Query = "select struc_code from struc_tbl where struc_code ='" + sCode + "'";
                    if (pSet.Execute())
                        if (!pSet.Read())
                        {
                            pSet.Query = "insert into struc_tbl values ";
                            pSet.Query += "('" + sCode + "', ";
                            pSet.Query += " '" + StringUtilities.HandleApostrophe(txtType.Text.Trim()) + "') ";
                            pSet.ExecuteNonQuery();
                        }
                        else
                        {
                            MessageBox.Show("Structure Code " + sCode + " already exists.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtType.Focus();
                            pSet.Close();
                            return;
                        }
                    pSet.Close();

                    //pApp->AuditTrail("SS-A", "CONFIG", "Subj Code : " + m_sSubjCode);
                    MessageBox.Show("Structure successfully saved.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnExit.PerformClick();

                }
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
                txtCode.Focus();
            }
            else //Update
            {
                if (txtType.Text.Trim() == "")
                {
                    MessageBox.Show("Value field requires an entry", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                OracleResultSet pSet = new OracleResultSet();
                pSet.Query = "select * from struc_tbl where struc_desc = '" + StringUtilities.HandleApostrophe(txtType.Text.Trim()) + "' and struc_code <> '" + txtCode.Text.Trim() + "'";
                if (pSet.Execute())
                {
                    if (pSet.Read())
                    {
                        MessageBox.Show("Category: " + txtType.Text.Trim() + " already exists.", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtType.Focus();
                        pSet.Close();
                        return;
                    }
                    pSet.Close();
                }

                if (MessageBox.Show("Are you sure you want to save the changes?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    pSet = new OracleResultSet();
                    pSet.Query = "update struc_tbl set struc_desc = '";
                    pSet.Query += StringUtilities.HandleApostrophe(txtType.Text.Trim()) + "'";
                    pSet.Query += "where struc_code = '" + txtCode.Text.Trim() + "'";
                    pSet.ExecuteNonQuery();

                    //pApp->AuditTrail("SS-E", "CONFIG", m_sObj);
                    MessageBox.Show("Changes successfully saved.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnExit.PerformClick();
                }
            }
        }

        private bool IsStructureTypeInUse()
        {
            OracleResultSet pSet = new OracleResultSet();
            pSet.Query = "select struc_code from application_que where struc_code = '" + txtCode.Text.Trim() + "'";
            if (pSet.Execute())
            {
                if (pSet.Read())
                {
                    MessageBox.Show("There are records found using this Structure Type: " + txtType.Text + "\nDeletion denied !", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }
            else
            {
                pSet.Close();
                pSet = new OracleResultSet();
                pSet.Query = "select struc_code from application where struc_code = '" + txtCode.Text.Trim() + "'";
                if (pSet.Execute())
                {
                    if (pSet.Read())
                    {
                        MessageBox.Show("There are records found using this Structure Type: " + txtType.Text.Trim()  +"\nDeletion denied !", "",MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return false;

                    }
                }
            }
            pSet.Close();
            return true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgStruct.RowCount <= 0)
                return;

            if (txtCode.Text.Trim() == "")
            {
                MessageBox.Show("Please select the record to delete.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!IsStructureTypeInUse())
                return;

            if (MessageBox.Show("Are you sure you want to delete?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                OracleResultSet pSet = new OracleResultSet();
                pSet.Query = "delete from struc_tbl where struc_code = '" + txtCode.Text.Trim() + "'";
                pSet.ExecuteNonQuery();

                //pApp->AuditTrail("SS-D", "CONFIG", m_sSubjCode);
                MessageBox.Show("Structure successfully deleted!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void dgStruct_SelectionChanged(object sender, EventArgs e)
        {
            if (dgStruct.RowCount >= 1)
            {
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                txtCode.Text = dgStruct.CurrentRow.Cells[0].Value?.ToString();
                txtType.Text = dgStruct.CurrentRow.Cells[1].Value?.ToString();
            }
        }
    }
}
