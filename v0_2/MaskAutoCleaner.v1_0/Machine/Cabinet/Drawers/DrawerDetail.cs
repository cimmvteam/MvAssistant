using MvAssistant.v0_2.Mac.Hal.CompDrawer;
using MvAssistant.v0_2.Mac.JSon.RobotTransferFile;
using MvAssistant.v0_2.Mac.Manifest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.Cabinet.Drawers
{
   public class DrawerDetail
    {
        /// <summary>Drawer 實體</summary>
        public IMacHalDrawer HalDrawer { get;private set; }

        /// <summary>Box</summary>
        public BoxUnit BoxUnit { get; private set; }
      
        /// <summary>使用 Boxrobot 時, 描述往 Drawer路徑的 Drawer 代碼 </summary>
        public BoxrobotTransferLocation DrawerLocation { get { return DeviceID.ToBoxrobotTransferLocation(); } }
        /// <summary>在 Manifest 中定義的 裝置 ID </summary>
        public MacEnumDevice DeviceID { get; set; }

        public bool Enabled { get; private set; }

        /// <summary>目前有没有盒子</summary>
        public bool HasBox { get;private set; }

        public bool IsDrawerInitialing
        {
            get
            {
                return HalDrawer.IsInitialing;
            }

        }
       

        /// <summary>開始 Initial</summary>
        public void StartInitial()
        {
            HalDrawer.IsInitialing= true;
        }

        /// <summary>完成(取消) Initial</summary>
        public void StopInitial()
        {
            HalDrawer.IsInitialing = false;
        }

        

        /// <summary>Constructor</summary>
        public DrawerDetail(MacEnumDevice deviceID, IMacHalDrawer halDrawer)
        {
            HasBox = false;
            HalDrawer = halDrawer;
            BoxUnit = null;
            DeviceID = deviceID;
            EnableDrawer();
        }

        /// <summary>啟用 Drawer</summary>
        public void EnableDrawer()
        {
            Enabled = true;
        }

        /// <summary>停用 Drawer</summary>
        public void DisableDrawer()
        {
            Enabled = false;
        }

        /// <summary>將 Box 實體放在 Tray 上</summary>
        public void PutBoxInTray()
        {
            HasBox = true;
        }

        /// <summary>將盒子自 Tray 拿走</summary>
        public void TakeBoxFromTray()
        {
            HasBox = false; ;
        }

        /// <summary>綁定 Box </summary>
        /// <param name="boxUnit"></param>
        public void BindBoxUnit(BoxUnit boxUnit)
        {
            BoxUnit = boxUnit;
        }

        /// <summary>解除綁定 Box</summary>
        public void UnBindBoxUnit()
        {
            BoxUnit = null;
        }

        /// <summary>載入光罩盒</summary>
        /// <param name="box"></param>
        public void LoadBox(BoxUnit box)
        {
            BindBoxUnit(box);
            PutBoxInTray();
            
        }

        /// <summary>卸載光罩盒</summary>
        public void UnLoadBox()
        {

            TakeBoxFromTray();
            UnBindBoxUnit();
            
        }
    }
}
