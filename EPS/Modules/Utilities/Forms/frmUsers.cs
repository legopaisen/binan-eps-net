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
using Common.AppSettings;
using Common.EncryptUtilities;
using Common.StringUtilities;
using Oracle.ManagedDataAccess.Client;

namespace Modules.Utilities.Forms
{
    public partial class frmUsers : Form
    {
        private static ConnectionString dbConn = new ConnectionString();
        private string m_sUserCode = string.Empty;
        private string m_sPassword = string.Empty;
        private string m_sConfirmPass = string.Empty;

        public frmUsers()
        {
            InitializeComponent();
        }
        
        private void frmUsers_Load(object sender, EventArgs e)
        {
            UpdateUserList();
            UpdateModuleList();
            LoadUserLevel();
            EnableControls(false);
        }

        private void ClearControls()
        {
            m_sUserCode = string.Empty;
            txtUserCode.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtFirstName.Text = string.Empty;
            txtMI.Text = string.Empty;
            txtPosition.Text = string.Empty;
            txtMemo.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtConfirmPass.Text = string.Empty;
            cmbUserLevel.Text = string.Empty;
        }

        private void EnableControls(bool bEnable)
        {
            txtUserCode.ReadOnly = !bEnable;
            txtLastName.ReadOnly = !bEnable;
            txtFirstName.ReadOnly = !bEnable;
            txtMI.ReadOnly = !bEnable;
            txtPosition.ReadOnly = !bEnable;
            txtMemo.ReadOnly = !bEnable;
            txtPassword.ReadOnly = !bEnable;
            txtConfirmPass.ReadOnly = !bEnable;
            cmbUserLevel.Enabled = bEnable;
            dgvModuleList.ReadOnly = !bEnable;
            btnGrant.Enabled = bEnable;
            btnRevoke.Enabled = bEnable;
        }

        private void LoadUserLevel()
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;
            string sUserLevel = string.Empty;
            
            cmbUserLevel.Items.Add("");

            sQuery = "select * from user_level";
            var epsrec = db.Database.SqlQuery<USER_LEVEL>(sQuery);

            foreach (var items in epsrec)
            {
                cmbUserLevel.Items.Add(items.USER_LEV);
                
            }
            
        }

        private void UpdateUserList()
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;

            sQuery = "select * from users order by user_ln,user_code";
            var epsrec = db.Database.SqlQuery<USERS>(sQuery);

            dgvUserList.Rows.Clear();

