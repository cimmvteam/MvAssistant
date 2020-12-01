using System;
using MaskAutoCleaner.v1_0.Machine.Cabinet;
using MaskAutoCleaner.v1_0.Machine.Cabinet.DrawerQueues;
using MaskAutoCleaner.v1_0.Machine.Cabinet.DrawerStatus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.v1_0;

namespace MaskAutoCleaner.v1_0.TestMy.Machine
{
    [TestClass]
    public class UtQueue
    {
        [TestMethod]
        public void TestMethod1()
        {
            DrawerForBankOutQue que = DrawerForBankOutQue.GetInstance();
          
                string message1;
               var result1= que.EnqueueUnique(
                    new DrawerSatusInfo("A12345", EnumMachineID.MID_DRAWER_01_01, BoxType.CrystalBox,DrawerDuration.BankOut_Load_TrayAtHomeNoBox),out message1
                    );
                string message2;
              var result2=  que.EnqueueUnique(
                     new DrawerSatusInfo("B12345", EnumMachineID.MID_DRAWER_02_01, BoxType.CrystalBox, DrawerDuration.BankOut_Load_TrayAtHomeNoBox),out message2
                    );
                string message3;
              var result3=  que.EnqueueUnique(
                     new DrawerSatusInfo("C12345", EnumMachineID.MID_DRAWER_02_01, BoxType.CrystalBox, DrawerDuration.BankOut_Load_TrayAtHomeNoBox),out message3
                    );

            string message4;
            var result4 = que.EnqueueUnique(
                   new DrawerSatusInfo("A12345", EnumMachineID.MID_DRAWER_03_01, BoxType.CrystalBox, DrawerDuration.BankOut_Load_TrayAtHomeNoBox), out message4
                  );

            que.Clear();
        }
    }
}
