#define NoConfig
using MaskAutoCleaner.v1_0.StateMachineBeta;
using MvAssistant.Mac.v1_0.Hal.CompLoadPort;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using MaskAutoCleaner.v1_0.StateMachineAlpha;
using MaskAutoCleaner.v1_0.StateMachineExceptions.LoadportStateMachineException;
using MvAssistant.Mac.v1_0.Hal.Assembly;

namespace MaskAutoCleaner.v1_0.Machine.LoadPort
{
    [Guid("B6CCEC0B-9042-4B88-A306-E29B87B6469C")]
    public class MacMsLoadPort : MacMachineStateBase
    {
#if NoConfig
        IMacHalLoadPortUnit HalLoadPort = null;
#endif
        private IMacHalLoadPort HalLoadPortUniversal { get { return this.halAssembly as IMacHalLoadPort; } }
        private IMacHalLoadPortUnit HalLoadPortunit { get { return this.halAssembly as IMacHalLoadPortUnit; } }

        public IMacHalLoadPortUnit HalLoadPortUnit
        {
#if NoConfig

            get
            {
              
             
                return HalLoadPort;
            }
#else
             get { return this.halAssembly as IMacHalLoadPortUnit; }
#endif

        }
        MacLoadPortUnitStateTimeOutController TimeController = new MacLoadPortUnitStateTimeOutController();

#if NoConfig
        public MacMsLoadPort()
        {
            //HalLoadPort = new MacHalGudengLoadPort();
            //HalLoadPort.HalConnect();
            LoadStateMachine();
        }
#endif
        public void TestLoadportInstance()
        {
            var s = HalLoadPortUnit;
            HalLoadPortUniversal.ReadPressureDiff();
            HalLoadPortunit.CommandAlarmReset();
        }

        #region  Command
        public void Reset()
        {
            var state = this.States[EnumMacMsLoadPortState.ResetStart.ToString()];
            state.DoEntry(new MacStateEntryEventArgs(null));
        }

        public void Inintial()
        {
            var state = this.States[EnumMacMsLoadPortState.InitialStart.ToString()];
            state.DoEntry(new MacStateEntryEventArgs(null));
        }


        public void Dock()
        {
            var state = this.States[EnumMacMsLoadPortState.DockStart.ToString()];
            state.DoEntry(new MacStateEntryEventArgs(null));
        }
         
        public void Undock()
        {

            var state = this.States[EnumMacMsLoadPortState.UndockStart.ToString()];
            state.DoEntry(new MacStateEntryEventArgs(null));
        }
        #endregion command

