using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.Cabinet.DrawerStatus
{
    public enum DrawerDuration
    {
       

        /// <summary>Tray 在Home 點Idle</summary>
        Idle_TrayAtHome,

        /// <summary>Bank out, Load, Tray 移到 Out等待放置盒子 </summary>
        BankOut_Load_TrayAtOutForPutBoxOnTray,

        /// <summary>Bank out, Load, Tray 在 Out已經放上盒子 </summary>
       // BankOut_Load_TrayAtOutWithPutBoxOnTray,

        /// <summary>Bnak Out, Load, Tray 移到 Home,己經放上盒子 </summary>
        BankOut_Load_TrayAtHomeWithBox,

        /// <summary>Bnak Out, Load, Tray 在 In, 等待 Robot 取走Box </summary>
        BankOut_Load_TrayAtInWithBoxForRobotGrabBox,

        /// <summary>Bankout , Load, Tray 在In , Tray 上没有盒子 </summary>
       // BankOut_Load_TrayAtInNoBox,

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
