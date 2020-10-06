using CToolkit.v1_1;
using MvAssistant.TypeGuid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MaskAutoCleaner.v1_0.Machine
{
    [Serializable]
    public class MacMachineCtrlCfg
    {
        public string ID;

        public MvTypeGuid MachineCtrlType;

        public string HalId;
    }
}
