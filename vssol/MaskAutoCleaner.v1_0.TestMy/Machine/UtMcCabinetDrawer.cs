using MaskAutoCleaner.v1_0.Machine.CabinetDrawer;
using MaskAutoCleaner.v1_0.Machine.Drawer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.TestMy.Machine
{
    [TestClass]
    public   class UtMcCabinetDrawer
    {
        MacMsCabinetDrawer _machine { get; set; }
        public UtMcCabinetDrawer()
        {
            _machine = new MacMsCabinetDrawer();
        }

        [TestMethod]
        public void SystemBootup()
        {
            _machine.SystemBootup();
        }

        [TestMethod]
        public void SystemBootupInitial()
        {
            _machine.SystemBootupInitial();
        }

        [TestMethod]
        public void Load_MoveTrayToOut()
        {
            _machine.Load_MoveTrayToOut();
        }

        [TestMethod]
        public void Load_MoveTrayToHome()
        {
            _machine.Load_MoveTrayToHome();
        }

        [TestMethod]
        public void Load_MoveTrayToIn()
        {
            _machine.Load_MoveTrayToIn();
        }

        [TestMethod]
        public void MoveTrayToHomeWaitingUnloadInstruction()
        {
            _machine.MoveTrayToHomeWaitingUnloadInstruction();
        }

        [TestMethod]
        public void Unload_MoveTrayToIn()
        {
            _machine.Unload_MoveTrayToIn();
        }

        [TestMethod]
        public void Unload_MoveTrayToHome()
        {
            _machine.Unload_MoveTrayToHome();
        }

        [TestMethod]
        public void MoveTrayToHomeWaitingLoadInstruction()
        {
            _machine.MoveTrayToHomeWaitingLoadInstruction();
        }

    }
}
