using MaskAutoCleaner.v1_0.Msg;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.InspectionCh
{
    [Guid("85BE70B6-10A6-4403-B4E3-3224CD847B48")]
    public class MacMcInspectionCh : MacMachineCtrlBase
    {
        public IMacHalInspectionCh HalInspectionCh { get { return this.HalAssembly as IMacHalInspectionCh; } }
        /// <summary>
        /// 使用固定的State Machine,
        /// 若有其它版的狀態機, 一般也會用不同的控制機
        /// </summary>
        public MacMsInspectionCh StateMachine { get { return this.MsAssembly as MacMsInspectionCh; } set { this.MsAssembly = value; } }

        public MacMcInspectionCh()
        {
            this.MsAssembly = new MacMsInspectionCh();
        }



        public override int RequestProcMsg(IMacMsg msg)
        {
            var msgCmd = msg as MacMsgCommand;
            if (msgCmd != null)
            {
                var type = typeof(MacMsInspectionCh);
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
