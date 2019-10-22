using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common.AppSettings;
using Modules.Utilities;
using EPSEntities.Connection;
using EPSEntities.Entity;
using Oracle.ManagedDataAccess.Client;
using ARCSEntities.Connection;
using Common.DataConnector;

namespace Modules.Transactions
{
    public partial class frmPosting : Form
    {
        TaskManager taskman = new TaskManager();
        public static ConnectionString dbConn = new ConnectionString();
        public static ARCSConnectionString dbConnArcs = new ARCSConnectionString();
        private string m_sOwnLn = string.Empty;
        private string m_sOwnFn = string.Empty;
        private string m_sOwnMi = string.Empty;
        private string m_sCheckNo = string.Empty;
        private string m_sCertType = string.Empty;
        private string m_sProjOwner = string.Empty;
        private string m_sPaymentType = string.Empty;
        private string m_sWiringNo = string.Empty;
        private string m_sCategoryCd = string.Empty;
        private double m_dDrCr = 0;
        private double m_dDebitCredit = 0;
        

        public frmPosting()
        {
            InitializeComponent();
        }

        private void frmPosting_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btnSearch, "Search Application");
            toolTip2.SetToolTip(btnClear, "Clear Controls");
        }
        private bool ValidatePayment()
        {
            OracleResultSet result = new OracleResultSet();
            result.Query = $"select * from mrs_payments where arn = '{arn1.GetArn()}'";
            if(result.Execute())
                if(result.Read())
                {
                    MessageBox.Show("ARN already paid");
                    ClearControls();
                    //arn1.GetCode = "";
                    arn1.GetLGUCode = "";
                    arn1.GetTaxYear = "";
                    //arn1.GetMonth = "";
                    arn1.GetDistCode = "";
                    arn1.GetSeries = "";
                    return false;
                }
            return true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!ValidatePayment()) //AFM 20191018 check if record has payment
                return;

            if (string.IsNullOrEmpty(arn1.GetTaxYear) && string.IsNullOrEmpty(arn1.GetSeries))
            {
                SearchAccount.frmSearchARN form = new SearchAccount.frmSearchARN();

                if (m_sCertType.Contains("EXCAVATION"))
                    form.SearchCriteria = "AppForExcavation";
                else if (m_sCertType.Contains("CERTIFICATE OF OCCUPANCY"))
                    form.SearchCriteria = "APP";
                else if (m_sCertType.Contains("CERTIFICATE OF ELECTRICAL INSPECTION"))
                    form.SearchCriteria = "ApplyForCEI";
                else if (m_sCertType.Contains("WIRING PERMIT"))
                    form.SearchCriteria = "AppWire";
                else
                    form.SearchCriteria = "QUE";
                form.ShowDialog();

                arn1.SetArn(form.sArn);
            }

            if (!taskman.AddTask("POSTING", arn1.GetArn()))
            {
                //arn1.GetCode = "";
                arn1.GetLGUCode = "";
                arn1.GetTaxYear = "";
                //arn1.GetMonth = "";
                arn1.GetDistCode = "";
                arn1.GetSeries = "";

                return;
            }

            ClearControls();
            DisplayData();
            OnCashPayment();
            grpDetails.Enabled = true;
        }

        
        private void ClearControls()
        {
            txtProjDesc.Text = string.Empty;
            txtProjLoc.Text = string.Empty;
            txtProjOwn.Text = string.Empty;
            dgvList.Rows.Clear();
            dtpDate.Value = AppSettingsManager.GetCurrentDate();
            txtOrNo.Text = string.Empty;
            txtMemo.Text = string.Empty;
            txtTeller.Text = string.Empty;
            txtBillNo.Text = string.Empty;
            txtCash.Text = string.Empty;
            txtAmtTendered.Text = string.Empty;
            txtDebit.Text = string.Empty;
            txtChange.Text = string.Empty;
            rdoCash.Checked = false;
            rdoCheck.Checked = false;
            grpDetails.Enabled = false;
            m_sOwnLn = string.Empty;
            m_sOwnFn = string.Empty;
            m_sOwnMi = string.Empty;
            m_sCheckNo = string.Empty;
            m_sCertType = string.Empty;
            m_sProjOwner = string.Empty;
            m_sPaymentType = string.Empty;
            m_sWiringNo = string.Empty;
            m_sCategoryCd = string.Empty;
            m_dDrCr = 0;
            m_dDebitCredit = 0;
        }

        private void DisplayData()
        {
            string strWhereCond = string.Empty;
            m_sProjOwner = string.Empty;
            var result = (dynamic)null;

            if (m_sCertType.Contains("EXCAVATION PERMIT"))
            {
                strWhereCond = $" where arn = '{arn1.GetArn()}'";
                result = from a in Records.ExcavationTblList.GetRecord(strWhereCond)
                         select a;
            }
            else if (m_sCertType.Contains("CERTIFICATE OF ELECTRICAL INSPECTION"))
            {
                if (string.IsNullOrEmpty(m_sWiringNo))
                {
                    strWhereCond = $" where arn = '{arn1.GetArn()}' and assigned_no = ''";
                    result = from a in Records.OtherCertList.GetRecord(strWhereCond)
                             select a;
                }
                else
                {
                    strWhereCond = $" where arn = '{arn1.GetArn()}' and wiring_no = '{m_sWiringNo}'";
                    result = from a in Records.OtherCertList.GetRecord(strWhereCond)
                             select a;
                }
            }
            else if (m_sCertType.Contains("WIRING PERMIT"))
            {
                strWhereCond = $" where arn = '{arn1.GetArn()}'";
                result = from a in Records.WiringTblList.GetRecord(strWhereCond)
                         select a;
            }
            else if (m_sCertType.Contains("OCCUPANCY"))
            {
                strWhereCond = $" where arn = '{arn1.GetArn()}'";
                result = from a in Records.ApplicationTblList.GetRecord(strWhereCond)
                             select a;
            }
            else
            {
                if (AppSettingsManager.GetConfigValue("30") == "0")
                {
                    strWhereCond = $" where arn = '{arn1.GetArn()}'";
                    result = from a in Records.ApplicationQueList.GetApplicationQue(strWhereCond)
                             select a;
                }
                if (AppSettingsManager.GetConfigValue("30") == "1") //AFM 20191016 get from application table
                {
                    strWhereCond = $" where arn = '{arn1.GetArn()}'";
                    result = from a in Records.ApplicationTblList.GetRecord(strWhereCond)
                             select a;
                }

            }

            foreach (var item in result)
            {
                if (m_sCertType.Contains("EXCAVATION PERMIT") ||
                    m_sCertType.Contains("CERTIFICATE OF ELECTRICAL INSPECTION") ||
                    m_sCertType.Contains("WIRING PERMIT"))
                {
                    if (!m_sCertType.Contains("EXCAVATION PERMIT"))
                        m_sCategoryCd = item.OCCUPANCY;

                    txtProjDesc.Text = item.PROJ_DESC;
                    m_sProjOwner = item.ACCT_CODE;

                    if (m_sCertType.Contains("CERTIFICATE OF ELECTRICAL INSPECTION"))
                    {
                        m_sWiringNo = item.WIRING_NO;
                        txtProjLoc.Text = item.LOCATION;
                    }
                    else
                    if (m_sCertType.Contains("WIRING PERMIT") ||
                        m_sCertType.Contains("EXCAVATION PERMIT"))
                    {
                        txtProjLoc.Text = txtProjLoc.Text = string.Format("{0} {1} {2} {3} {4} {5} {6} ", item.HSE_NO, item.LOT_NO, item.BLK_NO, item.ADDRESS, item.BRGY, item.CITY, item.PROVINCE);
                    }
                }
                else
                {
                    txtProjDesc.Text = item.PROJ_DESC;
                    txtProjLoc.Text = txtProjLoc.Text = string.Format("{0} {1} {2} {3} {4} {5} {6} ", item.PROJ_HSE_NO, item.PROJ_LOT_NO, item.PROJ_BLK_NO, item.PROJ_ADDR, item.PROJ_BRGY, item.PROJ_CITY, item.PROJ_PROV);
                    m_sProjOwner = item.PROJ_OWNER;
                    Accounts account = new Accounts();
                    account.GetOwner(m_sProjOwner);

                    txtProjOwn.Text = account.OwnerName;
                    m_sOwnLn = account.LastName;
                    m_sOwnFn = account.FirstName;
                    m_sOwnMi = account.MiddleInitial;
                }
            }

            if (string.IsNullOrEmpty(txtProjDesc.Text.ToString()))
            {
                MessageBox.Show("No record found", "Posting", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                ClearControls();
                return;
            }

            LoadGrid();

            DisplayDebitCredit();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
            //arn1.GetCode = "";
            arn1.GetLGUCode = "";
            arn1.GetTaxYear = "";
            //arn1.GetMonth = "";
            arn1.GetDistCode = "";
            arn1.GetSeries = "";
            taskman.RemTask(arn1.GetArn());
        }

        private void LoadGrid()
        {
            var db = new EPSConnection(dbConn);
            var result = (dynamic)null;

            dgvList.Rows.Clear();

            string sQuery = string.Empty;

            sQuery = $"select * from billing where arn = '{arn1.GetArn()}' ";
            result = db.Database.SqlQuery<BILLING>(sQuery).ToList();

            foreach (var item in result)
            {
                txtBillNo.Text = item.BILL_NO;
            }

            if (AppSettingsManager.GetConfigValue("30") == "0") //AFM 20191016
            {
                if (string.IsNullOrEmpty(txtBillNo.Text.ToString()))
                {
                    MessageBox.Show("Application not yet billed", "Posting", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            }
            else
            {
                txtBillNo.Enabled = true;
            }

            string sFeesCode = string.Empty;
            string sFeesAmt = string.Empty;

            sQuery = $"select * from tax_details where arn = '{arn1.GetArn()}' and bill_no = '{txtBillNo.Text.ToString()}' order by fees_code";
            result = db.Database.SqlQuery<TAX_DETAILS>(sQuery).ToList();
            foreach (var item in result)
            {
                Subcategories subcat = new Subcategories();

                sFeesCode = item.FEES_CODE;
                sFeesAmt = string.Format("{0:#,###.00}", item.FEES_AMT);
                dgvList.Rows.Add(sFeesCode, subcat.GetFeesDesc(sFeesCode), sFeesAmt);
            }

            if(AppSettingsManager.GetConfigValue("30") == "1")// AFM 20191017 allow editing of amount in build up mode
                dgvList.Columns[2].ReadOnly = false;

            ComputeTotal();
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            /*if (e.ColumnIndex == 2)
                dgvList.ReadOnly = false;
            else
                dgvList.ReadOnly = true;*/
        }

        private void ComputeTotal()
        {
            double dTotAmount = 0;

            for (int i = 0; i <= dgvList.Rows.Count; i++)
            {
                double dAmount = 0;

                try
                {
                    double.TryParse(dgvList[2, i].Value.ToString(), out dAmount);
                    dTotAmount += dAmount;
                }
                catch { }
            }

            txtAmt.Text = string.Format("{0:#,###.00}", dTotAmount);
        }

        private void rdoCheck_CheckedChanged(object sender, EventArgs e)
        {
            OnCheckPayment();
        }

        private void OnCheckPayment()
        {
            var db = new EPSConnection(dbConn);

            if (rdoCheck.Checked)
            {
                if (!string.IsNullOrEmpty(txtOrNo.Text.ToString().Trim()))
                {
                    frmCheck check = new frmCheck();
                    check.txtFirstName.Text = m_sOwnFn;
                    check.txtLastName.Text = m_sOwnLn;
                    check.txtMI.Text = m_sOwnMi;
                    if (!string.IsNullOrEmpty(m_sCheckNo))
                        check.txtCheckNo.Text = m_sCheckNo;
                    check.m_sOrNo = txtOrNo.Text.ToString().Trim();
                    check.m_sAmount = txtAmt.Text.ToString();
                    check.ShowDialog();

                    if (!string.IsNullOrEmpty(check.txtBankCode.Text.ToString()) &&
                        !string.IsNullOrEmpty(check.txtCheckNo.Text.ToString()) &&
                        !string.IsNullOrEmpty(check.txtAcctNo.Text.ToString()))
                    {
                        m_sCheckNo = check.txtCheckNo.Text.ToString();

                        txtAmtTendered.Text = check.txtCheckAmt.Text;

                        double dDebitCredit = 0;
                        double.TryParse(txtDebit.Text.ToString(), out dDebitCredit);

                        if (dDebitCredit > 0)
                        {
                            double dDrCr = 0;
                            double.TryParse(check.txtDebit.Text.ToString(), out dDebitCredit);
                            dDrCr = m_dDrCr;
                            m_dDebitCredit = dDebitCredit;

                            if (dDebitCredit > 0)
                            {
                                dDrCr = dDrCr + dDebitCredit;
                            }

                            txtDebit.Text = string.Format("{0:#,###.00}", dDrCr);
                        }
                        else
                        {
                            double.TryParse(check.txtDebit.Text.ToString(), out dDebitCredit);
                            txtDebit.Text = string.Format("{0:#,###.00}", dDebitCredit);
                            m_dDebitCredit = dDebitCredit;
                        }

                        int iCntCheck = 0;
                        string sQuery = string.Empty;

                        sQuery = $"select count(*) from check_tbl where chk_ref = '{txtOrNo.Text.ToString().Trim()}' and chk_no = '{m_sCheckNo}'";
                        iCntCheck = db.Database.SqlQuery<Int32>(sQuery).SingleOrDefault();

                        if (iCntCheck > 0)
                        {
                            sQuery = $"delete from check_tbl where chk_ref = '{txtOrNo.Text.ToString().Trim()}' and chk_no = '{m_sCheckNo}'";
                            db.Database.ExecuteSqlCommand(sQuery);

                            sQuery = $"delete from debit_credit where or_no = '{txtOrNo.Text.ToString().Trim()}' and chk_no = '{m_sCheckNo}'";
                            db.Database.ExecuteSqlCommand(sQuery);
                        }

                        double dAmt = 0;
                        double.TryParse(check.txtCheckAmt.Text.ToString(), out dAmt);
                        try
                        {
                            sQuery = $"insert into check_tbl values (:1,to_date(:2,'MM/dd/yyyy'),:3,:4,:5,:6,:7,:8,to_date(:9,'MM/dd/yyyy'),:10,:11)";
                            db.Database.ExecuteSqlCommand(sQuery,
                                        new OracleParameter(":1", txtOrNo.Text.ToString().Trim()),
                                        new OracleParameter(":2", string.Format("{0:MM/dd/yyyy}", check.dtpCheckDate.Text.ToString())),
                                        new OracleParameter(":3", check.txtCheckNo.Text.ToString().Trim()),
                                        new OracleParameter(":4", check.txtAcctNo.Text.ToString().Trim()),
                                        new OracleParameter(":5", check.txtLastName.Text.ToString().Trim()),
                                        new OracleParameter(":6", check.txtFirstName.Text.ToString().Trim()),
                                        new OracleParameter(":7", check.txtMI.Text.ToString().Trim()),
                                        new OracleParameter(":8", dAmt),
                                        new OracleParameter(":9", string.Format("{0:MM/dd/yyyy}", AppSettingsManager.GetCurrentDate())),
                                        new OracleParameter(":10", check.txtBankCode.Text.ToString().Trim()),
                                        new OracleParameter(":11", dDebitCredit));
                        }
                        catch (Exception ex)
                        {
                            ex.ToString();
                            MessageBox.Show("Unable to save to the database", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        if (dDebitCredit != 0)
                        {
                            SaveDebitCredit(dDebitCredit, m_sCheckNo, true);
                        }

                        txtCash.Text = "";
                        txtChange.Text = "";
                        m_sPaymentType = "CHECK";
                        txtCash.ReadOnly = true;
                    }
                    else
                    {
                        m_sPaymentType = "";
                        rdoCheck.Checked = false;
                    }
                }
                else
                {
                    MessageBox.Show("Enter O.R. no. first", "Posting", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    m_sPaymentType = "";
                    rdoCheck.Checked = false;
                    return;
                }
            }
        }

        private void rdoCash_CheckedChanged(object sender, EventArgs e)
        {
            OnCashPayment();
        }

        private void OnCashPayment()
        {
            var db = new EPSConnection(dbConn);
            if (rdoCash.Checked)
            {
                if (!string.IsNullOrEmpty(txtOrNo.Text.ToString().Trim()))
                {
                    string sQuery = string.Empty;
                    int iCnt = 0;

                    sQuery = $"select count(*) from mrs_payments where or_no = '{txtOrNo.Text.ToString().Trim()}'";
                    iCnt = db.Database.SqlQuery<Int32>(sQuery).SingleOrDefault();

                    if (iCnt == 0)
                    {
                        sQuery = $"delete from check_tbl where chk_ref= '{txtOrNo.Text.ToString().Trim()}'";
                        db.Database.ExecuteSqlCommand(sQuery);

                        sQuery = $"delete from debit_credit where or_no = '{txtOrNo.Text.ToString().Trim()}'";
                        db.Database.ExecuteSqlCommand(sQuery);
                    }

                    txtCash.Text = "";
                    txtChange.Text = "";
                    txtDebit.Text = string.Format("{0:#,###.00}", m_dDrCr);
                    txtAmtTendered.Text = "";
                    m_sPaymentType = "CASH";
                    txtCash.ReadOnly = false;

                }
                else
                {
                    MessageBox.Show("Enter O.R. no. first", "Posting", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    m_sPaymentType = "";
                    rdoCash.Checked = false;
                    return;
                }
            }
        }

        private void DisplayDebitCredit()
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;
            var result = (dynamic)null;
            m_dDrCr = 0;

            sQuery = $"select * from debit_credit where payer_code = '{m_sProjOwner}' and served = 'N'";
            result = db.Database.SqlQuery<DEBIT_CREDIT>(sQuery).ToList();

            foreach (var item in result)
            {
                txtDebit.Text = string.Format("{0:#,###.00}", item.BALANCE);
                double.TryParse(txtDebit.Text.ToString(), out m_dDrCr);
            }

            if (m_dDrCr == 0)
            {
                txtDebit.Text = "";
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            taskman.RemTask(arn1.GetArn());
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateData())
                return;
            if (!ValidatePermitNo())
                return;

            if (MessageBox.Show("Save Payment?", "POSTING", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SavePosting();
                taskman.RemTask(arn1.GetArn());
            }
        }

        private bool ValidateData()
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;
            int iCnt = 0;

            sQuery = $"select count(*) from mrs_payments where or_no ='{txtOrNo.Text.ToString().Trim()}'";
            iCnt = db.Database.SqlQuery<Int32>(sQuery).SingleOrDefault();

            if(iCnt > 0)
            {
                MessageBox.Show("The OR No you entered has already been used online.", "POSTING", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if(string.IsNullOrEmpty(txtTeller.Text.ToString()))
            {
                MessageBox.Show("Teller is required","POSTING",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                return false;
            }

            return true;
        }

        private bool ValidatePermitNo()
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;
            string sYear1 = AppSettingsManager.GetCurrentDate().Year.ToString();
            string strWhereCond = string.Empty;
            string sPermitCode = string.Empty;
            string sPermitName = string.Empty;
            var result = (dynamic)null;

            strWhereCond = $" where arn = '{arn1.GetArn()}'";
            result = from a in Records.ApplicationQueList.GetApplicationQue(strWhereCond)
                     select a;

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
                
                if (sYear != sYear1 && !string.IsNullOrEmpty(sYear))
                {
                    MessageBox.Show("The Permit No assigned to " + sPermitName + " already expired. \n Please update permit nos. before payments.", "POSTING", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }
            return true;
        }

        private void SavePosting()
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;
            string sFeesCode = string.Empty;
            double dFeesDue = 0;
            double dTotalFeesDue = 0;

            for (int i = 0; i <= dgvList.Rows.Count; i++)
            {
                try
                {
                    sFeesCode = dgvList[0, i].Value.ToString();

                }
                catch { sFeesCode = ""; }

                try
                {
                    double.TryParse(dgvList[2, i].Value.ToString(), out dFeesDue);
                }
                catch { dFeesDue = 0; }

                dTotalFeesDue += dFeesDue;

                if (!string.IsNullOrEmpty(sFeesCode) && dFeesDue > 0)
                {
                    try
                    {
                        string sTime = string.Empty;
                        sTime = string.Format("{0:HH:mm:ss}", AppSettingsManager.GetCurrentDate());

                        sQuery = $"insert into mrs_payments values (:1,:2,:3,:4,:5,to_date(:6,'MM/dd/yyyy'),:7,:8,to_date(:9,'MM/dd/yyyy'),:10,:11,:12)";
                        db.Database.ExecuteSqlCommand(sQuery,
                                    new OracleParameter(":1", arn1.GetArn()),
                                    new OracleParameter(":2", txtBillNo.Text.ToString()),
                                    new OracleParameter(":3", m_sProjOwner),
                                    new OracleParameter(":4", ""),
                                    new OracleParameter(":5", txtOrNo.Text.ToString().Trim()),
                                    new OracleParameter(":6", string.Format("{0:MM/dd/yyyy}", dtpDate.Text.ToString())),
                                    new OracleParameter(":7", sFeesCode),
                                    new OracleParameter(":8", dFeesDue),
                                    new OracleParameter(":9", string.Format("{0:MM/dd/yyyy}", AppSettingsManager.GetCurrentDate())),
                                    new OracleParameter(":10", sTime),
                                    new OracleParameter(":11", txtTeller.Text.ToString().Trim()),
                                    new OracleParameter(":12", AppSettingsManager.SystemUser.UserCode));
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                        MessageBox.Show("Unable to save to the database", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }

            try
            {
                sQuery = $"insert into application select * from application_que where arn = '{arn1.GetArn()}'";
                db.Database.ExecuteSqlCommand(sQuery);

                AssignPermitNo();

                sQuery = $"delete from application_que where arn = '{arn1.GetArn()}'";
                db.Database.ExecuteSqlCommand(sQuery);

                sQuery = $"insert into billing_paid select * from billing where arn = '{arn1.GetArn()}' and bill_no = '{txtBillNo.Text.ToString()}'";
                db.Database.ExecuteSqlCommand(sQuery);

                sQuery = $"delete from billing where arn = '{arn1.GetArn()}' and bill_no = '{txtBillNo.Text.ToString()}'";
                db.Database.ExecuteSqlCommand(sQuery);

                sQuery = $"insert into taxdues_paid select * from taxdues where arn = '{arn1.GetArn()}'  and bill_no  = '{txtBillNo.Text.ToString()}'";
                db.Database.ExecuteSqlCommand(sQuery);

                sQuery = $"delete from taxdues where arn = '{arn1.GetArn()}' and bill_no = '{txtBillNo.Text.ToString()}'";
                db.Database.ExecuteSqlCommand(sQuery);

                sQuery = $"insert into tax_details_paid select * from tax_details where arn = '{arn1.GetArn()}' and bill_no = '{txtBillNo.Text.ToString()}'";
                db.Database.ExecuteSqlCommand(sQuery);

                sQuery = $"delete from tax_details where arn = '{arn1.GetArn()}' and bill_no = '{txtBillNo.Text.ToString()}'";
                db.Database.ExecuteSqlCommand(sQuery);

                double dCash = 0;
                double.TryParse(txtCash.Text.ToString(), out dCash);
                double dCheck = 0;
                double.TryParse(txtAmtTendered.Text.ToString(), out dCheck);

                sQuery = $"insert into payment_denom values (:1,to_date(:2,'MM/dd/yyyy'),:3,:4,:5,:6,:7)";
                db.Database.ExecuteSqlCommand(sQuery,
                            new OracleParameter(":1", txtOrNo.Text.ToString().Trim()),
                            new OracleParameter(":2", string.Format("{0:MM/dd/yyyy}", dtpDate.Text)),
                            new OracleParameter(":3", arn1.GetArn()),
                            new OracleParameter(":4", dTotalFeesDue),
                            new OracleParameter(":5", dCash),
                            new OracleParameter(":6", dCheck),
                            new OracleParameter(":7", AppSettingsManager.SystemUser.UserCode));
            }
            catch (Exception ex)
            {
                ex.ToString();
                MessageBox.Show("Unable to save to the database", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            double dDebitCredit = 0;
            double.TryParse(txtDebit.Text.ToString(), out dDebitCredit);

            if (dDebitCredit != 0 && m_sPaymentType != "CHECK")
            {
                SaveDebitCredit(dDebitCredit, m_sCheckNo, true);
            }

            if (m_dDebitCredit != 0)
            {
                int iCnt = 0;
                sQuery = $"select count(*) from debit_credit where payer_code = '{m_sProjOwner}'";
                iCnt = db.Database.SqlQuery<Int32>(sQuery).SingleOrDefault();

                if (iCnt > 1)
                {
                    sQuery = $"update debit_credit set served = 'Y' where payer_code = '{m_sProjOwner}'";
                    db.Database.ExecuteSqlCommand(sQuery);

                    SaveDebitCredit(dDebitCredit, m_sCheckNo, false);
                }
            }


            try
            {    
                OracleResultSet result = new OracleResultSet();
                result.CreateANGARCS();
                result.Query = $"delete from eps_billing where arn = '{arn1.GetArn()}'";
                result.ExecuteNonQuery();
                result.Close();

                //var dbarcs = new ARCSConnection(dbConnArcs);
                //sQuery = $"delete from eps_billing where arn = '{arn1.GetArn()}'";
                //dbarcs.Database.ExecuteSqlCommand(sQuery);

            }
            catch { }

            MessageBox.Show("Payment Successfully Posted.", "POSTING", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (Utilities.AuditTrail.InsertTrail("P-PP", "PAYMENTS", "ARN: " + arn1.GetArn()) == 0)
            {
                MessageBox.Show("Failed to insert audit trail.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ClearControls();
        }

        private int GetDbcrSeqId()
        {
            int iSeqid = 0;
            string sQuery = string.Empty;
            var db = new EPSConnection(dbConn);

            try
            {
                sQuery = $"select max(seqid) from debit_credit where payer_code = '{m_sProjOwner}'";
                iSeqid = db.Database.SqlQuery<Int32>(sQuery).SingleOrDefault();
                if (iSeqid > 0)
                    iSeqid += 1;
            }
            catch
            {
                iSeqid = 1;
            }

            return iSeqid;
        }

        private void SaveDebitCredit(double dDebitCredit, string sCheckNo, bool bExcess)
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;

            double dDebit = 0;
            double dCredit = 0;
            int iSeqid = GetDbcrSeqId();

            if (dDebitCredit == 0)
            {
                dDebit = 0;
                dCredit = 0;
            }
            else
            {
                if (dDebitCredit < 0)
                {
                    dDebit = dDebitCredit;
                    dCredit = 0;
                }
                else
                {
                    dDebit = 0;
                    dCredit = dDebitCredit;
                }
            }

            string sMemo = string.Empty;
            string sServed = string.Empty;

            if (bExcess)
            {
                if (dDebit == 0)
                    sMemo = $"IN EXCESS OF CHECK PAYMENT FROM O.R. {txtOrNo.Text.ToString().Trim()}";
                else
                    sMemo = $"CREDITED OF CHECK PAYMENT FROM O.R. {txtOrNo.Text.ToString().Trim()}";

                sServed = "Y";
            }
            else
            {
                sMemo = $"REMAINING BALANCE FROM O.R. {txtOrNo.Text.ToString().Trim()}";
                sServed = "N";
            }
            try
            {
                sQuery = $"insert into debit_credit values (:1,:2,:3,to_date(:4,'MM/dd/yyyy'),:5,:6,:7,:8,:9,to_date(:10,'MM/dd/yyyy'),:11,:12,:13)";
                db.Database.ExecuteSqlCommand(sQuery,
                            new OracleParameter(":1", m_sProjOwner),
                            new OracleParameter(":2", iSeqid),
                            new OracleParameter(":3", txtOrNo.Text.ToString().Trim()),
                            new OracleParameter(":4", string.Format("{0:MM/dd/yyyy}", dtpDate.Text)),
                            new OracleParameter(":5", dDebit),
                            new OracleParameter(":6", dCredit),
                            new OracleParameter(":7", dDebitCredit),
                            new OracleParameter(":8", sMemo),
                            new OracleParameter(":9", sCheckNo),
                            new OracleParameter(":10", string.Format("{0:MM/dd/yyyy}", AppSettingsManager.GetCurrentDate())),
                            new OracleParameter(":11", string.Format("{0:HH:mm:ss}", AppSettingsManager.GetCurrentDate())),
                            new OracleParameter(":12", AppSettingsManager.SystemUser.UserCode),
                            new OracleParameter(":13", sServed));
            }
            catch (Exception ex)
            {
                ex.ToString();
                MessageBox.Show("Unable to save to the database", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void AssignPermitNo()
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;
            string strWhereCond = string.Empty;

            if (m_sCertType.Contains("OCCUPANCY"))
                AssignCertofOccupancyNo();
            else
            {
                strWhereCond = $" where arn = '{arn1.GetArn()}'";
                var result = from a in Records.ApplicationTblList.GetRecord(strWhereCond)
                             select a;

                string sPermitCode = string.Empty;
                string sPermitNo = string.Empty;

                foreach (var item in result)
                {
                    sPermitCode = item.PERMIT_CODE;
                    sPermitNo = GetCurrentPermitNo(sPermitCode);

                    sQuery = $"update application set permit_no = '{sPermitNo}', date_issued = to_date('{AppSettingsManager.GetCurrentDate().ToShortDateString()}','MM/dd/yyyy') where arn = '{arn1.GetArn()}' and permit_code = '{sPermitCode}'";
                    db.Database.ExecuteSqlCommand(sQuery);
                }
            }
        }

        private void AssignCertofOccupancyNo()
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;
            string sYear = AppSettingsManager.GetCurrentDate().Year.ToString("####");
            string sMonth = AppSettingsManager.GetCurrentDate().Month.ToString("##");
            string sCertNo = string.Empty;
            int iOccu = 0;

            try
            {
                sQuery = $"select substr(cert_no,6,11) from curr_cert_no where cert_no like '{sYear}%'";
                sCertNo = db.Database.SqlQuery<string>(sQuery).SingleOrDefault();
            }
            catch { }

            int.TryParse(sCertNo, out iOccu);
            if (iOccu == 0)
                iOccu = 1;
            string sSeries = string.Empty;
            sSeries = FormatSeries(iOccu);
            sCertNo = sYear + "-" + sMonth + "-" + sSeries;

            string sTmpCert = string.Empty;
            
            if (iOccu == 1)
            {
                sTmpCert = sYear + "-" + sMonth + "-" + FormatSeries(iOccu+1);

                sQuery = $"insert into curr_cert_no values (:1)";
                db.Database.ExecuteSqlCommand(sQuery,
                            new OracleParameter(":1", sTmpCert));
            }
            else
            {
                sTmpCert = sYear + "-" + sMonth + "-" + FormatSeries(iOccu+1);
                sQuery = "update curr_cert_no set cert_no = '" + sTmpCert + "' ";
                sQuery += "where cert_no like '{sYear}%'";
                db.Database.ExecuteSqlCommand(sQuery);
            }

            sQuery = $"insert into cert_occupancy values (:1,:2,to_date(:3,'MM/dd/yyyy'))";
            db.Database.ExecuteSqlCommand(sQuery,
                        new OracleParameter(":1", arn1.GetArn()),
                        new OracleParameter(":2", sCertNo),
                        new OracleParameter(":3", string.Format("{0:MM/dd/yyyy}",AppSettingsManager.GetCurrentDate())));

            string sPermitFr, sPermitTo, sPermitCode;

            PermitList permit = new PermitList(null);
            sPermitCode = permit.GetPermitCode("OCCUPANCY");
            int iCnt = 0;

            sQuery = $"Select count(*) from other_permit_ass where permit_code = '{permit.GetPermitCode("OCCUPANCY")}'";
            iCnt = db.Database.SqlQuery<Int32>(sQuery).SingleOrDefault();
            
            if (iCnt > 0)
            {
                sQuery = $"update other_permit_ass set cu_permit_no = '{sSeries}' where permit_code = '{sPermitCode}'";
                db.Database.ExecuteSqlCommand(sQuery);
            }
            else
            {
                int iPermitTo = 999999;

                sPermitFr = sSeries;
                sPermitTo = FormatSeries(iPermitTo);


                sQuery = $"insert into other_permit_ass values (:1,:2,:3,to_date(:4,'MM/dd/yyyy'),:5)";
                db.Database.ExecuteSqlCommand(sQuery,
                            new OracleParameter(":1", sPermitCode),
                            new OracleParameter(":2", sPermitFr),
                            new OracleParameter(":3", sPermitTo),
                            new OracleParameter(":4", string.Format("{0:MM/dd/yyyy}", AppSettingsManager.GetCurrentDate())),
                            new OracleParameter(":5", AppSettingsManager.SystemUser.UserCode));

            }
        }

        private string GetCurrentPermitNo(string sPermitCode)
        {
            string sPermitNo = string.Empty;
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;
            string sYear = string.Empty;
            string sMonth = string.Empty;
            int iPermit = 0;

            sQuery = $"select * from permit_assigned where permit_code = '{sPermitCode}'";
            var result = db.Database.SqlQuery<PERMIT_ASSIGNED>(sQuery).ToList();

            foreach (var item in result)
            {
                int.TryParse(item.CU_PERMIT_NO, out iPermit);
            }

            if (iPermit == 0)
                iPermit = 1;

            sYear = AppSettingsManager.GetCurrentDate().Year.ToString("####");
            sMonth = AppSettingsManager.GetCurrentDate().Month.ToString("0#");
            sPermitNo = FormatSeries(iPermit);
            sPermitNo = sYear + "-" + sMonth + "-" + sPermitNo;

            if (iPermit == 1)
            {
                iPermit++;

                sQuery = $"insert into permit_assigned values (:1,:2,:3,:4,:5)";
                db.Database.ExecuteSqlCommand(sQuery,
                            new OracleParameter(":1", sPermitCode),
                            new OracleParameter(":2", sPermitNo),
                            new OracleParameter(":3", iPermit.ToString()),
                            new OracleParameter(":4", AppSettingsManager.GetCurrentDate()),
                            new OracleParameter(":5", AppSettingsManager.SystemUser.UserCode));
            }
            else
            {
                iPermit++;

                sQuery = $"update permit_assigned set cu_permit_no = '{iPermit.ToString()}' where permit_code = '{sPermitCode}'";
                db.Database.ExecuteSqlCommand(sQuery);
            }
            return sPermitNo;
        }

        private string FormatSeries(int iSeries)
        {
            string sSeries = string.Empty;
            sSeries = iSeries.ToString();

            switch (sSeries.Length)
            {
                case 1:
                    {
                        sSeries = "00000" + iSeries.ToString();
                        break;
                    }
                case 2:
                    {
                        sSeries = "0000" + iSeries.ToString();
                        break;
                    }
                case 3:
                    {
                        sSeries = "000" + iSeries.ToString();
                        break;
                    }
                case 4:
                    {
                        sSeries = "00" + iSeries.ToString();
                        break;
                    }
                case 5:
                    {
                        sSeries = "0" + iSeries.ToString();
                        break;
                    }
                case 6:
                    {
                        sSeries = iSeries.ToString();
                        break;
                    }

            }

            return sSeries;
        }

        private void chkOthers_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOthers.Checked)
            {
                frmOtherCert form = new frmOtherCert();
                form.ShowDialog();
                m_sCertType = form.m_sCert;

                OnCertTypeSelect();

                if (!string.IsNullOrEmpty(arn1.GetArn()))
                    DisplayData();
            }
            else
                m_sCertType = "";
            

        }

        private void OnCertTypeSelect()
        {
            if (!string.IsNullOrEmpty(m_sCertType))
                chkOthers.Checked = true;
            else
            {
                chkOthers.Checked = false;
                m_sCertType = string.Empty;
            }
        }

        private void dgvList_Leave(object sender, EventArgs e)
        {
            // ComputeTotal();
        }

        //private void dgvList_CellLeave(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.ColumnIndex == 2)
        //        ComputeTotal();
        //}

        // AFM 20191017 changed event to properly get value before firing
        private void dgvList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
                ComputeTotal();
        }


        private void txtCash_Leave(object sender, EventArgs e)
        {
            double dTmp = 0;
            double.TryParse(txtCash.Text.ToString(), out dTmp);

            txtCash.Text = string.Format("{0:#,###.00}", dTmp);
        }
    }
}