        public override void LoadStateMachine()
        {
            #region State

            // Reset
            MacState sResetStart = NewState(EnumMacMsLoadPortState.ResetStart);
            MacState sResetIng = NewState(EnumMacMsLoadPortState.ResetIng);
            MacState sResetComplete = NewState(EnumMacMsLoadPortState.ResetComplete);
          

            // Initial
            MacState sInitialStart = NewState(EnumMacMsLoadPortState.InitialStart);
            MacState sInitialIng = NewState(EnumMacMsLoadPortState.InitialIng);
            MacState sInitialComplete = NewState(EnumMacMsLoadPortState.InitialComplete);
            MacState sIdleForPutPOD = NewState(EnumMacMsLoadPortState.IdleForPutPOD);

            // dock
            MacState sDockStart = NewState(EnumMacMsLoadPortState.DockStart);
            MacState sDockIng= NewState(EnumMacMsLoadPortState.DockIng);
            MacState sDockComplete = NewState(EnumMacMsLoadPortState.DockComplete);
            // dock 等待Robot 夾取光置
            MacState sIdleForGetMask = NewState(EnumMacMsLoadPortState.IdleForGetMask);
            
            // undock
            MacState sUndockStart = NewState(EnumMacMsLoadPortState.UndockStart);
            MacState sUndockIng= NewState(EnumMacMsLoadPortState.UndockIng);
            MacState sUndockComplete = NewState(EnumMacMsLoadPortState.UndockComplete);
            // Undock 等待POD 被取走
            MacState sIdleForGetPOD = NewState(EnumMacMsLoadPortState.IdleForGetPOD);
            #endregion State


            #region Transition 
            // Reset
            MacTransition tResetStart_ResetIng = NewTransition(sResetStart, sResetIng, EnumMacMsLoadPortTransition.ResetStart_ResetIng);
            MacTransition tResetIng_ResetComplete = NewTransition(sResetIng, sResetComplete, EnumMacMsLoadPortTransition.ResetIng_ResetComplete);
            MacTransition tResetComplete_NULL = NewTransition(sResetComplete, null, EnumMacMsLoadPortTransition.ResetComplete_NULL);

            //Initial
            MacTransition tInitialStart_InitialIng = NewTransition(sInitialStart, sInitialIng, EnumMacMsLoadPortTransition.InitialStart_InitialIng);
            MacTransition tInitialIng_InitialComplete = NewTransition(sInitialIng, sInitialComplete, EnumMacMsLoadPortTransition.InitialIng_InitialComplete);
            MacTransition tInitialComplete_IdleForPutPOD= NewTransition(sInitialComplete, sIdleForPutPOD, EnumMacMsLoadPortTransition.InitialComplete_IdleForPutPOD);
            MacTransition tIdleForPutPOD_NULL = NewTransition(sIdleForPutPOD, null, EnumMacMsLoadPortTransition.IdleForPutPOD_NULL); 

            // Dock
            MacTransition tDockStart_DockIng = NewTransition(sDockStart, sDockIng, EnumMacMsLoadPortTransition.DockStart_DockIng);
            MacTransition tDockIng_DockComplete = NewTransition(sDockIng, sDockComplete, EnumMacMsLoadPortTransition.DockIng_DockComplete);
            MacTransition tDockComplete_IdleForGetMask= NewTransition(sDockComplete, sIdleForGetMask, EnumMacMsLoadPortTransition.DockComplete_IdleForGetMask);
            MacTransition tIdleForGetMask_NULL = NewTransition(sIdleForGetMask,null, EnumMacMsLoadPortTransition.IdleForGetMask_NULL);

            // Undock
            MacTransition tUndockStart_UndockIng = NewTransition(sUndockStart, sUndockIng, EnumMacMsLoadPortTransition.UndockStart_UndockIng);
            MacTransition tUndockIng_UndockComplete = NewTransition(sUndockIng, sUndockComplete, EnumMacMsLoadPortTransition.UndockIng_UndockComplete);
            MacTransition tUndockComplete_IdleForGetPOD= NewTransition(sUndockComplete, sIdleForGetPOD, EnumMacMsLoadPortTransition.UndockComplete_IdleForGetPOD);
            MacTransition tIdleForGetPOD_NULL = NewTransition(sIdleForGetPOD,null, EnumMacMsLoadPortTransition.IdleForGetPOD_NULL);
            #endregion


            #region  Register OnEntry, OnExit Event Handler

            sResetStart.OnEntry += (sender, e) =>
            {   // Sync
                var transition = tResetStart_ResetIng;
                var triggerMember = new TriggerMember
                {
                    Action = (parameter) => this.HalLoadPortUnit.CommandAlarmReset(),
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        //TODO: do something
                    },
                    Guard = () => true,
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sResetStart.OnExit += (sender, e) =>
            {
                // 視狀況新增 Code
            };

            sResetIng.OnEntry += (sender, e) => 
            {   // Async
                var transition = tResetIng_ResetComplete;
                var triggerMemberAsync = new TriggerMemberAsync
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // TODO: do something
                    },
                    Guard = (startTime) =>
                    {
                        if (this.HalLoadPortUnit.CurrentWorkState == LoadPortWorkState.ResetComplete)
                        {
                            return true;
                        }
                        else if (this.HalLoadPortUnit.CurrentWorkState == LoadPortWorkState.ResetFail)
                        {
                            throw new LoadportResetFailException();
                        }
                        else if (TimeController.IsTimeOut(startTime))
                        {
                            throw new LoadportResetTimeOutException();
                        }
                        else
                        {
                            return false;
                        }
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs=new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMemberAsync);
                TriggerAsync(   transition);
            };
            sResetIng.OnExit += (sender, e) => 
            {
                // 視實況新增 Code 
            };

            sResetComplete.OnEntry += (sender, e) => 
            {  // Sync
                var transition = tResetComplete_NULL;
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // TODO: do something
                    },
                    Guard = () => true,
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sResetComplete.OnExit += (sender, e) => 
            {
                // 視狀況增加 Code
            };

            sInitialStart.OnEntry += (sender, e) =>
            {  // Sync 
                var transition = tInitialStart_InitialIng;
                var triggerMember = new TriggerMember
                {
                    Action = (parameter) => this.HalLoadPortUnit.CommandInitialRequest(),
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // TODO: do something
                    },
                    Guard = () => true,
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                 };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sInitialStart.OnExit += (sender, e) =>
            {
                // 視狀況增加 Code
            };

            sInitialIng.OnEntry += (sender, e) =>
            {   // Async
                var transition = tInitialIng_InitialComplete;
                var triggerMemberAsync = new TriggerMemberAsync
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // TODO: do something
                    },
                    Guard = (startTime) =>
                    {
                        if (this.HalLoadPortUnit.CurrentWorkState == LoadPortWorkState.InitialComplete)
                        {
                            return true;
                        }
                        else if (HalLoadPortUnit.CurrentWorkState == LoadPortWorkState.MustResetFirst)
                        {
                            throw new LoadportInitialMustResetException();
                        }
                        else if(TimeController.IsTimeOut(startTime))
                        {
                            throw new LoadportInitialTimeOutException();
                        }
                        else
                        {
                            return false;
                        }
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMemberAsync);
                TriggerAsync(transition);
    
            };
            sInitialIng.OnExit += (sender, e) =>
            {
                // 視狀況新增 Code
            };

