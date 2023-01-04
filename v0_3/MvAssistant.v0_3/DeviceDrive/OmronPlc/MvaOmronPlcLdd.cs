using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using MvaCToolkitCs.v1_2;
using OMRON.Compolet.CIP;
namespace MvAssistant.v0_3.DeviceDrive.OmronPlc
{

    /// <summary>
    /// 需要開啟Sysmac Gateway Console, 啟動此服務. 
    /// 並在對應連結的網卡上設定Properties,再按下Open. 
    /// 記得PLC需要與該網卡設定在同一個網域/遮罩. 
    /// PortID與一般Socket Port不同, 是
    /// </summary>
    public class MvaOmronPlcLdd : IDisposable
    {
        NJCompolet _CIPcompolet;

        public NJCompolet CIPcompolet { get { return _CIPcompolet; } }
        public MvaOmronPlcLdd()
        {
        }



        ~MvaOmronPlcLdd() { this.Dispose(false); }

        public void NLPLC_Initial(string ip, int portId)
        {
            lock (this)
            {
                if (this._CIPcompolet == null)
                    this._CIPcompolet = new NJCompolet();

                this._CIPcompolet.Active = false;
                this._CIPcompolet.ConnectionType = ConnectionType.UCMM;
                this._CIPcompolet.ReceiveTimeLimit = 750;
                this._CIPcompolet.PeerAddress = ip;
                this._CIPcompolet.LocalPort = portId;
                this._CIPcompolet.Active = true;
            }
        }
        public void NLPLC_ClosePort()
        {
            lock (this)
            {
                Thread.Sleep(50);
                if (this._CIPcompolet == null) return;

                using (var obj = this._CIPcompolet)
                {
                    this._CIPcompolet.Active = false;
                }
            }
        }
        public bool IsConnected() { lock (this) return this._CIPcompolet.IsConnected; }

        #region Read / Write Variable

        public object Read(string VarName)
        {
            Exception myex = null;
            //Fail允許重新再執行, 上限3次
            for (var tryIndex = 0; tryIndex < 3; tryIndex++)
            {
                try
                {
                    lock (this)
                    {
                        //每個要存取PLC的 都要稍等一下, 讓PLC有恢復Clock的時間
                        Thread.Sleep(50);
                        return _CIPcompolet.ReadVariable(VarName);
                    }
                }
                catch (Exception ex)
                {
                    LogInfo(ex.Message);
                    CtkLog.WarnAn(this, ex);
                    myex = ex;
                }
            }

            //若3次嘗試存取失敗, 直接拋出Exception
            throw new MvaException("PLC read fail over 3 times", myex);
        }
        public void Write(string VarName, Object data)
        {
            Exception myex = null;
            //Fail允許重新再執行, 上限3次
            for (var tryIndex = 0; tryIndex < 3; tryIndex++)
            {
                try
                {
                    lock (this)
                    {
                        //每個要存取PLC的 都要稍等一下, 讓PLC有恢復Clock的時間
                        Thread.Sleep(50);
                        this._CIPcompolet.WriteVariable(VarName, data);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    LogInfo(ex.Message);
                    CtkLog.WarnAn(this, ex);
                    myex = ex;
                }
            }

            //若3次嘗試存取失敗, 直接拋出Exception
            throw new MvaException("PLC read fail over 3 times", myex);
        }

        public Hashtable ReadMulti(String[] VarNames)
        {
            Exception myex = null;
            //Fail允許重新再執行, 上限3次
            for (var tryIndex = 0; tryIndex < 3; tryIndex++)
            {
                try
                {
                    lock (this)
                    {
                        //每個要存取PLC的 都要稍等一下, 讓PLC有恢復Clock的時間
                        Thread.Sleep(50);
                        return _CIPcompolet.ReadVariableMultiple(VarNames);
                    }
                }
                catch (Exception ex)
                {
                    LogInfo(ex.Message);
                    CtkLog.WarnAn(this, ex);
                    myex = ex;
                }
            }

            //若3次嘗試存取失敗, 直接拋出Exception
            throw new MvaException("PLC read fail over 3 times", myex);
        }

        public int ReadInt32(string varName) { return Convert.ToInt32(this.Read(varName)); }
        public void WriteIn32(string varName, int data) { this.Write(varName, data); }

        #endregion

        #region LogInfo()
        public void LogInfo(string pMessage)
        {
            string tFilePath = @"D:\PLC_Logg.txt";
            StreamWriter tStreamWriter = null;
            try
            {
                if (!File.Exists(tFilePath)) File.Create(tFilePath);
                tStreamWriter = new StreamWriter(tFilePath, true, System.Text.UTF8Encoding.UTF8);
                tStreamWriter.WriteLine(pMessage);
            }
            catch (Exception) { }
            finally
            {
                if (tStreamWriter != null) tStreamWriter.Close();
            }
        }
        #endregion LogInfo()

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
