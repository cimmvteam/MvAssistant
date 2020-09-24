using MaskAutoCleaner.v1_0.Machine;
using MaskAutoCleaner.v1_0.Machine.LoadPort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.TestMy.Machine
{
    [TestClass]
    public class UtMcLoadport
    {
        private MacMsLoadPort StateMachineA = null;
        private MacMsLoadPort StateMachineB = null;
        public UtMcLoadport()
        {
            StateMachineA = MacMsLoadPort.LoadPortStateMachineA;
            StateMachineB = MacMsLoadPort.LoadPortStateMachineB;
        }

        public void Repeat()
        {
            while (true)
            {
                Thread.Sleep(8);
            }
        }

        [TestMethod]
        public void TestLoadportInstance()
        {
           Repeat();
        }

        [TestMethod]
        public void Reset()
        {
           
            StateMachineA.AlarmReset();
            Repeat();
        }

        [TestMethod]
        public void Initial()
        {
            // var machine = new v1_0.Machine.LoadPort.MacMsLoadPort();
            // var loadPort = machine.HalLoadPortUnit;
            // machine.LoadStateMachine();
            StateMachineA.Inintial();
            StateMachineB.Inintial();
            Repeat();
        }
        [TestMethod]
        public void Dock()
        {
            //var machine = new v1_0.Machine.LoadPort.MacMsLoadPort();
            //var loadPort = machine.HalLoadPortUnit;
            //machine.LoadStateMachine();
            StateMachineA.Dock();
            StateMachineB.Dock();
            Repeat();
        }
        [TestMethod]
        public void Undock()
        {
            //var machine = new v1_0.Machine.LoadPort.MacMsLoadPort();
            //var loadPort = machine.HalLoadPortUnit;
            //machine.LoadStateMachine();
            StateMachineA.Undock();
            StateMachineB.Undock();
            Repeat();
        }

    }
}
