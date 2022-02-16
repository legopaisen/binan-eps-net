using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DataConnector;
using Common.AppSettings;


namespace Modules.Reports
{
  

   public class CertofAnnualInsBinan : FormReportClass
    {
        public CertofAnnualInsBinan(frmReport Form) : base(Form)
        { }

        public override void LoadForm()
        {
           


            Microsoft.Reporting.WinForms.ReportParameter[] para = new Microsoft.Reporting.WinForms.ReportParameter[]
            {
                new Microsoft.Reporting.WinForms.ReportParameter("LGUName", AppSettingsManager.GetConfigValue("02")),
                new Microsoft.Reporting.WinForms.ReportParameter("ProvinceName", AppSettingsManager.GetConfigValue("03")),
                new Microsoft.Reporting.WinForms.ReportParameter("sCertNo", ReportForm.CertNo),
                new Microsoft.Reporting.WinForms.ReportParameter("ORNo", ReportForm.ORNo ),
                new Microsoft.Reporting.WinForms.ReportParameter("sFeePaid", ReportForm.sFeepaid),
                new Microsoft.Reporting.WinForms.ReportParameter("DatePaid", ReportForm.dtPaid.ToShortDateString()),
                new Microsoft.Reporting.WinForms.ReportParameter("DateIssued", ReportForm.CertDtIssued.ToShortDateString()),
                new Microsoft.Reporting.WinForms.ReportParameter("sBuildingOfficial", AppSettingsManager.GetConfigValue("08")),
                new Microsoft.Reporting.WinForms.ReportParameter("sOccupancyNo",ReportForm.sOccupancyNo),
                new Microsoft.Reporting.WinForms.ReportParameter("sChiefIED", AppSettingsManager.GetConfigValue("31")),
                new Microsoft.Reporting.WinForms.ReportParameter("sChiefPED", AppSettingsManager.GetConfigValue("32")),
                new Microsoft.Reporting.WinForms.ReportParameter("dtChiefIED",  ReportForm.CertDtIssued.ToShortDateString()),
                new Microsoft.Reporting.WinForms.ReportParameter("dtChiefPED",  ReportForm.CertDtIssued.ToShortDateString()),
                new Microsoft.Reporting.WinForms.ReportParameter("sNameOwner", ReportForm.OwnerName),
                new Microsoft.Reporting.WinForms.ReportParameter("sCharacterOccupancy", ReportForm.sCharacterOccupancy),
                new Microsoft.Reporting.WinForms.ReportParameter("sGroup", ReportForm.sGroup),
                new Microsoft.Reporting.WinForms.ReportParameter("sNameofProject",ReportForm.ProjName),
                new Microsoft.Reporting.WinForms.ReportParameter("sLocated",ReportForm.sLocated),
                new Microsoft.Reporting.WinForms.ReportParameter("dtBuildingOffical",  ReportForm.CertDtIssued.ToShortDateString())
            };
            ReportForm.reportViewer1.LocalReport.SetParameters(para);
        }
    }
}
