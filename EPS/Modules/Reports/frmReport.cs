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

namespace Modules.Reports
{
    public partial class frmReport : Form
    {
        public string ReportName { get; set; }
        public string Arn { get; set; }
        FormReportClass ReportClass = null;
        private static ConnectionString dbConn = new ConnectionString();

        public frmReport()
        {
            InitializeComponent();
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            var db = new EPSConnection(dbConn);
            if (ReportName == "ORDER OF PAYMENT")
            {
                string sQuery = string.Empty;
                string sBillNo = string.Empty;

                try
                {
                    sQuery = $"select distinct bill_no from billing where arn = '{Arn}'";
                    sBillNo = db.Database.SqlQuery<string>(sQuery).SingleOrDefault();
                }
                catch
                { }

                if(string.IsNullOrWhiteSpace(sBillNo))
                {
                    MessageBox.Show("Record has no existing billing","SOA",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                    return;
                }

                this.reportViewer1.LocalReport.ReportEmbeddedResource = "Modules.Reports.Report.SOA.rdlc";
                this.Text = ReportName;

                ReportClass = new SOA(this);
                ReportClass.BillNo = sBillNo;
            }
            else if(ReportName == "Records" || ReportName == "Application")
            {
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "Modules.Reports.Report.Application.rdlc";
                this.Text = ReportName;

                ReportClass = new Application(this);
                ReportClass.ARN = Arn;
            }

            ReportClass.LoadForm();

            this.reportViewer1.RefreshReport();
        }
    }
}
