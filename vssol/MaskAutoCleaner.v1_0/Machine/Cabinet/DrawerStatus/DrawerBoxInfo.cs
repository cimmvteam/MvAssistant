using MvAssistant.Mac.v1_0;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.CompDrawer;
using MvAssistant.Mac.v1_0.JSon.RobotTransferFile;
using MvAssistant.Mac.v1_0.Manifest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.Cabinet.DrawerStatus
{
   public  class DrawerBoxInfo
    {
        //MacHalAssemblyBase Assembly { get; set; }

        public MacEnumDevice DeviceID { get;  }
        public string MaskBarCode { get; private set;}
        public IMacHalDrawer Drawer { get; }
        public DrawerDuration Duration { get; private set; }
        public BoxrobotTransferLocation BoxrobotTransferLocation { get { return DeviceID.ToBoxrobotTransferLocation(); } }
        public BoxType BoxType { get; private set; }  

        public DateTime LastTime { get; private set; }
        private DrawerBoxInfo()
        {
            Duration = DrawerDuration.Idle_TrayAtHome;
            BoxType = BoxType.DontCare;
        }
        public DrawerBoxInfo(MacEnumDevice deviceID, IMacHalDrawer drawer):this()
        {
            Drawer = drawer;
            UpdateLastTime();
        }
        public void SetBoxType(BoxType boxType)
        {
            this.BoxType = boxType;
        }

        public void SetMaskBarCode(string barcode)
        {
            MaskBarCode = barcode;
            UpdateLastTime();
        }
        public void ResetBarcode()
        {
            MaskBarCode = string.Empty;
            UpdateLastTime();
        }
        
        public void SetDuration(DrawerDuration duration)
        {
            Duration = duration;
            UpdateLastTime();
        }
        public void ResetDuration()
        {
            SetDuration(DrawerDuration.Idle_TrayAtHome);
            UpdateLastTime();
        }
        private void UpdateLastTime()
        {
            LastTime = DateTime.Now;
        }

   }

    public static class DrawerBoxInfoExtends
    {
        public static BoxrobotTransferLocation? GetBoxrobotTransferLocation(this Dictionary<BoxrobotTransferLocation, DrawerBoxInfo> inst,IMacHalDrawer drawer)
        {
            var feedBack = inst.Where(m => m.Value.Drawer == drawer).ToList();
            if (feedBack.Count !=0)
            {
                return feedBack[0].Key;
            }
            else
            {
                return default(BoxrobotTransferLocation?);
            }
        }

        public static KeyValuePair<BoxrobotTransferLocation, DrawerBoxInfo>  GetKeyValue(this Dictionary<BoxrobotTransferLocation, DrawerBoxInfo> inst, IMacHalDrawer drawer)
        {
            var feedBack = inst.Where(m => m.Value.Drawer == drawer).ToList();
            if (feedBack.Count != 0)
            {
                return feedBack[0];
            }
            else
            {
                return default(KeyValuePair<BoxrobotTransferLocation, DrawerBoxInfo>);
            }
        }

        public static KeyValuePair<BoxrobotTransferLocation, DrawerBoxInfo> GetKeyValue(this Dictionary<BoxrobotTransferLocation, DrawerBoxInfo> inst, BoxrobotTransferLocation location)
        {
            if (inst.ContainsKey(location))
            {
                var feedBack = inst.Where(m => m.Key == location).First();
                return feedBack;
            }
            else
            {
                return default(KeyValuePair<BoxrobotTransferLocation, DrawerBoxInfo>);
            }
        }
    }

}
