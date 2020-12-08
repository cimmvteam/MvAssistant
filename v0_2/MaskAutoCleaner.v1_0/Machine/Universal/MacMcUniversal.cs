using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MaskAutoCleaner.v1_0.Msg;
using MvAssistant.Mac.v1_0.Hal.Assembly;

namespace MaskAutoCleaner.v1_0.Machine.Universal
{
    /// <summary>
    /// 代為處理整機未歸類Device的Assembly
    /// 名稱不採用EQP, 因為EQP會代表整機, 名稱會有上下階層的關係
    /// 但此類別與其它Assembly為同階
    /// </summary>
    [Guid("AFEFFEA4-B491-4A75-9E31-BDDBB5131262")]
    public class MacMcUniversal : MacMachineCtrlBase
    {
        public IMacHalUniversal HalUniversal { get { return this.HalAssembly as IMacHalUniversal; } }
        /// <summary>
        /// 使用固定的State Machine,
        /// 若有其它版的狀態機, 一般也會用不同的控制機
        /// </summary>
        public MacMsUniversal StateMachine { get { return this.MsAssembly as MacMsUniversal; } set { this.MsAssembly = value; } }

        public MacMcUniversal()
        {
            this.MsAssembly = new MacMsUniversal();
        }
        public override int RequestProcMsg(IMacMsg msg)
        {
            return 0;
        }
    }
}
