using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common.AppSettings;
using EPSEntities.Entity;
using Common.StringUtilities;
using Modules.Utilities;
using Modules.Records;
using EPSEntities.Connection;
using Common.DataConnector;

namespace Modules.SearchAccount
{
    public partial class frmSearchARN : Form
    {
        public static ConnectionString dbConn = new ConnectionString();

        public string SearchCriteria { get; set; }

        public string sArn { get; set; }
        public string sProjectDesc { get; set; }
        public string sPermitDesc { get; set; }
        public string sScopeDesc { get; set; }
        public string sAcctLn1 { get; set; }
        public string sAcctFn1 { get; set; }
        public string sAcctMi1 { get; set; }
        public string sLotOwn { get; set; }
        public string sProjBrgy { get; set; }
        public string sAcctLn2 { get; set; }
        public string sAcctFn2 { get; set; }
        public string sAcctMi2 { get; set; }
        public string sProjOwn { get; set; }
        public string StatusCode { get; set; }
        public string sDistCode { get; set; }

        public string PermitCode { get; set; }

        public string SelectedPermit { get; set; }

        public frmSearchARN()
        {
            InitializeComponent();
        }

        private void bntList_Click2(object sender, EventArgs e)
        {
            string sQuery = string.Empty;

            if (SearchCriteria == "QUE-NEW")
                StatusCode = "NEW";
            else if (SearchCriteria == "QUE-REN")
                StatusCode = "RENEWAL";

            if (SearchCriteria == "AppForExcavation")
            {
                sQuery = $"select excavation_tbl.arn,";
                sQuery += "excavation_tbl.proj_desc,'','',";
                sQuery += "billing.bill_no,'','',";
                sQuery += "excavation_tbl.acct_code,excavation_tbl.brgy,";
                sQuery += "account2.acct_ln, account2.acct_fn, account2.acct_mi, ";
                sQuery += "account2.acct_code ";
                sQuery += "from excavation_tbl,billing,account account2 where ";
                sQuery += "excavation_tbl.arn = billing.arn and ";
                sQuery += "excavation_tbl.acct_code = account2.acct_code and ";
                sQuery += $"excavation_tbl.arn like '{arn1.GetAn()}%' ";
                sQuery += $" and excavation_tbl.proj_desc like '{StringUtilities.HandleApostrophe(txtProjDesc.Text.ToString())}%' ";
                sQuery += $" and excavation_tbl.brgy like '{((DataRowView)cmbBrgy.SelectedItem)["Desc"].ToString()}%' ";
                sQuery += $" and billing.bill_no like '{StringUtilities.HandleApostrophe(txtLotLastName.Text.ToString())}%' ";
                sQuery += $" and account2.acct_ln like '{StringUtilities.HandleApostrophe(txtOwnLastName.Text.ToString())}%' ";
                sQuery += $" and account2.acct_fn like '{StringUtilities.HandleApostrophe(txtOwnFirstName.Text.ToString())}%' ";
                sQuery += $" and account2.acct_mi like '{StringUtilities.HandleApostrophe(txtOwnMI.Text.ToString())}%' ";
                sQuery += "order by excavation_tbl.arn";
            }
            else
            if (SearchCriteria == "EXCAVATION PERMIT")
            {
                sQuery = $"select excavation_tbl.arn,";
                sQuery += "excavation_tbl.proj_desc,'','',";
                sQuery += "excavation_tbl.excavation_no,'','',";
                sQuery += "excavation_tbl.acct_code,excavation_tbl.brgy,";
                sQuery += "account2.acct_ln, account2.acct_fn, account2.acct_mi, ";
                sQuery += "account2.acct_code ";
                sQuery += "from excavation_tbl,account account2 where ";
                sQuery += "excavation_tbl.excavation_no is not null and ";
                sQuery += "excavation_tbl.acct_code = account2.acct_code and ";
                sQuery += $"excavation_tbl.arn like '{arn1.GetAn()}%' ";
                sQuery += $" and excavation_tbl.proj_desc like '{StringUtilities.HandleApostrophe(txtProjDesc.Text.ToString())}%' ";
                sQuery += $" and excavation_tbl.brgy like '{((DataRowView)cmbBrgy.SelectedItem)["Desc"].ToString()}%' ";
                sQuery += $" and excavation_tbl.excavation_no like '{StringUtilities.HandleApostrophe(txtLotLastName.Text.ToString())}%' ";
                sQuery += $" and account2.acct_ln like '{StringUtilities.HandleApostrophe(txtOwnLastName.Text.ToString())}%' ";
                sQuery += $" and account2.acct_fn like '{StringUtilities.HandleApostrophe(txtOwnFirstName.Text.ToString())}%' ";
                sQuery += $" and account2.acct_mi like '{StringUtilities.HandleApostrophe(txtOwnMI.Text.ToString())}%' ";
                sQuery += "order by excavation_tbl.arn";
            }
            else if (SearchCriteria == "ApplyForCEI")
            {
                sQuery = $"select wiring_tbl.arn,wiring_tbl.proj_desc, ";
                sQuery += "wiring_tbl.type_fld,'',category_tbl.category_desc, ";
                sQuery += "wiring_tbl.assigned_no,'',wiring_tbl.acct_code,'', ";
                sQuery += "account2.acct_ln, account2.acct_fn, account2.acct_mi, ";
                sQuery += "account2.acct_code,category_tbl.category_code,wiring_tbl.occupancy ";
                sQuery += "from wiring_tbl,account account2,category_tbl where ";
                sQuery += "wiring_tbl.assigned_no is not null and ";
                sQuery += "wiring_tbl.acct_code = account2.acct_code and ";
                sQuery += "wiring_tbl.occupancy = category_tbl.category_code and ";
                sQuery += $"wiring_tbl.arn like '{arn1.GetAn()}%' ";
                sQuery += $" and wiring_tbl.proj_desc like '{StringUtilities.HandleApostrophe(txtProjDesc.Text.ToString())}%' ";
                sQuery += $" and wiring_tbl.brgy like '{((DataRowView)cmbBrgy.SelectedItem)["Desc"].ToString()}%' ";

                CategoryList lstCategory = new CategoryList(txtLotLastName.Text.ToString());
                sQuery += $" and wiring_tbl.occupancy like '{lstCategory.CategoryLst[0].Code}%' ";
                sQuery += $" and wiring_tbl.assigned_no like '{StringUtilities.HandleApostrophe(txtLotFirstName.Text.ToString())}%' ";
                sQuery += $" and account2.acct_ln like '{StringUtilities.HandleApostrophe(txtOwnLastName.Text.ToString())}%' ";
                sQuery += $" and account2.acct_fn like '{StringUtilities.HandleApostrophe(txtOwnFirstName.Text.ToString())}%' ";
                sQuery += $" and account2.acct_mi like '{StringUtilities.HandleApostrophe(txtOwnMI.Text.ToString())}%' ";
                sQuery += "order by wiring_tbl.proj_desc";
            }
            else if (SearchCriteria == "PrintCEI")
            {
                sQuery = $"select other_cert.arn,other_cert.proj_desc, ";
                sQuery += "other_cert.type_fld,'',other_cert.assigned_no, ";
                sQuery += "other_cert.wiring_no,'',other_cert.acct_code,'', ";
                sQuery += "account2.acct_ln, account2.acct_fn, account2.acct_mi, account2.acct_code ";
                sQuery += "from other_cert,account account2 where ";
                sQuery += "other_cert.assigned_no is not null and ";
                sQuery += "other_cert.acct_code = account2.acct_code and ";
                sQuery += $"other_cert.arn like '{arn1.GetAn()}%' ";
                sQuery += $" and other_cert.proj_desc like '{StringUtilities.HandleApostrophe(txtProjDesc.Text.ToString())}%' ";
                sQuery += $" and other_cert.brgy like '{((DataRowView)cmbBrgy.SelectedItem)["Desc"].ToString()}%' ";
                sQuery += $" and other_cert.assigned_no like '{StringUtilities.HandleApostrophe(txtLotLastName.Text.ToString())}%' ";
                sQuery += $" and other_cert.wiring_no like '{StringUtilities.HandleApostrophe(txtLotFirstName.Text.ToString())}%' ";
                sQuery += $" and account2.acct_ln like '{StringUtilities.HandleApostrophe(txtOwnLastName.Text.ToString())}%' ";
                sQuery += $" and account2.acct_fn like '{StringUtilities.HandleApostrophe(txtOwnFirstName.Text.ToString())}%' ";
                sQuery += $" and account2.acct_mi like '{StringUtilities.HandleApostrophe(txtOwnMI.Text.ToString())}%' ";
                sQuery += "order by other_cert.proj_desc";
            }
            else if (SearchCriteria == "WIRING")
            {
                sQuery = $"select wiring_tbl.arn,wiring_tbl.proj_desc, ";
                sQuery += "wiring_tbl.type_fld,'',category_tbl.category_desc, ";
                sQuery += "wiring_tbl.assigned_no,'',wiring_tbl.acct_code,'', ";
                sQuery += "account2.acct_ln, account2.acct_fn, account2.acct_mi, ";
                sQuery += "account2.acct_code,category_tbl.category_code,wiring_tbl.occupancy ";
                sQuery += "from wiring_tbl,account account2,category_tbl where ";
                sQuery += "wiring_tbl.assigned_no is not null and ";
                sQuery += "wiring_tbl.acct_code = account2.acct_code and ";
                sQuery += "wiring_tbl.occupancy = category_tbl.category_code and ";
                sQuery += $"wiring_tbl.arn like '{arn1.GetAn()}%' ";
                sQuery += $" and wiring_tbl.proj_desc like '{StringUtilities.HandleApostrophe(txtProjDesc.Text.ToString())}%' ";
                sQuery += $" and wiring_tbl.brgy like '{((DataRowView)cmbBrgy.SelectedItem)["Desc"].ToString()}%' ";

                CategoryList lstCategory = new CategoryList(txtLotLastName.Text.ToString());
                sQuery += $" and wiring_tbl.occupancy like '{lstCategory.CategoryLst[0].Code}%' ";
                sQuery += $" and wiring_tbl.assigned_no like '{StringUtilities.HandleApostrophe(txtLotFirstName.Text.ToString())}%' ";
                sQuery += $" and account2.acct_ln like '{StringUtilities.HandleApostrophe(txtOwnLastName.Text.ToString())}%' ";
                sQuery += $" and account2.acct_fn like '{StringUtilities.HandleApostrophe(txtOwnFirstName.Text.ToString())}%' ";
                sQuery += $" and account2.acct_mi like '{StringUtilities.HandleApostrophe(txtOwnMI.Text.ToString())}%' ";
                sQuery += "order by wiring_tbl.proj_desc";
            }
            else if (SearchCriteria == "AppWire")
            {
                sQuery = $"select wiring_tbl.arn,wiring_tbl.proj_desc, ";
                sQuery += "wiring_tbl.type_fld,'',category_tbl.category_desc, ";
                sQuery += "'','',wiring_tbl.acct_code,'', ";
                sQuery += "account2.acct_ln, account2.acct_fn, account2.acct_mi, ";
                sQuery += "account2.acct_code,category_tbl.category_code,wiring_tbl.occupancy ";
                sQuery += "from wiring_tbl , account account2,category_tbl where ";
                sQuery += "wiring_tbl.assigned_no is null and ";
                sQuery += "wiring_tbl.acct_code = account2.acct_code and ";
                sQuery += "wiring_tbl.occupancy = category_tbl.category_code and ";
                sQuery += $"wiring_tbl.arn like '{arn1.GetAn()}%' ";
                sQuery += $" and wiring_tbl.proj_desc like '{StringUtilities.HandleApostrophe(txtProjDesc.Text.ToString())}%' ";
                sQuery += $" and wiring_tbl.brgy like '{((DataRowView)cmbBrgy.SelectedItem)["Desc"].ToString()}%' ";

                CategoryList lstCategory = new CategoryList(txtLotLastName.Text.ToString());
                sQuery += $" and wiring_tbl.occupancy like '{lstCategory.CategoryLst[0].Code}%' ";
                sQuery += $" and wiring_tbl.assigned_no like '{StringUtilities.HandleApostrophe(txtLotFirstName.Text.ToString())}%' ";
                sQuery += $" and account2.acct_ln like '{StringUtilities.HandleApostrophe(txtOwnLastName.Text.ToString())}%' ";
                sQuery += $" and account2.acct_fn like '{StringUtilities.HandleApostrophe(txtOwnFirstName.Text.ToString())}%' ";
                sQuery += $" and account2.acct_mi like '{StringUtilities.HandleApostrophe(txtOwnMI.Text.ToString())}%' ";
                sQuery += "order by wiring_tbl.proj_desc";
            }
            else if (SearchCriteria == "QUE-NEW" || SearchCriteria == "QUE-REN" || SearchCriteria == "QUE")    // RMC 20120214 QA EPS for Aparri
            {
                sQuery = $"select distinct(application_que.arn),";
                sQuery += "application_que.proj_desc, ";
                sQuery += "permit_tbl.permit_desc,scope_tbl.scope_desc, ";
                sQuery += "account1.acct_ln,account1.acct_fn,account1.acct_mi,";
                sQuery += "application_que.proj_lot_owner, ";
                sQuery += "application_que.proj_brgy, ";
                sQuery += "account2.acct_ln,account2.acct_fn,";
                sQuery += "account2.acct_mi,application_que.proj_owner ";
                sQuery += "from application_que ";
                sQuery += "left join permit_tbl on permit_tbl.permit_code = application_que.permit_code ";
                sQuery += "left join scope_tbl on  scope_tbl.scope_code = application_que.scope_code ";
                sQuery += "left join account account2 on account2.acct_code = application_que.proj_owner ";
                sQuery += "left join account account1 on account1.acct_code= application_que.proj_lot_owner ";
                sQuery += "where application_que.main_application = 1 and ";
                sQuery += $"application_que.status_code like '{StatusCode}%' and ";
                sQuery += $"application_que.arn like '{arn1.GetAn()}%' ";
                sQuery += $" and application_que.proj_desc like '{StringUtilities.HandleApostrophe(txtProjDesc.Text.ToString())}%' ";
                if (!string.IsNullOrEmpty(cmbBrgy.Text.ToString()))
                    sQuery += $" and application_que.proj_brgy like '{((DataRowView)cmbBrgy.SelectedItem)["Desc"].ToString()}%' ";
                sQuery += $" and account1.acct_ln like '{StringUtilities.HandleApostrophe(txtLotLastName.Text.ToString())}%' ";
                sQuery += $" and account1.acct_fn like '{StringUtilities.HandleApostrophe(txtLotFirstName.Text.ToString())}%' ";
                sQuery += $" and account1.acct_mi like '{StringUtilities.HandleApostrophe(txtLotMI.Text.ToString())}%' ";
                sQuery += $" and account2.acct_ln like '{StringUtilities.HandleApostrophe(txtOwnLastName.Text.ToString())}%' ";
                sQuery += $" and account2.acct_fn like '{StringUtilities.HandleApostrophe(txtOwnFirstName.Text.ToString())}%' ";
                sQuery += $" and account2.acct_mi like '{StringUtilities.HandleApostrophe(txtOwnMI.Text.ToString())}%' ";
                sQuery += " order by application_que.proj_desc";
            }
            else if (SearchCriteria == "APP")
            {
                sQuery = $"select distinct(application.arn),";
                sQuery += "application.proj_desc, ";
                sQuery += "permit_tbl.permit_desc,scope_tbl.scope_desc, ";
                sQuery += "account1.acct_ln,account1.acct_fn,account1.acct_mi,application.proj_lot_owner, ";
                sQuery += "application.proj_brgy, ";
                sQuery += "account2.acct_ln,account2.acct_fn,account2.acct_mi,application.proj_owner ";
                sQuery += "from application,permit_tbl,scope_tbl,account account1,account account2 where ";
                sQuery += "application.permit_code = permit_tbl.permit_code and ";
                sQuery += "application.scope_code = scope_tbl.scope_code and ";
                sQuery += "application.proj_owner = account2.acct_code and ";
                sQuery += "application.proj_lot_owner = account1.acct_code and ";
                sQuery += "application.main_application = 1 and ";
                sQuery += $"application.arn like '{arn1.GetAn()}%' ";
                sQuery += $" and application.proj_desc like '{StringUtilities.HandleApostrophe(txtProjDesc.Text.ToString())}%' ";
                sQuery += $" and application.brgy like '{((DataRowView)cmbBrgy.SelectedItem)["Desc"].ToString()}%' ";
                sQuery += $" and account1.acct_ln like '{StringUtilities.HandleApostrophe(txtLotLastName.Text.ToString())}%' ";
                sQuery += $" and account1.acct_fn like '{StringUtilities.HandleApostrophe(txtLotFirstName.Text.ToString())}%' ";
                sQuery += $" and account1.acct_mi like '{StringUtilities.HandleApostrophe(txtLotMI.Text.ToString())}%' ";
                sQuery += $" and account2.acct_ln like '{StringUtilities.HandleApostrophe(txtOwnLastName.Text.ToString())}%' ";
                sQuery += $" and account2.acct_fn like '{StringUtilities.HandleApostrophe(txtOwnFirstName.Text.ToString())}%' ";
                sQuery += $" and account2.acct_mi like '{StringUtilities.HandleApostrophe(txtOwnMI.Text.ToString())}%' ";
                sQuery += " order by application.proj_desc";
            }
            else if (SearchCriteria == "CERT")
            {
                sQuery = $"select application.arn,application.proj_desc, ";
                sQuery += "permit_tbl.permit_desc,scope_tbl.scope_desc, ";
                sQuery += "account1.acct_ln,account1.acct_fn,account1.acct_mi, ";
                sQuery += "application.proj_lot_owner,application.proj_brgy, ";
                sQuery += "account2.acct_ln,account2.acct_fn,account2.acct_mi, ";
                sQuery += "application.proj_owner,cert_occupancy.cert_no ";
                sQuery += "from application,permit_tbl,cert_occupancy, ";
                sQuery += "scope_tbl,account account1,account account2 ";
                sQuery += "where ";
                sQuery += "application.arn = cert_occupancy.arn and ";
                sQuery += "application.permit_code = permit_tbl.permit_code and ";
                sQuery += "application.scope_code = scope_tbl.scope_code and ";
                sQuery += "application.proj_owner = account2.acct_code and ";
                sQuery += "application.proj_lot_owner = account1.acct_code and ";
                sQuery += "application.main_application = 1 and ";
                sQuery += $"application.arn like '{arn1.GetAn()}%' ";
                sQuery += $" and application.proj_desc like '{StringUtilities.HandleApostrophe(txtProjDesc.Text.ToString())}%' ";
                sQuery += $" and application.brgy like '{((DataRowView)cmbBrgy.SelectedItem)["Desc"].ToString()}%' ";
                sQuery += $" and account1.acct_ln like '{StringUtilities.HandleApostrophe(txtLotLastName.Text.ToString())}%' ";
                sQuery += $" and account1.acct_fn like '{StringUtilities.HandleApostrophe(txtLotFirstName.Text.ToString())}%' ";
                sQuery += $" and account1.acct_mi like '{StringUtilities.HandleApostrophe(txtLotMI.Text.ToString())}%' ";
                sQuery += $" and account2.acct_ln like '{StringUtilities.HandleApostrophe(txtOwnLastName.Text.ToString())}%' ";
                sQuery += $" and account2.acct_fn like '{StringUtilities.HandleApostrophe(txtOwnFirstName.Text.ToString())}%' ";
                sQuery += $" and account2.acct_mi like '{StringUtilities.HandleApostrophe(txtOwnMI.Text.ToString())}%' ";
                sQuery += " order by application.proj_desc";
            }
            else
            {
                sQuery = $"select distinct(application.arn),";
                sQuery += "application.proj_desc, ";
                sQuery += "permit_tbl.permit_desc,scope_tbl.scope_desc, ";
                sQuery += "account1.acct_ln,account1.acct_fn,account1.acct_mi,application.proj_lot_owner, ";
                sQuery += "application.proj_brgy, ";
                sQuery += "account2.acct_ln,account2.acct_fn,account2.acct_mi,application.proj_owner ";
                sQuery += "from application,permit_tbl,scope_tbl,account account1,account account2 where ";
                sQuery += "application.permit_code = permit_tbl.permit_code and ";
                sQuery += "application.scope_code = scope_tbl.scope_code and ";
                sQuery += "application.proj_owner = account2.acct_code and ";
                sQuery += "application.proj_lot_owner = account1.acct_code and ";
                sQuery += "application.main_application = 1 and ";
                sQuery += $"application.arn like '{arn1.GetAn()}%' ";
                sQuery += $" and application.proj_desc like '{StringUtilities.HandleApostrophe(txtProjDesc.Text.ToString())}%' ";
                sQuery += $" and application.brgy like '{((DataRowView)cmbBrgy.SelectedItem)["Desc"].ToString()}%' ";
                sQuery += $" and account1.acct_ln like '{StringUtilities.HandleApostrophe(txtLotLastName.Text.ToString())}%' ";
                sQuery += $" and account1.acct_fn like '{StringUtilities.HandleApostrophe(txtLotFirstName.Text.ToString())}%' ";
                sQuery += $" and account1.acct_mi like '{StringUtilities.HandleApostrophe(txtLotMI.Text.ToString())}%' ";
                sQuery += $" and account2.acct_ln like '{StringUtilities.HandleApostrophe(txtOwnLastName.Text.ToString())}%' ";
                sQuery += $" and account2.acct_fn like '{StringUtilities.HandleApostrophe(txtOwnFirstName.Text.ToString())}%' ";
                sQuery += $" and account2.acct_mi like '{StringUtilities.HandleApostrophe(txtOwnMI.Text.ToString())}%' ";
                sQuery += " order by application.proj_desc";

            }
            var db = new EPSConnection(dbConn);
            var result = db.Database.SqlQuery<SEARCH_ARN>(sQuery);

            foreach (var item in result)
            {
                sArn = item.sArn;
                sProjectDesc = item.sProjectDesc;
                sPermitDesc = item.sPermitDesc;
                sScopeDesc = item.sScopeDesc;
                sAcctLn1 = item.sAcctLn1;
                sAcctFn1 = item.sAcctFn1;
                sAcctMi1 = item.sAcctMi1;
                sLotOwn = item.sLotOwn;
                sProjBrgy = item.sProjBrgy;
                sAcctLn2 = item.sAcctLn2;
                sAcctFn2 = item.sAcctFn2;
                sAcctMi2 = item.sAcctMi2;
                sProjOwn = item.sProjOwn;

                dgvList.Rows.Add(sArn, sProjectDesc, sPermitDesc, sScopeDesc, sAcctLn1,
                    sAcctFn1, sAcctMi1, sLotOwn, sProjBrgy, sAcctLn2, sAcctFn2, sAcctMi2, sProjOwn);
            }


        }

