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

namespace Modules.Utilities.Forms
{
    public partial class frmTaskManager : Form
    {
        private string m_sModCode = string.Empty;
        private string m_sObject = string.Empty;
        private string m_sUser = string.Empty;
        
        public frmTaskManager()
        {
            InitializeComponent();
        }

        private void frmTaskManager_Load(object sender, EventArgs e)
        {
            UpdateList();
        }

        private void UpdateList()
        {
            TaskManager taskman = new TaskManager();

            dgvList.Columns.Clear();
            dgvList.DataSource = null;
            dgvList.DataSource = taskman.GetRecords();
            dgvList.Columns[0].Width = 100;
            dgvList.Columns[1].Width = 100;
            dgvList.Columns[2].Width = 150;
            dgvList.Columns[3].Width = 200;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            UpdateList();
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            m_sModCode = string.Empty;
            m_sObject = string.Empty;
            m_sUser = string.Empty;
            
            try
            {
                m_sModCode = dgvList[0, e.RowIndex].Value.ToString();
            }
            catch { }
            try
            {
                m_sObject = dgvList[1, e.RowIndex].Value.ToString();
            }
            catch { }

            try
            {
                m_sUser = dgvList[2, e.RowIndex].Value.ToString();
            }
            catch { }

            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(m_sModCode))
            {
                if (MessageBox.Show("Remove selected task?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    TaskManager taskman = new TaskManager();
                    taskman.RemTask(m_sObject, m_sModCode, m_sUser);
                    UpdateList();
                }
            }
            else
            {
                MessageBox.Show("Select task to remove first","Task Manager",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                return;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
