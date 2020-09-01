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
        #region 指令

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

        /// <summary>Initial 集合內的CabinetDrawer </summary>
        public void InitialAllCabinetDrawers()
        {
            if (SumOfCabinetDrawerStates == 0)
            {

            }
            else
            {
                foreach (var state in _dicCabinetDrawerStates)
                {
                   
                }
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
                
            } 
        }

       

        public override void LoadStateMachine()
        {
#region state

            MacState sBeforeInitial = NewState(EnumMacCabinetState.BeforeInitial);
           

            MacState sInitialAllDrawersStart = NewState(EnumMacCabinetState.InitialAllDrawersStart);
            MacState sInitialAllDrawersIng = NewState(EnumMacCabinetState.InitialAllDrawersIng);
            MacState sInitialAllDrawersComplete = NewState(EnumMacCabinetState.InitialAllDrawersComplete);
            MacState sInitialAllDrawersError = NewState(EnumMacCabinetState.InitialAllDrawersError);

            MacState sWaitingLoad = NewState(EnumMacCabinetState.WaitingLoadInstruction);

            MacState sInitialDrawersStart = NewState(EnumMacCabinetState.InitialDrawersStart);
            MacState sInitialDrawersIng = NewState(EnumMacCabinetState.InitialDrawersIng);
            MacState sInitialDrawersComplete = NewState(EnumMacCabinetState.InitialDrawersComplete);
            MacState sInitialDrawersError = NewState(EnumMacCabinetState.InitialDrawersError);


           
            MacState sMoveDrawerTraysToOutStart = NewState(EnumMacCabinetState.MoveDrawerTraysToOutStart);
            MacState sMoveDrawerTraysToOutIng = NewState(EnumMacCabinetState.MoveDrawerTraysToOutIng);
            MacState sMoveDrawerTraysToOutComplete = NewState(EnumMacCabinetState.MoveDrawerTraysToOutComplete);
            #endregion state

            #region transition
            MacTransition tBeforeInitial_InitialAllDrawersStart = NewTransition(sBeforeInitial,sInitialAllDrawersStart,
                                                                  EnumMacCabinetTransition.BeforeInitial_InitialAllDrawersStart);
            MacTransition tInitialAllDrawersStart_InitialAllDrawersIng = NewTransition(sInitialAllDrawersStart, sInitialAllDrawersIng, 
                                                                      EnumMacCabinetTransition.InitialAllDrawersStart_InitialAllDrawersIng);
            MacTransition tInitialAllDrawersIng_InitialAllDrawersComplete = NewTransition( sInitialAllDrawersIng, sInitialAllDrawersComplete,
                                                                                 EnumMacCabinetTransition.InitialAllDrawersIng_InitialAllDrawersComplete);
            MacTransition tInitialAllDrawersComplete_WaitingLoad = NewTransition(sInitialAllDrawersComplete,sWaitingLoad,
                                                                               EnumMacCabinetTransition.InitialAllDrawersComplete_WaitingLoad);
                                                                                                        
             MacTransition tInitialAllDrawersComplete_InitialAllDrawersError = NewTransition(sInitialAllDrawersComplete,sInitialAllDrawersError,
                                                                              EnumMacCabinetTransition.InitialAllDrawersComplete_InitialAllDrawersError);

            MacTransition tWaitingLoad_NULL = NewTransition(sWaitingLoad,null,  EnumMacCabinetTransition.WaitingLoad_NULL );
            MacTransition tInitialAllDrawersError_NULL = NewTransition(sInitialAllDrawersError, null,EnumMacCabinetTransition.InitialAllDrawersError_NULL);

            MacTransition tWaitingLoad_InitialDrawersStart = NewTransition(sWaitingLoad,sInitialAllDrawersStart, 
                                                             EnumMacCabinetTransition.WaitingLoad_InitialDrawersStart);
            MacTransition tInitialDrawersStart_InitialDrawersIng = NewTransition(sInitialAllDrawersStart, sInitialAllDrawersIng,
                                                             EnumMacCabinetTransition.InitialDrawersStart_InitialDrawersIng);

            MacTransition tInitialDrawersIng_InitialDrawersComplete = NewTransition(sInitialDrawersIng, sInitialDrawersComplete,
                                                             EnumMacCabinetTransition.InitialAllDrawersIng_InitialAllDrawersComplete);

            MacTransition tInitialDrawersComplete_WaitingtLoad = NewTransition(sInitialDrawersComplete, sWaitingLoad,
                                                              EnumMacCabinetTransition.InitialDrawersComplete_WaitingtLoad);
            MacTransition tInitialDrawersComplete_InitialDrawersError = NewTransition(sInitialDrawersComplete, sInitialDrawersError,
                                                              EnumMacCabinetTransition.InitialDrawersComplete_InitialDrawersError);

            MacTransition tInitialDrawersError_NULL= NewTransition( sInitialDrawersError,null,
                                                             EnumMacCabinetTransition.InitialDrawersError_NULL);

            MacTransition tWaitingLoad_MoveDrawerTraysToOutStart = NewTransition(sWaitingLoad, sMoveDrawerTraysToOutStart,
                                                             EnumMacCabinetTransition.WaitingLoad_MoveDrawerTraysToOutStart);
            MacTransition tMoveDrawerTraysToOutStart_MoveDrawerTraysToOutIng = NewTransition(sMoveDrawerTraysToOutStart, sMoveDrawerTraysToOutIng,
                                                            EnumMacCabinetTransition.MoveDrawerTraysToOutStart_MoveDrawerTraysToOutIng);
            MacTransition tMoveDrawerTraysToOutIng_MoveDrawerTraysToOutComplete = NewTransition(sMoveDrawerTraysToOutIng, sMoveDrawerTraysToOutComplete,
                                                            EnumMacCabinetTransition.MoveDrawerTraysToOutIng_MoveDrawerTraysToOutComplete);
            MacTransition tMoveDrawerTraysToOutComplete_WaitingLoad = NewTransition( sMoveDrawerTraysToOutComplete, sWaitingLoad,
                                                          EnumMacCabinetTransition.MoveDrawerTraysToOutIng_MoveDrawerTraysToOutComplete);

            #endregion transition

            #region event

            sBeforeInitial.OnEntry +=(sender, e) =>
            {   // Sync
                var transition = tBeforeInitial_InitialAllDrawersStart;
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
            sBeforeInitial.OnExit += (sender, e) =>
            {
                // TODO: depends on demand
            };

            sInitialAllDrawersStart.OnEntry += (sender, e) =>
            {  // Sync
                var transition = tInitialAllDrawersStart_InitialAllDrawersIng;
                var triggerMember = new TriggerMember
                {
                    Guard = () => true,
                    Action = (parameter) => this.InitialAllCabinetDrawers(),
                    ActionParameter = false,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);

            };
            sInitialAllDrawersStart.OnExit += (sender, e) =>
            {

            };

            sInitialAllDrawersIng.OnEntry += (sender, e) =>
            {  // Async
                var transition = tInitialAllDrawersIng_InitialAllDrawersComplete;
                var triggerMemberAsync = new TriggerMemberAsync
                {
                    Guard = (startTime) =>
                      {
                          var rtnV = false;
                          return rtnV;
                      },
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // TODO: depends on demand
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMemberAsync);
                TriggerAsync(transition);

            };
            sInitialAllDrawersIng.OnExit += (sender, e) =>
            {
                
            };

            sInitialAllDrawersComplete.OnEntry+=(sender, e)=>
            { 


            };
            sInitialAllDrawersComplete.OnExit += (sender, e) => {

            };

            sInitialAllDrawersError.OnEntry += (sender, e) =>
            {

            };
            sInitialAllDrawersError.OnExit += (sender, e) =>
            {

            };

            sWaitingLoad.OnEntry += (sender, e) =>
            {

            };
            sWaitingLoad.OnExit += (sender, e) =>
            {

            };

            sInitialDrawersStart.OnEntry += (sender, e) =>
            {

            };
            sInitialDrawersStart.OnExit += (sender, e) =>
            {

            };

            sInitialDrawersIng.OnEntry += (sender, e) =>
            {

            };
            sInitialDrawersIng.OnExit += (sender, e) =>
            {

            };

            sInitialDrawersComplete.OnEntry += (sender, e) =>
            {

            };
            sInitialDrawersComplete.OnExit += (sender, e) =>
            {

            };

            sInitialDrawersError.OnEntry += (sender, e) =>
            {

            };
            sInitialDrawersError.OnExit += (sender, e) =>
            {

            };

            sMoveDrawerTraysToOutStart.OnEntry += (sender, e) =>
            {

            };
            sMoveDrawerTraysToOutStart.OnExit += (sender, e) =>
            {

            };

            sMoveDrawerTraysToOutIng.OnEntry += (sender, e) =>
            {

            };
            sMoveDrawerTraysToOutIng.OnExit += (sender, e) =>
            {

            };

            sMoveDrawerTraysToOutComplete.OnEntry += (sender, e) =>
            {

            };
            sMoveDrawerTraysToOutComplete.OnExit += (sender, e) =>
            {

            };
            #endregion event



        }




    }
}
