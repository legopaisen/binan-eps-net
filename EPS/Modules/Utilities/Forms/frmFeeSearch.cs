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
using EPSEntities.Entity;
using Oracle.ManagedDataAccess.Client;
using Common.StringUtilities;

namespace Modules.Utilities.Forms
{
    public partial class frmFeeSearch : Form
    {
        private static ConnectionString dbConn = new ConnectionString();
        public string m_sPermitCode = string.Empty;

        public frmFeeSearch()
        {
            InitializeComponent();
        }

        private void frmFeeSearch_Load(object sender, EventArgs e)
        {
            PopulateRevenueAccount();
        }

        private void PopulateRevenueAccount()
        {
            cmbRevenueAcct.DataSource = null;
            cmbRevenueAcct.Items.Clear();

            DataTable dataTable = new DataTable("MajorFee");
            dataTable.Columns.Clear();
            dataTable.Columns.Add("Code", typeof(String));
            dataTable.Columns.Add("Desc", typeof(String));
            dataTable.Columns.Add("Type", typeof(String));

            var result = from a in Utilities.MajorFeesList.GetMajorFeesListd()
                         select a;
            foreach (var item in result)
            {
                dataTable.Rows.Add(new String[] { item.FEES_CODE, item.FEES_DESC, item.FEES_TYPE });
            }

            cmbRevenueAcct.DataSource = dataTable;
            cmbRevenueAcct.DisplayMember = "Desc";
            cmbRevenueAcct.ValueMember = "Desc";
            cmbRevenueAcct.SelectedIndex = 0;
        }

        private void cmbRevenueAcct_SelectedValueChanged(object sender, EventArgs e)
        {
            UpdateList();
        }

        private void UpdateList()
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;
            string sRevAcct = string.Empty;
            string sFeesCode = string.Empty;
            dgvList.Rows.Clear();
            var epsrec = (dynamic)null;

            sRevAcct = ((DataRowView)cmbRevenueAcct.SelectedItem)["Code"].ToString();

            sQuery = $"select fees_code,fees_desc from subcategories where fees_code LIKE '{sRevAcct}%' and fees_term <> 'SUBCATEGORY' order by fees_code";
            epsrec = db.Database.SqlQuery<SUBCATEGORIES_X>(sQuery);

            foreach (var items in epsrec)
            {
                sFeesCode = items.FEES_CODE;
                dgvList.Rows.Add(CheckSelected(sFeesCode), items.FEES_DESC, sFeesCode);
            }

        }

        private bool CheckSelected(string sFeesCode)
        {
            bool bSelected = false;
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;
            int iCnt = 0;

            sQuery = $"select count(*) from permit_fees_tbl_tmp where fees_code = '{sFeesCode}'";
            iCnt = db.Database.SqlQuery<Int32>(sQuery).SingleOrDefault();

            if (iCnt > 0)
                bSelected = true;

            return bSelected;
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;
            string sFeesCode = string.Empty;

            if (btnCheck.Text == "Uncheck All")
            {
                for (int iRow = 0; iRow < dgvList.Rows.Count; iRow++)
                {
                    //m_bIsUpdate = false;
                    dgvList[0, iRow].Value = false;

                    sFeesCode = dgvList[2,iRow].Value.ToString();

                    sQuery = $"delete from permit_fees_tbl_tmp where permit_code = '{m_sPermitCode}' and fees_code = '{sFeesCode}'";
                    db.Database.ExecuteSqlCommand(sQuery);
                }

                btnCheck.Text = "Check All";
            }
            else
            {
                for (int iRow = 0; iRow < dgvList.Rows.Count; iRow++)
                {
                    //m_bIsUpdate = true;
                    dgvList[0, iRow].Value = true;
                    sFeesCode = dgvList[2, iRow].Value.ToString();

                    sQuery = $"delete from permit_fees_tbl_tmp where permit_code = '{m_sPermitCode}' and fees_code = '{sFeesCode}'";
                    db.Database.ExecuteSqlCommand(sQuery);

                    sQuery = $"insert into permit_tbl values (:1,:2)";
                    db.Database.ExecuteSqlCommand(sQuery,
                        new OracleParameter(":1", m_sPermitCode),
                        new OracleParameter(":2", sFeesCode));

                }
                btnCheck.Text = "Uncheck All";

            }
        }

        private void dgvList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;
            string sFeesCode = string.Empty;

            for (int iRow = 0; iRow < dgvList.Rows.Count; iRow++)
            {
                sFeesCode = dgvList[2, iRow].Value.ToString();

                if ((bool)dgvList[0,iRow].Value)
                {
                    int iCnt = 0;

                    sQuery = $"select count(*) from permit_fees_tbl_tmp where fees_code = '{sFeesCode}'";
                    iCnt = db.Database.SqlQuery<Int32>(sQuery).SingleOrDefault();

                    if(iCnt == 0)
                    {
                        sQuery = $"insert into permit_tbl values (:1,:2)";
                        db.Database.ExecuteSqlCommand(sQuery,
                            new OracleParameter(":1", m_sPermitCode),
                            new OracleParameter(":2", sFeesCode));
                    }
                }
                else
                {
                    sQuery = $"delete from permit_fees_tbl_tmp where permit_code = '{m_sPermitCode}' and fees_code = '{sFeesCode}'";
                    db.Database.ExecuteSqlCommand(sQuery);
                }
            }
        }
    }
}
