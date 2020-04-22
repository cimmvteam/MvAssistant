using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Hal.Intf.Component
{
    [GuidAttribute("BEB0E42C-0D9D-4033-B3B3-FF48C6C6582D")]
    public interface IHalTactile : IHalComponent
    {
        /// <summary>
        /// 讀取[Tactile]壓力 raw value
        /// </summary>
        /// <returns></returns>
        float GetPressValue();

        /// <summary>
        /// Read sensor raw current data, unit: mA
        /// </summary>
        /// <returns></returns>
        float GetCurrent();


        //TODO: 列出所有Issue所需的Method
        
        /*
        enum TactileResponseMode { Pressed = 100, Released = 200 }
        TactileResponseMode IssuePressMode();


        float IssueOperationCurrent();
        float IssueIdleCurrent();*/
    }
}
