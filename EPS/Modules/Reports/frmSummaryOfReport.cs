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
    public partial class frmSummaryOfReport : Form
    {
        public frmSummaryOfReport()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if(cmbReportType.SelectedIndex == -1)
            {
                MessageBox.Show("No report type selected");
                return;
            }

            if(dtFrom.Value > dtTo.Value)
            {
                MessageBox.Show("Date range invalid");
                return;
            }

            frmReport form = new frmReport();
            form.dtTo = Convert.ToDateTime(dtTo.Text);
            form.dtFrom = Convert.ToDateTime(dtFrom.Text);

            if (cmbReportType.Text.Contains("BUILDING INFORMATION"))
                form.ReportName = "Building Information";
            else if (cmbReportType.Text.Contains("LIST OF APPROVED PERMIT APPLICATION"))
                form.ReportName = "List of Approved Permit Application";
            else if (cmbReportType.Text.Contains("ISSUED PERMITS SUMMARY"))
                form.ReportName = "Issued Permits Summary";
            form.ShowDialog();
        }

        private void frmSummaryOfReport_Load(object sender, EventArgs e)
        {
            dtFrom.Value = DateTime.Now;
            dtTo.Value = DateTime.Now;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbReportType_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
