using MaskAutoCleaner.v1_0.StateMachineBeta;
using MaskAutoCleaner.v1_0.StateMachineExceptions.DrawerStateMachineException;
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
        private MacState _currentState = null;
        public IMacHalDrawer HalDrawer { get { return this.halAssembly as IMacHalDrawer; } }
        private void ResetState()
        {
            SetCurrentState(this.States[EnumMacCabinetDrawerState.SystemBootup.ToString()]);
        }

        private void SetCurrentState(MacState state) 
        {
            _currentState = state;
        }
        public MacState CutrrentState { get { return _currentState; }  }


        public override void LoadStateMachine()
        {
            #region  state
            MacState sSystemBootup = NewState(EnumMacCabinetDrawerState.SystemBootup);
            //--
            MacState sSystemBootupInitialStart = NewState(EnumMacCabinetDrawerState.SystemBootupInitialStart);
            MacState sSystemBootupInitialIng = NewState(EnumMacCabinetDrawerState.SystemBootupInitialIng);
            MacState sSystemBootupInitialComplete = NewState(EnumMacCabinetDrawerState.SystemBootupInitialComplete);
            //--
            MacState sWaitingLoadInstruction = NewState(EnumMacCabinetDrawerState.WaitingLoadInstruction);
            //--
            MacState sLoadMoveTrayToOutStart =NewState(EnumMacCabinetDrawerState.LoadMoveTrayToOutStart);
            MacState sLoadMoveTrayToOutIng = NewState(EnumMacCabinetDrawerState.LoadMoveTrayToOutIng);
            MacState sLoadMoveTrayToOutComplete = NewState(EnumMacCabinetDrawerState.LoadMoveTrayToOutComplete);
            MacState sLoadWaitingPutBoxOnTray = NewState(EnumMacCabinetDrawerState.LoadWaitingPutBoxOnTray);
            //--
            MacState sLoadMoveTrayToHomeStart = NewState(EnumMacCabinetDrawerState.LoadMoveTrayToHomeStart);
            MacState sLoadMoveTrayToHomeIng = NewState(EnumMacCabinetDrawerState.LoadMoveTrayToHomeIng);
            MacState sLoadMoveTrayToHomeComplete = NewState(EnumMacCabinetDrawerState.LoadMoveTrayToHomeComplete);
            MacState sLoadCheckBoxExistenceStart = NewState(EnumMacCabinetDrawerState.LoadCheckBoxExistenceStart);
            MacState sLoadCheckBoxExistenceIng = NewState(EnumMacCabinetDrawerState.LoadCheckBoxExistenceIng);
            MacState sLoadCheckBoxExistenceComplete = NewState(EnumMacCabinetDrawerState.LoadCheckBoxExistenceComplete);

            MacState sLoadWaitingMoveTrayToIn = NewState(EnumMacCabinetDrawerState.LoadWaitingMoveTrayToIn);

            MacState sLoadRejectToOutStart = NewState(EnumMacCabinetDrawerState.LoadRejectToOutStart);
            MacState sLoadRejectToOutIng = NewState(EnumMacCabinetDrawerState.LoadRejectToOutIng);
            MacState sLoadRejectToOutComplete = NewState(EnumMacCabinetDrawerState.LoadRejectToOutComplete);

            #endregion state

            #region transition
            MacTransition tSystemBootup_NULL = NewTransition(sSystemBootup,null,EnumMacCabinetDrawerTransition.SystemBootup_NULL);
            //--
            MacTransition tSystemBootupInitialStart_SystemBootupInitialIng = NewTransition(sSystemBootupInitialStart, sSystemBootupInitialIng, EnumMacCabinetDrawerTransition.SystemBootupInitialStart_SystemBootupInitialIng);
            MacTransition tSystemBootupInitialIng_SystemBootupInitialComplete = NewTransition(sSystemBootupInitialIng, sSystemBootupInitialComplete, EnumMacCabinetDrawerTransition.SystemBootupInitialIng_SystemBootupInitialComplete);
            MacTransition tSystemBootupInitialComplete_WaitingLoadInstruction = NewTransition(sSystemBootupInitialComplete, sWaitingLoadInstruction, EnumMacCabinetDrawerTransition.SystemBootupInitialComplete_WaitingLoadInstruction);
            MacTransition tWaitingLoadInstruction_NULL = NewTransition(sWaitingLoadInstruction,null, EnumMacCabinetDrawerTransition.WaitingLoadInstruction_NULL);
            //--
            MacTransition tLoadMoveTrayToOutStart_LoadMoveTrayToOutIng = NewTransition(sLoadMoveTrayToOutStart, sLoadMoveTrayToOutIng, EnumMacCabinetDrawerTransition.LoadMoveTrayToOutStart_LoadMoveTrayToOutIng);
            MacTransition tLoadMoveTrayToOutIng_LoadMoveTrayToOutComplete = NewTransition(sLoadMoveTrayToOutIng, sLoadMoveTrayToOutComplete, EnumMacCabinetDrawerTransition.LoadMoveTrayToOutIng_LoadMoveTrayToOutComplete);
            MacTransition tLoadMoveTrayToOutComplete_LoadWaitingPutBoxOnTray = NewTransition(sLoadMoveTrayToOutComplete, sLoadWaitingPutBoxOnTray, EnumMacCabinetDrawerTransition.LoadMoveTrayToOutComplete_LoadWaitingPutBoxOnTray);
            MacTransition tLoadWaitingPutBoxOnTray_NULL = NewTransition(sLoadWaitingPutBoxOnTray, null, EnumMacCabinetDrawerTransition.LoadWaitingPutBoxOnTray_NULL);
            //--
            MacTransition tLoadMoveTrayToHomeStart_LoadMoveTrayToHomeIng= NewTransition(sLoadMoveTrayToHomeStart, sLoadMoveTrayToHomeIng, EnumMacCabinetDrawerTransition.LoadMoveTrayToHomeStart_LoadMoveTrayToHomeIng);
            MacTransition tLoadMoveTrayToHomeIng_LoadMoveTrayToHomeComplete = NewTransition(sLoadMoveTrayToHomeIng,sLoadMoveTrayToHomeComplete, EnumMacCabinetDrawerTransition.LoadMoveTrayToHomeIng_LoadMoveTrayToHomeComplete);
            MacTransition tLoadMoveTrayToHomeComplete_LoadCheckBoxExistenceStart = NewTransition(sLoadMoveTrayToHomeComplete,sLoadCheckBoxExistenceStart, EnumMacCabinetDrawerTransition.LoadMoveTrayToHomeComplete_LoadCheckBoxExistenceStart);
            MacTransition tLoadCheckBoxExistenceStart_LoadCheckBoxExistenceIng = NewTransition(sLoadCheckBoxExistenceStart,sLoadCheckBoxExistenceIng, EnumMacCabinetDrawerTransition.LoadCheckBoxExistenceStart_LoadCheckBoxExistenceIng);
            MacTransition tLoadCheckBoxExistenceIng_LoadCheckBoxExistenceComplete = NewTransition(sLoadCheckBoxExistenceIng,sLoadCheckBoxExistenceComplete, EnumMacCabinetDrawerTransition.LoadCheckBoxExistenceIng_LoadCheckBoxExistenceComplete);

            MacTransition tLoadCheckBoxExistenceComplete_LoadWaitingMoveTrayToIn = NewTransition(sLoadCheckBoxExistenceComplete,sLoadWaitingMoveTrayToIn, EnumMacCabinetDrawerTransition.LoadCheckBoxExistenceComplete_LoadWaitingMoveTrayToIn);

            MacTransition tLoadCheckBoxExistenceComplete_LoadRejectToOutStart = NewTransition(sLoadCheckBoxExistenceComplete,sLoadRejectToOutStart, EnumMacCabinetDrawerTransition.LoadCheckBoxExistenceComplete_LoadRejectToOutStart);
            MacTransition tLoadRejectToOutStart_LoadRejectToOutIng = NewTransition(sLoadRejectToOutStart,sLoadRejectToOutIng, EnumMacCabinetDrawerTransition.LoadRejectToOutStart_LoadRejectToOutIng);
            MacTransition tLoadRejectToOutIng_LoadRejectToOutComplete = NewTransition(sLoadRejectToOutIng,sLoadRejectToOutComplete, EnumMacCabinetDrawerTransition.LoadRejectToOutIng_LoadRejectToOutComplete );

            //

            #endregion transition

            #region event
            sSystemBootup.OnEntry += (sender, e) =>
            {  // Sync
               this.SetCurrentState((MacState)sender);
                var transition = tSystemBootup_NULL;
                var triggerMember = new TriggerMember
                {
                    Action = null,
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
            sSystemBootup.OnExit += (sender, e) =>
            {

            };

            sSystemBootupInitialStart.OnEntry += (sender, e) =>
             { // Sync
                 this.SetCurrentState((MacState)sender);
                 var transition = tSystemBootupInitialStart_SystemBootupInitialIng;
                 var triggerMember = new TriggerMember
                 {
                     Action = (parameter) =>
                     { this.HalDrawer.CommandINI(); },
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
                 this.Trigger(transition);
             };
            sSystemBootupInitialStart.OnExit += (sender, e) =>
            {

            };

            sSystemBootupInitialIng.OnEntry += (sender, e) =>
            {  // Async
                MacMsTimeOutController timeoutObject = new MacMsTimeOutController();
                this.SetCurrentState((MacState)sender);
                var transition = tSystemBootupInitialIng_SystemBootupInitialComplete;
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
                        var rtnV = false;
                        if (this.HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtHome)
                        {
                            rtnV = true;
                        }
                        else if(this.HalDrawer.CurrentWorkState == DrawerWorkState.InitialFailed)
                        {
                            throw new DrawerInitialFailException();
                        }
                        else if (timeoutObject.IsTimeOut(startTime))
                        {
                            throw new DrawerInitialTimeOutException();
                        }
                        return rtnV;
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMemberAsync);
                this.TriggerAsync(transition);
            };
            sSystemBootupInitialIng.OnExit += (sender, e) =>
            {

            };

            sSystemBootupInitialComplete.OnEntry += (sender, e) =>
            { // Sync
                this.SetCurrentState((MacState)sender);
                var transition = tSystemBootupInitialComplete_WaitingLoadInstruction;
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        //  do something
                    },
                    Guard = () => true,
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                this.Trigger(transition);
            };
            sSystemBootupInitialComplete.OnExit += (sender, e) =>
            {

            };

            sWaitingLoadInstruction.OnEntry += (sender, e) =>
              { //Sync
                  this.SetCurrentState((MacState)sender);
                  var transition = tWaitingLoadInstruction_NULL;
                  var triggerMember = new TriggerMember
                  {
                      Action = null,
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
                  this.Trigger(transition); 
              };
            sWaitingLoadInstruction.OnExit += (sender, e) =>
            {

            };

            sLoadMoveTrayToOutStart.OnEntry+=(sender, e)=>
            {   // Sync
                var transition = tLoadMoveTrayToOutStart_LoadMoveTrayToOutIng;
                this.SetCurrentState((MacState)sender);
                var triggerMember = new TriggerMember
                {
                    Action = (parameter) => { this.HalDrawer.CommandTrayMotionOut(); },
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something
                    },
                    Guard = () => true,
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sLoadMoveTrayToOutStart.OnExit += (sender, e) =>
            {

            };

            sLoadMoveTrayToOutIng.OnEntry += (sender, e) =>
            {// Async
                var transition = tLoadMoveTrayToOutIng_LoadMoveTrayToOutComplete;
                this.SetCurrentState((MacState)sender);
                MacMsTimeOutController timeoutObject = new MacMsTimeOutController();
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
                        if(HalDrawer.CurrentWorkState== DrawerWorkState.TrayArriveAtPositionOut)
                        {
                            rtnV = true;
                        }
                        else if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                        {
                            throw new DrawerLoadMoveTrayToPositionOutFailException();
                        }
                        else if (timeoutObject.IsTimeOut(startTime))
                        {
                            throw new DrawerLoadMoveTrayToPositionOutTimeOutException();
                        }
                        return rtnV;
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMemberAsync);
                Trigger(transition);
            };

            sLoadMoveTrayToOutComplete.OnEntry += (sender, e) =>
            {// Sync
                var transition = tLoadMoveTrayToOutComplete_LoadWaitingPutBoxOnTray;
                SetCurrentState((MacState)sender);
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                      {
                           // do something
                       },
                    Guard = () => true,
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sLoadMoveTrayToOutComplete.OnExit += (sender, e) =>
            {

            };

            sLoadWaitingPutBoxOnTray.OnEntry += (sender, e) =>
            {  // Sync
                SetCurrentState((MacState)sender);
                var transition = tLoadWaitingPutBoxOnTray_NULL;
                var triggerMember = new TriggerMember
                {
                    Action = null,
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
            sLoadWaitingPutBoxOnTray.OnExit += (sender, e) =>
            {

            };

            sLoadMoveTrayToHomeStart.OnEntry += (sender, e) =>
            { // Sync
                SetCurrentState((MacState)sender);
                var transition = tLoadMoveTrayToHomeStart_LoadMoveTrayToHomeIng;
                var triggerMember = new TriggerMember
                {
                    Action = (parameter) => { this.HalDrawer.CommandTrayMotionHome(); },
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
            sLoadMoveTrayToHomeStart.OnExit += (sender, e) =>
            {

            };

            sLoadMoveTrayToHomeIng.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);
                var transition = tLoadMoveTrayToHomeIng_LoadMoveTrayToHomeComplete;
                MacMsTimeOutController timeoutObject = new MacMsTimeOutController();
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
                        var rtnV = false;
                        if(HalDrawer.CurrentWorkState == DrawerWorkState.TrayArriveAtHome)
                        {
                            rtnV = true;
                        }
                        else if (HalDrawer.CurrentWorkState == DrawerWorkState.TrayMotionFailed)
                        {
                            throw new DrawerLoadMoveTrayToPositionHomeFailException();
                        }
                        else if(timeoutObject.IsTimeOut(startTime))
                        {
                            throw new DrawerLoadMoveTrayToPositionHomeTimeOutException();
                        }
                        return rtnV;
                    },
                };
            };
            sLoadMoveTrayToHomeIng.OnExit += (sender, e) =>
            {

            };

            sLoadMoveTrayToHomeComplete.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);
                var transition = tLoadMoveTrayToHomeComplete_LoadCheckBoxExistenceStart;
                var triggerMember = new TriggerMember
                {
                    Action = null,
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
            sLoadMoveTrayToHomeComplete.OnExit += (sender, e) =>
            {

            };


            #endregion event

        }
    }
    /**
   public class MacMsCabinetDrawer : MacMachineStateBase
   { 
       
       #region Instruction

       public void StartupInitialCabinetDrawer()
       {
          
       }


       /// <summary>初始化 Cabinet Drawer</summary>
       /// <param name="initialType"></param>
       public void InitialCabinetDrawer()
       {
           //this.States[EnumMacCabinetDrawerState.InitialStart.ToString()].DoEntry(new MacStateEntryEventArgs(initialType));
       }
       #endregion Instruction

       public IMacHalDrawer HalDrawer { get { return this.halAssembly as IMacHalDrawer; } }
       private MacMsTimeOutController TimeoutObj;
    
       public MacState GetState(EnumMacCabinetDrawerState state)
       {
           var rtnV = this.States[state.ToString()];
           return rtnV;
       }

       /// <summary>這個 Machine State 的索引</summary>
       public string CabinetDrawerIndex
       {
           get
           {
               if (HalDrawer == null)
               {
                   return null;
               }
               else
               {
                   return HalDrawer.DeviceIndex;
               }
           }
       }

     


       public override void LoadStateMachine()
       {
           TimeoutObj = new MacMsTimeOutController();
        

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
                    NextStateEntryEventArgs=new MacStateEntryEventArgs(e),
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
                   NextStateEntryEventArgs = new MacStateEntryEventArgs(e),
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
                   Action =(parameter)=>
                   {
                      
                   },
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
   */

    /**
             /// <summary>Cabinet Drawer Initial 的時機 </summary>
     public enum CabinetDrawerInitialType
     {
         /// <summary>系統啟動後的 Initial</summary>
         SystemBootupInitial,
         /// <summary>抽換 Drawer 後的 Initial</summary>
         SwapCabinetDrawerInitial,
         /// <summary>除上述各項外任意時間點的 Initial</summary>
         NormalInitial,
         
     }
     
     */
}
