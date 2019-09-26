using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPSEntities.Entity;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using Modules.Utilities;
using Common.StringUtilities;
using System.Windows.Forms;
using Common.AppSettings;
using EPSEntities.Connection;

namespace Modules.Transactions
{
    public class EngrRecords : RecordForm
    {
        TaskManager taskman = new TaskManager();

        public static ConnectionString dbConn = new ConnectionString();
        public EngrRecords(frmRecords Form) : base(Form)
        { }

        public override void FormLoad()
        {
            RecordFrm.Text = "Engineering Records";

            RecordFrm.EnableControl(false);

        }

        public override void PopulatePermit()
        {
            RecordFrm.cmbPermit.Items.Clear();

            m_lstPermit = new PermitList(null);
            int iCnt = m_lstPermit.PermitLst.Count;

            DataTable dataTable = new DataTable("Permit");
            dataTable.Columns.Clear();
            dataTable.Columns.Add("PermitCode", typeof(String));
            dataTable.Columns.Add("PermitDesc", typeof(String));

            dataTable.Rows.Add(new String[] { "", "" });

            for (int i = 0; i < iCnt; i++)
            {
                dataTable.Rows.Add(new String[] { m_lstPermit.PermitLst[i].PermitCode, m_lstPermit.PermitLst[i].PermitDesc });
            }

            RecordFrm.cmbPermit.DataSource = dataTable;
            RecordFrm.cmbPermit.DisplayMember = "PermitDesc";
            RecordFrm.cmbPermit.ValueMember = "PermitDesc";
            RecordFrm.cmbPermit.SelectedIndex = 0;
        }

