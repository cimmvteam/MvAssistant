using MvAssistant.v0_3.Mac;
using MvAssistant.v0_3.Mac.Manifest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.Mac.Hal
{
    [GuidAttribute("8592A98B-C7DD-46C3-9965-BFD952A7742A")]
    public abstract class MacHalBase : IHal, IDisposable
    {
        public MacHalContext HalContext;
        public MacManifestDeviceCfg HalDeviceCfg = new MacManifestDeviceCfg();
        public MacManifestDriverCfg HalDriverCfg = new MacManifestDriverCfg();
        public Dictionary<string, MacHalBase> Hals = new Dictionary<string, MacHalBase>();
        ~MacHalBase() { this.Dispose(false); }
        public string DeviceId { get { return this.HalDeviceCfg.DeviceId; } }


        #region Machines Get/Set

        public MacHalBase this[string key] { get { return this.GetHalDevice(key); } set { this.SetHalDevice(key, value); } }
        public MacHalBase this[EnumMacDeviceId key] { get { return this.GetHalDevice(key); } set { this.SetHalDevice(key, value); } }
        public MacHalBase GetHalDevice(EnumMacDeviceId key) { return this.GetHalDevice(key.ToString()); }
        public MacHalBase GetHalDevice(string key)
        {
            var hals = (from row in this.Hals
                        where row.Key == key
                        select row).ToList();

            if (hals.Count == 0) throw new MacException("No exist machine");
            else if (hals.Count > 1) throw new MacException("Duplicate machine");

            return hals.FirstOrDefault().Value;
        }

        public MacHalBase GetHalDeviceOrDefault(EnumMacDeviceId key) { return this.GetHalDeviceOrDefault(key.ToString()); }
        public MacHalBase GetHalDeviceOrDefault(string key)
        {
            var hals = (from row in this.Hals
                        where row.Key == key
                        select row).ToList();

            if (hals.Count == 0) return null;
            else if (hals.Count > 1) throw new MacException("Duplicate machine");

            return hals.FirstOrDefault().Value;
        }
        public bool IsContainDevice(EnumMacDeviceId key) { return this.IsContainDevice(key.ToString()); }
        public bool IsContainDevice(string key)
        {
            var qhals = (from row in this.Hals
                         where row.Key == key
                         select row);
            return qhals.Count() > 0;
        }


        public void SetHalDevice(EnumMacDeviceId key, MacHalBase hal) { this.SetHalDevice(key, hal); }
        public void SetHalDevice(string key, MacHalBase hal) { this.Hals[key] = hal; }

        #endregion

        #region IHal



        public abstract int HalClose();
        public abstract int HalConnect();
        public virtual bool HalIsConnected() { throw new NotImplementedException(); }
        public virtual int HalStop() { throw new NotImplementedException(); }

        #endregion

        #region Device Setting


        protected Dictionary<string, string> DeviceConnSetting
        {
            get { return this.ReadDevConnSetting(); }
            set { this.WriteDevConnSetting(value); }
        }


        public string GetDevConnSetting(string key) { return this.DeviceConnSetting[key.ToLower()]; }

        public T GetDevConnSettingEnum<T>(string key) { return MvaUtil.EnumParse<T>(this.GetDevConnSetting(key)); }

        public int GetDevConnSettingInt(string key) { return Int32.Parse(this.GetDevConnSetting(key)); }


        public void SetDevConnSetting(String key, String value)
        {
            var dict = this.ReadDevConnSetting();
            dict[key.ToLower()] = value;
            this.WriteDevConnSetting(dict);
        }
        public void SetDevConnSetting(String key, int value) { this.SetDevConnSetting(key, value + ""); }




        protected Dictionary<string, string> ReadDevConnSetting()
        {
            if (this.HalDeviceCfg == null) return new Dictionary<string, string>();
            var connSettingStr = this.HalDeviceCfg.DevConnStr;

            var settings = (from row in connSettingStr.Split(new char[] { ';' })
                            where !string.IsNullOrEmpty(row)
                            select row.Trim()).ToList();
            var dict = (from row in settings
                        where row.Contains("=")
                        select new { row = row, idx = row.IndexOf("=") }
                        ).ToDictionary(x => x.row.Substring(0, x.idx).Trim().ToLower(), x => x.row.Trim().Substring(x.idx + 1));
            return dict;
        }

        protected void WriteDevConnSetting(Dictionary<String, String> dict)
        {
            if (this.HalDeviceCfg == null) this.HalDeviceCfg = new MacManifestDeviceCfg();

            var list = (from row in dict
                        select row.Key + "=" + row.Value).ToList();
            this.HalDeviceCfg.DevConnStr = String.Join(";", list);
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
