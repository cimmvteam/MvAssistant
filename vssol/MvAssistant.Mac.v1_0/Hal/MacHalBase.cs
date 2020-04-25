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
        public MacMachineDeviceCfg MachineDeviceCfg;
        public MacMachineDriverCfg MachineDriverCfg;
        public Dictionary<string, HalBase> Machines = new Dictionary<string, HalBase>();
        private Dictionary<string, string> m_DevSettings;

        ~HalBase() { this.Dispose(false); }
        public Dictionary<string, string> DevSettings
        {
            get
            {
                if (this.m_DevSettings == null) { this.m_DevSettings = this.GetDevSettings(); }
                return this.m_DevSettings;
            }
        }
        public string ID { get { return this.MachineDeviceCfg.ID; } }
        protected string DeviceConnStr { get { return this.MachineDeviceCfg.DevConnStr; } }

        public HalBase this[string key] { get { return this.GetMachine(key); } set { this.SetMachine(key, value); } }
        public HalBase this[MacEnumDevice key] { get { return this.GetMachine(key); } set { this.SetMachine(key, value); } }


        public HalBase GetMachine(MacEnumDevice key) { return this.GetMachine(key.ToString()); }
        public HalBase GetMachine(string key)
        {
            var machines = (from row in this.Machines
                            where row.Key == key
                            select row).ToList();

            if (machines.Count == 0) throw new MacException("No exist machine");
            else if (machines.Count > 1) throw new MacException("Duplicate machine");

            return machines.FirstOrDefault().Value;
        }
        public void SetMachine(MacEnumDevice key, HalBase hal) { this.SetMachine(key, hal); }
        public void SetMachine(string key, HalBase hal) { this.Machines[key] = hal; }
        private Dictionary<string, string> GetDevSettings()
        {
            if (string.IsNullOrEmpty(this.DeviceConnStr)) return new Dictionary<string, string>();

            var settings = (from row in this.DeviceConnStr.Split(new char[] { ';' })
                            where !string.IsNullOrEmpty(row)
                            select row.Trim()).ToList();
            var dict = (from row in settings
                        where row.Contains("=")
                        select new { row = row, idx = row.IndexOf("=") }
                        ).ToDictionary(x => x.row.Substring(0, x.idx), x => x.row.Substring(x.idx + 1));
            return dict;
        }


        #region IHal

        public virtual int HalClose()
        {
            throw new NotImplementedException();
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
