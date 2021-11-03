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

        private Modules.Reports.Model.PermitsSummaryMODEL _item;

        public Modules.Reports.Model.PermitsSummaryMODEL item
        {
            get { return _item; }
            set { _item = value; }
        }

        private ObservableCollection<Modules.Reports.Model.PermitsSummaryMODEL> dtSetRES;
        private ObservableCollection<Modules.Reports.Model.PermitsSummaryMODEL> dtSetCOM;
        private ObservableCollection<Modules.Reports.Model.PermitsSummaryMODEL> dtSetIND;
        private ObservableCollection<Modules.Reports.Model.PermitsSummaryMODEL> dtSetSocEd;
        private ObservableCollection<Modules.Reports.Model.PermitsSummaryMODEL> dtSetAGRI;
        private ObservableCollection<Modules.Reports.Model.PermitsSummaryMODEL> dtSetANCI;

        private int _sum;

        public int sum
        {
            get { return _sum; }
            set { _sum = value; }
        }


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
           


            dtSetRES = new ObservableCollection<Model.PermitsSummaryMODEL>();
            dtSetCOM = new ObservableCollection<Model.PermitsSummaryMODEL>();
            dtSetIND = new ObservableCollection<Model.PermitsSummaryMODEL>();
            dtSetSocEd = new ObservableCollection<Model.PermitsSummaryMODEL>();
            dtSetAGRI = new ObservableCollection<Model.PermitsSummaryMODEL>();
            dtSetANCI = new ObservableCollection<Model.PermitsSummaryMODEL>();

            CreateDataSet();
            ReportDataSource dsRES = new ReportDataSource("PermitsSummaryRES", dtSetRES);
            ReportDataSource dsCOM = new ReportDataSource("PermitsSummaryCOM", dtSetCOM);
            ReportDataSource dsIND = new ReportDataSource("PermitsSummaryIND", dtSetIND);
            ReportDataSource dsSocEd = new ReportDataSource("PermitsSummarySocEd", dtSetSocEd);
            ReportDataSource dsAGRI = new ReportDataSource("PermitsSummaryAGRI", dtSetAGRI);
            ReportDataSource dsANCI = new ReportDataSource("PermitsSummaryANCI", dtSetANCI);

            this.ReportForm.reportViewer1.LocalReport.DataSources.Clear();
            this.ReportForm.reportViewer1.LocalReport.DataSources.Add(dsRES);
            this.ReportForm.reportViewer1.LocalReport.DataSources.Add(dsCOM);
            this.ReportForm.reportViewer1.LocalReport.DataSources.Add(dsIND);
            this.ReportForm.reportViewer1.LocalReport.DataSources.Add(dsSocEd);
            this.ReportForm.reportViewer1.LocalReport.DataSources.Add(dsAGRI);
            this.ReportForm.reportViewer1.LocalReport.DataSources.Add(dsANCI);

        }

        private void CreateDataSet() //AFM 20191001
        {
            OracleResultSet result = new OracleResultSet();
            OracleResultSet result2 = new OracleResultSet();
            OracleResultSet result3 = new OracleResultSet();
            int Cnt = 0;

            //RESIDENTIAL
            result.Query = "select distinct(application.permit_code), permit_tbl.permit_desc from application,permit_tbl "+
                " where application.permit_code = permit_tbl.permit_code"+ 
                " and application.date_issued between '"+ string.Format("{0:dd-MMM-yy}", ReportForm.dtFrom) + "' and '"+ string.Format("{0:dd-MMM-yy}", ReportForm.dtTo) + "'"+
                " order by application.permit_code"; //determines permit
            if(result.Execute())
                while(result.Read())
                {
                    Cnt = 0;
                    item = new Model.PermitsSummaryMODEL();
                    item.PermitType = result.GetString("permit_desc");
                    result2.Query = "select * from application where permit_code = '"+ result.GetString("permit_code") +"' and category_code = '01' order by arn"; //determines category
                    if(result2.Execute())
                        while(result2.Read())
                        {
                            result3.Query = "select COALESCE(sum(p.fees_due), 0) from mrs_payments p,permit_tbl pt where pt.permit_code = '" + result.GetString("permit_code") + "' and substr (p.fees_code,0,2) = pt.fees_code and p.arn = '"+ result2.GetString("arn") +"'";
                            if(result3.Execute())
                                if(result3.Read())
                                {
                                    item.PermitValue += result3.GetDecimal(0);
                                }
                            Cnt += 1;
                        }
                    item.PermitCnt = Cnt;
                    dtSetRES.Add(item);
                }
            sum = dtSetRES.Sum(x => x.PermitCnt);
            if (sum == 0)
                dtSetRES = new ObservableCollection<PermitsSummaryMODEL>();

            //COMMERCIAL
            result.Query = "select distinct(application.permit_code), permit_tbl.permit_desc from application,permit_tbl " +
                " where application.permit_code = permit_tbl.permit_code" +
                " and application.date_issued between '" + string.Format("{0:dd-MMM-yy}", ReportForm.dtFrom) + "' and '" + string.Format("{0:dd-MMM-yy}", ReportForm.dtTo) + "'" +
                " order by application.permit_code"; //determines permit
            if (result.Execute())
                while (result.Read())
                {
                    Cnt = 0;
                    item = new Model.PermitsSummaryMODEL();
                    item.PermitType = result.GetString("permit_desc");
                    result2.Query = "select * from application where permit_code = '" + result.GetString("permit_code") + "' and category_code = '02' order by arn"; //determines category
                    if (result2.Execute())
                        while (result2.Read())
                        {
                            result3.Query = "select COALESCE(sum(p.fees_due), 0) from mrs_payments p,permit_tbl pt where pt.permit_code = '" + result.GetString("permit_code") + "' and substr (p.fees_code,0,2) = pt.fees_code and p.arn = '" + result2.GetString("arn") + "'";
                            if (result3.Execute())
                                if (result3.Read())
                                {
                                    item.PermitValue += result3.GetDecimal(0);
                                }
                            Cnt += 1;
                        }
                    item.PermitCnt = Cnt;
                    dtSetCOM.Add(item);
                }
            sum = dtSetCOM.Sum(x => x.PermitCnt);
            if (sum == 0)
                dtSetCOM = new ObservableCollection<PermitsSummaryMODEL>();

            //INDUSTRIAL
            result.Query = "select distinct(application.permit_code), permit_tbl.permit_desc from application,permit_tbl " +
                " where application.permit_code = permit_tbl.permit_code" +
                " and application.date_issued between '" + string.Format("{0:dd-MMM-yy}", ReportForm.dtFrom) + "' and '" + string.Format("{0:dd-MMM-yy}", ReportForm.dtTo) + "'" +
                " order by application.permit_code"; //determines permit
            if (result.Execute())
                while (result.Read())
                {
                    Cnt = 0;
                    item = new Model.PermitsSummaryMODEL();
                    item.PermitType = result.GetString("permit_desc");
                    result2.Query = "select * from application where permit_code = '" + result.GetString("permit_code") + "' and category_code = '03' order by arn"; //determines category
                    if (result2.Execute())
                        while (result2.Read())
                        {
                            result3.Query = "select COALESCE(sum(p.fees_due), 0) from mrs_payments p,permit_tbl pt where pt.permit_code = '" + result.GetString("permit_code") + "' and substr (p.fees_code,0,2) = pt.fees_code and p.arn = '" + result2.GetString("arn") + "'";
                            if (result3.Execute())
                                if (result3.Read())
                                {
                                    item.PermitValue += result3.GetDecimal(0);
                                }
                            Cnt += 1;
                        }
                    item.PermitCnt = Cnt;
                    dtSetIND.Add(item);
                }
            sum = dtSetIND.Sum(x => x.PermitCnt);
            if (sum == 0)
                dtSetIND = new ObservableCollection<PermitsSummaryMODEL>();

            //SOCIAL/EDUC
            result.Query = "select distinct(application.permit_code), permit_tbl.permit_desc from application,permit_tbl " +
                " where application.permit_code = permit_tbl.permit_code" +
                " and application.date_issued between '" + string.Format("{0:dd-MMM-yy}", ReportForm.dtFrom) + "' and '" + string.Format("{0:dd-MMM-yy}", ReportForm.dtTo) + "'" +
                " order by application.permit_code"; //determines permit
            if (result.Execute())
                while (result.Read())
                {
                    Cnt = 0;
                    item = new Model.PermitsSummaryMODEL();
                    item.PermitType = result.GetString("permit_desc");
                    result2.Query = "select * from application where permit_code = '" + result.GetString("permit_code") + "' and category_code = '04' order by arn"; //determines category
                    if (result2.Execute())
                        while (result2.Read())
                        {
                            result3.Query = "select COALESCE(sum(p.fees_due), 0) from mrs_payments p,permit_tbl pt where pt.permit_code = '" + result.GetString("permit_code") + "' and substr (p.fees_code,0,2) = pt.fees_code and p.arn = '" + result2.GetString("arn") + "'";
                            if (result3.Execute())
                                if (result3.Read())
                                {
                                    item.PermitValue += result3.GetDecimal(0);
                                }
                            Cnt += 1;
                        }
                    item.PermitCnt = Cnt;
                    dtSetSocEd.Add(item);
                }
            sum = dtSetSocEd.Sum(x => x.PermitCnt);
            if (sum == 0)
                dtSetSocEd = new ObservableCollection<PermitsSummaryMODEL>();

            //AGRICULTURAL
            result.Query = "select distinct(application.permit_code), permit_tbl.permit_desc from application,permit_tbl " +
                " where application.permit_code = permit_tbl.permit_code" +
                " and application.date_issued between '" + string.Format("{0:dd-MMM-yy}", ReportForm.dtFrom) + "' and '" + string.Format("{0:dd-MMM-yy}", ReportForm.dtTo) + "'" +
                " order by application.permit_code"; //determines permit
            if (result.Execute())
                while (result.Read())
                {
                    Cnt = 0;
                    item = new Model.PermitsSummaryMODEL();
                    item.PermitType = result.GetString("permit_desc");
                    result2.Query = "select * from application where permit_code = '" + result.GetString("permit_code") + "' and category_code = '05' order by arn"; //determines category
                    if (result2.Execute())
                        while (result2.Read())
                        {
                            result3.Query = "select COALESCE(sum(p.fees_due), 0) from mrs_payments p,permit_tbl pt where pt.permit_code = '" + result.GetString("permit_code") + "' and substr (p.fees_code,0,2) = pt.fees_code and p.arn = '" + result2.GetString("arn") + "'";
                            if (result3.Execute())
                                if (result3.Read())
                                {
                                    item.PermitValue += result3.GetDecimal(0);
                                }
                            Cnt += 1;
                        }
                    item.PermitCnt = Cnt;
                    dtSetAGRI.Add(item);
                }
            sum = dtSetAGRI.Sum(x => x.PermitCnt);
            if (sum == 0)
                dtSetAGRI = new ObservableCollection<PermitsSummaryMODEL>();

            //ANCILLARY
            result.Query = "select distinct(application.permit_code), permit_tbl.permit_desc from application,permit_tbl " +
                " where application.permit_code = permit_tbl.permit_code" +
                " and application.date_issued between '" + string.Format("{0:dd-MMM-yy}", ReportForm.dtFrom) + "' and '" + string.Format("{0:dd-MMM-yy}", ReportForm.dtTo) + "'" +
                " order by application.permit_code"; //determines permit
            if (result.Execute())
                while (result.Read())
                {
                    Cnt = 0;
                    item = new Model.PermitsSummaryMODEL();
                    item.PermitType = result.GetString("permit_desc");
                    result2.Query = "select * from application where permit_code = '" + result.GetString("permit_code") + "' and category_code = '06' order by arn"; //determines category
                    if (result2.Execute())
                        while (result2.Read())
                        {
                            result3.Query = "select COALESCE(sum(p.fees_due), 0) from mrs_payments p,permit_tbl pt where pt.permit_code = '" + result.GetString("permit_code") + "' and substr (p.fees_code,0,2) = pt.fees_code and p.arn = '" + result2.GetString("arn") + "'";
                            if (result3.Execute())
                                if (result3.Read())
                                {
                                    item.PermitValue += result3.GetDecimal(0);
                                }
                            Cnt += 1;
                        }
                    item.PermitCnt = Cnt;
                    dtSetANCI.Add(item);
                }
            sum = dtSetANCI.Sum(x => x.PermitCnt);
            if (sum == 0)
                dtSetANCI = new ObservableCollection<PermitsSummaryMODEL>();
        }
    }
}
