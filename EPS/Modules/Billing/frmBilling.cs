using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modules.Utilities;
using Modules.Reports;
using EPSEntities.Connection;
using Common.AppSettings;
using Amellar.Common.ImageViewer;
using Common.DataConnector;
using Modules.Transactions;

namespace Modules.Billing
{
    public partial class frmBilling : Form
    {
        public static ConnectionString dbConn = new ConnectionString();
        TaskManager taskman = new TaskManager();
        public string Source { get; set; }
        private FormClass RecordClass = null;
        public string m_sModule = string.Empty;
        public string m_sAN = string.Empty;
        public string PermitCode { get; set; }
        public string ModuleCode { get; set; }
        private string m_sCurrPermit = string.Empty;
        private string m_sPrevPermit = string.Empty;

        protected frmImageList m_frmImageList;
        protected frmImageViewer m_frmImageViewer;
        public static int m_intImageListInstance;
        private bool isActive = false;


        public bool CtrlPressed = false;

        public frmBilling()
        {
            InitializeComponent();
        }

        private void frmBilling_Load(object sender, EventArgs e)
        {
            //AFM 20201104 COMMENTED - REQUESTED BY BINAN AND RJ 20201103 - NEW BILLING DESIGN AND FLOW (s)
            //PermitList permit = new PermitList(null);
            //PermitCode = string.Empty;
            //PermitCode = permit.GetPermitCode(Source);
            //AFM 20201104 COMMENTED - REQUESTED BY BINAN AND RJ 20201103 - NEW BILLING DESIGN AND FLOW (e)

            m_sModule = "BILLING";
            PopulatePermit();

            if (Source == "CERTIFICATE OF OCCUPANCY" || Source == "CERTIFICATE OF ANNUAL INSPECTION")
            {
                RecordClass = new BillCertificate(this);
            }
            else
            {
                RecordClass = new Building(this);
                EnableControls(false);
            }

            //AFM 20201104 COMMENTED CONDITION - REQUESTED BY BINAN AND RJ 20201103 - NEW BILLING DESIGN AND FLOW (S)
            //if (Source == "BUILDING PERMIT")
            //{
            //    RecordClass = new Building(this);
            //}
            //else if (Source == "CERTIFICATE OF OCCUPANCY" || Source == "CERTIFICATE OF ANNUAL INSPECTION")
            //{
            //    RecordClass = new BillCertificate(this);
            //}
            //else
            //{
            //    RecordClass = new OtherPermit(this);
            //}

            //this.Text = "Billing - " + Source;
            //AFM 20201104 COMMENTED CONDITION - REQUESTED BY BINAN AND RJ 20201103 - NEW BILLING DESIGN AND FLOW (E)


            RecordClass.FormLoad();

            if (AppSettingsManager.GetConfigValue("30") == "1")// AFM 20191016 prompting for build up mode
            {
                MessageBox.Show("System is in Build-up Mode");
                txtBillNo.ReadOnly = false;
            }
            else
            {
                txtBillNo.ReadOnly = true;
                btnImgView.Visible = false;
            }

        }

        public void EnableControls(bool bln)
        {
            groupBox1.Enabled = bln;
            grpAssessment.Enabled = bln;
            btnSave.Enabled = bln;
            grpAddOn.Enabled = bln;
            grpOther.Enabled = bln;
            btnRequirements.Enabled = bln;
        }

