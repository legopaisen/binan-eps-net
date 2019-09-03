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

namespace Modules.Utilities.Forms
{
    public partial class frmBank : Form
    {
        public static ConnectionString dbConn = new ConnectionString();
        public string m_sBankCode = string.Empty;
        public string m_sTmpBankCode = string.Empty;

        public frmBank()
        {
            InitializeComponent();
        }

        private void frmBank_Load(object sender, EventArgs e)
        {
            var result = from a in Utilities.BankList.GetBankList(m_sBankCode)
                         select a;
            foreach (var item in result)
            {
                dgvList.Rows.Add(item.BANK_CODE,item.BANK_NM,item.BANK_BRANCH,item.BANK_ADD);
            }
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            m_sTmpBankCode = string.Empty;
            try
            {
                m_sTmpBankCode = dgvList[0, e.RowIndex].Value.ToString();
            }
            catch { }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            m_sBankCode = m_sTmpBankCode;
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            m_sBankCode = "";
            this.Close();
        }
    }
}
