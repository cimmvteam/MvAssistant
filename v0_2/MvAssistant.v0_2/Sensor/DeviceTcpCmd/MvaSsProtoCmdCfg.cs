using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.Sensor.Proto
{
    public class MvaSsProtoCmdCfg
    {


        public String DeviceUid;
        public int IntervalTimeOfConnectCheck = 30 * 1000;
        public bool IsActivelyConnect;
        public bool IsActivelyTx = false;
        public String LocalUri;
        public String RemoteUri;
        public List<MvaSsProtoCmdSvidCfg> SvidConfigs = new List<MvaSsProtoCmdSvidCfg>();
        public int TimeoutResponse = 1000;

        /// <summary> 傳輸間隔(milli-second) </summary>
        public int TxIntervalMs = 0;
    }
}
