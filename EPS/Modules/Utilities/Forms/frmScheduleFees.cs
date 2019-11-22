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
using EPSEntities.Entity;
using Oracle.ManagedDataAccess.Client;
using Common.AppSettings;
using Common.StringUtilities;
using Common.DataConnector;

namespace Modules.Utilities.Forms
{
    public partial class frmScheduleFees : Form
    {
        public static ConnectionString dbConn = new ConnectionString();
        private DataGridViewComboBoxColumn comboTerm = new DataGridViewComboBoxColumn();
        private DataGridViewComboBoxColumn comboMeans = new DataGridViewComboBoxColumn();
        private DataGridViewComboBoxColumn comboAreaNeeded = new DataGridViewComboBoxColumn();
        private DataGridViewComboBoxColumn comboCumulative = new DataGridViewComboBoxColumn();
        private string m_sFeesCode = string.Empty;
        private string m_sFeesDesc = string.Empty;
        private string m_sFeesMeans = string.Empty;
        private string m_sMainCode = string.Empty;
        private int m_iMajorFeeRow = 0;

        public frmScheduleFees()
        {
            InitializeComponent();
        }

        private void frmScheduleFees_Load(object sender, EventArgs e)
        {
            PopulateType();
            PopulateRevenueAccount();
            PopulateStructure();
            PopulateScope();
            PopulateCategory();
            EnableControls(false);
            InitializeControls();
            try
            {
                dgvMajorFees.CurrentCell.Selected = false;
            }
            catch { }
        }

        private void InitializeControls()
        {
            m_sFeesCode = string.Empty;
            m_sFeesDesc = string.Empty;
            m_sFeesMeans = string.Empty;
            m_sMainCode = string.Empty;
            m_iMajorFeeRow = 0;

        }

        private void PopulateRevenueAccount()
        {
            cmbRevenueAcct.DataSource = null;
            cmbRevenueAcct.Items.Clear();

            DataTable dataTable = new DataTable("MajorFee");
            dataTable.Columns.Clear();
            dataTable.Columns.Add("Code", typeof(String));
            dataTable.Columns.Add("Desc", typeof(String));
            dataTable.Columns.Add("Type", typeof(String));

            var result = from a in Utilities.MajorFeesList.GetMajorFeesListd()
                         select a;
            foreach (var item in result)
            { 
                dataTable.Rows.Add(new String[] { item.FEES_CODE, item.FEES_DESC, item.FEES_TYPE });
            }

            cmbRevenueAcct.DataSource = dataTable;
            cmbRevenueAcct.DisplayMember = "Desc";
            cmbRevenueAcct.ValueMember = "Desc";
            cmbRevenueAcct.SelectedIndex = 0;
        }

        private void PopulateStructure()
        {
            dgvStruc.Rows.Clear();

            StructureList lstStructure = new StructureList();

            int iCnt = lstStructure.StructureLst.Count;

            for (int i = 0; i < iCnt; i++)
            {
                dgvStruc.Rows.Add(false, lstStructure.StructureLst[i].Desc, lstStructure.StructureLst[i].Code);
            }
                        
        }

        private void PopulateScope()
        {
            dgvScope.Rows.Clear();

            ScopeList m_lstScope = new ScopeList();
            int iCnt = m_lstScope.ScopeLst.Count;
            
            for (int i = 0; i < iCnt; i++)
            {
                dgvScope.Rows.Add(false, m_lstScope.ScopeLst[i].ScopeDesc, m_lstScope.ScopeLst[i].ScopeCode);
            }
        }

        private void EnableControls(bool bEnable)
        {
            cmbType.Enabled = bEnable;
            cmbSubAcctDesc.Enabled = bEnable;
            dgvMajorFees.ReadOnly = !bEnable;
            dgvSchedule.ReadOnly = !bEnable;
            txtRevenueAcctNew.ReadOnly = !bEnable;
            txtSubAcctDescNew.ReadOnly = !bEnable;
            dgvScope.ReadOnly = !bEnable;
            dgvCategory.ReadOnly = !bEnable;
            dgvStruc.ReadOnly = !bEnable;
        }

        private void dgvMajorFees_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnAdd.Text == "Save" || btnEdit.Text == "Update")
            {
                if (e.ColumnIndex == 0)
                    dgvMajorFees.ReadOnly = true;
                else
                    dgvMajorFees.ReadOnly = false;
            }

            m_sFeesCode = string.Empty;
            m_sFeesDesc = string.Empty;
            m_sFeesMeans = string.Empty;
            string sFeesTerm = string.Empty;
            string sFeesUnit = string.Empty;
            string sScope = string.Empty;
            string sCategory = string.Empty;
            string sCumulative = string.Empty;
            string sArea = string.Empty;
            string sStruc = string.Empty;
            m_iMajorFeeRow = 0;
            
            m_iMajorFeeRow = e.RowIndex;
            try
            {
                m_sFeesCode = dgvMajorFees[0, e.RowIndex].Value.ToString();
            }
            catch { }
            try
            {
                m_sFeesDesc = dgvMajorFees[1, e.RowIndex].Value.ToString();
            }
            catch { }
            try
            {
                m_sFeesMeans = dgvMajorFees[3, e.RowIndex].Value.ToString();
            }
            catch { }
            try
            {
                sScope = dgvMajorFees[5, e.RowIndex].Value.ToString();
            } catch { }
            try
            {
                sCategory = dgvMajorFees[6, e.RowIndex].Value.ToString();
            }
            catch { }
            try
            {
                sCumulative = dgvMajorFees[7, e.RowIndex].Value.ToString();
            }
            catch { }
            try
            {
                sArea = dgvMajorFees[8, e.RowIndex].Value.ToString();
            }
            catch { }
            try
            {
                sStruc = dgvMajorFees[9, e.RowIndex].Value.ToString();
            }
            catch
            { }

