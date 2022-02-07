using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPSEntities.Connection;
using Modules.Utilities;
using System.Windows.Forms;
using EPSEntities.Entity;
using Oracle.ManagedDataAccess.Client;
using Common.AppSettings;
using ARCSEntities.Connection;
using Common.StringUtilities;
using Common.DataConnector;

namespace Modules.Billing
{
    public class FormClass
    {

        public static ConnectionString dbConn = new ConnectionString();
        public static ARCSConnectionString dbConnArcs = new ARCSConnectionString();
        TaskManager taskman = new TaskManager();
        protected frmBilling RecordFrm = null;
        private double m_dArea = 0;
        private double m_dInputValue = 0; //for input amount value
        private double m_LMTotal = 0; //for electrical LM total value
        private int m_iAssessmentRow = 0;
        private string m_sFeesCode = string.Empty;
        private string m_sFeesMeans = string.Empty;
        private string m_sCumulative = string.Empty;
        private double m_dMonths = 0;
        protected string m_sProjOwner = string.Empty;
        protected int m_iMainApplication = 0;
        protected string m_sPermitCodeSelected = string.Empty;
        private string m_sAddlFeeCode = string.Empty;
        private string m_sSelectedFeesCode = string.Empty;

        public string CellClickEvent = string.Empty;

        private List<string> sPermitList;

        public List<string> PermitList
        {
            get { return sPermitList; }
            set { sPermitList = value; }
        }

        

        public FormClass(frmBilling Form)
        {
            this.RecordFrm = Form;

        }

        public virtual void FormLoad()
        {
            
        }

        public void ClearControls()
        {
            RecordFrm.txtBillNo.Text = string.Empty;
            RecordFrm.txtProjDesc.Text = string.Empty;
            RecordFrm.txtProjLoc.Text = string.Empty;
            RecordFrm.txtProjOwn.Text = string.Empty;
            RecordFrm.txtOccupancy.Text = string.Empty;
            RecordFrm.grpAddlFee.Text = string.Empty;
            RecordFrm.txtAppDate.Text = string.Empty;
            RecordFrm.dgvPermit.Rows.Clear();

            RecordFrm.dgvAssessment.DataSource = null;
            RecordFrm.dgvAssessment.Rows.Clear();
            RecordFrm.dgvAddOnFees.DataSource = null;
            RecordFrm.dgvAddOnFees.Rows.Clear();
            RecordFrm.dgvOtherFees.DataSource = null;
            RecordFrm.dgvOtherFees.Rows.Clear();

            RecordFrm.txtAddlFees.Text = string.Empty;
            RecordFrm.txtAddlFees.Text = string.Empty;
            RecordFrm.btnAddlAdd.Text = "Add";
            RecordFrm.txtAddlFees.ReadOnly = true;
            RecordFrm.txtAddlAmt.ReadOnly = true;
            RecordFrm.txtAmtDue.Text = string.Empty;
            RecordFrm.txtAllTotAmtDue.Text = string.Empty;
            RecordFrm.txtAddOnTotAmTDue.Text = string.Empty;
            RecordFrm.txtOtherTotAmtDue.Text = string.Empty;
            RecordFrm.txtAddOnAmt.ReadOnly = true;
            RecordFrm.txtOtherAmt.ReadOnly = true;
            RecordFrm.txtAddOnFeesDesc.Text = string.Empty;
            RecordFrm.txtOtherFeesDesc.Text = string.Empty;
            m_sAddlFeeCode = string.Empty;

            m_sProjOwner = string.Empty;
            RecordFrm.btnCancel.Text = "Exit";
            
        }
        
