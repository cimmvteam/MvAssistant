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
        TestLoadPorts loadPorts;
        public FrmTestUI()
        {
            InitializeComponent();
        }



        private void FrmTestUI_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            drawers = new TestDrawers(this);
            loadPorts = new TestLoadPorts(this);
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
            if (sender != null) { drawers.countDrawerBox = false; }
            chkBoxDrawerAHasbox.Checked = false;
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
            if (sender != null) { drawers.countDrawerBox = false; }
            chkBoxDrawerBHasbox.Checked = false;
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
            if (sender != null) { drawers.countDrawerBox = false; }
            chkBoxDrawerCHasbox.Checked = false;
            drawers.InitialDRawer(drawers.DrawerC);
            drawers.DisableDrawerComps(drawers.DrawerC);
            drawers.DrawerC.CommandBoxDetection();
        }

        private void btnInitialDrawerD_Click(object sender, EventArgs e)
        {
            drawers.InitialDRawer(drawers.DrawerD);
            drawers.DisableDrawerComps(drawers.DrawerD);
            drawers.DrawerD.CommandINI();
        }

        private void btnMoveDrawerDHome_Click(object sender, EventArgs e)
        {
            drawers.InitialDRawer(drawers.DrawerD);
            drawers.DisableDrawerComps(drawers.DrawerD);
            drawers.DrawerD.CommandTrayMotionHome();
        }

        private void txtBxDetectDrawerD_Click(object sender, EventArgs e)
        {
            if (sender != null) { drawers.countDrawerBox = false; }
            chkBoxDrawerDHasbox.Checked = false;
            drawers.InitialDRawer(drawers.DrawerD);
            drawers.DisableDrawerComps(drawers.DrawerD);
            drawers.DrawerD.CommandBoxDetection();
        }

        private void btnInitialAllDrawer_Click(object sender, EventArgs e)
        {
            this.btnInitialDrawerA_Click(null, null);
            this.btnInitialDrawerB_Click(null, null);
            this.btnInitialDrawerC_Click(null, null);
            this.btnInitialDrawerD_Click(null, null);
        }

        private void btnMoveAllDrawersHome_Click(object sender, EventArgs e)
        {
            this.btnMoveDrawerAHome_Click(null, null);
            this.btnMoveDrawerBHome_Click(null, null);
            this.btnMoveDrawerCHome_Click(null, null);
            this.btnMoveDrawerDHome_Click(null, null);
        }

        private void btnDetectAllDrawers_Click(object sender, EventArgs e)
        {
            txtDrawerBoxNum.Text = "0";
            drawers.countDrawerBox = true;
            this.txtBxDetectDrawerA_Click(null, null);
            this.txtBxDetectDrawerB_Click(null, null);
            this.txtBxDetectDrawerC_Click(null, null);
            this.txtBxDetectDrawerD_Click(null, null);
        }

        private void BtnReleaseAllComp_Click(object sender, EventArgs e)
        {
            this.grpDrawerAComp.Enabled = this.grpDrawerBComp.Enabled = this.grpDrawerCComp.Enabled = this.grpDrawerDComp.Enabled = true;
        }

        private void btnLoadPortBAlarmReset_Click(object sender, EventArgs e)
        {
            this.loadPorts.ResetResult(loadPorts.LoadPort2);
            this.loadPorts.DisableLoadportOperate(loadPorts.LoadPort2);
            loadPorts.LoadPort2.CommandAlarmReset();
            //this.loadPorts.ResetResult(loadPorts.LoadPort2);
            //this.loadPorts.DisableLoadportOperate(loadPorts.LoadPort2);
        }

        private void btnInitialLoadportB_Click(object sender, EventArgs e)
        {
            this.loadPorts.ResetResult(loadPorts.LoadPort2);
            this.loadPorts.DisableLoadportOperate(loadPorts.LoadPort2);
            loadPorts.LoadPort2.CommandInitialRequest();
        }

        private void btnLoadportBBarcode_Click(object sender, EventArgs e)
        {
            this.loadPorts.ResetResult(loadPorts.LoadPort2);
            this.loadPorts.DisableLoadportOperate(loadPorts.LoadPort2);
            loadPorts.LoadPort2.CommandAskBarcodeStatus();
        }

        private void btnLoadportBRFID_Click(object sender, EventArgs e)
        {
            this.loadPorts.ResetResult(loadPorts.LoadPort2);
            this.loadPorts.DisableLoadportOperate(loadPorts.LoadPort2);
            loadPorts.LoadPort2.CommandAskRFIDStatus();
        }

        private void btnLoadportABarcode_Click(object sender, EventArgs e)
        {
            this.loadPorts.ResetResult(loadPorts.LoadPort1);
            this.loadPorts.DisableLoadportOperate(loadPorts.LoadPort1);
            loadPorts.LoadPort1.CommandAskBarcodeStatus();
        }

        private void btnLoadportARFID_Click(object sender, EventArgs e)
        {
            this.loadPorts.ResetResult(loadPorts.LoadPort1);
            this.loadPorts.DisableLoadportOperate(loadPorts.LoadPort1);
            loadPorts.LoadPort1.CommandAskRFIDStatus();
        }

        private void btnLoadPortAAlarmReset_Click(object sender, EventArgs e)
        {

            this.loadPorts.ResetResult(loadPorts.LoadPort1);
            this.loadPorts.DisableLoadportOperate(loadPorts.LoadPort1);
            loadPorts.LoadPort1.CommandAlarmReset();
        }

        private void btnInitialLoadportA_Click(object sender, EventArgs e)
        {
            this.loadPorts.ResetResult(loadPorts.LoadPort1);
            this.loadPorts.DisableLoadportOperate(loadPorts.LoadPort1);
            loadPorts.LoadPort1.CommandInitialRequest();
        }
    }


    
}
