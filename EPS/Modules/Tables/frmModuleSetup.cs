using Common.DataConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Modules.Tables
{
    //AFM 20200807 created module
    public partial class frmModuleSetup : Form
    {
        public frmModuleSetup()
        {
            InitializeComponent();
        }

        private string m_sCode = "";
        private string m_sDesc = "";

        private void frmModuleSetup_Load(object sender, EventArgs e)
        {
            Populate();
            Init();
            dgvModules.PerformLayout();
        }

        private void Populate()
        {
            dgvModules.Rows.Clear();
            OracleResultSet result = new OracleResultSet();
            result.Query = "select * from rights order by right_desc";
            if(result.Execute())
                while(result.Read())
                {
                    dgvModules.Rows.Add(result.GetString(0), result.GetString(1));
                }
            result.Close();
        }

        private void Init()
        {
            dgvModules.Enabled = true;
            txtCode.Text = "";
            txtDesc.Text = "";
            txtCode.ReadOnly = true;
            txtDesc.ReadOnly = true;
            btnDelete.Enabled = true;
            btnEdit.Enabled = true;
            btnAdd.Enabled = true;
            btnAdd.Text = "Add";
            btnEdit.Text = "Edit";
            btnExit.Text = "Exit";
        }

        private void dgvModules_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCode.Text = dgvModules.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
            txtDesc.Text = dgvModules.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();
            m_sCode = txtCode.Text;
            m_sDesc = txtDesc.Text;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if(btnExit.Text == "Exit")
            {
                this.Close();
            }
            else
            {
                Init();
            }
        }

        private bool Validation()
        {
            OracleResultSet result = new OracleResultSet();
            result.Query = "select * from rights where right_code = '"+ txtCode.Text.Trim() +"'";
            if (result.Execute())
                if (result.Read())
                {
                    MessageBox.Show("Code already exists!");
                    return true;
                }
                else
                    return false;
            else
                return false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(btnAdd.Text == "Add")
            {
                btnAdd.Text = "Save";
                txtCode.Text = "";
                txtDesc.Text = "";
                dgvModules.Enabled = false;
                txtCode.ReadOnly = false;
                txtDesc.ReadOnly = false;
                btnDelete.Enabled = false;
                btnEdit.Enabled = false;
                btnExit.Text = "Cancel";
            }
            else
            { 
                if(Validation())
                    return;
                if(txtCode.Text == "" || txtDesc.Text == "")
                {
                    MessageBox.Show("Code or Description is empty.");
                    return;
                }
                

                OracleResultSet result = new OracleResultSet();
                result.Query = "INSERT INTO RIGHTS VALUES ('"+ txtCode.Text.Trim() +"', '"+ txtDesc.Text.Trim() +"')";
                if(result.ExecuteNonQuery() == 0)
                { }
                result.Close();

                MessageBox.Show("Saved Successfully!");
                Populate();
                Init();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (ValidationUsed())
                return;
            if (MessageBox.Show("Are you sure you want to delete this record?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                OracleResultSet result = new OracleResultSet();
                result.Query = "DELETE FROM RIGHTS WHERE RIGHT_CODE = '" + txtCode.Text.Trim() + "' AND RIGHT_DESC = '" + txtDesc.Text.Trim() + "'";
                if (result.ExecuteNonQuery() == 0)
                { }

                MessageBox.Show("Record Deleted!");
            }
            Populate();
            Init();
        }
        
        private bool ValidationUsed()
        {
            OracleResultSet result = new OracleResultSet();
            result.Query = "select * from user_level_rights where module_code = '"+ m_sCode + "'";
            if (result.Execute())
                if (result.Read())
                {
                    MessageBox.Show("Cannot Change Code/Delete Record. Record is currently being used!");
                    return true;
                }
                else
                    return false;
            else
                return false;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if(txtCode.Text == "")
            {
                MessageBox.Show("Select record first!");
                return;
            }
            if(btnEdit.Text == "Edit")
            { 
                dgvModules.Enabled = false;
                txtCode.ReadOnly = false;
                txtDesc.ReadOnly = false;
                btnAdd.Enabled = false;
                btnDelete.Enabled = false;
                btnEdit.Text = "Update";
                btnExit.Text = "Cancel";
            }
            else
            {
                if (txtCode.Text != m_sCode)
                {
                    if (ValidationUsed() || Validation())
                        return;
                }
                if (txtCode.Text != m_sCode)
                        return;

                if (MessageBox.Show("Are you sure you want to update this record?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    OracleResultSet result = new OracleResultSet();
                    result.Query = "UPDATE RIGHTS SET RIGHT_CODE = '"+ txtCode.Text.Trim() +"', RIGHT_DESC = '"+ txtDesc.Text.Trim() + "'  WHERE RIGHT_CODE = '" + m_sCode + "' AND RIGHT_DESC = '" + m_sDesc + "'";
                    if(result.ExecuteNonQuery() == 0)
                    { }

                    MessageBox.Show("Updated Successfully!");
                }
                Populate();
                Init();
            }
        }
    }
}
