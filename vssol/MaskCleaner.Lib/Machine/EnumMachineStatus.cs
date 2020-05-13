using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Machine
{
    public enum EnumMachineStatus
    {
        Created,
        Initialized,
        Loaded,
        TaskReady,
        Running,
        IsRequestCancel,
        Close,


    }
}
