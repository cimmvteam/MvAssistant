using MaskAutoCleaner.v1_0.StateMachineBeta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.CleanCh
{
    public class MacMsCleanCh : MacMachineStateBase
    {
        public override void LoadStateMachine()
        {
            #region State
            MacState sStart = NewState(EnumMacMsCleanChState.Start);
            MacState sInitial = NewState(EnumMacMsCleanChState.Initial);

            MacState sIdle = NewState(EnumMacMsCleanChState.Idle);
            MacState sCleaningMask = NewState(EnumMacMsCleanChState.CleaningMask);
            MacState sInspectingMask = NewState(EnumMacMsCleanChState.InspectingMask);
            MacState sCleaningGlass = NewState(EnumMacMsCleanChState.CleaningGlass);
            MacState sInspectingGlass = NewState(EnumMacMsCleanChState.InspectingGlass);
            #endregion State

            #region Transition
            MacTransition tStart_Initial = NewTransition(sStart, sInitial, EnumMacMsCleanChTransition.PowerON);
            MacTransition tInitial_Idle = NewTransition(sStart, sIdle, EnumMacMsCleanChTransition.Initial);

            MacTransition tIdle_CleaningMask = NewTransition(sIdle, sCleaningMask, EnumMacMsCleanChTransition.CleanMask);
            MacTransition tCleaningMask_Idle = NewTransition(sCleaningMask, sIdle, EnumMacMsCleanChTransition.ReturnToIdleWithMaskCleaned);
            MacTransition tIdle_InspectingMask = NewTransition(sIdle, sInspectingMask, EnumMacMsCleanChTransition.InspectMask);
            MacTransition tInspectingMask_Idle = NewTransition(sInspectingMask, sIdle, EnumMacMsCleanChTransition.ReturnToIdleWithMaskInspected);
            MacTransition tIdle_CleaningGlass = NewTransition(sIdle, sCleaningGlass, EnumMacMsCleanChTransition.CleanGlass);
            MacTransition tCleaningGlass_Idle = NewTransition(sCleaningGlass, sIdle, EnumMacMsCleanChTransition.ReturnToIdleWithGlassCleaned);
            MacTransition tIdle_InspectingGlass = NewTransition(sIdle, sInspectingGlass, EnumMacMsCleanChTransition.InspectGlass);
            MacTransition tInspectingGlass_Idle = NewTransition(sInspectingGlass, sIdle, EnumMacMsCleanChTransition.ReturnToIdleWithGlassInspected);
            #endregion Transition

            #region State Register OnEntry OnExit
            sStart.OnEntry += (sender, e) =>
            { };
            sStart.OnExit += (sender, e) =>
            { };
            sInitial.OnEntry += (sender, e) =>
            { };
            sInitial.OnExit += (sender, e) =>
            { };

            sIdle.OnEntry += (sender, e) =>
            { };
            sIdle.OnExit += (sender, e) =>
            { };
            sCleaningMask.OnEntry += (sender, e) =>
            { };
            sCleaningMask.OnExit += (sender, e) =>
            { };
            sInspectingMask.OnEntry += (sender, e) =>
            { };
            sInspectingMask.OnExit += (sender, e) =>
            { };
            sCleaningGlass.OnEntry += (sender, e) =>
            { };
            sCleaningGlass.OnExit += (sender, e) =>
            { };
            sInspectingGlass.OnEntry += (sender, e) =>
            { };
            sInspectingGlass.OnExit += (sender, e) =>
            { };
            #endregion State Register OnEntry OnExit
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
