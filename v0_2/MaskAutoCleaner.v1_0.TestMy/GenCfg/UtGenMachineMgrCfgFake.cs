using MaskAutoCleaner.v1_0.Machine;
using MaskAutoCleaner.v1_0.Machine.BoxTransfer;
using MaskAutoCleaner.v1_0.Machine.CabinetDrawer;
using MaskAutoCleaner.v1_0.Machine.CleanCh;
using MaskAutoCleaner.v1_0.Machine.Drawer;
using MaskAutoCleaner.v1_0.Machine.InspectionCh;
using MaskAutoCleaner.v1_0.Machine.LoadPort;
using MaskAutoCleaner.v1_0.Machine.MaskTransfer;
using MaskAutoCleaner.v1_0.Machine.OpenStage;
using MaskAutoCleaner.v1_0.Machine.Universal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.v0_2.Mac.TestMy.GenCfg.Manifest;
using System.IO;

namespace MaskAutoCleaner.v1_0.TestMy.GenCfg
{
    [TestClass]
    public class UtGenMachineMgrCfgFake
    {
        [TestMethod]
        public void TestMethod1()
        {
            var cfg = new MacMachineMgrCfg();
            cfg.ManifestCfgPath = "UserData/Manifest/Manifest.xml.fake";
            cfg.MachineCtrls.Add(new MacMachineCtrlCfg()
            {
                ID = EnumMachineID.MID_LP_A_ASB.ToString(),
                MachineCtrlType = typeof(MacMcLoadPort),
                HalId = EnuDeviceId.HID_LP_A_ASSY.ToString(),
            });
            cfg.MachineCtrls.Add(new MacMachineCtrlCfg()
            {
                ID = EnumMachineID.MID_LP_B_ASB.ToString(),
                MachineCtrlType = typeof(MacMcLoadPort),
                HalId = EnuDeviceId.HID_LP_B_ASSY.ToString(),
            });
            cfg.MachineCtrls.Add(new MacMachineCtrlCfg()
            {
                ID = EnumMachineID.MID_MT_A_ASB.ToString(),
                MachineCtrlType = typeof(MacMcMaskTransfer),
                HalId = EnuDeviceId.HID_MT_A_ASSY.ToString(),
            });
            cfg.MachineCtrls.Add(new MacMachineCtrlCfg()
            {
                ID = EnumMachineID.MID_IC_A_ASB.ToString(),
                MachineCtrlType = typeof(MacMcInspectionCh),
                HalId = EnuDeviceId.HID_IC_A_ASSY.ToString(),
            });
            cfg.MachineCtrls.Add(new MacMachineCtrlCfg()
            {
                ID = EnumMachineID.MID_CC_A_ASB.ToString(),
                MachineCtrlType = typeof(MacMcCleanCh),
                HalId = EnuDeviceId.HID_CC_A_ASSY.ToString(),
            });
            cfg.MachineCtrls.Add(new MacMachineCtrlCfg()
            {
                ID = EnumMachineID.MID_OS_A_ASB.ToString(),
                MachineCtrlType = typeof(MacMcOpenStage),
                HalId = EnuDeviceId.HID_OS_A_ASSY.ToString(),
            });
            cfg.MachineCtrls.Add(new MacMachineCtrlCfg()
            {
                ID = EnumMachineID.MID_BT_A_ASB.ToString(),
                MachineCtrlType = typeof(MacMcBoxTransfer),
                HalId = EnuDeviceId.HID_BT_A_ASSY.ToString(),
            });
            cfg.MachineCtrls.Add(new MacMachineCtrlCfg()
            {
                ID = EnumMachineID.MID_CB_A_ASB.ToString(),
                MachineCtrlType = typeof(MacMcDrawer),
                HalId = EnuDeviceId.HID_CB_A_ASSY.ToString(),
            });
            cfg.MachineCtrls.Add(new MacMachineCtrlCfg()
            {
                ID = EnumMachineID.MID_UNI_A_ASB.ToString(),
                MachineCtrlType = typeof(MacMcUniversal),
                HalId = EnuDeviceId.HID_UNI_A_ASSY.ToString(),
            });
            /**
            cfg.MachineCtrls.Add(new MacMachineCtrlCfg()
            {
                ID = EnumMachineID.MID_DRAWER_01_01.ToString(),
                MachineCtrlType = typeof(MacMcCabinetDrawer),
                HalId = EnuDeviceId.DE_CB_A_01_01.ToString(),
            });

            cfg.MachineCtrls.Add(new MacMachineCtrlCfg()
            {
                ID = EnumMachineID.MID_DRAWER_01_02.ToString(),
                MachineCtrlType = typeof(MacMcCabinetDrawer),
                HalId = EnuDeviceId.DE_CB_A_01_02.ToString(),
            });

            cfg.MachineCtrls.Add(new MacMachineCtrlCfg()
            {
                ID = EnumMachineID.MID_DRAWER_01_03.ToString(),
                MachineCtrlType = typeof(MacMcCabinetDrawer),
                HalId = EnuDeviceId.DE_CB_A_01_03.ToString(),
            });
            cfg.MachineCtrls.Add(new MacMachineCtrlCfg()
            {
                ID = EnumMachineID.MID_DRAWER_01_04.ToString(),
                MachineCtrlType = typeof(MacMcCabinetDrawer),
                HalId = EnuDeviceId.DE_CB_A_01_04.ToString(),
            });*/
            var fn = "../../UserData/MachineMgr.config.fake";
            var fi = new FileInfo(fn);
            if (!fi.Directory.Exists) fi.Directory.Create();
            cfg.SaveToXmlFile(fn);
        }
    }
}
