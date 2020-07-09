using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.MaskTransfer
{
    public enum EnumMacCmMaskTransferCmd
    {
        None,


        HomeTo_BoxTrasnfer01,
        HomeTo_CleanCh01,
        HomeTo_InspectionCh01,
        HomeTo_InspectionCh01Glass,
        HomeTo_LoadPort01,
        HomeTo_LoadPort02,
        HomeTo_MaskTransfer01,
        HomeTo_OpenStage01,
        HomeTo_BarcodeReader01,

        ToHome_BoxTrasnfer01,
        ToHome_CleanCh01,
        ToHome_InspectionCh01,
        ToHome_InspectionCh01Glass,
        ToHome_LoadPort01,
        ToHome_LoadPort02,
        ToHome_MaskTransfer01,
        ToHome_OpenStage01,
        ToHome_BarcodeReader01,


        Clamp_BoxTrasnfer01,
        Clamp_CleanCh01,
        Clamp_InspectionCh01,
        Clamp_InspectionCh01Glass,
        Clamp_LoadPort01,
        Clamp_LoadPort02,
        Clamp_MaskTransfer01,
        Clamp_OpenStage01,


        Release_BoxTrasnfer01,
        Release_CleanCh01,
        Release_InspectionCh01,
        Release_InspectionCh01Glass,
        Release_LoadPort01,
        Release_LoadPort02,
        Release_MaskTransfer01,
        Release_OpenStage01,

        ClampCalibrate_BoxTrasnfer01,
        ClampCalibrate_CleanCh01,
        ClampCalibrate_InspectionCh01,
        ClampCalibrate_InspectionCh01Glass,
        ClampCalibrate_LoadPort01,
        ClampCalibrate_LoadPort02,
        ClampCalibrate_MaskTransfer01,
        ClampCalibrate_OpenStage01,

        ReleaseCalibrate_BoxTrasnfer01,
        ReleaseCalibrate_CleanCh01,
        ReleaseCalibrate_InspectionCh01,
        ReleaseCalibrate_InspectionCh01Glass,
        ReleaseCalibrate_LoadPort01,
        ReleaseCalibrate_LoadPort02,
        ReleaseCalibrate_MaskTransfer01,
        ReleaseCalibrate_OpenStage01,




    }
}

