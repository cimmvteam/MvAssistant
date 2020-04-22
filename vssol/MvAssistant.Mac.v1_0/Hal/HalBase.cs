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
        ~HalBase() { this.Dispose(false); }

        private Dictionary<string, string> m_DevSettings;
        public Dictionary<string, string> DevSettings
        {
            get
            {
                if (this.m_DevSettings == null) { this.m_DevSettings = this.GetDevSettings(); }
                return this.m_DevSettings;
            }
        }

        public string DeviceConnStr { get; set; }
        public string ID { get; set; }
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
