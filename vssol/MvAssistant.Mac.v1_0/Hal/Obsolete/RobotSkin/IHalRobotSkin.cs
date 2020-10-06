using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component
{
    [GuidAttribute("973AB1DD-AE70-42E9-9B61-1CB763D00430")]
    public interface IHalRobotSkin : IMacHalComponent
    {
        /// <summary>
        /// 讀取[Robot Skin] value
        /// </summary>
        /// <returns></returns>
        float GetValue();
    }
}
