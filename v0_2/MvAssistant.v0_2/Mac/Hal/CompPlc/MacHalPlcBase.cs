using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.Mac.Hal.CompPlc
{


    [Guid("22421239-9CEA-4050-AE0C-FF997A872FED")]
    public abstract class MacHalPlcBase : MacHalComponentBase, IMacHalPlcBase
    {
        #region Const
        public const string DevConnStr_Ip = "ip";
        public const string DevConnStr_PortId = "portid";
        #endregion

        #region Device Connection Str
        string ip;
        int portid;
        string resourceKey { get { return string.Format("net.tcp://{0}:{1}", this.ip, this.portid); } }
        #endregion

        protected MacHalPlcContext plcContext;

        public Dictionary<EnumMacHalPlcVariable, Object> ReadMulti(IEnumerable<EnumMacHalPlcVariable> varNames) { return this.plcContext.ReadMulti(varNames); }


        #region Hal

        public override int HalClose()
        {
            //可能有其它人在使用 Resource, 不在個別 HAL 裡釋放, 由 HalContext 統一釋放
            using (var obj = this.plcContext)
            {
                if (plcContext != null)
                {
                    this.plcContext.Close();
                    this.plcContext = null;
                }
            }
            return 0;
        }

        public override int HalConnect()
        {
            this.ip = this.GetDevConnSetting(DevConnStr_Ip);
            this.portid = this.GetDevConnSettingInt(DevConnStr_PortId);
            this.plcContext = this.HalContext.ResourceGetOrRegister(this.resourceKey, () => new MacHalPlcContext()
            {
                PlcIp = this.ip,
                PlcPortId = this.portid,
            });

            return 0;
        }
        public override bool HalIsConnected()
        {
            if (this.plcContext == null) return false;
            return this.plcContext.ReadPowerON();
        }



        #endregion

    }




}
