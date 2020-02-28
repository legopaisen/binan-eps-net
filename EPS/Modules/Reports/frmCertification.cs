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
using Modules.Reports;
using EPSEntities.Connection;
using Common.DataConnector;
using Common.AppSettings;

namespace Modules.Reports
{
    public partial class frmCertification : Form
    {
        public frmCertification()
        {
            InitializeComponent();
        }

        private Modules.Reports.Model.CertificateMODEL _item;

        public Modules.Reports.Model.CertificateMODEL item
        {
            get { return _item; }
            set { _item = value; }
        }

        public string m_sAN = string.Empty;

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void an1_Load(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            m_sAN = an1.GetAn();
            TaskManager taskman = new TaskManager();

            if (string.IsNullOrEmpty(m_sAN))
            {
                SearchAccount.frmSearchARN form = new SearchAccount.frmSearchARN();

                form.ShowDialog();
                an1.SetAn(form.sArn);
                m_sAN = an1.GetAn();
            }
            else
                m_sAN = an1.GetAn();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(m_sAN))
            {
                MessageBox.Show("No ARN selected");
                return;
            }
            if (SendValues())
            {

            }
            
        }
        private bool SendValues()
        {
            item = new Model.CertificateMODEL();
            string bldgCode = string.Empty;
            string engrCode = string.Empty;
            item.EngrOfficial = AppSettingsManager.GetConfigValue("08").ToString();

            OracleResultSet result = new OracleResultSet();
            result.Query = "select AP.*, AC.acct_ln || ', ' || AC.acct_fn || AC.acct_mi as REAL_BLDG_OWNER, AP.proj_hse_no || ', ' || AP.proj_lot_no || ', ' || AP.proj_addr || ', ' || AP.proj_brgy AS REAL_ADDR from application AP, account AC where arn = '" + m_sAN + "' and AP.proj_owner = AC.acct_code and main_application = '1'"; //gets main permit
            if (result.Execute())
            {
                if (result.Read())
                {
                    item.BldgOwner = result.GetString("REAL_BLDG_OWNER");
                    item.DtIssued = result.GetDateTime("date_issued");
                    item.BldgAdress = result.GetString("REAL_ADDR");
                }
            }
            else
            {
                MessageBox.Show("ARN Not Found");
                return false;
            }

            result.Query = "select sum(fees_due), or_date from mrs_payments where arn = '"+ m_sAN + "' group by or_date";
            if(result.Execute())
                if(result.Read())
                {
                    item.AmtPaid = result.GetDecimal(0);
                    item.ORDate = result.GetDateTime(1);
                }
            return true;
        }

        private void frmCertification_Load(object sender, EventArgs e)
        {

        }
    }
}
