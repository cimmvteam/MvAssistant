using CToolkit.v1_1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MvAssistant.Logging
{
    [Serializable]
    public class MvLoggerMapper : Dictionary<String, MvLogger>
    {


        ~MvLoggerMapper()
        {
            CtkEventUtil.RemoveEventHandlersOfOwnerByFilter(this, (dlgt) => true);
        }

        public MvLogger Get(String id = "")
        {
            lock (this)
            {
                if (!this.ContainsKey(id))
                {
                    var logger = new MvLogger();
                    this.Add(id, logger);

                    this.OnCreated(new MvLoggerMapperEventArgs() { LoggerId = id, Logger = logger });
                }
            }
            //不能 override/new this[] 會造成無窮迴圈
            return this[id];
        }


        public void RegisterAllLogger(EventHandler<MvLoggerEventArgs> evt, Func<string, bool> filter = null)
        {
            MvLoggerMapper.Singleton.evtCreated += delegate (object ss, MvLoggerMapperEventArgs ee)
            {
                if (filter != null && !filter(ee.LoggerId)) return;
                ee.Logger.EhLogWrite += evt;
            };
            foreach (var kv in MvLoggerMapper.Singleton)
            {
                if (filter != null && !filter(kv.Key)) continue;
                kv.Value.EhLogWrite += evt;
            }
        }



        #region Event
        public event EventHandler<MvLoggerMapperEventArgs> evtCreated;
        void OnCreated(MvLoggerMapperEventArgs ea)
        {
            if (this.evtCreated == null)
                return;
            this.evtCreated(this, ea);
        }
        #endregion






        //--- Static ---------------------------



        static MvLoggerMapper m_Singleton;

        public static MvLoggerMapper Singleton
        {
            get
            {

                if (m_Singleton == null) m_Singleton = new MvLoggerMapper();
                return m_Singleton;
            }

        }

    }


}
