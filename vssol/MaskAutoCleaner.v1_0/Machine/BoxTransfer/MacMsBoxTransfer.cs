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
        BoxrobotTransferPathFile pathObj =new BoxrobotTransferPathFile(PositionInstance.BTR_Path) ;// new BoxrobotTransferPathFile(@"D:\Positions\BTRobot\");
        private IMacHalUniversal HalUniversal { get { return this.Mediater.GetCtrlMachine(EnumMachineID.MID_UNI_A_ASB.ToString()).HalAssembly as IMacHalUniversal; } }
        public IMacHalBoxTransfer HalBoxTransfer { get { return this.halAssembly as IMacHalBoxTransfer; } }
        private IMacHalOpenStage HalOpenStage { get { return this.Mediater.GetCtrlMachine(EnumMachineID.MID_OS_A_ASB.ToString()).HalAssembly as IMacHalOpenStage; } }
        public BoxrobotTransferLocation DrawerLocation { get; private set; }
        public void ResetState()
        { this.States[EnumMacMsBoxTransferState.Start.ToString()].DoEntry(new MacStateEntryEventArgs(null)); }

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

        //public MacState CurrentState { get { return _currentState; } }

        public MacMsBoxTransfer() { LoadStateMachine(); }


        //  MacMaskTransferUnitStateTimeOutController timeoutObj = new MacMaskTransferUnitStateTimeOutController();

        public void SystemBootup()
        {
            // state: sStart
            this.States[EnumMacMsBoxTransferState.Start.ToString()].ExecuteCommand(new MacStateEntryEventArgs(null));
        }
        public void Initial()
        {
            // State: sInitial
            this.States[EnumMacMsBoxTransferState.Initial.ToString()].ExecuteCommand(new MacStateEntryEventArgs(null));
        }
        public void MoveToLock(BoxType boxType)
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;

            // from:sCB1Home, to:sLocking
            transition = Transitions[EnumMacMsBoxTransferTransition.MoveToLock.ToString()];
            //var tempBoxType = BoxType.IronBox; // TODO: 假定的 BoxType, 以後補上 
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
                // NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                NextStateEntryEventArgs = new MacStateMoveToLockEntryEventArgs(boxType),
                ThisStateExitEventArgs = new MacStateExitEventArgs(),
            };
            transition.SetTriggerMembers(triggerMember);
            Trigger(transition);
        }
        public void MoveToUnlock(BoxType boxType)
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            // from: sCB1Home, to: sUnlocking
            transition = Transitions[EnumMacMsBoxTransferTransition.MoveToUnlock.ToString()];
            //uint uintTempBoxType = 1; // TODO: 假定的 BoxType, 以後補上 
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
                // NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                NextStateEntryEventArgs = new MacStateMoveToUnLockEntryEventArgs(boxType),
                ThisStateExitEventArgs = new MacStateExitEventArgs(),
            };
            transition.SetTriggerMembers(triggerMember);
            Trigger(transition);
        }
        public void MoveToOpenStageGet()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            //from: sCB1Home, to: sMovingToOpenStage
            transition = Transitions[EnumMacMsBoxTransferTransition.MoveToOpenStage.ToString()];

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
        public void MoveToOpenStagePut()
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;

            // from: sCB1HomeClamped, to: sMovingToOpenStageForRelease
            transition = Transitions[EnumMacMsBoxTransferTransition.MoveToOpenStageForRelease.ToString()];
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






        /**
        [Obsolete]
        /// <summary>
        /// 移動到指定Cabinet編號的位置取盒，Cabinet編號0101~0705
        /// </summary>
        /// <param name="CabinetNumber">0101~0705</param>
        public void MoveToCabinetGet(string CabinetNumber)
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = EnumMacMsBoxTransferTransitionContainValue("MoveToCB" + CabinetNumber);
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
            */

        /// <summary>移動到指定Cabinet編號的位置取盒，Cabinet編號0101~0705</summary>
        /// <remarks>
        /// <para>2020/09/22, King Add</para>
        /// <para>合併 State</para>
        /// </remarks>
        /// <param name="drawerLocation"></param>
        public void MoveToCabinetGet(BoxrobotTransferLocation drawerLocation)
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;

            // 從 CB1Home 移到指定的 Drawer
            //from: sCB1Home, to: stateMovingToDrawer
            transition = Transitions[EnumMacMsBoxTransferTransition.MoveToDrawer.ToString()];  //transitionCB1Home_MovingToDrawer
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
                NextStateEntryEventArgs = new MacStateMovingToDrawerEntryEventArgs(drawerLocation),
                ThisStateExitEventArgs = new MacStateExitEventArgs(),
            };
            transition.SetTriggerMembers(triggerMember);
            Trigger(transition);
        }

        /// <summary>
        /// 移動到指定Cabinet編號的位置放置，Cabinet編號0101~0705
        /// </summary>
        /// <remarks>
        /// <para>2020/09/22, King Add</para>
        /// <para>合併State 測試</para>
        /// </remarks>
        /// <param name="CabinetNumber">0101~0705</param>
        public void MoveToCabinetPut(BoxrobotTransferLocation drawerLocation)
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;

            //  transition = EnumMacMsBoxTransferTransitionContainValue("MoveToCB" + cabinetNumber.Item2 + "ForRelease");
            
            // from: sCB1HomeClamped, to: stateMovingToDrawerForRelease
            transition = Transitions[EnumMacMsBoxTransferTransition.MoveToDrawerForRelease.ToString()];  //transitionCB1HomeClamped_MovingToDrawerForRelease
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
                NextStateEntryEventArgs = new MacStateMovingToDrawerForReleaseEntryEventArgs(drawerLocation),
                ThisStateExitEventArgs = new MacStateExitEventArgs(),
            };
            transition.SetTriggerMembers(triggerMember);
            Trigger(transition);
        }

        /**
        [Obsolete]
        /// <summary>
        /// 移動到指定Cabinet編號的位置放置，Cabinet編號0101~0705
        /// </summary>
        /// <param name="CabinetNumber">0101~0705</param>
        public void MoveToCabinetPut(string CabinetNumber)
        {
            MacTransition transition = null;
            TriggerMember triggerMember = null;
            transition = EnumMacMsBoxTransferTransitionContainValue("MoveToCB" + CabinetNumber + "ForRelease");
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
   */







        public override void LoadStateMachine()
        {
            #region State
            MacState sStart = NewState(EnumMacMsBoxTransferState.Start);
            MacState sInitial = NewState(EnumMacMsBoxTransferState.Initial);

            MacState sCB1Home = NewState(EnumMacMsBoxTransferState.CB1Home);
            //MacState sCB2Home = NewState(EnumMacMsBoxTransferState.CB2Home);
            MacState sCB1HomeClamped = NewState(EnumMacMsBoxTransferState.CB1HomeClamped);
            //MacState sCB2HomeClamped = NewState(EnumMacMsBoxTransferState.CB2HomeClamped);


            //  MacState stateCB1HomeClamped = NewState(EnumMacMsBoxTransferState.CB1HomeClamped_C);
            #region Change Direction
            MacState sChangingDirectionToCB1Home = NewState(EnumMacMsBoxTransferState.ChangingDirectionToCB1Home);
            //MacState sChangingDirectionToCB2Home = NewState(EnumMacMsBoxTransferState.ChangingDirectionToCB2Home);
            MacState sChangingDirectionToCB1HomeClamped = NewState(EnumMacMsBoxTransferState.ChangingDirectionToCB1HomeClamped);
            //MacState sChangingDirectionToCB2HomeClamped = NewState(EnumMacMsBoxTransferState.ChangingDirectionToCB2HomeClamped);
            #endregion Change Direction

            #region OS
            MacState sMovingToOpenStage = NewState(EnumMacMsBoxTransferState.MovingToOpenStage);
            MacState sOpenStageClamping = NewState(EnumMacMsBoxTransferState.OpenStageClamping);
            MacState sMovingToCB1HomeClampedFromOpenStage = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeClampedFromOpenStage);

            MacState sMovingToOpenStageForRelease = NewState(EnumMacMsBoxTransferState.MovingOpenStageForRelease);
            MacState sOpenStageReleasing = NewState(EnumMacMsBoxTransferState.OpenStageReleasing);
            MacState sMovingToCB1HomeFromOpenStage = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeFromOpenStage);
            #endregion OS

            #region Lock & Unlock
            MacState sLocking = NewState(EnumMacMsBoxTransferState.Locking);
            MacState sUnlocking = NewState(EnumMacMsBoxTransferState.Unlocking);
            #endregion Lock & Unlock

            #region CB1
            #region Move To Cabinet
            /**
            MacState sMovingToCabinet0101 = NewState(EnumMacMsBoxTransferState.MovingToCabinet0101);
            MacState sMovingToCabinet0102 = NewState(EnumMacMsBoxTransferState.MovingToCabinet0102);
            MacState sMovingToCabinet0103 = NewState(EnumMacMsBoxTransferState.MovingToCabinet0103);
            MacState sMovingToCabinet0104 = NewState(EnumMacMsBoxTransferState.MovingToCabinet0104);
            MacState sMovingToCabinet0105 = NewState(EnumMacMsBoxTransferState.MovingToCabinet0105);
            MacState sMovingToCabinet0201 = NewState(EnumMacMsBoxTransferState.MovingToCabinet0201);
            MacState sMovingToCabinet0202 = NewState(EnumMacMsBoxTransferState.MovingToCabinet0202);
            MacState sMovingToCabinet0203 = NewState(EnumMacMsBoxTransferState.MovingToCabinet0203);
            MacState sMovingToCabinet0204 = NewState(EnumMacMsBoxTransferState.MovingToCabinet0204);
            MacState sMovingToCabinet0205 = NewState(EnumMacMsBoxTransferState.MovingToCabinet0205);
            MacState sMovingToCabinet0301 = NewState(EnumMacMsBoxTransferState.MovingToCabinet0301);
            MacState sMovingToCabinet0302 = NewState(EnumMacMsBoxTransferState.MovingToCabinet0302);
            MacState sMovingToCabinet0303 = NewState(EnumMacMsBoxTransferState.MovingToCabinet0303);
            MacState sMovingToCabinet0304 = NewState(EnumMacMsBoxTransferState.MovingToCabinet0304);
            MacState sMovingToCabinet0305 = NewState(EnumMacMsBoxTransferState.MovingToCabinet0305);
            */
            MacState stateMovingToDrawer = NewState(EnumMacMsBoxTransferState.MovingToDrawer);
            #endregion Move To Cabinet

            #region Clamping At Cabinet
            /**
            MacState sCabinet0101Clamping = NewState(EnumMacMsBoxTransferState.Cabinet0101Clamping);
            MacState sCabinet0102Clamping = NewState(EnumMacMsBoxTransferState.Cabinet0102Clamping);
            MacState sCabinet0103Clamping = NewState(EnumMacMsBoxTransferState.Cabinet0103Clamping);
            MacState sCabinet0104Clamping = NewState(EnumMacMsBoxTransferState.Cabinet0104Clamping);
            MacState sCabinet0105Clamping = NewState(EnumMacMsBoxTransferState.Cabinet0105Clamping);
            MacState sCabinet0201Clamping = NewState(EnumMacMsBoxTransferState.Cabinet0201Clamping);
            MacState sCabinet0202Clamping = NewState(EnumMacMsBoxTransferState.Cabinet0202Clamping);
            MacState sCabinet0203Clamping = NewState(EnumMacMsBoxTransferState.Cabinet0203Clamping);
            MacState sCabinet0204Clamping = NewState(EnumMacMsBoxTransferState.Cabinet0204Clamping);
            MacState sCabinet0205Clamping = NewState(EnumMacMsBoxTransferState.Cabinet0205Clamping);
            MacState sCabinet0301Clamping = NewState(EnumMacMsBoxTransferState.Cabinet0301Clamping);
            MacState sCabinet0302Clamping = NewState(EnumMacMsBoxTransferState.Cabinet0302Clamping);
            MacState sCabinet0303Clamping = NewState(EnumMacMsBoxTransferState.Cabinet0303Clamping);
            MacState sCabinet0304Clamping = NewState(EnumMacMsBoxTransferState.Cabinet0304Clamping);
            MacState sCabinet0305Clamping = NewState(EnumMacMsBoxTransferState.Cabinet0305Clamping);
            */
            MacState stateDrawerClamping = NewState(EnumMacMsBoxTransferState.DrawerClamping);
            #endregion Clamping At Cabinet

            #region Return To CB Home Clamped From Cabinet
            /**
            MacState sMovingToCB1HomeClampedFromCabinet0101 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeClampedFromCabinet0101);
            MacState sMovingToCB1HomeClampedFromCabinet0102 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeClampedFromCabinet0102);
            MacState sMovingToCB1HomeClampedFromCabinet0103 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeClampedFromCabinet0103);
            MacState sMovingToCB1HomeClampedFromCabinet0104 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeClampedFromCabinet0104);
            MacState sMovingToCB1HomeClampedFromCabinet0105 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeClampedFromCabinet0105);
            MacState sMovingToCB1HomeClampedFromCabinet0201 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeClampedFromCabinet0201);
            MacState sMovingToCB1HomeClampedFromCabinet0202 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeClampedFromCabinet0202);
            MacState sMovingToCB1HomeClampedFromCabinet0203 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeClampedFromCabinet0203);
            MacState sMovingToCB1HomeClampedFromCabinet0204 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeClampedFromCabinet0204);
            MacState sMovingToCB1HomeClampedFromCabinet0205 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeClampedFromCabinet0205);
            MacState sMovingToCB1HomeClampedFromCabinet0301 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeClampedFromCabinet0301);
            MacState sMovingToCB1HomeClampedFromCabinet0302 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeClampedFromCabinet0302);
            MacState sMovingToCB1HomeClampedFromCabinet0303 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeClampedFromCabinet0303);
            MacState sMovingToCB1HomeClampedFromCabinet0304 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeClampedFromCabinet0304);
            MacState sMovingToCB1HomeClampedFromCabinet0305 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeClampedFromCabinet0305);
            */

            MacState stateMovingToCB1HomeClampedFromDrawer = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeClampedFromDrawer);

            #endregion Return To CB Home Clamped From Cabinet

            #region Move To Cabinet Fro Release
            /**
            MacState sMovingToCabinet0101ForRelease = NewState(EnumMacMsBoxTransferState.MovingToCabinet0101ForRelease);
            MacState sMovingToCabinet0102ForRelease = NewState(EnumMacMsBoxTransferState.MovingToCabinet0102ForRelease);
            MacState sMovingToCabinet0103ForRelease = NewState(EnumMacMsBoxTransferState.MovingToCabinet0103ForRelease);
            MacState sMovingToCabinet0104ForRelease = NewState(EnumMacMsBoxTransferState.MovingToCabinet0104ForRelease);
            MacState sMovingToCabinet0105ForRelease = NewState(EnumMacMsBoxTransferState.MovingToCabinet0105ForRelease);
            MacState sMovingToCabinet0201ForRelease = NewState(EnumMacMsBoxTransferState.MovingToCabinet0201ForRelease);
            MacState sMovingToCabinet0202ForRelease = NewState(EnumMacMsBoxTransferState.MovingToCabinet0202ForRelease);
            MacState sMovingToCabinet0203ForRelease = NewState(EnumMacMsBoxTransferState.MovingToCabinet0203ForRelease);
            MacState sMovingToCabinet0204ForRelease = NewState(EnumMacMsBoxTransferState.MovingToCabinet0204ForRelease);
            MacState sMovingToCabinet0205ForRelease = NewState(EnumMacMsBoxTransferState.MovingToCabinet0205ForRelease);
            MacState sMovingToCabinet0301ForRelease = NewState(EnumMacMsBoxTransferState.MovingToCabinet0301ForRelease);
            MacState sMovingToCabinet0302ForRelease = NewState(EnumMacMsBoxTransferState.MovingToCabinet0302ForRelease);
            MacState sMovingToCabinet0303ForRelease = NewState(EnumMacMsBoxTransferState.MovingToCabinet0303ForRelease);
            MacState sMovingToCabinet0304ForRelease = NewState(EnumMacMsBoxTransferState.MovingToCabinet0304ForRelease);
            MacState sMovingToCabinet0305ForRelease = NewState(EnumMacMsBoxTransferState.MovingToCabinet0305ForRelease);
            */
            MacState stateMovingToDrawerForRelease = NewState(EnumMacMsBoxTransferState.MovingToDrawerForRelease);
            #endregion Move To Cabinet Fro Release

            #region Releasing At Cabinet
            /**
            MacState sCabinet0101Releasing = NewState(EnumMacMsBoxTransferState.Cabinet0101Releasing);
            MacState sCabinet0102Releasing = NewState(EnumMacMsBoxTransferState.Cabinet0102Releasing);
            MacState sCabinet0103Releasing = NewState(EnumMacMsBoxTransferState.Cabinet0103Releasing);
            MacState sCabinet0104Releasing = NewState(EnumMacMsBoxTransferState.Cabinet0104Releasing);
            MacState sCabinet0105Releasing = NewState(EnumMacMsBoxTransferState.Cabinet0105Releasing);
            MacState sCabinet0201Releasing = NewState(EnumMacMsBoxTransferState.Cabinet0201Releasing);
            MacState sCabinet0202Releasing = NewState(EnumMacMsBoxTransferState.Cabinet0202Releasing);
            MacState sCabinet0203Releasing = NewState(EnumMacMsBoxTransferState.Cabinet0203Releasing);
            MacState sCabinet0204Releasing = NewState(EnumMacMsBoxTransferState.Cabinet0204Releasing);
            MacState sCabinet0205Releasing = NewState(EnumMacMsBoxTransferState.Cabinet0205Releasing);
            MacState sCabinet0301Releasing = NewState(EnumMacMsBoxTransferState.Cabinet0301Releasing);
            MacState sCabinet0302Releasing = NewState(EnumMacMsBoxTransferState.Cabinet0302Releasing);
            MacState sCabinet0303Releasing = NewState(EnumMacMsBoxTransferState.Cabinet0303Releasing);
            MacState sCabinet0304Releasing = NewState(EnumMacMsBoxTransferState.Cabinet0304Releasing);
            MacState sCabinet0305Releasing = NewState(EnumMacMsBoxTransferState.Cabinet0305Releasing);
            */
            MacState stateDrawerReleasing = NewState(EnumMacMsBoxTransferState.DrawerReleasing);
            #endregion Releasing At Cabinet

            #region Return To CB Home From Cabinet
            /**
            MacState sMovingToCB1HomeFromCabinet0101 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeFromCabinet0101);
            MacState sMovingToCB1HomeFromCabinet0102 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeFromCabinet0102);
            MacState sMovingToCB1HomeFromCabinet0103 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeFromCabinet0103);
            MacState sMovingToCB1HomeFromCabinet0104 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeFromCabinet0104);
            MacState sMovingToCB1HomeFromCabinet0105 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeFromCabinet0105);
            MacState sMovingToCB1HomeFromCabinet0201 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeFromCabinet0201);
            MacState sMovingToCB1HomeFromCabinet0202 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeFromCabinet0202);
            MacState sMovingToCB1HomeFromCabinet0203 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeFromCabinet0203);
            MacState sMovingToCB1HomeFromCabinet0204 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeFromCabinet0204);
            MacState sMovingToCB1HomeFromCabinet0205 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeFromCabinet0205);
            MacState sMovingToCB1HomeFromCabinet0301 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeFromCabinet0301);
            MacState sMovingToCB1HomeFromCabinet0302 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeFromCabinet0302);
            MacState sMovingToCB1HomeFromCabinet0303 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeFromCabinet0303);
            MacState sMovingToCB1HomeFromCabinet0304 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeFromCabinet0304);
            MacState sMovingToCB1HomeFromCabinet0305 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeFromCabinet0305);
            */
            MacState stateMovingToCB1HomeFromDrawer = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeFromDrawer);

            #endregion Return To CB Home From Cabinet
            #endregion CB1

            #region CB2
            #region Move To Cabinet
            /**
            MacState sMovingToCabinet0401 = NewState(EnumMacMsBoxTransferState.MovingToCabinet0401);
            MacState sMovingToCabinet0402 = NewState(EnumMacMsBoxTransferState.MovingToCabinet0402);
            MacState sMovingToCabinet0403 = NewState(EnumMacMsBoxTransferState.MovingToCabinet0403);
            MacState sMovingToCabinet0404 = NewState(EnumMacMsBoxTransferState.MovingToCabinet0404);
            MacState sMovingToCabinet0405 = NewState(EnumMacMsBoxTransferState.MovingToCabinet0405);
            MacState sMovingToCabinet0501 = NewState(EnumMacMsBoxTransferState.MovingToCabinet0501);
            MacState sMovingToCabinet0502 = NewState(EnumMacMsBoxTransferState.MovingToCabinet0502);
            MacState sMovingToCabinet0503 = NewState(EnumMacMsBoxTransferState.MovingToCabinet0503);
            MacState sMovingToCabinet0504 = NewState(EnumMacMsBoxTransferState.MovingToCabinet0504);
            MacState sMovingToCabinet0505 = NewState(EnumMacMsBoxTransferState.MovingToCabinet0505);
            MacState sMovingToCabinet0601 = NewState(EnumMacMsBoxTransferState.MovingToCabinet0601);
            MacState sMovingToCabinet0602 = NewState(EnumMacMsBoxTransferState.MovingToCabinet0602);
            MacState sMovingToCabinet0603 = NewState(EnumMacMsBoxTransferState.MovingToCabinet0603);
            MacState sMovingToCabinet0604 = NewState(EnumMacMsBoxTransferState.MovingToCabinet0604);
            MacState sMovingToCabinet0605 = NewState(EnumMacMsBoxTransferState.MovingToCabinet0605);
            MacState sMovingToCabinet0701 = NewState(EnumMacMsBoxTransferState.MovingToCabinet0701);
            MacState sMovingToCabinet0702 = NewState(EnumMacMsBoxTransferState.MovingToCabinet0702);
            MacState sMovingToCabinet0703 = NewState(EnumMacMsBoxTransferState.MovingToCabinet0703);
            MacState sMovingToCabinet0704 = NewState(EnumMacMsBoxTransferState.MovingToCabinet0704);
            MacState sMovingToCabinet0705 = NewState(EnumMacMsBoxTransferState.MovingToCabinet0705);
            */
            //MacState stateMovingToDrawer = NewState(EnumMacMsBoxTransferState.MovingToDrawer);
            #endregion Move To Cabinet

            #region Clamping At Cabinet
            /**
            MacState sCabinet0401Clamping = NewState(EnumMacMsBoxTransferState.Cabinet0401Clamping);
            MacState sCabinet0402Clamping = NewState(EnumMacMsBoxTransferState.Cabinet0402Clamping);
            MacState sCabinet0403Clamping = NewState(EnumMacMsBoxTransferState.Cabinet0403Clamping);
            MacState sCabinet0404Clamping = NewState(EnumMacMsBoxTransferState.Cabinet0404Clamping);
            MacState sCabinet0405Clamping = NewState(EnumMacMsBoxTransferState.Cabinet0405Clamping);
            MacState sCabinet0501Clamping = NewState(EnumMacMsBoxTransferState.Cabinet0501Clamping);
            MacState sCabinet0502Clamping = NewState(EnumMacMsBoxTransferState.Cabinet0502Clamping);
            MacState sCabinet0503Clamping = NewState(EnumMacMsBoxTransferState.Cabinet0503Clamping);
            MacState sCabinet0504Clamping = NewState(EnumMacMsBoxTransferState.Cabinet0504Clamping);
            MacState sCabinet0505Clamping = NewState(EnumMacMsBoxTransferState.Cabinet0505Clamping);
            MacState sCabinet0601Clamping = NewState(EnumMacMsBoxTransferState.Cabinet0601Clamping);
            MacState sCabinet0602Clamping = NewState(EnumMacMsBoxTransferState.Cabinet0602Clamping);
            MacState sCabinet0603Clamping = NewState(EnumMacMsBoxTransferState.Cabinet0603Clamping);
            MacState sCabinet0604Clamping = NewState(EnumMacMsBoxTransferState.Cabinet0604Clamping);
            MacState sCabinet0605Clamping = NewState(EnumMacMsBoxTransferState.Cabinet0605Clamping);
            MacState sCabinet0701Clamping = NewState(EnumMacMsBoxTransferState.Cabinet0701Clamping);
            MacState sCabinet0702Clamping = NewState(EnumMacMsBoxTransferState.Cabinet0702Clamping);
            MacState sCabinet0703Clamping = NewState(EnumMacMsBoxTransferState.Cabinet0703Clamping);
            MacState sCabinet0704Clamping = NewState(EnumMacMsBoxTransferState.Cabinet0704Clamping);
            MacState sCabinet0705Clamping = NewState(EnumMacMsBoxTransferState.Cabinet0705Clamping);
            */

            // MacState state_DrawerClamping = NewState(EnumMacMsBoxTransferState.DrawerClamping);
            #endregion Clamping At Cabinet

            #region Return To CB Home Clamped From Cabinet
            /**
            MacState sMovingToCB1HomeClampedFromCabinet0401 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeClampedFromCabinet0401);
            MacState sMovingToCB1HomeClampedFromCabinet0402 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeClampedFromCabinet0402);
            MacState sMovingToCB1HomeClampedFromCabinet0403 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeClampedFromCabinet0403);
            MacState sMovingToCB1HomeClampedFromCabinet0404 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeClampedFromCabinet0404);
            MacState sMovingToCB1HomeClampedFromCabinet0405 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeClampedFromCabinet0405);
            MacState sMovingToCB1HomeClampedFromCabinet0501 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeClampedFromCabinet0501);
            MacState sMovingToCB1HomeClampedFromCabinet0502 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeClampedFromCabinet0502);
            MacState sMovingToCB1HomeClampedFromCabinet0503 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeClampedFromCabinet0503);
            MacState sMovingToCB1HomeClampedFromCabinet0504 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeClampedFromCabinet0504);
            MacState sMovingToCB1HomeClampedFromCabinet0505 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeClampedFromCabinet0505);
            MacState sMovingToCB1HomeClampedFromCabinet0601 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeClampedFromCabinet0601);
            MacState sMovingToCB1HomeClampedFromCabinet0602 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeClampedFromCabinet0602);
            MacState sMovingToCB1HomeClampedFromCabinet0603 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeClampedFromCabinet0603);
            MacState sMovingToCB1HomeClampedFromCabinet0604 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeClampedFromCabinet0604);
            MacState sMovingToCB1HomeClampedFromCabinet0605 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeClampedFromCabinet0605);
            MacState sMovingToCB1HomeClampedFromCabinet0701 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeClampedFromCabinet0701);
            MacState sMovingToCB1HomeClampedFromCabinet0702 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeClampedFromCabinet0702);
            MacState sMovingToCB1HomeClampedFromCabinet0703 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeClampedFromCabinet0703);
            MacState sMovingToCB1HomeClampedFromCabinet0704 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeClampedFromCabinet0704);
            MacState sMovingToCB1HomeClampedFromCabinet0705 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeClampedFromCabinet0705);
            */
            // MacState state_MovingToCB1HomeFromDrawer = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeFromDrawer);
            #endregion Return To CB Home Clamped From Cabinet

            #region Move To Cabinet Fro Release
            /**
            MacState sMovingToCabinet0401ForRelease = NewState(EnumMacMsBoxTransferState.MovingToCabinet0401ForRelease);
            MacState sMovingToCabinet0402ForRelease = NewState(EnumMacMsBoxTransferState.MovingToCabinet0402ForRelease);
            MacState sMovingToCabinet0403ForRelease = NewState(EnumMacMsBoxTransferState.MovingToCabinet0403ForRelease);
            MacState sMovingToCabinet0404ForRelease = NewState(EnumMacMsBoxTransferState.MovingToCabinet0404ForRelease);
            MacState sMovingToCabinet0405ForRelease = NewState(EnumMacMsBoxTransferState.MovingToCabinet0405ForRelease);
            MacState sMovingToCabinet0501ForRelease = NewState(EnumMacMsBoxTransferState.MovingToCabinet0501ForRelease);
            MacState sMovingToCabinet0502ForRelease = NewState(EnumMacMsBoxTransferState.MovingToCabinet0502ForRelease);
            MacState sMovingToCabinet0503ForRelease = NewState(EnumMacMsBoxTransferState.MovingToCabinet0503ForRelease);
            MacState sMovingToCabinet0504ForRelease = NewState(EnumMacMsBoxTransferState.MovingToCabinet0504ForRelease);
            MacState sMovingToCabinet0505ForRelease = NewState(EnumMacMsBoxTransferState.MovingToCabinet0505ForRelease);
            MacState sMovingToCabinet0601ForRelease = NewState(EnumMacMsBoxTransferState.MovingToCabinet0601ForRelease);
            MacState sMovingToCabinet0602ForRelease = NewState(EnumMacMsBoxTransferState.MovingToCabinet0602ForRelease);
            MacState sMovingToCabinet0603ForRelease = NewState(EnumMacMsBoxTransferState.MovingToCabinet0603ForRelease);
            MacState sMovingToCabinet0604ForRelease = NewState(EnumMacMsBoxTransferState.MovingToCabinet0604ForRelease);
            MacState sMovingToCabinet0605ForRelease = NewState(EnumMacMsBoxTransferState.MovingToCabinet0605ForRelease);
            MacState sMovingToCabinet0701ForRelease = NewState(EnumMacMsBoxTransferState.MovingToCabinet0701ForRelease);
            MacState sMovingToCabinet0702ForRelease = NewState(EnumMacMsBoxTransferState.MovingToCabinet0702ForRelease);
            MacState sMovingToCabinet0703ForRelease = NewState(EnumMacMsBoxTransferState.MovingToCabinet0703ForRelease);
            MacState sMovingToCabinet0704ForRelease = NewState(EnumMacMsBoxTransferState.MovingToCabinet0704ForRelease);
            MacState sMovingToCabinet0705ForRelease = NewState(EnumMacMsBoxTransferState.MovingToCabinet0705ForRelease);
            */
            //MacState stateMovingToDrawerForRelease = NewState(EnumMacMsBoxTransferState.MovingToDrawerForRelease);
            #endregion Move To Cabinet Fro Release

            #region Releasing At Cabinet
            /**
            MacState sCabinet0401Releasing = NewState(EnumMacMsBoxTransferState.Cabinet0401Releasing);
            MacState sCabinet0402Releasing = NewState(EnumMacMsBoxTransferState.Cabinet0402Releasing);
            MacState sCabinet0403Releasing = NewState(EnumMacMsBoxTransferState.Cabinet0403Releasing);
            MacState sCabinet0404Releasing = NewState(EnumMacMsBoxTransferState.Cabinet0404Releasing);
            MacState sCabinet0405Releasing = NewState(EnumMacMsBoxTransferState.Cabinet0405Releasing);
            MacState sCabinet0501Releasing = NewState(EnumMacMsBoxTransferState.Cabinet0501Releasing);
            MacState sCabinet0502Releasing = NewState(EnumMacMsBoxTransferState.Cabinet0502Releasing);
            MacState sCabinet0503Releasing = NewState(EnumMacMsBoxTransferState.Cabinet0503Releasing);
            MacState sCabinet0504Releasing = NewState(EnumMacMsBoxTransferState.Cabinet0504Releasing);
            MacState sCabinet0505Releasing = NewState(EnumMacMsBoxTransferState.Cabinet0505Releasing);
            MacState sCabinet0601Releasing = NewState(EnumMacMsBoxTransferState.Cabinet0601Releasing);
            MacState sCabinet0602Releasing = NewState(EnumMacMsBoxTransferState.Cabinet0602Releasing);
            MacState sCabinet0603Releasing = NewState(EnumMacMsBoxTransferState.Cabinet0603Releasing);
            MacState sCabinet0604Releasing = NewState(EnumMacMsBoxTransferState.Cabinet0604Releasing);
            MacState sCabinet0605Releasing = NewState(EnumMacMsBoxTransferState.Cabinet0605Releasing);
            MacState sCabinet0701Releasing = NewState(EnumMacMsBoxTransferState.Cabinet0701Releasing);
            MacState sCabinet0702Releasing = NewState(EnumMacMsBoxTransferState.Cabinet0702Releasing);
            MacState sCabinet0703Releasing = NewState(EnumMacMsBoxTransferState.Cabinet0703Releasing);
            MacState sCabinet0704Releasing = NewState(EnumMacMsBoxTransferState.Cabinet0704Releasing);
            MacState sCabinet0705Releasing = NewState(EnumMacMsBoxTransferState.Cabinet0705Releasing);
            */
            // MacState state_DrawerReleasing = NewState(EnumMacMsBoxTransferState.DrawerReleasing);
            #endregion Releasing At Cabinet

            #region Return To CB Home From Cabinet
            /**
            MacState sMovingToCB1HomeFromCabinet0401 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeFromCabinet0401);
            MacState sMovingToCB1HomeFromCabinet0402 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeFromCabinet0402);
            MacState sMovingToCB1HomeFromCabinet0403 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeFromCabinet0403);
            MacState sMovingToCB1HomeFromCabinet0404 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeFromCabinet0404);
            MacState sMovingToCB1HomeFromCabinet0405 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeFromCabinet0405);
            MacState sMovingToCB1HomeFromCabinet0501 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeFromCabinet0501);
            MacState sMovingToCB1HomeFromCabinet0502 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeFromCabinet0502);
            MacState sMovingToCB1HomeFromCabinet0503 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeFromCabinet0503);
            MacState sMovingToCB1HomeFromCabinet0504 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeFromCabinet0504);
            MacState sMovingToCB1HomeFromCabinet0505 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeFromCabinet0505);
            MacState sMovingToCB1HomeFromCabinet0601 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeFromCabinet0601);
            MacState sMovingToCB1HomeFromCabinet0602 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeFromCabinet0602);
            MacState sMovingToCB1HomeFromCabinet0603 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeFromCabinet0603);
            MacState sMovingToCB1HomeFromCabinet0604 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeFromCabinet0604);
            MacState sMovingToCB1HomeFromCabinet0605 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeFromCabinet0605);
            MacState sMovingToCB1HomeFromCabinet0701 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeFromCabinet0701);
            MacState sMovingToCB1HomeFromCabinet0702 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeFromCabinet0702);
            MacState sMovingToCB1HomeFromCabinet0703 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeFromCabinet0703);
            MacState sMovingToCB1HomeFromCabinet0704 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeFromCabinet0704);
            MacState sMovingToCB1HomeFromCabinet0705 = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeFromCabinet0705);
            */

            // MacState stateMovingToCB1HomeFromDrawer = NewState(EnumMacMsBoxTransferState.MovingToCB1HomeFromDrawer);

            #endregion Return To CB Home From Cabinet
            #endregion CB2
            #endregion State

            #region Transition
            MacTransition tStart_DeviceInitial = NewTransition(sStart, sInitial, EnumMacMsBoxTransferTransition.PowerON);
            MacTransition tDeviceInitial_CB1Home = NewTransition(sInitial, sCB1Home, EnumMacMsBoxTransferTransition.Initial);

            MacTransition tCB1Home_NULL = NewTransition(sCB1Home, null, EnumMacMsBoxTransferTransition.StandbyAtCB1Home);
            MacTransition tCB1HomeClamped_NULL = NewTransition(sCB1HomeClamped, null, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeClamped);

            //MacTransition transitionCB1HomeClamped_NULL = NewTransition(stateCB1HomeClamped, null, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeClamped_C);

            #region Change Direction
            //MacTransition tCB1Home_ChangingDirectionToCB2Home = NewTransition(sCB1Home, sChangingDirectionToCB2Home, EnumMacMsBoxTransferTransition.ChangeDirectionToCB2HomeFromCB1Home);
            //MacTransition tCB2Home_ChangingDirectionToCB1Home = NewTransition(sCB2Home, sChangingDirectionToCB1Home, EnumMacMsBoxTransferTransition.ChangeDirectionToCB1HomeFromCB2Home);
            //MacTransition tCB1HomeClamped_ChangingDirectionToCB2HomeClamped = NewTransition(sCB1HomeClamped, sChangingDirectionToCB2HomeClamped, EnumMacMsBoxTransferTransition.ChangeDirectionToCB2HomeClampedFromCB1HomeClamped);
            //MacTransition tCB2HomeClamped_ChangingDirectionToCB1HomeClamped = NewTransition(sCB2HomeClamped, sChangingDirectionToCB1HomeClamped, EnumMacMsBoxTransferTransition.ChangeDirectionToCB1HomeClampedFromCB2HomeClamped);
            #endregion Change Direction

            #region OS
            MacTransition tCB1Home_MovingToOpenStage = NewTransition(sCB1Home, sMovingToOpenStage, EnumMacMsBoxTransferTransition.MoveToOpenStage);
            MacTransition tMovingToOpenStage_OpenStageClamping = NewTransition(sMovingToOpenStage, sOpenStageClamping, EnumMacMsBoxTransferTransition.ClampInOpenStage);
            MacTransition tOpenStageClamping_MovingToCB1HomeClampedFromOpenStage = NewTransition(sOpenStageClamping, sMovingToCB1HomeClampedFromOpenStage, EnumMacMsBoxTransferTransition.MoveToCB1HomeClampedFromOpenStage);
            MacTransition tMovingToCB1HomeClampedFromOpenStage_CB1HomeClamped = NewTransition(sMovingToCB1HomeClampedFromOpenStage, sCB1HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeClampedFromOpenStage);

            MacTransition tCB1HomeClamped_MovingToOpenStageForRelease = NewTransition(sCB1HomeClamped, sMovingToOpenStageForRelease, EnumMacMsBoxTransferTransition.MoveToOpenStageForRelease);
            MacTransition tMovingToOpenStageForRelease_OpenStageReleasing = NewTransition(sMovingToOpenStageForRelease, sOpenStageReleasing, EnumMacMsBoxTransferTransition.ReleaseInOpenStage);
            MacTransition tOpenStageReleasing_MovingToCB1HomeFromOpenStage = NewTransition(sOpenStageReleasing, sMovingToCB1HomeFromOpenStage, EnumMacMsBoxTransferTransition.MoveToCB1HomeFromOpenStage);
            MacTransition tMovingToCB1HomeFromOpenStage_CB1Home = NewTransition(sMovingToCB1HomeFromOpenStage, sCB1Home, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeFromOpenStage);
            #endregion OS

            #region Lock & Unlock
            MacTransition tCB1Home_Locking = NewTransition(sCB1Home, sLocking, EnumMacMsBoxTransferTransition.MoveToLock);
            MacTransition tLocking_CB1Home = NewTransition(sLocking, sCB1Home, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeFromLock);
            MacTransition tCB1Home_Unlocking = NewTransition(sCB1Home, sUnlocking, EnumMacMsBoxTransferTransition.MoveToUnlock);
            MacTransition tUnlocking_CB1Home = NewTransition(sUnlocking, sCB1Home, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeFromUnlock);
            #endregion Lock & Unlock

            #region CB1
            #region Get

            /**   Obsolete
            MacTransition tCB1Home_MovingToCabinet0101 = NewTransition(sCB1Home, sMovingToCabinet0101, EnumMacMsBoxTransferTransition.MoveToCB0101);
            MacTransition tCB1Home_MovingToCabinet0102 = NewTransition(sCB1Home, sMovingToCabinet0102, EnumMacMsBoxTransferTransition.MoveToCB0102);
            MacTransition tCB1Home_MovingToCabinet0103 = NewTransition(sCB1Home, sMovingToCabinet0103, EnumMacMsBoxTransferTransition.MoveToCB0103);
            MacTransition tCB1Home_MovingToCabinet0104 = NewTransition(sCB1Home, sMovingToCabinet0104, EnumMacMsBoxTransferTransition.MoveToCB0104);
            MacTransition tCB1Home_MovingToCabinet0105 = NewTransition(sCB1Home, sMovingToCabinet0105, EnumMacMsBoxTransferTransition.MoveToCB0105);
            MacTransition tCB1Home_MovingToCabinet0201 = NewTransition(sCB1Home, sMovingToCabinet0201, EnumMacMsBoxTransferTransition.MoveToCB0201);
            MacTransition tCB1Home_MovingToCabinet0202 = NewTransition(sCB1Home, sMovingToCabinet0202, EnumMacMsBoxTransferTransition.MoveToCB0202);
            MacTransition tCB1Home_MovingToCabinet0203 = NewTransition(sCB1Home, sMovingToCabinet0203, EnumMacMsBoxTransferTransition.MoveToCB0203);
            MacTransition tCB1Home_MovingToCabinet0204 = NewTransition(sCB1Home, sMovingToCabinet0204, EnumMacMsBoxTransferTransition.MoveToCB0204);
            MacTransition tCB1Home_MovingToCabinet0205 = NewTransition(sCB1Home, sMovingToCabinet0205, EnumMacMsBoxTransferTransition.MoveToCB0205);
            MacTransition tCB1Home_MovingToCabinet0301 = NewTransition(sCB1Home, sMovingToCabinet0301, EnumMacMsBoxTransferTransition.MoveToCB0301);
            MacTransition tCB1Home_MovingToCabinet0302 = NewTransition(sCB1Home, sMovingToCabinet0302, EnumMacMsBoxTransferTransition.MoveToCB0302);
            MacTransition tCB1Home_MovingToCabinet0303 = NewTransition(sCB1Home, sMovingToCabinet0303, EnumMacMsBoxTransferTransition.MoveToCB0303);
            MacTransition tCB1Home_MovingToCabinet0304 = NewTransition(sCB1Home, sMovingToCabinet0304, EnumMacMsBoxTransferTransition.MoveToCB0304);
            MacTransition tCB1Home_MovingToCabinet0305 = NewTransition(sCB1Home, sMovingToCabinet0305, EnumMacMsBoxTransferTransition.MoveToCB0305);
            */
            MacTransition transitionCB1Home_MovingToDrawer = NewTransition(sCB1Home, stateMovingToDrawer, EnumMacMsBoxTransferTransition.MoveToDrawer);

            /**  Obsolete
            MacTransition tMovingToCabinet0101_Cabinet0101Clamping = NewTransition(sMovingToCabinet0101, sCabinet0101Clamping, EnumMacMsBoxTransferTransition.ClampAtCB0101);
            MacTransition tMovingToCabinet0102_Cabinet0102Clamping = NewTransition(sMovingToCabinet0102, sCabinet0102Clamping, EnumMacMsBoxTransferTransition.ClampAtCB0102);
            MacTransition tMovingToCabinet0103_Cabinet0103Clamping = NewTransition(sMovingToCabinet0103, sCabinet0103Clamping, EnumMacMsBoxTransferTransition.ClampAtCB0103);
            MacTransition tMovingToCabinet0104_Cabinet0104Clamping = NewTransition(sMovingToCabinet0104, sCabinet0104Clamping, EnumMacMsBoxTransferTransition.ClampAtCB0104);
            MacTransition tMovingToCabinet0105_Cabinet0105Clamping = NewTransition(sMovingToCabinet0105, sCabinet0105Clamping, EnumMacMsBoxTransferTransition.ClampAtCB0105);
            MacTransition tMovingToCabinet0201_Cabinet0201Clamping = NewTransition(sMovingToCabinet0201, sCabinet0201Clamping, EnumMacMsBoxTransferTransition.ClampAtCB0201);
            MacTransition tMovingToCabinet0202_Cabinet0202Clamping = NewTransition(sMovingToCabinet0202, sCabinet0202Clamping, EnumMacMsBoxTransferTransition.ClampAtCB0202);
            MacTransition tMovingToCabinet0203_Cabinet0203Clamping = NewTransition(sMovingToCabinet0203, sCabinet0203Clamping, EnumMacMsBoxTransferTransition.ClampAtCB0203);
            MacTransition tMovingToCabinet0204_Cabinet0204Clamping = NewTransition(sMovingToCabinet0204, sCabinet0204Clamping, EnumMacMsBoxTransferTransition.ClampAtCB0204);
            MacTransition tMovingToCabinet0205_Cabinet0205Clamping = NewTransition(sMovingToCabinet0205, sCabinet0205Clamping, EnumMacMsBoxTransferTransition.ClampAtCB0205);
            MacTransition tMovingToCabinet0301_Cabinet0301Clamping = NewTransition(sMovingToCabinet0301, sCabinet0301Clamping, EnumMacMsBoxTransferTransition.ClampAtCB0301);
            MacTransition tMovingToCabinet0302_Cabinet0302Clamping = NewTransition(sMovingToCabinet0302, sCabinet0302Clamping, EnumMacMsBoxTransferTransition.ClampAtCB0302);
            MacTransition tMovingToCabinet0303_Cabinet0303Clamping = NewTransition(sMovingToCabinet0303, sCabinet0303Clamping, EnumMacMsBoxTransferTransition.ClampAtCB0303);
            MacTransition tMovingToCabinet0304_Cabinet0304Clamping = NewTransition(sMovingToCabinet0304, sCabinet0304Clamping, EnumMacMsBoxTransferTransition.ClampAtCB0304);
            MacTransition tMovingToCabinet0305_Cabinet0305Clamping = NewTransition(sMovingToCabinet0305, sCabinet0305Clamping, EnumMacMsBoxTransferTransition.ClampAtCB0305);
            */
            MacTransition transitionMovingToDrawer_DrawerClamping = NewTransition(stateMovingToDrawer, stateDrawerClamping, EnumMacMsBoxTransferTransition.ClampAtDrawer);


            /**  Obsolete
            MacTransition tCabinet0101Clamping_MovingToCB1HomeClampedFromCabinet0101 = NewTransition(sCabinet0101Clamping, sMovingToCB1HomeClampedFromCabinet0101, EnumMacMsBoxTransferTransition.MoveToCB1HomeClampedFromCB0101);
            MacTransition tCabinet0102Clamping_MovingToCB1HomeClampedFromCabinet0102 = NewTransition(sCabinet0102Clamping, sMovingToCB1HomeClampedFromCabinet0102, EnumMacMsBoxTransferTransition.MoveToCB1HomeClampedFromCB0102);
            MacTransition tCabinet0103Clamping_MovingToCB1HomeClampedFromCabinet0103 = NewTransition(sCabinet0103Clamping, sMovingToCB1HomeClampedFromCabinet0103, EnumMacMsBoxTransferTransition.MoveToCB1HomeClampedFromCB0103);
            MacTransition tCabinet0104Clamping_MovingToCB1HomeClampedFromCabinet0104 = NewTransition(sCabinet0104Clamping, sMovingToCB1HomeClampedFromCabinet0104, EnumMacMsBoxTransferTransition.MoveToCB1HomeClampedFromCB0104);
            MacTransition tCabinet0105Clamping_MovingToCB1HomeClampedFromCabinet0105 = NewTransition(sCabinet0105Clamping, sMovingToCB1HomeClampedFromCabinet0105, EnumMacMsBoxTransferTransition.MoveToCB1HomeClampedFromCB0105);
            MacTransition tCabinet0201Clamping_MovingToCB1HomeClampedFromCabinet0201 = NewTransition(sCabinet0201Clamping, sMovingToCB1HomeClampedFromCabinet0201, EnumMacMsBoxTransferTransition.MoveToCB1HomeClampedFromCB0201);
            MacTransition tCabinet0202Clamping_MovingToCB1HomeClampedFromCabinet0202 = NewTransition(sCabinet0202Clamping, sMovingToCB1HomeClampedFromCabinet0202, EnumMacMsBoxTransferTransition.MoveToCB1HomeClampedFromCB0202);
            MacTransition tCabinet0203Clamping_MovingToCB1HomeClampedFromCabinet0203 = NewTransition(sCabinet0203Clamping, sMovingToCB1HomeClampedFromCabinet0203, EnumMacMsBoxTransferTransition.MoveToCB1HomeClampedFromCB0203);
            MacTransition tCabinet0204Clamping_MovingToCB1HomeClampedFromCabinet0204 = NewTransition(sCabinet0204Clamping, sMovingToCB1HomeClampedFromCabinet0204, EnumMacMsBoxTransferTransition.MoveToCB1HomeClampedFromCB0204);
            MacTransition tCabinet0205Clamping_MovingToCB1HomeClampedFromCabinet0205 = NewTransition(sCabinet0205Clamping, sMovingToCB1HomeClampedFromCabinet0205, EnumMacMsBoxTransferTransition.MoveToCB1HomeClampedFromCB0205);
            MacTransition tCabinet0301Clamping_MovingToCB1HomeClampedFromCabinet0301 = NewTransition(sCabinet0301Clamping, sMovingToCB1HomeClampedFromCabinet0301, EnumMacMsBoxTransferTransition.MoveToCB1HomeClampedFromCB0301);
            MacTransition tCabinet0302Clamping_MovingToCB1HomeClampedFromCabinet0302 = NewTransition(sCabinet0302Clamping, sMovingToCB1HomeClampedFromCabinet0302, EnumMacMsBoxTransferTransition.MoveToCB1HomeClampedFromCB0302);
            MacTransition tCabinet0303Clamping_MovingToCB1HomeClampedFromCabinet0303 = NewTransition(sCabinet0303Clamping, sMovingToCB1HomeClampedFromCabinet0303, EnumMacMsBoxTransferTransition.MoveToCB1HomeClampedFromCB0303);
            MacTransition tCabinet0304Clamping_MovingToCB1HomeClampedFromCabinet0304 = NewTransition(sCabinet0304Clamping, sMovingToCB1HomeClampedFromCabinet0304, EnumMacMsBoxTransferTransition.MoveToCB1HomeClampedFromCB0304);
            MacTransition tCabinet0305Clamping_MovingToCB1HomeClampedFromCabinet0305 = NewTransition(sCabinet0305Clamping, sMovingToCB1HomeClampedFromCabinet0305, EnumMacMsBoxTransferTransition.MoveToCB1HomeClampedFromCB0305);
            */
            MacTransition transitionDrawerClamping_MovingToCB1HomeClampedFromDrawer = NewTransition(stateDrawerClamping, stateMovingToCB1HomeClampedFromDrawer, EnumMacMsBoxTransferTransition.MoveToCB1HomeClampedFromDrawer);

            /** Obsolete
            MacTransition tMovingToCB1HomeClampedFromCabinet0101_CB1HomeClamped = NewTransition(sMovingToCB1HomeClampedFromCabinet0101, sCB1HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeClampedFromCB0101);
            MacTransition tMovingToCB1HomeClampedFromCabinet0102_CB1HomeClamped = NewTransition(sMovingToCB1HomeClampedFromCabinet0102, sCB1HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeClampedFromCB0102);
            MacTransition tMovingToCB1HomeClampedFromCabinet0103_CB1HomeClamped = NewTransition(sMovingToCB1HomeClampedFromCabinet0103, sCB1HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeClampedFromCB0103);
            MacTransition tMovingToCB1HomeClampedFromCabinet0104_CB1HomeClamped = NewTransition(sMovingToCB1HomeClampedFromCabinet0104, sCB1HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeClampedFromCB0104);
            MacTransition tMovingToCB1HomeClampedFromCabinet0105_CB1HomeClamped = NewTransition(sMovingToCB1HomeClampedFromCabinet0105, sCB1HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeClampedFromCB0105);
            MacTransition tMovingToCB1HomeClampedFromCabinet0201_CB1HomeClamped = NewTransition(sMovingToCB1HomeClampedFromCabinet0201, sCB1HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeClampedFromCB0201);
            MacTransition tMovingToCB1HomeClampedFromCabinet0202_CB1HomeClamped = NewTransition(sMovingToCB1HomeClampedFromCabinet0202, sCB1HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeClampedFromCB0202);
            MacTransition tMovingToCB1HomeClampedFromCabinet0203_CB1HomeClamped = NewTransition(sMovingToCB1HomeClampedFromCabinet0203, sCB1HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeClampedFromCB0203);
            MacTransition tMovingToCB1HomeClampedFromCabinet0204_CB1HomeClamped = NewTransition(sMovingToCB1HomeClampedFromCabinet0204, sCB1HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeClampedFromCB0204);
            MacTransition tMovingToCB1HomeClampedFromCabinet0205_CB1HomeClamped = NewTransition(sMovingToCB1HomeClampedFromCabinet0205, sCB1HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeClampedFromCB0205);
            MacTransition tMovingToCB1HomeClampedFromCabinet0301_CB1HomeClamped = NewTransition(sMovingToCB1HomeClampedFromCabinet0301, sCB1HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeClampedFromCB0301);
            MacTransition tMovingToCB1HomeClampedFromCabinet0302_CB1HomeClamped = NewTransition(sMovingToCB1HomeClampedFromCabinet0302, sCB1HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeClampedFromCB0302);
            MacTransition tMovingToCB1HomeClampedFromCabinet0303_CB1HomeClamped = NewTransition(sMovingToCB1HomeClampedFromCabinet0303, sCB1HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeClampedFromCB0303);
            MacTransition tMovingToCB1HomeClampedFromCabinet0304_CB1HomeClamped = NewTransition(sMovingToCB1HomeClampedFromCabinet0304, sCB1HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeClampedFromCB0304);
            MacTransition tMovingToCB1HomeClampedFromCabinet0305_CB1HomeClamped = NewTransition(sMovingToCB1HomeClampedFromCabinet0305, sCB1HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeClampedFromCB0305);
            */

            MacTransition transitionMovingToCB1HomeClampedFromDrawer_CB1HomeClamped = NewTransition(stateMovingToCB1HomeClampedFromDrawer, sCB1HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeClampedFromDrawer);
            #endregion Get

            #region Release
            /** Obsolete
            MacTransition tCB1HomeClamped_MovingToCabinet0101ForRelease = NewTransition(sCB1HomeClamped, sMovingToCabinet0101ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0101ForRelease);
            MacTransition tCB1HomeClamped_MovingToCabinet0102ForRelease = NewTransition(sCB1HomeClamped, sMovingToCabinet0102ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0102ForRelease);
            MacTransition tCB1HomeClamped_MovingToCabinet0103ForRelease = NewTransition(sCB1HomeClamped, sMovingToCabinet0103ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0103ForRelease);
            MacTransition tCB1HomeClamped_MovingToCabinet0104ForRelease = NewTransition(sCB1HomeClamped, sMovingToCabinet0104ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0104ForRelease);
            MacTransition tCB1HomeClamped_MovingToCabinet0105ForRelease = NewTransition(sCB1HomeClamped, sMovingToCabinet0105ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0105ForRelease);
            MacTransition tCB1HomeClamped_MovingToCabinet0201ForRelease = NewTransition(sCB1HomeClamped, sMovingToCabinet0201ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0201ForRelease);
            MacTransition tCB1HomeClamped_MovingToCabinet0202ForRelease = NewTransition(sCB1HomeClamped, sMovingToCabinet0202ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0202ForRelease);
            MacTransition tCB1HomeClamped_MovingToCabinet0203ForRelease = NewTransition(sCB1HomeClamped, sMovingToCabinet0203ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0203ForRelease);
            MacTransition tCB1HomeClamped_MovingToCabinet0204ForRelease = NewTransition(sCB1HomeClamped, sMovingToCabinet0204ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0204ForRelease);
            MacTransition tCB1HomeClamped_MovingToCabinet0205ForRelease = NewTransition(sCB1HomeClamped, sMovingToCabinet0205ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0205ForRelease);
            MacTransition tCB1HomeClamped_MovingToCabinet0301ForRelease = NewTransition(sCB1HomeClamped, sMovingToCabinet0301ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0301ForRelease);
            MacTransition tCB1HomeClamped_MovingToCabinet0302ForRelease = NewTransition(sCB1HomeClamped, sMovingToCabinet0302ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0302ForRelease);
            MacTransition tCB1HomeClamped_MovingToCabinet0303ForRelease = NewTransition(sCB1HomeClamped, sMovingToCabinet0303ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0303ForRelease);
            MacTransition tCB1HomeClamped_MovingToCabinet0304ForRelease = NewTransition(sCB1HomeClamped, sMovingToCabinet0304ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0304ForRelease);
            MacTransition tCB1HomeClamped_MovingToCabinet0305ForRelease = NewTransition(sCB1HomeClamped, sMovingToCabinet0305ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0305ForRelease);
            */
            MacTransition transitionCB1HomeClamped_MovingToDrawerForRelease = NewTransition(sCB1HomeClamped, stateMovingToDrawerForRelease, EnumMacMsBoxTransferTransition.MoveToDrawerForRelease);

            /** Obsolete 
            MacTransition tMovingToCabinet0101ForRelease_Cabinet0101Releasing = NewTransition(sMovingToCabinet0101ForRelease, sCabinet0101Releasing, EnumMacMsBoxTransferTransition.ReleaseAtCB0101);
            MacTransition tMovingToCabinet0102ForRelease_Cabinet0102Releasing = NewTransition(sMovingToCabinet0102ForRelease, sCabinet0102Releasing, EnumMacMsBoxTransferTransition.ReleaseAtCB0102);
            MacTransition tMovingToCabinet0103ForRelease_Cabinet0103Releasing = NewTransition(sMovingToCabinet0103ForRelease, sCabinet0103Releasing, EnumMacMsBoxTransferTransition.ReleaseAtCB0103);
            MacTransition tMovingToCabinet0104ForRelease_Cabinet0104Releasing = NewTransition(sMovingToCabinet0104ForRelease, sCabinet0104Releasing, EnumMacMsBoxTransferTransition.ReleaseAtCB0104);
            MacTransition tMovingToCabinet0105ForRelease_Cabinet0105Releasing = NewTransition(sMovingToCabinet0105ForRelease, sCabinet0105Releasing, EnumMacMsBoxTransferTransition.ReleaseAtCB0105);
            MacTransition tMovingToCabinet0201ForRelease_Cabinet0201Releasing = NewTransition(sMovingToCabinet0201ForRelease, sCabinet0201Releasing, EnumMacMsBoxTransferTransition.ReleaseAtCB0201);
            MacTransition tMovingToCabinet0202ForRelease_Cabinet0202Releasing = NewTransition(sMovingToCabinet0202ForRelease, sCabinet0202Releasing, EnumMacMsBoxTransferTransition.ReleaseAtCB0202);
            MacTransition tMovingToCabinet0203ForRelease_Cabinet0203Releasing = NewTransition(sMovingToCabinet0203ForRelease, sCabinet0203Releasing, EnumMacMsBoxTransferTransition.ReleaseAtCB0203);
            MacTransition tMovingToCabinet0204ForRelease_Cabinet0204Releasing = NewTransition(sMovingToCabinet0204ForRelease, sCabinet0204Releasing, EnumMacMsBoxTransferTransition.ReleaseAtCB0204);
            MacTransition tMovingToCabinet0205ForRelease_Cabinet0205Releasing = NewTransition(sMovingToCabinet0205ForRelease, sCabinet0205Releasing, EnumMacMsBoxTransferTransition.ReleaseAtCB0205);
            MacTransition tMovingToCabinet0301ForRelease_Cabinet0301Releasing = NewTransition(sMovingToCabinet0301ForRelease, sCabinet0301Releasing, EnumMacMsBoxTransferTransition.ReleaseAtCB0301);
            MacTransition tMovingToCabinet0302ForRelease_Cabinet0302Releasing = NewTransition(sMovingToCabinet0302ForRelease, sCabinet0302Releasing, EnumMacMsBoxTransferTransition.ReleaseAtCB0302);
            MacTransition tMovingToCabinet0303ForRelease_Cabinet0303Releasing = NewTransition(sMovingToCabinet0303ForRelease, sCabinet0303Releasing, EnumMacMsBoxTransferTransition.ReleaseAtCB0303);
            MacTransition tMovingToCabinet0304ForRelease_Cabinet0304Releasing = NewTransition(sMovingToCabinet0304ForRelease, sCabinet0304Releasing, EnumMacMsBoxTransferTransition.ReleaseAtCB0304);
            MacTransition tMovingToCabinet0305ForRelease_Cabinet0305Releasing = NewTransition(sMovingToCabinet0305ForRelease, sCabinet0305Releasing, EnumMacMsBoxTransferTransition.ReleaseAtCB0305);
            */

            MacTransition transitionMovingToDrawerForRelease_DrawerReleasing = NewTransition(stateMovingToDrawerForRelease, stateDrawerReleasing, EnumMacMsBoxTransferTransition.ReleaseAtDrawer);

            /** Obsolete
            MacTransition tCabinet0101Releasing_MovingToCB1HomeFromCabinet0101 = NewTransition(sCabinet0101Releasing, sMovingToCB1HomeFromCabinet0101, EnumMacMsBoxTransferTransition.MoveToCB1HomeFromCB0101);
            MacTransition tCabinet0102Releasing_MovingToCB1HomeFromCabinet0102 = NewTransition(sCabinet0102Releasing, sMovingToCB1HomeFromCabinet0102, EnumMacMsBoxTransferTransition.MoveToCB1HomeFromCB0102);
            MacTransition tCabinet0103Releasing_MovingToCB1HomeFromCabinet0103 = NewTransition(sCabinet0103Releasing, sMovingToCB1HomeFromCabinet0103, EnumMacMsBoxTransferTransition.MoveToCB1HomeFromCB0103);
            MacTransition tCabinet0104Releasing_MovingToCB1HomeFromCabinet0104 = NewTransition(sCabinet0104Releasing, sMovingToCB1HomeFromCabinet0104, EnumMacMsBoxTransferTransition.MoveToCB1HomeFromCB0104);
            MacTransition tCabinet0105Releasing_MovingToCB1HomeFromCabinet0105 = NewTransition(sCabinet0105Releasing, sMovingToCB1HomeFromCabinet0105, EnumMacMsBoxTransferTransition.MoveToCB1HomeFromCB0105);
            MacTransition tCabinet0201Releasing_MovingToCB1HomeFromCabinet0201 = NewTransition(sCabinet0201Releasing, sMovingToCB1HomeFromCabinet0201, EnumMacMsBoxTransferTransition.MoveToCB1HomeFromCB0201);
            MacTransition tCabinet0202Releasing_MovingToCB1HomeFromCabinet0202 = NewTransition(sCabinet0202Releasing, sMovingToCB1HomeFromCabinet0202, EnumMacMsBoxTransferTransition.MoveToCB1HomeFromCB0202);
            MacTransition tCabinet0203Releasing_MovingToCB1HomeFromCabinet0203 = NewTransition(sCabinet0203Releasing, sMovingToCB1HomeFromCabinet0203, EnumMacMsBoxTransferTransition.MoveToCB1HomeFromCB0203);
            MacTransition tCabinet0204Releasing_MovingToCB1HomeFromCabinet0204 = NewTransition(sCabinet0204Releasing, sMovingToCB1HomeFromCabinet0204, EnumMacMsBoxTransferTransition.MoveToCB1HomeFromCB0204);
            MacTransition tCabinet0205Releasing_MovingToCB1HomeFromCabinet0205 = NewTransition(sCabinet0205Releasing, sMovingToCB1HomeFromCabinet0205, EnumMacMsBoxTransferTransition.MoveToCB1HomeFromCB0205);
            MacTransition tCabinet0301Releasing_MovingToCB1HomeFromCabinet0301 = NewTransition(sCabinet0301Releasing, sMovingToCB1HomeFromCabinet0301, EnumMacMsBoxTransferTransition.MoveToCB1HomeFromCB0301);
            MacTransition tCabinet0302Releasing_MovingToCB1HomeFromCabinet0302 = NewTransition(sCabinet0302Releasing, sMovingToCB1HomeFromCabinet0302, EnumMacMsBoxTransferTransition.MoveToCB1HomeFromCB0302);
            MacTransition tCabinet0303Releasing_MovingToCB1HomeFromCabinet0303 = NewTransition(sCabinet0303Releasing, sMovingToCB1HomeFromCabinet0303, EnumMacMsBoxTransferTransition.MoveToCB1HomeFromCB0303);
            MacTransition tCabinet0304Releasing_MovingToCB1HomeFromCabinet0304 = NewTransition(sCabinet0304Releasing, sMovingToCB1HomeFromCabinet0304, EnumMacMsBoxTransferTransition.MoveToCB1HomeFromCB0304);
            MacTransition tCabinet0305Releasing_MovingToCB1HomeFromCabinet0305 = NewTransition(sCabinet0305Releasing, sMovingToCB1HomeFromCabinet0305, EnumMacMsBoxTransferTransition.MoveToCB1HomeFromCB0305);
            */
            MacTransition transitionDrawerReleasing_MovingToCB1HomeFromDrawer = NewTransition(stateDrawerReleasing, stateMovingToCB1HomeFromDrawer, EnumMacMsBoxTransferTransition.MoveToCB1HomeFromDrawer);


            /** Obsolete
            MacTransition tMovingToCB1HomeFromCabinet0101_CB1Home = NewTransition(sMovingToCB1HomeFromCabinet0101, sCB1Home, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeFromCB0101);
            MacTransition tMovingToCB1HomeFromCabinet0102_CB1Home = NewTransition(sMovingToCB1HomeFromCabinet0102, sCB1Home, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeFromCB0102);
            MacTransition tMovingToCB1HomeFromCabinet0103_CB1Home = NewTransition(sMovingToCB1HomeFromCabinet0103, sCB1Home, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeFromCB0103);
            MacTransition tMovingToCB1HomeFromCabinet0104_CB1Home = NewTransition(sMovingToCB1HomeFromCabinet0104, sCB1Home, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeFromCB0104);
            MacTransition tMovingToCB1HomeFromCabinet0105_CB1Home = NewTransition(sMovingToCB1HomeFromCabinet0105, sCB1Home, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeFromCB0105);
            MacTransition tMovingToCB1HomeFromCabinet0201_CB1Home = NewTransition(sMovingToCB1HomeFromCabinet0201, sCB1Home, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeFromCB0201);
            MacTransition tMovingToCB1HomeFromCabinet0202_CB1Home = NewTransition(sMovingToCB1HomeFromCabinet0202, sCB1Home, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeFromCB0202);
            MacTransition tMovingToCB1HomeFromCabinet0203_CB1Home = NewTransition(sMovingToCB1HomeFromCabinet0203, sCB1Home, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeFromCB0203);
            MacTransition tMovingToCB1HomeFromCabinet0204_CB1Home = NewTransition(sMovingToCB1HomeFromCabinet0204, sCB1Home, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeFromCB0204);
            MacTransition tMovingToCB1HomeFromCabinet0205_CB1Home = NewTransition(sMovingToCB1HomeFromCabinet0205, sCB1Home, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeFromCB0205);
            MacTransition tMovingToCB1HomeFromCabinet0301_CB1Home = NewTransition(sMovingToCB1HomeFromCabinet0301, sCB1Home, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeFromCB0301);
            MacTransition tMovingToCB1HomeFromCabinet0302_CB1Home = NewTransition(sMovingToCB1HomeFromCabinet0302, sCB1Home, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeFromCB0302);
            MacTransition tMovingToCB1HomeFromCabinet0303_CB1Home = NewTransition(sMovingToCB1HomeFromCabinet0303, sCB1Home, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeFromCB0303);
            MacTransition tMovingToCB1HomeFromCabinet0304_CB1Home = NewTransition(sMovingToCB1HomeFromCabinet0304, sCB1Home, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeFromCB0304);
            MacTransition tMovingToCB1HomeFromCabinet0305_CB1Home = NewTransition(sMovingToCB1HomeFromCabinet0305, sCB1Home, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeFromCB0305);
            */
            //  StandbyAtCB1HomeFromCB0305
            MacTransition transitionMovingToCB1HomeFromDrawer_CB1Home = NewTransition(stateMovingToCB1HomeFromDrawer, sCB1Home, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeFromDrawer);
            #endregion Release
            #endregion CB1

            #region CB2
            #region Get
            /** Obsolete
            MacTransition tCB1Home_MovingToCabinet0401 = NewTransition(sCB1Home, sMovingToCabinet0401, EnumMacMsBoxTransferTransition.MoveToCB0401);
            MacTransition tCB1Home_MovingToCabinet0402 = NewTransition(sCB1Home, sMovingToCabinet0402, EnumMacMsBoxTransferTransition.MoveToCB0402);
            MacTransition tCB1Home_MovingToCabinet0403 = NewTransition(sCB1Home, sMovingToCabinet0403, EnumMacMsBoxTransferTransition.MoveToCB0403);
            MacTransition tCB1Home_MovingToCabinet0404 = NewTransition(sCB1Home, sMovingToCabinet0404, EnumMacMsBoxTransferTransition.MoveToCB0404);
            MacTransition tCB1Home_MovingToCabinet0405 = NewTransition(sCB1Home, sMovingToCabinet0405, EnumMacMsBoxTransferTransition.MoveToCB0405);
            MacTransition tCB1Home_MovingToCabinet0501 = NewTransition(sCB1Home, sMovingToCabinet0501, EnumMacMsBoxTransferTransition.MoveToCB0501);
            MacTransition tCB1Home_MovingToCabinet0502 = NewTransition(sCB1Home, sMovingToCabinet0502, EnumMacMsBoxTransferTransition.MoveToCB0502);
            MacTransition tCB1Home_MovingToCabinet0503 = NewTransition(sCB1Home, sMovingToCabinet0503, EnumMacMsBoxTransferTransition.MoveToCB0503);
            MacTransition tCB1Home_MovingToCabinet0504 = NewTransition(sCB1Home, sMovingToCabinet0504, EnumMacMsBoxTransferTransition.MoveToCB0504);
            MacTransition tCB1Home_MovingToCabinet0505 = NewTransition(sCB1Home, sMovingToCabinet0505, EnumMacMsBoxTransferTransition.MoveToCB0505);
            MacTransition tCB1Home_MovingToCabinet0601 = NewTransition(sCB1Home, sMovingToCabinet0601, EnumMacMsBoxTransferTransition.MoveToCB0601);
            MacTransition tCB1Home_MovingToCabinet0602 = NewTransition(sCB1Home, sMovingToCabinet0602, EnumMacMsBoxTransferTransition.MoveToCB0602);
            MacTransition tCB1Home_MovingToCabinet0603 = NewTransition(sCB1Home, sMovingToCabinet0603, EnumMacMsBoxTransferTransition.MoveToCB0603);
            MacTransition tCB1Home_MovingToCabinet0604 = NewTransition(sCB1Home, sMovingToCabinet0604, EnumMacMsBoxTransferTransition.MoveToCB0604);
            MacTransition tCB1Home_MovingToCabinet0605 = NewTransition(sCB1Home, sMovingToCabinet0605, EnumMacMsBoxTransferTransition.MoveToCB0605);
            MacTransition tCB1Home_MovingToCabinet0701 = NewTransition(sCB1Home, sMovingToCabinet0701, EnumMacMsBoxTransferTransition.MoveToCB0701);
            MacTransition tCB1Home_MovingToCabinet0702 = NewTransition(sCB1Home, sMovingToCabinet0702, EnumMacMsBoxTransferTransition.MoveToCB0702);
            MacTransition tCB1Home_MovingToCabinet0703 = NewTransition(sCB1Home, sMovingToCabinet0703, EnumMacMsBoxTransferTransition.MoveToCB0703);
            MacTransition tCB1Home_MovingToCabinet0704 = NewTransition(sCB1Home, sMovingToCabinet0704, EnumMacMsBoxTransferTransition.MoveToCB0704);
            MacTransition tCB1Home_MovingToCabinet0705 = NewTransition(sCB1Home, sMovingToCabinet0705, EnumMacMsBoxTransferTransition.MoveToCB0705);
            */
            /** Obsolete
            MacTransition tMovingToCabinet0401_Cabinet0401Clamping = NewTransition(sMovingToCabinet0401, sCabinet0401Clamping, EnumMacMsBoxTransferTransition.ClampAtCB0401);
            MacTransition tMovingToCabinet0402_Cabinet0402Clamping = NewTransition(sMovingToCabinet0402, sCabinet0402Clamping, EnumMacMsBoxTransferTransition.ClampAtCB0402);
            MacTransition tMovingToCabinet0403_Cabinet0403Clamping = NewTransition(sMovingToCabinet0403, sCabinet0403Clamping, EnumMacMsBoxTransferTransition.ClampAtCB0403);
            MacTransition tMovingToCabinet0404_Cabinet0404Clamping = NewTransition(sMovingToCabinet0404, sCabinet0404Clamping, EnumMacMsBoxTransferTransition.ClampAtCB0404);
            MacTransition tMovingToCabinet0405_Cabinet0405Clamping = NewTransition(sMovingToCabinet0405, sCabinet0405Clamping, EnumMacMsBoxTransferTransition.ClampAtCB0405);
            MacTransition tMovingToCabinet0501_Cabinet0501Clamping = NewTransition(sMovingToCabinet0501, sCabinet0501Clamping, EnumMacMsBoxTransferTransition.ClampAtCB0501);
            MacTransition tMovingToCabinet0502_Cabinet0502Clamping = NewTransition(sMovingToCabinet0502, sCabinet0502Clamping, EnumMacMsBoxTransferTransition.ClampAtCB0502);
            MacTransition tMovingToCabinet0503_Cabinet0503Clamping = NewTransition(sMovingToCabinet0503, sCabinet0503Clamping, EnumMacMsBoxTransferTransition.ClampAtCB0503);
            MacTransition tMovingToCabinet0504_Cabinet0504Clamping = NewTransition(sMovingToCabinet0504, sCabinet0504Clamping, EnumMacMsBoxTransferTransition.ClampAtCB0504);
            MacTransition tMovingToCabinet0505_Cabinet0505Clamping = NewTransition(sMovingToCabinet0505, sCabinet0505Clamping, EnumMacMsBoxTransferTransition.ClampAtCB0505);
            MacTransition tMovingToCabinet0601_Cabinet0601Clamping = NewTransition(sMovingToCabinet0601, sCabinet0601Clamping, EnumMacMsBoxTransferTransition.ClampAtCB0601);
            MacTransition tMovingToCabinet0602_Cabinet0602Clamping = NewTransition(sMovingToCabinet0602, sCabinet0602Clamping, EnumMacMsBoxTransferTransition.ClampAtCB0602);
            MacTransition tMovingToCabinet0603_Cabinet0603Clamping = NewTransition(sMovingToCabinet0603, sCabinet0603Clamping, EnumMacMsBoxTransferTransition.ClampAtCB0603);
            MacTransition tMovingToCabinet0604_Cabinet0604Clamping = NewTransition(sMovingToCabinet0604, sCabinet0604Clamping, EnumMacMsBoxTransferTransition.ClampAtCB0604);
            MacTransition tMovingToCabinet0605_Cabinet0605Clamping = NewTransition(sMovingToCabinet0605, sCabinet0605Clamping, EnumMacMsBoxTransferTransition.ClampAtCB0605);
            MacTransition tMovingToCabinet0701_Cabinet0701Clamping = NewTransition(sMovingToCabinet0701, sCabinet0701Clamping, EnumMacMsBoxTransferTransition.ClampAtCB0701);
            MacTransition tMovingToCabinet0702_Cabinet0702Clamping = NewTransition(sMovingToCabinet0702, sCabinet0702Clamping, EnumMacMsBoxTransferTransition.ClampAtCB0702);
            MacTransition tMovingToCabinet0703_Cabinet0703Clamping = NewTransition(sMovingToCabinet0703, sCabinet0703Clamping, EnumMacMsBoxTransferTransition.ClampAtCB0703);
            MacTransition tMovingToCabinet0704_Cabinet0704Clamping = NewTransition(sMovingToCabinet0704, sCabinet0704Clamping, EnumMacMsBoxTransferTransition.ClampAtCB0704);
            MacTransition tMovingToCabinet0705_Cabinet0705Clamping = NewTransition(sMovingToCabinet0705, sCabinet0705Clamping, EnumMacMsBoxTransferTransition.ClampAtCB0705);
            */
            /**Obsolete
            MacTransition tCabinet0401Clamping_MovingToCB1HomeClampedFromCabinet0401 = NewTransition(sCabinet0401Clamping, sMovingToCB1HomeClampedFromCabinet0401, EnumMacMsBoxTransferTransition.MoveToCB1HomeClampedFromCB0401);
            MacTransition tCabinet0402Clamping_MovingToCB1HomeClampedFromCabinet0402 = NewTransition(sCabinet0402Clamping, sMovingToCB1HomeClampedFromCabinet0402, EnumMacMsBoxTransferTransition.MoveToCB1HomeClampedFromCB0402);
            MacTransition tCabinet0403Clamping_MovingToCB1HomeClampedFromCabinet0403 = NewTransition(sCabinet0403Clamping, sMovingToCB1HomeClampedFromCabinet0403, EnumMacMsBoxTransferTransition.MoveToCB1HomeClampedFromCB0403);
            MacTransition tCabinet0404Clamping_MovingToCB1HomeClampedFromCabinet0404 = NewTransition(sCabinet0404Clamping, sMovingToCB1HomeClampedFromCabinet0404, EnumMacMsBoxTransferTransition.MoveToCB1HomeClampedFromCB0404);
            MacTransition tCabinet0405Clamping_MovingToCB1HomeClampedFromCabinet0405 = NewTransition(sCabinet0405Clamping, sMovingToCB1HomeClampedFromCabinet0405, EnumMacMsBoxTransferTransition.MoveToCB1HomeClampedFromCB0405);
            MacTransition tCabinet0501Clamping_MovingToCB1HomeClampedFromCabinet0501 = NewTransition(sCabinet0501Clamping, sMovingToCB1HomeClampedFromCabinet0501, EnumMacMsBoxTransferTransition.MoveToCB1HomeClampedFromCB0501);
            MacTransition tCabinet0502Clamping_MovingToCB1HomeClampedFromCabinet0502 = NewTransition(sCabinet0502Clamping, sMovingToCB1HomeClampedFromCabinet0502, EnumMacMsBoxTransferTransition.MoveToCB1HomeClampedFromCB0502);
            MacTransition tCabinet0503Clamping_MovingToCB1HomeClampedFromCabinet0503 = NewTransition(sCabinet0503Clamping, sMovingToCB1HomeClampedFromCabinet0503, EnumMacMsBoxTransferTransition.MoveToCB1HomeClampedFromCB0503);
            MacTransition tCabinet0504Clamping_MovingToCB1HomeClampedFromCabinet0504 = NewTransition(sCabinet0504Clamping, sMovingToCB1HomeClampedFromCabinet0504, EnumMacMsBoxTransferTransition.MoveToCB1HomeClampedFromCB0504);
            MacTransition tCabinet0505Clamping_MovingToCB1HomeClampedFromCabinet0505 = NewTransition(sCabinet0505Clamping, sMovingToCB1HomeClampedFromCabinet0505, EnumMacMsBoxTransferTransition.MoveToCB1HomeClampedFromCB0505);
            MacTransition tCabinet0601Clamping_MovingToCB1HomeClampedFromCabinet0601 = NewTransition(sCabinet0601Clamping, sMovingToCB1HomeClampedFromCabinet0601, EnumMacMsBoxTransferTransition.MoveToCB1HomeClampedFromCB0601);
            MacTransition tCabinet0602Clamping_MovingToCB1HomeClampedFromCabinet0602 = NewTransition(sCabinet0602Clamping, sMovingToCB1HomeClampedFromCabinet0602, EnumMacMsBoxTransferTransition.MoveToCB1HomeClampedFromCB0602);
            MacTransition tCabinet0603Clamping_MovingToCB1HomeClampedFromCabinet0603 = NewTransition(sCabinet0603Clamping, sMovingToCB1HomeClampedFromCabinet0603, EnumMacMsBoxTransferTransition.MoveToCB1HomeClampedFromCB0603);
            MacTransition tCabinet0604Clamping_MovingToCB1HomeClampedFromCabinet0604 = NewTransition(sCabinet0604Clamping, sMovingToCB1HomeClampedFromCabinet0604, EnumMacMsBoxTransferTransition.MoveToCB1HomeClampedFromCB0604);
            MacTransition tCabinet0605Clamping_MovingToCB1HomeClampedFromCabinet0605 = NewTransition(sCabinet0605Clamping, sMovingToCB1HomeClampedFromCabinet0605, EnumMacMsBoxTransferTransition.MoveToCB1HomeClampedFromCB0605);
            MacTransition tCabinet0701Clamping_MovingToCB1HomeClampedFromCabinet0701 = NewTransition(sCabinet0701Clamping, sMovingToCB1HomeClampedFromCabinet0701, EnumMacMsBoxTransferTransition.MoveToCB1HomeClampedFromCB0701);
            MacTransition tCabinet0702Clamping_MovingToCB1HomeClampedFromCabinet0702 = NewTransition(sCabinet0702Clamping, sMovingToCB1HomeClampedFromCabinet0702, EnumMacMsBoxTransferTransition.MoveToCB1HomeClampedFromCB0702);
            MacTransition tCabinet0703Clamping_MovingToCB1HomeClampedFromCabinet0703 = NewTransition(sCabinet0703Clamping, sMovingToCB1HomeClampedFromCabinet0703, EnumMacMsBoxTransferTransition.MoveToCB1HomeClampedFromCB0703);
            MacTransition tCabinet0704Clamping_MovingToCB1HomeClampedFromCabinet0704 = NewTransition(sCabinet0704Clamping, sMovingToCB1HomeClampedFromCabinet0704, EnumMacMsBoxTransferTransition.MoveToCB1HomeClampedFromCB0704);
            MacTransition tCabinet0705Clamping_MovingToCB1HomeClampedFromCabinet0705 = NewTransition(sCabinet0705Clamping, sMovingToCB1HomeClampedFromCabinet0705, EnumMacMsBoxTransferTransition.MoveToCB1HomeClampedFromCB0705);
            */
            /** Obsolete
            MacTransition tMovingToCB1HomeClampedFromCabinet0401_CB1HomeClamped = NewTransition(sMovingToCB1HomeClampedFromCabinet0401, sCB1HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeClampedFromCB0401);
            MacTransition tMovingToCB1HomeClampedFromCabinet0402_CB1HomeClamped = NewTransition(sMovingToCB1HomeClampedFromCabinet0402, sCB1HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeClampedFromCB0402);
            MacTransition tMovingToCB1HomeClampedFromCabinet0403_CB1HomeClamped = NewTransition(sMovingToCB1HomeClampedFromCabinet0403, sCB1HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeClampedFromCB0403);
            MacTransition tMovingToCB1HomeClampedFromCabinet0404_CB1HomeClamped = NewTransition(sMovingToCB1HomeClampedFromCabinet0404, sCB1HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeClampedFromCB0404);
            MacTransition tMovingToCB1HomeClampedFromCabinet0405_CB1HomeClamped = NewTransition(sMovingToCB1HomeClampedFromCabinet0405, sCB1HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeClampedFromCB0405);
            MacTransition tMovingToCB1HomeClampedFromCabinet0501_CB1HomeClamped = NewTransition(sMovingToCB1HomeClampedFromCabinet0501, sCB1HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeClampedFromCB0501);
            MacTransition tMovingToCB1HomeClampedFromCabinet0502_CB1HomeClamped = NewTransition(sMovingToCB1HomeClampedFromCabinet0502, sCB1HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeClampedFromCB0502);
            MacTransition tMovingToCB1HomeClampedFromCabinet0503_CB1HomeClamped = NewTransition(sMovingToCB1HomeClampedFromCabinet0503, sCB1HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeClampedFromCB0503);
            MacTransition tMovingToCB1HomeClampedFromCabinet0504_CB1HomeClamped = NewTransition(sMovingToCB1HomeClampedFromCabinet0504, sCB1HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeClampedFromCB0504);
            MacTransition tMovingToCB1HomeClampedFromCabinet0505_CB1HomeClamped = NewTransition(sMovingToCB1HomeClampedFromCabinet0505, sCB1HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeClampedFromCB0505);
            MacTransition tMovingToCB1HomeClampedFromCabinet0601_CB1HomeClamped = NewTransition(sMovingToCB1HomeClampedFromCabinet0601, sCB1HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeClampedFromCB0601);
            MacTransition tMovingToCB1HomeClampedFromCabinet0602_CB1HomeClamped = NewTransition(sMovingToCB1HomeClampedFromCabinet0602, sCB1HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeClampedFromCB0602);
            MacTransition tMovingToCB1HomeClampedFromCabinet0603_CB1HomeClamped = NewTransition(sMovingToCB1HomeClampedFromCabinet0603, sCB1HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeClampedFromCB0603);
            MacTransition tMovingToCB1HomeClampedFromCabinet0604_CB1HomeClamped = NewTransition(sMovingToCB1HomeClampedFromCabinet0604, sCB1HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeClampedFromCB0604);
            MacTransition tMovingToCB1HomeClampedFromCabinet0605_CB1HomeClamped = NewTransition(sMovingToCB1HomeClampedFromCabinet0605, sCB1HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeClampedFromCB0605);
            MacTransition tMovingToCB1HomeClampedFromCabinet0701_CB1HomeClamped = NewTransition(sMovingToCB1HomeClampedFromCabinet0701, sCB1HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeClampedFromCB0701);
            MacTransition tMovingToCB1HomeClampedFromCabinet0702_CB1HomeClamped = NewTransition(sMovingToCB1HomeClampedFromCabinet0702, sCB1HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeClampedFromCB0702);
            MacTransition tMovingToCB1HomeClampedFromCabinet0703_CB1HomeClamped = NewTransition(sMovingToCB1HomeClampedFromCabinet0703, sCB1HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeClampedFromCB0703);
            MacTransition tMovingToCB1HomeClampedFromCabinet0704_CB1HomeClamped = NewTransition(sMovingToCB1HomeClampedFromCabinet0704, sCB1HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeClampedFromCB0704);
            MacTransition tMovingToCB1HomeClampedFromCabinet0705_CB1HomeClamped = NewTransition(sMovingToCB1HomeClampedFromCabinet0705, sCB1HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeClampedFromCB0705);
            */
            #endregion Get

            #region Release
            /** Obsolete
            MacTransition tCB1HomeClamped_MovingToCabinet0401ForRelease = NewTransition(sCB1HomeClamped, sMovingToCabinet0401ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0401ForRelease);
            MacTransition tCB1HomeClamped_MovingToCabinet0402ForRelease = NewTransition(sCB1HomeClamped, sMovingToCabinet0402ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0402ForRelease);
            MacTransition tCB1HomeClamped_MovingToCabinet0403ForRelease = NewTransition(sCB1HomeClamped, sMovingToCabinet0403ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0403ForRelease);
            MacTransition tCB1HomeClamped_MovingToCabinet0404ForRelease = NewTransition(sCB1HomeClamped, sMovingToCabinet0404ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0404ForRelease);
            MacTransition tCB1HomeClamped_MovingToCabinet0405ForRelease = NewTransition(sCB1HomeClamped, sMovingToCabinet0405ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0405ForRelease);
            MacTransition tCB1HomeClamped_MovingToCabinet0501ForRelease = NewTransition(sCB1HomeClamped, sMovingToCabinet0501ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0501ForRelease);
            MacTransition tCB1HomeClamped_MovingToCabinet0502ForRelease = NewTransition(sCB1HomeClamped, sMovingToCabinet0502ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0502ForRelease);
            MacTransition tCB1HomeClamped_MovingToCabinet0503ForRelease = NewTransition(sCB1HomeClamped, sMovingToCabinet0503ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0503ForRelease);
            MacTransition tCB1HomeClamped_MovingToCabinet0504ForRelease = NewTransition(sCB1HomeClamped, sMovingToCabinet0504ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0504ForRelease);
            MacTransition tCB1HomeClamped_MovingToCabinet0505ForRelease = NewTransition(sCB1HomeClamped, sMovingToCabinet0505ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0505ForRelease);
            MacTransition tCB1HomeClamped_MovingToCabinet0601ForRelease = NewTransition(sCB1HomeClamped, sMovingToCabinet0601ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0601ForRelease);
            MacTransition tCB1HomeClamped_MovingToCabinet0602ForRelease = NewTransition(sCB1HomeClamped, sMovingToCabinet0602ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0602ForRelease);
            MacTransition tCB1HomeClamped_MovingToCabinet0603ForRelease = NewTransition(sCB1HomeClamped, sMovingToCabinet0603ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0603ForRelease);
            MacTransition tCB1HomeClamped_MovingToCabinet0604ForRelease = NewTransition(sCB1HomeClamped, sMovingToCabinet0604ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0604ForRelease);
            MacTransition tCB1HomeClamped_MovingToCabinet0605ForRelease = NewTransition(sCB1HomeClamped, sMovingToCabinet0605ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0605ForRelease);
            MacTransition tCB1HomeClamped_MovingToCabinet0701ForRelease = NewTransition(sCB1HomeClamped, sMovingToCabinet0701ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0701ForRelease);
            MacTransition tCB1HomeClamped_MovingToCabinet0702ForRelease = NewTransition(sCB1HomeClamped, sMovingToCabinet0702ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0702ForRelease);
            MacTransition tCB1HomeClamped_MovingToCabinet0703ForRelease = NewTransition(sCB1HomeClamped, sMovingToCabinet0703ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0703ForRelease);
            MacTransition tCB1HomeClamped_MovingToCabinet0704ForRelease = NewTransition(sCB1HomeClamped, sMovingToCabinet0704ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0704ForRelease);
            MacTransition tCB1HomeClamped_MovingToCabinet0705ForRelease = NewTransition(sCB1HomeClamped, sMovingToCabinet0705ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0705ForRelease);
            */
            /** Obsolete
            MacTransition tMovingToCabinet0401ForRelease_Cabinet0401Releasing = NewTransition(sMovingToCabinet0401ForRelease, sCabinet0401Releasing, EnumMacMsBoxTransferTransition.ReleaseAtCB0401);
            MacTransition tMovingToCabinet0402ForRelease_Cabinet0402Releasing = NewTransition(sMovingToCabinet0402ForRelease, sCabinet0402Releasing, EnumMacMsBoxTransferTransition.ReleaseAtCB0402);
            MacTransition tMovingToCabinet0403ForRelease_Cabinet0403Releasing = NewTransition(sMovingToCabinet0403ForRelease, sCabinet0403Releasing, EnumMacMsBoxTransferTransition.ReleaseAtCB0403);
            MacTransition tMovingToCabinet0404ForRelease_Cabinet0404Releasing = NewTransition(sMovingToCabinet0404ForRelease, sCabinet0404Releasing, EnumMacMsBoxTransferTransition.ReleaseAtCB0404);
            MacTransition tMovingToCabinet0405ForRelease_Cabinet0405Releasing = NewTransition(sMovingToCabinet0405ForRelease, sCabinet0405Releasing, EnumMacMsBoxTransferTransition.ReleaseAtCB0405);
            MacTransition tMovingToCabinet0501ForRelease_Cabinet0501Releasing = NewTransition(sMovingToCabinet0501ForRelease, sCabinet0501Releasing, EnumMacMsBoxTransferTransition.ReleaseAtCB0501);
            MacTransition tMovingToCabinet0502ForRelease_Cabinet0502Releasing = NewTransition(sMovingToCabinet0502ForRelease, sCabinet0502Releasing, EnumMacMsBoxTransferTransition.ReleaseAtCB0502);
            MacTransition tMovingToCabinet0503ForRelease_Cabinet0503Releasing = NewTransition(sMovingToCabinet0503ForRelease, sCabinet0503Releasing, EnumMacMsBoxTransferTransition.ReleaseAtCB0503);
            MacTransition tMovingToCabinet0504ForRelease_Cabinet0504Releasing = NewTransition(sMovingToCabinet0504ForRelease, sCabinet0504Releasing, EnumMacMsBoxTransferTransition.ReleaseAtCB0504);
            MacTransition tMovingToCabinet0505ForRelease_Cabinet0505Releasing = NewTransition(sMovingToCabinet0505ForRelease, sCabinet0505Releasing, EnumMacMsBoxTransferTransition.ReleaseAtCB0505);
            MacTransition tMovingToCabinet0601ForRelease_Cabinet0601Releasing = NewTransition(sMovingToCabinet0601ForRelease, sCabinet0601Releasing, EnumMacMsBoxTransferTransition.ReleaseAtCB0601);
            MacTransition tMovingToCabinet0602ForRelease_Cabinet0602Releasing = NewTransition(sMovingToCabinet0602ForRelease, sCabinet0602Releasing, EnumMacMsBoxTransferTransition.ReleaseAtCB0602);
            MacTransition tMovingToCabinet0603ForRelease_Cabinet0603Releasing = NewTransition(sMovingToCabinet0603ForRelease, sCabinet0603Releasing, EnumMacMsBoxTransferTransition.ReleaseAtCB0603);
            MacTransition tMovingToCabinet0604ForRelease_Cabinet0604Releasing = NewTransition(sMovingToCabinet0604ForRelease, sCabinet0604Releasing, EnumMacMsBoxTransferTransition.ReleaseAtCB0604);
            MacTransition tMovingToCabinet0605ForRelease_Cabinet0605Releasing = NewTransition(sMovingToCabinet0605ForRelease, sCabinet0605Releasing, EnumMacMsBoxTransferTransition.ReleaseAtCB0605);
            MacTransition tMovingToCabinet0701ForRelease_Cabinet0701Releasing = NewTransition(sMovingToCabinet0701ForRelease, sCabinet0701Releasing, EnumMacMsBoxTransferTransition.ReleaseAtCB0701);
            MacTransition tMovingToCabinet0702ForRelease_Cabinet0702Releasing = NewTransition(sMovingToCabinet0702ForRelease, sCabinet0702Releasing, EnumMacMsBoxTransferTransition.ReleaseAtCB0702);
            MacTransition tMovingToCabinet0703ForRelease_Cabinet0703Releasing = NewTransition(sMovingToCabinet0703ForRelease, sCabinet0703Releasing, EnumMacMsBoxTransferTransition.ReleaseAtCB0703);
            MacTransition tMovingToCabinet0704ForRelease_Cabinet0704Releasing = NewTransition(sMovingToCabinet0704ForRelease, sCabinet0704Releasing, EnumMacMsBoxTransferTransition.ReleaseAtCB0704);
            MacTransition tMovingToCabinet0705ForRelease_Cabinet0705Releasing = NewTransition(sMovingToCabinet0705ForRelease, sCabinet0705Releasing, EnumMacMsBoxTransferTransition.ReleaseAtCB0705);
            */
            /** Obsolete
            MacTransition tCabinet0401Releasing_MovingToCB1HomeFromCabinet0401 = NewTransition(sCabinet0401Releasing, sMovingToCB1HomeFromCabinet0401, EnumMacMsBoxTransferTransition.MoveToCB1HomeFromCB0401);
            MacTransition tCabinet0402Releasing_MovingToCB1HomeFromCabinet0402 = NewTransition(sCabinet0402Releasing, sMovingToCB1HomeFromCabinet0402, EnumMacMsBoxTransferTransition.MoveToCB1HomeFromCB0402);
            MacTransition tCabinet0403Releasing_MovingToCB1HomeFromCabinet0403 = NewTransition(sCabinet0403Releasing, sMovingToCB1HomeFromCabinet0403, EnumMacMsBoxTransferTransition.MoveToCB1HomeFromCB0403);
            MacTransition tCabinet0404Releasing_MovingToCB1HomeFromCabinet0404 = NewTransition(sCabinet0404Releasing, sMovingToCB1HomeFromCabinet0404, EnumMacMsBoxTransferTransition.MoveToCB1HomeFromCB0404);
            MacTransition tCabinet0405Releasing_MovingToCB1HomeFromCabinet0405 = NewTransition(sCabinet0405Releasing, sMovingToCB1HomeFromCabinet0405, EnumMacMsBoxTransferTransition.MoveToCB1HomeFromCB0405);
            MacTransition tCabinet0501Releasing_MovingToCB1HomeFromCabinet0501 = NewTransition(sCabinet0501Releasing, sMovingToCB1HomeFromCabinet0501, EnumMacMsBoxTransferTransition.MoveToCB1HomeFromCB0501);
            MacTransition tCabinet0502Releasing_MovingToCB1HomeFromCabinet0502 = NewTransition(sCabinet0502Releasing, sMovingToCB1HomeFromCabinet0502, EnumMacMsBoxTransferTransition.MoveToCB1HomeFromCB0502);
            MacTransition tCabinet0503Releasing_MovingToCB1HomeFromCabinet0503 = NewTransition(sCabinet0503Releasing, sMovingToCB1HomeFromCabinet0503, EnumMacMsBoxTransferTransition.MoveToCB1HomeFromCB0503);
            MacTransition tCabinet0504Releasing_MovingToCB1HomeFromCabinet0504 = NewTransition(sCabinet0504Releasing, sMovingToCB1HomeFromCabinet0504, EnumMacMsBoxTransferTransition.MoveToCB1HomeFromCB0504);
            MacTransition tCabinet0505Releasing_MovingToCB1HomeFromCabinet0505 = NewTransition(sCabinet0505Releasing, sMovingToCB1HomeFromCabinet0505, EnumMacMsBoxTransferTransition.MoveToCB1HomeFromCB0505);
            MacTransition tCabinet0601Releasing_MovingToCB1HomeFromCabinet0601 = NewTransition(sCabinet0601Releasing, sMovingToCB1HomeFromCabinet0601, EnumMacMsBoxTransferTransition.MoveToCB1HomeFromCB0601);
            MacTransition tCabinet0602Releasing_MovingToCB1HomeFromCabinet0602 = NewTransition(sCabinet0602Releasing, sMovingToCB1HomeFromCabinet0602, EnumMacMsBoxTransferTransition.MoveToCB1HomeFromCB0602);
            MacTransition tCabinet0603Releasing_MovingToCB1HomeFromCabinet0603 = NewTransition(sCabinet0603Releasing, sMovingToCB1HomeFromCabinet0603, EnumMacMsBoxTransferTransition.MoveToCB1HomeFromCB0603);
            MacTransition tCabinet0604Releasing_MovingToCB1HomeFromCabinet0604 = NewTransition(sCabinet0604Releasing, sMovingToCB1HomeFromCabinet0604, EnumMacMsBoxTransferTransition.MoveToCB1HomeFromCB0604);
            MacTransition tCabinet0605Releasing_MovingToCB1HomeFromCabinet0605 = NewTransition(sCabinet0605Releasing, sMovingToCB1HomeFromCabinet0605, EnumMacMsBoxTransferTransition.MoveToCB1HomeFromCB0605);
            MacTransition tCabinet0701Releasing_MovingToCB1HomeFromCabinet0701 = NewTransition(sCabinet0701Releasing, sMovingToCB1HomeFromCabinet0701, EnumMacMsBoxTransferTransition.MoveToCB1HomeFromCB0701);
            MacTransition tCabinet0702Releasing_MovingToCB1HomeFromCabinet0702 = NewTransition(sCabinet0702Releasing, sMovingToCB1HomeFromCabinet0702, EnumMacMsBoxTransferTransition.MoveToCB1HomeFromCB0702);
            MacTransition tCabinet0703Releasing_MovingToCB1HomeFromCabinet0703 = NewTransition(sCabinet0703Releasing, sMovingToCB1HomeFromCabinet0703, EnumMacMsBoxTransferTransition.MoveToCB1HomeFromCB0703);
            MacTransition tCabinet0704Releasing_MovingToCB1HomeFromCabinet0704 = NewTransition(sCabinet0704Releasing, sMovingToCB1HomeFromCabinet0704, EnumMacMsBoxTransferTransition.MoveToCB1HomeFromCB0704);
            MacTransition tCabinet0705Releasing_MovingToCB1HomeFromCabinet0705 = NewTransition(sCabinet0705Releasing, sMovingToCB1HomeFromCabinet0705, EnumMacMsBoxTransferTransition.MoveToCB1HomeFromCB0705);
            */
            /** Obsolete
            MacTransition tMovingToCB1HomeFromCabinet0401_CB1Home = NewTransition(sMovingToCB1HomeFromCabinet0401, sCB1Home, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeFromCB0401);
            MacTransition tMovingToCB1HomeFromCabinet0402_CB1Home = NewTransition(sMovingToCB1HomeFromCabinet0402, sCB1Home, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeFromCB0402);
            MacTransition tMovingToCB1HomeFromCabinet0403_CB1Home = NewTransition(sMovingToCB1HomeFromCabinet0403, sCB1Home, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeFromCB0403);
            MacTransition tMovingToCB1HomeFromCabinet0404_CB1Home = NewTransition(sMovingToCB1HomeFromCabinet0404, sCB1Home, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeFromCB0404);
            MacTransition tMovingToCB1HomeFromCabinet0405_CB1Home = NewTransition(sMovingToCB1HomeFromCabinet0405, sCB1Home, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeFromCB0405);
            MacTransition tMovingToCB1HomeFromCabinet0501_CB1Home = NewTransition(sMovingToCB1HomeFromCabinet0501, sCB1Home, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeFromCB0501);
            MacTransition tMovingToCB1HomeFromCabinet0502_CB1Home = NewTransition(sMovingToCB1HomeFromCabinet0502, sCB1Home, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeFromCB0502);
            MacTransition tMovingToCB1HomeFromCabinet0503_CB1Home = NewTransition(sMovingToCB1HomeFromCabinet0503, sCB1Home, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeFromCB0503);
            MacTransition tMovingToCB1HomeFromCabinet0504_CB1Home = NewTransition(sMovingToCB1HomeFromCabinet0504, sCB1Home, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeFromCB0504);
            MacTransition tMovingToCB1HomeFromCabinet0505_CB1Home = NewTransition(sMovingToCB1HomeFromCabinet0505, sCB1Home, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeFromCB0505);
            MacTransition tMovingToCB1HomeFromCabinet0601_CB1Home = NewTransition(sMovingToCB1HomeFromCabinet0601, sCB1Home, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeFromCB0601);
            MacTransition tMovingToCB1HomeFromCabinet0602_CB1Home = NewTransition(sMovingToCB1HomeFromCabinet0602, sCB1Home, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeFromCB0602);
            MacTransition tMovingToCB1HomeFromCabinet0603_CB1Home = NewTransition(sMovingToCB1HomeFromCabinet0603, sCB1Home, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeFromCB0603);
            MacTransition tMovingToCB1HomeFromCabinet0604_CB1Home = NewTransition(sMovingToCB1HomeFromCabinet0604, sCB1Home, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeFromCB0604);
            MacTransition tMovingToCB1HomeFromCabinet0605_CB1Home = NewTransition(sMovingToCB1HomeFromCabinet0605, sCB1Home, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeFromCB0605);
            MacTransition tMovingToCB1HomeFromCabinet0701_CB1Home = NewTransition(sMovingToCB1HomeFromCabinet0701, sCB1Home, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeFromCB0701);
            MacTransition tMovingToCB1HomeFromCabinet0702_CB1Home = NewTransition(sMovingToCB1HomeFromCabinet0702, sCB1Home, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeFromCB0702);
            MacTransition tMovingToCB1HomeFromCabinet0703_CB1Home = NewTransition(sMovingToCB1HomeFromCabinet0703, sCB1Home, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeFromCB0703);
            MacTransition tMovingToCB1HomeFromCabinet0704_CB1Home = NewTransition(sMovingToCB1HomeFromCabinet0704, sCB1Home, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeFromCB0704);
            MacTransition tMovingToCB1HomeFromCabinet0705_CB1Home = NewTransition(sMovingToCB1HomeFromCabinet0705, sCB1Home, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeFromCB0705);
            */
            #endregion Release
            #endregion CB2
            #endregion Transition

            #region State Register OnEntry OnExit
            sStart.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                // CheckEquipmentStatus();            CheckAssemblyAlarmSignal();      CheckAssemblyWarningSignal();\
                OnEntryCheck();

                // from: sStart, to: sInitial
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
            {

            };
            sInitial.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                //CheckEquipmentStatus();CheckAssemblyAlarmSignal(); CheckAssemblyWarningSignal();
                OnEntryCheck();

                try
                {
                    HalBoxTransfer.Initial();
                }
                catch (Exception ex)
                {
                    throw new BoxTransferInitialFailException(ex.Message);
                }
                //from: sInitial, to: sCB1Home
                var transition = tDeviceInitial_CB1Home;
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
            {
            };

            sCB1Home.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                /**CheckEquipmentStatus(); CheckAssemblyAlarmSignal(); CheckAssemblyWarningSignal(); */
                OnEntryCheck();

                // from: sCB1Home(last state)
                var transition = tCB1Home_NULL;
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
            sCB1Home.OnExit += (sender, e) =>
            {
            };
            //sCB2Home.OnEntry += (sender, e) => { }; sCB2Home.OnExit += (sender, e) => { };

            //state_CB1HomwClamped.

            sCB1HomeClamped.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                //CheckEquipmentStatus();CheckAssemblyAlarmSignal();CheckAssemblyWarningSignal();
                OnEntryCheck();

                var transition = tCB1HomeClamped_NULL;
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
            sCB1HomeClamped.OnExit += (sender, e) => { };
            //sCB2HomeClamped.OnEntry += (sender, e) => { }; sCB2HomeClamped.OnExit += (sender, e) => { };

            #region Change Direction
            sChangingDirectionToCB1Home.OnEntry += (sender, e) => { }; sChangingDirectionToCB1Home.OnExit += (sender, e) => { };
            //sChangingDirectionToCB2Home.OnEntry += (sender, e) => { }; sChangingDirectionToCB2Home.OnExit += (sender, e) => { };
            sChangingDirectionToCB1HomeClamped.OnEntry += (sender, e) => { }; sChangingDirectionToCB1HomeClamped.OnExit += (sender, e) => { };
            //sChangingDirectionToCB2HomeClamped.OnEntry += (sender, e) => { }; sChangingDirectionToCB2HomeClamped.OnExit += (sender, e) => { };
            #endregion Change Direction

            #region OS
            sMovingToOpenStage.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                //CheckEquipmentStatus(); CheckAssemblyAlarmSignal(); CheckAssemblyWarningSignal();
                OnEntryCheck();

                try
                {
                    HalOpenStage.ReadRobotIntrude(true, null); // Fake OK
                    HalBoxTransfer.RobotMoving(true); // Fake OK
                   // HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_OpenStage_GET.json"); // Fake OK
                    HalBoxTransfer.ExePathMove(pathObj.FromCabinet01HomeToOpenStage_GET_PathFile()); // Fake OK
                    HalBoxTransfer.RobotMoving(false); // Fake Ok
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToOpenStage_OpenStageClamping;
                uint uintTempBoxType = 1; // TODO: 假定的 BoxType, 以後補上 
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
                    // NextStateEntryEventArgs = new MacStateEntryEventArgs(null),
                    NextStateEntryEventArgs = new MacStateOpenStageClampingEntryEventArgs(uintTempBoxType),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMovingToOpenStage.OnExit += (sender, e) =>
            {
            };
            sOpenStageClamping.OnEntry += (sender, e) =>
            {
                var eventArgs = (MacStateOpenStageClampingEntryEventArgs)e;
                SetCurrentState((MacState)sender);

                // CheckEquipmentStatus();            CheckAssemblyAlarmSignal();     CheckAssemblyWarningSignal();
                OnEntryCheck();

                try
                {
                    // var BoxType = (uint)e.Parameter;
                    var boxType = eventArgs.BoxType;
                    HalBoxTransfer.Clamp(boxType); // Fake OK
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tOpenStageClamping_MovingToCB1HomeClampedFromOpenStage;
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
            }; sOpenStageClamping.OnExit += (sender, e) => { };
            sMovingToCB1HomeClampedFromOpenStage.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                // CheckEquipmentStatus(); CheckAssemblyAlarmSignal(); CheckAssemblyWarningSignal();
                OnEntryCheck();
                try
                {
                    HalBoxTransfer.RobotMoving(true); // Fake OK
                                                      //  HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\OpenStage_Backward_Cabinet_01_Home_GET.json");  // Fake OK
                    HalBoxTransfer.ExePathMove(pathObj.FromOpenStageToCabinet01Home_GET_PathFile());
                    HalBoxTransfer.RobotMoving(false); // Fake OK
                    HalOpenStage.ReadRobotIntrude(false, null); // Fake OK
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeClampedFromOpenStage_CB1HomeClamped;
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
            }; sMovingToCB1HomeClampedFromOpenStage.OnExit += (sender, e) => { };

            sMovingToOpenStageForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                // CheckEquipmentStatus();    CheckAssemblyAlarmSignal();                CheckAssemblyWarningSignal();
                OnEntryCheck();
                try
                {
                    HalOpenStage.ReadRobotIntrude(true, null);  // Fake OK
                    HalBoxTransfer.RobotMoving(true);   // Fake OK
                                                        // HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_OpenStage_PUT.json");  // Fake OK
                    HalBoxTransfer.ExePathMove(pathObj.FromCabinet01HomeToOpenStage_PUT_PathFile());  // Fake OK
                    HalBoxTransfer.RobotMoving(false);   // Fake OK
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToOpenStageForRelease_OpenStageReleasing;
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
            }; sMovingToOpenStageForRelease.OnExit += (sender, e) => { };
            sOpenStageReleasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                //   CheckEquipmentStatus();                CheckAssemblyAlarmSignal();                CheckAssemblyWarningSignal();

                OnEntryCheck();
                try
                {
                    HalBoxTransfer.Unclamp();  // Fake OK
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tOpenStageReleasing_MovingToCB1HomeFromOpenStage;
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
            }; sOpenStageReleasing.OnExit += (sender, e) => { };
            sMovingToCB1HomeFromOpenStage.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                //CheckEquipmentStatus();    CheckAssemblyAlarmSignal();                CheckAssemblyWarningSignal();
                OnEntryCheck();

                try
                {
                    HalBoxTransfer.RobotMoving(true);   // Fake OK
                    //HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\OpenStage_Backward_Cabinet_01_Home_PUT.json");  // Fake OK
                    HalBoxTransfer.ExePathMove(pathObj.FromOpenStageToCabinet01Home_PUT_PathFile());
                    HalBoxTransfer.RobotMoving(false);  // Fake OK
                    HalOpenStage.ReadRobotIntrude(false, null);   // Fake OK
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeFromOpenStage_CB1Home;
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
            }; sMovingToCB1HomeFromOpenStage.OnExit += (sender, e) => { };
            #endregion OS

            #region Lock & Unlock
            sLocking.OnEntry += (sender, e) =>
            {
                var eventArgs=(MacStateMoveToLockEntryEventArgs)e;
                SetCurrentState((MacState)sender);

                // CheckEquipmentStatus(); CheckAssemblyAlarmSignal();  CheckAssemblyWarningSignal();
                OnEntryCheck();
                var boxType = eventArgs.BoxType;
                try
                {
                    // var BoxType = (uint)e.Parameter;
                    // if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))  // Fake OK
                    if (!HalBoxTransfer.CheckPosition(pathObj.Cabinet01HomePathFile()))  // Fake OK
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to lock box.");
                    HalOpenStage.ReadRobotIntrude(true, null);  // Fake OK
                    HalBoxTransfer.RobotMoving(true); // Fake OK
                    if (boxType ==BoxType.IronBox)
                    {  // 鐵盒 
                        // HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\LockIronBox.json"); // Fake OK
                        HalBoxTransfer.ExePathMove(pathObj.LockIronBoxPathFile()); // Fake OK
                    }
                    else if (boxType == BoxType.CrystalBox)
                    {   // 水晶盒
                        //HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\LockCrystalBox.json");  // Fake OK
                        HalBoxTransfer.ExePathMove(pathObj.LockCrystalBoxPathFile());  // Fake OK
                    }
                    else
                    {   // 非水晶盒與也非鐵盒
                        HalBoxTransfer.RobotMoving(false);   // Fake OK
                        HalOpenStage.ReadRobotIntrude(false, null); // Fake OK
                        throw new Exception("Unknown box type, can not move to lock box.");
                    }
                    HalOpenStage.ReadRobotIntrude(false, null);  // Fake OK
                    HalBoxTransfer.RobotMoving(false);  // Fake OK
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }
                //from: sLocking, to: sCB1Home
                var transition = tLocking_CB1Home;
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
            }; sLocking.OnExit += (sender, e) => { };
            sUnlocking.OnEntry += (sender, e) =>
            {

                var eventArgs = (MacStateMoveToUnLockEntryEventArgs)e;
                SetCurrentState((MacState)sender);

                //CheckEquipmentStatus();   CheckAssemblyAlarmSignal();          CheckAssemblyWarningSignal();
                OnEntryCheck();

                try
                {
                    var boxType = eventArgs.BoxType;
                    //if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))   // Fake OK
                    if (!HalBoxTransfer.CheckPosition(pathObj.Cabinet01HomePathFile()))   // Fake OK
                    {
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to unlock box.");
                    }
                    HalOpenStage.ReadRobotIntrude(true, null); // Fake OK
                    HalBoxTransfer.RobotMoving(true);  // Fake OK
                    if (boxType == BoxType.IronBox)
                    {
                        // HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\UnlockIronBox.json");  // Fake OK
                        HalBoxTransfer.ExePathMove(pathObj.UnlockIronBoxPathFile());  // Fake OK
                    }
                    else if (boxType == BoxType.CrystalBox)
                    {
                        //HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\UnlockCrystalBox.json"); // Fake OK
                        HalBoxTransfer.ExePathMove(pathObj.UnlockCrystalBoxPathFile());
                    }
                    else
                    {
                        HalBoxTransfer.RobotMoving(false); // Fake OK
                        HalOpenStage.ReadRobotIntrude(false, null);  // Fake OK
                        throw new Exception("Unknown box type, can not move to unlock box.");
                    }
                    HalOpenStage.ReadRobotIntrude(false, null);  // Fake OK
                    HalBoxTransfer.RobotMoving(false);  // Fake OK
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tUnlocking_CB1Home;
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
            sUnlocking.OnExit += (sender, e) =>
            {
            };
            #endregion Lock & Unlock

            #region CB1




            #region Move To Cabinet

            stateMovingToDrawer.OnEntry += (sender, e) =>
            {
                var eventArgs = (MacStateMovingToDrawerEntryEventArgs)e;
                var drawerLocation = eventArgs.DrawerLocation;

                SetCurrentState((MacState)sender, drawerLocation);
                //CheckEquipmentStatus();            CheckAssemblyAlarmSignal();      CheckAssemblyWarningSignal();
                OnEntryCheck();
                try
                {
                    /**
                      Cabinet01:
                          HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_01_01_GET.json");
                      Cabinet02:
                          HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                          HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_07_01_GET.json");
                     */

                    // Robot 目前不在Cabinet 1 Home
                    if (!HalBoxTransfer.CheckPosition(pathObj.Cabinet01HomePathFile())) // Fake OK
                    { throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to get box."); }

                    // 判斷 目標 Drawer 是屬於哪個 Cabinet(1 or 2)? 
                    var cabinetHome = drawerLocation.GetCabinetHomeCode();
                    if (cabinetHome.Item1)
                    {
                        HalBoxTransfer.RobotMoving(true);  // Fake OK
                        if (cabinetHome.Item2 == BoxrobotTransferLocation.Cabinet_01_Home)
                        {  // Cabinet 1
                            HalBoxTransfer.ExePathMove(pathObj.FromCabinet01HomeToDrawer_GET_PathFile(drawerLocation));  // Fake OK
                        }
                        else //if(cabinetHome.Item2 == BoxrobotTransferLocation.Cabinet_02_Home)
                        {  // Cabinet 2
                            HalBoxTransfer.ExePathMove(pathObj.Cabinet02HomePathFile());  // Fake OK
                            HalBoxTransfer.ExePathMove(pathObj.FromCabinet02HomeToDrawer_GET_PathFile(drawerLocation)); // Fake OK
                        }
                        HalBoxTransfer.RobotMoving(false);
                    }
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }
                //var  transition = tMovingToCabinet0101_Cabinet0101Clamping;
                var transition = transitionMovingToDrawer_DrawerClamping;
                uint uintTempBoxType = 1;  // TODO: 假定的 BoxType, 以後補上 
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
                    NextStateEntryEventArgs = new MacStateDrawerClampingEntryEventArgs(eventArgs.DrawerLocation, uintTempBoxType),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            stateMovingToDrawer.OnExit += (sender, e) =>
            { };

           /**
            sMovingToCabinet0101.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to get box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_01_01_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }
                var transition = tMovingToCabinet0101_Cabinet0101Clamping;
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
            sMovingToCabinet0101.OnExit += (sender, e) =>
            {
            };
            */
            /**
            sMovingToCabinet0102.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to get box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_01_02_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0102_Cabinet0102Clamping;
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
            sMovingToCabinet0102.OnExit += (sender, e) =>
            {
            };
            */
            /**
            sMovingToCabinet0103.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to get box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_01_03_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0103_Cabinet0103Clamping;
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
            sMovingToCabinet0103.OnExit += (sender, e) =>
            {
            };
            */
            /**
            sMovingToCabinet0104.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to get box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_01_04_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0104_Cabinet0104Clamping;
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
            sMovingToCabinet0104.OnExit += (sender, e) => { };
            */
            /**
            sMovingToCabinet0105.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to get box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_01_05_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0105_Cabinet0105Clamping;
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
            }; sMovingToCabinet0105.OnExit += (sender, e) => { };
            */
            /**
            sMovingToCabinet0201.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to get box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_02_01_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0201_Cabinet0201Clamping;
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
            }; sMovingToCabinet0201.OnExit += (sender, e) => { };
            */
            /**
            sMovingToCabinet0202.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to get box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_02_02_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0202_Cabinet0202Clamping;
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
            }; sMovingToCabinet0202.OnExit += (sender, e) => { };
            */
            /**
            sMovingToCabinet0203.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to get box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_02_03_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0203_Cabinet0203Clamping;
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
            }; sMovingToCabinet0203.OnExit += (sender, e) => { };
            */
            /**
            sMovingToCabinet0204.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to get box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_02_04_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0204_Cabinet0204Clamping;
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
            }; sMovingToCabinet0204.OnExit += (sender, e) => { };*/
            /**
            sMovingToCabinet0205.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to get box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_02_05_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0205_Cabinet0205Clamping;
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
            }; sMovingToCabinet0205.OnExit += (sender, e) => { };*/
            /**
            sMovingToCabinet0301.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to get box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_03_01_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0301_Cabinet0301Clamping;
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
            }; sMovingToCabinet0301.OnExit += (sender, e) => { };
            */
            /**
            sMovingToCabinet0302.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to get box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_03_02_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0302_Cabinet0302Clamping;
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
            }; sMovingToCabinet0302.OnExit += (sender, e) => { };
            */
            /**
            sMovingToCabinet0303.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to get box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_03_03_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0303_Cabinet0303Clamping;
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
            }; sMovingToCabinet0303.OnExit += (sender, e) => { };
            */
            /**
            sMovingToCabinet0304.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to get box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_03_04_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0304_Cabinet0304Clamping;
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
            }; sMovingToCabinet0304.OnExit += (sender, e) => { };
            */
            /**
            sMovingToCabinet0305.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to get box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_03_05_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0305_Cabinet0305Clamping;
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
            }; sMovingToCabinet0305.OnExit += (sender, e) => { };
            */
            #endregion Move To Cabinet

            #region Clamping At Cabinet
            stateDrawerClamping.OnEntry += (sender, e) =>
            {
                var eventArgs = (MacStateDrawerClampingEntryEventArgs)e;
                var drawerLocation = eventArgs.DrawerLocation;
                var boxType = eventArgs.BoxType;
                SetCurrentState((MacState)sender, drawerLocation);
                // CheckEquipmentStatus();                CheckAssemblyAlarmSignal();                CheckAssemblyWarningSignal();

                OnEntryCheck();
                try
                {
                    // Clamp
                    HalBoxTransfer.Clamp(boxType); // Fake OK
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }
                var transition = transitionDrawerClamping_MovingToCB1HomeClampedFromDrawer;
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
                    NextStateEntryEventArgs = new MacStateMovingToCB1HomeClampedFromDrawerEntryEventArgs(drawerLocation),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            stateDrawerClamping.OnExit += (sender, e) =>
            {

            };
            /**
            sCabinet0101Clamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    var BoxType = (uint)e.Parameter;
                    HalBoxTransfer.Clamp(BoxType);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0101Clamping_MovingToCB1HomeClampedFromCabinet0101;
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
            }; sCabinet0101Clamping.OnExit += (sender, e) => { };
            */
            /**
            sCabinet0102Clamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    var BoxType = (uint)e.Parameter;
                    HalBoxTransfer.Clamp(BoxType);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0102Clamping_MovingToCB1HomeClampedFromCabinet0102;
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
            }; sCabinet0102Clamping.OnExit += (sender, e) => { };
            */
            /**
            sCabinet0103Clamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    var BoxType = (uint)e.Parameter;
                    HalBoxTransfer.Clamp(BoxType);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0103Clamping_MovingToCB1HomeClampedFromCabinet0103;
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
            }; sCabinet0103Clamping.OnExit += (sender, e) => { };
            */
            /**
            sCabinet0104Clamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    var BoxType = (uint)e.Parameter;
                    HalBoxTransfer.Clamp(BoxType);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0104Clamping_MovingToCB1HomeClampedFromCabinet0104;
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
            }; sCabinet0104Clamping.OnExit += (sender, e) => { };
            */
            /**
            sCabinet0105Clamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    var BoxType = (uint)e.Parameter;
                    HalBoxTransfer.Clamp(BoxType);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0105Clamping_MovingToCB1HomeClampedFromCabinet0105;
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
            }; sCabinet0105Clamping.OnExit += (sender, e) => { };
            */
            /**
            sCabinet0201Clamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    var BoxType = (uint)e.Parameter;
                    HalBoxTransfer.Clamp(BoxType);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0201Clamping_MovingToCB1HomeClampedFromCabinet0201;
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
            }; sCabinet0201Clamping.OnExit += (sender, e) => { };
            */
            /**
            sCabinet0202Clamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    var BoxType = (uint)e.Parameter;
                    HalBoxTransfer.Clamp(BoxType);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0202Clamping_MovingToCB1HomeClampedFromCabinet0202;
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
            }; sCabinet0202Clamping.OnExit += (sender, e) => { };
            */
            /**
            sCabinet0203Clamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    var BoxType = (uint)e.Parameter;
                    HalBoxTransfer.Clamp(BoxType);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0203Clamping_MovingToCB1HomeClampedFromCabinet0203;
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
            }; sCabinet0203Clamping.OnExit += (sender, e) => { };*/
            /**
            sCabinet0204Clamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    var BoxType = (uint)e.Parameter;
                    HalBoxTransfer.Clamp(BoxType);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0204Clamping_MovingToCB1HomeClampedFromCabinet0204;
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
            }; sCabinet0204Clamping.OnExit += (sender, e) => { };
            */
            /**
            sCabinet0205Clamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    var BoxType = (uint)e.Parameter;
                    HalBoxTransfer.Clamp(BoxType);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0205Clamping_MovingToCB1HomeClampedFromCabinet0205;
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
            }; sCabinet0205Clamping.OnExit += (sender, e) => { };
            */
            /**
            sCabinet0301Clamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    var BoxType = (uint)e.Parameter;
                    HalBoxTransfer.Clamp(BoxType);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0301Clamping_MovingToCB1HomeClampedFromCabinet0301;
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
            }; sCabinet0301Clamping.OnExit += (sender, e) => { };
            */
            /**
            sCabinet0302Clamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    var BoxType = (uint)e.Parameter;
                    HalBoxTransfer.Clamp(BoxType);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0302Clamping_MovingToCB1HomeClampedFromCabinet0302;
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
            }; sCabinet0302Clamping.OnExit += (sender, e) => { };
            */
            /**
            sCabinet0303Clamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    var BoxType = (uint)e.Parameter;
                    HalBoxTransfer.Clamp(BoxType);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0303Clamping_MovingToCB1HomeClampedFromCabinet0303;
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
            }; sCabinet0303Clamping.OnExit += (sender, e) => { };
            */
            /**
            sCabinet0304Clamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    var BoxType = (uint)e.Parameter;
                    HalBoxTransfer.Clamp(BoxType);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0304Clamping_MovingToCB1HomeClampedFromCabinet0304;
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
            }; sCabinet0304Clamping.OnExit += (sender, e) => { };
            */
            /**
            sCabinet0305Clamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    var BoxType = (uint)e.Parameter;
                    HalBoxTransfer.Clamp(BoxType);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0305Clamping_MovingToCB1HomeClampedFromCabinet0305;
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
            }; sCabinet0305Clamping.OnExit += (sender, e) => { };
            */
            #endregion Clamping At Cabinet

            #region Return To CB Home Clamped From Cabinet
            stateMovingToCB1HomeClampedFromDrawer.OnEntry += (sender, e) =>
            {
                var eventArgs = (MacStateMovingToCB1HomeClampedFromDrawerEntryEventArgs)e;
                var drawerLocation = eventArgs.DrawerLocation;
                SetCurrentState((MacState)sender, drawerLocation);
                // CheckEquipmentStatus();                CheckAssemblyAlarmSignal();                CheckAssemblyWarningSignal();

                OnEntryCheck();
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
                            HalBoxTransfer.ExePathMove(pathObj.FromDrawerToCabinet01Home_GET_PathFile(drawerLocation));  // Fake OK
                        }
                        else //if (cabinetHome.Item2 == BoxrobotTransferLocation.Cabinet_02_Home) // 屬於Cabinet2 所管
                        {
                            HalBoxTransfer.ExePathMove(pathObj.FromDrawerToCabinet02Home_GET_PathFile(drawerLocation)); // Fake OK
                            HalBoxTransfer.ExePathMove(pathObj.Cabinet01HomePathFile()); // Fake OK
                        }
                        HalBoxTransfer.RobotMoving(false); // Fake OK
                    }

                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }
                var transition = transitionMovingToCB1HomeClampedFromDrawer_CB1HomeClamped;
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
            stateMovingToCB1HomeClampedFromDrawer.OnExit += (sender, e) =>
            {

            };
            /**
            sMovingToCB1HomeClampedFromCabinet0101.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_01_01_Backward_Cabinet_01_Home_GET.json");
                    HalBoxTransfer.RobotMoving(false);



                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeClampedFromCabinet0101_CB1HomeClamped;
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
            }; sMovingToCB1HomeClampedFromCabinet0101.OnExit += (sender, e) => { };
            */
            /**
            sMovingToCB1HomeClampedFromCabinet0102.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_01_02_Backward_Cabinet_01_Home_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeClampedFromCabinet0102_CB1HomeClamped;
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
            }; sMovingToCB1HomeClampedFromCabinet0102.OnExit += (sender, e) => { };
            */
            /**
            sMovingToCB1HomeClampedFromCabinet0103.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_01_03_Backward_Cabinet_01_Home_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeClampedFromCabinet0103_CB1HomeClamped;
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
            }; sMovingToCB1HomeClampedFromCabinet0103.OnExit += (sender, e) => { };
            */
            /**
            sMovingToCB1HomeClampedFromCabinet0104.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_01_04_Backward_Cabinet_01_Home_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeClampedFromCabinet0104_CB1HomeClamped;
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
            }; sMovingToCB1HomeClampedFromCabinet0104.OnExit += (sender, e) => { };
            */
            /**
            sMovingToCB1HomeClampedFromCabinet0105.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_01_05_Backward_Cabinet_01_Home_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeClampedFromCabinet0105_CB1HomeClamped;
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
            }; sMovingToCB1HomeClampedFromCabinet0105.OnExit += (sender, e) => { };
            */
            /**
            sMovingToCB1HomeClampedFromCabinet0201.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_02_01_Backward_Cabinet_01_Home_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeClampedFromCabinet0201_CB1HomeClamped;
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
            }; sMovingToCB1HomeClampedFromCabinet0201.OnExit += (sender, e) => { };
            */
            /**
            sMovingToCB1HomeClampedFromCabinet0202.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_02_02_Backward_Cabinet_01_Home_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeClampedFromCabinet0202_CB1HomeClamped;
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
            }; sMovingToCB1HomeClampedFromCabinet0202.OnExit += (sender, e) => { };
            */
            /**
            sMovingToCB1HomeClampedFromCabinet0203.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_02_03_Backward_Cabinet_01_Home_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeClampedFromCabinet0203_CB1HomeClamped;
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
            }; sMovingToCB1HomeClampedFromCabinet0203.OnExit += (sender, e) => { };*/
            /**
            sMovingToCB1HomeClampedFromCabinet0204.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_02_04_Backward_Cabinet_01_Home_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeClampedFromCabinet0204_CB1HomeClamped;
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
            }; sMovingToCB1HomeClampedFromCabinet0204.OnExit += (sender, e) => { };
            */
            /**
            sMovingToCB1HomeClampedFromCabinet0205.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_02_05_Backward_Cabinet_01_Home_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeClampedFromCabinet0205_CB1HomeClamped;
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
            }; sMovingToCB1HomeClampedFromCabinet0205.OnExit += (sender, e) => { };
            */
            /**
            sMovingToCB1HomeClampedFromCabinet0301.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_03_01_Backward_Cabinet_01_Home_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeClampedFromCabinet0301_CB1HomeClamped;
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
            }; sMovingToCB1HomeClampedFromCabinet0301.OnExit += (sender, e) => { };*/

            /**
            sMovingToCB1HomeClampedFromCabinet0302.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_03_02_Backward_Cabinet_01_Home_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeClampedFromCabinet0302_CB1HomeClamped;
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
            }; sMovingToCB1HomeClampedFromCabinet0302.OnExit += (sender, e) => { };
            */

            /**
            sMovingToCB1HomeClampedFromCabinet0303.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_03_03_Backward_Cabinet_01_Home_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeClampedFromCabinet0303_CB1HomeClamped;
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
            }; sMovingToCB1HomeClampedFromCabinet0303.OnExit += (sender, e) => { };*/

            /**
            sMovingToCB1HomeClampedFromCabinet0304.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_03_04_Backward_Cabinet_01_Home_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeClampedFromCabinet0304_CB1HomeClamped;
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
            }; sMovingToCB1HomeClampedFromCabinet0304.OnExit += (sender, e) => { };
            */
            /**
            sMovingToCB1HomeClampedFromCabinet0305.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_03_05_Backward_Cabinet_01_Home_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeClampedFromCabinet0305_CB1HomeClamped;
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
            }; sMovingToCB1HomeClampedFromCabinet0305.OnExit += (sender, e) => { };
            */
            #endregion Return To CB Home Clamped From Cabinet

            #region Move To Cabinet Fro Release
            stateMovingToDrawerForRelease.OnEntry += (sender, e) =>
            {
                var eventArgs = (MacStateMovingToDrawerForReleaseEntryEventArgs)e;
                var drawerLocation = eventArgs.DrawerLocation;
                SetCurrentState((MacState)sender);
                //  CheckEquipmentStatus(); CheckAssemblyAlarmSignal();  CheckAssemblyWarningSignal();
                OnEntryCheck();

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
                            // HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_01_01_PUT.json");
                            HalBoxTransfer.ExePathMove(pathObj.FromCabinet01HomeToDrawer_PUT_PathFile(drawerLocation)); // Fake OK
                        }
                        else if (cabinetHome.Item2 == BoxrobotTransferLocation.Cabinet_02_Home)
                        {
                            //HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                            //HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_07_01_PUT.json");
                            HalBoxTransfer.ExePathMove(pathObj.Cabinet02HomePathFile()); // Fake OK
                            HalBoxTransfer.ExePathMove(pathObj.FromCabinet02HomeToDrawer_PUT_PathFile(drawerLocation)); // Fake OK
                        }
                        HalBoxTransfer.RobotMoving(false); // Fake OK
                    }
                    else { }
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }
                // var transition = tMovingToCabinet0101ForRelease_Cabinet0101Releasing;
                var transition = transitionMovingToDrawerForRelease_DrawerReleasing;
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
                    NextStateEntryEventArgs = new MacStateDrawerReleasingEntryArgs(drawerLocation),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            stateMovingToDrawerForRelease.OnExit += (sender, e) =>
            {

            };

            /**
            sMovingToCabinet0101ForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to put box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_01_01_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0101ForRelease_Cabinet0101Releasing;
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
            }; sMovingToCabinet0101ForRelease.OnExit += (sender, e) => { };*/
            /**
            sMovingToCabinet0102ForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to put box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_01_02_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0102ForRelease_Cabinet0102Releasing;
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
            }; sMovingToCabinet0102ForRelease.OnExit += (sender, e) => { };*/
            /**
            sMovingToCabinet0103ForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to put box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_01_03_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0103ForRelease_Cabinet0103Releasing;
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
            }; sMovingToCabinet0103ForRelease.OnExit += (sender, e) => { };*/
            /**
            sMovingToCabinet0104ForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to put box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_01_04_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0104ForRelease_Cabinet0104Releasing;
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
            }; sMovingToCabinet0104ForRelease.OnExit += (sender, e) => { };*/
            /**
            sMovingToCabinet0105ForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to put box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_01_05_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0105ForRelease_Cabinet0105Releasing;
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
            }; sMovingToCabinet0105ForRelease.OnExit += (sender, e) => { };*/
            /**
            sMovingToCabinet0201ForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to put box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_02_01_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0201ForRelease_Cabinet0201Releasing;
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
            }; sMovingToCabinet0201ForRelease.OnExit += (sender, e) => { };*/
            /**
            sMovingToCabinet0202ForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to put box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_02_02_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0202ForRelease_Cabinet0202Releasing;
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
            }; sMovingToCabinet0202ForRelease.OnExit += (sender, e) => { };
            */
            /**
            sMovingToCabinet0203ForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to put box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_02_03_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0203ForRelease_Cabinet0203Releasing;
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
            }; sMovingToCabinet0203ForRelease.OnExit += (sender, e) => { };
            */
            /**
            sMovingToCabinet0204ForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to put box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_02_04_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0204ForRelease_Cabinet0204Releasing;
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
            }; sMovingToCabinet0204ForRelease.OnExit += (sender, e) => { };
            */
            /**
            sMovingToCabinet0205ForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to put box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_02_05_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0205ForRelease_Cabinet0205Releasing;
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
            }; sMovingToCabinet0205ForRelease.OnExit += (sender, e) => { };
            */
            /**
            sMovingToCabinet0301ForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to put box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_03_01_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0301ForRelease_Cabinet0301Releasing;
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
            }; sMovingToCabinet0301ForRelease.OnExit += (sender, e) => { };*/
            /**
            sMovingToCabinet0302ForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to put box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_03_02_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0302ForRelease_Cabinet0302Releasing;
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
            }; sMovingToCabinet0302ForRelease.OnExit += (sender, e) => { };*/
            /**
            sMovingToCabinet0303ForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to put box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_03_03_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0303ForRelease_Cabinet0303Releasing;
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
            }; sMovingToCabinet0303ForRelease.OnExit += (sender, e) => { };*/

            /**
            sMovingToCabinet0304ForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to put box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_03_04_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0304ForRelease_Cabinet0304Releasing;
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
            }; sMovingToCabinet0304ForRelease.OnExit += (sender, e) => { };*/
            /**
            sMovingToCabinet0305ForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to put box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home_Forward_Drawer_03_05_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0305ForRelease_Cabinet0305Releasing;
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
            }; sMovingToCabinet0305ForRelease.OnExit += (sender, e) => { };
            */
            #endregion Move To Cabinet Fro Release

            #region Releasing At Cabinet
            stateDrawerReleasing.OnEntry += (sender, e) =>
            {
                var eventArgs = (MacStateDrawerReleasingEntryArgs)e;
                var drawerLocation = eventArgs.DrawerLocation;

                SetCurrentState((MacState)sender);
                // CheckEquipmentStatus();         CheckAssemblyAlarmSignal();            CheckAssemblyWarningSignal();
                OnEntryCheck();

                try
                {
                    HalBoxTransfer.Unclamp(); // Fake OK
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }
                var transition = transitionDrawerReleasing_MovingToCB1HomeFromDrawer;
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
                    NextStateEntryEventArgs = new MacStateMovingToCB1HomeFromDrawerEntryEventArgs(drawerLocation),
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);

            };
            stateDrawerReleasing.OnExit += (sender, e) =>
            {

            };
            /**
            sCabinet0101Releasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0101Releasing_MovingToCB1HomeFromCabinet0101;
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
            }; sCabinet0101Releasing.OnExit += (sender, e) => { };
            */
            /**
            sCabinet0102Releasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0102Releasing_MovingToCB1HomeFromCabinet0102;
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
            }; sCabinet0102Releasing.OnExit += (sender, e) => { };*/
            /**
            sCabinet0103Releasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0103Releasing_MovingToCB1HomeFromCabinet0103;
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
            }; sCabinet0103Releasing.OnExit += (sender, e) => { };*/
            /**
            sCabinet0104Releasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0104Releasing_MovingToCB1HomeFromCabinet0104;
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
            }; sCabinet0104Releasing.OnExit += (sender, e) => { };*/
            /**
            sCabinet0105Releasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0105Releasing_MovingToCB1HomeFromCabinet0105;
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
            }; sCabinet0105Releasing.OnExit += (sender, e) => { };*/
            /**
            sCabinet0201Releasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0201Releasing_MovingToCB1HomeFromCabinet0201;
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
            }; sCabinet0201Releasing.OnExit += (sender, e) => { };*/
            /**
            sCabinet0202Releasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0202Releasing_MovingToCB1HomeFromCabinet0202;
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
            }; sCabinet0202Releasing.OnExit += (sender, e) => { };*/
            /**
            sCabinet0203Releasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0203Releasing_MovingToCB1HomeFromCabinet0203;
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
            }; sCabinet0203Releasing.OnExit += (sender, e) => { };
            */
            /**
            sCabinet0204Releasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0204Releasing_MovingToCB1HomeFromCabinet0204;
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
            }; sCabinet0204Releasing.OnExit += (sender, e) => { };*/
            /**
            sCabinet0205Releasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0205Releasing_MovingToCB1HomeFromCabinet0205;
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
            }; sCabinet0205Releasing.OnExit += (sender, e) => { };*/
            /**
            sCabinet0301Releasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0301Releasing_MovingToCB1HomeFromCabinet0301;
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
            }; sCabinet0301Releasing.OnExit += (sender, e) => { };*/
            /**
            sCabinet0302Releasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0302Releasing_MovingToCB1HomeFromCabinet0302;
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
            }; sCabinet0302Releasing.OnExit += (sender, e) => { };*/
            /**
            sCabinet0303Releasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0303Releasing_MovingToCB1HomeFromCabinet0303;
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
            }; sCabinet0303Releasing.OnExit += (sender, e) => { };*/
            /**
            sCabinet0304Releasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0304Releasing_MovingToCB1HomeFromCabinet0304;
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
            }; sCabinet0304Releasing.OnExit += (sender, e) => { };*/
            /**
            sCabinet0305Releasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0305Releasing_MovingToCB1HomeFromCabinet0305;
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
            }; sCabinet0305Releasing.OnExit += (sender, e) => { };*/
            #endregion Releasing At Cabinet

            #region Return To CB Home From Cabinet
            stateMovingToCB1HomeFromDrawer.OnEntry += (sender, e) =>
            {
                var eventArgs = (MacStateMovingToCB1HomeFromDrawerEntryEventArgs)e;
                var drawerLocation = eventArgs.DrawerLocation;
                SetCurrentState((MacState)sender);
                // CheckEquipmentStatus();                CheckAssemblyAlarmSignal();                CheckAssemblyWarningSignal();
                OnEntryCheck();

                try
                {
                    /**
                     Cabinet1:
                        HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_01_01_Backward_Cabinet_01_Home_PUT.json");
                    Cabinet2: 
                       HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_07_01_Backward_Cabinet_02_Home_PUT.json");
                       HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                     */


                    //  HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_01_01_Backward_Cabinet_01_Home_PUT.json");
                    var cabinetHome = drawerLocation.GetCabinetHomeCode();
                    if (cabinetHome.Item1)
                    {
                        HalBoxTransfer.RobotMoving(true); // Fake OK
                        if (cabinetHome.Item2 == BoxrobotTransferLocation.Cabinet_01_Home)
                        {

                            HalBoxTransfer.ExePathMove(pathObj.FromDrawerToCabinet01Home_PUT_PathFile(drawerLocation)); // Fake OK
                        }
                        else //if(cabinetHome.Item2 == BoxrobotTransferLocation.Cabinet_02_Home)
                        {

                            HalBoxTransfer.ExePathMove(pathObj.FromDrawerToCabinet02Home_PUT_PathFile(drawerLocation)); // Fake OK
                            HalBoxTransfer.ExePathMove(pathObj.Cabinet01HomePathFile()); // Fake OK
                        }
                        HalBoxTransfer.RobotMoving(false); // Fake OK
                    }

                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }
                //var transition = tMovingToCB1HomeFromCabinet0101_CB1Home;
                var transition = transitionMovingToCB1HomeFromDrawer_CB1Home;
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
            stateMovingToCB1HomeFromDrawer.OnExit += (sender, e) => { };

            /**
            sMovingToCB1HomeFromCabinet0101.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_01_01_Backward_Cabinet_01_Home_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeFromCabinet0101_CB1Home;
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
            }; sMovingToCB1HomeFromCabinet0101.OnExit += (sender, e) => { };
           */
           /**
            sMovingToCB1HomeFromCabinet0102.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_01_02_Backward_Cabinet_01_Home_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeFromCabinet0102_CB1Home;
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
            }; sMovingToCB1HomeFromCabinet0102.OnExit += (sender, e) => { };*/

            /**
            sMovingToCB1HomeFromCabinet0103.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_01_03_Backward_Cabinet_01_Home_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeFromCabinet0103_CB1Home;
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
            }; sMovingToCB1HomeFromCabinet0103.OnExit += (sender, e) => { };
    */

            /**
            sMovingToCB1HomeFromCabinet0104.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_01_04_Backward_Cabinet_01_Home_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeFromCabinet0104_CB1Home;
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
            }; sMovingToCB1HomeFromCabinet0104.OnExit += (sender, e) => { };*/
            /**
            sMovingToCB1HomeFromCabinet0105.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_01_05_Backward_Cabinet_01_Home_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeFromCabinet0105_CB1Home;
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
            }; sMovingToCB1HomeFromCabinet0105.OnExit += (sender, e) => { };*/
            /**
            sMovingToCB1HomeFromCabinet0201.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_02_01_Backward_Cabinet_01_Home_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeFromCabinet0201_CB1Home;
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
            }; sMovingToCB1HomeFromCabinet0201.OnExit += (sender, e) => { };*/
            /**
            sMovingToCB1HomeFromCabinet0202.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_02_02_Backward_Cabinet_01_Home_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeFromCabinet0202_CB1Home;
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
            }; sMovingToCB1HomeFromCabinet0202.OnExit += (sender, e) => { };*/
            /**
            sMovingToCB1HomeFromCabinet0203.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_02_03_Backward_Cabinet_01_Home_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeFromCabinet0203_CB1Home;
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
            }; sMovingToCB1HomeFromCabinet0203.OnExit += (sender, e) => { };*/
            /**
            sMovingToCB1HomeFromCabinet0204.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_02_04_Backward_Cabinet_01_Home_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeFromCabinet0204_CB1Home;
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
            }; sMovingToCB1HomeFromCabinet0204.OnExit += (sender, e) => { }; */
            /**
            sMovingToCB1HomeFromCabinet0205.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_02_05_Backward_Cabinet_01_Home_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeFromCabinet0205_CB1Home;
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
            }; sMovingToCB1HomeFromCabinet0205.OnExit += (sender, e) => { };*/
            /**
            sMovingToCB1HomeFromCabinet0301.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_03_01_Backward_Cabinet_01_Home_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeFromCabinet0301_CB1Home;
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
            }; sMovingToCB1HomeFromCabinet0301.OnExit += (sender, e) => { };*/
            /**
            sMovingToCB1HomeFromCabinet0302.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_03_02_Backward_Cabinet_01_Home_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeFromCabinet0302_CB1Home;
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
            }; sMovingToCB1HomeFromCabinet0302.OnExit += (sender, e) => { };*/
            /**
            sMovingToCB1HomeFromCabinet0303.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_03_03_Backward_Cabinet_01_Home_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeFromCabinet0303_CB1Home;
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
            }; sMovingToCB1HomeFromCabinet0303.OnExit += (sender, e) => { };*/

            /**
            sMovingToCB1HomeFromCabinet0304.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_03_04_Backward_Cabinet_01_Home_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeFromCabinet0304_CB1Home;
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
            }; sMovingToCB1HomeFromCabinet0304.OnExit += (sender, e) => { };*/
            /**
            sMovingToCB1HomeFromCabinet0305.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_03_05_Backward_Cabinet_01_Home_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeFromCabinet0305_CB1Home;
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
            }; sMovingToCB1HomeFromCabinet0305.OnExit += (sender, e) => { };*/
            #endregion Return To CB Home From Cabinet
            #endregion CB1

            /**
            #region CB2
            #region Move To Cabinet
            sMovingToCabinet0401.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to get box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_04_01_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0401_Cabinet0401Clamping;
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
            }; sMovingToCabinet0401.OnExit += (sender, e) => { };
            sMovingToCabinet0402.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to get box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_04_02_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0402_Cabinet0402Clamping;
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
            }; sMovingToCabinet0402.OnExit += (sender, e) => { };
            sMovingToCabinet0403.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to get box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_04_03_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0403_Cabinet0403Clamping;
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
            }; sMovingToCabinet0403.OnExit += (sender, e) => { };
            sMovingToCabinet0404.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to get box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_04_04_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0404_Cabinet0404Clamping;
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
            }; sMovingToCabinet0404.OnExit += (sender, e) => { };
            sMovingToCabinet0405.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to get box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_04_05_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0405_Cabinet0405Clamping;
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
            }; sMovingToCabinet0405.OnExit += (sender, e) => { };
            sMovingToCabinet0501.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to get box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_05_01_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0501_Cabinet0501Clamping;
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
            }; sMovingToCabinet0501.OnExit += (sender, e) => { };
            sMovingToCabinet0502.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to get box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_05_02_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0502_Cabinet0502Clamping;
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
            }; sMovingToCabinet0502.OnExit += (sender, e) => { };
            sMovingToCabinet0503.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to get box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_05_03_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0503_Cabinet0503Clamping;
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
            }; sMovingToCabinet0503.OnExit += (sender, e) => { };
            sMovingToCabinet0504.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to get box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_05_04_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0504_Cabinet0504Clamping;
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
            }; sMovingToCabinet0504.OnExit += (sender, e) => { };
            sMovingToCabinet0505.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to get box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_05_05_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0505_Cabinet0505Clamping;
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
            }; sMovingToCabinet0505.OnExit += (sender, e) => { };
            sMovingToCabinet0601.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to get box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_06_01_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0601_Cabinet0601Clamping;
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
            }; sMovingToCabinet0601.OnExit += (sender, e) => { };
            sMovingToCabinet0602.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to get box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_06_02_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0602_Cabinet0602Clamping;
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
            }; sMovingToCabinet0602.OnExit += (sender, e) => { };
            sMovingToCabinet0603.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to get box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_06_03_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0603_Cabinet0603Clamping;
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
            }; sMovingToCabinet0603.OnExit += (sender, e) => { };
            sMovingToCabinet0604.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to get box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_06_04_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0604_Cabinet0604Clamping;
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
            }; sMovingToCabinet0604.OnExit += (sender, e) => { };
            sMovingToCabinet0605.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to get box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_06_05_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0605_Cabinet0605Clamping;
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
            }; sMovingToCabinet0605.OnExit += (sender, e) => { };
            sMovingToCabinet0701.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to get box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_07_01_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0701_Cabinet0701Clamping;
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
            }; sMovingToCabinet0701.OnExit += (sender, e) => { };
            sMovingToCabinet0702.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to get box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_07_02_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0702_Cabinet0702Clamping;
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
            }; sMovingToCabinet0702.OnExit += (sender, e) => { };
            sMovingToCabinet0703.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to get box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_07_03_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0703_Cabinet0703Clamping;
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
            }; sMovingToCabinet0703.OnExit += (sender, e) => { };
            sMovingToCabinet0704.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to get box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_07_04_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0704_Cabinet0704Clamping;
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
            }; sMovingToCabinet0704.OnExit += (sender, e) => { };
            sMovingToCabinet0705.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to get box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_07_05_GET.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0705_Cabinet0705Clamping;
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
            }; sMovingToCabinet0705.OnExit += (sender, e) => { };
            #endregion Move To Cabinet

            #region Clamping At Cabinet
            sCabinet0401Clamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    var BoxType = (uint)e.Parameter;
                    HalBoxTransfer.Clamp(BoxType);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0401Clamping_MovingToCB1HomeClampedFromCabinet0401;
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
            }; sCabinet0401Clamping.OnExit += (sender, e) => { };
            sCabinet0402Clamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    var BoxType = (uint)e.Parameter;
                    HalBoxTransfer.Clamp(BoxType);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0402Clamping_MovingToCB1HomeClampedFromCabinet0402;
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
            }; sCabinet0402Clamping.OnExit += (sender, e) => { };
            sCabinet0403Clamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    var BoxType = (uint)e.Parameter;
                    HalBoxTransfer.Clamp(BoxType);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0403Clamping_MovingToCB1HomeClampedFromCabinet0403;
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
            }; sCabinet0403Clamping.OnExit += (sender, e) => { };
            sCabinet0404Clamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    var BoxType = (uint)e.Parameter;
                    HalBoxTransfer.Clamp(BoxType);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0404Clamping_MovingToCB1HomeClampedFromCabinet0404;
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
            }; sCabinet0404Clamping.OnExit += (sender, e) => { };
            sCabinet0405Clamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    var BoxType = (uint)e.Parameter;
                    HalBoxTransfer.Clamp(BoxType);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0405Clamping_MovingToCB1HomeClampedFromCabinet0405;
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
            }; sCabinet0405Clamping.OnExit += (sender, e) => { };
            sCabinet0501Clamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    var BoxType = (uint)e.Parameter;
                    HalBoxTransfer.Clamp(BoxType);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0501Clamping_MovingToCB1HomeClampedFromCabinet0501;
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
            }; sCabinet0501Clamping.OnExit += (sender, e) => { };
            sCabinet0502Clamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    var BoxType = (uint)e.Parameter;
                    HalBoxTransfer.Clamp(BoxType);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0502Clamping_MovingToCB1HomeClampedFromCabinet0502;
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
            }; sCabinet0502Clamping.OnExit += (sender, e) => { };
            sCabinet0503Clamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    var BoxType = (uint)e.Parameter;
                    HalBoxTransfer.Clamp(BoxType);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0503Clamping_MovingToCB1HomeClampedFromCabinet0503;
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
            }; sCabinet0503Clamping.OnExit += (sender, e) => { };
            sCabinet0504Clamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    var BoxType = (uint)e.Parameter;
                    HalBoxTransfer.Clamp(BoxType);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0504Clamping_MovingToCB1HomeClampedFromCabinet0504;
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
            }; sCabinet0504Clamping.OnExit += (sender, e) => { };
            sCabinet0505Clamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    var BoxType = (uint)e.Parameter;
                    HalBoxTransfer.Clamp(BoxType);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0505Clamping_MovingToCB1HomeClampedFromCabinet0505;
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
            }; sCabinet0505Clamping.OnExit += (sender, e) => { };
            sCabinet0601Clamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    var BoxType = (uint)e.Parameter;
                    HalBoxTransfer.Clamp(BoxType);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0601Clamping_MovingToCB1HomeClampedFromCabinet0601;
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
            }; sCabinet0601Clamping.OnExit += (sender, e) => { };
            sCabinet0602Clamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    var BoxType = (uint)e.Parameter;
                    HalBoxTransfer.Clamp(BoxType);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0602Clamping_MovingToCB1HomeClampedFromCabinet0602;
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
            }; sCabinet0602Clamping.OnExit += (sender, e) => { };
            sCabinet0603Clamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    var BoxType = (uint)e.Parameter;
                    HalBoxTransfer.Clamp(BoxType);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0603Clamping_MovingToCB1HomeClampedFromCabinet0603;
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
            }; sCabinet0603Clamping.OnExit += (sender, e) => { };
            sCabinet0604Clamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    var BoxType = (uint)e.Parameter;
                    HalBoxTransfer.Clamp(BoxType);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0604Clamping_MovingToCB1HomeClampedFromCabinet0604;
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
            }; sCabinet0604Clamping.OnExit += (sender, e) => { };
            sCabinet0605Clamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    var BoxType = (uint)e.Parameter;
                    HalBoxTransfer.Clamp(BoxType);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0605Clamping_MovingToCB1HomeClampedFromCabinet0605;
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
            }; sCabinet0605Clamping.OnExit += (sender, e) => { };
            sCabinet0701Clamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    var BoxType = (uint)e.Parameter;
                    HalBoxTransfer.Clamp(BoxType);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0701Clamping_MovingToCB1HomeClampedFromCabinet0701;
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
            }; sCabinet0701Clamping.OnExit += (sender, e) => { };
            sCabinet0702Clamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    var BoxType = (uint)e.Parameter;
                    HalBoxTransfer.Clamp(BoxType);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0702Clamping_MovingToCB1HomeClampedFromCabinet0702;
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
            }; sCabinet0702Clamping.OnExit += (sender, e) => { };
            sCabinet0703Clamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    var BoxType = (uint)e.Parameter;
                    HalBoxTransfer.Clamp(BoxType);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0703Clamping_MovingToCB1HomeClampedFromCabinet0703;
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
            }; sCabinet0703Clamping.OnExit += (sender, e) => { };
            sCabinet0704Clamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    var BoxType = (uint)e.Parameter;
                    HalBoxTransfer.Clamp(BoxType);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0704Clamping_MovingToCB1HomeClampedFromCabinet0704;
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
            }; sCabinet0704Clamping.OnExit += (sender, e) => { };
            sCabinet0705Clamping.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    var BoxType = (uint)e.Parameter;
                    HalBoxTransfer.Clamp(BoxType);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0705Clamping_MovingToCB1HomeClampedFromCabinet0705;
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
            }; sCabinet0705Clamping.OnExit += (sender, e) => { };
            #endregion Clamping At Cabinet

            #region Return To CB Home Clamped From Cabinet
            sMovingToCB1HomeClampedFromCabinet0401.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_04_01_Backward_Cabinet_02_Home_GET.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeClampedFromCabinet0401_CB1HomeClamped;
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
            }; sMovingToCB1HomeClampedFromCabinet0401.OnExit += (sender, e) => { };
            sMovingToCB1HomeClampedFromCabinet0402.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_04_02_Backward_Cabinet_02_Home_GET.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeClampedFromCabinet0402_CB1HomeClamped;
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
            }; sMovingToCB1HomeClampedFromCabinet0402.OnExit += (sender, e) => { };
            sMovingToCB1HomeClampedFromCabinet0403.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_04_03_Backward_Cabinet_02_Home_GET.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeClampedFromCabinet0403_CB1HomeClamped;
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
            }; sMovingToCB1HomeClampedFromCabinet0403.OnExit += (sender, e) => { };
            sMovingToCB1HomeClampedFromCabinet0404.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_04_04_Backward_Cabinet_02_Home_GET.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeClampedFromCabinet0404_CB1HomeClamped;
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
            }; sMovingToCB1HomeClampedFromCabinet0404.OnExit += (sender, e) => { };
            sMovingToCB1HomeClampedFromCabinet0405.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_04_05_Backward_Cabinet_02_Home_GET.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeClampedFromCabinet0405_CB1HomeClamped;
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
            }; sMovingToCB1HomeClampedFromCabinet0405.OnExit += (sender, e) => { };
            sMovingToCB1HomeClampedFromCabinet0501.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_05_01_Backward_Cabinet_02_Home_GET.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeClampedFromCabinet0501_CB1HomeClamped;
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
            }; sMovingToCB1HomeClampedFromCabinet0501.OnExit += (sender, e) => { };
            sMovingToCB1HomeClampedFromCabinet0502.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_05_02_Backward_Cabinet_02_Home_GET.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeClampedFromCabinet0502_CB1HomeClamped;
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
            }; sMovingToCB1HomeClampedFromCabinet0502.OnExit += (sender, e) => { };
            sMovingToCB1HomeClampedFromCabinet0503.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_05_03_Backward_Cabinet_02_Home_GET.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeClampedFromCabinet0503_CB1HomeClamped;
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
            }; sMovingToCB1HomeClampedFromCabinet0503.OnExit += (sender, e) => { };
            sMovingToCB1HomeClampedFromCabinet0504.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_05_04_Backward_Cabinet_02_Home_GET.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeClampedFromCabinet0504_CB1HomeClamped;
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
            }; sMovingToCB1HomeClampedFromCabinet0504.OnExit += (sender, e) => { };
            sMovingToCB1HomeClampedFromCabinet0505.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_05_05_Backward_Cabinet_02_Home_GET.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeClampedFromCabinet0505_CB1HomeClamped;
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
            }; sMovingToCB1HomeClampedFromCabinet0505.OnExit += (sender, e) => { };
            sMovingToCB1HomeClampedFromCabinet0601.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_06_01_Backward_Cabinet_02_Home_GET.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeClampedFromCabinet0601_CB1HomeClamped;
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
            }; sMovingToCB1HomeClampedFromCabinet0601.OnExit += (sender, e) => { };
            sMovingToCB1HomeClampedFromCabinet0602.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_06_02_Backward_Cabinet_02_Home_GET.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeClampedFromCabinet0602_CB1HomeClamped;
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
            }; sMovingToCB1HomeClampedFromCabinet0602.OnExit += (sender, e) => { };
            sMovingToCB1HomeClampedFromCabinet0603.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_06_03_Backward_Cabinet_02_Home_GET.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeClampedFromCabinet0603_CB1HomeClamped;
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
            }; sMovingToCB1HomeClampedFromCabinet0603.OnExit += (sender, e) => { };
            sMovingToCB1HomeClampedFromCabinet0604.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_06_04_Backward_Cabinet_02_Home_GET.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeClampedFromCabinet0604_CB1HomeClamped;
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
            }; sMovingToCB1HomeClampedFromCabinet0604.OnExit += (sender, e) => { };
            sMovingToCB1HomeClampedFromCabinet0605.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_06_05_Backward_Cabinet_02_Home_GET.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeClampedFromCabinet0605_CB1HomeClamped;
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
            }; sMovingToCB1HomeClampedFromCabinet0605.OnExit += (sender, e) => { };
            sMovingToCB1HomeClampedFromCabinet0701.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_07_01_Backward_Cabinet_02_Home_GET.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeClampedFromCabinet0701_CB1HomeClamped;
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
            }; sMovingToCB1HomeClampedFromCabinet0701.OnExit += (sender, e) => { };
            sMovingToCB1HomeClampedFromCabinet0702.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_07_02_Backward_Cabinet_02_Home_GET.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeClampedFromCabinet0702_CB1HomeClamped;
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
            }; sMovingToCB1HomeClampedFromCabinet0702.OnExit += (sender, e) => { };
            sMovingToCB1HomeClampedFromCabinet0703.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_07_03_Backward_Cabinet_02_Home_GET.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeClampedFromCabinet0703_CB1HomeClamped;
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
            }; sMovingToCB1HomeClampedFromCabinet0703.OnExit += (sender, e) => { };
            sMovingToCB1HomeClampedFromCabinet0704.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_07_04_Backward_Cabinet_02_Home_GET.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeClampedFromCabinet0704_CB1HomeClamped;
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
            }; sMovingToCB1HomeClampedFromCabinet0704.OnExit += (sender, e) => { };
            sMovingToCB1HomeClampedFromCabinet0705.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_07_05_Backward_Cabinet_02_Home_GET.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeClampedFromCabinet0705_CB1HomeClamped;
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
            }; sMovingToCB1HomeClampedFromCabinet0705.OnExit += (sender, e) => { };
            #endregion Return To CB Home Clamped From Cabinet

            #region Move To Cabinet Fro Release
            sMovingToCabinet0401ForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to put box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_04_01_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0401ForRelease_Cabinet0401Releasing;
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
            }; sMovingToCabinet0401ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0402ForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to put box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_04_02_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0402ForRelease_Cabinet0402Releasing;
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
            }; sMovingToCabinet0402ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0403ForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to put box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_04_03_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0403ForRelease_Cabinet0403Releasing;
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
            }; sMovingToCabinet0403ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0404ForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to put box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_04_04_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0404ForRelease_Cabinet0404Releasing;
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
            }; sMovingToCabinet0404ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0405ForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to put box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_04_05_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0405ForRelease_Cabinet0405Releasing;
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
            }; sMovingToCabinet0405ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0501ForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to put box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_05_01_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0501ForRelease_Cabinet0501Releasing;
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
            }; sMovingToCabinet0501ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0502ForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to put box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_05_02_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0502ForRelease_Cabinet0502Releasing;
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
            }; sMovingToCabinet0502ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0503ForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to put box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_05_03_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0503ForRelease_Cabinet0503Releasing;
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
            }; sMovingToCabinet0503ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0504ForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to put box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_05_04_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0504ForRelease_Cabinet0504Releasing;
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
            }; sMovingToCabinet0504ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0505ForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to put box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_05_05_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0505ForRelease_Cabinet0505Releasing;
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
            }; sMovingToCabinet0505ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0601ForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to put box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_06_01_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0601ForRelease_Cabinet0601Releasing;
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
            }; sMovingToCabinet0601ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0602ForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to put box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_06_02_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0602ForRelease_Cabinet0602Releasing;
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
            }; sMovingToCabinet0602ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0603ForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to put box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_06_03_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0603ForRelease_Cabinet0603Releasing;
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
            }; sMovingToCabinet0603ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0604ForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to put box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_06_04_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0604ForRelease_Cabinet0604Releasing;
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
            }; sMovingToCabinet0604ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0605ForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to put box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_06_05_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0605ForRelease_Cabinet0605Releasing;
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
            }; sMovingToCabinet0605ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0701ForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to put box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_07_01_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0701ForRelease_Cabinet0701Releasing;
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
            }; sMovingToCabinet0701ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0702ForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to put box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_07_02_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0702ForRelease_Cabinet0702Releasing;
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
            }; sMovingToCabinet0702ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0703ForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to put box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_07_03_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0703ForRelease_Cabinet0703Releasing;
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
            }; sMovingToCabinet0703ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0704ForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to put box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_07_04_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0704ForRelease_Cabinet0704Releasing;
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
            }; sMovingToCabinet0704ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0705ForRelease.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    if (!HalBoxTransfer.CheckPosition(@"D:\Positions\BTRobot\Cabinet_01_Home.json"))
                        throw new Exception("Robot is not at position of Cabinet_01_Home, can not move to cabinet to put box.");
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_02_Home_Forward_Drawer_07_05_PUT.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCabinet0705ForRelease_Cabinet0705Releasing;
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
            }; sMovingToCabinet0705ForRelease.OnExit += (sender, e) => { };
            #endregion Move To Cabinet Fro Release

            #region Releasing At Cabinet
            sCabinet0401Releasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0401Releasing_MovingToCB1HomeFromCabinet0401;
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
            }; sCabinet0401Releasing.OnExit += (sender, e) => { };
            sCabinet0402Releasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0402Releasing_MovingToCB1HomeFromCabinet0402;
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
            }; sCabinet0402Releasing.OnExit += (sender, e) => { };
            sCabinet0403Releasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0403Releasing_MovingToCB1HomeFromCabinet0403;
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
            }; sCabinet0403Releasing.OnExit += (sender, e) => { };
            sCabinet0404Releasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0404Releasing_MovingToCB1HomeFromCabinet0404;
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
            }; sCabinet0404Releasing.OnExit += (sender, e) => { };
            sCabinet0405Releasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0405Releasing_MovingToCB1HomeFromCabinet0405;
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
            }; sCabinet0405Releasing.OnExit += (sender, e) => { };
            sCabinet0501Releasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0501Releasing_MovingToCB1HomeFromCabinet0501;
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
            }; sCabinet0501Releasing.OnExit += (sender, e) => { };
            sCabinet0502Releasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0502Releasing_MovingToCB1HomeFromCabinet0502;
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
            }; sCabinet0502Releasing.OnExit += (sender, e) => { };
            sCabinet0503Releasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0503Releasing_MovingToCB1HomeFromCabinet0503;
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
            }; sCabinet0503Releasing.OnExit += (sender, e) => { };
            sCabinet0504Releasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0504Releasing_MovingToCB1HomeFromCabinet0504;
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
            }; sCabinet0504Releasing.OnExit += (sender, e) => { };
            sCabinet0505Releasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0505Releasing_MovingToCB1HomeFromCabinet0505;
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
            }; sCabinet0505Releasing.OnExit += (sender, e) => { };
            sCabinet0601Releasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0601Releasing_MovingToCB1HomeFromCabinet0601;
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
            }; sCabinet0601Releasing.OnExit += (sender, e) => { };
            sCabinet0602Releasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0602Releasing_MovingToCB1HomeFromCabinet0602;
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
            }; sCabinet0602Releasing.OnExit += (sender, e) => { };
            sCabinet0603Releasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0603Releasing_MovingToCB1HomeFromCabinet0603;
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
            }; sCabinet0603Releasing.OnExit += (sender, e) => { };
            sCabinet0604Releasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0604Releasing_MovingToCB1HomeFromCabinet0604;
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
            }; sCabinet0604Releasing.OnExit += (sender, e) => { };
            sCabinet0605Releasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0605Releasing_MovingToCB1HomeFromCabinet0605;
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
            }; sCabinet0605Releasing.OnExit += (sender, e) => { };
            sCabinet0701Releasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0701Releasing_MovingToCB1HomeFromCabinet0701;
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
            }; sCabinet0701Releasing.OnExit += (sender, e) => { };
            sCabinet0702Releasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0702Releasing_MovingToCB1HomeFromCabinet0702;
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
            }; sCabinet0702Releasing.OnExit += (sender, e) => { };
            sCabinet0703Releasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0703Releasing_MovingToCB1HomeFromCabinet0703;
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
            }; sCabinet0703Releasing.OnExit += (sender, e) => { };
            sCabinet0704Releasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0704Releasing_MovingToCB1HomeFromCabinet0704;
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
            }; sCabinet0704Releasing.OnExit += (sender, e) => { };
            sCabinet0705Releasing.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.Unclamp();
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPLCExecuteFailException(ex.Message);
                }

                var transition = tCabinet0705Releasing_MovingToCB1HomeFromCabinet0705;
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
            }; sCabinet0705Releasing.OnExit += (sender, e) => { };
            #endregion Releasing At Cabinet

            #region Return To CB Home From Cabinet
            sMovingToCB1HomeFromCabinet0401.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_04_01_Backward_Cabinet_02_Home_PUT.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeFromCabinet0401_CB1Home;
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
            }; sMovingToCB1HomeFromCabinet0401.OnExit += (sender, e) => { };
            sMovingToCB1HomeFromCabinet0402.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_04_02_Backward_Cabinet_02_Home_PUT.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeFromCabinet0402_CB1Home;
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
            }; sMovingToCB1HomeFromCabinet0402.OnExit += (sender, e) => { };
            sMovingToCB1HomeFromCabinet0403.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_04_03_Backward_Cabinet_02_Home_PUT.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeFromCabinet0403_CB1Home;
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
            }; sMovingToCB1HomeFromCabinet0403.OnExit += (sender, e) => { };
            sMovingToCB1HomeFromCabinet0404.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_04_04_Backward_Cabinet_02_Home_PUT.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeFromCabinet0404_CB1Home;
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
            }; sMovingToCB1HomeFromCabinet0404.OnExit += (sender, e) => { };
            sMovingToCB1HomeFromCabinet0405.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_04_05_Backward_Cabinet_02_Home_PUT.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeFromCabinet0405_CB1Home;
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
            }; sMovingToCB1HomeFromCabinet0405.OnExit += (sender, e) => { };
            sMovingToCB1HomeFromCabinet0501.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_05_01_Backward_Cabinet_02_Home_PUT.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeFromCabinet0501_CB1Home;
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
            }; sMovingToCB1HomeFromCabinet0501.OnExit += (sender, e) => { };
            sMovingToCB1HomeFromCabinet0502.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_05_02_Backward_Cabinet_02_Home_PUT.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeFromCabinet0502_CB1Home;
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
            }; sMovingToCB1HomeFromCabinet0502.OnExit += (sender, e) => { };
            sMovingToCB1HomeFromCabinet0503.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_05_03_Backward_Cabinet_02_Home_PUT.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeFromCabinet0503_CB1Home;
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
            }; sMovingToCB1HomeFromCabinet0503.OnExit += (sender, e) => { };
            sMovingToCB1HomeFromCabinet0504.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_05_04_Backward_Cabinet_02_Home_PUT.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeFromCabinet0504_CB1Home;
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
            }; sMovingToCB1HomeFromCabinet0504.OnExit += (sender, e) => { };
            sMovingToCB1HomeFromCabinet0505.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_05_05_Backward_Cabinet_02_Home_PUT.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeFromCabinet0505_CB1Home;
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
            }; sMovingToCB1HomeFromCabinet0505.OnExit += (sender, e) => { };
            sMovingToCB1HomeFromCabinet0601.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_06_01_Backward_Cabinet_02_Home_PUT.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeFromCabinet0601_CB1Home;
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
            }; sMovingToCB1HomeFromCabinet0601.OnExit += (sender, e) => { };
            sMovingToCB1HomeFromCabinet0602.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_06_02_Backward_Cabinet_02_Home_PUT.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeFromCabinet0602_CB1Home;
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
            }; sMovingToCB1HomeFromCabinet0602.OnExit += (sender, e) => { };
            sMovingToCB1HomeFromCabinet0603.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_06_03_Backward_Cabinet_02_Home_PUT.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeFromCabinet0603_CB1Home;
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
            }; sMovingToCB1HomeFromCabinet0603.OnExit += (sender, e) => { };
            sMovingToCB1HomeFromCabinet0604.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_06_04_Backward_Cabinet_02_Home_PUT.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeFromCabinet0604_CB1Home;
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
            }; sMovingToCB1HomeFromCabinet0604.OnExit += (sender, e) => { };
            sMovingToCB1HomeFromCabinet0605.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_06_05_Backward_Cabinet_02_Home_PUT.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeFromCabinet0605_CB1Home;
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
            }; sMovingToCB1HomeFromCabinet0605.OnExit += (sender, e) => { };
            sMovingToCB1HomeFromCabinet0701.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_07_01_Backward_Cabinet_02_Home_PUT.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeFromCabinet0701_CB1Home;
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
            }; sMovingToCB1HomeFromCabinet0701.OnExit += (sender, e) => { };
            sMovingToCB1HomeFromCabinet0702.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_07_02_Backward_Cabinet_02_Home_PUT.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeFromCabinet0702_CB1Home;
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
            }; sMovingToCB1HomeFromCabinet0702.OnExit += (sender, e) => { };
            sMovingToCB1HomeFromCabinet0703.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_07_03_Backward_Cabinet_02_Home_PUT.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeFromCabinet0703_CB1Home;
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
            }; sMovingToCB1HomeFromCabinet0703.OnExit += (sender, e) => { };
            sMovingToCB1HomeFromCabinet0704.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_07_04_Backward_Cabinet_02_Home_PUT.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeFromCabinet0704_CB1Home;
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
            }; sMovingToCB1HomeFromCabinet0704.OnExit += (sender, e) => { };
            sMovingToCB1HomeFromCabinet0705.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

                try
                {
                    HalBoxTransfer.RobotMoving(true);
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Drawer_07_05_Backward_Cabinet_02_Home_PUT.json");
                    HalBoxTransfer.ExePathMove(@"D:\Positions\BTRobot\Cabinet_01_Home.json");
                    HalBoxTransfer.RobotMoving(false);
                }
                catch (Exception ex)
                {
                    throw new BoxTransferPathMoveFailException(ex.Message);
                }

                var transition = tMovingToCB1HomeFromCabinet0705_CB1Home;
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
            }; sMovingToCB1HomeFromCabinet0705.OnExit += (sender, e) => { };
            #endregion Return To CB Home From Cabinet
            #endregion CB2
    */        
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

        private MacTransition EnumMacMsBoxTransferTransitionContainValue(string myValue)
        {
            if (!Enum.IsDefined(typeof(EnumMacMsBoxTransferTransition), myValue))
            { throw new BoxTransferException("Can not found " + myValue + " from EnumMacMsBoxTransferTransition list. "); }
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
