using System;
using System.Net;
using System.Net.Sockets;
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


        [TestMethod]
        public void TestUdpClient()
        {
        


        }

        [TestMethod]
        public void TestUdpSocket()
        {
            using (var client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {

            }

        }


    }
}
