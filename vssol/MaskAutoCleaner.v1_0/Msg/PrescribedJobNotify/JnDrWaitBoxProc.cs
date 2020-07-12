using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Msg.PrescribedJobNotify
{
    public class JnDrawerReg : PrescribedJobNotifyBase
    {
        public string MachineID;
    }
    public class JnDrWaitBoxProc : PrescribedJobNotifyBase
    {
        public List<BoxInfo> BoxInfoList;
    }
    public class JnDrBoxProcReq : PrescribedJobNotifyBase
    {
        public BoxInfo boxInfo;
    }

    public class BoxInfo
    {
        bool isProcessed = false;
        public bool IsProcessed
        {
            get { return isProcessed; }
            set { isProcessed = value; }
        }

        bool isBoxOpened = false;

        string barCode = String.Empty;
        public string BarCode
        {
            get { return barCode; }
            set { barCode = value; }
        }

        private BoxProcessStatus processStatus;
        public BoxProcessStatus ProcessStatus
        {
            get { return processStatus; }
            set { processStatus = value; }
        }

        private string drawerNo;
        public string DrawerNo
        {
            get{return drawerNo;}
        }
        public EnumBoxPositon boxPosition;
        public EnumBoxLocker boxLocker;
        public EnumBoxType boxType;

        private EnumBoxStatus currentStatus = EnumBoxStatus.Initial;
        public bool hasMask()
        {
            return currentStatus == EnumBoxStatus.MaskInbox ? true : false;
        }

        public BoxInfo()
        {
            drawerNo = null;
            this.isProcessed = false;
            this.isBoxOpened = false;
            boxPosition = EnumBoxPositon.DrawerSlot;
        }
        //20190828 mht 把SLOT的位置資訊紀錄到BOXINFO上
        public BoxInfo(string dno, bool hasMask = false)
        {
            if (hasMask)
            {
                currentStatus = EnumBoxStatus.MaskInbox;
            }
            else
            {
                currentStatus = EnumBoxStatus.Empty;
            }
            drawerNo = dno;
            this.isProcessed = false;
            this.isBoxOpened = false;
            boxPosition = EnumBoxPositon.DrawerSlot;
        }

        
    }
}
