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
using RPTEntities;
using System.Diagnostics;

namespace Modules.Transactions
{
    public partial class frmProjectInfo : Form
    {
        private string m_sDistCode = string.Empty;
        private string m_sCategoryCode = string.Empty;
        public string SourceClass { get; set; }
        public string DialogText { get; set; }

        //ComboBox cmbBrgyCode = new ComboBox();
        //public MultiColumnComboBoxDemo.MultiColumnComboBox cmbBrgyCode;
        public ComboBox cmbBrgyCode = new ComboBox();


        private static string sBrgyCode;

        public static string BrgyCode
        {
            get { return sBrgyCode; }
            set { sBrgyCode = value; }
        }


        public string DistCode
        {
            get { return m_sDistCode; }
            set { m_sDistCode = value; }
        }
                       
        public frmProjectInfo()
        {
            InitializeComponent();
        }

        private void frmProjectInfo_Load(object sender, EventArgs e)
        {
            if(AppSettingsManager.GetConfigValue("25") == "Y" || AppSettingsManager.GetConfigValue("25") == "1")
            {
                btnSearchLot.Enabled = true;
                EnableLocationControls(false);
            }
            else
            {
                btnSearchLot.Enabled = false;
                EnableLocationControls(true);
            }
            PopulateBrgy();
            PopulateCategory();
            
            PopulateOccupancy(null);
            PopulateStructure();
            PopulateBusiness();
            ClearLocationControls();

            txtMun.Text = AppSettingsManager.GetConfigValue("02");
            txtProv.Text = AppSettingsManager.GetConfigValue("03");

            txtZIP.Text = AppSettingsManager.GetConfigValue("28");
        }

        public void ClearControls()
        {
            ClearProjectInfo();
            ClearLocationControls();
        }

        private void ClearProjectInfo()
        {
            txtProjDesc.Text = string.Empty;
            txtMemo.Text = string.Empty;
            cmbCategory.Text = string.Empty;
            cmbOccupancy.Text = string.Empty;
            cmbBussKind.Text = string.Empty;
            cmbOwnership.Text = string.Empty;
            cmbStrucType.Text = string.Empty;
        }

        private void PopulateBrgy()
        {
            cmbBrgy.Items.Clear();
            cmbBrgyCode = new MultiColumnComboBoxDemo.MultiColumnComboBox();

            DataTable dataTable = new DataTable("Barangay");
            dataTable.Columns.Clear();
            dataTable.Columns.Add("Code", typeof(String));
            dataTable.Columns.Add("Desc", typeof(String));
            dataTable.Columns.Add("Dist", typeof(String));

            BarangayList lstBrgy = new BarangayList();
            lstBrgy.GetBarangayList(m_sDistCode, true);
            int intCount = lstBrgy.BarangayNames.Count;
            for (int i = 0; i < intCount; i++)
            {
                dataTable.Rows.Add(new String[] { lstBrgy.BarangayCodes[i], lstBrgy.BarangayNames[i], lstBrgy.DistrictCodes[i], });
            }

            //cmbBrgyCode.DataSource = dataTable;
            //cmbBrgyCode.DisplayMember = "Dist";
            //cmbBrgyCode.ValueMember = "Dist";

            cmbBrgy.DataSource = dataTable;
            cmbBrgy.DisplayMember = "Desc";
            cmbBrgy.ValueMember = "Dist"; //get district code
            cmbBrgy.SelectedIndex = -1;

        }

        private void PopulateCategory()
        {
            cmbCategory.Items.Clear();

            CategoryList lstCategory = new CategoryList();

            int iCnt = lstCategory.CategoryLst.Count;

            DataTable dataTable = new DataTable("Category");
            dataTable.Columns.Clear();
            dataTable.Columns.Add("Code", typeof(String));
            dataTable.Columns.Add("Desc", typeof(String));

            dataTable.Rows.Add(new String[] { "", "" });

            for (int i = 0; i < iCnt; i++)
            {
                dataTable.Rows.Add(new String[] { lstCategory.CategoryLst[i].Code, lstCategory.CategoryLst[i].Desc });
            }

            cmbCategory.DataSource = dataTable;
            cmbCategory.DisplayMember = "Desc";
            cmbCategory.ValueMember = "Desc";
            cmbCategory.SelectedIndex = 0;

        }

