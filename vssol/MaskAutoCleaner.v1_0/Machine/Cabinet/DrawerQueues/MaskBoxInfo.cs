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
    public class MaskBoxInfo
    {
        public string BoxBarcode { get;  set;}
        public BoxType BoxType { get; set; }
        public EnumMachineID DrawerMachineID { get; set; }
        public string SN { get; set; } 
        public  MaskBoxInfo(string boxBarcode, EnumMachineID machineID,BoxType boxType)
        {
            BoxBarcode = boxBarcode;
            DrawerMachineID = machineID;
            BoxType=boxType;
            DateTime dateTime = DateTime.Now;
            // SN = dateTime.Year.ToString("0000") + dateTime.Month.ToString("00") + dateTime.Day.ToString("00") +  dateTime.Hour.ToString("00") + dateTime.Minute.ToString("00") + dateTime.Second.ToString("00") + "_" + boxBarcode ;
            SN = dateTime.ToString("yyyyMMddHHmmssfff");    
        }
    }
}
