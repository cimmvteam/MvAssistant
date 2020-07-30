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

namespace MaskAutoCleaner.v1_0.Machine.LoadPort
{
    [Guid("B6CCEC0B-9042-4B88-A306-E29B87B6469C")]
    public class MacMsLoadPort : MacMachineStateBase
    {
        public IMacHalLoadPortUnit HalLoadPortUnit { get { return this.halAssembly as IMacHalLoadPortUnit; } }


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
            MacState sResetFail=NewState(EnumMacMsLoadPortState.ResetFail);
            MacState sResetTimeOut = NewState(EnumMacMsLoadPortState.ResetTimeOut);

            // Initial
            MacState sInitialStart = NewState(EnumMacMsLoadPortState.InitialStart);
            MacState sInitialing = NewState(EnumMacMsLoadPortState.Initialing);
            MacState sInitialComplete = NewState(EnumMacMsLoadPortState.InitialComplete);
            MacState sInitialTimeOut = NewState(EnumMacMsLoadPortState.InitialTimeOut);
            MacState sInitialMustReset= NewState(EnumMacMsLoadPortState.InitialMustReset);
            MacState sIdleForPutPOD = NewState(EnumMacMsLoadPortState.IdleForPutPOD);

            // Dock Idle
            // TODO: Dock Idle state

            // dock
            MacState sDockStart = NewState(EnumMacMsLoadPortState.DockStart);
            MacState sDockIng= NewState(EnumMacMsLoadPortState.DockIng);
            MacState sDockMustReset= NewState(EnumMacMsLoadPortState.DockMustReset);
            MacState sDockMustInitial = NewState(EnumMacMsLoadPortState.DockMustInitial);
            MacState sDockComplete = NewState(EnumMacMsLoadPortState.DockComplete);
            MacState sDockTimeOut = NewState(EnumMacMsLoadPortState.DockTimeOut);

            // dock 等待Robot 夾取光置
            MacState sIdleForGetMask = NewState(EnumMacMsLoadPortState.IdleForGetMask);
            
            // undock
            MacState sUndockStart = NewState(EnumMacMsLoadPortState.UndockStart);
            MacState sUndockIng= NewState(EnumMacMsLoadPortState.UndockIng);
            MacState sUndockMustReset = NewState(EnumMacMsLoadPortState.UndockMustReset);
            MacState sUndockMustInitial = NewState(EnumMacMsLoadPortState.UndockMustInitial);
            MacState sUndockComplete = NewState(EnumMacMsLoadPortState.UndockComplete);
            MacState sUndockTimeOut = NewState(EnumMacMsLoadPortState.UndockTimeOut);

            // Undock 等待POD 被取走
            MacState sIdleForGetPOD = NewState(EnumMacMsLoadPortState.IdleForGetPOD);


            #endregion State


            #region Transition 
            // Reset
            MacTransition tResetStart_ResetIng = NewTransition(sResetStart, sResetIng, EnumMacMsLoadPortTransition.ResetStart_ResetIng);
            MacTransition tResetIng_ResetComplete = NewTransition(sResetIng, sResetComplete, EnumMacMsLoadPortTransition.ResetIng_ResetComplete);
            MacTransition tResetIng_ResetFail = NewTransition(sResetIng, sResetFail, EnumMacMsLoadPortTransition.ResetIng_ResetFail);
            MacTransition tResetIng_ResetTimeOut = NewTransition(sResetIng, sResetTimeOut, EnumMacMsLoadPortTransition.ResetIng_ResetTimeOut);


            //Initial
            MacTransition tInitialStart_Initialing = NewTransition(sInitialStart, sInitialing, EnumMacMsLoadPortTransition.InitialStart_Initialing);
            MacTransition tInitialIng_InitialComplete = NewTransition(sInitialing, sInitialComplete, EnumMacMsLoadPortTransition.InitialIng_InitialComplete);
            MacTransition tInitialIng_InitialTimeOut = NewTransition(sInitialing, sInitialTimeOut, EnumMacMsLoadPortTransition.InitialIng_InitialTimeOut);
            MacTransition tInitialIng_InitialMustReset = NewTransition(sInitialing, sInitialMustReset, EnumMacMsLoadPortTransition.InitialIng_InitialMustReset);
            MacTransition tInitialComplete_IdleForPutPOD= NewTransition(sInitialComplete, sIdleForPutPOD, EnumMacMsLoadPortTransition.InitialComplete_IdleForPutPOD);

            // Dock Idle
            // TODO: Dock Idle Transitions

