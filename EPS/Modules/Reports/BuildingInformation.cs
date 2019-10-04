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
    public class BuildingInformation : FormReportClass
    {
        public BuildingInformation(frmReport Form) : base(Form)
        { }

        FormReportClass ReportClass = null;

        private ObservableCollection<Modules.Reports.Model.BuildingInformation> dtSet;
        private ObservableCollection<Modules.Reports.Model.BuildingInformation> dtSetCom;
        private ObservableCollection<Modules.Reports.Model.BuildingInformation> dtSetInd;
        private ObservableCollection<Modules.Reports.Model.BuildingInformation> dtSetSocEd;
        private ObservableCollection<Modules.Reports.Model.BuildingInformation> dtSetAgri;
        private ObservableCollection<Modules.Reports.Model.BuildingInformation> dtSetAnci;

        private Modules.Reports.Model.BuildingInformation _item;

        public Modules.Reports.Model.BuildingInformation item
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
                new Microsoft.Reporting.WinForms.ReportParameter("DateRange", ReportForm.dtFrom.ToShortDateString() + " - " + ReportForm.dtTo.ToShortDateString()),
            };
            ReportForm.reportViewer1.LocalReport.SetParameters(para);

            dtSet = new ObservableCollection<Model.BuildingInformation>();
            dtSetCom = new ObservableCollection<Model.BuildingInformation>();
            dtSetInd = new ObservableCollection<Model.BuildingInformation>();
            dtSetSocEd = new ObservableCollection<Model.BuildingInformation>();
            dtSetAgri = new ObservableCollection<Model.BuildingInformation>();
            dtSetAnci = new ObservableCollection<Model.BuildingInformation>();

            CreateDataSet();
            ReportDataSource ds = new ReportDataSource("BldgInfoDataset", dtSet);
            ReportDataSource dsCom = new ReportDataSource("BldgInfoDatasetCOM", dtSetCom);
            ReportDataSource dsInd = new ReportDataSource("BldgInfoDatasetIND", dtSetInd);
            ReportDataSource dsSocEd = new ReportDataSource("BldgInfoDatasetSocEd", dtSetSocEd);
            ReportDataSource dsAgri = new ReportDataSource("BldgInfoDatasetAGRI", dtSetAgri);
            ReportDataSource dsAnci = new ReportDataSource("BldgInfoDatasetANCI", dtSetAnci);

            this.ReportForm.reportViewer1.LocalReport.DataSources.Clear();
            this.ReportForm.reportViewer1.LocalReport.DataSources.Add(ds);
            this.ReportForm.reportViewer1.LocalReport.DataSources.Add(dsCom);
            this.ReportForm.reportViewer1.LocalReport.DataSources.Add(dsInd);
            this.ReportForm.reportViewer1.LocalReport.DataSources.Add(dsSocEd);
            this.ReportForm.reportViewer1.LocalReport.DataSources.Add(dsAgri);
            this.ReportForm.reportViewer1.LocalReport.DataSources.Add(dsAnci);

        }

        private void CreateDataSet() //AFM 20191001
        {
            OracleResultSet result = new OracleResultSet();
            OracleResultSet result2 = new OracleResultSet();
            //RESIDENTIAL
            result.Query = "select AP.*, AC.acct_ln || ', ' || AC.acct_fn || ' ' || AC.acct_mi as REAL_PROJ_OWNER, " +
                " AC.acct_hse_no || ',' || AC.acct_lot_no || ',' || AC.acct_blk_no || ',' || AC.acct_addr || ',' || AC.acct_brgy AS REAL_PROJ_ADDR " +
                "from application AP, account AC " +
                " where AP.proj_owner = AC.acct_code and AP.category_code = '01' and AP.date_applied between '"+ string.Format("{0:dd-MMM-yy}", ReportForm.dtFrom) + "' and '" + string.Format("{0:dd-MMM-yy}", ReportForm.dtTo) + "'";
            if(result.Execute())
                while(result.Read())
                {
                    item = new Model.BuildingInformation();
                    item.OwnerName = result.GetString("REAL_PROJ_OWNER");
                    item.ProjDesc = result.GetString("PROJ_DESC");
                    item.ProjLocation = result.GetString("REAL_PROJ_ADDR");

                    result2.Query = "select BD.*, MT.material_desc from building BD, material_tbl MT where BD.material_code = MT.material_code and bldg_no = '"+ result.GetInt("BLDG_NO") +"'";
                    if(result2.Execute())
                        if(result2.Read())
                        {
                            item.BldgMaterial = result2.GetString("MATERIAL_DESC");
                            item.BldgHeight = result2.GetDecimal("BLDG_HEIGHT");
                            item.Area = result2.GetDecimal("TOTAL_FLR_AREA");
                            item.EstiCost = result2.GetDecimal("EST_COST");
                            item.ActualCost = result2.GetDecimal("ACTUAL_COST");
                        }
                    item.DtIssued = result.GetDateTime("DATE_ISSUED");
                    item.BldgPermitNo = result.GetString("PERMIT_NO");

                    dtSet.Add(item);
                }

            //COMMERCIAL
            result.Query = "select AP.*, AC.acct_ln || ', ' || AC.acct_fn || ' ' || AC.acct_mi as REAL_PROJ_OWNER, " +
                " AC.acct_hse_no || ',' || AC.acct_lot_no || ',' || AC.acct_blk_no || ',' || AC.acct_addr || ',' || AC.acct_brgy AS REAL_PROJ_ADDR " +
                "from application AP, account AC " +
                " where AP.proj_owner = AC.acct_code and AP.category_code = '02' and AP.date_applied between '" + string.Format("{0:dd-MMM-yy}", ReportForm.dtFrom) + "' and '" + string.Format("{0:dd-MMM-yy}", ReportForm.dtTo) + "'";
            if (result.Execute())
                while (result.Read())
                {
                    item = new Model.BuildingInformation();
                    item.OwnerName = result.GetString("REAL_PROJ_OWNER");
                    item.ProjDesc = result.GetString("PROJ_DESC");
                    item.ProjLocation = result.GetString("REAL_PROJ_ADDR");

                    result2.Query = "select BD.*, MT.material_desc from building BD, material_tbl MT where BD.material_code = MT.material_code and bldg_no = '" + result.GetInt("BLDG_NO") + "'";
                    if (result2.Execute())
                        if (result2.Read())
                        {
                            item.BldgMaterial = result2.GetString("MATERIAL_DESC");
                            item.BldgHeight = result2.GetDecimal("BLDG_HEIGHT");
                            item.Area = result2.GetDecimal("TOTAL_FLR_AREA");
                            item.EstiCost = result2.GetDecimal("EST_COST");
                            item.ActualCost = result2.GetDecimal("ACTUAL_COST");
                        }
                    item.DtIssued = result.GetDateTime("DATE_APPLIED");
                    item.BldgPermitNo = result.GetString("PERMIT_NO");

                    dtSetCom.Add(item);
                }

            //INDUSTRIAL
            result.Query = "select AP.*, AC.acct_ln || ', ' || AC.acct_fn || ' ' || AC.acct_mi as REAL_PROJ_OWNER, " +
                " AC.acct_hse_no || ',' || AC.acct_lot_no || ',' || AC.acct_blk_no || ',' || AC.acct_addr || ',' || AC.acct_brgy AS REAL_PROJ_ADDR " +
                "from application AP, account AC " +
                " where AP.proj_owner = AC.acct_code and AP.category_code = '03' and AP.date_applied between '" + string.Format("{0:dd-MMM-yy}", ReportForm.dtFrom) + "' and '" + string.Format("{0:dd-MMM-yy}", ReportForm.dtTo) + "'";
            if (result.Execute())
                while (result.Read())
                {
                    item = new Model.BuildingInformation();
                    item.OwnerName = result.GetString("REAL_PROJ_OWNER");
                    item.ProjDesc = result.GetString("PROJ_DESC");
                    item.ProjLocation = result.GetString("REAL_PROJ_ADDR");

                    result2.Query = "select BD.*, MT.material_desc from building BD, material_tbl MT where BD.material_code = MT.material_code and bldg_no = '" + result.GetInt("BLDG_NO") + "'";
                    if (result2.Execute())
                        if (result2.Read())
                        {
                            item.BldgMaterial = result2.GetString("MATERIAL_DESC");
                            item.BldgHeight = result2.GetDecimal("BLDG_HEIGHT");
                            item.Area = result2.GetDecimal("TOTAL_FLR_AREA");
                            item.EstiCost = result2.GetDecimal("EST_COST");
                            item.ActualCost = result2.GetDecimal("ACTUAL_COST");
                        }
                    item.DtIssued = result.GetDateTime("DATE_APPLIED");
                    item.BldgPermitNo = result.GetString("PERMIT_NO");

                    dtSetInd.Add(item);
                }

            //SOCIAL/EDUC
            result.Query = "select AP.*, AC.acct_ln || ', ' || AC.acct_fn || ' ' || AC.acct_mi as REAL_PROJ_OWNER, " +
                " AC.acct_hse_no || ',' || AC.acct_lot_no || ',' || AC.acct_blk_no || ',' || AC.acct_addr || ',' || AC.acct_brgy AS REAL_PROJ_ADDR " +
                "from application AP, account AC " +
                " where AP.proj_owner = AC.acct_code and AP.category_code = '04' and AP.date_applied between '" + string.Format("{0:dd-MMM-yy}", ReportForm.dtFrom) + "' and '" + string.Format("{0:dd-MMM-yy}", ReportForm.dtTo) + "'";
            if (result.Execute())
                while (result.Read())
                {
                    item = new Model.BuildingInformation();
                    item.OwnerName = result.GetString("REAL_PROJ_OWNER");
                    item.ProjDesc = result.GetString("PROJ_DESC");
                    item.ProjLocation = result.GetString("REAL_PROJ_ADDR");

                    result2.Query = "select BD.*, MT.material_desc from building BD, material_tbl MT where BD.material_code = MT.material_code and bldg_no = '" + result.GetInt("BLDG_NO") + "'";
                    if (result2.Execute())
                        if (result2.Read())
                        {
                            item.BldgMaterial = result2.GetString("MATERIAL_DESC");
                            item.BldgHeight = result2.GetDecimal("BLDG_HEIGHT");
                            item.Area = result2.GetDecimal("TOTAL_FLR_AREA");
                            item.EstiCost = result2.GetDecimal("EST_COST");
                            item.ActualCost = result2.GetDecimal("ACTUAL_COST");
                        }
                    item.DtIssued = result.GetDateTime("DATE_APPLIED");
                    item.BldgPermitNo = result.GetString("PERMIT_NO");

                    dtSetSocEd.Add(item);
                }

            //AGRICULTURAL
            result.Query = "select AP.*, AC.acct_ln || ', ' || AC.acct_fn || ' ' || AC.acct_mi as REAL_PROJ_OWNER, " +
                " AC.acct_hse_no || ',' || AC.acct_lot_no || ',' || AC.acct_blk_no || ',' || AC.acct_addr || ',' || AC.acct_brgy AS REAL_PROJ_ADDR " +
                "from application AP, account AC " +
                " where AP.proj_owner = AC.acct_code and AP.category_code = '05' and AP.date_applied between '" + string.Format("{0:dd-MMM-yy}", ReportForm.dtFrom) + "' and '" + string.Format("{0:dd-MMM-yy}", ReportForm.dtTo) + "'";
            if (result.Execute())
                while (result.Read())
                {
                    item = new Model.BuildingInformation();
                    item.OwnerName = result.GetString("REAL_PROJ_OWNER");
                    item.ProjDesc = result.GetString("PROJ_DESC");
                    item.ProjLocation = result.GetString("REAL_PROJ_ADDR");

                    result2.Query = "select BD.*, MT.material_desc from building BD, material_tbl MT where BD.material_code = MT.material_code and bldg_no = '" + result.GetInt("BLDG_NO") + "'";
                    if (result2.Execute())
                        if (result2.Read())
                        {
                            item.BldgMaterial = result2.GetString("MATERIAL_DESC");
                            item.BldgHeight = result2.GetDecimal("BLDG_HEIGHT");
                            item.Area = result2.GetDecimal("TOTAL_FLR_AREA");
                            item.EstiCost = result2.GetDecimal("EST_COST");
                            item.ActualCost = result2.GetDecimal("ACTUAL_COST");
                        }
                    item.DtIssued = result.GetDateTime("DATE_APPLIED");
                    item.BldgPermitNo = result.GetString("PERMIT_NO");

                    dtSetAgri.Add(item);
                }

            //ANCILLARY
            result.Query = "select AP.*, AC.acct_ln || ', ' || AC.acct_fn || ' ' || AC.acct_mi as REAL_PROJ_OWNER, " +
                " AC.acct_hse_no || ',' || AC.acct_lot_no || ',' || AC.acct_blk_no || ',' || AC.acct_addr || ',' || AC.acct_brgy AS REAL_PROJ_ADDR " +
                "from application AP, account AC " +
                " where AP.proj_owner = AC.acct_code and AP.category_code = '05' and AP.date_applied between '" + string.Format("{0:dd-MMM-yy}", ReportForm.dtFrom) + "' and '" + string.Format("{0:dd-MMM-yy}", ReportForm.dtTo) + "'";
            if (result.Execute())
                while (result.Read())
                {
                    item = new Model.BuildingInformation();
                    item.OwnerName = result.GetString("REAL_PROJ_OWNER");
                    item.ProjDesc = result.GetString("PROJ_DESC");
                    item.ProjLocation = result.GetString("REAL_PROJ_ADDR");

                    result2.Query = "select BD.*, MT.material_desc from building BD, material_tbl MT where BD.material_code = MT.material_code and bldg_no = '" + result.GetInt("BLDG_NO") + "'";
                    if (result2.Execute())
                        if (result2.Read())
                        {
                            item.BldgMaterial = result2.GetString("MATERIAL_DESC");
                            item.BldgHeight = result2.GetDecimal("BLDG_HEIGHT");
                            item.Area = result2.GetDecimal("TOTAL_FLR_AREA");
                            item.EstiCost = result2.GetDecimal("EST_COST");
                            item.ActualCost = result2.GetDecimal("ACTUAL_COST");
                        }
                    item.DtIssued = result.GetDateTime("DATE_APPLIED");
                    item.BldgPermitNo = result.GetString("PERMIT_NO");

                    dtSetAnci.Add(item);
                }

        }
    }
}
