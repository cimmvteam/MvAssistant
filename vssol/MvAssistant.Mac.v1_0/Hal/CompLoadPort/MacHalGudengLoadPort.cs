using MvAssistant.DeviceDrive;
using MvAssistant.DeviceDrive.GudengLoadPort;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.CompLoadPort
{
    [Guid("F02B078D-30B7-44CF-9D9C-DAC2FE9A26C6")]
    public class MacHalGudengLoadPort : MacHalComponentBase, IMacHalLoadPortComp
    {
        private static object getLddObject = new object();
        private  MvGudengLoadPortLdd _ldd;
       

        public string DeviceIP
        {
            get
            {
                var ip = this.DevSettings["ip"];
                return ip;
            }
        }

        public int DevicePort  
        {
            get
            {
                var port =Convert.ToInt32( this.DevSettings["port"]);
                return port;
            }
        }
        public string Index { get { return HalDeviceCfg.DeviceName; } }

        public bool IsConnected {
            get
            {
                if (_ldd == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

        }

        public override int HalClose()
        {
            try
            {

                _ldd = null;
                return 1;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        public override int HalConnect()
        {
            try
            {
                var connected = false;
                if (_ldd == null)
                {
                    lock (getLddObject)
                    {
                        if (_ldd == null)
                        {
                            _ldd = new MvGudengLoadPortLdd(DeviceIP, DevicePort, Index);
                             connected=_ldd.StartListenServerThread();
                            if (!connected )
                            {
                                _ldd = null;
                            }
                        }
                    }
                }
                return connected? 1:0;
            }
            catch(Exception ex)
            {
                _ldd = null;
                return 0;
            }
        }


       public string CommandAlarmReset()
        {
            var commandText=_ldd.CommandAlarmReset();
            return commandText;
        }

    }
}