        private void PopulatePermit() //AFM 20201104 REQUESTED BY BINAN AND RJ 20201103 - NEW BILLING DESIGN AND FLOW
        {
            cmbPermit.Items.Clear();
            OracleResultSet result = new OracleResultSet();
            result.Query = "select permit_desc from permit_tbl order by permit_code";
            if (result.Execute())
                while (result.Read())
                    cmbPermit.Items.Add(result.GetString("permit_desc"));
            result.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            m_sAN = an1.GetAn();
            TaskManager taskman = new TaskManager();

            if (string.IsNullOrEmpty(m_sAN))
            {
                SearchAccount.frmSearchARN form = new SearchAccount.frmSearchARN();
                if (Source == "CERTIFICATE OF OCCUPANCY" || Source == "CERTIFICATE OF ANNUAL INSPECTION")
                    form.SearchCriteria = "APP";
                else if (AppSettingsManager.GetConfigValue("30") == "1") //AFM 20200703 set config to 1 to get all arn in application table
                    form.SearchCriteria = "APP";
                else if(Source.Contains("OCCUPANCY")) //AFM 2021025 added for new process of occupancy
                    form.SearchCriteria = "QUE-OCC";
                else if (Source.Contains("ELECTRICAL")) 
                    form.SearchCriteria = "QUE-ELEC";
                else if (Source.Contains("MECHANICAL")) 
                    form.SearchCriteria = "QUE-MECH";
                else if (Source.Contains("CFEI")) 
                    form.SearchCriteria = "QUE-CFEI";
                else if(!Source.Contains("OCCUPANCY") && !Source.Contains("ELECTRICAL") && !Source.Contains("MECHANICAL") && !Source.Contains("BUILDING"))
                    form.SearchCriteria = "QUE-OTH";
                else
                    form.SearchCriteria = "QUE";

                form.PermitCode = PermitCode;
                form.ShowDialog();

                an1.SetAn(form.sArn);

                m_sAN = an1.GetAn();
            }
            else
                m_sAN = an1.GetAn();

            if (string.IsNullOrEmpty(m_sAN))
                return;

            //temporarily disabled 
            //if (!RecordClass.ValidatePermitNo())
                //return;

            if (!taskman.AddTask(m_sModule, m_sAN))
                return;

            if (AppSettingsManager.GetConfigValue("30") == "1") // validate posted payment in build up mode
            {
                if (RecordClass.ValidatePayment())
                    return;
            }

            if (!RecordClass.DisplayData())
            {
                RecordClass.ClearControls();
                return;
            }
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            RecordClass.ClearControls();
            taskman.RemTask(m_sAN);
            //an1.GetCode = "";
            //an1.GetLGUCode = "";
            an1.GetTaxYear = "";
            //an1.GetMonth = ""; // disabled for new arn of binan
            //an1.GetDistCode = "";
            an1.GetSeries = "";
            m_sAN = "";
            btnPrint.Enabled = false;
        }

        private void dgvAssessment_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if(m_sCurrPermit == m_sPrevPermit)
                RecordClass.CellLeave(sender, e);
            //if (dgvAssessment.CurrentCell.ColumnIndex == 7)
                //RecordClass.ComputeCell();
        }

