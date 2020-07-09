using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.TypeMap
{
    public class MvTypeMapper
    {
        static MvTypeMapper m_Singleton;
        SortedDictionary<string, Type> mapper;


        public SortedDictionary<string, Type> GetCollector()
        {
            if (this.mapper == null) this.mapper = LoadTypeMap();
            return this.mapper;
        }


        #region Static

        public static MvTypeMapper Singleton { get { if (m_Singleton == null) { m_Singleton = new MvTypeMapper(); } return m_Singleton; } }

        public static Type Get(string guid)
        {
            var coll = Singleton.GetCollector();
            return coll[guid];
        }

        public static Type Get(Guid guid) { return Get(guid.ToString()); }

        public static SortedDictionary<string, Type> LoadTypeMap(Func<string, bool> filter = null)
        {
            var collector = new SortedDictionary<string, Type>();
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
                    var guid = guid_attrs.FirstOrDefault() as GuidAttribute;
                    if (guid == null)
                    {
                        //throw new Exception(string.Format("Type {0} is no Guid Attribute", t.FullName));
                        continue;
                    }

                    if (collector.ContainsKey(guid.Value))
                    {
                        var t2 = collector[guid.Value];
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
