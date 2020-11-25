using MaskAutoCleaner.v1_0.Msg;
using MvAssistant.Mac.v1_0;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.JSon.RobotTransferFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.BoxTransfer
{
    [Guid("024E0148-043D-4D07-9661-6A4BCD40B316")]
    public class MacMcBoxTransfer : MacMachineCtrlBase
    {
        public IMacHalBoxTransfer HalBoxTransfer { get { return this.HalAssembly as IMacHalBoxTransfer; } }
        /// <summary>
        /// 使用固定的State Machine,
        /// 若有其它版的狀態機, 一般也會用不同的控制機
        /// </summary>
        public MacMsBoxTransfer StateMachine { get { return this.MsAssembly as MacMsBoxTransfer; } set { this.MsAssembly = value; } }

        public MacMcBoxTransfer()
        {
            this.MsAssembly = new MacMsBoxTransfer();
        }

        public override int RequestProcMsg(IMacMsg msg)
        {
            var msgCmd = msg as MacMsgCommand;
            if (msgCmd != null)
            {
                var type = typeof(MacMsBoxTransfer);
                var method = type.GetMethod(msgCmd.Command);
                if (msgCmd.Command.ToString() == "MoveToLock") 
                    method.Invoke(this.StateMachine, new object[] { (uint)BoxType.DontCare });
                else if (msgCmd.Command.ToString() == "MoveToUnlock")
                    method.Invoke(this.StateMachine, new object[] { (uint)BoxType.DontCare });
                else if (msgCmd.Command.ToString() == "MoveToOpenStageGet")
                    method.Invoke(this.StateMachine, new object[] { (uint)BoxType.DontCare });
                else if (msgCmd.Command.ToString() == "MoveToCabinetGet")
                    method.Invoke(this.StateMachine, new object[] { BoxrobotTransferLocation.Drawer_01_01,(uint)BoxType.DontCare });
                else if (msgCmd.Command.ToString() == "MoveToCabinetPut")
                    method.Invoke(this.StateMachine, new object[] { BoxrobotTransferLocation.Drawer_01_01, (uint)BoxType.DontCare });
                else
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
