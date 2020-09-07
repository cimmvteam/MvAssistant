using MaskAutoCleaner.v1_0.Machine;
using MaskAutoCleaner.v1_0.Machine.LoadPort;
using MaskAutoCleaner.v1_0.TestMy.UserData;
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
            var MachineMgr = new MacMachineMgr();
            MachineMgr.MvCfInit();
            var MachineCtrlA = MachineMgr.CtrlMachines[EnumMachineID.MID_LP_A_ASB.ToString()] as MacMcLoadPort;
            var MachineCtrlB = MachineMgr.CtrlMachines[EnumMachineID.MID_LP_B_ASB.ToString()] as MacMcLoadPort;
            StateMachineA = MachineCtrlA.StateMachine;
            StateMachineB = MachineCtrlB.StateMachine;
            StateMachineA.LoadportKey = "loadport_1";
            StateMachineB.LoadportKey = "loadport_2";

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
            /*
            //var machine = new v1_0.Machine.LoadPort.MacMsLoadPort();
            // var loadPort = machine.HalLoadPortUnit;
            var MachineMgr = new MacMachineMgr();
            MachineMgr.MvCfInit();
            var MachineCtrl = MachineMgr.CtrlMachines[EnumMachineID.MID_LP_A_ASB.ToString()] as MacMcLoadPort;
            var MS = MachineCtrl.StateMachine;
            //machine.LoadStateMachine();
            MS.TestLoadportInstance();*/
            StateMachineA.TestLoadportInstance();
            Repeat();
        }

        [TestMethod]
        public void Reset()
        {
            var machine = new v1_0.Machine.LoadPort.MacMsLoadPort();
            //var loadPort = machine.HalLoadPortUnit;
            machine.LoadStateMachine();
            machine.Reset();
            Repeat();
        }

        [TestMethod]
        public void Initial()
        {
            var machine = new v1_0.Machine.LoadPort.MacMsLoadPort();
           // var loadPort = machine.HalLoadPortUnit;
            machine.LoadStateMachine();
            machine.Inintial();
            Repeat();
        }
        [TestMethod]
        public void Dock()
        {
            var machine = new v1_0.Machine.LoadPort.MacMsLoadPort();
            //var loadPort = machine.HalLoadPortUnit;
            machine.LoadStateMachine();
            machine.Dock();
            Repeat();
        }
        [TestMethod]
        public void Undock()
        {
            var machine = new v1_0.Machine.LoadPort.MacMsLoadPort();
            //var loadPort = machine.HalLoadPortUnit;
            machine.LoadStateMachine();
            machine.Undock();
            Repeat();
        }

    }
}
