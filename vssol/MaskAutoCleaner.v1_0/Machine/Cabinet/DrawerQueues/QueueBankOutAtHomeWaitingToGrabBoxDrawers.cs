using MvAssistant.Mac.v1_0.JSon.RobotTransferFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.Cabinet.DrawerQueues
{
    /// <summary>
    /// 儲存 Tray 已經在 Home(已經有盒子), 待命讓 Boxrrobot 呼叫 到In 的Drawer 的Queue   
    /// </summary>
    public class QueueBankOutAtHomeWaitingToGrabBoxDrawers:Queue<BoxrobotTransferLocation>
    {
        public new BoxrobotTransferLocation Dequeue()
        {
            if (this.Count == 0)
            {
                return default(BoxrobotTransferLocation); 
            }
            else
            {
                var rtnV = base.Dequeue();
                return rtnV;
            }
        }

        public new BoxrobotTransferLocation Peek()
        {
            if (this.Count == 0)
            {
                return default(BoxrobotTransferLocation);
            }
            else
            {
                var rtnV = base.Peek();
                return rtnV;
            }
        }

        public new void Enqueue(BoxrobotTransferLocation item)
        {
            base.Enqueue(item);
        }

    }
}
