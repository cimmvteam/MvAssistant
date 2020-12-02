using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.Cabinet
{
    public enum EnumMacMcCabinetCmd
    {
        /// <summary>系統啟動</summary>
        SystemBootup,

        Load_Drawers,
        
        /// <summary>Bank Out, Load, 將 指定數量的 Tray移到 Out 等待 放置盒子(一或多個) </summary>
        BankOutLoadMoveTraysToOutForPutBoxOnTray,

        /// <summary>Bankout, Load, 放入盒子之後, 將指定的 Drawer 的Tray(有盒子) 推入 Home  () </summary>
        BankOutLoadMoveTraysToHomeAfterPutBoxOnTray,

        /// <summary>BankOut, Load, 將指定具有 盒子的 Tray 移到 In, 等待 BoxRobot 抓取</summary>
        BankOutLoadMoveSpecificTrayToInForBoxRobotGrabBox,

        /// <summary>BankOut, Load, 具有 盒子的 Tray 原本在  In, BoxRobot 抓取 盒子後 將 Tray 移至 Home</summary>
        BankOutLoadMoveSpecificTrayToHomeAfterBoxrobotGrabBox,

        /// <summary>BankOut, UnLoad, 將指定 盒子的 Tray(没有盒子) 移到 In, 等待 BoxRobot 放置盒子</summary>
        BankOutUnLoadMoveSpecificTrayToInForBoxrobotPutBox,

        /// <summary>BankOut, UnLoad, 將指定具有 盒子的 Tray 移到 In, 等待 BoxRobot 放置盒子</summary>
        BankOutUnLoadMoveSpecificTrayToHomeAfterBoxrobotPutBox,

        /// <summary>BankOut, UnLoad, 將指定具有 盒子的 Tray 移到Home, 等待 將盒子取走</summary>
        BankOutUnLoadMoveSpecificTraysToOutForGrabBox

    }
}
