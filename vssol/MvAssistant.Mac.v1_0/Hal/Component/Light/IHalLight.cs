using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Hal.Intf.Component
{
    [GuidAttribute("AD149AD6-FB29-4E00-B69A-2438DB7D69E8")]
    public interface IHalLight : IHalComponent
    {
        /// <summary>
        /// 設定[光源Intensity] value
        /// </summary>
        /// <param name="value"></param>
        void SetLightValue(int value);

        /// <summary>
        /// 讀取[光源Intensity] value
        /// </summary>
        /// <returns>light intensity, unit: int</returns>
        int GetLightValve();
    }
}
