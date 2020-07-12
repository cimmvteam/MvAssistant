using MvAssistant.DeviceDrive.KjMachineDrawer;
using MvAssistant.Mac.v1_0.Hal.CompDrawer;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace MvAssistant.Mac.v1_0.Hal.Assembly
{
    [GuidAttribute("002CE873-5909-4874-96BF-6CD3971DAB39")]
    public interface IMacHalCabinet : IMacHalAssembly
    {
       /** To Drop
        /// <summary>Cabinet 存放 Drawer 的容器</summary>
        Dictionary<string, IMacHalDrawer> Drawers { get; set; }
      
        /// <summary>取出某一特定Index 的 Drawer</summary>
        /// <param name="index"></param>
        /// <returns></returns>
        IMacHalDrawer GetDrawer(string index);
       
       
        /// <summary>產生 Drawer的集合</summary>
        void CreateDrawers<T>(Dictionary<string,HalBase> hals) where T :HalBase, IMacHalDrawer, new();
        */
    }
}
