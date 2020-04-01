using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OMRON.Compolet.CIP;
namespace MvAssistant.DeviceDrive.OmronPlc
{

    /// <summary>
    /// 需要開啟Sysmac Gateway Console, 啟動此服務. 
    /// 並在對應連結的網卡上設定Properties,再按下Open. 
    /// 記得PLC需要與該網卡設定在同一個網域/遮罩. 
    /// PortID與一般Socket Port不同, 是
    /// </summary>
    public class MvOmronPlcLdd : IDisposable
    {
        NJCompolet _CIPcompolet;

        public NJCompolet CIPcompolet { get { return _CIPcompolet; } }
        public MvOmronPlcLdd()
        {
        }



        ~MvOmronPlcLdd() { this.Dispose(false); }

        public void NLPLC_Initial(string ip, int portId)
        {
            lock (this)
            {
                if (this._CIPcompolet == null)
                    this._CIPcompolet = new NJCompolet();

                _CIPcompolet.Active = false;
                _CIPcompolet.ConnectionType = ConnectionType.UCMM;
                _CIPcompolet.ReceiveTimeLimit = 750;
                _CIPcompolet.PeerAddress = ip;
                _CIPcompolet.LocalPort = portId;
                _CIPcompolet.Active = true;
            }
        }
        public void NLPLC_ClosePort()
        {
            lock (this)
            {
                if (this._CIPcompolet == null)
                    this._CIPcompolet = new NJCompolet();

                _CIPcompolet.Active = false;
            }
        }
        public bool IsConnected() { lock (this) return this._CIPcompolet != null; }

        #region Read / Write Variable

        public object Read(string VarName) { return _CIPcompolet.ReadVariable(VarName); }
        public void Write(string VarName, Object data) { _CIPcompolet.WriteVariable(VarName, data); }


        public int ReadInt32(string varName) { return Convert.ToInt32(this.Read(varName)); }
        public void WriteIn32(string varName, int data) { this.Write(varName, data); }

        #endregion


        #region IDisposable
        // Flag: Has Dispose already been called?
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
        }



        #endregion

    }
}
