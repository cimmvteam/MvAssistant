using MaskAutoCleaner.v1_0.Machine;
using MaskAutoCleaner.v1_0.Machine.Cabinet;
using MaskAutoCleaner.v1_0.Machine.CabinetDrawer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.TestMy.Machine
{
    [TestClass]
    public class UtMcCabinet
    {
        MacMsCabinet0 _machine { get; set; }
        private void Repeat()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(10);
            }
        }
        public UtMcCabinet()
        {
            _machine = MacMsCabinet0.GetInstance();
            // 
            var DrawerMachineIdRange = EnumMachineID.MID_DRAWER_01_01.GetDrawerStateMachineIDRange();
            var MachineMgr = new MacMachineMgr();
            MachineMgr.MvaCfInit();
            MachineControls = new List<MacMcCabinetDrawer>();
            DicStateMachines = new Dictionary<EnumMachineID, MacMsCabinetDrawer>();

            for (var i = (int)DrawerMachineIdRange.StartID; i <= (int)DrawerMachineIdRange.EndID; i++)
            {
                var machineId = ((EnumMachineID)i);
                try
                {
                    var control = MachineMgr.CtrlMachines[machineId.ToString()] as MacMcCabinetDrawer;
                    MachineControls.Add(control);
                    DicStateMachines.Add(machineId, control.StateMachine);

                   
                }
                catch (Exception ex)
                {

                }
            }
        }
        /// <summary>CabinetDrawer StateMachine 的集合</summary>
        private Dictionary<EnumMachineID, MacMsCabinetDrawer> DicStateMachines { get; set; }
        private List<MacMcCabinetDrawer> MachineControls { get; set; }





        /// <summary>
        /// 要測試 BootupInitialDrawers指令
        /// </summary>
        /// <remarks>
        /// 2020/10/20 OK
        /// </remarks>
        [TestMethod]
         public void Test_SystemBootup()
        {
            _machine.DicCabinetDrawerStateMachines = DicStateMachines;
            var method = typeof(MacMsCabinet0).GetMethod(EnumMacMcCabinetCmd.SystemBootup.ToString());
            method.Invoke(_machine, null);
            
        }

        /// <summary>測試 Load Drawer 指令</summary>
        /// <remarks>
        /// 2020/10/20 OK
        /// </remarks>
        [TestMethod]
        [DataRow(3)]
        public void Load_Drawers(int drawersToLoad)
        {
            _machine.DicCabinetDrawerStateMachines = this.DicStateMachines;
            var method = typeof(MacMsCabinet0).GetMethod(EnumMacMcCabinetCmd.Load_Drawers.ToString());
            method.Invoke(method,new object[] { drawersToLoad});
            Repeat();
        }

        /// <summary>測試 SynchrousDrawerStates() 指令</summary>
        /// <remarks>
        /// 2020/10/20 OK
        /// </remarks>
        [TestMethod]
        public void SynchrousDrawerStates()
        {
            _machine.SynchrousDrawerStates(DicStateMachines);
            Repeat();
        }
    }
}
