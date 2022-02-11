using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using EPSEntities.Entity;
using System.Data;
using System.Collections;
using Oracle.ManagedDataAccess.EntityFramework;
using Oracle.ManagedDataAccess.Client;
using Common.AppSettings;
using System.Windows.Forms;
using Modules.Utilities;
using Modules.Reports;
using EPSEntities.Connection;
using Amellar.Common.ImageViewer;

namespace Modules.Transactions
{
    public partial class frmRecords : Form
    {
        private string m_sSource = string.Empty;
        public string Status { get; set; }
        private RecordForm RecordClass = null;
        public static ConnectionString dbConn = new ConnectionString();

        private ScopeList m_lstScope;
        public frmProjectInfo formProject = new frmProjectInfo();
        public frmBldgDates formBldgDate = new frmBldgDates();
        public frmStrucOwner formStrucOwn = new frmStrucOwner();
        public frmLotOwner formLotOwn = new frmLotOwner();
        public frmEngineer formEngr = new frmEngineer();
        public string DialogText { get; set; }

        //AFM 20191028 copy owner values
        public string LastName { get; set; }
        public string Fname { get; set; }
        public string MiName { get; set; }
        public string TelNo { get; set; }
        public string TIN { get; set; }
        public string TCT { get; set; }
        public string CTC { get; set; }
        public string LotNo { get; set; }
        public string BlkNo { get; set; }
        public string Addr { get; set; }
        public string Brgy { get; set; }
        public string Municipality { get; set; }
        public string Province { get; set; }
        public string ZIP { get; set; }

        public bool blnSetDefaultStruc = false;
        public bool blnSetDefaultOwn = false;

        public enum PostingState
        {
            Add, Edit, View, Delete, Print
        }
        public PostingState state;

        public string PermitApplication = string.Empty;

        public string SelectedPermitCode = string.Empty;

        public string SelectedPermitDesc = string.Empty;

        public string SourceClass
        {
            get { return m_sSource; }
            set { m_sSource = value; }
        }

        public Button ButtonAdd
        {
            get { return this.btnAdd; }
            set { this.btnAdd = value; }
        }

        public Button ButtonEdit
        {
            get { return this.btnEdit; }
            set { this.btnEdit = value; }
        }

        public Button ButtonDelete
        {
            get { return this.btnDelete; }
            set { this.btnDelete = value; }
        }

        public Button ButtonPrint
        {
            get { return this.btnPrint; }
            set { this.btnPrint = value; }
        }

        public Button ButtonExit
        {
            get { return this.btnExit; }
            set { this.btnExit = value; }
        }

        public Button ButtonSearch
        {
            get { return this.btnSearch; }
            set { this.btnSearch = value; }
        }

        public Button ButtonClear
        {
            get { return this.btnClear; }
            set { this.btnClear = value; }
        }

        public string AN
        {
            get { return this.arn1.GetAn(); }
        }

        public Button ButtonImgView
        {
            get { return this.btnImgView; }
            set { this.btnImgView = value; }
        }

        public Button ButtonImgAttach
        {
            get { return this.btnImgAttach; }
            set { this.btnImgAttach = value; }
        }

        public Button ButtonImgDetach
        {
            get { return this.btnImgDetach; }
            set { this.btnImgDetach = value; }
        }


        public frmRecords()
        {
            InitializeComponent();

        }

