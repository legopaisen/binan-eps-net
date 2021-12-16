using Common.AppSettings;
using Common.DataConnector;
using EPSEntities.Connection;
using EPSEntities.Entity;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Modules.Reports
{
    public partial class frmPermits : Form
    {
        public frmPermits()
        {
            InitializeComponent();
        }

        private string m_sAN = string.Empty;
        public static ConnectionString dbConn = new ConnectionString();
        private string m_sPermitCode = string.Empty;
        private string m_sPermitNo = string.Empty;

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPermits_Load(object sender, EventArgs e)
        {
            an1.ArnCode.Enabled = true;
            btnGenerate.Enabled = false;
            EnableControls(false);
        }

        private void EnableControls(bool bln)
        {
            txtProjDesc.Enabled = bln;
            txtLot.Enabled = bln;
            txtBlk.Enabled = bln;
            txtStreet.Enabled = bln;
            txtBrgy.Enabled = bln;
            txtCity.Enabled = bln;
            txtScope.Enabled = bln;
            txtEngr.Enabled = bln;
            txtProjCost.Enabled = bln;
            txtOr.Enabled = bln;
            txtOwner.Enabled = bln;
            txtOwnAddr.Enabled = bln;
            txtTCT.Enabled = bln;
            dtIssued.Enabled = bln;

            rdoWithoutPermit.Enabled = bln;
            rdoWithPermit.Enabled = bln;

            btnEditPermit.Enabled = bln;
            txtPermitCode.Enabled = bln;
            txtPermitYear.Enabled = bln;
            txtPermitMonth.Enabled = bln;
            txtPermitSeries.Enabled = bln;
        }

        private void ClearControls()
        {
            an1.Clear();
            rdoWithoutPermit.Checked = false;
            rdoWithPermit.Checked = false;
            m_sAN = string.Empty;
            ClearAllText(this);

        }

        private void ClearAllText(Control con)  // ty google
        {
            foreach (Control c in con.Controls)
            {
                if (c is TextBox)
                    ((TextBox)c).Clear();
                else
                    ClearAllText(c);
            }
            m_sAN = "";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        private void LoadGrid(string An)
        {
            ClearAllText(this);
            dgvList.Rows.Clear();
            string sProjOwner = string.Empty;
            string sTCT = string.Empty;
            string sScope = string.Empty;
            string sCategory = string.Empty;
            string sArn = string.Empty;
            string sNoStoreys = string.Empty;
            double dFlrArea = 0;

            OracleResultSet res = new OracleResultSet();
            //permit code = 1 is building permit, change depending on lgu
            if (cmbFilter.Text == "PAID" && rdoWithoutPermit.Checked == true)
                res.Query = $"select distinct APP.*, PI.fees_amt_due, PI.or_no, PT.permit_desc, BD.total_flr_area, BD.no_storeys, null as date_assigned from application APP, payments_info PI, permit_tbl PT, building BD where APP.arn = PI.refno and APP.permit_code = PT.permit_code and APP.bldg_no = BD.bldg_no and PT.permit_desc = 'BUILDING PERMIT' and APP.permit_no is null and (APP.arn like 'AN%' and APP.arn like '{An}%') and APP.arn not in (select arn from permit_arn where permit_arn.arn = APP.arn) and APP.permit_code = '01' and PI.permit_code = '01' order by APP.date_applied";
            else if (cmbFilter.Text == "PAID" && rdoWithPermit.Checked == true)
                res.Query = $"select distinct APP.*, PI.fees_amt_due, PI.or_no, PT.permit_desc, BD.total_flr_area, BD.no_storeys, PA.date_assigned from application APP, payments_info PI, permit_tbl PT, permit_arn PA, building BD where APP.arn = PI.refno and APP.permit_code = PT.permit_code and APP.arn = PA.arn and APP.bldg_no = BD.bldg_no and PT.permit_desc = 'BUILDING PERMIT' and APP.permit_no is not null and (APP.arn like 'AN%' and APP.arn like '{An}%') and APP.permit_code = '01' and PI.permit_code = '01' order by APP.date_applied";
            else if (cmbFilter.Text == "PAID" && rdoWithPermit.Checked == false && rdoWithoutPermit.Checked == false)
                res.Query = $"select distinct APP.*, PI.fees_amt_due, PI.or_no, PT.permit_desc, BD.total_flr_area, BD.no_storeys, PA.date_assigned from application APP, payments_info PI, permit_tbl PT, permit_arn PA, building BD where APP.arn = PI.refno and APP.permit_code = PT.permit_code and APP.arn = PA.arn and APP.bldg_no = BD.bldg_no and PT.permit_desc = 'BUILDING PERMIT' and (APP.arn like 'AN%' and APP.arn like '{An}%') and APP.permit_code = '01' and PI.permit_code = '01' order by APP.date_applied";

            else if (cmbFilter.Text == "UNPAID")
            {
                res.Query = "select distinct APP.*, PT.permit_desc, BD.total_flr_area, BD.no_storeys from application_que APP, permit_tbl PT, building BD where APP.permit_code = PT.permit_code and APP.bldg_no = BD.bldg_no and PT.permit_desc = 'BUILDING PERMIT' and APP.arn like 'AN%' and APP.permit_code = '01' and PT.permit_code = '01' order by APP.date_applied";
                if (res.Execute())
                    while(res.Read())
                    {
                        sProjOwner = GetOwnerName(res.GetString("proj_owner"));
                        sTCT = GetTCT(res.GetString("proj_owner"));
                        sArn = res.GetString("arn");
                        sScope = GetApplicationScope(res.GetString("scope_code"));
                        sNoStoreys = res.GetInt("no_storeys").ToString();
                        dFlrArea = res.GetDouble("total_flr_area");

                        dgvList.Rows.Add(
                            sArn,
                            res.GetString("proj_desc"),
                            sProjOwner,
                            res.GetString("proj_lot_no"),
                            res.GetString("proj_blk_no"),
                            res.GetString("proj_addr"),
                            res.GetString("proj_brgy"),
                            res.GetString("proj_city"),
                            sTCT,
                            dFlrArea.ToString("#,##0.00"),
                            sNoStoreys,
                            sScope,
                            "",
                            "",
                            "",
                            res.GetString("permit_no"),
                            res.GetString("proj_owner"),
                            res.GetString("permit_code"),
                            ""
                            );

                    }
                return;
            }


            if (res.Execute())
                while (res.Read())
                {
                    sProjOwner = GetOwnerName(res.GetString("proj_owner"));
                    sTCT = GetTCT(res.GetString("proj_owner"));
                    sArn = res.GetString("arn");
                    sScope = GetApplicationScope(res.GetString("scope_code"));
                    sNoStoreys = res.GetInt("no_storeys").ToString();
                    dFlrArea = res.GetDouble("total_flr_area");
                    string sDate = string.Empty;
                    try
                    {
                        sDate = res.GetDateTime("date_assigned").ToShortDateString();
                    }
                    catch
                    {
                        sDate = "";
                    }

                    dgvList.Rows.Add(
                        sArn,
                        res.GetString("proj_desc"),
                        sProjOwner,
                        res.GetString("proj_lot_no"),
                        res.GetString("proj_blk_no"),
                        res.GetString("proj_addr"),
                        res.GetString("proj_brgy"),
                        res.GetString("proj_city"),
                        sTCT,
                        dFlrArea.ToString("#,##0.00"),
                        sNoStoreys,
                        sScope,
                        res.GetDouble("fees_amt_due").ToString("##,##0.00"),
                        "",
                        res.GetString("or_no"),
                        res.GetString("permit_no"),
                        res.GetString("proj_owner"),
                        res.GetString("permit_code"),
                        sDate
                        );
                }
            res.Close();
        }

        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
           if(cmbFilter.Text == "UNPAID")
            {
                rdoWithoutPermit.Enabled = false;
                rdoWithPermit.Enabled = false;
                btnGenerate.Enabled = false;
            }
           else
            {
                rdoWithoutPermit.Enabled = true;
                rdoWithPermit.Enabled = true;
            }
            LoadGrid("");
        }

        private string GetOwnerName(string sVal)
        {
            OracleResultSet res = new OracleResultSet();
            res.Query = $"select * from account where acct_code = '{sVal}'";
            if(res.Execute())
                if(res.Read())
                {
                    sVal = res.GetString("acct_fn") + " " + res.GetString("acct_mi") + ". " + res.GetString("acct_ln");
                }
            res.Close();
            return sVal;
        }


        private string GetTCT(string sVal)
        {
            OracleResultSet res = new OracleResultSet();
            res.Query = $"select * from account where acct_code = '{sVal}'";
            if (res.Execute())
                if (res.Read())
                {
                    sVal = res.GetString("acct_tct_lot") ;
                }
            res.Close();
            return sVal;
        }

        private string GetApplicationScope(string sVal)
        {
            OracleResultSet res = new OracleResultSet();
            res.Query = $"select * from scope_tbl where scope_code = '{sVal}'";
            if (res.Execute())
                if (res.Read())
                {
                    sVal = res.GetString("scope_desc");
                }
            res.Close();
            return sVal;                     
        }

        private string GetApplicationEngr(string sVal)
        {
            OracleResultSet res = new OracleResultSet();
            res.Query = $"select * from ENGINEER_TBL where engr_code = '{sVal}'";
            if (res.Execute())
                if (res.Read())
                {
                    sVal = res.GetString("engr_fn") + " " + res.GetString("engr_mi") + ". " + res.GetString("engr_ln");
                }
            res.Close();
            return sVal;
        }

        private void rdoWithoutPermit_CheckedChanged(object sender, EventArgs e)
        {
            LoadGrid("");
            btnEditPermit.Enabled = false;
        }

        private void rdoWithPermit_CheckedChanged(object sender, EventArgs e)
        {
            LoadGrid("");
            txtPermitCode.Enabled = false;
            txtPermitYear.Enabled = false;
            txtPermitMonth.Enabled = false;
            txtPermitSeries.Enabled = false;
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                an1.SetAn(dgvList[0, e.RowIndex].Value.ToString());
                m_sAN = an1.GetAn();
            }
            catch { }
            try
            {
                txtProjDesc.Text = dgvList[1, e.RowIndex].Value.ToString();
            }
            catch { }
            try
            {
                txtOwner.Text = dgvList[2, e.RowIndex].Value.ToString();
            }
            catch { }
            try
            {
                txtLot.Text = dgvList[3, e.RowIndex].Value.ToString();
            }
            catch { }
            try
            {
                txtBlk.Text = dgvList[4, e.RowIndex].Value.ToString();
            }
            catch { }
            try
            {
                txtStreet.Text = dgvList[5, e.RowIndex].Value.ToString();
            }
            catch { }
            try
            {
                txtBrgy.Text = dgvList[6, e.RowIndex].Value.ToString();
            }
            catch { }
            try
            {
                txtCity.Text = dgvList[7, e.RowIndex].Value.ToString();
            }
            catch { }
            try
            {
                txtTCT.Text = AppSettingsManager.GetAcctName("TCT", dgvList[8, e.RowIndex].Value.ToString());
            }
            catch { }
            try
            {
                txtFlrArea.Text = dgvList[9, e.RowIndex].Value.ToString();
            }
            catch { }
            try
            {
                txtStoreys.Text = dgvList[10, e.RowIndex].Value.ToString();
            }
            catch { }
            try
            {
                txtScope.Text = dgvList[11, e.RowIndex].Value.ToString();
            }
            catch { }
            try
            {
                txtProjCost.Text = dgvList[12, e.RowIndex].Value.ToString();
            }
            catch { }
            try
            {
                txtEngr.Text = dgvList[13, e.RowIndex].Value.ToString();
            }
            catch { }

            try
            {
                txtOr.Text = dgvList[14, e.RowIndex].Value.ToString();
            }
            catch { }
            try
            {
                txtOwnAddr.Text = AppSettingsManager.GetAcctName("addr", dgvList[16, e.RowIndex].Value.ToString());
            }
            catch { }


            try
            {
                SetPermitNo(dgvList[15, e.RowIndex].Value.ToString());
            }
            catch { }

            try
            {
                m_sPermitCode = dgvList[17, e.RowIndex].Value.ToString();
            }
            catch { }

            try
            {
                string sDt = dgvList[18, e.RowIndex].Value.ToString();
                DateTime dt;
                DateTime.TryParse(sDt, out dt);
                dtIssued.Value = dt;
            }
            catch { }


            try
            {
                if (!string.IsNullOrEmpty(dgvList[15, e.RowIndex].Value.ToString()))
                    btnGenerate.Text = "Print";
                else
                    btnGenerate.Text = "Generate";

                if (cmbFilter.Text == "UNPAID")
                    btnGenerate.Enabled = false;
                else
                    btnGenerate.Enabled = true;
            }
            catch { }

            try
            {
                if (!string.IsNullOrEmpty(dgvList[15, e.RowIndex].Value.ToString()))
                {
                    btnGenerate.Text = "Print";
                    //btnEditPermit.Enabled = true;
                }
            }
            catch { }

            //if(cmbFilter.Text == "PAID" && rdoWithoutPermit.Checked == true) //disabled - set arn as always automated as per maam mitch
            //{
            //    txtPermitCode.Enabled = true;
            //    txtPermitYear.Enabled = true;
            //    txtPermitMonth.Enabled = true;
            //    txtPermitSeries.Enabled = true;
            //}          
        }

        private void SetPermitNo(string sVal)
        {
            string sPermitAcro = sVal.Substring(0, sVal.IndexOf("-"));

            txtPermitCode.Text = sPermitAcro;

            if(sPermitAcro.Length == 2)
            {
                txtPermitYear.Text = sVal.Substring(3, 4);
                txtPermitMonth.Text = sVal.Substring(7, 2);
                txtPermitSeries.Text = sVal.Substring(10, 4);
            }
            else if (sPermitAcro.Length == 3)
            {
                txtPermitYear.Text = sVal.Substring(4, 4);
                txtPermitMonth.Text = sVal.Substring(8, 2);
                txtPermitSeries.Text = sVal.Substring(11, 4);
            }
        }

        private string GetPermitNo()
        {
            string sPermitNo = string.Empty;
            if (!string.IsNullOrEmpty(txtPermitCode.Text.Trim()) && !string.IsNullOrEmpty(txtPermitCode.Text.Trim()) && !string.IsNullOrEmpty(txtPermitCode.Text.Trim()) && !string.IsNullOrEmpty(txtPermitCode.Text.Trim()))
            {
                sPermitNo = txtPermitCode.Text.Trim() + "-" + txtPermitYear.Text.Trim() + txtPermitMonth.Text.Trim() + "-" + txtPermitSeries.Text.Trim();
            }
            return sPermitNo;
        }

        private string GetCurrentPermitNoCheck(string sPermitCode)
        {
            string sPermitNo = string.Empty;
            var db = new EPSConnection(dbConn);
            OracleResultSet res = new OracleResultSet();
            string sQuery = string.Empty;
            string sYear = string.Empty;
            string sMonth = string.Empty;
            string sPermitAcro = AppSettingsManager.GetPermitAcro(sPermitCode);
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
            sPermitNo = sPermitAcro + "-" + sYear + sMonth + "-" + sPermitNo;

            return sPermitNo;     

        }

        private DateTime GetLastYearMonth()
        {
            OracleResultSet res = new OracleResultSet();
            DateTime dt = AppSettingsManager.GetSystemDate();
            res.Query = "select max(date_assigned) as date from permit_arn";
            if(res.Execute())
                if(res.Read())
                {
                    dt = res.GetDateTime("date");
                }
            return dt;
        }

        private string GetCurrentPermitNo(string sPermitCode)
        {
            string sPermitNo = string.Empty;
            var db = new EPSConnection(dbConn);
            OracleResultSet res = new OracleResultSet();
            OracleResultSet res2 = new OracleResultSet();
            string sQuery = string.Empty;
            string sYear = string.Empty;
            string sMonth = string.Empty;
            string sNewYearMonth = string.Empty;
            string sPrevYearMonth = string.Empty;
            string sPermitAcro = AppSettingsManager.GetPermitAcro(sPermitCode);
            int iPermit = 0;
            int iCnt = 0;

            sQuery = $"select * from permit_assigned where permit_code = '{sPermitCode}'";
            var result = db.Database.SqlQuery<PERMIT_ASSIGNED>(sQuery).ToList();

            foreach (var item in result)
            {
                int.TryParse(item.CU_PERMIT_NO, out iPermit);
            }

            if (iPermit == 0)
                iPermit = 1;

            sYear = AppSettingsManager.GetSystemDate().Year.ToString("####");
            sMonth = AppSettingsManager.GetSystemDate().Month.ToString("0#");

            sPrevYearMonth = sYear + sMonth;
            sNewYearMonth = GetLastYearMonth().Year.ToString("####") + GetLastYearMonth().Month.ToString("0#");
            if (sPrevYearMonth != sNewYearMonth)
                iPermit = 1;

            sPermitNo = FormatSeries(iPermit);
            sPermitNo = sPermitAcro + "-" + sYear + sMonth + "-" + sPermitNo;

            res.Query = $"select count(*) from permit_arn where permit_no = '{sPermitNo}'";
            int.TryParse(res.ExecuteScalar(), out iCnt);
            if(iCnt > 0)
            {
                res2.Query = $"select cu_permit_no from permit_assigned where permit_code = '{sPermitCode}'";
                if(res2.Execute())
                    if(res2.Read())
                    {
                        int iPermitMax = Convert.ToInt32(res2.GetString("cu_permit_no"));
                        string sPermitSeriesNew = FormatSeries(iPermitMax + 1);
                        string sPermitNoTmp = sPermitAcro + "-" + sYear + sMonth + "-" + sPermitSeriesNew;

                        if (MessageBox.Show($"Permit No. already used!\nWould you like to apply suggested permit no.?\n\nSuggested: {sPermitNoTmp}", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                            return "0";
                        else
                        {
                            sPermitNo = sPermitNoTmp;
                            iPermit = iPermitMax + 1;
                        }

                    }

            }

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

            //res.Query = $"DELETE FROM PERMIT_ARN WHERE ARN = $'{m_sAN}'";

            //res.Query = "INSERT INTO PERMIT_ARN VALUES(";
            //res.Query += $"'{m_sAN}', ";
            //res.Query += $"'{sPermitNo}', ";
            //res.Query += $"to_date('{AppSettingsManager.GetSystemDate().ToShortDateString()}','MM/dd/yyyy'), ";
            //res.Query += $"'{AppSettingsManager.SystemUser.UserCode}') ";
            //if(res.ExecuteNonQuery() == 0)
            //{ }
            //res.Close();

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
                        sSeries = "000" + iSeries.ToString();
                        break;
                    }
                case 2:
                    {
                        sSeries = "00" + iSeries.ToString();
                        break;
                    }
                case 3:
                    {
                        sSeries = "0" + iSeries.ToString();
                        break;
                    }
                case 4:
                    {
                        sSeries = iSeries.ToString();
                        break;
                    }

            }

            return sSeries;
        }


        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string sAssignedEngr = string.Empty;
            string sPermitTmp = string.Empty;
            OracleResultSet res = new OracleResultSet();
            frmEngrAssigned form = new frmEngrAssigned();
            form.m_sAn = m_sAN;
            form.m_sPermitCode = m_sPermitCode;
            form.ShowDialog();
            sAssignedEngr = form.m_sEngrName;
            if (form.m_isCancel == true)
                return;

            frmFSECSelect frmfsecselect = new frmFSECSelect();
            frmfsecselect.ShowDialog();
            if (frmfsecselect.m_isCancel == true)
                return;

            m_sPermitNo = GetPermitNo();
            //if (string.IsNullOrEmpty(m_sPermitNo) && btnEditPermit.Text == "Save")
            if (string.IsNullOrEmpty(m_sPermitNo)) //set permit as always automated as per maam mitch
            {
                m_sPermitNo = GetCurrentPermitNo(m_sPermitCode);
                if (m_sPermitNo == "0")
                    return;

                res.Query = "INSERT INTO PERMIT_ARN VALUES(";
                res.Query += $"'{m_sAN}', ";
                res.Query += $"'{m_sPermitNo}', ";
                res.Query += $"to_date('{AppSettingsManager.GetSystemDate().ToShortDateString()}','MM/dd/yyyy'), ";
                res.Query += $"'{AppSettingsManager.SystemUser.UserCode}') ";
                if (res.ExecuteNonQuery() == 0)
                { }
                res.Close();
            }
            //disabled block below - set permit as always automated as per maam mitch
            //else if(!string.IsNullOrEmpty(m_sPermitNo) && btnEditPermit.Text == "Save")
            //else if(!string.IsNullOrEmpty(m_sPermitNo))
            //{
            //    if (MessageBox.Show("Permit no. manually applied. Continue?", "Detected Permit No.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            //        return;
            //    else
            //    {
            //        if(!ValidatePermitNo(m_sPermitNo))
            //        {
            //            sPermitTmp = GetCurrentPermitNoCheck(m_sPermitCode);
            //            if (MessageBox.Show($"Permit No. already used!\nWould you like to apply suggested permit no.?\n\nSuggested: {sPermitTmp}", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            //            {
            //                m_sPermitNo = GetCurrentPermitNo(m_sPermitCode);
            //            }
            //            else
            //                return;
            //        }

            //        res.Query = "INSERT INTO PERMIT_ARN VALUES(";
            //        res.Query += $"'{m_sAN}', ";
            //        res.Query += $"'{m_sPermitNo}', ";
            //        res.Query += $"to_date('{AppSettingsManager.GetSystemDate().ToShortDateString()}','MM/dd/yyyy'), ";
            //        res.Query += $"'{AppSettingsManager.SystemUser.UserCode}') ";
            //        if (res.ExecuteNonQuery() == 0)
            //        { }
            //        res.Close();
            //    }
            //}

            frmReport frmreport = new frmReport();
            if (MessageBox.Show("Pre-printed form?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                frmreport.isPrePrint = true;
            else
                frmreport.isPrePrint = false;

            frmreport.ReportName = "BUILDING PERMIT";
            frmreport.PermitNo = m_sPermitNo;
            frmreport.PermitDtIssued = dtIssued.Value;
            frmreport.ORNo = txtOr.Text.Trim();
            frmreport.OwnerName = txtOwner.Text;
            frmreport.ProjName = txtProjDesc.Text;
            frmreport.LotNo = txtLot.Text;
            frmreport.BlkNo = txtBlk.Text;
            frmreport.Street = txtStreet.Text;
            frmreport.TCT = txtTCT.Text;
            frmreport.Brgy = txtBrgy.Text;
            frmreport.City = txtCity.Text;
            frmreport.Scope = txtScope.Text;
            frmreport.ProjCost = txtProjCost.Text;
            frmreport.AssignedEngr = sAssignedEngr;
            frmreport.NoStoreys = txtStoreys.Text;
            frmreport.FloorArea = txtFlrArea.Text;
            frmreport.An = m_sAN;
            frmreport.FsecNo = frmfsecselect.m_sFsecNo;
            frmreport.FsecDate = frmfsecselect.m_sDateFsec;
            frmreport.ShowDialog();

            int iCnt = 0;
            string sDateIssued = string.Empty;
            sDateIssued = string.Format("{0:dd/MMM/yyyy}", dtIssued.Value);
            res.Query = $"UPDATE APPLICATION SET PERMIT_NO = '{m_sPermitNo}', DATE_ISSUED = '{sDateIssued}' WHERE ARN = '{m_sAN}' and permit_code = '01'"; //permit code = 1, default bldg permit code. specified to update only bldg permit record
            if(res.ExecuteNonQuery() == 0)
            { }

            if (Utilities.AuditTrail.InsertTrail("TP-P", "PERMIT PRINT", "ARN: " + m_sAN) == 0)
            {
                MessageBox.Show("Failed to insert audit trail.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ClearControls();
            LoadGrid("");
            btnGenerate.Enabled = false;
        }

        private void btnEditPermit_Click(object sender, EventArgs e)
        {
            if(btnEditPermit.Text == "Edit")
            {
                if(string.IsNullOrEmpty(m_sAN))
                {
                    MessageBox.Show("No record selected!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                txtPermitCode.Enabled = true;
                txtPermitYear.Enabled = true;
                txtPermitMonth.Enabled = true;
                txtPermitSeries.Enabled = true;
                btnGenerate.Enabled = false;
                dgvList.Enabled = false;
                an1.Enabled = false;
                btnEditPermit.Text = "Save";
            }
            else
            {
                string sPermitTmp = string.Empty;
                string sNewPermit = string.Empty;
                m_sPermitNo = GetPermitNo(); //altered
                if (string.IsNullOrEmpty(txtPermitCode.Text.Trim()) && string.IsNullOrEmpty(txtPermitCode.Text.Trim()) && string.IsNullOrEmpty(txtPermitCode.Text.Trim()) && string.IsNullOrEmpty(txtPermitCode.Text.Trim()))
                {
                    MessageBox.Show("Invalid Permit No.!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
             

                if (MessageBox.Show("Edit permit no?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    bool isManual = false;
                    if (!ValidatePermitNo(m_sPermitNo))
                    {
                        sPermitTmp = GetCurrentPermitNoCheck(m_sPermitCode);
                        if (MessageBox.Show($"Permit No. already used!\nWould you like to apply suggested permit no.?\n\nSuggested: {sPermitTmp}", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            sNewPermit = GetCurrentPermitNo(m_sPermitCode);
                            isManual = true;
                        }
                        else
                            return;
                    }
                    else
                        sNewPermit = m_sPermitNo;

                    OracleResultSet res = new OracleResultSet();
                    res.Query = $"UPDATE APPLICATION SET PERMIT_NO = '{sNewPermit}' WHERE ARN = '{m_sAN}'";
                    if(res.ExecuteNonQuery() == 0)
                    { }
                    res.Close();

                    res.Query = $"INSERT INTO PERMIT_ARN_HIST SELECT * FROM PERMIT_ARN WHERE ARN = '{m_sAN}' AND PERMIT_NO != '{sNewPermit}'";
                    if (res.ExecuteNonQuery() == 0)
                    { }
                    res.Close();

                    res.Query = $"DELETE FROM PERMIT_ARN WHERE ARN = '{m_sAN}' AND PERMIT_NO != '{sNewPermit}'";
                    if (res.ExecuteNonQuery() == 0)
                    { }
                    res.Close();

                    res.Query = "INSERT INTO PERMIT_ARN VALUES(";
                    res.Query += $"'{m_sAN}', ";
                    res.Query += $"'{sNewPermit}', ";
                    res.Query += $"to_date('{AppSettingsManager.GetSystemDate().ToShortDateString()}','MM/dd/yyyy'), ";
                    res.Query += $"'{AppSettingsManager.SystemUser.UserCode}') ";
                    if (res.ExecuteNonQuery() == 0)
                    { }
                    res.Close();

                    MessageBox.Show("Permit No. Changed!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (Utilities.AuditTrail.InsertTrail("TP-EPN", "PERMIT NO EDIT", "ARN: " + m_sAN + "TO " + m_sPermitNo) == 0)
                    {
                        MessageBox.Show("Failed to insert audit trail.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    ClearControls();
                    LoadGrid("");
                    txtPermitCode.Enabled = false;
                    txtPermitYear.Enabled = false;
                    txtPermitMonth.Enabled = false;
                    txtPermitSeries.Enabled = false;
                    btnGenerate.Enabled = false;
                    dgvList.Enabled = true;
                    an1.Enabled = true;
                    btnEditPermit.Text = "Edit";
                }
                
            }

        }

        private bool ValidatePermitNo(string sPermitNo)
        {
            OracleResultSet res = new OracleResultSet();
            int iCnt = 0;
            res.Query = $"select COUNT(*) from permit_arn where permit_no = '{sPermitNo.Trim()}'";
            int.TryParse(res.ExecuteScalar(), out iCnt);
            if (iCnt > 0)
                return false;
            else
                return true;
        }

        private void an1_Load(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadGrid(an1.GetAn());
        }

        private void txtPermitSeries_Leave(object sender, EventArgs e)
        {
            string sSeries = string.Empty;
            sSeries = txtPermitSeries.Text.Trim();


            switch (sSeries.Length)
            {
                case 1:
                    {
                        sSeries = "000" + sSeries;
                        break;
                    }
                case 2:
                    {
                        sSeries = "00" + sSeries;
                        break;
                    }
                case 3:
                    {
                        sSeries = "0" + sSeries;
                        break;
                    }
                case 4:
                    {
                        sSeries = sSeries;
                        break;
                    }

            }
            txtPermitSeries.Text = sSeries;

        }
    }
}
