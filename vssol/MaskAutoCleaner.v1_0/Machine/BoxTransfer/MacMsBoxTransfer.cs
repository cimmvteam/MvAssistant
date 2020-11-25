using MaskAutoCleaner.v1_0.Machine.BoxTransfer.OnEntryEventArgs;
using MaskAutoCleaner.v1_0.StateMachineBeta;
using MaskAutoCleaner.v1_0.StateMachineExceptions.BoxTransferStateMachineException;
using MaskAutoCleaner.v1_0.StateMachineExceptions.UniversalStateMachineException;
using MvAssistant.Mac.v1_0;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.JSon;
using MvAssistant.Mac.v1_0.JSon.RobotTransferFile;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// vs 2013
//using static MaskAutoCleaner.v1_0.Machine.MaskTransfer.MacMsMaskTransfer;

namespace MaskAutoCleaner.v1_0.Machine.BoxTransfer
{
    public class MacMsBoxTransfer : MacMachineStateBase
    {
        // private MacState _currentState = null;
        BoxrobotTransferPathFile pathObj = new BoxrobotTransferPathFile(PositionInstance.BTR_Path);// new BoxrobotTransferPathFile(@"D:\Positions\BTRobot\");
        private IMacHalUniversal HalUniversal { get { return this.Mediater.GetCtrlMachine(EnumMachineID.MID_UNI_A_ASB.ToString()).HalAssembly as IMacHalUniversal; } }
        public IMacHalBoxTransfer HalBoxTransfer { get { return this.halAssembly as IMacHalBoxTransfer; } }
        private IMacHalOpenStage HalOpenStage { get { return this.Mediater.GetCtrlMachine(EnumMachineID.MID_OS_A_ASB.ToString()).HalAssembly as IMacHalOpenStage; } }
        public BoxrobotTransferLocation DrawerLocation { get; private set; }
        public void ResetState()
        { this.States[EnumMacBoxTransferState.Start.ToString()].DoEntry(new MacStateEntryEventArgs(null)); }

        protected override void SetCurrentState(MacState state)
        {
            base.SetCurrentState(state);
            DrawerLocation = BoxrobotTransferLocation.Dontcare;
        }


        protected void SetCurrentState(MacState state, BoxrobotTransferLocation drawerLocation)
        {

            this.SetCurrentState(state);
            this.DrawerLocation = drawerLocation;
        }



        public MacMsBoxTransfer() { this.LoadStateMachine(); }




        #region State Machine Command

        public override void SystemBootup()
        {
            // state: sStart
            Debug.WriteLine("Command: SystemBootup()");
            this.States[EnumMacBoxTransferState.Start.ToString()].ExecuteCommandAtEntry(new MacStateEntryEventArgs(null));
        }
        public void Initial()
        {
            // State: sInitial
            Debug.WriteLine("Command: Initial()");
            this.States[EnumMacBoxTransferState.DeviceInitial.ToString()].ExecuteCommandAtEntry(new MacStateEntryEventArgs(null));
        }


        public void MoveToLock(BoxType boxType)
        {
            Debug.WriteLine("Command: MoveToLock(), " + "BoxType=" + boxType.ToString());

            // from: sCB1Home, to:sLocking
            //var transition = Transitions[EnumMacBoxTransferTransition.MoveToLock.ToString()];
            var transition = Transitions[EnumMacBoxTransferTransition.CB1Home_Locking.ToString()];
#if GNotCareState
            var state = transition.StateFrom;
#else
            var state = this.CurrentState;
#endif
            state.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateMoveToLockEntryEventArgs(boxType));

        }

