using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using EPSEntities.Entity;
using System.Data;
using System.Collections;
using Oracle.ManagedDataAccess.EntityFramework;
using Oracle.ManagedDataAccess.Client;
using Common.AppSettings;
using System.Windows.Forms;
using RPTEntities.Entity;
using RPTEntities;

namespace Modules.Transactions
{
    public partial class frmSearchLot : Form
    {
        public string AcctNo = string.Empty;
        public string LotPIN = string.Empty;
        public string LotNo = string.Empty;
        public string BlkNo = string.Empty;
        public string BrgyName = string.Empty;
        public string City = string.Empty;
        public string Prov = string.Empty;
        BarangayList brgyList = new BarangayList();
        //DistrictList distList = new DistrictList();

        public frmSearchLot()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
           if (string.IsNullOrEmpty(txtLotPIN.Text.Trim()) && 
                string.IsNullOrEmpty(txtLastName.Text.Trim()) &&
                string.IsNullOrEmpty(txtFirstName.Text.Trim()))
            {
                MessageBox.Show("Specify search criteria","EPS",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                return;
            }
            string strQuery = string.Empty;

            using (RPTEntities.Entities db = new RPTEntities.Entities())
            {
                strQuery = $"select faas_land.land_pin as PIN, own_names.own_code as OWN_CODE, own_names.own_ln as OWN_LN, ";
                strQuery += $"own_names.own_fn as OWN_FN, own_names.own_mi as OWN_MI,";
                strQuery += $"(own_names.own_addr||' '||own_names.own_brgy||' '||own_names.own_mun) as OWN_ADDR ";
                strQuery += $"from faas_land, own_names ";
                strQuery += $"where faas_land.own_code = own_names.own_code ";

                if (!string.IsNullOrEmpty(txtLotPIN.Text.Trim()))
                    strQuery += $"and faas_land.land_pin like '{txtLotPIN.Text.Trim()}%'";
                else
                {
                    if (!string.IsNullOrEmpty(txtLastName.Text.Trim()))
                        strQuery += $" and own_names.own_ln like '{txtLastName.Text.Trim()}%'";
                    if (!string.IsNullOrEmpty(txtFirstName.Text.Trim()))
                        strQuery += $" and own_names.own_fn like '{txtFirstName.Text.Trim()}%'";
                }
                var rec = db.Database.SqlQuery<RPTEntities.Entity.LAND_INFO>(strQuery);

                foreach (var items in rec)
                {
                    LotPIN = items.PIN;
                    
                    strQuery = $"select * from faas_land where land_pin = '{LotPIN}'";
                    var faasrec = db.Database.SqlQuery<FAAS_LAND>(strQuery);

                    foreach (var faasitems in faasrec)
                    {
                        LotNo = faasitems.LOT_NO;
                        BlkNo = faasitems.BLK_NO;
                        brgyList = new BarangayList();
                        brgyList.GetBarangayList(LotPIN.Substring(4, 2), true);
                        BrgyName = brgyList.GetBarangayName(LotPIN.Substring(4, 2), LotPIN.Substring(7, 3));
                        //City = distList.GetDistrictName(LotPIN.Substring(4,2));
                        City = AppSettingsManager.GetConfigValue("02");

                        dgvList.Rows.Add(LotPIN, items.OWN_CODE, items.OWN_LN, items.OWN_FN, items.OWN_MI, items.OWN_ADDR,LotNo,BlkNo,BrgyName, City);
                    }
                }
            }
        }

        private void frmSearchLot_Load(object sender, EventArgs e)
        {
            txtLotPIN.Focus();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtLotPIN.Text.Trim()) &&
                string.IsNullOrEmpty(txtLastName.Text.Trim()) &&
                string.IsNullOrEmpty(txtFirstName.Text.Trim()))
            {
                MessageBox.Show("No record selected", "EPS", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            this.Close();
        }

        private void ClearControls()
        {
            txtLotPIN.Text = "";
            txtLastName.Text = "";
            txtFirstName.Text = "";
            txtMI.Text = "";
            AcctNo = "";
            LotNo = "";
            BlkNo = "";
            BrgyName = "";
            City = "";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtLotPIN.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtFirstName.Text = string.Empty;
            dgvList.Rows.Clear();
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvList[0, e.RowIndex].Value != null)
                txtLotPIN.Text = dgvList[0, e.RowIndex].Value.ToString();
            else
                txtLotPIN.Text = "";
            if (dgvList[2, e.RowIndex].Value != null)
                txtLastName.Text = dgvList[2, e.RowIndex].Value.ToString();
            else
                txtLastName.Text = "";
            if (dgvList[3, e.RowIndex].Value != null)
                txtFirstName.Text = dgvList[3, e.RowIndex].Value.ToString();
            else
                txtFirstName.Text = "";
            if (dgvList[4, e.RowIndex].Value != null)
                txtMI.Text = dgvList[4, e.RowIndex].Value.ToString();
            else
                txtMI.Text = "";
            if (dgvList[1, e.RowIndex].Value != null)
                AcctNo = dgvList[1, e.RowIndex].Value.ToString();
            else
                AcctNo = "";
            if (dgvList[6, e.RowIndex].Value != null)
                LotNo = dgvList[6, e.RowIndex].Value.ToString();
            else
                LotNo = "";
            if (dgvList[7, e.RowIndex].Value != null)
                BlkNo = dgvList[7, e.RowIndex].Value.ToString();
            else
                BlkNo = "";
            if (dgvList[8, e.RowIndex].Value != null)
                BrgyName = dgvList[8, e.RowIndex].Value.ToString();
            else
                BrgyName = "";
            if (dgvList[9, e.RowIndex].Value != null)
                City = dgvList[9, e.RowIndex].Value.ToString();
            else
                City = "";
            
            
        }
    }
}
