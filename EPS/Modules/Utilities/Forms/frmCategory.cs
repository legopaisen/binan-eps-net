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
using EPSEntities.Entity;
using Oracle.ManagedDataAccess.Client;
using Common.StringUtilities;

namespace Modules.Utilities.Forms
{
    public partial class frmCategory : Form
    {
        private static ConnectionString dbConn = new ConnectionString();

        public frmCategory()
        {
            InitializeComponent();
        }

        private void frmCategory_Load(object sender, EventArgs e)
        {
            UpdateList();
        }

        private void UpdateList()
        {
            CategoryList lstCategory = new CategoryList();
            int iCnt = lstCategory.CategoryLst.Count;

            dgvList.Rows.Clear();

            for(int iRow = 0; iRow < iCnt; iRow++)
            {
                dgvList.Rows.Add(lstCategory.CategoryLst[iRow].Code, lstCategory.CategoryLst[iRow].Desc);
            }

            /*StructureList lstStructure = new StructureList();
            iCnt = lstStructure.StructureLst.Count;

            for(int iRow = 0; iRow < iCnt; iRow++)
            {
                dgvStruc.Rows.Add(lstStructure.StructureLst[iRow].Desc,lstStructure.StructureLst[iRow].Code);
            }*/ // removed this, since table is not used in the system
        }

        private void ClearControls()
        {
            txtCode.Text = "";
            txtType.Text = "";
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ClearControls();
            try
            {
                txtCode.Text = dgvList[0, e.RowIndex].Value.ToString();
            }
            catch { }

            try
            {
                txtType.Text = dgvList[1, e.RowIndex].Value.ToString();
            }
            catch { }

            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(btnAdd.Text == "Add")
            {
                ClearControls();
                btnAdd.Text = "Save";
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                btnExit.Text = "Cancel";
                txtType.ReadOnly = false;
                Category category = new Category();
                category.CreateCategoryCode();
                txtCode.Text = category.Code;
                txtType.Focus();
            }
            else {

                var db = new EPSConnection(dbConn);
                string sQuery = string.Empty;

                if (string.IsNullOrEmpty(txtType.Text.ToString()))
                {
                    MessageBox.Show("Category field requires an entry",this.Text, MessageBoxButtons.OK,MessageBoxIcon.Stop);
                    return;
                }

                if (MessageBox.Show("Are you sure you want to Save this record?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sQuery = $"insert into category_tbl values (:1,:2)";
                    db.Database.ExecuteSqlCommand(sQuery,
                        new OracleParameter(":1", txtCode.Text.ToString().Trim()),
                        new OracleParameter(":2", StringUtilities.HandleApostrophe(txtType.Text.ToString().Trim())));

                    string sObject = string.Empty;
                    sObject = "Code:" + txtCode.Text.ToString();

                    if (Utilities.AuditTrail.InsertTrail("SC-A", "category_tbl", sObject) == 0)
                    {
                        MessageBox.Show("Failed to insert audit trail.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    UpdateList();
                    btnAdd.Text = "Add";
                    btnExit.Text = "Exit";
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;
                    txtType.ReadOnly = true;
                    MessageBox.Show("Record saved", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEdit.Text == "Edit")
            {
                ClearControls();
                btnEdit.Text = "Update";
                btnAdd.Enabled = false;
                btnDelete.Enabled = false;
                btnExit.Text = "Cancel";
                txtType.ReadOnly = false;
                txtType.Focus();
            }
            else
            {

                var db = new EPSConnection(dbConn);
                string sQuery = string.Empty;

                if (string.IsNullOrEmpty(txtType.Text.ToString()))
                {
                    MessageBox.Show("Category field requires an entry", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                if (MessageBox.Show("Are you sure you want to update this record?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sQuery = $"update category_tbl set category_desc = '{StringUtilities.HandleApostrophe(txtType.Text.ToString())}' where category_code = '{txtCode.Text.ToString()}'";
                    db.Database.ExecuteSqlCommand(sQuery);

                    string sObject = string.Empty;
                    sObject = "Code:" + txtCode.Text.ToString();

                    if (Utilities.AuditTrail.InsertTrail("SC-E", "category_tbl", sObject) == 0)
                    {
                        MessageBox.Show("Failed to insert audit trail.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    UpdateList();
                    btnEdit.Text = "Edit";
                    btnExit.Text = "Exit";
                    btnAdd.Enabled = true;
                    btnDelete.Enabled = true;
                    txtType.ReadOnly = true;
                    MessageBox.Show("Record edited", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;

            if (string.IsNullOrEmpty(txtCode.Text.ToString()))
            {
                MessageBox.Show("Select record to delete first", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (IsCategoryInUse()) return;

            if (MessageBox.Show("Are you sure you want to Delete this record?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sQuery = $"delete from category_tbl where category_code = '{txtCode.Text.ToString()}'";
                db.Database.ExecuteSqlCommand(sQuery);

                string sObject = string.Empty;
                sObject = "Code:" + txtCode.Text.ToString();

                if (Utilities.AuditTrail.InsertTrail("SC-D", "category_tbl", sObject) == 0)
                {
                    MessageBox.Show("Failed to insert audit trail.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                UpdateList();

                ClearControls();

                MessageBox.Show("Record deleted", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool IsCategoryInUse()
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;
            int iCnt = 0;

            iCnt = 0;
                sQuery = $"select count(*) from application_que where category_code ='{txtCode.Text.ToString()}'";
                iCnt = db.Database.SqlQuery<Int32>(sQuery).SingleOrDefault();

            if (iCnt > 0)
            {
                MessageBox.Show("There are records found using this Permit Type: " + txtType.Text.ToString() + "\nDeletion not allowed.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return true;
            }
            else
            {
                iCnt = 0;
                sQuery = $"select count(*) from application where category_code ='{txtCode.Text.ToString()}'";
                iCnt = db.Database.SqlQuery<Int32>(sQuery).SingleOrDefault();

                if (iCnt > 0)
                {
                    MessageBox.Show("There are records found using this Permit Type: " + txtType.Text.ToString() + "\nDeletion not allowed.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return true;
                }
                else
                    return false;
            }
            

            return false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (btnExit.Text == "Cancel")
            {
                btnAdd.Text = "Add";
                btnEdit.Text = "Edit";
                btnExit.Text = "Exit";
                txtType.ReadOnly = true;
                btnAdd.Enabled = true;
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
            }
            else
                this.Close();
        }
    }
}
