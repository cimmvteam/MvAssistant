using System;
using MaskAutoCleaner.v1_0.Machine.Cabinet;
using MaskAutoCleaner.v1_0.Machine.Cabinet.DrawerQueues;
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
            MaskBoxForBankOutQue que = MaskBoxForBankOutQue.GetInstance();
          
                string message1;
               var result1= que.EnqueueUnique(
                    new MaskBoxInfo("A12345", EnumMachineID.MID_DRAWER_01_01, BoxType.CrystalBox),out message1
                    );
                string message2;
              var result2=  que.EnqueueUnique(
                     new MaskBoxInfo("B12345", EnumMachineID.MID_DRAWER_02_01, BoxType.CrystalBox),out message2
                    );
                string message3;
              var result3=  que.EnqueueUnique(
                     new MaskBoxInfo("C12345", EnumMachineID.MID_DRAWER_02_01, BoxType.CrystalBox),out message3
                    );

            string message4;
            var result4 = que.EnqueueUnique(
                   new MaskBoxInfo("A12345", EnumMachineID.MID_DRAWER_03_01, BoxType.CrystalBox), out message4
                  );

            que.Clear();
        }
    }
}
