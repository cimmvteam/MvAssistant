using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MvAssistantMacVerifyEqp
{
    public partial class FrmTestUI : Form
    {

        TestDrawers drawers;
        public FrmTestUI()
        {
            InitializeComponent();
        }



        private void FrmTestUI_Load(object sender, EventArgs e)
        {
            drawers = new TestDrawers(this);
        }

        private void btnInitialDrawerA_Click(object sender, EventArgs e)
        {
            drawers.InitialDRawer(drawers.DrawerA);
            drawers.DisableDrawerComps(drawers.DrawerA);
            drawers.DrawerA.CommandINI();
        }

        private void GrpDrawerA_Enter(object sender, EventArgs e)
        {

        }

        private void btnMoveDrawerAHome_Click(object sender, EventArgs e)
        {
            drawers.InitialDRawer(drawers.DrawerA);
            drawers.DisableDrawerComps(drawers.DrawerA);
            drawers.DrawerA.CommandTrayMotionHome();
        }

        private void txtBxDetectDrawerA_Click(object sender, EventArgs e)
        {
            drawers.InitialDRawer(drawers.DrawerA);
            drawers.DisableDrawerComps(drawers.DrawerA);
            drawers.DrawerA.CommandBoxDetection();
        }

        private void btnInitialDrawerB_Click(object sender, EventArgs e)
        {
            drawers.InitialDRawer(drawers.DrawerB);
            drawers.DisableDrawerComps(drawers.DrawerB);
            drawers.DrawerB.CommandINI();
        }

        private void btnMoveDrawerBHome_Click(object sender, EventArgs e)
        {
            drawers.InitialDRawer(drawers.DrawerB);
            drawers.DisableDrawerComps(drawers.DrawerB);
            drawers.DrawerB.CommandTrayMotionHome();
        }

        private void txtBxDetectDrawerB_Click(object sender, EventArgs e)
        {
            drawers.InitialDRawer(drawers.DrawerB);
            drawers.DisableDrawerComps(drawers.DrawerB);
            drawers.DrawerB.CommandBoxDetection();
        }

        private void btnInitialDrawerC_Click(object sender, EventArgs e)
        {
            drawers.InitialDRawer(drawers.DrawerC);
            drawers.DisableDrawerComps(drawers.DrawerC);
            drawers.DrawerC.CommandINI();
        }

        private void btnMoveDrawerCHome_Click(object sender, EventArgs e)
        {
            drawers.InitialDRawer(drawers.DrawerC);
            drawers.DisableDrawerComps(drawers.DrawerC);
            drawers.DrawerC.CommandTrayMotionHome();
        }

        private void txtBxDetectDrawerC_Click(object sender, EventArgs e)
        {
            drawers.InitialDRawer(drawers.DrawerC);
            drawers.DisableDrawerComps(drawers.DrawerC);
            drawers.DrawerC.CommandBoxDetection();
        }

        private void btnInitialDrawerD_Click(object sender, EventArgs e)
        {
            drawers.InitialDRawer(drawers.DrawerD);
            drawers.DisableDrawerComps(drawers.DrawerD);
            drawers.DrawerC.CommandINI();
        }

        private void btnMoveDrawerDHome_Click(object sender, EventArgs e)
        {
            drawers.InitialDRawer(drawers.DrawerD);
            drawers.DisableDrawerComps(drawers.DrawerD);
            drawers.DrawerD.CommandTrayMotionHome();
        }

        private void txtBxDetectDrawerD_Click(object sender, EventArgs e)
        {
            drawers.InitialDRawer(drawers.DrawerD);
            drawers.DisableDrawerComps(drawers.DrawerD);
            drawers.DrawerD.CommandBoxDetection();
        }
    }


    
}
