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
using Common.DataConnector;

namespace Modules.Transactions
{
    public partial class frmStrucOwner : Form
    {
        public string SourceClass { get; set; }
        public string DistCode { get; set; }
        public string StrucAcctNo { get; set; }
        public string DialogText { get; set; }

        public OwnerInfo ownerinfo = new OwnerInfo();

        public string sLastName
        { get { return txtLastName.Text; } }

        public Button ButtonLotOwner
        {
            get { return this.btnCopy; }
            set { this.btnCopy = value; }
        }

        public frmStrucOwner()
        {
            InitializeComponent();
        }

        private void frmStrucOwner_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btnCopy, "Use Lot Owner");

            ClearControls();

            PopulateBrgy();


            txtMun.Text = AppSettingsManager.GetConfigValue("02");
            txtProv.Text = AppSettingsManager.GetConfigValue("03");
            txtZIP.Text = AppSettingsManager.GetConfigValue("28");
        }

        public void ClearControls()
        {
            StrucAcctNo = string.Empty;
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
            this.txtMun.ReadOnly = !blnEnable;
            this.txtProv.ReadOnly = !blnEnable;
            this.txtZIP.ReadOnly = !blnEnable;
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

            StrucAcctNo = form.AcctNo;
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
            txtVillage.Text = form.Village;
            
        }

        private bool SearchAccount()
        {
            string sAcctNo = string.Empty;

            Utilities.Accounts acct = new Utilities.Accounts();
            acct.GetOwner(txtLastName.Text.Trim(), txtFirstName.Text.Trim(),txtMI.Text.Trim());
            sAcctNo = acct.OwnerCode;

            if (!string.IsNullOrEmpty(sAcctNo) && SourceClass != "NEW_VIEW")
            {
                if (string.IsNullOrEmpty(StrucAcctNo))
                {
                    MessageBox.Show("Record found having the same name of " + txtLastName.Text.Trim() + ", " + txtFirstName.Text.Trim() + ".\nPlease use search account.",DialogText,MessageBoxButtons.OK,MessageBoxIcon.Stop);
                    ClearControls();
                    return false;
                }
                else
                {
                    if (StrucAcctNo != sAcctNo)
                    {
                        MessageBox.Show("Record found having the same name of " + txtLastName.Text.Trim() + ", " + txtFirstName.Text.Trim() + ".\nPlease use search account.", DialogText, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        ClearControls();
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

        public bool ValidateOwner()
        {
            OracleResultSet result = new OracleResultSet();
            result.Query = "select * from owner_tmp";
            if(result.Execute())
                if(result.Read())
                {
                    return true;
                }
            return false;
        }

        private void CopyOwner(string AcctNo)
        {
            if (AcctNo != "")
            {
                OracleResultSet result = new OracleResultSet();
                result.Query = "INSERT INTO OWNER_TMP ";
                result.Query += $"VALUES ('{AcctNo}', ";
                result.Query += $"'{txtLastName.Text}', ";
                result.Query += $"'{txtFirstName.Text}', ";
                result.Query += $"'{txtMI.Text}', ";
                result.Query += $"'{txtHseNo.Text}', ";
                result.Query += $"'{txtLotNo.Text}', ";
                result.Query += $"'{txtBlkNo.Text}', ";
                result.Query += $"'{txtStreet.Text}', ";
                result.Query += $"'{cmbBrgy.Text}', ";
                result.Query += $"'{txtMun.Text}', ";
                result.Query += $"'{txtProv.Text}', ";
                result.Query += $"'{txtZIP.Text}', ";
                result.Query += $"'{txtTCT.Text}', ";
                result.Query += $"'{txtTIN.Text}', ";
                result.Query += $"'{txtCTC.Text}', ";
                result.Query += $"'{txtTelNo.Text}', ";
                result.Query += $"'')";
                result.ExecuteNonQuery();
                result.Close();
            }
            else
                return;
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            OracleResultSet result = new OracleResultSet();
            OracleResultSet result2 = new OracleResultSet();
            if (!ValidateOwner() && StrucAcctNo != null)
            {
                result.Query = "INSERT INTO OWNER_TMP ";
                result.Query += $"VALUES ('{StrucAcctNo}', ";
                result.Query += $"'{txtLastName.Text.Trim()}', ";
                result.Query += $"'{txtFirstName.Text.Trim()}', ";
                result.Query += $"'{txtMI.Text.Trim()}', ";
                result.Query += $"'{txtHseNo.Text.Trim()}', ";
                result.Query += $"'{txtLotNo.Text.Trim()}', ";
                result.Query += $"'{txtBlkNo.Text.Trim()}', ";
                result.Query += $"'{txtStreet.Text.Trim()}', ";
                result.Query += $"'{cmbBrgy.Text.Trim()}', ";
                result.Query += $"'{txtMun.Text.Trim()}', ";
                result.Query += $"'{txtProv.Text.Trim()}', ";
                result.Query += $"'{txtZIP.Text.Trim()}', ";
                result.Query += $"'{txtTCT.Text.Trim()}', ";
                result.Query += $"'{txtTIN.Text.Trim()}', ";
                result.Query += $"'{txtCTC.Text.Trim()}', ";
                result.Query += $"'{txtTelNo.Text.Trim()}', ";
                result.Query += $"'',";
                result.Query += $"'{txtVillage.Text.Trim()}')"; //requested subdivision data
                result.ExecuteNonQuery();
                result.Close();

            }
            else
            {
                result.Query = "select * from owner_tmp";
                if (result.Execute())
                    if (result.Read())
                    {
                        StrucAcctNo = result.GetString("acct_code");
                        txtLastName.Text = result.GetString("acct_ln");
                        txtFirstName.Text = result.GetString("acct_fn");
                        txtMI.Text = result.GetString("acct_mi");
                        txtHseNo.Text = result.GetString("acct_hse_no");
                        txtLotNo.Text = result.GetString("acct_lot_no");
                        txtBlkNo.Text = result.GetString("acct_blk_no");
                        txtStreet.Text = result.GetString("acct_addr");
                        cmbBrgy.Text = result.GetString("acct_brgy");
                        txtMun.Text = result.GetString("acct_city");
                        txtProv.Text = result.GetString("acct_prov");
                        txtZIP.Text = result.GetString("acct_zip");
                        txtTCT.Text = result.GetString("acct_tct");
                        txtCTC.Text = result.GetString("acct_ctc");
                        txtTelNo.Text = result.GetString("acct_telno");

                        result2.Query = "delete from owner_tmp";
                        result2.ExecuteNonQuery();
                        result2.Close();

                    }
                    else
                    {
                        result.Query = "delete from owner_tmp";
                        result.ExecuteNonQuery();
                        result.Close();
                        CopyOwner(StrucAcctNo);
                    }
            }
            // temp disabled button
            // frmRecords record = new frmRecords();
            //ownerinfo.LastName = txtLastName.Text;
            //record.Fname = txtFirstName.Text;
            //record.MiName = txtMI.Text;
            //record.TelNo = txtTelNo.Text;
            //record.TIN = txtTIN.Text;
            //record.TCT = txtTCT.Text;
            //record.CTC = txtCTC.Text;
            //record.LotNo = txtLotNo.Text;
            //record.BlkNo = txtBlkNo.Text;
            //record.Addr = txtStreet.Text;
            //record.Brgy = cmbBrgy.Text;
            //record.Municipality = txtMun.Text;
            //record.Province = txtProv.Text;
            //record.ZIP = txtZIP.Text;
            //record.LastName = txtLastName.Text;

            //record.CopyLotStrucOwner("CopyLot");
        }

        public bool ValidateData()
        {
            //AFM 20220202 requested by cient - disabled some validations
            if (string.IsNullOrEmpty(txtLastName.Text.ToString().Trim()))
            {
                MessageBox.Show("Structure owner's Last name is required", DialogText, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(txtFirstName.Text.ToString().Trim()))
            {
                //MessageBox.Show("Structure owner's First name is required", DialogText, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //return false;
            }
            if (string.IsNullOrEmpty(txtMI.Text.ToString().Trim()))
            {
                //MessageBox.Show("Structure owner's Middle Inital is required", DialogText, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //return false;
            }

            if (string.IsNullOrEmpty(txtTCT.Text.ToString().Trim()))
            {
                //MessageBox.Show("Structure owner's TCT/OCT no. is required", DialogText, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //return false;
            }

            if (string.IsNullOrEmpty(txtCTC.Text.ToString().Trim()))
            {
                //MessageBox.Show("Structure owner's CTC no. is required", DialogText, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //return false;
            }

            if (string.IsNullOrEmpty(txtCTC.Text.ToString().Trim()))
            {
                //MessageBox.Show("Structure owner's CTC no. is required", DialogText, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //return false;
            }

            if (string.IsNullOrEmpty(txtHseNo.Text.ToString().Trim()))
            {
                //MessageBox.Show("Structure owner's House no. is required", DialogText, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //return false;
            }


            if (string.IsNullOrEmpty(txtLotNo.Text.ToString().Trim()))
            {
                //MessageBox.Show("Structure owner's Lot no. is required", DialogText, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //return false;
            }

            if (string.IsNullOrEmpty(txtBlkNo.Text.ToString().Trim()))
            {
                //MessageBox.Show("Structure owner's Blk no. is required", DialogText, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //return false;
            }

            if (string.IsNullOrEmpty(txtStreet.Text.ToString().Trim()))
            {
                //MessageBox.Show("Structure owner's Street is required", DialogText, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //return false;
            }
            if (string.IsNullOrEmpty(txtVillage.Text.ToString().Trim()))
            {
                //MessageBox.Show("Structure owner's Subdivision is required", DialogText, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //return false;
            }

            if (string.IsNullOrEmpty(StrucAcctNo))
            {
                Accounts account = new Accounts();
                account.CreateAccount(txtLastName.Text.ToString(), txtFirstName.Text.ToString(),
                    txtMI.Text.ToString(), txtStreet.Text.ToString(), txtHseNo.Text.ToString(),
                    txtLotNo.Text.ToString(), txtBlkNo.Text.ToString(), cmbBrgy.Text.ToString().ToUpper(),
                    txtMun.Text.ToString(), txtProv.Text.ToString(), txtZIP.Text.ToString(),
                    txtTIN.Text.ToString(), txtTCT.Text.ToString(), txtCTC.Text.ToString(), txtTelNo.Text.ToString(), txtVillage.Text.ToString().Trim()); //added requested subdivision

                StrucAcctNo = account.OwnerCode;

                if(string.IsNullOrEmpty(StrucAcctNo))
                {
                    MessageBox.Show("Cannot create account record",DialogText,MessageBoxButtons.OK,MessageBoxIcon.Stop);
                    return false;
                }
            }
            return true;
        }

        private void txtFirstName_Leave(object sender, EventArgs e)
        {
            if (!SearchAccount())
                return;
        }

        private void txtLastName_Leave(object sender, EventArgs e)
        {
            if (!SearchAccount())
                return;
        }

        public void EnableFormControls(bool blnEnable)
        {
            EnableControls(blnEnable);
        }

        private void txtHseNo_Leave(object sender, EventArgs e)
        {

        }
        //private void cmbBrgy_KeyPress(object sender, KeyPressEventArgs e) //removed to allow input for owners outside binan
        //{
        //    e.Handled = true;
        //}
    }
}