        private void dgvParameter_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnCompute_Click(object sender, EventArgs e)
        {
            if (!RecordClass.Compute())
            {
                MessageBox.Show("Please complete all parameters", "Billing", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            RecordClass.ButtonOk("");
        }

        private void dgvAssessment_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //bool entry = false;
            //for (int row = 0; row < dgvPermit.Rows.Count; row++)
            //{
            //    if ((bool)dgvPermit[0, row].Value == true)
            //    {
            //    }
            //}           
            RecordClass.CellClick(sender, e);
        }

        private void frmBilling_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                taskman.RemTask(m_sAN);
                an1.GetCode = "";
                //an1.GetLGUCode = "";
                an1.GetTaxYear = "";
                //an1.GetMonth = ""; // disabled for new arn of binan
                //an1.GetDistCode = "";
                an1.GetSeries = "";
                m_sAN = "";

            }
            else
                return;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if((txtAddOnTotAmTDue.Text != "0.00" || txtAddOnTotAmTDue.Text != "") && (txtAmtDue.Text == "0.00" || txtAmtDue.Text == "")) //AFM 20211123 requested by client as per rj - allow to bill only additional fees on any permit
            {
                if (MessageBox.Show("Only additional fees are detected! Would you like to proceed?", "No Main Fees", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return;
                }
            }
            else if (txtAmtDue.Text == "0.00" || txtAmtDue.Text == "" || txtAmtDue.Text == string.Empty) //AFM 20191015 uncomputed fees validation
            {
                MessageBox.Show("No fees computed", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            RecordClass.Save();
        }

        private bool ValidatePermit()
        {
            for (int i = 0; i < dgvPermit.Rows.Count; i++)
            {
                if ((bool)dgvPermit[0, i].Value == true)
                {
                    return true;
                }
            }
            return false;
        }

        private void dgvPermit_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (ValidatePermit())
            {
                btnSave.Enabled = true;
            }
            else { btnSave.Enabled = false; }

            try
            {
                if ((bool)dgvPermit.CurrentRow.Cells[0].Value == true)
                {
                    dgvAssessment.Enabled = true;
                    dgvAddOnFees.Enabled = true;
                    dgvOtherFees.Enabled = true;

                    btnEdit.Enabled = true;

                    txtFront.Enabled = true;
                    txtLeft.Enabled = true;
                    txtRight.Enabled = true;
                    txtRear.Enabled = true;
                }
                else
                {
                    dgvAssessment.Enabled = false;
                    dgvAddOnFees.Enabled = false;
                    dgvOtherFees.Enabled = false;

                    btnEdit.Enabled = false;

                    txtFront.Enabled = false;
                    txtLeft.Enabled = false;
                    txtRight.Enabled = false;
                    txtRear.Enabled = false;
                }
            }
            catch { }
        }

        private void dgvPermit_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            m_sCurrPermit = dgvPermit[2, e.RowIndex].Value.ToString();
            if (ValidatePermit())
            {
                btnSave.Enabled = true;
            }
            else { btnSave.Enabled = false; }

            try
            {
                if ((bool)dgvPermit.CurrentRow.Cells[0].Value == true)
                {
                    dgvAssessment.Enabled = true;
                    dgvAddOnFees.Enabled = true;
                    dgvOtherFees.Enabled = true;
                    

                }
                else
                {
                    dgvAssessment.Enabled = false;
                    dgvAddOnFees.Enabled = false;
                    dgvOtherFees.Enabled = false;
                    
                }
            }
            catch { }
            try
            {
                RecordClass.PermitCellClick(sender, e);
                //AFM 20190911 permit checkbox (s)
                if ((bool)dgvPermit.CurrentCell.Value != true && (bool)dgvPermit.CurrentCell.Value != false) return;
                if ((bool)dgvPermit.CurrentRow.Cells[0].Value == true)
                    dgvPermit.CurrentRow.Cells[0].Value = false;
                else if ((bool)dgvPermit.CurrentRow.Cells[0].Value == false)
                    dgvPermit.CurrentCell.Value = true;
                //AFM 20190911 permit checkbox (e)
            }
            catch { }
            if(m_sCurrPermit == m_sPrevPermit)
            {
                RecordClass.PermitCellClick(sender, e);
                RemoveUnbilled();
            }


        }

        private void RemoveUnbilled() //AFM 20190913
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;
            List<string> feesCode = new List<string>();
            string PermitDlt = string.Empty;
            bool remove = false;
            for (int cnt = 0; cnt < dgvPermit.Rows.Count; cnt++)
            {
                if ((bool)dgvPermit[0, cnt].Value == false)
                {
                    if (dgvPermit[0, cnt].Selected == true)
                    {
                        for (int list = 0; list < dgvAssessment.Rows.Count; list++)
                        {
                            if ((bool)dgvAssessment[0, list].Value == true)
                            {
                                feesCode.Add(dgvAssessment[2, list].Value.ToString());
                            }
                        }
                    }
                }
            }
            foreach (var s in feesCode)
            {
                sQuery = $"delete from bill_tmp where arn = '{m_sAN}' and fees_code = '{s}' and fees_category = 'MAIN'";
                db.Database.ExecuteSqlCommand(sQuery);
                remove = true;
            }

            feesCode = new List<string>();
            for (int cnt = 0; cnt < dgvPermit.Rows.Count; cnt++)
            {
                if ((bool)dgvPermit[0, cnt].Value == false)
                {
                    if (dgvPermit[0, cnt].Selected == true)
                    {
                        for (int list = 0; list < dgvAddOnFees.Rows.Count; list++)
                        {
                            if ((bool)dgvAddOnFees[0, list].Value == true)
                            {
                                feesCode.Add(dgvAddOnFees[2, list].Value.ToString());
                            }
                        }
                    }
                }
            }
            foreach (var s in feesCode)
            {
                sQuery = $"delete from bill_tmp where arn = '{m_sAN}' and fees_code = '{s}' and fees_category = 'ADDITIONAL'";
                db.Database.ExecuteSqlCommand(sQuery);
                remove = true;
            }

