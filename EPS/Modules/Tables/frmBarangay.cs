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
    public partial class frmBarangay : Form
    {
        public frmBarangay()
        {
            InitializeComponent();
        }

        private void frmBarangay_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            dgView.Rows.Clear();
            OracleResultSet pSet = new OracleResultSet();
            pSet.Query = "select brgy_code, brgy_nm, dist_code from brgy order by brgy_code";
            if (pSet.Execute())
            {
                while (pSet.Read())
                    dgView.Rows.Add(pSet.GetString(0), pSet.GetString(1), pSet.GetString(2));
            }
            pSet.Close();
            dgView.ClearSelection();
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
            txtName.ReadOnly = blnEnable;
            txtMunCode.ReadOnly = blnEnable;
            dgView.Enabled = blnEnable;
            btnAdd.Enabled = blnEnable;
            btnEdit.Enabled = blnEnable;
            btnDelete.Enabled = blnEnable;
            btnExit.Enabled = blnEnable;
            btnPrint.Enabled = blnEnable;
        }

        private string GenerateCode()
        {
            int iCode = 0;
            string sCode = "";
            OracleResultSet pSet = new OracleResultSet();
            pSet.Query = "select max(brgy_code) from brgy";
            if (pSet.Execute())
                if (pSet.Read())
                {
                    iCode = Convert.ToInt16(pSet.GetString(0));
                    iCode++;
                    sCode = iCode.ToString("000");
                }
                else
                    sCode = "001";
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
                txtMunCode.Focus();
                txtCode.Text = GenerateCode();
            }
            else //Save
            {
                if (txtCode.Text.Trim() == "")
                {
                    MessageBox.Show("Barangay name field entry is required.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (txtMunCode.Text.Trim() == "")
                {
                    MessageBox.Show("Municipal code field entry is required", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (MessageBox.Show("Are you sure you want to save?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string sCode = GenerateCode();

                    OracleResultSet pSet = new OracleResultSet();
                    pSet.Query = "select brgy_code from brgy where brgy_code ='" + sCode + "'";
                    if (pSet.Execute())
                        if (!pSet.Read())
                        {
                            pSet.Query = "insert into brgy(brgy_code,brgy_nm,dist_code) values ";
                            pSet.Query += "('" + sCode + "', ";
                            pSet.Query += " '" + StringUtilities.HandleApostrophe(txtName.Text.Trim()) + "', ";
                            pSet.Query += " '" + StringUtilities.HandleApostrophe(txtMunCode.Text.Trim()) + "') ";
                            pSet.ExecuteNonQuery();
                        }
                        else
                        {
                            MessageBox.Show("Barangay Code " + sCode + " already exists.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtMunCode.Focus();
                            pSet.Close();
                            return;
                        }
                    pSet.Close();

                    //pApp->AuditTrail("STB-A", "brgy", "Brgy Code : " + txtName.Text.Trim());
                    MessageBox.Show("Barangay successfully saved.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                txtMunCode.Focus();
            }
            else //Update
            {
                if (txtMunCode.Text.Trim() == "")
                {
                    MessageBox.Show("Municipal code requires an entry.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (txtName.Text.Trim() == "")
                {
                    MessageBox.Show("Barangay field requires an entry", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (MessageBox.Show("Are you sure you want to save the changes?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    OracleResultSet pSet = new OracleResultSet();
                    pSet.Query = "update brgy set brgy_nm = '";
                    pSet.Query += StringUtilities.HandleApostrophe(txtName.Text.Trim()) + "'";
                    pSet.Query += ", dist_code = '" + StringUtilities.HandleApostrophe(txtMunCode.Text.Trim()) + "' ";
                    pSet.Query += "where brgy_code = '" + txtCode.Text.Trim() + "'";
                    pSet.ExecuteNonQuery();

                    //pApp->AuditTrail("STB-E","brgy","Brgy: "+m_sObj);
                    MessageBox.Show("Changes successfully saved.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnExit.PerformClick();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgView.RowCount <= 0)
                return;

            if (txtCode.Text.Trim() == "")
            {
                MessageBox.Show("Please select the record to delete.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                OracleResultSet pSet = new OracleResultSet();
                if (CheckIfBrgyIsInUse(txtName.Text.Trim()))
                {
                    pSet.Query = "delete from brgy where brgy_code = '" + txtCode.Text.Trim() + "'";
                    pSet.ExecuteNonQuery();

                    //pApp->AuditTrail("STB-D", "brgy", "Brgy:"+m_sObj);
                    MessageBox.Show("Barangay successfully deleted!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    btnExit.Text = "Cancel";
                    btnExit.Enabled = true;
                    btnExit.PerformClick();
                }
            }
        }

        private bool CheckIfBrgyIsInUse(string sBrgyName)
        {
            OracleResultSet pSet = new OracleResultSet();
            pSet.Query = "select account.acct_brgy, engineer_tbl.engr_brgy ";
            pSet.Query += "from account, engineer_tbl ";
            pSet.Query += "where account.acct_brgy = '" + sBrgyName + "' ";
            pSet.Query += "or engineer_tbl.engr_brgy = '" + sBrgyName + "'";

            if (pSet.Execute())
            {
                if (pSet.Read())
                {
                    pSet.Close();
                    MessageBox.Show("Some records are found referenced with this Barangay\nDeletion denied !", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
                else
                {
                    pSet.Close();
                    pSet.Query = "select proj_brgy from application ";
                    pSet.Query += "where proj_brgy  = '" + sBrgyName + "'";
                    if (pSet.Execute())
                    {
                        if (pSet.Read())
                        {
                            pSet.Close();
                            MessageBox.Show("Some records are found referenced with this Barangay.\nDeletion denied !", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return false;
                        }
                        else
                        {
                            pSet.Close();
                            //checks from referenced table(s)
                            pSet.Query = "select proj_brgy from application_que ";
                            pSet.Query += "where proj_brgy  = '" + sBrgyName + "'";
                            if (pSet.Execute())
                                if (pSet.Read())
                                {
                                    MessageBox.Show("Some records are found referenced with this Barangay.\nDeletion denied !", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                    return false;
                                }
                                else
                                {
                                    pSet.Close();
                                    return true;
                                }
                        }
                    }

                }
            }
            pSet.Close();
            return true;
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

        private void dgView_SelectionChanged(object sender, EventArgs e)
        {
            if (dgView.RowCount >= 1)
            {
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                txtCode.Text = dgView.CurrentRow.Cells[0].Value?.ToString();
                txtName.Text = dgView.CurrentRow.Cells[1].Value?.ToString();
                txtMunCode.Text = dgView.CurrentRow.Cells[2].Value?.ToString();
            }
        }
    }
}
