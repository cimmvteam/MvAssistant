using System;
using System.IO;
using MaskAutoCleaner.v1_0.Machine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MvAssistant.Mac.TestMy.GenCfg.Machine
{
    [TestClass]
    public class UtGenMachineMgrCfg
    {
        [TestMethod]
        public void TestMethod1()
        {


            var cfg = new MacMachineMgrCfg();

            cfg.ManifestCfgPath = Path.Combine(@"../../", "GenCfg/Manifest/Manifest.xml.real"); ;


            var fn = Path.Combine(@"../../", "GenCfg/Machine/MachineMgrCfg.xml");
            cfg.SaveToXmlFile(fn);





        }
    }
}
