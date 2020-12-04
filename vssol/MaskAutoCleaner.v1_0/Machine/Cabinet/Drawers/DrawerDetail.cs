using MvAssistant.Mac.v1_0.Hal.CompDrawer;
using MvAssistant.Mac.v1_0.JSon.RobotTransferFile;
using MvAssistant.Mac.v1_0.Manifest;
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
        public IMacHalDrawer Drawer { get; set; }

        /// <summary>Box</summary>
        public BoxUnit BoxUnit { get; private set; }
      

        public BoxrobotTransferLocation DrawerLocation { get { return DeviceID.ToBoxrobotTransferLocation(); } }
        public MacEnumDevice DeviceID { get; set; }

        /// <summary>目前有没有盒子</summary>
        public bool HasBox { get;private set; }

        public DrawerDetail()
        {
            HasBox = false;
            Drawer = null;
            BoxUnit = null;
            DeviceID = MacEnumDevice.cabinet_drawer;
        }


        /// <summary>將 Box 實體放在 Tray 上</summary>
        public void PutBoxInTray()
        {
            HasBox = true;
        }

        /// <summary>將盒子自 Tray 拿走</summary>
        public void GrabBoxFromTray()
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
    }
}
