using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EPSEntities.Connection;
using Common.StringUtilities;
using Modules.Records;
using Common.DataConnector;

namespace Modules.Utilities
{
    public class FormEngr:FormOwnerClass
    {
        public static ConnectionString dbConn = new ConnectionString();

        public FormEngr(frmOwner Form) : base(Form)
        { }

        public override void FormLoad()
        {
            RecordFrm.Text = "Engineer/Architect's Profile";

            UpdateList();

            RecordFrm.cmbEngrType.Visible = true;
            RecordFrm.lblTelNo.Text = "Engr. Type:";
            RecordFrm.txtTelNo.Visible = false;
            RecordFrm.lblTCT.Text = "PTR No. :";
            RecordFrm.lblCTC.Text = "PRC No. :";
            //RecordFrm.lblValidity.Visible = true; //AFM 20200630 disabled for binan ver
            //RecordFrm.dtpValidDt.Visible = true; //AFM 20200630 disabled for binan ver

            //AFM 20191025 (s)
            OracleResultSet result = new OracleResultSet();
            result.Query = $"select * from engineer_type order by engr_type_code";
            if (result.Execute())
                while (result.Read())
                {
                    RecordFrm.cmbEngrType.Items.Add(result.GetString("engr_desc"));
                }
            //AFM 20191025 (e)

            //RecordFrm.cmbEngrType.Items.Add("ARCHITECT");
            //RecordFrm.cmbEngrType.Items.Add("CIVIL");
            //RecordFrm.cmbEngrType.Items.Add("ELECTRICAL");
            //RecordFrm.cmbEngrType.Items.Add("MECHANICAL");
            //RecordFrm.cmbEngrType.Items.Add("SANITARY");

            RecordFrm.txtLastName.Focus();
        }

        private void UpdateList()
        {
            RecordFrm.dgvList.Rows.Clear();
            RecordFrm.dgvList.Columns.Clear();
            RecordFrm.dgvList.Columns.Add("AcctNo", "Acct No");
            RecordFrm.dgvList.Columns.Add("LastName", "Last Name");
            RecordFrm.dgvList.Columns.Add("FirstName", "First Name");
            RecordFrm.dgvList.Columns.Add("MI", "MI");
            RecordFrm.dgvList.Columns.Add("Street", "Street");
            RecordFrm.dgvList.Columns.Add("HseNo", "House No");
            RecordFrm.dgvList.Columns.Add("LotNo", "Lot No");
            RecordFrm.dgvList.Columns.Add("BlkNo", "Blk No");
            RecordFrm.dgvList.Columns.Add("Barangay", "Barangay");
            RecordFrm.dgvList.Columns.Add("City", "City");
            RecordFrm.dgvList.Columns.Add("Province", "Province");
            RecordFrm.dgvList.Columns.Add("ZIP", "ZIP");
            RecordFrm.dgvList.Columns.Add("EngrType", "Engr Type");
            RecordFrm.dgvList.Columns.Add("TIN", "TIN");
            RecordFrm.dgvList.Columns.Add("PRC", "PRC");
            RecordFrm.dgvList.Columns.Add("PTR", "PTR");
            RecordFrm.dgvList.Columns.Add("ValidityDt", "Validity Date");
            RecordFrm.dgvList.Columns.Add("Vill", "Village");

            EngineersList engr = new EngineersList("", "", "", "","");
            int iCnt = 0;
            for (int i = 0; i < engr.AcctLst.Count; i++)
            {
                iCnt++;
                RecordFrm.dgvList.Rows.Add(engr.AcctLst[i].OwnerCode, engr.AcctLst[i].LastName,
                    engr.AcctLst[i].FirstName, engr.AcctLst[i].MiddleInitial, engr.AcctLst[i].Address,
                    engr.AcctLst[i].HouseNo, engr.AcctLst[i].LotNo, engr.AcctLst[i].BlkNo,
                    engr.AcctLst[i].Barangay, engr.AcctLst[i].City, engr.AcctLst[i].Province,
                    engr.AcctLst[i].ZIP, engr.AcctLst[i].EngrType, engr.AcctLst[i].TIN, engr.AcctLst[i].PRC, engr.AcctLst[i].PTR,"", engr.AcctLst[i].Village); //added requested village

                //AFM 20191113 ANG-19-11104
                OracleResultSet result = new OracleResultSet();
                result.Query = $"select * from engineer_tbl where 1=1 and engr_code = '{engr.AcctLst[i].OwnerCode}'";
                if (result.Execute())
                    if (result.Read())
                    {
                        try
                        {
                            RecordFrm.dgvList.Rows[i].Cells["ValidityDt"].Value = String.Format("{0:MM/dd/yyyy}", result.GetDateTime("VALIDITY_DT"));
                        }
                        catch { }
                    }
            }

            RecordFrm.lblNoRecords.Text += " " + iCnt.ToString("#,###");
        }

