using MaskAutoCleaner.v1_0.Machine.CabinetDrawer;
using MaskAutoCleaner.v1_0.Machine.Drawer;
using MaskAutoCleaner.v1_0.StateMachineBeta;
using MaskAutoCleaner.v1_0.StateMachineExceptions;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Hal.CompDrawer;
using MvAssistant.Mac.v1_0.JSon.RobotTransferFile;
using MvAssistant.Mac.v1_0.Manifest;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.Cabinet
{
    [Guid("11111111-1111-1111-1111-111111111111")]// TODO: UPdate this Guid
    public class MacMsCabinet : MacMachineStateBase
    {

       Dictionary <BoxrobotTransferLocation,IMacHalDrawer> dicMacHalDrawers = null;
       private readonly static object GetDrawerLockObj = new object();
        /// <summary>取得所有的 Drawer 與 BoxrobotTransferLocation 的  Dictionary</summary>
        /// <returns></returns>
        public Dictionary<BoxrobotTransferLocation,IMacHalDrawer> GetDicMacHalDrawers()
        {
               if (dicMacHalDrawers == null)
                {
                    lock (GetDrawerLockObj)
                    {
                        if (dicMacHalDrawers == null)
                        {
                            dicMacHalDrawers = new Dictionary<BoxrobotTransferLocation, IMacHalDrawer>();
                            var drawerIdRange = MacEnumDevice.boxtransfer_assembly.GetDrawerRange();
                            for (var i = drawerIdRange.StartID; i <= drawerIdRange.EndID; i++)
                            {
                               try
                               {
                                //var drawer = (this.halAssembly.Hals[i.ToString()] as IMacHalCabinet).MacHalDrawer as IMacHalDrawer;
                                var drawer = this.halAssembly.Hals[i.ToString()] as IMacHalDrawer;
                                var drawerLocation = i.ToBoxrobotTransferLocation();
                                   dicMacHalDrawers.Add(drawerLocation, drawer);
                                }
                               catch(Exception ex)
                               {

                               }
                            }
                            BindDrawerEventHandler();
                        }
                    }
                }
                return dicMacHalDrawers;
        }

        /// <summary>取得指定的 State Machine</summary>
        /// <param name="machineId"></param>
        /// <param name="dicStateMachines"></param>
        /// <returns></returns>
        public static MacMsCabinetDrawer GetMacMsCabinetDrawer(EnumMachineID machineId, Dictionary<EnumMachineID, MacMsCabinetDrawer> dicStateMachines)
        {
            if (dicStateMachines == null) { return null; }
            var rtn = dicStateMachines[machineId];
            return rtn;
        }

        public List<MacState> LoadState = null;


        #region State Machine Command
        public Dictionary<EnumMachineID, MacMsCabinetDrawer> DicCabinetDrawerStateMachines = new Dictionary<EnumMachineID, MacMsCabinetDrawer>();
        public override void SystemBootup()
        {
            
            Debug.WriteLine("Command: SystemBootup");
            var transition = this.Transitions[EnumMacCabinetTransition.Start_NULL.ToString()];
            var state = transition.StateFrom;
            state.ExecuteCommandAtEntry(new MacStateEntryEventArgs(null));
        }

        /// <summary>load</summary>
        /// <param name="targetDrawerQuantity"> Drawer 數量</param>
        /// <param name="dicStateMachines">所有Drawer 的集合</param>
        public void Load_Drawers(int targetDrawerQuantity,Dictionary<EnumMachineID, MacMsCabinetDrawer> dicStateMachines)
        {

            var states = dicStateMachines.Values.Where(m => m.CanLoad()).ToList();
            if (states.Count==0)
            {
                // 
            }
            else if (states.Count> targetDrawerQuantity)
            {
                states = states.Take(targetDrawerQuantity).ToList();
            }
            if(CurrentState != null)
            {
                CurrentState.DoExit(null);
            }
            var transition = this.Transitions[EnumMacCabinetTransition.LoadMoveDrawerTraysToOutStart_LoadMoveDrawerTraysToOutIng.ToString()];
            transition.StateFrom.ExecuteCommandAtEntry(new MacStateEntryEventArgs(null));
           
        }


        /// <summary>系統啟動之後 的 Initial</summary>
        /// <param name="dicCabinetDrawerStateMachines">Cabinet 之下  Drawer State Machine 的數量</param>
        public void BootupInitialDrawers(Dictionary<EnumMachineID, MacMsCabinetDrawer> dicCabinetDrawerStateMachines)
        {
            var cabinetDrawerStateMachines = dicCabinetDrawerStateMachines.Values.ToList();
            if (cabinetDrawerStateMachines.Count == 0)
            { }
            else
            {
                this.States[EnumMacCabinetState.BootupInitialDrawersStart.ToString()].ExecuteCommand(new CabinetSystemUpInitialMacStateEntryEventArgs(cabinetDrawerStateMachines));
            }
        }

        public void SynchrousDrawerStates(Dictionary<EnumMachineID, MacMsCabinetDrawer> dicStateMachines)
        {
            #region //[???] 不必檢查狀態
            #endregion
            var states = dicStateMachines.Values.ToList();
            if (states.Count == 0)
            { }
            else
            {
                this.States[EnumMacCabinetState.SynchronousDrawerStatesStart.ToString()].ExecuteCommand(new  CabinetSynchronousDrawerStatesMacStateEntryEventArgs(states));
            }
        }


        #endregion

        /// <summary>綁定 事件程序</summary>
        /// <remarks>
        /// <para>目前只綁定必要的</para>
        /// </remarks>
        void BindDrawerEventHandler()
        {
            EventHandler drawerINIOKHandler  = (sender, e)=>
            {

            };
            EventHandler drawerINIFailedHandler = (sender, e) =>
            {

            };
            EventHandler drawerTrayArriveHomeHandler = (sender, e) =>
            {

            };
            EventHandler drawerTrayArriveInHandler = (sender, e) =>
            {

            };
            EventHandler drawerTrayArriveOutHandler = (sender, e) =>
            {

            };

            EventHandler drawerOnSysyStartUpHandler = (sender, e) =>
            {

            };

            EventHandler drawerOnButtonEventHandler = (sender, e) =>
            {

            };
            foreach (var ele in dicMacHalDrawers)
            {
                var drawer = ele.Value;
                drawer.OnINIOKHandler -= drawerINIOKHandler;
                drawer.OnINIOKHandler += drawerINIOKHandler;

                drawer.OnINIFailedHandler -= drawerINIFailedHandler;
                drawer.OnINIFailedHandler += drawerINIFailedHandler;

                drawer.OnTrayArriveHomeHandler -= drawerTrayArriveHomeHandler;
                drawer.OnTrayArriveHomeHandler += drawerTrayArriveHomeHandler;

                drawer.OnTrayArriveOutHandler -= drawerTrayArriveOutHandler;
                drawer.OnTrayArriveOutHandler += drawerTrayArriveOutHandler;

                drawer.OnTrayArriveInHandler -= drawerTrayArriveInHandler;
                drawer.OnTrayArriveInHandler += drawerTrayArriveInHandler;

                drawer.OnSysStartUpHandler -= drawerOnSysyStartUpHandler;
                drawer.OnSysStartUpHandler += drawerOnSysyStartUpHandler;

                drawer.OnButtonEventHandler -= drawerOnButtonEventHandler;
                drawer.OnButtonEventHandler += drawerOnButtonEventHandler;

                drawer.OnDetectedEmptyBoxHandler -= null;
                drawer.OnDetectedEmptyBoxHandler += null;

                drawer.OnDetectedHasBoxHandler -= null;
                drawer.OnDetectedHasBoxHandler += null;

                drawer.OnTrayMotionFailedHandler -= null;
                drawer.OnTrayMotionFailedHandler += null;

                drawer.OnERRORErrorHandler -= null;
                drawer.OnERRORErrorHandler -= null;

                drawer.OnERRORREcoveryHandler -= null;
                drawer.OnERRORREcoveryHandler += null;

                drawer.OnPositionStatusHandler -= null;
                drawer.OnPositionStatusHandler += null;

                drawer.OnTrayMotionFailedHandler -= null;
                drawer.OnTrayMotionFailedHandler += null;

                drawer.OnTrayMotioningHandler -= null;
                drawer.OnTrayMotioningHandler += null;

                drawer.OnTrayMotionOKHandler -= null;
                drawer.OnTrayMotionOKHandler += null;

                drawer.OnTrayMotionSensorOFFHandler -= null;
                drawer.OnTrayMotionSensorOFFHandler += null;

                

            }
        }
        

        public MacMsCabinet()
        {
            //_dicCabinetDrawerStates = new Dictionary<string, MacMsCabinetDrawer>();
            LoadStateMachine();
        }

       
       

        public override void LoadStateMachine()
        {

            #region state
            MacState sStart= NewState(EnumMacCabinetState.Start);


            MacState sLoadMoveDrawerTraysToOutStart = NewState(EnumMacCabinetState.LoadMoveDrawerTraysToOutStart);
            MacState sLoadMoveDrawerTraysToOutIng = NewState(EnumMacCabinetState.LoadMoveDrawerTraysToOutIng);
            MacState sLoadMoveDrawerTraysToOutComplete = NewState(EnumMacCabinetState.LoadMoveDrawerTraysToOutComplete);

            MacState sBootupInitialDrawersStart = NewState(EnumMacCabinetState.BootupInitialDrawersStart);
            MacState sBootupInitialDrawersIng = NewState(EnumMacCabinetState.BootupInitialDrawersIng);
            MacState sBootupInitialDrawersComplete = NewState(EnumMacCabinetState.BootupInitialDrawersComplete);

            MacState sSynchronousDrawerStatesStart = NewState(EnumMacCabinetState.SynchronousDrawerStatesStart);
            MacState sSynchronousDrawerStatesIng = NewState(EnumMacCabinetState.SynchronousDrawerStatesIng);
            MacState sSynchronousDrawerStatesComplete = NewState(EnumMacCabinetState.SynchronousDrawerStatesComplete);
            #endregion state

            #region transition
           
            MacTransition tStart_NULL= NewTransition(sStart, null,  EnumMacCabinetTransition.Start_NULL);

            MacTransition tLoadMoveDrawerTraysToOutStart_LoadMoveDrawerTraysToOutIng = NewTransition(sLoadMoveDrawerTraysToOutStart,sLoadMoveDrawerTraysToOutIng,
                                                                                                                 EnumMacCabinetTransition.LoadMoveDrawerTraysToOutStart_LoadMoveDrawerTraysToOutIng);
            MacTransition tLoadMoveDrawerTraysToOutIng_LoadMoveDrawerTraysToOutComplete = NewTransition(sLoadMoveDrawerTraysToOutIng, sLoadMoveDrawerTraysToOutComplete,
                                                                                                                 EnumMacCabinetTransition.LoadMoveDrawerTraysToOutIng_LoadMoveDrawerTraysToOutComplete);
            MacTransition tLoadMoveDrawerTraysToOutComplete_NULL = NewTransition(sLoadMoveDrawerTraysToOutComplete, null,
                                                                                                               EnumMacCabinetTransition.LoadMoveDrawerTraysToOutComplete_NULL);
            
            MacTransition tBootupInitialDrawersStart_BootupInitialDrawersIng = NewTransition(sBootupInitialDrawersStart,sBootupInitialDrawersIng,
                                                                                                                EnumMacCabinetTransition.BootupInitialDrawersStart_BootupInitialDrawersIng);
            MacTransition tBootupInitialDrawersIng_BootupInitialDrawersComplete = NewTransition(sBootupInitialDrawersIng, sBootupInitialDrawersComplete,
                                                                                                              EnumMacCabinetTransition.BootupInitialDrawersIng_BootupInitialDrawersComplete);
            MacTransition tBootupInitialDrawersComplete_NULL = NewTransition(sBootupInitialDrawersComplete, null,
                                                                                                              EnumMacCabinetTransition.BootupInitialDrawersComplete_NULL);

            
            MacTransition tSynchronousDrawerStatesStart_SynchronousDrawerStatesIng = NewTransition(sSynchronousDrawerStatesStart,sSynchronousDrawerStatesIng,
                                                                                                                EnumMacCabinetTransition.SynchronousDrawerStatesStart_SynchronousDrawerStatesIng);
            MacTransition tSynchronousDrawerStatesIng_SynchronousDrawerStatesComplete = NewTransition(sSynchronousDrawerStatesIng, sSynchronousDrawerStatesComplete,
                                                                                                               EnumMacCabinetTransition.SynchronousDrawerStatesIng_SynchronousDrawerStatesComplete);
            MacTransition tSynchronousDrawerStatesComplete_NULL = NewTransition(sSynchronousDrawerStatesComplete, null, EnumMacCabinetTransition.SynchronousDrawerStatesComplete_NULL);
            #endregion transition

            #region event

            sStart.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("State: [sStart.OnEntry]");
                SetCurrentState((MacState)sender);
            };
            sStart.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [sStart.OnExit]");

            };

            sLoadMoveDrawerTraysToOutStart.OnEntry+=(sender, e)=>
            { // Synch
                Debug.WriteLine("LoadMoveDrawerTraysToOutStart.OnEntry");
                SetCurrentState((MacState)sender);
                var transition = tLoadMoveDrawerTraysToOutStart_LoadMoveDrawerTraysToOutIng;
                var args = (CabinetLoadStartMacStateEntryEventArgs)e;
               
                var triggerMember = new TriggerMember
                {
                    Action = (parameter)=>
                    {
                        var loadDrawerStates = (List<MacMsCabinetDrawer>)parameter;
                        foreach(var state in loadDrawerStates)
                        {
                            state.Load_MoveTrayToOut();
                        }
                    },
                    ActionParameter= args.LoadDrawerStates,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something,
                    },
                      Guard = () => { return true; },
                    NextStateEntryEventArgs =  e,
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sLoadMoveDrawerTraysToOutStart.OnExit += (sender, e) =>
            {
                Debug.WriteLine("LoadMoveDrawerTraysToOutStart.OnExit");
            };

            sLoadMoveDrawerTraysToOutIng.OnEntry += (sender, e) =>
            {  // Async
                Debug.WriteLine("LoadMoveDrawerTraysToOutIng.OnEntry");
                SetCurrentState((MacState)sender);
                var transition = tLoadMoveDrawerTraysToOutIng_LoadMoveDrawerTraysToOutComplete;
                var args = (CabinetLoadStartMacStateEntryEventArgs)e;
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
                        var completeDrawers = args.LoadDrawerStates.Where(m => m.CurrentState == m.StateLoadWaitingPutBoxOnTray).ToList().Count();
                        var exceptionDrawers= args.LoadDrawerStates.Where(m => m.CurrentState.IsStateMachineException.HasValue).ToList().Count();
                        if (completeDrawers + exceptionDrawers == args.LoadDrawerStates.Count())
                        {
                            rtnV = true;
                        }
                        return rtnV;
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMemberAsync);
                TriggerAsync(transition);
            };
            sLoadMoveDrawerTraysToOutIng.OnExit+= (sender, e) =>
            {
                Debug.WriteLine("LoadMoveDrawerTraysToOutIng.OnExit");
            };

            sLoadMoveDrawerTraysToOutComplete.OnEntry += (sender, e) =>
              {
                  Debug.WriteLine("LoadMoveDrawerTraysToOutComplete.OnEntry");
                  SetCurrentState((MacState)sender);
                  var transition =tLoadMoveDrawerTraysToOutComplete_NULL;
                  var triggerMember = new TriggerMember
                  {
                      Action = null,
                      ActionParameter = null,
                      ExceptionHandler = (state, ex) =>
                      {
                          // do domething
                      },
                        Guard = () => { return true; },
                      NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                      NotGuardException = null,
                      ThisStateExitEventArgs = new MacStateExitEventArgs()
                  };
                  transition.SetTriggerMembers(triggerMember);
                  Trigger(transition);
              };
            sLoadMoveDrawerTraysToOutComplete.OnExit += (sender, e) =>
            {
                Debug.WriteLine("LoadMoveDrawerTraysToOutComplete.OnExit ");
            };

            sBootupInitialDrawersStart.OnEntry+=(sender,e)=>
            {
                Debug.WriteLine("BootupInitialDrawersStart.OnEntry");
                SetCurrentState((MacState)sender);
                var transition = tBootupInitialDrawersStart_BootupInitialDrawersIng;
                var args=(CabinetSystemUpInitialMacStateEntryEventArgs)e;
                var triggerMember = new TriggerMember
                {
                    Action = (parameter) =>
                    {
                        var initialDrawerStates = (List<MacMsCabinetDrawer>)parameter;
                        foreach (var state in initialDrawerStates)
                        {
                           
                            state.SystemBootupInitial();
                        }
                    },
                    ActionParameter = args.InitialDrawerStates,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do domething
                    },
                      Guard = () => { return true; },
                    NextStateEntryEventArgs =e,
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                        
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sBootupInitialDrawersStart.OnExit += (sender, e) =>
            {
                Debug.WriteLine("BootupInitialDrawersStart.OnExit");
            };

            sBootupInitialDrawersIng.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("BootupInitialDrawersIng.OnEntry");
                SetCurrentState((MacState)sender);
                var transition = tBootupInitialDrawersIng_BootupInitialDrawersComplete;
                var args = (CabinetSystemUpInitialMacStateEntryEventArgs)e;
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

                        var completeDrawers = args.InitialDrawerStates.Where(m => m.CurrentState == m.StateWaitingLoadInstruction).ToList().Count();
                        var exceptionDrawers = args.InitialDrawerStates.Where(m => m.CurrentState.IsStateMachineException.HasValue).ToList().Count();
                        if (completeDrawers + exceptionDrawers == args.InitialDrawerStates.Count())
                        {
                            rtnV = true;
                        }
                        return rtnV;
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMemberAsync);
                TriggerAsync(transition);

            };
            sBootupInitialDrawersIng.OnExit += (sender, e) =>
            {
                Debug.WriteLine("BootupInitialDrawersIng.OnExit");
            };

            sBootupInitialDrawersComplete.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("BootupInitialDrawersComplete.OnEntry");
                var transition = tBootupInitialDrawersComplete_NULL;
                SetCurrentState((MacState)sender);
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something
                    },
                      Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sBootupInitialDrawersComplete.OnExit += (sender, e) =>
            {
                Debug.WriteLine("BootupInitialDrawersComplete.OnExit");
            };

            sSynchronousDrawerStatesStart.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("SynchronousDrawerStatesStart.OnEntry");
                SetCurrentState((MacState)sender);
                var transition = tSynchronousDrawerStatesStart_SynchronousDrawerStatesIng;
                var args=(CabinetSynchronousDrawerStatesMacStateEntryEventArgs)e;
                var triggerMember = new TriggerMember
                {
                    Action = (parameter) =>
                    {
                        foreach(var state in args.SynchronousDrawerStates)
                        {
                            state.SystemBootup();
                        }
                    },
                    ActionParameter = args.SynchronousDrawerStates,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something
                    },
                      Guard = () => { return true; },
                    NextStateEntryEventArgs = e,
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sSynchronousDrawerStatesStart.OnExit += (sender, e) =>
            {
                Debug.WriteLine("SynchronousDrawerStatesStart.OnExit");
            };

            sSynchronousDrawerStatesIng.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("SynchronousDrawerStatesIng.OnEntry");
                SetCurrentState((MacState)sender);
                var transition = tSynchronousDrawerStatesIng_SynchronousDrawerStatesComplete;
                //  var args = (CabinetSystemUpInitialMacStateEntryEventArgs)e;
                //  var args = (CabinetSystemUpInitialMacStateEntryEventArgs)e;
                var args = (CabinetSynchronousDrawerStatesMacStateEntryEventArgs)e;
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
                        var completeDrawers = args.SynchronousDrawerStates.Where(m => m.CurrentState == m.StateWaitingLoadInstruction).ToList().Count();
                        var exceptionDrawers = args.SynchronousDrawerStates.Where(m => m.CurrentState.IsStateMachineException.HasValue).ToList().Count();
                        if (completeDrawers + exceptionDrawers == args.SynchronousDrawerStates.Count())
                        {
                            rtnV = true;
                        }
                        return rtnV;
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                   ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMemberAsync);
                TriggerAsync(transition);
            };
            sSynchronousDrawerStatesIng.OnExit += (sender, e) =>
            {
                Debug.WriteLine("SynchronousDrawerStatesIng.OnExit");
            };

            sSynchronousDrawerStatesComplete.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("SynchronousDrawerStatesComplete.OnEntry");
                SetCurrentState((MacState)sender);
                var transition = tSynchronousDrawerStatesComplete_NULL;
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) => {
                        //do something
                    },
                      Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()

                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sSynchronousDrawerStatesComplete.OnExit += (sender, e) =>
            {
                Debug.WriteLine("SynchronousDrawerStatesComplete.OnExit");
            };

            #endregion event
        }

    }
    public class CabinetLoadStartMacStateEntryEventArgs: MacStateEntryEventArgs
    {
        public CabinetLoadStartMacStateEntryEventArgs(List<MacMsCabinetDrawer> drawerStates)
        {
            LoadDrawerStates = drawerStates;
        }
        public List<MacMsCabinetDrawer> LoadDrawerStates { get; private set; }

    }

    public class CabinetSystemUpInitialMacStateEntryEventArgs : MacStateEntryEventArgs
    {
        public CabinetSystemUpInitialMacStateEntryEventArgs(List<MacMsCabinetDrawer> drawerStates)
        {
            InitialDrawerStates = drawerStates;
        }
        public List<MacMsCabinetDrawer> InitialDrawerStates { get; private set; }
    }

    public class CabinetSynchronousDrawerStatesMacStateEntryEventArgs : MacStateEntryEventArgs
    {
        public CabinetSynchronousDrawerStatesMacStateEntryEventArgs(List<MacMsCabinetDrawer> drawerStates)
        {
            SynchronousDrawerStates = drawerStates;
        }
        public List<MacMsCabinetDrawer> SynchronousDrawerStates { get; private set; }
    }

   
}
