using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.Mac.Hal.CompPlc
{
    /*[d20211126] IHalPlc 基本上只提供通用存取方法.
        IHalPlcAssembly 提供該組件必然會用的Plc方法. 因為改機改版後, 部份function可能不採用PLC.
        IHalAssembly 提供應用層function, 底下實作由實體決定, 這樣才能面對多個機台版本

        Controller 應用層, 應避免直接越過 HalAssembly 進行裝置存取, 那會導致未來支援不同版本機台的困擾.
        也就是, 硬體異動導致的改寫, 止損在Hal層

        當然, 提供HAL library的角色, 也不太會去限制應用層使用方式.
        若硬要存取底層或特殊原因, 仍可開放使用.
        若應用層也是可以將 interface 物件直接轉為實體type 去使用, 但自己要明確知道自己在做什麼
    */



    public interface IMacHalPlcBase: IMacHalComponent
    {
        Dictionary<MacHalPlcEnumVariable, Object> ReadMulti(IEnumerable<MacHalPlcEnumVariable> varNames);
    }
}
