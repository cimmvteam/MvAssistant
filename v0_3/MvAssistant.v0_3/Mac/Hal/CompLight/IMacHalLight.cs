using System.Runtime.InteropServices;

namespace MvAssistant.v0_3.Mac.Hal.CompLight
{
    [Guid("608C3A0B-6FDE-4CFD-9801-0CEF702E7451")]
    public interface IMacHalLight : IMacHalComponent
    {

        void TurnOn(int value);
        void TurnOff();
        int GetValue();

    }
}
