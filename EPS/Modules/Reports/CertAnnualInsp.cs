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
    public class CertAnnualInsp : FormReportClass
    {
        public CertAnnualInsp(frmReport Form) : base(Form)
        { }

        private string BldgOwner = string.Empty;
        private string BldgAdress = string.Empty;
        private DateTime DtIssued;
        private string EngrOfficial = string.Empty;

        private Modules.Reports.Model.CertificateMODEL _item;

        public Modules.Reports.Model.CertificateMODEL item
        {
            get { return _item; }
            set { _item = value; }
        }

        public override void LoadForm()
        {
            Microsoft.Reporting.WinForms.ReportParameter[] para = new Microsoft.Reporting.WinForms.ReportParameter[]
            {
                new Microsoft.Reporting.WinForms.ReportParameter("ReportName", ReportForm.ReportName.ToUpper()),
                new Microsoft.Reporting.WinForms.ReportParameter("LGUName", ConfigurationAttributes.LguName2),
                new Microsoft.Reporting.WinForms.ReportParameter("Province", ConfigurationAttributes.ProvinceName),
                new Microsoft.Reporting.WinForms.ReportParameter("DateIssued", AppSettingsManager.GetCurrentDate().ToShortDateString()),
                new Microsoft.Reporting.WinForms.ReportParameter("ZipCode", AppSettingsManager.GetConfigValue("28")),
            };
            ReportForm.reportViewer1.LocalReport.SetParameters(para);
        }
        private void GetData()
        {
            BldgOwner = item.BldgOwner;
        }
    }
    
}
