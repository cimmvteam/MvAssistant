using System;
using MaskAutoCleaner.v1_0.Machine;
using MaskAutoCleaner.v1_0.Machine.OpenStage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MaskAutoCleaner.v1_0.TestMy.Machine
{
    [TestClass]
    public class UtMcOpenStage
    {
        [TestMethod]
        public void InputMask()
        {
            var MachineMgr = new MacMachineMgr();
            MachineMgr.MvaCfInit();
            var MachineCtrl = MachineMgr.CtrlMachines[EnumMachineID.MID_OS_A_ASB.ToString()] as MacMcOpenStage;
            var MS = MachineCtrl.StateMachine;

            MS.SystemBootup();
            MS.InputBox();
            MS.CalibrationClosedBox();
            MS.OpenBox();
            if (true)//如果有放入Mask
            {
                MS.CloseBoxWithMask();
                MS.ReleaseBoxWithMask();
                MS.ReturnToIdleAfterReleaseBoxWithMask();
            }
            else//如果沒有放入Mask(非常態)
            {
                MS.ReturnCloseBox();
                MS.ReleaseBox();
                MS.ReturnToIdleAfterReleaseBox();
            }
        }

        [TestMethod]
        public void ReleaseMask()
        {
            var MachineMgr = new MacMachineMgr();
            MachineMgr.MvaCfInit();
            var MachineCtrl = MachineMgr.CtrlMachines[EnumMachineID.MID_OS_A_ASB.ToString()] as MacMcOpenStage;
            var MS = MachineCtrl.StateMachine;

            MS.SystemBootup();
            MS.InputBoxWithMask();
            MS.CalibrationClosedBoxWithMask();
            MS.OpenBoxWithMask();
            if (false)//如果沒有取出Mask(非常態)
            {
                MS.ReturnCloseBoxWithMask();
                MS.ReleaseBoxWithMask();
                MS.ReturnToIdleAfterReleaseBoxWithMask();
            }
            else//如果有取出Mask
            {
                MS.CloseBox();
                MS.ReleaseBox();
                MS.ReturnToIdleAfterReleaseBox();
            }
        }
    }
}
