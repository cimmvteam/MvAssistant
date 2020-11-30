using MvAssistant.Mac.v1_0;
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
        public DrawerStaus DrawerStatus{get;set;}
        
        public  DrawerSatusInfo(string boxBarcode, EnumMachineID machineID,BoxType boxType)
        {
            BoxBarcode = boxBarcode;
            DrawerMachineID = machineID;
            BoxType=boxType;
            DateTime dateTime = DateTime.Now;
            // SN = dateTime.Year.ToString("0000") + dateTime.Month.ToString("00") + dateTime.Day.ToString("00") +  dateTime.Hour.ToString("00") + dateTime.Minute.ToString("00") + dateTime.Second.ToString("00") + "_" + boxBarcode ;
            SN = dateTime.ToString("yyyyMMddHHmmssfff");
            DrawerStatus = DrawerStaus.Idle_TrayAtHome;
        }

        public DrawerSatusInfo(string boxBarcode, EnumMachineID machineID, BoxType boxType,DrawerStaus drawerStaus):this(boxBarcode, machineID,boxType)
        {

            DrawerStatus = drawerStaus;
        }

        public void SetDrawerState(DrawerStaus drawerStatus)
        {
            DrawerStatus = drawerStatus;
        }  
    }

    public enum DrawerStaus
    {
        /// <summary>Tray 在Home 點Idle</summary>
        Idle_TrayAtHome,
        
        /// <summary>Bank out, Load, Tray 移到 Out等待放置盒子 </summary>
        BankOut_Load_TrayAtOutForPutBoxOnTray,
        
        /// <summary>Bnak Out, Load, Tray 在 Home,己經放上盒子 </summary>
        BankOut_Load_TrayAtHomeWithBox,
        
        /// <summary>Bnak Out, Load, Tray 在 In, 等待 Robot 取走Box </summary>
        BankOut_Load_TrayAtInWithBoxForRobotClampBox,

        /// <summary>Bankout , Load, Tray 在In , Tray 上没有盒子 </summary>
        BankOut_Load_TrayAtInNoBox,

        /// <summary>Bank Out, Load, Tray 在Home, 没有盒子(等待 Unload)</summary>
        BankOut_Load_TrayAtHomeNoBox,

        /// <summary>Bank out, Unload, Tray 移到 In, 没有盒子(等待放盒子上去)</summary>
        BankOut_UnLoad_TrayAtInNoBox,

        /// <summary>Bank out, Unload, Tray 移到 In, 盒子已經放在上面 </summary>
        BankOut_UnLoad_TrayAtInWithBox,

        /// <summary>Bnak Out, Unload, Tray 移到Home </summary>
        BankOut_UnLoad_TrayAtHomeWithBox,

        /// <summary>Bank Out, Unload,  Tray 移到 Out (等待盒子被取走)   =>  Idle_TrayAtHome</summary>
        BankOut_UnLoad_TrayAtOutWithBox,


    }
}
