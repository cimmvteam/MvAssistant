using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Manifest
{
    public interface IManifestMachine
    {
        string DeviceConnStr { get; set; }
        string ID { get; set; }

    }
}
