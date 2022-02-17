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
        private DataSet dtSet3;
        private double dAllTotalAmt = 0;

        //building
        double dLineGradeAmtTot = 0;
        double dBldgFeeAmtTot = 0;
        double dSanitaryAmtTot = 0;
        double dElecFeeTot = 0;
        double dConstFeeTot = 0;
        double dFilingFeeTot = 0;

        //electrical
        double dWiring = 0;
        double dCFEI = 0;
        double dLoad = 0;
        double dInspect = 0;

        //other permit
        double dOtherPermitFee = 0;

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
            dtSet3 = new DataSet();

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
                new Microsoft.Reporting.WinForms.ReportParameter("AllTotalAmt", dAllTotalAmt.ToString("##,##0.00"))
                //new Microsoft.Reporting.WinForms.ReportParameter("LineGradeAmt", dLineGradeAmtTot.ToString("##,##0.00")),
                //new Microsoft.Reporting.WinForms.ReportParameter("BldgAmt", dBldgFeeAmtTot.ToString("##,##0.00")),
                //new Microsoft.Reporting.WinForms.ReportParameter("SanitaryAmt", dSanitaryAmtTot.ToString("##,##0.00")),
                //new Microsoft.Reporting.WinForms.ReportParameter("ElecFee", dElecFeeTot.ToString("##,##0.00")),
                //new Microsoft.Reporting.WinForms.ReportParameter("ConstFee", dConstFeeTot.ToString("##,##0.00")),
                //new Microsoft.Reporting.WinForms.ReportParameter("FilingFee", dFilingFeeTot.ToString("##,##0.00"))
     };
            ReportForm.reportViewer1.LocalReport.SetParameters(para);


            ReportDataSource ds = new ReportDataSource("DataSet1", dtSet.Tables[0]);
            ReportDataSource ds2 = new ReportDataSource("DataSet2", dtSet2.Tables[0]);
            ReportDataSource ds3 = new ReportDataSource("DataSet3", dtSet3.Tables[0]);
            this.ReportForm.reportViewer1.LocalReport.DataSources.Clear();
            this.ReportForm.reportViewer1.LocalReport.DataSources.Add(ds);
            this.ReportForm.reportViewer1.LocalReport.DataSources.Add(ds2);
            this.ReportForm.reportViewer1.LocalReport.DataSources.Add(ds3);




        }

        private void CreateDataSet()
        {
            var db = new EPSConnection(dbConn);

            string sQuery = string.Empty;

            DataTable dtTable = new DataTable("List");
            DataTable dtTable2 = new DataTable("List");
            DataTable dtTable3 = new DataTable("List");
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

            ////////

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(String);
            dtColumn.ColumnName = "FeesDesc";
            dtColumn.ReadOnly = false;
            dtColumn.Unique = false;
            dtTable3.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(Double);
            dtColumn.ColumnName = "Fees";
            dtColumn.ReadOnly = false;
            dtTable3.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(Double);
            dtColumn.ColumnName = "Surcharge";
            dtColumn.ReadOnly = false;
            dtTable3.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(Double);
            dtColumn.ColumnName = "AdminFine";
            dtColumn.ReadOnly = false;
            dtTable3.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(Double);
            dtColumn.ColumnName = "Total";
            dtColumn.ReadOnly = false;
            dtTable3.Columns.Add(dtColumn);

            //DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            //PrimaryKeyColumns[0] = dtTable.Columns["FeesDesc"];
            //dtTable.PrimaryKey = PrimaryKeyColumns;

            dtSet = new DataSet();
            dtSet2 = new DataSet();
            dtSet3 = new DataSet();
            dtSet.Tables.Add(dtTable);
            dtSet2.Tables.Add(dtTable2);
            dtSet3.Tables.Add(dtTable3);
            string sValue = string.Empty;
            string sPres = string.Empty;
            string sNoMember = string.Empty;

            //AFM 20220214 added feescode - adjustments binan meeting 2/8/22
            sQuery = "SELECT fees_desc, SUM(fees_amt) as fees_amt, permit_code, FEES_CODE from (";
            sQuery += "select major_fees.fees_desc as fees_desc,";
            sQuery += "sum(taxdues.fees_amt) as fees_amt, taxdues.permit_code, taxdues.FEES_CODE from taxdues,major_fees ";
            sQuery += "where substr(taxdues.fees_code,1,2) = major_fees.fees_code and ";
            sQuery += $"taxdues.arn = '{ReportForm.An}' ";
            sQuery += $"and taxdues.FEES_CATEGORY <> 'OTHERS' ";
            sQuery += $"and taxdues.FEES_CATEGORY <> 'ADDITIONAL' ";
            sQuery += $" group by major_fees.fees_desc, taxdues.fees_code, taxdues.permit_code, taxdues.FEES_CODE ";
            sQuery += $" ) group by  fees_desc, permit_code, fees_code ";


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

                //AFM 20220214 - adjustments binan meeting 2/8/22 (s)
                if (ReportForm.SOAPermit.Contains("BUILDING")) //format for building permit
                {
                    if (items.FEES_DESC == "BUILDING FEES" || sPermitCode == "01") //line and grade
                    {
                        dLineGradeAmtTot += items.FEES_AMT;
                        dAllTotalAmt += items.FEES_AMT;
                    }
                    if (items.FEES_DESC == "ELECTRICAL FEES")
                    {
                        dElecFeeTot += items.FEES_AMT;
                        dAllTotalAmt += items.FEES_AMT;
                    }
                    if (items.FEES_DESC == "PLUMBING FEES")
                    {
                        dSanitaryAmtTot += items.FEES_AMT;
                        dAllTotalAmt += items.FEES_AMT;
                    }
                }
                else if (ReportForm.SOAPermit.Contains("ELECTRICAL")) //format for electrical permit
                {
                    if (items.FEES_DESC == "ELECTRICAL FEES") //total connected load
                    {
                        dLoad += items.FEES_AMT;
                        dAllTotalAmt += items.FEES_AMT;
                    }
                }
                else //format for other permits
                {
                    dAllTotalAmt += items.FEES_AMT;
                    dOtherPermitFee += items.FEES_AMT;
                }
                //AFM 20220214 - adjustments binan meeting 2/8/22 (e)

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

                if ((ReportForm.SOAPermit.Contains("BUILDING") && sPermitCode != "01") && (ReportForm.SOAPermit.Contains("BUILDING") && sPermitCode != "02") && (ReportForm.SOAPermit.Contains("BUILDING") && items.FEES_DESC != "PLUMBING FEES")) //format for building permit
                {
                    //items.FEES_AMT += fSurch;
                    //items.FEES_AMT += fAddl;

                    //myDataRow["FeesDesc"] = items.FEES_DESC;
                    myDataRow["FeesDesc"] = AppSettingsManager.GetPermitDesc(sPermitCode).Substring(0, AppSettingsManager.GetPermitDesc(sPermitCode).Length - 6) + "FEE";
                    myDataRow["Fees"] = items.FEES_AMT;

                    myDataRow["AdminFine"] = 0; //pending value nito
                    myDataRow["Total"] = items.FEES_AMT;

                    dtTable.Rows.Add(myDataRow);

                    dAllTotalAmt += items.FEES_AMT;
                }
            }


            //AFM 20220214 - adjustments binan meeting 2/8/22
            //addl fees, other fee, filing fee etc.
            result.Query = "select taxdues.fees_code, taxdues.fees_amt, addl_subcategories.FEES_DESC from taxdues, addl_subcategories ";
            result.Query += $"where taxdues.arn = '{ReportForm.An}' ";
            result.Query += $"and taxdues.fees_code = addl_subcategories.fees_code ";
            result.Query += $"AND (TAXDUES.FEES_CATEGORY = 'ADDITIONAL' OR TAXDUES.FEES_CATEGORY = 'OTHERS')";
            //result.Query += $"AND taxdues.permit_code = '{sPermitCode}' ";
            if (result.Execute())
            {
                while (result.Read())
                {
                    string sFeesdesc = result.GetString("fees_desc");
                    double dAmt = 0;
                    double.TryParse(result.GetDouble("fees_amt").ToString(), out dAmt);

                    if (ReportForm.SOAPermit.Contains("BUILDING"))
                    {
                        if (sFeesdesc.Contains("FILLING FEE")) //filing fee is separate
                        {
                            dFilingFeeTot += dAmt;
                            dAllTotalAmt += dAmt;
                        }
                        else
                        {
                            dAllTotalAmt += dAmt;
                            dBldgFeeAmtTot += dAmt;
                        }
                    }
                    else if (ReportForm.SOAPermit.Contains("ELECTRICAL"))
                    {
                        if (sFeesdesc.Contains("CFEI"))
                        {
                            dAllTotalAmt += dAmt;
                            dCFEI += dAmt;
                        }
                        else if (sFeesdesc.Contains("INSPECTION"))
                        {
                            dAllTotalAmt += dAmt;
                            dInspect += dAmt;
                        }
                        else
                        {
                            dAllTotalAmt += dAmt;
                            dWiring += dAmt;
                        }

                    }
                    else //other permits
                    {
                        if (sFeesdesc.Contains("FILLING FEE")) //filing fee is separate
                        {
                            dFilingFeeTot += dAmt;
                            dAllTotalAmt += dAmt;
                        }
                        else
                        {
                            dOtherPermitFee += dAmt;
                            dAllTotalAmt += dAmt;
                        }
                    }
                }
            }
            result.Close();

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

                foreach (var items in record)
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
            if (result.Execute())
                while (result.Read())
                {
                    sDisplay = result.GetString("display_amt");
                    if (sDisplay == "Y")
                    {
                        myDataRow = dtTable2.NewRow();
                        myDataRow["FeesDesc"] = result.GetString("fees_desc");
                        myDataRow["Fees"] = result.GetDouble("fees_amt");
                        myDataRow["Total"] = result.GetDouble("fees_amt");
                        dtTable2.Rows.Add(myDataRow);
                    }
                }
            result.Close();


            //AFM 20220216 - adjustments binan meeting 2/8/22
            if (iCnt > 0)
            {
                if (ReportForm.SOAPermit.Contains("BUILDING"))
                {
                    myDataRow = dtTable3.NewRow();
                    myDataRow["FeesDesc"] = "LINE & GRADE";
                    myDataRow["Fees"] = dLineGradeAmtTot;
                    myDataRow["Total"] = dLineGradeAmtTot;
                    dtTable3.Rows.Add(myDataRow);

                    myDataRow = dtTable3.NewRow();
                    myDataRow["FeesDesc"] = "BUILDING FEE";
                    myDataRow["Fees"] = dBldgFeeAmtTot;
                    myDataRow["Total"] = dBldgFeeAmtTot;
                    dtTable3.Rows.Add(myDataRow);

                    myDataRow = dtTable3.NewRow();
                    myDataRow["FeesDesc"] = "SANITARY/PLUMBING FEE";
                    myDataRow["Fees"] = dSanitaryAmtTot;
                    myDataRow["Total"] = dSanitaryAmtTot;
                    dtTable3.Rows.Add(myDataRow);

                    myDataRow = dtTable3.NewRow();
                    myDataRow["FeesDesc"] = "ELECTRICAL FEE";
                    myDataRow["Fees"] = dElecFeeTot;
                    myDataRow["Total"] = dElecFeeTot;
                    dtTable3.Rows.Add(myDataRow);

                    myDataRow = dtTable3.NewRow();
                    myDataRow["FeesDesc"] = "CONSTRUCTION FEE";
                    myDataRow["Fees"] = dConstFeeTot;
                    myDataRow["Total"] = dConstFeeTot;
                    dtTable3.Rows.Add(myDataRow);

                    myDataRow = dtTable3.NewRow();
                    myDataRow["FeesDesc"] = "FILING FEE";
                    myDataRow["Fees"] = dFilingFeeTot;
                    myDataRow["Total"] = dFilingFeeTot;
                    dtTable3.Rows.Add(myDataRow);
                }
                else if (ReportForm.SOAPermit.Contains("ELECTRICAL"))
                {
                    myDataRow = dtTable3.NewRow();
                    myDataRow["FeesDesc"] = "WIRING PERMIT";
                    myDataRow["Fees"] = dWiring;
                    myDataRow["Total"] = dWiring;
                    dtTable3.Rows.Add(myDataRow);

                    myDataRow = dtTable3.NewRow();
                    myDataRow["FeesDesc"] = "CFEI";
                    myDataRow["Fees"] = dCFEI;
                    myDataRow["Total"] = dCFEI;
                    dtTable3.Rows.Add(myDataRow);

                    myDataRow = dtTable3.NewRow();
                    myDataRow["FeesDesc"] = "TOTAL CONNECTED LOAD";
                    myDataRow["Fees"] = dLoad;
                    myDataRow["Total"] = dLoad;
                    dtTable3.Rows.Add(myDataRow);

                    myDataRow = dtTable3.NewRow();
                    myDataRow["FeesDesc"] = "INSPECTION";
                    myDataRow["Fees"] = dInspect;
                    myDataRow["Total"] = dInspect;
                    dtTable3.Rows.Add(myDataRow);
                }
                else //other permits
                {
                    myDataRow = dtTable3.NewRow();
                    myDataRow["FeesDesc"] = AppSettingsManager.GetPermitDesc(sPermitCode).Substring(0, AppSettingsManager.GetPermitDesc(sPermitCode).Length - 6) + "FEE";
                    myDataRow["Fees"] = dOtherPermitFee;
                    myDataRow["Total"] = dOtherPermitFee;
                    dtTable3.Rows.Add(myDataRow);

                    myDataRow = dtTable3.NewRow();
                    myDataRow["FeesDesc"] = "FILING FEE";
                    myDataRow["Fees"] = dFilingFeeTot;
                    myDataRow["Total"] = dFilingFeeTot;
                    dtTable3.Rows.Add(myDataRow);
                }
            }

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
