using MvAssistant.DeviceDrive.KjMachineDrawer;
using MvAssistant.DeviceDrive.KjMachineDrawer.ReplyCode;
using MvAssistant.DeviceDrive.KjMachineDrawer.UDPCommand.HostToEquipment;
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
        object Tag { get; set; }
        string Index { get; set; }
#region command
        string DeviceIP { get; set; }
        string CommandINI();
        void  INIResult(object sender,bool result);

        string CommandSetMotionSpeed(int speed);
        void SetMotionSpeedResult(object sender, bool result);

        string CommandSetTimeOut(int timeoutSeconds);
        void SetTimeOutResult(object sender, bool result);

        string CommandTrayMotionHome();
       // void TrayMotionHomeResult(object sender, bool result);

        string CommandTrayMotionOut();
        //void TrayMotionOutResult(object sender, bool result);

        string CommandTrayMotionIn();
        //void TrayMotionInResult(object sender, bool result);
       
        string CommandBrightLEDAllOn();
        string CommandBrightLEDAllOff();
        string CommandBrightLEDGreenOn();
        string CommandBrightLEDRedOn();
        void BrightLEDResult(object sender, bool result);
        

        string CommandPositionRead();
        void PositionReadResult(object sender, string result);

        string CommandBoxDetection();
        void BoxDetectionResult(object sender, bool result);
        event EventHandler OnBoxDetectionResultHandler;


        string CommandWriteNetSetting();
        string CommandLCDMsg(string message);

        string CommandSetParameterHomePosition(string homePosition);
        string CommandSetParameterOutSidePosition(string outsidePosition);
        string CommandSetParameterInSidePosition(string insidePosition);
        string CommandSetParameterIPAddress(string ipAddress);
        string CommandSetParameterSubMask(string submaskAddress);
        #endregion
        
    }
    /**
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
    }*/
}
