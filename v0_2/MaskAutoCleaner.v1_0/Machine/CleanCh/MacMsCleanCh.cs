using MaskAutoCleaner.v1_0.StateMachineBeta;
using MaskAutoCleaner.v1_0.StateMachineExceptions.CleanChStateMachineException;
using MaskAutoCleaner.v1_0.StateMachineExceptions.UniversalStateMachineException;
using MvAssistant.v0_2.Mac.Hal.Assembly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.CleanCh
{
    public class MacMsCleanCh : MacMachineStateBase
    {
        private IMacHalCleanCh HalCleanCh { get { return this.halAssembly as IMacHalCleanCh; } }

        public MacMsCleanCh() { LoadStateMachine(); }

        #region State Machine Command

        /// <summary> 狀態機啟動 </summary>
        public override void SystemBootup()
        {
            var transition = this.Transitions[EnumMacCleanChTransition.SystemBootup.ToString()];
            transition.StateFrom.ExecuteCommandAtEntry(new MacStateEntryEventArgs());
        }
        /// <summary> 清理Pellicle </summary>
        public void CleanPellicle()
        {
            var transition = Transitions[EnumMacCleanChTransition.TriggerToCleanPellicle.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> 停止/結束清理Pellicle </summary>
        public void FinishCleanPellicle()
        {
            var transition = Transitions[EnumMacCleanChTransition.TriggerToReturnToIdleAfterCleanPellicle.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> 檢測Pellicle </summary>
        public void InspectPellicle()
        {
            var transition = Transitions[EnumMacCleanChTransition.TriggerToInspectPellicle.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> 停止/結束檢測Pellicle </summary>
        public void FinishInspectPellicle()
        {
            var transition = Transitions[EnumMacCleanChTransition.TriggerToReturnToIdleAfterInspectPellicle.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }

        /// <summary> 清理Glass </summary>
        public void CleanGlass()
        {
            var transition = Transitions[EnumMacCleanChTransition.TriggerToCleanGlass.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> 停止/結束清理Glass </summary>
        public void FinishCleanGlass()
        {
            var transition = Transitions[EnumMacCleanChTransition.TriggerToReturnToIdleAfterCleanGlass.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> 檢測Glass </summary>
        public void InspectGlass()
        {
            var transition = Transitions[EnumMacCleanChTransition.TriggerToInspectGlass.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> 停止/結束檢測Glass </summary>
        public void FinishInspectGlass()
        {
            var transition = Transitions[EnumMacCleanChTransition.TriggerToReturnToIdleAfterInspectGlass.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }

        #endregion

        public override void LoadStateMachine()
        {
            #region State
            MacState sStart = NewState(EnumMacCleanChState.Start);
            //MacState sInitial = NewState(EnumMacMsCleanChState.Initial);

            MacState sIdle = NewState(EnumMacCleanChState.Idle);
            MacState sCleaningPellicle = NewState(EnumMacCleanChState.CleaningPellicle);
            MacState sCleanedPellicle = NewState(EnumMacCleanChState.CleanedPellicle);
            MacState sInspectingPellicle = NewState(EnumMacCleanChState.InspectingPellicle);
            MacState sInspectedPellicle = NewState(EnumMacCleanChState.InspectedPellicle);

            MacState sCleaningGlass = NewState(EnumMacCleanChState.CleaningGlass);
            MacState sCleanedGlass = NewState(EnumMacCleanChState.CleanedGlass);
            MacState sInspectingGlass = NewState(EnumMacCleanChState.InspectingGlass);
            MacState sInspectedGlass = NewState(EnumMacCleanChState.InspectedGlass);
            #endregion State

            #region Transition
            MacTransition tStart_Idle = NewTransition(sStart, sIdle, EnumMacCleanChTransition.SystemBootup);
            MacTransition tIdle_NULL = NewTransition(sIdle, null, EnumMacCleanChTransition.StandbyAtIdle);

            MacTransition tIdle_CleaningPellicle = NewTransition(sIdle, sCleaningPellicle, EnumMacCleanChTransition.TriggerToCleanPellicle);
            MacTransition tCleaningPellicle_CleanedPellicle = NewTransition(sCleaningPellicle, sCleanedPellicle, EnumMacCleanChTransition.CleaningPellicle);
            MacTransition tCleanedPellicle_NULL = NewTransition(sCleanedPellicle, null, EnumMacCleanChTransition.CleanedPellicle);
            MacTransition tCleanedPellicle_Idle = NewTransition(sCleanedPellicle, sIdle, EnumMacCleanChTransition.TriggerToReturnToIdleAfterCleanPellicle);
            MacTransition tIdle_InspectingPellicle = NewTransition(sIdle, sInspectingPellicle, EnumMacCleanChTransition.TriggerToInspectPellicle);
            MacTransition tInspectingPellicle_InspectedPellicle = NewTransition(sInspectingPellicle, sInspectedPellicle, EnumMacCleanChTransition.InspectingPellicle);
            MacTransition tInspectedPellicle_NULL = NewTransition(sInspectedPellicle, null, EnumMacCleanChTransition.InspectedPellicle);
            MacTransition tInspectedPellicle_Idle = NewTransition(sInspectedPellicle, sIdle, EnumMacCleanChTransition.TriggerToReturnToIdleAfterInspectPellicle);

            MacTransition tIdle_CleaningGlass = NewTransition(sIdle, sCleaningGlass, EnumMacCleanChTransition.TriggerToCleanGlass);
            MacTransition tCleaningGlass_CleanedGlass = NewTransition(sCleaningGlass, sCleanedGlass, EnumMacCleanChTransition.CleaningGlass);
            MacTransition tCleanedGlass_NULL = NewTransition(sCleanedGlass, null, EnumMacCleanChTransition.CleanedGlass);
            MacTransition tCleanedGlass_Idle = NewTransition(sCleanedGlass, sIdle, EnumMacCleanChTransition.TriggerToReturnToIdleAfterCleanGlass);
            MacTransition tIdle_InspectingGlass = NewTransition(sIdle, sInspectingGlass, EnumMacCleanChTransition.TriggerToInspectGlass);
            MacTransition tInspectingGlass_InspectedGlass = NewTransition(sInspectingGlass, sInspectedGlass, EnumMacCleanChTransition.InspectingGlass);
            MacTransition tInspectedGlass_NULL = NewTransition(sInspectedGlass, null, EnumMacCleanChTransition.InspectedGlass);
            MacTransition tInspectedGlass_Idle = NewTransition(sInspectedGlass, sIdle, EnumMacCleanChTransition.TriggerToReturnToIdleAfterInspectGlass);
            #endregion Transition

            #region State Register OnEntry OnExit
            sStart.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tStart_Idle;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () => { return true; },
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sStart.OnExit += (sender, e) => { };

            sIdle.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tIdle_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () => { return true; },
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sIdle.OnExit += (sender, e) => { };

            sCleaningPellicle.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tCleaningPellicle_CleanedPellicle;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            // TODO:清理的詳細動作
                        }
                        catch (Exception ex) { throw new CleanChCleanFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sCleaningPellicle.OnExit += (sender, e) => { };

            sCleanedPellicle.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tCleanedPellicle_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () => { return true; },
                    Action = (parameter) => { },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sCleanedPellicle.OnExit += (sender, e) => { };

            sInspectingPellicle.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tInspectingPellicle_InspectedPellicle;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            // TODO:檢查的詳細動作
                        }
                        catch (Exception ex) { throw new CleanChInspectFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sInspectingPellicle.OnExit += (sender, e) => { };

            sInspectedPellicle.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tInspectedPellicle_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () => { return true; },
                    Action = (parameter) => { },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sInspectedPellicle.OnExit += (sender, e) => { };


            sCleaningGlass.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tCleaningGlass_CleanedGlass;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            // TODO:清理的詳細動作
                        }
                        catch (Exception ex) { throw new CleanChCleanFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sCleaningGlass.OnExit += (sender, e) => { };

            sCleanedGlass.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tCleanedGlass_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () => { return true; },
                    Action = (parameter) => { },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sCleanedGlass.OnExit += (sender, e) => { };

            sInspectingGlass.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tInspectingGlass_InspectedGlass;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        CheckEquipmentStatus();
                        CheckAssemblyAlarmSignal();
                        CheckAssemblyWarningSignal();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            // TODO:檢查的詳細動作
                        }
                        catch (Exception ex) { throw new CleanChInspectFailException(ex.Message); }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sInspectingGlass.OnExit += (sender, e) => { };

            sInspectedGlass.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tInspectedGlass_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () => { return true; },
                    Action = (parameter) => { },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sInspectedGlass.OnExit += (sender, e) => { };
            #endregion State Register OnEntry OnExit
        }

        private bool CheckEquipmentStatus()
        {
            string Result = null;
            if (Mediater.ReadPowerON() == false) Result += "Equipment is power off now, ";
            if (Mediater.ReadBCP_Maintenance()) Result += "Key lock in the electric control box is turn to maintenance, ";
            if (Mediater.ReadCB_Maintenance()) Result += "Outside key lock between cabinet_1 and cabinet_2 is turn to maintenance, ";
            if (Mediater.ReadBCP_EMO().Item1) Result += "EMO_1 has been trigger, ";
            if (Mediater.ReadBCP_EMO().Item2) Result += "EMO_2 has been trigger, ";
            if (Mediater.ReadBCP_EMO().Item3) Result += "EMO_3 has been trigger, ";
            if (Mediater.ReadBCP_EMO().Item4) Result += "EMO_4 has been trigger, ";
            if (Mediater.ReadBCP_EMO().Item5) Result += "EMO_5 has been trigger, ";
            if (Mediater.ReadCB_EMO().Item1) Result += "EMO_6 has been trigger, ";
            if (Mediater.ReadCB_EMO().Item2) Result += "EMO_7 has been trigger, ";
            if (Mediater.ReadCB_EMO().Item3) Result += "EMO_8 has been trigger, ";
            if (Mediater.ReadLP1_EMO()) Result += "Load Port_1 EMO has been trigger, ";
            if (Mediater.ReadLP2_EMO()) Result += "Load Port_2 EMO has been trigger, ";
            if (Mediater.ReadBCP_Door()) Result += "The door of electric control box has been open, ";
            if (Mediater.ReadLP1_Door()) Result += "The door of Load Port_1 has been open, ";
            if (Mediater.ReadLP2_Door()) Result += "The door of Load Pord_2 has been open, ";
            if (Mediater.ReadBCP_Smoke()) Result += "Smoke detected in the electric control box, ";

            if (Result == null)
                return true;
            else
                throw new UniversalEquipmentException(Result);
        }

        private bool CheckAssemblyAlarmSignal()
        {
            //var CB_Alarm = HalUniversal.ReadAlarm_Cabinet();
            var CC_Alarm = Mediater.ReadAlarm_CleanCh();
            //var CF_Alarm = HalUniversal.ReadAlarm_CoverFan();
            //var BT_Alarm = HalUniversal.ReadAlarm_BTRobot();
            //var MTClampInsp_Alarm = HalUniversal.ReadAlarm_MTClampInsp();
            //var MT_Alarm = HalUniversal.ReadAlarm_MTRobot();
            //var IC_Alarm = HalUniversal.ReadAlarm_InspCh();
            //var LP_Alarm = HalUniversal.ReadAlarm_LoadPort();
            //var OS_Alarm = HalUniversal.ReadAlarm_OpenStage();

            //if (CB_Alarm != "") throw new CabinetPLCAlarmException(CB_Alarm);
            if (CC_Alarm != "") throw new CleanChPLCAlarmException(CC_Alarm);
            //if (CF_Alarm != "") throw new UniversalCoverFanPLCAlarmException(CF_Alarm);
            //if (BT_Alarm != "") throw new BoxTransferPLCAlarmException(BT_Alarm);
            //if (MTClampInsp_Alarm != "") throw new MTClampInspectDeformPLCAlarmException(MTClampInsp_Alarm);
            //if (MT_Alarm != "") throw new MaskTransferPLCAlarmException(MT_Alarm);
            //if (IC_Alarm != "") throw new InspectionChPLCAlarmException(IC_Alarm);
            //if (LP_Alarm != "") throw new LoadportPLCAlarmException(LP_Alarm);
            //if (OS_Alarm != "") throw new OpenStagePLCAlarmException(OS_Alarm);

            return true;
        }

        private bool CheckAssemblyWarningSignal()
        {
            //var CB_Warning = HalUniversal.ReadWarning_Cabinet();
            var CC_Warning = Mediater.ReadWarning_CleanCh();
            //var CF_Warning = HalUniversal.ReadWarning_CoverFan();
            //var BT_Warning = HalUniversal.ReadWarning_BTRobot();
            //var MTClampInsp_Warning = HalUniversal.ReadWarning_MTClampInsp();
            //var MT_Warning = HalUniversal.ReadWarning_MTRobot();
            //var IC_Warning = HalUniversal.ReadWarning_InspCh();
            //var LP_Warning = HalUniversal.ReadWarning_LoadPort();
            //var OS_Warning = HalUniversal.ReadWarning_OpenStage();

            //if (CB_Warning != "") throw new CabinetPLCWarningException(CB_Warning);
            if (CC_Warning != "") throw new CleanChPLCWarningException(CC_Warning);
            //if (CF_Warning != "") throw new UniversalCoverFanPLCWarningException(CF_Warning);
            //if (BT_Warning != "") throw new BoxTransferPLCWarningException(BT_Warning);
            //if (MTClampInsp_Warning != "") throw new MTClampInspectDeformPLCWarningException(MTClampInsp_Warning);
            //if (MT_Warning != "") throw new MaskTransferPLCWarningException(MT_Warning);
            //if (IC_Warning != "") throw new InspectionChPLCWarningException(IC_Warning);
            //if (LP_Warning != "") throw new LoadportPLCWarningException(LP_Warning);
            //if (OS_Warning != "") throw new OpenStagePLCWarningException(OS_Warning);

            return true;
        }
    }
}
