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

        /// <summary>測試一下,  是否可產生Load Port State Machine Instance</summary>
        /// <para>Date: 2020/10/14, OK</para>
        [TestMethod]
        public void TestLoadportStateMachineInstance()
        {
           Repeat();
        }

        /// <summary>
        /// 測試系統啟動 
        /// </summary>
        /// <remarks>
        /// <para>Date: 2020/10/14, OK</para>
        /// </remarks>
        [TestMethod]
        public void Test_SystemBootup()
        {
            // Machine A
            var method = typeof(MacMsLoadPort).GetMethod(EnumMacLoadportCmd.SystemBootup.ToString());
            method.Invoke(StateMachineA, null);
            // MAchine B
           // method.Invoke(StateMachineB, null);
            Repeat();
        }


        [TestMethod]
        public void Test_ToGetPOD()
        {
            // Machine A
            var method = typeof(MacMsLoadPort).GetMethod(EnumMacLoadportCmd.ToGetPOD.ToString());
            method.Invoke(StateMachineA, null);
            // MAchine B
           // method.Invoke(StateMachineB, null);
            Repeat();
        }
       

        [TestMethod]
        public void Test_Dock()
        {
            // Machine A
            var method = typeof(MacMsLoadPort).GetMethod(EnumMacLoadportCmd.Dock.ToString());
            method.Invoke(StateMachineA, null);
            // MAchine B
            //  method.Invoke(StateMachineB, null);
            Repeat();
        }

        [TestMethod]
        public void Test_UndockWithMaskFromIdleForGetMask()
        {
            // Machine A
            var method = typeof(MacMsLoadPort).GetMethod(EnumMacLoadportCmd.UndockWithMaskFromIdleForGetMask.ToString());
            method.Invoke(StateMachineA, null);
            // MAchine B
            //  method.Invoke(StateMachineB, null);
            Repeat();
        }

        [TestMethod]
        public void Test_ReleasePODWithMask()
        {
            // Machine A
            var method = typeof(MacMsLoadPort).GetMethod(EnumMacLoadportCmd.ReleasePODWithMask.ToString());
            method.Invoke(StateMachineA, null);
            // MAchine B
            //  method.Invoke(StateMachineB, null);
            Repeat();
        }

        [TestMethod]
        public void Test_ToGetPODWithMask()
        {
            // Machine A
            var method = typeof(MacMsLoadPort).GetMethod(EnumMacLoadportCmd.ToGetPODWithMask.ToString());
            method.Invoke(StateMachineA, null);
            // MAchine B
            //  method.Invoke(StateMachineB, null);
            Repeat();
        }

        [TestMethod]
        public void Test_DockWithMask()
        {
            // Machine A
            var method = typeof(MacMsLoadPort).GetMethod(EnumMacLoadportCmd.DockWithMask.ToString());
            method.Invoke(StateMachineA, null);
            // MAchine B
            //  method.Invoke(StateMachineB, null);
            Repeat();
        }

        [TestMethod]
        public void Test_UndockFromIdleForRelesaseMask()
        {
            // Machine A
            var method = typeof(MacMsLoadPort).GetMethod(EnumMacLoadportCmd.UndockFromIdleForRelesaseMask.ToString());
            method.Invoke(StateMachineA, null);
            // MAchine B
            //  method.Invoke(StateMachineB, null);
            Repeat();
        }

        //-------

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
            // Machine A
            StateMachineA.Inintial();
            // Machine B
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
            // Machine A
            StateMachineA.Dock();
            // Machine B
           //  StateMachineB.Dock();
            Repeat();
        }

        /// <summary>測試 Undock</summary>
        /// <remarks>
        /// <para>Date: 2020/10/14, OK</para>
        /// </remarks>
        [TestMethod]
        public void Undock()
        {
            // Machine A
            StateMachineA.Undock();
            // Machine B 
            StateMachineB.Undock();
            Repeat();
        }

    }
}
