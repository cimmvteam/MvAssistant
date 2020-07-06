using System.Runtime.InteropServices;

namespace MvAssistant.Mac.v1_0.Hal.Assembly
{
    [GuidAttribute("002CE873-5909-4874-96BF-6CD3971DAB39")]
    public interface IMacHalCabinet : IMacHalAssembly
    {
        object CreateDrawer(object param);

    }
}
