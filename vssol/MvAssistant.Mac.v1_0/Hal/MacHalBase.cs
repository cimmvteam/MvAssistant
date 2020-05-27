using MvAssistant.Mac.v1_0;
using MvAssistant.Mac.v1_0.Manifest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac
{
    [GuidAttribute("8592A98B-C7DD-46C3-9965-BFD952A7742A")]
    public abstract class HalBase : IHal, IDisposable
    {
        public MacManifestDeviceCfg HalDeviceCfg;
        public MacManifestDriverCfg HalDriverCfg;
        public Dictionary<string, HalBase> Hals = new Dictionary<string, HalBase>();

        ~HalBase() { this.Dispose(false); }
        public string ID { get { return this.HalDeviceCfg.ID; } }



        #region Machines Get/Set

        public HalBase this[string key] { get { return this.GetHalDevice(key); } set { this.SetHalDevice(key, value); } }
        public HalBase this[MacEnumDevice key] { get { return this.GetHalDevice(key); } set { this.SetHalDevice(key, value); } }
        public HalBase GetHalDevice(MacEnumDevice key) { return this.GetHalDevice(key.ToString()); }
        public HalBase GetHalDevice(string key)
        {
            var hals = (from row in this.Hals
                            where row.Key == key
                            select row).ToList();

            if (hals.Count == 0) throw new MacException("No exist machine");
            else if (hals.Count > 1) throw new MacException("Duplicate machine");

            return hals.FirstOrDefault().Value;
        }
        public void SetHalDevice(MacEnumDevice key, HalBase hal) { this.SetHalDevice(key, hal); }
        public void SetHalDevice(string key, HalBase hal) { this.Hals[key] = hal; }

        #endregion

        #region IHal

        public virtual int HalClose()
        {
            return 0;
            //throw new NotImplementedException();
        }
        public virtual int HalConnect()
        {
            throw new NotImplementedException();
        }
        public virtual bool HalIsConnected()
        {
            throw new NotImplementedException();
        }
        public int HalStop()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Device Setting

        protected string DeviceConnStr { get { return this.HalDeviceCfg.DevConnStr; } }
        private Dictionary<string, string> m_DevSettings;
        public Dictionary<string, string> DevSettings
        {
            get
            {
                if (this.m_DevSettings == null) { this.m_DevSettings = this.GetDevSettings(); }
                return this.m_DevSettings;
            }
        }

        public int GetDevSettingInt(string key) { return Int32.Parse(this.DevSettings[key.ToLower()]); }
        public string GetDevSetting(string key) { return this.DevSettings[key.ToLower()]; }

        private Dictionary<string, string> GetDevSettings()
        {
            if (string.IsNullOrEmpty(this.DeviceConnStr)) return new Dictionary<string, string>();

            var settings = (from row in this.DeviceConnStr.Split(new char[] { ';' })
                            where !string.IsNullOrEmpty(row)
                            select row.Trim()).ToList();

            //Key 全轉小寫
            var dict = (from row in settings
                        where row.Contains("=")
                        select new { row = row, idx = row.IndexOf("=") }
                        ).ToDictionary(x => x.row.Substring(0, x.idx).ToLower(), x => x.row.Substring(x.idx + 1));
            return dict;
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
            }

            // Free any unmanaged objects here.
            //


            this.DisposeSelf();

            disposed = true;
        }



        protected virtual void DisposeSelf()
        {
            this.HalClose();
        }

        #endregion


    }
}
