using MvAssistant;
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


        [XmlIgnore] public Dictionary<Type, Type> MachineHalMap = new Dictionary<Type, Type>();


        public List<MacMachineMap> MachineHalMapList
        {
            get
            {
                return (from row in MachineHalMap.ToList()
                        select new MacMachineMap()
                        {
                            HalType = row.Key,
                            CtrlMachineType = row.Value,
                        }).ToList();
            }
        }










        public void SaveToXmlFile(string fn) { MvUtil.SaveToXmlFile(this, fn); }

        #region static function


        /// <summary>
        /// deserialize (反序列化) MachineManifest object from xml file
        /// </summary>
        /// <param name="filePath">xml file path for loading</param>
        /// <returns></returns>
        public static MacMachineMgrCfg LoadFromXmlFile(string filePath = "MacMachineMgr.config") { return MvUtil.LoadFromXmlFile<MacMachineMgrCfg>(filePath); }
        /// <summary>
        /// serialize (序列化) manifest object, and save as xml file
        /// </summary>
        /// <param name="cfg">MachineManifest object</param>
        /// <param name="filePath">xml file path for saving</param>
        public static void SaveToXmlFile(MacMachineMgrCfg cfg, string filePath) { MvUtil.SaveToXmlFile<MacMachineMgrCfg>(cfg, filePath); }

        #endregion



    }
}
