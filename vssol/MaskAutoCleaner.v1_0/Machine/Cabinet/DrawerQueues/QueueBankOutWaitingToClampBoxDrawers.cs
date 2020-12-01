using MvAssistant.Mac.v1_0.JSon.RobotTransferFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.Cabinet.DrawerQueues
{
    public class QueueBankOutWaitingToClampBoxDrawers:Queue<BoxrobotTransferLocation>
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
