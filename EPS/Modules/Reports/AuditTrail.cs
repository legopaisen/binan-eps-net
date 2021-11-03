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
    public class AuditTrail : FormReportClass
    {
        public AuditTrail(frmReport Form) : base(Form)
        { }

        private ObservableCollection<TRAIL> dtSet;

        private TRAIL _item;

        public TRAIL item
        {
            get { return _item; }
            set { _item = value; }
        }

        public override void LoadForm()
        {
            Microsoft.Reporting.WinForms.ReportParameter[] para = new Microsoft.Reporting.WinForms.ReportParameter[]
            {
                new Microsoft.Reporting.WinForms.ReportParameter("sPrintedBy", AppSettingsManager.SystemUser.UserCode.ToString()),
                new Microsoft.Reporting.WinForms.ReportParameter("dtDateTime", AppSettingsManager.GetSystemDate().ToShortDateString()),
                new Microsoft.Reporting.WinForms.ReportParameter("dtSelected", ReportForm.dtFrom.ToShortDateString() + " - " + ReportForm.dtTo.ToShortDateString()),
            };
            ReportForm.reportViewer1.LocalReport.SetParameters(para);

            dtSet = new ObservableCollection<TRAIL>();
            CreateDataSet();
            ReportDataSource ds = new ReportDataSource("TrailDataset", dtSet);

            this.ReportForm.reportViewer1.LocalReport.DataSources.Clear();
            this.ReportForm.reportViewer1.LocalReport.DataSources.Add(ds);

        }

        private void CreateDataSet() //AFM 20191001
        {
            OracleResultSet result = new OracleResultSet();
            result.Query = "select t.*, m.module_desc from trail t, modules m where t.module_code = m.module_code and ";
            if(ReportForm.TrailMode == "User")
                result.Query += "t.user_code = '"+ ReportForm.User +"' ";
            else
                result.Query += "m.module_desc = '" + ReportForm.Module.Trim() + "' ";

            result.Query += "and trans_date between to_date('" + ReportForm.dtFrom.ToShortDateString() + "', 'MM-DD-RR') and to_date('" + ReportForm.dtTo.ToShortDateString() + "', 'MM-DD-RR') "; //AFM 20200827
            result.Query += "order by t.module_code";

            if(result.Execute())
                while(result.Read())
                {
                    item = new TRAIL();
                    item.USER_CODE = result.GetString("user_code");
                    item.MODULE_CODE = result.GetString("module_code");
                    item.TRANS_DATE = result.GetDateTime("trans_date");
                    item.TRANS_TIME = result.GetString("trans_time");
                    item.MODULE_DESC = result.GetString("module_desc");
                    item.OBJECT_FLD = result.GetString("object_fld");

                    dtSet.Add(item);
                }
        }
    }
}
