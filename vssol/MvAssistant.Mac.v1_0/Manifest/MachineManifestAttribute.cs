using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Manifest
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    /// <summary>
    /// manifest defines HAL / ImpClass / Driver / HW relationships
    /// </summary>
    public class MachineManifestAttribute : Attribute
    {
        private DeviceEnum device;

        /// <summary>
        /// Specify device for linking HAL/Driver/HW relationship
        /// </summary>
        public DeviceEnum Device
        {
            get { return device; }
            set { device = value; }
        }

        public MachineManifestAttribute(DeviceEnum _device)
        {
            Device = _device;
        }
    }
}
