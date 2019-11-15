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
using Common.AppSettings;
using Modules.SearchAccount;
using Common.DataConnector;

namespace Modules.Transactions
{
    public partial class frmEngineer : Form
    {
        public string SourceClass { get; set; }
        public string DistCode { get; set; }
        public string EngrAcctNo { get; set; }
        public int iRow = -1;
        public string DialogText { get; set; }

        public frmEngineer()
        {
            InitializeComponent();
        }

        private void frmEngineer_Load(object sender, EventArgs e)
        {
            PopulateBrgy();
            LoadGrid();

            //AFM 20191025 (s)
            OracleResultSet result = new OracleResultSet();
            result.Query = "select * from engineer_type order by engr_type_code";
            if(result.Execute())
                while(result.Read())
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

        public void LoadGrid()
        {
            dgvList.Rows.Clear();
            dgvList.Columns.Clear();
            
            dgvList.Columns.Add("AccountNo", "Engr No");
            dgvList.Columns.Add("LastName", "Last Name");
            dgvList.Columns.Add("FirstName", "First Name");
            dgvList.Columns.Add("MI", "MI");
            dgvList.Columns.Add("EngrType", "Engr Type");
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
        }

        private void PopulateBrgy()
        {
            cmbBrgy.Items.Clear();

            DataTable dataTable = new DataTable("Barangay");
            dataTable.Columns.Clear();
            dataTable.Columns.Add("Code", typeof(String));
            dataTable.Columns.Add("Desc", typeof(String));

            BarangayList lstBrgy = new BarangayList();
            lstBrgy.GetBarangayList(DistCode, true);
            dataTable.Rows.Add("");
            int intCount = lstBrgy.BarangayNames.Count;
            for (int i = 0; i < intCount; i++)
            {
                dataTable.Rows.Add(new String[] { lstBrgy.BarangayCodes[i], lstBrgy.BarangayNames[i] });
            }

            cmbBrgy.DataSource = dataTable;
            cmbBrgy.DisplayMember = "Desc";
            cmbBrgy.ValueMember = "Desc";
            cmbBrgy.SelectedIndex = 0;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            frmSearchAccount form = new frmSearchAccount();
            form.SearchMode = "ENGR";
            form.ShowDialog();

            EngrAcctNo = form.AcctNo;
            cmbEngrType.Text = form.EngrType;
            txtLastName.Text = form.LastName;
            txtFirstName.Text = form.FirstName;
            txtMI.Text = form.MI;
            txtTIN.Text = form.TIN;
            txtPTR.Text = form.PTR;
            txtPRC.Text = form.PRC;
            txtHseNo.Text = form.HouseNo;
            txtLotNo.Text = form.LotNo;
            txtBlkNo.Text = form.BlkNo;
            cmbBrgy.Text = form.Brgy;
            txtMun.Text = form.City;
            txtProv.Text = form.Province;
            txtStreet.Text = form.Address;
            txtZIP.Text = form.Zip;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        public void ClearDgView()
        {
            dgvList.Rows.Clear();
        }

        public void ClearControls()
        {
            iRow = -1;
            EngrAcctNo = "";
            txtLastName.Text = "";
            txtFirstName.Text = "";
            txtMI.Text = "";
            txtTIN.Text = "";
            txtPTR.Text = "";
            txtPRC.Text = "";
            txtHseNo.Text = "";
            txtLotNo.Text = "";
            txtBlkNo.Text = "";
            cmbBrgy.Text = "";
            txtMun.Text = "";
            txtProv.Text = "";
            txtStreet.Text = "";
            txtZIP.Text = "";
            //dgvList.Rows.Clear();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string sLastName = string.Empty;
            string sFirstName = string.Empty;
            string sMI = string.Empty;
            string sEngrType = string.Empty;
            string sTIN = string.Empty;

            if(!string.IsNullOrEmpty(txtLastName.Text.Trim().ToString()) &&
                !string.IsNullOrEmpty(txtFirstName.Text.Trim().ToString()))
            {
                //validate duplicate
                for(int i = 0; i<dgvList.Rows.Count;i++)
                {
                    try
                    {
                        sLastName = dgvList[1, i].Value.ToString();
                        sFirstName = dgvList[2, i].Value.ToString();
                        sMI = dgvList[3, i].Value.ToString();
                        sEngrType = dgvList[4, i].Value.ToString();
                        sTIN = dgvList[13, i].Value.ToString();
                    }
                    catch {
                        
                        sLastName = "";
                        sFirstName = "";
                        sMI = "";
                        sEngrType = "";
                        sTIN = "";
                    }

                    if (sLastName == txtLastName.Text.ToString() &&
                        sFirstName == txtFirstName.Text.ToString() && 
                        //sMI == txtMI.Text.ToString() && 
                        sTIN == txtTIN.Text.ToString())
                    {
                        MessageBox.Show("The engineer you are adding is already in the list.", "EPS", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        ClearControls();
                        return;
                    }

                    if (sEngrType == cmbEngrType.Text.ToString())
                    {
                        MessageBox.Show("The engineer type you are adding is already in the list.", "EPS", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        ClearControls();
                        return;
                    }
                    //AFM 20191113 ANG-19-11104 (s)
                    OracleResultSet result = new OracleResultSet();
                    result.Query = $"select validity_dt from engineer_tbl where engr_code = '{EngrAcctNo}'";
                    if (result.Execute())
                        if (result.Read())
                        {
                            if (result.GetDateTime(0) < AppSettingsManager.GetCurrentDate())
                            {
                                MessageBox.Show("Selected Engineer's license has expired!", "EPS", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                ClearControls();
                                return;
                            }
                        }
                    //AFM 20191113 ANG-19-11104 (e)
                }

                //addrow
                int iRow = dgvList.Rows.Count - 1;

                dgvList.Rows.Add(EngrAcctNo,txtLastName.Text,txtFirstName.Text,txtMI.Text,
                    cmbEngrType.Text,txtStreet.Text, txtHseNo.Text,txtLotNo.Text,
                    txtBlkNo.Text,cmbBrgy.Text,txtMun.Text,txtProv.Text,
                    txtZIP.Text,txtTIN.Text,txtPTR.Text,txtPRC.Text);

            }
            else
            {
                MessageBox.Show("Engineer's information required",DialogText,MessageBoxButtons.OK,MessageBoxIcon.Stop);
                return;
            }
            ClearControls();
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ClearControls();

            iRow = e.RowIndex;
            if (dgvList[0, e.RowIndex].Value != null)
                EngrAcctNo = dgvList[0, e.RowIndex].Value.ToString();
            if(dgvList[1, e.RowIndex].Value != null)
                txtLastName.Text = dgvList[1, e.RowIndex].Value.ToString();
            if (dgvList[2, e.RowIndex].Value != null)
                txtFirstName.Text = dgvList[2, e.RowIndex].Value.ToString();
            if(dgvList[3, e.RowIndex].Value != null)
                txtMI.Text = dgvList[3, e.RowIndex].Value.ToString();
            if(dgvList[4, e.RowIndex].Value != null)
                cmbEngrType.Text = dgvList[4, e.RowIndex].Value.ToString();
            if(dgvList[5, e.RowIndex].Value != null)
                txtStreet.Text = dgvList[5, e.RowIndex].Value.ToString();
            if(dgvList[6, e.RowIndex].Value != null)
                txtHseNo.Text = dgvList[6, e.RowIndex].Value.ToString();
            if(dgvList[7, e.RowIndex].Value != null)
                txtLotNo.Text = dgvList[7, e.RowIndex].Value.ToString();
            if(dgvList[8, e.RowIndex].Value != null)
                txtBlkNo.Text = dgvList[8, e.RowIndex].Value.ToString();
            if(dgvList[9, e.RowIndex].Value != null)
                cmbBrgy.Text = dgvList[9, e.RowIndex].Value.ToString();
            if(dgvList[10, e.RowIndex].Value != null)
                txtMun.Text = dgvList[10, e.RowIndex].Value.ToString();
            if(dgvList[11, e.RowIndex].Value != null)
                txtProv.Text = dgvList[11, e.RowIndex].Value.ToString();
            if(dgvList[12, e.RowIndex].Value != null)
                txtZIP.Text = dgvList[12, e.RowIndex].Value.ToString();
            if(dgvList[13, e.RowIndex].Value != null)
                txtTIN.Text = dgvList[13, e.RowIndex].Value.ToString();
            if(dgvList[14, e.RowIndex].Value != null)
                txtPTR.Text = dgvList[14, e.RowIndex].Value.ToString();
            if(dgvList[15, e.RowIndex].Value != null)
                txtPRC.Text = dgvList[15, e.RowIndex].Value.ToString();

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to remove "+txtLastName.Text.ToString() + ", " + txtFirstName.Text.ToString() + " from the list?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                dgvList.Rows.RemoveAt(iRow);
            }
        }

        public bool ValidateData()
        {
            if(dgvList.Rows.Count < 1)
            {
                MessageBox.Show("Architect/Engineer information required",DialogText,MessageBoxButtons.OK,MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }

        public void EnableControls(bool blnEnable)
        {
            this.txtMI.ReadOnly = !blnEnable;
            this.txtLastName.ReadOnly = !blnEnable;
            this.txtFirstName.ReadOnly = !blnEnable;
            this.txtPTR.ReadOnly = !blnEnable;
            this.txtPRC.ReadOnly = !blnEnable;
            this.txtTIN.ReadOnly = !blnEnable;
            this.cmbBrgy.Enabled = blnEnable;
            this.txtHseNo.ReadOnly = !blnEnable;
            this.txtLotNo.ReadOnly = !blnEnable;
            this.txtBlkNo.ReadOnly = !blnEnable;
            this.txtStreet.ReadOnly = !blnEnable;
            this.txtMun.ReadOnly = !blnEnable;
            this.txtProv.ReadOnly = !blnEnable;
            this.txtZIP.ReadOnly = !blnEnable;
            this.cmbEngrType.Enabled = blnEnable;
            this.dgvList.Enabled = blnEnable;
            this.btnSearch.Enabled = blnEnable;
            this.btnClear.Enabled = blnEnable;
            this.btnAdd.Enabled = blnEnable;
            this.btnRemove.Enabled = blnEnable;
        }

        public void EnableFormControls(bool blnEnable)
        {
            EnableControls(blnEnable);
        }
    }
}
