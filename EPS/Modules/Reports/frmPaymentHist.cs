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

namespace Modules.Reports
{
    public partial class frmPaymentHist : Form
    {
        public frmPaymentHist()
        {
            InitializeComponent();
        }

        public static ARCSConnectionString dbConnArcs = new ARCSConnectionString();

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPaymentHist_Load(object sender, EventArgs e)
        {

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
                if(txtAcct.Text == "")
                {
                    MessageBox.Show("Enter account no.");
                    return;
                }
                btnSearch.Text = "Clear";
                OracleResultSet result = new OracleResultSet();
                result.Query = "select distinct((account.acct_ln ||', '|| account.acct_fn ||' '|| account.acct_mi)) as account_name, " +
                    " (account.acct_hse_no ||' '|| account.acct_lot_no ||' '|| account.acct_blk_no ||' '|| account.acct_addr ||' '|| account.acct_brgy ||' '|| account.acct_city ||' '|| account.acct_prov) as addr " +
                    " from account INNER JOIN mrs_payments ON mrs_payments.acct_code = account.acct_code where mrs_payments.acct_code = '" + txtAcct.Text + "'";
                if (result.Execute())
                    if (result.Read())
                    {
                        txtPayerNm.Text = result.GetString(0);
                        txtAddr.Text = result.GetString(1);
                    }

                result.Query = "select distinct or_no, or_date from mrs_payments where acct_code = '" + txtAcct.Text + "'";
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
            result.Query = "select distinct or_no, or_date, fees_code, fees_due, sum(fees_due) as amount from mrs_payments where acct_code = '" + txtAcct.Text + "' and or_no = '"+ cmbOR.Text + "' group by or_no, or_date, fees_code, fees_due";
            if (result.Execute())
                while (result.Read())
                {
                    txtORdt.Text = result.GetDateTime(1).ToShortDateString();
                    sFeesCode = result.GetString(2);

                    result2.CreateANGARCS();
                    result2.Query = "select fees_code, fees_desc from eps_major_fees where fees_code = '" + sFeesCode + "'";
                    if (result2.Execute())
                        if (result2.Read())
                        {
                            dgvFees.Rows.Add(result2.GetString(0), result2.GetString(1), string.Format("{0:#,###.00}", result.GetDouble("fees_due")), 0.00, string.Format("{0:#,###.00}", result.GetDouble("amount")));
                        }
                }
            result.Query = "select sum(amount) as GRAND_TOTAL from (select distinct or_no, or_date, fees_code, fees_due, sum(fees_due) as amount from mrs_payments where acct_code = '" + txtAcct.Text + "' and or_no = '" + cmbOR.Text + "' group by or_no, or_date, fees_code, fees_due)";
            if(result.Execute())
                if(result.Read())
                {
                    txtGrandTotal.Text = string.Format("{0:#,###.00}", result.GetDouble("GRAND_TOTAL"));
                }
        }
    }
}
