using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.DeviceDrive.GudengLoadPort.ReplyCode
{
   public enum EventLoadportStatusCode
    {
        /// <summary>靜止中且可載入光罩</summary>
        DockReady = 0,
        /// <summary>靜止中且可載出光罩</summary>
        UndockReady = 1,
        /// <summary>動作中不可接收指令</summary>
        Busy = 2,
        /// <summary>異常中需進行異常復歸動作</summary>
        Alarm = 3,
        /// <summary>需要進行初始化動作</summary>
        WaitInitial = 4
    }

}
