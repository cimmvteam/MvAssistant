using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaskTool.TestMy.MachineReal;

namespace BoxTransferTest.ViewUc
{
    public partial class UcOpenStage : UserControl
    {
        UtPlc plc = new UtPlc();
        public UcOpenStage()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            plc.TestPlcOpenStageFlow();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            plc.boolTestStop = true;
        }

    }
}
