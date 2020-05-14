using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Msg.PrescribedJobNotify
{
    public enum BoxProcessStatus
    {
        Nothing = 0,
        Not_Finish = 1,
        Finish = 2,
        Cancel = 3,
    }

    public enum EnumBoxPositon
    {
        Openstage,
        BoxGripper,
        DrawerSlot,
    }

    public enum EnumBoxLocker
    {
        Lock,
        Unlock,
    }

    public enum EnumBoxType
    {
        Crystal,
        Metal,
    }

    public enum EnumBoxStatus
    {
        Initial,
        MaskInbox,
        Empty,
    }
}
