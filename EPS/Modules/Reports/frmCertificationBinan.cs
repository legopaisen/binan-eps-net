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
    public partial class frmCertificationBinan : Form
    {
        public frmCertificationBinan()
        {
            InitializeComponent();
        }

        private void frmCertificationBinan_Load(object sender, EventArgs e)
        {
            LoadCertificates(); 

        }

        private void LoadCertificates()
        {
            
            cmbCert.Items.Clear();
            cmbCert.Items.Add("Certificate of Use");  
            cmbCert.Items.Add("Certificate of Annual Inspection");
        }

        private void cmbCert_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCert.Text == "Certificate of Use")
            {
                txtCertNo.Enabled = true;
                txtFeePaid.Enabled = true;
                txtORNo.Enabled = true;
                dtPaid.Enabled = true;
                dtIssued.Enabled = true;
                txtOwnersName.Enabled = true;
                txtProjectName.Enabled = true;
                txtCharacterOccupancy.Enabled = true;
                txtLocation.Enabled = true;
                 
                txtGrp.Enabled = false;

                txtProfession.Enabled = true;
                txtPermitNo.Enabled = true;
                txtSignNo.Enabled = true;
                dtPermitIssued.Enabled = true;
                cb1.Enabled = true;
                cb2.Enabled = true;
                cb3.Enabled = true;
                cb4.Enabled = true;
                txtSpecify.Enabled = true;

            }
            else if (cmbCert.Text == "Certificate of Annual Inspection")
            {
                txtCertNo.Enabled = true;
                txtFeePaid.Enabled = true;
                txtORNo.Enabled = true;
                dtPaid.Enabled = true;
                dtIssued.Enabled = true;
                txtOwnersName.Enabled = true;
                txtProjectName.Enabled = true;
                txtCharacterOccupancy.Enabled = true;
                txtLocation.Enabled = true;
                txtLocation.Enabled = true;

                txtGrp.Enabled = true;
                txtOccupancyNo.Enabled = true;


                txtProfession.Enabled = false;
                txtPermitNo.Enabled = false;
                txtSignNo.Enabled = false;
                dtPermitIssued.Enabled = false;
                cb1.Enabled = false;
                cb2.Enabled = false;
                cb3.Enabled = false;
                cb4.Enabled = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cb4_CheckedChanged(object sender, EventArgs e)
        {
         

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            frmReport frmreport = new frmReport();

            if (MessageBox.Show("Pre-printed form?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                frmreport.isPrePrint = true;
            else
                frmreport.isPrePrint = false;

            if (cmbCert.Text == "Certificate of Annual Inspection")
            { 
                frmreport.ReportName = "CERTIFICATE OF ANNUAL INSPECTION";

                frmreport.sGroup = txtGrp.Text.Trim();

            }
            else if (cmbCert.Text == "Certificate of Use")
            {
                frmreport.ReportName = "CERTIFICATE OF USE";

                frmreport.sProfession = txtProfession.Text.Trim();
                frmreport.PermitNo = txtPermitNo.Text.Trim();
                frmreport.SignPermitNo = txtSignNo.Text.Trim();
                frmreport.dtPermitIssued = Convert.ToDateTime(dtPermitIssued.Text);

                if (cb1.Checked == true)
                    frmreport.cb1 = "/";
                else
                    frmreport.cb1 = "";

                if (cb2.Checked == true)
                    frmreport.cb2 = "/";
                else
                    frmreport.cb2 = "";

                if (cb3.Checked == true)
                    frmreport.cb3 = "/";
                else
                    frmreport.cb3 = "";

                if (cb4.Checked == true)
                {
                    frmreport.cb4 = "/";
                    frmreport.Specify = txtSpecify.Text.Trim();
                }
                else
                    frmreport.cb4 = "";
            }
            frmreport.CertNo = txtCertNo.Text.Trim();
            frmreport.ORNo = txtORNo.Text.Trim();
             frmreport.sFeepaid = txtFeePaid.Text.Trim();
            frmreport.dtPaid = frmreport.CertDtIssued = Convert.ToDateTime(dtPaid.Text); 
            frmreport.CertDtIssued = Convert.ToDateTime(dtIssued.Text);
            frmreport.OwnerName = txtOwnersName.Text.Trim();
            frmreport.sCharacterOccupancy = txtCharacterOccupancy.Text.Trim();
         
            frmreport.ProjName = txtProjectName.Text.Trim();
            frmreport.sLocated = txtLocation.Text.Trim();
            frmreport.sOccupancyNo = txtOccupancyNo.Text.Trim();



            frmreport.ShowDialog();

        }
    }
}
