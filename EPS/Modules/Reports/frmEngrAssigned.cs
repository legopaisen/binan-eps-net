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

namespace Modules.Reports
{
    public partial class frmEngrAssigned : Form
    {
        public frmEngrAssigned()
        {
            InitializeComponent();
        }

        public string m_sAn = string.Empty;
        public string m_sEngrCode = string.Empty;
        public string m_sPermitCode = string.Empty;
        public string m_sEngrName = string.Empty;
        public bool m_isCancel = false;

        private void btnCancel_Click(object sender, EventArgs e)
        {
            m_isCancel = true;
            this.Close();
        }

        private void frmEngrAssigned_Load(object sender, EventArgs e)
        {
            OracleResultSet res = new OracleResultSet();
            string[] arrEngr = new string[] { };
            string[] arrArch = new string[] { };
            string sEngrCode = string.Empty;
            string sArchCode = string.Empty;
            string sEngrName = string.Empty;
            res.Query = $"select * from application where arn = '{m_sAn}' and permit_code = '{m_sPermitCode}'";
            if (res.Execute())
                if(res.Read())
                {
                    sEngrCode = res.GetString("engr_code");
                    sArchCode = res.GetString("architect");
                    arrEngr = sEngrCode.Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                    arrArch = sArchCode.Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                }
            res.Close();

            for(int cnt = 0; cnt < arrEngr.Length; cnt++)
            {
                res.Query = $"select * from ENGINEER_TBL where engr_code = '{arrEngr[cnt].ToString()}'";
                if(res.Execute())
                    if(res.Read())
                    {
                        sEngrName = res.GetString("engr_fn") + " " + res.GetString("engr_mi") + ". " + res.GetString("engr_ln");
                        cmbList.Items.Add(sEngrName);
                    }
                res.Close();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(cmbList.Text))
            {
                MessageBox.Show("No item selected!", "" , MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            m_sEngrName = cmbList.Text.Trim();
            this.Close();
        }
    }
}
