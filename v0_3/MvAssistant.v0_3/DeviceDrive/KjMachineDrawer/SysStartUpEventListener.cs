using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.DeviceDrive.KjMachineDrawer
{

    /// <summary>Lisent SysStartUp Event 的物件類別</summary>
    public class SysStartUpEventListener : IDisposable
    {
        /// <summary>Listen Port</summary>
        private int _listenPort;

        /// <summary>Listen 的Thread</summary>
        private Thread _listenThread;

        /// <summary>Listen 的物件</summary>
        public UdpClient UdpClient { get; set; }

        /// <summary>發出訊息的端點</summary>
        private IPEndPoint IpEndPoint;

        /// <summary>建構式</summary>
        private SysStartUpEventListener()
        {
            _listenThread = new Thread(EventListener);
            _listenThread.IsBackground = true;
            _listenThread.Name = nameof(SysStartUpEventListener);
        }

        /// <summary>建構式</summary>
        /// <param name="port">Listen Port</param>
        public SysStartUpEventListener(int port) : this()
        {
            _listenPort = port;

        }

        ~SysStartUpEventListener() { this.Dispose(false); }

        /// <summary>開始監聽</summary>
        /// <param name="onReciveMessageCallback">監聽到訊息後的回呼函式</param>
        public void Listen(DelOnRcvMessage onReciveMessageCallback)
        {
            this.OnRcvMessageCallBack = onReciveMessageCallback;
            _listenThread.Start();
        }

        /// <summary>監聽的函式</summary>
        public void EventListener()
        {
            IpEndPoint = new IPEndPoint(IPAddress.Any, _listenPort);
            UdpClient = new UdpClient(IpEndPoint.Port);
            while (!this.disposed)
            {
                try
                {
                    // [Listen Point]
                    var rcvMessage = System.Text.Encoding.UTF8.GetString(UdpClient.Receive(ref IpEndPoint));
                    OnRcvMessage(rcvMessage, IpEndPoint);
                }
                catch (Exception) { }
            }
        }

        /// <summary>接到監聽資料時的處理函式</summary>
        /// <param name="message">收到的訊息</param>
        /// <param name="ipEndpoint">發出訊號的 端點</param>
        private void OnRcvMessage(string message, IPEndPoint ipEndpoint)
        {
            var ip = ipEndpoint.Address;
            if (OnRcvMessageCallBack != null)
            {
                OnRcvMessageCallBack(message, ipEndpoint);
            }
        }


        /// <summary>回呼函式</summary>
        public DelOnRcvMessage OnRcvMessageCallBack = null;


        #region IDisposable
        protected bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //

            this.DisposeSelf();

            disposed = true;
        }

        protected virtual void DisposeSelf()
        {
            if (this.UdpClient != null)
                using (var obj = this.UdpClient)
                {
                    try { obj.Close(); }
                    catch (Exception) { }
                }

            if (this._listenThread != null)
            {
                try { this._listenThread.Abort(); }
                catch (Exception) { /*不需要catch, 到dispose階段的中斷成敗都要結束*/ }
            }
        }
        #endregion

    }
}
