using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.DeviceDrive.LeimacLight;

namespace MvAssistant.Mac.TestMy.Device
{
    [TestClass]
    public class UtDeviceLight
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Clean Ch./Light IWDV-100S-24
            var are = new AutoResetEvent(false);
            using (var light = new MvLeimacLightLdd())
            {

                light.TcpClient.EhDataReceive += (ss, ee) =>
                {
                    System.Diagnostics.Debug.WriteLine("Message:" + ee.Message);
                    System.Diagnostics.Debug.WriteLine("Data:" + ee.TrxMessage.GetString());
                    are.Set();
                };


                //LP
                //light.Model = MvEnumLeimacModel.IDGB_50M4PG_24_TP;
                //light.ConnectIfNo("192.168.0.119", 1000);

                //CL
                //light.Model = MvEnumLeimacModel.IWDV_100S_24;
                //light.ConnectIfNo("192.168.0.129", 1000);

                //OS
                //light.Model = MvEnumLeimacModel.IDGB_50M4PG_24_TP;
                //light.ConnectIfNo("192.168.0.139", 1000);

                //RB
                //light.Model = MvEnumLeimacModel.IDGB_50M4PG_24_TP;
                //light.ConnectIfNo("192.168.0.155", 1000);

                //IC1: ch1: Top Defense 環形光 ; ch2: Side Inspection 條形光 ; ch3: Side Defense 條形光 ; ch4: No install
                light.Model = MvEnumLeimacModel.IDGB_50M4PG_24_TP;
                light.ConnectIfNo("192.168.0.160", 1000);

                //IC2: ch1: Top Inspcetion 環形光 ; ch2: No install
                //light.Model = MvEnumLeimacModel.IDGB_50M2PG_12_TP;
                //light.ConnectIfNo("192.168.0.161", 1000);

                //IC3: ch1: Left Spot Light ; ch2: Right Spot Light
                //light.Model = MvEnumLeimacModel.IWDV_600M2_24;
                //light.ConnectIfNo("192.168.0.162", 1000);



                light.SetValue(1, 0);

                Thread.Sleep(1000);
                are.WaitOne();
            }


        }
    }
}
