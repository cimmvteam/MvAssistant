using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Hal.Component.Robot;
using MvAssistant.Mac.v1_0.Manifest;

namespace MvAssistant.Mac.TestMy.MachineRealHal
{
    [TestClass]
    public class UtHalMaskTransfer
    {
        [TestMethod]
        public void TestRobotMoveToLoadPort()
        {
            using (var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real"))
            {
                halContext.Load();


                var mt = halContext.HalDevices[MacEnumDevice.masktransfer_assembly.ToString()] as MacHalMaskTransfer;
                mt.RobotMove(mt.HomeToOpenStage());
                mt.RobotMove(mt.OpenStageToHome());
                mt.ChangeDirection(mt.PosToInspCh());
                mt.RobotMove(mt.FrontSideIntoInspCh());
                mt.RobotMove(mt.FrontSideLeaveInspCh());
                mt.RobotMove(mt.BackSideIntoInspCh());
                mt.RobotMove(mt.BackSideLeaveInspCh());
                mt.ChangeDirection(mt.PosToCleanCh());
                //mt.Robot.HalMoveAsyn();
                //mt.HalMoveAsyn();
                mt.MtClamp();
            }

        }
    }
}
