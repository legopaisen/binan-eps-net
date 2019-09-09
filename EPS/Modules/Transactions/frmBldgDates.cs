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
using EPSEntities.Connection;

namespace Modules.Transactions
{
    public partial class frmBldgDates : Form
    {
        public static ConnectionString dbConn = new ConnectionString();
        private DataGridViewComboBoxColumn comboPermit = new DataGridViewComboBoxColumn();
        Rectangle _Rectangle;
        DateTimePicker dtDateStart = new DateTimePicker();
        DateTimePicker dtDateComp = new DateTimePicker();
        PermitList lstPermit = new PermitList(null);

        public string SourceClass { get; set; }
        public string DistCode { get; set; }
        public string DialogText { get; set; }
        public double ActualCost { get; set; }
        public string PermitAppl { get; set; }

        public bool permitIsNull = false;

        public frmBldgDates()
        {
            InitializeComponent();
        }

        private void frmBldgDates_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btnAdd, "Add row");
            toolTip2.SetToolTip(btnRemove, "Remove row");

            ActualCost = 0;
            if (SourceClass == "Delete")
                EnableControls(false);

            ClearControls();

            PopulateMaterials();

            LoadGrid();
        }

        private void EnableControls(bool blnEnable)
        {
            this.txtPIN.ReadOnly = !blnEnable;
            this.txtBldgName.ReadOnly = !blnEnable;
            this.txtHeight.ReadOnly = !blnEnable;
            this.txtArea.ReadOnly = !blnEnable;
            this.txtCost.ReadOnly = !blnEnable;
            this.txtStoreys.ReadOnly = !blnEnable;
            this.txtUnits.ReadOnly = !blnEnable;
            this.txtArea.ReadOnly = !blnEnable;
            this.dgvList.Enabled = blnEnable;
            this.cmbMaterials.Enabled = blnEnable;
        }

        public void ClearControls()
        {
            this.txtPIN.Text = "";
            this.txtBldgName.Text = "";
            this.txtHeight.Text = "";
            this.txtArea.Text = "";
            this.txtCost.Text = "";
            this.txtStoreys.Text = "";
            this.txtUnits.Text = "";
            this.txtBldgNo.Text = "";
            dgvList.Rows.Clear();

        }


        private void PopulateMaterials()
        {
            cmbMaterials.Items.Clear();

            MaterialList lstMaterial = new MaterialList();

            int iCnt = lstMaterial.MaterialLst.Count;

            DataTable dataTable = new DataTable("Material");
            dataTable.Columns.Clear();
            dataTable.Columns.Add("Code", typeof(String));
            dataTable.Columns.Add("Desc", typeof(String));

            dataTable.Rows.Add(new String[] { "", "" });

            for (int i = 0; i < iCnt; i++)
            {
                dataTable.Rows.Add(new String[] { lstMaterial.MaterialLst[i].Code, lstMaterial.MaterialLst[i].Desc });
            }

            cmbMaterials.DataSource = dataTable;
            cmbMaterials.DisplayMember = "Desc";
            cmbMaterials.ValueMember = "Desc";
            cmbMaterials.SelectedIndex = 0;
        }

        public void LoadGrid()
        {
            comboPermit.HeaderCell.Value = "Permit";
            dtDateStart = new DateTimePicker();
            dtDateStart.Format = DateTimePickerFormat.Short;
            //dtDateStart.Text = "StartDate";
            dtDateComp = new DateTimePicker();
            dtDateComp.Format = DateTimePickerFormat.Short;
            //dtDateComp.Text = "CompletionDate";

            dgvList.Rows.Clear();
            dgvList.Columns.Clear();

            dgvList.Columns.Add("PermitCode", "");

            dgvList.Columns.Insert(1, comboPermit);
            PopulatePermit();
            dgvList.Columns.Add("PermitNo", "Permit No.");

            if (SourceClass == "NEW_ADD" || SourceClass == "NEW_EDIT" || SourceClass == "NEW_VIEW"
                || SourceClass == "REN_ADD" || SourceClass == "REN_EDIT" || SourceClass == "REN_VIEW")
            {
                dgvList.Columns.Add("StartDate", "Prop. Start Date");
                dgvList.Columns.Add("CompletionDate", "Prop. Completion Date");
                dgvList.Columns[2].Visible = false;
            }
            else
            {
                dgvList.Columns.Add("StartDate", "Start Date");
                dgvList.Columns.Add("CompletionDate", "Completion Date");
            }
            
            dgvList.Controls.Add(dtDateStart);
            dgvList.Controls.Add(dtDateComp);
            dtDateStart.Visible = false;
            dtDateComp.Visible = false;

            dtDateStart.TextChanged += new EventHandler(dtDateStart_TextChange);
            dtDateComp.TextChanged += new EventHandler(dtDateComp_TextChange);

            dgvList.RowHeadersVisible = false;
            dgvList.Columns[0].Visible = false;
            dgvList.Columns[1].Width = 150;
            dgvList.Columns[2].Width = 130;
            dgvList.Columns[3].Width = 140;
            dgvList.Columns[4].Width = 140;

            //dgvList.Columns[2].DefaultCellStyle.Format = "MM/dd/yyyy";
            // dgvList.Columns[3].DefaultCellStyle.Format = "MM/dd/yyyy";

        }

        private void PopulatePermit()
        {
            comboPermit.DataSource = null;
            comboPermit.Items.Clear();

            lstPermit = new PermitList(null);

            int iCnt = lstPermit.PermitLst.Count;

            DataTable dataTable = new DataTable("Permit");
            dataTable.Columns.Clear();
            dataTable.Columns.Add("PermitCode", typeof(String));
            dataTable.Columns.Add("PermitDesc", typeof(String));

            dataTable.Rows.Add(new String[] { "", "" });

            for (int i = 0; i < iCnt; i++)
            {
                dataTable.Rows.Add(new String[] { lstPermit.PermitLst[i].PermitCode, lstPermit.PermitLst[i].PermitDesc });
            }

            comboPermit.DataSource = dataTable;
            comboPermit.DisplayMember = "PermitDesc";
            comboPermit.ValueMember = "PermitDesc";

        }

        //dgvList_CellLeave
        private void dgvList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (permitIsNull != false)
            {
                dgvList[2, e.RowIndex].Value = null;
            }
            try
            {
                if (e.ColumnIndex == 1)
                {
                    //validate duplicate permit
                    string sPermit = dgvList[1, e.RowIndex].Value.ToString();

                    for (int i = 0; i < dgvList.Rows.Count; i++)
                    {
                        string sPermitTmp = string.Empty;

                        if (i != e.RowIndex)
                        {
                            sPermitTmp = dgvList[1, i].Value.ToString();

                            if (sPermitTmp == sPermit)
                            {
                                MessageBox.Show("Duplicate Permit type", DialogText, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                dgvList[1, e.RowIndex].Value = "";
                                return;
                            }
                        }
                    }

                    dgvList[0, e.RowIndex].Value = lstPermit.GetPermitCode(dgvList[1, e.RowIndex].Value.ToString());

                    if(SourceClass == "ENG_REC_ADD" || SourceClass == "ENG_REC_EDIT")
                    {
                        dgvList.Rows.Add("");
                    }
                }

                //if (e.ColumnIndex == 2) //AFM 20190906 permit no. format validation
                //{
                //    if (Convert.ToString(dgvList[2, e.RowIndex]).Length != 11 || Convert.ToString(dgvList[e.ColumnIndex, e.RowIndex]).Length != 12)
                //    {
                //        MessageBox.Show("Invalid Permit Format (e.g. BP-19-00001)");
                //        dgvList[2, e.RowIndex].Value = null;
                //        return;
                //    }
                //}



                if (e.ColumnIndex == 3)
                {
                    string sStart = string.Empty;
                    try
                    {
                        sStart = dgvList[e.ColumnIndex, e.RowIndex].Value.ToString();
                        dgvList[e.ColumnIndex, e.RowIndex].Value = sStart;
                    }
                    catch {
                        dgvList[e.ColumnIndex, e.RowIndex].Value = "";
                    }
                }
                if (e.ColumnIndex == 4)
                {
                    string sComplete = string.Empty;
                    try
                    {
                        sComplete = dgvList[e.ColumnIndex, e.RowIndex].Value.ToString();
                        dgvList[e.ColumnIndex, e.RowIndex].Value = sComplete;
                    }
                    catch {
                        dgvList[e.ColumnIndex, e.RowIndex].Value = "";
                    }
                }
            }
            catch { }
        }

        private void dgvList_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            permitIsNull = false;
            if (e.ColumnIndex == 2) //AFM 20190906 permit no. format validation
            {
                if (dgvList[2, e.RowIndex].EditedFormattedValue != null)
                {
                    dgvList[2, e.RowIndex].Value = dgvList[2, e.RowIndex].EditedFormattedValue;
                    if (Convert.ToString(dgvList[2, e.RowIndex].Value).Length != 11 && Convert.ToString(dgvList[2, e.RowIndex].Value).Length != 12 || !Convert.ToString(dgvList[2, e.RowIndex].Value).Contains("BP") && !Convert.ToString(dgvList[2, e.RowIndex].Value).Contains("FP") && !Convert.ToString(dgvList[2, e.RowIndex].Value).Contains("EP") && !Convert.ToString(dgvList[2, e.RowIndex].Value).Contains("DP") && !Convert.ToString(dgvList[2, e.RowIndex].Value).Contains("SP") && !Convert.ToString(dgvList[2, e.RowIndex].Value).Contains("ECP") && !Convert.ToString(dgvList[2, e.RowIndex].Value).Contains("AP") && !Convert.ToString(dgvList[2, e.RowIndex].Value).Contains("OP") && !Convert.ToString(dgvList[2, e.RowIndex].Value).Contains("WP") && !Convert.ToString(dgvList[2, e.RowIndex].Value).Contains("MP"))
                    {
                        MessageBox.Show("Invalid Permit Format (e.g. BP-19-00001)");
                        dgvList[2, e.RowIndex].Value = null;
                        permitIsNull = true;
                        return;
                    }
                }
            }

            try
            {
                if (e.ColumnIndex == 2)
                {
                    int iCnt = dgvList[2, e.RowIndex].Value.ToString().Length;
                    //for (iCnt = 0; );
                    //format data entry yyyy-mm-series 2003-06-000001
                }
            }
            catch { }
        }





        //private void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        //{
        //    if (e.ColumnIndex == 2) //AFM 20190906 permit no. format validation
        //    {
        //        if (Convert.ToString(dgvList[2, e.RowIndex]).Length != 11 || Convert.ToString(dgvList[e.ColumnIndex, e.RowIndex]).Length != 12)
        //        {
        //            MessageBox.Show("Invalid Permit Format (e.g. BP-19-00001)");
        //            dgvList[2, e.RowIndex].Value = null;
        //            return;
        //        }
        //    }

            //    try
            //        {
            //        if (e.ColumnIndex == 2)
            //        {
            //            int iCnt = dgvList[2, e.RowIndex].Value.ToString().Length; //AFM 20190909 get value
            //            //for (iCnt = 0; );
            //            //format data entry yyyy-mm-series 2003-06-000001
            //        }
            //    }
            //    catch { }
            //}


        private void dgvList_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (SourceClass != "NEW_ADD" && SourceClass != "NEW_EDIT")
                {
                    if (e.KeyCode == Keys.Delete)
                    {
                        if (MessageBox.Show("Delete entry?", "EPS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            dgvList.Rows.RemoveAt(dgvList.SelectedCells[0].RowIndex);

                        }
                    }
                }
            }
            catch { }
        }

        private void btnBldgUnits_Click(object sender, EventArgs e)
        {
            frmBuildingUnits form = new frmBuildingUnits();
            form.BldgNo = txtBldgNo.Text;
            form.DistCode = DistCode;
            form.ShowDialog();
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                switch (e.ColumnIndex)
                {
                    case 1:
                        if ((SourceClass == "NEW_ADD" || SourceClass == "NEW_EDIT"
                            || SourceClass == "REN_ADD" || SourceClass == "REN_EDIT")
                            && e.RowIndex == 0)
                        {
                            dgvList.ReadOnly = true;
                        }
                        else
                        {
                            dgvList.ReadOnly = false;
                        }
                        break;
                    case 3:

                        _Rectangle = dgvList.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true); //  
                        dtDateStart.Size = new Size(_Rectangle.Width, _Rectangle.Height); //  
                        dtDateStart.Location = new Point(_Rectangle.X, _Rectangle.Y); //  
                        dtDateStart.Visible = true;
                        dgvList.CurrentCell.Value = dtDateStart.Text.ToString();
                        break;

                    case 4:

                        _Rectangle = dgvList.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true); //  
                        dtDateComp.Size = new Size(_Rectangle.Width, _Rectangle.Height); //  
                        dtDateComp.Location = new Point(_Rectangle.X, _Rectangle.Y); //  
                        dtDateComp.Visible = true;
                        dgvList.CurrentCell.Value = dtDateComp.Text.ToString();
                        break;

                }
            }
        }

        private void dtDateStart_TextChange(object sender, EventArgs e)
        {
            dgvList.CurrentCell.Value = dtDateStart.Text.ToString();
        }

        private void dtDateComp_TextChange(object sender, EventArgs e)
        {
            dgvList.CurrentCell.Value = dtDateComp.Text.ToString();
        }

        private void dgvList_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                dtDateStart.Visible = false;
                dtDateComp.Visible = false;
            }
            catch { }
        }

        public bool ValidateData()
        {
            double dCostTmp = 0;
            double.TryParse(txtCost.Text.ToString(), out dCostTmp);

            if (dCostTmp < 0)
            {
                MessageBox.Show("Estimated cost is required", DialogText, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            int iNoTmp = 0;
            int.TryParse(txtUnits.Text.ToString(), out iNoTmp);
            if(iNoTmp == 0)
            {
                txtUnits.Text = "0";
            }

            int.TryParse(txtStoreys.Text.ToString(), out iNoTmp);
            if(iNoTmp == 0)
            {
                txtStoreys.Text = "0";
            }

            double.TryParse(txtArea.Text.ToString(), out dCostTmp);

            if (dCostTmp < 0)
            {
                MessageBox.Show("Floor area is required", DialogText, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (string.IsNullOrEmpty(txtBldgNo.Text.ToString()))
            {
                string strQuery = string.Empty;
                var db = new EPSConnection(dbConn);
                strQuery = $"select max(bldg_no) from building";
                iNoTmp = db.Database.SqlQuery<Int32>(strQuery).SingleOrDefault();

                if (iNoTmp == 0)
                    txtBldgNo.Text = "1";
                else
                    txtBldgNo.Text = string.Format("{0:###}", iNoTmp + 1);
            }

            double.TryParse(txtAssVal.Text.ToString(), out dCostTmp);

            if (dCostTmp < 0)
            {
                MessageBox.Show("Assessed Value is required", DialogText, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if(dgvList.Rows.Count == 0)
            {
                MessageBox.Show("Approved permit/s is/are required", DialogText, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if(string.IsNullOrEmpty(cmbMaterials.Text.ToString()))
            {
                MessageBox.Show("Materials used is required", DialogText, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;

            }

            for(int i = 0; i < dgvList.Rows.Count; i++)
            {
                string sPermit = string.Empty;
                string sPermitNo = string.Empty;
                string sStart = string.Empty;
                string sCompleted = string.Empty;
                try
                {
                    try { 
                    sPermit = dgvList[1, i].Value.ToString();
                    }
                    catch { }
                    try { sPermitNo = dgvList[2, i].Value.ToString();
                    }
                    catch { }
                    try { sStart = dgvList[3, i].Value.ToString();
                    }
                    catch { }
                    try { 
                    sCompleted = dgvList[4, i].Value.ToString();
                    }
                    catch { }

                    if (string.IsNullOrEmpty(sStart) || string.IsNullOrEmpty(sCompleted))
                    {
                        MessageBox.Show("Please enter date of approved Permit/s", DialogText, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return false;
                    }
                }
                catch { }
            }

            return true;
        }

        private void cmbMaterials_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbMaterials.Text.ToString()))
            {
                MaterialList material = new MaterialList();
                if (string.IsNullOrEmpty(material.GetMaterialCode(cmbMaterials.Text.ToString())))
                {
                    MessageBox.Show("Material not found in list", DialogText, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    cmbMaterials.Text = "";
                    cmbMaterials.Focus();
                    return;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            dgvList.Rows.Add();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int iRows = 0;
            iRows = dgvList.Rows.Count;

            try
            {
                dgvList.Rows.RemoveAt(iRows-1);
                dtDateStart.Visible = false;
                dtDateComp.Visible = false;
            }
            catch { }
        }

        public void EnableFormControls(bool blnEnable)
        {
            EnableControls(blnEnable);
        }

        private void txtCost_Leave(object sender, EventArgs e)
        {
            double dValue = 0;

            double.TryParse(txtCost.Text.ToString(), out dValue);
            txtCost.Text = string.Format("{0:#,###.00}", dValue);
        }

        private void txtArea_Leave(object sender, EventArgs e)
        {
            double dValue = 0;

            double.TryParse(txtArea.Text.ToString(), out dValue);
            txtArea.Text = string.Format("{0:#,###.00}", dValue);
        }

        private void txtAssVal_Leave(object sender, EventArgs e)
        {
            double dValue = 0;

            double.TryParse(txtAssVal.Text.ToString(), out dValue);
            txtAssVal.Text = string.Format("{0:#,###.00}", dValue);
        }
    }
}
