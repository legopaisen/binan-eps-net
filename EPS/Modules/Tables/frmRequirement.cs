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
    public partial class frmRequirement : Form
    {
        public frmRequirement()
        {
            InitializeComponent();
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            if (btnExit.Text == "Exit")
                this.Close();
            else
            {
                ClearControls();
                EnableControls(false);
                btnAdd.Text = "Add";
                btnEdit.Text = "Edit";
                btnExit.Text = "Exit";
                dgvList.Enabled = true;
                btnAdd.Enabled = true;
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
            }
        }

        private void frmRequirement_Load(object sender, EventArgs e)
        {
            cmbFilter.Items.Add("ALL");
            cmbFilter.Items.Add("BUILDING");
            cmbFilter.Items.Add("ELECTRICAL");
            cmbFilter.Items.Add("MECHANICAL");
            cmbFilter.Items.Add("OCCUPANCY");
            cmbFilter.Items.Add("CFEI");
            cmbFilter.SelectedIndex = 0;

            ClearControls();
            EnableControls(false);
            PopulateGrid();
            LoadApplication();
        }

        private void ClearControls()
        {
            txtDesc.Text = string.Empty;
            txtID.Text = string.Empty;
            cmbApplication.Text = string.Empty;
        }

        private void LoadApplication()
        {
            cmbApplication.Items.Clear();
            cmbApplication.Items.Add("BUILDING");
            cmbApplication.Items.Add("ELECTRICAL");
            cmbApplication.Items.Add("MECHANICAL");
            cmbApplication.Items.Add("OCCUPANCY");
            cmbApplication.Items.Add("CFEI");
        }

        private void PopulateGrid()
        {
            dgvList.Rows.Clear();
            OracleResultSet res = new OracleResultSet();

            if (cmbFilter.Text == "ALL")
                res.Query = "select * from requirements_tbl";
            else
                res.Query = $"select * from requirements_tbl where req_appl = '{cmbFilter.Text.Trim()}'";

            if (res.Execute())
                while(res.Read())
                {
                    dgvList.Rows.Add(res.GetString("req_id"), res.GetString("req_desc"), res.GetString("req_appl"));
                }
            res.Close();
        }

        private void EnableControls(bool bln)
        {
            txtDesc.Enabled = bln;
            cmbApplication.Enabled = bln;
            cmbFilter.Enabled = !bln;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(btnAdd.Text == "Add")
            {
                btnAdd.Text = "Save";
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                btnExit.Text = "Cancel";
                dgvList.Enabled = false;
                EnableControls(true);
            }
            else
            {
                if(string.IsNullOrEmpty(txtDesc.Text.Trim()))
                {
                    MessageBox.Show("Incomplete details!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                if(MessageBox.Show("Save record?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    OracleResultSet res = new OracleResultSet();
                    res.Query = "INSERT INTO REQUIREMENTS_TBL VALUES(";
                    res.Query += $"'{GetNextID()}', ";
                    res.Query += $"'{txtDesc.Text.Trim()}', ";
                    res.Query += $"'')";
                    //res.Query += $"'{cmbApplication.Text.Trim()}')";

                    if (res.ExecuteNonQuery() == 0)
                    { }

                    ClearControls();
                    PopulateGrid();
                    EnableControls(false);

                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;
                    dgvList.Enabled = true;
                    btnExit.Text = "Exit";
                    btnAdd.Text = "Add";
                }
            }
        }

        private string GetNextID()
        {
            OracleResultSet res = new OracleResultSet();
            int iCnt = 0;
            string sID = string.Empty;
            res.Query = "select nvl(max(to_number(req_id)), 0) as id from requirements_tbl";
            int.TryParse(res.ExecuteScalar(), out iCnt);

            if(iCnt == 0)
            {
                sID = "001";
            }
            else
            {
                iCnt += 1;
                switch(iCnt.ToString().Length)
                {
                    case 1: sID = "00" + iCnt.ToString(); break;
                    case 2: sID = "0" + iCnt.ToString(); break;
                    case 3: sID = iCnt.ToString(); break;
                }
            }

            return sID;
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtID.Text = dgvList.CurrentRow.Cells[0].Value?.ToString();
            }
            catch { }

            try
            {
                txtDesc.Text = dgvList.CurrentRow.Cells[1].Value?.ToString();
            }
            catch { }

            try
            {
                cmbApplication.Text = dgvList.CurrentRow.Cells[2].Value?.ToString();
            }
            catch { }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtID.Text.Trim()))
            {
                MessageBox.Show("No record selected!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if(MessageBox.Show("Delete record?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                OracleResultSet res = new OracleResultSet();
                res.Query = $"DELETE FROM REQUIREMENTS_TBL WHERE REQ_ID = '{txtID.Text.Trim()}'";
                if (res.ExecuteNonQuery() == 0)
                { }

                ClearControls();
                PopulateGrid();
                EnableControls(false);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if(btnEdit.Text == "Edit")
            {
                if (string.IsNullOrEmpty(txtID.Text.Trim()))
                {
                    MessageBox.Show("No record selected!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                EnableControls(true);
                dgvList.Enabled = false;
                btnEdit.Text = "Update";
                btnAdd.Enabled = false;
                btnExit.Text = "Cancel";
                btnDelete.Enabled = false;
            }
            else
            {
                if (string.IsNullOrEmpty(txtDesc.Text.Trim()))
                {
                    MessageBox.Show("Incomplete details!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                if (MessageBox.Show("Update record?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    OracleResultSet res = new OracleResultSet();
                    res.Query = "UPDATE REQUIREMENTS_TBL SET ";
                    res.Query += $"REQ_DESC = '{txtDesc.Text.Trim()}' ";
                    //res.Query += $"REQ_APPL = '{cmbApplication.Text.Trim()}' ";
                    res.Query += $"WHERE REQ_ID = '{txtID.Text.Trim()}'";
                    if (res.ExecuteNonQuery() == 0)
                    { }

                    ClearControls();
                    PopulateGrid();
                    EnableControls(false);

                    btnAdd.Enabled = true;
                    btnDelete.Enabled = true;
                    dgvList.Enabled = true;
                    btnExit.Text = "Exit";
                    btnEdit.Text = "Edit";
                }
            }
        }

        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearControls();
            PopulateGrid();
        }
    }
}
