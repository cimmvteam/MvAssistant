using CToolkit.v1_1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.TypeGuid
{
    public class MvTypeGuidMapper
    {
        static MvTypeGuidMapper m_Singleton;
        SortedDictionary<Guid, Type> mapper;


        public SortedDictionary<Guid, Type> GetCollector()
        {
            if (this.mapper == null) this.mapper = LoadTypeMap();
            return this.mapper;
        }


        #region Static

        public static MvTypeGuidMapper Singleton { get { if (m_Singleton == null) { m_Singleton = new MvTypeGuidMapper(); } return m_Singleton; } }


        public static Type Get(Guid guid)
        {
            var coll = Singleton.GetCollector();
            return coll[guid];
        }
        public static Type Get(string guid) { return Get(Guid.Parse(guid)); }

        public static SortedDictionary<Guid, Type> LoadTypeMap(Func<string, bool> filter = null)
        {
            var collector = new SortedDictionary<Guid, Type>();
            var qAssemblies = AppDomain.CurrentDomain.GetAssemblies();


            foreach (var assem in qAssemblies)
            {
                var qTypes = assem.GetTypes().ToList();

                foreach (var t in qTypes)
                {
                    if (filter != null && !filter(t.FullName)) continue;

                    var fullname = t.FullName;

                    //Console.WriteLine(fullname);

                    var guid_attrs = t.GetCustomAttributes(typeof(GuidAttribute), false);
                    var guid = CtkUtil.TypeGuid(t);
                    if (guid == null) continue;
                    if (collector.ContainsKey(guid.Value))
                    {
                        //var t2 = collector[guid.Value];
                        //throw new Exception(string.Format("Type {0} and {1} have same Guid", t.FullName, t2.FullName));
                    }

                    collector[guid.Value] = t;
                }
            }
            return collector;
        }

        #endregion

    }
}
