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
        public void Initial()
        {

            var machine = new v1_0.Machine.Drawer.MacMsDrawer();
            //var drawer = machine.HalDrawer;
            machine.LoadStateMachine();
           machine.InitialFromAnyState();
         }




        [TestMethod]
        public void Load_MoveTrayToPositionOut()
        {
            var machine = new v1_0.Machine.Drawer.MacMsDrawer();
            //var drawer = machine.HalDrawer;
            machine.LoadStateMachine();
            machine.Load_MoveTrayToPositionOutFromAnywhere();
        }
       

        [TestMethod]
        public void Load_MoveTrayToPositionIn()
        {
            var machine = new v1_0.Machine.Drawer.MacMsDrawer();
           // var drawer = machine.HalDrawer;
            machine.LoadStateMachine();
            machine.Load_MoveTrayToPositionInFromPositionOut();
        }

        [TestMethod]
        public void Unload_MoveTrayToPositionIn()
        {
            var machine = new v1_0.Machine.Drawer.MacMsDrawer();
            //var drawer = machine.HalDrawer;
            machine.LoadStateMachine();
            machine.Unload_MoveTrayToPositionInFromAnywhere();
        }


        [TestMethod]
        public void Unload_MoveTrayToPositionOut()
        {
            var machine = new v1_0.Machine.Drawer.MacMsDrawer();
            //var drawer = machine.HalDrawer;
            machine.LoadStateMachine();
            machine.Unload_MoveTrayToPositionOutFromPositionIn();
        }











    }





}
