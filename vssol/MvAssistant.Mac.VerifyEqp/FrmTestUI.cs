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

        private void button1_Click(object sender, EventArgs e)
        {
            drawers.DrawerA.CommandTrayMotionHome();
        }
    }


    
}
