using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.TestMy.Machine
{
    [TestClass]
    public class UtMcLoadport
    {
        [TestMethod]
        public void TestLoadportInstance()
        {
            var machine = new v1_0.Machine.LoadPort.MacMsLoadPort();
           // var loadPort = machine.HalLoadPortUnit;
            machine.LoadStateMachine();
            machine.TestLoadportInstance();
        }

        [TestMethod]
        public void Reset()
        {
            var machine = new v1_0.Machine.LoadPort.MacMsLoadPort();
            var loadPort = machine.HalLoadPortUnit;
            machine.LoadStateMachine();
            machine.Reset();
        }

        [TestMethod]
        public void Initial()
        {
            var machine = new v1_0.Machine.LoadPort.MacMsLoadPort();
            var loadPort = machine.HalLoadPortUnit;
            machine.LoadStateMachine();
            machine.Inintial();
        }
        [TestMethod]
        public void Dock()
        {
            var machine = new v1_0.Machine.LoadPort.MacMsLoadPort();
            var loadPort = machine.HalLoadPortUnit;
            machine.LoadStateMachine();
            machine.Dock();
        }
        [TestMethod]
        public void Undock()
        {
            var machine = new v1_0.Machine.LoadPort.MacMsLoadPort();
            var loadPort = machine.HalLoadPortUnit;
            machine.LoadStateMachine();
            machine.Undock();
        }

    }
}
