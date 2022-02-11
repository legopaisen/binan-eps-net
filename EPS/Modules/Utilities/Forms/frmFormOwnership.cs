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
   // JAA 20220211
    public partial class frmFormOwnership : Form
    {
        public static ConnectionString dbConn = new ConnectionString();
        private string m_sCode = string.Empty;
        private string m_sDesc = string.Empty;



        public frmFormOwnership()
        {
            InitializeComponent();
        }

       
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (btnEdit.Text == "Cancel")
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

        private void frmFormOwnership_Load(object sender, EventArgs e)
        {
            UpdateList();
        }

        private void UpdateList()
        {
            OwnershipList ownership = new OwnershipList();

            int iCnt = ownership.OwnershipLst.Count;
            dgvList.Rows.Clear();

            for (int i = 0; i < iCnt; i++)
            {
                dgvList.Rows.Add(ownership.OwnershipLst[i].sOwnershipCode, ownership.OwnershipLst[i].sOwnershipDesc);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;

            if (btnAdd.Text == "Add")
            {
                txtCode.Text = "";
                txtOwnership.Text = "";
                txtOwnership.ReadOnly = false;

                btnAdd.Text = "Save";
                btnCancel.Text = "Cancel";
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;

                string sCode = string.Empty;
                int iCode = 0;
                try
                {
                    sQuery = $"select max(ownership_code) from ownership_tbl";
                    sCode = db.Database.SqlQuery<string>(sQuery).SingleOrDefault();
                }
                catch { }

                if (string.IsNullOrEmpty(sCode))
                    sCode = "01";
                else
                {
                    int.TryParse(sCode, out iCode);
                    sCode = FormatSeries((iCode + 1).ToString());
                }
                txtCode.Text = sCode;
                txtOwnership.Focus();
            }
            else
            {
                if (string.IsNullOrEmpty(txtOwnership.Text.ToString().Trim()))
                {
                    MessageBox.Show("Form of Ownership field requires an entry", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtOwnership.Focus();
                    return;
                }

                OwnershipList ownership = new OwnershipList();
                string sownershipCode = string.Empty;

                sownershipCode = ownership.GetOwnershipCode(txtOwnership.Text.ToString().Trim());
                if (!string.IsNullOrEmpty(sownershipCode))
                {
                    MessageBox.Show("" + txtOwnership.Text.ToString().Trim() + " already exists.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtOwnership.Focus();
                    return;

                }

                if (string.IsNullOrEmpty(txtOwnership.Text.ToString().Trim()))
                {
                    MessageBox.Show("Form of Ownership required", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtOwnership.Focus();
                    return;
                }

                if (MessageBox.Show("Save this record?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sQuery = $"insert into ownership_tbl values (:1,:2)";
                    db.Database.ExecuteSqlCommand(sQuery,
                         new OracleParameter(":1", txtCode.Text.ToString()),
                         new OracleParameter(":2", txtOwnership.Text.ToString().Trim()));

                    MessageBox.Show("Record saved", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (Utilities.AuditTrail.InsertTrail("SSC-A", "ownership_tbl", txtCode.Text.ToString()) == 0)
                    {
                        MessageBox.Show("Failed to insert audit trail.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                UpdateList();
                txtOwnership.ReadOnly = true;

                btnAdd.Text = "Add";
                btnCancel.Text = "Exit";
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;

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
                        sNewSeries = "0" + sSeries;
                        break;
                    }
                case 2:
                    {
                        sNewSeries = sSeries;
                        break;
                    }
            }

            return sNewSeries;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;

            if (btnEdit.Text == "Edit")
            {
                if (string.IsNullOrEmpty(m_sCode))
                {
                    MessageBox.Show("Select record to edit first", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                txtOwnership.ReadOnly = false;
                btnEdit.Text = "Update";
                btnCancel.Text = "Cancel";
                btnAdd.Enabled = false;
                btnDelete.Enabled = false;

                txtOwnership.Focus();
            }
            else
            {
                if (string.IsNullOrEmpty(txtOwnership.Text.ToString().Trim()))
                {
                    MessageBox.Show("Form of Ownership field requires an entry", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtOwnership.Focus();
                    return;
                }

                int iCheck = 0;
                sQuery = $"select count(*) from ownership_tbl where ownership_desc = '{txtOwnership.Text.ToString().Trim()}' and ownership_code <> '{m_sCode}'";
                iCheck = db.Database.SqlQuery<Int32>(sQuery).SingleOrDefault();

                if (iCheck > 0)
                {
                    MessageBox.Show("" + txtOwnership.Text.ToString().Trim() + " already exists.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                if (MessageBox.Show("Update this record?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sQuery = $"update ownership_tbl set ownership_desc = '{StringUtilities.HandleApostrophe(txtOwnership.Text.ToString().Trim())}' where ownership_code = '{m_sCode}'";
                    db.Database.ExecuteSqlCommand(sQuery);

                    MessageBox.Show("Record updated", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    string sObject = txtCode.Text.ToString() + "|" + m_sDesc + " to " + txtOwnership.Text.ToString().Trim();

                    if (Utilities.AuditTrail.InsertTrail("SSC-E", "ownership_tbl", sObject) == 0)
                    {
                        MessageBox.Show("Failed to insert audit trail.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                UpdateList();
                btnAdd.Enabled = true;
                btnDelete.Enabled = true;
                btnEdit.Text = "Edit";
                btnCancel.Text = "Exit";
                txtOwnership.ReadOnly = true;
            }
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                m_sCode = dgvList[0, e.RowIndex].Value.ToString();
                m_sDesc = dgvList[1, e.RowIndex].Value.ToString();
                txtCode.Text = m_sCode;
                txtOwnership.Text = m_sDesc;
            }
            catch { }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;

            if (IsOwnershipInUse()) return;

            if (string.IsNullOrEmpty(m_sCode))
            {
                MessageBox.Show("Select record to delete first", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this record?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sQuery = $"delete from ownership_tbl where ownership_desc = '{m_sDesc}'";
                db.Database.ExecuteSqlCommand(sQuery);

                txtCode.Text = "";
                txtOwnership.Text = "";
                m_sCode = "";
                m_sDesc = "";

                MessageBox.Show("Record deleted", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);

                UpdateList();
            }
        }

        private bool IsOwnershipInUse()
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;
            int iCheck = 0;

            sQuery = $"select count(*) from application_que where own_type = '{m_sDesc}'";
            iCheck = db.Database.SqlQuery<Int32>(sQuery).SingleOrDefault();
            if (iCheck > 0)
            {
                MessageBox.Show("There are records found using this Form of Ownership: " + m_sDesc + ".\nDeletion not allowed", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return true;
            }

            sQuery = $"select count(*) from application where own_type = '{m_sDesc}'";
            iCheck = db.Database.SqlQuery<Int32>(sQuery).SingleOrDefault();
            if (iCheck > 0)
            {
                MessageBox.Show("There are records found using this Form of Ownership: " + m_sDesc + ".\nDeletion not allowed", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return true;
            }

            return false;
        }

      
    }
}
