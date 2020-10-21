using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MaskAutoCleaner.v1_0.Recipe
{
    [Serializable]
    public class MacRecipeMachineState
    {
        [XmlAttribute]
        public string Key;
        [XmlAttribute]
        public string Value;

        public MacRecipeMachineState() { }
        public MacRecipeMachineState(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }


    }
}
