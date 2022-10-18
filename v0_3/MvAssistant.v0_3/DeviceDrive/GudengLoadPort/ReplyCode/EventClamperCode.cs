using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.DeviceDrive.GudengLoadPort.ReplyCode
{
   public  enum EventClamperCode
    {
        /// <summary>Clamper 關閉</summary>
        Unlock =0,
        /// <summary>Clamper 打開</summary>
        Lock =1,
        /// <summary>Clamper 不在定位需復歸</summary>
        NotOnFixedPosition=2
    }
}
