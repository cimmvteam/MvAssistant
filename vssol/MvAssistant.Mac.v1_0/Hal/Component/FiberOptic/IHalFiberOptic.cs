using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component.FiberOptic
{
    [GuidAttribute("924DD1B9-A363-4004-A970-0F0AA63EB9F8")]
    public interface IHalFiberOptic : IHalComponent
    {
        /// <summary>
        /// 讀取[Fiber Optic] 光學value
        /// </summary>
        /// <returns></returns>
        float GetOpticalValue();
        bool GetBooleanStatus();
    }
}
