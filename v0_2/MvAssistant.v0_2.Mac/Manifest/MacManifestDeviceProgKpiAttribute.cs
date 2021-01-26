using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.Mac.Manifest
{
    public class MacManifestDeviceProgKpiAttribute : Attribute
    {
        public string Category;
        public string Name;

        public EnumManifestDeviceProgStatus Status;

        public MacManifestDeviceProgKpiAttribute(string category, string name, EnumManifestDeviceProgStatus status)
        {
            this.Category = category;
            this.Name = name;
            this.Status = status;
        }






        public enum EnumManifestDeviceProgStatus
        {
            None,
            Install,
            Cabling,
            Driver,
            SoftTest,

            DontInstall,

            FailInstall,
            FailCabling,
            FailDriver,
            FailSoftTest,


        }


    }
}
