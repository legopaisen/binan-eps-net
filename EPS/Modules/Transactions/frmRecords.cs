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

namespace Modules.Transactions
{
    public partial class frmRecords : Form
    {
        private string m_sSource = string.Empty;
        public string Status { get; set; }
        private RecordForm RecordClass = null;

        private ScopeList m_lstScope;
        public frmProjectInfo formProject = new frmProjectInfo();
        public frmBldgDates formBldgDate = new frmBldgDates();
        public frmStrucOwner formStrucOwn = new frmStrucOwner();
        public frmLotOwner formLotOwn = new frmLotOwner();
        public frmEngineer formEngr = new frmEngineer();
        public string DialogText { get; set; }

        public enum PostingState
        {
            Add, Edit, View, Delete, Print
        }
        public PostingState state;

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

        public string ARN
        {
            get { return this.arn1.GetArn(); }
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
            }
            else if (this.SourceClass == "NEW_ADD" || this.SourceClass == "NEW_EDIT"
                || this.SourceClass == "NEW_VIEW" || this.SourceClass == "NEW_CANCEL")
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
            PopulateScope();
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

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            TaskManager taskman = new TaskManager();
            taskman.RemTask(ARN);

            if (btnExit.Text == "Cancel")
            {
                ClearControls();
                btnAdd.Text = "Add";
                btnEdit.Text = "Edit";
                btnExit.Text = "Exit";
                btnAdd.Enabled = true;
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                btnPrint.Enabled = true;
                btnSearch.Enabled = true;
                btnClear.Enabled = true;
                arn1.Enabled = true;
            }
            else
            {
                this.Close();
            }
        }

        private void ClearControls()
        {
            RecordClass.ClearControl();
            
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
            if(string.IsNullOrEmpty(cmbScope.Text.ToString()))
            {
                MessageBox.Show("Scope of Work is required", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (!RecordClass.ValidateData())
                return false;

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
            RecordClass.ButtonEditClick(btnEdit.Text);
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
            if(!string.IsNullOrEmpty(cmbPermit.Text.ToString()) &&
                !string.IsNullOrEmpty(cmbScope.Text.ToString()))
            {
                RecordClass.EnableRecordEntry();
            }

            try
            {
                if (!string.IsNullOrEmpty(cmbPermit.Text.ToString()) && (SourceClass == "NEW_ADD" || SourceClass == "REN_ADD" || SourceClass == "ENG_REC_ADD"))
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
                    MessageBox.Show("Permit not found in list",DialogText,MessageBoxButtons.OK,MessageBoxIcon.Stop);
                    cmbPermit.Text = "";
                    cmbPermit.Focus();
                    return;
                }
            }
        }

        private void cmbScope_Leave(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(cmbScope.Text.ToString()))
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
            if(sSwitch == "CopyLot")
            {
                if(!string.IsNullOrEmpty(formLotOwn.LotAcctNo))
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
            /*frmReport form = new frmReport();
            form.ReportName = "APPLICATION";
            form.Arn = ARN;
            form.ShowDialog();*/
        }
    }
}
