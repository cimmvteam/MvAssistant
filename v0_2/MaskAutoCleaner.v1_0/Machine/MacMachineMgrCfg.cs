using MvAssistant.v0_2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace MaskAutoCleaner.v1_0.Machine
{
    public class MacMachineMgrCfg
    {
        public string ManifestCfgPath;


        public List<MacMachineCtrlCfg> MachineCtrls = new List<MacMachineCtrlCfg>();



        public void SaveToXmlFile(string fn) { MvaUtil.SaveToXmlFile(this, fn); }

        #region static function


        /// <summary>
        /// deserialize (反序列化) MachineManifest object from xml file
        /// </summary>
        /// <param name="filePath">xml file path for loading</param>
        /// <returns></returns>
        public static MacMachineMgrCfg LoadFromXmlFile(string filePath = "../../UserData/MachineMgr.config.fake") { return MvaUtil.LoadFromXmlFile<MacMachineMgrCfg>(filePath); }
        /// <summary>
        /// serialize (序列化) manifest object, and save as xml file
        /// </summary>
        /// <param name="cfg">MachineManifest object</param>
        /// <param name="filePath">xml file path for saving</param>
        public static void SaveToXmlFile(MacMachineMgrCfg cfg, string filePath) { MvaUtil.SaveToXmlFile<MacMachineMgrCfg>(cfg, filePath); }

        #endregion



    }
}
