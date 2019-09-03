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

namespace Modules.Transactions
{
    public partial class frmOtherCert : Form
    {
        public string m_sCert = string.Empty;

        public frmOtherCert()
        {
            InitializeComponent();
        }

        private void frmOtherCert_Load(object sender, EventArgs e)
        {
            PermitList permit = new PermitList(" where other_type = 'TRUE'");

            dgvList.Rows.Clear();

            for (int i = 0; i < permit.PermitLst.Count; i++)
            {
                dgvList.Rows.Add(permit.PermitLst[i].PermitDesc);
            }

        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                m_sCert = dgvList[0, e.RowIndex].Value.ToString();

            }
            catch
            { m_sCert = string.Empty; }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            m_sCert = string.Empty;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(m_sCert))
                this.Close();
            else
            {
                MessageBox.Show("Select type of certificate first","Certificate",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                return;
            }
        }
    }
}
