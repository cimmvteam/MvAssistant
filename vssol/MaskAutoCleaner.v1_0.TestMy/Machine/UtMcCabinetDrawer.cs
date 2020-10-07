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

        /**
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


        private Dictionary<EnumMachineID, MacMsCabinetDrawer> DicStateMachines { get; set; }
        private List<MacMcCabinetDrawer> MachineControls  { get;set; }
        public UtMcCabinetDrawer()
        {
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
            //    DicMachineStates = GetDicCabinetDrawerMachineStates(out MachineControls);

        }

        [TestMethod]
        //[DataRow(EnumMachineID.MID_DRAWER_01_01, EnumMachineID.MID_DRAWER_01_02)]
        public void CreateInstance(/*EnumMachineID machineID1, EnumMachineID machineID2*/)
        {
            // DataRow
            EnumMachineID machineID1 = EnumMachineID.MID_DRAWER_01_01, machineID2 = EnumMachineID.MID_DRAWER_01_02;

            var machine1 = MacMsCabinet.GetMacMsCabinetDrawer(machineID1,DicStateMachines);
           var machine2 = MacMsCabinet.GetMacMsCabinetDrawer(machineID2, DicStateMachines);

        }

        [TestMethod]
        //[DataRow(EnumMachineID.MID_DRAWER_01_02)]
        public void SystemBootup(/*EnumMachineID machineID*/)
        {
            // DataRow
            EnumMachineID machineID = EnumMachineID.MID_DRAWER_01_02;

            var machine = MacMsCabinet.GetMacMsCabinetDrawer(machineID, DicStateMachines);
            machine.SystemBootup();
        }
        
        


        [TestMethod]
        //[DataRow(EnumMachineID.MID_DRAWER_01_02)]
        public void SystemBootupInitial(/*EnumMachineID machineID*/)
        {
            // DataRow
            EnumMachineID machineID = EnumMachineID.MID_DRAWER_01_02;
            var machine = MacMsCabinet.GetMacMsCabinetDrawer(machineID, DicStateMachines);
            machine.SystemBootupInitial();
        }

        [TestMethod]
        //[DataRow(EnumMachineID.MID_DRAWER_01_02)]
        public void Load_MoveTrayToOut(/*EnumMachineID machineID*/)
        {
            // DataRow
            EnumMachineID machineID = EnumMachineID.MID_DRAWER_01_02;

            var machine = MacMsCabinet.GetMacMsCabinetDrawer(machineID, DicStateMachines);
            machine.Load_MoveTrayToOut();
        }

        [TestMethod]
        //[DataRow(EnumMachineID.MID_DRAWER_01_02)]
        public void Load_MoveTrayToHome(/*EnumMachineID machineID*/)
        {
            // DataRow
            EnumMachineID machineID = EnumMachineID.MID_DRAWER_01_02;

            var machine = MacMsCabinet.GetMacMsCabinetDrawer(machineID, DicStateMachines);
            machine.Load_MoveTrayToHome();
        }

        [TestMethod]
        //[DataRow(EnumMachineID.MID_DRAWER_01_02)]
        public void Load_MoveTrayToIn(/*EnumMachineID machineID*/)
        {
            // DataRow
            EnumMachineID machineID = EnumMachineID.MID_DRAWER_01_02;

            var machine = MacMsCabinet.GetMacMsCabinetDrawer(machineID, DicStateMachines);
            machine.Load_MoveTrayToIn();
        }

        [TestMethod]
        //[DataRow(EnumMachineID.MID_DRAWER_01_02)]
        public void MoveTrayToHomeWaitingUnloadInstruction(/*EnumMachineID machineID*/)
        {
            // DataRow
            EnumMachineID machineID = EnumMachineID.MID_DRAWER_01_02;

            var machine = MacMsCabinet.GetMacMsCabinetDrawer(machineID, DicStateMachines);
            machine.MoveTrayToHomeWaitingUnloadInstruction();
        }

        [TestMethod]
        //[DataRow(EnumMachineID.MID_DRAWER_01_02)]
        public void Unload_MoveTrayToIn(/*EnumMachineID machineID*/)
        {
            // DataRow
            EnumMachineID machineID = EnumMachineID.MID_DRAWER_01_02;

            var machine = MacMsCabinet.GetMacMsCabinetDrawer(machineID, DicStateMachines);
            machine.Unload_MoveTrayToIn();
        }

        [TestMethod]
        //[DataRow(EnumMachineID.MID_DRAWER_01_02)]
        public void Unload_MoveTrayToHome(/*EnumMachineID machineID*/)
        {
            // DataRow
            EnumMachineID machineID = EnumMachineID.MID_DRAWER_01_02;

            var machine = MacMsCabinet.GetMacMsCabinetDrawer(machineID, DicStateMachines);
            machine.Unload_MoveTrayToHome();
        }

        [TestMethod]
        //[DataRow(EnumMachineID.MID_DRAWER_01_02)]
        public void MoveTrayToHomeWaitingLoadInstruction(/*EnumMachineID machineID*/)
        {
            // DataRow
            EnumMachineID machineID = EnumMachineID.MID_DRAWER_01_02;

            var machine = MacMsCabinet.GetMacMsCabinetDrawer(machineID, DicStateMachines);
            machine.MoveTrayToHomeWaitingLoadInstruction();
        }

    }
}
