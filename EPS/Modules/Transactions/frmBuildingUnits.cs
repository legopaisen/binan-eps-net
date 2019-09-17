using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EPSEntities.Entity;
using System.Data.Entity;
using Oracle.ManagedDataAccess.EntityFramework;
using Oracle.ManagedDataAccess.Client;
using Common.AppSettings;
using EPSEntities.Connection;

namespace Modules.Transactions
{
    public partial class frmBuildingUnits : Form
    {
        public static ConnectionString dbConn = new ConnectionString();
        public string SourceClass { get; set; }
        public string DistCode { get; set; }
        public string BldgNo { get; set; }
        DataGridViewComboBoxColumn comboBrgy = new DataGridViewComboBoxColumn();
        private TextBox tmptxtBox;
        private bool isPressed = false;

        public frmBuildingUnits()
        {
            InitializeComponent();
        }

        private void frmBuildingUnits_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(BldgNo))
                CreateBldgNo();

            UpdateDataControls();
        }

        private void LoadGrid()
        {
            PopulateBrgy();
            comboBrgy.HeaderCell.Value = "Brgy";

            dgvList.Rows.Clear();
            dgvList.Columns.Clear();

            dgvList.Columns.Add("Unit", "Unit");
            dgvList.Columns.Add("HouseNo", "House No");
            dgvList.Columns.Add("LotNo", "Lot No");
            dgvList.Columns.Add("BlkNo", "Blk No");
            dgvList.Columns.Add("Address", "Street Address");
            dgvList.Columns.Insert(5, comboBrgy);

            dgvList.RowHeadersVisible = false;
            dgvList.Columns[0].Width = 100;
            dgvList.Columns[1].Width = 100;
            dgvList.Columns[2].Width = 100;
            dgvList.Columns[3].Width = 100;
            dgvList.Columns[4].Width = 200;
            dgvList.Columns[5].Width = 100;

        }

        private void CreateBldgNo()
        {
            string strQuery = string.Empty;
            int intBldgNo = 0;

            var db = new EPSConnection(dbConn);
            strQuery = $"select max(bldg_no) from building";
            intBldgNo = db.Database.SqlQuery<Int32>(strQuery).SingleOrDefault();

            if (intBldgNo == 0)
                intBldgNo = 1;

            BldgNo = string.Format("{0:####}",intBldgNo);
        }

        //AFM 20190912 accepts numbers only in unit col.(s)
        private void dgvList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            isPressed = false;
            TextBox tb = (dgvList.EditingControl as TextBox);
            if (dgvList.CurrentCell.ColumnIndex != dgvList.Columns["Unit"].Index)
            {
                isPressed = true;
                return;
            }
            tb.KeyPress += new KeyPressEventHandler(tb_KeyPress);
        }

        void tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (isPressed == false)
            {
                if (!char.IsNumber(e.KeyChar) && e.KeyChar != (char)8)
                    e.Handled = true;
            }
        }
        //AFM 20190912 accepts numbers only in unit col.(e)

        private void UpdateDataControls()
        {
            string strQuery = string.Empty;
            LoadGrid();

            var db = new EPSConnection(dbConn);

            strQuery = $"select * from bldg_units where bldg_no = {BldgNo} order by unit_no";
            var epsrec = db.Database.SqlQuery<BLDG_UNITS>(strQuery);

            foreach (var items in epsrec)
            {
                dgvList.Rows.Add(items.UNIT_NO,items.HOUSE_NO,items.LOT_NO,items.BLK_NO,items.STREET_ADDR, items.BRGY);
            }

            dgvList.Rows.Add("");
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string strQuery = string.Empty;
            if (string.IsNullOrEmpty(BldgNo))
                CreateBldgNo();

            var db = new EPSConnection(dbConn);

            strQuery = $"delete from bldg_units where bldg_no = '{BldgNo}'";
            db.Database.ExecuteSqlCommand(strQuery);
            
            for (int i = 0; i < dgvList.Rows.Count; i++)
            {
                try
                {
                    strQuery = $"insert into bldg_units values (:1,:2,:3,:4,:5,:6,:7,:8)";
                    db.Database.ExecuteSqlCommand(strQuery,
                        new OracleParameter(":1", BldgNo),
                        new OracleParameter(":2", i + 1),
                        new OracleParameter(":3", dgvList[0, i].Value.ToString()),
                        new OracleParameter(":4", dgvList[1, i].Value.ToString()),
                        new OracleParameter(":5", dgvList[2, i].Value.ToString()),
                        new OracleParameter(":6", dgvList[3, i].Value.ToString()),
                        new OracleParameter(":7", dgvList[4, i].Value.ToString()),
                        new OracleParameter(":8", dgvList[5, i].Value.ToString()));
                }
                catch { }
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PopulateBrgy()
        {
            comboBrgy.Items.Clear();

            DataTable dataTable = new DataTable("Barangay");
            dataTable.Columns.Clear();
            dataTable.Columns.Add("Code", typeof(String));
            dataTable.Columns.Add("Desc", typeof(String));

            BarangayList lstBrgy = new BarangayList();
            lstBrgy.GetBarangayList(DistCode, true);
            int intCount = lstBrgy.BarangayNames.Count;
            for (int i = 0; i < intCount; i++)
            {
                dataTable.Rows.Add(new String[] { lstBrgy.BarangayCodes[i], lstBrgy.BarangayNames[i] });
            }

            comboBrgy.DataSource = dataTable;
            comboBrgy.DisplayMember = "Desc";
            comboBrgy.ValueMember = "Desc";
            
        }
    }
}
