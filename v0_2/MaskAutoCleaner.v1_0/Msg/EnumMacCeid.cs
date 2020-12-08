using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Msg
{
    public enum EnumMacCeid
    {

        //TAP
        S3F17_CreateJob,
        S3F17_DockStart,
        S3F17_UnDockRequest,

        //BT
        S6F11_BT_BarcodeRead,
        S6F11_BT_RobotMoveToHome,
        S6F11_BT_RobotMoveToDrawer,
        S6F11_BT_RobotGetBoxAtDrawer,
        S6F11_BT_RobotPutBoxAtDrawer,
        S6F11_BT_RobotMoveToOpenStage,
        S6F11_BT_RobotGetBoxAtOpenStage,
        S6F11_BT_RobotPutBoxAtOpenStage,

        
        
        //MT
        S6F11_MT_RobotMoveToLoadPort,
        S6F11_MT_RobotMoveToCleanCh,
        S6F11_MT_RobotMoveToBarcodeReader,
        S6F11_MT_RobotMoveToHome,
        S6F11_MT_RobotMoveToInspectionCh,
        S6F11_MT_RobotMoveToInspectionChGlass,


        
        S6F11_ReadyToLoad,
        S6F11_ReadToUnload,
        S6F11_ReleaseComplete,
        S6F11_ReportPODInfo,

        S6F11_ReportSlotMap,
        S6F11_LoadComplete,
        S6F11_DockComplete,








    }
}
