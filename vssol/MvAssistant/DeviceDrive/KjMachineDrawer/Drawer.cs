using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.KjMachineDrawer
{
    public class Drawer
    {

        public int CabinetNO { get; private set; }
        public int DrawerNO { get; private set; }
        public string UDPServerIP { get; private set; }
        public DrawerSocket DrawerSocket { get; private set; }

        private Drawer() { }
        public Drawer(int cabinetNO, int drawerNO, string udpServerIP, int udpServerPort) : this()
        {
            DrawerNO = drawerNO;
            CabinetNO = cabinetNO;
            UDPServerIP = udpServerIP;
            DrawerSocket = new DrawerSocket(udpServerIP, udpServerPort);
        }
        public int SentTo(string message)
        {
            var feedBack = DrawerSocket.SentTo(message);
            return feedBack;
        }
        
    }
}
