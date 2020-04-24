using MvAssistant.Mac.v1_0.Hal.Component;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace MvAssistant.Mac.v1_0.Hal.ComponentFake.Plc
{
    [GuidAttribute("DDC7BD3A-C7FF-40FD-9A0D-283EA4C8772A")]
    public class HalPlcFake : HalFakeBase, IHalPlc
    {
        object FakeData = null;


        public HalPlcFake() { }

        public Object GetValue(string varName) { return this.FakeData; }

        public void SetValue(string varName, Object data) { this.FakeData = data; }




    }
}