        public override bool SearchAccount()
        {
            string sAcctNo = string.Empty;

            Utilities.Engineers acct = new Utilities.Engineers();
            acct.GetOwner(RecordFrm.txtLastName.Text.Trim(), RecordFrm.txtFirstName.Text.Trim(), RecordFrm.cmbEngrType.Text);
            sAcctNo = acct.OwnerCode;

            if (!string.IsNullOrEmpty(sAcctNo))
            {
                if (string.IsNullOrEmpty(RecordFrm.AcctNo))
                {
                    if (MessageBox.Show("Record found having the same name of " + RecordFrm.txtLastName.Text.Trim() + ", " + RecordFrm.txtFirstName.Text.Trim() + ".\nContinue?", RecordFrm.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        RecordFrm.ClearControls();
                        return false;
                    }
                }
                else
                {
                    if (RecordFrm.AcctNo != sAcctNo)
                    {
                        if (MessageBox.Show("Record found having the same name of " + RecordFrm.txtLastName.Text.Trim() + ", " + RecordFrm.txtFirstName.Text.Trim() + ".\nContinue?", RecordFrm.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            RecordFrm.ClearControls();
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public override bool ValidateData()
        {
            if (string.IsNullOrEmpty(RecordFrm.txtLastName.Text.ToString().Trim()))
            {
                MessageBox.Show("Name is required", RecordFrm.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            //AFM 20200824 disabled for binan version
            //if (string.IsNullOrEmpty(RecordFrm.txtTCT.Text.ToString().Trim()))
            //{
            //    MessageBox.Show("PTR no. is required", RecordFrm.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return false;
            //}

            //if (string.IsNullOrEmpty(RecordFrm.txtCTC.Text.ToString().Trim()))
            //{
            //    MessageBox.Show("PCR no. is required", RecordFrm.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return false;
            //}

            Engineers account = new Engineers();
            account.GetOwner(RecordFrm.txtLastName.Text.ToString().Trim(),RecordFrm.txtFirstName.Text.ToString().Trim(), RecordFrm.cmbEngrType.Text);
            //RecordFrm.AcctNo = account.OwnerCode;

            if (string.IsNullOrEmpty(RecordFrm.AcctNo))
            {
                if (!string.IsNullOrEmpty(account.OwnerCode))
                {
                    MessageBox.Show("Engineer already exists", RecordFrm.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }
            return true;
        }

        public override void Save()
        {
            if (string.IsNullOrEmpty(RecordFrm.AcctNo))
            {
                Engineers account = new Engineers();
                account.CreateAccount(RecordFrm.txtLastName.Text.ToString(), RecordFrm.txtFirstName.Text.ToString(),
                    RecordFrm.txtMI.Text.ToString(), RecordFrm.txtStreet.Text.ToString(), RecordFrm.txtHseNo.Text.ToString(),
                    RecordFrm.txtLotNo.Text.ToString(), RecordFrm.txtBlkNo.Text.ToString(), RecordFrm.cmbBrgy.Text.ToString(),
                    RecordFrm.txtMun.Text.ToString(), RecordFrm.txtProv.Text.ToString(), RecordFrm.txtZIP.Text.ToString(),
                    RecordFrm.cmbEngrType.Text.ToString(), RecordFrm.txtTIN.Text.ToString(), RecordFrm.txtCTC.Text.ToString(), RecordFrm.txtTCT.Text.ToString(), RecordFrm.dtpValidDt.Value, RecordFrm.txtVillage.Text.ToString().Trim()); //added requested subdivision

                RecordFrm.AcctNo = account.OwnerCode;

                if (string.IsNullOrEmpty(RecordFrm.AcctNo))
                {
                    MessageBox.Show("Cannot create account record", RecordFrm.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                MessageBox.Show("Record saved", RecordFrm.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                string sObject = string.Empty;
                sObject = "Engr Code: " + RecordFrm.AcctNo + "/" + RecordFrm.txtLastName.Text.ToString() + ", " + RecordFrm.txtFirstName.Text.ToString() + " " + RecordFrm.txtMI.Text.ToString();

                if (Utilities.AuditTrail.InsertTrail("SE-A", "engineer_tbl", sObject) == 0)
                {
                    MessageBox.Show("Failed to insert audit trail.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                UpdateList();
            }
            else
            {
                var db = new EPSConnection(dbConn);
                string strQuery = string.Empty;

                strQuery = $"update engineer_tbl set ENGR_LN = '{StringUtilities.HandleApostrophe(RecordFrm.txtLastName.Text.ToString()).Trim()}', ";
                strQuery += $" ENGR_FN = '{StringUtilities.HandleApostrophe(RecordFrm.txtFirstName.Text.ToString()).Trim()}', ";
                strQuery += $" ENGR_MI = '{StringUtilities.HandleApostrophe(RecordFrm.txtMI.Text.ToString()).Trim()}', ";
                strQuery += $" ENGR_HSE_NO = '{StringUtilities.HandleApostrophe(RecordFrm.txtHseNo.Text.ToString()).Trim()}', ";
                strQuery += $" ENGR_LOT_NO = '{StringUtilities.HandleApostrophe(RecordFrm.txtLotNo.Text.ToString()).Trim()}', ";
                strQuery += $" ENGR_BLK_NO= '{StringUtilities.HandleApostrophe(RecordFrm.txtBlkNo.Text.ToString()).Trim()}', ";
                strQuery += $" ENGR_ADDR = '{StringUtilities.HandleApostrophe(RecordFrm.txtStreet.Text.ToString()).Trim()}', ";
                strQuery += $" ENGR_BRGY = '{StringUtilities.HandleApostrophe(RecordFrm.cmbBrgy.Text.ToString()).Trim()}', ";
                strQuery += $" ENGR_CITY = '{StringUtilities.HandleApostrophe(RecordFrm.txtMun.Text.ToString()).Trim()}', ";
                strQuery += $" ENGR_PROV = '{StringUtilities.HandleApostrophe(RecordFrm.txtProv.Text.ToString()).Trim()}', ";
                strQuery += $" ENGR_ZIP = '{StringUtilities.HandleApostrophe(RecordFrm.txtZIP.Text.ToString()).Trim()}', ";
                strQuery += $" ENGR_PRC= '{StringUtilities.HandleApostrophe(RecordFrm.txtCTC.Text.ToString()).Trim()}', ";
                strQuery += $" ENGR_PTR = '{StringUtilities.HandleApostrophe(RecordFrm.txtTCT.Text.ToString()).Trim()}', ";
                strQuery += $" ENGR_TYPE = '{StringUtilities.HandleApostrophe(RecordFrm.cmbEngrType.Text.ToString()).Trim()}',";
                //strQuery += $" VALIDITY_DT = to_date('{RecordFrm.dtpValidDt.Value.ToShortDateString()}', 'MM/dd/yyyy')"; //AFM 20191113
                strQuery += $" VALIDITY_DT = null,"; //AFM 20200630 changed to null for binan ver
                strQuery += $" ENGR_VILL = '{StringUtilities.HandleApostrophe(RecordFrm.txtVillage.Text.ToString()).Trim()}'"; //ADDED REQUESTED SUBDIVISION
                strQuery += $" where ENGR_CODE = '{RecordFrm.AcctNo}' ";
                db.Database.ExecuteSqlCommand(strQuery);

                MessageBox.Show("Record updated", RecordFrm.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                string sObject = string.Empty;
                sObject = "Engr Code: " + RecordFrm.AcctNo + "/" + RecordFrm.txtLastName.Text.ToString() + ", " + RecordFrm.txtFirstName.Text.ToString() + " " + RecordFrm.txtMI.Text.ToString();

                if (Utilities.AuditTrail.InsertTrail("SE-E", "engineer_tbl", sObject) == 0)
                {
                    MessageBox.Show("Failed to insert audit trail.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                UpdateList();
            }
        }

        public override void CellClick(object sender, DataGridViewCellEventArgs e)
        {
            RecordFrm.ClearControls();

            try
            {
                RecordFrm.AcctNo = RecordFrm.dgvList[0, e.RowIndex].Value.ToString();
                RecordFrm.txtLastName.Text = RecordFrm.dgvList[1, e.RowIndex].Value.ToString();
                RecordFrm.txtFirstName.Text = RecordFrm.dgvList[2, e.RowIndex].Value.ToString();
            }
            catch { }
            try
            {
                RecordFrm.txtMI.Text = RecordFrm.dgvList[3, e.RowIndex].Value.ToString();
            }
            catch { }
            try
            {
                RecordFrm.txtHseNo.Text = RecordFrm.dgvList[5, e.RowIndex].Value.ToString();
            }
            catch { }
            try
            {
                RecordFrm.txtLotNo.Text = RecordFrm.dgvList[6, e.RowIndex].Value.ToString();
            }
            catch { }
            try
            {
                RecordFrm.txtBlkNo.Text = RecordFrm.dgvList[7, e.RowIndex].Value.ToString();
            }
            catch { }
            try {
                RecordFrm.txtStreet.Text = RecordFrm.dgvList[4, e.RowIndex].Value.ToString();
            }
            catch { }
            try
            {
                RecordFrm.cmbBrgy.Text = RecordFrm.dgvList[8, e.RowIndex].Value.ToString();
            }
            catch { }
            try
            {
                RecordFrm.txtMun.Text = RecordFrm.dgvList[9, e.RowIndex].Value.ToString();
            }
            catch { }
            try
            {
                RecordFrm.txtProv.Text = RecordFrm.dgvList[10, e.RowIndex].Value.ToString();
            }
            catch { }
            try
            {
                RecordFrm.txtZIP.Text = RecordFrm.dgvList[11, e.RowIndex].Value.ToString();
            }
            catch { }

            try
            {
                RecordFrm.cmbEngrType.Text = RecordFrm.dgvList[12, e.RowIndex].Value.ToString();
            }
            catch { }
            try
            {
                RecordFrm.txtTIN.Text = RecordFrm.dgvList[13, e.RowIndex].Value.ToString();
            }
            catch { }
            try
            {
                RecordFrm.txtTCT.Text = RecordFrm.dgvList[15, e.RowIndex].Value.ToString();
            }
            catch { }
            try {
                RecordFrm.txtCTC.Text = RecordFrm.dgvList[14, e.RowIndex].Value.ToString();
            }
            catch { }
            try
            {
                RecordFrm.dtpValidDt.Text = RecordFrm.dgvList[16, e.RowIndex].Value.ToString();
            }
            catch { }
            try
            {
                RecordFrm.txtVillage.Text = RecordFrm.dgvList[17, e.RowIndex].Value.ToString(); //added requested subdivision
            }
            catch { }


        }

        public override void Delete()
        {
            var db = new EPSConnection(dbConn);
            string strQuery = string.Empty;

            int iCnt = 0;
            string strWhereCond = $" where (ENGR_CODE = '{RecordFrm.AcctNo}' or ARCHITECT = '{RecordFrm.AcctNo}')";
            var result = (dynamic)null;
                
            result = from a in Records.ApplicationTblList.GetRecord(strWhereCond)
                         select a;
            foreach (var item in result)
            {
                iCnt++;
            }

            result = from a in Records.ApplicationQueList.GetApplicationQue(strWhereCond)
                         select a;
            foreach (var item in result)
            {
                iCnt++;
            }

            if (iCnt > 0)
            {
                MessageBox.Show("Engineer has record.\nDeletion not allowed",RecordFrm.Text,MessageBoxButtons.OK,MessageBoxIcon.Stop);
                return;
            }

            strQuery = $"delete from engineer_tbl where engr_code = '{RecordFrm.AcctNo}'";
            db.Database.ExecuteSqlCommand(strQuery);

            MessageBox.Show("Record deleted", RecordFrm.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            string sObject = string.Empty;

            sObject = "Engr. Code: " + RecordFrm.AcctNo + "/" + RecordFrm.txtLastName.Text.ToString() + ", " + RecordFrm.txtFirstName.Text.ToString() + " " + RecordFrm.cmbEngrType.Text.ToString();

            if (Utilities.AuditTrail.InsertTrail("SE-D", "engineer_tbl", sObject) == 0)
            {
                MessageBox.Show("Failed to insert audit trail.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            UpdateList();
        }

    }
}
