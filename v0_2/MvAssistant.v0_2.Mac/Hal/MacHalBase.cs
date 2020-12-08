using MvAssistant.Mac.v1_0;
using MvAssistant.Mac.v1_0.Manifest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal
{
    [GuidAttribute("8592A98B-C7DD-46C3-9965-BFD952A7742A")]
    public abstract class MacHalBase : IHal, IDisposable
    {
        public MacHalContext HalContext;
        public MacManifestDeviceCfg HalDeviceCfg;
        public MacManifestDriverCfg HalDriverCfg;
        public Dictionary<string, MacHalBase> Hals = new Dictionary<string, MacHalBase>();
        ~MacHalBase() { this.Dispose(false); }
        public string ID { get { return this.HalDeviceCfg.ID; } }


        #region Machines Get/Set

        public MacHalBase this[string key] { get { return this.GetHalDevice(key); } set { this.SetHalDevice(key, value); } }
        public MacHalBase this[MacEnumDevice key] { get { return this.GetHalDevice(key); } set { this.SetHalDevice(key, value); } }
        public MacHalBase GetHalDevice(MacEnumDevice key) { return this.GetHalDevice(key.ToString()); }
        public MacHalBase GetHalDevice(string key)
        {
            var hals = (from row in this.Hals
                        where row.Key == key
                        select row).ToList();

            if (hals.Count == 0) throw new MacException("No exist machine");
            else if (hals.Count > 1) throw new MacException("Duplicate machine");

            return hals.FirstOrDefault().Value;
        }
        public bool IsContainDevice(MacEnumDevice key) { return this.IsContainDevice(key.ToString()); }
        public bool IsContainDevice(string key)
        {
            var qhals = (from row in this.Hals
                         where row.Key == key
                         select row);
            return qhals.Count() > 0;
        }


        public void SetHalDevice(MacEnumDevice key, MacHalBase hal) { this.SetHalDevice(key, hal); }
        public void SetHalDevice(string key, MacHalBase hal) { this.Hals[key] = hal; }

        #endregion

        #region IHal

        public abstract int HalClose();
        public abstract int HalConnect();
        public virtual bool HalIsConnected() { throw new NotImplementedException(); }
        public virtual int HalStop() { throw new NotImplementedException(); }

        #endregion

        #region Device Setting

        private Dictionary<string, string> m_DevSettings;
        public Dictionary<string, string> DevSettings
        {
            get
            {
                if (this.m_DevSettings == null) { this.m_DevSettings = this.GetDevConnStr(); }
                return this.m_DevSettings;
            }
        }

        protected string DeviceConnStr { get { return this.HalDeviceCfg.DevConnStr; } }
        public string GetDevConnStr(string key) { return this.DevSettings[key.ToLower()]; }

        public T GetDevConnStrEnum<T>(string key) { return MvUtil.EnumParse<T>(this.DevSettings[key] as string); }

        public int GetDevConnStrInt(string key) { return Int32.Parse(this.DevSettings[key.ToLower()]); }
        protected Dictionary<string, string> GetDevConnStr()
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
