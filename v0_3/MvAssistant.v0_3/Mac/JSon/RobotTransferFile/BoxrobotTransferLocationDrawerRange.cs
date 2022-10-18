using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.Mac.JSon.RobotTransferFile
{
    public class BoxrobotTransferLocationDrawerRange
    {
        public BoxrobotTransferLocation Start { get; private set; }
        public BoxrobotTransferLocation End { get; private set; }
        public BoxrobotTransferLocation Cabinet2Start { get; private set; }
        public BoxrobotTransferLocation Cabinet1End { get; private set; }
        public BoxrobotTransferLocation Cabinet1Start { get { return Start; } private set { this.Start = value; } }
        public BoxrobotTransferLocation Cabinet2End { get { return End; } private set { this.End = value; } }
        public BoxrobotTransferLocationDrawerRange()
        {
            Start = BoxrobotTransferLocation.Drawer_01_01;
            End = BoxrobotTransferLocation.Drawer_07_05;
            Cabinet1End = BoxrobotTransferLocation.Drawer_03_05;
            Cabinet2Start = BoxrobotTransferLocation.Drawer_04_01;
        }
    }
}
