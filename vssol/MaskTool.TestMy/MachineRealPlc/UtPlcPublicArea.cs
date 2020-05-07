using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant;
using MvAssistant.Mac.v1_0.Hal.CompPlc;

namespace MaskTool.TestMy.MachineRealPlc
{
    [TestClass]
    public class UtPlcPublicArea
    {
        [TestMethod]
        public void TestPlcConnect()
        {

            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);


                plc.StartAsyn();

                if (!SpinWait.SpinUntil(() => plc.IsConnected, 60 * 1000))
                    throw new MvException("PLC connection fail");

                Console.WriteLine("PLC connection success");


                //while (true) System.Threading.Thread.Sleep(1000);

            }

        }

        //[TestMethod]
        //public void TestPublicArea()   //Public Area所有Function，測試OK
        //{
        //    using (var plc = new MacHalPlcContext())
        //    {
        //        plc.Connect("192.168.0.200", 2);
        //        plc.ResetAll();
        //        plc.SetSignalTower(true, false, false);
        //        plc.SetSignalTower(false, true, false);
        //        plc.SetSignalTower(false, false, true);
        //        plc.SetBuzzer(1);
        //        plc.SetBuzzer(2);
        //        plc.SetBuzzer(3);
        //        plc.SetBuzzer(4);
        //        plc.SetBuzzer(0);
        //        for (uint i = 1; i < 13; i++)
        //        {
        //            plc.CoverFanCtrl(i, (600));
        //        }
        //        Console.WriteLine(plc.ReadCoverFanSpeed());
        //        plc.EMSAlarm(true, false, false, false);
        //        Console.WriteLine(plc.ReadPowerON());
        //        Console.WriteLine(plc.ReadBCP_Maintenance());
        //        Console.WriteLine(plc.ReadCB_Maintenance());
        //        Console.WriteLine(plc.ReadBCP_EMO());
        //        Console.WriteLine(plc.ReadCB_EMO());
        //        Console.WriteLine(plc.ReadLP1_EMO());
        //        Console.WriteLine(plc.ReadLP2_EMO());
        //        Console.WriteLine(plc.ReadBCP_Door());
        //        Console.WriteLine(plc.ReadLP1_Door());
        //        Console.WriteLine(plc.ReadLP2_Door());
        //        Console.WriteLine(plc.ReadBCP_Smoke());
        //        Console.WriteLine(plc.ReadLP_Light_Curtain());
        //    }
        //}

        [TestMethod]
        public void TestSignalTower()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                plc.SetSignalTower(true, false, false);
                plc.SetSignalTower(false, true, false);
                plc.SetSignalTower(false, false, true);
            }
        }

        [TestMethod]
        public void TestBuzzer()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                plc.SetBuzzer(1);
                plc.SetBuzzer(2);
                plc.SetBuzzer(3);
                plc.SetBuzzer(4);
                plc.SetBuzzer(0);
            }
        }

        [TestMethod]
        public void TestOuterCoverFan()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                for (uint i = 1; i < 13; i++)
                {
                    plc.CoverFanCtrl(i, (600 + i * 50));
                }
                Console.WriteLine(plc.ReadCoverFanSpeed());
            }
        }

        [TestMethod]
        public void TestEMSSignal()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                plc.EMSAlarm(true, false, false, false);
                plc.EMSAlarm(false, true, false, false);
                plc.EMSAlarm(false, false, true, false);
                plc.EMSAlarm(false, false, false, true);
            }
        }

        [TestMethod]
        public void TestPLCSignal()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                Console.WriteLine(plc.ReadPowerON());
                Console.WriteLine(plc.ReadBCP_Maintenance());
                Console.WriteLine(plc.ReadCB_Maintenance());
                Console.WriteLine(plc.ReadBCP_EMO());
                Console.WriteLine(plc.ReadCB_EMO());
                Console.WriteLine(plc.ReadLP1_EMO());
                Console.WriteLine(plc.ReadLP2_EMO());
                Console.WriteLine(plc.ReadBCP_Door());
                Console.WriteLine(plc.ReadLP1_Door());
                Console.WriteLine(plc.ReadLP2_Door());
                Console.WriteLine(plc.ReadBCP_Smoke());
                Console.WriteLine(plc.ReadLP_Light_Curtain());
            }
        }

        [TestMethod]
        public void TestResetAll()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                plc.ResetAll();
            }
        }

        [TestMethod]
        public void TestPLCClosePort()//當發生PLC連線占用Port，嘗試關閉Port解決問題
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                plc.ClosePort();
            }
        }
    }
}
