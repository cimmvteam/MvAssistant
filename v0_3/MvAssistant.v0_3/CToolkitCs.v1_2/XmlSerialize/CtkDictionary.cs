using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CToolkitCs.v1_2.XmlSerialize
{

    [Serializable]
    public class CtkDictionary
    {
        public string this[string key]
        {
            get
            {
                return (from kv in KeyValues
                        where kv.Key == key
                        select kv.Value).FirstOrDefault();
            }
            set
            { SetOrCreate(key, value); }
        }

        public HashSet<CtkKeyValuePair> KeyValues = new HashSet<CtkKeyValuePair>();



        private CtkKeyValuePair Add(string key, string value)
        {
            CtkKeyValuePair askv = new CtkKeyValuePair() { Key = key, Value = value };
            this.KeyValues.Add(askv);
            return askv;
        }
        public void SetOrCreate(string key, string value)
        {
            CtkKeyValuePair askv = GetByKey(key);
            if (askv == null) { askv = this.Add(key, value); }
            askv.Value = value;
        }
        public CtkKeyValuePair GetByKey(string key)
        {
            return (from kv in KeyValues
                    where kv.Key == key
                    select kv).FirstOrDefault();
        }
    }
}
