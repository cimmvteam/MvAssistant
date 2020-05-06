using MvAssistant.Mac.v1_0.Manifest;
using MvAssistant.Mac.v1_0.Manifest.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal
{
    /// <summary>
    /// HAL assembly, comporot or device resource manager
    /// </summary>
    public class MacHalContext : IHal, IDisposable
    {
        MacManifestCfg manifest;
        public string Path;

        public Dictionary<string, HalBase> HalDevices = new Dictionary<string, HalBase>();

        public MacHalContext(string path = null)
        {
            this.Path = path;
        }



        void HalCreator(MacManifestDeviceCfg deviceCfg, HalBase hal = null)
        {
            var drivers = (from row in this.manifest.Drivers
                           where row.DriverId == deviceCfg.DriverId
                           select row).ToList();

            //Check driver count
            if (drivers.Count == 0)
                throw new WrongDriverException("No Driver");
            else if (drivers.Count > 1)
                throw new WrongDriverException("Duplicate Driver");

            var driver = drivers.FirstOrDefault();
            var type = driver.AssignType;
            var inst = Activator.CreateInstance(type) as HalBase;

            if (inst == null) throw new MacHalObjectNotFoundException();

            inst.HalDeviceCfg = deviceCfg;
            inst.HalDriverCfg = driver;

            if (hal == null)
                this.HalDevices[deviceCfg.DeviceName] = inst;
            else hal.Hals[deviceCfg.DeviceName] = inst;


            if (deviceCfg.Devices == null) return;
            foreach (var dcv in deviceCfg.Devices)
            {
                HalCreator(dcv, inst);
            }


        }


        #region Context Flow

        public void Load()
        {
            if (!string.IsNullOrEmpty(this.Path))
                this.manifest = MacManifestCfg.LoadFromXmlFile(this.Path);

            this.Check();


            foreach (var dcv in this.manifest.Devices)
            {
                this.HalCreator(dcv);
            }
        }







        public void Unload()
        {

            foreach (var dcv in this.manifest.Devices)
            {
            }

        }


        #endregion


        #region HAL



        private void HalConnect(HalBase hal)
        {
            hal.HalConnect();
            foreach (var kv in hal.Hals)
            {
                this.HalConnect(kv.Value);
            }


        }

        public int HalClose()
        {
            throw new NotImplementedException();
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
                MvLog.WarnNs(this, "Duplicate driver id: {0}", did.Key);
            }

            if (duplicates.Count != 0)
            {
                var did = duplicates.FirstOrDefault().Key;
                throw new WrongDriverException("Exist non-unique driver id " + did);
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

        }



        #endregion

    }
}
