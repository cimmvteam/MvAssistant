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
        IList<MacMsCabinetDrawer> _cabinetDrawers { get; set; }
        private IList<MacMsCabinetDrawer> GetCabinetDrawers()
        {
            // TODO: 去得到所有的Cabinet Drawer
            // 先以 null 暫時.....
            //return new List<MacMsCabinetDrawer>();
            return null;
        }

        public UtMcCabinet()
        {
            _cabinetDrawers = GetCabinetDrawers();
            _machine = new MacMsCabinet(_cabinetDrawers);
            _machine.LoadStateMachine();
           
        }

         [TestMethod]
        public void LoadDrawers()
        {
            int drawers = 30;// 要 執行Load 的數量 
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
