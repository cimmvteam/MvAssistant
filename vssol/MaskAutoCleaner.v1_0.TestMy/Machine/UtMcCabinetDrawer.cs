using MaskAutoCleaner.v1_0.Machine;
using MaskAutoCleaner.v1_0.Machine.Cabinet;
using MaskAutoCleaner.v1_0.Machine.CabinetDrawer;
using MaskAutoCleaner.v1_0.Machine.Drawer;
using MaskAutoCleaner.v1_0.TestMy.UserData;
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
        [DataRow(EnumDrawerStateMachineID.MID_DRAWER_01_02,EnumDrawerStateMachineID.MID_DRAWER_01_01)]
        public void CreateInstance(EnumDrawerStateMachineID machineID1, EnumDrawerStateMachineID machineID2)
        {
            var machine1 = MacMsCabinet.GetMacMsCabinetDrawer(machineID1);
            var machine2 = MacMsCabinet.GetMacMsCabinetDrawer(machineID2);

        }

        [TestMethod()]
        [DataRow(EnumDrawerStateMachineID.MID_DRAWER_01_02)]
        public void SystemBootup(EnumDrawerStateMachineID machineID)
        {
            var machine = MacMsCabinet.GetMacMsCabinetDrawer(machineID);
            machine.SystemBootup();
        }
        
        


        [TestMethod]
        [DataRow(EnumDrawerStateMachineID.MID_DRAWER_01_02)]
        public void SystemBootupInitial(EnumDrawerStateMachineID machineID)
        {
            var machine = MacMsCabinet.GetMacMsCabinetDrawer(machineID);
            machine.SystemBootupInitial();
        }

        [TestMethod]
        [DataRow(EnumDrawerStateMachineID.MID_DRAWER_01_02)]
        public void Load_MoveTrayToOut(EnumDrawerStateMachineID machineID)
        {
            var machine = MacMsCabinet.GetMacMsCabinetDrawer(machineID);
            machine.Load_MoveTrayToOut();
        }

        [TestMethod]
        [DataRow(EnumDrawerStateMachineID.MID_DRAWER_01_02)]
        public void Load_MoveTrayToHome(EnumDrawerStateMachineID machineID)
        {
            var machine = MacMsCabinet.GetMacMsCabinetDrawer(machineID);
            machine.Load_MoveTrayToHome();
        }

        [TestMethod]
        [DataRow(EnumDrawerStateMachineID.MID_DRAWER_01_02)]
        public void Load_MoveTrayToIn(EnumDrawerStateMachineID machineID)
        {
            var machine = MacMsCabinet.GetMacMsCabinetDrawer(machineID);
            machine.Load_MoveTrayToIn();
        }

        [TestMethod]
        [DataRow(EnumDrawerStateMachineID.MID_DRAWER_01_02)]
        public void MoveTrayToHomeWaitingUnloadInstruction(EnumDrawerStateMachineID machineID)
        {
            var machine = MacMsCabinet.GetMacMsCabinetDrawer(machineID);
            machine.MoveTrayToHomeWaitingUnloadInstruction();
        }

        [TestMethod]
        [DataRow(EnumDrawerStateMachineID.MID_DRAWER_01_02)]
        public void Unload_MoveTrayToIn(EnumDrawerStateMachineID machineID)
        {
            var machine = MacMsCabinet.GetMacMsCabinetDrawer(machineID);
            machine.Unload_MoveTrayToIn();
        }

        [TestMethod]
        [DataRow(EnumDrawerStateMachineID.MID_DRAWER_01_02)]
        public void Unload_MoveTrayToHome(EnumDrawerStateMachineID machineID)
        {
            var machine = MacMsCabinet.GetMacMsCabinetDrawer(machineID);
            machine.Unload_MoveTrayToHome();
        }

        [TestMethod]
        [DataRow(EnumDrawerStateMachineID.MID_DRAWER_01_02)]
        public void MoveTrayToHomeWaitingLoadInstruction(EnumDrawerStateMachineID machineID)
        {
            var machine = MacMsCabinet.GetMacMsCabinetDrawer(machineID);
            machine.MoveTrayToHomeWaitingLoadInstruction();
        }

    }
}
