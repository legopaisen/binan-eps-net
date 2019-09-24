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

namespace Modules.Billing
{
    public partial class frmBilling : Form
    {
        public static ConnectionString dbConn = new ConnectionString();
        TaskManager taskman = new TaskManager();
        public string Source { get; set; }
        private FormClass RecordClass = null;
        public string m_sModule = string.Empty;
        public string m_sAN = string.Empty;
        public string PermitCode { get; set; }
        public string ModuleCode { get; set; }

        public frmBilling()
        {
            InitializeComponent();
        }

        private void frmBilling_Load(object sender, EventArgs e)
        {
            PermitList permit = new PermitList(null);
            PermitCode = string.Empty;
            m_sModule = "BILLING";
            PermitCode = permit.GetPermitCode(Source);

            if (Source == "BUILDING PERMIT")
            {
                RecordClass = new Building(this);
            }
            else if (Source == "CERTIFICATE OF OCCUPANCY" || Source == "CERTIFICATE OF ANNUAL INSPECTION")
            {
                RecordClass = new BillCertificate(this);
            }
            else
            {
                RecordClass = new OtherPermit(this);
            }

            this.Text = "Billing - " + Source;
            RecordClass.FormLoad();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            m_sAN = an1.GetArn();
            TaskManager taskman = new TaskManager();

            if (string.IsNullOrEmpty(m_sAN))
            {
                SearchAccount.frmSearchARN form = new SearchAccount.frmSearchARN();
                if (Source == "CERTIFICATE OF OCCUPANCY" || Source == "CERTIFICATE OF ANNUAL INSPECTION")
                    form.SearchCriteria = "APP";
                else
                    form.SearchCriteria = "QUE";

                form.ShowDialog();

                an1.SetArn(form.sArn);

                m_sAN = an1.GetArn();
            }
            else
                m_sAN = an1.GetArn();

            if (string.IsNullOrEmpty(m_sAN))
                return;

            if (!RecordClass.ValidatePermitNo())
                return;

            if (!taskman.AddTask(m_sModule, m_sAN))
                return;

            if (!RecordClass.DisplayData())
            {    
                RecordClass.ClearControls();
                return;
            }
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            RecordClass.ClearControls();
            taskman.RemTask(m_sAN);
            //an1.GetCode = "";
            an1.GetLGUCode = "";
            an1.GetTaxYear = "";
            //an1.GetMonth = "";
            an1.GetDistCode = "";
            an1.GetSeries = "";
            m_sAN = "";
        }

        private void dgvAssessment_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            RecordClass.CellLeave(sender, e);
        }

        private void dgvParameter_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnCompute_Click(object sender, EventArgs e)
        {
            if(!RecordClass.Compute())
            {
                MessageBox.Show("Please complete all parameters","Billing",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                return;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            RecordClass.ButtonOk();
        }

        private void dgvAssessment_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //bool entry = false;
            //for (int row = 0; row<dgvPermit.Rows.Count; row++)
            //{
            //    if((bool)dgvPermit[0, row].Value == true)
            //    {
            //        entry = true;
            //    }
            //}
            //if(entry == true)
                RecordClass.CellClick(sender, e);
        }

        private void frmBilling_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                taskman.RemTask(m_sAN);
                //an1.GetCode = "";
                an1.GetLGUCode = "";
                an1.GetTaxYear = "";
                //an1.GetMonth = "";
                an1.GetDistCode = "";
                an1.GetSeries = "";
                m_sAN = "";

            }
            else
                return;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            RecordClass.Save();
        }

        private void dgvPermit_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                RecordClass.PermitCellClick(sender, e);
                //AFM 20190911 permit checkbox (s)
                if (dgvPermit.CurrentCell == null) return;
                if ((bool)dgvPermit.CurrentCell.Value == true)
                    dgvPermit.CurrentCell.Value = false;
                else if ((bool)dgvPermit.CurrentCell.Value == false)
                    dgvPermit.CurrentCell.Value = true;
                //AFM 20190911 permit checkbox (e)
            }
            catch { }
            RemoveUnbilled();
        }

        private void RemoveUnbilled() //AFM 20190913
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;
            List<string> feesCode = new List<string>();
            string PermitDlt = string.Empty;
            for (int cnt = 0; cnt < dgvPermit.Rows.Count; cnt++)
            {
                if ((bool)dgvPermit[0, cnt].Value == false)
                {
                    if (dgvPermit[0, cnt].Selected == true)
                    {
                        for (int list = 0; list < dgvAssessment.Rows.Count; list++)
                        {
                            if ((bool)dgvAssessment[0, list].Value == true)
                            {
                                feesCode.Add(dgvAssessment[2, list].Value.ToString());
                            }
                        }
                    }
                }
            }
            foreach (var s in feesCode)
            {
                sQuery = $"delete from bill_tmp where arn = '{m_sAN}' and fees_code = '{s}'";
                db.Database.ExecuteSqlCommand(sQuery);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (btnCancel.Text == "Exit")
                this.Close();
            else
            {
                if (MessageBox.Show("Are you sure you want to cancel transaction?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    RecordClass.ClearControls();
                    taskman.RemTask(m_sAN);
                    //an1.GetCode = "";
                    an1.GetLGUCode = "";
                    an1.GetTaxYear = "";
                    //an1.GetMonth = "";
                    an1.GetDistCode = "";
                    an1.GetSeries = "";
                    m_sAN = "";
                    this.Close();
                }
                else
                    return;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            frmReport form = new frmReport();
            form.ReportName = "ORDER OF PAYMENT";
            form.Arn = m_sAN;
            form.ShowDialog();
        }

        private void btnAddlAdd_Click(object sender, EventArgs e)
        {
            RecordClass.AdditionalFeesAdd(sender, e);
        }

        private void txtAddlAmt_Leave(object sender, EventArgs e)
        {
            double dAmt = 0;
            double.TryParse(txtAddlAmt.Text.ToString(), out dAmt);

            txtAddlAmt.Text = string.Format("{0:#,###.00}", dAmt);
        }
    }
}