        private void frmRecords_Load(object sender, EventArgs e)
        {
             RecordClass = new RecordForm(this);
            if (this.SourceClass == "ENG_REC")
            {
                this.Status = "NEW";
                RecordClass = new EngrRecords(this);
                this.Text = "Engineering Records";
                btnImgAttach.Visible = true; //temporary
                btnImgView.Visible = true;
                btnImgDetach.Visible = true;
            }
            else if (this.SourceClass == "NEW_ADD" || this.SourceClass == "NEW_EDIT"
                || this.SourceClass == "NEW_VIEW" || this.SourceClass == "NEW_CANCEL" || this.SourceClass.Contains("NEW_ADD_") || this.SourceClass == "NEW_EDIT_OTH") //AFM 20210316 added condition for other applications
            {
                this.Status = "NEW";
                RecordClass = new Application(this);
                this.Text = "Application - New";
            }
            else if (this.SourceClass == "REN_ADD" || this.SourceClass == "REN_EDIT"
                || this.SourceClass == "REN_VIEW" || this.SourceClass == "REN_CANCEL")
            {
                this.Status = "REN";
                RecordClass = new Application(this);
                this.Text = "Application - Renewal";
            }

            DialogText = this.Text;
            RecordClass.FormLoad();


            formProject.SourceClass = m_sSource;
            formProject.DialogText = this.Text;
            formProject.TopLevel = false;
            formProject.Visible = true;
            formProject.FormBorderStyle = FormBorderStyle.None;
            formProject.Dock = DockStyle.Fill;

            formBldgDate.SourceClass = m_sSource;
            formBldgDate.DialogText = this.Text;
            formBldgDate.TopLevel = false;
            formBldgDate.Visible = true;
            formBldgDate.FormBorderStyle = FormBorderStyle.None;
            formBldgDate.Dock = DockStyle.Fill;

            formStrucOwn.SourceClass = m_sSource;
            formStrucOwn.DialogText = this.Text;
            formStrucOwn.TopLevel = false;
            formStrucOwn.Visible = true;
            formStrucOwn.FormBorderStyle = FormBorderStyle.None;
            formStrucOwn.Dock = DockStyle.Fill;

            formLotOwn.SourceClass = m_sSource;
            formLotOwn.DialogText = this.Text;
            formLotOwn.TopLevel = false;
            formLotOwn.Visible = true;
            formLotOwn.FormBorderStyle = FormBorderStyle.None;
            formLotOwn.Dock = DockStyle.Fill;

            formEngr.SourceClass = m_sSource;
            formEngr.DialogText = this.Text;
            formEngr.TopLevel = false;
            formEngr.Visible = true;
            formEngr.FormBorderStyle = FormBorderStyle.None;
            formEngr.Dock = DockStyle.Fill;

            tabControl1.TabPages[0].Controls.Add(formProject);
            tabControl1.TabPages[0].Text = "Project Information";
            tabControl1.TabPages[1].Controls.Add(formBldgDate);
            tabControl1.TabPages[1].Text = "Building Information";
            tabControl1.TabPages[2].Controls.Add(formStrucOwn);
            tabControl1.TabPages[2].Text = "Structure Owner";
            tabControl1.TabPages[3].Controls.Add(formLotOwn);
            tabControl1.TabPages[3].Text = "Lot Owner";
            tabControl1.TabPages[4].Controls.Add(formEngr);
            tabControl1.TabPages[4].Text = "Architect & Engineers";

            RecordClass.PopulatePermit();
            if (PermitApplication.Contains("BUILDING") || PermitApplication.Contains("MECHANICAL") || PermitApplication.Contains("CFEI"))
            {
                if (PermitApplication.Contains("BUILDING"))
                    this.btnSearch.Enabled = false;
                else
                    this.btnSearch.Enabled = true; //AFM 20220209 adjustments binan meeting 20220209
                cmbPermit.SelectedIndex = 1;
            }
            PopulateScope();

            if((PermitApplication.Contains("ELECTRICAL") || PermitApplication.Contains("OCCUPANCY") || PermitApplication.Contains("OTHERS")) && this.SourceClass != "NEW_EDIT_OTH")
            {
                this.btnSearch.Enabled = true;
                //this.arn1.Enabled = true;
                this.arn1.Enabled = false; ////AFM 20220209 adjustments binan meeting 20220209
                this.btnEdit.Enabled = false;
                this.btnAdd.Enabled = false;
                cmbPermit.SelectedIndex = 1;
            }
            else if(SourceClass == "NEW_EDIT" || SourceClass == "NEW_EDIT_OTH")
            {
                this.btnSearch.Enabled = true;
                this.arn1.Enabled = true;
                btnExit.Text = "Exit";
            }

        }



