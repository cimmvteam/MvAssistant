using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.v1_0.Hal;

namespace MvAssistant.Mac.TestMy.MachineRealHal.Hirata_ScenarioTest
{
    /// <summary>MI-CT02-ST-001</summary>
    [TestClass]
    public class Ut001_BT_
    {
        [TestMethod]
        public void TestMethod1_5()
        {
            var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real");
            
            // 1. 光罩鐵盒放置於Open Stage平台上

            // 2. Box Robot從Home點至Open Stage進行光罩鐵盒夾取

            // 3. Box Robot將光罩鐵盒從Open Stage移動, 移入Drawer內部 (夾爪夾持光罩鐵盒, 不做放置光罩鐵盒於drawer的動作)

            // 4. Box Robot將光罩鐵盒從Drawer內部, 移到Drawer外部 (進入drawer內部的前一個點位)

            // 5. 重複2~4步驟, 完成20個Drawer的光罩鐵盒的移動測試



        }

        public void TestMethod6_10()
        {
            // 6.光罩水晶盒放置於Open Stage平台上
            // 7.Box Robot從Home點至Open Stage進行光罩水晶盒夾取
            // 8.Box Robot將光罩水晶盒從Open Stage移動, 移入Drawer內部 (夾爪夾持光罩水晶盒, 不做放置光罩水晶盒於drawer的動作)
            // 9.Box Robot將光罩水晶盒從Drawer內部, 移到Drawer外部(進入drawer內部的前一個點位)
            //10.重複7~9步驟, 完成20個Drawer的光罩水晶盒的移動測試
        }
    }
}
