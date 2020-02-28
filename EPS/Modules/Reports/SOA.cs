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

namespace Modules.Reports
{
    public class SOA:FormReportClass
    {
        private DataSet dtSet;

        public SOA(frmReport Form) : base(Form)
        { }

        public override void LoadForm()
        {
            var db = new EPSConnection(dbConn);
            string strWhereCond = string.Empty;
            string sProjDesc = string.Empty;
            string sProjLoc = string.Empty;
            string sCategory = string.Empty;
            string sOwnName = string.Empty;
            string sNoStorey = string.Empty;
            string sArea = string.Empty;
            string sConsCost = string.Empty;
            int iBldgNo = 0;
            var result = (dynamic)null;

            strWhereCond = $" where arn = '{ReportForm.An}'";

            result = from a in Records.ApplicationQueList.GetApplicationQue(strWhereCond)
                         select a;

            foreach (var item in result)
            {
                sProjDesc = item.PROJ_DESC;
                sProjLoc = string.Format("{0} {1} {2} {3} {4} {5} {6} ", item.PROJ_HSE_NO, item.PROJ_LOT_NO, item.PROJ_BLK_NO, item.PROJ_ADDR, item.PROJ_BRGY, item.PROJ_CITY, item.PROJ_PROV);
                
                Accounts account = new Accounts();
                account.GetOwner(item.PROJ_OWNER);
                sOwnName = account.OwnerName;
                Category category = new Category();
                sCategory = category.GetCategoryDesc(item.CATEGORY_CODE);
                iBldgNo = item.BLDG_NO;
            }

            if(string.IsNullOrEmpty(sProjDesc))
            {
                result = from a in Records.ApplicationTblList.GetRecord(strWhereCond)
                             select a;

                foreach (var item in result)
                {
                    sProjDesc = item.PROJ_DESC;
                    sProjLoc = string.Format("{0} {1} {2} {3} {4} {5} {6} ", item.PROJ_HSE_NO, item.PROJ_LOT_NO, item.PROJ_BLK_NO, item.PROJ_ADDR, item.PROJ_BRGY, item.PROJ_CITY, item.PROJ_PROV);

                    Accounts account = new Accounts();
                    account.GetOwner(item.PROJ_OWNER);
                    sOwnName = account.OwnerName;
                    Category category = new Category();
                    sCategory = category.GetCategoryDesc(item.CATEGORY_CODE);
                    iBldgNo = item.BLDG_NO;
                }
            }

            var result2 = from a in Records.Building.GetBuildingRecord(iBldgNo)
                         select a;

            foreach (var item in result2)
            {
                sNoStorey = string.Format("{0:###}", item.NO_STOREYS);
                sArea = string.Format("{0:###,##}", item.TOTAL_FLR_AREA);
                sConsCost = string.Format("{0:###,##}", item.EST_COST);
            }

            Microsoft.Reporting.WinForms.ReportParameter[] para = new Microsoft.Reporting.WinForms.ReportParameter[]
            {
                new Microsoft.Reporting.WinForms.ReportParameter("ReportName", ReportForm.ReportName),
                new Microsoft.Reporting.WinForms.ReportParameter("BillNo", BillNo),
                new Microsoft.Reporting.WinForms.ReportParameter("AppNo", ReportForm.An),
                new Microsoft.Reporting.WinForms.ReportParameter("ProjDesc", sProjDesc),
                new Microsoft.Reporting.WinForms.ReportParameter("ProjLoc", sProjLoc),
                new Microsoft.Reporting.WinForms.ReportParameter("NoStorey", sNoStorey),
                new Microsoft.Reporting.WinForms.ReportParameter("FlrArea", sArea),
                new Microsoft.Reporting.WinForms.ReportParameter("ConsCost", sConsCost),
                new Microsoft.Reporting.WinForms.ReportParameter("ProjOwn", sOwnName),
                new Microsoft.Reporting.WinForms.ReportParameter("Category", sCategory),
                new Microsoft.Reporting.WinForms.ReportParameter("ApprovedBy", AppSettingsManager.GetConfigValue("08")),
                new Microsoft.Reporting.WinForms.ReportParameter("LGUName", ConfigurationAttributes.LguName2),
                new Microsoft.Reporting.WinForms.ReportParameter("Province", ConfigurationAttributes.ProvinceName),
                new Microsoft.Reporting.WinForms.ReportParameter("PrintBy", AppSettingsManager.SystemUser.UserName)
            };
            ReportForm.reportViewer1.LocalReport.SetParameters(para);

            dtSet = new DataSet();

            CreateDataSet();
            ReportDataSource ds = new ReportDataSource("DataSet1", dtSet.Tables[0]);
            this.ReportForm.reportViewer1.LocalReport.DataSources.Clear();
            this.ReportForm.reportViewer1.LocalReport.DataSources.Add(ds);

        }

        private void CreateDataSet()
        {
            var db = new EPSConnection(dbConn);

            string sQuery = string.Empty;

            DataTable dtTable = new DataTable("List");
            DataColumn dtColumn;
            DataRow myDataRow;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(String);
            dtColumn.ColumnName = "FeesDesc";
            dtColumn.ReadOnly = false;
            dtColumn.Unique = false;
            dtTable.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(Double);
            dtColumn.ColumnName = "Fees";
            dtColumn.ReadOnly = false;
            dtTable.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(Double);
            dtColumn.ColumnName = "Surcharge";
            dtColumn.ReadOnly = false;
            dtTable.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(Double);
            dtColumn.ColumnName = "AdminFine";
            dtColumn.ReadOnly = false;
            dtTable.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(Double);
            dtColumn.ColumnName = "Total";
            dtColumn.ReadOnly = false;
            dtTable.Columns.Add(dtColumn);

            //DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            //PrimaryKeyColumns[0] = dtTable.Columns["FeesDesc"];
            //dtTable.PrimaryKey = PrimaryKeyColumns;

            dtSet = new DataSet();
            dtSet.Tables.Add(dtTable);
            string sValue = string.Empty;
            string sPres = string.Empty;
            string sNoMember = string.Empty;

            sQuery = "select major_fees.fees_desc as fees_desc,";
            sQuery += "sum(taxdues.fees_amt) as fees_amt from taxdues,major_fees ";
            sQuery += "where substr(taxdues.fees_code,1,2) = major_fees.fees_code and ";   
            sQuery += $"taxdues.arn = '{ReportForm.An}' group by major_fees.fees_desc";
            var record = db.Database.SqlQuery<SOA_TBL>(sQuery);
            string sOwnCode = string.Empty;

            foreach (var items in record)
            {
                myDataRow = dtTable.NewRow();
                myDataRow["FeesDesc"] = items.FEES_DESC;
                myDataRow["Fees"] = items.FEES_AMT;

                myDataRow["Surcharge"] = 0; //pending value nito
                myDataRow["AdminFine"] = 0; //pending value nito
                myDataRow["Total"] = items.FEES_AMT;

                dtTable.Rows.Add(myDataRow);
            }
        }
    }
}