        public void MoveToUnlock(BoxType boxType)
        {
            Debug.WriteLine("Command: MoveToUnLock(), " + "BoxType=" + boxType.ToString());
            // from: sCB1Home, to: sUnlocking
            //var transition = Transitions[EnumMacBoxTransferTransition.MoveToUnlock.ToString()];
            var transition = Transitions[EnumMacBoxTransferTransition.CB1Home_Unlocking.ToString()];
#if GNotCareState
            var state = transition.StateFrom;
#else
            var state = this.CurrentState;
#endif
            state.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateMoveToUnLockEntryEventArgs(boxType));
        }


        public void MoveToOpenStageGet(BoxType boxType)
        {
            Debug.WriteLine("Command: MoveToOpenStageGet(), BoxType= " + boxType);
            MacTransition transition = null;

            //from: sCB1Home, to: sMovingToOpenStage
            //transition = Transitions[EnumMacBoxTransferTransition.MoveToOpenStage.ToString()];
            transition = Transitions[EnumMacBoxTransferTransition.CB1Home_MovingToOpenStage.ToString()];
#if GNotCareState
            var state = transition.StateFrom;
#else
            var state = this.CurrentState;
#endif
            state.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateMovingToOpenStageEntryEventArgs(boxType));
        }
        public void MoveToOpenStagePut()
        {
            Debug.WriteLine("Command: MoveToOpenStagePut()");
            // from: sCB1HomeClamped, to: sMovingToOpenStageForRelease
            //var transition = Transitions[EnumMacBoxTransferTransition.MoveToOpenStageForRelease.ToString()];
            var transition = Transitions[EnumMacBoxTransferTransition.MoveToOpenStageForRelease.ToString()];
#if GNotCareState
            var state = transition.StateFrom;
#else
            var state = this.CurrentState;
#endif
            state.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateEntryEventArgs(null));
        }




        /// <summary>移動到指定Cabinet編號的位置取盒，Cabinet編號0101~0705</summary>
        /// <remarks>
        /// <para>2020/09/22, King Add</para>
        /// <para>合併 State</para>
        /// </remarks>
        /// <param name="drawerLocation"></param>
        public void MoveToCabinetGet(BoxrobotTransferLocation drawerLocation, BoxType boxType = BoxType.DontCare)
        {
            Debug.WriteLine("Command: MoveToCabinetGet, DrawerLocation: " + drawerLocation + ", BoxType:" + boxType);
            MacTransition transition = null;
            // TriggerMember triggerMember = null;

            // 從 CB1Home 移到指定的 Drawer
            //from: sCB1Home, to: sMovingToDrawer
            transition = Transitions[EnumMacBoxTransferTransition.CB1Home_MovingToDrawer.ToString()];
#if GNotCareState
            var state = transition.StateFrom;
#else
            var state = this.CurrentState;
#endif
            state.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateMovingToDrawerEntryEventArgs(drawerLocation, boxType));

        }

        /// <summary>
        /// 移動到指定Cabinet編號的位置放置，Cabinet編號0101~0705
        /// </summary>
        /// <remarks>
        /// <para>2020/09/22, King Add</para>
        /// <para>合併State 測試</para>
        /// </remarks>
        /// <param name="CabinetNumber">0101~0705</param>
        public void MoveToCabinetPut(BoxrobotTransferLocation drawerLocation, BoxType boxType)
        {

            Debug.WriteLine("Command: MoveToCabinetPut, DrawerNumber=" + drawerLocation + ", BoxType=" + boxType);
            // from: sCB1HomeClamped, to: stateMovingToDrawerForRelease
           // var transition = Transitions[EnumMacBoxTransferTransition.MoveToDrawerForRelease.ToString()];  //transitionCB1HomeClamped_MovingToDrawerForRelease
            var transition = Transitions[EnumMacBoxTransferTransition.CB1HomeClamped_MovingToDrawerForRelease.ToString()];  //transitionCB1HomeClamped_MovingToDrawerForRelease
#if GNotCareState
            var state = transition.StateFrom;
#else
            var state = this.CurrentState;
#endif
            state.ExecuteCommandAtExit(transition, new MacStateExitEventArgs(), new MacStateMovingToDrawerForReleaseEntryEventArgs(drawerLocation, boxType));

        }
        #endregion

        public override void LoadStateMachine()
        {
            #region State
            #region Basic
            MacState sStart = NewState(EnumMacBoxTransferState.Start);
            MacState sDeviceInitial = NewState(EnumMacBoxTransferState.DeviceInitial);
            MacState sCB1Home = NewState(EnumMacBoxTransferState.CB1Home);
            MacState sCB1HomeClamped = NewState(EnumMacBoxTransferState.CB1HomeClamped);
            #endregion

            #region Change Direction
            MacState sChangingDirectionToCB1Home = NewState(EnumMacBoxTransferState.ChangingDirectionToCB1Home);
            MacState sChangingDirectionToCB1HomeClamped = NewState(EnumMacBoxTransferState.ChangingDirectionToCB1HomeClamped);
            #endregion Change Direction

            #region OS
            MacState sMovingToOpenStage = NewState(EnumMacBoxTransferState.MovingToOpenStage);
            MacState sOpenStageClamping = NewState(EnumMacBoxTransferState.OpenStageClamping);
            MacState sMovingToCB1HomeClampedFromOpenStage = NewState(EnumMacBoxTransferState.MovingToCB1HomeClampedFromOpenStage);

            MacState sMovingToOpenStageForRelease = NewState(EnumMacBoxTransferState.MovingToOpenStageForRelease);
            MacState sOpenStageReleasing = NewState(EnumMacBoxTransferState.OpenStageReleasing);
            MacState sMovingToCB1HomeFromOpenStage = NewState(EnumMacBoxTransferState.MovingToCB1HomeFromOpenStage);
            #endregion OS

            #region Lock & Unlock
            MacState sLocking = NewState(EnumMacBoxTransferState.Locking);
            MacState sUnlocking = NewState(EnumMacBoxTransferState.Unlocking);
            #endregion Lock & Unlock

            #region CB1
            #region Move To Cabinet

            MacState sMovingToDrawer = NewState(EnumMacBoxTransferState.MovingToDrawer);
            #endregion Move To Cabinet

            #region Clamping At Cabinet

            MacState sDrawerClamping = NewState(EnumMacBoxTransferState.DrawerClamping);
            #endregion Clamping At Cabinet

            #region Return To CB Home Clamped From Cabinet


            MacState sMovingToCB1HomeClampedFromDrawer = NewState(EnumMacBoxTransferState.MovingToCB1HomeClampedFromDrawer);

            #endregion Return To CB Home Clamped From Cabinet

            #region Move To Cabinet Fro Release

            MacState sMovingToDrawerForRelease = NewState(EnumMacBoxTransferState.MovingToDrawerForRelease);
            #endregion Move To Cabinet Fro Release

            #region Releasing At Cabinet

            MacState sDrawerReleasing = NewState(EnumMacBoxTransferState.DrawerReleasing);
            #endregion Releasing At Cabinet

            #region Return To CB Home From Cabinet

            MacState sMovingToCB1HomeFromDrawer = NewState(EnumMacBoxTransferState.MovingToCB1HomeFromDrawer);

            #endregion Return To CB Home From Cabinet
            #endregion CB1

            #region CB2
            #region Move To Cabinet

            #endregion Move To Cabinet

            #region Clamping At Cabinet

            #endregion Clamping At Cabinet

            #region Return To CB Home Clamped From Cabinet

            #endregion Return To CB Home Clamped From Cabinet

            #region Move To Cabinet Fro Release

            #endregion Move To Cabinet Fro Release

            #region Releasing At Cabinet

            #endregion Releasing At Cabinet

            #region Return To CB Home From Cabinet


            #endregion Return To CB Home From Cabinet
            #endregion CB2
            #endregion State

            #region Transition
            MacTransition tStart_DeviceInitial = NewTransition(sStart, sDeviceInitial, EnumMacBoxTransferTransition.Start_DeviceInitial);
            MacTransition tDeviceInitial_CB1Home = NewTransition(sDeviceInitial, sCB1Home, EnumMacBoxTransferTransition.DeviceInitial_CB1Home);

           // MacTransition tCB1Home_NULL = NewTransition(sCB1Home, null, EnumMacBoxTransferTransition.StandbyAtCB1Home);
            MacTransition tCB1Home_NULL = NewTransition(sCB1Home, null, EnumMacBoxTransferTransition.CB1Home_NULL);

            //MacTransition tCB1HomeClamped_NULL = NewTransition(sCB1HomeClamped, null, EnumMacBoxTransferTransition.StandbyAtCB1HomeClamped);
            MacTransition tCB1HomeClamped_NULL = NewTransition(sCB1HomeClamped, null, EnumMacBoxTransferTransition.CB1HomeClamped_NULL);


            #region Change Direction
            #endregion Change Direction

            #region OS
            //MacTransition tCB1Home_MovingToOpenStage = NewTransition(sCB1Home, sMovingToOpenStage, EnumMacBoxTransferTransition.MoveToOpenStage);
            MacTransition tCB1Home_MovingToOpenStage = NewTransition(sCB1Home, sMovingToOpenStage, EnumMacBoxTransferTransition.CB1Home_MovingToOpenStage);

            //MacTransition tMovingToOpenStage_OpenStageClamping = NewTransition(sMovingToOpenStage, sOpenStageClamping, EnumMacBoxTransferTransition.ClampInOpenStage);
            MacTransition tMovingToOpenStage_OpenStageClamping = NewTransition(sMovingToOpenStage, sOpenStageClamping, EnumMacBoxTransferTransition.MovingToOpenStage_OpenStageClamping);

            //MacTransition tOpenStageClamping_MovingToCB1HomeClampedFromOpenStage = NewTransition(sOpenStageClamping, sMovingToCB1HomeClampedFromOpenStage, EnumMacBoxTransferTransition.MoveToCB1HomeClampedFromOpenStage);
            MacTransition tOpenStageClamping_MovingToCB1HomeClampedFromOpenStage = NewTransition(sOpenStageClamping, sMovingToCB1HomeClampedFromOpenStage, EnumMacBoxTransferTransition.OpenStageClamping_MovingToCB1HomeClampedFromOpenStage);

            
            //MacTransition tMovingToCB1HomeClampedFromOpenStage_CB1HomeClamped = NewTransition(sMovingToCB1HomeClampedFromOpenStage, sCB1HomeClamped, EnumMacBoxTransferTransition.StandbyAtCB1HomeClampedFromOpenStage);
            MacTransition tMovingToCB1HomeClampedFromOpenStage_CB1HomeClamped = NewTransition(sMovingToCB1HomeClampedFromOpenStage, sCB1HomeClamped, EnumMacBoxTransferTransition.MovingToCB1HomeClampedFromOpenStage_CB1HomeClamped);

            //MacTransition tCB1HomeClamped_MovingToOpenStageForRelease = NewTransition(sCB1HomeClamped, sMovingToOpenStageForRelease, EnumMacBoxTransferTransition.MoveToOpenStageForRelease);
            MacTransition tCB1HomeClamped_MovingToOpenStageForRelease = NewTransition(sCB1HomeClamped, sMovingToOpenStageForRelease, EnumMacBoxTransferTransition.CB1HomeClamped_MovingToOpenStageForRelease);

            //MacTransition tMovingToOpenStageForRelease_OpenStageReleasing = NewTransition(sMovingToOpenStageForRelease, sOpenStageReleasing, EnumMacBoxTransferTransition.ReleaseInOpenStage);
            MacTransition tMovingToOpenStageForRelease_OpenStageReleasing = NewTransition(sMovingToOpenStageForRelease, sOpenStageReleasing, EnumMacBoxTransferTransition.MovingToOpenStageForRelease_OpenStageReleasing);

            //   MacTransition tOpenStageReleasing_MovingToCB1HomeFromOpenStage = NewTransition(sOpenStageReleasing, sMovingToCB1HomeFromOpenStage, EnumMacBoxTransferTransition.MoveToCB1HomeFromOpenStage);
            MacTransition tOpenStageReleasing_MovingToCB1HomeFromOpenStage = NewTransition(sOpenStageReleasing, sMovingToCB1HomeFromOpenStage, EnumMacBoxTransferTransition.OpenStageReleasing_MovingToCB1HomeFromOpenStage);

            //MacTransition tMovingToCB1HomeFromOpenStage_CB1Home = NewTransition(sMovingToCB1HomeFromOpenStage, sCB1Home, EnumMacBoxTransferTransition.StandbyAtCB1HomeFromOpenStage);
            MacTransition tMovingToCB1HomeFromOpenStage_CB1Home = NewTransition(sMovingToCB1HomeFromOpenStage, sCB1Home, EnumMacBoxTransferTransition.MovingToCB1HomeFromOpenStage_CB1Home);
            #endregion OS

            #region Lock & Unlock
            // MacTransition tCB1Home_Locking = NewTransition(sCB1Home, sLocking, EnumMacBoxTransferTransition.MoveToLock);
            MacTransition tCB1Home_Locking = NewTransition(sCB1Home, sLocking, EnumMacBoxTransferTransition.CB1Home_Locking);

            //MacTransition tLocking_CB1Home = NewTransition(sLocking, sCB1Home, EnumMacBoxTransferTransition.StandbyAtCB1HomeFromLock);
            MacTransition tLocking_CB1Home = NewTransition(sLocking, sCB1Home, EnumMacBoxTransferTransition.Locking_CB1Home);

            //MacTransition tCB1Home_Unlocking = NewTransition(sCB1Home, sUnlocking, EnumMacBoxTransferTransition.MoveToUnlock);
            MacTransition tCB1Home_Unlocking = NewTransition(sCB1Home, sUnlocking, EnumMacBoxTransferTransition.CB1Home_Unlocking);

           // MacTransition tUnlocking_CB1Home = NewTransition(sUnlocking, sCB1Home, EnumMacBoxTransferTransition.StandbyAtCB1HomeFromUnlock);
            MacTransition tUnlocking_CB1Home = NewTransition(sUnlocking, sCB1Home, EnumMacBoxTransferTransition.Unlocking_CB1Home);
            #endregion Lock & Unlock

            #region CB1
            #region Get


            MacTransition tCB1Home_MovingToDrawer = NewTransition(sCB1Home, sMovingToDrawer, EnumMacBoxTransferTransition.CB1Home_MovingToDrawer);

            //MacTransition tMovingToDrawer_DrawerClamping = NewTransition(sMovingToDrawer, sDrawerClamping, EnumMacBoxTransferTransition.ClampAtDrawer);
            MacTransition tMovingToDrawer_DrawerClamping = NewTransition(sMovingToDrawer, sDrawerClamping, EnumMacBoxTransferTransition.MovingToDrawer_DrawerClamping);


            //MacTransition transitionDrawerClamping_MovingToCB1HomeClampedFromDrawer = NewTransition(sDrawerClamping, sMovingToCB1HomeClampedFromDrawer, EnumMacBoxTransferTransition.MoveToCB1HomeClampedFromDrawer);
            MacTransition tDrawerClamping_MovingToCB1HomeClampedFromDrawer = NewTransition(sDrawerClamping, sMovingToCB1HomeClampedFromDrawer, EnumMacBoxTransferTransition.DrawerClamping_MovingToCB1HomeClampedFromDrawer);


            //MacTransition transitionMovingToCB1HomeClampedFromDrawer_CB1HomeClamped = NewTransition(sMovingToCB1HomeClampedFromDrawer, sCB1HomeClamped, EnumMacBoxTransferTransition.StandbyAtCB1HomeClampedFromDrawer);
            MacTransition tMovingToCB1HomeClampedFromDrawer_CB1HomeClamped = NewTransition(sMovingToCB1HomeClampedFromDrawer, sCB1HomeClamped, EnumMacBoxTransferTransition.MovingToCB1HomeClampedFromDrawer_CB1HomeClamped);
            #endregion Get

            #region Release

            //MacTransition transitionCB1HomeClamped_MovingToDrawerForRelease = NewTransition(sCB1HomeClamped, sMovingToDrawerForRelease, EnumMacBoxTransferTransition.MoveToDrawerForRelease);
            MacTransition tCB1HomeClamped_MovingToDrawerForRelease = NewTransition(sCB1HomeClamped, sMovingToDrawerForRelease, EnumMacBoxTransferTransition.CB1HomeClamped_MovingToDrawerForRelease);


            //MacTransition transitionMovingToDrawerForRelease_DrawerReleasing = NewTransition(sMovingToDrawerForRelease, sDrawerReleasing, EnumMacBoxTransferTransition.ReleaseAtDrawer);
            MacTransition tMovingToDrawerForRelease_DrawerReleasing = NewTransition(sMovingToDrawerForRelease, sDrawerReleasing, EnumMacBoxTransferTransition.MovingToDrawerForRelease_DrawerReleasing);

            //MacTransition transitionDrawerReleasing_MovingToCB1HomeFromDrawer = NewTransition(sDrawerReleasing, sMovingToCB1HomeFromDrawer, EnumMacBoxTransferTransition.MoveToCB1HomeFromDrawer);
            MacTransition tDrawerReleasing_MovingToCB1HomeFromDrawer = NewTransition(sDrawerReleasing, sMovingToCB1HomeFromDrawer, EnumMacBoxTransferTransition.DrawerReleasing_MovingToCB1HomeFromDrawer);


            //MacTransition transitionMovingToCB1HomeFromDrawer_CB1Home = NewTransition(sMovingToCB1HomeFromDrawer, sCB1Home, EnumMacBoxTransferTransition.StandbyAtCB1HomeFromDrawer);
            MacTransition tMovingToCB1HomeFromDrawer_CB1Home = NewTransition(sMovingToCB1HomeFromDrawer, sCB1Home, EnumMacBoxTransferTransition.MovingToCB1HomeFromDrawer_CB1Home);
            #endregion Release
            #endregion CB1

            #region CB2
            #region Get


            #endregion Get

            #region Release


            #endregion Release
            #endregion CB2
            #endregion Transition

            #region State Register OnEntry OnExit
            sStart.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("State: [sStart.OnEntry]");
                SetCurrentState((MacState)sender);

                // from: sStart, to: sDeviceInitial
                var transition = tStart_DeviceInitial;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        OnEntryCheck();
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
            {
                Debug.WriteLine("State: [sStart.OnExit]");
            };
            sDeviceInitial.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("State: [sDeviceInitial.OnEntry]");
                SetCurrentState((MacState)sender);

                //from: sInitial, to: sCB1Home
                var transition = tDeviceInitial_CB1Home;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        OnEntryCheck();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalBoxTransfer.Initial();
                        }
                        catch (Exception ex)
                        {
                            throw new BoxTransferInitialFailException(ex.Message);
                        }
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
            sDeviceInitial.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [sDeviceInitial.OnExit]");
            };

            sCB1Home.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("State: [sCB1Home.OnEntry]");
                SetCurrentState((MacState)sender);


                // from: sCB1Home(last state)
                var transition = tCB1Home_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        OnEntryCheck();
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
            sCB1Home.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [sCB1Home.OnExit]");
            };


            sCB1HomeClamped.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("State: [sCB1HomeClamped.OnEntry]");
                SetCurrentState((MacState)sender);
                // from: sCB1HomeClamped,   to: null,
                var transition = tCB1HomeClamped_NULL;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        OnEntryCheck();
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
            sCB1HomeClamped.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [sCB1HomeClamped.OnExit]");

            };

            #region Change Direction
            sChangingDirectionToCB1Home.OnEntry += (sender, e) =>
            {
            };
            sChangingDirectionToCB1Home.OnExit += (sender, e) =>
            {
            };
            sChangingDirectionToCB1HomeClamped.OnEntry += (sender, e) =>
            {
            };
            sChangingDirectionToCB1HomeClamped.OnExit += (sender, e) =>
            { };

            #endregion Change Direction

            #region OS
            sMovingToOpenStage.OnEntry += (sender, e) =>
            {

                var eventArgs = (MacStateMovingToOpenStageEntryEventArgs)e;
                var boxType = eventArgs.BoxType;
                Debug.WriteLine("State: [sMovingToOpenStage.OnEntry], BoxType=" + boxType);
                SetCurrentState((MacState)sender);

                //from: sMovingToOpenStage, to:sOpenStageClamping
                var transition = tMovingToOpenStage_OpenStageClamping;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        OnEntryCheck();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                          var intrudeResult=  HalOpenStage.ReadRobotIntrude(true, null); // Fake OK
                          HalBoxTransfer.RobotMoving(true); // Fake OK

                            // path: @"D:\Positions\BTRobot\Cabinet_01_Home_Forward_OpenStage_GET.json"
                          var path = pathObj.FromCabinet01HomeToOpenStage_GET_PathFile();
                          var moveResult=HalBoxTransfer.ExePathMove(path); // Fake OK
                          HalBoxTransfer.RobotMoving(false); // Fake Ok
                        }
                        catch (Exception ex)
                        {
                            throw new BoxTransferPathMoveFailException(ex.Message);
                        }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },

                    NextStateEntryEventArgs = new MacStateOpenStageClampingEntryEventArgs(boxType),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMovingToOpenStage.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [sMovingToOpenStage.OnExit]");
            };
            sOpenStageClamping.OnEntry += (sender, e) =>
            {
                var eventArgs = (MacStateOpenStageClampingEntryEventArgs)e;
                var boxType = eventArgs.BoxType;
                SetCurrentState((MacState)sender);
                Debug.WriteLine("State: [sOpenStageClamping.OnEntry], BoxType=" + boxType);

                // from: sOpenStageClamping, to: sMovingToCB1HomeClampedFromOpenStage
                var transition = tOpenStageClamping_MovingToCB1HomeClampedFromOpenStage;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        OnEntryCheck();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                          var clampResult=  HalBoxTransfer.Clamp((uint)boxType); // Fake OK
                        }
                        catch (Exception ex)
                        {
                            throw new BoxTransferPLCExecuteFailException(ex.Message);
                        }
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
            sOpenStageClamping.OnExit += (sender, e) =>
            {
                Debug.WriteLine("state: [sOpenStageClamping.OnExit]");
            };
            sMovingToCB1HomeClampedFromOpenStage.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("State:[sMovingToCB1HomeClampedFromOpenStage.OnEntry]");
                SetCurrentState((MacState)sender);

                // from: sMovingToCB1HomeClampedFromOpenStage, to: sCB1HomeClamped
                var transition = tMovingToCB1HomeClampedFromOpenStage_CB1HomeClamped;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        OnEntryCheck();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalBoxTransfer.RobotMoving(true); // Fake OK

                            // path: @"D:\Positions\BTRobot\OpenStage_Backward_Cabinet_01_Home_GET.json"
                            var path = pathObj.FromOpenStageToCabinet01Home_GET_PathFile();
                            var moveREsult=  HalBoxTransfer.ExePathMove(path);
                            HalBoxTransfer.RobotMoving(false); // Fake OK
                            var intrudeResult=HalOpenStage.ReadRobotIntrude(false, null); // Fake OK
                        }
                        catch (Exception ex)
                        {
                            throw new BoxTransferPathMoveFailException(ex.Message);
                        }
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
            sMovingToCB1HomeClampedFromOpenStage.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State:[sMovingToCB1HomeClampedFromOpenStage.OnExit]");
            };

            sMovingToOpenStageForRelease.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("State: [sMovingToOpenStageForRelease.OnEntry]");
                SetCurrentState((MacState)sender);

                // from: MovingToOpenStageForRelease, to: sOpenStageReleasing
                var transition = tMovingToOpenStageForRelease_OpenStageReleasing;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        OnEntryCheck();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            var intrudeResult=HalOpenStage.ReadRobotIntrude(true, null);  // Fake OK
                            HalBoxTransfer.RobotMoving(true);   // Fake OK

                            // Path: @"D:\Positions\BTRobot\Cabinet_01_Home_Forward_OpenStage_PUT.json"
                            var path = pathObj.FromCabinet01HomeToOpenStage_PUT_PathFile();
                            var moveResult=HalBoxTransfer.ExePathMove(path);  // Fake OK
                            HalBoxTransfer.RobotMoving(false);   // Fake OK
                        }
                        catch (Exception ex)
                        {
                            throw new BoxTransferPathMoveFailException(ex.Message);
                        }
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
            sMovingToOpenStageForRelease.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [sMovingToOpenStageForRelease.OnExit]");
            };
            sOpenStageReleasing.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("State: [sOpenStageReleasing.OnEntry]");
                SetCurrentState((MacState)sender);

                // from: sOpenStageReleasing, to: sMovingToCB1HomeFromOpenStage
                var transition = tOpenStageReleasing_MovingToCB1HomeFromOpenStage;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        OnEntryCheck();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            var unclampResult=HalBoxTransfer.Unclamp();  // Fake OK
                        }
                        catch (Exception ex)
                        {
                            throw new BoxTransferPLCExecuteFailException(ex.Message);
                        }

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
            sOpenStageReleasing.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [sOpenStageReleasing.OnExit]");
            };
            sMovingToCB1HomeFromOpenStage.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("State: [sMovingToCB1HomeFromOpenStage.OnEntry]");
                SetCurrentState((MacState)sender);

                // from: sMovingToCB1HomeFromOpenStage, to: sCB1Home
                var transition = tMovingToCB1HomeFromOpenStage_CB1Home;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        OnEntryCheck();
                        return true;
                    },
                    Action = (parameter) =>
                     {
                         try
                         {
                             HalBoxTransfer.RobotMoving(true);   // Fake OK

<<<<<<< HEAD
                             // path: @"D:\Positions\BTRobot\OpenStage_Backward_Cabinet_01_Home_PUT.json"
                             var path = pathObj.FromOpenStageToCabinet01Home_PUT_PathFile();
                             HalBoxTransfer.ExePathMove(path);
                             HalBoxTransfer.RobotMoving(false);  // Fake OK
                             HalOpenStage.ReadRobotIntrude(false, null);   // Fake OK
                         }
=======
                            // path: @"D:\Positions\BTRobot\OpenStage_Backward_Cabinet_01_Home_PUT.json"
                            var path = pathObj.FromOpenStageToCabinet01Home_PUT_PathFile();
                            var moveREsult= HalBoxTransfer.ExePathMove(path);
                             HalBoxTransfer.RobotMoving(false);  // Fake OK
                            var intrudeResult =HalOpenStage.ReadRobotIntrude(false, null);   // Fake OK
                        }
