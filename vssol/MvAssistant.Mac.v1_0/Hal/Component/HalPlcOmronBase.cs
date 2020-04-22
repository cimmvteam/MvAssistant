using MvAssistant.DeviceDrive.OmronPlc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Hal.Imp.Component
{
    public class HalPlcOmronBase : HalComponentBase
    {
        protected string plcAssembly;
        protected string plcComponent;



        public MvOmronPlcLdd Plc()
        {
            var compFullName = string.Format("{0}/{1}", plcAssembly, plcComponent);
            return MvOmronPlcMapper.GetOrDefault(compFullName);
        }
        public object PlcGetValue(string variable)
        {
            var plc = this.Plc();
            return plc.Read(variable);
        }

        public void PlcSetValue(string variable, object data)
        {
            var plc = this.Plc();
            plc.Write(variable, data);
        }



        public void PlcSetup()
        {
            var dict = this.DevSettings;

            this.plcAssembly = dict["Assembly"];
            this.plcComponent = dict["Component"];
        }


    }
}
