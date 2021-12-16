using Common.AppSettings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Modules.Reports
{
    public partial class frmFSECSelect : Form
    {
        public frmFSECSelect()
        {
            InitializeComponent();
        }

        public bool m_isCancel = false;
        public string m_sFsecNo = string.Empty;
        public string m_sDateFsec = string.Empty;

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            m_isCancel = true;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            m_sFsecNo = txtNo.Text.Trim();
            m_sDateFsec = dtIssued.Value.ToShortDateString();
            this.Close();
        }
    }
}
