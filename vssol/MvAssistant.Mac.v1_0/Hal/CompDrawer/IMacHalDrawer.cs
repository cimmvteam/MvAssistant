using MvAssistant.DeviceDrive.KjMachineDrawer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.CompDrawer
{
    public interface IMacHalDrawer: IMacHalComponent
    {
       
    }

    public class Drawer : IMacHalDrawer
    {

        public MvKjMachineDrawerLdd DrawerLdd { get; set; }
       
        public Drawer(int cabinetNO, string drawerNO, IPEndPoint deviceEndpoint, string localIp, IDictionary<int, bool?> portTable)
        {
            DrawerLdd = new MvKjMachineDrawerLdd(cabinetNO, drawerNO, deviceEndpoint, localIp, portTable); 
        }
        public int HalClose()
        {
            throw new NotImplementedException();
        }

        public int HalConnect()
        {
            throw new NotImplementedException();
        }

        public bool HalIsConnected()
        {
            throw new NotImplementedException();
        }

        public int HalStop()
        {
            throw new NotImplementedException();
        }
    }
}