>>>>>>> ceb2f2d084e0615e5e1369ec2901ac10f2118c99
                         catch (Exception ex)
                         {
                             throw new BoxTransferPathMoveFailException(ex.Message);
                         }

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
            sMovingToCB1HomeFromOpenStage.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [sMovingToCB1HomeFromOpenStage.OnExit]");
            };
            #endregion OS

            #region Lock & Unlock
            sLocking.OnEntry += (sender, e) =>
            {
                var eventArgs = (MacStateMoveToLockEntryEventArgs)e;
                var boxType = eventArgs.BoxType;
                Debug.WriteLine("State: [sLocking.OnEntry], BoxType=" + boxType.ToString());
                SetCurrentState((MacState)sender);

                //from: sLocking, to: sCB1Home
                var transition = tLocking_CB1Home;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        OnEntryCheck();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {

                            // path:  @"D:\Positions\BTRobot\Cabinet_01_Home.json"
                            var path = pathObj.Cabinet01HomePathFile();
                            if (!HalBoxTransfer.CheckPosition(path))  // Fake OK
                                throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to lock box.");
                            var intrudeResult= HalOpenStage.ReadRobotIntrude(true, null);  // Fake OK
                            HalBoxTransfer.RobotMoving(true); // Fake OK
                            if (boxType == BoxType.IronBox)
                            {  // 鐵盒 

                                // path: @"D:\Positions\BTRobot\LockIronBox.json"
                                path = pathObj.LockIronBoxPathFile();
                                var moveResult=HalBoxTransfer.ExePathMove(path); // Fake OK
                            }
                            else if (boxType == BoxType.CrystalBox)
                            {   // 水晶盒

                                //path: @"D:\Positions\BTRobot\LockCrystalBox.json"
                                path = pathObj.LockCrystalBoxPathFile();
                                var moveResult=HalBoxTransfer.ExePathMove(path);  // Fake OK
                            }
                            else
                            {   // 非水晶盒與也非鐵盒
                                HalBoxTransfer.RobotMoving(false);   // Fake OK
                                HalOpenStage.ReadRobotIntrude(false, null); // Fake OK
                                if (boxType != 0)//測試Fake State Machine先用BoxType.DontCare，因此先略過BoxType=0的錯誤
                                    throw new Exception("Unknown box type, can not move to lock box.");
                            }

                            intrudeResult= HalOpenStage.ReadRobotIntrude(false, null);  // Fake OK
                            HalBoxTransfer.RobotMoving(false);  // Fake OK
                        }
                        catch (Exception ex)
                        {
                            throw new BoxTransferPathMoveFailException(ex.Message);
                        }
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
            sLocking.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [sLocking.OnExit]");
            };
            sUnlocking.OnEntry += (sender, e) =>
            {

                var eventArgs = (MacStateMoveToUnLockEntryEventArgs)e;
                var boxType = eventArgs.BoxType;
                Debug.WriteLine("State: [sUnlocking.OnEntry], BoxType=" + boxType.ToString());
                SetCurrentState((MacState)sender);

                // from: sUnlocking, to: sCB1Home
                var transition = tUnlocking_CB1Home;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        OnEntryCheck();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            //path: @"D:\Positions\BTRobot\Cabinet_01_Home.json"
                            var path = pathObj.Cabinet01HomePathFile();
                            if (!HalBoxTransfer.CheckPosition(path))   // Fake OK
                            {
                                throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to unlock box.");
                            }
                           var intrudeResult= HalOpenStage.ReadRobotIntrude(true, null); // Fake OK
                            HalBoxTransfer.RobotMoving(true);  // Fake OK
                            if (boxType == BoxType.IronBox)
                            {
                                // path: @"D:\Positions\BTRobot\UnlockIronBox.json"
                                path = pathObj.UnlockIronBoxPathFile();
                                var moveResult=HalBoxTransfer.ExePathMove(path);  // Fake OK
                            }
                            else if (boxType == BoxType.CrystalBox)
                            {
                                //path: @"D:\Positions\BTRobot\UnlockCrystalBox.json"
                                path = pathObj.UnlockCrystalBoxPathFile();
                                var moveResult=HalBoxTransfer.ExePathMove(path);
                            }
                            else
                            {
                                HalBoxTransfer.RobotMoving(false); // Fake OK
<<<<<<< HEAD
                                HalOpenStage.ReadRobotIntrude(false, null);  // Fake OK
                                if (boxType != 0)//測試Fake State Machine先用BoxType.DontCare，因此先略過BoxType=0的錯誤
                                    throw new Exception("Unknown box type, can not move to unlock box.");
=======
                                var intrudeREsult=HalOpenStage.ReadRobotIntrude(false, null);  // Fake OK
                                throw new Exception("Unknown box type, can not move to unlock box.");
>>>>>>> ceb2f2d084e0615e5e1369ec2901ac10f2118c99
                            }
                           intrudeResult= HalOpenStage.ReadRobotIntrude(false, null);  // Fake OK
                            HalBoxTransfer.RobotMoving(false);  // Fake OK
                        }
                        catch (Exception ex)
                        {
                            throw new BoxTransferPathMoveFailException(ex.Message);
                        }
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
            sUnlocking.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [sUnlocking.OnExit]");
            };
            #endregion Lock & Unlock

            #region CB1




            #region Move To Cabinet

            sMovingToDrawer.OnEntry += (sender, e) =>
            {

                var eventArgs = (MacStateMovingToDrawerEntryEventArgs)e;
                var drawerLocation = eventArgs.DrawerLocation;
                var boxType = eventArgs.BoxType;
                Debug.WriteLine("State: [sMovingToDrawer.OnEntry], DrawerLocation: " + drawerLocation + ", BoxType: " + boxType);
                SetCurrentState((MacState)sender, drawerLocation);

                //var  transition = tMovingToCabinet0101_Cabinet0101Clamping;
                var transition = tMovingToDrawer_DrawerClamping;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        OnEntryCheck();
                        return true;
                    },
                    Action = (parameter) =>
                     {
                         try
                         {
                             // Robot 目前不在Cabinet 1 Home
                             // path="D:\Positions\BTRobot\Cabinet_01_Home.json"
                             var path = pathObj.Cabinet01HomePathFile();
                             if (!HalBoxTransfer.CheckPosition(path)) // Fake OK
                             { throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to get box."); }

                             // 判斷 目標 Drawer 是屬於哪個 Cabinet(1 or 2)? 
                             var cabinetHome = drawerLocation.GetCabinetHomeCode();
                             if (cabinetHome.Item1)
                             {
                                 HalBoxTransfer.RobotMoving(true);  // Fake OK
                                 if (cabinetHome.Item2 == BoxrobotTransferLocation.Cabinet_01_Home)
                                 {  // Cabinet 1

<<<<<<< HEAD
                                     //path: @"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_01_01_GET.json"
                                     path = pathObj.FromCabinet01HomeToDrawer_GET_PathFile(drawerLocation);
                                     HalBoxTransfer.ExePathMove(path);  // Fake OK
                                 }
=======
                                    //path: @"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_01_01_GET.json"
                                    path = pathObj.FromCabinet01HomeToDrawer_GET_PathFile(drawerLocation);
                                    var moveResult= HalBoxTransfer.ExePathMove(path);  // Fake OK
                                }
>>>>>>> ceb2f2d084e0615e5e1369ec2901ac10f2118c99
                                 else //if(cabinetHome.Item2 == BoxrobotTransferLocation.Cabinet_02_Home)
                                 {  // Cabinet 2

<<<<<<< HEAD
                                     // path:  @"D:\Positions\BTRobot\Cabinet_02_Home.json"
                                     path = pathObj.Cabinet02HomePathFile();
                                     HalBoxTransfer.ExePathMove(path);  // Fake OK

                                     // path: @"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_07_01_GET.json"
                                     path = pathObj.FromCabinet02HomeToDrawer_GET_PathFile(drawerLocation);
                                     HalBoxTransfer.ExePathMove(path); // Fake OK
                                 }
=======
                                    // path:  @"D:\Positions\BTRobot\Cabinet_02_Home.json"
                                    path = pathObj.Cabinet02HomePathFile();
                                    var moveResult= HalBoxTransfer.ExePathMove(path);  // Fake OK

                                    // path: @"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_07_01_GET.json"
                                    path = pathObj.FromCabinet02HomeToDrawer_GET_PathFile(drawerLocation);
                                   moveResult=  HalBoxTransfer.ExePathMove(path); // Fake OK
                                }
>>>>>>> ceb2f2d084e0615e5e1369ec2901ac10f2118c99
                                 HalBoxTransfer.RobotMoving(false);
                             }
                         }
                         catch (Exception ex)
                         {
                             throw new BoxTransferPathMoveFailException(ex.Message);
                         }
                     },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateDrawerClampingEntryEventArgs(eventArgs.DrawerLocation, eventArgs.BoxType),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMovingToDrawer.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [sMovingToDrawer.OnExit]");
            };



            #endregion Move To Cabinet

            #region Clamping At Cabinet
            sDrawerClamping.OnEntry += (sender, e) =>
            {
                var eventArgs = (MacStateDrawerClampingEntryEventArgs)e;
                var drawerLocation = eventArgs.DrawerLocation;
                var boxType = eventArgs.BoxType;
                Debug.WriteLine("State: [stateDrawerClamping.OnEntry], DrawerLocation: " + drawerLocation + ", BoxType: " + boxType);
                SetCurrentState((MacState)sender, drawerLocation);
                var transition = tDrawerClamping_MovingToCB1HomeClampedFromDrawer;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        OnEntryCheck();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            // Clamp
                            HalBoxTransfer.Clamp((uint)boxType); // Fake OK
                        }
                        catch (Exception ex)
                        {
                            throw new BoxTransferPLCExecuteFailException(ex.Message);
                        }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateMovingToCB1HomeClampedFromDrawerEntryEventArgs(drawerLocation, boxType),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sDrawerClamping.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [stateDrawerClamping.OnExit]");
            };

            #endregion Clamping At Cabinet

            #region Return To CB Home Clamped From Cabinet
            sMovingToCB1HomeClampedFromDrawer.OnEntry += (sender, e) =>
            {
                var eventArgs = (MacStateMovingToCB1HomeClampedFromDrawerEntryEventArgs)e;
                var drawerLocation = eventArgs.DrawerLocation;
                var boxType = eventArgs.BoxType;
                Debug.WriteLine("State: [stateMovingToCB1HomeClampedFromDrawer.OnEntry], DrawerLocation: " + drawerLocation + ",  BoxType: " + boxType);
                SetCurrentState((MacState)sender, drawerLocation);

                // from: stateMovingToCB1HomeClampedFromDrawer, to: sCB1HomeClamped
                var transition = tMovingToCB1HomeClampedFromDrawer_CB1HomeClamped;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        OnEntryCheck();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            /**
                             Cabinet1Home: 
                                 HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_01_01_Backward_Cabinet_01_Home_GET.json");
                             Cabinet2Home:
                                 HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_07_01_Backward_Cabinet_02_Home_GET.json");
                                 HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                             */

                            var cabinetHome = drawerLocation.GetCabinetHomeCode();

                            if (cabinetHome.Item1)
                            {
                                HalBoxTransfer.RobotMoving(true); // Fake OK
                                if (cabinetHome.Item2 == BoxrobotTransferLocation.Cabinet_01_Home)
                                {
                                    // path: @"D:\Positions\BTRobot\Drawer_01_01_Backward_Cabinet_01_Home_GET.json"
                                    var path = pathObj.FromDrawerToCabinet01Home_GET_PathFile(drawerLocation);
                                    HalBoxTransfer.ExePathMove(path);  // Fake OK
                                }
                                else //if (cabinetHome.Item2 == BoxrobotTransferLocation.Cabinet_02_Home) // 屬於Cabinet2 所管
                                {
                                    //path: @"D:\Positions\BTRobot\Drawer_07_01_Backward_Cabinet_02_Home_GET.json"
                                    var path = pathObj.FromDrawerToCabinet02Home_GET_PathFile(drawerLocation);
                                    HalBoxTransfer.ExePathMove(path); // Fake OK

                                    // path: @"D:\Positions\BTRobot\Cabinet_01_Home.json"
                                    path = pathObj.Cabinet01HomePathFile();
                                    HalBoxTransfer.ExePathMove(path); // Fake OK
                                }
                                HalBoxTransfer.RobotMoving(false); // Fake OK
                            }

                        }
                        catch (Exception ex)
                        {
                            throw new BoxTransferPathMoveFailException(ex.Message);
                        }
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
            sMovingToCB1HomeClampedFromDrawer.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [stateMovingToCB1HomeClampedFromDrawer.OnExit]");
            };
            #endregion Return To CB Home Clamped From Cabinet

            #region Move To Cabinet Fro Release
            sMovingToDrawerForRelease.OnEntry += (sender, e) =>
            {
                var eventArgs = (MacStateMovingToDrawerForReleaseEntryEventArgs)e;
                var drawerLocation = eventArgs.DrawerLocation;
                var boxType = eventArgs.BoxType;
                Debug.WriteLine("State: [stateMovingToDrawerForRelease.OnEntry], DrawerNumber=" + drawerLocation + ", BoxType=" + boxType);
                SetCurrentState((MacState)sender, drawerLocation);

                // from: stateMovingToDrawerForRelease, to: stateDrawerReleasing
                var transition = tMovingToDrawerForRelease_DrawerReleasing;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        OnEntryCheck();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            var path = pathObj.Cabinet01HomePathFile();//D:\Positions\BTRobot\Cabinet_01_Home.json
                            if (!HalBoxTransfer.CheckPosition(path)) // Fake OK
                            { throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to put box."); }

                            var cabinetHome = drawerLocation.GetCabinetHomeCode();
                            if (cabinetHome.Item1 == true)
                            {
                                HalBoxTransfer.RobotMoving(true); // Fake OK
                                if (cabinetHome.Item2 == BoxrobotTransferLocation.Cabinet_01_Home)
                                {
                                    // path: @"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_01_01_PUT.json"
                                    path = pathObj.FromCabinet01HomeToDrawer_PUT_PathFile(drawerLocation);
                                    HalBoxTransfer.ExePathMove(pathObj.FromCabinet01HomeToDrawer_PUT_PathFile(drawerLocation)); // Fake OK
                                }
                                else if (cabinetHome.Item2 == BoxrobotTransferLocation.Cabinet_02_Home)
                                {
                                    // path: @"D:\Positions\BTRobot\Cabinet_02_Home.json"
                                    path = pathObj.Cabinet02HomePathFile();
                                    HalBoxTransfer.ExePathMove(path); // Fake OK

                                    // path: @"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_07_01_PUT.json"
                                    path = pathObj.FromCabinet02HomeToDrawer_PUT_PathFile(drawerLocation);
                                    HalBoxTransfer.ExePathMove(path); // Fake OK
                                }
                                HalBoxTransfer.RobotMoving(false); // Fake OK
                            }
                            else { }
                        }
                        catch (Exception ex)
                        {
                            throw new BoxTransferPathMoveFailException(ex.Message);
                        }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateDrawerReleasingEntryArgs(drawerLocation, boxType),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMovingToDrawerForRelease.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [stateMovingToDrawerForRelease.OnExit]");
            };


            #endregion Move To Cabinet Fro Release

            #region Releasing At Cabinet
            sDrawerReleasing.OnEntry += (sender, e) =>
            {
                var eventArgs = (MacStateDrawerReleasingEntryArgs)e;
                var drawerLocation = eventArgs.DrawerLocation;
                var boxType = eventArgs.BoxType;
                Debug.WriteLine("State: [stateDrawerReleasing.OnEntry], DrawerNumber=" + drawerLocation + ", BoxType= " + boxType);

                SetCurrentState((MacState)sender, drawerLocation);

                // from: stateDrawerReleasing, to: stateMovingToCB1HomeFromDrawer
                var transition = tDrawerReleasing_MovingToCB1HomeFromDrawer;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        OnEntryCheck();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            HalBoxTransfer.Unclamp(); // Fake OK
                        }
                        catch (Exception ex)
                        {
                            throw new BoxTransferPLCExecuteFailException(ex.Message);
                        }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (thisState, ex) =>
                    { // TODO: do something
                    },
                    NextStateEntryEventArgs = new MacStateMovingToCB1HomeFromDrawerEntryEventArgs(drawerLocation, boxType),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);

            };
            sDrawerReleasing.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [stateDrawerReleasing.OnExit]");
            };

            #endregion Releasing At Cabinet

            #region Return To CB Home From Cabinet
            sMovingToCB1HomeFromDrawer.OnEntry += (sender, e) =>
            {
                var eventArgs = (MacStateMovingToCB1HomeFromDrawerEntryEventArgs)e;
                var drawerLocation = eventArgs.DrawerLocation;
                var boxType = (BoxType)eventArgs.BoxType;
                Debug.WriteLine("State: [stateMovingToCB1HomeFromDrawer.OnEntry], + DrawerNumber=" + drawerLocation + ", BoxType=" + boxType);
                SetCurrentState((MacState)sender, drawerLocation);

                // from: stateMovingToCB1HomeFromDrawer, to: sCB1Home
                var transition = tMovingToCB1HomeFromDrawer_CB1Home;
                TriggerMember triggerMember = new TriggerMember
                {
                    Guard = () =>
                    {
                        OnEntryCheck();
                        return true;
                    },
                    Action = (parameter) =>
                    {
                        try
                        {
                            var cabinetHome = drawerLocation.GetCabinetHomeCode();
                            if (cabinetHome.Item1)
                            {
                                HalBoxTransfer.RobotMoving(true); // Fake OK
                                if (cabinetHome.Item2 == BoxrobotTransferLocation.Cabinet_01_Home)
                                {
                                    // path: @"D:\Positions\BTRobot\Drawer_01_01_Backward_Cabinet_01_Home_PUT.json"
                                    var path = pathObj.FromDrawerToCabinet01Home_PUT_PathFile(drawerLocation);
                                    HalBoxTransfer.ExePathMove(path); // Fake OK
                                }
                                else //if(cabinetHome.Item2 == BoxrobotTransferLocation.Cabinet_02_Home)
                                {
                                    // path: @"D:\Positions\BTRobot\Drawer_07_01_Backward_Cabinet_02_Home_PUT.json"
                                    var path = pathObj.FromDrawerToCabinet02Home_PUT_PathFile(drawerLocation);
                                    HalBoxTransfer.ExePathMove(path); // Fake OK

                                    // path: (@"D:\Positions\BTRobot\Cabinet_01_Home.json"
                                    path = pathObj.Cabinet01HomePathFile();
                                    HalBoxTransfer.ExePathMove(path); // Fake OK
                                }
                                HalBoxTransfer.RobotMoving(false); // Fake OK
                            }

                        }
                        catch (Exception ex)
                        {
                            throw new BoxTransferPathMoveFailException(ex.Message);
                        }
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
            sMovingToCB1HomeFromDrawer.OnExit += (sender, e) =>
            {

                Debug.WriteLine("State:[ stateMovingToCB1HomeFromDrawer.OnExit]");
            };


            #endregion Return To CB Home From Cabinet
            #endregion CB1

            #endregion State Register OnEntry OnExit
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

        /// <summary>
        /// 只取該Assembly需要確認的Alarm訊號，其他Assembly的Alarm訊號註解
        /// </summary>
        /// <returns></returns>
        private bool CheckAssemblyAlarmSignal()
        {
            //var CB_Alarm = HalUniversal.ReadAlarm_Cabinet();
            //var CC_Alarm = HalUniversal.ReadAlarm_CleanCh();
            //var CF_Alarm = HalUniversal.ReadAlarm_CoverFan();
            var BT_Alarm = HalUniversal.ReadAlarm_BTRobot();
            //var MTClampInsp_Alarm = HalUniversal.ReadAlarm_MTClampInsp();
            //var MT_Alarm = HalUniversal.ReadAlarm_MTRobot();
            //var IC_Alarm = HalUniversal.ReadAlarm_InspCh();
            //var LP_Alarm = HalUniversal.ReadAlarm_LoadPort();
            //var OS_Alarm = HalUniversal.ReadAlarm_OpenStage();

            //if (CB_Alarm != "") throw new CabinetPLCAlarmException(CB_Alarm);
            //if (CC_Alarm != "") throw new CleanChPLCAlarmException(CC_Alarm);
            //if (CF_Alarm != "") throw new UniversalCoverFanPLCAlarmException(CF_Alarm);
            if (BT_Alarm != "") throw new BoxTransferPLCAlarmException(BT_Alarm);
            //if (MTClampInsp_Alarm != "") throw new MTClampInspectDeformPLCAlarmException(MTClampInsp_Alarm);
            //if (MT_Alarm != "") throw new MaskTransferPLCAlarmException(MT_Alarm);
            //if (IC_Alarm != "") throw new InspectionChPLCAlarmException(IC_Alarm);
            //if (LP_Alarm != "") throw new LoadportPLCAlarmException(LP_Alarm);
            //if (OS_Alarm != "") throw new OpenStagePLCAlarmException(OS_Alarm);

            return true;
        }

        /// <summary>
        /// 只取該Assembly需要確認的Warning訊號，其他Assembly的Warning訊號註解
        /// </summary>
        /// <returns></returns>
        private bool CheckAssemblyWarningSignal()
        {
            //var CB_Warning = HalUniversal.ReadWarning_Cabinet();
            //var CC_Warning = HalUniversal.ReadWarning_CleanCh();
            //var CF_Warning = HalUniversal.ReadWarning_CoverFan();
            var BT_Warning = HalUniversal.ReadWarning_BTRobot();
            //var MTClampInsp_Warning = HalUniversal.ReadWarning_MTClampInsp();
            //var MT_Warning = HalUniversal.ReadWarning_MTRobot();
            //var IC_Warning = HalUniversal.ReadWarning_InspCh();
            //var LP_Warning = HalUniversal.ReadWarning_LoadPort();
            //var OS_Warning = HalUniversal.ReadWarning_OpenStage();

            //if (CB_Warning != "") throw new CabinetPLCWarningException(CB_Warning);
            //if (CC_Warning != "") throw new CleanChPLCWarningException(CC_Warning);
            //if (CF_Warning != "") throw new UniversalCoverFanPLCWarningException(CF_Warning);
            if (BT_Warning != "") throw new BoxTransferPLCWarningException(BT_Warning);
            //if (MTClampInsp_Warning != "") throw new MTClampInspectDeformPLCWarningException(MTClampInsp_Warning);
            //if (MT_Warning != "") throw new MaskTransferPLCWarningException(MT_Warning);
            //if (IC_Warning != "") throw new InspectionChPLCWarningException(IC_Warning);
            //if (LP_Warning != "") throw new LoadportPLCWarningException(LP_Warning);
            //if (OS_Warning != "") throw new OpenStagePLCWarningException(OS_Warning);

            return true;
        }

        private MacTransition EnumMacBoxTransferTransitionContainValue(string myValue)
        {
            if (!Enum.IsDefined(typeof(EnumMacBoxTransferTransition), myValue))
            { throw new BoxTransferException("Can not found " + myValue + " from EnumMacBoxTransferTransition list. "); }
            else
            { return Transitions[myValue]; }
        }

        private void OnEntryCheck()
        {
            CheckEquipmentStatus();
            CheckAssemblyAlarmSignal();
            CheckAssemblyWarningSignal();
        }
    }
}