            foreach (var items in epsrec)
            {
                dgvUserList.Rows.Add(items.USER_CODE, items.USER_LN, items.USER_FN, items.USER_MI, items.USER_POS, items.USER_LEVEL, items.USER_PWD, items.USER_MEMO);
            }
        }

        private void UpdateModuleList()
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;
            string sTemp = string.Empty;

            dgvModuleList.Rows.Clear();
            sTemp = GetUserLevel(AppSettingsManager.SystemUser.UserCode);

            if (sTemp == "ADMINISTRATOR" || sTemp == "ADMINISTRATOR|")
                sQuery = $"select right_desc,right_code from rights where right_code in (select module_code from user_level_rights where user_level = '{cmbUserLevel.Text.ToString()}') order by right_desc";
            else
                sQuery = $"select right_desc,right_code from rights where right_code in (select module_code from user_level_rights where user_level = '{cmbUserLevel.Text.ToString()}') and right_code <> 'BOA' order by right_desc";
            var epsrec = db.Database.SqlQuery<RIGHTS>(sQuery);

            foreach (var items in epsrec)
            {
                dgvModuleList.Rows.Add(false, items.RIGHT_DESC, items.RIGHT_CODE);
            }

            FindRights();
        }

        private string GetUserLevel(string sUserCode)
        {
            string sUserLevel = string.Empty;
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;

            sQuery = $"Select * From Users where user_code = '{sUserCode}'";
            var epsrec = db.Database.SqlQuery<USERS>(sQuery);

            foreach (var items in epsrec)
            {
                sUserLevel = items.USER_LEVEL;
            }

            return sUserLevel;
        }

        private void dgvUserList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ClearControls();

            try
            {
                if (dgvUserList[0, e.RowIndex].Value != null)
                {
                    m_sUserCode = dgvUserList[0, e.RowIndex].Value.ToString();
                    txtUserCode.Text = dgvUserList[0, e.RowIndex].Value.ToString();
                }

                if (dgvUserList[1, e.RowIndex].Value != null)
                    txtLastName.Text = dgvUserList[1, e.RowIndex].Value.ToString();

                if (dgvUserList[2, e.RowIndex].Value != null)
                    txtFirstName.Text = dgvUserList[2, e.RowIndex].Value.ToString();

                if (dgvUserList[3, e.RowIndex].Value != null)
                    txtMI.Text = dgvUserList[3, e.RowIndex].Value.ToString();

                if (dgvUserList[4, e.RowIndex].Value != null)
                    txtPosition.Text = dgvUserList[4, e.RowIndex].Value.ToString();

                if (dgvUserList[5, e.RowIndex].Value != null)
                    cmbUserLevel.Text = dgvUserList[5, e.RowIndex].Value.ToString();

                if (dgvUserList[6, e.RowIndex].Value != null)
                {
                    m_sPassword = dgvUserList[6, e.RowIndex].Value.ToString();
                    m_sConfirmPass = m_sPassword;
                }

                if (dgvUserList[7, e.RowIndex].Value != null)
                    txtMemo.Text = dgvUserList[7, e.RowIndex].Value.ToString();

                UpdateModuleList();
            }
            catch { }
            

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;

            if (btnAdd.Text == "Add")
            {
                btnAdd.Text = "Save";
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                btnExit.Text = "Cancel";

                EnableControls(true);
                ClearControls();
                UpdateModuleList();
                txtUserCode.Focus();
            }
            else
            {
                if (string.IsNullOrEmpty(txtUserCode.Text.ToString()))
                {
                    MessageBox.Show("User Code is required", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                if (string.IsNullOrEmpty(txtLastName.Text.ToString()))
                {
                    MessageBox.Show("Last Name is required", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                if (string.IsNullOrEmpty(txtFirstName.Text.ToString()))
                {
                    MessageBox.Show("First Name is required", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                if (string.IsNullOrEmpty(cmbUserLevel.Text.ToString()))
                {
                    MessageBox.Show("User Level is required", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                if (string.IsNullOrEmpty(txtPassword.Text.ToString()))
                {
                    MessageBox.Show("Password is required", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                if (string.IsNullOrEmpty(txtConfirmPass.Text.ToString()))
                {
                    MessageBox.Show("Please Confirm User's Password", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                if (txtPassword.Text.ToString().Trim() != txtConfirmPass.Text.ToString().Trim())
                {
                    MessageBox.Show("Password entries do not match", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                int iCnt = 0;
                sQuery = $"select count(*) from users where user_code = '{txtUserCode.Text.ToString().Trim()}'";
                iCnt = db.Database.SqlQuery<Int32>(sQuery).SingleOrDefault();

                if (iCnt > 0)
                {
                    MessageBox.Show("User Code " + txtUserCode.Text.ToString().Trim() + " already exists.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                iCnt = 0;
                sQuery = $"select count(*) from users where user_ln = '{txtLastName.Text.ToString().Trim()}' ";
                sQuery += $" and user_fn = '{txtFirstName.Text.ToString().Trim()}' and user_code <> '{txtUserCode.Text.ToString().Trim()}'";
                iCnt = db.Database.SqlQuery<Int32>(sQuery).SingleOrDefault();

                if (iCnt > 0)
                {
                    if (MessageBox.Show("Duplicate lastname and firstname with the other record./n Continue?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;
                }

                if (MessageBox.Show("Are you sure you want to Save this record?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    m_sUserCode = txtUserCode.Text.ToString().Trim();
                    SaveUser("SUA-A");

                    string sObj = string.Empty;
                    string sUserName = string.Empty;
                    sUserName = PersonName.ToPersonName(txtLastName.Text.ToString().Trim(), txtFirstName.Text.ToString().Trim(), txtMI.Text.ToString().Trim(), "L", "L, F", "L, F M.");

                    sObj = "User Code: " + txtUserCode.Text.ToString().Trim();
                    sObj += "/User Name: " + sUserName;

                    if (Utilities.AuditTrail.InsertTrail("SUA-A", "users", sObj) == 0)
                    {
                        MessageBox.Show("Failed to insert audit trail.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    btnAdd.Text = "Add";
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;
                    btnExit.Text = "Exit";

                    EnableControls(false);
                    UpdateUserList();

                    MessageBox.Show("User record added", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void SaveUser(string sTransCode)
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;

            sQuery = "delete from users where user_code = '" + m_sUserCode + "'";
            db.Database.ExecuteSqlCommand(sQuery);

            sQuery = "delete from users_rights where user_code = '" + m_sUserCode + "'";
            db.Database.ExecuteSqlCommand(sQuery);

            sQuery = $"insert into users values (:1,:2,:3,:4,:5,:6,:7,:8)";
            db.Database.ExecuteSqlCommand(sQuery,
                new OracleParameter(":1", StringUtilities.HandleApostrophe(m_sUserCode)),
                new OracleParameter(":2", StringUtilities.HandleApostrophe(txtLastName.Text.ToString().Trim())),
                new OracleParameter(":3", StringUtilities.HandleApostrophe(txtFirstName.Text.ToString().Trim())),
                new OracleParameter(":4", StringUtilities.HandleApostrophe(txtMI.Text.ToString().Trim())),
                new OracleParameter(":5", StringUtilities.HandleApostrophe(txtPosition.Text.ToString().Trim())),
                new OracleParameter(":6", StringUtilities.HandleApostrophe(cmbUserLevel.Text.ToString().Trim())),
                new OracleParameter(":7", StringUtilities.HandleApostrophe(m_sPassword)),
                new OracleParameter(":8", StringUtilities.HandleApostrophe(txtMemo.Text.ToString().Trim())));

            SaveRights(sTransCode);
        }

        private void SaveRights(string sTransCode)
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;
            string sModuleCode = string.Empty;

            for (int i = 0; i < dgvModuleList.Rows.Count; i++)
            {
                sModuleCode = dgvModuleList[2, i].Value.ToString();

                if ((bool)dgvModuleList[0, i].Value)
                {
                    sQuery = $"insert into users_rights values (:1,:2)";
                    db.Database.ExecuteSqlCommand(sQuery,
                        new OracleParameter(":1", StringUtilities.HandleApostrophe(m_sUserCode)),
                        new OracleParameter(":2", sModuleCode));
                    
                    string sObj = string.Empty;
                    sObj = "User Code: " + txtUserCode.Text.ToString().Trim();
                    sObj += "/Right Code: " + sModuleCode;

                    if (Utilities.AuditTrail.InsertTrail(sTransCode, "users_rights", sObj) == 0)
                    {
                        MessageBox.Show("Failed to insert audit trail.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                }
            }
        }
        
        private void FindRights()
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;
            string sModuleCode = string.Empty;

            for (int i = 0; i <dgvModuleList.Rows.Count; i++)
            {
                sModuleCode = dgvModuleList[2, i].Value.ToString();

                int iCnt = 0;
                sQuery = $"select count(*) from users_rights where user_code = '{m_sUserCode}' and right_code = '{sModuleCode}'";
                iCnt = db.Database.SqlQuery<Int32>(sQuery).SingleOrDefault();

                if (iCnt > 0)
                {
                    dgvModuleList[0, i].Value = true;
                }
                else
                {
                    dgvModuleList[0, i].Value = false;
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (btnExit.Text == "Cancel")
            {
                btnExit.Text = "Exit";
                btnAdd.Enabled = true;
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                ClearControls();
                EnableControls(false);
                UpdateUserList();
                UpdateModuleList();
            }
            else
            {
                this.Close();
            }
        }

        private void btnGrant_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < dgvModuleList.Rows.Count; i++)
            {
                dgvModuleList[0, i].Value = true;
            }
        }

        private void btnRevoke_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvModuleList.Rows.Count; i++)
            {
                dgvModuleList[0, i].Value = false;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;

            if (btnEdit.Text == "Edit")
            {
                if (string.IsNullOrEmpty(m_sUserCode))
                {
                    MessageBox.Show("Please select the record to edit.",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Stop);
                    return;
                }

                btnEdit.Text = "Update";
                btnAdd.Enabled = false;
                btnDelete.Enabled = false;
                btnExit.Text = "Cancel";
                EnableControls(true);
                txtUserCode.ReadOnly = true;
                txtLastName.Focus();
            }
            else
            {
                if (string.IsNullOrEmpty(txtLastName.Text.ToString()))
                {
                    MessageBox.Show("Last Name is required", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                if (string.IsNullOrEmpty(txtFirstName.Text.ToString()))
                {
                    MessageBox.Show("First Name is required", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                if (string.IsNullOrEmpty(cmbUserLevel.Text.ToString()))
                {
                    MessageBox.Show("User Level is required", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                if (m_sPassword != m_sConfirmPass)
                {
                    MessageBox.Show("Password entries do not match", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                int iCnt = 0;
                sQuery = $"select count(*) from users where user_ln = '{txtLastName.Text.ToString().Trim()}' ";
                sQuery += $" and user_fn = '{txtFirstName.Text.ToString().Trim()}' and user_code <> '{m_sUserCode}'";
                iCnt = db.Database.SqlQuery<Int32>(sQuery).SingleOrDefault();

                if (iCnt > 0)
                {
                    if (MessageBox.Show("Duplicate lastname and firstname with the other record./n Continue?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;
                }

                if (MessageBox.Show("Are you sure you want to Update this record?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SaveUser("SUA-E");

                    string sObj = string.Empty;
                    string sUserName = string.Empty;
                    sUserName = PersonName.ToPersonName(txtLastName.Text.ToString().Trim(), txtFirstName.Text.ToString().Trim(), txtMI.Text.ToString().Trim(), "L", "L, F", "L, F M.");

                    sObj = "User Code: " + txtUserCode.Text.ToString().Trim();
                    sObj += "/User Name: " + sUserName;

                    if (Utilities.AuditTrail.InsertTrail("SUA-E", "users", sObj) == 0)
                    {
                        MessageBox.Show("Failed to insert audit trail.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    UpdateUserList();
                    UpdateModuleList();
                    btnEdit.Text = "Edit";
                    btnAdd.Enabled = true;
                    btnDelete.Enabled = true;
                    btnExit.Text = "Exit";
                    EnableControls(false);
                    ClearControls();
                    MessageBox.Show("User record updated", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            Encryption enc = new Encryption();

            m_sPassword = enc.EncryptString(txtPassword.Text.ToString().Trim());
        }

        private void txtConfirmPass_Leave(object sender, EventArgs e)
        {
            Encryption enc = new Encryption();

            m_sConfirmPass = enc.EncryptString(txtConfirmPass.Text.ToString().Trim());
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(m_sUserCode))
            {
                MessageBox.Show("Please select the record to delete.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;

            int iCnt = 0;

            sQuery = $"select count(*) from trail where user_code = '{m_sUserCode}'";
            iCnt = db.Database.SqlQuery<Int32>(sQuery).SingleOrDefault();

            if (iCnt > 0)
            {
                MessageBox.Show("User Code:" + m_sUserCode + " has transaction.\nDeletion not allowed.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this record?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sQuery = "delete from users where user_code = '" + m_sUserCode + "'";
                db.Database.ExecuteSqlCommand(sQuery);

                string sObj = string.Empty;
                string sUserName = string.Empty;
                sUserName = PersonName.ToPersonName(txtLastName.Text.ToString().Trim(), txtFirstName.Text.ToString().Trim(), txtMI.Text.ToString().Trim(), "L", "L, F", "L, F M.");

                sObj = "User Code: " + m_sUserCode;
                sObj += "/User Name: " + sUserName;

                if (Utilities.AuditTrail.InsertTrail("SUA-D", "users", sObj) == 0)
                {
                    MessageBox.Show("Failed to insert audit trail.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                sQuery = "delete from users_rights where user_code = '" + m_sUserCode + "'";
                db.Database.ExecuteSqlCommand(sQuery);

                MessageBox.Show("User record deleted",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Information);

                UpdateUserList();
                UpdateModuleList();
                ClearControls();
            }
        }

        private void cmbUserLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateModuleList();
        }
    }
}
