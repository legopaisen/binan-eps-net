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
    public class CertOccupancyBinan : FormReportClass
    {
        public CertOccupancyBinan(frmReport Form) : base(Form)
        { }

        public override void LoadForm()
        {

            OracleResultSet res = new OracleResultSet();
            OracleResultSet res2 = new OracleResultSet();
            DateTime dtOR = AppSettingsManager.GetSystemDate();

            //permit code, owner code, and proj info
            string sPermitCode = string.Empty;
            string sOwnerCode = string.Empty;
            string sProjName = string.Empty;
            string sProjAddr = string.Empty;
            string sDtCompletion = string.Empty;
            res.Query = $"select * from application where arn = '{ReportForm.An}' and permit_code in (select permit_code from permit_tbl where permit_desc like '%OCCUPANCY%')";
            if (res.Execute())
                if (res.Read())
                {
                    sPermitCode = res.GetString("permit_code");
                    sOwnerCode = res.GetString("proj_owner");
                    sProjName = res.GetString("proj_desc");
                    sProjAddr = res.GetString("proj_hse_no") + " " + res.GetString("proj_lot_no") + " " + res.GetString("proj_blk_no") + " " + res.GetString("proj_addr") + " " + res.GetString("proj_brgy") + ", " + res.GetString("proj_city") + " CITY";
                    sDtCompletion = res.GetDateTime("prop_complete").ToShortDateString();
                }
            res.Close();

            //bldg permit no and date
            string sBldgPermitNo = string.Empty;
            string sBldgPermitDt = string.Empty;
            res.Query = $"select permit_no, date_issued from application where arn = '{ReportForm.An}' and permit_code = '01'"; //default 01 for building permit
            if (res.Execute())
                if (res.Read())
                {
                    sBldgPermitNo = res.GetString("permit_no");
                    sBldgPermitDt = res.GetDateTime("date_issued").ToShortDateString();
                }
            res.Close();

            //or date and or amount
            string sORNo = string.Empty;
            string sFeesAmt = string.Empty;
            res.Query = $"select or_no, or_date, nvl(sum(fees_amt_due),0) as fees_amt from payments_info where refno = '{ReportForm.An}' and permit_code = '{sPermitCode}' group by or_no, or_date";
            if (res.Execute())
                if (res.Read())
                {
                    dtOR = res.GetDateTime("or_date");
                    sORNo = res.GetString("or_no");
                    sFeesAmt = res.GetDouble("fees_amt").ToString();
                }
            res.Close();

            //occupancy description
            string sOccu = string.Empty;
            res.Query = $"select distinct APP.*, PT.permit_desc from application APP, permit_tbl PT where APP.permit_code = PT.permit_code and PT.permit_desc = 'BUILDING PERMIT' and APP.arn = '{ReportForm.An}'";
            if (res.Execute())
                if (res.Read())
                {
                    res2.Query = $"select occ_desc from OCCUPANCY_TBL where occ_code = '{res.GetString("occupancy_code")}'";
                    if (res2.Execute())
                        if (res2.Read())
                        {
                            sOccu = res2.GetString("occ_desc");
                        }
                    res2.Close();
                }
            res.Close();


            string sOwnerName = string.Empty;
            Accounts account = new Accounts();
            account.GetOwner(sOwnerCode);       
            sOwnerName = account.FirstName + " " + account.MiddleInitial + ". " + account.LastName;


            Microsoft.Reporting.WinForms.ReportParameter[] para = new Microsoft.Reporting.WinForms.ReportParameter[]
            {
                new Microsoft.Reporting.WinForms.ReportParameter("sCertNo", ReportForm.CertNo),
                new Microsoft.Reporting.WinForms.ReportParameter("sFSICNo", ReportForm.FSICNo),
                new Microsoft.Reporting.WinForms.ReportParameter("dtCertDt", ReportForm.CertDtIssued.ToShortDateString()),
                new Microsoft.Reporting.WinForms.ReportParameter("dtFSICDt", ReportForm.FSICDtIssued.ToShortDateString()),
                new Microsoft.Reporting.WinForms.ReportParameter("dtDateCompletion", sDtCompletion),
                new Microsoft.Reporting.WinForms.ReportParameter("sORNo", sORNo),
                new Microsoft.Reporting.WinForms.ReportParameter("dtORDate", dtOR.ToShortDateString()),
                new Microsoft.Reporting.WinForms.ReportParameter("sOwnerName", sOwnerName),
                new Microsoft.Reporting.WinForms.ReportParameter("sProjName", sProjName),
                new Microsoft.Reporting.WinForms.ReportParameter("sProjAddr", sProjAddr),
                new Microsoft.Reporting.WinForms.ReportParameter("sOccupancy", sOccu),
                new Microsoft.Reporting.WinForms.ReportParameter("fFeesPaid", sFeesAmt),
                new Microsoft.Reporting.WinForms.ReportParameter("sBldgPermitNo", sBldgPermitNo),
                new Microsoft.Reporting.WinForms.ReportParameter("dtBldgPermit", sBldgPermitDt),
                new Microsoft.Reporting.WinForms.ReportParameter("dtToday", AppSettingsManager.GetSystemDate().ToShortDateString()),
                new Microsoft.Reporting.WinForms.ReportParameter("PrePrinted", ReportForm.isPrePrint.ToString())
            };
            ReportForm.reportViewer1.LocalReport.SetParameters(para);

        }
    }
}
