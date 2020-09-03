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
            return new List<MacMsCabinetDrawer>();
        }

        public UtMcCabinet()
        {
            _cabinetDrawers = GetCabinetDrawers();
            _machine = new MacMsCabinet(_cabinetDrawers);
            _machine.LoadStateMachine();
           
        }

         [TestMethod]
        public void TestLoad()
        {
            
        }
    }
}