            feesCode = new List<string>();
            for (int cnt = 0; cnt < dgvPermit.Rows.Count; cnt++)
            {
                if ((bool)dgvPermit[0, cnt].Value == false)
                {
                    if (dgvPermit[0, cnt].Selected == true)
                    {
                        for (int list = 0; list < dgvOtherFees.Rows.Count; list++)
                        {
                            if ((bool)dgvOtherFees[0, list].Value == true)
                            {
                                feesCode.Add(dgvOtherFees[2, list].Value.ToString());
                            }
                        }
                    }
                }
            }
            foreach (var s in feesCode)
            {
                sQuery = $"delete from bill_tmp where arn = '{m_sAN}' and fees_code = '{s}' and fees_category = 'OTHERS'";
                db.Database.ExecuteSqlCommand(sQuery);
                remove = true;
            }

            dgvPermit.Refresh();
            if (remove == true)
            {
                for (int i = 0; i < dgvAssessment.Rows.Count; i++)
                {
                    dgvAssessment[0, i].Value = false;
                    dgvAssessment[7, i].Value = "0";
                    dgvAssessment[8, i].Value = "0";
                    dgvAssessment[11, i].Value = "0";

                }

                for (int i = 0; i < dgvAddOnFees.Rows.Count; i++)
                {
                    dgvAddOnFees[0, i].Value = false;
                    dgvAddOnFees[7, i].Value = "0";
                    dgvAddOnFees[8, i].Value = "0";
                    dgvAddOnFees[11, i].Value = "0";

                }

                for (int i = 0; i < dgvOtherFees.Rows.Count; i++)
                {
                    dgvOtherFees[0, i].Value = false;
                    dgvOtherFees[7, i].Value = "0";
                    dgvOtherFees[8, i].Value = "0";
                    dgvOtherFees[11, i].Value = "0";

                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (btnCancel.Text == "Exit")
                this.Close();
            else
            {
                if (MessageBox.Show("Are you sure you want to cancel transaction?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    RecordClass.ClearControls();
                    taskman.RemTask(m_sAN);
                    an1.GetCode = "";
                    //an1.GetLGUCode = "";
                    an1.GetTaxYear = "";
                    //an1.GetMonth = ""; // disabled for new arn of binan
                    //an1.GetDistCode = "";
                    an1.GetSeries = "";
                    m_sAN = "";
                    this.Close();
                }
                else
                    return;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

            if (CheckApproval())
            {
                MessageBox.Show($"AN: '{m_sAN}' is subject for approval", m_sModule, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            frmReport form = new frmReport();
            form.ReportName = "ORDER OF PAYMENT";
            form.An = m_sAN;
            form.ShowDialog();
        }

        private bool CheckApproval()
        {
            OracleResultSet res = new OracleResultSet();
            string sStatus = string.Empty;
            res.Query = $"select status from application_approval where arn = '{m_sAN}'";
            if (res.Execute())
                if (res.Read())
                {
                    sStatus = res.GetString("status");
                    if (sStatus == "PENDING")
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            else
                return false;
        }

        private void btnAddlAdd_Click(object sender, EventArgs e)
        {
            RecordClass.AdditionalFeesAdd(sender, e);
        }

        private void txtAddlAmt_Leave(object sender, EventArgs e)
        {
            double dAmt = 0;
            double.TryParse(txtAddlAmt.Text.ToString(), out dAmt);

            txtAddlAmt.Text = string.Format("{0:#,###.00}", dAmt);
        }

        private void dgvAssessment_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnImgView_Click(object sender, EventArgs e) // view image
        {
            m_intImageListInstance = 0;
            m_frmImageList = new frmImageList();
            m_frmImageList.IsBuildUpPosting = true;

            if (m_frmImageList.ValidateImage(m_sAN, AppSettingsManager.GetSystemType)) //MCR 20141209
            {
                ImageInfo objImageInfo;
                objImageInfo = new ImageInfo();

                objImageInfo.TRN = m_sAN;
                //objImageInfo.System = "A"; 
                objImageInfo.System = AppSettingsManager.GetSystemType; //MCR 20121209
                m_frmImageList.isFortagging = false;
                m_frmImageList.setImageInfo(objImageInfo);
                m_frmImageList.Text = m_sAN;
                m_frmImageList.IsAutoDisplay = true;
                m_frmImageList.Source = "VIEW";
                m_frmImageList.Show();
                m_intImageListInstance += 1;
            }
            else
            {

                MessageBox.Show(string.Format("ARN {0} has no image", m_sAN));
            }

        }

        private void cmbPermit_SelectedIndexChanged(object sender, EventArgs e) //AFM 20201104 REQUESTED BY BINAN AND RJ 20201103 - NEW BILLING DESIGN AND FLOW
        {
            PermitList permit = new PermitList(null);
            string sPermitAcro = string.Empty;

            this.btnClear.PerformClick();
            PermitCode = string.Empty;
            Source = cmbPermit.Text;
            PermitCode = permit.GetPermitCode(Source);
            EnableControls(true);

            sPermitAcro = this.an1.ANCodeGenerator(PermitCode);
            if (AppSettingsManager.GetConfigValue("30") == "1")
                this.an1.SetAn(sPermitAcro);
            else
                this.an1.SetAn("AN"); //AFM 20201204 REQUESTED BY BINAN AS PER MITCH - FIXED "AN" FOR NEW APPLICATION
        }

        private void dgvAddOn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            RecordClass.CellClickAddOnCELL(sender, e);
        }

        private void btnAddOnAmt_Click(object sender, EventArgs e)
        {
            RecordClass.AdditionalFeesAddAddOn(sender, e);
        }

        private void dgvOtherFees_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            RecordClass.CellClickOthersCELL(sender, e);
        }

        private void dgvAddOn_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            RecordClass.CellClickAddOn(sender, e);
        }

        private void dgvOtherFees_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            RecordClass.CellClickOthers(sender, e);
        }

        private void btnOtherAdd_Click(object sender, EventArgs e)
        {
            RecordClass.OtherFeesAddAddOn(sender, e);
        }
      
        private void txtAddOnAmt_KeyUp(object sender, KeyEventArgs e) //requested by binan - override will trigger when pressed "ctrl+F"
        {
            if (e.KeyCode == Keys.F && CtrlPressed == true)
            {
                btnAddOnAmt.Visible = true;
                RecordClass.AdditionalFeesAddAddOn(sender, e);             
            }
            CtrlPressed = false;
        }

        private void txtAddOnAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true)
            {
                CtrlPressed = true;
            }
        }

