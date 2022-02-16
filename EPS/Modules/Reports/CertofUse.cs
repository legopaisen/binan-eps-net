using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DataConnector;
using Common.AppSettings;
namespace Modules.Reports
{
   public class CertofUse : FormReportClass
    {
        public CertofUse(frmReport Form) : base(Form)
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
                new Microsoft.Reporting.WinForms.ReportParameter("sChiefIED", AppSettingsManager.GetConfigValue("31")),
                new Microsoft.Reporting.WinForms.ReportParameter("sChiefPED", AppSettingsManager.GetConfigValue("32")),
                new Microsoft.Reporting.WinForms.ReportParameter("dtChiefIED",  ReportForm.CertDtIssued.ToShortDateString()),
                new Microsoft.Reporting.WinForms.ReportParameter("dtChiefPED",  ReportForm.CertDtIssued.ToShortDateString()),
                new Microsoft.Reporting.WinForms.ReportParameter("sOwnerName", ReportForm.OwnerName),
                new Microsoft.Reporting.WinForms.ReportParameter("sCharacterOccupancy", ReportForm.sCharacterOccupancy),
                new Microsoft.Reporting.WinForms.ReportParameter("dtSubmitted", ReportForm.CertDtIssued.ToShortDateString()),

                new Microsoft.Reporting.WinForms.ReportParameter("sNameofProject",ReportForm.ProjName),
                new Microsoft.Reporting.WinForms.ReportParameter("sLocated",ReportForm.sLocated),
                new Microsoft.Reporting.WinForms.ReportParameter("dtBuildingOfficial",  ReportForm.CertDtIssued.ToShortDateString()),

                new Microsoft.Reporting.WinForms.ReportParameter("sProfessional",ReportForm.sProfession),
                new Microsoft.Reporting.WinForms.ReportParameter("sPermitNo",ReportForm.PermitNo),
                new Microsoft.Reporting.WinForms.ReportParameter("dtPermitIssued",ReportForm.dtPermitIssued.ToShortDateString()),
                new Microsoft.Reporting.WinForms.ReportParameter("sSignPermitNo",ReportForm.SignPermitNo), 

                new Microsoft.Reporting.WinForms.ReportParameter("cb1",ReportForm.cb1),
                new Microsoft.Reporting.WinForms.ReportParameter("cb2",ReportForm.cb2),
                new Microsoft.Reporting.WinForms.ReportParameter("cb3",ReportForm.cb3),
                new Microsoft.Reporting.WinForms.ReportParameter("cb4",ReportForm.cb4),
                new Microsoft.Reporting.WinForms.ReportParameter("sSpecify",ReportForm.Specify)
            };
            ReportForm.reportViewer1.LocalReport.SetParameters(para);
        }
    }
     
}
