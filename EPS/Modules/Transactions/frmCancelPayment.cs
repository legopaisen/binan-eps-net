using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Common.DataConnector;
using Common.StringUtilities;
using Modules.Utilities;

namespace Modules.Transactions
{
    public partial class frmCancelPayment : Form
    {
        public frmCancelPayment()
        {
            InitializeComponent();
        }

        OracleResultSet result = new OracleResultSet();

        private void frmCancelPayment_Load(object sender, EventArgs e)
        {
            this.LoadList();
        }

        private void LoadList()
        {
            result.Query = @"SELECT distinct a.refno, b.acct_code, b.acct_ln, b.acct_fn, b.acct_mi, a.bill_no, a.or_no, a.date_posted
                                FROM payments_info a, account b WHERE a.payer_code = b.acct_code and a.data_mode = 'POS'";
            dgView.Rows.Clear();
            if (result.Execute())
            {
                while (result.Read())
                {
                    dgView.Rows.Add(
                        result.GetString(0),
                        result.GetString(1),
                        result.GetString(2),
                        result.GetString(3),
                        result.GetString(4),
                        result.GetString(5),
                        result.GetString(6),
                        result.GetDateTime(7).ToShortDateString());
                }
            }
            result.Close();

            lblTotalRec.Text = dgView.RowCount + " Record(s) found";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                result.Query = @"SELECT distinct a.refno, b.acct_code, b.acct_ln, b.acct_fn, b.acct_mi, a.bill_no, a.or_no, a.date_posted
                                FROM payments_info a, account b WHERE a.payer_code = b.acct_code and a.data_mode = 'POS' ";

                if (txtLastName.Text.Trim() != string.Empty ||
                    txtFirstName.Text.Trim() != string.Empty ||
                    txtMI.Text.Trim() != string.Empty)
                {
                    string strData = string.Empty;
                    result.Query += "AND b.Acct_Code IN (SELECT Acct_Code FROM Account WHERE 1=1 ";

                    if (txtLastName.Text != string.Empty)
                        strData = string.Format("AND acct_ln like '%{0}%' ", StringUtilities.HandleApostrophe(txtLastName.Text));

                    if (txtFirstName.Text != string.Empty)
                        strData = string.Format("AND acct_fn like '%{0}%' ", StringUtilities.HandleApostrophe(txtFirstName.Text));

                    if (txtMI.Text != string.Empty)
                        strData = string.Format("AND acct_mi like '%{0}%' ", StringUtilities.HandleApostrophe(txtMI.Text));

                    result.Query += ") ";
                }

                if (txtARN.Text.Trim() != string.Empty)
                    result.Query += string.Format("AND a.refno = '{0}' ", StringUtilities.HandleApostrophe(txtARN.Text));

                if (txtBillNo.Text.Trim() != string.Empty)
                    result.Query += string.Format("AND a.bill_no = '{0}' ", StringUtilities.HandleApostrophe(txtBillNo.Text));

                if (txtORNo.Text.Trim() != string.Empty)
                    result.Query += string.Format("AND a.or_no = '{0}' ", StringUtilities.HandleApostrophe(txtORNo.Text));

                if (chkInclude.CheckState == CheckState.Checked)
                {
                    result.Query += string.Format("AND a.date_posted between to_date('{0}', 'MM/dd/yyyy') AND to_date('{1}', 'MM/dd/yyyy') ", dtpFrom.Value.ToShortDateString(), dtpTo.Value.ToShortDateString());
                }

                dgView.Rows.Clear();
                if (result.Execute())
                {
                    while (result.Read())
                    {
                        dgView.Rows.Add(
                        result.GetString(0),
                        result.GetString(1),
                        result.GetString(2),
                        result.GetString(3),
                        result.GetString(4),
                        result.GetString(5),
                        result.GetString(6),
                        result.GetDateTime(7).ToShortDateString());
                    }
                }
                result.Close();
                lblTotalRec.Text = dgView.RowCount + " Record(s) found";
            }
            catch { }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in this.grpFields.Controls)
            {
                if (ctrl is TextBox)
                    ctrl.Text = string.Empty;
            }

            dtpFrom.Value = DateTime.Now;
            dtpTo.Value = DateTime.Now;

            this.LoadList();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Cancel?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string strARN = dgView.CurrentRow.Cells[0].Value.ToString();
                string strORNo = dgView.CurrentRow.Cells[6].Value.ToString();
                OracleResultSet rs = new OracleResultSet();
                rs.Transaction = true;
                //rs.Query = string.Format("DELETE FROM mrs_payments WHERE arn = '{0}' ", strARN);
                //if (rs.ExecuteNonQuery() == 0) { }

                //rs.Query = string.Format("INSERT INTO application_que SELECT * FROM application WHERE arn = '{0}' ", strARN);
                //if (rs.ExecuteNonQuery() == 0) { }

                //rs.Query = string.Format("DELETE FROM application WHERE arn = '{0}' ", strARN);
                //if (rs.ExecuteNonQuery() == 0) { }

                rs.Query = string.Format("DELETE FROM payments_info WHERE refno = '{0}' and data_mode = 'POS'", strARN);
                if (rs.ExecuteNonQuery() == 0) { }

                rs.Query = string.Format("DELETE FROM payments_tendered WHERE or_no = '{0}' ", strORNo);
                if (rs.ExecuteNonQuery() == 0) { }

                rs.Query = string.Format("DELETE FROM billing_paid WHERE arn = '{0}' ", strARN);
                if (rs.ExecuteNonQuery() == 0) { }

                rs.Query = string.Format("DELETE FROM taxdues_paid WHERE arn = '{0}'", strARN);
                if (rs.ExecuteNonQuery() == 0) { }

                rs.Query = string.Format("DELETE FROM tax_details_paid WHERE arn = '{0}'", strARN);
                if (rs.ExecuteNonQuery() == 0) { }

                rs.Query = string.Format("DELETE FROM payment_denom WHERE arn = '{0}'", strARN);
                if (rs.ExecuteNonQuery() == 0) { }

                rs.Query = string.Format("DELETE FROM debit_credit WHERE or_no = '{0}'", strARN);
                if (rs.ExecuteNonQuery() == 0) { }

                if (!rs.Commit())
                {
                    rs.Rollback();
                    MessageBox.Show("Transaction cannot be cancelled", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                rs.Commit();
                MessageBox.Show("Payment cancelled.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (Utilities.AuditTrail.InsertTrail("P-CP", "PAYMENTS", "ARN: " + strARN) == 0)
                {
                    MessageBox.Show("Failed to insert audit trail.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkInclude_CheckedChanged(object sender, EventArgs e)
        {
            if (chkInclude.CheckState == CheckState.Checked)
            {
                dtpFrom.Enabled = true;
                dtpTo.Enabled = true;
            }
            else if (chkInclude.CheckState == CheckState.Unchecked)
            {
                dtpFrom.Enabled = false;
                dtpTo.Enabled = false;
            }
        }

        private void dgView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgView.RowCount > 0)
            {
                txtARN.Text = dgView.CurrentRow.Cells[0].Value.ToString();
                txtLastName.Text = dgView.CurrentRow.Cells[2].Value.ToString();
                txtFirstName.Text = dgView.CurrentRow.Cells[3].Value.ToString();
                txtMI.Text = dgView.CurrentRow.Cells[4].Value.ToString();
                txtBillNo.Text = dgView.CurrentRow.Cells[5].Value.ToString();
                txtORNo.Text = dgView.CurrentRow.Cells[6].Value.ToString();
            }
        }

        private void dgView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
