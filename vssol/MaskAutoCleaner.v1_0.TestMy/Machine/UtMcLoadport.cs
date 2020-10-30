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
           // StateMachineA = MacMsLoadPort.LoadPortStateMachineA;
         //   StateMachineB = MacMsLoadPort.LoadPortStateMachineB;
           
            var MachineMgr = new MacMachineMgr();

            MachineMgr.MvCfInit();
            var MachineCtrlA = MachineMgr.CtrlMachines[EnumLoadportStateMachineID.MID_LP_A_ASB.ToString()] as MacMcLoadPort;
            var MachineCtrlB = MachineMgr.CtrlMachines[EnumLoadportStateMachineID.MID_LP_B_ASB.ToString()] as MacMcLoadPort;
            StateMachineA = MachineCtrlA.StateMachine;
            StateMachineB = MachineCtrlB.StateMachine;
            
        }

        public void Repeat()
        {
            while (true)
            {
                Thread.Sleep(8);
            }
        }


        /// <summary>
        /// 測試系統啟動 
        /// </summary>
        /// <remarks>
        /// <para>Date: 2020/10/14, OK</para>
        /// </remarks>
        [TestMethod]
        public void TestSystemBootup()
        {
            // Machine A
            StateMachineA.SystemBootup();
            // MAchine B
            StateMachineB.SystemBootup();
            Repeat();
        }

        /// <summary>測試一下,  是否可產生Load Port State Machine Instance</summary>
        /// <para>Date: 2020/10/14, OK</para>
        [TestMethod]
        public void TestLoadportStateMachineInstance()
        {
           Repeat();
        }


        /// <summary>測試 AlarmReset</summary>
        /// <remarks>
        /// <para>Date: 2020/10/14, OK</para>
        /// </remarks>
        [TestMethod]
        public void AlarmReset()
        {
            // Machine A
            StateMachineA.AlarmReset();
            // Machine B
            StateMachineB.AlarmReset();
            Repeat();
        }

        /// <summary>測試 Initial</summary>
        /// <remarks>
        /// <para>Date: 2020/10/14, OK</para>
        /// </remarks>
        [TestMethod]
        public void Initial()
        {
            StateMachineA.Inintial();
            StateMachineB.Inintial();
            Repeat();
        }

        /// <summary>測試 Dock</summary>
        /// <remarks>
        /// <para>Date: 2020/10/14, OK</para>
        /// </remarks>
        [TestMethod]
        public void Dock()
        {
            StateMachineA.Dock();
            StateMachineB.Dock();
            Repeat();
        }

        /// <summary>測試 Undock</summary>
        /// <remarks>
        /// <para>Date: 2020/10/14, OK</para>
        /// </remarks>
        [TestMethod]
        public void Undock()
        {
            StateMachineA.Undock();
            StateMachineB.Undock();
            Repeat();
        }

    }
}
