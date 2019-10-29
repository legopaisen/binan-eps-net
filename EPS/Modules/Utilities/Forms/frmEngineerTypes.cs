using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modules.Utilities;
using Oracle.ManagedDataAccess.Client;
using Common.StringUtilities;
using System.Data;
using EPSEntities.Connection;
using System.Windows.Forms;
using Common.AppSettings;
using Common.DataConnector;

namespace Modules.Utilities.Forms
{
    public partial class frmEngineerTypes : Form
    {
        public frmEngineerTypes()
        {
            InitializeComponent();
        }

        private void frmEngineerTypes_Load(object sender, EventArgs e)
        {
            Populate();
            try
            {
                dgvList.Rows[0].Cells[0].Selected = false;
            }
            catch { } //AFM 20191025 catches emptiness
        }

        private void Populate()
        {
            dgvList.Rows.Clear();
            OracleResultSet result = new OracleResultSet();
            result.Query = $"select * from engineer_type order by engr_type_code";
            if(result.Execute())
                while(result.Read())
                {
                    dgvList.Rows.Add(result.GetString(0), result.GetString(1));
                }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Add")
            {
                ClearControls();
                btnAdd.Text = "Save";
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                btnExit.Text = "Cancel";
                txtType.ReadOnly = false;
                txtType.Focus();
                dgvList.Enabled = false;
            }
            else
            {
                if(txtCode.Text == "" && txtType.Text == "")
                {
                    MessageBox.Show("Incomplete details");
                    return;
                }
                Save();
                dgvList.Enabled = true;
                ClearControls();
                Populate();
                btnAdd.Text = "Add";
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                btnExit.Text = "Exit";
                txtType.ReadOnly = true;
            }
        }

        private int GetEngrTypeCode(int cnt)
        {
            OracleResultSet result = new OracleResultSet();
            cnt = 0;
            result.Query = "select max(to_number(engr_type_code)) from engineer_type";
            try
            {
                if (result.Execute())
                    if (result.Read())
                    {
                        cnt = result.GetInt(0);
                    }
            }
            catch { } //AFM 20191025 catches emptiness
            cnt += 1;
            return cnt;
        }

        private void Save()
        {
            int engrCnt = 0;
            int cnt = 0;
            string engrCntStr = string.Empty;
            if (btnAdd.Text == "Save")
            {
                cnt = GetEngrTypeCode(engrCnt);
                engrCntStr = cnt.ToString().PadLeft(cnt.ToString().Length + 1, '0');
            }
            if (MessageBox.Show("Are you sure you want to Save this record?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (btnAdd.Text == "Save")
                {
                    OracleResultSet result = new OracleResultSet();
                    result.Query = $"INSERT INTO ENGINEER_TYPE VALUES ('{engrCntStr}', '{txtType.Text}')";
                    result.ExecuteNonQuery();
                    result.Close();
                    MessageBox.Show("Record Saved!");
                }
                else if (btnAdd.Text == "Update")
                {
                    OracleResultSet result = new OracleResultSet();
                    result.Query = $"UPDATE ENGINEER_TYPE SET ENGR_DESC = '{txtType.Text}' WHERE ENGR_TYPE_CODE = '{txtCode.Text}'";
                    result.ExecuteNonQuery();
                    result.Close();
                    MessageBox.Show("Record Updated!");
                }        
            }
            else
                return;
        }

        private void ClearControls()
        {
            txtCode.Text = "";
            txtType.Text = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if(btnExit.Text == "Exit")
                this.Close(); 
            else
            {
                ClearControls();
                btnAdd.Text = "Add";
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                btnExit.Text = "Exit";
                txtType.ReadOnly = true;
                dgvList.Enabled = true;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(txtCode.Text == "" && txtType.Text == "")
            {
                MessageBox.Show("Select record to delete");
                return;
            }
            if (MessageBox.Show("Are you sure you want to delete this record?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                OracleResultSet result = new OracleResultSet();
                result.Query = $"delete from engineer_type where engr_type_code = '{txtCode.Text}'";
                result.ExecuteNonQuery();
                result.Close();
                MessageBox.Show("Record Deleted!");
            }
            else
                return;
            Populate();
            ClearControls();
        }

        private void dgvList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtCode.Text == "" && txtType.Text == "")
            {
                MessageBox.Show("Select record to edit");
                return;
            }
            dgvList.Enabled = false;
            btnAdd.Text = "Update";
            btnExit.Text = "Cancel";
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            txtType.ReadOnly = false;
        }
    }
}