        private void PopulateOccupancy(string sCategory)
        {
            cmbOccupancy.DataBindings.Clear();
            cmbOccupancy.DataSource = null;
            cmbOccupancy.Items.Clear(); //error here

            OccupancyList lstOccupancy = new OccupancyList(sCategory, null);

            int iCnt = lstOccupancy.OccupancyLst.Count;

            DataTable dataTable = new DataTable("Occupancy");
            dataTable.Columns.Clear();
            
            dataTable.Columns.Add("Code", typeof(String));
            dataTable.Columns.Add("Desc", typeof(String));
            
            dataTable.Rows.Add(new String[] { "", "" });

            for (int i = 0; i < iCnt; i++)
            {
                dataTable.Rows.Add(new String[] { lstOccupancy.OccupancyLst[i].Code, lstOccupancy.OccupancyLst[i].Desc });
            }

            cmbOccupancy.DataSource = dataTable;
            cmbOccupancy.DisplayMember = "Desc";
            cmbOccupancy.ValueMember = "Desc";
            cmbOccupancy.SelectedIndex = 0;
        }

        private void PopulateStructure()
        {
            cmbStrucType.Items.Clear();

            StructureList lstStructure = new StructureList();

            int iCnt = lstStructure.StructureLst.Count;

            DataTable dataTable = new DataTable("Structure");
            dataTable.Columns.Clear();
            dataTable.Columns.Add("Code", typeof(String));
            dataTable.Columns.Add("Desc", typeof(String));
            
            dataTable.Rows.Add(new String[] { "", "" });

            for (int i = 0; i < iCnt; i++)
            {
                dataTable.Rows.Add(new String[] { lstStructure.StructureLst[i].Code, lstStructure.StructureLst[i].Desc });
            }

            cmbStrucType.DataSource = dataTable;
            cmbStrucType.DisplayMember = "Desc";
            cmbStrucType.ValueMember = "Desc";
            cmbStrucType.SelectedIndex = 0;
        }

