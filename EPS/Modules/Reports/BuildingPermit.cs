using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPSEntities.Connection;
using System.Data;
using Common.AppSettings;
using Microsoft.Reporting.WinForms;
using Modules.Records;
using Modules.Utilities;
using EPSEntities.Entity;
using Common.DataConnector;

namespace Modules.Reports
{
    public class BuildingPermit : FormReportClass
    {
        public BuildingPermit(frmReport Form) : base(Form)
        { }

        public override void LoadForm()
        {

            OracleResultSet res = new OracleResultSet();
            OracleResultSet res2 = new OracleResultSet();
            DateTime dtOR = AppSettingsManager.GetSystemDate();
            res.Query = $"select or_date payments_info where or_no = '{ReportForm.ORNo}'";
            if(res.Execute())
                if(res.Read())
                {
                    dtOR = res.GetDateTime("or_date");
                }
            res.Close();

            string sOccu = string.Empty;
            res.Query = $"select distinct APP.*, PT.permit_desc from application APP, permit_tbl PT where APP.permit_code = PT.permit_code and PT.permit_desc = 'BUILDING PERMIT' and APP.arn = '{ReportForm.An}'";
            if(res.Execute())
                if(res.Read())
                {
                    res2.Query = $"select occ_desc from OCCUPANCY_TBL where occ_code = '{res.GetString("occupancy_code")}'";
                    if(res2.Execute())
                        if(res2.Read())
                        {
                            sOccu = res2.GetString("occ_desc");
                        }
                    res2.Close();
                }
            res.Close();


            Microsoft.Reporting.WinForms.ReportParameter[] para = new Microsoft.Reporting.WinForms.ReportParameter[]
            {
                new Microsoft.Reporting.WinForms.ReportParameter("PermitNo", ReportForm.PermitNo),
                new Microsoft.Reporting.WinForms.ReportParameter("PermitDtIssued", ReportForm.PermitDtIssued.ToShortDateString()),
                new Microsoft.Reporting.WinForms.ReportParameter("ORNo", ReportForm.ORNo),
                new Microsoft.Reporting.WinForms.ReportParameter("DatePaid", dtOR.ToShortDateString()),
                new Microsoft.Reporting.WinForms.ReportParameter("OwnerName", ReportForm.OwnerName),
                new Microsoft.Reporting.WinForms.ReportParameter("ProjName", ReportForm.ProjName),
                new Microsoft.Reporting.WinForms.ReportParameter("LotNo", ReportForm.LotNo),
                new Microsoft.Reporting.WinForms.ReportParameter("BlkNo", ReportForm.BlkNo),
                new Microsoft.Reporting.WinForms.ReportParameter("TCT", ReportForm.TCT),
                new Microsoft.Reporting.WinForms.ReportParameter("Street", ReportForm.Street),
                new Microsoft.Reporting.WinForms.ReportParameter("Brgy", ReportForm.Brgy),
                new Microsoft.Reporting.WinForms.ReportParameter("City", ReportForm.City),
                new Microsoft.Reporting.WinForms.ReportParameter("Zip", AppSettingsManager.GetConfigValue("28")),
                new Microsoft.Reporting.WinForms.ReportParameter("Occupancy", sOccu),
                new Microsoft.Reporting.WinForms.ReportParameter("Scope", ReportForm.Scope),
                new Microsoft.Reporting.WinForms.ReportParameter("ProjCost", ReportForm.ProjCost),
                new Microsoft.Reporting.WinForms.ReportParameter("PrePrinted", ReportForm.isPrePrint.ToString()),
                new Microsoft.Reporting.WinForms.ReportParameter("AssignedPro", ReportForm.AssignedEngr)
            };
            ReportForm.reportViewer1.LocalReport.SetParameters(para);

        }
    }
}
