using MaskAutoCleaner.v1_0.Msg;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace MaskAutoCleaner.v1_0.Machine.MaskTransfer
{
    [Guid("80CB72BD-4FB5-4EAA-B5DB-B77308864FD4")]
    public class MacMcMaskTransfer : MacMachineCtrlBase
    {
        public IMacHalMaskTransfer HalMaskTransfer { get { return this.HalAssembly as IMacHalMaskTransfer; } }
        /// <summary>
        /// 使用固定的State Machine,
        /// 若有其它版的狀態機, 一般也會用不同的控制機
        /// </summary>
        public MacMsMaskTransfer StateMachine { get { return this.MsAssembly as MacMsMaskTransfer; } set { this.MsAssembly = value; } }

        public MacMcMaskTransfer()
        {
            this.MsAssembly = new MacMsMaskTransfer();
        }



        public override int RequestProcMsg(IMacMsg msg)
        {
            var msgTran = msg as MacMsgTransition;
            if (msgTran != null)
            {
                var type = typeof(MacMsMaskTransfer);
                var method = type.GetMethod("Cmd" + "SystemBootup");
                method.Invoke(this.StateMachine, null);

                //"Cmd" + EnumMacMsMaskTransferTransition.MoveToLoadPortA;
                //
                //
                //
            }
            var msgSecs = msg as MacMsgSecs;
            if (msgSecs != null)
            {

            }
            return 0;
        }
    }
}

