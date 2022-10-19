using MvaCToolkitCs.v1_2.Protocol;
using MvaCToolkitCs.v1_2.Threading;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MvaCToolkitCs.v1_2.Wcf.DuplexTcp
{



    /// <summary>
    /// 尚不完整, 除了自己的專案以外, 盡量不要用
    /// </summary>
    public class CtkWcfDuplexTcpClientInst : ICTkWcfDuplexTcpCallback
    {

        public static CtkWcfDuplexTcpClient<TService, TCallback> NewInst<TService, TCallback>(TCallback inst, NetTcpBinding binding = null)
            where TService : ICtkWcfDuplexTcpService
            where TCallback : ICTkWcfDuplexTcpCallback
        {
            if (binding == null) binding = new NetTcpBinding();
            return new CtkWcfDuplexTcpClient<TService, TCallback>(inst, binding);
        }

        public static CtkWcfDuplexTcpClient<TService, CtkWcfDuplexTcpClientInst> NewDefault<TService>(NetTcpBinding binding = null)
            where TService : ICtkWcfDuplexTcpService
        {
            if (binding == null) binding = new NetTcpBinding();
            return new CtkWcfDuplexTcpClient<TService, CtkWcfDuplexTcpClientInst>(new CtkWcfDuplexTcpClientInst(), binding);
        }

        public static CtkWcfDuplexTcpClient<ICtkWcfDuplexTcpService, CtkWcfDuplexTcpClientInst> NewDefault(NetTcpBinding binding = null)
        {
            if (binding == null) binding = new NetTcpBinding();
            return new CtkWcfDuplexTcpClient<ICtkWcfDuplexTcpService, CtkWcfDuplexTcpClientInst>(new CtkWcfDuplexTcpClientInst(), binding);
        }




        public event EventHandler<CtkWcfDuplexEventArgs> EhReceiveMsg;

        void OnReceiveMsg(CtkWcfDuplexEventArgs tcpstate)
        {
            if (this.EhReceiveMsg == null) return;
            this.EhReceiveMsg(this, tcpstate);
        }


        public void CtkSend(CtkWcfMessage msg)
        {
            var ea = new CtkWcfDuplexEventArgs();
            ea.WcfMsg = msg;
            ea.IsWcfNeedReturnMsg = false;
            this.OnReceiveMsg(ea);
        }

        public CtkWcfMessage CtkSendReply(CtkWcfMessage msg)
        {
            var ea = new CtkWcfDuplexEventArgs();
            ea.WcfMsg = msg;
            ea.IsWcfNeedReturnMsg = true;
            this.OnReceiveMsg(ea);
            return ea.WcfReturnMsg;
        }


    }




}
