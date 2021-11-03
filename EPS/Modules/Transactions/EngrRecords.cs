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
using Common.DataConnector;
using Amellar.Common.ImageViewer;

namespace Modules.Transactions
{
	public class EngrRecords : RecordForm
	{
		TaskManager taskman = new TaskManager();

        protected frmImageList m_frmImageList;
        protected frmImageViewer m_frmImageViewer;
        public static int m_intImageListInstance;
        public string m_sFormStatus = string.Empty;

        private bool m_isImgOpen;
        private bool m_isNotSaved = false;

        public bool isImgOpen
        {
            get { return m_isImgOpen; }
            set { m_isImgOpen = value; }
        }


        public static ConnectionString dbConn = new ConnectionString();
		public EngrRecords(frmRecords Form) : base(Form)
		{ }

		public override void FormLoad()
		{
			RecordFrm.Text = "Engineering Records";

			RecordFrm.EnableControl(false);

            // blob
            m_intImageListInstance = 0;
            m_frmImageViewer = new frmImageViewer();
            m_frmImageList = new frmImageList();
            m_frmImageList.IsBuildUpPosting = true;
            // blob

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
                    InitEdit(true); //AFM 20191024 ANG-19-11168
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
					//RecordFrm.arn1.GetLGUCode = "";
					RecordFrm.arn1.GetTaxYear = "";
                    //RecordFrm.arn1.GetMonth = ""; // disabled for new arn of binan
                    //RecordFrm.arn1.GetDistCode = "";
                    RecordFrm.arn1.GetSeries = "";

                    //RecordFrm.ButtonImgAttach.Enabled = true;
                    m_frmImageList = new frmImageList();
                    m_sFormStatus = RecordFrm.SourceClass;
                    LoadImageList(); //image loader
                }
				else
				{
					if (!ValidateData())
						return;
					if (!RecordFrm.ValidateData())
						return;

                    Save();
                    if(m_isNotSaved == false)
                    {
                        MessageBox.Show("Record " + RecordFrm.AN + " is saved", RecordFrm.DialogText, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        SaveImage(); // save attached image
                        RecordFrm.arn1.Enabled = true;
                        RecordFrm.EnableControl(false);
                        RecordFrm.ButtonEdit.Enabled = true;
                        RecordFrm.ButtonDelete.Enabled = true;
                        RecordFrm.ButtonPrint.Enabled = true;
                        RecordFrm.ButtonClear.Enabled = true;
                        RecordFrm.ButtonSearch.Enabled = true;
                        ClearControl();
                    }
                    else
                        MessageBox.Show("Record " + RecordFrm.AN + "is not saved", RecordFrm.DialogText, MessageBoxButtons.OK, MessageBoxIcon.Information);


                }
			}
		}

        public override void ButtonImgView()
        {
            {
            // RMC 20111206 Added viewing of blob

            if (m_frmImageList.IsDisposed)
            {
                m_intImageListInstance = 0;
                m_frmImageList = new frmImageList();
                m_frmImageList.IsBuildUpPosting = true;
            }
            if (!m_frmImageList.IsDisposed && m_intImageListInstance == 0)
            {
                //if (m_frmImageList.ValidateImage(bin1.GetBin(), "A"))
                if (m_frmImageList.ValidateImage(RecordFrm.arn1.GetAn(), AppSettingsManager.GetSystemType)) //MCR 20141209
                {
                    ImageInfo objImageInfo;
                    objImageInfo = new ImageInfo();

                    objImageInfo.TRN = RecordFrm.arn1.GetAn();
                    //objImageInfo.System = "A"; 
                    objImageInfo.System = AppSettingsManager.GetSystemType; //MCR 20121209
                    m_frmImageList.isFortagging = false;
                    m_frmImageList.setImageInfo(objImageInfo);
                    m_frmImageList.Text = RecordFrm.arn1.GetAn();
                    m_frmImageList.IsAutoDisplay = true;
                    m_frmImageList.Source = "VIEW";
                    m_frmImageList.Show();
                    m_intImageListInstance += 1;
                }
                else
                {

                    MessageBox.Show(string.Format("ARN {0} has no image", RecordFrm.arn1.GetAn()));
                }

            }
        }
        }

