using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.DeviceDrive.GudengLoadPort.ReplyCode
{
   public enum EventStagePositionCode
    {
        /// <summary>Stage 位於上升位置</summary>
        OnAscentPosition =0,
        /// <summary>Stage 於光罩檢查位置</summary>
        OnMaskCheckingPosition = 1,
        /// <summary>Stage 於下降位置</summary>
        OnDescentPosition = 2,
        /// <summary>Stage不在定位需進行初始化</summary>
        NotOnFixedPlsition = 3

    }
}
