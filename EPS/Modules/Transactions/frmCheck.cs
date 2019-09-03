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

namespace Modules.Transactions
{
    public partial class frmCheck : Form
    {
        public static ConnectionString dbConn = new ConnectionString();
        public string m_sOrNo = string.Empty;
        public string m_sAmount = string.Empty;
            
        public frmCheck()
        {
            InitializeComponent();
        }

        private void frmCheck_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btnSearch, "Search Bank");
            toolTip2.SetToolTip(btnAdd, "Add Bank");

            if (!string.IsNullOrEmpty(txtCheckNo.Text.ToString().Trim()))
            {
                string sQuery = string.Empty;
                var result = (dynamic)null;
                var db = new EPSConnection(dbConn);

                sQuery = $"select * from check_tbl where chk_no = '{txtCheckNo.Text.ToString().Trim()}' and chk_ref = '{m_sOrNo}'";
                result = db.Database.SqlQuery<CHECK_TBL>(sQuery).ToList();

                foreach (var item in result)
                {
                    dtpCheckDate.Text = item.CHK_DATE;
                    txtBankCode.Text = item.BANK_CODE;
                    txtAcctNo.Text = item.ACCT_NO;
                    txtLastName.Text = item.ACCT_LN;
                    txtFirstName.Text = item.ACCT_FN;
                    txtMI.Text = item.ACCT_MI;
                    txtCheckAmt.Text = string.Format("{0:#,###.00}",item.CHK_AMT);
                    txtDebit.Text = string.Format("{0:#,###.00}", item.DEBIT_CREDIT);
                }
            }
        }

        private void ClearBankInfo()
        {
            txtBank.Text = string.Empty;
            txtBankBranch.Text = string.Empty;
            txtBankAdd.Text = string.Empty;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Add")
            {
                ClearBankInfo();
                txtBankCode.Focus();
                txtBank.ReadOnly = false;
                txtBankBranch.ReadOnly = false;
                txtBankAdd.ReadOnly = false;
                btnAdd.Text = "Save";
                grpBankInfo.Enabled = false;
                grpCheckInfo.Enabled = false;
            }
            else
            {
                SaveBank();
                grpBankInfo.Enabled = true;
                grpCheckInfo.Enabled = true;
                txtBank.ReadOnly = true;
                txtBankBranch.ReadOnly = true;
                txtBankAdd.ReadOnly = true;
            }
        }

        private void SaveBank()
        {
            if(string.IsNullOrEmpty(txtBankCode.Text.ToString()) ||
                string.IsNullOrEmpty(txtBank.Text.ToString()) ||
                string.IsNullOrEmpty(txtBankAdd.Text.ToString()) ||
                string.IsNullOrEmpty(txtBankBranch.Text.ToString()))
            {
                MessageBox.Show("Complete bank information","Bank",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                return;
            }

            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;
            int iCnt = 0;

            sQuery = $"select count(*) from bank_table where bank_code = '{txtBankCode.Text.ToString().Trim()}'";
            iCnt = db.Database.SqlQuery<Int32>(sQuery).SingleOrDefault();

            if (iCnt == 0)
            {
                sQuery = $"insert into bank_table values (:1,:2,:3,:4)";
                db.Database.ExecuteSqlCommand(sQuery,
                    new OracleParameter(":1", txtBankCode.Text.ToString()),
                    new OracleParameter(":1", txtBank.Text.ToString()),
                    new OracleParameter(":1", txtBankBranch.Text.ToString()),
                    new OracleParameter(":1", txtBankAdd.Text.ToString()));

                MessageBox.Show("Bank information saved", "Bank", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnAdd.Text = "Add";
            }
            else
            {
                MessageBox.Show("Bank code already exists", "Bank", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBankCode.Text.ToString()))
            {
                frmBank bank = new frmBank();
                bank.ShowDialog();
                txtBankCode.Text = bank.m_sBankCode;
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

            if(string.IsNullOrEmpty(txtBank.Text.ToString()))
            {
                MessageBox.Show("Bank information not found","Bank",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtCheckNo.Text.ToString().Trim()))
            {
                MessageBox.Show("Please enter check no.","Check",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                return;
            }

            if (string.IsNullOrEmpty(txtBankCode.Text.ToString().Trim()))
            {
                MessageBox.Show("Please enter bank code", "Check", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (string.IsNullOrEmpty(txtAcctNo.Text.ToString().Trim()))
            {
                MessageBox.Show("Please enter account no.", "Check", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Save")
            {
                btnAdd.Text = "Add";
                grpBankInfo.Enabled = true;
                grpCheckInfo.Enabled = true;
                txtBank.ReadOnly = true;
                txtBankBranch.ReadOnly = true;
                txtBankAdd.ReadOnly = true;
            }
            else
            {
                txtCheckNo.Text = string.Empty;
                txtBankCode.Text = string.Empty;
                txtAcctNo.Text = string.Empty;

                this.Close();
            }
        }

        private void txtCheckAmt_Leave(object sender, EventArgs e)
        {
            double dAmtPay = 0, dAmtDue = 0;
            double.TryParse(txtCheckAmt.Text.ToString(), out dAmtPay);
            txtCheckAmt.Text = string.Format("{0:#,###.00}", dAmtPay);
            double.TryParse(m_sAmount, out dAmtDue);

            dAmtPay = dAmtPay - dAmtDue;
            txtDebit.Text = string.Format("{0:#,###.00}", dAmtPay);

        }
    }
}
