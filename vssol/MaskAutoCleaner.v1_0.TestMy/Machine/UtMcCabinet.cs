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
           
        }

         [TestMethod]
        public void LoadDrawers()
        {
            int drawers = 20;// 要 執行Load 的數量 
            _machine.LoadDrawers(drawers);
        }
        [TestMethod]
        public void BootupInitialDrawers()
        {
            _machine.BootupInitialDrawers();
        }

        [TestMethod]
        public void SynchrousDrawerStates()
        {
            _machine.SynchrousDrawerStates();
        }
    }
}
