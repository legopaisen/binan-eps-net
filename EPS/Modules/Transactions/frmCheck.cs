using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modules.Utilities;
using Modules.Utilities.Forms;
using EPSEntities.Connection;
using Oracle.ManagedDataAccess.Client;
using EPSEntities.Entity;
using Common.DataConnector;

namespace Modules.Transactions
{
    public partial class frmCheck : Form
    {
        public static ConnectionString dbConn = new ConnectionString();
        public string m_sOrNo = string.Empty;
        public string m_sAmount = string.Empty;
        private string sChkType = string.Empty;
        public string m_sCashAmount = string.Empty;
        public DateTime dtOrDate;
        public string m_sFormType = string.Empty;
        public string m_sTeller = string.Empty;
        public string m_sPayType = string.Empty;
        public string m_sChkAmt = string.Empty;

        public frmCheck()
        {
            InitializeComponent();
        }

        private void frmCheck_Load(object sender, EventArgs e)
        {
            EnableControls(false);
            toolTip1.SetToolTip(btnSearch, "Search Bank");
            toolTip2.SetToolTip(btnAdd, "Add Bank");

            //if (!string.IsNullOrEmpty(txtCheckNo.Text.ToString().Trim()))
            //{
            //string sQuery = string.Empty;
            //var result = (dynamic)null;
            //var db = new EPSConnection(dbConn);

            //sQuery = $"select * from chk_tbl_temp where chk_no = '{txtCheckNo.Text.ToString().Trim()}' and or_no = '{m_sOrNo}'";
            //result = db.Database.SqlQuery<CHECK_TBL>(sQuery).ToList();

            //foreach (var item in result)
            //{
            //    dtpCheckDate.Text = item.CHK_DATE;
            //    txtBankCode.Text = item.BANK_CODE;
            //    txtCheckAmt.Text = string.Format("{0:#,###.00}",item.CHK_AMT);
            //    txtDebit.Text = string.Format("{0:#,###.00}", item.DEBIT_CREDIT);
            //}
            //}

            PopulateCheck();
            txtTotTaxDue.Text = m_sAmount;
            ComputeCheck();
        }

        private void PopulateCheck()
        {
            dgvCheck.Rows.Clear();
            OracleResultSet res = new OracleResultSet();
            res.Query = $"select * from chk_tbl_temp where or_no = '{m_sOrNo}'";
            if (res.Execute())
                while (res.Read())
                {
                    string sChkNo = res.GetString("chk_no");
                    double dChkAmt = res.GetDouble("chk_amt");
                    string sBankID = res.GetString("bank_code");
                    string sBank = GetBank(sBankID);
                    string sBankBranch = GetBankBranch(sBankID);
                    string sChkType = res.GetString("chk_type");

                    dgvCheck.Rows.Add(sChkNo, string.Format("{0:#,##0.00}", dChkAmt), sBank, sBankBranch, sBankID, sChkType);
                }
            res.Close();
        }

        private void ComputeCheck()
        {
            OracleResultSet res = new OracleResultSet();
            double dChkTotAmt = 0;
            res.Query = $"select nvl(sum(chk_amt),0) as chk_amt from chk_tbl_temp where or_no = '{m_sOrNo}'";
            if(res.Execute())
                if(res.Read())
                {
                    dChkTotAmt = res.GetDouble("chk_amt");
                }
            res.Close();

            txtTotChkAmt.Text = string.Format("{0:#,##0.00}", dChkTotAmt);
        }

        private string GetBank(string sCode)
        {
            OracleResultSet res = new OracleResultSet();
            res.Query = $"select * from BANK_TABLE where bank_id = '{sCode}'";
            if (res.Execute())
                if (res.Read())
                {
                    return res.GetString("bank_nm");
                }
                else
                    return null;
            else
                return null;
        }
        private string GetBankBranch(string sCode)
        {
            OracleResultSet res = new OracleResultSet();
            res.Query = $"select * from BANK_TABLE where bank_id = '{sCode}'";
            if (res.Execute())
                if (res.Read())
                {
                    return res.GetString("bank_branch");
                }
                else
                    return null;
            else
                return null;
        }

