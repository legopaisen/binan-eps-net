using Common.AppSettings;
using Common.DataConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Modules.Reports
{
    public partial class frmEncoderLog : Form
    {
        public frmEncoderLog()
        {
            InitializeComponent();
        }

        private void frmEncoderLog_Load(object sender, EventArgs e)
        {
            ClearControls();
        }

        private void ClearControls()
        {
            dtFrom.Text = AppSettingsManager.GetSystemDate().ToShortDateString();
            dtTo.Text = AppSettingsManager.GetSystemDate().ToShortDateString();
        }



        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool ValidateData()
        {
            if (dtTo.Value < dtFrom.Value)
            {
                MessageBox.Show("Invalid date!");
                return false;
            }
            return true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!ValidateData())
                return;

            frmReport form = new frmReport();
            form.ReportName = "ENCODER LOG";
            form.dtFrom = dtFrom.Value;
            form.dtTo = dtTo.Value;
            form.ShowDialog();

            ClearControls();

        }

        private void cmbBrgy_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void rdoByEncoder_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
