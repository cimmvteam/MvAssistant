using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.CompPlc
{

    [Guid("22421239-9CEA-4050-AE0C-FF997A872FED")]
    public abstract class MacHalPlcBase : MacHalComponentBase
    {

        public const string DevConnStr_Ip = "ip";
        public const string DevConnStr_PortId = "portid";


        protected MacHalPlcContext m_PlcContext;

        public int GetPlcContext()
        {
            var ip = this.GetDevSetting(DevConnStr_Ip);
            var port = this.GetDevSettingInt(DevConnStr_PortId);
            this.m_PlcContext = MacHalPlcContext.Get(ip, port);
            return 0;
        }


        #region Hal

        public override int HalConnect()
        {
            return 0;
        }

        public override int HalClose()
        {
            using (var obj = this.m_PlcContext)
            {
                if (m_PlcContext != null)
                {
                    this.m_PlcContext.Close();
                    this.m_PlcContext = null;
                }
            }
            return 0;
        }

        public override bool HalIsConnected()
        {
            if (this.m_PlcContext == null) return false;
            return this.m_PlcContext.ReadPowerON();
        }



        #endregion

    }




}
