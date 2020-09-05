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
        public List<MacState> LoadState = null;
        
        #region 指令

        /// <summary>啟動後, 重設所有 Drawer 狀態</summary>
        public void ResetAllDrawerState()
        {
            if (SumOfCabinetDrawerStates == 0)
            {

            }
            else
            {
                foreach (var state in _dicCabinetDrawerStates)
                {
                    state.Value.SystemBootup();
                }
            }
        }
        
        /// <summary>啟動後 Initial 所有 Drawer</summary>
        public void InitialBootupDrawer()
        {
            if (SumOfCabinetDrawerStates == 0)
            {

            }
            else
            {
                foreach (var state in _dicCabinetDrawerStates)
                {
                    state.Value.SystemBootupInitial();
                }
            }
        }

        /// <summary>load</summary>
        /// <param name="targetDrawerQuantity"> Drawer 數量</param>
        public void Load(int targetDrawerQuantity)
        {

            var states = _dicCabinetDrawerStates.Values.Where(m => m.CanLoad()).ToList();
            if (states.Count==0)
            {
                // 
            }
            else if (states.Count> targetDrawerQuantity)
            {
                states = states.Take(targetDrawerQuantity).ToList();
            }
            this.States[EnumMacCabinetState.StateMachineLoadAllDrawersStateMchineStart.ToString()].DoEntry(new CabinetLoadStartMacStateEntryEventArgs(states));
           
        }
        #endregion 指令


        /// <summary>CabinetDrawer State 的集合</summary>
        private IDictionary<string, MacMsCabinetDrawer> _dicCabinetDrawerStates;
        /// <summary>CabinetDrawer State的數量</summary>
        public int SumOfCabinetDrawerStates
        {
            get
            {
                int states= 0;
                if (_dicCabinetDrawerStates != null)
                {
                    states = _dicCabinetDrawerStates.Count();
                }
                return states;
            }
        }

        public MacMsCabinet()
        {
            _dicCabinetDrawerStates = new Dictionary<string, MacMsCabinetDrawer>();
        }

        public MacMsCabinet(IList<MacMsCabinetDrawer> drawerStates):this()
        {
            foreach(var drawerState in drawerStates)
            {
                if (_dicCabinetDrawerStates.ContainsKey(drawerState.DeviceIndex))
                {
                    _dicCabinetDrawerStates.Remove(drawerState.DeviceIndex);
                }
                _dicCabinetDrawerStates.Add(drawerState.DeviceIndex, drawerState);
            } 
        }

       

        public override void LoadStateMachine()
        {

            #region state
         
            MacState sStateMachineLoadAllDrawersStateMchineStart = NewState(EnumMacCabinetState.StateMachineLoadAllDrawersStateMchineStart);
            MacState sStateMachineLoadAllDrawersStateMchineIng = NewState(EnumMacCabinetState.StateMachineLoadAllDrawersStateMchineIng);
            MacState sStateMachineLoadAllDrawersStateMchineComplete = NewState(EnumMacCabinetState.StateMachineLoadAllDrawersStateMchineComplete);

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
            MacTransition tStateMachineLoadAllDrawersStateMchineStart_StateMachineLoadAllDrawersStateMchineIng = NewTransition(sStateMachineLoadAllDrawersStateMchineStart, sStateMachineLoadAllDrawersStateMchineIng,
                                                                                                                  EnumMacCabinetTransition.StateMachineLoadAllDrawersStateMchineStart_StateMachineLoadAllDrawersStateMchineIng);
            MacTransition tStateMachineLoadAllDrawersStateMchineIng_StateMachineLoadAllDrawersStateMchineComplete = NewTransition(sStateMachineLoadAllDrawersStateMchineIng, sStateMachineLoadAllDrawersStateMchineComplete,
                                                                                                                  EnumMacCabinetTransition.StateMachineLoadAllDrawersStateMchineIng_StateMachineLoadAllDrawersStateMchineComplete);
            /** 
              MacTransition tStateMachineLoadAllDrawersStateMchineComplete_AnyState = NewTransition(sStateMachineLoadAllDrawersStateMchineComplete, sAnyState,
                                                                                                                  EnumMacCabinetTransition.StateMachineLoadAllDrawersStateMchineComplete_AnyState);
           */
            MacTransition tLoadMoveDrawerTraysToOutStart_LoadMoveDrawerTraysToOutIng = NewTransition(sLoadMoveDrawerTraysToOutStart,sLoadMoveDrawerTraysToOutIng,
                                                                                                                 EnumMacCabinetTransition.LoadMoveDrawerTraysToOutStart_LoadMoveDrawerTraysToOutIng);
            MacTransition tLoadMoveDrawerTraysToOutIng_LoadMoveDrawerTraysToOutComplete = NewTransition(sLoadMoveDrawerTraysToOutIng, sLoadMoveDrawerTraysToOutComplete,
                                                                                                                 EnumMacCabinetTransition.LoadMoveDrawerTraysToOutIng_LoadMoveDrawerTraysToOutComplete);
            MacTransition tLoadMoveDrawerTraysToOutComplete_NULL = NewTransition(sLoadMoveDrawerTraysToOutComplete, null,
                                                                                                               EnumMacCabinetTransition.LoadMoveDrawerTraysToOutComplete_NULL);
            /**
            MacTransition tLoadMoveDrawerTraysToOutComplete_AnyState = NewTransition(sLoadMoveDrawerTraysToOutComplete,sAnyState,
                                                                                                                 EnumMacCabinetTransition.LoadMoveDrawerTraysToOutComplete_AnyState);
             */
            MacTransition tBootupInitialDrawersStart_BootupInitialDrawersIng = NewTransition(sBootupInitialDrawersStart,sBootupInitialDrawersIng,
                                                                                                                EnumMacCabinetTransition.BootupInitialDrawersStart_BootupInitialDrawersIng);
            MacTransition tBootupInitialDrawersIng_BootupInitialDrawersComplete = NewTransition(sBootupInitialDrawersIng, sBootupInitialDrawersComplete,
                                                                                                              EnumMacCabinetTransition.BootupInitialDrawersStart_BootupInitialDrawersIng);
            /**
            MacTransition tBootupInitialDrawersComplete_AnyState = NewTransition(sBootupInitialDrawersComplete, sAnyState,
                                                                                                              EnumMacCabinetTransition.BootupInitialDrawersComplete_AnyState);
            */
            MacTransition tSynchronousDrawerStatesStart_SynchronousDrawerStatesIng = NewTransition(sSynchronousDrawerStatesStart,sSynchronousDrawerStatesIng,
                                                                                                                EnumMacCabinetTransition.SynchronousDrawerStatesStart_SynchronousDrawerStatesIng);
            MacTransition tSynchronousDrawerStatesIng_SynchronousDrawerStatesComplete = NewTransition(sSynchronousDrawerStatesIng, sSynchronousDrawerStatesComplete,
                                                                                                               EnumMacCabinetTransition.SynchronousDrawerStatesIng_SynchronousDrawerStatesComplete);
            /**
            MacTransition tSynchronousDrawerStatesComplete_AnyState = NewTransition(sSynchronousDrawerStatesComplete,sAnyState,
                                                                                                         EnumMacCabinetTransition.SynchronousDrawerStatesComplete_AnyState);
           */
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
                    Guard = () => true,
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
                        var normals = args.LoadDrawerStates.Where(m => m.CutrrentState == m.StateLoadWaitingPutBoxOnTray).ToList().Count();
                        var exceptions= args.LoadDrawerStates.Where(m => m.CutrrentState.IsStateMachineException.HasValue).ToList().Count();
                        if (normals + exceptions == args.LoadDrawerStates.Count())
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

                      },
                      Guard = () => true,
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
            #endregion event
        }

    }
    public class CabinetLoadStartMacStateEntryEventArgs: MacStateEntryEventArgs
    {
        public CabinetLoadStartMacStateEntryEventArgs(List<MacMsCabinetDrawer> drawerStates)
        {
            LoadDrawerStates = drawerStates;
        }
        public List<MacMsCabinetDrawer> LoadDrawerStates { get; set; }


    }
}
