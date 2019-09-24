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
using Modules.SearchAccount;

namespace Modules.Transactions
{
    public partial class frmLotOwner : Form
    {
        public string SourceClass { get; set; }
        public string DistCode { get; set; }
        public string LotAcctNo { get; set; }
        public string DialogText { get; set; }

        public Button ButtonStrucOwner
        {
            get { return this.btnCopy; }
            set { this.btnCopy = value; }
        }

        public frmLotOwner()
        {
            InitializeComponent();
        }

        private void frmLotOwner_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btnCopy, "Use Structure Owner");

            ClearControls();

            PopulateBrgy();

            txtMun.Text = AppSettingsManager.GetConfigValue("02");
            txtProv.Text = AppSettingsManager.GetConfigValue("03");
            txtZIP.Text = AppSettingsManager.GetConfigValue("28");
        }
        public void ClearControls()
        {
            LotAcctNo = string.Empty;
            this.txtMI.Text = string.Empty;
            this.txtLastName.Text = string.Empty;
            this.txtFirstName.Text = string.Empty;
            this.txtCTC.Text = string.Empty;
            this.txtTelNo.Text = string.Empty;
            this.txtTCT.Text = string.Empty;
            this.txtTIN.Text = string.Empty;
            this.cmbBrgy.Text = string.Empty;
            this.txtHseNo.Text = string.Empty;
            this.txtLotNo.Text = string.Empty;
            this.txtBlkNo.Text = string.Empty;
            this.txtStreet.Text = string.Empty;
            //this.txtMun.Text = string.Empty;
            //this.txtProv.Text = string.Empty;
            //this.txtZIP.Text = string.Empty;
        }

        private void EnableControls(bool blnEnable)
        {
            this.txtMI.ReadOnly = !blnEnable;
            this.txtLastName.ReadOnly = !blnEnable;
            this.txtFirstName.ReadOnly = !blnEnable;
            this.txtCTC.ReadOnly = !blnEnable;
            this.txtTelNo.ReadOnly = !blnEnable;
            this.txtTCT.ReadOnly = !blnEnable;
            this.txtTIN.ReadOnly = !blnEnable;
            this.cmbBrgy.Enabled = blnEnable;
            this.txtHseNo.ReadOnly = !blnEnable;
            this.txtLotNo.ReadOnly = !blnEnable;
            this.txtBlkNo.ReadOnly = !blnEnable;
            this.txtStreet.ReadOnly = !blnEnable;
            //this.txtMun.ReadOnly = !blnEnable;
            //this.txtProv.ReadOnly = !blnEnable;
            //this.txtZIP.ReadOnly = !blnEnable;
            this.btnSearch.Enabled = blnEnable;
            this.btnClear.Enabled = blnEnable;
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
            if (!SearchAccount())
                return;

            frmSearchAccount form = new frmSearchAccount();
            form.SearchMode = "ACCOUNT";
            form.ShowDialog();

            LotAcctNo = form.AcctNo;
            txtLastName.Text = form.LastName;
            txtFirstName.Text = form.FirstName;
            txtMI.Text = form.MI;
            txtTelNo.Text = form.Phone;
            txtTIN.Text = form.TIN;
            txtTCT.Text = form.TCT;
            txtCTC.Text = form.CTC;
            txtHseNo.Text = form.HouseNo;
            txtLotNo.Text = form.LotNo;
            txtBlkNo.Text = form.BlkNo;
            cmbBrgy.Text = form.Brgy;
            txtMun.Text = form.City;
            txtProv.Text = form.Province;
            txtStreet.Text = form.Address;
            txtZIP.Text = form.Zip;

        }

        private bool SearchAccount()
        {
            string sAcctNo = string.Empty;

            Utilities.Accounts acct = new Utilities.Accounts();
            acct.GetOwner(txtLastName.Text.Trim(), txtFirstName.Text.Trim());
            sAcctNo = acct.OwnerCode;

            if (!string.IsNullOrEmpty(sAcctNo))
            {
                if (string.IsNullOrEmpty(LotAcctNo))
                {
                    MessageBox.Show("Record found having the same name of " + txtLastName.Text.Trim() + ", " + txtFirstName.Text.Trim() + ".\nPlease use search account.", DialogText, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
                else
                {
                    if (LotAcctNo != sAcctNo)
                    {
                        MessageBox.Show("Record found having the same name of " + txtLastName.Text.Trim() + ", " + txtFirstName.Text.Trim() + ".\nPlease use search account.", DialogText, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return false;
                    }
                }
            }

            return true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            // temp disabled button
            frmRecords record = new frmRecords();
            record.CopyLotStrucOwner("CopyStruc");
        }

        public bool ValidateData()
        {
            if (string.IsNullOrEmpty(LotAcctNo))
            {
                Accounts account = new Accounts();
                account.CreateAccount(txtLastName.Text.ToString(), txtFirstName.Text.ToString(),
                    txtMI.Text.ToString(), txtStreet.Text.ToString(), txtHseNo.Text.ToString(),
                    txtLotNo.Text.ToString(), txtBlkNo.Text.ToString(), cmbBrgy.Text.ToString(),
                    txtMun.Text.ToString(), txtProv.Text.ToString(), txtZIP.Text.ToString(),
                    txtTIN.Text.ToString(), txtTCT.Text.ToString(), txtCTC.Text.ToString(), txtTelNo.Text.ToString());

                LotAcctNo = account.OwnerCode;

                if (string.IsNullOrEmpty(LotAcctNo))
                {
                    MessageBox.Show("Cannot create account record", DialogText, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }

            return true;
        }

        private void txtLastName_Leave(object sender, EventArgs e)
        {
            if (!SearchAccount())
                return;
        }

        private void txtFirstName_Leave(object sender, EventArgs e)
        {
            if (!SearchAccount())
                return;
        }

        public void EnableFormControls(bool blnEnable)
        {
            EnableControls(blnEnable);
        }
    }
}
