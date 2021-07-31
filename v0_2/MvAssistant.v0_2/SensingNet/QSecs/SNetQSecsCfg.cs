using CToolkit.v1_1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SensingNet.v0_2.QSecs
{
    /// <summary>
    /// Query Svid
    /// </summary>
    public class SNetQSecsCfg
    {
        [XmlAttribute] public String LocalUri;
        [XmlAttribute] public String RemoteUri;

        public List<SNetQSvidCfg> QSvidCfgList = new List<SNetQSvidCfg>();


        public void SaveToXmlFile(string fn) { CtkUtil.SaveToXmlFileT(this, fn); }

    }

}
