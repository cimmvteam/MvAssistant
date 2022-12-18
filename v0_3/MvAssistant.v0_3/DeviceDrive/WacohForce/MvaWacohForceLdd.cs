using MvaCToolkitCs.v1_2;
using MvaCToolkitCs.v1_2.Net;
using MvaCToolkitCs.v1_2.Net.SocketTx;
using MvAssistant.v0_3;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace MvAssistant.v0_3.DeviceDrive.WacohForce
{



    public class MvaWacohForceLdd : IDisposable
    {

        CtkTcpClient netNonStopTcpClient = new CtkTcpClient();
        public CtkTcpClient netClient { get { return this.netNonStopTcpClient; } }
        MvaWacohForceMessageReceiver messageReceiver = new MvaWacohForceMessageReceiver();

        public IPEndPoint localEP { get { return CtkNetUtil.ToIPEndPoint(this.netNonStopTcpClient.LocalUri); } set { this.netNonStopTcpClient.LocalUri = CtkNetUtil.ToUri(value); } }
        public IPEndPoint remoteEP { get { return CtkNetUtil.ToIPEndPoint(this.netNonStopTcpClient.RemoteUri); } set { this.netNonStopTcpClient.RemoteUri = CtkNetUtil.ToUri(value); } }

        Boolean correctionFlag = false;
        MvaWacohForceVector centerForceVector = new MvaWacohForceVector();

        MvaWacohForceEnumConnectionStatus _connectionStatus = MvaWacohForceEnumConnectionStatus.Disconnection;
        MvaWacohForceEnumConnectionStatus connectionStatus
        {
            get { return _connectionStatus; }
            set
            {
                try
                {
                    Monitor.TryEnter(this, 5000);
                    this._connectionStatus = value;
                }
                finally { Monitor.Exit(this); }
            }
        }


        public MvaWacohForceLdd()
        {
            this.netNonStopTcpClient.EhDataReceive += (sender, e) =>
            {
                var ee = e as CtkNetStateEventArgs;
                var msg = ee.TrxBuffer;
                this.messageReceiver.Receive(msg.Buffer, msg.Offset, msg.Length);
                this.messageReceiver.AnalysisMessage();

                if (this.messageReceiver.Count == 0) return;

                var vec = this.messageReceiver.Dequeue();
                if (this.correctionFlag)
                {
                    this.centerForceVector = vec;
                    lock (this)
                        correctionFlag = false;
                }

                var ea = new MvaWacohForceMessageEventArgs();
                ea.centerForceVector = this.centerForceVector;
                ea.rawForceVector = vec;
                this.OnDataReceive(ea);
            };


            this.netNonStopTcpClient.EhDisconnect += (sender, e) =>
            {
                this.connectionStatus = MvaWacohForceEnumConnectionStatus.Disconnection;
            };
            this.netNonStopTcpClient.EhFirstConnect += (sender, e) =>
            {
                this.connectionStatus = MvaWacohForceEnumConnectionStatus.Connected;
            };

        }

        ~MvaWacohForceLdd() { this.Dispose(false); }


        public int ConnectTry()
        {
            if (this.remoteEP == null) return -1;


            if (this.connectionStatus == MvaWacohForceEnumConnectionStatus.Connecting) return 1;

            this.netNonStopTcpClient.ConnectTry();
            if (this.netNonStopTcpClient.IsLocalPrepared)
                this.connectionStatus = MvaWacohForceEnumConnectionStatus.Connecting;

            return 0;
        }




        public int Close()
        {
            if (this.netNonStopTcpClient != null)
                this.netNonStopTcpClient.Disconnect();


            return 0;
        }


        public bool IsConnect()
        {
            return this.netNonStopTcpClient.IsRemoteConnected;
        }





        public void SendCmd_RequestData()
        {
            this.netNonStopTcpClient.WriteMsg("R");
        }

        public void SendCmd_CorrectRequestData()
        {
            lock (this)
                this.correctionFlag = true;

            this.netNonStopTcpClient.WriteMsg("R");
        }








        #region Event

        public void CleanEvent()
        {
            try
            {
                foreach (Delegate d in this.evtDataReceive.GetInvocationList())
                {
                    this.evtDataReceive -= (EventHandler<MvaWacohForceMessageEventArgs>)d;
                }
            }
            catch (Exception ex) { CtkLog.Write(ex); }
        }





        public event EventHandler<MvaWacohForceMessageEventArgs> evtDataReceive;
        void OnDataReceive(MvaWacohForceMessageEventArgs ea)
        {
            if (this.evtDataReceive == null) return;
            this.evtDataReceive(this, ea);
        }


        #endregion




        #region IDisposable
        // Flag: Has Dispose already been called?
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
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
                this.DisposeManaged();
            }

            // Free any unmanaged objects here.
            //
            this.DisposeUnmanaged();

            this.DisposeSelf();

            disposed = true;
        }



        void DisposeManaged()
        {
        }

        void DisposeUnmanaged()
        {

        }

        void DisposeSelf()
        {
            this.Close();
        }

        #endregion




        public int DdZeroCorrect(IEnumerable<float> input, IEnumerable<float> offset, IEnumerable<float> result)
        {
            throw new NotImplementedException();
        }
    }

}
