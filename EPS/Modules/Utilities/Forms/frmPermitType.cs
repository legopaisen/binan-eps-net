using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EPSEntities.Connection;
using Oracle.ManagedDataAccess.Client;
using Common.StringUtilities;

namespace Modules.Utilities.Forms
{
    public partial class frmPermitType : Form
    {
        private static ConnectionString dbConn = new ConnectionString();
        public frmPermitType()
        {
            InitializeComponent();
        }

        private void frmPermitType_Load(object sender, EventArgs e)
        {
            UpdateList();
            EnableControls(false);
        }
        private void EnableControls(bool bEnable)
        {
            chkOther.Enabled = bEnable;
            btnSearch.Enabled = bEnable;
            //txtCode.ReadOnly = !bEnable;
            txtType.ReadOnly = !bEnable;
            txtAppCode.ReadOnly = !bEnable;
            //txtFeesCode.ReadOnly = !bEnable;
        }
        private void ClearControls()
        {
            txtCode.Text = "";
            txtType.Text = "";
            txtFeesCode.Text = "";
            txtAppCode.Text = "";
            chkOther.Checked = false;
        }
        private void UpdateList()
        {
            int iCnt = 0;
            PermitList permit = new PermitList(null);
            iCnt = permit.PermitLst.Count;

            for (int i = 0; i < iCnt; i++)
            {
                dgvList.Rows.Add(permit.PermitLst[i].PermitCode, permit.PermitLst[i].PermitDesc, permit.PermitLst[i].PermitFeesCode,permit.PermitLst[i].PermitAppCode, permit.PermitLst[i].PermitOtherType);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;

            if (btnAdd.Text == "Add")
            {
                EnableControls(true);
                ClearControls();
                dgvList.Enabled = false;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                btnAdd.Text = "Save";
                btnExit.Text = "Cancel";
                Permit permit = new Permit();
                permit.CreatePermitCode();
                txtCode.Text = permit.PermitCode;
                txtType.Focus();

                sQuery = "delete from permit_fees_tbl_tmp";
                db.Database.ExecuteSqlCommand(sQuery);
            }
            else
            {
                if (string.IsNullOrEmpty(txtType.Text.ToString().Trim()))
                {
                    MessageBox.Show("Permit Type field requires an entry", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtType.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtAppCode.Text.ToString().Trim()))
                {
                    MessageBox.Show("Application no. code field requires an entry", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtAppCode.Focus();
                    return;
                }

                PermitList permit = new PermitList(null);
                if(!string.IsNullOrEmpty(permit.GetPermitCode(txtType.Text.ToString().Trim())))
                {
                    MessageBox.Show("Category: " + txtType.Text.ToString().Trim() + " already exists.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtType.Focus();
                    return;
                }
                else if(!string.IsNullOrEmpty(permit.GetFeesCode(txtFeesCode.Text.ToString().Trim())))
                {
                    MessageBox.Show("Fee Code: " + txtFeesCode.Text.ToString().Trim() + " already exists.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtFeesCode.Focus();
                        return;
                }
                
                int iCnt = 0;
                sQuery = $"select count(*) from permit_fees_tbl_tmp where permit_code = '{txtCode.Text.ToString().Trim()}'";
                iCnt = db.Database.SqlQuery<Int32>(sQuery).SingleOrDefault();

                if(iCnt == 0)
                {
                    MessageBox.Show("Fees Code field requires an entry", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                iCnt = 0;
                sQuery = $"select count(*) from permit_tbl where app_code = '{txtAppCode.Text.ToString().Trim()}'";
                if (iCnt > 0)
                {
                    MessageBox.Show("Application no. code already exists.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                string sOther = string.Empty;
                if (chkOther.Checked)
                    sOther = "TRUE";
                else
                    sOther = "FALSE";

                if (MessageBox.Show("Are you sure you want to Save this record?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sQuery = $"insert into permit_tbl values (:1,:2,:3,:4,:5)";
                    db.Database.ExecuteSqlCommand(sQuery,
                        new OracleParameter(":1", txtCode.Text.ToString().Trim()),
                        new OracleParameter(":2", StringUtilities.HandleApostrophe(txtType.Text.ToString().Trim())),
                        new OracleParameter(":3", StringUtilities.HandleApostrophe(txtFeesCode.Text.ToString().Trim())),
                        new OracleParameter(":4", sOther),
                        new OracleParameter(":5", StringUtilities.HandleApostrophe(txtAppCode.Text.ToString().Trim())));


                    sQuery = $"insert into permit_fees_tbl select * from permit_fees_tbl_tmp where permit_code = '{txtCode.Text.ToString().Trim()}'";
                    db.Database.ExecuteSqlCommand(sQuery);

                    sQuery = "delete from permit_fees_tbl_tmp";
                    db.Database.ExecuteSqlCommand(sQuery);

                    string sObject = string.Empty;
                    sObject = "Code:" + txtCode.Text.ToString();

                    if (Utilities.AuditTrail.InsertTrail("SPT-A", "permi_tbl", sObject) == 0)
                    {
                        MessageBox.Show("Failed to insert audit trail.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    UpdateList();

                    EnableControls(false);
                    dgvList.Enabled = true;
                    dgvList.ReadOnly = true;
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;
                    btnAdd.Text = "Add";
                    btnExit.Text = "Exit";
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            frmFeeSearch form = new frmFeeSearch();
            form.m_sPermitCode = txtCode.Text.ToString();
            form.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (btnExit.Text == "Cancel")
            {
                btnAdd.Text = "Add";
                btnEdit.Text = "Edit";
                btnExit.Text = "Exit";
                EnableControls(false);
                btnAdd.Enabled = true;
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
            }
            else
                this.Close();
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string sOtherType = string.Empty;
            ClearControls();
            try
            {
                txtCode.Text = dgvList[0, e.RowIndex].Value.ToString();
                txtType.Text = dgvList[1, e.RowIndex].Value.ToString();
                txtAppCode.Text = dgvList[3, e.RowIndex].Value.ToString();
                sOtherType = dgvList[4, e.RowIndex].Value.ToString();
                if (sOtherType == "TRUE")
                    chkOther.Checked = true;
                else
                    chkOther.Checked = false;
            }
            catch { }

            try
            {
                txtFeesCode.Text = dgvList[2, e.RowIndex].Value.ToString();
            }
            catch { }
        }
    }
}
