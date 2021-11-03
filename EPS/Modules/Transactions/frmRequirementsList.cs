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

//AFM 20210119 REQUIREMENTS MODULE in application as requested from eps training (binan)
namespace Modules.Transactions
{
    public partial class frmRequirementsList : Form
    {
        public frmRequirementsList()
        {
            InitializeComponent();
        }

        public string Permit = string.Empty;
        public string ARN = string.Empty;
        public bool ViewMode = false;

        private void frmRequirementsList_Load(object sender, EventArgs e)
        {
            LoadList();
        }

        private void LoadList()
        {
            dgvList.Rows.Clear();

            OracleResultSet res = new OracleResultSet();
            string[] arrPermit = Permit.Split(' ');
            res.Query = $"select * from requirements_tbl where req_appl like '%{arrPermit[0]}%' order by req_id";
            if (res.Execute())
                while (res.Read())
                {
                    dgvList.Rows.Add(false, res.GetString("req_desc"), res.GetString("req_id"));
                }
            res.Close();

            if (ViewMode == true)
            {
                int cnt = 0;
                string sID = string.Empty;
                res.Query = $"select * from requirements_tbl where req_appl like '%{arrPermit[0]}%' order by req_id";
                if (res.Execute())
                    while (res.Read())
                    {
                        sID = res.GetString("req_id");
                        for(cnt = 0; cnt < dgvList.Rows.Count; cnt++)
                        {
                            if (sID == dgvList[2, cnt].Value.ToString())
                            {
                                dgvList[0, cnt].Value = true;
                            }
                        }

                    }
                res.Close();
                dgvList.ReadOnly = true;
            }

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if(ViewMode == false)
            {
                OracleResultSet res = new OracleResultSet();
                res.Query = $"DELETE FROM REQUIREMENTS_QUE a WHERE a.ARN = '{ARN}' AND a.REQ_ID IN (SELECT b.REQ_ID FROM REQUIREMENTS_TBL b WHERE a.REQ_ID = b.REQ_ID AND b.REQ_APPL LIKE '%{Permit}%')";
                if (res.ExecuteNonQuery() == 0)
                { }
                res.Close();

                foreach (DataGridViewRow row in dgvList.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[0].Value) == true)
                    {
                        res.Query = "INSERT INTO REQUIREMENTS_QUE VALUES(";
                        res.Query += $"'{row.Cells[2].Value}', ";
                        res.Query += $"'{ARN}')";
                        if (res.ExecuteNonQuery() == 0)
                        { }
                        res.Close();
                    }

                }
            }          
            this.Close();
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(ViewMode == false)
            {
                if (dgvList.CurrentCell == dgvList[0, e.RowIndex])
                {
                    if (Convert.ToBoolean(dgvList[0, e.RowIndex].Value) == false)
                    {
                        dgvList[0, e.RowIndex].Value = true;
                    }
                    else if (Convert.ToBoolean(dgvList[0, e.RowIndex].Value) == true)
                    {
                        dgvList[0, e.RowIndex].Value = false;
                    }
                }
            }          
        }
    }
}
