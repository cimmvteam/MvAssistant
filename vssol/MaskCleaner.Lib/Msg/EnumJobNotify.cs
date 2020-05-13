using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Msg
{
    public enum EnumJobNotify
    {

        //Mask Transfer
        MT_CompleteRelease,
        MT_CompleteClamp,
        MT_ReadyToClean,
        
        MT_CleanMove,
        MT_CleanCompleteMove,
        MT_CleanEnd,


        //Drawer
        DR_RegisterAvailable,
        DR_RegisterFinish,
        DR_WaitBoxProcess,
        DR_BoxProcessRequest,

        //Box Transfer
        BT_CompleteClamp,
        BT_FinalBoxProcessEnd,
        BT_CompleteRelease,


        //Recipe Agent
        Rcp_Start,


        //Mask
        Mask_ProcessInitial,
        Mask_ProcessStart,




    }
}