        private void EnableControls(bool bln)
        {
            //grpBankInfo.Enabled = bln;
            grpCheckInfo.Enabled = bln;
            groupBox1.Enabled = bln;
            dgvCheck.Enabled = !bln;
            grpChkType.Enabled = bln;
        }

        private void ClearBankInfo()
        {
            txtBank.Text = string.Empty;
            txtBankBranch.Text = string.Empty;
            txtBankAdd.Text = string.Empty;
        }

        private void ClearControls()
        {
            rdoCashier.Checked = false;
            rdoManager.Checked = false;
            rdoPersonal.Checked = false;
            rdoPostal.Checked = false;
            txtBankCode.Text = string.Empty;
            txtBank.Text = string.Empty;
            txtBankBranch.Text = string.Empty;
            txtBankAdd.Text = string.Empty;
            txtCheckNo.Text = string.Empty;
            txtCheckAmt.Text = string.Empty;
            txtDebit.Text = string.Empty;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Add")
            {
                ClearBankInfo();
                txtBankCode.Focus();
                txtBank.ReadOnly = true;
                txtBankBranch.ReadOnly = true;
                txtBankAdd.ReadOnly = true;
                btnAdd.Text = "Save";
                btnSave.Text = "Cancel";
                btnDelete.Enabled = false;
                EnableControls(true);
                grpCheckInfo.Enabled = false;
                ClearControls();
            }
            else
            {
                if (!SaveCheck())
                    return;
                //grpTotal.Enabled = true;
                EnableControls(false);
                btnAdd.Text = "Add";
                ClearControls();
                btnDelete.Enabled = true;
                btnSave.Text = "Ok";
                //txtBank.ReadOnly = true;
                //txtBankBranch.ReadOnly = true;
                //txtBankAdd.ReadOnly = true;
            }
        }

