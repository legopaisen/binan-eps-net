using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EPSEntities.Connection;
using Oracle.ManagedDataAccess.Client;
using Common.StringUtilities;

namespace Modules.Utilities.Forms
{
    public partial class frmOccupancy : Form
    {
        public static ConnectionString dbConn = new ConnectionString();
        private string m_sCatCode = string.Empty;
        private string m_sOccCode = string.Empty;
        private string m_sOccDesc = string.Empty;

        public frmOccupancy()
        {
            InitializeComponent();
        }



        private void frmOccupancy_Load(object sender, EventArgs e)
        {
            PopulateCategory();
            UpdateList();
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
            cmbCategory.SelectedIndex = 1;
        }

        private void UpdateList()
        {
            dgvList.Rows.Clear();

            OccupancyList occ = new OccupancyList(m_sCatCode, null);
            int iCnt = occ.OccupancyLst.Count;

            for (int i = 0; i < iCnt; i++)
            {
                dgvList.Rows.Add(occ.OccupancyLst[i].Code, occ.OccupancyLst[i].Desc);
            }

        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_sCatCode = ((DataRowView)cmbCategory.SelectedItem)["Code"].ToString();

            UpdateList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;

            if (btnAdd.Text == "Add")
            {
                btnAdd.Text = "Save";
                btnCancel.Text = "Cancel";
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                txtCode.Text = "";
                txtOccupancy.Text = "";
                dgvList.Enabled = false;
                cmbCategory.Enabled = false;

                string sCode = string.Empty;
                int iCode = 0;
                try
                {
                    sQuery = $"select max(occ_code) from occupancy_tbl where substr(occ_code,1,2) = '{m_sCatCode}'";
                    sCode = db.Database.SqlQuery<string>(sQuery).SingleOrDefault();
                }
                catch { }

                if (string.IsNullOrEmpty(sCode))
                    sCode = m_sCatCode + "01";
                else
                {
                    int.TryParse(sCode, out iCode);
                    sCode = FormatSeries((iCode + 1).ToString());
                }
                txtCode.Text = sCode;
                txtOccupancy.Focus();
                txtOccupancy.ReadOnly = false; 
            }
            else
            {
                if (string.IsNullOrEmpty(cmbCategory.Text.ToString()))
                {
                    MessageBox.Show("Category field requires an entry", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    cmbCategory.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtOccupancy.Text.ToString().Trim()))
                {
                    MessageBox.Show("Occupancy field requires an entry", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtOccupancy.Focus();
                    return;
                }

                string sTmpPermit = string.Empty;
                OccupancyList occ = new OccupancyList(null, null);

                sTmpPermit = occ.GetOccupancyCode(txtOccupancy.Text.ToString().Trim());
                if(!string.IsNullOrEmpty(sTmpPermit))
                {
                    MessageBox.Show("" + txtOccupancy.Text.ToString().Trim() + " already exists.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtOccupancy.Focus();
                    return;
                }

                if (MessageBox.Show("Save this record?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sQuery = $"insert into occupancy_tbl values (:1,:2,:3)";
                    db.Database.ExecuteSqlCommand(sQuery,
                         new OracleParameter(":1", txtCode.Text.ToString()),
                         new OracleParameter(":2", txtOccupancy.Text.ToString().Trim()),
                         new OracleParameter(":3", m_sCatCode));

                    MessageBox.Show("Record saved", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (Utilities.AuditTrail.InsertTrail("SOC-A", "occupancy_tbl", txtCode.Text.ToString()) == 0)
                    {
                        MessageBox.Show("Failed to insert audit trail.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                }
                UpdateList();

                btnAdd.Text = "Add";
                btnCancel.Text = "Exit";
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                dgvList.Enabled = true;
                cmbCategory.Enabled = true;
                txtOccupancy.ReadOnly = true;
            }
        }

        private string FormatSeries(string sSeries)
        {
            int iCount = sSeries.Length;
            string sNewSeries = string.Empty;

            switch (iCount)
            {
                case 1:
                    {
                        sNewSeries = "000" + sSeries;
                        break;
                    }
                case 2:
                    {
                        sNewSeries = "00" + sSeries;
                        break;
                    }
                case 3:
                    {
                        sNewSeries = "0" + sSeries;
                        break;
                    }
                case 4:
                    {
                        sNewSeries = sSeries;
                        break;
                    }

            }

            return sNewSeries;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if(btnEdit.Text == "Cancel")
            {
                btnAdd.Text = "Add";
                btnEdit.Text = "Edit";
                btnAdd.Enabled = true;
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                btnCancel.Text = "Exit";
            }
            else
            {
                this.Close();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;

            if (btnEdit.Text == "Edit")
            {
                if (string.IsNullOrEmpty(m_sOccCode))
                {
                    MessageBox.Show("Select record to delete first", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                txtOccupancy.ReadOnly = false;
                btnAdd.Enabled = false;
                btnDelete.Enabled = false;
                btnEdit.Text = "Update";
                btnCancel.Text = "Cancel";
                txtOccupancy.Focus();
                
            }
            else 
            {
               
                if (string.IsNullOrEmpty(txtOccupancy.Text.ToString()))
                {
                    MessageBox.Show("Occupancy field requires an entry", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                        
                if (m_sOccDesc != txtOccupancy.Text.ToString().Trim())
                {
                    int iCheck = 0;
                    sQuery = $"select count(*) from occupancy_tbl where occ_desc = '{txtOccupancy.Text.ToString().Trim()}' and occ_code <> '{m_sOccCode}'";
                    iCheck = db.Database.SqlQuery<Int32>(sQuery).SingleOrDefault();
                    if(iCheck > 0)
                    {
                        MessageBox.Show("Occupancy: " + txtOccupancy.Text.ToString().Trim() + " already exists.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtOccupancy.Focus();
                        return;
                    }
                }

                if (MessageBox.Show("Update this record?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sQuery = $"update occupancy_tbl set occ_desc = '{StringUtilities.HandleApostrophe(txtOccupancy.Text.ToString().Trim())}' where occ_code = '{txtCode.Text.ToString().Trim()}' and category_code = '{m_sCatCode}'";
                    db.Database.ExecuteSqlCommand(sQuery);

                    MessageBox.Show("Record updated", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    string sObject = txtCode.Text.ToString() + "|" + m_sOccDesc + " to " + txtOccupancy.Text.ToString().Trim();

                    if (Utilities.AuditTrail.InsertTrail("SOC-E", "occupancy_tbl", sObject) == 0)
                    {
                        MessageBox.Show("Failed to insert audit trail.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                UpdateList();

                txtOccupancy.ReadOnly = true;
                btnAdd.Enabled = true;
                btnDelete.Enabled = true;
                btnEdit.Text = "Edit";
                btnCancel.Text = "Exit";
                
            }
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                m_sOccCode = dgvList[0, e.RowIndex].Value.ToString();
                m_sOccDesc = dgvList[1, e.RowIndex].Value.ToString();
                txtCode.Text = m_sOccCode;
                txtOccupancy.Text = m_sOccDesc;
            }
            catch { }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;

            if (IsTypeOfOccupancyInUse()) return;

            if (string.IsNullOrEmpty(m_sOccCode))
            {
                MessageBox.Show("Select record to delete first", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this record?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                sQuery = $"delete from occupancy_tbl where occ_code = '{m_sOccCode}'";
                db.Database.ExecuteSqlCommand(sQuery);

                MessageBox.Show("Record deleted", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (Utilities.AuditTrail.InsertTrail("SOC-D", "occupancy_tbl", m_sOccCode + "|" + m_sOccDesc) == 0)
                {
                    MessageBox.Show("Failed to insert audit trail.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            txtCode.Text = "";
            txtOccupancy.Text = "";
            UpdateList();
        }

        private bool IsTypeOfOccupancyInUse()
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;
            int iCheck = 0;

            sQuery = $"select count(*) from application_que where occupancy_code = '{m_sOccCode}'";
            iCheck = db.Database.SqlQuery<Int32>(sQuery).SingleOrDefault();
            if (iCheck > 0)
            {
                MessageBox.Show("There are records found using this Type Of Occupancy:: " + txtOccupancy.Text.ToString().Trim() + ".\nDeletion not allowed", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return true;
            }

            sQuery = $"select count(*) from application where occupancy_code = '{m_sOccCode}'";
            iCheck = db.Database.SqlQuery<Int32>(sQuery).SingleOrDefault();
            if (iCheck > 0)
            {
                MessageBox.Show("There are records found using this Type Of Occupancy:: " + txtOccupancy.Text.ToString().Trim() + ".\nDeletion not allowed", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return true;
            }

            return false;
        }
    }
}
