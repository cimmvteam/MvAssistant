using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvAssistant.MaskTool_v0_1.Plc
{
    public enum MvEnumPlcVariable
    {
        PC_TO_PLC_CheckClock,//PLC軟體狀態檢查
        PC_TO_PLC_CheckClock_Reply,

        ic_stage_pos_x,
        ic_stage_pos_y,
        ic_stage_pos_z,
        ic_stage_trigger,


        plc_on,
    }
}
