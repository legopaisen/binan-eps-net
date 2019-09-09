using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EPSEntities.Connection;
using Common.StringUtilities;
using Modules.Records;
    
namespace Modules.Utilities
{
    public class FormOwner:FormOwnerClass
    {
        public static ConnectionString dbConn = new ConnectionString();

        public FormOwner(frmOwner Form) : base(Form)
        { }

        public override void FormLoad()
        {
            RecordFrm.Text = "Owner's Profile";

            UpdateList();
            RecordFrm.cmbEngrType.Visible = false;
            RecordFrm.lblTelNo.Text = "Telephone No.:";
            RecordFrm.txtTelNo.Visible = true;
            RecordFrm.lblTCT.Text = "TCT/OCT No. :";
            RecordFrm.lblCTC.Text = "CTC No. :";
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
            RecordFrm.dgvList.Columns.Add("TIN", "TIN");
            RecordFrm.dgvList.Columns.Add("TCT", "TCT");
            RecordFrm.dgvList.Columns.Add("CTC", "CTC");
            RecordFrm.dgvList.Columns.Add("TelNo", "TelNo");

            AccountsList account = new AccountsList("", "", "", "");
            int iCnt = 0;
            for (int i = 0; i < account.AcctLst.Count; i++)
            {
                iCnt++;
                RecordFrm.dgvList.Rows.Add(account.AcctLst[i].OwnerCode, account.AcctLst[i].LastName,
                    account.AcctLst[i].FirstName, account.AcctLst[i].MiddleInitial, account.AcctLst[i].Address,
                    account.AcctLst[i].HouseNo, account.AcctLst[i].LotNo, account.AcctLst[i].BlkNo,
                    account.AcctLst[i].Barangay, account.AcctLst[i].City, account.AcctLst[i].Province,
                    account.AcctLst[i].ZIP, account.AcctLst[i].TIN, account.AcctLst[i].TCT, account.AcctLst[i].CTC, account.AcctLst[i].TelNo);
            }

            RecordFrm.lblNoRecords.Text += " " + iCnt.ToString("#,###");
        }

