using Common.DataConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Modules.Billing
{
    public partial class frmAddOn : Form
    {
        public frmAddOn()
        {
            InitializeComponent();
        }
        public string Source { get; set; }

        public string m_sBillNo = string.Empty;
        public string m_sAN = string.Empty;
        public bool CtrlPressed = false;

        private void frmAddOn_Load(object sender, EventArgs e)
        {
            LoadAddl();
        }

        private void LoadAddl()
        {
            dgvAddOnFees.Rows.Clear();
            OracleResultSet res = new OracleResultSet();
            res.Query = $"select * from bill_addl_temp where arn = '{m_sAN}'";
            if (res.Execute())
                while (res.Read())
                {
                    bool isChecked = false;
                    if (res.GetString("ischecked") == "Y")
                        isChecked = true;
                    dgvAddOnFees.Rows.Add(isChecked, res.GetString("fees_desc"), res.GetString("fees_code"), res.GetString("fees_means"), res.GetString("fees_unit"), res.GetString("area_needed"), res.GetString("cumulative"), res.GetDouble("unit_value"), res.GetDouble("fees_amt"), res.GetString("category"), res.GetString("scope_code"), res.GetDouble("fees_orig_amt"));
                }
            res.Close();
        }

        private void bntOk_Click(object sender, EventArgs e)
        {

        }

        private void btnAddOnAmt_Click(object sender, EventArgs e)
        {
            dgvAddOnFees.Rows.Clear();
            OracleResultSet res = new OracleResultSet();
            res.Query = $"select * from bill_addl_temp where arn = '{m_sAN}'";
            if (res.Execute())
                while (res.Read())
                {
                    bool isChecked = false;
                    if (res.GetString("ischecked") == "Y")
                        isChecked = true;
                    dgvAddOnFees.Rows.Add(isChecked, res.GetString("fees_desc"), res.GetString("fees_code"), res.GetString("fees_means"), res.GetString("fees_unit"), res.GetString("area_needed"), res.GetString("cumulative"), res.GetDouble("unit_value"), res.GetDouble("fees_amt"), res.GetString("category"), res.GetString("scope_code"), res.GetDouble("fees_orig_amt"));
                }
            res.Close();
        }

        private void txtAddOnAmt_KeyUp(object sender, KeyEventArgs e) //requested by binan - override will trigger when pressed ctrl+F
        {
            if (e.KeyCode == Keys.F && CtrlPressed == true)
            {
                OracleResultSet res = new OracleResultSet();
                for(int cnt = 0; cnt < dgvAddOnFees.Rows.Count; cnt++)
                {
                    double dPrevAmt = 0;
                    double dNewAmt = 0;
                    double.TryParse(dgvAddOnFees[8, cnt].Value.ToString(), out dPrevAmt);
                    double.TryParse(txtAddOnAmt.Text.Trim(), out dNewAmt);
                    res.Query = $"UPDATE BILL_ADDL_TEMP SET FEES_AMT = {dNewAmt}, FEES_ORIG_AMT = {dPrevAmt} where arn = '{m_sAN}'";
                    if(res.ExecuteNonQuery() == 0)
                    { }
                }
            }
            CtrlPressed = false;
        }

        private void txtAddOnAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true)
            {
                CtrlPressed = true;
            }
        }
    }
}
