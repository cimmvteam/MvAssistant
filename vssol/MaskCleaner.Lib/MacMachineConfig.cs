using MvLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MaskAutoCleaner
{
    [Serializable]
    public class MacMachineConfig
    {


        public string Manifest = "UserData/Manifest/MachineManifest.xml";
        public string RecipePathMaskTransfer = "UserData/RecipeFlow/MaskTransfer";


        public static MacMachineConfig LoadXmlFile(string fn = "UserData/Eqp.config")
        {
            return MvUtil.LoadFromXmlFile<MacMachineConfig>(fn);
        }

        public void SaveXmlFile(string fn = "UserData/Eqp.config") { MvUtil.SaveToXmlFile(this, fn); }
    }
}