            sInitialComplete.OnEntry += (sender, e) =>
            {   // Sync
                var transition = tInitialComplete_IdleForPutPOD;
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // TODO: do something
                    },
                    Guard = () => true,
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sInitialComplete.OnExit += (sender, e) =>
            {
                // 視狀況增加Code
            };

          
            sIdleForPutPOD.OnEntry += (sender, e) =>
            {  // Sync
                var transition = tIdleForPutPOD_NULL;
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // TODO: do something
                    },
                    Guard = () => true,
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sIdleForPutPOD.OnExit += (sender, e) =>
            {
                // 依實際狀況 增加 Code
            };
          
            sDockStart.OnEntry += (sender, e) =>
            {   // Sync
                var transition = tDockStart_DockIng;
                var triggerMember = new TriggerMember
                {
                    Action = (parameter) => this.HalLoadPortUnit.CommandDockRequest(),
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // TODO: do something
                    },
                    Guard = () => true,
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                     
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sDockStart.OnExit += (sender, e) =>
            {
                // 視狀況增加 Code
            };

            sDockIng.OnEntry += (sender,e)=>
            {  // Async
                var transition = tDockIng_DockComplete;
                var triggerMemberAsync = new TriggerMemberAsync
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // TODO: dosomething
                    },
                    Guard = (startTime) =>
                    {
                        if(this.HalLoadPortUnit.CurrentWorkState == LoadPortWorkState.DockComplete)
                        {
                            return true;
                        }
                        else if (this.HalLoadPortUnit.CurrentWorkState == LoadPortWorkState.MustInitialFirst)
                        {
                            throw new LoadportDockMustInitialException();
                        }
                        else if (this.HalLoadPortUnit.CurrentWorkState == LoadPortWorkState.MustResetFirst)
                        {
                            throw new LoadportDockMustResetException();
                        }
                        else if (TimeController.IsTimeOut(startTime))
                        {
                            throw new LoadportDockTimeOutException();
                        }
                        else
                        {
                            return false;
                        }
                    },
                };
                transition.SetTriggerMembers(triggerMemberAsync);
                TriggerAsync(transition);
            };
            sDockIng.OnExit += (sender, e) =>
            {
                // 視狀況增加 Code
            };

            
            sDockComplete.OnEntry += (sender, e) =>
            {   // Sync 
                var transition = tDockComplete_IdleForGetMask;
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // TODO : do something
                    },
                    Guard = () => true,
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sDockComplete.OnExit+= (sender, e) =>
            {
                // 視狀況加 Code
            };

            sIdleForGetMask.OnEntry += (sender, e) =>
            {   // Sync
                var transition = tIdleForGetMask_NULL;
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // TODO: do something
                    },
                    Guard = () => true,
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sIdleForGetMask.OnExit += (sender, e) =>
            {
                // TODO: Depends on,......
            };


            sUndockStart.OnEntry += (sender, e) =>
            {  // Sync
                var transition = tUndockStart_UndockIng;
                var triggerMember = new TriggerMember
                {
                    Action = (parameter) => this.HalLoadPortUnit.CommandUndockRequest(),
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                      {
                          // TODO: do something
                      },
                    Guard = () => true,
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sUndockStart.OnExit += (sender, e) =>
            {
                // 視狀況增加 Code
            };

            sUndockIng.OnEntry += (sender, e) =>
            {
                // Async
                var transition = tUndockIng_UndockComplete;
                var triggerMemberAsync = new TriggerMemberAsync
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (State, ex) =>
                    {
                        // TODO: do something 
                    },
                    Guard = (startTime) =>
                    {
                        if (this.HalLoadPortUnit.CurrentWorkState == LoadPortWorkState.UndockComplete)
                        {
                            return true;
                        }
                        else if (this.HalLoadPortUnit.CurrentWorkState == LoadPortWorkState.MustInitialFirst)
                        {
                            throw new LoadportUndockMustInitialException();
                        }
                        else if (this.HalLoadPortUnit.CurrentWorkState == LoadPortWorkState.MustResetFirst)
                        {
                            throw new LoadportUndockMustResetException();
                        }
                        else if (TimeController.IsTimeOut(startTime))
                        {
                            throw new LoadportUndockTimeOutException();
                        }
                        else
                        {
                            return false;
                        }
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMemberAsync);
                TriggerAsync(transition);
            };
            sUndockIng.OnExit += (sender, e) =>
            {
               // 視狀況增加 Code
            };


            sUndockComplete.OnEntry += (sender, e) =>
            {  // Sync 
                var transition = tUndockComplete_IdleForGetPOD;
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // TODO: do something 
                    },
                    Guard = () => true,
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sUndockComplete.OnExit += (sender, e) =>
            {
                // 視狀況新增 Code
            };
          
            sIdleForGetPOD.OnEntry += (sender, e) =>
            {
                var transition = tIdleForGetPOD_NULL;
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // TODO: do something 
                    },
                    Guard = () => true,
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()

                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sIdleForGetPOD.OnExit += (sender, e) =>
            {
                // 視狀況新增 Code
            };
            #endregion


        }
    }
    public class MacLoadPortUnitStateTimeOutController
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
