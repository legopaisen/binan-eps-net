using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPSEntities.Connection;
using System.Data;
using Common.AppSettings;
using Modules.Utilities;
using EPSEntities.Entity;
using Common.DataConnector;
using System.Windows.Forms;
using ARCSEntities.Connection;
using ARCSEntities.Entity;
using Modules.SearchAccount;

namespace Modules.Reports
{
    public partial class frmPaymentHist : Form
    {
        public frmPaymentHist()
        {
            InitializeComponent();
        }

        public static ARCSConnectionString dbConnArcs = new ARCSConnectionString();
        private string m_sAn = string.Empty;

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPaymentHist_Load(object sender, EventArgs e)
        {
            arn1.ArnCode.Enabled = true;
        }
        private void ClearAll()
        {
            txtAcct.Text = "";
            txtPayerNm.Text = "";
            txtAddr.Text = "";
            cmbOR.Items.Clear();
            cmbOR.Text = "";
            txtORdt.Text = "";
            txtGrandTotal.Text = "";
            txtMemo.Text = "";
            dgvFees.Rows.Clear();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (btnSearch.Text == "Clear")
            {
                btnSearch.Text = "Search";
                ClearAll();
            }
            else
            {
                m_sAn = arn1.GetAn();
                btnSearch.Text = "Clear";
                OracleResultSet result = new OracleResultSet();
                if (txtAcct.Text == "" && m_sAn == "")
                {
                    //AFM2 20200805 BIN-20-13402 (s)
                    frmSearchAccount frmsearchaccount = new frmSearchAccount();
                    frmsearchaccount.SearchMode = "ACCOUNT";
                    frmsearchaccount.ShowDialog();

                    txtAcct.Text = frmsearchaccount.AcctNo;
                    txtPayerNm.Text = frmsearchaccount.LastName + ", " + frmsearchaccount.FirstName + " " + frmsearchaccount.MI;
                    txtAddr.Text = frmsearchaccount.HouseNo + " " + frmsearchaccount.LotNo + " " + frmsearchaccount.BlkNo + ", " + frmsearchaccount.Brgy + ", " + frmsearchaccount.City + ", " + frmsearchaccount.Province;
                    //AFM2 20200805 BIN-20-13402 (e)

                    //MessageBox.Show("Enter account no.");
                    //return;
                }
                else
                {
                    if (txtAcct.Text != "" && m_sAn != "")
                    {
                        result.Query = "select distinct((account.acct_ln ||', '|| account.acct_fn ||' '|| account.acct_mi)) as account_name, " +
                                                " (account.acct_hse_no ||' '|| account.acct_lot_no ||' '|| account.acct_blk_no ||' '|| account.acct_addr ||' '|| account.acct_brgy ||' '|| account.acct_city ||' '|| account.acct_prov) as addr " +
                                                " from account INNER JOIN payments_info ON payments_info.payer_code = account.acct_code where payments_info.payer_code = '" + txtAcct.Text + "' and payments_info.refno = '" + m_sAn + "'";
                    }
                    else if (m_sAn != "")
                    {
                        result.Query = "select distinct((account.acct_ln ||', '|| account.acct_fn ||' '|| account.acct_mi)) as account_name, " +
                                                " (account.acct_hse_no ||' '|| account.acct_lot_no ||' '|| account.acct_blk_no ||' '|| account.acct_addr ||' '|| account.acct_brgy ||' '|| account.acct_city ||' '|| account.acct_prov) as addr " +
                                                " from account INNER JOIN payments_info ON payments_info.payer_code = account.acct_code where payments_info.refno = '" + m_sAn + "'";
                    }
                    else if (txtAcct.Text.Trim() != "")
                    {
                        result.Query = "select distinct((account.acct_ln ||', '|| account.acct_fn ||' '|| account.acct_mi)) as account_name, " +
                                                " (account.acct_hse_no ||' '|| account.acct_lot_no ||' '|| account.acct_blk_no ||' '|| account.acct_addr ||' '|| account.acct_brgy ||' '|| account.acct_city ||' '|| account.acct_prov) as addr " +
                                                " from account INNER JOIN payments_info ON payments_info.payer_code = account.acct_code where payments_info.payer_code = '" + txtAcct.Text + "'";
                    }
                    else
                        return;

                    if (result.Execute())
                        if (result.Read())
                        {
                            txtPayerNm.Text = result.GetString(0);
                            txtAddr.Text = result.GetString(1);
                        }
                }
                if (txtAcct.Text != "" && m_sAn != "")
                {
                    result.Query = "select distinct or_no, or_date from payments_info where payer_code = '" + txtAcct.Text + "' and refno = '"+ m_sAn +"'";
                }
                else if (m_sAn != "")
                {
                    result.Query = "select distinct or_no, or_date from payments_info where refno = '" + m_sAn + "'";
                }
                else if (txtAcct.Text.Trim() != "")
                {
                    result.Query = "select distinct or_no, or_date from payments_info where payer_code = '" + txtAcct.Text + "'";

                }
                if (result.Execute())
                    while (result.Read())
                    {
                        cmbOR.Items.Add(result.GetString(0));
                    }
            }
        }