        public bool DisplayData()
        {
            ClearControls();

            var db = new EPSConnection(dbConn);
            string strWhereCond = string.Empty;
            int iRecCount = 0;
            int iBldgNo = 0;
            DateTime dtApplied;
            m_iMainApplication = 0;
            var result = (dynamic)null;

            if (RecordFrm.Source == "CERTIFICATE OF ANNUAL INSPECTION" ||
                RecordFrm.Source == "CERTIFICATE OF OCCUPANCY")
            {
                strWhereCond = $" where arn = '{RecordFrm.m_sAN}' and main_application = 1";

                result = from a in Records.ApplicationTblList.GetRecord(strWhereCond)
                         select a;
            }
            else
            {
                strWhereCond = $" where arn = '{RecordFrm.m_sAN}' and permit_code = '{RecordFrm.PermitCode}'";

                if (AppSettingsManager.GetConfigValue("30") == "1")// AFM 20191016 if posting mode is set for engineering records
                {
                    result = from a in Records.ApplicationTblList.GetRecord(strWhereCond)
                             select a;
                }
                else
                {
                    result = from a in Records.ApplicationQueList.GetApplicationQue(strWhereCond)
                             select a;
                }
            }

            foreach (var item in result)
            {
                OracleResultSet res = new OracleResultSet();
                iRecCount++;
                RecordFrm.txtProjDesc.Text = item.PROJ_DESC;
                RecordFrm.txtProjLoc.Text = string.Format("{0} {1} {2} {3} {4} {5} {6} ", item.PROJ_HSE_NO, item.PROJ_LOT_NO, item.PROJ_BLK_NO, item.PROJ_ADDR, item.PROJ_BRGY, item.PROJ_CITY, item.PROJ_PROV).Trim();
                m_sProjOwner = item.PROJ_OWNER;
                Accounts account = new Accounts();
                account.GetOwner(m_sProjOwner);

                RecordFrm.txtProjOwn.Text = account.OwnerName.Trim();
                ScopeList scopelist = new ScopeList();
                RecordFrm.grpAddlFee.Text = scopelist.GetScopeDesc(item.SCOPE_CODE);

                //AFM 20210308 (s)
                CategoryList catList = new CategoryList();
                Category category = new Category();
                RecordFrm.cmbCategory.Items.Clear();
                foreach (var s in catList.CategoryLst)
                {
                    RecordFrm.cmbCategory.Items.Add(s.Desc);
                }
                RecordFrm.cmbCategory.Text = category.GetCategoryDesc(item.CATEGORY_CODE);

                OccupancyList lstOccupancy = new OccupancyList(item.CATEGORY_CODE, item.OCCUPANCY_CODE);
                RecordFrm.cmbOccupancy.Items.Clear();
                foreach (var s in lstOccupancy.OccupancyLst)
                {
                    RecordFrm.cmbOccupancy.Items.Add(s.Desc);
                }
                RecordFrm.cmbOccupancy.Text = lstOccupancy.OccupancyLst[0].Desc;
                RecordFrm.txtOccupancy.Text = lstOccupancy.OccupancyLst[0].Desc;


                StructureList lstStructure = new StructureList();
                RecordFrm.cmbStructure.Items.Clear();
                foreach(var s in lstStructure.StructureLst)
                {
                    RecordFrm.cmbStructure.Items.Add(s.Desc);
                }

                string sBldgNo = string.Empty;
                Structure structure = new Structure();
                RecordFrm.cmbStructure.Text = structure.GetStructureDesc(item.STRUC_CODE);
                sBldgNo = Convert.ToString(item.BLDG_NO);

                res.Query = $"select * from building where bldg_no = '{sBldgNo}'";
                if(res.Execute())
                    if(res.Read())
                    {                       
                        RecordFrm.txtFlrArea.Text = string.Format("{0:#,###.00}", res.GetDouble("total_flr_area"));
                        RecordFrm.txtHeight.Text = string.Format("{0:#,###.00}", res.GetDouble("bldg_height"));
                        RecordFrm.txtBldgCost.Text = string.Format("{0:#,###.00}", res.GetDouble("est_cost"));
                    }
                res.Close();
                //AFM 20210308 (e)

                try
                {
                    DateTime.TryParse(item.DATE_APPLIED.ToString(), out dtApplied);

                    RecordFrm.txtAppDate.Text = dtApplied.ToShortDateString();
                }
                catch
                {
                    RecordFrm.txtAppDate.Text = "";
                }

                iBldgNo = item.BLDG_NO;
                RecordFrm.txtStatus.Text = item.STATUS_CODE;
                m_iMainApplication = item.MAIN_APPLICATION;
            }

            var resultBldg = from a in Records.Building.GetBuildingRecord(iBldgNo)
                         select a;

            foreach (var item in resultBldg)
            {
                m_dArea = item.TOTAL_FLR_AREA;
            }

            if (iRecCount == 0)
            {
                MessageBox.Show("No application found",RecordFrm.m_sModule,MessageBoxButtons.OK,MessageBoxIcon.Stop);
                return false;
            }

            string sQuery = string.Empty;
            int iCnt = 0;

            sQuery = "select * from taxdues where ";
            sQuery += $" arn = '{RecordFrm.m_sAN}' ";
            var epsrec = db.Database.SqlQuery<TAXDUES>(sQuery);

            foreach (var items in epsrec)
            {
                iCnt++;
                RecordFrm.txtBillNo.Text = items.BILL_NO;
            }       

            if (iCnt > 0)
            {
                if (MessageBox.Show("Billing found.\nContinue?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    ClearControls();
                }
                else
                {
                    RecordFrm.btnPrint.Enabled = true;
                }
            }

            LoadLM();

            DisplayAssessmentData();
            RecordFrm.btnCancel.Text = "Cancel";

            if (CheckApproval())
            {
                MessageBox.Show($"AN: '{RecordFrm.m_sAN}' is subject for approval", RecordFrm.m_sModule, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return true;
        }

        private void LoadLM() //load electrical linear meter values
        {
            OracleResultSet res = new OracleResultSet();
            res.Query = $"select * from bill_lm_tmp where arn = '{RecordFrm.m_sAN}'";
            if(res.Execute())
                if(res.Read())
                {
                    RecordFrm.txtFront.Text = string.Format("{0:#,###.00}", res.GetDouble("front_val"));
                    RecordFrm.txtLeft.Text = string.Format("{0:#,###.00}", res.GetDouble("left_val"));
                    RecordFrm.txtRight.Text = string.Format("{0:#,###.00}", res.GetDouble("right_val"));
                    RecordFrm.txtRear.Text = string.Format("{0:#,###.00}", res.GetDouble("rear_val"));
                    RecordFrm.txtElecTotal.Text = string.Format("{0:#,###.00}", res.GetDouble("total"));
                }
            res.Close();
        }

        private bool CheckApproval()
        {
            OracleResultSet res = new OracleResultSet();
            string sStatus = string.Empty;
            res.Query = $"select status from application_approval where arn = '{RecordFrm.m_sAN}'";
            if (res.Execute())
                if (res.Read())
                {
                    sStatus = res.GetString("status");
                    if (sStatus == "PENDING" || sStatus == "REJECTED")
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            else
                return false;
        }
        
        public virtual void DisplayAssessmentData()
        {
        }

        protected bool ValidatePermitAssessed(string sPermitCode)
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;
            int iCnt = 0;

            sQuery = $"SELECT count(*) FROM taxdues where arn = '{RecordFrm.m_sAN}'";
            //sQuery += $" and fees_code like '{sPermitCode}%'";
            sQuery += $" and fees_code in (select fees_code from permit_fees_tbl where permit_code = '{sPermitCode}')";
            iCnt = db.Database.SqlQuery<Int32>(sQuery).SingleOrDefault();

            if (iCnt > 0)
                return true;
            
            return false;
        }

        protected void LoadAssessmentGrid(string sPermitCode)
        {
            Subcategories subcat = new Subcategories();

            RecordFrm.dgvAssessment.Columns.Clear();
            RecordFrm.dgvOtherFees.Columns.Clear();
            RecordFrm.dgvAddOnFees.Columns.Clear();
            RecordFrm.dgvAssessment.DataSource = null;
            RecordFrm.dgvOtherFees.DataSource = null;
            RecordFrm.dgvAddOnFees.DataSource = null;
            m_sSelectedFeesCode = sPermitCode;
       
            if (RecordFrm.Source == "CERTIFICATE OF OCCUPANCY" || RecordFrm.Source == "CERTIFICATE OF ANNUAL INSPECTION")
                RecordFrm.dgvAssessment.DataSource = subcat.GetSubcategory(sPermitCode, RecordFrm.m_sAN, "application");
            else
            {
                if (AppSettingsManager.GetConfigValue("30") == "1") //AFM 20191016 added build up mode condition
                {
                    RecordFrm.dgvAssessment.DataSource = subcat.GetSubcategory(sPermitCode, RecordFrm.m_sAN, "application");
                    RecordFrm.dgvOtherFees.DataSource = subcat.GetOtherSubcategory(sPermitCode, RecordFrm.m_sAN, "application");
                    RecordFrm.dgvAddOnFees.DataSource = subcat.GetAddlSubcategory(sPermitCode, RecordFrm.m_sAN, "application");
                }
                else
                {
                    RecordFrm.dgvAssessment.DataSource = subcat.GetSubcategory(sPermitCode, RecordFrm.m_sAN, "application_que");
                    RecordFrm.dgvOtherFees.DataSource = subcat.GetOtherSubcategory(sPermitCode, RecordFrm.m_sAN, "application_que");
                    RecordFrm.dgvAddOnFees.DataSource = subcat.GetAddlSubcategory(sPermitCode, RecordFrm.m_sAN, "application_que");
                }
                RecordFrm.dgvAssessment.Columns[0].Width = 30;
                RecordFrm.dgvAssessment.Columns[1].Width = 250;
                RecordFrm.dgvAssessment.Columns[2].Visible = false;
                RecordFrm.dgvAssessment.Columns[3].Visible = false;
                RecordFrm.dgvAssessment.Columns[4].Width = 50;
                RecordFrm.dgvAssessment.Columns[5].Visible = false;
                RecordFrm.dgvAssessment.Columns[6].Visible = false;
                RecordFrm.dgvAssessment.Columns[9].Visible = false;
                RecordFrm.dgvAssessment.Columns[10].Visible = false;
                RecordFrm.dgvAssessment.Columns[11].Visible = false;

                RecordFrm.dgvAssessment.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                RecordFrm.dgvAssessment.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;

                RecordFrm.dgvOtherFees.Columns[0].Width = 30;
                RecordFrm.dgvOtherFees.Columns[1].Width = 250;
                RecordFrm.dgvOtherFees.Columns[2].Visible = false;
                RecordFrm.dgvOtherFees.Columns[3].Visible = false;
                RecordFrm.dgvOtherFees.Columns[4].Visible = true;
                RecordFrm.dgvOtherFees.Columns[4].Width = 50;
                RecordFrm.dgvOtherFees.Columns[5].Visible = false;
                RecordFrm.dgvOtherFees.Columns[6].Visible = false;
                RecordFrm.dgvOtherFees.Columns[7].Visible = true;
                RecordFrm.dgvOtherFees.Columns[9].Visible = false;
                RecordFrm.dgvOtherFees.Columns[10].Visible = false;
                RecordFrm.dgvOtherFees.Columns[11].Visible = false;

                RecordFrm.dgvOtherFees.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                RecordFrm.dgvOtherFees.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;

                RecordFrm.dgvAddOnFees.Columns[0].Width = 30;
                RecordFrm.dgvAddOnFees.Columns[1].Width = 400;
                RecordFrm.dgvAddOnFees.Columns[2].Visible = false;
                RecordFrm.dgvAddOnFees.Columns[3].Visible = false;
                RecordFrm.dgvAddOnFees.Columns[4].Visible = false;
                RecordFrm.dgvAddOnFees.Columns[5].Visible = false;
                RecordFrm.dgvAddOnFees.Columns[6].Visible = false;
                RecordFrm.dgvAddOnFees.Columns[7].Visible = false;
                RecordFrm.dgvAddOnFees.Columns[9].Visible = false;
                RecordFrm.dgvAddOnFees.Columns[10].Visible = false;
                RecordFrm.dgvAddOnFees.Columns[11].Visible = false;

                RecordFrm.dgvAddOnFees.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                RecordFrm.dgvAddOnFees.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;


                foreach (DataGridViewColumn column in RecordFrm.dgvAssessment.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                foreach (DataGridViewColumn column in RecordFrm.dgvOtherFees.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                foreach (DataGridViewColumn column in RecordFrm.dgvAddOnFees.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

            }

            var db = new EPSConnection(dbConn);

            string sQuery = string.Empty;
            string sFeesCode = string.Empty;
            int iBillTmp = 0;

            sQuery = $"select count(*) from bill_tmp where arn = '{RecordFrm.m_sAN}'";
            iBillTmp = db.Database.SqlQuery<Int32>(sQuery).SingleOrDefault();
            if(iBillTmp == 0)
            {
                sQuery = "insert into bill_tmp ";
                sQuery += $"select ARN, FEES_CODE, FEES_UNIT, FEES_UNIT_VALUE, FEES_AMT, PERMIT_CODE,'{AppSettingsManager.SystemUser.UserCode}',ORIG_AMT, FEES_CATEGORY";
                sQuery += $" from tax_details where arn = '{RecordFrm.m_sAN}' and bill_no = '{RecordFrm.txtBillNo.Text.ToString()}'";
                db.Database.ExecuteSqlCommand(sQuery);
            }

            //MAIN
            for (int i = 0; i <= RecordFrm.dgvAssessment.Rows.Count; i++)
            {
                try
                {
                    sFeesCode = RecordFrm.dgvAssessment[2, i].Value.ToString();
                    sQuery = "select * from bill_tmp where ";
                    sQuery += $"arn = '{RecordFrm.m_sAN}' ";
                    sQuery += $" and fees_code = '{sFeesCode}'";
                    sQuery += $" and fees_category = 'MAIN'";
                    var epsrec = db.Database.SqlQuery<BILL_TMP>(sQuery);

                    foreach (var items in epsrec)
                    {
                        RecordFrm.dgvAssessment[0, i].Value = true;
                        RecordFrm.dgvAssessment[7, i].Value = items.FEES_UNIT_VALUE.ToString();
                        RecordFrm.dgvAssessment[8, i].Value = string.Format("{0:#,###.00}", items.FEES_AMT);
                        RecordFrm.dgvAssessment[11, i].Value = string.Format("{0:#,###.00}", items.ORIG_AMT);
                    }
                }
                catch { }
            }

            //ADDITIONAL FEES
            for (int i = 0; i <= RecordFrm.dgvAddOnFees.Rows.Count; i++)
            {
                try
                {
                    sFeesCode = RecordFrm.dgvAddOnFees[2, i].Value.ToString();
                    sQuery = "select * from bill_tmp where ";
                    sQuery += $"arn = '{RecordFrm.m_sAN}' ";
                    sQuery += $" and fees_code = '{sFeesCode}' ";
                    sQuery += $" and fees_category = 'ADDITIONAL'";
                    sQuery += $" and permit_code  = '{m_sPermitCodeSelected}'";
                    var epsrec = db.Database.SqlQuery<BILL_TMP>(sQuery);

                    foreach (var items in epsrec)
                    {
                        RecordFrm.dgvAddOnFees[0, i].Value = true;
                        RecordFrm.dgvAddOnFees[7, i].Value = items.FEES_UNIT_VALUE.ToString();
                        RecordFrm.dgvAddOnFees[8, i].Value = string.Format("{0:#,###.00}", items.FEES_AMT);
                        RecordFrm.dgvAddOnFees[11, i].Value = string.Format("{0:#,###.00}", GetOtherFeesValue(sFeesCode));
                    }
                }
                catch { }
            }

            //OTHER FEES
            for (int i = 0; i <= RecordFrm.dgvOtherFees.Rows.Count; i++)
            {
                try
                {
                    sFeesCode = RecordFrm.dgvOtherFees[2, i].Value.ToString();
                    sQuery = "select * from bill_tmp where ";
                    sQuery += $"arn = '{RecordFrm.m_sAN}' ";
                    sQuery += $" and fees_code = '{sFeesCode}'";
                    sQuery += $" and fees_category = 'OTHERS'";
                    sQuery += $" and permit_code  = '{m_sPermitCodeSelected}'";
                    var epsrec = db.Database.SqlQuery<BILL_TMP>(sQuery);

                    foreach (var items in epsrec)
                    {
                        RecordFrm.dgvOtherFees[0, i].Value = true;
                        RecordFrm.dgvOtherFees[7, i].Value = items.FEES_UNIT_VALUE.ToString();
                        RecordFrm.dgvOtherFees[8, i].Value = string.Format("{0:#,###.00}", items.FEES_AMT);
                        RecordFrm.dgvOtherFees[11, i].Value = string.Format("{0:#,###.00}", GetOtherFeesValue(sFeesCode));
                    }
                }
                catch { }
            }

            ComputeTotal();
           
        }

        private string GetAddlFeesValue(string sValue)
        {
            OracleResultSet result = new OracleResultSet();
            double dValue = 0;
            result.Query = $"select amount1 from addl_schedules where fees_code = '{sValue}'";
            if (result.Execute())
                if (result.Read())
                {
                    dValue = result.GetDouble("amount1");
                    sValue = dValue.ToString();
                }
                else
                    sValue = "0";
            else
                sValue = "0";
            return sValue;
        }

        private string GetOtherFeesValue(string sValue)
        {
            OracleResultSet result = new OracleResultSet();
            double dValue = 0;
            result.Query = $"select amount1 from other_schedules where fees_code = '{sValue}'";
            if (result.Execute())
                if (result.Read())
                {
                    dValue = result.GetDouble("amount1");
                    sValue = dValue.ToString();
                }
                else
                    sValue = "0";
            else
                sValue = "0";
            return sValue;
        }

        public void CellClickOthersCELL(object sender, DataGridViewCellEventArgs e)
        {
            m_sFeesCode = string.Empty;
            m_sFeesMeans = string.Empty;
            m_sCumulative = string.Empty;
            string sValue = string.Empty;
            bool bSelect = false;

            try
            {
                m_iAssessmentRow = e.RowIndex;
                m_sFeesCode = RecordFrm.dgvOtherFees[2, m_iAssessmentRow].Value.ToString();
                m_sFeesMeans = RecordFrm.dgvOtherFees[3, m_iAssessmentRow].Value.ToString();
                m_sCumulative = RecordFrm.dgvOtherFees[6, m_iAssessmentRow].Value.ToString();

                RecordFrm.txtOtherFeesDesc.Text = RecordFrm.dgvOtherFees[1, m_iAssessmentRow].Value.ToString();
                m_sAddlFeeCode = m_sFeesCode;
                RecordFrm.txtOtherAmt.Text = string.Empty;

                ComputeTotal();
            }
            catch { }
        }

        public void CellClickAddOnCELL(object sender, DataGridViewCellEventArgs e)
        {
            m_sFeesCode = string.Empty;
            m_sFeesMeans = string.Empty;
            m_sCumulative = string.Empty;
            string sValue = string.Empty;
            bool bSelect = false;

            try
            {
                m_iAssessmentRow = e.RowIndex;
                m_sFeesCode = RecordFrm.dgvAddOnFees[2, m_iAssessmentRow].Value.ToString();
                m_sFeesMeans = RecordFrm.dgvAddOnFees[3, m_iAssessmentRow].Value.ToString();
                m_sCumulative = RecordFrm.dgvAddOnFees[6, m_iAssessmentRow].Value.ToString();

                RecordFrm.txtAddOnFeesDesc.Text = RecordFrm.dgvAddOnFees[1, m_iAssessmentRow].Value.ToString();
                m_sAddlFeeCode = m_sFeesCode;
                RecordFrm.txtAddOnAmt.Text = string.Empty;

                ComputeTotal();

            }
            catch (Exception ex)
            { }
        }


        public void CellClickOthers(object sender, DataGridViewCellEventArgs e)
        {
            m_sFeesCode = string.Empty;
            m_sFeesMeans = string.Empty;
            m_sCumulative = string.Empty;
            string sValue = string.Empty;
            double dUnitAmtTmp = 0;
            bool bSelect = false;

            try
            {
                m_iAssessmentRow = e.RowIndex;
                m_sFeesCode = RecordFrm.dgvOtherFees[2, m_iAssessmentRow].Value.ToString();
                m_sFeesMeans = RecordFrm.dgvOtherFees[3, m_iAssessmentRow].Value.ToString();
                m_sCumulative = RecordFrm.dgvOtherFees[6, m_iAssessmentRow].Value.ToString();

                double.TryParse(RecordFrm.dgvOtherFees[7, m_iAssessmentRow].Value.ToString(), out dUnitAmtTmp);
                // DisplayParameters(m_iAssessmentRow);

                if (e.ColumnIndex == 0) //AFM 20220202 moved overrding of amount to datagridview from separate textbox
                {
                    RecordFrm.dgvOtherFees.ReadOnly = false;

                    bSelect = (bool)RecordFrm.dgvOtherFees[0, m_iAssessmentRow].Value;
                    if (!bSelect)
                    {
                        sValue = GetOtherFeesValue(Convert.ToString(RecordFrm.dgvOtherFees[2, m_iAssessmentRow].Value));
                        RecordFrm.dgvOtherFees[8, m_iAssessmentRow].Value = sValue;

                        RecordFrm.dgvOtherFees.BeginEdit(true);
                        RecordFrm.dgvOtherFees.EndEdit();

                        SaveBillOthersTmp(m_sFeesCode, Convert.ToDouble(sValue));
                        RecordFrm.dgvOtherFees[0, m_iAssessmentRow].Value = true;
                        RecordFrm.txtOtherAmt.ReadOnly = false;
                    }
                    else
                    {
                        RecordFrm.dgvOtherFees[0, m_iAssessmentRow].Value = false;
                        RecordFrm.dgvOtherFees[7, m_iAssessmentRow].Value = "0";
                        RecordFrm.dgvOtherFees[8, m_iAssessmentRow].Value = "0";

                        RecordFrm.dgvOtherFees.BeginEdit(true);
                        RecordFrm.dgvOtherFees.EndEdit();

                        SaveBillOthersTmp(m_sFeesCode, 0);
                        RecordFrm.dgvOtherFees.ReadOnly = true;

                    }
                }
                else if (e.ColumnIndex == 7 || (e.ColumnIndex == 8)) //AFM 20220202 moved overrding of amount to datagridview from separate textbox
                {
                    RecordFrm.dgvAssessment.ReadOnly = false; //AFM 20210302
                }
                else
                {
                    RecordFrm.dgvOtherFees.ReadOnly = true;
                }

                RecordFrm.txtOtherFeesDesc.Text = RecordFrm.dgvOtherFees[1, m_iAssessmentRow].Value.ToString();
                m_sAddlFeeCode = m_sFeesCode;
                RecordFrm.txtOtherAmt.Text = string.Empty;

                ComputeTotal();

            }
            catch { }
        }

        public void CellClickAddOn(object sender, DataGridViewCellEventArgs e)
        {
            m_sFeesCode = string.Empty;
            m_sFeesMeans = string.Empty;
            m_sCumulative = string.Empty;
            string sValue = string.Empty;
            double dUnitAmtTmp = 0;
            bool bSelect = false;

            try
            {
                m_iAssessmentRow = e.RowIndex;
                m_sFeesCode = RecordFrm.dgvAddOnFees[2, m_iAssessmentRow].Value.ToString();
                m_sFeesMeans = RecordFrm.dgvAddOnFees[3, m_iAssessmentRow].Value.ToString();
                m_sCumulative = RecordFrm.dgvAddOnFees[6, m_iAssessmentRow].Value.ToString();

                double.TryParse(RecordFrm.dgvAddOnFees[7, m_iAssessmentRow].Value.ToString(), out dUnitAmtTmp);

                // DisplayParameters(m_iAssessmentRow);

                if (e.ColumnIndex == 0)
                {
                    RecordFrm.dgvAddOnFees.ReadOnly = false;

                    bSelect = (bool)RecordFrm.dgvAddOnFees[0, m_iAssessmentRow].Value;
                    if (!bSelect)
                    {
                        sValue = GetOtherFeesValue(Convert.ToString(RecordFrm.dgvAddOnFees[2, m_iAssessmentRow].Value));
                        RecordFrm.dgvAddOnFees[8, m_iAssessmentRow].Value = sValue;

                        RecordFrm.dgvAddOnFees.BeginEdit(true);
                        RecordFrm.dgvAddOnFees.EndEdit();

                        SaveBillAddOnTmp(m_sFeesCode, Convert.ToDouble(sValue));
                        RecordFrm.dgvAddOnFees[0, m_iAssessmentRow].Value = true;
                        RecordFrm.txtAddOnAmt.ReadOnly = false;
                    }
                    else
                    {
                        RecordFrm.dgvAddOnFees[0, m_iAssessmentRow].Value = false;
                        RecordFrm.dgvAddOnFees[7, m_iAssessmentRow].Value = "0";
                        RecordFrm.dgvAddOnFees[8, m_iAssessmentRow].Value = "0";

                        RecordFrm.dgvAddOnFees.BeginEdit(true);
                        RecordFrm.dgvAddOnFees.EndEdit();

                        SaveBillAddOnTmp(m_sFeesCode, 0);
                        RecordFrm.txtAddOnAmt.ReadOnly = true;

                    }
                }
                else if (e.ColumnIndex == 7 || (e.ColumnIndex == 8)) //AFM 20220202 moved overrding of amount to datagridview from separate textbox
                {
                    RecordFrm.dgvAssessment.ReadOnly = false; //AFM 20210302
                }
                else
                {
                    RecordFrm.dgvAddOnFees.ReadOnly = true;
                }

                RecordFrm.txtAddOnFeesDesc.Text = RecordFrm.dgvAddOnFees[1, m_iAssessmentRow].Value.ToString();
                m_sAddlFeeCode = m_sFeesCode;
                RecordFrm.txtAddOnAmt.Text = string.Empty;

                ComputeTotal();

            }
            catch (Exception ex)
            { }
        }

        public void CellClick(object sender, DataGridViewCellEventArgs e)
        {
            m_sFeesCode = string.Empty;
            m_sFeesMeans = string.Empty;
            m_sCumulative = string.Empty;
            double dUnitAmtTmp = 0;
            bool bSelect = false;

            try
            {
                m_iAssessmentRow = e.RowIndex;
                m_sFeesCode = RecordFrm.dgvAssessment[2, m_iAssessmentRow].Value.ToString();
                m_sFeesMeans = RecordFrm.dgvAssessment[3, m_iAssessmentRow].Value.ToString();
                m_sCumulative = RecordFrm.dgvAssessment[6, m_iAssessmentRow].Value.ToString();
                DisplayParameters(m_iAssessmentRow);
                double.TryParse(RecordFrm.dgvAssessment[7, m_iAssessmentRow].Value.ToString(), out dUnitAmtTmp);

                if (e.ColumnIndex == 0)
                {
                    RecordFrm.dgvAssessment.ReadOnly = false;
                    
                    bSelect = (bool)RecordFrm.dgvAssessment[0, m_iAssessmentRow].Value;
                    if (!bSelect)
                    {
                        RecordFrm.dgvAssessment[0, m_iAssessmentRow].Value = true;
                    }
                    else
                    {
                        RecordFrm.dgvAssessment[0, m_iAssessmentRow].Value = false;
                        RecordFrm.dgvAssessment[7, m_iAssessmentRow].Value = "0";
                        RecordFrm.dgvAssessment[8, m_iAssessmentRow].Value = "0";

                        SaveBillTmp(m_sFeesCode, 0);

                    }
                }
                else if (e.ColumnIndex == 7 || (e.ColumnIndex == 8 && dUnitAmtTmp > 0)) //AFM 20220202 moved overrding of amount to datagridview from separate textbox
                {                   
                    RecordFrm.dgvAssessment.ReadOnly = false; //AFM 20210302
                }
                else
                {
                    RecordFrm.dgvAssessment.ReadOnly = true;
                }


                RecordFrm.txtAddlFees.Text = RecordFrm.dgvAssessment[1, m_iAssessmentRow].Value.ToString();
                m_sAddlFeeCode = m_sFeesCode;
                RecordFrm.txtAddlAmt.Text = string.Empty;

            }
            catch { }
        }

        private void DisplayParameters(int iRow)
        {
            
            try
            {
                string sFeesMeans = string.Empty;
                string sUnit = string.Empty;
                try
                {
                    sFeesMeans = RecordFrm.dgvAssessment[3, iRow].Value.ToString();
                }
                catch { }
                try
                {
                    sUnit = RecordFrm.dgvAssessment[4, iRow].Value.ToString();
                }
                catch { }

                RecordFrm.dgvParameter.Rows.Clear();

                if (sFeesMeans == "FA")
                {
                    string sQuery = string.Empty;
                    string sFeesCode = string.Empty;

                    try { sFeesCode = RecordFrm.dgvAssessment[2, iRow].Value.ToString(); }
                    catch { }
                    var db = new EPSConnection(dbConn);
                    sQuery = $"select amount1 from schedules where fees_code = '{sFeesCode}'";
                    m_dArea = db.Database.SqlQuery<double>(sQuery).SingleOrDefault();
                    RecordFrm.dgvParameter.Rows.Add("Fixed Amount ", m_dArea);
                }
                else
                {
                    RecordFrm.dgvParameter.Rows.Add("Enter " + sUnit, m_dArea);// default value is the total bldg flr area

                    if (sFeesMeans == "AR")
                    {
                        RecordFrm.dgvParameter.Rows.Add("No. of Month ", "");
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                MessageBox.Show("error encountered", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        public void CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            double dAmount = 0;            
            try
            {
                double.TryParse(RecordFrm.dgvAssessment[8, e.RowIndex].Value.ToString(), out dAmount);
            }
            catch { }

            if(dAmount == 0)
            {
                RecordFrm.dgvAssessment[0, e.RowIndex].Value = false;
                SaveBillTmp(m_sFeesCode, 0);
            }
            //catch { }
        }

        public void CellLeaveOther(object sender, DataGridViewCellEventArgs e)
        {
            double dAmount = 0;
            try
            {
                double.TryParse(RecordFrm.dgvOtherFees[8, e.RowIndex].Value.ToString(), out dAmount);
            }
            catch { }

            if (dAmount == 0)
            {
                RecordFrm.dgvOtherFees[0, e.RowIndex].Value = false;
                SaveBillOthersTmp(m_sFeesCode, 0);
            }
            //catch { }
        }

        public void CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            double.TryParse(RecordFrm.dgvAssessment[RecordFrm.dgvAssessment.CurrentCell.ColumnIndex, RecordFrm.dgvAssessment.CurrentCell.RowIndex].EditedFormattedValue.ToString(), out m_dInputValue);


            if (e.ColumnIndex == 8)
            {
                MainFeesAddOn(sender, e);
            }
        }

        public void CellEndEditOther(object sender, DataGridViewCellEventArgs e)
        {
            double.TryParse(RecordFrm.dgvOtherFees[RecordFrm.dgvOtherFees.CurrentCell.ColumnIndex, RecordFrm.dgvOtherFees.CurrentCell.RowIndex].EditedFormattedValue.ToString(), out m_dInputValue);

            if (e.ColumnIndex == 8)
            {
                OtherFeesAddAddOn(sender, e);
            }
        }

        public void CellEndEditAdditional(object sender, DataGridViewCellEventArgs e)
        {
            double.TryParse(RecordFrm.dgvAddOnFees[RecordFrm.dgvAddOnFees.CurrentCell.ColumnIndex, RecordFrm.dgvAddOnFees.CurrentCell.RowIndex].EditedFormattedValue.ToString(), out m_dInputValue);

            if (e.ColumnIndex == 8)
            {
                AdditionalFeesAddAddOn(sender, e);
            }
        }


        public void ComputeCellOther()
        {
            try
            {
                m_dMonths = 0;
                m_dArea = 0;
                //for (int i = 0; i < RecordFrm.dgvParameter.Rows.Count; i++)
                //{
                //    double dValue = 0;

                //    if (RecordFrm.dgvParameter[1, i].Value == null)
                //        return;

                //    if (RecordFrm.dgvParameter[1, i].Value.ToString() == "")
                //        return;

                //    double.TryParse(RecordFrm.dgvParameter[1, i].Value.ToString(), out dValue);
                //    if (dValue == 0) return;

                //    if (i == 0)
                //        double.TryParse(RecordFrm.dgvParameter[1, i].Value.ToString(), out m_dArea);
                //    if (i == 1)
                //        double.TryParse(RecordFrm.dgvParameter[1, i].Value.ToString(), out m_dMonths);
                //}

                double.TryParse(RecordFrm.txtFlrArea.Text, out m_dArea); //AFM 20210309
                //double.TryParse(RecordFrm.dgvAssessment[RecordFrm.dgvAssessment.CurrentCell.ColumnIndex, RecordFrm.dgvAssessment.CurrentCell.RowIndex].EditedFormattedValue.ToString(), out m_dInputValue);
                double.TryParse(RecordFrm.txtElecTotal.Text, out m_LMTotal);
            }
            catch
            {
                return;
            }

            //AFM 20210321 if computation is based on area/LM(electrical) and if fees code is set to need area
            string sComputeValue = string.Empty;
            bool blnNeedArea = false;
            bool blnNeedLM = false;
            OracleResultSet res = new OracleResultSet();
            res.Query = $"select area_needed, lm_needed from other_subcategories where fees_code = '{m_sFeesCode}'";
            if (res.Execute())
                if (res.Read())
                {
                    if (res.GetString("area_needed") == "Y")
                        blnNeedArea = true;
                    else
                        blnNeedArea = false;

                    if (res.GetString("lm_needed") == "Y")
                        blnNeedLM = true;
                    else
                        blnNeedLM = false;
                }
            res.Close();

            //same computation with Compute() function
            // computation here
            string sQuery = string.Empty;
            var db = new EPSConnection(dbConn);
            double dAmountDue = 0;

            if (m_sFeesMeans == "FA") //AFM 20210405
            {
                double dAmount1 = 0;
                sQuery = $"select amount1 from other_schedules where fees_code = '{m_sFeesCode}'";
                var epsrec = db.Database.SqlQuery<SCHEDULES>(sQuery);

                foreach (var items in epsrec)
                {
                    double.TryParse(items.AMOUNT1.ToString(), out dAmount1);
                }
                dAmountDue = dAmount1;
            }

            else if (m_sFeesMeans == "FR")
            {
                double dRate = 0;

                sQuery = $"select coalesce(rate1,0) as rate1 from other_schedules where fees_code = '{m_sFeesCode}'";
                var epsrec = db.Database.SqlQuery<SCHEDULES>(sQuery);

                foreach (var items in epsrec)
                {
                    double.TryParse(items.RATE1.ToString(), out dRate);
                }
                if (blnNeedArea)
                {
                    dAmountDue = dRate * m_dArea;
                    sComputeValue = "AREA";
                }
                else if (blnNeedLM)
                {
                    dAmountDue = dRate * m_LMTotal; //AFM 20210323 compute due based on electrical LM total
                    sComputeValue = "LM";
                }
                else
                    dAmountDue = dRate * m_dInputValue;


            }
            else if (m_sFeesMeans == "QN")
            {
                double dAmt1 = 0;
                double dAmt2 = 0;

                sQuery = $"select coalesce(amount1,0) as amount1, coalesce(amount2,0) as amount2 from other_schedules where fees_code = '{m_sFeesCode}'";
                var epsrec = db.Database.SqlQuery<SCHEDULES>(sQuery);

                foreach (var items in epsrec)
                {
                    double.TryParse(items.AMOUNT1.ToString(), out dAmt1);
                    double.TryParse(items.AMOUNT2.ToString(), out dAmt2);
                }
                if (blnNeedArea)
                {
                    dAmountDue = dAmt1 * m_dArea;
                    sComputeValue = "AREA";
                }
                else if (blnNeedLM)
                {
                    dAmountDue = dAmt1 * m_LMTotal; //AFM 20210323 compute due based on electrical LM total
                    sComputeValue = "LM";
                }
                else
                    dAmountDue = dAmt1 * m_dInputValue;

                if (dAmountDue < dAmt2)
                    dAmountDue = dAmt2;

            }
            else if (m_sFeesMeans == "QR")
            {
                double dAmt1 = 0;
                double dRate2 = 0;
                double dQty1 = 0;
                double dQty = 0;

                var epsrec = db.Database.SqlQuery<SCHEDULES>("select * from other_schedules"); //instantiate

                if (blnNeedArea == true) //AFM 20210323
                {
                    sQuery = $"select coalesce(amount1,0) as amount1, coalesce(rate2,0) as rate2, coalesce(qty1,0) as qty1 from other_schedules where  ";
                    sQuery += $"qty1 <= {m_dArea} and qty2 >= {m_dArea} and fees_code = '{m_sFeesCode}'";
                    epsrec = db.Database.SqlQuery<SCHEDULES>(sQuery);
                }
                else if (blnNeedLM == true) //AFM 20210323
                {
                    sQuery = $"select coalesce(amount1,0) as amount1, coalesce(rate2,0) as rate2, coalesce(qty1,0) as qty1 from other_schedules where  ";
                    sQuery += $"qty1 <= {m_LMTotal} and qty2 >= {m_LMTotal} and fees_code = '{m_sFeesCode}'";
                    epsrec = db.Database.SqlQuery<SCHEDULES>(sQuery);
                }
                else
                {
                    sQuery = $"select coalesce(amount1,0) as amount1, coalesce(rate2,0) as rate2, coalesce(qty1,0) as qty1 from other_schedules where  ";
                    sQuery += $"qty1 <= {m_dInputValue} and qty2 >= {m_dInputValue} and fees_code = '{m_sFeesCode}'";
                    epsrec = db.Database.SqlQuery<SCHEDULES>(sQuery);
                }


                foreach (var items in epsrec)
                {
                    if (m_sCumulative == "N")
                    {
                        double.TryParse(items.AMOUNT1.ToString(), out dAmt1);
                        double.TryParse(items.RATE2.ToString(), out dRate2);
                        double.TryParse(items.QTY1.ToString(), out dQty1);

                        if (dRate2 > 0)
                        {
                            if (blnNeedArea)
                            {
                                dQty = m_dArea - (dQty1 - .01);
                                sComputeValue = "AREA";
                            }
                            else if (blnNeedLM)
                            {
                                dQty = m_LMTotal - (dQty1 - .01); //AFM 20210323 compute due based on electrical LM total
                                sComputeValue = "LM";
                            }
                            else
                                dQty = m_dInputValue - (dQty1 - .01);

                            dAmountDue = (dQty * dRate2) + dAmt1;
                        }
                        else
                        {
                            dAmountDue = dAmt1;
                        }

                    }
                    else
                        dAmountDue = ComputeCumulative(m_sFeesCode, m_dArea);
                }
            }
            else if (m_sFeesMeans == "RR")
            {
                var epsrec = db.Database.SqlQuery<SCHEDULES>("select * from other_schedules"); //instantiate
                if (blnNeedArea)
                {
                    sQuery = $"select coalesce(amount1,0) as amount1,coalesce(rate1,0) as rate1 from other_schedules where range1 <= ";
                    sQuery += $"{m_dArea} and range2 >= {m_dArea} and fees_code = '{m_sFeesCode}'";
                    epsrec = db.Database.SqlQuery<SCHEDULES>(sQuery);
                }
                else
                {
                    sQuery = $"select coalesce(amount1,0) as amount1,coalesce(rate1,0) as rate1 from other_schedules where range1 <= ";
                    sQuery += $"{m_dInputValue} and range2 >= {m_dInputValue} and fees_code = '{m_sFeesCode}'";
                    epsrec = db.Database.SqlQuery<SCHEDULES>(sQuery);
                }



                foreach (var items in epsrec)
                {
                    if (m_sCumulative == "N")
                    {
                        double dRate = 0;
                        double dTemp = 0;
                        double.TryParse(items.RATE1.ToString(), out dRate);
                        double.TryParse(items.AMOUNT1.ToString(), out dTemp);

                        if (dRate != 0)
                        {
                            if (blnNeedArea)
                            {
                                dAmountDue = dTemp * m_dArea * dRate;
                                sComputeValue = "AREA";
                            }
                            else if (blnNeedLM)
                            {
                                dAmountDue = dTemp * m_LMTotal * dRate;
                                sComputeValue = "LM";
                            }
                            else
                                dAmountDue = dTemp * m_dInputValue * dRate;
                        }
                        else
                        {
                            if (blnNeedArea)
                            {
                                dAmountDue = dTemp * m_dArea;
                                sComputeValue = "AREA";
                            }
                            else if (blnNeedLM)
                            {
                                dAmountDue = dTemp * m_LMTotal;
                                sComputeValue = "LM";
                            }
                            else
                                dAmountDue = dTemp * m_dInputValue;
                        }

                    }
                    else
                        dAmountDue = ComputeCumulative(m_sFeesCode, m_dArea);
                }
            }
            else if (m_sFeesMeans == "AR")
            {
                sQuery = $"select coalesce(amount1,0) as AMOUNT1, coalesce(rate2,0) as RATE2, coalesce(range1,0) as RANGE1 from other_schedules where range1 <= ";
                sQuery += $"{m_dArea} and range2 >= {m_dArea} and fees_code = '{m_sFeesCode}'";
                var epsrec = db.Database.SqlQuery<SCHEDULES>(sQuery);

                foreach (var items in epsrec)
                {
                    if (m_sCumulative == "N")
                    {
                        double dAmt1 = 0;
                        double dRate2 = 0;
                        double dRange1 = 0;
                        double.TryParse(items.AMOUNT1.ToString(), out dAmt1);
                        double.TryParse(items.RATE2.ToString(), out dRate2);
                        double.TryParse(items.RANGE1.ToString(), out dRange1);

                        if (dRate2 > 0)
                        {
                            dRange1 = m_dArea - (dRange1 - .01);
                            dAmountDue = (dRange1 * dRate2) + dAmt1;
                            dAmountDue = dAmountDue * m_dMonths;
                        }
                        else
                            dAmountDue = dAmt1 * m_dArea;
                    }
                    else
                        dAmountDue = ComputeCumulative(m_sFeesCode, m_dArea);
                    sComputeValue = "AREA";
                }
            }
            else if (m_sFeesMeans == "Override")
            {
                dAmountDue = m_dArea;
            }
            else
                dAmountDue = m_dArea;

            if (dAmountDue <= 0)
            {
                MessageBox.Show("Invalid Amount/Rate, Pls. Check!", "Billing", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            RecordFrm.txtAmount.Text = string.Format("{0:#,###.00}", dAmountDue);
            ButtonOkOther(sComputeValue);
        }

        public void ComputeCell() //AFM 20210303 new design of computation of amount as per ma'am Mitch's design
        {
            try
            {
                m_dMonths = 0;
                m_dArea = 0;
                //for (int i = 0; i < RecordFrm.dgvParameter.Rows.Count; i++)
                //{
                //    double dValue = 0;

                //    if (RecordFrm.dgvParameter[1, i].Value == null)
                //        return;

                //    if (RecordFrm.dgvParameter[1, i].Value.ToString() == "")
                //        return;

                //    double.TryParse(RecordFrm.dgvParameter[1, i].Value.ToString(), out dValue);
                //    if (dValue == 0) return;

                //    if (i == 0)
                //        double.TryParse(RecordFrm.dgvParameter[1, i].Value.ToString(), out m_dArea);
                //    if (i == 1)
                //        double.TryParse(RecordFrm.dgvParameter[1, i].Value.ToString(), out m_dMonths);
                //}

                double.TryParse(RecordFrm.txtFlrArea.Text, out m_dArea); //AFM 20210309
                //double.TryParse(RecordFrm.dgvAssessment[RecordFrm.dgvAssessment.CurrentCell.ColumnIndex, RecordFrm.dgvAssessment.CurrentCell.RowIndex].EditedFormattedValue.ToString(), out m_dInputValue);
                double.TryParse(RecordFrm.txtElecTotal.Text, out m_LMTotal);
            }
            catch
            {
                return;
            }

            //AFM 20210321 if computation is based on area/LM(electrical) and if fees code is set to need area
            string sComputeValue = string.Empty;
            bool blnNeedArea = false;
            bool blnNeedLM = false;
            OracleResultSet res = new OracleResultSet();
            res.Query = $"select area_needed, lm_needed from subcategories where fees_code = '{m_sFeesCode}'";
            if(res.Execute())
                if(res.Read())
                {
                    if (res.GetString("area_needed") == "Y")
                        blnNeedArea = true;
                    else
                        blnNeedArea = false;

                    if (res.GetString("lm_needed") == "Y")
                        blnNeedLM = true;
                    else
                        blnNeedLM = false;
                }
            res.Close();

            //same computation with Compute() function
            // computation here
            string sQuery = string.Empty;
            var db = new EPSConnection(dbConn);
            double dAmountDue = 0;

            if(m_sFeesMeans == "FA") //AFM 20210405
            {
                double dAmount1 = 0;
                sQuery = $"select amount1 from schedules where fees_code = '{m_sFeesCode}'";
                var epsrec = db.Database.SqlQuery<SCHEDULES>(sQuery);

                foreach (var items in epsrec)
                {
                    double.TryParse(items.AMOUNT1.ToString(), out dAmount1);
                }
                dAmountDue = dAmount1;
            }

            else if (m_sFeesMeans == "FR")
            {
                double dRate = 0;

                sQuery = $"select coalesce(rate1,0) as rate1 from schedules where fees_code = '{m_sFeesCode}'";
                var epsrec = db.Database.SqlQuery<SCHEDULES>(sQuery);

                foreach (var items in epsrec)
                {
                    double.TryParse(items.RATE1.ToString(), out dRate);
                }
                if(blnNeedArea)
                {
                    dAmountDue = dRate * m_dArea;
                    sComputeValue = "AREA";
                }
                else if(blnNeedLM)
                {
                    dAmountDue = dRate * m_LMTotal; //AFM 20210323 compute due based on electrical LM total
                    sComputeValue = "LM";
                }
                else
                    dAmountDue = dRate * m_dInputValue;


            }
            else if (m_sFeesMeans == "QN")
            {
                double dAmt1 = 0;
                double dAmt2 = 0;

                sQuery = $"select coalesce(amount1,0) as amount1, coalesce(amount2,0) as amount2 from schedules where fees_code = '{m_sFeesCode}'";
                var epsrec = db.Database.SqlQuery<SCHEDULES>(sQuery);

                foreach (var items in epsrec)
                {
                    double.TryParse(items.AMOUNT1.ToString(), out dAmt1);
                    double.TryParse(items.AMOUNT2.ToString(), out dAmt2);
                }
                if(blnNeedArea)
                {
                    dAmountDue = dAmt1 * m_dArea;
                    sComputeValue = "AREA";
                }
                else if (blnNeedLM)
                {
                    dAmountDue = dAmt1 * m_LMTotal; //AFM 20210323 compute due based on electrical LM total
                    sComputeValue = "LM";
                }
                else
                    dAmountDue = dAmt1 * m_dInputValue;

                if (dAmountDue < dAmt2)
                    dAmountDue = dAmt2;

            }
            else if (m_sFeesMeans == "QR")
            {
                double dAmt1 = 0;
                double dRate2 = 0;
                double dQty1 = 0;
                double dQty = 0;

                var epsrec = db.Database.SqlQuery<SCHEDULES>("select * from schedules"); //instantiate

                if (blnNeedArea == true) //AFM 20210323
                {
                    sQuery = $"select coalesce(amount1,0) as amount1, coalesce(rate2,0) as rate2, coalesce(qty1,0) as qty1 from schedules where  ";
                    sQuery += $"qty1 <= {m_dArea} and qty2 >= {m_dArea} and fees_code = '{m_sFeesCode}'";
                    epsrec = db.Database.SqlQuery<SCHEDULES>(sQuery);
                }
                else if (blnNeedLM == true) //AFM 20210323
                {
                    sQuery = $"select coalesce(amount1,0) as amount1, coalesce(rate2,0) as rate2, coalesce(qty1,0) as qty1 from schedules where  ";
                    sQuery += $"qty1 <= {m_LMTotal} and qty2 >= {m_LMTotal} and fees_code = '{m_sFeesCode}'";
                    epsrec = db.Database.SqlQuery<SCHEDULES>(sQuery);
                }
                else
                {
                    sQuery = $"select coalesce(amount1,0) as amount1, coalesce(rate2,0) as rate2, coalesce(qty1,0) as qty1 from schedules where  ";
                    sQuery += $"qty1 <= {m_dInputValue} and qty2 >= {m_dInputValue} and fees_code = '{m_sFeesCode}'";
                    epsrec = db.Database.SqlQuery<SCHEDULES>(sQuery);
                }


                foreach (var items in epsrec)
                {
                    if (m_sCumulative == "N")
                    {
                        double.TryParse(items.AMOUNT1.ToString(), out dAmt1);
                        double.TryParse(items.RATE2.ToString(), out dRate2);
                        double.TryParse(items.QTY1.ToString(), out dQty1);

                        if (dRate2 > 0)
                        {
                            if(blnNeedArea)
                            {
                                dQty = m_dArea - (dQty1 - .01);
                                sComputeValue = "AREA";
                            }
                            else if (blnNeedLM)
                            {
                                dQty = m_LMTotal - (dQty1 - .01); //AFM 20210323 compute due based on electrical LM total
                                sComputeValue = "LM";
                            }
                            else
                                dQty = m_dInputValue - (dQty1 - .01);

                            dAmountDue = (dQty * dRate2) + dAmt1;
                        }
                        else
                        {
                            dAmountDue = dAmt1;
                        }

                    }
                    else
                        dAmountDue = ComputeCumulative(m_sFeesCode, m_dArea);
                }
            }
            else if (m_sFeesMeans == "RR")
            {
                var epsrec = db.Database.SqlQuery<SCHEDULES>("select * from schedules"); //instantiate
                if (blnNeedArea)
                {
                    sQuery = $"select coalesce(amount1,0) as amount1,coalesce(rate1,0) as rate1 from schedules where range1 <= ";
                    sQuery += $"{m_dArea} and range2 >= {m_dArea} and fees_code = '{m_sFeesCode}'";
                    epsrec = db.Database.SqlQuery<SCHEDULES>(sQuery);
                }
                else
                {
                    sQuery = $"select coalesce(amount1,0) as amount1,coalesce(rate1,0) as rate1 from schedules where range1 <= ";
                    sQuery += $"{m_dInputValue} and range2 >= {m_dInputValue} and fees_code = '{m_sFeesCode}'";
                    epsrec = db.Database.SqlQuery<SCHEDULES>(sQuery);
                }

                

                foreach (var items in epsrec)
                {
                    if (m_sCumulative == "N")
                    {
                        double dRate = 0;
                        double dTemp = 0;
                        double.TryParse(items.RATE1.ToString(), out dRate);
                        double.TryParse(items.AMOUNT1.ToString(), out dTemp);

                        if (dRate != 0)
                        {
                            if (blnNeedArea)
                            {
                                dAmountDue = dTemp * m_dArea * dRate;
                                sComputeValue = "AREA";
                            }
                            else if(blnNeedLM)
                            {
                                dAmountDue = dTemp * m_LMTotal * dRate;
                                sComputeValue = "LM";
                            }
                            else
                                dAmountDue = dTemp * m_dInputValue * dRate;
                        }
                        else
                        {
                            if (blnNeedArea)
                            {
                                dAmountDue = dTemp * m_dArea;
                                sComputeValue = "AREA";
                            }
                            else if(blnNeedLM)
                            {
                                dAmountDue = dTemp * m_LMTotal;
                                sComputeValue = "LM";
                            }
                            else
                                dAmountDue = dTemp * m_dInputValue;
                        }

                    }
                    else
                        dAmountDue = ComputeCumulative(m_sFeesCode, m_dArea);
                }
            }
            else if (m_sFeesMeans == "AR")
            {
                sQuery = $"select coalesce(amount1,0) as AMOUNT1, coalesce(rate2,0) as RATE2, coalesce(range1,0) as RANGE1 from schedules where range1 <= ";
                sQuery += $"{m_dArea} and range2 >= {m_dArea} and fees_code = '{m_sFeesCode}'";
                var epsrec = db.Database.SqlQuery<SCHEDULES>(sQuery);

                foreach (var items in epsrec)
                {
                    if (m_sCumulative == "N")
                    {
                        double dAmt1 = 0;
                        double dRate2 = 0;
                        double dRange1 = 0;
                        double.TryParse(items.AMOUNT1.ToString(), out dAmt1);
                        double.TryParse(items.RATE2.ToString(), out dRate2);
                        double.TryParse(items.RANGE1.ToString(), out dRange1);

                        if (dRate2 > 0)
                        {
                            dRange1 = m_dArea - (dRange1 - .01);
                            dAmountDue = (dRange1 * dRate2) + dAmt1;
                            dAmountDue = dAmountDue * m_dMonths;
                        }
                        else
                            dAmountDue = dAmt1 * m_dArea;
                    }
                    else
                        dAmountDue = ComputeCumulative(m_sFeesCode, m_dArea);
                    sComputeValue = "AREA";
                }
            }
            else if (m_sFeesMeans == "Override")
            {
                dAmountDue = m_dArea;
            }
            else
                dAmountDue = m_dArea;

            if (dAmountDue <= 0)
            {
                MessageBox.Show("Invalid Amount/Rate, Pls. Check!", "Billing", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            RecordFrm.txtAmount.Text = string.Format("{0:#,###.00}", dAmountDue);
            ButtonOk(sComputeValue);
            RecordFrm.txtAmount.TextAlign = HorizontalAlignment.Left; //should be right, but left somehow aligns to right
        }

        public void ComputeALL() //AFM 20210325 after editing info this will update all amount in datagridview
        {
            string sFeesMeans = string.Empty;
            string sCumulative = string.Empty;
            foreach(DataGridViewRow row in RecordFrm.dgvAssessment.Rows)
            {
                //AFM 20210321 if computation is based on area/LM(electrical) and if fees code is set to need area
                string sComputeValue = string.Empty;
                bool blnNeedArea = false;
                bool blnNeedLM = false;
                OracleResultSet res = new OracleResultSet();
                res.Query = $"select area_needed, lm_needed from subcategories where fees_code = '{row.Cells[2].Value.ToString()}'";
                if (res.Execute())
                    if (res.Read())
                    {
                        if (res.GetString("area_needed") == "Y")
                            blnNeedArea = true;
                        else
                            blnNeedArea = false;

                        if (res.GetString("lm_needed") == "Y")
                            blnNeedLM = true;
                        else
                            blnNeedLM = false;
                    }
                res.Close();

                string sFeesCode = row.Cells[2].Value.ToString();
                sFeesMeans = row.Cells[3].Value.ToString();
                sCumulative = row.Cells[6].Value.ToString();
                if (blnNeedArea == true || blnNeedLM == true)
                {
                    double dInputAreaLM = 0;
                    if(blnNeedArea)
                    {
                        double.TryParse(RecordFrm.txtFlrArea.Text.Trim(), out dInputAreaLM);
                        row.Cells[7].Value = dInputAreaLM;
                    }
                    else if (blnNeedLM)
                    {
                        double.TryParse(RecordFrm.txtElecTotal.Text.Trim(), out dInputAreaLM);
                        row.Cells[7].Value = dInputAreaLM;
                    }

                    string sQuery = string.Empty;
                    var db = new EPSConnection(dbConn);
                    double dAmountDue = 0;

                    if (sFeesMeans == "FR")
                    {
                        double dRate = 0;

                        sQuery = $"select coalesce(rate1,0) as rate1 from schedules where fees_code = '{sFeesCode}'";
                        var epsrec = db.Database.SqlQuery<SCHEDULES>(sQuery);

                        foreach (var items in epsrec)
                        {
                            double.TryParse(items.RATE1.ToString(), out dRate);
                        }
                        if (blnNeedArea)
                        {
                            dAmountDue = dRate * dInputAreaLM;
                            sComputeValue = "AREA";
                        }
                        else if (blnNeedLM)
                        {
                            dAmountDue = dRate * dInputAreaLM; //AFM 20210323 compute due based on electrical LM total
                            sComputeValue = "LM";
                        }


                    }
                    else if (sFeesMeans == "QN")
                    {
                        double dAmt1 = 0;
                        double dAmt2 = 0;

                        sQuery = $"select coalesce(amount1,0) as amount1, coalesce(amount2,0) as amount2 from schedules where fees_code = '{sFeesCode}'";
                        var epsrec = db.Database.SqlQuery<SCHEDULES>(sQuery);

                        foreach (var items in epsrec)
                        {
                            double.TryParse(items.AMOUNT1.ToString(), out dAmt1);
                            double.TryParse(items.AMOUNT2.ToString(), out dAmt2);
                        }
                        if (blnNeedArea)
                        {
                            dAmountDue = dAmt1 * dInputAreaLM;
                            sComputeValue = "AREA";
                        }
                        else if (blnNeedLM)
                        {
                            dAmountDue = dAmt1 * dInputAreaLM; //AFM 20210323 compute due based on electrical LM total
                            sComputeValue = "LM";
                        }

                        if (dAmountDue < dAmt2)
                            dAmountDue = dAmt2;

                    }
                    else if (sFeesMeans == "QR")
                    {
                        double dAmt1 = 0;
                        double dRate2 = 0;
                        double dQty1 = 0;
                        double dQty = 0;

                        var epsrec = db.Database.SqlQuery<SCHEDULES>("select * from schedules"); //instantiate

                        if (blnNeedArea == true || blnNeedLM == true) //AFM 20210323
                        {
                            sQuery = $"select coalesce(amount1,0) as amount1, coalesce(rate2,0) as rate2, coalesce(qty1,0) as qty1 from schedules where  ";
                            sQuery += $"qty1 <= {dInputAreaLM} and qty2 >= {dInputAreaLM} and fees_code = '{sFeesCode}'";
                            epsrec = db.Database.SqlQuery<SCHEDULES>(sQuery);
                        }

                        foreach (var items in epsrec)
                        {
                            if (sCumulative == "N")
                            {
                                double.TryParse(items.AMOUNT1.ToString(), out dAmt1);
                                double.TryParse(items.RATE2.ToString(), out dRate2);
                                double.TryParse(items.QTY1.ToString(), out dQty1);

                                if (dRate2 > 0)
                                {
                                    if (blnNeedArea)
                                    {
                                        dQty = dInputAreaLM - (dQty1 - .01);
                                        sComputeValue = "AREA";
                                    }
                                    else if (blnNeedLM)
                                    {
                                        dQty = dInputAreaLM - (dQty1 - .01); //AFM 20210323 compute due based on electrical LM total
                                        sComputeValue = "LM";
                                    }

                                    dAmountDue = (dQty * dRate2) + dAmt1;
                                }
                                else
                                {
                                    dAmountDue = dAmt1;
                                }

                            }
                            else
                                dAmountDue = ComputeCumulative(sFeesCode, dInputAreaLM);
                        }
                    }
                    else if (sFeesMeans == "RR")
                    {
                        var epsrec = db.Database.SqlQuery<SCHEDULES>("select * from schedules"); //instantiate
                        if (blnNeedArea == true || blnNeedLM == true)
                        {
                            sQuery = $"select coalesce(amount1,0) as amount1,coalesce(rate1,0) as rate1 from schedules where range1 <= ";
                            sQuery += $"{dInputAreaLM} and range2 >= {dInputAreaLM} and fees_code = '{sFeesCode}'";
                            epsrec = db.Database.SqlQuery<SCHEDULES>(sQuery);
                        }

                        foreach (var items in epsrec)
                        {
                            if (sCumulative == "N")
                            {
                                double dRate = 0;
                                double dTemp = 0;
                                double.TryParse(items.RATE1.ToString(), out dRate);
                                double.TryParse(items.AMOUNT1.ToString(), out dTemp);

                                if (dRate != 0)
                                {
                                    if (blnNeedArea)
                                    {
                                        dAmountDue = dTemp * dInputAreaLM * dRate;
                                        sComputeValue = "AREA";
                                    }
                                    else if (blnNeedLM)
                                    {
                                        dAmountDue = dTemp * dInputAreaLM * dRate;
                                        sComputeValue = "LM";
                                    }
                                }
                                else
                                {
                                    if (blnNeedArea)
                                    {
                                        dAmountDue = dTemp * dInputAreaLM;
                                        sComputeValue = "AREA";
                                    }
                                    else if (blnNeedLM)
                                    {
                                        dAmountDue = dTemp * dInputAreaLM;
                                        sComputeValue = "LM";
                                    }
                                }

                            }
                            else
                                dAmountDue = ComputeCumulative(sFeesCode, dInputAreaLM);
                        }
                    }
                    else if (sFeesMeans == "AR")
                    {
                        sQuery = $"select coalesce(amount1,0) as AMOUNT1, coalesce(rate2,0) as RATE2, coalesce(range1,0) as RANGE1 from schedules where range1 <= ";
                        sQuery += $"{dInputAreaLM} and range2 >= {dInputAreaLM} and fees_code = '{sFeesCode}'";
                        var epsrec = db.Database.SqlQuery<SCHEDULES>(sQuery);

                        foreach (var items in epsrec)
                        {
                            if (sCumulative == "N")
                            {
                                double dAmt1 = 0;
                                double dRate2 = 0;
                                double dRange1 = 0;
                                double.TryParse(items.AMOUNT1.ToString(), out dAmt1);
                                double.TryParse(items.RATE2.ToString(), out dRate2);
                                double.TryParse(items.RANGE1.ToString(), out dRange1);

                                if (dRate2 > 0)
                                {
                                    dRange1 = dInputAreaLM - (dRange1 - .01);
                                    dAmountDue = (dRange1 * dRate2) + dAmt1;
                                    dAmountDue = dAmountDue * m_dMonths;
                                }
                                else
                                    dAmountDue = dAmt1 * dInputAreaLM;
                            }
                            else
                                dAmountDue = ComputeCumulative(sFeesCode, dInputAreaLM);
                            sComputeValue = "AREA";
                        }
                    }
                    else if (m_sFeesMeans == "Override")
                    {
                        dAmountDue = dInputAreaLM;
                    }
                    else
                        dAmountDue = dInputAreaLM;

                    if (dAmountDue <= 0)
                    {
                        MessageBox.Show("Invalid Amount/Rate, Pls. Check!", "Billing", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    RecordFrm.txtAmount.Text = string.Format("{0:#,###.00}", dAmountDue);
                    ButtonOkALL (sComputeValue, dInputAreaLM, sFeesCode);
                    RecordFrm.txtAmount.TextAlign = HorizontalAlignment.Left; //should be right, but left somehow aligns to right
                }
            }
      
           
           
            
        }

        public bool Compute()
        {
            try
            {
                m_dMonths = 0;
                m_dArea = 0;
                for (int i = 0; i < RecordFrm.dgvParameter.Rows.Count; i++)
                {
                    double dValue = 0;

                    if (RecordFrm.dgvParameter[1, i].Value == null)
                        return false;

                    if (RecordFrm.dgvParameter[1, i].Value.ToString() == "")
                        return false;

                    double.TryParse(RecordFrm.dgvParameter[1, i].Value.ToString(), out dValue);
                    if (dValue == 0)return false;

                    if (i == 0)
                        double.TryParse(RecordFrm.dgvParameter[1, i].Value.ToString(), out m_dArea);
                    if (i == 1)
                        double.TryParse(RecordFrm.dgvParameter[1, i].Value.ToString(), out m_dMonths);
                }
            }
            catch
            {
                return false;
            }

            // computation here
            string sQuery = string.Empty;
            var db = new EPSConnection(dbConn);
            double dAmountDue = 0;

            if (m_sFeesMeans == "FR")
            {
                double dRate = 0;

                sQuery = $"select coalesce(rate1,0) as rate1 from schedules where fees_code = '{m_sFeesCode}'";
                var epsrec = db.Database.SqlQuery<SCHEDULES>(sQuery);

                foreach (var items in epsrec)
                {
                    double.TryParse(items.RATE1.ToString(), out dRate);
                }
                dAmountDue = dRate * m_dArea;


            }
            else if (m_sFeesMeans == "QN")
            {
                double dAmt1 = 0;
                double dAmt2 = 0;

                sQuery = $"select coalesce(amount1,0) as amount1, coalesce(amount2,0) as amount2 from schedules where fees_code = '{m_sFeesCode}'";
                var epsrec = db.Database.SqlQuery<SCHEDULES>(sQuery);

                foreach (var items in epsrec)
                {
                    double.TryParse(items.AMOUNT1.ToString(), out dAmt1);
                    double.TryParse(items.AMOUNT2.ToString(), out dAmt2);
                }
                dAmountDue = dAmt1 * m_dArea;
                if (dAmountDue < dAmt2)
                    dAmountDue = dAmt2;

            }
            else if (m_sFeesMeans == "QR")
            {
                double dAmt1 = 0;
                double dRate2 = 0;
                double dQty1 = 0;
                double dQty = 0;

                sQuery = $"select coalesce(amount1,0) as amount1, coalesce(rate2,0) as rate2, coalesce(qty1,0) as qty1 from schedules where  ";
                sQuery += $"qty1 <= {m_dArea} and qty2 >= {m_dArea} and fees_code = '{m_sFeesCode}'";
                var epsrec = db.Database.SqlQuery<SCHEDULES>(sQuery);

                foreach (var items in epsrec)
                {
                    if (m_sCumulative == "N")
                    {
                        double.TryParse(items.AMOUNT1.ToString(), out dAmt1);
                        double.TryParse(items.RATE2.ToString(), out dRate2);
                        double.TryParse(items.QTY1.ToString(), out dQty1);

                        if (dRate2 > 0)
                        {
                            dQty = m_dArea - (dQty1 - .01);
                            dAmountDue = (dQty * dRate2) + dAmt1;
                        }
                        else
                        {
                            dAmountDue = dAmt1;
                        }

                    }
                    else
                        dAmountDue = ComputeCumulative(m_sFeesCode, m_dArea);
                }
            }
            else if (m_sFeesMeans == "RR")
            {
                sQuery = $"select coalesce(amount1,0) as amount1,coalesce(rate1,0) as rate1 from schedules where range1 <= ";
                sQuery += $"{m_dArea} and range2 >= {m_dArea} and fees_code = '{m_sFeesCode}'";
                var epsrec = db.Database.SqlQuery<SCHEDULES>(sQuery);

                foreach (var items in epsrec)
                {
                    if (m_sCumulative == "N")
                    {
                        double dRate = 0;
                        double dTemp = 0;
                        double.TryParse(items.RATE1.ToString(), out dRate);
                        double.TryParse(items.AMOUNT1.ToString(), out dTemp);

                        if (dRate != 0)
                        {
                            dAmountDue = dTemp * m_dArea * dRate;
                        }
                        else
                        {
                            dAmountDue = dTemp * m_dArea;
                        }

                    }
                    else
                        dAmountDue = ComputeCumulative(m_sFeesCode, m_dArea);
                }
            }
            else if (m_sFeesMeans == "AR")
            {
                sQuery = $"select coalesce(amount1,0) as AMOUNT1, coalesce(rate2,0) as RATE2, coalesce(range1,0) as RANGE1 from schedules where range1 <= ";
                sQuery += $"{m_dArea} and range2 >= {m_dArea} and fees_code = '{m_sFeesCode}'";
                var epsrec = db.Database.SqlQuery<SCHEDULES>(sQuery);

                foreach (var items in epsrec)
                {
                    if (m_sCumulative == "N")
                    {
                        double dAmt1 = 0;
                        double dRate2 = 0;
                        double dRange1 = 0;
                        double.TryParse(items.AMOUNT1.ToString(), out dAmt1);
                        double.TryParse(items.RATE2.ToString(), out dRate2);
                        double.TryParse(items.RANGE1.ToString(), out dRange1);

                        if (dRate2 > 0)
                        {
                            dRange1 = m_dArea - (dRange1 - .01);
                            dAmountDue = (dRange1 * dRate2) + dAmt1;
                            dAmountDue = dAmountDue * m_dMonths;
                        }
                        else
                            dAmountDue = dAmt1 * m_dArea;
                    }
                    else
                        dAmountDue = ComputeCumulative(m_sFeesCode, m_dArea);
                }
            }
            else if (m_sFeesMeans == "Override")
            {
                dAmountDue = m_dArea;
            }
            else
                dAmountDue = m_dArea;

            if (dAmountDue <= 0)
            {
                MessageBox.Show("Invalid Amount/Rate, Pls. Check!", "Billing", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            RecordFrm.txtAmount.Text = string.Format("{0:#,###.00}", dAmountDue);
            RecordFrm.txtAmount.TextAlign = HorizontalAlignment.Left; //should be right, but left somehow aligns to right
            return true;
        }

        public void ButtonOk(string sVal)
        {
            try
            {
                if(sVal == "AREA")
                    RecordFrm.dgvAssessment[7, m_iAssessmentRow].Value = m_dArea;
                else if(sVal == "LM")
                    RecordFrm.dgvAssessment[7, m_iAssessmentRow].Value = m_LMTotal;

                RecordFrm.dgvAssessment[8, m_iAssessmentRow].Value = RecordFrm.txtAmount.Text;
                RecordFrm.dgvAssessment[11, m_iAssessmentRow].Value = RecordFrm.txtAmount.Text;

                double dAmountDue = 0;

                double.TryParse(RecordFrm.txtAmount.Text.ToString(), out dAmountDue);

                if (dAmountDue > 0)
                {
                    RecordFrm.dgvAssessment[0, m_iAssessmentRow].Value = true;
                    SaveBillTmp(RecordFrm.dgvAssessment[2, m_iAssessmentRow].Value.ToString(), dAmountDue);
                    RecordFrm.txtAmount.Text = "";  // initialize
                }
            }
            catch { }
        }

        public void ButtonOkOther(string sVal)
        {
            try
            {
                if (sVal == "AREA")
                    RecordFrm.dgvOtherFees[7, m_iAssessmentRow].Value = m_dArea;
                else if (sVal == "LM")
                    RecordFrm.dgvOtherFees[7, m_iAssessmentRow].Value = m_LMTotal;

                double dAmountDue = 0;

                RecordFrm.dgvOtherFees[8, m_iAssessmentRow].Value = RecordFrm.txtAmount.Text;
                RecordFrm.dgvOtherFees[11, m_iAssessmentRow].Value = RecordFrm.txtAmount.Text;
                double.TryParse(RecordFrm.txtAmount.Text.ToString(), out dAmountDue);

                if (dAmountDue > 0)
                {
                    RecordFrm.dgvOtherFees[0, m_iAssessmentRow].Value = true;
                    SaveBillOthersTmp(RecordFrm.dgvOtherFees[2, m_iAssessmentRow].Value.ToString(), dAmountDue);
                    RecordFrm.txtAmount.Text = "";  // initialize
                }
            }
            catch { }
        }

        public void ButtonOkALL(string sVal, double dInputAreaLM, string sFeesCode)
        {
            try
            {
                if (sVal == "AREA")
                    RecordFrm.dgvAssessment[7, m_iAssessmentRow].Value = dInputAreaLM;
                else if (sVal == "LM")
                    RecordFrm.dgvAssessment[7, m_iAssessmentRow].Value = dInputAreaLM;

                RecordFrm.dgvAssessment[8, m_iAssessmentRow].Value = RecordFrm.txtAmount.Text;
                RecordFrm.dgvAssessment[11, m_iAssessmentRow].Value = RecordFrm.txtAmount.Text;

                double dAmountDue = 0;

                double.TryParse(RecordFrm.txtAmount.Text.ToString(), out dAmountDue);

                if (dAmountDue > 0)
                {
                    RecordFrm.dgvAssessment[0, m_iAssessmentRow].Value = true;
                    SaveBillTmp(sFeesCode, dAmountDue);
                    RecordFrm.txtAmount.Text = "";  // initialize
                }
            }
            catch { }
        }
             

        public virtual void Save()
        {

        }


        private void SaveBillOthersTmp(string sFeesCode, double dAmountDue)
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;
            double dRowVal = 0;
            double dOrigVal = 0;


            if (dAmountDue > 0)
            {
                sQuery = "delete from bill_tmp where ";
                sQuery += $"arn = '{RecordFrm.m_sAN}' and ";
                sQuery += $"fees_code = '{sFeesCode}' and ";
                sQuery += $" fees_category = 'OTHERS' and";
                sQuery += $" permit_code = '{m_sPermitCodeSelected}'";
                db.Database.ExecuteSqlCommand(sQuery);

                double.TryParse(RecordFrm.dgvOtherFees[8, m_iAssessmentRow].Value.ToString(), out dRowVal);
                double.TryParse(RecordFrm.dgvOtherFees[11, m_iAssessmentRow].Value.ToString(), out dOrigVal);


                sQuery = "insert into bill_tmp values (:1,:2,:3,:4,:5,:6,:7,:8,:9)";
                db.Database.ExecuteSqlCommand(sQuery,
                        new OracleParameter(":1", RecordFrm.m_sAN),
                        new OracleParameter(":2", sFeesCode),
                        new OracleParameter(":3", RecordFrm.dgvOtherFees[4, m_iAssessmentRow].Value),
                        new OracleParameter(":4", RecordFrm.dgvOtherFees[7, m_iAssessmentRow].Value),
                        new OracleParameter(":5", dRowVal),
                        new OracleParameter(":6", m_sPermitCodeSelected),
                        new OracleParameter(":7", AppSettingsManager.SystemUser.UserCode),
                        new OracleParameter(":8", dOrigVal),
                        new OracleParameter(":9", "OTHERS"));

                ComputeTotal();
            }
            else if (dAmountDue == 0)
            {
                sQuery = "delete from bill_tmp where ";
                sQuery += $"arn = '{RecordFrm.m_sAN}' and ";
                sQuery += $"fees_code = '{sFeesCode}' and ";
                sQuery += $" fees_category = 'OTHERS' and ";
                sQuery += $" permit_code = '{m_sPermitCodeSelected}'";
                db.Database.ExecuteSqlCommand(sQuery);

                RecordFrm.dgvOtherFees[0, m_iAssessmentRow].Value = false;
            }
        }    

        private void SaveBillAddOnTmp(string sFeesCode, double dAmountDue)
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;
            double dRowVal = 0;
            double dOrigVal = 0;

            if (dAmountDue > 0)
            {
                sQuery = "delete from bill_tmp where ";
                sQuery += $"arn = '{RecordFrm.m_sAN}' and ";
                sQuery += $"fees_code = '{sFeesCode}' and ";
                sQuery += $" fees_category = 'ADDITIONAL' and ";
                sQuery += $" permit_code = '{m_sPermitCodeSelected}'";
                db.Database.ExecuteSqlCommand(sQuery);

                double.TryParse(RecordFrm.dgvAddOnFees[8, m_iAssessmentRow].Value.ToString(), out dRowVal);
                double.TryParse(RecordFrm.dgvAddOnFees[11, m_iAssessmentRow].Value.ToString(), out dOrigVal);

                sQuery = "insert into bill_tmp values (:1,:2,:3,:4,:5,:6,:7,:8,:9)";
                db.Database.ExecuteSqlCommand(sQuery,
                        new OracleParameter(":1", RecordFrm.m_sAN),
                        new OracleParameter(":2", sFeesCode),
                        new OracleParameter(":3", RecordFrm.dgvAddOnFees[4, m_iAssessmentRow].Value),
                        new OracleParameter(":4", RecordFrm.dgvAddOnFees[7, m_iAssessmentRow].Value),
                        new OracleParameter(":5", dRowVal),
                        new OracleParameter(":6", m_sPermitCodeSelected), // additional value is per permit selected
                        new OracleParameter(":7", AppSettingsManager.SystemUser.UserCode),
                        new OracleParameter(":8", dOrigVal),
                        new OracleParameter(":9", "ADDITIONAL"));

                ComputeTotal();
            }
            else if (dAmountDue == 0)
            {
                sQuery = "delete from bill_tmp where ";
                sQuery += $"arn = '{RecordFrm.m_sAN}' and ";
                sQuery += $"fees_code = '{sFeesCode}' and ";
                sQuery += $" fees_category = 'ADDITIONAL' and ";
                sQuery += $" permit_code = '{m_sPermitCodeSelected}'";
                db.Database.ExecuteSqlCommand(sQuery);

                RecordFrm.dgvAddOnFees[0, m_iAssessmentRow].Value = false;
            }
        }

        private void SaveBillTmp(string sFeesCode, double dAmountDue)
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;
            double dRowVal = 0;
            double dOrigVal = 0;

            if(dAmountDue > 0)
            {
                sQuery = "delete from bill_tmp where ";
                sQuery += $"arn = '{RecordFrm.m_sAN}' and ";
                sQuery += $"fees_code = '{sFeesCode}' and ";
                sQuery += $" fees_category = 'MAIN'";
                db.Database.ExecuteSqlCommand(sQuery);

                double.TryParse(RecordFrm.dgvAssessment[8, m_iAssessmentRow].Value.ToString(), out dRowVal);
                double.TryParse(RecordFrm.dgvAssessment[11, m_iAssessmentRow].Value.ToString(), out dOrigVal);

                sQuery = "insert into bill_tmp values (:1,:2,:3,:4,:5,:6,:7,:8,:9)";
                db.Database.ExecuteSqlCommand(sQuery,
                        new OracleParameter(":1", RecordFrm.m_sAN),
                        new OracleParameter(":2", sFeesCode),
                        new OracleParameter(":3", RecordFrm.dgvAssessment[4,m_iAssessmentRow].Value),
                        new OracleParameter(":4", RecordFrm.dgvAssessment[7, m_iAssessmentRow].EditedFormattedValue),
                        new OracleParameter(":5", dRowVal),
                        new OracleParameter(":6", m_sPermitCodeSelected),
                        new OracleParameter(":7", AppSettingsManager.SystemUser.UserCode),
                        new OracleParameter(":8", dOrigVal),
                        new OracleParameter(":9", "MAIN"));

                ComputeTotal();
            }
            else if(dAmountDue == 0)
            {
                sQuery = "delete from bill_tmp where ";
                sQuery += $"arn = '{RecordFrm.m_sAN}' and ";
                sQuery += $"fees_code = '{sFeesCode}' and ";
                sQuery += $" fees_category = 'MAIN'";
                db.Database.ExecuteSqlCommand(sQuery);

                try
                {
                    RecordFrm.dgvAssessment[0, m_iAssessmentRow].Value = false;
                }
                catch { }
            }
        }

        public void PermitCellClick(object sender, DataGridViewCellEventArgs e)
        {
            m_sPermitCodeSelected = string.Empty;
            try
            {
                m_sPermitCodeSelected = RecordFrm.dgvPermit[2, e.RowIndex].Value.ToString();

                if ((bool)RecordFrm.dgvPermit[0, e.RowIndex].Value)
                    RecordFrm.dgvPermit[0, e.RowIndex].Value = true;    // do not remove check

                RecordFrm.grpAssessment.Text = RecordFrm.dgvPermit[1, e.RowIndex].Value.ToString() + " Assessment";
            }
            catch { }

            LoadAssessmentGrid(m_sPermitCodeSelected);
        }

        private void ComputeTotal()
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;
            double dTotalAmt = 0;
            OracleResultSet result = new OracleResultSet();
            string sFees = string.Empty;
            string sFeesCat = string.Empty;

            //SUBTOTAL
            result.Query = "select sum(fees_amt) as fees_amt, fees_code, fees_category from bill_tmp ";
            result.Query += $" where arn = '{RecordFrm.m_sAN}'";
            result.Query += $" group by fees_code, fees_category";
            //sQuery += $" and permit_code = '{m_sPermitCodeSelected}'"; //removed for grand total
            if (result.Execute())
                while(result.Read())
                {
                    try
                    {
                        sFees = result.GetString("fees_code");
                        sFeesCat = result.GetString("fees_category");

                        if (ValidateTaggedDisplay(sFees) && sFeesCat == "OTHERS")
                            dTotalAmt += result.GetDouble("fees_amt");
                        else if(sFeesCat == "MAIN" || sFeesCat == "ADDITIONAL")
                            dTotalAmt += result.GetDouble("fees_amt");

                    }
                    catch { }
                }


            RecordFrm.txtAllTotAmtDue.Text = string.Format("{0:#,##0.#0}", dTotalAmt);
            RecordFrm.txtAllTotAmtDue.TextAlign = HorizontalAlignment.Left; //should be right, but left appear to align to right
           

            //MAIN TOTAL
            dTotalAmt = 0;
            sQuery = "select sum(fees_amt) as fess_amt from bill_tmp ";
            sQuery += $" where arn = '{RecordFrm.m_sAN}'";
            sQuery += $" and fees_category = 'MAIN'";
            //sQuery += $" and permit_code = '{m_sPermitCodeSelected}'"; //removed for grand total

            try
            {
                dTotalAmt = db.Database.SqlQuery<double>(sQuery).SingleOrDefault();
            }
            catch { }
            RecordFrm.txtAmtDue.Text = string.Format("{0:#,##0.#0}", dTotalAmt);
            RecordFrm.txtAmtDue.TextAlign = HorizontalAlignment.Left; //should be right, but left appear to align to right

            //ADDITIONAL TOTAL
            dTotalAmt = 0;
            sQuery = "select sum(fees_amt) as fess_amt from bill_tmp ";
            sQuery += $" where arn = '{RecordFrm.m_sAN}'";
            sQuery += $" and fees_category = 'ADDITIONAL'";
            //sQuery += $" and permit_code = '{m_sPermitCodeSelected}'"; //removed for grand total

            try
            {
                dTotalAmt = db.Database.SqlQuery<double>(sQuery).SingleOrDefault();
            }
            catch { }
            RecordFrm.txtAddOnTotAmTDue.Text = string.Format("{0:#,##0.#0}", dTotalAmt);
            RecordFrm.txtAddOnTotAmTDue.TextAlign = HorizontalAlignment.Left; //should be right, but left appear to align to right

            //OTHERS TOTAL
            dTotalAmt = 0;
            sQuery = "select sum(fees_amt) as fess_amt from bill_tmp ";
            sQuery += $" where arn = '{RecordFrm.m_sAN}'";
            sQuery += $" and fees_category = 'OTHERS'";
            //sQuery += $" and permit_code = '{m_sPermitCodeSelected}'"; //removed for grand total
            try
            {
                dTotalAmt = db.Database.SqlQuery<double>(sQuery).SingleOrDefault();
            }
            catch { }
            RecordFrm.txtOtherTotAmtDue.Text = string.Format("{0:#,##0.#0}", dTotalAmt);
            RecordFrm.txtOtherTotAmtDue.TextAlign = HorizontalAlignment.Left; //should be right, but left appear to align to right
        }

        private bool ValidateTaggedDisplay(string sFees) //check if fees is for display only and not computed to total amount
        {
            OracleResultSet result = new OracleResultSet();
            result.Query = $"select display_amt from other_subcategories where fees_code = '{sFees}'";
            if (result.Execute())
                if (result.Read())
                {
                    if (result.GetString("display_amt") == "N")
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            else
                return false;

        }

        protected string GenerateBillNo()
        {
            string sBillNo = string.Empty;
            string sQuery = string.Empty;
            string sCurrYear = AppSettingsManager.GetCurrentDate().Year.ToString();

            var db = new EPSConnection(dbConn);

            try
            {
                sQuery = $"select max(bill_no) from current_bill_no where bill_no like '{sCurrYear}%'";
                sBillNo = db.Database.SqlQuery<string>(sQuery).SingleOrDefault();
            }
            catch { }
            
            if(!string.IsNullOrEmpty(sBillNo))
            {
                int iNo = 0;
                int.TryParse(sBillNo.Substring(5,5), out iNo); //2019-00001
                sBillNo = sCurrYear + "-" + FormatSeries((iNo+1).ToString());
            }
            else
            {
                sBillNo = sCurrYear + "-00001";
            }

            sQuery = $"delete from current_bill_no where bill_no like '{sCurrYear}%'";
            db.Database.ExecuteSqlCommand(sQuery);

            sQuery = "insert into current_bill_no values (:1)";
            db.Database.ExecuteSqlCommand(sQuery,
                    new OracleParameter(":1", sBillNo));

            return sBillNo;
        }

        private double ComputeCumulative(string sFeesCode, double dAmount)
        {
            string sQuery = string.Empty;
            var db = new EPSConnection(dbConn);
            double dRange1 = 0;
            double dRange2 = 0;
            double dAmount1 = 0;
            double dValue = 0;
            double dPrevRec = 0;
            int iCtr = 0;
            int iLastRec = 0;

            sQuery = $"select count(*) from schedules where fees_code = '{sFeesCode}' ";
            iLastRec = db.Database.SqlQuery<Int32>(sQuery).SingleOrDefault();

            sQuery = $"select range1,range2,amount1 from schedules where fees_code = '{sFeesCode}' order by range1";
            var epsrec = db.Database.SqlQuery<SCHEDULES>(sQuery);

            foreach (var items in epsrec)
            {
                double.TryParse(items.RANGE1.ToString(), out dRange1);
                double.TryParse(items.RANGE2.ToString(), out dRange2);
                double.TryParse(items.AMOUNT1.ToString(), out dAmount1);
              
                iCtr++;

                if (dAmount <= dRange2)
                {
                    dValue = dAmount * dAmount1;
                    break;
                }

                if (iCtr == iLastRec)
                {
                    dValue += dAmount * dAmount1;
                    break;
                }
                if (dAmount > 0 && dAmount < (dRange2 - dPrevRec))
                {
                    dValue += dAmount * dAmount1;
                    break;
                }
                dAmount -= (dRange2 - dPrevRec);
                if (dAmount <= 0.00)
                    break;
                dValue += (dRange2 - dPrevRec) * dAmount1;
                dPrevRec = dRange2;

            }

            if (iCtr == 0)
            {
                string sMess = string.Empty;

                sMess = "No record extracted in schedule of fees for code [";
                sMess += sFeesCode + "].\nPlease check schedule of fees table.";

                MessageBox.Show(sMess, "Billing", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                dValue = 0;
            }

            return dValue;
        }

        protected void OnSaveIntoExcavation()
        {
            string sQuery = string.Empty;
            var db = new EPSConnection(dbConn);
            Accounts account = new Accounts();
            account.GetOwner(m_sProjOwner);

            try
            {
                sQuery = "delete from excavation_tbl where ";
                sQuery += $"excavation_no is null and arn = '{RecordFrm.m_sAN}'";
                db.Database.ExecuteSqlCommand(sQuery);

                sQuery = @"insert into excavation_tbl (arn,proj_desc,acct_code,category_code,
            hse_no,lot_no,blk_no,address,brgy,city,province,date_applied,excavation_no) 
            values (:1,:2,:3,:4,:5,:6,:7,:8,:9,:10,:11,:12,:13)";
                db.Database.ExecuteSqlCommand(sQuery,
                                        new OracleParameter(":1", RecordFrm.m_sAN),
                                        new OracleParameter(":2", StringUtilities.HandleApostrophe(RecordFrm.txtProjDesc.Text.ToString())),
                                        new OracleParameter(":3", m_sProjOwner),
                                        new OracleParameter(":4", "00"),
                                        new OracleParameter(":5", StringUtilities.HandleApostrophe(account.HouseNo)),
                                        new OracleParameter(":6", StringUtilities.HandleApostrophe(account.LotNo)),
                                        new OracleParameter(":7", StringUtilities.HandleApostrophe(account.BlkNo)),
                                        new OracleParameter(":8", StringUtilities.HandleApostrophe(account.Address)),
                                        new OracleParameter(":9", StringUtilities.HandleApostrophe(account.Barangay)),
                                        new OracleParameter(":10", StringUtilities.HandleApostrophe(account.City)),
                                        new OracleParameter(":11", StringUtilities.HandleApostrophe(account.Province)),
                                        new OracleParameter(":12", AppSettingsManager.GetCurrentDate()),
                                        new OracleParameter(":13", ""));
            }
            catch (Exception ex)
            {
                ex.ToString();
                MessageBox.Show("error encountered", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        }

        protected bool UpdateReassessmentBilling()
        {
            /*if (pApp->TrimAll(m_sWindowTxt) == "RE - ASSESSMENT"
        || pApp->TrimAll(m_sWindowTxt) == "CERTIFICATE OF ANNUAL INSPECTION"
        || pApp->TrimAll(m_sWindowTxt) == "CERTIFICATE OF OCCUPANCY")   // RMC 20121122 added printing of certificate of annual inspection
            {
                sQuery = "select * from tax_details where arn = '" + mv_sARN + "'";
                sQuery += " order by fees_code desc ";
                setTable->Open(_bstr_t(sQuery), pApp->m_pConnection.GetInterfacePtr(),
                    adOpenStatic, adLockReadOnly, adCmdText);
                if (!setTable->adoEOF)
                {
                    while (!setTable->adoEOF)
                    {
                        sFeesCode = pApp->GetStrVariant(setTable->GetCollect("fees_code"));
                        dNewFeesAmt = atof(pApp->GetStrVariant(setTable->GetCollect("fees_amt")));

                        sQuery = "select sum(fees_amt) from tax_details_paid where arn = '" + mv_sARN + "'";
                        sQuery += " and fees_code = '" + sFeesCode + "'";
                        pRec->Open(_bstr_t(sQuery), pApp->m_pConnection.GetInterfacePtr(), adOpenStatic, adLockReadOnly, adCmdText);
                        if (!pRec->adoEOF)
                        {
                            dFeesAmt = atof(pApp->GetStrVariant(pRec->GetCollect(_variant_t(long(0)))));

                            dNewFeesAmt = dNewFeesAmt - dFeesAmt;
                            sNewFeesAmt.Format("%.2f", dNewFeesAmt);

                            if (dNewFeesAmt < 0)
                            {
                                CString sFeesDesc;
                                sFeesDesc = GetFeesDesc(sFeesCode);
                                MessageBox("System detects negative value for " + sFeesDesc + ".\nPlease check.", APP_NAME, MB_ICONSTOP);
                                return false;

                            }

                            sQuery = "update tax_details set fees_amt = '" + sNewFeesAmt + "'";
                            sQuery += " where arn = '" + mv_sARN + "'";
                            sQuery += " and fees_code = '" + sFeesCode + "'";
                            pCmd->CommandText = _bstr_t(sQuery);
                            pCmd->Execute(NULL, NULL, NULL);
                        }
                        pRec->Close();

                        setTable->MoveNext();
                    }

                    sQuery = "select sum(fees_amt) from tax_details where arn = '" + mv_sARN + "'";
                    pRec->Open(_bstr_t(sQuery), pApp->m_pConnection.GetInterfacePtr(), adOpenStatic, adLockReadOnly, adCmdText);
                    if (!pRec->adoEOF)
                    {
                        dFeesAmt = atof(pApp->GetStrVariant(pRec->GetCollect(_variant_t(long(0)))));
                        sNewFeesAmt.Format("%.2f", dFeesAmt);
                    }
                    pRec->Close();



                    sQuery = "update taxdues set fees_amt = '" + sNewFeesAmt + "'";
                    sQuery += " where arn = '" + mv_sARN + "'";
                    pCmd->CommandText = _bstr_t(sQuery);
                    pCmd->Execute(NULL, NULL, NULL);
                }
                setTable->Close();

                pApp->Connect("MRS");
                pCmd.CreateInstance(__uuidof(Command));
                pCmd->ActiveConnection = pApp->m_pConnection2;

                sQuery = "update eps_billing set fees_amt = '" + sNewFeesAmt + "'";
                sQuery += " where arn = '" + mv_sARN + "'";
                pCmd->CommandText = _bstr_t(sQuery);
                pCmd->Execute(NULL, NULL, NULL);

                pCmd.CreateInstance(__uuidof(Command));
                pCmd->ActiveConnection = pApp->m_pConnection;
                pApp->Disconnect("MRS");

            }
            */
            return true;
        }

        private string FormatSeries(string sBillNo)
        {
            string sBillSeries = string.Empty;
            int iCount = sBillNo.Length;

            switch (iCount)
            {
                case 1:
                    {
                        sBillSeries = "0000" + sBillNo;
                        break;
                    }
                case 2:
                    {
                        sBillSeries = "000" + sBillNo;
                        break;
                    }
                case 3:
                    {
                        sBillSeries = "00" + sBillNo;
                        break;
                    }
                case 4:
                    {
                        sBillSeries = "0" + sBillNo;
                        break;
                    }
                case 5:
                    {
                        sBillSeries = sBillNo;
                        break;
                    }

            }

            return sBillSeries;
        }

        public void MainFeesAddOn(object sender, EventArgs e)
        {
            try
            {
                double dAmount = 0;
                double.TryParse(RecordFrm.dgvAssessment[8, m_iAssessmentRow].EditedFormattedValue.ToString(), out dAmount);

                RecordFrm.dgvAssessment[8, m_iAssessmentRow].Value = dAmount.ToString("#,##0.00");

                SaveBillTmp(m_sAddlFeeCode, (dAmount));

            }
            catch (Exception ex) { }
        }

        public void OtherFeesAddAddOn(object sender, EventArgs e)
        {
            RecordFrm.btnOtherAdd.Visible = false;
            if (string.IsNullOrEmpty(m_sAddlFeeCode))
            {
                MessageBox.Show("Select fee to override first", "Billing", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            // update grid

            try
            {
                double dAmount = 0;
                double.TryParse(RecordFrm.dgvOtherFees[8, m_iAssessmentRow].Value.ToString(), out dAmount);

                RecordFrm.dgvOtherFees[8, m_iAssessmentRow].Value = dAmount.ToString("#,##0.00");

                SaveBillOthersTmp(m_sAddlFeeCode, (dAmount));
                RecordFrm.dgvOtherFees[0, m_iAssessmentRow].Value = true;
                //MessageBox.Show("Amount for " + RecordFrm.dgvOtherFees[1, m_iAssessmentRow].Value + " updated.", "Billing", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch(Exception ex) { }

        }


        public void AdditionalFeesAddAddOn(object sender, EventArgs e)
        {
            RecordFrm.btnAddOnAmt.Visible = false;
            if (string.IsNullOrEmpty(m_sAddlFeeCode))
            {
                MessageBox.Show("Select fee to override first", "Billing", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            // update grid

            try
            {
                double dAmount = 0;
                double.TryParse(RecordFrm.dgvAddOnFees[8, m_iAssessmentRow].Value.ToString(), out dAmount);

                RecordFrm.dgvAddOnFees[8, m_iAssessmentRow].Value = dAmount.ToString("#,##0.00");

                SaveBillAddOnTmp(m_sAddlFeeCode, (dAmount));
                RecordFrm.dgvAddOnFees[0, m_iAssessmentRow].Value = true;
                //MessageBox.Show("Amount for " + RecordFrm.dgvAddOnFees[1, m_iAssessmentRow].Value + " updated.", "Billing", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch { }

        }

        public void AdditionalFeesAdd(object sender, EventArgs e)
        {
            if(RecordFrm.btnAddlAdd.Text == "Add")
            {
                RecordFrm.txtAddlAmt.ReadOnly = false;
                RecordFrm.btnAddlAdd.Text = "Save";
                m_sAddlFeeCode = string.Empty;
                RecordFrm.txtAddlFees.Text = string.Empty;
                RecordFrm.txtAddlAmt.Text = string.Empty;
            }
            else
            {
                if(string.IsNullOrEmpty(m_sAddlFeeCode))
                {
                    MessageBox.Show("Select fee to override first","Billing",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                    return;
                }

                // update grid

                try {
                    double dAmount = 0;
                    double dAddlAmt = 0;
                    double.TryParse(RecordFrm.dgvAssessment[8, m_iAssessmentRow].Value.ToString(), out dAmount);
                    double.TryParse(RecordFrm.txtAddlAmt.Text.ToString(), out dAddlAmt);

                    RecordFrm.dgvAssessment[8, m_iAssessmentRow].Value = (dAmount + dAddlAmt).ToString("#,###.00");

                    SaveBillTmp(m_sAddlFeeCode, (dAmount + dAddlAmt));
                    RecordFrm.dgvAssessment[0, m_iAssessmentRow].Value = true;
                    MessageBox.Show("Amount for " + RecordFrm.dgvAssessment[1, m_iAssessmentRow].Value + " updated.","Billing",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        
                }
                catch { }
            }
        }

        public bool ValidatePermitNo()
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;
            string sYear1 = AppSettingsManager.GetCurrentDate().Year.ToString();
            string strWhereCond = string.Empty;
            string sPermitCode = string.Empty;
            string sPermitName = string.Empty;
            var result = (dynamic)null;

            strWhereCond = $" where arn = '{RecordFrm.m_sAN}'";

            if (RecordFrm.Source == "CERTIFICATE OF ANNUAL INSPECTION" ||
                RecordFrm.Source == "CERTIFICATE OF OCCUPANCY")
            {
                result = from a in Records.ApplicationTblList.GetRecord(strWhereCond)
                         select a;
            }
            else
            {
                result = from a in Records.ApplicationQueList.GetApplicationQue(strWhereCond)
                         select a;
            }
            foreach (var item in result)
            {
                sPermitCode = item.PERMIT_CODE;
                string sWhereClause = " where permit_code = '";
                sWhereClause += sPermitCode;
                sWhereClause += "'";
                PermitList permitlist = new PermitList(sWhereClause);
                sPermitName = permitlist.PermitLst[0].PermitDesc;

                DateTime dtDate;
                string sYear = string.Empty;
                int iCnt = 0;
                try
                {
                    sQuery = $"select count(*) from permit_assigned where permit_code = '{sPermitCode}'";
                    iCnt = db.Database.SqlQuery<Int32>(sQuery).SingleOrDefault();
                }
                catch { }

                if (iCnt == 0)
                    sYear = "";
                else
                {
                    sQuery = $"select assigned_date from permit_assigned where permit_code = '{sPermitCode}'";
                    dtDate = db.Database.SqlQuery<DateTime>(sQuery).SingleOrDefault();
                    sYear = dtDate.Year.ToString();
                }

                //if (sYear != sYear1 && !string.IsNullOrEmpty(sYear)) //temporarily removed validation for future proper validation
                //{
                //    MessageBox.Show("The Permit No assigned to " + sPermitName + " already expired. \n Please update permit nos. before payments.", "POSTING", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //    return false;
                //}
            }
            return true;
        }

        public bool ValidatePayment()
        {
            OracleResultSet result = new OracleResultSet();
            result.Query = "select * from mrs_payments where arn = '"+ RecordFrm.m_sAN + "'";
            if (result.Execute())
                if (result.Read())
                {
                    MessageBox.Show("AN No. " + RecordFrm.m_sAN + " already paid!", "BILLING", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return true;
                }
                else
                    return false;
            return false;
        }
    }
}