            PopulateSchedule();
            SetCheckStructure(sStruc);
            SetCheckScope(sScope);
            SetCheckCategory(sCategory);
        }

        private void cmbRevenueAcct_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtRevenueAcct.Text = ((DataRowView)cmbRevenueAcct.SelectedItem)["Code"].ToString();
                cmbType.Text = ((DataRowView)cmbRevenueAcct.SelectedItem)["Type"].ToString();
            

            var db = new EPSConnection(dbConn);
            string strQuery = string.Empty;

            strQuery = "delete from subcategories_x";
            db.Database.ExecuteSqlCommand(strQuery);

            strQuery = "delete from schedules_x";
            db.Database.ExecuteSqlCommand(strQuery);

            strQuery = $"insert into subcategories_x select * from subcategories where fees_code like '{txtRevenueAcct.Text.ToString()}%'";
            db.Database.ExecuteSqlCommand(strQuery);

            strQuery = $"insert into schedules_x select * from schedules where fees_code like '{txtRevenueAcct.Text.ToString()}%'";
            db.Database.ExecuteSqlCommand(strQuery);

            PopulateSubCategory();

            cmbSubAcctDesc.Enabled = true;
            }
            catch { }
        }

        private void PopulateSubCategory()
        {
            var db = new EPSConnection(dbConn);
            string strQuery = string.Empty;

            cmbSubAcctDesc.DataSource = null;
            cmbSubAcctDesc.Items.Clear();

            DataTable dataTable = new DataTable("Subcategory");
            dataTable.Columns.Clear();
            dataTable.Columns.Add("Code", typeof(String));
            dataTable.Columns.Add("Desc", typeof(String));
            dataTable.Rows.Add(new String[] { "", ""});

            strQuery = $"select * from subcategories_x where fees_code like '{txtRevenueAcct.Text.ToString()}%' and fees_term = 'SUBCATEGORY' order by fees_code ";
            var epsrec = db.Database.SqlQuery<SUBCATEGORIES_X>(strQuery);

            foreach (var items in epsrec)
            {
                dataTable.Rows.Add(new String[] { items.FEES_CODE.Substring(2,2), items.FEES_DESC });
            }

            cmbSubAcctDesc.DataSource = dataTable;
            cmbSubAcctDesc.DisplayMember = "Desc";
            cmbSubAcctDesc.ValueMember = "Desc";
            cmbSubAcctDesc.SelectedIndex = 1;
        }

        private void cmbSubAcctDesc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtSubAcctDesc.Text = ((DataRowView)cmbSubAcctDesc.SelectedItem)["Code"].ToString();
                txtSubAcctDescNew.Text = "";
                txtSubAcctDescNew.Visible = false;
                PopulateMajorFees();
                dgvMajorFees.CurrentCell = null;

            }
            catch { }
        }

        private void PopulateMajorFees()
        {
            var db = new EPSConnection(dbConn);
            string strQuery = string.Empty;
            string sFeesCode = string.Empty;
            string sFeesDesc = string.Empty;
            string sFeesTerm = string.Empty;
            string sFeesMeans = string.Empty;
            string sFeesUnit = string.Empty;
            string sScope = string.Empty;
            string sCategory = string.Empty;
            string sCumulative = string.Empty;
            string sArea = string.Empty;
            string sStruc = string.Empty;

            LoadGridMajorFees();

            sFeesCode = txtRevenueAcct.Text.ToString() + txtSubAcctDesc.Text.ToString();

            if (!string.IsNullOrEmpty(txtRevenueAcct.Text.ToString()) &&
                !string.IsNullOrEmpty(txtSubAcctDesc.Text.ToString()))
            {
                strQuery = $"select * from subcategories_x where fees_code like '{sFeesCode}%' order by fees_code";
                var epsrec = db.Database.SqlQuery<SUBCATEGORIES_X>(strQuery);

                foreach (var items in epsrec)
                {
                    sFeesCode = items.FEES_CODE;
                    sFeesDesc = items.FEES_DESC;
                    sFeesTerm = items.FEES_TERM;
                    sFeesMeans = items.FEES_MEANS;
                    sFeesUnit = items.FEES_UNIT;
                    sScope = items.SCOPE_CODE;
                    sCategory = items.CATEGORY_CODE;
                    sCumulative = items.CUMULATIVE;
                    sArea = items.AREA_NEEDED;
                    sStruc = items.STRUC_CODE;

                    if (sFeesMeans == "FA")
                        sFeesMeans = "FIXED AMOUNT";
                    else
                    if (sFeesMeans == "FR")
                        sFeesMeans = "FIXED RATE";
                    else
                    if (sFeesMeans == "QN")
                        sFeesMeans = "QUANTITY NO";
                    else
                    if (sFeesMeans == "QR")
                        sFeesMeans = "QUANTITY RANGE";
                    else
                    if (sFeesMeans == "RR")
                        sFeesMeans = "RATE RANGE";
                    else
                    if (sFeesMeans == "AR")
                        sFeesMeans = "AREA RANGE";

                    if (sFeesTerm != "SUBCATEGORY")
                    {
                        dgvMajorFees.Rows.Add(sFeesCode, sFeesDesc, sFeesTerm, sFeesMeans, sFeesUnit, sScope, sCategory, sCumulative, sArea, sStruc);
                    }
                }
            }

        }

        private void LoadGridMajorFees()
        {
            dgvMajorFees.Rows.Clear();
            dgvMajorFees.Columns.Clear();
            comboTerm.HeaderCell.Value = "Term";
            comboMeans.HeaderCell.Value = "Means";
            comboAreaNeeded.HeaderCell.Value = "Need Area";
            comboCumulative.HeaderCell.Value = "Cumulative";

            PopulateTerm();
            PopulateMeans();
            PopulateNeedArea();
            PopulateCumulative();

            dgvMajorFees.Columns.Add("Code", "Code");
            dgvMajorFees.Columns.Add("Subsidiaries", "Subsidiaries");
            dgvMajorFees.Columns.Insert(2, comboTerm);
            dgvMajorFees.Columns.Insert(3, comboMeans);
            dgvMajorFees.Columns.Add("Unit", "Unit");
            dgvMajorFees.Columns.Add("Scope", "Scope");
            dgvMajorFees.Columns.Add("Category", "Category");
            dgvMajorFees.Columns.Insert(7, comboCumulative);
            dgvMajorFees.Columns.Insert(8, comboAreaNeeded);
            dgvMajorFees.Columns.Add("Structure", "Structure");
            dgvMajorFees.RowHeadersVisible = false;
            dgvMajorFees.Columns[0].Width = 50;
            dgvMajorFees.Columns[1].Width = 200;
            dgvMajorFees.Columns[2].Width = 100;
            dgvMajorFees.Columns[3].Width = 100;
            dgvMajorFees.Columns[4].Width = 100;
            dgvMajorFees.Columns[5].Width = 100;
            dgvMajorFees.Columns[6].Width = 100;
            dgvMajorFees.Columns[7].Width = 100;
            dgvMajorFees.Columns[8].Width = 100;
            dgvMajorFees.Columns[9].Width = 100;
            dgvMajorFees.Columns[5].Visible = false;
            dgvMajorFees.Columns[6].Visible = false;
            dgvMajorFees.Columns[9].Visible = false;
        }

        private void PopulateTerm()
        {
            comboTerm.Items.Clear();
            comboTerm.Items.Add("ANNUAL-Q");
            comboTerm.Items.Add("ANNUAL");
            comboTerm.Items.Add("MONTHLY");
            comboTerm.Items.Add("DAILY");
            comboTerm.Items.Add("ONCE");
        }
            
        private void PopulateMeans()
        {
            comboMeans.Items.Clear();
            comboMeans.Items.Add("FIXED AMOUNT");
            comboMeans.Items.Add("FIXED RATE");
            comboMeans.Items.Add("QUANTITY NO");
            comboMeans.Items.Add("QUANTITY RANGE");
            comboMeans.Items.Add("RATE RANGE");
            comboMeans.Items.Add("AREA RANGE");
        }

        private void PopulateNeedArea()
        {
            comboAreaNeeded.Items.Clear();
            comboAreaNeeded.Items.Add("Y");
            comboAreaNeeded.Items.Add("N");
        }

        private void PopulateCumulative()
        {
            comboCumulative.Items.Clear();
            comboCumulative.Items.Add("Y");
            comboCumulative.Items.Add("N");
        }
        

        private void PopulateType()
        {
            cmbType.Items.Clear();
            cmbType.Items.Add("TAX");
            cmbType.Items.Add("FEE");
            cmbType.Items.Add("CHARGE");
            cmbType.Items.Add("PENALTY");

        }

        private void PopulateSchedule()
        {
            var db = new EPSConnection(dbConn);
            string strQuery = string.Empty;
            double dValue = 0;
            dgvSchedule.Rows.Clear();
            dgvSchedule.Columns.Clear();

            if (m_sFeesMeans == "FIXED AMOUNT" || m_sFeesMeans == "FA")
            {
                m_sFeesMeans = "FA";
                strQuery = $"select * from schedules_x where fees_code = '{m_sFeesCode}'";
                var epsrec = db.Database.SqlQuery<SCHEDULES_X>(strQuery);
                foreach (var items in epsrec)
                {
                    double.TryParse(items.AMOUNT1.ToString(), out dValue);
                }
                dgvSchedule.Columns.Add("Fixed", "Fixed Amount");
                dgvSchedule.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvSchedule.Columns[0].Width = 100;
                dgvSchedule.Rows.Add(dValue);
            }
            if (m_sFeesMeans == "FIXED RATE" || m_sFeesMeans == "FR")
            {
                m_sFeesMeans = "FR";
                strQuery = $"select * from schedules_x where fees_code = '{m_sFeesCode}'";
                var epsrec = db.Database.SqlQuery<SCHEDULES_X>(strQuery);
                foreach (var items in epsrec)
                {
                    double.TryParse(items.RATE1.ToString(), out dValue);
                }
                dgvSchedule.Columns.Add("Fixed", "Fixed Rate");
                dgvSchedule.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvSchedule.Columns[0].Width = 100;
                dgvSchedule.Rows.Add(dValue);
            }
            if (m_sFeesMeans == "QUANTITY NO" || m_sFeesMeans == "QN")
            {
                m_sFeesMeans = "QN";
                double dAmount1 = 0;
                double dAmount2 = 0;
                strQuery = $"select * from schedules_x where fees_code = '{m_sFeesCode}'";
                strQuery += " order by amount1";
                var epsrec = db.Database.SqlQuery<SCHEDULES_X>(strQuery);
                foreach (var items in epsrec)
                {
                    double.TryParse(items.AMOUNT1.ToString(), out dAmount1);
                    double.TryParse(items.AMOUNT2.ToString(), out dAmount2);
                }
                dgvSchedule.Columns.Add("Amount", "Amount Per Unit");
                dgvSchedule.Columns.Add("MinFee", "Minimum Fee");
                dgvSchedule.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvSchedule.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvSchedule.Columns[0].Width = 100;
                dgvSchedule.Columns[1].Width = 100;
                dgvSchedule.Rows.Add(dAmount1, dAmount2);
            }

            if (m_sFeesMeans == "QUANTITY RANGE" || m_sFeesMeans == "QR")
            {
                m_sFeesMeans = "QR";
                double dQty1 = 0;
                double dQty2 = 0;
                double dAmount1 = 0;
                double dRate2 = 0;
                int iRowCnt = 0;
                dgvSchedule.Columns.Add("FeesCode", "Fees Code");
                dgvSchedule.Columns.Add("From", "From Qty");
                dgvSchedule.Columns.Add("To", "To Qty");
                dgvSchedule.Columns.Add("Amount", "Amount");
                dgvSchedule.Columns.Add("Plus", "Plus");
                dgvSchedule.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvSchedule.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvSchedule.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvSchedule.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvSchedule.Columns[0].Width = 100;
                dgvSchedule.Columns[1].Width = 100;
                dgvSchedule.Columns[2].Width = 100;
                dgvSchedule.Columns[3].Width = 100;
                dgvSchedule.Columns[0].Visible = false;

                strQuery = $"select * from schedules_x where fees_code = '{m_sFeesCode}'";
                strQuery += " order by qty1";
                var epsrec = db.Database.SqlQuery<SCHEDULES_X>(strQuery);
                foreach (var items in epsrec)
                {
                    iRowCnt++;
                    double.TryParse(items.QTY1.ToString(), out dQty1);
                    double.TryParse(items.QTY2.ToString(), out dQty2);
                    double.TryParse(items.AMOUNT1.ToString(), out dAmount1);
                    double.TryParse(items.RATE2.ToString(), out dRate2);

                    dgvSchedule.Rows.Add(m_sFeesCode, dQty1, dQty2, dAmount1, dRate2);
                }

                if(iRowCnt == 0)
                {
                    dgvSchedule.Rows.Add(m_sFeesCode, 0, 0, 0, 0);
                }
            }

            if (m_sFeesMeans == "RATE RANGE"|| m_sFeesMeans == "RR")
            {
                m_sFeesMeans = "RR";
                double dRange1 = 0;
                double dRange2 = 0;
                double dAmount1 = 0;
                double dRate1 = 0;
                int iRowCnt = 0;
                dgvSchedule.Columns.Add("FeesCode", "Fees Code");
                dgvSchedule.Columns.Add("From", "From Range");
                dgvSchedule.Columns.Add("To", "To Range");
                dgvSchedule.Columns.Add("Amount", "Amount");
                dgvSchedule.Columns.Add("Rate", "Rate");
                dgvSchedule.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvSchedule.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvSchedule.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvSchedule.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvSchedule.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvSchedule.Columns[0].Width = 100;
                dgvSchedule.Columns[1].Width = 100;
                dgvSchedule.Columns[2].Width = 100;
                dgvSchedule.Columns[3].Width = 100;
                dgvSchedule.Columns[4].Width = 100;
                dgvSchedule.Columns[0].Visible = false;

                strQuery = $"select * from schedules_x where fees_code = '{m_sFeesCode}'";
                strQuery += " order by range1";
                var epsrec = db.Database.SqlQuery<SCHEDULES_X>(strQuery);
                foreach (var items in epsrec)
                {
                    iRowCnt++;
                    double.TryParse(items.RANGE1.ToString(), out dRange1);
                    double.TryParse(items.RANGE2.ToString(), out dRange2);
                    double.TryParse(items.AMOUNT1.ToString(), out dAmount1);
                    double.TryParse(items.RATE1.ToString(), out dRate1);
                   
                    dgvSchedule.Rows.Add(m_sFeesCode, dRange1, dRange2, dAmount1, dRate1);
                }

                if (iRowCnt == 0)
                {
                    dgvSchedule.Rows.Add(m_sFeesCode, 0, 0, 0, 0);
                }
            }
            if (m_sFeesMeans == "AREA RANGE" || m_sFeesMeans == "AR")
            {
                m_sFeesMeans = "AR";
                double dRange1 = 0;
                double dRange2 = 0;
                double dAmount1 = 0;
                double dRate2 = 0;
                int iRowCnt = 0;
                dgvSchedule.Columns.Add("FeesCode", "Fees Code");
                dgvSchedule.Columns.Add("From", "From Area");
                dgvSchedule.Columns.Add("To", "To Area");
                dgvSchedule.Columns.Add("Amount", "Amount");
                dgvSchedule.Columns.Add("Plus", "Plus");
                dgvSchedule.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvSchedule.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvSchedule.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvSchedule.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvSchedule.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvSchedule.Columns[0].Width = 100;
                dgvSchedule.Columns[1].Width = 100;
                dgvSchedule.Columns[2].Width = 100;
                dgvSchedule.Columns[3].Width = 100;
                dgvSchedule.Columns[4].Width = 100;
                dgvSchedule.Columns[0].Visible = false;

                strQuery = $"select * from schedules_x where fees_code = '{m_sFeesCode}'";
                strQuery += " order by range1";
                var epsrec = db.Database.SqlQuery<SCHEDULES_X>(strQuery);
                foreach (var items in epsrec)
                {
                    iRowCnt++;
                    double.TryParse(items.RANGE1.ToString(), out dRange1);
                    double.TryParse(items.RANGE2.ToString(), out dRange2);
                    double.TryParse(items.AMOUNT1.ToString(), out dAmount1);
                    double.TryParse(items.RATE2.ToString(), out dRate2);
                    
                    dgvSchedule.Rows.Add(m_sFeesCode, dRange1, dRange2, dAmount1, dRate2);
                    
                }
                if (iRowCnt == 0)
                {
                    dgvSchedule.Rows.Add(m_sFeesCode, 0, 0, 0, 0);
                }

            }
        }

        private void SetCheckStructure(string sStruc)
        {
            if (sStruc == "") //AFM 20191122 if all structure is unchecked (s)
            {
                for (int i = 0; i < dgvStruc.Rows.Count; i++)
                {
                    try
                    {
                            dgvStruc[0, i].Value = false;
                    }
                    catch { }
                }
                return;  //AFM 20191122 if all structure is unchecked (e)
            }
            string[] separator = { "|" };
            int iCount = dgvStruc.Rows.Count;

            string[] strlist = sStruc.Split(separator, iCount, StringSplitOptions.RemoveEmptyEntries);

            if (!string.IsNullOrEmpty(sStruc))
            {
                for (int i = 0; i < dgvStruc.Rows.Count; i++)
                {
                    dgvStruc[0, i].Value = false;//reset
                }
            }

            foreach (string s in strlist)
            {
                if (!string.IsNullOrEmpty(s))
                {
                    for (int i = 0; i < dgvStruc.Rows.Count; i++)
                    {
                        string sTmpCat = string.Empty;
                        try
                        {
                            sTmpCat = dgvStruc[2, i].Value.ToString();
                            if (s == sTmpCat)
                            {
                                dgvStruc[0, i].Value = true;
                                break;
                            }
                            
                        }
                        catch { }
                        
                    }
                }
            }

        }

        private void SetCheckCategory(string sCategory)
        {
            if (sCategory == "") //AFM 20191122 if all category is unchecked (s)
            {
                for (int i = 0; i < dgvCategory.Rows.Count; i++)
                {
                    try
                    {
                        dgvCategory[0, i].Value = false;
                    }
                    catch { }
                }
                return;  //AFM 20191122 if all category is unchecked (e)
            }

            string[] separator = { "|" };
            int iCount = dgvCategory.Rows.Count;

            string[] strlist = sCategory.Split(separator, iCount, StringSplitOptions.RemoveEmptyEntries);

            if (!string.IsNullOrEmpty(sCategory))
            {
                for (int i = 0; i < dgvCategory.Rows.Count; i++)
                {
                    dgvCategory[0, i].Value = false;
                }
            }

            foreach (string s in strlist)
            {
                if (!string.IsNullOrEmpty(s))
                {
                    for (int i = 0; i < dgvCategory.Rows.Count; i++)
                    {
                        string sTmpCat = string.Empty;
                        try
                        {
                            sTmpCat = dgvCategory[2, i].Value.ToString();
                            if (s == sTmpCat)
                            {
                                dgvCategory[0, i].Value = true;
                                break;
                            }
                           
                                
                        }
                        catch { }
                        
                    }
                }
            }
        }

        private void SetCheckScope(string sScope)
        {
            if (sScope == "") //AFM 20191122 if all scope is unchecked (s)
            {
                for (int i = 0; i < dgvScope.Rows.Count; i++)
                {
                    try
                    {
                        dgvScope[0, i].Value = false;
                    }
                    catch { }
                }
                return;  //AFM 20191122 if all scope is unchecked (e)
            }

            string[] separator = { "|" };
            int iCount = dgvScope.Rows.Count;

            string[] strlist = sScope.Split(separator, iCount, StringSplitOptions.RemoveEmptyEntries);

            if (!string.IsNullOrEmpty(sScope))
            {
                for (int i = 0; i < dgvScope.Rows.Count; i++)
                {
                    dgvScope[0, i].Value = false;
                }
            }

            foreach (string s in strlist)
            {
                if (!string.IsNullOrEmpty(s))
                {
                    for (int i = 0; i < dgvScope.Rows.Count; i++)
                    {
                        string sTmpCat = string.Empty;
                        try
                        {
                            sTmpCat = dgvScope[2, i].Value.ToString();
                            if (s == sTmpCat)
                            {
                                dgvScope[0, i].Value = true;
                                break;
                            }
                            
                                
                        }
                        catch { }
                        
                    }
                }
            }

            
        }

        private void OnSaveSubCategories(string sFeesCode,string sFeesDesc,string sFeesTerm,string sFeesMeans,
        string sFeesUnit,string sScope,string sCategory,string sCumulative,string sArea,string sStruc)
        {
            var db = new EPSConnection(dbConn);
            string strQuery = string.Empty;
            m_sFeesCode = sFeesCode;

            string sTmpSubAcctDesc = string.Empty;

            if (!string.IsNullOrEmpty(txtSubAcctDescNew.Text.ToString()))
                sTmpSubAcctDesc = txtSubAcctDescNew.Text.ToString();
            else
                sTmpSubAcctDesc = cmbSubAcctDesc.Text.ToString();

            if(string.IsNullOrEmpty(sTmpSubAcctDesc))
            {
                MessageBox.Show("Subcategory description is required",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Stop);
                return;
            }

            string sSubCatCode = txtRevenueAcct.Text.ToString() + txtSubAcctDesc.Text.ToString();
            int iCnt = 0;
            strQuery = $"select count(*) from subcategories_x where fees_desc = '{StringUtilities.HandleApostrophe(sFeesDesc)}' and fees_code <> '{sFeesCode}' ";
            strQuery += $"and fees_term <> 'SUBCATEGORY' and substr(fees_code,1,4) = '{sSubCatCode}'";
            iCnt = db.Database.SqlQuery<Int32>(strQuery).SingleOrDefault();

            if(iCnt > 0)
            {
                MessageBox.Show("Duplicate Fee Description",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Stop);
                return;
            }
            
            if (sFeesMeans == "FIXED AMOUNT")
                m_sFeesMeans = "FA";
            if (sFeesMeans == "FIXED RATE")
                m_sFeesMeans = "FR";
            if (sFeesMeans == "QUANTITY NO")
                m_sFeesMeans = "QN";
            if (sFeesMeans == "QUANTITY RANGE")
                m_sFeesMeans = "QR";
            if (sFeesMeans == "RATE RANGE")
                m_sFeesMeans = "RR";
            if (sFeesMeans == "AREA RANGE")
                m_sFeesMeans = "AR";

            if (string.IsNullOrEmpty(sCumulative))
                sCumulative = "N";
            if (string.IsNullOrEmpty(sArea))
                sArea = "N";
                if (!string.IsNullOrEmpty(sFeesDesc) && !string.IsNullOrEmpty(sFeesTerm) && !string.IsNullOrEmpty(sFeesMeans))
                {
                    strQuery = $"delete from subcategories_x where fees_code = '{sFeesCode}'";
                    db.Database.ExecuteSqlCommand(strQuery);

                    strQuery = $"insert into subcategories_x values (:1,:2,:3,:4,:5,:6,:7,:8,:9,:10)";
                    db.Database.ExecuteSqlCommand(strQuery,
                        new OracleParameter(":1", sFeesCode),
                        new OracleParameter(":2", sFeesDesc),
                        new OracleParameter(":3", sFeesTerm),
                        new OracleParameter(":4", m_sFeesMeans),
                        new OracleParameter(":5", sFeesUnit),
                        new OracleParameter(":6", sCategory),
                        new OracleParameter(":7", sArea),
                        new OracleParameter(":8", sCumulative),
                        new OracleParameter(":9", sScope),
                        new OracleParameter(":10", sStruc));

                    string sTmpCode;
                    sTmpCode = txtRevenueAcct.Text.ToString();
                    sTmpCode += txtSubAcctDesc.Text.ToString();

                    iCnt = 0;
                    strQuery = $"select count(*) from subcategories_x where fees_code = '{sTmpCode}'";
                    iCnt = db.Database.SqlQuery<Int32>(strQuery).SingleOrDefault();

                    if (iCnt == 0)
                    {
                        strQuery = $"insert into subcategories_x values (:1,:2,:3,:4,:5,:6,:7,:8,:9,:10)";
                        db.Database.ExecuteSqlCommand(strQuery,
                            new OracleParameter(":1", sTmpCode),
                            new OracleParameter(":2", sTmpSubAcctDesc),
                            new OracleParameter(":3", "SUBCATEGORY"),
                            new OracleParameter(":4", ""),
                            new OracleParameter(":5", ""),
                            new OracleParameter(":6", ""),
                            new OracleParameter(":7", ""),
                            new OracleParameter(":8", ""),
                            new OracleParameter(":9", ""),
                            new OracleParameter(":10", ""));
                    }

                }
            
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var db = new EPSConnection(dbConn);
            string strQuery = string.Empty;

            if (btnAdd.Text == "Add")
            {
                m_sMainCode = txtRevenueAcct.Text.ToString();
                txtRevenueAcctNew.Visible = true;
                txtSubAcctDescNew.Visible = true;
                cmbRevenueAcct.Visible = false;
                cmbSubAcctDesc.Visible = false;
                ClearForms();

                btnAdd.Text = "Save";
                btnExit.Text = "Cancel";
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                btnPrint.Enabled = false;

                strQuery = "delete from subcategories_x";
                db.Database.ExecuteSqlCommand(strQuery);

                strQuery = "delete from schedules_x";
                db.Database.ExecuteSqlCommand(strQuery);

                string sCode = "00";
                int iCode = 0;
                try
                {
                    strQuery = "select max(fees_code) from major_fees";
                    sCode = db.Database.SqlQuery<string>(strQuery).SingleOrDefault();
                }
                catch { }

                int.TryParse(sCode, out iCode);
                sCode = FormatSeries((iCode + 1).ToString());
                txtRevenueAcct.Text = sCode;
                AddSubcategory(sCode);  

                EnableControls(true);
                LoadGridMajorFees();
                AddMajorFee();

                txtRevenueAcctNew.Focus();
                cmbSubAcctDesc.Visible = false;
            }
            else
            {
                if (string.IsNullOrEmpty(txtRevenueAcctNew.Text.ToString()))
                {
                    MessageBox.Show("Please enter the Revenue Account.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                
                if (string.IsNullOrEmpty(txtSubAcctDescNew.Text.ToString()))
                {
                    MessageBox.Show("Please enter Subcategory description.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                if(string.IsNullOrEmpty(cmbType.Text.ToString()))
                {
                    MessageBox.Show("Please select Type.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                OnSaveRecord("New");
                EnableControls(false);
                btnAdd.Text = "Add";
                btnExit.Text = "Exit";
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                btnPrint.Enabled = true;

            }
        }

        private void ClearForms()
        {
            LoadGridMajorFees();
            PopulateScope();
            PopulateStructure();
            PopulateType();
            txtRevenueAcct.Text = "";
            txtSubAcctDesc.Text = "";
            txtRevenueAcctNew.Text = "";
            txtSubAcctDescNew.Text = "";
            
            

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

        private void AddSubcategory(string sRevCode)
        {
            string sCode = string.Empty;
            string sParam = string.Empty;
            sParam = $"fees_term = 'SUBCATEGORY' and substr(fees_code, 1, 2) = '{sRevCode}'";
            sCode = GenCode(sParam);

            if (!string.IsNullOrEmpty(sCode))
                sCode = sCode.Substring(2, (sCode.Length-2));
            int iCode = 0;
            int.TryParse(sCode, out iCode);
                        
            sCode = FormatSeries((iCode + 1).ToString());

            txtSubAcctDesc.Text = sCode;
            txtSubAcctDescNew.Text = "";
        }

        private string GenCode(string sAddlParam)
        {
            var db = new EPSConnection(dbConn);
            string strQuery = string.Empty;

            string sCode = "00";

            try
            {
                strQuery = "select max(fees_code) from subcategories_x where  ";
                strQuery += $" {sAddlParam} ";
                strQuery += "order by fees_code";
                sCode = db.Database.SqlQuery<string>(strQuery).SingleOrDefault();
            }
            catch { }

            return sCode;
        }

        private void AddMajorFee()
        {
            string sCode = string.Empty;
            string sParam = string.Empty;
            sCode = txtRevenueAcct.Text.ToString() + txtSubAcctDesc.Text.ToString();
            sParam = $"fees_term <> 'SUBCATEGORY' and fees_code like '{sCode}%'";
            sCode = GenCode(sParam);

            if (!string.IsNullOrEmpty(sCode))
                sCode = sCode.Substring(4, (sCode.Length - 4));
            int iCode = 0;
            int.TryParse(sCode, out iCode);

            sCode = FormatSeries((iCode + 1).ToString());
            sCode = txtRevenueAcct.Text.ToString() + txtSubAcctDesc.Text.ToString() + sCode;
            
            //LoadGridMajorFees();
            int iCnt = dgvMajorFees.Rows.Count;
            dgvMajorFees.Rows.Add("");
            dgvMajorFees[0, iCnt].Value = sCode;
        }

        private void OnSaveRecord(string sSwitch)
        {
            var db = new EPSConnection(dbConn);
            string strQuery = string.Empty;
            string sRecordKeep = "N";   //set to N, purpose of this not clear, removed control from UI

            if (MessageBox.Show("Are you sure you want to Save this record?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (sSwitch == "New")
                {
                    strQuery = $"delete from major_fees where fees_code like '{txtRevenueAcct.Text.ToString()}%'";
                    db.Database.ExecuteSqlCommand(strQuery);

                    strQuery = $"insert into major_fees values (:1,:2,:3,:4)";
                    db.Database.ExecuteSqlCommand(strQuery,
                         new OracleParameter(":1", txtRevenueAcct.Text.ToString()),
                         new OracleParameter(":2", StringUtilities.HandleApostrophe(txtRevenueAcctNew.Text.ToString())),
                         new OracleParameter(":3", cmbType.Text.ToString()),
                         new OracleParameter(":4", sRecordKeep));
                }

                string sTmpCode;
                sTmpCode = txtRevenueAcct.Text.ToString();
                sTmpCode += txtSubAcctDesc.Text.ToString(); ;

                strQuery = $"delete from subcategories where fees_code like '{sTmpCode}%'";
                db.Database.ExecuteSqlCommand(strQuery);

                strQuery = $"delete from schedules where fees_code like '{sTmpCode}%'";
                db.Database.ExecuteSqlCommand(strQuery);

                string sCode = string.Empty;

                for (int ii = 0; ii < dgvMajorFees.Rows.Count; ii++)
                {
                    sCode = string.Empty;
                    try
                    {
                        sCode = dgvMajorFees[0, ii].Value.ToString();
                    }
                    catch { }

                    int iCheck = 0;
                    if (!string.IsNullOrEmpty(sCode))
                    {
                        strQuery = $"select count(*) from schedules_x where fees_code = '{sCode}'";
                        iCheck = db.Database.SqlQuery<Int32>(strQuery).SingleOrDefault();
                        if (iCheck > 0)
                        {
                            strQuery = $"insert into subcategories select * from subcategories_x where fees_code = '{sCode}'";
                            db.Database.ExecuteSqlCommand(strQuery);

                            strQuery = $"insert into schedules select * from schedules_x where fees_code = '{sCode}'";
                            db.Database.ExecuteSqlCommand(strQuery);

                            if (sSwitch == "New")
                            {
                                if (Utilities.AuditTrail.InsertTrail("STS-A", "subcategories/schedules", "Fees Code:" + sCode) == 0)
                                {
                                    MessageBox.Show("Failed to insert audit trail.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                            else if (sSwitch == "Update")
                            {
                                if (Utilities.AuditTrail.InsertTrail("STS-E", "subcategories/schedules", "Fees Code:" + sCode) == 0)
                                {
                                    MessageBox.Show("Failed to insert audit trail.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }

                        }
                    }

                    strQuery = $"select count(*) from subcategories where fees_code = '{sTmpCode}'";
                    iCheck = db.Database.SqlQuery<Int32>(strQuery).SingleOrDefault();
                    if (iCheck == 0)
                    {
                        strQuery = $"insert into subcategories select * from subcategories_x where fees_code = '{sTmpCode}'";
                        db.Database.ExecuteSqlCommand(strQuery);
                    }
                }

                MessageBox.Show("Schedule saved", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Transaction cancelled", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);

            PopulateRevenueAccount();
            PopulateStructure();
            PopulateScope();
            PopulateType();

            EnableControls(false);
            cmbRevenueAcct.Visible = true;
            cmbSubAcctDesc.Visible = true;
            txtRevenueAcctNew.Visible = false;
            txtSubAcctDescNew.Visible = false;
            InitializeControls();
            PopulateSchedule();
        }

        private void OnUpdateScopeCatStruc(string sScope, string sCategory, string sStruc)
        {
            OracleResultSet pSet = new OracleResultSet();
            int row = dgvMajorFees.SelectedCells[0].RowIndex;
            pSet.Query = $"UPDATE SUBCATEGORIES_X SET SCOPE_CODE = '{sScope}', CATEGORY_CODE = '{sCategory}', STRUC_CODE = '{sStruc}' where fees_code = '{dgvMajorFees.Rows[row].Cells[0].Value.ToString()}'";
            pSet.ExecuteNonQuery();
            pSet.Close();
        }

        private void dgvCategory_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string sScope = string.Empty;
            string sCategory = string.Empty;
            string sStruc = string.Empty;
            try
            {
                if (dgvMajorFees.CurrentCell.Value.ToString() != "")
                {
                    int row = dgvMajorFees.SelectedCells[0].RowIndex;
                    sScope = SelectScope();
                    dgvMajorFees[5, row].Value = sScope;
                    sCategory = SelectCategory();
                    dgvMajorFees[6, row].Value = sCategory;
                    sStruc = SelectStruc();
                    dgvMajorFees[9, row].Value = sCategory;
                }
            }
            catch { return; }

            OnUpdateScopeCatStruc(sScope, sCategory, sStruc);
        }

        private void dgvScope_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
                string sScope = string.Empty;
                string sCategory = string.Empty;
                string sStruc = string.Empty;
                try
                {
                    if (dgvMajorFees.CurrentCell.Value.ToString() != "")
                    {
                        int row = dgvMajorFees.SelectedCells[0].RowIndex;
                        sScope = SelectScope();
                        dgvMajorFees[5, row].Value = sScope;
                        sCategory = SelectCategory();
                        dgvMajorFees[6, row].Value = sCategory;
                        sStruc = SelectStruc();
                        dgvMajorFees[9, row].Value = sCategory;
                    }
                }
                catch { return; }

                OnUpdateScopeCatStruc(sScope, sCategory, sStruc);
        }

        private void dgvStruc_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string sScope = string.Empty;
            string sCategory = string.Empty;
            string sStruc = string.Empty;
            try
            {
                if (dgvMajorFees.CurrentCell.Value.ToString() != "")
                {
                    int row = dgvMajorFees.SelectedCells[0].RowIndex;
                    sScope = SelectScope();
                    dgvMajorFees[5, row].Value = sScope;
                    sCategory = SelectCategory();
                    dgvMajorFees[6, row].Value = sCategory;
                    sStruc = SelectStruc();
                    dgvMajorFees[9, row].Value = sStruc;
                }
            }
            catch { return; }

            OnUpdateScopeCatStruc(sScope, sCategory, sStruc);
        }

        private void dgvMajorFees_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string sFeesTerm = string.Empty;
            string sFeesUnit = string.Empty;
            string sScope = string.Empty;
            string sCategory = string.Empty;
            string sStruc = string.Empty;
            string sCumulative = string.Empty;
            string sArea = string.Empty;

            m_sFeesDesc = string.Empty;
            m_sFeesMeans = string.Empty;
            m_sFeesCode = string.Empty;

            if(e.ColumnIndex == 1)
            {
                m_sFeesDesc = dgvMajorFees[1, e.RowIndex].Value.ToString();
                dgvMajorFees[1, e.RowIndex].Value = m_sFeesDesc.ToUpper();
            }

            if (e.ColumnIndex == 1 || e.ColumnIndex == 2 || e.ColumnIndex == 3 || e.ColumnIndex == 4
                || e.ColumnIndex == 7 || e.ColumnIndex == 8)
            {
                try
                {
                    m_sFeesCode = dgvMajorFees[0, e.RowIndex].Value.ToString();
                }
                catch { }
                try
                {
                    m_sFeesDesc = dgvMajorFees[1, e.RowIndex].Value.ToString();
                }
                catch { }
                try
                {
                    m_sFeesMeans = dgvMajorFees[3, e.RowIndex].Value.ToString();
                }
                catch { }
                try
                {
                    sFeesTerm = dgvMajorFees[2, e.RowIndex].Value.ToString();
                }
                catch { }
                try {
                    sFeesUnit = dgvMajorFees[4, e.RowIndex].Value.ToString();
                }
                catch { }

                try
                {
                    sCumulative = dgvMajorFees[7, e.RowIndex].Value.ToString();
                }
                catch { }

                try
                {
                    sArea = dgvMajorFees[8, e.RowIndex].Value.ToString();
                }
                catch { }

                sScope = SelectScope();
                dgvMajorFees[5, e.RowIndex].Value = sScope;
                sCategory = SelectCategory();
                dgvMajorFees[6, e.RowIndex].Value = sCategory;
                sStruc = SelectStruc();
                dgvMajorFees[9, e.RowIndex].Value = sCategory;

                OnSaveSubCategories(m_sFeesCode, m_sFeesDesc, sFeesTerm, m_sFeesMeans, sFeesUnit, sScope, sCategory, sCumulative,sArea,sStruc);

                if (!string.IsNullOrEmpty(m_sFeesCode) && !string.IsNullOrEmpty(m_sFeesDesc)
                    && !string.IsNullOrEmpty(sFeesTerm) && !string.IsNullOrEmpty(m_sFeesMeans))
                    AddMajorFee();


                PopulateSchedule();
            }
        }

        private string SelectScope()
        {
            string sCode = string.Empty;
            string sScope = string.Empty;

            for (int iRow = 0; iRow < dgvScope.Rows.Count; iRow++)
            {
                if((bool)dgvScope[0,iRow].Value)
                {
                    sCode = dgvScope[2, iRow].Value.ToString();
                    sScope = sScope + sCode + "|";
                }
            }

            return sScope;
            
        }

        private string SelectCategory()
        {
            string sCode = string.Empty;
            string sCat = string.Empty;

            for (int iRow = 0; iRow < dgvCategory.Rows.Count; iRow++)
            {
                if ((bool)dgvCategory[0, iRow].Value)
                {
                    sCode = dgvCategory[2, iRow].Value.ToString();
                    sCat = sCat + sCode + "|";
                }
            }

            if (string.IsNullOrEmpty(sCat))
                sCat = "00";
            return sCat;
        }

        private void PopulateCategory()
        {
            CategoryList lstCategory = new CategoryList();

            int iCnt = lstCategory.CategoryLst.Count;

            for (int i = 0; i < iCnt; i++)
            {
                dgvCategory.Rows.Add(false, lstCategory.CategoryLst[i].Desc, lstCategory.CategoryLst[i].Code);
            }
        }

        private string SelectStruc()
        {
            string sCode = string.Empty;
            string sStruc = string.Empty;

            for (int iRow = 0; iRow < dgvStruc.Rows.Count; iRow++)
            {
                if ((bool)dgvStruc[0, iRow].Value)
                {
                    sCode = dgvStruc[2, iRow].Value.ToString();
                    sStruc = sStruc + sCode + "|";
                }
            }

            
            return sStruc;

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEdit.Text == "Edit")
            {
                m_sMainCode = txtRevenueAcct.Text.ToString();
                btnEdit.Text = "Update";
                btnExit.Text = "Cancel";
                btnAdd.Enabled = false;
                btnDelete.Enabled = false;
                btnPrint.Enabled = false;
                AddSubcategory(txtRevenueAcct.Text.ToString());
                LoadGridMajorFees();
                AddMajorFee();
                txtSubAcctDescNew.Focus();
                txtSubAcctDescNew.Visible = true;
                cmbSubAcctDesc.Visible = true;
                EnableControls(true);
                dgvMajorFees.CurrentCell = null;

            }
            else
            {
                if (string.IsNullOrEmpty(txtRevenueAcct.Text.ToString()))
                {
                    MessageBox.Show("Please enter the Revenue Account.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                OnSaveRecord("Update");
                btnEdit.Text = "Edit";
                btnExit.Text = "Exit";
                btnAdd.Enabled = true;
                btnDelete.Enabled = true;
                btnPrint.Enabled = true;
                EnableControls(false);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            frmScheduleDelete form = new frmScheduleDelete();
            form.ShowDialog();

            PopulateRevenueAccount();
            PopulateStructure();
            PopulateScope();
            PopulateType();

            EnableControls(false);
            cmbRevenueAcct.Visible = true;
            cmbSubAcctDesc.Visible = true;
            txtRevenueAcctNew.Visible = false;
            txtSubAcctDescNew.Visible = false;
            InitializeControls();
            PopulateSchedule();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (btnExit.Text == "Cancel")
            {
                PopulateRevenueAccount();
                PopulateStructure();
                PopulateScope();
                PopulateType();

                EnableControls(false);
                btnAdd.Text = "Add";
                btnEdit.Text = "Edit";
                btnExit.Text = "Exit";
                btnAdd.Enabled = true;
                btnEdit.Enabled = true;
                btnExit.Enabled = true;
                btnDelete.Enabled = true;
                btnPrint.Enabled = true;
                cmbRevenueAcct.Visible = true;
                cmbSubAcctDesc.Visible = true;
                txtRevenueAcctNew.Visible = false;
                txtSubAcctDescNew.Visible = false;
            }
            else
                this.Close();
        }

        private void btnPenalty_Click(object sender, EventArgs e)
        {

        }

        private void dgvSchedule_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var db = new EPSConnection(dbConn);
            string strQuery = string.Empty;
            
            strQuery = $"delete from schedules_x where fees_code = '{m_sFeesCode}'";
            db.Database.ExecuteSqlCommand(strQuery);

            if (m_sFeesMeans == "FIXED AMOUNT" || m_sFeesMeans == "FA")
            {
                double dAmount = 0;
                double.TryParse(dgvSchedule[0, e.RowIndex].Value.ToString(), out dAmount);

                strQuery = $"insert into schedules_x (fees_code,amount1) values (:1,:2)";
                db.Database.ExecuteSqlCommand(strQuery,
                    new OracleParameter(":1", m_sFeesCode),
                    new OracleParameter(":2", dAmount));
            }
            if (m_sFeesMeans == "FIXED RATE" || m_sFeesMeans == "FR")
            {
                double dAmount = 0;
                double.TryParse(dgvSchedule[0, e.RowIndex].Value.ToString(), out dAmount);

                strQuery = $"insert into schedules_x (fees_code,rate1) values (:1,:2)";
                db.Database.ExecuteSqlCommand(strQuery,
                    new OracleParameter(":1", m_sFeesCode),
                    new OracleParameter(":2", dAmount));
            }
            if (m_sFeesMeans == "QUANTITY NO" || m_sFeesMeans == "QN")
            {
                double dAmount1 = 0;
                double dAmount2 = 0;

                double.TryParse(dgvSchedule[0, e.RowIndex].Value.ToString(), out dAmount1);
                double.TryParse(dgvSchedule[1, e.RowIndex].Value.ToString(), out dAmount2);

                strQuery = $"insert into schedules_x (fees_code,amount1,amount2) values (:1,:2,:3)";
                db.Database.ExecuteSqlCommand(strQuery,
                    new OracleParameter(":1", m_sFeesCode),
                    new OracleParameter(":2", dAmount1),
                    new OracleParameter(":3", dAmount2));
            }
            if (m_sFeesMeans == "QUANTITY RANGE" || m_sFeesMeans == "QR")
            {
                int iQty1 = 0;
                int iQty2 = 0;
                double dAmount = 0;
                double dRate2 = 0;

                int.TryParse(dgvSchedule[1, e.RowIndex].Value.ToString(), out iQty1);
                int.TryParse(dgvSchedule[2, e.RowIndex].Value.ToString(), out iQty2);
                double.TryParse(dgvSchedule[3, e.RowIndex].Value.ToString(), out dAmount);
                double.TryParse(dgvSchedule[4, e.RowIndex].Value.ToString(), out dRate2);

                if (e.ColumnIndex == 1)
                {
                    if (iQty2 != 0)
                    {
                        if (iQty1 >= iQty2)
                        {
                            dgvSchedule[1, e.RowIndex].Value = "";
                            dgvSchedule[2, e.RowIndex].Value = "";
                        }
                    }
                }

                if (e.ColumnIndex == 2)
                {
                    if (iQty2 != 0)
                    {
                        if (iQty1 >= iQty2)
                        {
                            dgvSchedule[1, e.RowIndex].Value = "";
                            dgvSchedule[2, e.RowIndex].Value = "";
                        }
                    }
                }

                if (e.ColumnIndex == 3)
                {
                    if (dAmount == 0)
                        dgvSchedule[3, e.RowIndex].Value = "";

                    if (iQty1 == 0 || iQty2 == 0 || dAmount == 0)
                        dgvSchedule[3, e.RowIndex].Value = "";
                    else
                    {
                        int iRow = dgvSchedule.Rows.Count-1;
                        if (iRow == e.RowIndex)
                        {
                            iRow++;
                            dgvSchedule.Rows.Add(m_sFeesCode, "", "", "", "");

                        }
                    }
                }

                if (e.ColumnIndex == 1 || e.ColumnIndex == 2 || e.ColumnIndex == 3 || e.ColumnIndex == 4) 
                {
                    for (int ii = 0; ii < dgvSchedule.Rows.Count; ii++)
                    {
                        int.TryParse(dgvSchedule[1, ii].Value.ToString(), out iQty1);
                        int.TryParse(dgvSchedule[2, ii].Value.ToString(), out iQty2);
                        double.TryParse(dgvSchedule[3, ii].Value.ToString(), out dAmount);
                        double.TryParse(dgvSchedule[4, ii].Value.ToString(), out dRate2);

                        if (iQty1 == 0 || iQty2 == 0 || dAmount == 0)
                        { }
                        else
                        {
                            strQuery = $"insert into schedules_x (fees_code,qty1,qty2,amount1,rate2) values (:1,:2,:3,:4,:5)";
                            db.Database.ExecuteSqlCommand(strQuery,
                                new OracleParameter(":1", m_sFeesCode),
                                new OracleParameter(":2", iQty1),
                                new OracleParameter(":3", iQty2),
                                new OracleParameter(":4", dAmount),
                                new OracleParameter(":5", dRate2));
                        }
                    }
                }
            }

            if (m_sFeesMeans == "RATE RANGE" || m_sFeesMeans == "RR")
            {
                double dRange1 = 0;
                double dRange2 = 0;
                double dAmount = 0;
                double dRate = 0;

                double.TryParse(dgvSchedule[1, e.RowIndex].Value.ToString(), out dRange1);
                double.TryParse(dgvSchedule[2, e.RowIndex].Value.ToString(), out dRange2);
                double.TryParse(dgvSchedule[3, e.RowIndex].Value.ToString(), out dAmount);
                double.TryParse(dgvSchedule[4, e.RowIndex].Value.ToString(), out dRate);

                if (e.ColumnIndex == 1)
                {
                    if (dRange2 != 0)
                    {
                        if (dRange1 >= dRange2)
                        {
                            dgvSchedule[1, e.RowIndex].Value = "";
                            dgvSchedule[2, e.RowIndex].Value = "";
                        }
                    }
                }

                if (e.ColumnIndex == 2)
                {
                    if (dRange2 != 0)
                    {
                        if (dRange1 >= dRange2)
                        {
                            dgvSchedule[1, e.RowIndex].Value = "";
                            dgvSchedule[2, e.RowIndex].Value = "";
                        }
                    }
                }

                if (e.ColumnIndex == 3 || e.ColumnIndex == 4)
                {
                    if (dRange1 == 0 || dRange2 == 0 || dAmount == 0)
                    {
                        dgvSchedule[3, e.RowIndex].Value = "";
                        dgvSchedule[4, e.RowIndex].Value = "";
                    }
                    else
                    {
                        int iRow = dgvSchedule.Rows.Count - 1;
                        if (iRow == e.RowIndex)
                        {
                            iRow++;
                            dgvSchedule.Rows.Add(m_sFeesCode, "", "", "", "");
                        }
                    }
                }

                if (e.ColumnIndex == 1 || e.ColumnIndex == 2 || e.ColumnIndex == 3 || e.ColumnIndex == 4)
                {
                    if (dRate == 0)   
                        dgvSchedule[4, e.RowIndex].Value = "";

                    for (int ii = 0; ii <dgvSchedule.Rows.Count; ii++)
                    {
                        double.TryParse(dgvSchedule[1, ii].Value.ToString(), out dRange1);
                        double.TryParse(dgvSchedule[2, ii].Value.ToString(), out dRange2);
                        double.TryParse(dgvSchedule[3, ii].Value.ToString(), out dAmount);
                        double.TryParse(dgvSchedule[4, ii].Value.ToString(), out dRate);

                        if (dRange1 == 0 || dRange2 == 0 || dAmount == 0)
                        { }
                        else
                        {

                            strQuery = $"insert into schedules_x (fees_code,range1,range2,amount1,rate1) values (:1,:2,:3,:4,:5)";
                            db.Database.ExecuteSqlCommand(strQuery,
                                new OracleParameter(":1", m_sFeesCode),
                                new OracleParameter(":2", dRange1),
                                new OracleParameter(":3", dRange2),
                                new OracleParameter(":4", dAmount),
                                new OracleParameter(":5", dRate));
                        }
                    }
                }
            }
            if (m_sFeesMeans == "AREA RANGE" || m_sFeesMeans == "AR")
            {
                double dRange1 = 0;
                double dRange2 = 0;
                double dAmount = 0;
                double dRate2 = 0;

                double.TryParse(dgvSchedule[1, e.RowIndex].Value.ToString(), out dRange1);
                double.TryParse(dgvSchedule[2, e.RowIndex].Value.ToString(), out dRange2);
                double.TryParse(dgvSchedule[3, e.RowIndex].Value.ToString(), out dAmount);
                double.TryParse(dgvSchedule[4, e.RowIndex].Value.ToString(), out dRate2);

                if (e.ColumnIndex == 1)
                {
                    if (dRange2 != 0)
                    {
                        if (dRange1 >= dRange2)
                        {
                            dgvSchedule[1, e.RowIndex].Value = "";
                            dgvSchedule[2, e.RowIndex].Value = "";
                        }
                    }
                }

                if (e.ColumnIndex == 2)
                {
                    if (dRange2 != 0)
                    {
                        if (dRange1 >= dRange2)
                        {
                            dgvSchedule[1, e.RowIndex].Value = "";
                            dgvSchedule[2, e.RowIndex].Value = "";
                        }
                    }
                }

                if (e.ColumnIndex == 3)
                {
                    if (dRange1 == 0 || dRange2 == 0 || dAmount == 0)
                    {
                        dgvSchedule[3, e.RowIndex].Value = "";
                    }
                    else
                    {
                        int iRow = dgvSchedule.Rows.Count;
                        if (iRow == e.RowIndex)
                        {
                            dgvSchedule.Rows.Add(m_sFeesCode, "", "", "", "");
                        }
                    }
                }

                if (e.ColumnIndex == 1 || e.ColumnIndex == 2 || e.ColumnIndex == 3 || e.ColumnIndex == 4) 
                {
                    for (int ii = 0; ii < dgvSchedule.Rows.Count; ii++)
                    {
                        double.TryParse(dgvSchedule[1, ii].Value.ToString(), out dRange1);
                        double.TryParse(dgvSchedule[2, ii].Value.ToString(), out dRange2);
                        double.TryParse(dgvSchedule[3, ii].Value.ToString(), out dAmount);
                        double.TryParse(dgvSchedule[4, ii].Value.ToString(), out dRate2);

                        if (dRange1 == 0 || dRange2 == 0 || dAmount == 0)
                        { }
                        else
                        {
                            strQuery = $"insert into schedules_x (fees_code,range1,range2,amount1,rate2) values (:1,:2,:3,:4,:5)";
                            db.Database.ExecuteSqlCommand(strQuery,
                                new OracleParameter(":1", m_sFeesCode),
                                new OracleParameter(":2", dRange1),
                                new OracleParameter(":3", dRange2),
                                new OracleParameter(":4", dAmount),
                                new OracleParameter(":5", dRate2));
                        }
                    }
                }
            }
        }

        private void dgvMajorFees_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
