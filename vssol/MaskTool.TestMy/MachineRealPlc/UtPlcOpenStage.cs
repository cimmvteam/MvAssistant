using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant;
using MvAssistant.Mac.v1_0.Hal.CompPlc;

namespace MaskTool.TestMy.MachineRealPlc
{
    [TestClass]
    public class UtPlcOpenStage
    {
        //[TestMethod]
        //public void TestPlcOpenStage()
        //{
        //    using (var plc = new MacHalPlcContext())
        //    {
        //        plc.Connect("192.168.0.200", 2);

        //        //Console.WriteLine(plc.OpenStage.Open());
        //        //Console.WriteLine(plc.OpenStage.Close());
        //        //Console.WriteLine(plc.OpenStage.Clamp());
        //        //Console.WriteLine(plc.OpenStage.Unclamp());
        //        //Console.WriteLine(plc.OpenStage.SortClamp());
        //        //Console.WriteLine(plc.OpenStage.SortUnclamp());
        //        //Console.WriteLine(plc.OpenStage.Lock());
        //        //Console.WriteLine(plc.OpenStage.Initial());
        //        plc.OpenStage.SetBoxType(0);
        //        plc.OpenStage.SetSpeed(50);
        //        Console.WriteLine(plc.OpenStage.ReadBoxTypeSetting());
        //        Console.WriteLine(plc.OpenStage.ReadRobotIntrude(false, false));
        //        Console.WriteLine(plc.OpenStage.ReadClampStatus());
        //        Console.WriteLine(plc.OpenStage.ReadSortClampPosition());
        //        Console.WriteLine(plc.OpenStage.ReadSliderPosition());
        //        Console.WriteLine(plc.OpenStage.ReadCoverPos());
        //        Console.WriteLine(plc.OpenStage.ReadCoverSensor());
        //        Console.WriteLine(plc.OpenStage.ReadBoxDeform());
        //        Console.WriteLine(plc.OpenStage.ReadWeightOnStage());
        //        Console.WriteLine(plc.OpenStage.ReadBoxExist());
        //        Console.WriteLine(plc.OpenStage.ReadOpenStageStatus());
        //    }
        //}

        //[TestMethod]
        //public void TestPlcOpenStageFlow()
        //{
        //    using (var plc = new MacHalPlcContext())
        //    {
        //        plc.Connect("192.168.0.200", 2);
        //        plc.OpenStage.SetBoxType(1);//鐵盒：1，水晶盒：2
        //        Console.WriteLine(plc.OpenStage.Initial());
        //        //for (int i = 0; i < 1; i++)
        //        while (true)
        //        {
        //            Console.WriteLine(plc.OpenStage.SortClamp());
        //            Console.WriteLine(plc.OpenStage.SortUnclamp());
        //            Console.WriteLine(plc.OpenStage.Close());
        //            Console.WriteLine(plc.OpenStage.Clamp());
        //            Console.WriteLine(plc.OpenStage.Open());
        //            Console.WriteLine(plc.OpenStage.ReadRobotIntrude(false, true));//Mask Robot入侵將MTIntrude訊號改為False
        //            Console.WriteLine(plc.OpenStage.ReadRobotIntrude(false, false));//沒有Robot入侵時，將訊號改為True
        //            Console.WriteLine(plc.OpenStage.Close());
        //            Console.WriteLine(plc.OpenStage.Unclamp());
        //            Console.WriteLine(plc.OpenStage.Lock());
        //        }
        //        //Console.WriteLine(plc.OpenStage.ReadClampStatus());
        //        //Console.WriteLine(plc.OpenStage.ReadSortClampPosition());
        //        //Console.WriteLine(plc.OpenStage.ReadSliderPosition());
        //        //Console.WriteLine(plc.OpenStage.ReadCoverPos());
        //        //Console.WriteLine(plc.OpenStage.ReadCoverSensor());
        //        //Console.WriteLine(plc.OpenStage.ReadBoxExist());
        //    }
        //}
        [TestMethod]
        public void TestPlcVariableSetting()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                plc.OpenStage.SetBoxType(0);
                plc.OpenStage.SetSpeed(50);
            }
        }

        [TestMethod]
        public void TestPlcReadSetting()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                Console.WriteLine(plc.OpenStage.ReadBoxTypeSetting());
                Console.WriteLine(plc.OpenStage.ReadSpeedSetting());
            }
        }

        [TestMethod]
        public void TestPlcHardwareStatus()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                Console.WriteLine(plc.OpenStage.ReadClampStatus());
                Console.WriteLine(plc.OpenStage.ReadSortClampPosition());
                Console.WriteLine(plc.OpenStage.ReadSliderPosition());
                Console.WriteLine(plc.OpenStage.ReadCoverPos());
                Console.WriteLine(plc.OpenStage.ReadCoverSensor());
                Console.WriteLine(plc.OpenStage.ReadBoxDeform());
                Console.WriteLine(plc.OpenStage.ReadWeightOnStage());
                Console.WriteLine(plc.OpenStage.ReadBoxExist());
                Console.WriteLine(plc.OpenStage.ReadBeenIntruded());
            }
        }

        [TestMethod]
        public void TestPlcComponentStatus()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                Console.WriteLine(plc.OpenStage.ReadOpenStageStatus());
            }
        }

        [TestMethod]
        public void TestPlcHardwareAction()
        {
            using (var plc = new MacHalPlcContext())
            {
                plc.Connect("192.168.0.200", 2);
                plc.OpenStage.SetBoxType(1);//鐵盒：1，水晶盒：2
                Console.WriteLine(plc.OpenStage.Initial());
                for (int i = 0; i < 1; i++)
                //while (true)
                {
                    Console.WriteLine(plc.OpenStage.SortClamp());
                    Console.WriteLine(plc.OpenStage.Vacuum(true));
                    Console.WriteLine(plc.OpenStage.SortUnclamp());
                    Console.WriteLine(plc.OpenStage.Close());
                    Console.WriteLine(plc.OpenStage.Clamp());
                    Console.WriteLine(plc.OpenStage.Open());
                    Console.WriteLine(plc.OpenStage.ReadRobotIntrude(false, true));//Mask Robot入侵將MTIntrude訊號改為False
                    Console.WriteLine(plc.OpenStage.ReadRobotIntrude(false, false));//沒有Robot入侵時，將訊號改為True
                    Console.WriteLine(plc.OpenStage.Close());
                    Console.WriteLine(plc.OpenStage.Unclamp());
                    Console.WriteLine(plc.OpenStage.Lock());
                    Console.WriteLine(plc.OpenStage.Vacuum(false));
                }
            }
        }
    }
}