        private void frmSearchARN_Load(object sender, EventArgs e)
        {
            PopulateBrgy();
            LoadGrid();

            arn1.ArnCode.Enabled = true;
        }

        private void LoadGrid()
        {
            dgvList.Rows.Clear();
            dgvList.Columns.Clear();

            if (SearchCriteria == "AppForExcavation" || SearchCriteria == "EXCAVATION PERMIT")
            {
                grpBox1.Text = "EXCAVATION PERMIT";
                lblLotLN.Text = "EP No:";
                lblLotFN.Visible = false;
                txtLotFirstName.Visible = false;
                lblLotMI.Visible = false;
                txtLotMI.Visible = false;

                if (SearchCriteria == "AppForExcavation")
                {
                    lblLotLN.Text = "Bill No:";

                    dgvList.Columns.Add("ARN", "ARN");
                    dgvList.Columns.Add("Description", "Description");
                    dgvList.Columns.Add("Permit", "Permit");
                    dgvList.Columns.Add("Scope", "Scope");
                    dgvList.Columns.Add("BillNo", "Bill No");
                    dgvList.Columns.Add("WiringNo", "Wiring No");
                    dgvList.Columns.Add("MI", "MI");
                    dgvList.Columns.Add("AcctCode", "Acct Code");
                    dgvList.Columns.Add("Barangay", "Barangay");
                    dgvList.Columns.Add("LastName", "Last Name");
                    dgvList.Columns.Add("FirstName", "First Name");
                    dgvList.Columns.Add("MI", "MI");
                    dgvList.Columns.Add("AcctCode", "Acct Code");

                }
                else
                {
                    dgvList.Columns.Add("ARN", "ARN");
                    dgvList.Columns.Add("Description", "Description");
                    dgvList.Columns.Add("Permit", "Permit");
                    dgvList.Columns.Add("Scope", "Scope");
                    dgvList.Columns.Add("EPNo", "EP No");
                    dgvList.Columns.Add("WiringNo", "Wiring No");
                    dgvList.Columns.Add("MI", "MI");
                    dgvList.Columns.Add("AcctCode", "Acct Code");
                    dgvList.Columns.Add("Barangay", "Barangay");
                    dgvList.Columns.Add("LastName", "Last Name");
                    dgvList.Columns.Add("FirstName", "First Name");
                    dgvList.Columns.Add("MI", "MI");
                    dgvList.Columns.Add("AcctCode", "Acct Code");
                }

                dgvList.RowHeadersVisible = false;
                dgvList.Columns[0].Width = 100;
                dgvList.Columns[1].Width = 100;
                dgvList.Columns[2].Visible = false;
                dgvList.Columns[3].Visible = false;
                dgvList.Columns[4].Width = 100;
                dgvList.Columns[5].Visible = false;
                dgvList.Columns[6].Visible = false;
                dgvList.Columns[7].Visible = false;
                dgvList.Columns[8].Width = 100;
                dgvList.Columns[9].Width = 100;
                dgvList.Columns[10].Width = 100;
                dgvList.Columns[11].Visible = false;
                dgvList.Columns[12].Visible = false;
            }
            else if (SearchCriteria == "ApplyForCEI" || SearchCriteria == "PrintCEI")
            {
                grpBox1.Text = "C.E.I.";
                lblLotLN.Text = "CEI No";
                lblLotFN.Text = "Wiring No";
                lblLotMI.Visible = false;
                txtLotMI.Visible = false;
                lblBrgy.Visible = false;
                cmbBrgy.Visible = false;

                if (SearchCriteria == "ApplyForCEI")
                {
                    lblLotLN.Text = "Category";
                    dgvList.Columns.Add("ARN", "ARN");
                    dgvList.Columns.Add("Description", "Description");
                    dgvList.Columns.Add("Permit", "Permit");
                    dgvList.Columns.Add("Scope", "Scope");
                    dgvList.Columns.Add("Category", "Category");
                    dgvList.Columns.Add("WiringNo", "Wiring No");
                    dgvList.Columns.Add("MI", "MI");
                    dgvList.Columns.Add("AcctCode", "Acct Code");
                    dgvList.Columns.Add("Barangay", "Barangay");
                    dgvList.Columns.Add("LastName", "Last Name");
                    dgvList.Columns.Add("FirstName", "First Name");
                    dgvList.Columns.Add("MI", "MI");
                    dgvList.Columns.Add("AcctCode", "Acct Code");

                }
                else
                {
                    dgvList.Columns.Add("ARN", "ARN");
                    dgvList.Columns.Add("Description", "Description");
                    dgvList.Columns.Add("Permit", "Permit");
                    dgvList.Columns.Add("Scope", "Scope");
                    dgvList.Columns.Add("CEINo", "CEI No");
                    dgvList.Columns.Add("WiringNo", "Wiring No");
                    dgvList.Columns.Add("MI", "MI");
                    dgvList.Columns.Add("AcctCode", "Acct Code");
                    dgvList.Columns.Add("Barangay", "Barangay");
                    dgvList.Columns.Add("LastName", "Last Name");
                    dgvList.Columns.Add("FirstName", "First Name");
                    dgvList.Columns.Add("MI", "MI");
                    dgvList.Columns.Add("AcctCode", "Acct Code");
                }

                dgvList.RowHeadersVisible = false;
                dgvList.Columns[0].Width = 100;
                dgvList.Columns[1].Width = 100;
                dgvList.Columns[2].Width = 100;
                dgvList.Columns[3].Visible = false;
                dgvList.Columns[4].Width = 100;
                dgvList.Columns[5].Width = 100;
                dgvList.Columns[6].Visible = false;
                dgvList.Columns[7].Visible = false;
                dgvList.Columns[8].Visible = false;
                dgvList.Columns[9].Visible = false;
                dgvList.Columns[10].Visible = false;
                dgvList.Columns[11].Visible = false;
                dgvList.Columns[12].Visible = false;


            }
            else if (SearchCriteria == "WIRING")
            {
                grpBox1.Text = "Wiring Permit";
                lblLotLN.Text = "Category";
                lblLotFN.Text = "Wiring No";
                lblLotMI.Visible = false;
                txtLotMI.Visible = false;
                lblBrgy.Visible = false;
                cmbBrgy.Visible = false;

                dgvList.Columns.Add("ARN", "ARN");
                dgvList.Columns.Add("Description", "Description");
                dgvList.Columns.Add("Permit", "Permit");
                dgvList.Columns.Add("Scope", "Scope");
                dgvList.Columns.Add("Category", "Category");
                dgvList.Columns.Add("WiringNo", "Wiring No");
                dgvList.Columns.Add("MI", "MI");
                dgvList.Columns.Add("AcctCode", "Acct Code");
                dgvList.Columns.Add("Barangay", "Barangay");
                dgvList.Columns.Add("LastName", "Last Name");
                dgvList.Columns.Add("FirstName", "First Name");
                dgvList.Columns.Add("MI", "MI");
                dgvList.Columns.Add("AcctCode", "Acct Code");

                dgvList.RowHeadersVisible = false;
                dgvList.Columns[0].Width = 100;
                dgvList.Columns[1].Width = 100;
                dgvList.Columns[2].Width = 100;
                dgvList.Columns[3].Visible = false;
                dgvList.Columns[4].Width = 100;
                dgvList.Columns[5].Width = 100;
                dgvList.Columns[6].Visible = false;
                dgvList.Columns[7].Visible = false;
                dgvList.Columns[8].Visible = false;
                dgvList.Columns[9].Visible = false;
                dgvList.Columns[10].Visible = false;
                dgvList.Columns[11].Visible = false;
                dgvList.Columns[12].Visible = false;
            }
            else
            {
                dgvList.Columns.Add("ARN", "ARN");
                dgvList.Columns.Add("Description", "Description");
                dgvList.Columns.Add("Permit", "Permit");
                dgvList.Columns.Add("Scope", "Scope");
                dgvList.Columns.Add("LastName", "Last Name");
                dgvList.Columns.Add("FirstName", "First Name");
                dgvList.Columns.Add("MI", "MI");
                dgvList.Columns.Add("AcctCode", "Acct Code");
                dgvList.Columns.Add("Barangay", "Barangay");
                dgvList.Columns.Add("LastName", "Last Name");
                dgvList.Columns.Add("FirstName", "First Name");
                dgvList.Columns.Add("MI", "MI");
                dgvList.Columns.Add("AcctCode", "Acct Code");

                dgvList.RowHeadersVisible = false;
                dgvList.Columns[0].Width = 100;
                dgvList.Columns[1].Width = 100;
                dgvList.Columns[2].Width = 100;
                dgvList.Columns[3].Width = 100;
                dgvList.Columns[4].Width = 100;
                dgvList.Columns[5].Width = 100;
                dgvList.Columns[6].Width = 100;
                dgvList.Columns[7].Visible = false;
                dgvList.Columns[8].Width = 100;
                dgvList.Columns[9].Visible = false;
                dgvList.Columns[10].Visible = false;
                dgvList.Columns[11].Visible = false;
                dgvList.Columns[12].Visible = false;
            }
        }