        private void txtOtherAmt_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F && CtrlPressed == true)
            {
                btnAddOnAmt.Visible = true;
                RecordClass.OtherFeesAddAddOn(sender, e);
            }
            CtrlPressed = false;
        }

        private void txtOtherAmt_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Control == true)
            {
                CtrlPressed = true;
            }
        }

        private void lblAddOn_Click(object sender, EventArgs e)
        {
            if(isActive == false)
            {
                this.Size = new Size(1009, 746);

                this.grpAddOn.Size = new Size(975, 162);
                this.grpOther.Location = new Point(10, 490);
                this.lblTotAmtDue.Location = new Point(744, 649);
                this.txtAllTotAmtDue.Location = new Point(856, 646);
                this.btnSave.Location = new Point(715, 677);
                this.btnPrint.Location = new Point(806, 677);
                this.btnCancel.Location = new Point(897, 677);
                this.btnImgView.Location = new Point(623, 677);
                this.btnRequirements.Location = new Point(10, 677);
                isActive = true;
            }
            else
            {
                this.Size = new Size(1009, 605);
                this.grpAddOn.Size = new Size(975, 13);
                this.grpOther.Location = new Point(10, 348);
                this.lblTotAmtDue.Location = new Point(744, 507);
                this.txtAllTotAmtDue.Location = new Point(856, 504);
                this.btnSave.Location = new Point(715, 533);
                this.btnPrint.Location = new Point(806, 533);
                this.btnCancel.Location = new Point(897, 533);
                this.btnImgView.Location = new Point(623, 533);
                this.btnRequirements.Location = new Point(10, 533);
                isActive = false;

            }


            //OracleResultSet res = new OracleResultSet();
            //res.Query = $"DELETE FROM BILL_ADDL_TEMP WHERE ARN = '{m_sAN}'";
            //if(res.ExecuteNonQuery() == 0)
            //{ }
            //foreach(DataGridViewRow row in dgvAddOnFees.Rows)
            //{
            //    int index = row.Index;
            //    string sChecked = string.Empty;
            //    if (Convert.ToBoolean(dgvAddOnFees[0, index].Value) == false)
            //        sChecked = "N";
            //    else
            //        sChecked = "Y";
            //    res.Query = "INSERT INTO BILL_ADDL_TEMP VALUES(";
            //    res.Query += $"'{m_sAN}', ";
            //    res.Query += $"'{PermitCode}', ";
            //    res.Query += $"'{txtBillNo.Text.Trim()}', ";
            //    res.Query += $"'{sChecked}', ";
            //    res.Query += $"'{dgvAddOnFees[1, index].Value}', ";
            //    res.Query += $"'{dgvAddOnFees[2, index].Value}', ";
            //    res.Query += $"'{dgvAddOnFees[3, index].Value}', ";
            //    res.Query += $"'{dgvAddOnFees[4, index].Value}', ";
            //    res.Query += $"'{dgvAddOnFees[5, index].Value}', ";
            //    res.Query += $"'{dgvAddOnFees[6, index].Value}', ";
            //    res.Query += $"'{dgvAddOnFees[7, index].Value}', ";
            //    res.Query += $"'{dgvAddOnFees[8, index].Value}', ";
            //    res.Query += $"'{dgvAddOnFees[9, index].Value}', ";
            //    res.Query += $"'{dgvAddOnFees[10, index].Value}', ";
            //    res.Query += $"'{dgvAddOnFees[11, index].Value}') ";
            //    if (res.ExecuteNonQuery() == 0)
            //    { }
            //    res.Close();
            //}

            //RecordClass = new Building(this);

            //frmAddOn form = new frmAddOn();
            //form.m_sAN = m_sAN;
            //form.m_sBillNo = txtBillNo.Text;
            //form.ShowDialog();
        }

        private void btnRequirements_Click(object sender, EventArgs e) //AFM 20210129 requested during training of binan 2020
        {
            if(!string.IsNullOrEmpty(m_sAN))
            {
                frmRequirementsList frmrequirementslist = new frmRequirementsList();
                frmrequirementslist.ARN = m_sAN;
                frmrequirementslist.ViewMode = true;
                frmrequirementslist.Permit = cmbPermit.Text.Trim();
                frmrequirementslist.ShowDialog();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e) //AFM 20210309
        {
            if(btnEdit.Text == "Edit")
            {
                btnEdit.Text = "Update";
                BldgInfoControls(true);
            }
            else
            {
                btnEdit.Text = "Edit";
                BldgInfoControls(false);
                SaveBldgInfo();
                RecordClass.DisplayData();
                RecordClass.ComputeALL();

            }
        }

        private void SaveBldgInfo()
        {
            OracleResultSet res = new OracleResultSet();
            CategoryList category = new CategoryList();
            StructureList structure = new StructureList();
            string sCatCode = string.Empty;
            string sStrucCode = string.Empty;
            string sOccCode = string.Empty;

            double dHeight = 0;
            double dFlrArea = 0;
            double dEstCost = 0;
            double dUnits = 0;

            double.TryParse(txtHeight.Text.Trim(), out dHeight);
            double.TryParse(txtFlrArea.Text.Trim(), out dFlrArea);
            double.TryParse(txtBldgCost.Text.Trim(), out dEstCost);
            double.TryParse(txtUnits.Text.Trim(), out dUnits);

            sCatCode = category.GetCategoryCode(cmbCategory.Text);
            sStrucCode = structure.GetStructureCode(cmbStructure.Text);
            OccupancyList occupancy = new OccupancyList(cmbCategory.Text, "");
            sOccCode = occupancy.GetOccupancyCode(cmbOccupancy.Text);


            res.Query = $"UPDATE APPLICATION_QUE SET CATEGORY_CODE = '{sCatCode}', STRUC_CODE = '{sStrucCode}', OCCUPANCY_CODE = '{sOccCode}' where arn = '{m_sAN}'";
            if(res.ExecuteNonQuery() == 0)
            { }
            res.Close();

            res.Query = $"UPDATE BUILDING SET BLDG_HEIGHT = {dHeight}, TOTAL_FLR_AREA = {dFlrArea}, EST_COST = {dEstCost}, NO_UNITS = '{dUnits}' where bldg_no in (select bldg_no from application_que where arn = '{m_sAN}')"; //AFM 20220210 added units - adjustments binan meeting 20220209
            if (res.ExecuteNonQuery() == 0)
            { }
            res.Close();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void txtStatus_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbOccupancy.Text = string.Empty;
            cmbOccupancy.Items.Clear();

            OracleResultSet res = new OracleResultSet();
            CategoryList category = new CategoryList();
            string sCatCode = category.GetCategoryCode(cmbCategory.Text);
            res.Query = $"select * from occupancy_tbl where category_code = '{sCatCode}' order by occ_code";
            if(res.Execute())
                while(res.Read())
                {
                    cmbOccupancy.Items.Add(res.GetString("occ_desc"));
                }
            res.Close();
        }

        private void txtFlrArea_Leave(object sender, EventArgs e)
        {
            txtFlrArea.Text = string.Format("{0:#,###.00}", Convert.ToDouble(txtFlrArea.Text.Trim()));
        }

        private void txtHeight_Leave(object sender, EventArgs e)
        {
            txtHeight.Text = string.Format("{0:#,###.00}", Convert.ToDouble(txtHeight.Text.Trim()));
        }

        private void txtBldgCost_Leave(object sender, EventArgs e)
        {
            txtBldgCost.Text = string.Format("{0:#,###.00}", Convert.ToDouble(txtBldgCost.Text.Trim()));
        }

        private void btnCancelEdit_Click(object sender, EventArgs e)
        {
            BldgInfoControls(false);
            btnEdit.Text = "Edit";
        }

        private void BldgInfoControls(bool bln)
        {
            btnCancelEdit.Enabled = bln;

            cmbCategory.Enabled = bln;
            cmbOccupancy.Enabled = bln;
            cmbStructure.Enabled = bln;
            txtFlrArea.Enabled = bln;
            txtHeight.Enabled = bln;
            txtBldgCost.Enabled = bln;
            txtUnits.Enabled = bln;

            dgvPermit.Enabled = !bln;
            dgvAssessment.Enabled = !bln;
            dgvAddOnFees.Enabled = !bln;
            dgvOtherFees.Enabled = !bln;
            btnSave.Enabled = !bln;
            btnPrint.Enabled = !bln;
        }

        private void SaveLM()
        {
            OracleResultSet res = new OracleResultSet();
            double dFront = 0;
            double dLeft = 0;
            double dRight = 0;
            double dRear = 0;
            double dTotal = 0;

            double.TryParse(txtFront.Text, out dFront);
            double.TryParse(txtLeft.Text, out dLeft);
            double.TryParse(txtRight.Text, out dRight);
            double.TryParse(txtRear.Text, out dRear);
            double.TryParse(txtElecTotal.Text, out dTotal);

            int iCnt = 0;
            res.Query = $"select count(*) from bill_lm_tmp where arn = '{m_sAN}'";
            int.TryParse(res.ExecuteScalar(), out iCnt);
            if(iCnt > 0)
            {
                res.Query = $"UPDATE BILL_LM_TMP SET FRONT_VAL = {dFront}, LEFT_VAL = {dLeft}, RIGHT_VAL = {dRight}, REAR_VAL = {dRear}, TOTAL = {dTotal} WHERE ARN = '{m_sAN}'";
                if(res.ExecuteNonQuery() == 0)
                { }
            }
            else
            {
                res.Query = $"INSERT INTO BILL_LM_TMP VALUES ('{m_sAN}',{dFront},{dLeft},{dRight},{dRear}, {dTotal})";
                if (res.ExecuteNonQuery() == 0)
                { }
            }
        }

        private void txtFront_Leave(object sender, EventArgs e)
        {
            try
            {
                txtFront.Text = string.Format("{0:#,##0.00}", Convert.ToDouble(txtFront.Text.Trim()));
            }
            catch
            {
                txtFront.Text = "0.00";
            }

            double dFront = 0;
            double dLeft = 0;
            double dRight = 0;
            double dRear = 0;

            double.TryParse(txtFront.Text.Trim(), out dFront);
            double.TryParse(txtLeft.Text.Trim(), out dLeft);
            double.TryParse(txtRight.Text.Trim(), out dRight);
            double.TryParse(txtRear.Text.Trim(), out dRear);

            txtElecTotal.Text = string.Format("{0:#,##0.00}", dFront + dLeft + dRight + dRear);
        }

        private void txtLeft_Leave(object sender, EventArgs e)
        {
            try
            {
                txtLeft.Text = string.Format("{0:#,##0.00}", Convert.ToDouble(txtLeft.Text.Trim()));
            }
            catch
            {
                txtLeft.Text = "0.00";
            }

            double dFront = 0;
            double dLeft = 0;
            double dRight = 0;
            double dRear = 0;

            double.TryParse(txtFront.Text.Trim(), out dFront);
            double.TryParse(txtLeft.Text.Trim(), out dLeft);
            double.TryParse(txtRight.Text.Trim(), out dRight);
            double.TryParse(txtRear.Text.Trim(), out dRear);

            txtElecTotal.Text = string.Format("{0:#,##0.00}", dFront + dLeft + dRight + dRear);
        }

        private void txtRight_Leave(object sender, EventArgs e)
        {
            try
            {
                txtRight.Text = string.Format("{0:#,##0.00}", Convert.ToDouble(txtRight.Text.Trim()));
            }
            catch
            {
                txtRight.Text = "0.00";
            }

            double dFront = 0;
            double dLeft = 0;
            double dRight = 0;
            double dRear = 0;

            double.TryParse(txtFront.Text.Trim(), out dFront);
            double.TryParse(txtLeft.Text.Trim(), out dLeft);
            double.TryParse(txtRight.Text.Trim(), out dRight);
            double.TryParse(txtRear.Text.Trim(), out dRear);

            txtElecTotal.Text = string.Format("{0:#,##0.00}", dFront + dLeft + dRight + dRear);
        }

        private void txtRear_Leave(object sender, EventArgs e)
        {
            try
            {
                txtRear.Text = string.Format("{0:#,##0.00}", Convert.ToDouble(txtRear.Text.Trim()));
            }
            catch
            {
                txtRear.Text = "0.00";
            }

            double dFront = 0;
            double dLeft = 0;
            double dRight = 0;
            double dRear = 0;

            double.TryParse(txtFront.Text.Trim(), out dFront);
            double.TryParse(txtLeft.Text.Trim(), out dLeft);
            double.TryParse(txtRight.Text.Trim(), out dRight);
            double.TryParse(txtRear.Text.Trim(), out dRear);

            txtElecTotal.Text = string.Format("{0:#,##0.00}", dFront + dLeft + dRight + dRear);
        }

        private void dgvAssessment_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            RecordClass.CellEndEdit(sender, e);
            if (dgvAssessment.CurrentCell.ColumnIndex == 7)
                RecordClass.ComputeCell();
        }

        private void txtElecTotal_TextChanged(object sender, EventArgs e)
        {
            RecordClass.ComputeALL();
            if (txtFront.Enabled == true)
                SaveLM();
        }

        private void dgvPermit_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            m_sPrevPermit = dgvPermit[2, e.RowIndex].Value.ToString();
        }

        private void dgvOtherFees_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (m_sCurrPermit == m_sPrevPermit)
                RecordClass.CellLeaveOther(sender, e);
        }

        private void dgvOtherFees_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            RecordClass.CellEndEditOther(sender, e);
            if (dgvOtherFees.CurrentCell.ColumnIndex == 7)
                RecordClass.ComputeCellOther();
        }

        private void dgvAddOnFees_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            RecordClass.CellEndEditAdditional(sender, e);
        }

        private void txtUnits_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
    }
}
