using MaskAutoCleaner.v1_0.StateMachineBeta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.InspectionCh
{
    public class MacMsInspectionCh : MacMachineStateBase
    {
        public EnumMacMsInspectionChState CurrentWorkState { get; set; }
        public override void LoadStateMachine()
        {
            #region State
            MacState sStart = NewState(EnumMacMsInspectionChState.Start);
            MacState sInitial = NewState(EnumMacMsInspectionChState.Initial);

            MacState sIdle = NewState(EnumMacMsInspectionChState.Idle);
            MacState sWaitingForPutIntoMask = NewState(EnumMacMsInspectionChState.WaitingForInputMask);
            MacState sMaskOnStage = NewState(EnumMacMsInspectionChState.MaskOnStage);
            MacState sDefensingMask = NewState(EnumMacMsInspectionChState.DefensingMask);
            MacState sInspectingMask = NewState(EnumMacMsInspectionChState.InspectingMask);
            MacState sMaskOnStageInspected = NewState(EnumMacMsInspectionChState.MaskOnStageInspected);
            MacState sWaitingForReleaseMask = NewState(EnumMacMsInspectionChState.WaitingForReleaseMask);

            MacState sWaitingForPutIntoGlass = NewState(EnumMacMsInspectionChState.WaitingForInputGlass);
            MacState sGlassOnStage = NewState(EnumMacMsInspectionChState.GlassOnStage);
            MacState sDefensingGlass = NewState(EnumMacMsInspectionChState.DefensingGlass);
            MacState sInspectingGlass = NewState(EnumMacMsInspectionChState.InspectingGlass);
            MacState sGlassOnStageInspected = NewState(EnumMacMsInspectionChState.GlassOnStageInspected);
            MacState sWaitingForReleaseGlass = NewState(EnumMacMsInspectionChState.WaitingForReleaseGlass);
            #endregion State

            #region Transition
            MacTransition tStart_Initial = NewTransition(sStart, sInitial, EnumMacMsInspectionChTransition.PowerON);
            MacTransition tInitial_Idle = NewTransition(sStart, sIdle, EnumMacMsInspectionChTransition.Initial);

            MacTransition tIdle_WaitingForPutIntoMask = NewTransition(sIdle, sWaitingForPutIntoMask, EnumMacMsInspectionChTransition.WaitForInputMask);
            MacTransition tWaitingForPutIntoMask_MaskOnStage = NewTransition(sWaitingForPutIntoMask, sMaskOnStage, EnumMacMsInspectionChTransition.StandbyAtStageWithMask);
            MacTransition tMaskOnStage_MaskDefensing = NewTransition(sMaskOnStage, sDefensingMask, EnumMacMsInspectionChTransition.DefenseMask);
            MacTransition tMaskDefensing_MaskInspecting = NewTransition(sDefensingMask, sInspectingMask, EnumMacMsInspectionChTransition.InspectMask);
            MacTransition tMaskInspecting_MaskOnStageInspected = NewTransition(sInspectingMask, sMaskOnStageInspected, EnumMacMsInspectionChTransition.StandbyAtStageWithMaskInspected);
            MacTransition tMaskOnStageInspected_WaitingForReleaseMask = NewTransition(sMaskOnStageInspected, sWaitingForReleaseMask, EnumMacMsInspectionChTransition.WaitForReleaseMask);
            MacTransition tWaitingForReleaseMask_Idle = NewTransition(sWaitingForReleaseMask, sIdle, EnumMacMsInspectionChTransition.ReturnToIdleFromReleaseMask);

            MacTransition tIdle_WaitingForPutIntoGlass = NewTransition(sIdle, sWaitingForPutIntoGlass, EnumMacMsInspectionChTransition.WaitForInputGlass);
            MacTransition tWaitingForPutIntoGlass_GlassOnStage = NewTransition(sWaitingForPutIntoGlass, sGlassOnStage, EnumMacMsInspectionChTransition.StandbyAtStageWithGlass);
            MacTransition tGlassOnStage_GlassDefensing = NewTransition(sGlassOnStage, sDefensingGlass, EnumMacMsInspectionChTransition.DefenseGlass);
            MacTransition tGlassDefensing_GlassInspecting = NewTransition(sDefensingGlass, sInspectingGlass, EnumMacMsInspectionChTransition.InspectGlass);
            MacTransition tGlassInspecting_GlassOnStageInspected = NewTransition(sInspectingGlass, sGlassOnStageInspected, EnumMacMsInspectionChTransition.StandbyAtStageWithGlassInspected);
            MacTransition tGlassOnStageInspected_WaitingForReleaseGlass = NewTransition(sGlassOnStageInspected, sWaitingForReleaseGlass, EnumMacMsInspectionChTransition.WaitForReleaseGlass);
            MacTransition tWaitingForReleaseGlass_Idle = NewTransition(sWaitingForReleaseGlass, sIdle, EnumMacMsInspectionChTransition.ReturnToIdleFromReleaseGlass);
            #endregion Transition
        }

        public class TimeOutController
        {
            public bool IsTimeOut(DateTime startTime, int targetDiffSecs)
            {
                var thisTime = DateTime.Now;
                var diff = thisTime.Subtract(startTime).TotalSeconds;
                if (diff >= targetDiffSecs)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public bool IsTimeOut(DateTime startTime)
            {
                return IsTimeOut(startTime, 20);
            }
        }
    }
}
