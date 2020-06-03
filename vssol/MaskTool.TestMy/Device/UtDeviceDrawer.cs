using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.DeviceDrive.KjMachineDrawer;

namespace MvAssistant.Mac.TestMy.Device
{
    [TestClass]
    public class UtDeviceDrawer
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (var drawer = new MvKjMachineDrawerLdd())
            {
                drawer.ConnectIfNo();


            }

        }
    }
}
