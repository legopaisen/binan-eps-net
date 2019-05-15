using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        }

        private void panelLoc()
        {
            if (WindowState == FormWindowState.Maximized)
            {
                pnlButton.Location = new Point(0, 570);
            }
            if (WindowState == FormWindowState.Normal)
            {
                pnlButton.Location = new Point(0, 303);
            }
        }

        private void frmMenu_Resize(object sender, EventArgs e)
        {
            panelLoc();
        }
    }
}
