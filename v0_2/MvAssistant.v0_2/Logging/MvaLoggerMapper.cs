using CToolkit.v1_1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MvAssistant.v0_2.Logging
{
    [Serializable]
    public class MvaLoggerMapper : Dictionary<String, MvaLogger>
    {


        ~MvaLoggerMapper()
        {
            CtkEventUtil.RemoveEventHandlersOfOwnerByFilter(this, (dlgt) => true);
        }

        public MvaLogger Get(String id = "")
        {
            lock (this)
            {
                if (!this.ContainsKey(id))
                {
                    var logger = new MvaLogger();
                    this.Add(id, logger);

                    this.OnCreated(new MvaLoggerMapperEventArgs() { LoggerId = id, Logger = logger });
                }
            }
            //不能 override/new this[] 會造成無窮迴圈
            return this[id];
        }


        public void RegisterAllLogger(EventHandler<MvaLoggerEventArgs> evt, Func<string, bool> filter = null)
        {
            MvaLoggerMapper.Singleton.evtCreated += delegate (object ss, MvaLoggerMapperEventArgs ee)
            {
                if (filter != null && !filter(ee.LoggerId)) return;
                ee.Logger.EhLogWrite += evt;
            };
            foreach (var kv in MvaLoggerMapper.Singleton)
            {
                if (filter != null && !filter(kv.Key)) continue;
                kv.Value.EhLogWrite += evt;
            }
        }



        #region Event
        public event EventHandler<MvaLoggerMapperEventArgs> evtCreated;
        void OnCreated(MvaLoggerMapperEventArgs ea)
        {
            if (this.evtCreated == null)
                return;
            this.evtCreated(this, ea);
        }
        #endregion






        //--- Static ---------------------------



        static MvaLoggerMapper m_Singleton;

        public static MvaLoggerMapper Singleton
        {
            get
            {

                if (m_Singleton == null) m_Singleton = new MvaLoggerMapper();
                return m_Singleton;
            }

        }

    }


}