        private void PopulateScope()
        {
            cmbScope.Items.Clear();

            m_lstScope = new ScopeList();
            int iCnt = m_lstScope.ScopeLst.Count;

            DataTable dataTable = new DataTable("Scope");
            dataTable.Columns.Clear();
            dataTable.Columns.Add("ScopeCode", typeof(String));
            dataTable.Columns.Add("ScopeDesc", typeof(String));

            dataTable.Rows.Add(new String[] { "", "" });

            for (int i = 0; i < iCnt; i++)
            {
                if ((SourceClass == "REN_ADD" || SourceClass == "REN_EDIT") && m_lstScope.ScopeLst[i].ScopeDesc == "NEW")
                { }
                else
                    dataTable.Rows.Add(new String[] { m_lstScope.ScopeLst[i].ScopeCode, m_lstScope.ScopeLst[i].ScopeDesc });
            }

            cmbScope.DataSource = dataTable;
            cmbScope.DisplayMember = "ScopeDesc";
            cmbScope.ValueMember = "ScopeDesc";
            cmbScope.SelectedIndex = 0;

        }

        public void EnableControl(bool blnEnable)
        {
            //arn1.Enabled = blnEnable;
            cmbPermit.Enabled = blnEnable;
            cmbScope.Enabled = blnEnable;
            dtDateApplied.Enabled = blnEnable;
            dtpDateApproved.Enabled = blnEnable;

            tabControl1.Enabled = blnEnable;
            //btnSearch.Enabled = blnEnable;
            //btnClear.Enabled = blnEnable;
        }



        private void btnAdd_Click(object sender, EventArgs e)
        {
            RecordClass.ButtonAddClick(btnAdd.Text);
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
           formLotOwn.LastName = LastName;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            TaskManager taskman = new TaskManager();
            taskman.RemTask(AN);

            if (btnExit.Text == "Cancel")
            {
                frmBuildingUnits frmbldgunits = new frmBuildingUnits();
                ClearControls();
                RecordClass.InitEdit(true); //AFM 20191024 ANG-19-11168
                //btnAdd.Text = "Add";
                //btnEdit.Text = "Edit";
                //btnExit.Text = "Exit";
                //btnAdd.Enabled = true;
                //btnEdit.Enabled = true;
                //btnDelete.Enabled = true;
                //btnPrint.Enabled = true;
                btnSearch.Enabled = true;
                btnClear.Enabled = true;
                arn1.Enabled = true;
                btnImgAttach.Enabled = false;
                frmbldgunits.ClearBuilding();
            }
            else
            {
                this.Close();
            }
        }