        public override void ButtonAddClick(string sender)
        {
            if (AppSettingsManager.Granted("ERA"))
            {
                RecordFrm.SourceClass = "ENG_REC_ADD";
                if (sender == "Add")
                {
                    ClearControl();
                    RecordFrm.ButtonAdd.Text = "Save";
                    RecordFrm.ButtonExit.Text = "Cancel";
                    RecordFrm.ButtonEdit.Enabled = false;
                    RecordFrm.ButtonDelete.Enabled = false;
                    RecordFrm.ButtonPrint.Enabled = false;
                    RecordFrm.ButtonClear.Enabled = false;
                    RecordFrm.ButtonSearch.Enabled = false;
                    if (AppSettingsManager.GetConfigValue("29") == "Y")
                    {
                        RecordFrm.arn1.Enabled = true;
                    }
                    else
                        RecordFrm.arn1.Enabled = false;

                    RecordFrm.EnableControl(true);
                    //RecordFrm.arn1.GetCode = "";
                    RecordFrm.arn1.GetLGUCode = "";
                    RecordFrm.arn1.GetTaxYear = "";
                    //RecordFrm.arn1.GetMonth = "";
                    RecordFrm.arn1.GetDistCode = "";
                    RecordFrm.arn1.GetSeries = "";
                }
                else
                {
                    if (!ValidateData())
                        return;
                    if (!RecordFrm.ValidateData())
                        return;

                    Save();
                    MessageBox.Show("Record saved", RecordFrm.DialogText, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RecordFrm.arn1.Enabled = true;
                    RecordFrm.EnableControl(false);
                    RecordFrm.ButtonEdit.Enabled = true;
                    RecordFrm.ButtonDelete.Enabled = true;
                    RecordFrm.ButtonPrint.Enabled = true;
                    RecordFrm.ButtonClear.Enabled = true;
                    RecordFrm.ButtonSearch.Enabled = true;


                }
            }
        }

        public override void Save()
        {
            var db = new EPSConnection(dbConn);
            string strQuery = string.Empty;
            string sYear = AppSettingsManager.GetCurrentDate().Year.ToString();
            string sBrgyCode = frmProjectInfo.BrgyCode;

            if (string.IsNullOrEmpty(RecordFrm.ARN))
                RecordFrm.arn1.CreateARN(sYear, sBrgyCode);

            strQuery = $"delete from application where arn = '{RecordFrm.ARN}'";
            db.Database.ExecuteSqlCommand(strQuery);

            

            for (int i = 0; i < RecordFrm.formBldgDate.dgvList.Rows.Count; i++)
            {
                string sPermitCode = string.Empty;
                string sPermit = string.Empty;
                string sPermitNo = string.Empty;
                string sStart = string.Empty;
                string sCompleted = string.Empty;



                m_lstPermit = new PermitList(null);


                try { sPermitCode = RecordFrm.formBldgDate.dgvList[0, i].Value.ToString(); }
                catch { }
                try
                {
                    if (RecordFrm.formBldgDate.dgvList[1, i] != null)
                        sPermit = RecordFrm.formBldgDate.dgvList[1, i].Value.ToString();

                    if (string.IsNullOrEmpty(sPermitCode))
                        sPermitCode = m_lstPermit.GetPermitCode(sPermit);
                }
                catch { }
                try
                {
                    if (RecordFrm.formBldgDate.dgvList[2, i] != null)
                        sPermitNo = RecordFrm.formBldgDate.dgvList[2, i].Value.ToString();
                }
                catch { }

                try
                {
                    if (RecordFrm.formBldgDate.dgvList[3, i] != null)
                        sStart = RecordFrm.formBldgDate.dgvList[3, i].Value.ToString();
                }
                catch { }
                try
                {
                    if (RecordFrm.formBldgDate.dgvList[4, i] != null)
                        sCompleted = RecordFrm.formBldgDate.dgvList[4, i].Value.ToString();
                }
                catch { }

                if (!string.IsNullOrEmpty(sPermit)
                    && !string.IsNullOrEmpty(sStart) && !string.IsNullOrEmpty(sCompleted))
                {
                    GetArchEngr(sPermit);
                    int iMainApp = 0;
                    if (sPermit.Contains("BUILDING"))
                        iMainApp = 1;
                    try
                    {
                        strQuery = $"insert into application values (:1,:2,:3,:4,:5,:6,:7,:8,:9,:10,:11,:12,:13,:14,:15,:16,:17,:18,:19,:20,:21,:22,:23,:24,to_date(:25,'MM/dd/yyyy'),to_date(:26,'MM/dd/yyyy'),to_date(:27,'MM/dd/yyyy'),:28,:29,:30)";
                        db.Database.ExecuteSqlCommand(strQuery,
                            new OracleParameter(":1", RecordFrm.ARN),
                            new OracleParameter(":2", RecordFrm.formProject.txtProjDesc.Text.Trim()),
                            new OracleParameter(":3", sPermitCode),
                            new OracleParameter(":4", sPermitNo),
                            new OracleParameter(":5", ((DataRowView)RecordFrm.formProject.cmbStrucType.SelectedItem)["Code"].ToString()),
                            new OracleParameter(":6", RecordFrm.formBldgDate.txtBldgNo.Text.Trim()),
                            new OracleParameter(":7", RecordFrm.Status),
                            new OracleParameter(":8", ((DataRowView)RecordFrm.cmbScope.SelectedItem)["ScopeCode"].ToString()),
                            new OracleParameter(":9", ((DataRowView)RecordFrm.formProject.cmbCategory.SelectedItem)["Code"].ToString()),
                            new OracleParameter(":10", ((DataRowView)RecordFrm.formProject.cmbOccupancy.SelectedItem)["Code"].ToString()),
                            new OracleParameter(":11", RecordFrm.formProject.txtLotNo.Text.Trim()),
                            new OracleParameter(":12", RecordFrm.formProject.txtHseNo.Text.Trim()),
                            new OracleParameter(":13", RecordFrm.formProject.txtLotNo.Text.Trim()),
                            new OracleParameter(":14", RecordFrm.formProject.txtBlkNo.Text.Trim()),
                            new OracleParameter(":15", RecordFrm.formProject.txtStreet.Text.Trim()),
                            new OracleParameter(":16", ((DataRowView)RecordFrm.formProject.cmbBrgy.SelectedItem)["Desc"].ToString()),
                            new OracleParameter(":17", RecordFrm.formProject.txtMun.Text.Trim()),
                            new OracleParameter(":18", RecordFrm.formProject.txtProv.Text.Trim()),
                            new OracleParameter(":19", RecordFrm.formProject.txtZIP.Text.Trim()),
                            new OracleParameter(":20", RecordFrm.formStrucOwn.StrucAcctNo),
                            new OracleParameter(":21", RecordFrm.formLotOwn.LotAcctNo),
                            new OracleParameter(":22", RecordFrm.formProject.cmbOwnership.Text.ToString()),
                            new OracleParameter(":23", m_sEngr),
                            new OracleParameter(":24", m_sArch),
                            new OracleParameter(":25", sStart),
                            new OracleParameter(":26", sCompleted),
                            new OracleParameter(":27", RecordFrm.dtDateApplied.Value.ToShortDateString()),
                            new OracleParameter(":28", null),
                            new OracleParameter(":29", RecordFrm.formProject.txtMemo.Text.ToString()),
                            new OracleParameter(":30", iMainApp));
                    }
                    catch (Exception ex) // catches any error
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }

            SaveBuilding();

            //???strQuery = $"UPDATE APPLICATION SET STATUS_CODE = '{RecordFrm.cmbScope.Text.ToString()}' WHERE ARN = '{RecordFrm.ARN}'";
            //db.Database.ExecuteSqlCommand(strQuery);

            if (RecordFrm.SourceClass == "ENG_REC_ADD")
            {
                if (Utilities.AuditTrail.InsertTrail("ER-A", "APPLICATION", "ARN: " + RecordFrm.ARN) == 0)
                {
                    MessageBox.Show("Failed to insert audit trail.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (RecordFrm.SourceClass == "ENG_REC_EDIT")
            {
                if (Utilities.AuditTrail.InsertTrail("ER-E", "APPLICATION", "ARN: " + RecordFrm.ARN) == 0)
                {
                    MessageBox.Show("Failed to insert audit trail.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            ClearControl();
        }

        public override void ClearControl()
        {
            RecordFrm.cmbPermit.Text = "";
            RecordFrm.cmbScope.Text = "";
            RecordFrm.dtDateApplied.Text = AppSettingsManager.GetCurrentDate().ToShortDateString();
            RecordFrm.dtpDateApproved.Text = AppSettingsManager.GetCurrentDate().ToShortDateString();
            RecordFrm.ButtonAdd.Text = "Add";
            RecordFrm.ButtonEdit.Text = "Edit";
            RecordFrm.ButtonExit.Text = "Exit";
            RecordFrm.ButtonAdd.Enabled = true;
            RecordFrm.ButtonEdit.Enabled = true;
            RecordFrm.ButtonPrint.Enabled = true;

            RecordFrm.EnableControl(false);
            RecordFrm.arn1.Enabled = true;
            //RecordFrm.arn1.GetCode = "";
            RecordFrm.arn1.GetLGUCode = "";
            RecordFrm.arn1.GetTaxYear = "";
            //RecordFrm.arn1.GetMonth = ""; 
            RecordFrm.arn1.GetDistCode = "";
            RecordFrm.arn1.GetSeries = "";
            RecordFrm.formProject.ClearControls();
            RecordFrm.formBldgDate.ClearControls();
            RecordFrm.formStrucOwn.ClearControls();
            RecordFrm.formLotOwn.ClearControls();
            RecordFrm.formEngr.ClearControls();
        }

        public override bool ValidateData()
        {
            var db = new EPSConnection(dbConn);

            //if (string.IsNullOrEmpty(RecordFrm.ARN))
            //{
            //    MessageBox.Show("Incomplete ARN", RecordFrm.DialogText, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return false;
            //}

            // add validation of duplicate arn
            string strQuery = string.Empty;
            int iCnt = 0;

            if (RecordFrm.SourceClass == "ENG_REC_ADD")
            {
                strQuery = $"select count(*) from (select arn from application where arn = '{RecordFrm.ARN}' union ";
                strQuery += $"select arn from application_que where arn = '{RecordFrm.ARN}')";
                iCnt = db.Database.SqlQuery<Int32>(strQuery).SingleOrDefault();

                if (iCnt > 0)
                {
                    MessageBox.Show("ARN already in use", "Records", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }

            return true;
        }

        public override void ButtonSearchClick()
        {
            if (string.IsNullOrEmpty(RecordFrm.arn1.GetTaxYear) && string.IsNullOrEmpty(RecordFrm.arn1.GetSeries))
            {
                SearchAccount.frmSearchARN form = new SearchAccount.frmSearchARN();

                form.SearchCriteria = "APP";
                form.ShowDialog();

                RecordFrm.arn1.SetArn(form.sArn);
            }

            if (!taskman.AddTask(RecordFrm.SourceClass, RecordFrm.ARN))
                return;

            DisplayData();

            RecordFrm.tabControl1.Enabled = false;

        }

        public override void DisplayData()
        {
            var db = new EPSConnection(dbConn);
            string strWhereCond = string.Empty;

            if (!string.IsNullOrEmpty(RecordFrm.ARN))
                RecordFrm.arn1.Enabled = false;

            var result = (dynamic)null;

            strWhereCond = $" where arn = '{RecordFrm.ARN}' and main_application = 1";
            result = from a in Records.ApplicationTblList.GetRecord(strWhereCond)
                         select a;
            int iBldgNo = 0;

            foreach (var item in result)
            {
                Structure struc = new Structure();
                Category cat = new Category();
                Business buss = new Business();

                string sStatus = string.Empty;
                string sPermit = string.Empty;
                string sScope = string.Empty;
                string sArchitect = string.Empty;
                DateTime dtTmp;
                string sDateStart = string.Empty;
                string sDateComp = string.Empty;

                DateTime.TryParse(item.DATE_APPLIED.ToString(), out dtTmp);
                RecordFrm.dtDateApplied.Value = dtTmp;
                
                RecordFrm.formProject.txtProjDesc.Text = item.PROJ_DESC;
                RecordFrm.formProject.cmbStrucType.Text = struc.GetStructureDesc(item.STRUC_CODE);
                iBldgNo = item.BLDG_NO;

                sStatus = item.STATUS_CODE;
                RecordFrm.formProject.txtPIN.Text = item.BNS_CODE;
                RecordFrm.formProject.txtHseNo.Text = item.PROJ_HSE_NO;
                RecordFrm.formProject.txtLotNo.Text = item.PROJ_LOT_NO;
                RecordFrm.formProject.txtBlkNo.Text = item.PROJ_BLK_NO;
                RecordFrm.formProject.txtStreet.Text = item.PROJ_ADDR;
                RecordFrm.formProject.txtZIP.Text = item.PROJ_ZIP;
                sPermit = item.PERMIT_CODE;
                sScope = item.SCOPE_CODE;

                string sWhereClause = " where permit_code = '";
                sWhereClause += sPermit;
                sWhereClause += "'";
                PermitList permitlist = new PermitList(sWhereClause);
                RecordFrm.cmbPermit.Text = permitlist.PermitLst[0].PermitDesc;

                ScopeList scopelist = new ScopeList();
                RecordFrm.cmbScope.Text = scopelist.ScopeLst[0].ScopeDesc;

                m_sLotOwner = item.PROJ_LOT_OWNER;
                m_sStrucOwner = item.PROJ_OWNER;
                sArchitect = item.ARCHITECT;

                RecordFrm.formProject.cmbOwnership.Text = item.OWN_TYPE;
                DateTime.TryParse(item.PROP_START.ToString(), out dtTmp);
                sDateStart = dtTmp.ToShortDateString();
                DateTime.TryParse(item.PROP_COMPLETE.ToString(), out dtTmp);
                sDateComp = dtTmp.ToShortDateString();

                RecordFrm.formProject.txtMemo.Text = item.MEMO;
                RecordFrm.formProject.cmbBrgy.Text = item.PROJ_BRGY;
                RecordFrm.formProject.txtMun.Text = item.PROJ_CITY;
                RecordFrm.formProject.txtProv.Text = item.PROJ_PROV;

                RecordFrm.formProject.cmbCategory.Text = cat.GetCategoryDesc(item.CATEGORY_CODE);
                OccupancyList lstOccupancy = new OccupancyList(item.CATEGORY_CODE, item.OCCUPANCY_CODE);
                RecordFrm.formProject.cmbOccupancy.Text = lstOccupancy.OccupancyLst[0].Desc;
                RecordFrm.formProject.cmbBussKind.Text = buss.GetBusinessDesc(item.BNS_CODE);

            }

            if (string.IsNullOrEmpty(RecordFrm.formProject.txtProjDesc.Text.ToString()))
            {
                MessageBox.Show("No record found", RecordFrm.DialogText, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                ClearControl();
                return;
            }

            DisplayBuilding(iBldgNo);
            DisplayOwners();
            DisplayEngrArch();
        }

        public override void ButtonEditClick(string sender)
        {
            
            if (AppSettingsManager.Granted("ERE"))
            {
                RecordFrm.SourceClass = "ENG_REC_EDIT";

                if (sender == "Edit")
                {    
                    EnableRecordEntry();
                    RecordFrm.ButtonAdd.Enabled = false;
                    RecordFrm.ButtonEdit.Text = "Update";
                    RecordFrm.ButtonDelete.Enabled = false;
                }
                else
                {
                    if (RecordFrm.ValidateData())
                        Save();
                }
            }
        }

        public override void EnableRecordEntry()
        {
            RecordFrm.tabControl1.Enabled = true;
            RecordFrm.ButtonExit.Text = "Cancel";
            RecordFrm.ButtonPrint.Enabled = false;
            RecordFrm.arn1.Enabled = false;
            RecordFrm.btnSearch.Enabled = false;
            RecordFrm.btnClear.Enabled = false;
        }

        public override void DisplayBuilding(int iBldgNo)
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;

            var result = from a in Records.Building.GetBuildingRecord(iBldgNo)
                         select a;

            foreach (var item in result)
            {
                Material mat = new Material();
                //m_ppBldgDates.mv_sAncillaryArea = pApp->GetStrVariant(setTable->GetCollect("ancillary_area"));
                //m_sActualCost = pApp->GetStrVariant(setTable->GetCollect("actual_cost"));
                //m_sDateCompleted = pApp->GetStrVariant(setTable->GetCollect("date_completed"));
                RecordFrm.formBldgDate.txtBldgNo.Text = iBldgNo.ToString();
                RecordFrm.formBldgDate.txtBldgName.Text = item.BLDG_NM;
                RecordFrm.formBldgDate.txtPIN.Text = item.LAND_PIN;
                RecordFrm.formBldgDate.txtHeight.Text = item.BLDG_HEIGHT.ToString("#,###.00");
                RecordFrm.formBldgDate.txtArea.Text = item.TOTAL_FLR_AREA.ToString("#,###.00");
                RecordFrm.formBldgDate.txtUnits.Text = item.NO_UNITS.ToString("#,###");
                RecordFrm.formBldgDate.txtStoreys.Text = item.NO_STOREYS.ToString("#,###");
                RecordFrm.formBldgDate.txtCost.Text = item.EST_COST.ToString("#,###.00");
                RecordFrm.formBldgDate.cmbMaterials.Text = mat.GetMaterialDesc(item.MATERIAL_CODE);
                RecordFrm.formBldgDate.txtAssVal.Text = item.ASS_VAL.ToString("#,###.00");

                //m_ppBldgDates.m_sPermitCode = mc_cbPermit.GetItemText(mc_cbPermit.GetCurSel(), 1);
            }

            string strWhereCond = string.Empty;

            strWhereCond = $" where arn = '{RecordFrm.ARN}' order by main_application desc";

            var pset = from a in Records.ApplicationTblList.GetRecord(strWhereCond)
                       select a;
            RecordFrm.formBldgDate.LoadGrid();

            foreach (var item in pset)
            {
                string sPermitNo = string.Empty;
                string sPermitName = string.Empty;
                string sStart = string.Empty;
                string sComplete = string.Empty;
                string sPermitNum = string.Empty;
                sPermitNo = item.PERMIT_CODE;
                string sWhereClause = string.Empty;
                DateTime dtTmp;

                if (!string.IsNullOrEmpty(sPermitNo))
                {
                    sWhereClause = " where permit_code = '";
                    sWhereClause += sPermitNo;
                    sWhereClause += "'";
                }
                PermitList permitlist = new PermitList(sWhereClause);
                sPermitName = permitlist.PermitLst[0].PermitDesc;

                DateTime.TryParse(item.PROP_START.ToString(), out dtTmp);
                sStart = dtTmp.ToShortDateString();
                DateTime.TryParse(item.PROP_COMPLETE.ToString(), out dtTmp);
                sComplete = dtTmp.ToShortDateString();
                sPermitNum = item.PERMIT_NO;

                RecordFrm.formBldgDate.dgvList.Rows.Add(sPermitNo, sPermitName, sPermitNum, sStart, sComplete);
            }

        }

        public override void DisplayEngrArch()
        {
            string strWhereCond = string.Empty;
            string sEngrNo = string.Empty;

            strWhereCond = $" where arn = '{RecordFrm.ARN}' order by main_application desc";

            var pset = from a in Records.ApplicationTblList.GetRecord(strWhereCond)
                       select a;
            RecordFrm.formEngr.LoadGrid();

            foreach (var item in pset)
            {
                sEngrNo = item.ENGR_CODE;
                if (string.IsNullOrEmpty(sEngrNo))
                    sEngrNo = item.ARCHITECT;

                Engineers account = new Engineers();
                account.GetOwner(sEngrNo);

                RecordFrm.formEngr.dgvList.Rows.Add(account.OwnerCode, account.LastName, account.FirstName, account.MiddleInitial,
                    account.EngrType, account.Address, account.HouseNo, account.LotNo, account.BlkNo,
                    account.Barangay, account.City, account.Province, account.ZIP, account.TIN,
                    account.PTR, account.PRC);

            }
        }

        public override void ButtonDeleteClick()
        {
            if (AppSettingsManager.Granted("ERD"))
            {
                if (!string.IsNullOrEmpty(RecordFrm.ARN))
                {
                    if (MessageBox.Show("Are you sure you want to delete this record?", RecordFrm.DialogText, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }

                    DeleteRecord();
                }
            }
        }
        
        private void DeleteRecord()
        {
            var db = new EPSConnection(dbConn);
            string strQuery = string.Empty;

            strQuery = $"delete from application where arn = '{RecordFrm.ARN}'";
            db.Database.ExecuteSqlCommand(strQuery);

            MessageBox.Show("Record is deleted.", RecordFrm.DialogText, MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (Utilities.AuditTrail.InsertTrail("ER-D", "APPLICATION", "ARN: " + RecordFrm.ARN) == 0)
            {
                MessageBox.Show("Failed to insert audit trail.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            taskman.RemTask(RecordFrm.ARN);
            RecordFrm.Close();
        }
    }
}
