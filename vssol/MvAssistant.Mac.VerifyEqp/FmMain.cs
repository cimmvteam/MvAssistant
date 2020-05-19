using MaskCleanerVerify;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoxTransferTest
{
    public partial class FmMain : Form
    {
        public FmMain()
        {
            InitializeComponent();
        }

        private void tsmiMaskRobot_Click(object sender, EventArgs e)
        {
            var fm = new FmSgsTest();
            fm.MdiParent = this;
            fm.Show();

        }

        private void FmMain_Load(object sender, EventArgs e)
        {
            var fm = new FmSgsTest();
            fm.MdiParent = this;
            fm.WindowState = FormWindowState.Maximized;
            fm.Show();

        }

        private void tsmiBoxRobot_Click(object sender, EventArgs e)
        {
            var fm = new Form1();
            fm.MdiParent = this;
            fm.Show();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void robotPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fm = new FmRobotPath();
            fm.MdiParent = this;
            fm.Show();
        }
    }
}
