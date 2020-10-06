using MvAssistant.Mac.v1_0.Hal.Component.Button;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component.Button
{
    [GuidAttribute("5AC9EC7B-F39A-45BD-AAD9-3DD07A5FE0DF")]
    public class HalButton : MacHalComponentBase, IHalButton
    {
        public bool IsPressedOpen() { throw new NotImplementedException(); }
        public bool IsPressedClose() { throw new NotImplementedException(); }
        public bool IsProcessComplete() { throw new NotImplementedException(); }

        #region IHal

        int IHal.HalConnect()
        {
            throw new NotImplementedException();
        }

        int IHal.HalClose()
        {
            throw new NotImplementedException();
        }

        bool IHal.HalIsConnected()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
