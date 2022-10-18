//using Microsoft.Analytics.Interfaces;
//using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MvAssistant.v0_3.DeviceDrive.GudengLoadPort
{
    public enum LoadPortRequestContent
    {
        DockRequest=100,
        UndockRequest=101,
        AskPlacementStatus=102,
        AskPresentStatus=103,
        AskClamperStatus=104,
        AskRFIDStatus=105,
        AskBarcodeStatus=106,
        AskVacuumStatus=107,
        AskReticleExistStatus=108,
        AlarmReset=109,
        AskStagePosition=110,
        AskLoadportStatus=111,
        InitialRequest=112,
        ManualClamperLock=113,
        ManualClamperUnlock=114,
        ManualClamperOPR=115,
        ManualStageUp=116,
        ManualStageInspection=117,
        ManualStageDown=118,
        ManualStageOPR=119,
        ManualVacuumOn=120,
        ManualVacuumOff
    }

    public static class LoadPortRequestCommandExtends
    {
        public static string GetStringCode(this LoadPortRequestContent inst)
        {
            var rtnV = ((int)inst).ToString("000");
            return rtnV;
        }
    }

}