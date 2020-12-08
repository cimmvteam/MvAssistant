using MvAssistant.v0_2.Mac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.Mac.Hal
{
    /// <summary>
    /// abstract fake HAL (Hardware Abstract Layer) class
    /// </summary>
    [GuidAttribute("A0D5CD14-8A44-46EA-AF8C-DF2724A7517B")]
    public class MacHalFakeComponentBase : MacHalBase, IHalFake
    {


   

        public override int HalClose()
        {
            throw new NotImplementedException();
        }

        public override int HalConnect()
        {
            throw new NotImplementedException();
        }
    }
}