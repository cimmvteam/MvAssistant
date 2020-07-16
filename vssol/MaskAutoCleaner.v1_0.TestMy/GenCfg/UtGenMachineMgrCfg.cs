using System;
using System.IO;
using MaskAutoCleaner.v1_0.Machine;
using MaskAutoCleaner.v1_0.Machine.MaskTransfer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MaskAutoCleaner.v1_0.TestMy.GenCfg
{
    [TestClass]
    public class UtGenMachineMgrCfg
    {
        [TestMethod]
        public void TestMethod1()
        {

            var cfg = new MacMachineMgrCfg();
            cfg.ManifestCfgPath = "UserData/Manifest.xml.real";


            cfg.MachineCtrls.Add(new MacMachineCtrlCfg()
            {
                 MachineCtrlType = typeof(MacMcMaskTransfer),
            });



            var fn = "../../UserData/MachineMgr.config";
            var fi = new FileInfo(fn);
            if (!fi.Directory.Exists) fi.Directory.Create();
            cfg.SaveToXmlFile(fn);



        }
    }
}
