using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component.Door
{
    [GuidAttribute("4113A5C4-2707-4587-B981-A76F7ED9E05A")]
    public interface IHalDoor : IMacHalComponent
    {
        string CheckDoorStatus();
        bool OpenDoor();
        bool CloseDoor();
    }
}