            // Dock
            MacTransition tDockStart_DockIng = NewTransition(sDockStart, sDockIng, EnumMacMsLoadPortTransition.DockStart_DockIng);
            MacTransition tDockIng_DockComplete = NewTransition(sDockIng, sDockComplete, EnumMacMsLoadPortTransition.DockIng_DockComplete);
            MacTransition tDockIng_DockTimeOut = NewTransition(sDockIng, sDockTimeOut, EnumMacMsLoadPortTransition.DockIng_DockTimeOut);
            MacTransition tDockIng_DockMustReset = NewTransition(sDockIng, sDockMustReset, EnumMacMsLoadPortTransition.DockIng_DockMustReset);
            MacTransition tDockIng_DockMustInitial = NewTransition(sDockIng, sDockMustInitial, EnumMacMsLoadPortTransition.DockIng_DockMustInitial);
            MacTransition tDockComplete_IdleForGetMask= NewTransition(sDockComplete, sIdleForGetMask, EnumMacMsLoadPortTransition.DockComplete_IdleForGetMask);

            // Undock Idle
            // TODO: Undock Idel Transitions

            // Undock
            MacTransition tUndockStart_UndockIng = NewTransition(sUndockStart, sUndockIng, EnumMacMsLoadPortTransition.UndockStart_UndockIng);
            MacTransition tUndockIng_UndockComplete = NewTransition(sUndockIng, sUndockComplete, EnumMacMsLoadPortTransition.UndockIng_UndockComplete);
            MacTransition tUndockIng_UndockMustInitial = NewTransition(sUndockIng, sUndockMustInitial, EnumMacMsLoadPortTransition.UndockIng_UndockMustInitial);
            MacTransition tUndockIng_UndockTimeOut = NewTransition(sUndockIng, sUndockTimeOut, EnumMacMsLoadPortTransition.UndockIng_UndockTimeOut);
            MacTransition tUndockIng_UndockMustReset = NewTransition(sUndockIng, sUndockMustReset, EnumMacMsLoadPortTransition.UndockIng_UndockMustReset);
            MacTransition tUndockComplete_IdleForGetPOD= NewTransition(sUndockComplete, sIdleForGetPOD, EnumMacMsLoadPortTransition.UndockComplete_IdleForGetPOD);

            #endregion


            #region  Register OnEntry, OnExit Event Handler

            sResetStart.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                this.HalLoadPortUnit.CommandAlarmReset();
                state.DoExit(new MacStateExitEventArgs());
            };
            sResetStart.OnExit += (sender, e) =>
            {
                //var transition = this.Transitions[EnumMacMsLoadPortTransition.ResetStart_ResetIng.ToString()];
                var nextState = tResetStart_ResetIng.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            };

