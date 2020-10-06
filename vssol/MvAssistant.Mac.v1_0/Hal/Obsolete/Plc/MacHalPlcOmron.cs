using MvAssistant.Mac.v1_0.Hal.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using MvAssistant.DeviceDrive.OmronPlc;

namespace MvAssistant.Mac.v1_0.Hal.Component.Plc
{
    [GuidAttribute("75427515-83D5-46F6-8CD4-F87BDBD88620")]
    public class MacHalPlcOmron : MacHalComponentBase, IHalPlc
    {
        MvOmronPlcLdd OmronPLC;

        public Object GetValue(string varName)
        {
            return OmronPLC.Read(varName);
        }

        public void SetValue(string varName, Object value)
        {
            OmronPLC.Write(varName, value);
        }

        public MvOmronPlcLdd GetPLC_Comp()
        {
            return OmronPLC;
        }


        #region Override

        public override int HalConnect()
        {

            if (this.OmronPLC == null)
            {
                //需要安裝Omron CX-Complet library才能使用
                if (!File.Exists(@"C:\Program Files (x86)\OMRON\CX-Compolet\assembly\Sgw.1.7.0.0\CIPObjectLibrary.DLL"))
                    throw new Exception("請安裝Omron CX-Compolet");//return -1;


                var dict = this.DevSettings;

                var asmName = dict["Assembly"];
                var compName = this.ID;
                var ip = dict["IP"];
                var portId = Convert.ToInt32(dict["PortId"]);

                var compFullName = string.Format("{0}/{1}", asmName, compName);


                this.OmronPLC = MvOmronPlcMapper.Create(compFullName);
                this.OmronPLC.NLPLC_Initial(ip, portId);
            }
            return 0;
        }

        public override int HalClose()
        {
            if (this.OmronPLC == null) return 0;

            using (var plc = this.OmronPLC)
            {
            }


            return 0;
        }

        public override bool HalIsConnected()
        {
            var plcOn = this.GetValue("plc_on");
            return plcOn != null;
        }

        #endregion
    }
}