        private void cmbOR_SelectedIndexChanged(object sender, EventArgs e)
        {
            OracleResultSet result = new OracleResultSet();
            OracleResultSet result2 = new OracleResultSet();
            string sFeesCode = string.Empty;
            string sFeesCat = string.Empty;
            string sFeesDesc = string.Empty;
            double dFeesDue = 0;
            double dSurch = 0;
            double dFeesAmtDue = 0;
            result.Query = "select distinct or_no, or_date, fees_code, fees_due, fees_surch, fees_amt_due, fees_category, permit_code from payments_info where payer_code = '" + txtAcct.Text + "' and or_no = '"+ cmbOR.Text + "'";
            if (result.Execute())
                while (result.Read())
                {
                    txtORdt.Text = result.GetDateTime(1).ToShortDateString();
                    sFeesCode = result.GetString(2);
                    sFeesCat = result.GetString("fees_category");
                    dFeesDue = result.GetDouble("fees_due");
                    dSurch = result.GetDouble("fees_surch");
                    dFeesAmtDue = result.GetDouble("fees_amt_due");
                    //result2.CreateANGARCS();

                    if (sFeesCat == "MAIN")
                    {
                        result2.Query = $"select fees_desc from subcategories where fees_code = '{sFeesCode}'";
                        if (result2.Execute())
                            if (result2.Read())
                                sFeesDesc = result2.GetString("fees_desc");
                    }
                    else if (sFeesCat == "OTHERS")
                    {
                        result2.Query = $"select fees_desc from other_subcategories where fees_code = '{sFeesCode}'";
                        if (result2.Execute())
                            if (result2.Read())
                                sFeesDesc = result2.GetString("fees_desc");
                    }
                    else if (sFeesCat == "ADDITIONAL")
                    {
                        result2.Query = $"select fees_desc from addl_subcategories where fees_code = '{sFeesCode}'";
                        if (result2.Execute())
                            if (result2.Read())
                                sFeesDesc = result2.GetString("fees_desc");
                    }

                        dgvFees.Rows.Add(sFeesCode, sFeesDesc, string.Format("{0:#,###.00}", dFeesDue), string.Format("{0:#,###.00}", dSurch), string.Format("{0:#,###.00}", dFeesDue));

                }
            result.Query = "select fees_amt_due from payments_info where payer_code = '" + txtAcct.Text + "' and or_no = '" + cmbOR.Text + "'";
            if(result.Execute())
                if(result.Read())
                {
                    txtGrandTotal.Text = string.Format("{0:#,###.00}", result.GetDouble("fees_amt_due"));
                }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }
    }
}
