using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.DeviceDrive.GudengLoadPort;

namespace MvAssistant.Mac.TestMy.Device
{
    [TestClass]
    public class UtDeviceLoadPort
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (var loadport = new MvGudengLoadPortLdd())
            {
                loadport.ConnectIfNo();


            }

        }
    }
}
