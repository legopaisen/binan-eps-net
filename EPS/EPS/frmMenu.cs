using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modules.Tables;
using Common.StringUtilities;
using Common.DataConnector;
using Common.User;
using Modules.ARN;
using Modules.Utilities;
using Modules.Transactions;
using Common.AppSettings;
using Modules.Billing;

namespace EPS
{
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {
            panelLoc();

            frmLogin login = new frmLogin();
            login.ShowDialog();
            if (login.State == frmLogin.LogInState.CancelState)
            {
                this.Dispose();
            }
        }

        private void panelLoc()
        {
            if (WindowState == FormWindowState.Maximized)
                pnlButton.Location = new Point(0, 570);
            if (WindowState == FormWindowState.Normal)
                pnlButton.Location = new Point(0, 424);
        }

        private void frmMenu_Resize(object sender, EventArgs e)
        {
            panelLoc();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //Application.ExitThread();
                this.Close();
        }

        private void systemConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConfig frmconfig = new frmConfig();
            frmconfig.ShowDialog();
        }

        private void userLevelTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserlvltbl frmuserlvltbl = new frmUserlvltbl();
            frmuserlvltbl.ShowDialog();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBarangay frmbarangay = new frmBarangay();
            frmbarangay.ShowDialog();
        }

        private void structureTpeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmStructureType frmstructuretype = new frmStructureType();
            frmstructuretype.ShowDialog();
        }

        private void userLevelAccessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserlvlAccess frmuserlvlaccess = new frmUserlvlAccess();
            frmuserlvlaccess.ShowDialog();
        }

        private void engineeringRecordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TaskManager taskman = new TaskManager();
            if (!taskman.IsObjectLock("ENG_REC","",""))
            {
                frmRecords form = new frmRecords();
                form.SourceClass = "ENG_REC";
                form.ShowDialog();
            }
        }

        private void btnNewApp_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.ShowDialog();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AppSettingsManager.Granted("TNA"))
            {
                frmRecords form = new frmRecords();
                form.SourceClass = "NEW_ADD";
                form.ShowDialog();
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AppSettingsManager.Granted("TEA"))
            {
                frmRecords form = new frmRecords();
                form.SourceClass = "NEW_EDIT";
                form.ShowDialog();
            }
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AppSettingsManager.Granted("TVA"))
            {
                frmRecords form = new frmRecords();
                form.SourceClass = "NEW_VIEW";
                form.ShowDialog();
            }
        }

        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AppSettingsManager.Granted("TDA"))
            {
                frmRecords form = new frmRecords();
                form.SourceClass = "NEW_CANCEL";
                form.ShowDialog();
            }
        }

        private void addToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (AppSettingsManager.Granted("TRA"))
            {
                frmRecords form = new frmRecords();
                form.SourceClass = "REN_ADD";
                form.ShowDialog();
            }
        }

        private void buildingPermitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(AppSettingsManager.Granted("TB-B"))
            {
                frmBilling form = new frmBilling();
                form.Source = "BUILDING PERMIT";
                form.ModuleCode = "TB-UBB";
                form.ShowDialog();
            }
        }

        private void mechanicalPermitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AppSettingsManager.Granted("TB-M"))
            {
                frmBilling form = new frmBilling();
                form.Source = "MECHANICAL PERMIT";
                form.ModuleCode = "TB-UBM";
                form.ShowDialog();
            }
        }

        private void electricalPermitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AppSettingsManager.Granted("TB-E"))
            {
                frmBilling form = new frmBilling();
                form.Source = "ELECTRICAL PERMIT";
                form.ModuleCode = "TB-UBE";
                form.ShowDialog();
            }

        }

        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (AppSettingsManager.Granted("TER"))
            {
                frmRecords form = new frmRecords();
                form.SourceClass = "REN_EDIT";
                form.ShowDialog();
            }
        }

        private void viewToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (AppSettingsManager.Granted("TVR"))
            {
                frmRecords form = new frmRecords();
                form.SourceClass = "REN_VIEW";
                form.ShowDialog();
            }
        }

        private void cancelToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (AppSettingsManager.Granted("TDAR"))
            {
                frmRecords form = new frmRecords();
                form.SourceClass = "REN_CANCEL";
                form.ShowDialog();
            }
        }

        private void taskManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AppSettingsManager.Granted("SUM"))
            {
                
            }
        }
    }
}
