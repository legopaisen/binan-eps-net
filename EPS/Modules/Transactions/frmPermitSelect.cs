using Common.DataConnector;
using Modules.Utilities;
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
    public partial class frmPermitSelect : Form
    {
        public frmPermitSelect()
        {
            InitializeComponent();
        }

        private PermitList m_lstPermit;
        public string m_sPermitCode = string.Empty;
        public string m_sPermitDesc = string.Empty;
        public bool isCancel = false;

        private void frmPermitSelect_Load(object sender, EventArgs e)
        {
            cmbPermits.Items.Clear();
            m_lstPermit = new PermitList("where other_type = 'FALSE' and permit_desc NOT LIKE 'BUILDING%' and permit_desc NOT LIKE 'MECHANICAL%' and permit_desc NOT LIKE 'ELECTRICAL%' and permit_desc NOT LIKE 'OCCUPANCY%' and permit_desc NOT LIKE 'CFEI%'");
            int iCnt = m_lstPermit.PermitLst.Count;

            DataTable dataTable = new DataTable("Permit");
            dataTable.Columns.Clear();
            dataTable.Columns.Add("PermitCode", typeof(String));
            dataTable.Columns.Add("PermitDesc", typeof(String));

            dataTable.Rows.Add(new String[] { "", "" });

            for (int i = 0; i < iCnt; i++)
            {
                dataTable.Rows.Add(new String[] { m_lstPermit.PermitLst[i].PermitCode, m_lstPermit.PermitLst[i].PermitDesc });
            }

            cmbPermits.DataSource = dataTable;
            cmbPermits.DisplayMember = "PermitDesc";
            cmbPermits.ValueMember = "PermitDesc";
            cmbPermits.SelectedIndex = 0;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(cmbPermits.Text))
            {
                MessageBox.Show("Please select permit!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            m_sPermitCode = ((DataRowView)cmbPermits.SelectedItem)["PermitCode"].ToString();
            m_sPermitDesc = ((DataRowView)cmbPermits.SelectedItem)["PermitDesc"].ToString();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            isCancel = true;
            this.Close();
        }
    }
}
