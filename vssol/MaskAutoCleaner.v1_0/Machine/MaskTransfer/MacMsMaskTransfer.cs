using MaskAutoCleaner.v1_0.Machine.CleanCh;
using MaskAutoCleaner.v1_0.Machine.InspectionCh;
using MaskAutoCleaner.v1_0.Machine.LoadPort;
using MaskAutoCleaner.v1_0.Machine.OpenStage;
using MaskAutoCleaner.v1_0.Machine.StateExceptions;
using MaskAutoCleaner.v1_0.Msg;
using MaskAutoCleaner.v1_0.Msg.SecsReport;
using MaskAutoCleaner.v1_0.StateMachineBeta;
using MaskAutoCleaner.v1_0.StateMachineExceptions.BoxTransferStateMachineException;
using MaskAutoCleaner.v1_0.StateMachineExceptions.CleanChStateMachineException;
using MaskAutoCleaner.v1_0.StateMachineExceptions.DrawerStateMachineException;
using MaskAutoCleaner.v1_0.StateMachineExceptions.InspectionChStateMachineException;
using MaskAutoCleaner.v1_0.StateMachineExceptions.LoadportStateMachineException;
using MaskAutoCleaner.v1_0.StateMachineExceptions.MaskTransferStateMachineException;
using MaskAutoCleaner.v1_0.StateMachineExceptions.OpenStageStateMachineException;
using MaskAutoCleaner.v1_0.StateMachineExceptions.UniversalStateMachineException;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.JSon.RobotTransferFile;
using MvAssistant.Mac.v1_0.Manifest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.MaskTransfer
{

    /// <summary>
    /// MaskTransfer state machine
    /// </summary>
    [Guid("3C333536-8B09-43B0-9F56-957920050CFB")]
    public class MacMsMaskTransfer : MacMachineStateBase
    {
        public IMacHalMaskTransfer HalMaskTransfer { get { return (IMacHalMaskTransfer)this.halAssembly; } }
        private IMacHalInspectionCh HalInspectionCh { get { return this.Mediater.GetCtrlMachine(EnumMachineID.MID_IC_A_ASB.ToString()).HalAssembly as IMacHalInspectionCh; } }
        private IMacHalOpenStage HalOpenStage { get { return this.Mediater.GetCtrlMachine(EnumMachineID.MID_OS_A_ASB.ToString()).HalAssembly as IMacHalOpenStage; } }
        private IMacHalUniversal HalUniversal { get { return this.Mediater.GetCtrlMachine(EnumMachineID.MID_UNI_A_ASB.ToString()).HalAssembly as IMacHalUniversal; } }


        public void ResetState()
        { this.States[EnumMacMaskTransferState.Start.ToString()].DoEntry(new MacStateEntryEventArgs(null)); }

        public MacMsMaskTransfer() { LoadStateMachine(); }

        MacMaskTransferUnitStateTimeOutController timeoutObj = new MacMaskTransferUnitStateTimeOutController();

        #region State Machine Command

        /// <summary> 狀態機啟動 </summary>
        public override void SystemBootup()
        {
            this.States[EnumMacMaskTransferState.Start.ToString()].ExecuteCommand(new MacStateEntryEventArgs(null));
        }
        /// <summary> Mask Transfer初始化 </summary>
        public void Initial()
        {
            this.States[EnumMacMaskTransferState.Initial.ToString()].DoEntry(new MacStateEntryEventArgs(null));
        }
        /// <summary> 從 LP Home 到 Load Port A 夾取 Mask 並返回 LP Home </summary>
        public void LPHomeToLPAGetMaskReturnToLPHomeClamped()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMaskTransferTransition.MoveToLoadPortA.ToString()];
            triggerMember = new TriggerMember
            {
                Guard = () =>
                {
                    return true;
                },
                Action = null,
                ActionParameter = null,
                ExceptionHandler = (thisState, ex) =>
                {   // TODO: do something
                },
                NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                ThisStateExitEventArgs = new MacStateExitEventArgs(),
            };
            transition.SetTriggerMembers(triggerMember);
            Trigger(transition);
        }
        /// <summary> 從 LP Home 到 Load Port B 夾取 Mask 並返回 LP Home </summary>
        public void LPHomeToLPBGetMaskReturnToLPHomeClamped()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMaskTransferTransition.MoveToLoadPortB.ToString()];
            triggerMember = new TriggerMember
            {
                Guard = () =>
                {
                    return true;
                },
                Action = null,
                ActionParameter = null,
                ExceptionHandler = (thisState, ex) =>
                {   // TODO: do something
                },
                NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                ThisStateExitEventArgs = new MacStateExitEventArgs(),
            };
            transition.SetTriggerMembers(triggerMember);
            Trigger(transition);
        }
        /// <summary> 從 LP Home 到 Open Stage 夾取 Mask 並返回 LP Home </summary>
        public void LPHomeToOSGetMaskReturnToLPHomeClamped()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMaskTransferTransition.MoveToOpenStage.ToString()];
            triggerMember = new TriggerMember
            {
                Guard = () =>
                {
                    return true;
                },
                Action = null,
                ActionParameter = null,
                ExceptionHandler = (thisState, ex) =>
                {   // TODO: do something
                },
                NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                ThisStateExitEventArgs = new MacStateExitEventArgs(),
            };
            transition.SetTriggerMembers(triggerMember);
            Trigger(transition);
        }
        /// <summary> 從 LP Home 轉向到 IC Home(夾著Mask) </summary>
        public void LPHomeClampedToICHomeClamped()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMaskTransferTransition.ChangeDirectionToICHomeClampedFromLPHomeClamped.ToString()];
            triggerMember = new TriggerMember
            {
                Guard = () =>
                {
                    return true;
                },
                Action = null,
                ActionParameter = null,
                ExceptionHandler = (thisState, ex) =>
                {   // TODO: do something
                },
                NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                ThisStateExitEventArgs = new MacStateExitEventArgs(),
            };
            transition.SetTriggerMembers(triggerMember);
            Trigger(transition);
        }
        /// <summary> 從 LP Home 轉向到 IC Home(不夾Mask) </summary>
        public void LPHomeToICHome()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMaskTransferTransition.ChangeDirectionToICHomeFromLPHome.ToString()];
            triggerMember = new TriggerMember
            {
                Guard = () =>
                {
                    return true;
                },
                Action = null,
                ActionParameter = null,
                ExceptionHandler = (thisState, ex) =>
                {   // TODO: do something
                },
                NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                ThisStateExitEventArgs = new MacStateExitEventArgs(),
            };
            transition.SetTriggerMembers(triggerMember);
            Trigger(transition);
        }
        /// <summary> 從 IC Home 轉向到 LP Home(不夾Mask) </summary>
        public void ICHomeToLPHome()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMaskTransferTransition.ChangeDirectionToLPHomeFromICHome.ToString()];
            triggerMember = new TriggerMember
            {
                Guard = () =>
                {
                    return true;
                },
                Action = null,
                ActionParameter = null,
                ExceptionHandler = (thisState, ex) =>
                {   // TODO: do something
                },
                NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                ThisStateExitEventArgs = new MacStateExitEventArgs(),
            };
            transition.SetTriggerMembers(triggerMember);
            Trigger(transition);
        }
        /// <summary> 從 IC Home 夾著 Mask 放入 Inspection Chamber(Pellicle面向上) </summary>
        public void ICHomeClampedToICReleaseReturnToICHome()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMaskTransferTransition.MoveToInspectionChForRelease.ToString()];
            triggerMember = new TriggerMember
            {
                Guard = () =>
                {
                    return true;
                },
                Action = null,
                ActionParameter = null,
                ExceptionHandler = (thisState, ex) =>
                {   // TODO: do something
                },
                NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                ThisStateExitEventArgs = new MacStateExitEventArgs(),
            };
            transition.SetTriggerMembers(triggerMember);
            Trigger(transition);
        }
        /// <summary> 從 IC Home 到 Inspection Chamber 取出 Mask(Pellicle面向上) </summary>
        public void ICHomeToICGetReturnToICClamped()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMaskTransferTransition.MoveToInspectionCh.ToString()];
            triggerMember = new TriggerMember
            {
                Guard = () =>
                {
                    return true;
                },
                Action = null,
                ActionParameter = null,
                ExceptionHandler = (thisState, ex) =>
                {  // TODO: do something
                },
                NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                ThisStateExitEventArgs = new MacStateExitEventArgs(),
            };
            transition.SetTriggerMembers(triggerMember);
            Trigger(transition);
        }
        /// <summary> 從 IC Home 夾著 Mask 放入 Inspection Chamber(Glass面向上) </summary>
        public void ICHomeClampedToICGlassReleaseReturnToICHome()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMaskTransferTransition.MoveToInspectionChGlassForRelease.ToString()];
            triggerMember = new TriggerMember
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
        }
        /// <summary> 從 IC Home 到 Inspection Chamber 取出 Mask(Glass面向上) </summary>
        public void ICHomeToICGlassGetReturnToICClamped()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMaskTransferTransition.MoveToInspectionChGlass.ToString()];
            triggerMember = new TriggerMember
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
        }
        /// <summary> 將 IC Home 夾著 Mask 的狀態轉成 IC Home 夾著 Mask 並且兩面都完成檢測 </summary>
        public void ICHomeClampedToICHomeInspected()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMaskTransferTransition.InspectedAtICHomeClamped.ToString()];
            triggerMember = new TriggerMember
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
        }
        /// <summary> 在 IC Home 夾著 Mask 並且兩面都完成檢測後，需要清潔 Mask ，轉向到 CC Home </summary>
        public void ICHomeInspectedToCCHomeClamped()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMaskTransferTransition.ChangeDirectionToCCHomeClampedFromICHomeInspected.ToString()];
            triggerMember = new TriggerMember
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
        }
        /// <summary> 從 CC Home 夾著 Mask 進入 Clean Chamber(Pellicle面向下) </summary>
        public void CCHomeClampedToCC()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMaskTransferTransition.MoveToCleanChPellicle.ToString()];
            triggerMember = new TriggerMember
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
        }
        /// <summary> 在 Clean Chamber 內夾著 Mask ，移動到 Air Gun 上方(Pellicle面向下) </summary>
        public void InCCMoveToClean()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMaskTransferTransition.MoveToCleanPellicle.ToString()];
            triggerMember = new TriggerMember
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
        }
        /// <summary> 開始進行清理Pellicle的動作 </summary>
        public void CleanPellicle()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMaskTransferTransition.CleanPellicle.ToString()];
            triggerMember = new TriggerMember
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
        }
        /// <summary> Pellicle 清理完回到 Clean Chamber 內的起始點 </summary>
        public void CCCleanedReturnInCC()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMaskTransferTransition.MoveAfterCleanedPellicle.ToString()];
            triggerMember = new TriggerMember
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
        }
        /// <summary> 在 Clean Chamber 內夾著 Mask ，移動到 Camera 上方(Pellicle面向下) </summary>
        public void InCCMoveToInspect()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMaskTransferTransition.MoveToInspectPellicle.ToString()];
            triggerMember = new TriggerMember
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
        }
        /// <summary> 開始進行檢測Pellicle的動作 </summary>
        public void InspectPellicle()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMaskTransferTransition.InspectPellicle.ToString()];
            triggerMember = new TriggerMember
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
        }
        /// <summary> Pellicle 檢測完回到 Clean Chamber 內的起始點 </summary>
        public void CCInspectedReturnInCC()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMaskTransferTransition.MoveAfterInspectedPellicle.ToString()];
            triggerMember = new TriggerMember
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
        }
        /// <summary> 從 Clean Cjamber 內(Pellecle面向下)，夾著 Mask 回到 CC Home </summary>
        public void InCCToCCHomeClamped()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMaskTransferTransition.MoveToCCHomeClampedFromCleanCh.ToString()];
            triggerMember = new TriggerMember
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
        }
        /// <summary> 從 CC Home 夾著 Mask 進入 Clean Chamber(Glass面向下) </summary>
        public void CCHomeClampedToCCGlass()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMaskTransferTransition.MoveToCleanChGlass.ToString()];
            triggerMember = new TriggerMember
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
        }
        /// <summary> 在 Clean Chamber 內夾著 Mask ，移動到 Air Gun 上方(Glass面向下) </summary>
        public void InCCGlassMoveToClean()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMaskTransferTransition.MoveToCleanGlass.ToString()];
            triggerMember = new TriggerMember
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
        }
        /// <summary> 開始進行清理Glass的動作 </summary>
        public void CleanGlass()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMaskTransferTransition.CleanGlass.ToString()];
            triggerMember = new TriggerMember
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
        }
        /// <summary> Glass 清理完回到 Clean Chamber 內的起始點 </summary>
        public void CCGlassCleanedReturnInCCGlass()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMaskTransferTransition.MoveAfterCleanedGlass.ToString()];
            triggerMember = new TriggerMember
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
        }
        /// <summary> 在 Clean Chamber 內夾著 Mask ，移動到 Camera 上方(Glass面向下) </summary>
        public void InCCGlassMoveToInspect()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMaskTransferTransition.MoveToInspectGlass.ToString()];
            triggerMember = new TriggerMember
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
        }
        /// <summary> 開始進行檢測Glass的動作 </summary>
        public void InspectGlass()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMaskTransferTransition.InspectGlass.ToString()];
            triggerMember = new TriggerMember
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
        }
        /// <summary> Glass 檢測完回到 Clean Chamber 內的起始點 </summary>
        public void CCGlassInspectedReturnInCCGlass()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMaskTransferTransition.MoveAfterInspectedGlass.ToString()];
            triggerMember = new TriggerMember
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
        }
        /// <summary> 從 Clean Cjamber 內(Glass面向下)，夾著 Mask 回到 CC Home </summary>
        public void InCCGlassToCCHomeClamped()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMaskTransferTransition.MoveToCCHomeClampedFromCleanChGlass.ToString()];
            triggerMember = new TriggerMember
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
        }
        /// <summary> 將 CC Home 夾著 Mask 的狀態轉成 CC Home 夾著 Mask 並且完成清潔 </summary>
        public void CCHomeClampedToCCHomeCleaned()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMaskTransferTransition.CleanedAtCCHomeClamped.ToString()];
            triggerMember = new TriggerMember
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
        }
        /// <summary> 在 IC Home 夾著 Mask 並且兩面都完成檢測，不用清潔直接轉向到 LP Home </summary>
        public void ICHomeInspectedToLPHomeInspected()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMaskTransferTransition.InspectedAtLPHomeClamped.ToString()];
            triggerMember = new TriggerMember
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
        }
        /// <summary> 在 CC Home 夾著 Mask 並且完成清潔，轉向到 LP Home </summary>
        public void CCHomeCleanedToLPHomeCleaned()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMaskTransferTransition.CleanedAtLPHomeClamped.ToString()];
            triggerMember = new TriggerMember
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
        }
        /// <summary> 從 LP Home 將未經過檢查的 Mask 放到 Open Stage，回到 LP Home </summary>
        public void LPHomeClampedToOSReleaseMaskReturnToLPHome()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMaskTransferTransition.MoveToOpenStageForRelease.ToString()];
            triggerMember = new TriggerMember
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
        }
        /// <summary> 從 LP Home 將已經檢測過，不需清理的 Mask 放到 Load Port A </summary>
        public void LPHomeInspectedToLPARelease()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMaskTransferTransition.MoveToLoadPortAInspectedForRelease.ToString()];
            triggerMember = new TriggerMember
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
        }
        /// <summary> 從 LP Home 將已經檢測過，不需清理的 Mask 放到 Load Port B </summary>
        public void LPHomeInspectedToLPBRelease()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMaskTransferTransition.MoveToLoadPortBInspectedForRelease.ToString()];
            triggerMember = new TriggerMember
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
        }
        /// <summary> 從 LP Home 將已經檢測過，不需清理的 Mask 放到 Open Stage </summary>
        public void LPHomeInspectedToOSRelease()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMaskTransferTransition.MoveToOpenStageInspectedForRelease.ToString()];
            triggerMember = new TriggerMember
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
        }
        /// <summary> 從 LP Home 將已經清理過的 Mask 放到 Load Port A </summary>
        public void LPHomeCleanedToLPARelease()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMaskTransferTransition.MoveToLoadPortACleanedForRelease.ToString()];
            triggerMember = new TriggerMember
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
        }
        /// <summary> 從 LP Home 將已經清理過的 Mask 放到 Load Port B </summary>
        public void LPHomeCleanedToLPBRelease()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMaskTransferTransition.MoveToLoadPortBCleanedForRelease.ToString()];
            triggerMember = new TriggerMember
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
        }
        /// <summary> 從 LP Home 將已經清理過的 Mask 放到 Open Stage </summary>
        public void LPHomeCleanedToOSRelease()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMaskTransferTransition.MoveToOpenStageCleanedForRelease.ToString()];
            triggerMember = new TriggerMember
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
        }
        /// <summary> 從 LP Home 移動到 BarcodeReader </summary>
        public void LPHomeToBarcodeReader()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMaskTransferTransition.MoveToBarcodeReaderClamped.ToString()];
            triggerMember = new TriggerMember
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
        }
        /// <summary> 從 BarcodeReader移動到 LP Home </summary>
        public void BarcodeReaderToLPHome()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMaskTransferTransition.MoveToLPHomeClampedFromBarcodeReader.ToString()];
            triggerMember = new TriggerMember
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
        }
        /// <summary> 從 IC Home 移動到變形檢測裝置 </summary>
        public void ICHomeToInspDeform()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMaskTransferTransition.MoveToInspectDeformFromICHome.ToString()];
            triggerMember = new TriggerMember
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
        }
        /// <summary> 從變形檢測裝置移動到 IC Home </summary>
        public void InspDeformToICHome()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = Transitions[EnumMacMaskTransferTransition.MoveToICHomeFromInspectDeform.ToString()];
            triggerMember = new TriggerMember
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
        }

        #endregion

        public override void LoadStateMachine()
        {
            //--- Declare State ---
            #region State
            MacState sStart = NewState(EnumMacMaskTransferState.Start);
            MacState sInitial = NewState(EnumMacMaskTransferState.Initial);

            // Position
            MacState sLPHome = NewState(EnumMacMaskTransferState.LPHome);
            MacState sICHome = NewState(EnumMacMaskTransferState.ICHome);
            MacState sLPHomeClamped = NewState(EnumMacMaskTransferState.LPHomeClamped);
            MacState sLPHomeInspected = NewState(EnumMacMaskTransferState.LPHomeInspected);
            MacState sLPHomeCleaned = NewState(EnumMacMaskTransferState.LPHomeCleaned);
            MacState sICHomeClamped = NewState(EnumMacMaskTransferState.ICHomeClamped);
            MacState sICHomeInspected = NewState(EnumMacMaskTransferState.ICHomeInspected);
            MacState sCCHomeClamped = NewState(EnumMacMaskTransferState.CCHomeClamped);
            MacState sCCHomeCleaned = NewState(EnumMacMaskTransferState.CCHomeCleaned);

            // Change Direction
            MacState sChangingDirectionToLPHome = NewState(EnumMacMaskTransferState.ChangingDirectionToLPHome);
            MacState sChangingDirectionToICHome = NewState(EnumMacMaskTransferState.ChangingDirectionToICHome);
            MacState sChangingDirectionToLPHomeClamped = NewState(EnumMacMaskTransferState.ChangingDirectionToLPHomeClamped);
            MacState sChangingDirectionToLPHomeInspected = NewState(EnumMacMaskTransferState.ChangingDirectionToLPHomeClamped);
            MacState sChangingDirectionToLPHomeCleaned = NewState(EnumMacMaskTransferState.ChangingDirectionToLPHomeClamped);
            MacState sChangingDirectionToICHomeClamped = NewState(EnumMacMaskTransferState.ChangingDirectionToICHomeClamped);
            MacState sChangingDirectionToCCHomeClamped = NewState(EnumMacMaskTransferState.ChangingDirectionToCCHomeClamped);

            //To Target Clamp - Move
            MacState sMovingToLoadPortA = NewState(EnumMacMaskTransferState.MovingToLoadPortA);
            MacState sMovingToLoadPortB = NewState(EnumMacMaskTransferState.MovingToLoadPortB);
            MacState sMovingToInspectionCh = NewState(EnumMacMaskTransferState.MovingToInspectionCh);
            MacState sMovingToInspectionChGlass = NewState(EnumMacMaskTransferState.MovingToInspectionChGlass);
            MacState sMovingToOpenStage = NewState(EnumMacMaskTransferState.MovingToOpenStage);

            //To Target Clamp - Calibration
            MacState sLoadPortAClamping = NewState(EnumMacMaskTransferState.LoadPortAClamping);
            MacState sLoadPortBClamping = NewState(EnumMacMaskTransferState.LoadPortBClamping);
            MacState sInspectionChClamping = NewState(EnumMacMaskTransferState.InspectionChClamping);
            MacState sInspectionChGlassClamping = NewState(EnumMacMaskTransferState.InspectionChGlassClamping);
            MacState sOpenStageClamping = NewState(EnumMacMaskTransferState.OpenStageClamping);

            //Clamped Back - Move
            MacState sMovingToLPHomeClampedFromLoadPortA = NewState(EnumMacMaskTransferState.MovingToLPHomeClampedFromLoadPortA);
            MacState sMovingToLPHomeClampedFromLoadPortB = NewState(EnumMacMaskTransferState.MovingToLPHomeClampedFromLoadPortB);
            MacState sMovingToICHomeClampedFromInspectionCh = NewState(EnumMacMaskTransferState.MovingToICHomeClampedFromInspectionCh);
            MacState sMovingToICHomeClampedFromInspectionChGlass = NewState(EnumMacMaskTransferState.MovingToICHomeClampedFromInspectionChGlass);
            MacState sMovingToLPHomeClampedFromOpenStage = NewState(EnumMacMaskTransferState.MovingToLPHomeClampedFromOpenStage);


            //Barcode Reader
            MacState sMovingToBarcodeReaderClamped = NewState(EnumMacMaskTransferState.MovingToBarcodeReaderClamped);
            MacState sReadingBarcode = NewState(EnumMacMaskTransferState.ReadingBarcode);
            MacState sMovingToLPHomeClampedFromBarcodeReader = NewState(EnumMacMaskTransferState.MovingToLPHomeClampedFromBarcodeReader);


            //Clean
            MacState sMovingToCleanCh = NewState(EnumMacMaskTransferState.MovingToCleanChPellicle);//前往CleanCh
            MacState sClampedInCleanCh = NewState(EnumMacMaskTransferState.ClampedInCleanChTargetPellicle);//準備好Clean
            MacState sMovingToClean = NewState(EnumMacMaskTransferState.MovingInCleanChToCleanPellicle);
            MacState sCleaningPellicle = NewState(EnumMacMaskTransferState.CleaningPellicleInCleanCh);
            MacState sMovingAfterCleaned = NewState(EnumMacMaskTransferState.MovingInCleanChAfterCleanedPellicle);
            MacState sMovingToInspect = NewState(EnumMacMaskTransferState.MovingInCleanChToInspectPellicle);
            MacState sInspectingPellicle = NewState(EnumMacMaskTransferState.InspectingPellicleInCleanCh);
            MacState sMovingAfterInspected = NewState(EnumMacMaskTransferState.MovingInCleanChAfterInspectedPellicle);
            MacState sMovingToCCHomeClampedFromCleanCh = NewState(EnumMacMaskTransferState.MovingToCCHomeClampedFromCleanCh);//離開CleanCh

            MacState sMovingToCleanChGlass = NewState(EnumMacMaskTransferState.MovingToCleanChGlass);//前往CleanChGlass
            MacState sClampedInCleanChGlass = NewState(EnumMacMaskTransferState.ClampedInCleanChTargetGlass);//準備好CleanGlass
            MacState sMovingToCleanGlass = NewState(EnumMacMaskTransferState.MovingInCleanChToCleanGlass);
            MacState sCleaningGlass = NewState(EnumMacMaskTransferState.CleaningGlassInCleanCh);
            MacState sMovingAfterCleanedGlass = NewState(EnumMacMaskTransferState.MovingInCleanChAfterCleanedGlass);
            MacState sMovingToInspectGlass = NewState(EnumMacMaskTransferState.MovingInCleanChToInspectGlass);
            MacState sInspectingGlass = NewState(EnumMacMaskTransferState.InspectingGlassInCleanCh);
            MacState sMovingAfterInspectedGlass = NewState(EnumMacMaskTransferState.MovingInCleanChAfterInspectedGlass);
            MacState sMovingToCCHomeClampedFromCleanChGlass = NewState(EnumMacMaskTransferState.MovingToCCHomeClampedFromCleanChGlass);//離開CleanChGlass

            //Inspect Deform
            MacState sMovingToInspectDeformFromICHome = NewState(EnumMacMaskTransferState.MovingToInspectDeform);
            MacState sInspectingClampDeform = NewState(EnumMacMaskTransferState.InspectingClampDeform);
            MacState sMovingToICHomeFromInspectDeform = NewState(EnumMacMaskTransferState.MovingToICHomeFromInspectDeform);

            //To Target
            MacState sMovingToLoadPortAForRelease = NewState(EnumMacMaskTransferState.MovingToLoadPortAForRelease);
            MacState sMovingToLoadPortBForRelease = NewState(EnumMacMaskTransferState.MovingToLoadPortBForRelease);
            MacState sMovingToInspectionChForRelease = NewState(EnumMacMaskTransferState.MovingToInspectionChForRelease);
            MacState sMovingToInspectionChGlassForRelease = NewState(EnumMacMaskTransferState.MovingToInspectionChGlassForRelease);
            MacState sMovingOpenStageForRelease = NewState(EnumMacMaskTransferState.MovingToOpenStageForRelease);

            MacState sLoadPortAReleasing = NewState(EnumMacMaskTransferState.LoadPortAReleasing);
            MacState sLoadPortBReleasing = NewState(EnumMacMaskTransferState.LoadPortBReleasing);
            MacState sInspectionChReleasing = NewState(EnumMacMaskTransferState.InspectionChReleasing);
            MacState sInspectionChGlassReleasing = NewState(EnumMacMaskTransferState.InspectionChGlassReleasing);
            MacState sOpenStageReleasing = NewState(EnumMacMaskTransferState.OpenStageReleasing);


            MacState sMovingToLPHomeFromLoadPortA = NewState(EnumMacMaskTransferState.MovingToLPHomeFromLoadPortA);
            MacState sMovingToLPHomeFromLoadPortB = NewState(EnumMacMaskTransferState.MovingToLPHomeFromLoadPortB);
            MacState sMovingToICHomeFromInspectionCh = NewState(EnumMacMaskTransferState.MovingToICHomeFromInspectionCh);
            MacState sMovingToICHomeFromInspectionChGlass = NewState(EnumMacMaskTransferState.MovingToICHomeFromInspectionChGlass);
            MacState sMovingToLPHomeFromOpenStage = NewState(EnumMacMaskTransferState.MovingToLPHomeFromOpenStage);

            #endregion State

            //--- Transition ---
            #region Transition
            MacTransition tStart_DeviceInitial = NewTransition(sStart, sInitial, EnumMacMaskTransferTransition.PowerON);
            MacTransition tDeviceInitial_LPHome = NewTransition(sInitial, sLPHome, EnumMacMaskTransferTransition.Initial);
            MacTransition tLPHome_NULL = NewTransition(sLPHome, null, EnumMacMaskTransferTransition.StandbyAtLPHome);
            MacTransition tLPHomeClamped_NULL = NewTransition(sLPHomeClamped, null, EnumMacMaskTransferTransition.StandbyAtLPHomeClamped);
            MacTransition tLPHomeInspected_NULL = NewTransition(sLPHomeInspected, null, EnumMacMaskTransferTransition.StandbyAtLPHomeInspected);
            MacTransition tLPHomeCleaned_NULL = NewTransition(sLPHomeCleaned, null, EnumMacMaskTransferTransition.StandbyAtLPHomeCleaned);
            MacTransition tICHome_NULL = NewTransition(sICHome, null, EnumMacMaskTransferTransition.StandbyAtICHome);
            MacTransition tICHomeClamped_NULL = NewTransition(sICHomeClamped, null, EnumMacMaskTransferTransition.StandbyAtICHomeClamped);
            MacTransition tICHomeClamped_ICHomeInspected = NewTransition(sICHomeClamped, sICHomeInspected, EnumMacMaskTransferTransition.InspectedAtICHomeClamped);
            MacTransition tICHomeInspected_NULL = NewTransition(sICHomeInspected, null, EnumMacMaskTransferTransition.StandbyAtICHomeInspected);
            MacTransition tICHomeInspected_LPHomeInspected = NewTransition(sICHomeInspected, sLPHomeInspected, EnumMacMaskTransferTransition.InspectedAtLPHomeClamped);
            MacTransition tCCHomeClamped_NULL = NewTransition(sCCHomeClamped, null, EnumMacMaskTransferTransition.StandbyAtCCHomeClamped);
            MacTransition tCCHomeClamped_CCHomeCleaned = NewTransition(sCCHomeClamped, sCCHomeCleaned, EnumMacMaskTransferTransition.CleanedAtCCHomeClamped);
            MacTransition tCCHomeCleaned_NULL = NewTransition(sCCHomeCleaned, null, EnumMacMaskTransferTransition.StandbyAtCCHomeCleaned);
            MacTransition tCCHomeCleaned_LPHomeCleaned = NewTransition(sCCHomeCleaned, sLPHomeCleaned, EnumMacMaskTransferTransition.CleanedAtLPHomeClamped);

            #region Change Direction
            MacTransition tLPHome_ChangingDirectionToICHome = NewTransition(sLPHome, sChangingDirectionToICHome, EnumMacMaskTransferTransition.ChangeDirectionToICHomeFromLPHome);
            MacTransition tICHome_ChangingDirectionToLPHome = NewTransition(sICHome, sChangingDirectionToLPHome, EnumMacMaskTransferTransition.ChangeDirectionToLPHomeFromICHome);
            MacTransition tLPHomeClamped_ChangingDirectionToICHomeClamped = NewTransition(sLPHomeClamped, sChangingDirectionToICHomeClamped, EnumMacMaskTransferTransition.ChangeDirectionToICHomeClampedFromLPHomeClamped);
            //MacTransition tLPHomeClamped_ChangingDirectionToCCHomeClamped = NewTransition(sLPHomeClamped, sChangingDirectionToCCHomeClamped, EnumMacMaskTransferTransition.ChangeDirectionToCCHomeClampedFromLPHomeClamped);
            MacTransition tICHomeClamped_ChangingDirectionToLPHomeClamped = NewTransition(sICHomeClamped, sChangingDirectionToLPHomeClamped, EnumMacMaskTransferTransition.ChangeDirectionToLPHomeClampedFromICHomeClamped);
            MacTransition tICHomeInspected_ChangingDirectionToCCHomeClamped = NewTransition(sICHomeInspected, sChangingDirectionToCCHomeClamped, EnumMacMaskTransferTransition.ChangeDirectionToCCHomeClampedFromICHomeInspected);
            MacTransition tICHomeInspected_ChangingDirectionToLPHomeInspected = NewTransition(sICHomeInspected, sChangingDirectionToLPHomeInspected, EnumMacMaskTransferTransition.ChangeDirectionToLPHomeInspectedFromICHomeInspected);
            MacTransition tCCHomeClamped_ChangingDirectionToLPHomeClamped = NewTransition(sCCHomeClamped, sChangingDirectionToLPHomeClamped, EnumMacMaskTransferTransition.ChangeDirectionToLPHomeClampedFromCCHomeClamped);
            MacTransition tCCHomeClamped_ChangingDirectionToICHomeClamped = NewTransition(sCCHomeClamped, sChangingDirectionToICHomeClamped, EnumMacMaskTransferTransition.ChangeDirectionToICHomeClampedFromCCHomeClamped);
            MacTransition tCCHomeCleaned_ChangingDirectionToLPHomeCleaned = NewTransition(sCCHomeCleaned, sChangingDirectionToLPHomeCleaned, EnumMacMaskTransferTransition.ChangeDirectionToLPHomeCleanedFromCCHomeCleaned);
            MacTransition tChangingDirectionToICHome_ICHome = NewTransition(sChangingDirectionToICHome, sICHome, EnumMacMaskTransferTransition.FinishChangeDirectionToICHome);
            MacTransition tChangingDirectionToLPHome_LPHome = NewTransition(sChangingDirectionToLPHome, sLPHome, EnumMacMaskTransferTransition.FinishChangeDirectionToLPHome);
            MacTransition tChangingDirectionToICHomeClamped_ICHomeClamped = NewTransition(sChangingDirectionToICHomeClamped, sICHomeClamped, EnumMacMaskTransferTransition.FinishChangeDirectionToICHomeClamped);
            MacTransition tChangingDirectionToLPHomeClamped_LPHomeClamped = NewTransition(sChangingDirectionToLPHomeClamped, sLPHomeClamped, EnumMacMaskTransferTransition.FinishChangeDirectionToLPHomeClamped);
            MacTransition tChangingDirectionToLPHomeInspected_LPHomeInspected = NewTransition(sChangingDirectionToLPHomeInspected, sLPHomeInspected, EnumMacMaskTransferTransition.FinishChangeDirectionToLPHomeInspected);
            MacTransition tChangingDirectionToLPHomeCleaned_LPHomeCleaned = NewTransition(sChangingDirectionToLPHomeCleaned, sLPHomeCleaned, EnumMacMaskTransferTransition.FinishChangeDirectionToLPHomeCleaned);
            MacTransition tChangingDirectionToCCHomeClamped_CCHomeClamped = NewTransition(sChangingDirectionToCCHomeClamped, sCCHomeClamped, EnumMacMaskTransferTransition.FinishChangeDirectionToCCHomeClamped);
            #endregion Change Direction

            #region Load Port A
            MacTransition tLPHome_MovingToLoadPortA = NewTransition(sLPHome, sMovingToLoadPortA, EnumMacMaskTransferTransition.MoveToLoadPortA);
            MacTransition tMovingToLoadPortA_LoadPortAClamping = NewTransition(sMovingToLoadPortA, sLoadPortAClamping, EnumMacMaskTransferTransition.ClampInLoadPortA);
            MacTransition tLoadPortAClamping_MovingToLPHomeClampedFromLoadPortA = NewTransition(sLoadPortAClamping, sMovingToLPHomeClampedFromLoadPortA, EnumMacMaskTransferTransition.MoveToLPHomeClampedFromLoadPortA);
            MacTransition tMovingToLPHomeClampedFromLoadPortA_LPHomeClamped = NewTransition(sMovingToLPHomeClampedFromLoadPortA, sLPHomeClamped, EnumMacMaskTransferTransition.StandbyAtLPHomeClampedFromLoadPortA);

            MacTransition tLPHomeInspected_MovingToLoadPortAForRelease = NewTransition(sLPHomeInspected, sMovingToLoadPortAForRelease, EnumMacMaskTransferTransition.MoveToLoadPortAInspectedForRelease);
            MacTransition tLPHomeCleaned_MovingToLoadPortAForRelease = NewTransition(sLPHomeCleaned, sMovingToLoadPortAForRelease, EnumMacMaskTransferTransition.MoveToLoadPortACleanedForRelease);
            MacTransition tMovingToLoadPortAForRelease_LoadPortAReleasing = NewTransition(sMovingToLoadPortAForRelease, sLoadPortAReleasing, EnumMacMaskTransferTransition.ReleaseInLoadPortA);
            MacTransition tLoadPortAReleasing_MovingToLPHomeFromLoadPortA = NewTransition(sLoadPortAReleasing, sMovingToLPHomeFromLoadPortA, EnumMacMaskTransferTransition.MoveToLPHomeFromLoadPortA);
            MacTransition tMovingToLPHomeFromLoadPortA_LPHome = NewTransition(sMovingToLPHomeFromLoadPortA, sLPHome, EnumMacMaskTransferTransition.StandbyAtLPHomeFromLoadPortA);
            #endregion Load Port A

            #region Load Port B
            MacTransition tLPHome_MovingToLoadPortB = NewTransition(sLPHome, sMovingToLoadPortB, EnumMacMaskTransferTransition.MoveToLoadPortB);
            MacTransition tMovingToLoadPortB_LoadPortBClamping = NewTransition(sMovingToLoadPortB, sLoadPortBClamping, EnumMacMaskTransferTransition.ToClampInLoadPortB);
            MacTransition tLoadPortBClamping_MovingToLPHomeClampedFromLoadPortB = NewTransition(sLoadPortBClamping, sMovingToLPHomeClampedFromLoadPortB, EnumMacMaskTransferTransition.MoveToLPHomeClampedFromLoadPortB);
            MacTransition tMovingToLPHomeClampedFromLoadPortB_LPHomeClamped = NewTransition(sMovingToLPHomeClampedFromLoadPortB, sLPHomeClamped, EnumMacMaskTransferTransition.StandbyAtLPHomeClampedFromLoadPortB);

            MacTransition tLPHomeInspected_MovingToLoadPortBForRelease = NewTransition(sLPHomeInspected, sMovingToLoadPortBForRelease, EnumMacMaskTransferTransition.MoveToLoadPortBInspectedForRelease);
            MacTransition tLPHomeCleaned_MovingToLoadPortBForRelease = NewTransition(sLPHomeCleaned, sMovingToLoadPortBForRelease, EnumMacMaskTransferTransition.MoveToLoadPortBCleanedForRelease);
            MacTransition tMovingToLoadPortBForRelease_LoadPortBReleasing = NewTransition(sMovingToLoadPortBForRelease, sLoadPortBReleasing, EnumMacMaskTransferTransition.ReleaseInLoadPortB);
            MacTransition tLoadPortBReleasing_MovingToLPHomeFromLoadPortB = NewTransition(sLoadPortBReleasing, sMovingToLPHomeFromLoadPortB, EnumMacMaskTransferTransition.MoveToLPHomeFromLoadPortB);
            MacTransition tMovingToLPHomeFromLoadPortB_LPHome = NewTransition(sMovingToLPHomeFromLoadPortB, sLPHome, EnumMacMaskTransferTransition.StandbyAtLPHomeFromLoadPortB);
            #endregion Load Port B

            #region Inspection Ch
            MacTransition tICHome_MovingToInspectionCh = NewTransition(sICHome, sMovingToInspectionCh, EnumMacMaskTransferTransition.MoveToInspectionCh);
            MacTransition tMovingToInspectionCh_InspectionChClamping = NewTransition(sMovingToInspectionCh, sInspectionChClamping, EnumMacMaskTransferTransition.ClampInInspectionCh);
            MacTransition tInspectionChClamping_MovingToICHomeClampedFromInspectionCh = NewTransition(sInspectionChClamping, sMovingToICHomeClampedFromInspectionCh, EnumMacMaskTransferTransition.MoveToICHomeClampedFromInspectionCh);
            MacTransition tMovingToICHomeClampedFromInspectionCh_ICHomeClamped = NewTransition(sMovingToICHomeClampedFromInspectionCh, sICHomeClamped, EnumMacMaskTransferTransition.StandbyAtICHomeClampedFromInspectionCh);
            MacTransition tICHomeClamped_MovingToInspectionChForRelease = NewTransition(sICHomeClamped, sMovingToInspectionChForRelease, EnumMacMaskTransferTransition.MoveToInspectionChForRelease);
            MacTransition tMovingInspectionChForRelease_InspectionChReleasing = NewTransition(sMovingToInspectionChForRelease, sInspectionChReleasing, EnumMacMaskTransferTransition.ReleaseInInspectionCh);
            MacTransition tInspectionChReleasing_MovingToICHomeFromInspectionCh = NewTransition(sInspectionChReleasing, sMovingToICHomeFromInspectionCh, EnumMacMaskTransferTransition.MoveToICHomeFromInspectionCh);
            MacTransition tMovingToICHomeFromInspectionCh_ICHome = NewTransition(sMovingToICHomeFromInspectionCh, sICHome, EnumMacMaskTransferTransition.StandbyAtICHomeFromInspectionCh);

            MacTransition tICHome_MovingToInspectionChGlass = NewTransition(sICHome, sMovingToInspectionChGlass, EnumMacMaskTransferTransition.MoveToInspectionChGlass);
            MacTransition tMovingToInspectionChGlass_InspectionChGlassClamping = NewTransition(sMovingToInspectionChGlass, sInspectionChGlassClamping, EnumMacMaskTransferTransition.ClampInInspectionChGlass);
            MacTransition tInspectionChGlassClamping_MovingToICHomeClampedFromInspectionChGlass = NewTransition(sInspectionChGlassClamping, sMovingToICHomeClampedFromInspectionChGlass, EnumMacMaskTransferTransition.MoveToICHomeClampedFromInspectionChGlass);
            MacTransition tMovingToICHomeClampedFromInspectionChGlass_ICHomeClamped = NewTransition(sMovingToICHomeClampedFromInspectionChGlass, sICHomeClamped, EnumMacMaskTransferTransition.StandbyAtICHomeClampedFromInspectionChGlass);
            MacTransition tICHomeClamped_MovingToInspectionChGlassForRelease = NewTransition(sICHomeClamped, sMovingToInspectionChGlassForRelease, EnumMacMaskTransferTransition.MoveToInspectionChGlassForRelease);
            MacTransition tMovingInspectionChGlassForRelease_InspectionChGlassReleasing = NewTransition(sMovingToInspectionChGlassForRelease, sInspectionChGlassReleasing, EnumMacMaskTransferTransition.ReleaseInInspectionChGlass);
            MacTransition tInspectionChGlassReleasing_MovingToICHomeFromInspectionChGlass = NewTransition(sInspectionChGlassReleasing, sMovingToICHomeFromInspectionChGlass, EnumMacMaskTransferTransition.MoveToICHomeFromInspectionChGlass);
            MacTransition tMovingToICHomeFromInspectionChGlass_ICHome = NewTransition(sMovingToICHomeFromInspectionChGlass, sICHome, EnumMacMaskTransferTransition.StandbyAtICHomeFromInspectionChGlass);
            #endregion Inspection Ch

            #region Clean Ch
            MacTransition tCCHomeClamped_MovingToCleanCh = NewTransition(sCCHomeClamped, sMovingToCleanCh, EnumMacMaskTransferTransition.MoveToCleanChPellicle);
            MacTransition tMovingToCleanCh_ClampedInCleanCh = NewTransition(sMovingToCleanCh, sClampedInCleanCh, EnumMacMaskTransferTransition.WaitForMoveToClean);
            MacTransition tClampedInCleanCh_NULL = NewTransition(sClampedInCleanCh, null, EnumMacMaskTransferTransition.StandbyClampedInCleanCh);
            MacTransition tClampedInCleanCh_MovingToClean = NewTransition(sClampedInCleanCh, sMovingToClean, EnumMacMaskTransferTransition.MoveToCleanPellicle);
            MacTransition tMovingToClean_NULL = NewTransition(sMovingToClean, null, EnumMacMaskTransferTransition.WaitFroClean);
            MacTransition tMovingToClean_CleaningPellicle = NewTransition(sMovingToClean, sCleaningPellicle, EnumMacMaskTransferTransition.CleanPellicle);
            MacTransition tCleaningPellicle_NULL = NewTransition(sCleaningPellicle, null, EnumMacMaskTransferTransition.StandbyAtClean);
            MacTransition tCleaningPellicle_MovingAfterCleaned = NewTransition(sCleaningPellicle, sMovingAfterCleaned, EnumMacMaskTransferTransition.MoveAfterCleanedPellicle);
            MacTransition tMovingAfterCleaned_ClampedInCleanCh = NewTransition(sMovingAfterCleaned, sClampedInCleanCh, EnumMacMaskTransferTransition.WaitForMoveToInspect);
            MacTransition tClampedInCleanCh_MovingToInspect = NewTransition(sClampedInCleanCh, sMovingToInspect, EnumMacMaskTransferTransition.MoveToInspectPellicle);
            MacTransition tMovingToInspect_NULL = NewTransition(sMovingToInspect, null, EnumMacMaskTransferTransition.WaitForInspect);
            MacTransition tMovingToInspect_InspectingPellicle = NewTransition(sMovingToInspect, sInspectingPellicle, EnumMacMaskTransferTransition.InspectPellicle);
            MacTransition tInspectingPellicle_NULL = NewTransition(sInspectingPellicle, null, EnumMacMaskTransferTransition.StandbyAtInspect);
            MacTransition tInspectingPellicle_MovingAfterInspected = NewTransition(sInspectingPellicle, sMovingAfterInspected, EnumMacMaskTransferTransition.MoveAfterInspectedPellicle);
            MacTransition tMovingAfterInspected_ClampedInCleanCh = NewTransition(sMovingAfterInspected, sClampedInCleanCh, EnumMacMaskTransferTransition.WaitForLeaveCleanCh);
            MacTransition tClampedInCleanCh_MovingToCCHomeClampedFromCleanCh = NewTransition(sClampedInCleanCh, sMovingToCCHomeClampedFromCleanCh, EnumMacMaskTransferTransition.MoveToCCHomeClampedFromCleanCh);
            MacTransition tMovingToCCHomeClampedFromCleanCh_CCHomeClamped = NewTransition(sMovingToCCHomeClampedFromCleanCh, sCCHomeClamped, EnumMacMaskTransferTransition.StandbyAtCCHomeClampedFromCleanCh);

            MacTransition tCCHomeClamped_MovingToCleanChGlass = NewTransition(sCCHomeClamped, sMovingToCleanChGlass, EnumMacMaskTransferTransition.MoveToCleanChGlass);
            MacTransition tMovingToCleanChGlass_ClampedInCleanChGlass = NewTransition(sMovingToCleanChGlass, sClampedInCleanChGlass, EnumMacMaskTransferTransition.WaitForMoveToCleanGlass);
            MacTransition tClampedInCleanChGlass_NULL = NewTransition(sClampedInCleanChGlass, null, EnumMacMaskTransferTransition.StandbyClampedInCleanChGlass);
            MacTransition tClampedInCleanChGlass_MovingToCleanGlass = NewTransition(sClampedInCleanChGlass, sMovingToCleanGlass, EnumMacMaskTransferTransition.MoveToCleanGlass);
            MacTransition tMovingToCleanGlass_NULL = NewTransition(sMovingToCleanGlass, null, EnumMacMaskTransferTransition.WaitFroCleanGlass);
            MacTransition tMovingToCleanGlass_CleaningGlass = NewTransition(sMovingToCleanGlass, sCleaningGlass, EnumMacMaskTransferTransition.CleanGlass);
            MacTransition tCleaningGlass_NULL = NewTransition(sCleaningGlass, null, EnumMacMaskTransferTransition.StandbyAtCleanGlass);
            MacTransition tCleaningGlass_MovingAfterCleanedGlass = NewTransition(sCleaningGlass, sMovingAfterCleanedGlass, EnumMacMaskTransferTransition.MoveAfterCleanedGlass);
            MacTransition tMovingAfterCleanedGlass_ClampedInCleanChGlass = NewTransition(sMovingAfterCleanedGlass, sClampedInCleanChGlass, EnumMacMaskTransferTransition.WaitForMoveToInspectGlass);
            MacTransition tClampedInCleanChGlass_MovingToInspectGlass = NewTransition(sClampedInCleanChGlass, sMovingToInspectGlass, EnumMacMaskTransferTransition.MoveToInspectGlass);
            MacTransition tMovingToInspectGlass_NULL = NewTransition(sMovingToInspectGlass, null, EnumMacMaskTransferTransition.WaitForInspectGlass);
            MacTransition tMovingToInspectGlass_InspectingGlass = NewTransition(sMovingToInspectGlass, sInspectingGlass, EnumMacMaskTransferTransition.InspectGlass);
            MacTransition tInspectingGlass_NULL = NewTransition(sInspectingGlass, null, EnumMacMaskTransferTransition.StandbyAtInspectGlass);
            MacTransition tInspectingGlass_MovingAfterInspectedGlass = NewTransition(sInspectingGlass, sMovingAfterInspectedGlass, EnumMacMaskTransferTransition.MoveAfterInspectedGlass);
            MacTransition tMovingAfterInspectedGlass_ClampedInCleanChGlass = NewTransition(sMovingAfterInspectedGlass, sClampedInCleanChGlass, EnumMacMaskTransferTransition.WaitForLeaveCleanChGlass);
            MacTransition tClampedInCleanChGlass_MovingToCCHomeClampedFromCleanChGlass = NewTransition(sClampedInCleanChGlass, sMovingToCCHomeClampedFromCleanChGlass, EnumMacMaskTransferTransition.MoveToCCHomeClampedFromCleanChGlass);
            MacTransition tMovingToCCHomeClampedFromCleanChGlass_CCHomeClamped = NewTransition(sMovingToCCHomeClampedFromCleanChGlass, sCCHomeClamped, EnumMacMaskTransferTransition.StandbyAtCCHomeClampedFromCleanChGlass);

            #endregion Clean Ch

            #region Open Stage
            MacTransition tLPHome_MovingToOpenStage = NewTransition(sLPHome, sMovingToOpenStage, EnumMacMaskTransferTransition.MoveToOpenStage);
            MacTransition tMovingToOpenStage_OpenStageClamping = NewTransition(sMovingToOpenStage, sOpenStageClamping, EnumMacMaskTransferTransition.ClampInOpenStage);
            MacTransition tOpenStageClamping_MovingToLPHomeClampedFromOpenStage = NewTransition(sOpenStageClamping, sMovingToLPHomeClampedFromOpenStage, EnumMacMaskTransferTransition.MoveToLPHomeClampedFromOpenStage);
            MacTransition tMovingToLPHomeClampedFromOpenStage_LPHomeClamped = NewTransition(sMovingToLPHomeClampedFromOpenStage, sLPHomeClamped, EnumMacMaskTransferTransition.StandbyAtLPHomeClampedFromOpenStage);

            MacTransition tLPHomeClamped_MovingToOpenStageForRelease = NewTransition(sLPHomeClamped, sMovingOpenStageForRelease, EnumMacMaskTransferTransition.MoveToOpenStageForRelease);
            MacTransition tLPHomeInspected_MovingToOpenStageForRelease = NewTransition(sLPHomeInspected, sMovingOpenStageForRelease, EnumMacMaskTransferTransition.MoveToOpenStageInspectedForRelease);
            MacTransition tLPHomeCleaned_MovingToOpenStageForRelease = NewTransition(sLPHomeCleaned, sMovingOpenStageForRelease, EnumMacMaskTransferTransition.MoveToOpenStageCleanedForRelease);
            MacTransition tMovingOpenStageForRelease_OpenStageReleasing = NewTransition(sMovingOpenStageForRelease, sOpenStageReleasing, EnumMacMaskTransferTransition.ReleaseInOpenStage);
            MacTransition tOpenStageReleasing_MovingToLPHomeFromOpenStage = NewTransition(sOpenStageReleasing, sMovingToLPHomeFromOpenStage, EnumMacMaskTransferTransition.CompleteReleased);
            MacTransition tMovingToLPHomeFromOpenStage_LPHome = NewTransition(sMovingToLPHomeFromOpenStage, sLPHome, EnumMacMaskTransferTransition.StandbyAtLPHomeFromOpenStage);
            #endregion Open Stage

            #region Barcode Reader
            MacTransition tLPHomeClamped_MovingToBarcodeReaderClamped = NewTransition(sLPHomeClamped, sMovingToBarcodeReaderClamped, EnumMacMaskTransferTransition.MoveToBarcodeReaderClamped);
            MacTransition tMovingToBarcodeReaderClamped_ReadingBarcode = NewTransition(sMovingToBarcodeReaderClamped, sReadingBarcode, EnumMacMaskTransferTransition.WaitForBarcodeReader);
            MacTransition tReadingBarcode_NULL = NewTransition(sReadingBarcode, null, EnumMacMaskTransferTransition.StandbyAtBarcodeReader);
            MacTransition tReadingBarcode_MovingToLPHomeClampedFromBarcodeReader = NewTransition(sReadingBarcode, sMovingToLPHomeClampedFromBarcodeReader, EnumMacMaskTransferTransition.MoveToLPHomeClampedFromBarcodeReader);
            MacTransition tMovingToLPHomeClampedFromBarcodeReader_LPHomeClamped = NewTransition(sMovingToLPHomeClampedFromBarcodeReader, sLPHomeClamped, EnumMacMaskTransferTransition.StandbyAtLPHomeClampedFromBarcodeReader);
            #endregion Barcode Reader

            #region Inspect Deform
            MacTransition tICHome_MovingToInspectDeformFromICHome = NewTransition(sICHome, sMovingToInspectDeformFromICHome, EnumMacMaskTransferTransition.MoveToInspectDeformFromICHome);
            MacTransition tMovingToInspectDeformFromICHome_InspectingClampDeform = NewTransition(sMovingToInspectDeformFromICHome, sInspectingClampDeform, EnumMacMaskTransferTransition.WaitForInspectDeform);
            MacTransition tInspectingClampDeform_NULL = NewTransition(sInspectingClampDeform, null, EnumMacMaskTransferTransition.StandbyAtInspectDeform);
            MacTransition tInspectingClampDeform_MovingToICHomeFromInspectDeform = NewTransition(sInspectingClampDeform, sMovingToICHomeFromInspectDeform, EnumMacMaskTransferTransition.MoveToICHomeFromInspectDeform);
            MacTransition tMovingToICHomeFromInspectDeform_ICHome = NewTransition(sMovingToICHomeFromInspectDeform, sICHome, EnumMacMaskTransferTransition.StandbyAtICHomeFromInspectDeform);
            #endregion Inspect Deform

            //Is Ready to Release
            //MacTransition tLPHomeClamped_ReadyToRelease = NewTransition(sLPHomeClamped, sReadyToRelease, EnumMacMaskTransferTransition.IsReady);


            //--- Clean Start



            //Complete or No Clean Job
            //MacTransition tReadyToRelease_MovingToLoadPortForRelease = NewTransition(sReadyToRelease, sMovingToLoadPortForRelease, EnumMacMaskTransferTransition.NoCleanJob);
            //MacTransition tReadyToRelease_MovingInspectionChForRelease = NewTransition(sReadyToRelease, sMovingInspectionChForRelease, EnumMacMaskTransferTransition.NoCleanJob);
            //MacTransition tReadyToRelease_MovingInspectionChGlassForRelease = NewTransition(sReadyToRelease, sMovingInspectionChGlassForRelease, EnumMacMaskTransferTransition.NoCleanJob);
            //MacTransition tReadyToRelease_MovingOpenStageForRelease = NewTransition(sReadyToRelease, sMovingOpenStageForRelease, EnumMacMaskTransferTransition.NoCleanJob);


            //Complete Move
            //MacTransition tMovingToLPHomeFromLoadPort_WaitAckHome = NewTransition(sMovingToLPHomeFromLoadPort, sWaitAckHome, EnumMacMaskTransferTransition.CleanMoveComplete);
            //MacTransition tMovingToICHomeFromInspectionCh_WaitAckHome = NewTransition(sMovingToICHomeFromInspectionCh, sWaitAckHome, EnumMacMaskTransferTransition.CleanMoveComplete);
            //MacTransition tMovingToICHomeFromInspectionChGlass_WaitAckHome = NewTransition(sMovingToICHomeFromInspectionChGlass, sWaitAckHome, EnumMacMaskTransferTransition.CleanMoveComplete);
            //MacTransition tMovingToLPHomeFromOpenStage_WaitAckHome = NewTransition(sMovingToLPHomeFromOpenStage, sWaitAckHome, EnumMacMaskTransferTransition.CleanMoveComplete);

            //MacTransition tWaitAckHome_LPHome = NewTransition(sWaitAckHome, sLPHome, EnumMacMaskTransferTransition.ReceiveAckHome);
            #endregion Transition

            #region State Register OnEntry OnExit
            MaskrobotTransferPathFile fileObj = new MaskrobotTransferPathFile(@"D:\Positions\MTRobot\");
            sStart.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();



                var transition = tStart_DeviceInitial;
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
            sStart.OnExit += (sender, e) =>
            { };

            sInitial.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.Initial();
                    HalMaskTransfer.Reset();
                }
                catch (Exception ex)
                {
                    throw new MaskTransferInitialFailException(ex.Message);
                }

                var transition = tDeviceInitial_LPHome;
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
            sInitial.OnExit += (sender, e) =>
            { };

            sLPHome.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                var transition = tLPHome_NULL;
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
            sLPHome.OnExit += (sender, e) =>
            { };

            sLPHomeClamped.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                var transition = tLPHomeClamped_NULL;
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
            sLPHomeClamped.OnExit += (sender, e) =>
            { };

            sLPHomeInspected.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                var transition = tLPHomeInspected_NULL;
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
            sLPHomeInspected.OnExit += (sender, e) =>
            { };

            sLPHomeCleaned.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                var transition = tLPHomeCleaned_NULL;
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
            sLPHomeCleaned.OnExit += (sender, e) =>
            { };

            sICHome.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                var transition = tICHome_NULL;
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
            sICHome.OnExit += (sender, e) =>
            { };

            sICHomeClamped.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                var transition = tICHomeClamped_NULL;
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
            sICHomeClamped.OnExit += (sender, e) =>
            { };

            sICHomeInspected.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                var transition = tICHomeInspected_NULL;
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
            sICHomeInspected.OnExit += (sender, e) =>
            { };

            sCCHomeClamped.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                var transition = tCCHomeClamped_NULL;
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
            sCCHomeClamped.OnExit += (sender, e) =>
            { };

            sCCHomeCleaned.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                var transition = tCCHomeCleaned_NULL;
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
            sCCHomeCleaned.OnExit += (sender, e) =>
            { };

            #region Change Direction
            sChangingDirectionToLPHome.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ChangeDirection(fileObj.LoadPortHomePathFile());
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tChangingDirectionToLPHome_LPHome;
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
            sChangingDirectionToLPHome.OnExit += (sender, e) =>
            { };

            sChangingDirectionToLPHomeClamped.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ChangeDirection(fileObj.LoadPortHomePathFile());
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tChangingDirectionToLPHomeClamped_LPHomeClamped;
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
            sChangingDirectionToLPHomeClamped.OnExit += (sender, e) =>
            { };

            sChangingDirectionToLPHomeInspected.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ChangeDirection(fileObj.LoadPortHomePathFile());
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tChangingDirectionToLPHomeInspected_LPHomeInspected;
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
            sChangingDirectionToLPHomeInspected.OnExit += (sender, e) =>
            { };

            sChangingDirectionToLPHomeCleaned.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ChangeDirection(fileObj.LoadPortHomePathFile());
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tChangingDirectionToLPHomeCleaned_LPHomeCleaned;
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
            sChangingDirectionToLPHomeCleaned.OnExit += (sender, e) =>
            { };

            sChangingDirectionToICHome.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ChangeDirection(fileObj.InspChHomePathFile());
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tChangingDirectionToICHome_ICHome;
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
            sChangingDirectionToICHome.OnExit += (sender, e) =>
            { };

            sChangingDirectionToICHomeClamped.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ChangeDirection(fileObj.InspChHomePathFile());
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tChangingDirectionToICHomeClamped_ICHomeClamped;
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
            sChangingDirectionToICHomeClamped.OnExit += (sender, e) =>
            { };

            sChangingDirectionToCCHomeClamped.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ChangeDirection(fileObj.CleanChHomePathFile());
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tChangingDirectionToCCHomeClamped_CCHomeClamped;
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
            sChangingDirectionToCCHomeClamped.OnExit += (sender, e) =>
            { };
            #endregion Change Direction

            #region Load PortA
            sMovingToLoadPortA.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(fileObj.FromLPHomeToLP1PathFile());
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToLoadPortA_LoadPortAClamping;
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
            sMovingToLoadPortA.OnExit += (sender, e) =>
            { };

            sLoadPortAClamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    var MaskType = (uint)e.Parameter;
                    HalMaskTransfer.Clamp(MaskType);
                    HalMaskTransfer.Clamp(MaskType);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tLoadPortAClamping_MovingToLPHomeClampedFromLoadPortA;
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
            sLoadPortAClamping.OnExit += (sender, e) =>
            { };

            sMovingToLPHomeClampedFromLoadPortA.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(fileObj.FromLP1ToLPHomePathFile());
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToLPHomeClampedFromLoadPortA_LPHomeClamped;
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
            sMovingToLPHomeClampedFromLoadPortA.OnExit += (sender, e) =>
            { };

            sMovingToLoadPortAForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(fileObj.FromLPHomeToLP1PathFile());
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToLoadPortAForRelease_LoadPortAReleasing;
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
            sMovingToLoadPortAForRelease.OnExit += (sender, e) =>
            { };

            sLoadPortAReleasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tLoadPortAReleasing_MovingToLPHomeFromLoadPortA;
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
            sLoadPortAReleasing.OnExit += (sender, e) =>
            { };

            sMovingToLPHomeFromLoadPortA.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(fileObj.FromLP1ToLPHomePathFile());
                    HalMaskTransfer.RobotMoving(true);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToLPHomeFromLoadPortA_LPHome;
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
            sMovingToLPHomeFromLoadPortA.OnExit += (sender, e) =>
            { };
            #endregion Load PortA

            #region Load PortB
            sMovingToLoadPortB.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(fileObj.FromLPHomeToLP2PathFile());
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToLoadPortB_LoadPortBClamping;
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
            sMovingToLoadPortB.OnExit += (sender, e) =>
            { };

            sLoadPortBClamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    var MaskType = (uint)e.Parameter;
                    HalMaskTransfer.Clamp(MaskType);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tLoadPortBClamping_MovingToLPHomeClampedFromLoadPortB;
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
            sLoadPortBClamping.OnExit += (sender, e) =>
            { };

            sMovingToLPHomeClampedFromLoadPortB.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(fileObj.FromLP2ToLPHomePathFile());
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToLPHomeClampedFromLoadPortB_LPHomeClamped;
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
            sMovingToLPHomeClampedFromLoadPortB.OnExit += (sender, e) =>
            { };

            sMovingToLoadPortBForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(fileObj.FromLPHomeToLP2PathFile());
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToLoadPortBForRelease_LoadPortBReleasing;
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
            sMovingToLoadPortBForRelease.OnExit += (sender, e) =>
            { };

            sLoadPortBReleasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tLoadPortBReleasing_MovingToLPHomeFromLoadPortB;
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
            sLoadPortBReleasing.OnExit += (sender, e) =>
            { };

            sMovingToLPHomeFromLoadPortB.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(fileObj.FromLP2ToLPHomePathFile());
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToLPHomeFromLoadPortB_LPHome;
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
            sMovingToLPHomeFromLoadPortB.OnExit += (sender, e) =>
            { };
            #endregion Load PortB

            #region Inspection Ch
            sMovingToInspectionCh.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalInspectionCh.ReadRobotIntrude(true))
                        throw new MaskTransferPathMoveFailException("Inspection Chamber not allowed to intrude !");
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(fileObj.FromICHomeToICBackSidePathFile());
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToInspectionCh_InspectionChClamping;
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
            sMovingToInspectionCh.OnExit += (sender, e) =>
            { };

            sInspectionChClamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(fileObj.FromICBackSideToICStagePathFile());
                    HalMaskTransfer.RobotMoving(false);
                    var MaskType = (uint)e.Parameter;
                    HalMaskTransfer.Clamp(MaskType);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tInspectionChClamping_MovingToICHomeClampedFromInspectionCh;
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
            sInspectionChClamping.OnExit += (sender, e) =>
            { };

            sMovingToICHomeClampedFromInspectionCh.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(fileObj.FromICStageToICBackSidePathFile());
                    HalMaskTransfer.ExePathMove(fileObj.FromICBackSideToICHomePathFile());
                    HalMaskTransfer.RobotMoving(false);
                    HalInspectionCh.ReadRobotIntrude(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToICHomeClampedFromInspectionCh_ICHomeClamped;
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
            sMovingToICHomeClampedFromInspectionCh.OnExit += (sender, e) =>
            { };

            sMovingToInspectionChForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalInspectionCh.ReadRobotIntrude(true))
                        throw new MaskTransferPathMoveFailException("Inspection Chamber not allowed to intrude !");
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(fileObj.FromICHomeToICBackSidePathFile());
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingInspectionChForRelease_InspectionChReleasing;
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
            sMovingToInspectionChForRelease.OnExit += (sender, e) =>
            { };

            sInspectionChReleasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(fileObj.FromICBackSideToICStagePathFile());
                    HalMaskTransfer.RobotMoving(false);
                    HalMaskTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tInspectionChReleasing_MovingToICHomeFromInspectionCh;
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
            sInspectionChReleasing.OnExit += (sender, e) =>
            { };

            sMovingToICHomeFromInspectionCh.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(fileObj.FromICStageToICBackSidePathFile());
                    HalMaskTransfer.ExePathMove(fileObj.FromICBackSideToICHomePathFile());
                    HalMaskTransfer.RobotMoving(false);
                    HalInspectionCh.ReadRobotIntrude(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToICHomeFromInspectionCh_ICHome;
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
            sMovingToICHomeFromInspectionCh.OnExit += (sender, e) =>
            { };



            sMovingToInspectionChGlass.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalInspectionCh.ReadRobotIntrude(true))
                        throw new MaskTransferPathMoveFailException("Inspection Chamber not allowed to intrude !");
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(fileObj.FromICHomeToICFrontSidePathFile());
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToInspectionChGlass_InspectionChGlassClamping;
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
            sMovingToInspectionChGlass.OnExit += (sender, e) =>
            { };

            sInspectionChGlassClamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(fileObj.FromICFrontSideToICStagePathFile());
                    HalMaskTransfer.RobotMoving(false);
                    var MaskType = (uint)e.Parameter;
                    HalMaskTransfer.Clamp(MaskType);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tInspectionChGlassClamping_MovingToICHomeClampedFromInspectionChGlass;
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
            sInspectionChGlassClamping.OnExit += (sender, e) =>
            { };

            sMovingToICHomeClampedFromInspectionChGlass.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(fileObj.FromICStageToICFrontSidePathFile());
                    HalMaskTransfer.ExePathMove(fileObj.FromICFrontSideToICHomePathFile());
                    HalMaskTransfer.RobotMoving(false);
                    HalInspectionCh.ReadRobotIntrude(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToICHomeClampedFromInspectionChGlass_ICHomeClamped;
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
            sMovingToICHomeClampedFromInspectionChGlass.OnExit += (sender, e) =>
            { };

            sMovingToInspectionChGlassForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalInspectionCh.ReadRobotIntrude(true))
                        throw new MaskTransferPathMoveFailException("Inspection Chamber not allowed to intrude !");
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(fileObj.FromICHomeToICFrontSidePathFile());
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingInspectionChGlassForRelease_InspectionChGlassReleasing;
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
            sMovingToInspectionChGlassForRelease.OnExit += (sender, e) =>
            { };

            sInspectionChGlassReleasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(fileObj.FromICFrontSideToICStagePathFile());
                    HalMaskTransfer.RobotMoving(false);
                    HalMaskTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tInspectionChGlassReleasing_MovingToICHomeFromInspectionChGlass;
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
            sInspectionChGlassReleasing.OnExit += (sender, e) =>
            { };

            sMovingToICHomeFromInspectionChGlass.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(fileObj.FromICStageToICFrontSidePathFile());
                    HalMaskTransfer.ExePathMove(fileObj.FromICFrontSideToICHomePathFile());
                    HalMaskTransfer.RobotMoving(false);
                    HalInspectionCh.ReadRobotIntrude(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToICHomeFromInspectionChGlass_ICHome;
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
            sMovingToICHomeFromInspectionChGlass.OnExit += (sender, e) =>
            { };
            #endregion Inspection Ch

            #region Clean Ch
            sMovingToCleanCh.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(fileObj.FromCCHomeToCCFrontSidePathFile());
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCleanCh_ClampedInCleanCh;
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
            sMovingToCleanCh.OnExit += (sender, e) =>
            { };

            sClampedInCleanCh.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {

                }
                catch (Exception ex)
                {
                    throw new MaskTransferException(ex.Message);
                }

                var transition = tClampedInCleanCh_NULL;
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
            sClampedInCleanCh.OnExit += (sender, e) =>
            { };

            sMovingToClean.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(fileObj.FromCCFrontSideToCleanPathFile());
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToClean_CleaningPellicle;
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
            sMovingToClean.OnExit += (sender, e) =>
            { };

            sCleaningPellicle.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {

                }
                catch (Exception ex)
                {
                    throw new MaskTransferException(ex.Message);
                }

                var transition = tCleaningPellicle_NULL;
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
            sCleaningPellicle.OnExit += (sender, e) =>
            { };

            sMovingAfterCleaned.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(fileObj.FromFrontSideCleanFinishToCCPathFile());
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingAfterCleaned_ClampedInCleanCh;
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
            sMovingAfterCleaned.OnExit += (sender, e) =>
            { };

            sMovingToInspect.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(fileObj.FromCCFrontSideToCapturePathFile());
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToInspect_InspectingPellicle;
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
            sMovingToInspect.OnExit += (sender, e) =>
            { };

            sInspectingPellicle.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {

                }
                catch (Exception ex)
                {
                    throw new MaskTransferException(ex.Message);
                }

                var transition = tInspectingPellicle_NULL;
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
            sInspectingPellicle.OnExit += (sender, e) =>
            { };

            sMovingAfterInspected.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(fileObj.FromFrontSideCaptureFinishToCCPathFile());
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingAfterInspected_ClampedInCleanCh;
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
            sMovingAfterInspected.OnExit += (sender, e) =>
            { };

            sMovingToCCHomeClampedFromCleanCh.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(fileObj.FromCCFrontSideToCCHomePathFile());
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCCHomeClampedFromCleanCh_CCHomeClamped;
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
            sMovingToCCHomeClampedFromCleanCh.OnExit += (sender, e) =>
            { };



            sMovingToCleanChGlass.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(fileObj.FromCCHomeToCCBackSidePathFile());
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCleanChGlass_ClampedInCleanChGlass;
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
            sMovingToCleanChGlass.OnExit += (sender, e) =>
            { };

            sClampedInCleanChGlass.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {

                }
                catch (Exception ex)
                {
                    throw new MaskTransferException(ex.Message);
                }

                var transition = tClampedInCleanChGlass_NULL;
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
            sClampedInCleanChGlass.OnExit += (sender, e) =>
            { };

            sMovingToCleanGlass.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(fileObj.FromCCBackSideToCleanPathFile());
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCleanGlass_CleaningGlass;
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
            sMovingToCleanGlass.OnExit += (sender, e) =>
            { };

            sCleaningGlass.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {

                }
                catch (Exception ex)
                {
                    throw new MaskTransferException(ex.Message);
                }

                var transition = tCleaningGlass_NULL;
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
            sCleaningGlass.OnExit += (sender, e) =>
            { };

            sMovingAfterCleanedGlass.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(fileObj.FromBackSideCleanFinishToCCPathFile());
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingAfterCleanedGlass_ClampedInCleanChGlass;
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
            sMovingAfterCleanedGlass.OnExit += (sender, e) =>
            { };

            sMovingToInspectGlass.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(fileObj.FromCCBackSideToCapturePathFile());
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToInspectGlass_InspectingGlass;
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
            sMovingToInspectGlass.OnExit += (sender, e) =>
            { };

            sInspectingGlass.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {

                }
                catch (Exception ex)
                {
                    throw new MaskTransferException(ex.Message);
                }

                var transition = tInspectingGlass_NULL;
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
            sInspectingGlass.OnExit += (sender, e) =>
            { };

            sMovingAfterInspectedGlass.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(fileObj.FromBackSideCaptureFinishToCCPathFile());
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingAfterInspectedGlass_ClampedInCleanChGlass;
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
            sMovingAfterInspectedGlass.OnExit += (sender, e) =>
            { };

            sMovingToCCHomeClampedFromCleanChGlass.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(fileObj.FromCCBackSideToCCHomePathFile());
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCCHomeClampedFromCleanChGlass_CCHomeClamped;
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
            sMovingToCCHomeClampedFromCleanChGlass.OnExit += (sender, e) =>
            { };
            #endregion Clean Ch

            #region OpenStage
            sMovingToOpenStage.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalOpenStage.ReadRobotIntrude(null, true);
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(fileObj.FromLPHomeToOSPathFile());
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToOpenStage_OpenStageClamping;
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
            sMovingToOpenStage.OnExit += (sender, e) =>
            { };

            sOpenStageClamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(fileObj.FromOSToOSStagePathFile());
                    HalMaskTransfer.RobotMoving(false);
                    var MaskType = (uint)e.Parameter;
                    HalMaskTransfer.Clamp(MaskType);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tOpenStageClamping_MovingToLPHomeClampedFromOpenStage;
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
            sOpenStageClamping.OnExit += (sender, e) =>
            { };

            sMovingToLPHomeClampedFromOpenStage.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(fileObj.FromOSStageToOSPathFile());
                    HalMaskTransfer.ExePathMove(fileObj.FromOSToLPHomePathFile());
                    HalMaskTransfer.RobotMoving(false);
                    HalOpenStage.ReadRobotIntrude(null, false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToLPHomeClampedFromOpenStage_LPHomeClamped;
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
            sMovingToLPHomeClampedFromOpenStage.OnExit += (sender, e) =>
            { };

            sMovingOpenStageForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalOpenStage.ReadRobotIntrude(null, true);
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(fileObj.FromLPHomeToOSPathFile());
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingOpenStageForRelease_OpenStageReleasing;
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
            sMovingOpenStageForRelease.OnExit += (sender, e) =>
            { };

            sOpenStageReleasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(fileObj.FromOSToOSStagePathFile());
                    HalMaskTransfer.RobotMoving(false);
                    HalMaskTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tOpenStageReleasing_MovingToLPHomeFromOpenStage;
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
            sOpenStageReleasing.OnExit += (sender, e) =>
            { };

            sMovingToLPHomeFromOpenStage.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(fileObj.FromOSStageToOSPathFile());
                    HalMaskTransfer.ExePathMove(fileObj.FromOSToLPHomePathFile());
                    HalMaskTransfer.RobotMoving(false);
                    HalOpenStage.ReadRobotIntrude(null, false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToLPHomeFromOpenStage_LPHome;
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
            sMovingToLPHomeFromOpenStage.OnExit += (sender, e) =>
            { };
            #endregion OpenStage

            #region Barcode Reader
            sMovingToBarcodeReaderClamped.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    if (!HalMaskTransfer.CheckPosition(@"D:\Positions\MTRobot\LoadPortHome.json"))
                        throw new MaskTransferPathMoveFailException("Robot position was not at Load Port Home,could not move to Barcode Reader !");
                    HalMaskTransfer.ExePathMove(fileObj.FromLPHomeToBarcodeReaderPathFile());
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToBarcodeReaderClamped_ReadingBarcode;
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
            sMovingToBarcodeReaderClamped.OnExit += (sender, e) =>
            { };

            sReadingBarcode.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try// TODO: 判斷Barcode Reader已經讀取完等待移走
                {

                }
                catch (Exception ex)
                {
                    throw new MaskTransferException(ex.Message);
                }

                var transition = tReadingBarcode_NULL;
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
            sReadingBarcode.OnExit += (sender, e) =>
            { };

            sMovingToLPHomeClampedFromBarcodeReader.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(fileObj.FromBarcodeReaderToLPHomePathFile());
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToLPHomeClampedFromBarcodeReader_LPHomeClamped;
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
            sMovingToLPHomeClampedFromBarcodeReader.OnExit += (sender, e) =>
            { };
            #endregion

            #region Inspect Deform
            sMovingToInspectDeformFromICHome.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    if (!HalMaskTransfer.CheckPosition(@"D:\Positions\MTRobot\InspChHome.json"))
                        throw new MaskTransferPathMoveFailException("Robot position was not at Inspection Chamber Home,could not move to Inspect Deform !");
                    HalMaskTransfer.ExePathMove(fileObj.FromICHomeToInspDeformPathFile());
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToInspectDeformFromICHome_InspectingClampDeform;
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
            sMovingToInspectDeformFromICHome.OnExit += (sender, e) =>
            { };

            sInspectingClampDeform.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try// TODO: 判斷Inspect Deform已經檢查完等待移走
                {

                }
                catch (Exception ex)
                {
                    throw new MaskTransferException(ex.Message);
                }

                var transition = tInspectingClampDeform_NULL;
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
            sInspectingClampDeform.OnExit += (sender, e) =>
            { };

            sMovingToICHomeFromInspectDeform.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalMaskTransfer.RobotMoving(true);
                    HalMaskTransfer.ExePathMove(fileObj.FromInspDeformToICHomePathFile());
                    HalMaskTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new MaskTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToICHomeFromInspectDeform_ICHome;
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
            sMovingToICHomeFromInspectDeform.OnExit += (sender, e) =>
            { };
            #endregion Inspect Deform

            #endregion State Register OnEntry OnExit
            //--- Exception Transition ---

        }

        private bool CheckEquipmentStatus()
        {
            string Result = null;
            if (HalUniversal.ReadPowerON() == false) Result += "Equipment is power off now, ";
            if (HalUniversal.ReadBCP_Maintenance()) Result += "Key lock in the electric control box is turn to maintenance, ";
            if (HalUniversal.ReadCB_Maintenance()) Result += "Outside key lock between cabinet_1 and cabinet_2 is turn to maintenance, ";
            if (HalUniversal.ReadBCP_EMO().Item1) Result += "EMO_1 has been trigger, ";
            if (HalUniversal.ReadBCP_EMO().Item2) Result += "EMO_2 has been trigger, ";
            if (HalUniversal.ReadBCP_EMO().Item3) Result += "EMO_3 has been trigger, ";
            if (HalUniversal.ReadBCP_EMO().Item4) Result += "EMO_4 has been trigger, ";
            if (HalUniversal.ReadBCP_EMO().Item5) Result += "EMO_5 has been trigger, ";
            if (HalUniversal.ReadCB_EMO().Item1) Result += "EMO_6 has been trigger, ";
            if (HalUniversal.ReadCB_EMO().Item2) Result += "EMO_7 has been trigger, ";
            if (HalUniversal.ReadCB_EMO().Item3) Result += "EMO_8 has been trigger, ";
            if (HalUniversal.ReadLP1_EMO()) Result += "Load Port_1 EMO has been trigger, ";
            if (HalUniversal.ReadLP2_EMO()) Result += "Load Port_2 EMO has been trigger, ";
            if (HalUniversal.ReadBCP_Door()) Result += "The door of electric control box has been open, ";
            if (HalUniversal.ReadLP1_Door()) Result += "The door of Load Port_1 has been open, ";
            if (HalUniversal.ReadLP2_Door()) Result += "The door of Load Pord_2 has been open, ";
            if (HalUniversal.ReadBCP_Smoke()) Result += "Smoke detected in the electric control box, ";

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
            var MTClampInsp_Alarm = HalUniversal.ReadAlarm_MTClampInsp();
            var MT_Alarm = HalUniversal.ReadAlarm_MTRobot();
            //var IC_Alarm = HalUniversal.ReadAlarm_InspCh();
            //var LP_Alarm = HalUniversal.ReadAlarm_LoadPort();
            //var OS_Alarm = HalUniversal.ReadAlarm_OpenStage();

            //if (CB_Alarm != "") throw new CabinetPLCAlarmException(CB_Alarm);
            //if (CC_Alarm != "") throw new CleanChPLCAlarmException(CC_Alarm);
            //if (CF_Alarm != "") throw new UniversalCoverFanPLCAlarmException(CF_Alarm);
            //if (BT_Alarm != "") throw new BoxTransferPLCAlarmException(BT_Alarm);
            if (MTClampInsp_Alarm != "") throw new MTClampInspectDeformPLCAlarmException(MTClampInsp_Alarm);
            if (MT_Alarm != "") throw new MaskTransferPLCAlarmException(MT_Alarm);
            //if (IC_Alarm != "") throw new InspectionChPLCAlarmException(IC_Alarm);
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
            var MTClampInsp_Warning = HalUniversal.ReadWarning_MTClampInsp();
            var MT_Warning = HalUniversal.ReadWarning_MTRobot();
            //var IC_Warning = HalUniversal.ReadWarning_InspCh();
            //var LP_Warning = HalUniversal.ReadWarning_LoadPort();
            //var OS_Warning = HalUniversal.ReadWarning_OpenStage();

            //if (CB_Warning != "") throw new CabinetPLCWarningException(CB_Warning);
            //if (CC_Warning != "") throw new CleanChPLCWarningException(CC_Warning);
            //if (CF_Warning != "") throw new UniversalCoverFanPLCWarningException(CF_Warning);
            //if (BT_Warning != "") throw new BoxTransferPLCWarningException(BT_Warning);
            if (MTClampInsp_Warning != "") throw new MTClampInspectDeformPLCWarningException(MTClampInsp_Warning);
            if (MT_Warning != "") throw new MaskTransferPLCWarningException(MT_Warning);
            //if (IC_Warning != "") throw new InspectionChPLCWarningException(IC_Warning);
            //if (LP_Warning != "") throw new LoadportPLCWarningException(LP_Warning);
            //if (OS_Warning != "") throw new OpenStagePLCWarningException(OS_Warning);

            return true;
        }

        public class MacMaskTransferUnitStateTimeOutController
        {
            const int defTimeOutSec = 20;
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
                return IsTimeOut(startTime, defTimeOutSec);
            }
        }



    }
}