        private void PopulateBrgy()
        {
            cmbBrgy.Items.Clear();

            DataTable dataTable = new DataTable("Barangay");
            dataTable.Columns.Clear();
            dataTable.Columns.Add("Code", typeof(String));
            dataTable.Columns.Add("Desc", typeof(String));

            BarangayList lstBrgy = new BarangayList();
            lstBrgy.GetBarangayList(sDistCode, true);
            int intCount = lstBrgy.BarangayNames.Count;
            for (int i = 0; i < intCount; i++)
            {
                dataTable.Rows.Add(new String[] { lstBrgy.BarangayCodes[i], lstBrgy.BarangayNames[i] });
            }

            cmbBrgy.DataSource = dataTable;
            cmbBrgy.DisplayMember = "Desc";
            cmbBrgy.ValueMember = "Desc";
            cmbBrgy.SelectedIndex = -1;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (dgvList.Rows.Count < 1)
            {
                MessageBox.Show("Select from the list", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else
                this.Close();

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
            LoadGrid();
        }

        private void ClearControls()
        {
            txtProjDesc.Text = string.Empty;
            txtLotMI.Text = string.Empty;
            txtLotFirstName.Text = string.Empty;
            txtLotLastName.Text = string.Empty;
            txtOwnMI.Text = string.Empty;
            txtOwnLastName.Text = string.Empty;
            txtOwnFirstName.Text = string.Empty;
            arn1.Clear();
            cmbBrgy.Text = "";

            sArn = string.Empty;
            sProjectDesc = string.Empty;
            sPermitDesc = string.Empty;
            sScopeDesc = string.Empty;
            sAcctLn1 = string.Empty;
            sAcctFn1 = string.Empty;
            sAcctMi1 = string.Empty;
            sLotOwn = string.Empty;
            sProjBrgy = string.Empty;
            sAcctLn2 = string.Empty;
            sAcctFn2 = string.Empty;
            sAcctMi2 = string.Empty;
            sProjOwn = string.Empty;
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ClearControls();
            sArn = string.Empty;

            try
            {
                sArn = dgvList[0, e.RowIndex].Value.ToString();
                arn1.SetAn(sArn);
            }
            catch
            { }

            try { txtProjDesc.Text = dgvList[1, e.RowIndex].Value.ToString();
                sProjectDesc = txtProjDesc.Text.ToString();
            }
            catch { }
            try { txtLotLastName.Text = dgvList[4, e.RowIndex].Value.ToString();
                sAcctLn1 = txtLotLastName.Text.ToString();
            }
            catch { }
            try { txtLotFirstName.Text = dgvList[5, e.RowIndex].Value.ToString();
                sAcctFn1 = txtLotFirstName.Text.ToString();
            }
            catch { }
            try { txtLotMI.Text = dgvList[6, e.RowIndex].Value.ToString();
                sAcctMi1 = txtLotMI.Text.ToString();
            }
            catch { }
            try { txtOwnLastName.Text = dgvList[9, e.RowIndex].Value.ToString();
                sAcctLn2 = txtOwnLastName.Text.ToString();
            }
            catch { }
            try { txtOwnFirstName.Text = dgvList[10, e.RowIndex].Value.ToString();
                sAcctFn2 = txtOwnFirstName.Text.ToString();
            }
            catch { }
            try { txtOwnMI.Text = dgvList[11, e.RowIndex].Value.ToString();
                sAcctMi2 = txtOwnMI.Text.ToString();
            }
            catch { }
            try { cmbBrgy.Text = dgvList[8, e.RowIndex].Value.ToString();
                sProjBrgy = cmbBrgy.Text.ToString();
            }
            catch { }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
            this.Close();
        }

        private void bntList_Click(object sender, EventArgs e)
        {
            string strWhereCond = string.Empty;
            string sPermitAcro = string.Empty;

            dgvList.Rows.Clear();

            if (SearchCriteria == "QUE-NEW")
                StatusCode = "NEW";
            else if (SearchCriteria == "QUE-REN")
                StatusCode = "REN";

            sPermitAcro = arn1.ANCodeGenerator(PermitCode);

            if (SearchCriteria == "QUE-NEW" || SearchCriteria == "QUE-REN" || SearchCriteria == "QUE" || SearchCriteria.Contains("QUE-"))
            {
                //strWhereCond = $" where application_que.main_application = 1 and ";
                if(SearchCriteria == "QUE-OCC")
                    strWhereCond = $" where application_que.main_application = 0 and arn like 'AN%' and application_que.arn not in(select arn from application_que where arn = application_que.arn and permit_code = '01') and "; //AFM 20201204 REQUESTED BY BINAN AS PER MITCH - FIXED "AN" FOR NEW APPLICATION
                else if (SearchCriteria == "QUE-ELEC")
                    strWhereCond = $" where application_que.main_application = 0 and arn like 'AN%' and application_que.arn not in(select arn from application_que where arn = application_que.arn and permit_code = '01') and "; //AFM 20201204 REQUESTED BY BINAN AS PER MITCH - FIXED "AN" FOR NEW APPLICATION 
                else if (SearchCriteria == "QUE-MECH")
                    strWhereCond = $" where application_que.main_application = 1 and arn like 'AN%' and application_que.arn not in(select arn from application_que where arn = application_que.arn and permit_code = '01') and "; //AFM 20201204 REQUESTED BY BINAN AS PER MITCH - FIXED "AN" FOR NEW APPLICATION //mechanical main application set to 1 because it has unique arn and not dependent on building application
                else if (SearchCriteria == "QUE-CFEI")
                    strWhereCond = $" where application_que.main_application = 1 and arn like 'AN%' and application_que.arn not in(select arn from application_que where arn = application_que.arn and permit_code = '01') and "; //AFM 20201204 REQUESTED BY BINAN AS PER MITCH - FIXED "AN" FOR NEW APPLICATION //CFEI main application set to 1 because it has unique arn and not dependent on building application
                else if (SearchCriteria == "QUE-OTH")
                    strWhereCond = $" where application_que.main_application = 0 and arn like 'AN%' and permit_code = '{PermitCode}' and application_que.arn not in(select arn from application_que where arn = application_que.arn and permit_code = '01') and ";
                else if (SearchCriteria == "QUE-EDIT") //AFM 20211103
                    strWhereCond = $" where application_que.main_application = 1 and arn like 'AN%' and permit_code = '{SelectedPermit}' and ";
                else
                    strWhereCond = $" where application_que.main_application = 1 and arn like 'AN%' and "; //AFM 20201204 REQUESTED BY BINAN AS PER MITCH - FIXED "AN" FOR NEW APPLICATION
                //strWhereCond = $" where ";
                if (SearchCriteria != "QUE" && !SearchCriteria.Contains("QUE-"))
                    strWhereCond += $"application_que.status_code = '{StatusCode}' and ";

                //strWhereCond += $" application_que.permit_code like '{PermitCode}%' and application_que.arn like '{sPermitAcro}%' and "; //AFM 20201104 REQUESTED BY BINAN AND RJ 20201103 - NEW BILLING DESIGN AND FLOW
                strWhereCond += $" application_que.permit_code like '{PermitCode}%' and "; //AFM 20201204 REQUESTED BY BINAN AS PER MITCH - FIXED "AN" FOR NEW APPLICATION
                strWhereCond += $"application_que.arn like '{arn1.GetAn()}%' ";
                strWhereCond += $" and application_que.proj_desc like '{StringUtilities.HandleApostrophe(txtProjDesc.Text.ToString())}%' ";
                if (!string.IsNullOrEmpty(cmbBrgy.Text.ToString()))
                    strWhereCond += $" and application_que.proj_brgy like '{((DataRowView)cmbBrgy.SelectedItem)["Desc"].ToString()}%' ";
                strWhereCond += $" and application_que.proj_lot_owner in (select acct_code from account where acct_ln like '{StringUtilities.HandleApostrophe(txtLotLastName.Text.ToString())}%' ";
                strWhereCond += $" and acct_fn like '{StringUtilities.HandleApostrophe(txtLotFirstName.Text.ToString())}%' ";
                strWhereCond += $" and acct_mi like '{StringUtilities.HandleApostrophe(txtLotMI.Text.ToString())}%') ";
                strWhereCond += $" and application_que.proj_owner in (select acct_code from account where acct_ln like '{StringUtilities.HandleApostrophe(txtOwnLastName.Text.ToString())}%' ";
                strWhereCond += $" and acct_fn like '{StringUtilities.HandleApostrophe(txtOwnFirstName.Text.ToString())}%' ";
                strWhereCond += $" and acct_mi like '{StringUtilities.HandleApostrophe(txtOwnMI.Text.ToString())}%' ";
                //requested on site by RJ (s)
                strWhereCond += $" and acct_hse_no like '{StringUtilities.HandleApostrophe(txtProjHseNo.Text.ToString())}%' ";
                strWhereCond += $" and acct_lot_no like '{StringUtilities.HandleApostrophe(txtProjLotNo.Text.ToString())}%' ";
                strWhereCond += $" and acct_blk_no like '{StringUtilities.HandleApostrophe(txtProjBlkNo.Text.ToString())}%' ";
                strWhereCond += $" and acct_addr like '{StringUtilities.HandleApostrophe(txtProjStreet.Text.ToString())}%' ";
                strWhereCond += $" and acct_vill like '{StringUtilities.HandleApostrophe(txtProjVill.Text.ToString())}%') ";
                //requested on site by RJ (e)

                var result = from a in Records.ApplicationQueList.GetApplicationQue(strWhereCond)
                             select a;
                foreach (var item in result)
                {
                    PermitList permit = new PermitList(null);
                    ScopeList scope = new ScopeList();

                    sArn = item.ARN;
                    sProjectDesc = item.PROJ_DESC;
                    sPermitDesc = permit.GetPermitDesc(item.PERMIT_CODE);
                    sScopeDesc = scope.GetScopeDesc(item.SCOPE_CODE);
                    sLotOwn = item.PROJ_LOT_OWNER;

                    Accounts account = new Accounts();
                    account.GetOwner(sLotOwn);
                    sAcctLn1 = account.LastName;
                    sAcctFn1 = account.FirstName;
                    sAcctMi1 = account.MiddleInitial;

                    sProjBrgy = item.PROJ_BRGY;
                    sProjOwn = item.PROJ_OWNER;
                    account = new Accounts();
                    account.GetOwner(sProjOwn);

                    sAcctLn2 = account.LastName;
                    sAcctFn2 = account.FirstName;
                    sAcctMi2 = account.MiddleInitial;

                    dgvList.Rows.Add(sArn, sProjectDesc, sPermitDesc, sScopeDesc, sAcctLn1,
                        sAcctFn1, sAcctMi1, sLotOwn, sProjBrgy, sAcctLn2, sAcctFn2, sAcctMi2, sProjOwn);
                }
            }
            else if (SearchCriteria == "APP" || SearchCriteria.Contains("APP-NEW") || SearchCriteria.Contains("CERTIFICATE")) //AFM 20210315 added APP-NEW for electrical and other permits that is dependent on building permit application
            {
                //strWhereCond = $" where application.main_application = 1 and ";
                if(SearchCriteria == "APP-NEW")
                    strWhereCond = $" where application.main_application = 1 and arn like 'AN%' and (application.arn not in (select arn from application where arn = application.arn and permit_code = '02') and application.arn not in (select arn from application_que where arn = application.arn and permit_code = '02')) and permit_code = '01' and "; //change permit code depending on electrical permit on lgu //permit code = 01 is building permit default
                else if(SearchCriteria == "APP-NEW-OCC")
                    strWhereCond = $" where application.main_application = 1 and arn like 'AN%' and permit_code = '01' and (application.arn not in (select arn from application where arn = application.arn and permit_code = '10') and application.arn not in (select arn from application_que where arn = application.arn and permit_code = '10')) and permit_code = '01' and "; //change permit code depending on occupancy permit on lgu // for occupancy, will get building permit application only //permit code = 01 is building permit default
                else if(SearchCriteria == "APP-NEW-OTH")
                    strWhereCond = $" where application.main_application = 1 and arn like 'AN%' and permit_code = '01' and (application.arn not in (select arn from application where arn = application.arn and permit_code = '{SelectedPermit}') and application.arn not in (select arn from application_que where arn = application.arn and permit_code = '{SelectedPermit}')) and permit_code = '01' and "; //change permit code depending on occupancy permit on lgu // for occupancy, will get building permit application only //permit code = 01 is building permit default
                else if (SearchCriteria == "CERTIFICATE") //AFM 20210408 for certificate of occupancy, filters occupancy applications that are paid
                {
                    strWhereCond = $" where application.main_application = 0 and arn like 'AN%' and (application.arn in (select arn from application where arn = application.arn and permit_code = '{PermitCode}') and application.arn not in (select arn from application_que where arn = application.arn and permit_code = '{PermitCode}')) and ";
                    strWhereCond += $" application.arn in (select arn from payments where application.arn = payments.arn and permit_code = '{PermitCode}') and ";
                }
                else
                    strWhereCond = $" where application.main_application = 1 and arn not like 'AN%' and "; //AFM 20201204 REQUESTED BY BINAN AS PER MITCH - FIXED "AN" FOR NEW APPLICATION
                //strWhereCond = $" where ";
                if (SearchCriteria == "CERTIFICATE")
                    strWhereCond += $" permit_code like '{PermitCode}%' ";
                else
                    strWhereCond += $" permit_code like '{PermitCode}%' and application.arn like '{sPermitAcro}%' "; //AFM 20201104 REQUESTED BY BINAN AND RJ 20201103 - NEW BILLING DESIGN AND FLOW
                strWhereCond += $" and application.arn like '{arn1.GetAn()}%' ";
                strWhereCond += $" and application.proj_desc like '{StringUtilities.HandleApostrophe(txtProjDesc.Text.ToString())}%' ";
                if (!string.IsNullOrEmpty(cmbBrgy.Text.ToString()))
                    strWhereCond += $" and application.proj_brgy like '{((DataRowView)cmbBrgy.SelectedItem)["Desc"].ToString()}%' ";
                strWhereCond += $" and application.proj_lot_owner in (select acct_code from account where acct_ln like '{StringUtilities.HandleApostrophe(txtLotLastName.Text.ToString())}%' ";
                strWhereCond += $" and acct_fn like '{StringUtilities.HandleApostrophe(txtLotFirstName.Text.ToString())}%' ";
                strWhereCond += $" and acct_mi like '{StringUtilities.HandleApostrophe(txtLotMI.Text.ToString())}%') ";

                strWhereCond += $" and application.proj_owner in (select acct_code from account where acct_ln like '{StringUtilities.HandleApostrophe(txtOwnLastName.Text.ToString())}%' ";
                strWhereCond += $" and acct_fn like '{StringUtilities.HandleApostrophe(txtOwnFirstName.Text.ToString())}%' ";
                strWhereCond += $" and acct_mi like '{StringUtilities.HandleApostrophe(txtOwnMI.Text.ToString())}%' ";
                //requested on site by RJ (s)
                strWhereCond += $" and acct_hse_no like '{StringUtilities.HandleApostrophe(txtProjHseNo.Text.ToString())}%' ";
                strWhereCond += $" and acct_lot_no like '{StringUtilities.HandleApostrophe(txtProjLotNo.Text.ToString())}%' ";
                strWhereCond += $" and acct_blk_no like '{StringUtilities.HandleApostrophe(txtProjBlkNo.Text.ToString())}%' ";
                strWhereCond += $" and acct_addr like '{StringUtilities.HandleApostrophe(txtProjStreet.Text.ToString())}%' ";
                strWhereCond += $" and acct_vill like '{StringUtilities.HandleApostrophe(txtProjVill.Text.ToString())}%') ";
                //requested on site by RJ (e)

                var result = from a in Records.ApplicationTblList.GetRecord(strWhereCond)
                             select a;
                foreach (var item in result)
                {
                    PermitList permit = new PermitList(null);
                    ScopeList scope = new ScopeList();

                    sArn = item.ARN;
                    sProjectDesc = item.PROJ_DESC;
                    sPermitDesc = permit.GetPermitDesc(item.PERMIT_CODE);
                    sScopeDesc = scope.GetScopeDesc(item.SCOPE_CODE);
                    sLotOwn = item.PROJ_LOT_OWNER;

                    Accounts account = new Accounts();
                    account.GetOwner(sLotOwn);
                    sAcctLn1 = account.LastName;
                    sAcctFn1 = account.FirstName;
                    sAcctMi1 = account.MiddleInitial;

                    sProjBrgy = item.PROJ_BRGY;
                    sProjOwn = item.PROJ_OWNER;
                    account = new Accounts();
                    account.GetOwner(sProjOwn);

                    sAcctLn2 = account.LastName;
                    sAcctFn2 = account.FirstName;
                    sAcctMi2 = account.MiddleInitial;

                    dgvList.Rows.Add(sArn, sProjectDesc, sPermitDesc, sScopeDesc, sAcctLn1,
                        sAcctFn1, sAcctMi1, sLotOwn, sProjBrgy, sAcctLn2, sAcctFn2, sAcctMi2, sProjOwn);
                }
            }

            if (dgvList.Rows.Count == 0)
            {
                MessageBox.Show("No record found", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }

}
