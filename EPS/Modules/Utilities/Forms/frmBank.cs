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
using Common.DataConnector;

namespace Modules.Utilities.Forms
{
    public partial class frmBank : Form
    {
        public static ConnectionString dbConn = new ConnectionString();
        public string m_sBankCode = string.Empty;
        public string m_sBankID = string.Empty;
        public string m_sTmpBankCode = string.Empty;
        public string m_sTmpBankID = string.Empty;

        public bool isEdit = false;

        public frmBank()
        {
            InitializeComponent();
        }

        private void frmBank_Load(object sender, EventArgs e)
        {
            PopulateBank();
            EnableControl(false);
            if(isEdit == true)
            {
                btnAdd.Visible = true;
                btnEdit.Visible = true;
                btnDelete.Visible = true;
                btnOk.Visible = false;
            }
        }

        private void PopulateBank()
        {
            dgvList.Rows.Clear();
            var result = from a in Utilities.BankList.GetBankList(m_sBankID)
                         select a;
            foreach (var item in result)
            {
                dgvList.Rows.Add(item.BANK_ID, item.BANK_CODE, item.BANK_NM, item.BANK_BRANCH, item.BANK_ADD);
            }
        }

        private void ClearControls()
        {
            txtID.Text = string.Empty;
            txtCode.Text = string.Empty;
            txtAddr.Text = string.Empty;
            txtBranch.Text = string.Empty;
            txtName.Text = string.Empty;
            btnAdd.Text = "Add";
            btnEdit.Text = "Edit";
            btnExit.Text = "Exit";
        }

        private void EnableControl(bool bln)
        {
            grpText.Enabled = bln;
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            m_sTmpBankID = string.Empty;
            try
            {
                m_sTmpBankID = dgvList[0, e.RowIndex].Value.ToString();
                txtID.Text = dgvList[0, e.RowIndex].Value.ToString();
            }
            catch { }
            try
            {
                txtCode.Text = dgvList[1, e.RowIndex].Value.ToString();
            }
            catch { }
            try
            {
                txtName.Text = dgvList[2, e.RowIndex].Value.ToString();
            }
            catch { }
            try
            {
                txtBranch.Text = dgvList[3, e.RowIndex].Value.ToString();
            }
            catch { }
            try
            {
                txtAddr.Text = dgvList[4, e.RowIndex].Value.ToString();
            }
            catch { }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            m_sBankID = m_sTmpBankID;
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if(btnExit.Text == "Exit")
            {
                m_sBankID = "";
                this.Close();
            }
            else
            {
                ClearControls();
                dgvList.Enabled = true;
                PopulateBank();
                EnableControl(false);
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
            }
        }

        private string GetID()
        {
            OracleResultSet res = new OracleResultSet();
            int iID = 0;
            int iLength = 0;
            string sID = string.Empty;
            res.Query = "select nvl(max(bank_id), 0) as bank_id from bank_table";
            int.TryParse(res.ExecuteScalar(), out iID);

            iID += 1;
            sID = iID.ToString();
            iLength = sID.Length;

            switch (iLength)
            {
                case 1:
                    {
                        sID = "000" + sID;
                        break;
                    }
                case 2:
                    {
                        sID = "00" + sID;
                        break;
                    }
                case 3:
                    {
                        sID = "0" + sID;
                        break;
                    }
                case 4:
                    {
                        sID = sID;
                        break;
                    }

            }
            return sID;
        }

        private bool ValidateEntry()
        {
            if (string.IsNullOrEmpty(txtID.Text.Trim()) || string.IsNullOrEmpty(txtCode.Text.Trim()) || string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                MessageBox.Show("Incomplete details!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else
                return true;
        }

        private bool ValidateUsage()
        {
            OracleResultSet res = new OracleResultSet();
            int cnt = 0;
            res.Query = $"select count(*) from check_tbl where bank_code = '{txtID.Text.Trim()}'";
            int.TryParse(res.ExecuteScalar(), out cnt);
            if (cnt != 0)
            {
                MessageBox.Show("Cannot Delete. Bank already used!","",MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return true;
            }
            else 
                return false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(btnAdd.Text == "Add")
            {
                EnableControl(true);
                txtID.Text = string.Empty;
                txtCode.Text = string.Empty;
                txtAddr.Text = string.Empty;
                txtBranch.Text = string.Empty;
                txtName.Text = string.Empty;
                btnAdd.Text = "Save";
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                btnExit.Text = "Cancel";
                dgvList.Enabled = false;
            }
            else
            {
                if (!ValidateEntry())
                    return;
                if(MessageBox.Show("Save Record?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string sID = string.Empty;
                    sID = GetID();
                    OracleResultSet res = new OracleResultSet();
                    res.Query = $"INSERT INTO BANK_TABLE VALUES('{txtCode.Text.Trim()}', '{txtName.Text.Trim()}', '{txtBranch.Text.Trim()}', '{txtAddr.Text.Trim()}', '{sID}')";
                    if(res.ExecuteNonQuery() == 0)
                    { }
                    res.Close();
                    MessageBox.Show("Record Saved!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearControls();
                    dgvList.Enabled = true;
                    PopulateBank();
                    EnableControl(false);
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCode.Text.Trim()))
            {
                MessageBox.Show("Incomplete details!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (ValidateUsage())
                return;
            if (MessageBox.Show("Delete Record?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                OracleResultSet res = new OracleResultSet();
                res.Query = $"DELETE FROM BANK_TABLE WHERE BANK_ID ='{txtID.Text.Trim()}'";
                if (res.ExecuteNonQuery() == 0)
                { }
                res.Close();
                MessageBox.Show("Record Deleted!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearControls();
                PopulateBank();
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEdit.Text == "Edit")
            {
                EnableControl(true);
                btnEdit.Text = "Update";
                btnAdd.Enabled = false;
                btnDelete.Enabled = false;
                btnExit.Text = "Cancel";
                dgvList.Enabled = false;
            }
            else
            {
                if (!ValidateEntry())
                    return;
                if (MessageBox.Show("Update Record?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    OracleResultSet res = new OracleResultSet();
                    res.Query = $"UPDATE BANK_TABLE SET BANK_CODE = '{txtCode.Text.Trim()}', BANK_NM = '{txtName.Text.Trim()}', BANK_BRANCH = '{txtBranch.Text.Trim()}', BANK_ADD = '{txtAddr.Text.Trim()}' WHERE BANK_ID = '{txtID.Text.Trim()}'";
                    if (res.ExecuteNonQuery() == 0)
                    { }
                    res.Close();
                    MessageBox.Show("Record Updated!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearControls();
                    PopulateBank();
                    EnableControl(false);
                    btnDelete.Enabled = true;
                    btnAdd.Enabled = true;
                    dgvList.Enabled = true;
                }
            }
        }
    }
}
