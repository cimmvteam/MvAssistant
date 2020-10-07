using MaskAutoCleaner.v1_0.Machine;
using MaskAutoCleaner.v1_0.Machine.Cabinet;
using MaskAutoCleaner.v1_0.Machine.CabinetDrawer;
using MaskAutoCleaner.v1_0.Machine.Drawer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.v1_0.Manifest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.TestMy.Machine
{
    [TestClass]
    public class UtMcCabinetDrawer
    {
        // MacMsCabinetDrawer _machine { get; set; }

        /*
 List<MacMcCabinetDrawer> MachineControls = null;
 public Dictionary<EnumDrawerStateMachineID, MacMsCabinetDrawer> DicMachineStates = null;


 private Dictionary<EnumDrawerStateMachineID, MacMsCabinetDrawer> GetDicCabinetDrawerMachineStates(out List<MacMcCabinetDrawer> controls)
 {
     var MachineMgr = new MacMachineMgr();
     MachineMgr.MvCfInit();
     controls = new List<MacMcCabinetDrawer>();
     var states = new Dictionary<EnumDrawerStateMachineID, MacMsCabinetDrawer>();
     for (var i = (int)EnumDrawerStateMachineID.MID_DRAWER_01_01; i <= (int)EnumDrawerStateMachineID.MID_DRAWER_01_04; i++)
     {
         var machineId = ((EnumDrawerStateMachineID)i);
         try
         {
             var control = MachineMgr.CtrlMachines[machineId.ToString()] as MacMcCabinetDrawer;
             controls.Add(control);
             states.Add(machineId, control.StateMachine);
         }
         catch (Exception ex)
         {

         }
     }
     return states;
 }

 /// <summary>取得指定的 State Machine</summary>
 /// <param name="machineId"></param>
 /// <returns></returns>
 public MacMsCabinetDrawer GetMacMsCabinetDrawer(EnumDrawerStateMachineID machineId)
 {
     if (DicMachineStates == null) { return null; }
     var rtn = DicMachineStates[machineId];
     return rtn;
 }
*/
        public UtMcCabinetDrawer()
        {
          
        //    DicMachineStates = GetDicCabinetDrawerMachineStates(out MachineControls);
          
        }

        [TestMethod()]
        [DataRow(EnumMachineID.MID_DRAWER_01_02, EnumMachineID.MID_DRAWER_01_01)]
        public void CreateInstance(EnumMachineID machineID1, EnumMachineID machineID2)
        {
            var machine1 = MacMsCabinet.GetMacMsCabinetDrawer(machineID1);
            var machine2 = MacMsCabinet.GetMacMsCabinetDrawer(machineID2);

        }

        [TestMethod()]
        [DataRow(EnumMachineID.MID_DRAWER_01_02)]
        public void SystemBootup(EnumMachineID machineID)
        {
            var machine = MacMsCabinet.GetMacMsCabinetDrawer(machineID);
            machine.SystemBootup();
        }
        
        


        [TestMethod]
        [DataRow(EnumMachineID.MID_DRAWER_01_02)]
        public void SystemBootupInitial(EnumMachineID machineID)
        {
            var machine = MacMsCabinet.GetMacMsCabinetDrawer(machineID);
            machine.SystemBootupInitial();
        }

        [TestMethod]
        [DataRow(EnumMachineID.MID_DRAWER_01_02)]
        public void Load_MoveTrayToOut(EnumMachineID machineID)
        {
            var machine = MacMsCabinet.GetMacMsCabinetDrawer(machineID);
            machine.Load_MoveTrayToOut();
        }

        [TestMethod]
        [DataRow(EnumMachineID.MID_DRAWER_01_02)]
        public void Load_MoveTrayToHome(EnumMachineID machineID)
        {
            var machine = MacMsCabinet.GetMacMsCabinetDrawer(machineID);
            machine.Load_MoveTrayToHome();
        }

        [TestMethod]
        [DataRow(EnumMachineID.MID_DRAWER_01_02)]
        public void Load_MoveTrayToIn(EnumMachineID machineID)
        {
            var machine = MacMsCabinet.GetMacMsCabinetDrawer(machineID);
            machine.Load_MoveTrayToIn();
        }

        [TestMethod]
        [DataRow(EnumMachineID.MID_DRAWER_01_02)]
        public void MoveTrayToHomeWaitingUnloadInstruction(EnumMachineID machineID)
        {
            var machine = MacMsCabinet.GetMacMsCabinetDrawer(machineID);
            machine.MoveTrayToHomeWaitingUnloadInstruction();
        }

        [TestMethod]
        [DataRow(EnumMachineID.MID_DRAWER_01_02)]
        public void Unload_MoveTrayToIn(EnumMachineID machineID)
        {
            var machine = MacMsCabinet.GetMacMsCabinetDrawer(machineID);
            machine.Unload_MoveTrayToIn();
        }

        [TestMethod]
        [DataRow(EnumMachineID.MID_DRAWER_01_02)]
        public void Unload_MoveTrayToHome(EnumMachineID machineID)
        {
            var machine = MacMsCabinet.GetMacMsCabinetDrawer(machineID);
            machine.Unload_MoveTrayToHome();
        }

        [TestMethod]
        [DataRow(EnumMachineID.MID_DRAWER_01_02)]
        public void MoveTrayToHomeWaitingLoadInstruction(EnumMachineID machineID)
        {
            var machine = MacMsCabinet.GetMacMsCabinetDrawer(machineID);
            machine.MoveTrayToHomeWaitingLoadInstruction();
        }

    }
}
