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

namespace Modules.SearchAccount
{
    public partial class frmSearchDOLE : Form
    {
        public frmSearchDOLE()
        {
            InitializeComponent();
        }

        public string BillNo = string.Empty;

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvList.Rows.Clear();
            txtBillNo.Text = "";
            txtApplicant.Text = "";
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(BillNo))
            {
                MessageBox.Show("No record selected!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            this.Close();
        }

        private void bntList_Click(object sender, EventArgs e)
        {
                PopulateGrid();
        }

        private void PopulateGrid()
        {
            dgvList.Rows.Clear();
            OracleResultSet res = new OracleResultSet();
            res.Query = $"select * from billing_dole where bill_no like '%{txtBillNo.Text.Trim()}%' and applicant name like '%{txtApplicant.Text.Trim()}%' order by bill_no";
            if(res.Execute())
            {
                if (res.Read())
                {
                    while(res.Read())
                    {
                        dgvList.Rows.Add(res.GetString("bill_no"), res.GetString("applicant_name"));
                    }
                }
                else
                {
                    MessageBox.Show("No record found!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
                

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvList[0, e.RowIndex].Value != null)
                    BillNo = dgvList[0, e.RowIndex].Value.ToString();
            }
            catch { }

            try
            {
                if (dgvList[1, e.RowIndex].Value != null)
                    txtApplicant.Text = dgvList[1, e.RowIndex].Value.ToString();
            }
            catch { }
        }

        private void frmSearchDOLE_Load(object sender, EventArgs e)
        {

        }
    }
}
