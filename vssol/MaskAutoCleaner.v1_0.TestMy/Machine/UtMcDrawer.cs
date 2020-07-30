using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.DeviceDrive.KjMachineDrawer.DrawerEventArgs;
using MvAssistant.Mac.v1_0.Hal.CompDrawer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.TestMy.Machine
{

    [TestClass]
    public   class UtMcDrawer
    {

        [TestMethod]
        public void Test_Initial()
        {

            var machine = new v1_0.Machine.Cabinet.MacMsDrawer();
            var drawer = machine.HalDrawer;
            machine.LoadStateMachine();
            machine.Initial();
         }




        [TestMethod]
        public void Test_Load_TrayGotoIn()
        {
            var machine = new v1_0.Machine.Cabinet.MacMsDrawer();
            var drawer = machine.HalDrawer;
            machine.LoadStateMachine();
            machine.Load_TrayGotoIn();
        }
       

        [TestMethod]
        public void Test_Load_TrayGotoOut()
        {
            var machine = new v1_0.Machine.Cabinet.MacMsDrawer();
            var drawer = machine.HalDrawer;
            machine.LoadStateMachine();
            machine.Load_TrayGotoOut();
        }

        [TestMethod]
        public void Test_Unload_TrayGotoOut()
        {
            var machine = new v1_0.Machine.Cabinet.MacMsDrawer();
            var drawer = machine.HalDrawer;
            machine.LoadStateMachine();
            machine.Unload_TrayGotoOut();
        }


        [TestMethod]
        public void Test_Unload_TrayGotoIn()
        {
            var machine = new v1_0.Machine.Cabinet.MacMsDrawer();
            var drawer = machine.HalDrawer;
            machine.LoadStateMachine();
            machine.Unload_TrayGotoIn();
        }











    }





}
