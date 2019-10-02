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
using Modules.Reports.Model;
using Common.DataConnector;
using System.Collections.ObjectModel;

namespace Modules.Reports
{
    public class PermitsSummary : FormReportClass
    {
        public PermitsSummary(frmReport Form) : base(Form)
        { }

        FormReportClass ReportClass = null;

        public override void LoadForm()
        {
            Microsoft.Reporting.WinForms.ReportParameter[] para = new Microsoft.Reporting.WinForms.ReportParameter[]
           {
                new Microsoft.Reporting.WinForms.ReportParameter("ReportName", ReportForm.ReportName.ToUpper()),
                new Microsoft.Reporting.WinForms.ReportParameter("LGUName", ConfigurationAttributes.LguName2),
                new Microsoft.Reporting.WinForms.ReportParameter("Province", ConfigurationAttributes.ProvinceName),
                new Microsoft.Reporting.WinForms.ReportParameter("DateRange", ReportForm.dtFrom.ToShortDateString() + " - " + ReportForm.dtTo.ToShortDateString()),
           };
            ReportForm.reportViewer1.LocalReport.SetParameters(para);
        }

    }
}
