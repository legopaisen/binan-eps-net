using Common.AppSettings;
using Common.DataConnector;
using Modules.SearchAccount;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// AFM 20211125 Requested by Binan as per RJ - DOLE billing
namespace Modules.Billing
{
    public partial class frmDOLEBilling : Form
    {
        public frmDOLEBilling()
        {
            InitializeComponent();
        }

        private string m_sBillNo = string.Empty;

        private string m_sFeesCode = string.Empty;

        private void frmDOLEBilling_Load(object sender, EventArgs e)
        {
            ClearControls();
            OracleResultSet res = new OracleResultSet();
            res.Query = "select fees_code from SUBCATEGORIES where fees_desc = 'DOLE' and fees_term <> 'SUBCATEGORY'"; // get fees code of DOLE
            if (res.Execute())
                if (res.Read())
                    m_sFeesCode = res.GetString(0);
        }

        private void ClearControls()
        {
            foreach (Control c in gbInfo.Controls)
            {
                if (c is TextBox)
                {
                    c.Text = "";
                }
            }
            foreach (Control c in gbAmount.Controls)
            {
                if (c is TextBox)
                {
                    c.Text = "";
                }
            }

            txtBillNo.Text = "";
            m_sBillNo = "";

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        private void EnableControls(bool bln)
        {
            txtBillNo.Enabled = bln;
            btnSearch.Enabled = bln;
            btnPrint.Enabled = !bln;
            btnBill.Enabled = bln;

            gbInfo.Enabled = bln;
            gbAmount.Enabled = bln;
        }

        private bool ValidateBilling()
        {
            OracleResultSet res = new OracleResultSet();
            int cnt = 0;
            res.Query = $"select count(*) from billing_dole where bill_no = '{txtBillNo.Text.Trim()}'";
            int.TryParse(res.ExecuteScalar(), out cnt);
            if (cnt > 0)
                return true;
            else
                return false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBillNo.Text))
            {
                frmSearchDOLE form = new frmSearchDOLE();
                form.ShowDialog();
                m_sBillNo = form.BillNo;
                txtBillNo.Text = m_sBillNo;
            }
            else
            {
                if (!ValidateBilling())
                {
                    MessageBox.Show("No record found!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                    m_sBillNo = txtBillNo.Text.Trim();
            }
            if (!string.IsNullOrEmpty(m_sBillNo))
            {
                PopulateControls();
                EnableControls(false);
            }
            
        }

        private void PopulateControls()
        {
            OracleResultSet res = new OracleResultSet();
            string sApplicantNo = string.Empty;
            res.Query = $"select applicant_no from billing_dole where bill_no = '{m_sBillNo}'";
            if (res.Execute())
                if (res.Read())
                    sApplicantNo = res.GetString(0);
            res.Close();

            res.Query = $"select * from dole_applicant where applicant_no = '{sApplicantNo}'";
            if(res.Execute())
                while(res.Read())
                {
                    txtApplicant.Text = res.GetString("applicant_name");
                    txtAddress.Text = res.GetString("address");
                    txtCategory.Text = res.GetString("category_type");
                    txtStoreys.Text = res.GetString("no_storeys");
                    txtArea.Text = res.GetDouble("flr_area").ToString();
                    txtConstCost.Text = res.GetDouble("total_cost").ToString();
                }
            res.Close();

            res.Query = $"select * from bill_dole_info where bill_no = '{m_sBillNo}'";
            if(res.Execute())
                while(res.Read())
                {
                    txtLineGrade.Text = res.GetDouble("line_grade").ToString("#,##0.00");
                    txtBuilding.Text = res.GetDouble("BUILDING").ToString("#,##0.00");
                    txtPlumbing.Text = res.GetDouble("plumbing").ToString("#,##0.00");
                    txtWiring.Text = res.GetDouble("electrical_wiring").ToString("#,##0.00");
                    txtMech.Text = res.GetDouble("mechanical").ToString("#,##0.00");
                    txtOccu.Text = res.GetDouble("cert_occupancy").ToString("#,##0.00");
                    txtFencing.Text = res.GetDouble("fencing").ToString("#,##0.00");
                    txtSign.Text = res.GetDouble("sign").ToString("#,##0.00");
                    txtAnnual.Text = res.GetDouble("annual_insp").ToString("#,##0.00");
                    txtCivil.Text = res.GetDouble("civil_struc").ToString("#,##0.00");
                    txtElec.Text = res.GetDouble("electrical").ToString("#,##0.00");
                    txtMech2.Text = res.GetDouble("mechanical2").ToString("#,##0.00");
                    txtPlumb.Text = res.GetDouble("plumbing_sanitary").ToString("#,##0.00");
                    txtCfei.Text = res.GetDouble("cfei").ToString("#,##0.00");
                    txtFiling.Text = res.GetDouble("filing").ToString("#,##0.00");
                    txtOthers.Text = res.GetDouble("others").ToString("#,##0.00");
                    txtFire.Text = res.GetDouble("fire_fund").ToString("#,##0.00");
                    txtTotal.Text =  res.GetDouble("total_amt").ToString("#,##0.00");
                    txt80.Text =  res.GetDouble("p80").ToString("#,##0.00");
                    txt20.Text =  res.GetDouble("p20").ToString("#,##0.00");
                    txtAllTotal.Text = res.GetDouble("total_all_amt").ToString("#,##0.00");
                }
            res.Close();
        }
        private void txtLineGrade_Leave(object sender, EventArgs e)
        {
            double dAmt = 0;
            double.TryParse(txtLineGrade.Text, out dAmt);
            txtLineGrade.Text = string.Format("{0:#,##0.00}", dAmt);
            ComputeTotal();
        }
        private void txtLineGrade_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtLineGrade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtBuilding_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtBuilding_Leave(object sender, EventArgs e)
        {
            double dAmt = 0;
            double.TryParse(txtBuilding.Text, out dAmt);
            txtBuilding.Text = string.Format("{0:#,##0.00}", dAmt);
            ComputeTotal();
        }

        private void txtPlumbing_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtPlumbing_Leave(object sender, EventArgs e)
        {
            double dAmt = 0;
            double.TryParse(txtPlumbing.Text, out dAmt);
            txtPlumbing.Text = string.Format("{0:#,##0.00}", dAmt);
            ComputeTotal();
        }

        private void txtWiring_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtWiring_Leave(object sender, EventArgs e)
        {
            double dAmt = 0;
            double.TryParse(txtWiring.Text, out dAmt);
            txtWiring.Text = string.Format("{0:#,##0.00}", dAmt);
            ComputeTotal();
        }

        private void txtMech_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtMech_Leave(object sender, EventArgs e)
        {
            double dAmt = 0;
            double.TryParse(txtMech.Text, out dAmt);
            txtMech.Text = string.Format("{0:#,##0.00}", dAmt);
            ComputeTotal();
        }

        private void txtOccu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtOccu_Leave(object sender, EventArgs e)
        {
            double dAmt = 0;
            double.TryParse(txtOccu.Text, out dAmt);
            txtOccu.Text = string.Format("{0:#,##0.00}", dAmt);
            ComputeTotal();
        }

        private void txtFencing_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtFencing_Leave(object sender, EventArgs e)
        {
            double dAmt = 0;
            double.TryParse(txtFencing.Text, out dAmt);
            txtFencing.Text = string.Format("{0:#,##0.00}", dAmt);
            ComputeTotal();
        }

        private void txtSign_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtSign_Leave(object sender, EventArgs e)
        {
            double dAmt = 0;
            double.TryParse(txtSign.Text, out dAmt);
            txtSign.Text = string.Format("{0:#,##0.00}", dAmt);
            ComputeTotal();
        }

        private void txtAnnual_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtAnnual_Leave(object sender, EventArgs e)
        {
            double dAmt = 0;
            double.TryParse(txtAnnual.Text, out dAmt);
            txtAnnual.Text = string.Format("{0:#,##0.00}", dAmt);
            ComputeTotal();
        }

        private void txtCivil_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtCivil_Leave(object sender, EventArgs e)
        {
            double dAmt = 0;
            double.TryParse(txtCivil.Text, out dAmt);
            txtCivil.Text = string.Format("{0:#,##0.00}", dAmt);
            ComputeTotal();
        }

        private void txtElec_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtElec_Leave(object sender, EventArgs e)
        {
            double dAmt = 0;
            double.TryParse(txtElec.Text, out dAmt);
            txtElec.Text = string.Format("{0:#,##0.00}", dAmt);
            ComputeTotal();
        }

        private void txtMech2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtMech2_Leave(object sender, EventArgs e)
        {
            double dAmt = 0;
            double.TryParse(txtMech2.Text, out dAmt);
            txtMech2.Text = string.Format("{0:#,##0.00}", dAmt);
            ComputeTotal();
        }

        private void txtPlumb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtPlumb_Leave(object sender, EventArgs e)
        {
            double dAmt = 0;
            double.TryParse(txtPlumb.Text, out dAmt);
            txtPlumb.Text = string.Format("{0:#,##0.00}", dAmt);
            ComputeTotal();
        }

        private void txtCfei_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtCfei_Leave(object sender, EventArgs e)
        {
            double dAmt = 0;
            double.TryParse(txtCfei.Text, out dAmt);
            txtCfei.Text = string.Format("{0:#,##0.00}", dAmt);
            ComputeTotal();
        }

        private void txtFiling_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtFiling_Leave(object sender, EventArgs e)
        {
            double dAmt = 0;
            double.TryParse(txtFiling.Text, out dAmt);
            txtFiling.Text = string.Format("{0:#,##0.00}", dAmt);
            ComputeTotal();
        }

        private void txtOthers_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtOthers_Leave(object sender, EventArgs e)
        {
            double dAmt = 0;
            double.TryParse(txtOthers.Text, out dAmt);
            txtOthers.Text = string.Format("{0:#,##0.00}", dAmt);
            ComputeTotal();
        }

        private void txtConstCost_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtConstCost_Leave(object sender, EventArgs e)
        {
            double dAmt = 0;
            double.TryParse(txtOthers.Text, out dAmt);
            txtOthers.Text = string.Format("{0:#,##0.00}", dAmt);
            ComputeTotal();
        }

        private void txtStoreys_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtArea_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtFire_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtFire_Leave(object sender, EventArgs e)
        {
            double dAmt = 0;
            double.TryParse(txtFire.Text, out dAmt);
            txtFire.Text = string.Format("{0:#,##0.00}", dAmt);
            ComputeTotal();
        }

        private void ComputeTotal()
        {
            double dTotalAmt = 0;
            double dLineGrade = 0;
            double dBuilding = 0;
            double dPlumbing = 0;
            double dWiring = 0;
            double dMech = 0;
            double dOccu = 0;
            double dFencing = 0;
            double dSign = 0;
            double dAnnual = 0;
            double dCivil = 0;
            double dElec = 0;
            double dMech2 = 0;
            double dPlumbSani = 0;
            double dCfei = 0;
            double dFiling = 0;
            double dOthers = 0;
            double dFire = 0;
            double d80 = 0;
            double d20 = 0;

            double.TryParse(string.Format("{0:###}", txtLineGrade.Text), out dLineGrade);
            double.TryParse(string.Format("{0:###}", txtBuilding.Text), out dBuilding);
            double.TryParse(string.Format("{0:###}", txtPlumbing.Text), out dPlumbing);
            double.TryParse(string.Format("{0:###}", txtWiring.Text), out dWiring);
            double.TryParse(string.Format("{0:###}", txtMech.Text), out dMech);
            double.TryParse(string.Format("{0:###}", txtOccu.Text), out dOccu);
            double.TryParse(string.Format("{0:###}", txtFencing.Text), out dFencing);
            double.TryParse(string.Format("{0:###}", txtSign.Text), out dSign);
            double.TryParse(string.Format("{0:###}", txtAnnual.Text), out dAnnual);
            double.TryParse(string.Format("{0:###}", txtCivil.Text), out dCivil);
            double.TryParse(string.Format("{0:###}", txtElec.Text), out dElec);
            double.TryParse(string.Format("{0:###}", txtMech2.Text), out dMech2);
            double.TryParse(string.Format("{0:###}", txtPlumb.Text), out dPlumbSani);
            double.TryParse(string.Format("{0:###}", txtCfei.Text), out dCfei);
            double.TryParse(string.Format("{0:###}", txtFiling.Text), out dFiling);
            double.TryParse(string.Format("{0:###}", txtOthers.Text), out dOthers);
            double.TryParse(string.Format("{0:###}", txtFire.Text), out dFire);

            txtTotal.Text = "";

            dTotalAmt = dLineGrade + dBuilding + dPlumbing + dWiring + dMech + dOccu + dFencing + dSign + dAnnual + dCivil + dElec + dMech2 + dPlumbSani + dCfei + dFiling + dOthers;
            txtTotal.Text = string.Format("{0:#,##0.00}", dTotalAmt);

            d80 = dTotalAmt * .8;
            d20 = dTotalAmt * .2;

            txt80.Text = string.Format("{0:#,##0.00}", d80);
            txt20.Text = string.Format("{0:#,##0.00}", d20);

            txtAllTotal.Text = string.Format("{0:#,##0.00}", (dTotalAmt + d80 + d20 + dFire));
        }

        private string GenerateApplicantNo()
        {
            OracleResultSet res = new OracleResultSet();
            string sApplicantNo = string.Empty;
            string sSeries = string.Empty;
            int iCnt = 0;
            res.Query = "select max(to_number(applicant_no)) from DOLE_APPLICANT";
            int.TryParse(res.ExecuteScalar(), out iCnt);
            if (iCnt > 0)
            {
                switch (iCnt)
                {
                    case 1: sSeries = "000" + iCnt.ToString(); break;
                    case 2: sSeries = "00" + iCnt.ToString(); break;
                    case 3: sSeries = "0" + iCnt.ToString(); break;
                    case 4: sSeries = iCnt.ToString(); break;
                }
            }
            else
                sSeries = "0001";

            return sSeries;
        }

        private void btnBill_Click(object sender, EventArgs e)
        {
            string sBillNo = GenerateBillNo();
            string sApplicantNo = GenerateApplicantNo();
            string sAcctNo = string.Empty;
            double dTotalAmt = 0;
            double dAllTotalAmt = 0;
            double dLineGrade = 0;
            double dBuilding = 0;
            double dPlumbing = 0;
            double dWiring = 0;
            double dMech = 0;
            double dOccu = 0;
            double dFencing = 0;
            double dSign = 0;
            double dAnnual = 0;
            double dCivil = 0;
            double dElec = 0;
            double dMech2 = 0;
            double dPlumbSani = 0;
            double dCfei = 0;
            double dFiling = 0;
            double dOthers = 0;
            double dFire = 0;
            double d80 = 0;
            double d20 = 0;
            double.TryParse(string.Format("{0:###}", txtLineGrade.Text), out dLineGrade);
            double.TryParse(string.Format("{0:###}", txtBuilding.Text), out dBuilding);
            double.TryParse(string.Format("{0:###}", txtPlumbing.Text), out dPlumbing);
            double.TryParse(string.Format("{0:###}", txtWiring.Text), out dWiring);
            double.TryParse(string.Format("{0:###}", txtMech.Text), out dMech);
            double.TryParse(string.Format("{0:###}", txtOccu.Text), out dOccu);
            double.TryParse(string.Format("{0:###}", txtFencing.Text), out dFencing);
            double.TryParse(string.Format("{0:###}", txtSign.Text), out dSign);
            double.TryParse(string.Format("{0:###}", txtAnnual.Text), out dAnnual);
            double.TryParse(string.Format("{0:###}", txtCivil.Text), out dCivil);
            double.TryParse(string.Format("{0:###}", txtElec.Text), out dElec);
            double.TryParse(string.Format("{0:###}", txtMech2.Text), out dMech2);
            double.TryParse(string.Format("{0:###}", txtPlumb.Text), out dPlumbSani);
            double.TryParse(string.Format("{0:###}", txtCfei.Text), out dCfei);
            double.TryParse(string.Format("{0:###}", txtFiling.Text), out dFiling);
            double.TryParse(string.Format("{0:###}", txtOthers.Text), out dOthers);
            double.TryParse(string.Format("{0:###}", txtFire.Text), out dFire);
            double.TryParse(string.Format("{0:###}", txtTotal.Text), out dTotalAmt);
            double.TryParse(string.Format("{0:###}", txtAllTotal.Text), out dAllTotalAmt);
            double.TryParse(string.Format("{0:###}", txt80.Text), out d80);
            double.TryParse(string.Format("{0:###}", txt20.Text), out d20);
            OracleResultSet res = new OracleResultSet();

            res.Query = "select acct_code from account where acct_ln = 'DOLE'";
            if (res.Execute())
                if (res.Read())
                    sAcctNo = res.GetString(0);

            res.Query = "INSERT INTO BILLING_DOLE VALUES(";
            res.Query += $"'{sBillNo}', ";
            res.Query += $"'{sApplicantNo}', ";
            res.Query += $"'{AppSettingsManager.SystemUser.UserCode.ToString()}', ";
            res.Query += $"to_date('{AppSettingsManager.GetSystemDate().ToShortDateString()}','MM/dd/yyyy'))";
            if (res.ExecuteNonQuery() == 0)
            { }

            res.Query = "INSERT INTO BILL_DOLE_INFO VALUES(";
            res.Query += $"'{sBillNo}', ";
            res.Query += $"{m_sFeesCode}, ";
            res.Query += $"{dLineGrade}, ";
            res.Query += $"{dBuilding}, ";
            res.Query += $"{dPlumbing}, ";
            res.Query += $"{dWiring}, ";
            res.Query += $"{dMech}, ";
            res.Query += $"{dOccu}, ";
            res.Query += $"{dFencing}, ";
            res.Query += $"{dSign}, ";
            res.Query += $"{dAnnual}, ";
            res.Query += $"{dCivil}, ";
            res.Query += $"{dElec}, ";
            res.Query += $"{dMech2}, ";
            res.Query += $"{dPlumbSani}, ";
            res.Query += $"{dCfei}, ";
            res.Query += $"{dFiling}, ";
            res.Query += $"{dOthers}, ";
            res.Query += $"{dFire}, ";
            res.Query += $"{dTotalAmt}, ";
            res.Query += $"{d80}, ";
            res.Query += $"{d20}, ";
            res.Query += $"{dAllTotalAmt}, ";
            res.Query += $"{sAcctNo})";
            if (res.ExecuteNonQuery() == 0)
            { }

            res.Query = "INSERT INTO DOLE_APPLICANT VALUES(";
            res.Query += $"'{sApplicantNo}', ";
            res.Query += $"'{txtApplicant.Text.Trim()}', ";
            res.Query += $"'{txtAddress.Text.Trim()}', ";
            res.Query += $"'{txtCategory.Text.Trim()}', ";
            res.Query += $"{txtStoreys.Text.Trim()}, ";
            res.Query += $"{txtArea.Text.Trim()}, ";
            res.Query += $"{txtConstCost.Text.Trim()})";
            if (res.ExecuteNonQuery() == 0)
            { }
            res.Commit();

            MessageBox.Show("Successfully billed BILL NO: " + sBillNo, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            m_sBillNo = sBillNo;
            txtBillNo.Text = m_sBillNo;
            EnableControls(false);
        }

        private string GenerateBillNo()
        {
            OracleResultSet res = new OracleResultSet();
            string sBillNo = string.Empty;
            string sYear = (AppSettingsManager.GetSystemDate().Year.ToString()).Substring(2,2);
            string sSeries = string.Empty;
            int iCnt = 0;
            res.Query = "select * from current_dole_bill_no";
            if(res.Execute())
            {
                if(res.Read())
                {
                    sBillNo = res.GetString(0);
                    iCnt = Convert.ToInt32(sBillNo.Substring(6, 3)) + 1;
                    switch (iCnt.ToString().Length)
                    {
                        case 1: sSeries = "00" + iCnt.ToString(); break;
                        case 2: sSeries = "0" + iCnt.ToString(); break;
                        case 3: sSeries = iCnt.ToString(); break;
                    }

                    res.Query = $"DELETE FROM CURRENT_DOLE_BILL_NO WHERE BILL_NO = '{sBillNo}'";
                    if (res.ExecuteNonQuery() == 0)
                    { }
                    res.Query = $"INSERT INTO CURRENT_DOLE_BILL_NO VALUES ('AI-{sYear}-{sSeries}')";
                    if (res.ExecuteNonQuery() == 0)
                    { }
                    sBillNo = $"AI-{sYear}-{sSeries}";
                }
                else
                {
                    sBillNo = "AI" + "-" + sYear + "-" + "001";
                    res.Query = $"INSERT INTO CURRENT_DOLE_BILL_NO VALUES ('{sBillNo}')";
                    if(res.ExecuteNonQuery() == 0)
                    { }
                }
            }
            res.Close();
            return sBillNo;
        }
    }
}
