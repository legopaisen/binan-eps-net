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

        public DateTime dtTo { get; set; }
        public DateTime dtFrom { get; set; }

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
            else if(ReportName == "Building Information") //AFM 20190930
            {
                this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "Modules.Reports.Report.BuildingInformation.rdlc";
                this.Text = ReportName;

                ReportClass = new BuildingInformation(this);
                ReportClass.dtTo = dtTo;
                ReportClass.dtFrom = dtFrom;
            }

            else if (ReportName == "List of Approved Permit Application") //AFM 20190930
            {
                MessageBox.Show("Under Construction ༼ つ ◕o◕ ༽つ");
                return;
                this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "Modules.Reports.Report.ApprovedPermitApplication.rdlc";
                this.Text = ReportName;

                ReportClass = new ApprovedPermitApplication(this);
            }

            else if (ReportName == "Issued Permits Summary") //AFM 20190930
            {
                MessageBox.Show("Under Construction ༼ つ ◕o◕ ༽つ");
                return;
                this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "Modules.Reports.Report.PermitsSummary.rdlc";
                this.Text = ReportName;

                ReportClass = new PermitsSummary(this);
            }

            ReportClass.LoadForm();

            this.reportViewer1.RefreshReport();
        }
    }
}
