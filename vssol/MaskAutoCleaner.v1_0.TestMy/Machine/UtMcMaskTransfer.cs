using System;
using MaskAutoCleaner.v1_0.Machine;
using MaskAutoCleaner.v1_0.Machine.MaskTransfer;
using MaskAutoCleaner.v1_0.TestMy.UserData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MaskAutoCleaner.v1_0.TestMy.Machine
{
    [TestClass]
    public class UtMcMaskTransfer
    {
        [TestMethod]
        public void TestInitial()
        {
            var machineMgr = new MacMachineMgr();
            //var MTmachineMs = new v1_0.Machine.MaskTransfer.MacMsMaskTransfer();
            var MTmachineMc = new MacMcMaskTransfer();
            machineMgr.MvCfInit();
            MTmachineMc.StateMachine.Initial();
        }
    }
}
