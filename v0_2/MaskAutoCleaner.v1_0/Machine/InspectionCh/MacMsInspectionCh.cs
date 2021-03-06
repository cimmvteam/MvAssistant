﻿using MaskAutoCleaner.v1_0.StateMachineBeta;
using MaskAutoCleaner.v1_0.StateMachineExceptions.InspectionChStateMachineException;
using MaskAutoCleaner.v1_0.StateMachineExceptions.UniversalStateMachineException;
using MvAssistant.v0_2.Mac.Hal.Assembly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.InspectionCh
{
    public class MacMsInspectionCh : MacMachineStateBase
    {
        private IMacHalInspectionCh HalInspectionCh { get { return this.halAssembly as IMacHalInspectionCh; } }
        
        public MacMsInspectionCh() { this.LoadStateMachine(); }



        #region State Machine Command
        /// <summary> 狀態機啟動 </summary>
        public override void SystemBootup()
        {
            var transition = this.Transitions[EnumMacInspectionChTransition.SystemBootup.ToString()];
            transition.StateFrom.ExecuteCommandAtEntry(new MacStateEntryEventArgs());
        }
        /// <summary> Inspection Chamber初始化 </summary>
        public void Initial()
        {
            var transition = this.Transitions[EnumMacInspectionChTransition.Initial.ToString()];
            transition.StateFrom.ExecuteCommandAtEntry(new MacStateEntryEventArgs());
        }

        /// <summary> 檢測Pellicle </summary>
        public void InspectPellicle()
        {
            var transition = Transitions[EnumMacInspectionChTransition.TriggerToInspectPellicle.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> Mask被取出後將狀態改為Idle ( 必須先由Mask Transfer取出Mask ) </summary>
        public void ReturnToIdleAfterReleasePellicle()
        {
            var transition = Transitions[EnumMacInspectionChTransition.TriggerToIdleAfterReleasePellicle.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }

        /// <summary> 檢測Glass </summary>
        public void InspectGlass()
        {
            var transition = Transitions[EnumMacInspectionChTransition.TriggerToInspectGlass.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }
        /// <summary> Mask被取出後將狀態改為Idle ( 必須先由Mask Transfer取出Mask ) </summary>
        public void ReturnToIdleAfterReleaseGlass()
        {
            var transition = Transitions[EnumMacInspectionChTransition.TriggerToIdleAfterReleaseGlass.ToString()];
            CurrentState.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs());
        }

        #endregion

        public override void LoadStateMachine()
        {
            #region State
            MacState sStart = NewState(EnumMacInspectionChState.Start);
            MacState sInitial = NewState(EnumMacInspectionChState.Initial);

            MacState sIdle = NewState(EnumMacInspectionChState.Idle);
            MacState sPellicleOnStage = NewState(EnumMacInspectionChState.PellicleOnStage);
            MacState sDefensingPellicle = NewState(EnumMacInspectionChState.DefensingPellicle);
            MacState sInspectingPellicle = NewState(EnumMacInspectionChState.InspectingPellicle);
            MacState sPellicleOnStageInspected = NewState(EnumMacInspectionChState.PellicleOnStageInspected);
            MacState sWaitingForReleasePellicle = NewState(EnumMacInspectionChState.WaitingForReleasePellicle);

            MacState sGlassOnStage = NewState(EnumMacInspectionChState.GlassOnStage);
            MacState sDefensingGlass = NewState(EnumMacInspectionChState.DefensingGlass);
            MacState sInspectingGlass = NewState(EnumMacInspectionChState.InspectingGlass);
            MacState sGlassOnStageInspected = NewState(EnumMacInspectionChState.GlassOnStageInspected);
            MacState sWaitingForReleaseGlass = NewState(EnumMacInspectionChState.WaitingForReleaseGlass);
            #endregion State

            #region Transition
            MacTransition tStart_Initial = NewTransition(sStart, sInitial, EnumMacInspectionChTransition.SystemBootup);
            MacTransition tInitial_Idle = NewTransition(sInitial, sIdle, EnumMacInspectionChTransition.Initial);
            MacTransition tIdle_NULL = NewTransition(sIdle, null, EnumMacInspectionChTransition.StandbyAtIdle);

            MacTransition tIdle_PellicleOnStage = NewTransition(sIdle, sPellicleOnStage, EnumMacInspectionChTransition.TriggerToInspectPellicle);
            MacTransition tPellicleOnStage_DefensingPellicle = NewTransition(sPellicleOnStage, sDefensingPellicle, EnumMacInspectionChTransition.PellicleOnStage);
            MacTransition tDefensingPellicle_InspectingPellicle = NewTransition(sDefensingPellicle, sInspectingPellicle, EnumMacInspectionChTransition.DefensePellicle);
            MacTransition tInspectingPellicle_PellicleOnStageInspected = NewTransition(sInspectingPellicle, sPellicleOnStageInspected, EnumMacInspectionChTransition.InspectPellicle);
            MacTransition tPellicleOnStageInspected_WaitingForReleasePellicle = NewTransition(sPellicleOnStageInspected, sWaitingForReleasePellicle, EnumMacInspectionChTransition.InspectedPellicleOnStage);
            MacTransition tWaitingForReleasePellicle_NULL = NewTransition(sWaitingForReleasePellicle, null, EnumMacInspectionChTransition.WaitForReleasePellicle);
            MacTransition tWaitingForReleasePellicle_Idle = NewTransition(sWaitingForReleasePellicle, sIdle, EnumMacInspectionChTransition.TriggerToIdleAfterReleasePellicle);

            MacTransition tIdle_GlassOnStage = NewTransition(sIdle, sGlassOnStage, EnumMacInspectionChTransition.TriggerToInspectGlass);
            MacTransition tGlassOnStage_DefensingGlass = NewTransition(sGlassOnStage, sDefensingGlass, EnumMacInspectionChTransition.GlassOnStage);
            MacTransition tDefensingGlass_InspectingGlass = NewTransition(sDefensingGlass, sInspectingGlass, EnumMacInspectionChTransition.DefenseGlass);
            MacTransition tInspectingGlass_GlassOnStageInspected = NewTransition(sInspectingGlass, sGlassOnStageInspected, EnumMacInspectionChTransition.InspectGlass);
            MacTransition tGlassOnStageInspected_WaitingForReleaseGlass = NewTransition(sGlassOnStageInspected, sWaitingForReleaseGlass, EnumMacInspectionChTransition.InspectedGlassOnStage);
            MacTransition tWaitingForReleaseGlass_NULL = NewTransition(sWaitingForReleaseGlass, null, EnumMacInspectionChTransition.WaitForReleaseGlass);
            MacTransition tWaitingForReleaseGlass_Idle = NewTransition(sWaitingForReleaseGlass, sIdle, EnumMacInspectionChTransition.TriggerToIdleAfterReleaseGlass);
            #endregion Transition

            #region State Register OnEntry OnExit
            sStart.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tStart_Initial;
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

            sInitial.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);
                Mediater.ResetAllAlarm();

                var transition = tInitial_Idle;
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
                            HalInspectionCh.Initial();
                        }
                        catch (Exception ex) { throw new InspectionChInitialFailException(ex.Message); }
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
            sInitial.OnExit += (sender, e) => { };

            sIdle.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tIdle_NULL;
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
                            HalInspectionCh.XYPosition(0, 158);
                            HalInspectionCh.WPosition(0);
                        }
                        catch (Exception ex) { throw new InspectionChPLCExecuteFailException(ex.Message); }
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
            sIdle.OnExit += (sender, e) => { };


            sPellicleOnStage.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tPellicleOnStage_DefensingPellicle;
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
            sPellicleOnStage.OnExit += (sender, e) => { };

            sDefensingPellicle.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tDefensingPellicle_InspectingPellicle;
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
                            //上方相機
                            HalInspectionCh.Camera_TopDfs_CapToSave("D:/Image/IC/TopDfs", "bmp");
                            Thread.Sleep(500);

                            //側邊相機
                            for (int i = 0; i < 360; i += 90)
                            {
                                HalInspectionCh.WPosition(i);
                                HalInspectionCh.Camera_SideDfs_CapToSave("D:/Image/IC/SideDfs", "bmp");
                                Thread.Sleep(500);
                            }
                        }
                        catch (Exception ex) { throw new InspectionChDefenseFailException(ex.Message); }
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
            sDefensingPellicle.OnExit += (sender, e) => { };

            sInspectingPellicle.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tInspectingPellicle_PellicleOnStageInspected;
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
                            HalInspectionCh.ZPosition(-29.6);
                            //上方相機
                            for (int i = 158; i <= 296; i += 23)
                            {
                                for (int j = 123; j <= 261; j += 23)
                                {
                                    HalInspectionCh.XYPosition(i, j);
                                    HalInspectionCh.Camera_TopInsp_CapToSave("D:/Image/IC/TopInsp", "bmp");
                                    Thread.Sleep(500);
                                }
                            }

                            //側邊相機
                            HalInspectionCh.XYPosition(50, 250);
                            for (int i = 0; i < 360; i += 90)
                            {
                                HalInspectionCh.WPosition(i);
                                HalInspectionCh.Camera_SideInsp_CapToSave("D:/Image/IC/SideInsp", "bmp");
                                Thread.Sleep(500);
                            }
                        }
                        catch (Exception ex) { throw new InspectionChInspectFailException(ex.Message); }
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

            sPellicleOnStageInspected.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tPellicleOnStageInspected_WaitingForReleasePellicle;
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
                            HalInspectionCh.XYPosition(0, 158);
                            HalInspectionCh.ZPosition(0);
                        }
                        catch (Exception ex) { throw new InspectionChPLCExecuteFailException(ex.Message); }
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
            sPellicleOnStageInspected.OnExit += (sender, e) => { };

            sWaitingForReleasePellicle.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tWaitingForReleasePellicle_NULL;
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
            sWaitingForReleasePellicle.OnExit += (sender, e) => { };


            sGlassOnStage.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tGlassOnStage_DefensingGlass;
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
            sGlassOnStage.OnExit += (sender, e) => { };

            sDefensingGlass.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tDefensingGlass_InspectingGlass;
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
                            //上方相機
                            HalInspectionCh.Camera_TopDfs_CapToSave("D:/Image/IC/TopDfs", "bmp");
                            Thread.Sleep(500);

                            //側邊相機
                            for (int i = 0; i < 360; i += 90)
                            {
                                HalInspectionCh.WPosition(i);
                                HalInspectionCh.Camera_SideDfs_CapToSave("D:/Image/IC/SideDfs", "bmp");
                                Thread.Sleep(500);
                            }
                        }
                        catch (Exception ex) { throw new InspectionChDefenseFailException(ex.Message); }
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
            sDefensingGlass.OnExit += (sender, e) => { };

            sInspectingGlass.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tInspectingGlass_GlassOnStageInspected;
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
                            HalInspectionCh.ZPosition(-29.6);
                            //上方相機
                            for (int i = 158; i <= 296; i += 23)
                            {
                                for (int j = 123; j <= 261; j += 23)
                                {
                                    HalInspectionCh.XYPosition(i, j);
                                    HalInspectionCh.Camera_TopInsp_CapToSave("D:/Image/IC/TopInsp", "bmp");
                                    Thread.Sleep(500);
                                }
                            }

                            //側邊相機
                            HalInspectionCh.XYPosition(50, 250);
                            for (int i = 0; i < 360; i += 90)
                            {
                                HalInspectionCh.WPosition(i);
                                HalInspectionCh.Camera_SideInsp_CapToSave("D:/Image/IC/SideInsp", "bmp");
                                Thread.Sleep(500);
                            }
                        }
                        catch (Exception ex) { throw new InspectionChInspectFailException(ex.Message); }
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

            sGlassOnStageInspected.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tGlassOnStageInspected_WaitingForReleaseGlass;
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
                            HalInspectionCh.XYPosition(0, 158);
                            HalInspectionCh.ZPosition(0);
                        }
                        catch (Exception ex) { throw new InspectionChPLCExecuteFailException(ex.Message); }
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
            sGlassOnStageInspected.OnExit += (sender, e) => { };

            sWaitingForReleaseGlass.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                var transition = tWaitingForReleaseGlass_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        return true;
                    },
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
            sWaitingForReleaseGlass.OnExit += (sender, e) => { };
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
            //var CC_Alarm = HalUniversal.ReadAlarm_CleanCh();
            //var CF_Alarm = HalUniversal.ReadAlarm_CoverFan();
            //var BT_Alarm = HalUniversal.ReadAlarm_BTRobot();
            //var MTClampInsp_Alarm = HalUniversal.ReadAlarm_MTClampInsp();
            //var MT_Alarm = HalUniversal.ReadAlarm_MTRobot();
            var IC_Alarm = Mediater.ReadAlarm_InspCh();
            //var LP_Alarm = HalUniversal.ReadAlarm_LoadPort();
            //var OS_Alarm = HalUniversal.ReadAlarm_OpenStage();

            //if (CB_Alarm != "") throw new CabinetPLCAlarmException(CB_Alarm);
            //if (CC_Alarm != "") throw new CleanChPLCAlarmException(CC_Alarm);
            //if (CF_Alarm != "") throw new UniversalCoverFanPLCAlarmException(CF_Alarm);
            //if (BT_Alarm != "") throw new BoxTransferPLCAlarmException(BT_Alarm);
            //if (MTClampInsp_Alarm != "") throw new MTClampInspectDeformPLCAlarmException(MTClampInsp_Alarm);
            //if (MT_Alarm != "") throw new MaskTransferPLCAlarmException(MT_Alarm);
            if (IC_Alarm != "") throw new InspectionChPLCAlarmException(IC_Alarm);
            //if (LP_Alarm != "") throw new LoadportPLCAlarmException(LP_Alarm);
            //if (OS_Alarm != "") throw new OpenStagePLCAlarmException(OS_Alarm);

            return true;
        }

        private bool CheckAssemblyWarningSignal()
        {
            //var CB_Warning = HalUniversal.ReadWarning_Cabinet();
            //var CC_Warning = HalUniversal.ReadWarning_CleanCh();
            //var CF_Warning = HalUniversal.ReadWarning_CoverFan();
            //var BT_Warning = HalUniversal.ReadWarning_BTRobot();
            //var MTClampInsp_Warning = HalUniversal.ReadWarning_MTClampInsp();
            //var MT_Warning = HalUniversal.ReadWarning_MTRobot();
            var IC_Warning = Mediater.ReadWarning_InspCh();
            //var LP_Warning = HalUniversal.ReadWarning_LoadPort();
            //var OS_Warning = HalUniversal.ReadWarning_OpenStage();

            //if (CB_Warning != "") throw new CabinetPLCWarningException(CB_Warning);
            //if (CC_Warning != "") throw new CleanChPLCWarningException(CC_Warning);
            //if (CF_Warning != "") throw new UniversalCoverFanPLCWarningException(CF_Warning);
            //if (BT_Warning != "") throw new BoxTransferPLCWarningException(BT_Warning);
            //if (MTClampInsp_Warning != "") throw new MTClampInspectDeformPLCWarningException(MTClampInsp_Warning);
            //if (MT_Warning != "") throw new MaskTransferPLCWarningException(MT_Warning);
            if (IC_Warning != "") throw new InspectionChPLCWarningException(IC_Warning);
            //if (LP_Warning != "") throw new LoadportPLCWarningException(LP_Warning);
            //if (OS_Warning != "") throw new OpenStagePLCWarningException(OS_Warning);

            return true;
        }
    }
}
