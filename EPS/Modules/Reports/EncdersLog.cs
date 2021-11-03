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
    public class EncodersLog : FormReportClass
    {
        public EncodersLog(frmReport Form) : base(Form)
        { }


        private ObservableCollection<Modules.Reports.Model.EncoderLog> dtSet;

        private Modules.Reports.Model.EncoderLog _item;

        public Modules.Reports.Model.EncoderLog item
        {
            get { return _item; }
            set { _item = value; }
        }


        public override void LoadForm()
        {
            Microsoft.Reporting.WinForms.ReportParameter[] para = new Microsoft.Reporting.WinForms.ReportParameter[]
            {
                new Microsoft.Reporting.WinForms.ReportParameter("dtDateTime", AppSettingsManager.GetSystemDate().ToShortDateString()),
                new Microsoft.Reporting.WinForms.ReportParameter("dtSelected", ReportForm.dtFrom.ToShortDateString() + " - " + ReportForm.dtTo.ToShortDateString()),
                new Microsoft.Reporting.WinForms.ReportParameter("sLGU", AppSettingsManager.GetConfigValue("02")),
            };
            ReportForm.reportViewer1.LocalReport.SetParameters(para);

            dtSet = new ObservableCollection<Modules.Reports.Model.EncoderLog>();
            CreateDataSet();
            ReportDataSource ds = new ReportDataSource("EncoderLogDtSet", dtSet);

            this.ReportForm.reportViewer1.LocalReport.DataSources.Clear();
            this.ReportForm.reportViewer1.LocalReport.DataSources.Add(ds);

        }

        private void CreateDataSet() //AFM 20191001
        {
            OracleResultSet result = new OracleResultSet();
            OracleResultSet result2 = new OracleResultSet();
            OracleResultSet result3 = new OracleResultSet();

            string sArn = string.Empty;


            result.Query = "select user_code from users order by user_code ";
            if (result.Execute())
                while (result.Read())
                {
                    item = new Model.EncoderLog();
                    item.USER = result.GetString("user_code").Trim();

                    int intCount = 0;
                    result2.CreateBlobConnection();
                    result2.Query = "select arn from docblob_twopage where ";
                    result2.Query += " upload_by = '" + item.USER + "' ";
                    result2.Query += " and upload_dt between to_date('" + ReportForm.dtFrom.ToShortDateString() + "', 'MM-DD-RR') and to_date('" + ReportForm.dtTo.ToShortDateString() + "', 'MM-DD-RR')";
                    if (result2.Execute())
                        while(result2.Read())
                        {
                            result3.Query = "select * from application where arn = '"+ result2.GetString("arn") + "'"; //counter check record in application
                            if(result3.Execute())
                                if(result3.Read())
                                {;
                                    intCount += 1;
                                }
                            result3.Close();
                        }

                    result3.Query = "select USER_LN || ', ' || USER_FN || ', ' || USER_MI as name from users where user_code = '" + item.USER + "'";
                    if (result3.Execute())
                        if (result3.Read())
                        {
                            item.FULLNAME = result3.GetString("name");
                        }
                    result3.Close();
                    item.COUNT = intCount;

                    result2.Close();
                    dtSet.Add(item);
                }
            result.Close();
        }
    }
}

