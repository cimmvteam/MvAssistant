using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Msg.JobNotify
{
    public class JnDrawerReg : MacJobNotifyBase
    {
        public string MachineID;
    }
    public class MacJnDrWaitBoxProc : MacJobNotifyBase
    {
        public List<BoxInfo> BoxInfoList;
    }
    public class JnDrBoxProcReq : MacJobNotifyBase
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

        private EnumMacBoxProcessStatus processStatus;
        public EnumMacBoxProcessStatus ProcessStatus
        {
            get { return processStatus; }
            set { processStatus = value; }
        }

        private string drawerNo;
        public string DrawerNo
        {
            get{return drawerNo;}
        }
        public EnumMacBoxPositon boxPosition;
        public EnumMacBoxLocker boxLocker;
        public EnumMacBoxType boxType;

        private EnumMacBoxStatus currentStatus = EnumMacBoxStatus.Initial;
        public bool hasMask()
        {
            return currentStatus == EnumMacBoxStatus.MaskInbox ? true : false;
        }

        public BoxInfo()
        {
            drawerNo = null;
            this.isProcessed = false;
            this.isBoxOpened = false;
            boxPosition = EnumMacBoxPositon.DrawerSlot;
        }
        //20190828 mht 把SLOT的位置資訊紀錄到BOXINFO上
        public BoxInfo(string dno, bool hasMask = false)
        {
            if (hasMask)
            {
                currentStatus = EnumMacBoxStatus.MaskInbox;
            }
            else
            {
                currentStatus = EnumMacBoxStatus.Empty;
            }
            drawerNo = dno;
            this.isProcessed = false;
            this.isBoxOpened = false;
            boxPosition = EnumMacBoxPositon.DrawerSlot;
        }

        
    }
}
