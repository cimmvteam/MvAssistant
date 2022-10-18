using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.DeviceDrive.GudengLoadPort
{
   public enum LoadPortResponseContent
    {
        Placement=1,
        Present=2,
        Clamper=3,
        RFID=4,
        BarcodeID=5,
        ClamperUnlockComplete=6,
        VacuumComplete=7,
        DockPODStart=8,
        DockPODComplete_HasReticle=9,
        DockPODComplete_Empty=10,
        UndockComplete=11,
        ClamperLockComplete=12,
        AlarmResetSuccess=13,
        AlarmResetFail=14,
        ExecuteInitialFirst=15,
        ExecuteAlarmResetFirst=16,
        StagePosition=17,
        LoadportStatus=18,
        InitialComplete=19,
        MustInAutoMode=20,
        PODNotPutProperly=23,
        ClamperActionTimeOut =200,
        ClamperUnlockPositionFailed=201,
        VacuumAbnormality=202,
        StageMotionTimeout=203,
        StageOverUpLimitation=204,
        StageOverDownLimitation=205,
        ReticlePositionAbnormality=206,
        ClamperLockPositionFailed=207,
        CoverDisappear=208,
        ClamperMotorAbnormality=209,
        StageMotorAbnormality=210,
    }
}
