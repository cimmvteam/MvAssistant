using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;

namespace MvaCToolkitCs.v1_2.Logging
{
    [Serializable]
    public class CtkLoggerMapper : Dictionary<String, CtkLogger>
    {


        ~CtkLoggerMapper()
        {
            CtkEventUtil.RemoveEventHandlersOfOwnerByFilter(this, (dlgt) => true);
        }

        public CtkLogger Get(String id = "")
        {
            lock (this)
            {
                if (!this.ContainsKey(id))
                {
                    var logger = new CtkLogger();
                    this.Add(id, logger);

                    this.OnCreated(new CtkLoggerMapperEventArgs() { LoggerId = id, Logger = logger });
                }
            }
            //不能 override/new this[] 會造成無窮迴圈
            return this[id];
        }





        #region Event
        public event EventHandler<CtkLoggerMapperEventArgs> EhCreated;
        void OnCreated(CtkLoggerMapperEventArgs ea)
        {
            if (this.EhCreated == null)
                return;
            this.EhCreated(this, ea);
        }
        #endregion




    }
}
