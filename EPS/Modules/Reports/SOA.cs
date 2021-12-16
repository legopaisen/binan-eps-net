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
    public class SOA:FormReportClass
    {
        private DataSet dtSet;
        private DataSet dtSet2;
        private double dAllTotalAmt = 0;

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
                sProjLoc = string.Format("{0} {1} {2} {3} {4} {5} {6} ", item.PROJ_HSE_NO, item.PROJ_LOT_NO, item.PROJ_BLK_NO, item.PROJ_ADDR, item.PROJ_BRGY, item.PROJ_CITY, item.PROJ_PROV, item.PROJ_VILL);
                
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

            dtSet = new DataSet();
            dtSet2 = new DataSet();

            CreateDataSet();

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
                new Microsoft.Reporting.WinForms.ReportParameter("PrintBy", AppSettingsManager.SystemUser.UserName),
                new Microsoft.Reporting.WinForms.ReportParameter("AllTotalAmt", dAllTotalAmt.ToString())
     };
            ReportForm.reportViewer1.LocalReport.SetParameters(para);


            ReportDataSource ds = new ReportDataSource("DataSet1", dtSet.Tables[0]);
            ReportDataSource ds2 = new ReportDataSource("DataSet2", dtSet2.Tables[0]);
            this.ReportForm.reportViewer1.LocalReport.DataSources.Clear();
            this.ReportForm.reportViewer1.LocalReport.DataSources.Add(ds);
            this.ReportForm.reportViewer1.LocalReport.DataSources.Add(ds2);


           

        }

        private void CreateDataSet()
        {
            var db = new EPSConnection(dbConn);

            string sQuery = string.Empty;

            DataTable dtTable = new DataTable("List");
            DataTable dtTable2 = new DataTable("List");
            DataColumn dtColumn;
            DataRow myDataRow;
            OracleResultSet result = new OracleResultSet();
            float fSurch = 0;
            float fOthers = 0;
            float fAddl = 0;

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

            /////////////

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(String);
            dtColumn.ColumnName = "FeesDesc";
            dtColumn.ReadOnly = false;
            dtColumn.Unique = false;
            dtTable2.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(Double);
            dtColumn.ColumnName = "Fees";
            dtColumn.ReadOnly = false;
            dtTable2.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(Double);
            dtColumn.ColumnName = "Surcharge";
            dtColumn.ReadOnly = false;
            dtTable2.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(Double);
            dtColumn.ColumnName = "AdminFine";
            dtColumn.ReadOnly = false;
            dtTable2.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(Double);
            dtColumn.ColumnName = "Total";
            dtColumn.ReadOnly = false;
            dtTable2.Columns.Add(dtColumn);

            //DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            //PrimaryKeyColumns[0] = dtTable.Columns["FeesDesc"];
            //dtTable.PrimaryKey = PrimaryKeyColumns;

            dtSet = new DataSet();
            dtSet2 = new DataSet();
            dtSet.Tables.Add(dtTable);
            dtSet2.Tables.Add(dtTable2);
            string sValue = string.Empty;
            string sPres = string.Empty;
            string sNoMember = string.Empty;

            sQuery = "SELECT fees_desc, SUM(fees_amt) as fees_amt, permit_code from (";
            sQuery += "select major_fees.fees_desc as fees_desc,";
            sQuery += "sum(taxdues.fees_amt) as fees_amt, taxdues.permit_code from taxdues,major_fees ";
            sQuery += "where substr(taxdues.fees_code,1,2) = major_fees.fees_code and ";
            sQuery += $"taxdues.arn = '{ReportForm.An}' ";
            sQuery += $"and taxdues.FEES_CATEGORY <> 'OTHERS' ";
            sQuery += $"and taxdues.FEES_CATEGORY <> 'ADDITIONAL' ";
            sQuery += $" group by major_fees.fees_desc, taxdues.fees_code, taxdues.permit_code ";
            sQuery += $" ) group by  fees_desc, permit_code ";


            var record = db.Database.SqlQuery<SOA_TBL>(sQuery);
            string sOwnCode = string.Empty;
            string sFees = string.Empty;
            string sPermitCode = string.Empty;
            bool IsOk = false;
            int iCnt = 0;

            foreach (var items in record)
            {
                iCnt++;
                myDataRow = dtTable.NewRow();
                //sFees = items.FEES_CODE;
                sPermitCode = items.PERMIT_CODE;

                //surcharge
                result.Query = $"select sum(TD.fees_amt) as fees_amt from other_major_fees OM, taxdues TD ";
                result.Query += $"where fees_desc = 'SURCHARGE' ";
                result.Query += $"AND substr(TD.fees_code,1,2) = OM.fees_code and TD.arn = '{ReportForm.An}' ";
                result.Query += $"AND TD.fees_category = 'OTHERS' ";
                result.Query += $"AND TD.permit_code = '{sPermitCode}' ";
                float.TryParse(result.ExecuteScalar().ToString(), out fSurch);
                result.Close();

                //additional
                result.Query = "select sum(taxdues.fees_amt) as fees_amt ";
                result.Query += "from taxdues ";
                result.Query += $"where taxdues.arn = '{ReportForm.An}' ";
                result.Query += $"AND TAXDUES.FEES_CATEGORY = 'ADDITIONAL' ";
                result.Query += $"AND taxdues.permit_code = '{sPermitCode}' ";
                float.TryParse(result.ExecuteScalar().ToString(), out fAddl);
                result.Close();

                //add others value to total if is not tagged for display

                //if(IsOk == false)
                //{
                result.Query = "select O.fees_desc, sum(taxdues.fees_amt) as fees_amt, taxdues.fees_code as fees_code ";
                result.Query += "from taxdues, other_major_fees O ";
                result.Query += $"where substr(taxdues.fees_code,1,2) = O.fees_code and taxdues.arn = '{ReportForm.An}' ";
                result.Query += $"AND O.FEES_DESC <> 'SURCHARGE' ";
                result.Query += $"AND taxdues.fees_category = 'OTHERS' ";
                result.Query += $"AND taxdues.permit_code = '{sPermitCode}' ";
                result.Query += $"group by o.fees_desc, taxdues.fees_code";
                //result.Query += $"AND taxdues.fees_code = '{sFees}' ";
                if (result.Execute())
                    while (result.Read())
                    {
                        sFees = result.GetString("fees_code");
                        float.TryParse(result.GetDouble("fees_amt").ToString(), out fOthers);

                        if (ValidateTaggedDisplay(sFees))
                            items.FEES_AMT += fOthers;
                    }
                IsOk = true;
                result.Close();
                //}



                myDataRow["Surcharge"] = fSurch; //pending value nito

                items.FEES_AMT += fSurch;
                items.FEES_AMT += fAddl;

                myDataRow["FeesDesc"] = items.FEES_DESC;
                myDataRow["Fees"] = items.FEES_AMT;

                myDataRow["AdminFine"] = 0; //pending value nito
                myDataRow["Total"] = items.FEES_AMT;

                dtTable.Rows.Add(myDataRow);

                dAllTotalAmt += items.FEES_AMT;
            }
            
            if (iCnt == 0) //AFM 20211123 requested by binan as per rj - allow billing of additional fees only on any permit
            // proceeding this condition means additional fees are only billed on permit
            {
                result.Query = "select sum(taxdues.fees_amt) as fees_amt, permit_code ";
                result.Query += "from taxdues ";
                result.Query += $"where taxdues.arn = '{ReportForm.An}' ";
                result.Query += $"AND TAXDUES.FEES_CATEGORY = 'ADDITIONAL' group by permit_code "; 
                //result.Query += $"AND taxdues.permit_code = '{sPermitCode}' ";
                sQuery = result.Query;
                record = db.Database.SqlQuery<SOA_TBL>(sQuery);
                
                foreach(var items in record)
                {
                    sPermitCode = items.PERMIT_CODE;
                    myDataRow = dtTable.NewRow();
                    myDataRow["FeesDesc"] = AppSettingsManager.GetPermitDesc(sPermitCode); //requested by RJ to display by Permit name if additional fees are only billed
                    myDataRow["Fees"] = items.FEES_AMT;


                    myDataRow["Total"] = items.FEES_AMT;

                    dtTable.Rows.Add(myDataRow);
                    dAllTotalAmt += items.FEES_AMT;
                }

            }
           

            // for other fees
            string sDisplay = string.Empty;
            result.Query = "select O.fees_desc as fees_desc,sum(taxdues.fees_amt) as fees_amt, OS.display_amt ";
            result.Query += "from taxdues, other_major_fees O, other_subcategories OS ";
            result.Query += $"where substr(taxdues.fees_code,1,2) = O.fees_code and taxdues.fees_code = OS.fees_code and taxdues.arn = '{ReportForm.An}' ";
            result.Query += $"AND O.FEES_DESC <> 'SURCHARGE' ";
            result.Query += $"AND taxdues.fees_category = 'OTHERS' ";
            result.Query += $"group by O.fees_desc, OS.display_amt ";
            if(result.Execute())
                while(result.Read())
                {
                    sDisplay = result.GetString("display_amt");
                    if(sDisplay == "Y")
                    {
                        myDataRow = dtTable2.NewRow();
                        myDataRow["FeesDesc"] = result.GetString("fees_desc");
                        myDataRow["Fees"] = result.GetDouble("fees_amt");
                        myDataRow["Total"] = result.GetDouble("fees_amt");
                        dtTable2.Rows.Add(myDataRow);
                    }
                }
            result.Close();


        }

        private bool ValidateTaggedDisplay(string sFees) //check if fees is for display only and not computed to total amount
        {
            OracleResultSet result = new OracleResultSet();
            result.Query = $"select display_amt from other_subcategories where display_amt = 'N' and fees_code = '{sFees}'";
            if (result.Execute())
                if (result.Read())
                    return true;
                else
                    return false;
            else
                return false;

        }
    }
}
