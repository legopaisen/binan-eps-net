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
    public partial class frmScope : Form
    {
        public static ConnectionString dbConn = new ConnectionString();
        private string m_sCode = string.Empty;
        private string m_sDesc = string.Empty;

        public frmScope()
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

        private void frmScope_Load(object sender, EventArgs e)
        {
            UpdateList();
        }

        private void UpdateList()
        {
            ScopeList scope = new ScopeList();

            int iCnt = scope.ScopeLst.Count;
            dgvList.Rows.Clear();

            for (int i = 0; i < iCnt; i++)
            {
                dgvList.Rows.Add(scope.ScopeLst[i].ScopeCode, scope.ScopeLst[i].ScopeDesc);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;

            if (btnAdd.Text == "Add")
            {
                txtCode.Text = "";
                txtScope.Text = "";
                txtScope.ReadOnly = false;
                
                btnAdd.Text = "Save";
                btnCancel.Text = "Cancel";
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;

                string sCode = string.Empty;
                int iCode = 0;
                try
                {
                    sQuery = $"select max(scope_code) from scope_tbl";
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
                txtScope.Focus();
            }
            else 
            {
                if(string.IsNullOrEmpty(txtScope.Text.ToString().Trim()))
                {
                    MessageBox.Show("Scope of Work field requires an entry", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtScope.Focus();
                    return;
                }

                ScopeList scope = new ScopeList();
                string sScopeCode = string.Empty;

                sScopeCode = scope.GetScopeCode(txtScope.Text.ToString().Trim());
                if (!string.IsNullOrEmpty(sScopeCode))
                {
                    MessageBox.Show("" + txtScope.Text.ToString().Trim() + " already exists.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtScope.Focus();
                    return;

                }

                if(string.IsNullOrEmpty(txtScope.Text.ToString().Trim()))
                {
                    MessageBox.Show("Scope of Work required", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtScope.Focus();
                    return;
                }

                if (MessageBox.Show("Save this record?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sQuery = $"insert into scope_tbl values (:1,:2)";
                    db.Database.ExecuteSqlCommand(sQuery,
                         new OracleParameter(":1", txtCode.Text.ToString()),
                         new OracleParameter(":2", txtScope.Text.ToString().Trim()));

                    MessageBox.Show("Record saved", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (Utilities.AuditTrail.InsertTrail("SSC-A", "scope_tbl", txtCode.Text.ToString()) == 0)
                    {
                        MessageBox.Show("Failed to insert audit trail.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                UpdateList();
                txtScope.ReadOnly = true;

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

                txtScope.ReadOnly = false;
                btnEdit.Text = "Update";
                btnCancel.Text = "Cancel";
                btnAdd.Enabled = false;
                btnDelete.Enabled = false;

                txtScope.Focus();
            }
            else
            {
                if (string.IsNullOrEmpty(txtScope.Text.ToString().Trim()))
                {
                    MessageBox.Show("Scope of Work field requires an entry", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtScope.Focus();
                    return;
                }

                int iCheck = 0;
                sQuery = $"select count(*) from scope_tbl where scope_desc = '{txtScope.Text.ToString().Trim()}' and scope_code <> '{m_sCode}'";
                iCheck = db.Database.SqlQuery<Int32>(sQuery).SingleOrDefault();

                if (iCheck > 0)
                {
                    MessageBox.Show("" + txtScope.Text.ToString().Trim() + " already exists.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                if (MessageBox.Show("Update this record?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sQuery = $"update scope_tbl set scope_desc = '{StringUtilities.HandleApostrophe(txtScope.Text.ToString().Trim())}' where scope_code = '{m_sCode}'";
                    db.Database.ExecuteSqlCommand(sQuery);

                    MessageBox.Show("Record updated", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    string sObject = txtCode.Text.ToString() + "|" + m_sDesc + " to " + txtScope.Text.ToString().Trim();

                    if (Utilities.AuditTrail.InsertTrail("SSC-E", "scope_tbl", sObject) == 0)
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
                txtScope.ReadOnly = true;
            }
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                m_sCode = dgvList[0, e.RowIndex].Value.ToString();
                m_sDesc = dgvList[1, e.RowIndex].Value.ToString();
                txtCode.Text = m_sCode;
                txtScope.Text = m_sDesc;
            }
            catch { }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;

            if (IsScopeInUse()) return;

            if(string.IsNullOrEmpty(m_sCode))
            {
                MessageBox.Show("Select record to delete first",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Stop);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this record?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sQuery = $"delete from scope_tbl where scope_code = '{m_sCode}'";
                db.Database.ExecuteSqlCommand(sQuery);

                txtCode.Text = "";
                txtScope.Text = "";
                m_sCode = "";
                m_sDesc = "";

                MessageBox.Show("Record deleted",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Stop);

                UpdateList();
            }
        }

        private bool IsScopeInUse()
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;
            int iCheck = 0;

            sQuery = $"select count(*) from application_que where scope_code = '{m_sCode}'";
            iCheck = db.Database.SqlQuery<Int32>(sQuery).SingleOrDefault();
            if(iCheck > 0)
            {
                MessageBox.Show("There are records found using this Scope of Work: " + m_sDesc + ".\nDeletion not allowed", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return true;
            }

            sQuery = $"select count(*) from application where scope_code = '{m_sCode}'";
            iCheck = db.Database.SqlQuery<Int32>(sQuery).SingleOrDefault();
            if (iCheck > 0)
            {
                MessageBox.Show("There are records found using this Scope of Work: " + m_sDesc + ".\nDeletion not allowed", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return true;
            }

            return false;
        }
    }
}
