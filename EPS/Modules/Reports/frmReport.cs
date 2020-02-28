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

        private string report_cd = string.Empty;
        private string report_desc = string.Empty;

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
                    sQuery = $"select distinct bill_no from billing where arn = '{An}'";
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
                ReportClass.AN = An;
            }
            else if(ReportName == "Building Information") //AFM 20190930
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
