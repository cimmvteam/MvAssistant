//using Microsoft.Analytics.Interfaces;
//using Microsoft.Analytics.Types.Sql;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MvAssistant.Mac.TestMy.MachineRealHal.Hirata_ScenarioTest.Extends
{
   public static class MacHalBoxTransferExtends
    {

        /// <summary>移動</summary>
        /// <param name="instance"></param>
        /// <param name="path">路徑檔名稱</param>
        public static void Move(this MacHalBoxTransfer instance,string path)
        {
            instance.RobotMoving(true);
            instance.ExePathMove(path);
            instance.RobotMoving(false);
        } 
    }
}