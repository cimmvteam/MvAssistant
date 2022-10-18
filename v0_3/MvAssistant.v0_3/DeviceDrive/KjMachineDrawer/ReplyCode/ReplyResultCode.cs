//using Microsoft.Analytics.Interfaces;
//using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MvAssistant.v0_3.DeviceDrive.KjMachineDrawer.ReplyCode
{
    /// <summary>回傳資料的代碼</summary>
    public enum ReplyResultCode
    {
        /// <summary>動作失敗</summary>
        Failed =0,
        /// <summary>動作成功</summary>
        Set_Successfully=1
    }
}