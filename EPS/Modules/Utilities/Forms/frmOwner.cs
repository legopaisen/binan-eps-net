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

namespace Modules.Utilities
{
    public partial class frmOwner : Form
    {
        public string SourceClass { get; set; }
        public string DistCode { get; set; }
        public string AcctNo { get; set; }
        public string DialogText { get; set; }
        private FormOwnerClass RecordClass = null;

        public frmOwner()
        {
            InitializeComponent();
        }

        private void frmStrucOwner_Load(object sender, EventArgs e)
        {
            ClearControls();

            PopulateBrgy();

            if (this.SourceClass == "OWNER")
            {
                RecordClass = new FormOwner(this);
            }
            else
                RecordClass = new FormEngr(this);

            RecordClass.FormLoad();
        }

        public void ClearControls()
        {
            AcctNo = string.Empty;
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
            this.txtMun.Text = string.Empty;
            this.txtProv.Text = string.Empty;
            this.txtZIP.Text = string.Empty;
            this.dtpValidDt.Value = AppSettingsManager.GetSystemDate();
            this.txtVillage.Text = string.Empty;
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
            this.cmbEngrType.Enabled = blnEnable;
            this.dtpValidDt.Enabled = blnEnable;
            this.dgvList.Enabled = !blnEnable;
            this.txtVillage.ReadOnly = !blnEnable; //requested subdivision
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
        
        

        private void txtFirstName_Leave(object sender, EventArgs e)
        {
            //if (!RecordClass.SearchAccount())
            if(txtFirstName.Text.Trim() != "") //AFM 20200826
            {
                if (!RecordClass.SearchAccount())
                    return;
            }

        }

        private void txtLastName_Leave(object sender, EventArgs e)
        {
            //if (!RecordClass.SearchAccount())
            if(txtLastName.Text.Trim() != "") //AFM 20200826
            {
                if (!RecordClass.SearchAccount())
                    return;
            }
            
        }

        
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(btnAdd.Text == "Add")
            {
                ClearControls();
                EnableControls(true);
                btnAdd.Text = "Save";
                btnCancel.Text = "Cancel";
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                txtLastName.Focus();

                //AFM 20200826
                txtMun.Text = AppSettingsManager.GetConfigValue("02");
                txtProv.Text = AppSettingsManager.GetConfigValue("03");
                txtZIP.Text = AppSettingsManager.GetConfigValue("26");
            }
            else
            {
                if (MessageBox.Show("Save record?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (!RecordClass.ValidateData())
                        return;

                    RecordClass.Save();

                    EnableControls(false);
                    btnAdd.Text = "Add";
                    btnCancel.Text = "Exit";
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;
                    
                }

                
            }
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            RecordClass.CellClick(sender, e);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if(btnEdit.Text == "Edit")
            {
                if(string.IsNullOrEmpty(AcctNo))
                {
                    MessageBox.Show("Select record to edit",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Stop);
                    return;
                }

                btnEdit.Text = "Update";
                btnCancel.Text = "Cancel";
                btnAdd.Enabled = false;
                btnDelete.Enabled = false;
                EnableControls(true);
                txtLastName.Focus();
            }
            else
            {
                if (MessageBox.Show("Update record?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (!RecordClass.ValidateData())
                        return;

                    RecordClass.Save();

                    EnableControls(false);
                    btnEdit.Text = "Edit";
                    btnCancel.Text = "Exit";
                    btnAdd.Enabled = true;
                    btnDelete.Enabled = true;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if(btnCancel.Text == "Cancel")
            {
                btnCancel.Text = "Exit";
                btnAdd.Text = "Add";
                btnEdit.Text = "Edit";
                btnAdd.Enabled = true;
                btnDelete.Enabled = true;
                btnEdit.Enabled = true;
                ClearControls();
                EnableControls(false);
            }
            else
            {
                this.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(AcctNo))
            {
                MessageBox.Show("Select record to delete first", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else
            {
                if (MessageBox.Show("Are you sure you want to delete this record?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    RecordClass.Delete();

                    EnableControls(false);
                }
            }
        }
    }
}
