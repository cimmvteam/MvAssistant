using MvAssistant.v0_2.Mac.Hal.CompLight;
using MvAssistant.v0_2.Mac.Manifest;
using MvAssistant.v0_2.Mac.Manifest.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.Mac.Hal
{
    /// <summary>
    /// HAL assembly, comporot or device resource manager
    /// </summary>
    public class MacHalContext : IMvaContextFlow, IHal, IDisposable
    {
        public Dictionary<string, MacHalBase> HalDevices = new Dictionary<string, MacHalBase>();
        public string Path;
        public String DeviceId { get { return null; } }
        MacManifestCfg manifest;
        public MacHalContext(string path = null)
        {
            this.Path = path;
        }

        ~MacHalContext() { this.Dispose(false); }




        void HalCreate(MacManifestDeviceCfg deviceCfg, MacHalBase hal = null)
        {
            var drivers = (from row in this.manifest.Drivers
                           where row.DriverId == deviceCfg.DriverId
                           select row).ToList();

            //Check driver count
            if (drivers.Count == 0)
                throw new MacWrongDriverException("No Driver");
            else if (drivers.Count > 1)
                throw new MacWrongDriverException("Duplicate Driver");

            var driver = drivers.FirstOrDefault();
            var type = driver.AssignType;
            var inst = Activator.CreateInstance(type) as MacHalBase;

            if (inst == null) throw new MacHalObjectNotFoundException();

            inst.HalDeviceCfg = deviceCfg;
            inst.HalDriverCfg = driver;
            inst.HalContext = this;

            if (hal == null)
                this.HalDevices[deviceCfg.DeviceId] = inst;
            else hal.Hals[deviceCfg.DeviceId] = inst;


            if (deviceCfg.Devices == null) return;
            foreach (var dcv in deviceCfg.Devices)
            {
                HalCreate(dcv, inst);
            }


        }


        #region IMvContextFlow

        public int MvaCfFree()
        {
            this.DisposeSelf();
            return 0;
        }
        public int MvaCfBookup()
        {
            if (!string.IsNullOrEmpty(this.Path))
                this.manifest = MacManifestCfg.LoadFromXmlFile(this.Path);

            this.Check();

            foreach (var dcv in this.manifest.Devices)
            {
                this.HalCreate(dcv);
            }

            return 0;
        }
        public int MvaCfLoad()
        {

            return 0;
        }
        public int MvaCfUnload()
        {
            this.HalClose();
            return 0;
        }

        #endregion



        #region IHal

        public int HalClose()
        {

            //關閉所有HAL
            foreach (var kv in this.HalDevices)
            {
                try { this.HalClose(kv.Value); }
                catch (Exception ex) { MvaLog.WarnNs(this, ex); }
            }

            //釋放資源
            foreach (var kv in this.Resources)
            {
                try
                {
                    if (kv.Value == null) continue;
                    kv.Value.Dispose();
                }
                catch (Exception ex) { MvaLog.WarnNs(this, ex); }
            }



            return 0;
        }

        public int HalConnect()
        {
            foreach (var kv in this.HalDevices)
            {
                this.HalConnect(kv.Value);
            }
            return 0;
        }

        public bool HalIsConnected()
        {
            throw new NotImplementedException();
        }

        public int HalStop()
        {
            throw new NotImplementedException();
        }



        #region Recurrsive HAL

        private void HalClose(MacHalBase hal)
        {
            hal.HalClose();
            foreach (var kv in hal.Hals)
            {
                this.HalClose(kv.Value);
            }
        }

        private void HalConnect(MacHalBase hal)
        {
            hal.HalConnect();
            foreach (var kv in hal.Hals)
            {
                this.HalConnect(kv.Value);
            }


        }

        #endregion

        #endregion



        #region Check

        public void Check()
        {
            this.CheckDriverId();
        }
        /// <summary>
        /// Check driver id is unique
        /// </summary>
        public void CheckDriverId()
        {
            var dict = new Dictionary<string, int>();//Driver Id with Count

            foreach (var driver in this.manifest.Drivers)
            {
                var key = driver.DriverId;
                if (!dict.ContainsKey(key)) dict[key] = 0;
                dict[key]++;
            }

            var duplicates = (from row in dict
                              where row.Value > 1
                              select row).ToList();

            foreach (var did in duplicates)
            {
                MvaLog.WarnNs(this, "Duplicate driver id: {0}", did.Key);
            }

            if (duplicates.Count != 0)
            {
                var did = duplicates.FirstOrDefault().Key;
                throw new MacWrongDriverException("Exist non-unique driver id " + did);
            }


        }

        #endregion


        #region Resource Manage

        protected Dictionary<string, IDisposable> Resources = new Dictionary<string, IDisposable>();

        public T ResourceGetOrDefault<T>(string key) where T : IDisposable
        {
            lock (this)
            {
                if (this.Resources.ContainsKey(key)) return (T)this.Resources[key];
                return default(T);
            }
        }

        public T ResourceGetOrRegister<T>(string key, Func<T> creator) where T : IDisposable
        {
            lock (this)
            {
                if (this.Resources.ContainsKey(key)) return (T)this.Resources[key];
                var rsc = creator();
                this.Resources[key] = rsc;
                return rsc;
            }
        }

        public void ResourceRegister<T>(string key, T resource) where T : IDisposable
        {
            lock (this)
            {
                if (this.Resources.ContainsKey(key)) throw new MacException("Resource key is exist");
                this.Resources[key] = resource;
            }
        }

        public void ResourceDispose()
        {
            foreach (var rsc in this.Resources)
            {
                try { rsc.Value.Dispose(); }
                catch (Exception ex) { MvaLog.WarnNs(this, ex); }
            }
        }

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
            this.HalClose();
        }

        #endregion



        #region Static - Create

        public static MacHalContext Create(string path)
        {
            return new MacHalContext()
            {
                Path = path,
            };
        }


        #endregion


    }
}
