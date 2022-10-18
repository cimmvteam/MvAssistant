using MvaCToolkitCs.v1_2.Logging;
using MvaCToolkitCs.v1_2.Net;
using MvaCToolkitCs.v1_2.Protocol;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MvaCToolkitCs.v1_2.DigitalPort
{
    public class CtkNonStopSerialPort : ICtkProtocolNonStopConnect, IDisposable
    {
        public CtkSerialPortCfg Config = new CtkSerialPortCfg();
        protected int m_IntervalTimeOfConnectCheck = 5000;
        ManualResetEvent connectMre = new ManualResetEvent(true);
        SerialPort serialPort = new SerialPort();
        Thread threadNonStopConnect;// = new BackgroundWorker();
        public CtkNonStopSerialPort() : base() { }

        public CtkNonStopSerialPort(string portName
            , int baudRate = 9600
            , Parity parity = Parity.None
            , StopBits StopBits = StopBits.One
            , int DataBits = 8
            , Handshake handshake = Handshake.None
            , bool RtsEnable = true)
        {
            this.Config.PortName = portName;
            this.Config.BaudRate = baudRate;
            this.Config.Parity = parity;
            this.Config.StopBits = StopBits.One;
            this.Config.DataBits = 8;
            this.Config.Handshake = Handshake.None;
            this.Config.RtsEnable = true;
        }

        public CtkNonStopSerialPort(CtkSerialPortCfg config) { this.Config = config; }

        ~CtkNonStopSerialPort() { this.Dispose(false); }

        public bool IsLocalReadyConnect { get { return this.IsRemoteConnected; } }//Local連線成功=遠端連線成功
        public bool IsNonStopRunning { get { return this.threadNonStopConnect != null && this.threadNonStopConnect.IsAlive; } }
        public bool IsOpenRequesting { get { return !this.connectMre.WaitOne(10); } }
        public bool IsRemoteConnected { get { return this.serialPort == null ? false : this.serialPort.IsOpen; } }
        //用途是避免重複要求連線
        public void WriteBytes(byte[] buff, int offset, int length) { this.serialPort.Write(buff, offset, length); }



        #region ICtkProtocolConnect

        public event EventHandler<CtkProtocolEventArgs> EhDataReceive;
        public event EventHandler<CtkProtocolEventArgs> EhDisconnect;
        public event EventHandler<CtkProtocolEventArgs> EhErrorReceive;
        public event EventHandler<CtkProtocolEventArgs> EhFailConnect;
        public event EventHandler<CtkProtocolEventArgs> EhFirstConnect;

        public object ActiveWorkClient { get { return this.serialPort; } set { if (this.serialPort != value) throw new ArgumentException("不可傳入其它ActiveWorkClient"); } }


        public int ConnectTry()
        {
            if (this.serialPort != null && this.serialPort.IsOpen) return 0;//連線中直接離開
            try
            {
                if (!Monitor.TryEnter(this, 1000)) return -1;//進不去先離開
                if (!connectMre.WaitOne(10)) return 0;//連線中就離開
                this.connectMre.Reset();//先卡住, 不讓後面的再次進行連線

                if (this.serialPort != null)
                {
                    try
                    {
                        this.serialPort.Close();
                        this.OnDisconnect(new CtkNonStopSerialPortEventArgs() { Sender = this, SerialPort = this.serialPort });
                        this.serialPort.Dispose();
                    }
                    catch (Exception ex) { CtkLog.Write(ex); }
                }
                this.serialPort = new SerialPort(this.Config.PortName);
                this.serialPort.BaudRate = this.Config.BaudRate;
                this.serialPort.Parity = this.Config.Parity;
                this.serialPort.StopBits = this.Config.StopBits;
                this.serialPort.DataBits = this.Config.DataBits;
                this.serialPort.Handshake = this.Config.Handshake;
                this.serialPort.RtsEnable = this.Config.RtsEnable;

                this.serialPort.DataReceived += (sender, e) =>
                {
                    var sp = sender as SerialPort;
                    var ea = new CtkNonStopSerialPortEventArgs();
                    ea.Sender = this;
                    ea.SerialPort = sp;
                    ea.DataType = e.EventType;
                    ea.TrxMessageBuffer = new CtkProtocolBufferMessage();
                    var ctkBuffer = ea.TrxMessageBuffer;
                    do
                    {
                        ctkBuffer.Length = sp.Read(ctkBuffer.Buffer, 0, ctkBuffer.Buffer.Length);
                        if (ctkBuffer.Length > 0)
                            this.OnDataReceive(ea);
                    } while (ctkBuffer.Length > 0);
                };
                this.serialPort.ErrorReceived += (sender, e) =>
                {
                    var sp = sender as SerialPort;
                    var ea = new CtkNonStopSerialPortEventArgs();
                    ea.Sender = this;
                    ea.SerialPort = sp;
                    ea.ErrorType = e.EventType;
                    this.OnErrorReceive(ea);
                };


                try
                {
                    this.serialPort.Open();
                    this.OnFirstConnect(new CtkNonStopSerialPortEventArgs() { Sender = this, SerialPort = this.serialPort });
                }
                catch (Exception ex)
                {
                    this.OnFailConnect(new CtkNonStopSerialPortEventArgs() { Sender = this, SerialPort = this.serialPort, Exception = ex });
                }

                this.connectMre.Set();

                return 0;
            }
            finally { Monitor.Exit(this); }

        }
        public int ConnectTryStart() { return this.ConnectTry(); /*此通訊的連線方式, 同步=非同步*/ }
        public void Disconnect()
        {
            if (this.threadNonStopConnect != null)
                this.threadNonStopConnect.Abort();
            if (this.serialPort != null)
            {
                this.serialPort.Close();
                this.serialPort.Dispose();
            }
        }
        public void WriteMsg(CtkProtocolTrxMessage msg)
        {
            if (msg.Is<string>())
            {
                var buff = Encoding.UTF8.GetBytes(msg.As<string>());
                this.WriteBytes(buff, 0, buff.Length);
            }
            else if (msg.Is<CtkProtocolBufferMessage>())
            {
                var buff = msg.As<CtkProtocolBufferMessage>();
                this.WriteBytes(buff.Buffer, buff.Offset, buff.Length);
            }
            else
            {
                throw new ArgumentException("Not support type");
            }

        }

        #endregion


        #region ICtkProtocolNonStopConnect
        public int IntervalTimeOfConnectCheck { get { return this.m_IntervalTimeOfConnectCheck; } set { this.m_IntervalTimeOfConnectCheck = value; } }
        public void NonStopRunStop()
        {
            if (this.threadNonStopConnect != null)
                this.threadNonStopConnect.Abort();
        }
        public void NonStopRunStart()
        {
            NonStopRunStop();

            this.threadNonStopConnect = new Thread(new ThreadStart(delegate ()
            {
                while (!disposed)
                {
                    try
                    {
                        this.ConnectTry();
                    }
                    catch (Exception ex) { CtkLog.Write(ex); }

                    Thread.Sleep(this.IntervalTimeOfConnectCheck);

                }
            }));
            this.threadNonStopConnect.Start();

        }

        #endregion

        #region Event

        void OnDataReceive(CtkProtocolEventArgs ea)
        {
            if (this.EhDataReceive == null) return;
            this.EhDataReceive(this, ea);
        }
        void OnDisconnect(CtkProtocolEventArgs ea)
        {
            if (this.EhDisconnect == null) return;
            this.EhDisconnect(this, ea);
        }
        void OnErrorReceive(CtkProtocolEventArgs ea)
        {
            if (this.EhErrorReceive == null) return;
            this.EhErrorReceive(this, ea);
        }
        void OnFailConnect(CtkProtocolEventArgs ea)
        {
            if (this.EhFailConnect == null) return;
            this.EhFailConnect(this, ea);
        }
        void OnFirstConnect(CtkProtocolEventArgs ea)
        {
            if (this.EhFirstConnect == null) return;
            this.EhFirstConnect(this, ea);
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
        protected void Dispose(bool disposing)
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
        void DisposeSelf()
        {
            try
            {
                this.Disconnect();
                //一旦結束就死了, 需要重new, 所以清掉event沒問題
                CtkEventUtil.RemoveEventHandlersOfOwnerByFilter(this, (dlgt) => true);
            }
            catch (Exception ex) { CtkLog.Write(ex); }
        }



        #endregion

    }
}
