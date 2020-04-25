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
    public class MachineManifestAttribute : Attribute
    {
        private EnumDevice device;

        /// <summary>
        /// Specify device for linking HAL/Driver/HW relationship
        /// </summary>
        public EnumDevice Device
        {
            get { return device; }
            set { device = value; }
        }

        public MachineManifestAttribute(EnumDevice _device)
        {
            Device = _device;
        }
    }
}