        public override void ButtonImgAttach()
        {
            if (m_isImgOpen == false)
            {
                m_intImageListInstance = 0;
                m_frmImageList = new frmImageList();
                m_frmImageList.IsBuildUpPosting = true;
                m_sFormStatus = RecordFrm.SourceClass;
                LoadImageList(); //image loader
            }
        }

        public override void ButtonImgDetach()
        {
            if (m_frmImageList.GetRecentImageID == 0)
            {
                MessageBox.Show("View image to detach first.", "Business Records", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (MessageBox.Show("Do you want to detach file : " + m_frmImageList.GetRecentImageFileNameDisplay + " from AN: " + RecordFrm.arn1.GetAn() + ".", "Business Records", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (m_frmImageList.GetRecentImageID != 0)
                {
                    string strImageFile = m_frmImageList.GetRecentImageFileNameDisplay;
                    if (strImageFile != null && strImageFile != "")
                    {
                        ImageTransation objTransaction = new ImageTransation();
                        objTransaction.FileName = strImageFile; // AST 20150430
                        if (!(objTransaction.DetachImage(RecordFrm.arn1.GetAn(), m_frmImageList.GetRecentImageID)))
                        {
                            MessageBox.Show("Failed to detach image.", "Business Records", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }
                        else
                        {

                            m_frmImageList.Close();
                            MessageBox.Show("Image detached", "Business Records", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }


                    }
                    else
                        MessageBox.Show("View image to detach first.", "Business Records", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    MessageBox.Show("View image to detach first.", "Business Records", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }


    private void SaveImage()
        {

            if (m_frmImageList.GetRecentImageID != 0)   // image already in database (used in build-up)
            {
                string strImageFile = m_frmImageList.GetRecentImageFileNameDisplay;
                if (strImageFile != null && strImageFile != "")
                {
                    ImageInfo objImageInfo;
                    //objImageInfo = new ImageInfo(bin1.GetBin(), "A", AppSettingsManager.SystemUser.UserCode);   // RMC 20120119 corrected saving of image in Business records
                    objImageInfo = new ImageInfo(RecordFrm.arn1.GetAn(), AppSettingsManager.GetSystemType, AppSettingsManager.SystemUser.UserCode);   // MCR 20141209

                    if (!m_frmImageList.UpdateBlobImage(objImageInfo))
                    {
                        //pSet.Rollback();
                        //pSet.Close();
                        MessageBox.Show("Failed to attach image.", "Business Records", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                }
                else
                {
                    //pSet.Rollback();
                    //pSet.Close();
                    MessageBox.Show("Failed to attach image.", "Business Records", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

            }
            else
            {
                // for browsed local file to be inserted in database
                string strImageFile = m_frmImageList.GetRecentImageFileNameDisplay;
                string strImageName = System.IO.Path.GetFileName(strImageFile);

                if (strImageFile != "")
                {
                    if (MessageBox.Show(string.Format("Do you want to attach the image {0} to ARN {1}", strImageName, RecordFrm.arn1.GetAn()), "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        ImageTransation objTransaction = new ImageTransation();

                        if (!(objTransaction.InsertImage(RecordFrm.arn1.GetAn(), strImageFile, RecordFrm.Brgy.Trim())))
                        {
                            MessageBox.Show("Failed to attach image.", "Business Records", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }
                        else
                        {

                            m_frmImageList.Close();
                            MessageBox.Show("Image attached.", "Business Records", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }

        }


        private void LoadImageList()
        {
            // RMC 20111206 Added attachment of blob image

            if (m_frmImageList.IsDisposed)
            {
                m_intImageListInstance = 0;
                m_frmImageList = new frmImageList();
                m_frmImageList.IsBuildUpPosting = true;
            }
            if (!m_frmImageList.IsDisposed && m_intImageListInstance == 0)
            {
                ImageInfo objImageInfo;
                //objImageInfo = new ImageInfo("A", AppSettingsManager.SystemUser.UserCode);  // RMC 20111206
                objImageInfo = new ImageInfo(AppSettingsManager.GetSystemType, AppSettingsManager.SystemUser.UserCode);  // MCR 20141209

                //JVL20100107(s)
                /*if (state == PostingState.Add)
                {
                    objImageInfo = new ImageInfo("A", AppSettingsManager.SystemUser.UserCode);
                }
                else if (state == PostingState.Edit)
                {
                    //objImageInfo = new ImageInfo();
                    //objImageInfo.System = "T";
                    objImageInfo = new ImageInfo("A", AppSettingsManager.SystemUser.UserCode);
                    m_blnIsEditAttach = true;
                }
                else //modify this condition if you need to add different scenario for posting delete or posting view
                {
                    objImageInfo = new ImageInfo("A", AppSettingsManager.SystemUser.UserCode);
                }
                //JVL20100107(e)*/
                // RMC 20111206 put rem

                m_frmImageList.Text = string.Format("Assigned Images - {0}", AppSettingsManager.SystemUser.UserCode);
                m_frmImageList.setImageInfo(objImageInfo);
                m_frmImageList.isFortagging = true;

                //m_frmImageList.TopMost = true;
                //m_frmImageList.IsBuildUp = true;
                // RMC 20111206 Added attachment of blob image (s)
                if (m_sFormStatus == "ENG_REC_ADD")
                    m_frmImageList.IsBuildUp = true;
                else
                    m_frmImageList.IsBuildUp = false;
                m_frmImageList.Source = "ATTACH";
                // RMC 20111206 Added attachment of blob image (e)

                m_frmImageList.IsAutoDisplay = true;
                //m_frmImageList.Show(this.ApplicationFrm);
                m_frmImageList.TopMost = true; // CJC 20130401
                //m_frmImageList.Show();
                m_frmImageList.Show();
                m_intImageListInstance += 1;
            }
            else
            {
                // AST 20150316 Added This Block (s)
                ImageInfo objImageInfo;
                objImageInfo = new ImageInfo(AppSettingsManager.GetSystemType, AppSettingsManager.SystemUser.UserCode);  // MCR 20141209
                m_frmImageList.Text = string.Format("Assigned Images - {0}", AppSettingsManager.SystemUser.UserCode);
                m_frmImageList.setImageInfo(objImageInfo);
                m_frmImageList.isFortagging = true;

                if (m_sFormStatus == "BUSS-ADD-NEW" || m_sFormStatus == "BUSS-EDIT")
                    m_frmImageList.IsBuildUp = true;
                else
                    m_frmImageList.IsBuildUp = false;
                m_frmImageList.Source = "ATTACH";

                m_frmImageList.IsAutoDisplay = true;
                m_frmImageList.TopMost = true;
                m_frmImageList.ShowDialog();
                m_intImageListInstance += 1;
                // AST 20150316 Added This Block (e)
            }
        }


        public override void Save()
		{
			var db = new EPSConnection(dbConn);
			string strQuery = string.Empty;
			string sYear = AppSettingsManager.GetCurrentDate().Year.ToString();
			string sBrgyCode = frmProjectInfo.BrgyCode;
            m_isNotSaved = false;

            if (string.IsNullOrEmpty(RecordFrm.AN))
				RecordFrm.arn1.CreateAN(RecordFrm.cmbPermit.Text);

			strQuery = $"delete from application where arn = '{RecordFrm.AN}'";
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
                    //if (sPermit.Contains("BUILDING"))
                    //	iMainApp = 1;
                    if (sPermit == RecordFrm.cmbPermit.Text)
                    	iMainApp = 1;

                    string sStruc = string.Empty;
                    string sScope = string.Empty;
                    string sCat = string.Empty;
                    string sOccu = string.Empty;
                    string sBrgy = string.Empty;

                    try { sStruc = ((DataRowView)RecordFrm.formProject.cmbStrucType.SelectedItem)["Code"].ToString();} catch{sStruc = "";}
                    try { sScope = ((DataRowView)RecordFrm.cmbScope.SelectedItem)["ScopeCode"].ToString(); } catch{ sScope = ""; }
                    try { sCat = ((DataRowView)RecordFrm.formProject.cmbCategory.SelectedItem)["Code"].ToString(); } catch{ sCat = ""; }
                    try { sOccu = ((DataRowView)RecordFrm.formProject.cmbOccupancy.SelectedItem)["Code"].ToString();} catch{ sOccu = ""; }
                    try { sBrgy = ((DataRowView)RecordFrm.formProject.cmbBrgy.SelectedItem)["Desc"]?.ToString(); } catch{ sBrgy = ""; }
                    if (string.IsNullOrEmpty(sBrgy))
                    {
                        sBrgy = RecordFrm.formProject.cmbBrgy.Text.Trim().ToUpper();
                    }

                    DateTime dtStart;
                    DateTime dtCompleted;
                    string sdTApplied = string.Empty;

                    // convert properly to prevent error
                    try
                    {
                        dtStart = Convert.ToDateTime(RecordFrm.formBldgDate.dgvList[3, i].Value);
                        sStart = dtStart.ToShortDateString();
                    }
                    catch { }

                    try
                    {
                        dtCompleted = Convert.ToDateTime(RecordFrm.formBldgDate.dgvList[3, i].Value);
                        sCompleted = dtCompleted.ToShortDateString();
                    }
                    catch { }

                    try
                    {
                        sdTApplied = RecordFrm.dtDateApplied.Value.ToShortDateString();
                    }
                    catch { }

                    string dtApproved = RecordFrm.dtpDateApproved.Value.ToShortDateString();
                    try
					{
						strQuery = $"insert into application values (:1,:2,:3,:4,:5,:6,:7,:8,:9,:10,:11,:12,:13,:14,:15,:16,:17,:18,:19,:20,:21,:22,:23,:24,to_date(:25,'MM/dd/yyyy'),to_date(:26,'MM/dd/yyyy'),to_date(:27,'MM/dd/yyyy'),to_date(:28,'MM/dd/yyyy'),:29,:30,:31)";
						db.Database.ExecuteSqlCommand(strQuery,
							new OracleParameter(":1", RecordFrm.AN),
							new OracleParameter(":2", RecordFrm.formProject.txtProjDesc.Text.Trim()),
							new OracleParameter(":3", sPermitCode),
							new OracleParameter(":4", sPermitNo),
							new OracleParameter(":5", sStruc),
							new OracleParameter(":6", RecordFrm.formBldgDate.txtBldgNo.Text.Trim()),
							new OracleParameter(":7", RecordFrm.Status),
							new OracleParameter(":8", sScope),
							new OracleParameter(":9", sCat),
							new OracleParameter(":10", sOccu),
							new OracleParameter(":11", ""), //bns code
                            new OracleParameter(":12", RecordFrm.formProject.txtHseNo.Text.Trim()),
							new OracleParameter(":13", RecordFrm.formProject.txtLotNo.Text.Trim()),
							new OracleParameter(":14", RecordFrm.formProject.txtBlkNo.Text.Trim()),
							new OracleParameter(":15", RecordFrm.formProject.txtStreet.Text.Trim()),
							new OracleParameter(":16", sBrgy),
							new OracleParameter(":17", RecordFrm.formProject.txtMun.Text.Trim()),
							new OracleParameter(":18", RecordFrm.formProject.txtProv.Text.Trim()),
							new OracleParameter(":19", RecordFrm.formProject.txtZIP.Text.Trim()),
							new OracleParameter(":20", RecordFrm.formStrucOwn.StrucAcctNo),
							new OracleParameter(":21", RecordFrm.formLotOwn.LotAcctNo),
							new OracleParameter(":22", RecordFrm.formProject.cmbOwnership.Text.ToString()),
							new OracleParameter(":23", m_sEngr),
							new OracleParameter(":24", m_sArch),
							//new OracleParameter(":25", string.Format("{0:MM/dd/yyyy}", sStart)),
							new OracleParameter(":25", sStart), 
                            //new OracleParameter(":26", string.Format("{0:MM/dd/yyyy}", sCompleted)),
                            new OracleParameter(":26", sCompleted),
                            new OracleParameter(":27", sdTApplied),
							new OracleParameter(":28", dtApproved),
							new OracleParameter(":29", RecordFrm.formProject.txtMemo.Text.ToString()),
							new OracleParameter(":30", iMainApp),
                            new OracleParameter(":31", RecordFrm.formProject.txtVillage.Text.ToString().Trim())); //ADDED REQUESTED subdivision


                    }
                    catch (Exception ex) // catches any error
					{
						MessageBox.Show(ex.Message.ToString());
                        m_isNotSaved = true;
					}
				}
			}

            if(m_isNotSaved == false)
            {
                SaveBuilding();

                //???strQuery = $"UPDATE APPLICATION SET STATUS_CODE = '{RecordFrm.cmbScope.Text.ToString()}' WHERE ARN = '{RecordFrm.ARN}'";
                //db.Database.ExecuteSqlCommand(strQuery);

                if (RecordFrm.SourceClass == "ENG_REC_ADD")
                {
                    if (Utilities.AuditTrail.InsertTrail("ER-A", "APPLICATION", "ARN: " + RecordFrm.AN) == 0)
                    {
                        MessageBox.Show("Failed to insert audit trail.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (RecordFrm.SourceClass == "ENG_REC_EDIT")
                {
                    if (Utilities.AuditTrail.InsertTrail("ER-E", "APPLICATION", "ARN: " + RecordFrm.AN) == 0)
                    {
                        MessageBox.Show("Failed to insert audit trail.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                //ClearControl();
            }

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

            RecordFrm.ButtonImgView.Enabled = false;
            RecordFrm.ButtonImgAttach.Enabled = false;
            RecordFrm.ButtonImgDetach.Enabled = false;

            RecordFrm.EnableControl(false);
			RecordFrm.arn1.Enabled = true;
            //RecordFrm.arn1.GetCode = "";
            //RecordFrm.arn1.GetLGUCode = "";
            //RecordFrm.arn1.GetTaxYear = "";
            //RecordFrm.arn1.GetMonth = "";  // disabled for new arn of binan
            //RecordFrm.arn1.GetDistCode = "";
            RecordFrm.arn1.GetSeries = "";
			RecordFrm.formProject.ClearControls();
			RecordFrm.formBldgDate.ClearControls();
			RecordFrm.formStrucOwn.ClearControls();
			RecordFrm.formLotOwn.ClearControls();
			RecordFrm.formEngr.ClearControls();
            RecordFrm.formEngr.ClearDgView();

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
				strQuery = $"select count(*) from (select arn from application where arn = '{RecordFrm.AN}' union ";
				strQuery += $"select arn from application_que where arn = '{RecordFrm.AN}')";
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
			//if (string.IsNullOrEmpty(RecordFrm.arn1.GetTaxYear) && string.IsNullOrEmpty(RecordFrm.arn1.GetSeries))
			//if (string.IsNullOrEmpty(RecordFrm.arn1.GetMonth) || string.IsNullOrEmpty(RecordFrm.arn1.GetSeries)) //AFM 20200702 // disabled for new arn of binan
			if (string.IsNullOrEmpty(RecordFrm.arn1.GetTaxYear) || string.IsNullOrEmpty(RecordFrm.arn1.GetSeries))
                {
				SearchAccount.frmSearchARN form = new SearchAccount.frmSearchARN();

				form.SearchCriteria = "APP";
				form.ShowDialog();

				RecordFrm.arn1.SetAn(form.sArn);
			}

			if (!taskman.AddTask(RecordFrm.SourceClass, RecordFrm.AN))
				return;

			DisplayData();

            RecordFrm.tabControl1.Enabled = true; //false
            InitEdit(false);

		}

        public override void InitEdit(bool blnEnable) // AFM 20191024 ANG-19-11168
        {
            RecordFrm.formProject.txtProjDesc.Enabled = blnEnable;
            RecordFrm.formProject.txtMemo.Enabled = blnEnable;
            RecordFrm.formProject.cmbCategory.Enabled = blnEnable;
            RecordFrm.formProject.cmbOccupancy.Enabled = blnEnable;
            RecordFrm.formProject.cmbBussKind.Enabled = blnEnable;
            RecordFrm.formProject.cmbOwnership.Enabled = blnEnable;
            RecordFrm.formProject.cmbStrucType.Enabled = blnEnable;
            RecordFrm.formProject.txtHseNo.Enabled = blnEnable;
            RecordFrm.formProject.txtLotNo.Enabled = blnEnable;
            RecordFrm.formProject.txtStreet.Enabled = blnEnable;
            RecordFrm.formProject.cmbBrgy.Enabled = blnEnable;
            RecordFrm.formProject.txtMun.Enabled = blnEnable;
            RecordFrm.formProject.txtProv.Enabled = blnEnable;
            RecordFrm.formProject.txtZIP.Enabled = blnEnable;

            RecordFrm.formBldgDate.txtPIN.Enabled = blnEnable;
            RecordFrm.formBldgDate.txtBldgName.Enabled = blnEnable;
            RecordFrm.formBldgDate.txtCost.Enabled = blnEnable;
            RecordFrm.formBldgDate.txtUnits.Enabled = blnEnable;
            RecordFrm.formBldgDate.txtStoreys.Enabled = blnEnable;
            RecordFrm.formBldgDate.txtArea.Enabled = blnEnable;
            RecordFrm.formBldgDate.txtAssVal.Enabled = blnEnable;
            RecordFrm.formBldgDate.cmbMaterials.Enabled = blnEnable;
            RecordFrm.formBldgDate.txtHeight.Enabled = blnEnable;
            RecordFrm.formBldgDate.dgvList.Enabled = blnEnable;
            RecordFrm.formBldgDate.btnAdd.Enabled = blnEnable;
            RecordFrm.formBldgDate.btnRemove.Enabled = blnEnable;

            RecordFrm.formStrucOwn.txtLastName.Enabled = blnEnable;
            RecordFrm.formStrucOwn.txtFirstName.Enabled = blnEnable;
            RecordFrm.formStrucOwn.txtMI.Enabled = blnEnable;
            RecordFrm.formStrucOwn.txtHseNo.Enabled = blnEnable;
            RecordFrm.formStrucOwn.txtLotNo.Enabled = blnEnable;
            RecordFrm.formStrucOwn.txtBlkNo.Enabled = blnEnable;
            RecordFrm.formStrucOwn.txtStreet.Enabled = blnEnable;
            RecordFrm.formStrucOwn.cmbBrgy.Enabled = blnEnable;
            RecordFrm.formStrucOwn.txtMun.Enabled = blnEnable;
            RecordFrm.formStrucOwn.txtProv.Enabled = blnEnable;
            RecordFrm.formStrucOwn.txtZIP.Enabled = blnEnable;
            RecordFrm.formStrucOwn.txtTCT.Enabled = blnEnable;
            RecordFrm.formStrucOwn.txtTelNo.Enabled = blnEnable;
            RecordFrm.formStrucOwn.txtCTC.Enabled = blnEnable;
            RecordFrm.formStrucOwn.txtTIN.Enabled = blnEnable;
            RecordFrm.formStrucOwn.btnSearch.Enabled = blnEnable;
            RecordFrm.formStrucOwn.btnClear.Enabled = blnEnable;

            RecordFrm.formLotOwn.txtLastName.Enabled = blnEnable;
            RecordFrm.formLotOwn.txtFirstName.Enabled = blnEnable;
            RecordFrm.formLotOwn.txtMI.Enabled = blnEnable;
            RecordFrm.formLotOwn.txtHseNo.Enabled = blnEnable;
            RecordFrm.formLotOwn.txtLotNo.Enabled = blnEnable;
            RecordFrm.formLotOwn.txtBlkNo.Enabled = blnEnable;
            RecordFrm.formLotOwn.txtStreet.Enabled = blnEnable;
            RecordFrm.formLotOwn.cmbBrgy.Enabled = blnEnable;
            RecordFrm.formLotOwn.txtMun.Enabled = blnEnable;
            RecordFrm.formLotOwn.txtProv.Enabled = blnEnable;
            RecordFrm.formLotOwn.txtZIP.Enabled = blnEnable;
            RecordFrm.formLotOwn.txtTCT.Enabled = blnEnable;
            RecordFrm.formLotOwn.txtTelNo.Enabled = blnEnable;
            RecordFrm.formLotOwn.txtCTC.Enabled = blnEnable;
            RecordFrm.formLotOwn.txtTIN.Enabled = blnEnable;
            RecordFrm.formLotOwn.btnSearch.Enabled = blnEnable;
            RecordFrm.formLotOwn.btnClear.Enabled = blnEnable;

            RecordFrm.formEngr.txtLastName.Enabled = blnEnable;
            RecordFrm.formEngr.txtFirstName.Enabled = blnEnable;
            RecordFrm.formEngr.txtMI.Enabled = blnEnable;
            RecordFrm.formEngr.txtHseNo.Enabled = blnEnable;
            RecordFrm.formEngr.txtLotNo.Enabled = blnEnable;
            RecordFrm.formEngr.txtBlkNo.Enabled = blnEnable;
            RecordFrm.formEngr.txtStreet.Enabled = blnEnable;
            RecordFrm.formEngr.cmbBrgy.Enabled = blnEnable;
            RecordFrm.formEngr.txtMun.Enabled = blnEnable;
            RecordFrm.formEngr.txtProv.Enabled = blnEnable;
            RecordFrm.formEngr.txtZIP.Enabled = blnEnable;
            RecordFrm.formEngr.txtPTR.Enabled = blnEnable;
            RecordFrm.formEngr.txtPRC.Enabled = blnEnable;
            RecordFrm.formEngr.txtTIN.Enabled = blnEnable;
            RecordFrm.formEngr.btnSearch.Enabled = blnEnable;
            RecordFrm.formEngr.btnAdd.Enabled = blnEnable;
            RecordFrm.formEngr.btnRemove.Enabled = blnEnable;
            RecordFrm.formEngr.cmbEngrType.Enabled = blnEnable;
            RecordFrm.formEngr.dgvList.Enabled = blnEnable;
            RecordFrm.formEngr.btnClear.Enabled = blnEnable;
        }

        public override void DisplayData()
		{
			var db = new EPSConnection(dbConn);
			string strWhereCond = string.Empty;

			if (!string.IsNullOrEmpty(RecordFrm.AN))
				RecordFrm.arn1.Enabled = false;

			var result = (dynamic)null;

			strWhereCond = $" where arn = '{RecordFrm.AN}' and main_application = 1";
			//strWhereCond = $" where arn = '{RecordFrm.AN}'";
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

                DateTime.TryParse(item.DATE_ISSUED.ToString(), out dtTmp);
                RecordFrm.dtpDateApproved.Value = dtTmp;

                RecordFrm.formProject.txtProjDesc.Text = item.PROJ_DESC;
				RecordFrm.formProject.cmbStrucType.Text = struc.GetStructureDesc(item.STRUC_CODE);
				iBldgNo = item.BLDG_NO;

				sStatus = item.STATUS_CODE;
				//RecordFrm.formProject.txtPIN.Text = item.BNS_CODE;
				RecordFrm.formProject.txtHseNo.Text = item.PROJ_HSE_NO;
				RecordFrm.formProject.txtLotNo.Text = item.PROJ_LOT_NO;
				RecordFrm.formProject.txtBlkNo.Text = item.PROJ_BLK_NO;
				RecordFrm.formProject.txtStreet.Text = item.PROJ_ADDR;
				RecordFrm.formProject.txtZIP.Text = item.PROJ_ZIP;
                RecordFrm.formProject.txtVillage.Text = item.PROJ_VILL; //ADDED REQUESTED VILLAGE
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

                if(RecordFrm.ButtonAdd.Text == "Save")
                {
                    RecordFrm.ButtonImgAttach.Enabled = true;
                    RecordFrm.ButtonImgDetach.Enabled = true;
                }
                RecordFrm.ButtonImgView.Enabled = true;


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
                    InitEdit(true);
					RecordFrm.ButtonAdd.Enabled = false;
					RecordFrm.ButtonEdit.Text = "Update";
					RecordFrm.ButtonDelete.Enabled = false;
                    RecordFrm.ButtonImgView.Enabled = true;
                    RecordFrm.ButtonImgAttach.Enabled = true;
                    RecordFrm.ButtonImgDetach.Enabled = true;
                }
				else
				{
					if (RecordFrm.ValidateData())
                    {
                        m_isNotSaved = false;
                        Save();
                        if(m_isNotSaved == false)
                        {
                            SaveImage(); // save attached image
                            RecordFrm.ButtonExit.PerformClick();
                            MessageBox.Show("Record Successfully Updated");
                        }
                        else
                            MessageBox.Show("Record not saved!", RecordFrm.DialogText, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
			}
		}

		public override void EnableRecordEntry()
		{
			RecordFrm.tabControl1.Enabled = true;
			RecordFrm.ButtonExit.Text = "Cancel";
			RecordFrm.ButtonPrint.Enabled = false;
        
            if(AppSettingsManager.GetConfigValue("30") != "1")
                RecordFrm.arn1.Enabled = false;

            RecordFrm.btnSearch.Enabled = false;
			RecordFrm.btnClear.Enabled = false;
		}

        public override void SetPermitAN(string sPermit) //AFM 20201027 REQUESTED(10/23/2020) NEW BIN ARN FORMAT
        {
            string sPermitAcro = string.Empty;
            sPermitAcro = RecordFrm.arn1.ANCodeGenerator(sPermit);
            RecordFrm.arn1.SetAn(sPermitAcro);
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

			strWhereCond = $" where arn = '{RecordFrm.AN}' order by main_application desc";

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
            string sArchNo = string.Empty;

			//strWhereCond = $" where arn = '{RecordFrm.AN}' order by main_application desc";
			strWhereCond = $" where arn = '{RecordFrm.AN}' and main_application = '1'";

            var pset = from a in Records.ApplicationTblList.GetRecord(strWhereCond)
					   select a;
			RecordFrm.formEngr.LoadGrid();

			foreach (var item in pset)
			{
				sEngrNo = item.ENGR_CODE;
                sArchNo = item.ARCHITECT;
                if (sEngrNo == null)
                    sEngrNo = "";
                if (sArchNo == null)
                    sArchNo = "";
                string[] arrEngr = sEngrNo.Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                string[] arrArch = sArchNo.Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                if (string.IsNullOrEmpty(sEngrNo))
					sEngrNo = item.ARCHITECT;

                Engineers account = new Engineers();

                try
                {
                    for (int iCnt = 0; iCnt < arrEngr.Length; iCnt++) //requested multiple engr 20201202
                    {
                        account.GetOwner(arrEngr[iCnt].ToString());

                        RecordFrm.formEngr.dgvList.Rows.Add(account.OwnerCode, account.LastName, account.FirstName, account.MiddleInitial,
                            account.EngrType, account.Address, account.HouseNo, account.LotNo, account.BlkNo,
                            account.Barangay, account.City, account.Province, account.ZIP, account.TIN,
                            account.PTR, account.PRC, account.Village); //added requested village
                    }

                }
                catch { }


                try
                {
                    for (int iCnt = 0; iCnt < arrArch.Length; iCnt++) //requested multiple engr 20201202
                    {
                        //for architect
                        account.GetOwner(arrArch[iCnt].ToString());

                        RecordFrm.formEngr.dgvList.Rows.Add(account.OwnerCode, account.LastName, account.FirstName, account.MiddleInitial,
                            account.EngrType, account.Address, account.HouseNo, account.LotNo, account.BlkNo,
                            account.Barangay, account.City, account.Province, account.ZIP, account.TIN,
                            account.PTR, account.PRC, account.Village); //added requested village
                    }
                }
                catch { }  

            }
		}

		public override void ButtonDeleteClick()
		{
			if (AppSettingsManager.Granted("ERD"))
			{
				if (!string.IsNullOrEmpty(RecordFrm.AN))
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

			strQuery = $"delete from application where arn = '{RecordFrm.AN}'";
			db.Database.ExecuteSqlCommand(strQuery);

			MessageBox.Show("Record is deleted.", RecordFrm.DialogText, MessageBoxButtons.OK, MessageBoxIcon.Information);

			if (Utilities.AuditTrail.InsertTrail("ER-D", "APPLICATION", "ARN: " + RecordFrm.AN) == 0)
			{
				MessageBox.Show("Failed to insert audit trail.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			taskman.RemTask(RecordFrm.AN);
			RecordFrm.Close();
		}
	}
}
