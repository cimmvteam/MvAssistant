using MaskAutoCleaner.v1_0.Machine.Cabinet.DrawerStatus;
using MvAssistant.Mac.v1_0;
using MvAssistant.Mac.v1_0.JSon.RobotTransferFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.Cabinet.DrawerQueues
{
    /// <summary>Maxk Box Infomation</summary>
    /// <remarks>
    /// <para>2020/11/26 King Liu[C]</para>
    /// </remarks>
    public class DrawerSatusInfo
    {
        public string BoxBarcode { get; set; }
        public BoxType BoxType { get; set; }
        public EnumMachineID DrawerMachineID { get; set; }
        public string SN { get; set; }
        public DrawerDuration DrawerStatus{get;set;}
        public BoxrobotTransferLocation? DrawerLocation
        {
            get
            {
                var feedBack= DrawerMachineID.ToBoxrobotTransferLocationForDrawer();
                if (feedBack.Item1)
                {
                    return feedBack.Item2; 
                }
                else
                {
                    return default(BoxrobotTransferLocation?);
                }

            }
        }
        public  DrawerSatusInfo(string boxBarcode, EnumMachineID machineID,BoxType boxType)
        {
            BoxBarcode = boxBarcode;
            DrawerMachineID = machineID;
            BoxType=boxType;
            DateTime dateTime = DateTime.Now;
            // SN = dateTime.Year.ToString("0000") + dateTime.Month.ToString("00") + dateTime.Day.ToString("00") +  dateTime.Hour.ToString("00") + dateTime.Minute.ToString("00") + dateTime.Second.ToString("00") + "_" + boxBarcode ;
            SN = dateTime.ToString("yyyyMMddHHmmssfff");
            DrawerStatus = DrawerDuration.Idle_TrayAtHome;
        }

        public DrawerSatusInfo(string boxBarcode, EnumMachineID machineID, BoxType boxType,DrawerDuration drawerStaus):this(boxBarcode, machineID,boxType)
        {

            DrawerStatus = drawerStaus;
        }

        public void SetDrawerState(DrawerDuration drawerStatus)
        {
            DrawerStatus = drawerStatus;
        }  
    }

  
}
