using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.MqttLike
{
    /// <summary>
    /// 
    /// </summary>
    public enum EnumMqttTopicId
    {
        None,


        //Mask Transfer
        MT_Tactile1_Raw,
        MT_Tactile2_Raw,
        MT_Tactile3_Raw,
        MT_Tactile4_Raw,
        MT_Force6Axis_Raw,
        MT_Tactile1_Avg,

        //Box Transfer
        BT_Force6Axis_Raw,
        BT_ClawPosition_Raw,


        //SecsMgr
        SecsMgr_SecsReceive,


        //Mask


        Eqp_MsgJobNotify,


        Eqp_PcManagedMemeory,

    }
}
