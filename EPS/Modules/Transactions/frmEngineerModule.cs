using Common.AppSettings;
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

namespace Modules.Transactions
{
    public partial class frmEngineerModule : Form
    {
        public frmEngineerModule()
        {
            InitializeComponent();
        }

        private string m_sAn = string.Empty;

        private void frmEngineerModule_Load(object sender, EventArgs e)
        {
            PopulateGrid();
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
        }
        
        private void ClearControls()
        {
            txtNewAmt.Text = string.Empty;
            txtOrigAmt.Text = string.Empty;
            txtProj.Text = string.Empty;
            m_sAn = string.Empty;
            dgvFees.Rows.Clear();
            an1.Clear();
            PopulateGrid();
        }
        
        private void PopulateGrid()
        {
            dgvList.Rows.Clear();
            OracleResultSet res = new OracleResultSet();
            string sArn = string.Empty;
            string sProj = string.Empty;
            res.Query = "select distinct arn from application_approval where status = 'PENDING'";
            if(res.Execute())
                while(res.Read())
                {
                    sArn = res.GetString("arn");
                    sProj = GetProjName(sArn);
                    dgvList.Rows.Add(sArn, sProj);
                }
            res.Close();
        }

        private string GetProjName(string sArn)
        {
            OracleResultSet res = new OracleResultSet();
            string sProj = string.Empty;
            res.Query = $"select * from application_que where arn = '{sArn}'";
            if(res.Execute())
                if(res.Read())
                {
                    sProj = res.GetString("proj_desc");
                }
            res.Close();

            return sProj;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvFees.Rows.Clear();
            txtOrigAmt.Text = string.Empty;
            txtNewAmt.Text = string.Empty;
            string sArn = string.Empty;
            sArn = dgvList[0, e.RowIndex].Value.ToString();

            OracleResultSet res = new OracleResultSet();
            res.Query = $"select * from application_approval where arn = '{sArn}'";
            if(res.Execute())
                while(res.Read())
                {
                    dgvFees.Rows.Add(res.GetString("fees_code"), res.GetString("fees_desc"), res.GetDouble("orig_amount"), res.GetDouble("amount"));
                }
            res.Close();

            an1.SetAn(sArn);
            m_sAn = sArn;

            try
            {
                txtProj.Text = dgvList[1, e.RowIndex].Value.ToString();
            }
            catch { }
        }

        private void dgvFees_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtOrigAmt.Text = string.Format("{0:#,##0.##}", dgvFees[2, e.RowIndex].Value);
            }
            catch { }

            try
            {
                txtNewAmt.Text = string.Format("{0:#,##0.##}", dgvFees[3, e.RowIndex].Value);
            }
            catch { }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(m_sAn))
            {
                MessageBox.Show("Select application!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (MessageBox.Show("Approve application?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                OracleResultSet res = new OracleResultSet();
                res.Query = $"UPDATE APPLICATION_APPROVAL SET STATUS = 'APPROVED', DATE_APPROVED = to_date('{AppSettingsManager.GetSystemDate().ToShortDateString()}','MM/dd/yyyy') WHERE ARN = '{m_sAn}'";
                if(res.ExecuteNonQuery() == 0)
                { }
                res.Close();

                MessageBox.Show("Application approved!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearControls();
            }

        }
    }
}
