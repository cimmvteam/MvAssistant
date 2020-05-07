using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.CompPlc
{

    [Guid("22421239-9CEA-4050-AE0C-FF997A872FED")]
    public abstract class MacHalPlcBase : MacHalComponentBase, IMacHalPlcUniversal
    {

        protected MacHalPlcContext m_PlcContext;

        #region Hal

        public override int HalConnect()
        {
            var ip = this.GetDevSetting("ip");
            var port = this.GetDevSettingInt("portid");
            this.m_PlcContext = MacHalPlcContext.Get(ip, port);
            return 0;
        }


        public override int HalClose()
        {
            using (var obj = this.m_PlcContext)
            {
                this.m_PlcContext.Close();
                this.m_PlcContext = null;
            }
            return 0;
        }

        public override bool HalIsConnected()
        {
            throw new NotImplementedException();
        }
    }

    #endregion


}
