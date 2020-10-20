using MaskAutoCleaner.v1_0.Machine.CabinetDrawer;
using MaskAutoCleaner.v1_0.Machine.Drawer;
using MaskAutoCleaner.v1_0.StateMachineBeta;
using MaskAutoCleaner.v1_0.StateMachineExceptions;
using MvAssistant.Mac.v1_0.Hal.CompDrawer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.Cabinet
{
    [Guid("11111111-1111-1111-1111-111111111111")]// TODO: UPdate this Guid
    public class MacMsCabinet : MacMachineStateBase
    {

        /*
        private static List<MacMcCabinetDrawer> MachineControls = null;
        public static Dictionary<EnumMachineID, MacMsCabinetDrawer> _dicStateMachines = null;
        public readonly static object _getStateMachineLockObj = new object();
        public static Dictionary<EnumMachineID, MacMsCabinetDrawer> DicStateMachines
        {
            get
            {

                if (_dicStateMachines==null)
                {
                    lock (_getStateMachineLockObj)
                    {
                        if (_dicStateMachines == null)
                        {

                            var DrawerMachineIdRange = EnumMachineID.MID_DRAWER_01_01.GetDrawerStateMachineIDRange();
                            
                            var MachineMgr = new MacMachineMgr();
                            MachineMgr.MvCfInit();
                            MachineControls = new List<MacMcCabinetDrawer>();
                            _dicStateMachines = new Dictionary<EnumMachineID, MacMsCabinetDrawer>();
                            for (var i = (int)DrawerMachineIdRange.StartID; i <= (int)DrawerMachineIdRange.EndID; i++)
                            {
                                var machineId = ((EnumMachineID)i);
                                try
                                {
                                    var control = MachineMgr.CtrlMachines[machineId.ToString()] as MacMcCabinetDrawer;
                                    MachineControls.Add(control);
                                    _dicStateMachines.Add(machineId, control.StateMachine);
                                }
                                catch (Exception ex)
                                {

                                }
                            }
                        }
                    }
                }
                return _dicStateMachines;
            }

        }
    */

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

        
        #region 指令
        /// <summary>load</summary>
        /// <param name="targetDrawerQuantity"> Drawer 數量</param>
        /// <param name="dicStateMachines">所有Drawer 的集合</param>
        public void LoadDrawers(int targetDrawerQuantity,Dictionary<EnumMachineID, MacMsCabinetDrawer> dicStateMachines)
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
            this.States[EnumMacCabinetState.LoadMoveDrawerTraysToOutStart.ToString()].DoEntry(new CabinetLoadStartMacStateEntryEventArgs(states));
           
        }


        /// <summary>系統啟動之後 的 Initial</summary>
        /// <param name="dicStateMachines"></param>
        public void BootupInitialDrawers(Dictionary<EnumMachineID, MacMsCabinetDrawer> dicStateMachines)
        {
            // var states = dicStateMachines.Values.Where(m => m.CanLoad()).ToList();
            var states = dicStateMachines.Values.ToList();
            if (states.Count == 0)
            { }
            else
            {
                this.States[EnumMacCabinetState.BootupInitialDrawersStart.ToString()].DoEntry(new CabinetSystemUpInitialMacStateEntryEventArgs   (states));
            }
        }

        public void SynchrousDrawerStates(Dictionary<EnumMachineID, MacMsCabinetDrawer> dicStateMachines)
        {
            
            // [???]
            //  var states = dicStateMachines.Values.Where(m => m.CanLoad()).ToList();
            var states = new List<MacMsCabinetDrawer>();
            states.Add(dicStateMachines.Values.ToList()[0]);


            if (states.Count == 0)
            { }
            else
            {
                this.States[EnumMacCabinetState.SynchronousDrawerStatesStart.ToString()].DoEntry(new  CabinetSynchronousDrawerStatesMacStateEntryEventArgs(states));
            }
        }

        
        #endregion 指令


      
        public MacMsCabinet()
        {
            //_dicCabinetDrawerStates = new Dictionary<string, MacMsCabinetDrawer>();
            LoadStateMachine();
        }

       
       

        public override void LoadStateMachine()
        {

            #region state
         
           

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

            sLoadMoveDrawerTraysToOutStart.OnEntry+=(sender, e)=>
            { // Synch
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
            { };

            sLoadMoveDrawerTraysToOutIng.OnEntry += (sender, e) =>
            {  // Async
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
                        var completeDrawers = args.LoadDrawerStates.Where(m => m.CutrrentState == m.StateLoadWaitingPutBoxOnTray).ToList().Count();
                        var exceptionDrawers= args.LoadDrawerStates.Where(m => m.CutrrentState.IsStateMachineException.HasValue).ToList().Count();
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
            { };

            sLoadMoveDrawerTraysToOutComplete.OnEntry += (sender, e) =>
              {
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

            };

            sBootupInitialDrawersStart.OnEntry+=(sender,e)=>
            {
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

            };

            sBootupInitialDrawersIng.OnEntry += (sender, e) =>
            {
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

                        var completeDrawers = args.InitialDrawerStates.Where(m => m.CutrrentState == m.StateWaitingLoadInstruction).ToList().Count();
                        var exceptionDrawers = args.InitialDrawerStates.Where(m => m.CutrrentState.IsStateMachineException.HasValue).ToList().Count();
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

            };

            sBootupInitialDrawersComplete.OnEntry += (sender, e) =>
            {
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

            };

            sSynchronousDrawerStatesStart.OnEntry += (sender, e) =>
            {
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
            sSynchronousDrawerStatesStart.OnEntry += (sender, e) =>
            {

            };

            sSynchronousDrawerStatesIng.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);
                var transition = tSynchronousDrawerStatesIng_SynchronousDrawerStatesComplete;
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

                        /**
                        var completeDrawers = args.InitialDrawerStates.Where(m => m.CutrrentState == m.StateSystemBootup).ToList().Count();
                        var exceptionDrawers = args.InitialDrawerStates.Where(m => m.CutrrentState.IsStateMachineException.HasValue).ToList().Count();
                        if (completeDrawers + exceptionDrawers == args.InitialDrawerStates.Count())
                        {
                            rtnV = true;
                        }
                        */

                        var completeDrawers = args.SynchronousDrawerStates.Where(m => m.CutrrentState == m.StateSystemBootup).ToList().Count();
                        var exceptionDrawers = args.SynchronousDrawerStates.Where(m => m.CutrrentState.IsStateMachineException.HasValue).ToList().Count();
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

            };

            sSynchronousDrawerStatesComplete.OnEntry += (sender, e) =>
            {
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
            sSynchronousDrawerStatesComplete.OnEntry += (sender, e) =>
            {

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
