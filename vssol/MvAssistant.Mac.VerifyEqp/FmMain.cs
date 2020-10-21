using MaskCleanerVerify;
using System;
using System.Windows.Forms;

namespace MvAssistantMacVerifyEqp
{
    public partial class FmMain : Form
    {
        public FmMain()
        {
            InitializeComponent();
        }

        private void tsmiSgsTest_Click(object sender, EventArgs e)
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
            var fm = new FmBoxRobot();
            fm.MdiParent = this;
            fm.WindowState = FormWindowState.Maximized;
            fm.Show();
        }

    

        private void robotPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fm = new FmRobotPath();
            fm.MdiParent = this;
            this.WindowState = FormWindowState.Maximized;
            fm.Show();
        }

        private void tsmiVerifyEQP_Click(object sender, EventArgs e)
        {
            var fm = new FmVerifyEqp();
            fm.MdiParent = this;
            this.WindowState = FormWindowState.Maximized;
            fm.Show();

        }
    }
}
