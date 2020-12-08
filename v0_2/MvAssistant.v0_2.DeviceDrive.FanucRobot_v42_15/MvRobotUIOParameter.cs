using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.DeviceDrive.FanucRobot_v42_15
{

    public class MvRobotUIOParameter
    {
        public short UI1_IMSTP = 1;                          //Always ON.ON:Normal, OFF:Emergent Stop
        public short UI2_HOLD = 1;                           //Always ON.ON:Normal, OFF:PAUSE
        public short UI3_SFSPD = 1;                          //Always ON,ON:Normal, OFF:Safty Speed
        public short UI4_CycleStop = 0;                      //ON:[True]Abort, OFF: NA
        public short UI5_FaultReset_NEGEDGE = 1;             //NegtiveEdge trigger(1 -> 0)
        public short UI6_Start_NEGEDGE_NoUsed = 1;
        public short UI7_Home_NoUSed = 0;
        public short UI8_ENABLE = 1;                         //Always ON,ON:ENavle Robot Action, OFF:Otherwise
        public Array UI9to16_PNS = new short[8] { 0, 0, 0, 0, 0, 0, 0, 0 };   //8bit PNS selection
        public short UI17_PNStrobe_POSEDGE = 0;              //PositiveEdge trigger(0 -> 1)
        public short UI18_ProdStart = 1;                     //ON:PNS codestrat, OFF :NA
        public Array UO3_ProgRunnungValue = new short[20];
    }
}
