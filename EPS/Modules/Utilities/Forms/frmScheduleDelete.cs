using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using EPSEntities.Connection;
using Modules.Utilities;

namespace Modules.Utilities.Forms
{
    public partial class frmScheduleDelete : Form
    {
        public static ConnectionString dbConn = new ConnectionString();

        public frmScheduleDelete()
        {
            InitializeComponent();
        }

        private void ClearControls()
        {
            txtRevAcct.Text = "";
            txtSubCat.Text = "";
            txtSubsidiary.Text = "";
        }

        private void rdoRevAcct_CheckedChanged(object sender, EventArgs e)
        {
            txtRevAcct.ReadOnly = false;
            txtSubCat.ReadOnly = true;
            txtSubsidiary.ReadOnly = true;
            ClearControls();
        }

        private void rdoSubCat_CheckedChanged(object sender, EventArgs e)
        {
            txtRevAcct.ReadOnly = true;
            txtSubCat.ReadOnly = false;
            txtSubsidiary.ReadOnly = true;
            ClearControls();
        }

        private void rdoSubsidiary_CheckedChanged(object sender, EventArgs e)
        {
            txtRevAcct.ReadOnly = true;
            txtSubCat.ReadOnly = true;
            txtSubsidiary.ReadOnly = false;
            ClearControls();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;
            string sCode = string.Empty;

            if (IsFeeInUse())
                return;

            if (!Validate())
                return;


            if (rdoRevAcct.Checked)
            {
                sCode = txtRevAcct.Text.ToString().Trim();
                if (MessageBox.Show("The Revenue Account " + sCode + " will be permanently deleted.\nDo you want to continue? ", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sQuery = $"delete from major_fees where fees_code = '{sCode}'";
                    db.Database.ExecuteSqlCommand(sQuery);

                    sQuery = $"delete from subcategories where fees_code like '{sCode}%'";
                    db.Database.ExecuteSqlCommand(sQuery);

                    sQuery = $"delete from schedules where fees_code like '{sCode}%'";
                    db.Database.ExecuteSqlCommand(sQuery);

                    MessageBox.Show("Schedule was deleted.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    if (Utilities.AuditTrail.InsertTrail("STS-D", "major_fees", "Fees Code:" + sCode) == 0)
                    {
                        MessageBox.Show("Failed to insert audit trail.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    this.Close();
                }
            }
            else if (rdoSubCat.Checked)
            {
                sCode = txtSubCat.Text.ToString().Trim();

                if (MessageBox.Show("Subcategory " + sCode + " will be permanently deleted.\nDo you want to continue? ", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sQuery = $"delete from subcategories where fees_code = '{sCode}'";
                    db.Database.ExecuteSqlCommand(sQuery);

                    sQuery = $"delete from schedules where fees_code like '{sCode}%'";
                    db.Database.ExecuteSqlCommand(sQuery);

                    MessageBox.Show("Schedule was deleted.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    if (Utilities.AuditTrail.InsertTrail("STS-D", "subcategories/schedules", "ALL Fees Code:" + sCode) == 0)
                    {
                        MessageBox.Show("Failed to insert audit trail.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    this.Close();
                }

            }
            else
            {
                sCode = txtSubsidiary.Text.ToString().Trim();

                if (MessageBox.Show("Subsidiary " + sCode + " will be permanently deleted.\nDo you want to continue? ", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sQuery = $"delete from subcategories where fees_code = '{sCode}'";
                    db.Database.ExecuteSqlCommand(sQuery);

                    sQuery = $"delete from schedules where fees_code = '{sCode}'";
                    db.Database.ExecuteSqlCommand(sQuery);

                    MessageBox.Show("Schedule was deleted.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    if (Utilities.AuditTrail.InsertTrail("STS-D", "subcategories/schedules", "Fees Code:" + sCode) == 0)
                    {
                        MessageBox.Show("Failed to insert audit trail.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    this.Close();
                }

            }

            
        }

        private bool IsFeeInUse()
        {
            var db = new EPSConnection(dbConn);
            string sFeesCode = string.Empty;
            string sQuery = string.Empty;
            int iCnt = 0;
            int iPayCnt = 0;

            if (rdoRevAcct.Checked)
            {
                sFeesCode = txtRevAcct.Text.ToString().Trim();

                try
                {
                    sQuery = $"select count(*) from permit_tbl where fees_code like '{sFeesCode}%'";
                    iCnt = db.Database.SqlQuery<Int32>(sQuery).SingleOrDefault();
                }
                catch { }
                try
                {
                    sQuery = $"select count(*) from mrs_payments where fees_code like '{sFeesCode}%'";
                    iPayCnt = db.Database.SqlQuery<Int32>(sQuery).SingleOrDefault();
                }
                catch { }
            }
            if (rdoSubCat.Checked)
            {
                sFeesCode = txtSubCat.Text.ToString().Trim();

                try
                {
                    sQuery = $"select count(*) from permit_tbl where fees_code like '{sFeesCode}%'";
                    iCnt = db.Database.SqlQuery<Int32>(sQuery).SingleOrDefault();
                }
                catch { }

                try
                {
                    sQuery = $"select count(*) from mrs_payments where fees_code like '{sFeesCode}%'";
                    iPayCnt = db.Database.SqlQuery<Int32>(sQuery).SingleOrDefault();
                }
                catch { }
            }
            if (rdoSubsidiary.Checked)
            {
                sFeesCode = txtSubsidiary.Text.ToString().Trim();
                try
                {
                    sQuery = $"select count(*) from permit_tbl where fees_code = '{sFeesCode}'";
                    iCnt = db.Database.SqlQuery<Int32>(sQuery).SingleOrDefault();
                }
                catch { }
                try
                {
                    sQuery = $"select count(*) from mrs_payments where fees_code = '{sFeesCode}'";
                    iPayCnt = db.Database.SqlQuery<Int32>(sQuery).SingleOrDefault();
                }
                catch { }
            }

            if(iCnt>0)
            {
                MessageBox.Show("There are records found using this Fee: " + sFeesCode + ".\nDeletion not allowed.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return true;
            }

            if(iPayCnt > 0)
            {
                MessageBox.Show("Payment record found using this Fee: " + sFeesCode + ".\nDeletion not allowed.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return true;
            }

            return false;
        }

        private bool Validate()
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;
            int iCnt = 0;

            if (rdoRevAcct.Checked)
            {
                if (string.IsNullOrEmpty(txtRevAcct.Text.ToString().Trim()))
                {
                    MessageBox.Show("Enter a revenue account code first", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }

                sQuery = $"select count(*) from major_fees where fees_code = '{txtRevAcct.Text.ToString().Trim()}'";
                iCnt = db.Database.SqlQuery<Int32>(sQuery).SingleOrDefault();
            }

            if (rdoSubCat.Checked)
            {
                if (string.IsNullOrEmpty(txtSubCat.Text.ToString().Trim()))
                {
                    MessageBox.Show("Enter a subcategory code first", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }

                sQuery = $"select count(*) from subcategories where fees_code = '{txtSubCat.Text.ToString().Trim()}'";
                sQuery += " and fees_term = 'SUBCATEGORY'";
                iCnt = db.Database.SqlQuery<Int32>(sQuery).SingleOrDefault();
            }

            if (rdoSubsidiary.Checked)
            {
                if (string.IsNullOrEmpty(txtSubsidiary.Text.ToString().Trim()))
                {
                    MessageBox.Show("Enter a subsidiary code first", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }

                sQuery = $"select count(*) from subcategories where fees_code = '{txtSubsidiary.Text.ToString().Trim()}'";
                sQuery += " and fees_term <> 'SUBCATEGORY'";
                iCnt = db.Database.SqlQuery<Int32>(sQuery).SingleOrDefault();
            }

            if(iCnt ==0)
            {
                MessageBox.Show("No record found.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
