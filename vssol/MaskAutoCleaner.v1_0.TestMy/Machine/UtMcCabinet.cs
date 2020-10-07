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
        MacMsCabinet _machine { get; set; }
        public UtMcCabinet()
        {
            _machine = new MacMsCabinet();
            _machine.LoadStateMachine();


            var DrawerMachineIdRange = EnumMachineID.MID_DRAWER_01_01.GetDrawerStateMachineIDRange();
            var MachineMgr = new MacMachineMgr();
            MachineMgr.MvCfInit();
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

        private Dictionary<EnumMachineID, MacMsCabinetDrawer> DicStateMachines { get; set; }
        private List<MacMcCabinetDrawer> MachineControls { get; set; }
     






        [TestMethod]
        public void LoadDrawers()
        {
            int drawers = 20;// 要 執行Load 的數量 
            _machine.LoadDrawers(drawers, DicStateMachines);
        }
        [TestMethod]
        public void BootupInitialDrawers()
        {
            _machine.BootupInitialDrawers(DicStateMachines);
        }

        [TestMethod]
        public void SynchrousDrawerStates()
        {
            _machine.SynchrousDrawerStates(DicStateMachines);
        }
    }
}
