using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MvAssistant.v0_2.Mac.Hal.CompPlc;

namespace MvAssistantMacVerifyEqp.ViewUc
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

                boolTestStop = false;
                try
                {
                    plc.Connect("192.168.0.200", 2);
                    //var Insp = new UtPlc();
                    //var InspRun = Task.Run(() => { Insp.TestPlcInspChFlow(); });


                    var TaskInsp = Task.Run(() =>
                      {
                          try
                          {
                              Console.WriteLine(plc.InspCh.ReadRobotIntrude(false));
                              plc.InspCh.SetSpeed(100, 50, 500);
                              Console.WriteLine(plc.InspCh.Initial());

                              while (boolTestStop == false)
                              {
                                  Console.WriteLine(plc.InspCh.XYPosition(200, 10));//X:300~-10,Y:250~-10  左下
                                  if (boolTestStop) break;
                                  Console.WriteLine(plc.InspCh.WPosition(52));//0~359
                                  if (boolTestStop) break;
                                  Console.WriteLine(plc.InspCh.XYPosition(10, 10));//X:300~-10,Y:250~-10  右下
                                  if (boolTestStop) break;
                                  Console.WriteLine(plc.InspCh.XYPosition(10, 150));//X:300~-10,Y:250~-10  右上
                                  if (boolTestStop) break;
                                  Console.WriteLine(plc.InspCh.XYPosition(200, 150));//X:300~-10,Y:250~-10  左上
                                  if (boolTestStop) break;
                                  Console.WriteLine(plc.InspCh.ZPosition(-10));//1~-85
                                  Console.WriteLine(plc.InspCh.ZPosition(-50));//1~-85
                                  if (boolTestStop) break;
                              }
                          }
                          catch (Exception ex)
                          {
                              throw ex; ;
                          }

                      });
                    var TaskOpenStage = Task.Run(() =>
                    {
                        try
                        {

                            plc.OpenStage.SetBoxType(1);//鐵盒：1，水晶盒：2
                            Console.WriteLine(plc.OpenStage.ReadRobotIntrude(false, false));//沒有Robot入侵時，將訊號改為True
                            Console.WriteLine(plc.OpenStage.Initial());
                            //for (int i = 0; i < 1; i++)
                            while (boolTestStop == false)
                            {
                                Console.WriteLine(plc.OpenStage.SortClamp());
                                Console.WriteLine(plc.OpenStage.SortUnclamp());
                                Console.WriteLine(plc.OpenStage.Close());
                                Console.WriteLine(plc.OpenStage.Clamp());
                                Console.WriteLine(plc.OpenStage.Open());
                                Console.WriteLine(plc.OpenStage.ReadRobotIntrude(true, false));//Mask Robot入侵將MTIntrude訊號改為False
                                Console.WriteLine(plc.OpenStage.ReadRobotIntrude(false, false));//沒有Robot入侵時，將訊號改為True
                                Console.WriteLine(plc.OpenStage.Close());
                                Console.WriteLine(plc.OpenStage.Unclamp());
                                Console.WriteLine(plc.OpenStage.Lock());
                                if (boolTestStop) break;
                            }
                        }
                        catch (Exception ex) { throw ex; ; }

                });

                //Task.WaitAll(new Task[] { TaskOpenStage, TaskInsp }, 180 * 1000);

            }
                catch (Exception ex)
                {
                    boolTestStop = false;
                    throw ex;
                }


            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            boolTestStop = true;
        }

    }
}
