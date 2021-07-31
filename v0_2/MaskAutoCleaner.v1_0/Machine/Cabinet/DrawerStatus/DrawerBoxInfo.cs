using MvAssistant.v0_2.Mac;
using MvAssistant.v0_2.Mac.Hal;
using MvAssistant.v0_2.Mac.Hal.CompDrawer;
using MvAssistant.v0_2.Mac.JSon.RobotTransferFile;
using MvAssistant.v0_2.Mac.Manifest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.Cabinet.DrawerStatus
{
    /*
     * /// <summary></summary>
     */

    public class DrawerBoxInfo
    {
        //MacHalAssemblyBase Assembly { get; set; }

        public EnumMacDeviceId DeviceID { get;  }
        public string MaskBarCode { get; private set;}

        /// <summary> Drawer 裝置實體 </summary>
        public IMacHalDrawer Drawer { get; }

        /// <summary> Drawer 裝置實體 </summary>
        public DrawerDuration Duration { get; private set; }

        /// <summary>Drawer 工作期間</summary>
        public BoxrobotTransferLocation BoxrobotTransferLocation { get { return DeviceID.ToBoxrobotTransferLocation(); } }

        /// <summary>盒子種類</summary>
        public EnumMacMaskBoxType BoxType { get; private set; }

        /// <summary>開放 Drawer 工作</summary>
        public bool DrawerAbled { get; private set; }

        /// <summary>屬性更新時間</summary>
        public DateTime UpdateTime { get; private set; }

        /// <summary>停用 Drawer 裝置</summary>
        public void DisableDrawer()
        {
            DrawerAbled = false;
        }

        /// <summary>使用Drawer 裝置</summary>
        public void EnableDrawer()
        {
            DrawerAbled  = true;
        }

        /// <summary>Drawer 裝置是否可用 ?</summary>
        public bool IsDrawerAbled { get  {    return DrawerAbled;    }   }

        /// <summary>Constructor</summary>
        private DrawerBoxInfo()
        {
            Duration = DrawerDuration.Idle_TrayAtHome;
            BoxType = EnumMacMaskBoxType.DontCare;
            DrawerAbled = true;
        }

        /// <summary>Constructor</summary>
        /// <param name="deviceID">Drawer Device ID</param>
        /// <param name="drawer">Drawer 裝置</param>
        public DrawerBoxInfo(EnumMacDeviceId deviceID, IMacHalDrawer drawer):this()
        {
            Drawer = drawer;
            UpdateLastTime();
        }

        /// <summary>設定 BoxType</summary>
        /// <param name="boxType">Box 種類 (鐵盒/水晶盒)</param>
        public void SetBoxType(EnumMacMaskBoxType boxType)
        {
            this.BoxType = boxType;
        }

        /// <summary>設定 Mask(Box) Barcode</summary>
        /// <param name="barcode">Box(Mask) BarCode</param>
        public void SetMaskBarCode(string barcode)
        {
            MaskBarCode = barcode;
            UpdateLastTime();
        }

        /// <summary>重設(清除) BarCode</summary>
        public void ResetBarcode()
        {
            MaskBarCode = string.Empty;
            UpdateLastTime();
        }

        /// <summary>設定工作階段</summary>
        /// <param name="duration">工作階段</param>
        /// <remarks></remarks>
        public void SetDuration(DrawerDuration duration)
        {
            Duration = duration;
            UpdateLastTime();
        }

        /// <summary>重設(清除)工作階段</summary>
        public void ResetDuration()
        {
            SetDuration(DrawerDuration.Idle_TrayAtHome);
            UpdateLastTime();
        }

        /// <summary>更新設定時間</summary>
        private void UpdateLastTime()
        {
            UpdateTime = DateTime.Now;
        }

   }

    public static class DrawerBoxInfoExtends
    {
        /// <summary>取得 處理 Boxrobot 路徑有關 的 Drawer 編號</summary>
        /// <param name="drawer">Drawer 裝置</param>
        /// <returns>BoxrobotTransferLocation 型別資料</returns>
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

        /// <summary>取得 Drawer的相對 Dictionary&lt;BoxrobotTransferLocation, DrawerBoxInfo&gt; 字典中的 Key Value Pair 資料 </summary>
        /// <param name="drawer">Drawer 裝置</param>
        /// <returns>KeyValuePair&lt;BoxrobotTransferLocation, DrawerBoxInfo&gt; 的型別資料</returns>
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
        /// <summary>取得 Drawer的相對 Dictionary&lt;BoxrobotTransferLocation, DrawerBoxInfo&gt; 字典中的 Key Value Pair 資料 </summary>
        /// <param name="location">處理 Boxrobot 路徑有關 的 Drawer 編號</param>
        /// <returns>KeyValuePair&lt;BoxrobotTransferLocation, DrawerBoxInfo&gt; 的型別資料</returns>
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
