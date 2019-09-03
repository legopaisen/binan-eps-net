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
    public partial class frmMaterial : Form
    {
        public static ConnectionString dbConn = new ConnectionString();
        private string m_sCode = string.Empty;
        private string m_sDesc = string.Empty;

        public frmMaterial()
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
            MaterialList material = new MaterialList();

            int iCnt = material.MaterialLst.Count;
            dgvList.Rows.Clear();

            for (int i = 0; i < iCnt; i++)
            {
                dgvList.Rows.Add(material.MaterialLst[i].Code, material.MaterialLst[i].Desc);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;

            if (btnAdd.Text == "Add")
            {
                txtCode.Text = "";
                txtDesc.Text = "";
                txtDesc.ReadOnly = false;
                
                btnAdd.Text = "Save";
                btnCancel.Text = "Cancel";
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;

                string sCode = string.Empty;
                int iCode = 0;
                try
                {
                    sQuery = $"select max(material_code) from material_tbl";
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
                txtDesc.Focus();
            }
            else 
            {
                if(string.IsNullOrEmpty(txtDesc.Text.ToString().Trim()))
                {
                    MessageBox.Show("Material description requires an entry", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtDesc.Focus();
                    return;
                }

                MaterialList material = new MaterialList();
                string sMatCode = string.Empty;

                sMatCode = material.GetMaterialCode(txtDesc.Text.ToString().Trim());
                if (!string.IsNullOrEmpty(sMatCode))
                {
                    MessageBox.Show("" + txtDesc.Text.ToString().Trim() + " already exists.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtDesc.Focus();
                    return;

                }

                if(string.IsNullOrEmpty(txtDesc.Text.ToString().Trim()))
                {
                    MessageBox.Show("Material description required", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtDesc.Focus();
                    return;
                }

                if (MessageBox.Show("Save this record?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sQuery = $"insert into material_tbl values (:1,:2)";
                    db.Database.ExecuteSqlCommand(sQuery,
                         new OracleParameter(":1", txtCode.Text.ToString()),
                         new OracleParameter(":2", txtDesc.Text.ToString().Trim()));

                    MessageBox.Show("Record saved", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (Utilities.AuditTrail.InsertTrail("SMT-A", "material_tbl", txtCode.Text.ToString()) == 0)
                    {
                        MessageBox.Show("Failed to insert audit trail.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                UpdateList();
                txtDesc.ReadOnly = true;

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

                txtDesc.ReadOnly = false;
                btnEdit.Text = "Update";
                btnCancel.Text = "Cancel";
                btnAdd.Enabled = false;
                btnDelete.Enabled = false;

                txtDesc.Focus();
            }
            else
            {
                if (string.IsNullOrEmpty(txtDesc.Text.ToString().Trim()))
                {
                    MessageBox.Show("Material description requires an entry", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtDesc.Focus();
                    return;
                }

                int iCheck = 0;
                sQuery = $"select count(*) from material_tbl where material_desc = '{txtDesc.Text.ToString().Trim()}' and material_code <> '{m_sCode}'";
                iCheck = db.Database.SqlQuery<Int32>(sQuery).SingleOrDefault();

                if (iCheck > 0)
                {
                    MessageBox.Show("" + txtDesc.Text.ToString().Trim() + " already exists.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                if (MessageBox.Show("Update this record?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sQuery = $"update material_tbl set material_desc = '{StringUtilities.HandleApostrophe(txtDesc.Text.ToString().Trim())}' where material_code = '{m_sCode}'";
                    db.Database.ExecuteSqlCommand(sQuery);

                    MessageBox.Show("Record updated", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    string sObject = txtCode.Text.ToString() + "|" + m_sDesc + " to " + txtDesc.Text.ToString().Trim();

                    if (Utilities.AuditTrail.InsertTrail("SMT-E", "material_tbl", sObject) == 0)
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
                txtDesc.ReadOnly = true;
            }
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                m_sCode = dgvList[0, e.RowIndex].Value.ToString();
                m_sDesc = dgvList[1, e.RowIndex].Value.ToString();
                txtCode.Text = m_sCode;
                txtDesc.Text = m_sDesc;
            }
            catch { }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;

            if (IsMaterialInUse()) return;

            if(string.IsNullOrEmpty(m_sCode))
            {
                MessageBox.Show("Select record to delete first",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Stop);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this record?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sQuery = $"delete from material_tbl where material_code = '{m_sCode}'";
                db.Database.ExecuteSqlCommand(sQuery);

                txtCode.Text = "";
                txtDesc.Text = "";
                m_sCode = "";
                m_sDesc = "";

                MessageBox.Show("Record deleted", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                UpdateList();
            }
        }

        private bool IsMaterialInUse()
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;
            int iCheck = 0;

            sQuery = $"select count(*) from building where material_code = '{m_sCode}'";
            iCheck = db.Database.SqlQuery<Int32>(sQuery).SingleOrDefault();
            if(iCheck > 0)
            {
                MessageBox.Show("There are records found using this Type of Material: " + m_sDesc + ".\nDeletion not allowed", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return true;
            }

            return false;
        }
    }
}