        private void PopulateBusiness()
        {
            cmbBussKind.Items.Clear();

            BusinessList lstBusiness = new BusinessList();

            int iCnt = lstBusiness.BusinessLst.Count;

            DataTable dataTable = new DataTable("Business");
            dataTable.Columns.Clear();
            dataTable.Columns.Add("Code", typeof(String));
            dataTable.Columns.Add("Desc", typeof(String));

            dataTable.Rows.Add(new String[] { "", "" });

            for (int i = 0; i < iCnt; i++)
            {
                dataTable.Rows.Add(new String[] { lstBusiness.BusinessLst[i].Code, lstBusiness.BusinessLst[i].Desc });
            }

            cmbBussKind.DataSource = dataTable;
            cmbBussKind.DisplayMember = "Desc";
            cmbBussKind.ValueMember = "Desc";
            cmbBussKind.SelectedIndex = 0;
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbCategory_SelectedValueChanged(object sender, EventArgs e)
        {
            m_sCategoryCode = ((DataRowView)this.cmbCategory.SelectedItem)["Code"].ToString();

            PopulateOccupancy(m_sCategoryCode);
        }

        private void btnSearchLot_Click(object sender, EventArgs e)
        {
            ClearLocationControls();

            frmSearchLot form = new frmSearchLot();
            form.ShowDialog();
            txtPIN.Text = form.LotPIN;
            txtLotNo.Text = form.LotNo;
            txtBlkNo.Text = form.BlkNo;
            cmbBrgy.Text = form.BrgyName;
            txtMun.Text = form.City;
            
        }

        private void ClearLocationControls()
        {
            txtPIN.Text = string.Empty;
            txtHseNo.Text = string.Empty;
            txtLotNo.Text = string.Empty;
            txtBlkNo.Text = string.Empty;
            txtStreet.Text = string.Empty;
            cmbBrgy.Text = string.Empty;
            //txtMun.Text = string.Empty;
            //txtProv.Text= string.Empty;
            //txtZIP.Text = string.Empty;
        }

        private void EnableLocationControls(bool blnEnable)
        {
            txtHseNo.ReadOnly = !blnEnable;
            txtLotNo.ReadOnly = !blnEnable;
            txtBlkNo.ReadOnly = !blnEnable;
            txtStreet.ReadOnly = !blnEnable;
            cmbBrgy.Enabled = blnEnable;
            txtMemo.ReadOnly = !blnEnable;
            //txtMun.ReadOnly = !blnEnable;
            //txtProv.ReadOnly = !blnEnable;
            //txtZIP.ReadOnly = !blnEnable;
        }

        private void EnableOtherControls(bool blnEnable)
        {
            txtProjDesc.ReadOnly = !blnEnable;
            txtMemo.ReadOnly = !blnEnable;
            cmbStrucType.Enabled = blnEnable;
            cmbBussKind.Enabled = blnEnable;
            cmbOccupancy.Enabled = blnEnable;
            cmbCategory.Enabled = blnEnable;
            cmbOwnership.Enabled = blnEnable;
        }

        public bool ValidateData()
        {
            if(string.IsNullOrEmpty(txtProjDesc.Text.ToString()))
            {
                MessageBox.Show("Project description is required",DialogText,MessageBoxButtons.OK,MessageBoxIcon.Stop);
                return false;
            }
            if(string.IsNullOrEmpty(cmbCategory.Text.ToString()))
            {
                MessageBox.Show("Category is required", DialogText, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if(string.IsNullOrEmpty(cmbOccupancy.Text.ToString()))
            {
                MessageBox.Show("Occupancy type is required", DialogText, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(cmbStrucType.Text.ToString()))
            {
                MessageBox.Show("Structure type is required", DialogText, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (AppSettingsManager.GetConfigValue("25") == "Y" || AppSettingsManager.GetConfigValue("25") == "1")
            {
                if (string.IsNullOrEmpty(txtPIN.Text.ToString()))
                {
                    MessageBox.Show("Please specify Project lot PIN.", DialogText, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }
            return true;
        }

        private void cmbCategory_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbCategory.Text.ToString()))
            {
                CategoryList category = new CategoryList();

                if (string.IsNullOrEmpty(category.GetCategoryCode(cmbCategory.Text.ToString())))
                {
                    MessageBox.Show("Category not found in list", DialogText, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    cmbCategory.Text = "";
                    cmbCategory.Focus();
                    return;
                }
            }
        }

        private void cmbOccupancy_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbOccupancy.Text.ToString()))
            {
                OccupancyList occupancy = new OccupancyList(null,null);
                if (string.IsNullOrEmpty(occupancy.GetOccupancyCode(cmbOccupancy.Text.ToString())))
                {
                    MessageBox.Show("Occupancy not found in list", DialogText, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    cmbOccupancy.Text = "";
                    cmbOccupancy.Focus();
                    return;
                }
            }
        }

        private void cmbOwnership_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbOwnership.Text.ToString()))
            {
                ComboBox cb = (ComboBox)sender;
                if (!cb.Items.Contains(cb.Text))
                {
                    MessageBox.Show("Form of Ownership not found in list", DialogText, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    cmbOwnership.Text = "";
                    cmbOwnership.Focus();
                    return;

                }
            }
        }

        private void cmbStrucType_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbStrucType.Text.ToString()))
            {
                StructureList structure = new StructureList();

                if (string.IsNullOrEmpty(structure.GetStructureCode(cmbStrucType.Text.ToString())))
                {
                    MessageBox.Show("Structure not found in list", DialogText, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    cmbStrucType.Text = "";
                    cmbStrucType.Focus();
                    return;
                }
            }
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        public void EnableFormControls(bool blnEnable)
        {
            EnableLocationControls(blnEnable);
            EnableOtherControls(blnEnable);
        }

        private void cmbBrgy_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = true;
        }

        private void cmbBrgy_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cmbBrgyCode.Text = this.cmbBrgy.GetItemText(this.cmbBrgy.SelectedItem);
            sBrgyCode = cmbBrgy.SelectedValue?.ToString();
        }

        private void txtHseNo_Leave(object sender, EventArgs e)
        {
        }

        private void btnIRR_Click(object sender, EventArgs e)
        {
            Process.Start("IRR.pdf");
        }
    }
}