        public override bool SearchAccount()
        {
            string sAcctNo = string.Empty;

            Utilities.Accounts acct = new Utilities.Accounts();
            acct.GetOwner(RecordFrm.txtLastName.Text.Trim(), RecordFrm.txtFirstName.Text.Trim(), RecordFrm.txtMI.Text.Trim());
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

            if (string.IsNullOrEmpty(RecordFrm.txtTCT.Text.ToString().Trim()))
            {
                MessageBox.Show("TCT/OCT no. is required", RecordFrm.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (string.IsNullOrEmpty(RecordFrm.txtCTC.Text.ToString().Trim()))
            {
                MessageBox.Show("CTC no. is required", RecordFrm.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            Accounts account = new Accounts();
            account.GetOwner(RecordFrm.txtLastName.Text.ToString().Trim(),RecordFrm.txtFirstName.Text.ToString().Trim(), RecordFrm.txtMI.Text.ToString().Trim());
            //RecordFrm.AcctNo = account.OwnerCode;

            if (string.IsNullOrEmpty(RecordFrm.AcctNo))
            {
                if (!string.IsNullOrEmpty(account.OwnerCode))
                {
                    MessageBox.Show("Owner already exists", RecordFrm.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }
            return true;
        }

        public override void Save()
        {
            if (string.IsNullOrEmpty(RecordFrm.AcctNo))
            {
                Accounts account = new Accounts();
                account.CreateAccount(RecordFrm.txtLastName.Text.ToString(), RecordFrm.txtFirstName.Text.ToString(),
                    RecordFrm.txtMI.Text.ToString(), RecordFrm.txtStreet.Text.ToString(), RecordFrm.txtHseNo.Text.ToString(),
                    RecordFrm.txtLotNo.Text.ToString(), RecordFrm.txtBlkNo.Text.ToString(), RecordFrm.cmbBrgy.Text.ToString(),
                    RecordFrm.txtMun.Text.ToString(), RecordFrm.txtProv.Text.ToString(), RecordFrm.txtZIP.Text.ToString(),
                    RecordFrm.txtTIN.Text.ToString(), RecordFrm.txtTCT.Text.ToString(), RecordFrm.txtCTC.Text.ToString(), RecordFrm.txtTelNo.Text.ToString());

                RecordFrm.AcctNo = account.OwnerCode;

                if (string.IsNullOrEmpty(RecordFrm.AcctNo))
                {
                    MessageBox.Show("Cannot create account record", RecordFrm.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                MessageBox.Show("Record saved", RecordFrm.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                string sObject = string.Empty;
                sObject = "Acct Code: " + RecordFrm.AcctNo + "/" + RecordFrm.txtLastName.Text.ToString() + ", " + RecordFrm.txtFirstName.Text.ToString() + " " + RecordFrm.txtMI.Text.ToString();

                if (Utilities.AuditTrail.InsertTrail("SLS-A", "account", sObject) == 0)
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

                strQuery = $"update account set ACCT_LN = '{StringUtilities.HandleApostrophe(RecordFrm.txtLastName.Text.ToString()).Trim()}', ";
                strQuery += $" ACCT_FN = '{StringUtilities.HandleApostrophe(RecordFrm.txtFirstName.Text.ToString()).Trim()}', ";
                strQuery += $" ACCT_MI = '{StringUtilities.HandleApostrophe(RecordFrm.txtMI.Text.ToString()).Trim()}', ";
                strQuery += $" ACCT_HSE_NO = '{StringUtilities.HandleApostrophe(RecordFrm.txtHseNo.Text.ToString()).Trim()}', ";
                strQuery += $" ACCT_LOT_NO = '{StringUtilities.HandleApostrophe(RecordFrm.txtLotNo.Text.ToString()).Trim()}', ";
                strQuery += $" ACCT_BLK_NO= '{StringUtilities.HandleApostrophe(RecordFrm.txtBlkNo.Text.ToString()).Trim()}', ";
                strQuery += $" ACCT_ADDR = '{StringUtilities.HandleApostrophe(RecordFrm.txtStreet.Text.ToString()).Trim()}', ";
                strQuery += $" ACCT_BRGY = '{StringUtilities.HandleApostrophe(RecordFrm.cmbBrgy.Text.ToString()).Trim()}', ";
                strQuery += $" ACCT_CITY = '{StringUtilities.HandleApostrophe(RecordFrm.txtMun.Text.ToString()).Trim()}', ";
                strQuery += $" ACCT_PROV = '{StringUtilities.HandleApostrophe(RecordFrm.txtProv.Text.ToString()).Trim()}', ";
                strQuery += $" ACCT_ZIP = '{StringUtilities.HandleApostrophe(RecordFrm.txtZIP.Text.ToString()).Trim()}', ";
                strQuery += $" ACCT_TCT= '{StringUtilities.HandleApostrophe(RecordFrm.txtTCT.Text.ToString()).Trim()}', ";
                strQuery += $" ACCT_TIN = '{StringUtilities.HandleApostrophe(RecordFrm.txtTIN.Text.ToString()).Trim()}', ";
                strQuery += $" ACCT_CTC = '{StringUtilities.HandleApostrophe(RecordFrm.txtCTC.Text.ToString()).Trim()}', ";
                strQuery += $" ACCT_TELNO = '{StringUtilities.HandleApostrophe(RecordFrm.txtTelNo.Text.ToString()).Trim()}' ";
                strQuery += $" where ACCT_CODE = '{RecordFrm.AcctNo}' ";
                db.Database.ExecuteSqlCommand(strQuery);

                MessageBox.Show("Record updated", RecordFrm.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                string sObject = string.Empty;
                sObject = "Acct Code: " + RecordFrm.AcctNo + "/" + RecordFrm.txtLastName.Text.ToString() + ", " + RecordFrm.txtFirstName.Text.ToString() + " " + RecordFrm.txtMI.Text.ToString();

                if (Utilities.AuditTrail.InsertTrail("SLS-E", "account", sObject) == 0)
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
                RecordFrm.txtHseNo.Text = RecordFrm.dgvList[4, e.RowIndex].Value.ToString();
            }
            catch { }
            try
            {
                RecordFrm.txtLotNo.Text = RecordFrm.dgvList[5, e.RowIndex].Value.ToString();
            }
            catch { }
            try
            {
                RecordFrm.txtBlkNo.Text = RecordFrm.dgvList[6, e.RowIndex].Value.ToString();
            }
            catch { }
            try {
                RecordFrm.txtStreet.Text = RecordFrm.dgvList[7, e.RowIndex].Value.ToString();
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
                RecordFrm.txtTIN.Text = RecordFrm.dgvList[12, e.RowIndex].Value.ToString();
            }
            catch { }
            try
            {
                RecordFrm.txtTCT.Text = RecordFrm.dgvList[13, e.RowIndex].Value.ToString();
            }
            catch { }
            try {
                RecordFrm.txtCTC.Text = RecordFrm.dgvList[14, e.RowIndex].Value.ToString();
            }
            catch { }
            try { 
            RecordFrm.txtTelNo.Text = RecordFrm.dgvList[15, e.RowIndex].Value.ToString();
            }
            catch { }

        }

        public override void Delete()
        {
            var db = new EPSConnection(dbConn);
            string strQuery = string.Empty;

            int iCnt = 0;
            string strWhereCond = $" where (PROJ_OWNER = '{RecordFrm.AcctNo}' or PROJ_LOT_OWNER = '{RecordFrm.AcctNo}')";
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
                MessageBox.Show("Owner has record.\nDeletion not allowed",RecordFrm.Text,MessageBoxButtons.OK,MessageBoxIcon.Stop);
                return;
            }

            strQuery = $"delete from account where acct_code = '{RecordFrm.AcctNo}'";
            db.Database.ExecuteSqlCommand(strQuery);

            MessageBox.Show("Record deleted", RecordFrm.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            string sObject = string.Empty;

            sObject = "Acct Code: " + RecordFrm.AcctNo + "/" + RecordFrm.txtLastName.Text.ToString() + ", " + RecordFrm.txtFirstName.Text.ToString() + " " + RecordFrm.txtMI.Text.ToString();

            if (Utilities.AuditTrail.InsertTrail("SLS-D", "account", sObject) == 0)
            {
                MessageBox.Show("Failed to insert audit trail.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            UpdateList();
        }

    }
}
