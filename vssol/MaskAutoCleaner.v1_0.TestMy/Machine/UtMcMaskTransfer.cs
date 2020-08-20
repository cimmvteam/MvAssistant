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
            var MTmachine = new v1_0.Machine.MaskTransfer.MacMsMaskTransfer();
            
            MTmachine.LoadStateMachine();
            MTmachine.Initial();
        }
    }
}
