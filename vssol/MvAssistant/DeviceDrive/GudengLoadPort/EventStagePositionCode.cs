using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.GudengLoadPort
{
   public enum EventStagePositionCode
    {
        OnAscentPosition=0,
        OnMaskCheckingPosition=1,
        OnDescentPosition = 2,
        /// <summary>Stage不在定位需進行初始化</summary>
        NotOnFixedPlsition = 3

    }
}
