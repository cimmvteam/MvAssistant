using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CToolkitCs.v1_2.XmlSerialize
{
    [Serializable]
    public class CtkKeyValuePair
    {
        private string m_key;
        [System.Xml.Serialization.XmlAttribute]
        public string Key
        {
            get { return m_key; }
            set { m_key = value; }
        }

        private string m_value;
        [System.Xml.Serialization.XmlAttribute]
        public string Value
        {
            get { return m_value; }
            set { m_value = value; }
        }
    }
}
