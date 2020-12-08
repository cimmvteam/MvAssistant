using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MaskAutoCleaner.v1_0.Msg;
using MvAssistant.v0_2.Mac.Hal.Assembly;

namespace MaskAutoCleaner.v1_0.Machine.OpenStage
{
    [Guid("C5CA0CD4-C6A1-4F65-B4FA-9EED941864A0")]
    public class MacMcOpenStage : MacMachineCtrlBase
    {
        public IMacHalOpenStage HalOpenStage { get { return this.HalAssembly as IMacHalOpenStage; } }
        /// <summary>
        /// 使用固定的State Machine,
        /// 若有其它版的狀態機, 一般也會用不同的控制機
        /// </summary>
        public MacMsOpenStage StateMachine { get { return this.MsAssembly as MacMsOpenStage; } set { this.MsAssembly = value; } }

        public MacMcOpenStage()
        {
            this.MsAssembly = new MacMsOpenStage();
        }
        public override int RequestProcMsg(IMacMsg msg)
        {
            var msgCmd = msg as MacMsgCommand;
            if (msgCmd != null)
            {
                var type = typeof(MacMsOpenStage);
                var method = type.GetMethod(msgCmd.Command);
                method.Invoke(this.StateMachine, null);
            }
            var msgTran = msg as MacMsgTransition;
            if (msgTran != null)
            {

            }
            var msgSecs = msg as MacMsgSecs;
            if (msgSecs != null)
            {

            }
            return 0;
        }
    }
}
