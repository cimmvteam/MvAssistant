﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.BoxTransfer
{
    public enum EnumMacBoxTransferState
    {
        Start,
        DeviceInitial,

        CB1Home,
        CB2Home,
        CB1HomeClamped,
        CB2HomeClamped,

        ChangingDirectionToCB1Home,
        ChangingDirectionToCB2Home,
        ChangingDirectionToCB1HomeClamped,
        ChangingDirectionToCB2HomeClamped,

        MovingToOpenStage,
        OpenStageClamping,
        MovingToCB1HomeClampedFromOpenStage,
        MovingToOpenStageForRelease,
        OpenStageReleasing,
        MovingToCB1HomeFromOpenStage,

        Locking,
        Unlocking,

        #region Move To Cabinet
        MovingToCabinet0101,
        MovingToCabinet0102,
        MovingToCabinet0103,
        MovingToCabinet0104,
        MovingToCabinet0105,
        MovingToCabinet0201,
        MovingToCabinet0202,
        MovingToCabinet0203,
        MovingToCabinet0204,
        MovingToCabinet0205,
        MovingToCabinet0301,
        MovingToCabinet0302,
        MovingToCabinet0303,
        MovingToCabinet0304,
        MovingToCabinet0305,
        MovingToCabinet0401,
        MovingToCabinet0402,
        MovingToCabinet0403,
        MovingToCabinet0404,
        MovingToCabinet0405,
        MovingToCabinet0501,
        MovingToCabinet0502,
        MovingToCabinet0503,
        MovingToCabinet0504,
        MovingToCabinet0505,
        MovingToCabinet0601,
        MovingToCabinet0602,
        MovingToCabinet0603,
        MovingToCabinet0604,
        MovingToCabinet0605,
        MovingToCabinet0701,
        MovingToCabinet0702,
        MovingToCabinet0703,
        MovingToCabinet0704,
        MovingToCabinet0705,
        #endregion Move To Cabinet

        #region Clamping At Cabinet
        Cabinet0101Clamping,
        Cabinet0102Clamping,
        Cabinet0103Clamping,
        Cabinet0104Clamping,
        Cabinet0105Clamping,
        Cabinet0201Clamping,
        Cabinet0202Clamping,
        Cabinet0203Clamping,
        Cabinet0204Clamping,
        Cabinet0205Clamping,
        Cabinet0301Clamping,
        Cabinet0302Clamping,
        Cabinet0303Clamping,
        Cabinet0304Clamping,
        Cabinet0305Clamping,
        Cabinet0401Clamping,
        Cabinet0402Clamping,
        Cabinet0403Clamping,
        Cabinet0404Clamping,
        Cabinet0405Clamping,
        Cabinet0501Clamping,
        Cabinet0502Clamping,
        Cabinet0503Clamping,
        Cabinet0504Clamping,
        Cabinet0505Clamping,
        Cabinet0601Clamping,
        Cabinet0602Clamping,
        Cabinet0603Clamping,
        Cabinet0604Clamping,
        Cabinet0605Clamping,
        Cabinet0701Clamping,
        Cabinet0702Clamping,
        Cabinet0703Clamping,
        Cabinet0704Clamping,
        Cabinet0705Clamping,
        #endregion Clamping At Cabinet

        #region Return To CB Home Clamped From Cabinet
        MovingToCB1HomeClampedFromCabinet0101,
        MovingToCB1HomeClampedFromCabinet0102,
        MovingToCB1HomeClampedFromCabinet0103,
        MovingToCB1HomeClampedFromCabinet0104,
        MovingToCB1HomeClampedFromCabinet0105,
        MovingToCB1HomeClampedFromCabinet0201,
        MovingToCB1HomeClampedFromCabinet0202,
        MovingToCB1HomeClampedFromCabinet0203,
        MovingToCB1HomeClampedFromCabinet0204,
        MovingToCB1HomeClampedFromCabinet0205,
        MovingToCB1HomeClampedFromCabinet0301,
        MovingToCB1HomeClampedFromCabinet0302,
        MovingToCB1HomeClampedFromCabinet0303,
        MovingToCB1HomeClampedFromCabinet0304,
        MovingToCB1HomeClampedFromCabinet0305,
        MovingToCB1HomeClampedFromCabinet0401,
        MovingToCB1HomeClampedFromCabinet0402,
        MovingToCB1HomeClampedFromCabinet0403,
        MovingToCB1HomeClampedFromCabinet0404,
        MovingToCB1HomeClampedFromCabinet0405,
        MovingToCB1HomeClampedFromCabinet0501,
        MovingToCB1HomeClampedFromCabinet0502,
        MovingToCB1HomeClampedFromCabinet0503,
        MovingToCB1HomeClampedFromCabinet0504,
        MovingToCB1HomeClampedFromCabinet0505,
        MovingToCB1HomeClampedFromCabinet0601,
        MovingToCB1HomeClampedFromCabinet0602,
        MovingToCB1HomeClampedFromCabinet0603,
        MovingToCB1HomeClampedFromCabinet0604,
        MovingToCB1HomeClampedFromCabinet0605,
        MovingToCB1HomeClampedFromCabinet0701,
        MovingToCB1HomeClampedFromCabinet0702,
        MovingToCB1HomeClampedFromCabinet0703,
        MovingToCB1HomeClampedFromCabinet0704,
        MovingToCB1HomeClampedFromCabinet0705,
        #endregion Return To CB Home Clamped From Cabinet

        #region Move To Cabinet For Release
        MovingToCabinet0101ForRelease,
        MovingToCabinet0102ForRelease,
        MovingToCabinet0103ForRelease,
        MovingToCabinet0104ForRelease,
        MovingToCabinet0105ForRelease,
        MovingToCabinet0201ForRelease,
        MovingToCabinet0202ForRelease,
        MovingToCabinet0203ForRelease,
        MovingToCabinet0204ForRelease,
        MovingToCabinet0205ForRelease,
        MovingToCabinet0301ForRelease,
        MovingToCabinet0302ForRelease,
        MovingToCabinet0303ForRelease,
        MovingToCabinet0304ForRelease,
        MovingToCabinet0305ForRelease,
        MovingToCabinet0401ForRelease,
        MovingToCabinet0402ForRelease,
        MovingToCabinet0403ForRelease,
        MovingToCabinet0404ForRelease,
        MovingToCabinet0405ForRelease,
        MovingToCabinet0501ForRelease,
        MovingToCabinet0502ForRelease,
        MovingToCabinet0503ForRelease,
        MovingToCabinet0504ForRelease,
        MovingToCabinet0505ForRelease,
        MovingToCabinet0601ForRelease,
        MovingToCabinet0602ForRelease,
        MovingToCabinet0603ForRelease,
        MovingToCabinet0604ForRelease,
        MovingToCabinet0605ForRelease,
        MovingToCabinet0701ForRelease,
        MovingToCabinet0702ForRelease,
        MovingToCabinet0703ForRelease,
        MovingToCabinet0704ForRelease,
        MovingToCabinet0705ForRelease,
        #endregion Move To Cabinet For Release

        #region Releasing At Cabinet
        Cabinet0101Releasing,
        Cabinet0102Releasing,
        Cabinet0103Releasing,
        Cabinet0104Releasing,
        Cabinet0105Releasing,
        Cabinet0201Releasing,
        Cabinet0202Releasing,
        Cabinet0203Releasing,
        Cabinet0204Releasing,
        Cabinet0205Releasing,
        Cabinet0301Releasing,
        Cabinet0302Releasing,
        Cabinet0303Releasing,
        Cabinet0304Releasing,
        Cabinet0305Releasing,
        Cabinet0401Releasing,
        Cabinet0402Releasing,
        Cabinet0403Releasing,
        Cabinet0404Releasing,
        Cabinet0405Releasing,
        Cabinet0501Releasing,
        Cabinet0502Releasing,
        Cabinet0503Releasing,
        Cabinet0504Releasing,
        Cabinet0505Releasing,
        Cabinet0601Releasing,
        Cabinet0602Releasing,
        Cabinet0603Releasing,
        Cabinet0604Releasing,
        Cabinet0605Releasing,
        Cabinet0701Releasing,
        Cabinet0702Releasing,
        Cabinet0703Releasing,
        Cabinet0704Releasing,
        Cabinet0705Releasing,
        #endregion Releasing At Cabinet

        #region Return To CB Home From Cabinet
        MovingToCB1HomeFromCabinet0101,
        MovingToCB1HomeFromCabinet0102,
        MovingToCB1HomeFromCabinet0103,
        MovingToCB1HomeFromCabinet0104,
        MovingToCB1HomeFromCabinet0105,
        MovingToCB1HomeFromCabinet0201,
        MovingToCB1HomeFromCabinet0202,
        MovingToCB1HomeFromCabinet0203,
        MovingToCB1HomeFromCabinet0204,
        MovingToCB1HomeFromCabinet0205,
        MovingToCB1HomeFromCabinet0301,
        MovingToCB1HomeFromCabinet0302,
        MovingToCB1HomeFromCabinet0303,
        MovingToCB1HomeFromCabinet0304,
        MovingToCB1HomeFromCabinet0305,
        MovingToCB1HomeFromCabinet0401,
        MovingToCB1HomeFromCabinet0402,
        MovingToCB1HomeFromCabinet0403,
        MovingToCB1HomeFromCabinet0404,
        MovingToCB1HomeFromCabinet0405,
        MovingToCB1HomeFromCabinet0501,
        MovingToCB1HomeFromCabinet0502,
        MovingToCB1HomeFromCabinet0503,
        MovingToCB1HomeFromCabinet0504,
        MovingToCB1HomeFromCabinet0505,
        MovingToCB1HomeFromCabinet0601,
        MovingToCB1HomeFromCabinet0602,
        MovingToCB1HomeFromCabinet0603,
        MovingToCB1HomeFromCabinet0604,
        MovingToCB1HomeFromCabinet0605,
        MovingToCB1HomeFromCabinet0701,
        MovingToCB1HomeFromCabinet0702,
        MovingToCB1HomeFromCabinet0703,
        MovingToCB1HomeFromCabinet0704,
        MovingToCB1HomeFromCabinet0705,
        MovingToDrawer,
        DrawerClamping,
        MovingToCB1HomeClampedFromDrawer,
        MovingToDrawerForRelease,
        DrawerReleasing,
        MovingToCB1HomeFromDrawer,
        CB1HomeClamped_C,
        CB1Home_C,
        #endregion Return To CB Home From Cabinet
    }
}
