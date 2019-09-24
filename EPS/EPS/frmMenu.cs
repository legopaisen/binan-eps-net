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
using Modules.Utilities.Forms;


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
            if (AppSettingsManager.Granted("SS"))
            {
                frmConfig frmconfig = new frmConfig();
                frmconfig.ShowDialog();
            }
        }

        private void userLevelTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AppSettingsManager.Granted("SC"))
            {
                frmUserlvltbl frmuserlvltbl = new frmUserlvltbl();
                frmuserlvltbl.ShowDialog();
            }
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AppSettingsManager.Granted("STB"))
            {
                frmBarangay frmbarangay = new frmBarangay();
                frmbarangay.ShowDialog();
            }
        }

        private void structureTpeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AppSettingsManager.Granted("SST"))
            {
                frmStructureType frmstructuretype = new frmStructureType();
                frmstructuretype.ShowDialog();
            }
        }

        private void userLevelAccessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AppSettingsManager.Granted("SSS"))
            {
                frmUserlvlAccess frmuserlvlaccess = new frmUserlvlAccess();
                frmuserlvlaccess.ShowDialog();
            }
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
            taskman.IsObjectLock("ENG_REC", "DELETE", "");
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
                TaskManager taskman = new TaskManager();
                if (!taskman.IsObjectLock("BILLING", "", ""))
                {
                    frmBilling form = new frmBilling();
                    form.Source = "BUILDING PERMIT";
                    form.ModuleCode = "TB-UBB";
                    form.ShowDialog();

                    taskman.IsObjectLock("BILLING", "DELETE", "");
                }
            }
        }

        private void mechanicalPermitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AppSettingsManager.Granted("TB-M"))
            {
                TaskManager taskman = new TaskManager();
                if (!taskman.IsObjectLock("BILLING", "", ""))
                {
                    frmBilling form = new frmBilling();
                    form.Source = "MECHANICAL PERMIT";
                    form.ModuleCode = "TB-UBM";
                    form.ShowDialog();

                    taskman.IsObjectLock("BILLING", "DELETE", "");
                }
            }
        }

        private void electricalPermitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AppSettingsManager.Granted("TB-E"))
            {
                TaskManager taskman = new TaskManager();
                if (!taskman.IsObjectLock("BILLING", "", ""))
                {
                    frmBilling form = new frmBilling();
                    form.Source = "ELECTRICAL PERMIT";
                    form.ModuleCode = "TB-UBE";
                    form.ShowDialog();

                    taskman.IsObjectLock("BILLING", "DELETE", "");
                }
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
                frmTaskManager form = new frmTaskManager();
                form.ShowDialog();   
            }
        }

        private void postingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AppSettingsManager.Granted("P"))
            {
                TaskManager taskman = new TaskManager();
                if (!taskman.IsObjectLock("POSTING", "", ""))
                {
                    frmPosting form = new frmPosting();
                    form.ShowDialog();

                    taskman.IsObjectLock("POSTING", "DELETE", "");
                }
                
            }
        }

        private void cancelPostedPaymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(AppSettingsManager.Granted("PCP"))
            {
                TaskManager taskman = new TaskManager();
                if (!taskman.IsObjectLock("CANCEL PAYMENT", "", ""))
                {
                    frmCancelPayment form = new frmCancelPayment();
                    form.ShowDialog();

                    taskman.IsObjectLock("CANCEL PAYMENT", "DELETE", "");
                }
            }   
        }

        private void occupancyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AppSettingsManager.Granted("TB-O"))
            {
                TaskManager taskman = new TaskManager();
                if (!taskman.IsObjectLock("BILLING", "", ""))
                {
                    frmBilling form = new frmBilling();
                    form.Source = "CERTIFICATE OF OCCUPANCY";
                    form.ModuleCode = "TB-UBO";
                    form.ShowDialog();

                    taskman.IsObjectLock("BILLING", "DELETE", "");
                }
            }
        }

        private void zoningPermitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void annualInspectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AppSettingsManager.Granted("TB-AI"))
            {
                TaskManager taskman = new TaskManager();
                if (!taskman.IsObjectLock("BILLING", "", ""))
                {
                    frmBilling form = new frmBilling();
                    form.Source = "CERTIFICATE OF ANNUAL INSPECTION";
                    form.ModuleCode = "TB-UBAI";
                    form.ShowDialog();

                    taskman.IsObjectLock("BILLING", "DELETE", "");
                }
            }
        }

        private void permitNoadjustmentToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void auditTrailToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void connectivityToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void categoryToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void occupancyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if(AppSettingsManager.Granted("SOC"))
            {
                TaskManager taskman = new TaskManager();
                if (!taskman.IsObjectLock("OCCUPANCY", "", ""))
                {
                    frmOccupancy form = new frmOccupancy();
                    form.ShowDialog();
                    taskman.IsObjectLock("OCCUPANCY", "DELETE", "");
                }
            }
        }

        private void scheduleOfFeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AppSettingsManager.Granted("STS"))
            {
                TaskManager taskman = new TaskManager();
                if (!taskman.IsObjectLock("FEES", "", ""))
                {
                    frmScheduleFees form = new frmScheduleFees();
                    form.ShowDialog();
                    taskman.IsObjectLock("FEES", "DELETE", "");
                }
            }
        }

        private void scopeOfWorkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(AppSettingsManager.Granted("SSC"))
            {
                TaskManager taskman = new TaskManager();
                if (!taskman.IsObjectLock("SCOPE", "", ""))
                {
                    frmScope form = new frmScope();
                    form.ShowDialog();
                    taskman.IsObjectLock("SCOPE", "DELETE", "");
                }
            }
        }

        private void typesOfMaterialsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AppSettingsManager.Granted("SMT"))
            {
                TaskManager taskman = new TaskManager();
                if (!taskman.IsObjectLock("MATERIAL", "", ""))
                {
                    frmMaterial form = new frmMaterial();
                    form.ShowDialog();
                    taskman.IsObjectLock("MATERIAL", "DELETE", "");
                }
            }
        }

        private void lotOwnerApplicantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AppSettingsManager.Granted("SLS"))
            {
                TaskManager taskman = new TaskManager();
                if (!taskman.IsObjectLock("LOT OWNER", "", ""))
                {
                    frmOwner form = new frmOwner();
                    form.SourceClass = "OWNER";
                    form.ShowDialog();
                    taskman.IsObjectLock("LOT OWNER", "DELETE", "");
                }
            }
        }

        private void engineersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AppSettingsManager.Granted("SE"))
            {
                TaskManager taskman = new TaskManager();
                if (!taskman.IsObjectLock("ENGINEER", "", ""))
                {
                    frmOwner form = new frmOwner();
                    form.SourceClass = "ENGR";
                    form.ShowDialog();
                    taskman.IsObjectLock("ENGINEER", "DELETE", "");
                }
            }
        }
<<<<<<< .mine

=======

>>>>>>> .theirs
        private void permitTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AppSettingsManager.Granted("SPT"))
            {
                TaskManager taskman = new TaskManager();
                if (!taskman.IsObjectLock("PERMIT TYPE", "", ""))
                {
                    frmPermitType form = new frmPermitType();
                    form.ShowDialog();
                    taskman.IsObjectLock("PERMIT TYPE", "DELETE", "");
                }
            }
        }
		private void btnLO_Click(object sender, EventArgs e)
        {

        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

		private void permitBillingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AppSettingsManager.Granted("TB-B"))
            {
                TaskManager taskman = new TaskManager();
                if (!taskman.IsObjectLock("BILLING", "", ""))
                {
                    frmBilling form = new frmBilling();
                    form.Source = "BUILDING PERMIT";
                    form.ModuleCode = "TB-UBB";
                    form.ShowDialog();

                    taskman.IsObjectLock("BILLING", "DELETE", "");
                }
            }
        }
		
		private void assessmentOfFeesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void reassessmentOfPermitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

    }
}
