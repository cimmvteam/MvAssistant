using MvaCToolkitCs.v1_2.Protocol;
using MvaCToolkitCs.v1_2.Threading;
using System;
using System.Collections.Generic;
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
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class CtkWcfDuplexTcpListenerInst : ICtkWcfDuplexTcpService
    {
        //[ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(ICTkWcfDuplexOpCallback))]
        //無法同時繼承並宣告ServiceContract


        public event EventHandler<CtkWcfDuplexEventArgs> EhReceiveMsg;

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

        void OnReceiveMsg(CtkWcfDuplexEventArgs ea)
        {
            if (this.EhReceiveMsg == null) return;
            this.EhReceiveMsg(this, ea);
        }




        #region Static

        public static CtkWcfDuplexTcpListener<T> NewInst<T>(T svrInst, NetTcpBinding _binding = null) where T : class, ICtkWcfDuplexTcpService
        {
            if (_binding == null) _binding = new NetTcpBinding();
            return new CtkWcfDuplexTcpListener<T>(svrInst, _binding);
        }

        public static CtkWcfDuplexTcpListener<ICtkWcfDuplexTcpService> NewDefault(NetTcpBinding _binding = null)
        {
            var svrInst = new CtkWcfDuplexTcpListenerInst();
            if (_binding == null) _binding = new NetTcpBinding();
            return NewInst<ICtkWcfDuplexTcpService>(svrInst, _binding);
        }






        #endregion


    }


}
