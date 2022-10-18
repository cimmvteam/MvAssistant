using CToolkitCs.v1_2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;



namespace CToolkitCs.v1_2.TypeGuid
{

    public class CtkTypeGuidMapper
    {

        static CtkTypeGuidMapper m_Singleton;
        SortedDictionary<Guid, Type> mapper;

        public SortedDictionary<Guid, Type> GetMapper()
        {
            if (this.mapper == null) this.mapper = LoadTypeMap();
            return this.mapper;
        }


        public List<Type> GetChilds<T>() where T : class
        {
            var type = typeof(T);

            var mapper = this.GetMapper();
            var query = from row in mapper.Values
                        where type.IsAssignableFrom(row)
                        select row;
            return query.ToList();
        }


        public List<Type> Get(Func<Type, bool> filter)
        {
            var mapper = this.GetMapper();
            var query = from row in mapper.Values
                        where filter(row)
                        select row;
            return query.ToList();
        }





        #region Static

        public static CtkTypeGuidMapper Singleton { get { if (m_Singleton == null) { m_Singleton = new CtkTypeGuidMapper(); } return m_Singleton; } }
        public static Type Get(Guid guid)
        {
            var coll = Singleton.GetMapper();
            return coll[guid];
        }
        public static Type Get(string guid) { return Get(Guid.Parse(guid)); }
        public static SortedDictionary<Guid, Type> LoadTypeMap(Func<string, bool> filter = null)
        {
            var collector = new SortedDictionary<Guid, Type>();
            var qAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assem in qAssemblies)
            {

                var qTypes = new List<Type>();


                try { qTypes = assem.GetTypes().ToList(); }
                catch (ReflectionTypeLoadException ex) { qTypes = ex.Types.ToList(); }
                qTypes = qTypes.Where(x => x != null).ToList();


                foreach (var t in qTypes)
                {
                    if (filter != null && !filter(t.FullName)) continue;

                    //var fullname = t.FullName;
                    ////Console.WriteLine(fullname);
                    //var guid_attrs = t.GetCustomAttributes(typeof(GuidAttribute), false);
                    //var guid = CtkUtil.TypeGuid(t);
                    //if (guid == null) continue;
                    //if (collector.ContainsKey(guid.Value))
                    //{
                    //    //var t2 = collector[guid.Value];
                    //    //throw new Exception(string.Format("Type {0} and {1} have same Guid", t.FullName, t2.FullName));
                    //}
                    //collector[guid.Value] = t;

                    collector[t.GUID] = t;
                }

            }

            return collector;

        }



        #endregion


    }
}