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
using Common.DataConnector;
using Common.AppSettings;

namespace Modules.SearchAccount
{
    public partial class frmSearchAccount : Form
    {
        public string SearchMode { get; set; }

        private AccountsList m_lstAccount;
        private EngineersList m_lstEngr;
        public string AcctNo
        {
            get { return txtAcctNo.Text; }
            set { txtAcctNo.Text = value; }
        }
        public string LastName
        {
            get { return txtLastName.Text; }
            set { txtLastName.Text = value; }
        }
        public string FirstName
        {
            get { return txtFirstName.Text; }
            set { txtFirstName.Text = value; }
        }
        public string MI
        {
            get { return txtMI.Text; }
            set { txtMI.Text = value; }
        }

        public string EngrType
        {
            get { return cmbEngrType.Text; }
            set { cmbEngrType.Text = value; }
        }

        public string Address { get; set; }
        public string TIN { get; set; }
        public string TCT { get; set; }
        public string CTC { get; set; }
        public string Phone { get; set; }
        public string PTR { get; set; }
        public string PRC { get; set; }
        public string HouseNo { get; set; }
        public string LotNo { get; set; }
        public string BlkNo { get; set; }
        public string Brgy { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Zip { get; set; }

        public frmSearchAccount()
        {
            InitializeComponent();
        }

        private void bntList_Click(object sender, EventArgs e)
        {
            int intCount = 0;
            dgvList.Rows.Clear();

            if (SearchMode == "ACCOUNT")
            {
                m_lstAccount = new AccountsList(txtAcctNo.Text.Trim(), txtLastName.Text.Trim(),txtFirstName.Text.Trim(),txtMI.Text.Trim());
                intCount = m_lstAccount.AcctLst.Count;

                for (int i = 0; i < intCount; i++)
                {
                    dgvList.Rows.Add(m_lstAccount.AcctLst[i].OwnerCode,
                        m_lstAccount.AcctLst[i].LastName,
                        m_lstAccount.AcctLst[i].FirstName,
                        m_lstAccount.AcctLst[i].MiddleInitial,
                        m_lstAccount.AcctLst[i].Address,
                        m_lstAccount.AcctLst[i].HouseNo,
                        m_lstAccount.AcctLst[i].LotNo,
                        m_lstAccount.AcctLst[i].BlkNo,
                        m_lstAccount.AcctLst[i].Barangay,
                        m_lstAccount.AcctLst[i].City,
                        m_lstAccount.AcctLst[i].Province,
                        m_lstAccount.AcctLst[i].ZIP,
                        m_lstAccount.AcctLst[i].TIN,
                        m_lstAccount.AcctLst[i].TCT,
                        m_lstAccount.AcctLst[i].CTC,
                        m_lstAccount.AcctLst[i].TelNo);

                }
            }
            else
            {
                m_lstEngr = new EngineersList(txtAcctNo.Text.Trim(), txtLastName.Text.Trim(), txtFirstName.Text.Trim(), txtMI.Text.Trim(),cmbEngrType.Text);
                intCount = m_lstEngr.AcctLst.Count;

                for (int i = 0; i < intCount; i++)
                {
                    dgvList.Rows.Add(m_lstEngr.AcctLst[i].OwnerCode,
                        m_lstEngr.AcctLst[i].LastName,
                        m_lstEngr.AcctLst[i].FirstName,
                        m_lstEngr.AcctLst[i].MiddleInitial,
                        m_lstEngr.AcctLst[i].Address,
                        m_lstEngr.AcctLst[i].HouseNo,
                        m_lstEngr.AcctLst[i].LotNo,
                        m_lstEngr.AcctLst[i].BlkNo,
                        m_lstEngr.AcctLst[i].Barangay,
                        m_lstEngr.AcctLst[i].City,
                        m_lstEngr.AcctLst[i].Province,
                        m_lstEngr.AcctLst[i].ZIP,
                        m_lstEngr.AcctLst[i].TIN,
                        m_lstEngr.AcctLst[i].PTR,
                        m_lstEngr.AcctLst[i].PRC,
                        m_lstEngr.AcctLst[i].EngrType);

                }
            }
                
        }

        private void LoadGrid()
        {
            dgvList.Rows.Clear();
            dgvList.Columns.Clear();

            if(SearchMode == "ACCOUNT")
            {
                dgvList.Columns.Add("AccountNo", "Account No");
                dgvList.Columns.Add("LastName", "Last Name");
                dgvList.Columns.Add("FirstName", "First Name");
                dgvList.Columns.Add("MI", "MI");
                dgvList.Columns.Add("Address", "Address");
                dgvList.Columns.Add("HouseNo", "House No");
                dgvList.Columns.Add("LotNo", "Lot No");
                dgvList.Columns.Add("BlkNo", "Blk No");
                dgvList.Columns.Add("Brgy", "Brgy");
                dgvList.Columns.Add("City", "City");
                dgvList.Columns.Add("Province", "Province");
                dgvList.Columns.Add("Zip", "Zip");
                dgvList.Columns.Add("TIN", "TIN");
                dgvList.Columns.Add("TCT", "TCT");
                dgvList.Columns.Add("CTC", "CTC");
                dgvList.Columns.Add("Phone", "Phone");
            }
            else
            {
                dgvList.Columns.Add("EngrNo", "Engr No");
                dgvList.Columns.Add("LastName", "Last Name");
                dgvList.Columns.Add("FirstName", "First Name");
                dgvList.Columns.Add("MI", "MI");
                dgvList.Columns.Add("Address", "Address");
                dgvList.Columns.Add("HouseNo", "House No");
                dgvList.Columns.Add("LotNo", "Lot No");
                dgvList.Columns.Add("BlkNo", "Blk No");
                dgvList.Columns.Add("Brgy", "Brgy");
                dgvList.Columns.Add("City", "City");
                dgvList.Columns.Add("Province", "Province");
                dgvList.Columns.Add("Zip", "Zip");
                dgvList.Columns.Add("TIN", "TIN");
                dgvList.Columns.Add("PTR", "PTR");
                dgvList.Columns.Add("PRC", "PRC");
                dgvList.Columns.Add("Type", "Type");
            }

            dgvList.RowHeadersVisible = false;
            dgvList.Columns[0].Width = 80;
            dgvList.Columns[1].Width = 100;
            dgvList.Columns[2].Width = 100;
            dgvList.Columns[3].Width = 50;
            dgvList.Columns[4].Width = 150;
            dgvList.Columns[5].Width = 100;
            dgvList.Columns[6].Width = 100;
            dgvList.Columns[7].Width = 100;
            dgvList.Columns[8].Width = 100;
            dgvList.Columns[9].Width = 50;
            dgvList.Columns[10].Width = 150;
            dgvList.Columns[11].Width = 100;
            dgvList.Columns[12].Width = 100;
            dgvList.Columns[13].Width = 100;
            dgvList.Columns[14].Width = 100;
            dgvList.Columns[15].Width = 100;
        }

        private void frmSearchAccount_Load(object sender, EventArgs e)
        {
            if (SearchMode == "ACCOUNT")
            {
                this.lblAcctNo.Text = "Account No";
                this.Text = "Search Owners";
                lblEngrType.Visible = false;
                cmbEngrType.Visible = false;
            }
            else
            {
                this.lblAcctNo.Text = "Engr No";
                this.Text = "Search Engineers";
                lblEngrType.Visible = true;
                cmbEngrType.Visible = true;

                cmbEngrType.Items.Add("");
                //AFM 20191025 (s)
                OracleResultSet result = new OracleResultSet();
                result.Query = "select * from engineer_type order by engr_type_code";
                if (result.Execute())
                    while (result.Read())
                    {
                        cmbEngrType.Items.Add(result.GetString("engr_desc"));
                    }
                //AFM 20191025 (e)

                //cmbEngrType.Items.Add("");
                //cmbEngrType.Items.Add("ARCHITECT");
                //cmbEngrType.Items.Add("CIVIL");
                //cmbEngrType.Items.Add("ELECTRICAL");
                //cmbEngrType.Items.Add("MECHANICAL");
                //cmbEngrType.Items.Add("SANITARY");

            }

            LoadGrid();
        }

        private void ClearControl()
        {
            txtAcctNo.Text = "";
            txtLastName.Text = "";
            txtFirstName.Text = "";
            txtMI.Text = "";
            cmbEngrType.Text = "";
            AcctNo = "";
            LastName = "";
            FirstName = "";
            MI = "";
            Address = "";
            HouseNo = "";
            LotNo = "";
            BlkNo = "";
            Brgy = "";
            City = "";
            Province = "";
            Zip = "";
            TIN = "";
            TCT = "";
            CTC = "";
            Phone = "";
            PTR = "";
            PRC = "";
            EngrType = "";
           
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearControl();
            dgvList.Rows.Clear();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(AcctNo))
            {
                MessageBox.Show("Select owner first","EPS",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                return;
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControl();
            this.Close();
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ClearControl();
            try
            {
                if (dgvList[0, e.RowIndex].Value != null)
                    AcctNo = dgvList[0, e.RowIndex].Value.ToString();
                if (dgvList[1, e.RowIndex].Value != null)
                    LastName = dgvList[1, e.RowIndex].Value.ToString();
                if (dgvList[2, e.RowIndex].Value != null)
                    FirstName = dgvList[2, e.RowIndex].Value.ToString();
                if (dgvList[3, e.RowIndex].Value != null)
                    MI = dgvList[3, e.RowIndex].Value.ToString();
                if (dgvList[4, e.RowIndex].Value != null)
                    Address = dgvList[4, e.RowIndex].Value.ToString();
                if (dgvList[5, e.RowIndex].Value != null)
                    HouseNo = dgvList[5, e.RowIndex].Value.ToString();
                if (dgvList[6, e.RowIndex].Value != null)
                    LotNo = dgvList[6, e.RowIndex].Value.ToString();
                if (dgvList[7, e.RowIndex].Value != null)
                    BlkNo = dgvList[7, e.RowIndex].Value.ToString();
                if (dgvList[8, e.RowIndex].Value != null)
                    Brgy = dgvList[8, e.RowIndex].Value.ToString();
                if (dgvList[9, e.RowIndex].Value != null)
                    City = dgvList[9, e.RowIndex].Value.ToString();
                if (dgvList[10, e.RowIndex].Value != null)
                    Province = dgvList[10, e.RowIndex].Value.ToString();
                if (dgvList[11, e.RowIndex].Value != null)
                    Zip = dgvList[11, e.RowIndex].Value.ToString();
                if (dgvList[12, e.RowIndex].Value != null)
                    TIN = dgvList[12, e.RowIndex].Value.ToString();

                if (SearchMode == "ACCOUNT")
                {
                    if (dgvList[13, e.RowIndex].Value != null)
                        TCT = dgvList[13, e.RowIndex].Value.ToString();
                    if (dgvList[14, e.RowIndex].Value != null)
                        CTC = dgvList[14, e.RowIndex].Value.ToString();
                    if (dgvList[15, e.RowIndex].Value != null)
                        Phone = dgvList[15, e.RowIndex].Value.ToString();
                    PTR = "";
                    PRC = "";
                    EngrType = "";
                }
                else
                {
                    if (dgvList[13, e.RowIndex].Value != null)
                        PTR = dgvList[13, e.RowIndex].Value.ToString();
                    if (dgvList[14, e.RowIndex].Value != null)
                        PRC = dgvList[14, e.RowIndex].Value.ToString();
                    if (dgvList[15, e.RowIndex].Value != null)
                        EngrType = dgvList[15, e.RowIndex].Value.ToString();
                    TCT = "";
                    CTC = "";
                    Phone = "";
                }
            }
            catch { }

        }

        private void dgvList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
