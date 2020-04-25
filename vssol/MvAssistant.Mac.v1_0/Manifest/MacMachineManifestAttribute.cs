using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Manifest
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    /// <summary>
    /// manifest defines HAL / ImpClass / Driver / HW relationships
    /// </summary>
    public class MacMachineManifestAttribute : Attribute
    {
        private MacEnumDevice device;

        /// <summary>
        /// Specify device for linking HAL/Driver/HW relationship
        /// </summary>
        public MacEnumDevice Device
        {
            get { return device; }
            set { device = value; }
        }

        public MacMachineManifestAttribute(MacEnumDevice _device)
        {
            Device = _device;
        }
    }
}
