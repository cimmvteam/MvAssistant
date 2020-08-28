using MaskAutoCleaner.v1_0.StateMachineBeta;
using MvAssistant.Mac.v1_0.Hal.CompDrawer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.CabinetDrawer
{
    [Guid("11111111-1111-1111-1111-111111111111")]// TODO: UPdate this Guid
    public class MacMsCabinetDrawer : MacMachineStateBase
    {
        public IMacHalDrawer HalDrawer { get { return this.halAssembly as IMacHalDrawer; } }
        private MacMsTimeOutController TimeoutObj;

        public EnumMacCabinetDrawerJob DrawerJob  { set; private get; }
        public void ResetDrawerJob()
        {
            SetDrawerJob(EnumMacCabinetDrawerJob.None);
        }
        public void SetDrawerJob(EnumMacCabinetDrawerJob job)
        {
            DrawerJob = job;
        }


        public override void LoadStateMachine()
        {
            TimeoutObj = new MacMsTimeOutController();
            ResetDrawerJob();

            #region State
            MacState sInitialStart = NewState(EnumMacCabinetDrawerState.InitialStart);
            MacState sInitialIng = NewState(EnumMacCabinetDrawerState.InitialIng);
            MacState sInitialComplete = NewState(EnumMacCabinetDrawerState.InitialComplete);
            
            MacState sMoveTrayToHomeStart= NewState(EnumMacCabinetDrawerState.MoveTrayToHomeStart);
            MacState sMoveTrayToHomeIng = NewState(EnumMacCabinetDrawerState.MoveTrayToHomeIng);
            MacState sMoveTrayToHomeComplete = NewState(EnumMacCabinetDrawerState.MoveTrayToInComplete);

            MacState sMoveTrayToOutStart = NewState(EnumMacCabinetDrawerState.MoveTrayToOutStart);
            MacState sMoveTrayToOutIng = NewState(EnumMacCabinetDrawerState.MoveTrayToInIng);
            MacState sMoveTrayToOutComplete = NewState(EnumMacCabinetDrawerState.MoveTrayToOutComplete);

            MacState sMoveTrayToInStart = NewState(EnumMacCabinetDrawerState.MoveTrayToInStart);
            MacState sMoveTrayToInIng = NewState(EnumMacCabinetDrawerState.MoveTrayToInIng);
            MacState sMoveTrayToInComplete = NewState(EnumMacCabinetDrawerState.MoveTrayToInComplete);
            #endregion State

            #region Transition
            MacTransition tInitialStart_InitialIng = NewTransition(sInitialStart, sInitialIng, EnumMacCabinetDrawerTransition.InitialStart_InitialIng);
            MacTransition tInitialIng_InitialComplete = NewTransition(sInitialIng,sInitialComplete, EnumMacCabinetDrawerTransition.InitialIng_InitialComplete);
            MacTransition tInitialComplete_NULL = NewTransition(sInitialComplete,null, EnumMacCabinetDrawerTransition.InitialComplete_NULL);

            MacTransition tMoveTrayToHomeStart_MoveTrayToHomeIng = NewTransition(sMoveTrayToHomeStart,sMoveTrayToHomeIng, EnumMacCabinetDrawerTransition.MoveTrayToHomeStart_MoveTrayToHomeIng);
            MacTransition tMoveTrayToHomeIng_MoveTrayToHomeComplete = NewTransition(sMoveTrayToHomeIng, sMoveTrayToHomeComplete, EnumMacCabinetDrawerTransition.MoveTrayToHomeIng_MoveTrayToHomeComplete);
            MacTransition tMoveTrayToHomeComplete_NULL = NewTransition(sMoveTrayToHomeComplete, null, EnumMacCabinetDrawerTransition.MoveTrayToInComplete_NULL);

            MacTransition tMoveTrayToOutStart_MoveTrayToOutIng = NewTransition(sMoveTrayToOutStart, sMoveTrayToOutIng, EnumMacCabinetDrawerTransition.MoveTrayToOutStart_MoveTrayToOutIng);
            MacTransition tMoveTrayToOutIng_MoveTrayToOutComplete = NewTransition(sMoveTrayToOutIng, sMoveTrayToOutComplete, EnumMacCabinetDrawerTransition.MoveTrayToOutIng_MoveTrayToOutComplete);
            MacTransition tMoveTrayToOutComplete_NULL = NewTransition(sMoveTrayToOutComplete, null, EnumMacCabinetDrawerTransition.MoveTrayToOutComplete_NULL);

            MacTransition tMoveTrayToInStart_MoveTrayToInIng = NewTransition(sMoveTrayToInStart, sMoveTrayToInIng, EnumMacCabinetDrawerTransition.MoveTrayToInStart_MoveTrayToInIng);
            MacTransition tMoveTrayToInIng_MoveTrayToInComplete = NewTransition(sMoveTrayToInIng, sMoveTrayToInComplete, EnumMacCabinetDrawerTransition.MoveTrayToInIng_MoveTrayToInComplete);
            MacTransition tMoveTrayToInComplete_NULL = NewTransition(sMoveTrayToInComplete, null, EnumMacCabinetDrawerTransition.MoveTrayToInComplete_NULL);
            #endregion Transition

            #region Event
            #region Initial

             
            sInitialStart.OnEntry += (sender, e) =>
            {  // Sync
                var transition = tInitialStart_InitialIng;
                // TODO: Servey triggerMember
                var triggerMember = new TriggerMember
                {
                    Guard = () => true,
                    Action = (parameter) =>
                    {
                        this.HalDrawer.CommandINI();
                    },
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) => 
                    {  // do something 
                    },
                     NextStateEntryEventArgs=new MacStateEntryEventArgs(),
                     ThisStateExitEventArgs=new MacStateExitEventArgs(),
                     NotGuardException=null,
                };
                transition.SetTriggerMembers(triggerMember);
                this.Trigger(transition);
            };
            sInitialStart.OnExit += (sender, e) =>
            {   

            };

            sInitialIng.OnEntry += (sender, e) =>
            {  // Async
                var transition = tInitialIng_InitialComplete;
                // TODO: Servey triggerMemberAsync
                var triggerMemberAsync = new TriggerMemberAsync
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    { // do something
                    },
                    Guard = (startTime) =>
                    {
                        bool rtnV = false;
                        return rtnV;
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMemberAsync);
                this.TriggerAsync(transition);

            };
            sInitialIng.OnExit += (sender, e) =>
            {

            };

            sInitialComplete.OnEntry += (sender, e) =>
            { // Sync
                var transition = tInitialComplete_NULL;
                // TODO: Servey triggerMember
                var triggerMember = new TriggerMember
                {
                    Guard = () => true,
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                this.Trigger(transition);

            };
            sInitialComplete.OnExit += (sender, e) =>
            {

            };
            #endregion Initial

            #region MoveTrayToHome
            sMoveTrayToHomeStart.OnEntry += (sender, e) =>
            {  // Sync
                var transition = tMoveTrayToHomeStart_MoveTrayToHomeIng;
                // TODO:  Servey triggerMember
                var triggerMember = new TriggerMember
                {
                    Action = (parameter) => { HalDrawer.CommandTrayMotionHome(); },
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {

                    },
                    Guard = () => true,
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMoveTrayToHomeStart.OnExit += (sender, e) =>
            {

            };

            sMoveTrayToHomeIng.OnEntry += (sender, e) =>
            { // Async
                var transition = tMoveTrayToHomeIng_MoveTrayToHomeComplete;
                // TODO: Servey triggerMemberAsync
                var triggerMemberAsync = new TriggerMemberAsync
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {

                    },
                    Guard = (startTime) =>
                    {
                        var rtnV = false;
                        return rtnV;
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMemberAsync);
                TriggerAsync(transition);
            };
            sMoveTrayToHomeIng.OnExit += (sender, e) =>
            {

            };

            sMoveTrayToHomeComplete.OnEntry += (sender, e) =>
            {// Sync
                var transition = tMoveTrayToHomeComplete_NULL;
                // TODO: Servey triggerMember
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {

                    },
                    Guard = () => true,
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMoveTrayToHomeComplete.OnExit += (sender, e) =>
            {

            };
            #endregion MoveTrayToHome


            #region MoveTrayToOut
            sMoveTrayToOutStart.OnEntry += (sender, e) =>
            { // Sync
                var transition = tMoveTrayToOutStart_MoveTrayToOutIng;
                // TODO: servey triggerMember
                var triggerMember = new TriggerMember
                {
                    Action = (parameter) =>
                      { HalDrawer.CommandTrayMotionOut(); },
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something
                    },
                    Guard = () => true,
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = null
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMoveTrayToOutStart.OnExit += (sender, e) =>
            {   
            };

            sMoveTrayToOutIng.OnEntry += (sender, e) =>
            {   // Async
                var transition = tMoveTrayToOutStart_MoveTrayToOutIng;
                // TODO: servey  triggerMemberAsync
                var triggerMemberAsync = new TriggerMemberAsync
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something
                    },
                    Guard = (startTime) =>
                    {
                        bool rtnV = false;
                        return rtnV;
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMemberAsync);
                TriggerAsync(transition);
            };
            sMoveTrayToOutIng.OnExit += (sender, e) =>
            {

            };

            sMoveTrayToOutComplete.OnEntry += (sender, e) =>
            {    // Sync
                var transition = tMoveTrayToHomeComplete_NULL;
                // TODO: servey triggerMember
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {// do something
                    },
                    Guard = () => true,
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                TriggerAsync(transition);
            };
            sMoveTrayToOutComplete.OnExit += (sender, e) =>
            {

            };
            #endregion MoveTrayToOut


            #region MoveTrayToIn
            sMoveTrayToInStart.OnEntry += (sender, e) =>
            { // Sync,
                var transition = tMoveTrayToInStart_MoveTrayToInIng;
                // TODO: servey triggerMember
                var triggerMember = new TriggerMember
                {
                    Action = (parameter) =>
                    {
                        HalDrawer.CommandTrayMotionIn();
                    },
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something
                    },
                    Guard = () => true,
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMoveTrayToInStart.OnExit += (sender, e) =>
            {

            };

            sMoveTrayToInIng.OnEntry += (sender, e) =>
            { // Async
                var transition = tMoveTrayToInIng_MoveTrayToInComplete;
                // TODO: Servey triggerMemberAsync
                var triggerMemberAsync = new TriggerMemberAsync
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something
                    },
                    Guard = (startTime) =>
                      {
                          bool rtnV = false;
                          return rtnV;
                      },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMemberAsync);
                TriggerAsync(transition);
            };
            sMoveTrayToInIng.OnExit += (sender, e) =>
            {

            };

            sMoveTrayToInComplete.OnEntry += (sender, e) =>
            {// Sync
                var transition = tMoveTrayToInComplete_NULL;
                // TODO: servey triggerMember
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something,
                    },
                    Guard = () => true,
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);

            };
            sMoveTrayToInComplete.OnExit += (sender, e) =>
            {

            };
            #endregion MoveTrayToIn
            #endregion Event
        }
    }


}
