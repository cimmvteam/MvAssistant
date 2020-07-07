using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.CompLoadPort
{

    [Guid("92529F08-AC00-47CB-9587-6DE72BB2304A")]
    public class MacHalLoadPortCellGudeng : MacHalComponentBase, IMacHalLoadPortCell
    {

        #region Const
        public const string DevConnStr_Ip = "ip";
        public const string DevConnStr_Port = "port";

        public const string DevConnStr_LocalIp = "local_ip";
        public const string DevConnStr_LocalPort = "local_port";
        #endregion



        #region HAl

        public override int HalConnect()
        {
            throw new NotImplementedException();
        }

        public override int HalClose()
        {
            //throw new NotImplementedException();
            return 0;
        }

        #endregion


    }
}
