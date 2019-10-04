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
    public class ApprovedPermitApplication : FormReportClass
    {
        public ApprovedPermitApplication(frmReport Form) : base(Form)
        { }
        FormReportClass ReportClass = null;

        private Modules.Reports.Model.ApprovedPermitApplicationMODEL _item;

        public Modules.Reports.Model.ApprovedPermitApplicationMODEL item
        {
            get { return _item; }
            set { _item = value; }
        }

        private ObservableCollection<Modules.Reports.Model.ApprovedPermitApplicationMODEL> dtSetBLDG;
        private ObservableCollection<Modules.Reports.Model.ApprovedPermitApplicationMODEL> dtSetSNTRY;
        private ObservableCollection<Modules.Reports.Model.ApprovedPermitApplicationMODEL> dtSetELEC;
        private ObservableCollection<Modules.Reports.Model.ApprovedPermitApplicationMODEL> dtSetEXCAV;
        private ObservableCollection<Modules.Reports.Model.ApprovedPermitApplicationMODEL> dtSetFEN;
        private ObservableCollection<Modules.Reports.Model.ApprovedPermitApplicationMODEL> dtSetELECTRO;
        private ObservableCollection<Modules.Reports.Model.ApprovedPermitApplicationMODEL> dtSetMEKA;
        private ObservableCollection<Modules.Reports.Model.ApprovedPermitApplicationMODEL> dtSetSIGN;
        private ObservableCollection<Modules.Reports.Model.ApprovedPermitApplicationMODEL> dtSetPLUM;


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

            dtSetBLDG = new ObservableCollection<Model.ApprovedPermitApplicationMODEL>();
            dtSetSNTRY = new ObservableCollection<Model.ApprovedPermitApplicationMODEL>();
            dtSetELEC = new ObservableCollection<Model.ApprovedPermitApplicationMODEL>();
            dtSetEXCAV = new ObservableCollection<Model.ApprovedPermitApplicationMODEL>();
            dtSetFEN = new ObservableCollection<Model.ApprovedPermitApplicationMODEL>();
            dtSetELECTRO = new ObservableCollection<Model.ApprovedPermitApplicationMODEL>();
            dtSetMEKA = new ObservableCollection<Model.ApprovedPermitApplicationMODEL>();
            dtSetSIGN = new ObservableCollection<Model.ApprovedPermitApplicationMODEL>();
            dtSetPLUM = new ObservableCollection<Model.ApprovedPermitApplicationMODEL>();

            CreateDataSet();
            ReportDataSource dsBLDG = new ReportDataSource("ApprovedPermitDtSetRES", dtSetBLDG);
            ReportDataSource dsSNTRY = new ReportDataSource("ApprovedPermitDtSetSNTRY", dtSetSNTRY);
            ReportDataSource dsELEC = new ReportDataSource("ApprovedPermitDtSetELEC", dtSetELEC);
            ReportDataSource dsEXCAV = new ReportDataSource("ApprovedPermitDtSetEXCAV", dtSetEXCAV);
            ReportDataSource dsFEN = new ReportDataSource("ApprovedPermitDtSetFEN", dtSetFEN);
            ReportDataSource dsELECTRO = new ReportDataSource("ApprovedPermitDtSetELECTRO", dtSetELECTRO);
            ReportDataSource dsMEKA = new ReportDataSource("ApprovedPermitDtSetMEKA", dtSetMEKA);
            ReportDataSource dsSIGN = new ReportDataSource("ApprovedPermitDtSetSIGN", dtSetSIGN);
            ReportDataSource dsPLUM = new ReportDataSource("ApprovedPermitDtSetPLUM", dtSetPLUM);

            this.ReportForm.reportViewer1.LocalReport.DataSources.Clear();
            this.ReportForm.reportViewer1.LocalReport.DataSources.Add(dsBLDG);
            this.ReportForm.reportViewer1.LocalReport.DataSources.Add(dsSNTRY);
            this.ReportForm.reportViewer1.LocalReport.DataSources.Add(dsELEC);
            this.ReportForm.reportViewer1.LocalReport.DataSources.Add(dsEXCAV);
            this.ReportForm.reportViewer1.LocalReport.DataSources.Add(dsFEN);
            this.ReportForm.reportViewer1.LocalReport.DataSources.Add(dsELECTRO);
            this.ReportForm.reportViewer1.LocalReport.DataSources.Add(dsMEKA);
            this.ReportForm.reportViewer1.LocalReport.DataSources.Add(dsSIGN);
            this.ReportForm.reportViewer1.LocalReport.DataSources.Add(dsPLUM);

        }

        private void CreateDataSet() //AFM 20191001
        {
            OracleResultSet result = new OracleResultSet();
            OracleResultSet result2 = new OracleResultSet();

            //BUILDING 01
            result.Query = "select a.*, decode(b.est_cost, null, 0, b.est_cost) as est_cost , a.proj_hse_no|| ', ' ||a.proj_lot_no|| ', '||a.proj_blk_no|| ', ' ||proj_brgy AS REAL_PROJ_ADDR, c.acct_ln|| ', ' ||c.acct_fn|| ' ' ||c.acct_mi as REAL_PROJ_OWNER from application a, building b, account c where permit_code = '01'	and a.bldg_no = b.bldg_no and a.proj_owner = c.acct_code and a.date_issued between '" + string.Format("{0:dd-MMM-yy}", ReportForm.dtFrom) + "' and '" + string.Format("{0:dd-MMM-yy}", ReportForm.dtTo) + "' order by arn";
            if(result.Execute())
                while(result.Read())
                {
                    item = new Model.ApprovedPermitApplicationMODEL();
                    item.ProjOwner = result.GetString("REAL_PROJ_OWNER");
                    item.ProjAddr = result.GetString("REAL_PROJ_ADDR");
                    item.DtApplied = result.GetDateTime("DATE_APPLIED");
                    item.DtIssued = result.GetDateTime("DATE_ISSUED");
                    item.ProjCost = result.GetDecimal("EST_COST");
                    item.BldgPermitNo = result.GetString("PERMIT_NO");

                    result2.Query = "select sum(fees_due) AS AMT_PAID, or_no from mrs_payments where arn = '" + result.GetString("arn") +"' group by or_no";
                    if(result2.Execute())
                        if(result2.Read())
                        {
                            item.AmtPaid = result2.GetDecimal("AMT_PAID");
                            item.ORNo = result2.GetString("OR_NO");
                        }
                    dtSetBLDG.Add(item);
                }

            //SANITARY 02
            result.Query = "select a.*, decode(b.est_cost, null, 0, b.est_cost) as est_cost , a.proj_hse_no|| ', ' ||a.proj_lot_no|| ', '||a.proj_blk_no|| ', ' ||proj_brgy AS REAL_PROJ_ADDR, c.acct_ln|| ', ' ||c.acct_fn|| ' ' ||c.acct_mi as REAL_PROJ_OWNER from application a, building b, account c where permit_code = '02'	and a.bldg_no = b.bldg_no and a.proj_owner = c.acct_code and a.date_issued between '" + string.Format("{0:dd-MMM-yy}", ReportForm.dtFrom) + "' and '" + string.Format("{0:dd-MMM-yy}", ReportForm.dtTo) + "' order by arn";
            if (result.Execute())
                while (result.Read())
                {
                    item = new Model.ApprovedPermitApplicationMODEL();
                    item.ProjOwner = result.GetString("REAL_PROJ_OWNER");
                    item.ProjAddr = result.GetString("REAL_PROJ_ADDR");
                    item.DtApplied = result.GetDateTime("DATE_APPLIED");
                    item.DtIssued = result.GetDateTime("DATE_ISSUED");
                    item.ProjCost = result.GetDecimal("EST_COST");
                    item.BldgPermitNo = result.GetString("PERMIT_NO");

                    result2.Query = "select sum(fees_due) AS AMT_PAID, or_no from mrs_payments where arn = '" + result.GetString("arn") + "' group by or_no";
                    if (result2.Execute())
                        if (result2.Read())
                        {
                            item.AmtPaid = result2.GetDecimal("AMT_PAID");
                            item.ORNo = result2.GetString("OR_NO");
                        }
                    dtSetSNTRY.Add(item);
                }
            //ELECTRIC 03
            result.Query = "select a.*, decode(b.est_cost, null, 0, b.est_cost) as est_cost , a.proj_hse_no|| ', ' ||a.proj_lot_no|| ', '||a.proj_blk_no|| ', ' ||proj_brgy AS REAL_PROJ_ADDR, c.acct_ln|| ', ' ||c.acct_fn|| ' ' ||c.acct_mi as REAL_PROJ_OWNER from application a, building b, account c where permit_code = '03'	and a.bldg_no = b.bldg_no and a.proj_owner = c.acct_code and a.date_issued between '" + string.Format("{0:dd-MMM-yy}", ReportForm.dtFrom) + "' and '" + string.Format("{0:dd-MMM-yy}", ReportForm.dtTo) + "' order by arn";
            if (result.Execute())
                while (result.Read())
                {
                    item = new Model.ApprovedPermitApplicationMODEL();
                    item.ProjOwner = result.GetString("REAL_PROJ_OWNER");
                    item.ProjAddr = result.GetString("REAL_PROJ_ADDR");
                    item.DtApplied = result.GetDateTime("DATE_APPLIED");
                    item.DtIssued = result.GetDateTime("DATE_ISSUED");
                    item.ProjCost = result.GetDecimal("EST_COST");
                    item.BldgPermitNo = result.GetString("PERMIT_NO");

                    result2.Query = "select sum(fees_due) AS AMT_PAID, or_no from mrs_payments where arn = '" + result.GetString("arn") + "' group by or_no";
                    if (result2.Execute())
                        if (result2.Read())
                        {
                            item.AmtPaid = result2.GetDecimal("AMT_PAID");
                            item.ORNo = result2.GetString("OR_NO");
                        }
                    dtSetELEC.Add(item);
                }

            //EXCAVATION 04
            result.Query = "select a.*, decode(b.est_cost, null, 0, b.est_cost) as est_cost , a.proj_hse_no|| ', ' ||a.proj_lot_no|| ', '||a.proj_blk_no|| ', ' ||proj_brgy AS REAL_PROJ_ADDR, c.acct_ln|| ', ' ||c.acct_fn|| ' ' ||c.acct_mi as REAL_PROJ_OWNER from application a, building b, account c where permit_code = '04'	and a.bldg_no = b.bldg_no and a.proj_owner = c.acct_code and a.date_issued between '" + string.Format("{0:dd-MMM-yy}", ReportForm.dtFrom) + "' and '" + string.Format("{0:dd-MMM-yy}", ReportForm.dtTo) + "' order by arn";
            if (result.Execute())
                while (result.Read())
                {
                    item = new Model.ApprovedPermitApplicationMODEL();
                    item.ProjOwner = result.GetString("REAL_PROJ_OWNER");
                    item.ProjAddr = result.GetString("REAL_PROJ_ADDR");
                    item.DtApplied = result.GetDateTime("DATE_APPLIED");
                    item.DtIssued = result.GetDateTime("DATE_ISSUED");
                    item.ProjCost = result.GetDecimal("EST_COST");
                    item.BldgPermitNo = result.GetString("PERMIT_NO");

                    result2.Query = "select sum(fees_due) AS AMT_PAID, or_no from mrs_payments where arn = '" + result.GetString("arn") + "' group by or_no";
                    if (result2.Execute())
                        if (result2.Read())
                        {
                            item.AmtPaid = result2.GetDecimal("AMT_PAID");
                            item.ORNo = result2.GetString("OR_NO");
                        }
                    dtSetEXCAV.Add(item);
                }

            //FENCING 05
            result.Query = "select a.*, decode(b.est_cost, null, 0, b.est_cost) as est_cost , a.proj_hse_no|| ', ' ||a.proj_lot_no|| ', '||a.proj_blk_no|| ', ' ||proj_brgy AS REAL_PROJ_ADDR, c.acct_ln|| ', ' ||c.acct_fn|| ' ' ||c.acct_mi as REAL_PROJ_OWNER from application a, building b, account c where permit_code = '05'	and a.bldg_no = b.bldg_no and a.proj_owner = c.acct_code and a.date_issued between '" + string.Format("{0:dd-MMM-yy}", ReportForm.dtFrom) + "' and '" + string.Format("{0:dd-MMM-yy}", ReportForm.dtTo) + "' order by arn";
            if (result.Execute())
                while (result.Read())
                {
                    item = new Model.ApprovedPermitApplicationMODEL();
                    item.ProjOwner = result.GetString("REAL_PROJ_OWNER");
                    item.ProjAddr = result.GetString("REAL_PROJ_ADDR");
                    item.DtApplied = result.GetDateTime("DATE_APPLIED");
                    item.DtIssued = result.GetDateTime("DATE_ISSUED");
                    item.ProjCost = result.GetDecimal("EST_COST");
                    item.BldgPermitNo = result.GetString("PERMIT_NO");

                    result2.Query = "select sum(fees_due) AS AMT_PAID, or_no from mrs_payments where arn = '" + result.GetString("arn") + "' group by or_no";
                    if (result2.Execute())
                        if (result2.Read())
                        {
                            item.AmtPaid = result2.GetDecimal("AMT_PAID");
                            item.ORNo = result2.GetString("OR_NO");
                        }
                    dtSetFEN.Add(item);
                }

            //MECHANICAL 06
            result.Query = "select a.*, decode(b.est_cost, null, 0, b.est_cost) as est_cost , a.proj_hse_no|| ', ' ||a.proj_lot_no|| ', '||a.proj_blk_no|| ', ' ||proj_brgy AS REAL_PROJ_ADDR, c.acct_ln|| ', ' ||c.acct_fn|| ' ' ||c.acct_mi as REAL_PROJ_OWNER from application a, building b, account c where permit_code = '06'	and a.bldg_no = b.bldg_no and a.proj_owner = c.acct_code and a.date_issued between '" + string.Format("{0:dd-MMM-yy}", ReportForm.dtFrom) + "' and '" + string.Format("{0:dd-MMM-yy}", ReportForm.dtTo) + "' order by arn";
            if (result.Execute())
                while (result.Read())
                {
                    item = new Model.ApprovedPermitApplicationMODEL();
                    item.ProjOwner = result.GetString("REAL_PROJ_OWNER");
                    item.ProjAddr = result.GetString("REAL_PROJ_ADDR");
                    item.DtApplied = result.GetDateTime("DATE_APPLIED");
                    item.DtIssued = result.GetDateTime("DATE_ISSUED");
                    item.ProjCost = result.GetDecimal("EST_COST");
                    item.BldgPermitNo = result.GetString("PERMIT_NO");

                    result2.Query = "select sum(fees_due) AS AMT_PAID, or_no from mrs_payments where arn = '" + result.GetString("arn") + "' group by or_no";
                    if (result2.Execute())
                        if (result2.Read())
                        {
                            item.AmtPaid = result2.GetDecimal("AMT_PAID");
                            item.ORNo = result2.GetString("OR_NO");
                        }
                    dtSetMEKA.Add(item);
                }

            //ELECTRONICS 07
            result.Query = "select a.*, decode(b.est_cost, null, 0, b.est_cost) as est_cost , a.proj_hse_no|| ', ' ||a.proj_lot_no|| ', '||a.proj_blk_no|| ', ' ||proj_brgy AS REAL_PROJ_ADDR, c.acct_ln|| ', ' ||c.acct_fn|| ' ' ||c.acct_mi as REAL_PROJ_OWNER from application a, building b, account c where permit_code = '07'	and a.bldg_no = b.bldg_no and a.proj_owner = c.acct_code and a.date_issued between '" + string.Format("{0:dd-MMM-yy}", ReportForm.dtFrom) + "' and '" + string.Format("{0:dd-MMM-yy}", ReportForm.dtTo) + "' order by arn";
            if (result.Execute())
                while (result.Read())
                {
                    item = new Model.ApprovedPermitApplicationMODEL();
                    item.ProjOwner = result.GetString("REAL_PROJ_OWNER");
                    item.ProjAddr = result.GetString("REAL_PROJ_ADDR");
                    item.DtApplied = result.GetDateTime("DATE_APPLIED");
                    item.DtIssued = result.GetDateTime("DATE_ISSUED");
                    item.ProjCost = result.GetDecimal("EST_COST");
                    item.BldgPermitNo = result.GetString("PERMIT_NO");

                    result2.Query = "select sum(fees_due) AS AMT_PAID, or_no from mrs_payments where arn = '" + result.GetString("arn") + "' group by or_no";
                    if (result2.Execute())
                        if (result2.Read())
                        {
                            item.AmtPaid = result2.GetDecimal("AMT_PAID");
                            item.ORNo = result2.GetString("OR_NO");
                        }
                    dtSetELECTRO.Add(item);
                }

            //ELECTRONICS 08
            result.Query = "select a.*, decode(b.est_cost, null, 0, b.est_cost) as est_cost , a.proj_hse_no|| ', ' ||a.proj_lot_no|| ', '||a.proj_blk_no|| ', ' ||proj_brgy AS REAL_PROJ_ADDR, c.acct_ln|| ', ' ||c.acct_fn|| ' ' ||c.acct_mi as REAL_PROJ_OWNER from application a, building b, account c where permit_code = '08'	and a.bldg_no = b.bldg_no and a.proj_owner = c.acct_code and a.date_issued between '" + string.Format("{0:dd-MMM-yy}", ReportForm.dtFrom) + "' and '" + string.Format("{0:dd-MMM-yy}", ReportForm.dtTo) + "' order by arn";
            if (result.Execute())
                while (result.Read())
                {
                    item = new Model.ApprovedPermitApplicationMODEL();
                    item.ProjOwner = result.GetString("REAL_PROJ_OWNER");
                    item.ProjAddr = result.GetString("REAL_PROJ_ADDR");
                    item.DtApplied = result.GetDateTime("DATE_APPLIED");
                    item.DtIssued = result.GetDateTime("DATE_ISSUED");
                    item.ProjCost = result.GetDecimal("EST_COST");
                    item.BldgPermitNo = result.GetString("PERMIT_NO");

                    result2.Query = "select sum(fees_due) AS AMT_PAID, or_no from mrs_payments where arn = '" + result.GetString("arn") + "' group by or_no";
                    if (result2.Execute())
                        if (result2.Read())
                        {
                            item.AmtPaid = result2.GetDecimal("AMT_PAID");
                            item.ORNo = result2.GetString("OR_NO");
                        }
                    dtSetSIGN.Add(item);
                }
            //ELECTRONICS 09
            result.Query = "select a.*, decode(b.est_cost, null, 0, b.est_cost) as est_cost , a.proj_hse_no|| ', ' ||a.proj_lot_no|| ', '||a.proj_blk_no|| ', ' ||proj_brgy AS REAL_PROJ_ADDR, c.acct_ln|| ', ' ||c.acct_fn|| ' ' ||c.acct_mi as REAL_PROJ_OWNER from application a, building b, account c where permit_code = '09'	and a.bldg_no = b.bldg_no and a.proj_owner = c.acct_code and a.date_issued between '" + string.Format("{0:dd-MMM-yy}", ReportForm.dtFrom) + "' and '" + string.Format("{0:dd-MMM-yy}", ReportForm.dtTo) + "' order by arn";
            if (result.Execute())
                while (result.Read())
                {
                    item = new Model.ApprovedPermitApplicationMODEL();
                    item.ProjOwner = result.GetString("REAL_PROJ_OWNER");
                    item.ProjAddr = result.GetString("REAL_PROJ_ADDR");
                    item.DtApplied = result.GetDateTime("DATE_APPLIED");
                    item.DtIssued = result.GetDateTime("DATE_ISSUED");
                    item.ProjCost = result.GetDecimal("EST_COST");
                    item.BldgPermitNo = result.GetString("PERMIT_NO");

                    result2.Query = "select sum(fees_due) AS AMT_PAID, or_no from mrs_payments where arn = '" + result.GetString("arn") + "' group by or_no";
                    if (result2.Execute())
                        if (result2.Read())
                        {
                            item.AmtPaid = result2.GetDecimal("AMT_PAID");
                            item.ORNo = result2.GetString("OR_NO");
                        }
                    dtSetPLUM.Add(item);
                }
        }
    }
}