            sResetIng.OnEntry += (sender, e) => 
            {
                var state = (MacState)sender;
                var startTime = DateTime.Now;
               // var dicTransition = this.Transitions;
               
                Action guard = () =>
                {
                    while (true)
                    {
                        MacTransition transition = null;
                        if (this.HalLoadPortUnit.CurrentWorkState == LoadPortWorkState.ResetComplete)
                        { // 成功
                            transition = tResetIng_ResetComplete;// dicTransition[EnumMacMsLoadPortTransition.ResetIng_ResetComplete.ToString()];
;                        }
                        else if(this.HalLoadPortUnit.CurrentWorkState==LoadPortWorkState.ResetFail)
                        { // 失敗
                            transition = tResetIng_ResetFail;// dicTransition[EnumMacMsLoadPortTransition.ResetIng_ResetFail.ToString()];
                        }
                        else if(new MacLoadPortUnitStateTimeOutController().IsTimeOut(startTime))
                        { // 逾時
                            transition = tResetIng_ResetTimeOut;// dicTransition[EnumMacMsLoadPortTransition.ResetIng_ResetTimeOut.ToString()];
                        }
                        if(transition!=null)
                        {
                            var eventArgs = new MacStateExitWithTransitionEventArgs(transition);
                            state.DoExit(eventArgs);
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sResetIng.OnExit += (sender, e) => 
            {
                var args = (MacStateExitWithTransitionEventArgs)e;
               // MacTransition transition = args.Transition;
                var nextState = args.Transition.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            };

            sResetComplete.OnEntry += (sender, e) => 
            {
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sResetComplete.OnExit += (sender, e) => 
            {
                // Reset Complete, Terminal State
            };

            sResetFail.OnEntry +=(sender,e)=> 
            {
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sResetFail.OnExit +=(sender,e)=> 
            {
                // Reset Fail, Terminal State
            };

            sResetTimeOut.OnEntry += (sender, e) => 
            {
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sResetTimeOut.OnExit += (sender,e)=>
            {
                // Reset Time Out, Terminal State
            };

            sInitialStart.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                this.HalLoadPortUnit.CommandInitialRequest();
                state.DoExit(new MacStateExitEventArgs());
            };
            sInitialStart.OnExit += (sender, e) =>
            {
                //MacTransition transition = this.Transitions[EnumMacMsLoadPortTransition.InitialStart_Initialing.ToString()];
                var nextState = tInitialStart_Initialing.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            };

            sInitialing.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                DateTime startTime = DateTime.Now;
              //  var dicTransition = this.Transitions;
                Action guard = () =>
                {
                    while (true)
                    {
                        MacTransition transition = null;
                        if (this.HalLoadPortUnit.CurrentWorkState == LoadPortWorkState.InitialComplete)
                        {
                            transition = tInitialIng_InitialComplete;// dicTransition[EnumMacMsLoadPortTransition.InitialIng_InitialComplete.ToString()];
                        }
                        else if (HalLoadPortUnit.CurrentWorkState ==LoadPortWorkState.MustResetFirst)
                        {
                            // 必須先Reset
                            transition = tInitialIng_InitialMustReset;//dicTransition[EnumMacMsLoadPortTransition.InitialIng_InitialMustReset.ToString()];
                        }
                        else if (new MacLoadPortUnitStateTimeOutController().IsTimeOut(startTime))
                        {
                            // 逾時(因為没有回傳)
                            transition = tInitialIng_InitialTimeOut;// dicTransition[EnumMacMsLoadPortTransition.InitialIng_InitialTimeOut.ToString()];
                        }
                        if(transition != null)
                        {
                            var eventArgs = new MacStateExitWithTransitionEventArgs(transition);
                            state.DoExit(eventArgs);
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sInitialing.OnExit += (sender, e) =>
            {
                var args = (MacStateExitWithTransitionEventArgs)e;
               // MacTransition transition = args.Transition;
                var nextState = args.Transition.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            };

            sInitialComplete.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sInitialComplete.OnExit += (sender, e) =>
            {
                var nextState = tInitialComplete_IdleForPutPOD.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            };

            sIdleForPutPOD.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sIdleForPutPOD.OnExit += (sender, e) =>
            {
                // TODO: Depends On,......
            };

            sInitialTimeOut.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sInitialTimeOut.OnExit += (sender, e) =>
            {
                // Terminal State of Initial  TimeOut 
            };

            sInitialMustReset.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sInitialMustReset.OnExit += (sender,e)=>
            {
                // Terminal State of Initial TimeOut
                // TODO: 確定是否去做 Reset ?
            };
           
            sDockStart.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                this.HalLoadPortUnit.CommandDockRequest();
                state.DoExit(new MacStateExitEventArgs()); 
            };
            sDockStart.OnExit += (sender, e) =>
            {
                //var transition = this.Transitions[EnumMacMsLoadPortTransition.DockStart_DockIng.ToString()];
                var nextState = tDockStart_DockIng.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            };

            sDockIng.OnEntry += (sender,e)=>
            {
                var state = (MacState)sender;
                var startTime = DateTime.Now;
               // var dicTransition = this.Transitions;
                Action guard = () =>
                {
                    while (true)
                    {
                        MacTransition transition = null;
                        if (this.HalLoadPortUnit.CurrentWorkState == LoadPortWorkState.DockComplete)
                        {
                            transition = tDockIng_DockComplete;// dicTransition[EnumMacMsLoadPortTransition.DockIng_DockComplete.ToString()];
                        }
                        else if (this.HalLoadPortUnit.CurrentWorkState == LoadPortWorkState.MustInitialFirst)
                        {
                            transition = tDockIng_DockMustInitial;// dicTransition[EnumMacMsLoadPortTransition.DockIng_DockMustInitial.ToString()];
                        }
                        else if(this.HalLoadPortUnit.CurrentWorkState == LoadPortWorkState.MustResetFirst)
                        {
                            transition = tDockIng_DockMustReset;//dicTransition[EnumMacMsLoadPortTransition.DockIng_DockMustReset.ToString()];
                        }
                        else if(new MacLoadPortUnitStateTimeOutController().IsTimeOut(startTime))
                        {
                            transition = tDockIng_DockTimeOut;// dicTransition[EnumMacMsLoadPortTransition.DockIng_DockTimeOut.ToString()];
                        }
                        if(transition !=null)
                        {
                            var eventArgs = new MacStateExitWithTransitionEventArgs(transition);
                            state.DoExit(eventArgs);
                            break;
                        }
                        Thread.Sleep(10);
                    }
                    
                };
                new Task(guard).Start();
            };
            sDockIng.OnExit += (sender, e) =>
            {
               var args = (MacStateExitWithTransitionEventArgs)e;
                var nextState = args.Transition.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            };

            
            sDockComplete.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sDockComplete.OnExit+= (sender, e) =>
            {
                var nextState = tDockComplete_IdleForGetMask.StateTo;
                nextState.DoExit(new MacStateExitEventArgs());
            };

            sIdleForGetMask.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sIdleForGetMask.OnExit += (sender, e) =>
            {
                // TODO: Depends on,......
            };

            sDockMustInitial.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sDockMustInitial.OnExit += (sender, e) =>
            {
                // Terminal State of Dock Must Initial first 
            };

            sDockMustReset.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sDockMustReset.OnExit += (sender, e) =>
            {
                // Terminal State of Dock Must Reset first 
            };

            sDockTimeOut.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sDockTimeOut.OnExit += (sender, e) =>
            {
                // Terminal State of Dock Time out 
            };



            sUndockStart.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                this.HalLoadPortUnit.CommandUndockRequest();
                state.DoExit(new MacStateExitEventArgs());
            };
            sUndockStart.OnExit += (sender, e) =>
            {
                //var transition =  this.Transitions[EnumMacMsLoadPortTransition.DockStart_DockIng.ToString()];
                var nextState = tDockStart_DockIng.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            };

            sUndockIng.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                DateTime startTime = DateTime.Now;
              //  var dicTransitions = this.Transitions;
                Action guard = () =>
                {
                    while (true)
                    {
                        MacTransition transition = null;
                        if (this.HalLoadPortUnit.CurrentWorkState == LoadPortWorkState.UndockComplete)
                        {
                            transition = tUndockIng_UndockComplete;// dicTransitions[EnumMacMsLoadPortTransition.UndockIng_UndockComplete.ToString()];
                           
                        }
                        else if (this.HalLoadPortUnit.CurrentWorkState == LoadPortWorkState.MustInitialFirst)
                        {
                            transition = tUndockIng_UndockMustInitial;// dicTransitions[EnumMacMsLoadPortTransition.UndockIng_UndockMustInitial.ToString()];
                        }
                        else if (this.HalLoadPortUnit.CurrentWorkState == LoadPortWorkState.MustResetFirst)
                        {
                            transition = tUndockIng_UndockMustReset;// dicTransitions[EnumMacMsLoadPortTransition.UndockIng_UndockMustReset.ToString()];
                        }
                        else if (new MacLoadPortUnitStateTimeOutController().IsTimeOut(startTime)) 
                        {
                            transition = tUndockIng_UndockTimeOut;// dicTransitions[EnumMacMsLoadPortTransition.UndockIng_UndockTimeOut.ToString()];
                        }
                        if(transition != null)
                        {
                            var eventArgs = new MacStateExitWithTransitionEventArgs(transition);
                            state.DoExit(eventArgs);
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
            };
            sUndockIng.OnExit += (sender, e) =>
            {
                var args = (MacStateExitWithTransitionEventArgs)e;
                var nextState = args.Transition.StateTo;
                nextState.DoEntry(new MacStateEntryEventArgs(null));
            };

            sUndockTimeOut.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sUndockTimeOut.OnExit += (sender, e) =>
            {
                // Terminal State of undock Must Initial First
            };

            sUndockMustReset.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sUndockMustReset.OnExit += (sender, e) =>
            {
                // Terminal State of undock Must Reset First
            };

            sUndockMustInitial.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sUndockMustInitial.OnExit += (sender, e) =>
            {
                // Terminal State of undock Must Initial First
            };

            sUndockComplete.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
            };
            sUndockComplete.OnExit += (sender, e) =>
            {
                var nextState = tUndockComplete_IdleForGetPOD.StateTo;
                nextState.DoExit(new MacStateExitEventArgs());
            };
            sIdleForGetPOD.OnExit += (sender, e) =>
            {
                // TODO: Depends on,......
            };
            sIdleForGetPOD.OnEntry += (sender, e) =>
            {
                var state = (MacState)sender;
                state.DoExit(new MacStateExitEventArgs());
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
