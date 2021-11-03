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
        private string PermitCode = string.Empty;

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

                form.PermitCode = PermitCode;
                form.SearchCriteria = "CERTIFICATE";
                form.ShowDialog();
                an1.SetAn(form.sArn);
                m_sAN = an1.GetAn();
            }
            else
                m_sAN = an1.GetAn();

            txtCertNo.Enabled = true;
            txtFSIC.Enabled = true;
            dtpCertNo.Enabled = true;
            dtpCertNo.Enabled = true;
        }

        private bool CheckBldgPermit()
        {
            OracleResultSet res = new OracleResultSet();
            res.Query = $"select permit_no from application where arn = '{m_sAN}' and permit_code = '01'";
            if(res.Execute())
                if(res.Read())
                {
                    string sPermit = res.GetString(0);
                    if (string.IsNullOrEmpty(sPermit))
                        return false;
                    else
                        return true;
                }
            return false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(m_sAN))
            {
                MessageBox.Show("No ARN selected");
                return;
            }

            if(cmbCert.Text == "Certificate of Occupancy")
            {
                if(!CheckBldgPermit())
                {
                    MessageBox.Show("Application No. has not generated a Building Permit. Generate Permit first before proceeding.", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            }


            frmReport frmreport = new frmReport();

            if (MessageBox.Show("Pre-printed form?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                frmreport.isPrePrint = true;
            else
                frmreport.isPrePrint = false;

            frmreport.An = this.m_sAN;
            frmreport.ReportName = "CERTIFICATE OF OCCUPANCY";
            frmreport.CertNo = txtCertNo.Text.Trim();
            frmreport.FSICNo = txtFSIC.Text.Trim();
            frmreport.CertDtIssued = dtpCertNo.Value;
            frmreport.FSICDtIssued = dtpFSIC.Value;

            frmreport.ShowDialog();

            //if (SendValues())
            //{

            //}

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
            LoadCertificates();
            an1.ArnCode.Enabled = true;

        }

        private void LoadCertificates()
        {
            cmbCert.Items.Clear();
            cmbCert.Items.Add("Certificate of Occupancy"); //temporarily hardcoded
        }

        private void cmbCert_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbCert.Text == "Certificate of Occupancy")
            {
                PermitCode = GetPermit();
            }
        }

        private string GetPermit()
        {
            OracleResultSet res = new OracleResultSet();
            res.Query = $"select permit_code from permit_tbl where permit_desc like '%OCCUPANCY%'"; //temporarily hardcoded
            if (res.Execute())
                if (res.Read())
                    return res.GetString("permit_code");
                else
                    return "";
            else
                return "";
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
