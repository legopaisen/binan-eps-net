using Common.AppSettings;
using Common.DataConnector;
using Modules.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Modules.Utilities.Forms
{
    public partial class frmAuditTrail : Form
    {
        public frmAuditTrail()
        {
            InitializeComponent();
        }

        private string m_sTrailMode = string.Empty;

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Init()
        {
            cmbUserCode.Items.Clear();
            cmbModule.Items.Clear();
            dtFrom.Text = AppSettingsManager.GetCurrentDate().ToShortDateString();
            dtTo.Text = AppSettingsManager.GetCurrentDate().ToShortDateString();
        }

        private void frmAuditTrail_Load(object sender, EventArgs e)
        {
            Init();
            LoadData();
            cmbUserCode.Enabled = false;
            cmbModule.Enabled = false;
        }

        private void LoadData()
        {
            cmbUserCode.SelectedIndex = -1;
            cmbModule.SelectedIndex = -1;

            OracleResultSet result = new OracleResultSet();
            result.Query = "select user_code from users order by user_code";
            if(result.Execute())
                while(result.Read())
                {
                    cmbUserCode.Items.Add(result.GetString(0).Trim());
                }
            result.Close();

            result.Query = "select * from modules order by module_desc";
            if(result.Execute())
                while(result.Read())
                {
                    cmbModule.Items.Add(result.GetString(1).Trim());
                }
            result.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rdoByUser_CheckedChanged(object sender, EventArgs e)
        {
            if(rdoByUser.Checked == true)
            {
                cmbUserCode.Enabled = true;
                cmbModule.Enabled = false;
                m_sTrailMode = "User";
            }
        }

        private void rdoByMod_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoByMod.Checked == true)
            {
                cmbUserCode.Enabled = false;
                cmbModule.Enabled = true;
                m_sTrailMode = "Module";
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if(dtTo.Value < dtFrom.Value)
            {
                MessageBox.Show("Invalid Date!");
                return;
            }

            frmReport form = new frmReport();
            form.ReportName = "AUDIT TRAIL";
            form.dtFrom = dtFrom.Value;
            form.dtTo = dtTo.Value;
            form.User = cmbUserCode.Text.Trim();
            form.Module = cmbModule.Text.Trim();
            form.TrailMode = m_sTrailMode;
            form.ShowDialog();
        }
    }
}