        private bool SaveCheck()
        {
            if (rdoCashier.Checked == false && rdoManager.Checked == false && rdoPersonal.Checked == false && rdoPostal.Checked == false)
            {
                MessageBox.Show("Select check type!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (string.IsNullOrEmpty(txtBankCode.Text.ToString()) ||
                string.IsNullOrEmpty(txtBank.Text.ToString()) ||
                string.IsNullOrEmpty(txtBankAdd.Text.ToString()) ||
                string.IsNullOrEmpty(txtBankBranch.Text.ToString()))
            {
                MessageBox.Show("Complete bank information","Bank",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                return false;
            }


            double dChkAmt = 0;
            double dAmt = 0;
            double dTotChk = 0;
            double.TryParse(txtCheckAmt.Text.Trim(), out dChkAmt);
            double.TryParse(m_sAmount, out dAmt);
            double.TryParse(txtTotChkAmt.Text.Trim(), out dTotChk);
            double dTotTemp = dTotChk + dChkAmt;

            if(dChkAmt == 0)
            {
                MessageBox.Show("Amount cannot be zero!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (dTotChk > 0 && dAmt < dTotTemp)
            {
                MessageBox.Show("Total check amount can't be higher than amount due!/nExcess of checks is temporarily not allowed!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }


            OracleResultSet res = new OracleResultSet();
            res.Query = "INSERT INTO CHK_TBL_TEMP VALUES(";
            res.Query += $"'{txtCheckNo.Text.Trim()}', ";
            res.Query += $"to_date('{dtpCheckDate.Value.ToShortDateString()}', 'MM/dd/yyyy'), ";
            res.Query += $"'{sChkType}', ";
            res.Query += $"{dChkAmt}, ";
            res.Query += $"0, ";
            res.Query += $"'{txtBankCode.Text.Trim()}', ";
            res.Query += $"'{m_sOrNo}', ";
            res.Query += $"to_date('{dtOrDate.ToShortDateString()}', 'MM/dd/yyyy'), ";
            res.Query += $"'{m_sFormType}', ";
            res.Query += $"'{m_sTeller}', ";
            res.Query += $"to_date('{dtAccepted.Value.ToShortDateString()}', 'MM/dd/yyyy'))";
            if(res.ExecuteNonQuery() == 0)
            { }
            res.Close();

            dgvCheck.Rows.Add(txtCheckNo.Text.Trim(), txtCheckAmt.Text.Trim(), txtBank.Text.Trim(), txtBankBranch.Text.Trim(), txtBankCode.Text.Trim(), sChkType);
            ComputeCheck();
            return true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBankCode.Text.ToString()))
            {
                frmBank bank = new frmBank();
                bank.ShowDialog();
                txtBankCode.Text = bank.m_sBankID;
            }

            DisplayBankInfo();
        }

        private void DisplayBankInfo()
        {
            ClearBankInfo();

            var result = from a in Utilities.BankList.GetBankList(txtBankCode.Text.ToString().Trim())
                         select a;
            foreach (var item in result)
            {
                txtBank.Text = item.BANK_NM;
                txtBankAdd.Text = item.BANK_ADD;
                txtBankBranch.Text = item.BANK_BRANCH;
            }

            if (string.IsNullOrEmpty(txtBank.Text.ToString()))
            {
                MessageBox.Show("Bank information not found", "Bank", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else
                grpCheckInfo.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Save")
            {
                btnAdd.Text = "Add";
                grpCheckInfo.Enabled = true;
                txtBank.ReadOnly = true;
                txtBankBranch.ReadOnly = true;
                txtBankAdd.ReadOnly = true;
                EnableControls(false);
                btnDelete.Enabled = true;
                btnSave.Enabled = true;
                return;
            }

            //if (string.IsNullOrEmpty(txtTotChkAmt.Text.ToString().Trim()) || dgvCheck.Rows.Count == 0)
            //{
            //    MessageBox.Show("No check added!", "Check", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return;
            //}

            string sNewCashAmt = string.Empty;
            if(m_sPayType == "CC")
            {
                double dTotChk = 0;
                double dCashAmt = 0;
                double dTotTaxDue = 0;
                double.TryParse(txtTotChkAmt.Text.Trim(), out dTotChk);
                double.TryParse(txtTotTaxDue.Text.Trim(), out dTotTaxDue);

                dCashAmt = dTotTaxDue - dTotChk;

                if (dTotChk >= dTotTaxDue && dgvCheck.Rows.Count > 0)
                {
                    MessageBox.Show($"Total check amount can't be higher or equal to amount due! You are using cash/check payment!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                MessageBox.Show($"Cash amount will be '{dCashAmt.ToString("#,##0.00")}'");

                OracleResultSet res = new OracleResultSet();
                res.Query = $"UPDATE CHK_TBL_TEMP SET CASH_AMT = {dCashAmt} WHERE OR_NO = '{m_sOrNo}' and rownum = 1";
                if(res.ExecuteNonQuery() == 0)
                { }
                sNewCashAmt = dCashAmt.ToString("#,##0.00");
            }
            else if  (m_sPayType == "CQ")
            {
                double dTotChk = 0;
                double dTotTaxDue = 0;
                double dCashAmt = 0;
                double.TryParse(txtTotChkAmt.Text.Trim(), out dTotChk);
                double.TryParse(txtTotTaxDue.Text.Trim(), out dTotTaxDue);
                dCashAmt = dTotTaxDue - dTotChk;

                if (dTotChk != dTotTaxDue && dgvCheck.Rows.Count > 0)
                {
                    MessageBox.Show($"Total cash amount should be '{dCashAmt.ToString("#,##0.00")}'. To choose Cash and Check payment, please delete this check info, then select both cash and check button.", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }        
            }

            m_sChkAmt = txtTotChkAmt.Text.Trim();
            m_sCashAmount = sNewCashAmt;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Save")
            {
                btnAdd.Text = "Add";
                grpCheckInfo.Enabled = true;
                txtBank.ReadOnly = true;
                txtBankBranch.ReadOnly = true;
                txtBankAdd.ReadOnly = true;
                EnableControls(false);
                btnDelete.Enabled = true;
                btnSave.Enabled = true;
            }
            else
            {
                txtCheckNo.Text = string.Empty;
                txtBankCode.Text = string.Empty;

                this.Close();
            }
        }

        private void txtCheckAmt_Leave(object sender, EventArgs e)
        {
            if (!ValidateCheck())
                return;


            double dAmtPay = 0, dAmtDue = 0;
            double.TryParse(txtCheckAmt.Text.ToString(), out dAmtPay);
            txtCheckAmt.Text = string.Format("{0:#,##0.00}", dAmtPay);
            double.TryParse(m_sAmount, out dAmtDue);

            dAmtPay = dAmtPay - dAmtDue;
            txtDebit.Text = string.Format("{0:#,##0.00}", dAmtPay);

        }

        private bool ValidateCheck()
        {
            OracleResultSet res = new OracleResultSet();
            int cnt = 0;
            res.Query = $"select * from chk_tbl where chk_no = '{txtCheckNo.Text.Trim()}' and bank_code = '{txtBankCode.Text.Trim()}'";
            int.TryParse(res.ExecuteScalar(), out cnt);
            if (cnt != 0)
            {
                MessageBox.Show("Check no. already/currently used!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            res.Query = $"select * from chk_tbl_temp where chk_no = '{txtCheckNo.Text.Trim()}' and bank_code = '{txtBankCode.Text.Trim()}'";
            int.TryParse(res.ExecuteScalar(), out cnt);
            if (cnt != 0)
            {
                MessageBox.Show("Check no. already/currently used!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else
                return true;
        }

        private void btnSearchChk_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtBankCode.Text.Trim()))
            {
                MessageBox.Show("Select bank first!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if(!ValidateCheck())
            {
                return;
            }
        }

        private void rdoCashier_CheckedChanged(object sender, EventArgs e)
        {
            if(rdoCashier.Checked == true)
            {
                sChkType = "CC";
            }
        }

        private void rdoPersonal_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoPersonal.Checked == true)
            {
                sChkType = "PC";
            }
        }

        private void rdoManager_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoManager.Checked == true)
            {
                sChkType = "MC";
            }
        }

        private void rdoPostal_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoPostal.Checked == true)
            {
                sChkType = "MO";
            }
        }

        private void dgvCheck_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string sType = dgvCheck[5, e.RowIndex].Value.ToString();
                if (sType == "CC")
                    rdoCashier.Checked = true;
                else if (sType == "PC")
                    rdoPersonal.Checked = true;
                else if (sType == "MC")
                    rdoManager.Checked = true;
                else if (sType == "MO")
                    rdoPostal.Checked = true;
            }
            catch { }
            try
            {
                txtBankCode.Text = dgvCheck[4, e.RowIndex].Value.ToString();
                groupBox1.Enabled = true;
                btnSearch.PerformClick();
                groupBox1.Enabled = false;
                grpCheckInfo.Enabled = false;

            }
            catch { }
            try
            {
                txtCheckNo.Text = dgvCheck[0, e.RowIndex].Value.ToString();
            }
            catch { }
            try
            {
                txtCheckAmt.Text = dgvCheck[1, e.RowIndex].Value.ToString();
            }
            catch { }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtBankCode.Text) && string.IsNullOrEmpty(txtCheckNo.Text))
            {
                MessageBox.Show("Select cheque first!","", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (MessageBox.Show("Delete Check Info?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                OracleResultSet res = new OracleResultSet();
                res.Query = $"DELETE FROM CHK_TBL_TEMP WHERE CHK_NO = '{txtCheckNo.Text.Trim()}' AND BANK_CODE = '{txtBankCode.Text.Trim()}' AND OR_NO = '{m_sOrNo}'";
                if (res.ExecuteNonQuery() == 0)
                { }
                res.Close();
                ClearControls();
                PopulateCheck();
                ComputeCheck();
            }

        }

        private void frmCheck_FormClosing(object sender, FormClosingEventArgs e)
        {
          
        }
    }
}