        private void ClearControls()
        {
            RecordClass.ClearControl();

            this.arn1.Enabled = true;
            this.arn1.GetAn();
            this.arn1.GetTaxYear = "";

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        public bool ValidateData()
        {
            if (string.IsNullOrEmpty(cmbPermit.Text.ToString()))
            {
                MessageBox.Show("Permit Type is required", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(cmbScope.Text.ToString()))
            {
                MessageBox.Show("Scope of Work is required", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            //if (!RecordClass.ValidateData()) //AFM 20220209 DISABLED - adjustments binan meeting 20220209
            //return false;

            if (!formProject.ValidateData())
                return false;
            if (!formBldgDate.ValidateData())
                return false;
            if (!formStrucOwn.ValidateData())
                return false;
            if (!formLotOwn.ValidateData())
                return false;
            if (!formEngr.ValidateData())
                return false;

            if (MessageBox.Show("Save Record?", DialogText, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            { return false; }

            return true;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (arn1.Enabled == false && cmbPermit.Text.Trim() != "" && cmbScope.Text.Trim() != "") //AFM 20200910
                RecordClass.ButtonEditClick(btnEdit.Text);
            else
                MessageBox.Show("No record selected!");
        }

        private void cmbScope_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbPermit.Text.ToString()) &&
                !string.IsNullOrEmpty(cmbScope.Text.ToString()))
            {
                RecordClass.EnableRecordEntry();
            }
        }

        private void cmbPermit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbPermit.Text.ToString()) &&
                !string.IsNullOrEmpty(cmbScope.Text.ToString()))
            {
                RecordClass.EnableRecordEntry();
            }

            string sPermit = ((DataRowView)cmbPermit.SelectedItem)["PermitCode"].ToString();
            RecordClass.SetPermitAN(sPermit);


            try
            {
                if (!string.IsNullOrEmpty(cmbPermit.Text.ToString()) && (SourceClass == "NEW_ADD" || SourceClass == "REN_ADD" || SourceClass == "ENG_REC_ADD") || SourceClass == "NEW_ADD_MECH" || SourceClass == "NEW_ADD_CFEI")
                {
                    formBldgDate.LoadGrid();
                    formBldgDate.dgvList.Rows.Add(((DataRowView)cmbPermit.SelectedItem)["PermitCode"].ToString(), ((DataRowView)cmbPermit.SelectedItem)["PermitDesc"].ToString(), null, null, null);

                }
                else
                {
                    formBldgDate.LoadGrid();
                    formBldgDate.dgvList.Rows.Add();
                }

                formBldgDate.PermitAppl = cmbPermit.Text.ToString();
            }
            catch (Exception ex) // catches any error
            {
                MessageBox.Show(ex.Message.ToString());
            }
            if (this.SourceClass.Contains("ENG_REC"))
            {
                formBldgDate.dgvList.Columns[3].ReadOnly = true;
                formBldgDate.dgvList.Columns[4].ReadOnly = true;
            }
            else
            {
                formBldgDate.dgvList.Columns[2].ReadOnly = true;
                formBldgDate.dgvList.Columns[3].ReadOnly = true;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            RecordClass.ButtonSearchClick();
        }

        private void cmbPermit_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbPermit.Text.ToString()))
            {
                PermitList permit = new PermitList(null);
                if (string.IsNullOrEmpty(permit.GetPermitCode(cmbPermit.Text.ToString())))
                {
                    MessageBox.Show("Permit not found in list", DialogText, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    cmbPermit.Text = "";
                    cmbPermit.Focus();
                    return;
                }
            }
        }

