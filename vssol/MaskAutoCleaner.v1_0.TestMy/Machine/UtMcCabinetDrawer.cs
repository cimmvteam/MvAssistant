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

        }
        private void Repeat()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(50);
            };
        } 

        /// <summary>測試能否產生 CabinetDrawer Udp Listen Instance</summary>
        /// <remarks>
        /// 2020/10/19 OK 
        /// </remarks>
        [TestMethod]
        //[DataRow(EnumMachineID.MID_DRAWER_01_01, EnumMachineID.MID_DRAWER_01_02)]
        public void CreateInstance(/*EnumMachineID machineID1, EnumMachineID machineID2*/)
        {
            // DataRow
            EnumMachineID machineID1 = EnumMachineID.MID_DRAWER_01_01, machineID2 = EnumMachineID.MID_DRAWER_01_02;

            var machine1 = MacMsCabinet.GetMacMsCabinetDrawer(machineID1,DicStateMachines);
            //var machine2 = MacMsCabinet.GetMacMsCabinetDrawer(machineID2, DicStateMachines);

           

            Repeat();

        }

        /// <summary>測試 SystemBootup</summary>
        /// <remarks>
        /// 2020/10/19 OK
        /// </remarks>
        [TestMethod]
        [DataRow(EnumMachineID.MID_DRAWER_01_02)]
        //[DataRow(EnumMachineID.MID_DRAWER_04_02)]
        public void SystemBootup(EnumMachineID machineID)
        {
        
            var machine = MacMsCabinet.GetMacMsCabinetDrawer(machineID, DicStateMachines);
         
            var method = typeof(MacMsCabinetDrawer).GetMethod(EnumMacMcCabinetDrawerCmd.SystemBootup.ToString());

            method.Invoke(machine, null);
            Repeat();
        }
        
        


        [TestMethod]
        [DataRow(EnumMachineID.MID_DRAWER_01_02)]
        //[DataRow(EnumMachineID.MID_DRAWER_04_02)]
        public void SystemBootupInitial(EnumMachineID machineID)
        {
            // DataRow
           
            var machine = MacMsCabinet.GetMacMsCabinetDrawer(machineID, DicStateMachines);
            var method= typeof(MacMsCabinetDrawer).GetMethod(EnumMacMcCabinetDrawerCmd.SystemBootupInitial.ToString());
            method.Invoke(machine,null);
            

                Repeat();
            
        }

        /// <summary>測試Load_MoveTrayToOut</summary>
        /// <remarks>
        /// 2020/10/19 OK
        /// </remarks>
        [TestMethod]
        [DataRow(EnumMachineID.MID_DRAWER_01_02)]
        //[DataRow(EnumMachineID.MID_DRAWER_04_02)]
        public void Load_MoveTrayToOut(EnumMachineID machineID)
        {
           var machine = MacMsCabinet.GetMacMsCabinetDrawer(machineID, DicStateMachines);
           var method= typeof(MacMsCabinetDrawer).GetMethod(EnumMacMcCabinetDrawerCmd.Load_MoveTrayToOut.ToString());
           method.Invoke(machine, null);
           Repeat();
            

        }

        /// <summary>測試 Load_MoveTrayToHome 指令</summary>
        /// <remarks>
        /// 没有盒子: 2020/10/19 OK
        ///   有盒子: 2020/10/19 OK
        /// </remarks>
        [TestMethod]
        [DataRow(EnumMachineID.MID_DRAWER_01_02)]
        //[DataRow(EnumMachineID.MID_DRAWER_04_02)]
        public void Load_MoveTrayToHome(EnumMachineID machineID)
        {

            var machine = MacMsCabinet.GetMacMsCabinetDrawer(machineID, DicStateMachines);
            var method = typeof(MacMsCabinetDrawer).GetMethod(EnumMacMcCabinetDrawerCmd.Load_MoveTrayToHome.ToString());
            method.Invoke(machine, null);
            Repeat();

         
        }

      


        /// <summary>測試 Load_MoveTrayToIn 指令</summary>
        /// <remarks>
        /// 2020/10/19 OK
        /// </remarks>
        [TestMethod]
        [DataRow(EnumMachineID.MID_DRAWER_01_02)]
        public void Load_MoveTrayToIn(EnumMachineID machineID)
        {
            var machine = MacMsCabinet.GetMacMsCabinetDrawer(machineID, DicStateMachines);
            var method = typeof(MacMsCabinetDrawer).GetMethod(EnumMacMcCabinetDrawerCmd.Load_MoveTrayToIn.ToString());
            method.Invoke(machine, null);
            Repeat();
        }

        /// <summary>測試 MoveTrayToHomeWaitingUnloadInstruction 指令</summary>
        /// <remarks>
        /// 2020/10/19 OK
        /// </remarks>
        [TestMethod]
        public void MoveTrayToHomeWaitingUnloadInstruction(/*EnumMachineID machineID*/)
        {
            // DataRow
            EnumMachineID machineID1 = EnumMachineID.MID_DRAWER_01_01;
            EnumMachineID machineID2 = EnumMachineID.MID_DRAWER_01_02;
            var machine1 = MacMsCabinet.GetMacMsCabinetDrawer(machineID1, DicStateMachines);
            var machine2 = MacMsCabinet.GetMacMsCabinetDrawer(machineID2, DicStateMachines);


            machine1.MoveTrayToHomeWaitingUnloadInstruction();
            machine2.MoveTrayToHomeWaitingUnloadInstruction();
            Repeat();
        }


        /// <summary>測試 Unload_MoveTrayToIn() 指令</summary>
        /// <remarks>
        /// 2020/10/19 OK
        /// </remarks>
        [TestMethod]
        public void Unload_MoveTrayToIn()
        {
            // DataRow
            EnumMachineID machineID1 = EnumMachineID.MID_DRAWER_01_01;
            EnumMachineID machineID2 = EnumMachineID.MID_DRAWER_01_02;
            var machine1 = MacMsCabinet.GetMacMsCabinetDrawer(machineID1, DicStateMachines);
            var machine2 = MacMsCabinet.GetMacMsCabinetDrawer(machineID2, DicStateMachines);


            machine1.Unload_MoveTrayToIn();
            machine2.Unload_MoveTrayToIn();

            Repeat();
        }

        /// <summary>測試 Unload_MoveTrayToHome()</summary>
        /// <remarks>
        ///   有盒子:2020/10/19 OK
        /// 没有盒子: 2020/10/19 OK
        /// </remarks>
        [TestMethod]
        public void Unload_MoveTrayToHome()
        {
            // DataRow
            EnumMachineID machineID2 = EnumMachineID.MID_DRAWER_01_02;
            EnumMachineID machineID1 = EnumMachineID.MID_DRAWER_01_01;

            var machine1 = MacMsCabinet.GetMacMsCabinetDrawer(machineID1, DicStateMachines);
            var machine2 = MacMsCabinet.GetMacMsCabinetDrawer(machineID2, DicStateMachines);

            machine1.Unload_MoveTrayToHome();
            machine2.Unload_MoveTrayToHome();

            Repeat();
        }


        /// <summary>測試 MoveTrayToHomeWaitingLoadInstruction() 指令</summary>
        /// <remarks>
        /// 2020/10/19 OK
        /// </remarks>
        [TestMethod]
       public void MoveTrayToHomeWaitingLoadInstruction()
        {
            // DataRow
            EnumMachineID machineID1 = EnumMachineID.MID_DRAWER_01_01;
            EnumMachineID machineID2= EnumMachineID.MID_DRAWER_01_02;

            var machine1 = MacMsCabinet.GetMacMsCabinetDrawer(machineID1, DicStateMachines);
            var machine2 = MacMsCabinet.GetMacMsCabinetDrawer(machineID2, DicStateMachines);


            machine1.MoveTrayToHomeWaitingLoadInstruction();
             machine2.MoveTrayToHomeWaitingLoadInstruction();

            Repeat();
        }

    }
}
