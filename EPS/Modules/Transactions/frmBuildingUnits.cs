﻿using System;
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
using Common.DataConnector;

namespace Modules.Transactions
{
    public partial class frmBuildingUnits : Form
    {
        public static ConnectionString dbConn = new ConnectionString();
        public string SourceClass { get; set; }
        public string DistCode { get; set; }
        public string BldgNo { get; set; }
        DataGridViewComboBoxColumn comboBrgy = new DataGridViewComboBoxColumn();

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
            dgvList.Columns.Add("Vill", "Subdivision");
            dgvList.Columns.Insert(5, comboBrgy);

            dgvList.RowHeadersVisible = false;
            dgvList.Columns[0].Width = 100;
            dgvList.Columns[1].Width = 100;
            dgvList.Columns[2].Width = 100;
            dgvList.Columns[3].Width = 100;
            dgvList.Columns[4].Width = 200;
            dgvList.Columns[5].Width = 100;
            dgvList.Columns[6].Width = 100;

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

            BldgNo = string.Format("{0:####}",intBldgNo + 1); //AFM 20190927
        }

        private void UpdateDataControls()
        {
            string strQuery = string.Empty;
            LoadGrid();

            var db = new EPSConnection(dbConn);

            strQuery = $"select * from bldg_units where bldg_no = {BldgNo} order by unit_no";
            var epsrec = db.Database.SqlQuery<BLDG_UNITS>(strQuery);

            foreach (var items in epsrec)
            {
                dgvList.Rows.Add(items.UNIT_NO,items.HOUSE_NO,items.LOT_NO,items.BLK_NO,items.STREET_ADDR, items.BRGY, items.BLDG_VILL);
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

            bool bPrev = false;
            for (int i = 0; i < dgvList.Rows.Count; i++) //AFM 20200911 condition to check each row if incomplete (s)
            {
                    try
                    {
                    if ((dgvList[0, i].Value == null || dgvList[0, i].Value.ToString() == "" ||
                        dgvList[1, i].Value == null || dgvList[1, i].Value.ToString() == "" ||
                        dgvList[2, i].Value == null || dgvList[2, i].Value.ToString() == "" ||
                        dgvList[3, i].Value == null || dgvList[3, i].Value.ToString() == "" ||
                        dgvList[4, i].Value == null || dgvList[4, i].Value.ToString() == "" ||
                        dgvList[5, i].Value == null || dgvList[5, i].Value.ToString() == "") &&
                        bPrev == false)
                    {
                        MessageBox.Show("Please complete details!");
                        return;
                    }
                    else if ((!(string.IsNullOrEmpty(dgvList[0, i].Value?.ToString().Trim())) ||
                            !(string.IsNullOrEmpty(dgvList[1, i].Value?.ToString().Trim())) ||
                            !(string.IsNullOrEmpty(dgvList[2, i].Value?.ToString().Trim())) ||
                            !(string.IsNullOrEmpty(dgvList[3, i].Value?.ToString().Trim())) ||
                            !(string.IsNullOrEmpty(dgvList[4, i].Value?.ToString().Trim())) ||
                            !(string.IsNullOrEmpty(dgvList[5, i].Value?.ToString().Trim()))) &&
                            bPrev == true)
                    {
                        if (((string.IsNullOrEmpty(dgvList[0, i].Value?.ToString().Trim())) ||
                        (string.IsNullOrEmpty(dgvList[1, i].Value?.ToString().Trim())) ||
                        (string.IsNullOrEmpty(dgvList[2, i].Value?.ToString().Trim())) ||
                        (string.IsNullOrEmpty(dgvList[3, i].Value?.ToString().Trim())) ||
                        (string.IsNullOrEmpty(dgvList[4, i].Value?.ToString().Trim())) ||
                        (string.IsNullOrEmpty(dgvList[5, i].Value?.ToString().Trim()))))
                        {
                            MessageBox.Show("Please complete details!");
                            return;
                        }

                    }
                        else
                            bPrev = true;
                    }
                    catch (Exception ex) { }
            }
            //AFM 20200911 condition to check each row if incomplete (s)

            for (int i = 0; i < dgvList.Rows.Count; i++)
            {
                try
                {
                    strQuery = $"insert into bldg_units values (:1,:2,:3,:4,:5,:6,:7,:8,:9)";
                    db.Database.ExecuteSqlCommand(strQuery,
                        new OracleParameter(":1", BldgNo),
                        new OracleParameter(":2", i + 1),
                        new OracleParameter(":3", dgvList[0, i].Value.ToString()),
                        new OracleParameter(":4", dgvList[1, i].Value.ToString()),
                        new OracleParameter(":5", dgvList[2, i].Value.ToString()),
                        new OracleParameter(":6", dgvList[3, i].Value.ToString()),
                        new OracleParameter(":7", dgvList[4, i].Value.ToString()),
                        new OracleParameter(":8", dgvList[5, i].Value.ToString()),
                        new OracleParameter(":9", dgvList[6, i].Value.ToString()));

                }
                catch(Exception ex) { }
            }

                this.Close();
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void ClearBuilding() //AFM 20190927
        {
            OracleResultSet result = new OracleResultSet();
            OracleResultSet result2 = new OracleResultSet();

            result.Query = "select * from bldg_units where bldg_no not in (select bldg_no from building)";
            if (result.Execute())
                if (result.Read())
                {
                    result2.Query = "delete from bldg_units where bldg_no not in (select bldg_no from building)";
                    result2.ExecuteNonQuery();
                }
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
            dataTable.Rows.Add(new String[] { "", "" }); //AFM 20200909 blank item to prevent auto selecting first item which causes null value error
            for (int i = 0; i < intCount; i++)
            {
                dataTable.Rows.Add(new String[] { lstBrgy.BarangayCodes[i], lstBrgy.BarangayNames[i] });
            }

            comboBrgy.DataSource = dataTable;
            comboBrgy.DisplayMember = "Desc";
            comboBrgy.ValueMember = "Desc";
            
        }

        private void dgvList_CellEnter(object sender, DataGridViewCellEventArgs e) //AFM 20200909
        {
            bool validClick = (e.RowIndex != -1 && e.ColumnIndex != -1);
            var datagridview = sender as DataGridView;

            if (datagridview.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn && validClick)
            {
                datagridview.BeginEdit(true);
                ((ComboBox)datagridview.EditingControl).DroppedDown = true;
            }
        }

        private void dgvList_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
           dgvList.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                dgvList.Rows.RemoveAt(dgvList.CurrentRow.Index); //AFM 20200910
            }
            catch { }
        }
    }
}