        private void cmbScope_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbScope.Text.ToString()))
            {
                ScopeList permit = new ScopeList();
                if (string.IsNullOrEmpty(permit.GetScopeCode(cmbScope.Text.ToString())))
                {
                    MessageBox.Show("Scope not found in list", DialogText, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    cmbScope.Text = "";
                    cmbScope.Focus();
                    return;
                }
            }
        }

        public void CopyLotStrucOwner(string sSwitch)
        {
            if (sSwitch == "CopyLot")
            {
                if (!string.IsNullOrEmpty(formLotOwn.LotAcctNo))
                {
                    
                    formStrucOwn.StrucAcctNo = formLotOwn.LotAcctNo;
                    formStrucOwn.txtLastName.Text = formLotOwn.txtLastName.Text;
                    formStrucOwn.txtFirstName.Text = formLotOwn.txtFirstName.Text;
                    formStrucOwn.txtMI.Text = formLotOwn.txtMI.Text;
                    formStrucOwn.txtTelNo.Text = formLotOwn.txtTelNo.Text;
                    formStrucOwn.txtTIN.Text = formLotOwn.txtTIN.Text;
                    formStrucOwn.txtTCT.Text = formLotOwn.txtTCT.Text;
                    formStrucOwn.txtCTC.Text = formLotOwn.txtCTC.Text;
                    formStrucOwn.txtHseNo.Text = formLotOwn.txtHseNo.Text;
                    formStrucOwn.txtLotNo.Text = formLotOwn.txtLotNo.Text;
                    formStrucOwn.txtBlkNo.Text = formLotOwn.txtBlkNo.Text;
                    formStrucOwn.cmbBrgy.Text = formLotOwn.cmbBrgy.Text;
                    formStrucOwn.txtMun.Text = formLotOwn.txtMun.Text;
                    formStrucOwn.txtProv.Text = formLotOwn.txtProv.Text;
                    formStrucOwn.txtStreet.Text = formLotOwn.txtStreet.Text;
                    formStrucOwn.txtZIP.Text = formLotOwn.txtZIP.Text;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(formStrucOwn.StrucAcctNo))
                {
                    formLotOwn.txtLastName.Text = LastName;

                    formLotOwn.LotAcctNo = formStrucOwn.StrucAcctNo;
                    formLotOwn.txtLastName.Text = formStrucOwn.txtLastName.Text;
                    formLotOwn.txtFirstName.Text = formStrucOwn.txtFirstName.Text;
                    formLotOwn.txtMI.Text = formStrucOwn.txtMI.Text;
                    formLotOwn.txtTelNo.Text = formStrucOwn.txtTelNo.Text;
                    formLotOwn.txtTIN.Text = formStrucOwn.txtTIN.Text;
                    formLotOwn.txtTCT.Text = formStrucOwn.txtTCT.Text;
                    formLotOwn.txtCTC.Text = formStrucOwn.txtCTC.Text;
                    formLotOwn.txtHseNo.Text = formStrucOwn.txtHseNo.Text;
                    formLotOwn.txtLotNo.Text = formStrucOwn.txtLotNo.Text;
                    formLotOwn.txtBlkNo.Text = formStrucOwn.txtBlkNo.Text;
                    formLotOwn.cmbBrgy.Text = formStrucOwn.cmbBrgy.Text;
                    formLotOwn.txtMun.Text = formStrucOwn.txtMun.Text;
                    formLotOwn.txtProv.Text = formStrucOwn.txtProv.Text;
                    formLotOwn.txtStreet.Text = formStrucOwn.txtStreet.Text;
                    formLotOwn.txtZIP.Text = formStrucOwn.txtZIP.Text;
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            RecordClass.ButtonDeleteClick();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            frmReport form = new frmReport();
            if (this.Text.Contains("New Application") || this.Text.Contains("Renewal Application"))
                form.ReportName = "Application";
            else
                form.ReportName = "Records";
            form.An = AN;
            form.ShowDialog();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void btnImgView_Click(object sender, EventArgs e)
        {
            RecordClass.ButtonImgView();
        }

        private void btnImgAttach_Click(object sender, EventArgs e)
        {
            RecordClass.ButtonImgAttach();
        }

        private void btnImgDetach_Click(object sender, EventArgs e)
        {
            RecordClass.ButtonImgDetach();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //AFM 20220202 copy address info from project as default
            if (this.SourceClass == "NEW_ADD" || this.SourceClass == "NEW_ADD_MECH" || this.SourceClass == "NEW_ADD_CFEI")
            {
                if (tabControl1.SelectedTab == tabControl1.TabPages[2] && blnSetDefaultStruc == false) //struc own
                {
                    formStrucOwn.txtHseNo.Text = formProject.txtHseNo.Text;
                    formStrucOwn.txtLotNo.Text = formProject.txtLotNo.Text;
                    formStrucOwn.txtBlkNo.Text = formProject.txtBlkNo.Text;
                    formStrucOwn.txtStreet.Text = formProject.txtStreet.Text;
                    formStrucOwn.txtVillage.Text = formProject.txtVillage.Text;
                    formStrucOwn.txtMun.Text = formProject.txtMun.Text;
                    formStrucOwn.txtProv.Text = formProject.txtProv.Text;
                    formStrucOwn.txtZIP.Text = formProject.txtZIP.Text;
                    blnSetDefaultStruc = true;
                }
                if (tabControl1.SelectedTab == tabControl1.TabPages[3] && blnSetDefaultOwn == false) //lot own
                {
                    formLotOwn.txtHseNo.Text = formProject.txtHseNo.Text;
                    formLotOwn.txtLotNo.Text = formProject.txtLotNo.Text;
                    formLotOwn.txtBlkNo.Text = formProject.txtBlkNo.Text;
                    formLotOwn.txtStreet.Text = formProject.txtStreet.Text;
                    formLotOwn.txtVillage.Text = formProject.txtVillage.Text;
                    formLotOwn.txtMun.Text = formProject.txtMun.Text;
                    formLotOwn.txtProv.Text = formProject.txtProv.Text;
                    formLotOwn.txtZIP.Text = formProject.txtZIP.Text;
                    blnSetDefaultOwn = true;
                }
            }
           
        }
    }
}
