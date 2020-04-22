using MaskAutoCleaner.Hal.Intf;
using MaskAutoCleaner.Hal.Intf.Component.Robot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Hal.Imp.Component.Robot
{

    [GuidAttribute("C3782713-449C-4FC2-830C-5D8F0B48C35D")]
    public class HalRobotStaubli : HalComponentBase, IHalRobot
    {

        int IHal.HalConnect()
        {
            throw new NotImplementedException();
        }

        int IHal.HalClose()
        {
            throw new NotImplementedException();
        }


        bool IHal.HalIsConnected()
        {
            throw new NotImplementedException();
        }



        int IHalRobot.HalReset()
        {
            throw new NotImplementedException();
        }

        int IHalRobot.HalStartProgram(string name)
        {
            throw new NotImplementedException();
        }

        int IHalRobot.HalStopProgram()
        {
            throw new NotImplementedException();
        }




        int IHalRobot.HalMoveAsyn()
        {
            throw new NotImplementedException();
        }







        int IHalRobot.HalMoveStraightAsyn(HalRobotMotion motion)
        {
            throw new NotImplementedException();
        }


        int IHalRobot.HalAlarm()
        {
            throw new NotImplementedException();
        }


        bool IHalRobot.HalMoveIsComplete()
        {
            throw new NotImplementedException();
        }

        int IHalRobot.HalMoveEnd()
        {
            throw new NotImplementedException();
        }


        HalRobotMotion IHalRobot.HalGetPose()
        {
            throw new NotImplementedException();
        }

        public string ID
        {
            get;
            set;
        }

        public string DeviceConnStr
        {
            get;
            set;
        }


        public float HalDistanceWithCurr(HalRobotPose pose)
        {
            throw new NotImplementedException();
        }


        public int HalSysRecover()
        {
            throw new NotImplementedException();
        }
    }
}
