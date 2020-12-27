using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MaskAutoCleaner.v1_0.Msg;

namespace MaskAutoCleaner.v1_0.Machine.Cabinet
{
    //[Guid("8ddaa02f-8d2d-46c6-9e8b-3e861e431ff2")]
    [Guid("8DDAA02F-8D2D-46C6-9E8B-3E861E431FF2")]
    public class MacMcCabinet : MacMachineCtrlBase
    {

        public MacMsCabinet0 StateMachine { get { return this.MsAssembly as MacMsCabinet0; } set { this.MsAssembly = value; } }

        
        public Object[] InvokeParameters;
        public MacMcCabinet()
        {
            this.StateMachine = MacMsCabinet0.GetInstance();
            InvokeParameters = null;
        }
        //public Object[] InvokeParameters;
        public override int RequestProcMsg(IMacMsg msg)
        {
            var msgCmd = msg as MacMsgCommand;
            if (msgCmd != null)
            {
                var type = typeof(MacMsCabinet0);
                var method = type.GetMethod(msgCmd.Command);
                method.Invoke(this.StateMachine, InvokeParameters);
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
