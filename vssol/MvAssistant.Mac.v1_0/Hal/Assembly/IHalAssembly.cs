using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Hal.Intf.Assembly
{
    [GuidAttribute("FB454402-72E5-4C11-8B5C-F81D82591669")]
    public interface IHalAssembly : IHal
    {

        //string ID { get; set; }//Identifier;

        /// <summary>
        /// Only for assembly level
        /// 偵測到sub-assemblies out spec or exception
        /// assembly must stop any action
        /// </summary>
        /// <returns>0: succeed to stop; 1: failed to stop; 2以上各assembly可自行定義</returns>
        int HalStop();
    }
}
