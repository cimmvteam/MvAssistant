using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaskTool.TestMy.MachineRealPlc;
using MvAssistant.Mac.v1_0.Hal.CompPlc;

namespace BoxTransferTest.ViewUc
{
    public partial class UcOpenStage : UserControl
    {
        bool boolTestStop = false;
        public UcOpenStage()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                boolTestStop = false;
                plc.OpenStage.SetBoxType(1);//鐵盒：1，水晶盒：2
                Console.WriteLine(plc.OpenStage.ReadRobotIntrude(false, false));//沒有Robot入侵時，將訊號改為True
                Console.WriteLine(plc.OpenStage.Initial());
                //for (int i = 0; i < 1; i++)
                while (boolTestStop == false)
                {
                    Console.WriteLine(plc.OpenStage.SortClamp());
                    if (boolTestStop) break;
                    Console.WriteLine(plc.OpenStage.SortUnclamp());
                    if (boolTestStop) break;
                    Console.WriteLine(plc.OpenStage.Close());
                    if (boolTestStop) break;
                    Console.WriteLine(plc.OpenStage.Clamp());
                    if (boolTestStop) break;
                    Console.WriteLine(plc.OpenStage.Open());
                    if (boolTestStop) break;
                    Console.WriteLine(plc.OpenStage.ReadRobotIntrude(true, false));//Mask Robot入侵將MTIntrude訊號改為False
                    Console.WriteLine(plc.OpenStage.ReadRobotIntrude(false, false));//沒有Robot入侵時，將訊號改為True
                    if (boolTestStop) break;
                    Console.WriteLine(plc.OpenStage.Close());
                    if (boolTestStop) break;
                    Console.WriteLine(plc.OpenStage.Unclamp());
                    if (boolTestStop) break;
                    Console.WriteLine(plc.OpenStage.Lock());
                }
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            boolTestStop = true;
        }

    }
}
