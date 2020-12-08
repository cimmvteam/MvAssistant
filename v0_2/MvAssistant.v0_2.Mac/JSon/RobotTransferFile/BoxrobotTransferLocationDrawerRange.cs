using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.Mac.JSon.RobotTransferFile
{
    public class BoxrobotTransferLocationDrawerRange
    {
        public BoxrobotTransferLocation Start { get; }
        public BoxrobotTransferLocation End { get; }
        public BoxrobotTransferLocation Cabinet2Start { get; }
        public BoxrobotTransferLocation Cabinet1End { get; }
        public BoxrobotTransferLocation Cabinet1Start { get { return Start; } }
        public BoxrobotTransferLocation Cabinet2End { get { return End; } }
        public BoxrobotTransferLocationDrawerRange()
        {
            Start = BoxrobotTransferLocation.Drawer_01_01;
            End = BoxrobotTransferLocation.Drawer_07_05;
            Cabinet1End = BoxrobotTransferLocation.Drawer_03_05;
            Cabinet2Start = BoxrobotTransferLocation.Drawer_04_01;
        }
    }
}
