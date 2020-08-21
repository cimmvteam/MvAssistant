using System;
using MaskAutoCleaner.v1_0.Machine.MaskTransfer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MaskAutoCleaner.v1_0.TestMy.Machine
{
    [TestClass]
    public class UtMcMaskTransfer
    {
        [TestMethod]
        public void TestInitial()
        {
            var MTmachineMs = new v1_0.Machine.MaskTransfer.MacMsMaskTransfer();
            var MTmachineMc = new MacMcMaskTransfer();
            MTmachineMc.StateMachine.Initial();
        }
    }
}
