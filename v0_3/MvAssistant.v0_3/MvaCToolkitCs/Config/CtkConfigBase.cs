using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvaCToolkitCs.v1_2.Config
{
    public abstract class CtkConfigBase
    {

        public void SaveXml(string fn) { CtkUtil.SaveXmlFile(this, fn); }

        public static T LoadXml<T>(string fn) where T : class, new() { return CtkUtil.LoadXmlFileOrNew<T>(fn); }


    }
}
