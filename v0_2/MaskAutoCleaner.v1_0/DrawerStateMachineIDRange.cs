using MvAssistant.v0_2.Mac.JSon.RobotTransferFile;
using MvAssistant.v0_2.Mac.Manifest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0
{
 
    public class DrawerStateMachineIDRange
    {
        EnumMachineID _startID;
        EnumMachineID _endID;
        public DrawerStateMachineIDRange(EnumMachineID start, EnumMachineID end)
        {
             _startID=start;
            _endID = end;
        }
        public EnumMachineID StartID{ get {  return _startID; } }
        public EnumMachineID EndID { get {return _endID; } }
        
    }
}
