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
        public void TestInitial()
        {

            var machine = new v1_0.Machine.Cabinet.MacMsDrawer();
            var drawer = machine.HalDrawer;
            machine.LoadStateMachine();
            machine.Initial();
         }




        [TestMethod]
        public void TestLoadPreWork1()
        {
            var machine = new v1_0.Machine.Cabinet.MacMsDrawer();
            var drawer = machine.HalDrawer;
            machine.LoadStateMachine();
            machine.Load_TrayGotoIn();
        }
        [TestMethod]
        public void TestLoadPreWork2()
        {

        }

        [TestMethod]
        public void TestLoadMain()
        {
            var machine = new v1_0.Machine.Cabinet.MacMsDrawer();
            var drawer = machine.HalDrawer;
            machine.LoadStateMachine();
            machine.Load_TrayGotoOut();
        }

        [TestMethod]
        public void TestUnloadPreWork1()
        {
            var machine = new v1_0.Machine.Cabinet.MacMsDrawer();
            var drawer = machine.HalDrawer;
            machine.LoadStateMachine();
            machine.Unload_TrayGotoOut();
        }














    }





}
