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
using Common.DataConnector;
using Common.AppSettings;

namespace Modules.Reports
{
    public partial class frmReport : Form
    {
        public string ReportName { get; set; }
        public string An { get; set; }
        FormReportClass ReportClass = null;
        private static ConnectionString dbConn = new ConnectionString();

        public DateTime dtTo { get; set; }
        public DateTime dtFrom { get; set; }
        public string User { get; set; }
        public string Module { get; set; }
        public string TrailMode { get; set; }
        public string Barangay { get; set; }

        private string report_cd = string.Empty;
        private string report_desc = string.Empty;

        //building permit
        public string PermitNo { get; set; }
        public DateTime PermitDtIssued { get; set; }
        public string ORNo { get; set; }
        public string OwnerName { get; set; }
        public string ProjName { get; set; }
        public string LotNo { get; set; }
        public string BlkNo { get; set; }
        public string Street { get; set; }
        public string TCT { get; set; }
        public string Brgy { get; set; }
        public string City { get; set; }
        public string Scope { get; set; }
        public string ProjCost { get; set; }
        public string AssignedEngr { get; set; }
        public bool isPrePrint { get; set; }
        public string NoStoreys { get; set; } //AFM 20211122 new format as requested
        public string FloorArea { get; set; } //AFM 20211122 new format as requested
        public string FsecNo { get; set; }
        public string FsecDate { get; set; }

        //cert occupancy
        public string CertNo { get; set; }
        public DateTime CertDtIssued { get; set; }
        public string FSICNo { get; set; }
        public DateTime FSICDtIssued { get; set; }


        public frmReport()
        {
            InitializeComponent();
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            var db = new EPSConnection(dbConn);
            if (ReportName == "ORDER OF PAYMENT" || ReportName == "ELECTRICAL ASSESSMENT") //AFM added electrical assessment - 20220211 adjustments binan meeting 20220209
            {
                string sQuery = string.Empty;
                string sBillNo = string.Empty;

                try
                {
                    sQuery = $"select distinct bill_no from billing where arn = '{An}'";
                    sBillNo = db.Database.SqlQuery<string>(sQuery).SingleOrDefault();
                }
                catch (Exception ex)
                { }

                if (string.IsNullOrWhiteSpace(sBillNo))
                {
                    MessageBox.Show("Record has no existing billing", "SOA", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                this.reportViewer1.LocalReport.ReportEmbeddedResource = "Modules.Reports.Report.SOA.rdlc";
                this.Text = ReportName;

                ReportClass = new SOA(this);
                ReportClass.BillNo = sBillNo;
            }
            else if (ReportName == "Records" || ReportName == "Application")
            {
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "Modules.Reports.Report.Application.rdlc";
                this.Text = ReportName;

                ReportClass = new Application(this);
                ReportClass.AN = An;
            }
            else if (ReportName == "Building Information") //AFM 20190930
            {
                report_cd = string.Empty;
                report_desc = string.Empty;
                report_cd = "001";
                report_desc = "BUILDING INFORMATION";
                if (!GeneratedReport())
                    return;

                this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "Modules.Reports.Report.BuildingInformation.rdlc";
                this.Text = ReportName;

                ReportClass = new BuildingInformation(this);
                ReportClass.dtTo = dtTo;
                ReportClass.dtFrom = dtFrom;
            }

            else if (ReportName == "List of Approved Permit Application") //AFM 20190930
            {
                report_cd = string.Empty;
                report_desc = string.Empty;
                report_cd = "002";
                report_desc = "LIST OF APPROVED PERMIT APPLICATION";
                if (!GeneratedReport())
                    return;

                this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "Modules.Reports.Report.ApprovedPermitApplication.rdlc";
                this.Text = ReportName;

                ReportClass = new ApprovedPermitApplication(this);
                ReportClass.dtTo = dtTo;
                ReportClass.dtFrom = dtFrom;
            }

            else if (ReportName == "Issued Permits Summary") //AFM 20190930
            {
                report_desc = string.Empty;
                report_cd = string.Empty;
                report_cd = "003";
                report_desc = "SUMMARY OF PERMITS ISSUED";
                if (!GeneratedReport())
                    return;
                this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "Modules.Reports.Report.PermitsSummary.rdlc";
                this.Text = ReportName;

                ReportClass = new PermitsSummary(this);
                ReportClass.dtTo = dtTo;
                ReportClass.dtFrom = dtFrom;
            }
            else if (ReportName == "AUDIT TRAIL") //AFM 20200812
            {
                report_desc = "AUDIT TRAIL";
                if (!GeneratedReport())
                    return;
                this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "Modules.Reports.Report.AuditTrail.rdlc";
                this.Text = ReportName;

                ReportClass = new AuditTrail(this);
                ReportClass.dtTo = dtTo;
                ReportClass.dtFrom = dtFrom;
            }
            else if (ReportName == "ENCODER LOG") //AFM 20201029
            {
                report_desc = "AUDIT TRAIL";
                this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "Modules.Reports.Report.EncoderLog.rdlc";
                this.Text = ReportName;

                ReportClass = new EncodersLog(this);
                ReportClass.dtTo = dtTo;
                ReportClass.dtFrom = dtFrom;
            }
            else if (ReportName == "BUILDING PERMIT")
            {

                report_desc = "BUILDING PERMIT";
                this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "Modules.Reports.Report.BuildingPermit.rdlc";
                this.Text = ReportName;

                ReportClass = new BuildingPermit(this);
            }
            else if (ReportName == "CERTIFICATE OF OCCUPANCY")
            {
                report_desc = "CERTIFICATE OF OCCUPANCY";
                this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "Modules.Reports.Report.CertOccupancyBinan.rdlc";
                this.Text = ReportName;

                ReportClass = new CertOccupancyBinan(this);
            }

            ReportClass.LoadForm();

            this.reportViewer1.RefreshReport();
        }

        private bool GeneratedReport()
        {
            OracleResultSet result = new OracleResultSet();
            result.Query = "select * from rpt_info where report_cd = '"+ report_cd + "'";
            if (result.Execute())
                if (result.Read())
                {
                    string msg = string.Empty;
                    msg = "REPORT PREVIOUSLY GENERATED\n";
                    msg += "REPORT: '" + result.GetString("report_desc") + "'\n";
                    msg += "GENERATED BY: '" + result.GetString("user_name") + "'\n";
                    msg += "DATE: '" + result.GetString("gen_date") + "'\n";
                    msg += "TIME: '" + result.GetString("gen_time") + "'\n";
                    msg += "WOULD YOU LIKE TO GENERATE REPORT?";

                    DialogResult dg = MessageBox.Show(msg, "ReportClass GENERATED", MessageBoxButtons.YesNo);
                    if (dg == DialogResult.No)
                    {
                        return false;
                    }
                }
            result.Query = "delete from rpt_info where report_cd = '" + report_cd + "'";
            result.ExecuteNonQuery();
            result.Close();

            result.Query = "INSERT INTO RPT_INFO values ('" + report_cd + "', '"+ report_desc + "', '"+ AppSettingsManager.GetCurrentDate().ToShortDateString() +"', '"+ AppSettingsManager.GetCurrentDate().ToString("HH:mm:ss") +"', '"+ AppSettingsManager.SystemUser.UserCode +"', 'As of "+ AppSettingsManager.GetCurrentDate().ToShortDateString() +"')";
            result.ExecuteNonQuery();
            result.Close();

            return true;
        }
    }
}
