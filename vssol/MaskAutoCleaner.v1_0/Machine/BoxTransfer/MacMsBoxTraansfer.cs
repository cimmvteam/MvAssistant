using MaskAutoCleaner.v1_0.StateMachineBeta;
using MaskAutoCleaner.v1_0.StateMachineExceptions.BoxTransferStateMachineException;
using MaskAutoCleaner.v1_0.StateMachineExceptions.UniversalStateMachineException;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MaskAutoCleaner.v1_0.Machine.MaskTransfer.MacMsMaskTransfer;

namespace MaskAutoCleaner.v1_0.Machine.BoxTransfer
{
    public class MacMsBoxTraansfer : MacMachineStateBase
    {
        private MacState _currentState = null;

        private IMacHalUniversal HalUniversal { get { return this.halAssembly as IMacHalUniversal; } }
        private IMacHalBoxTransfer HalBoxTransfer { get { return this.halAssembly as IMacHalBoxTransfer; } }
        private IMacHalOpenStage HalOpenStage { get { return this.halAssembly as IMacHalOpenStage; } }

        public void ResetState()
        { this.States[EnumMacMsBoxTransferState.Start.ToString()].DoEntry(new MacStateEntryEventArgs(null)); }

        private void SetCurrentState(MacState state)
        { _currentState = state; }

        public MacState CurrentState { get { return _currentState; } }

        public MacMsBoxTraansfer() { LoadStateMachine(); }

        MacMaskTransferUnitStateTimeOutController timeoutObj = new MacMaskTransferUnitStateTimeOutController();

        public override void LoadStateMachine()
        {
            #region State
            MacState sStart = NewState(EnumMacMsBoxTransferState.Start);
            MacState sInitial = NewState(EnumMacMsBoxTransferState.Initial);

            MacState sCB1Home = NewState(EnumMacMsBoxTransferState.CB1Home);
            MacState sCB2Home = NewState(EnumMacMsBoxTransferState.CB2Home);
            MacState sCB1HomeClamped = NewState(EnumMacMsBoxTransferState.CB1HomeClamped);
            MacState sCB2HomeClamped = NewState(EnumMacMsBoxTransferState.CB2HomeClamped);

            #region Change Direction
            MacState sChangingDirectionToCB1Home = NewState(EnumMacMsBoxTransferState.ChangingDirectionToCB1Home);
            MacState sChangingDirectionToCB2Home = NewState(EnumMacMsBoxTransferState.ChangingDirectionToCB2Home);
            MacState sChangingDirectionToCB1HomeClamped = NewState(EnumMacMsBoxTransferState.ChangingDirectionToCB1HomeClamped);
            MacState sChangingDirectionToCB2HomeClamped = NewState(EnumMacMsBoxTransferState.ChangingDirectionToCB2HomeClamped);
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
            #endregion Move To Cabinet

            #region Clamping At Cabinet
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
            #endregion Clamping At Cabinet

            #region Return To CB Home Clamped From Cabinet
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
            #endregion Return To CB Home Clamped From Cabinet

            #region Move To Cabinet Fro Release
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
            #endregion Move To Cabinet Fro Release

            #region Releasing At Cabinet
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
            #endregion Releasing At Cabinet

            #region Return To CB Home From Cabinet
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
            #endregion Return To CB Home From Cabinet
            #endregion CB1

            #region CB2
            #region Move To Cabinet
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
            #endregion Move To Cabinet

            #region Clamping At Cabinet
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
            #endregion Clamping At Cabinet

            #region Return To CB Home Clamped From Cabinet
            MacState sMovingToCB2HomeClampedFromCabinet0401 = NewState(EnumMacMsBoxTransferState.MovingToCB2HomeClampedFromCabinet0401);
            MacState sMovingToCB2HomeClampedFromCabinet0402 = NewState(EnumMacMsBoxTransferState.MovingToCB2HomeClampedFromCabinet0402);
            MacState sMovingToCB2HomeClampedFromCabinet0403 = NewState(EnumMacMsBoxTransferState.MovingToCB2HomeClampedFromCabinet0403);
            MacState sMovingToCB2HomeClampedFromCabinet0404 = NewState(EnumMacMsBoxTransferState.MovingToCB2HomeClampedFromCabinet0404);
            MacState sMovingToCB2HomeClampedFromCabinet0405 = NewState(EnumMacMsBoxTransferState.MovingToCB2HomeClampedFromCabinet0405);
            MacState sMovingToCB2HomeClampedFromCabinet0501 = NewState(EnumMacMsBoxTransferState.MovingToCB2HomeClampedFromCabinet0501);
            MacState sMovingToCB2HomeClampedFromCabinet0502 = NewState(EnumMacMsBoxTransferState.MovingToCB2HomeClampedFromCabinet0502);
            MacState sMovingToCB2HomeClampedFromCabinet0503 = NewState(EnumMacMsBoxTransferState.MovingToCB2HomeClampedFromCabinet0503);
            MacState sMovingToCB2HomeClampedFromCabinet0504 = NewState(EnumMacMsBoxTransferState.MovingToCB2HomeClampedFromCabinet0504);
            MacState sMovingToCB2HomeClampedFromCabinet0505 = NewState(EnumMacMsBoxTransferState.MovingToCB2HomeClampedFromCabinet0505);
            MacState sMovingToCB2HomeClampedFromCabinet0601 = NewState(EnumMacMsBoxTransferState.MovingToCB2HomeClampedFromCabinet0601);
            MacState sMovingToCB2HomeClampedFromCabinet0602 = NewState(EnumMacMsBoxTransferState.MovingToCB2HomeClampedFromCabinet0602);
            MacState sMovingToCB2HomeClampedFromCabinet0603 = NewState(EnumMacMsBoxTransferState.MovingToCB2HomeClampedFromCabinet0603);
            MacState sMovingToCB2HomeClampedFromCabinet0604 = NewState(EnumMacMsBoxTransferState.MovingToCB2HomeClampedFromCabinet0604);
            MacState sMovingToCB2HomeClampedFromCabinet0605 = NewState(EnumMacMsBoxTransferState.MovingToCB2HomeClampedFromCabinet0605);
            MacState sMovingToCB2HomeClampedFromCabinet0701 = NewState(EnumMacMsBoxTransferState.MovingToCB2HomeClampedFromCabinet0701);
            MacState sMovingToCB2HomeClampedFromCabinet0702 = NewState(EnumMacMsBoxTransferState.MovingToCB2HomeClampedFromCabinet0702);
            MacState sMovingToCB2HomeClampedFromCabinet0703 = NewState(EnumMacMsBoxTransferState.MovingToCB2HomeClampedFromCabinet0703);
            MacState sMovingToCB2HomeClampedFromCabinet0704 = NewState(EnumMacMsBoxTransferState.MovingToCB2HomeClampedFromCabinet0704);
            MacState sMovingToCB2HomeClampedFromCabinet0705 = NewState(EnumMacMsBoxTransferState.MovingToCB2HomeClampedFromCabinet0705);
            #endregion Return To CB Home Clamped From Cabinet

            #region Move To Cabinet Fro Release
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
            #endregion Move To Cabinet Fro Release

            #region Releasing At Cabinet
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
            #endregion Releasing At Cabinet

            #region Return To CB Home From Cabinet
            MacState sMovingToCB2HomeFromCabinet0401 = NewState(EnumMacMsBoxTransferState.MovingToCB2HomeFromCabinet0401);
            MacState sMovingToCB2HomeFromCabinet0402 = NewState(EnumMacMsBoxTransferState.MovingToCB2HomeFromCabinet0402);
            MacState sMovingToCB2HomeFromCabinet0403 = NewState(EnumMacMsBoxTransferState.MovingToCB2HomeFromCabinet0403);
            MacState sMovingToCB2HomeFromCabinet0404 = NewState(EnumMacMsBoxTransferState.MovingToCB2HomeFromCabinet0404);
            MacState sMovingToCB2HomeFromCabinet0405 = NewState(EnumMacMsBoxTransferState.MovingToCB2HomeFromCabinet0405);
            MacState sMovingToCB2HomeFromCabinet0501 = NewState(EnumMacMsBoxTransferState.MovingToCB2HomeFromCabinet0501);
            MacState sMovingToCB2HomeFromCabinet0502 = NewState(EnumMacMsBoxTransferState.MovingToCB2HomeFromCabinet0502);
            MacState sMovingToCB2HomeFromCabinet0503 = NewState(EnumMacMsBoxTransferState.MovingToCB2HomeFromCabinet0503);
            MacState sMovingToCB2HomeFromCabinet0504 = NewState(EnumMacMsBoxTransferState.MovingToCB2HomeFromCabinet0504);
            MacState sMovingToCB2HomeFromCabinet0505 = NewState(EnumMacMsBoxTransferState.MovingToCB2HomeFromCabinet0505);
            MacState sMovingToCB2HomeFromCabinet0601 = NewState(EnumMacMsBoxTransferState.MovingToCB2HomeFromCabinet0601);
            MacState sMovingToCB2HomeFromCabinet0602 = NewState(EnumMacMsBoxTransferState.MovingToCB2HomeFromCabinet0602);
            MacState sMovingToCB2HomeFromCabinet0603 = NewState(EnumMacMsBoxTransferState.MovingToCB2HomeFromCabinet0603);
            MacState sMovingToCB2HomeFromCabinet0604 = NewState(EnumMacMsBoxTransferState.MovingToCB2HomeFromCabinet0604);
            MacState sMovingToCB2HomeFromCabinet0605 = NewState(EnumMacMsBoxTransferState.MovingToCB2HomeFromCabinet0605);
            MacState sMovingToCB2HomeFromCabinet0701 = NewState(EnumMacMsBoxTransferState.MovingToCB2HomeFromCabinet0701);
            MacState sMovingToCB2HomeFromCabinet0702 = NewState(EnumMacMsBoxTransferState.MovingToCB2HomeFromCabinet0702);
            MacState sMovingToCB2HomeFromCabinet0703 = NewState(EnumMacMsBoxTransferState.MovingToCB2HomeFromCabinet0703);
            MacState sMovingToCB2HomeFromCabinet0704 = NewState(EnumMacMsBoxTransferState.MovingToCB2HomeFromCabinet0704);
            MacState sMovingToCB2HomeFromCabinet0705 = NewState(EnumMacMsBoxTransferState.MovingToCB2HomeFromCabinet0705);
            #endregion Return To CB Home From Cabinet
            #endregion CB2
            #endregion State

            #region Transition
            MacTransition tStart_DeviceInitial = NewTransition(sStart, sInitial, EnumMacMsBoxTransferTransition.PowerON);
            MacTransition tDeviceInitial_CB1Home = NewTransition(sInitial, sCB1Home, EnumMacMsBoxTransferTransition.Initial);

            MacTransition tCB1Home_NULL = NewTransition(sCB1Home, null, EnumMacMsBoxTransferTransition.StandbyAtCB1Home);
            MacTransition tCB1HomeClamped_NULL = NewTransition(sCB1HomeClamped, null, EnumMacMsBoxTransferTransition.StandbyAtCB1HomeClamped);

            #region Change Direction
            MacTransition tCB1Home_ChangingDirectionToCB2Home = NewTransition(sCB1Home, sChangingDirectionToCB2Home, EnumMacMsBoxTransferTransition.ChangeDirectionToCB2HomeFromCB1Home);
            MacTransition tCB2Home_ChangingDirectionToCB1Home = NewTransition(sCB2Home, sChangingDirectionToCB1Home, EnumMacMsBoxTransferTransition.ChangeDirectionToCB1HomeFromCB2Home);
            MacTransition tCB1HomeClamped_ChangingDirectionToCB2HomeClamped = NewTransition(sCB1HomeClamped, sChangingDirectionToCB2HomeClamped, EnumMacMsBoxTransferTransition.ChangeDirectionToCB2HomeClampedFromCB1HomeClamped);
            MacTransition tCB2HomeClamped_ChangingDirectionToCB1HomeClamped = NewTransition(sCB2HomeClamped, sChangingDirectionToCB1HomeClamped, EnumMacMsBoxTransferTransition.ChangeDirectionToCB1HomeClampedFromCB2HomeClamped);
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
            #endregion Get

            #region Release
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
            #endregion Release
            #endregion CB1

            #region CB2
            #region Get
            MacTransition tCB2Home_MovingToCabinet0401 = NewTransition(sCB2Home, sMovingToCabinet0401, EnumMacMsBoxTransferTransition.MoveToCB0401);
            MacTransition tCB2Home_MovingToCabinet0402 = NewTransition(sCB2Home, sMovingToCabinet0402, EnumMacMsBoxTransferTransition.MoveToCB0402);
            MacTransition tCB2Home_MovingToCabinet0403 = NewTransition(sCB2Home, sMovingToCabinet0403, EnumMacMsBoxTransferTransition.MoveToCB0403);
            MacTransition tCB2Home_MovingToCabinet0404 = NewTransition(sCB2Home, sMovingToCabinet0404, EnumMacMsBoxTransferTransition.MoveToCB0404);
            MacTransition tCB2Home_MovingToCabinet0405 = NewTransition(sCB2Home, sMovingToCabinet0405, EnumMacMsBoxTransferTransition.MoveToCB0405);
            MacTransition tCB2Home_MovingToCabinet0501 = NewTransition(sCB2Home, sMovingToCabinet0501, EnumMacMsBoxTransferTransition.MoveToCB0501);
            MacTransition tCB2Home_MovingToCabinet0502 = NewTransition(sCB2Home, sMovingToCabinet0502, EnumMacMsBoxTransferTransition.MoveToCB0502);
            MacTransition tCB2Home_MovingToCabinet0503 = NewTransition(sCB2Home, sMovingToCabinet0503, EnumMacMsBoxTransferTransition.MoveToCB0503);
            MacTransition tCB2Home_MovingToCabinet0504 = NewTransition(sCB2Home, sMovingToCabinet0504, EnumMacMsBoxTransferTransition.MoveToCB0504);
            MacTransition tCB2Home_MovingToCabinet0505 = NewTransition(sCB2Home, sMovingToCabinet0505, EnumMacMsBoxTransferTransition.MoveToCB0505);
            MacTransition tCB2Home_MovingToCabinet0601 = NewTransition(sCB2Home, sMovingToCabinet0601, EnumMacMsBoxTransferTransition.MoveToCB0601);
            MacTransition tCB2Home_MovingToCabinet0602 = NewTransition(sCB2Home, sMovingToCabinet0602, EnumMacMsBoxTransferTransition.MoveToCB0602);
            MacTransition tCB2Home_MovingToCabinet0603 = NewTransition(sCB2Home, sMovingToCabinet0603, EnumMacMsBoxTransferTransition.MoveToCB0603);
            MacTransition tCB2Home_MovingToCabinet0604 = NewTransition(sCB2Home, sMovingToCabinet0604, EnumMacMsBoxTransferTransition.MoveToCB0604);
            MacTransition tCB2Home_MovingToCabinet0605 = NewTransition(sCB2Home, sMovingToCabinet0605, EnumMacMsBoxTransferTransition.MoveToCB0605);
            MacTransition tCB2Home_MovingToCabinet0701 = NewTransition(sCB2Home, sMovingToCabinet0701, EnumMacMsBoxTransferTransition.MoveToCB0701);
            MacTransition tCB2Home_MovingToCabinet0702 = NewTransition(sCB2Home, sMovingToCabinet0702, EnumMacMsBoxTransferTransition.MoveToCB0702);
            MacTransition tCB2Home_MovingToCabinet0703 = NewTransition(sCB2Home, sMovingToCabinet0703, EnumMacMsBoxTransferTransition.MoveToCB0703);
            MacTransition tCB2Home_MovingToCabinet0704 = NewTransition(sCB2Home, sMovingToCabinet0704, EnumMacMsBoxTransferTransition.MoveToCB0704);
            MacTransition tCB2Home_MovingToCabinet0705 = NewTransition(sCB2Home, sMovingToCabinet0705, EnumMacMsBoxTransferTransition.MoveToCB0705);

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

            MacTransition tCabinet0401Clamping_MovingToCB2HomeClampedFromCabinet0401 = NewTransition(sCabinet0401Clamping, sMovingToCB2HomeClampedFromCabinet0401, EnumMacMsBoxTransferTransition.MoveToCB2HomeClampedFromCB0401);
            MacTransition tCabinet0402Clamping_MovingToCB2HomeClampedFromCabinet0402 = NewTransition(sCabinet0402Clamping, sMovingToCB2HomeClampedFromCabinet0402, EnumMacMsBoxTransferTransition.MoveToCB2HomeClampedFromCB0402);
            MacTransition tCabinet0403Clamping_MovingToCB2HomeClampedFromCabinet0403 = NewTransition(sCabinet0403Clamping, sMovingToCB2HomeClampedFromCabinet0403, EnumMacMsBoxTransferTransition.MoveToCB2HomeClampedFromCB0403);
            MacTransition tCabinet0404Clamping_MovingToCB2HomeClampedFromCabinet0404 = NewTransition(sCabinet0404Clamping, sMovingToCB2HomeClampedFromCabinet0404, EnumMacMsBoxTransferTransition.MoveToCB2HomeClampedFromCB0404);
            MacTransition tCabinet0405Clamping_MovingToCB2HomeClampedFromCabinet0405 = NewTransition(sCabinet0405Clamping, sMovingToCB2HomeClampedFromCabinet0405, EnumMacMsBoxTransferTransition.MoveToCB2HomeClampedFromCB0405);
            MacTransition tCabinet0501Clamping_MovingToCB2HomeClampedFromCabinet0501 = NewTransition(sCabinet0501Clamping, sMovingToCB2HomeClampedFromCabinet0501, EnumMacMsBoxTransferTransition.MoveToCB2HomeClampedFromCB0501);
            MacTransition tCabinet0502Clamping_MovingToCB2HomeClampedFromCabinet0502 = NewTransition(sCabinet0502Clamping, sMovingToCB2HomeClampedFromCabinet0502, EnumMacMsBoxTransferTransition.MoveToCB2HomeClampedFromCB0502);
            MacTransition tCabinet0503Clamping_MovingToCB2HomeClampedFromCabinet0503 = NewTransition(sCabinet0503Clamping, sMovingToCB2HomeClampedFromCabinet0503, EnumMacMsBoxTransferTransition.MoveToCB2HomeClampedFromCB0503);
            MacTransition tCabinet0504Clamping_MovingToCB2HomeClampedFromCabinet0504 = NewTransition(sCabinet0504Clamping, sMovingToCB2HomeClampedFromCabinet0504, EnumMacMsBoxTransferTransition.MoveToCB2HomeClampedFromCB0504);
            MacTransition tCabinet0505Clamping_MovingToCB2HomeClampedFromCabinet0505 = NewTransition(sCabinet0505Clamping, sMovingToCB2HomeClampedFromCabinet0505, EnumMacMsBoxTransferTransition.MoveToCB2HomeClampedFromCB0505);
            MacTransition tCabinet0601Clamping_MovingToCB2HomeClampedFromCabinet0601 = NewTransition(sCabinet0601Clamping, sMovingToCB2HomeClampedFromCabinet0601, EnumMacMsBoxTransferTransition.MoveToCB2HomeClampedFromCB0601);
            MacTransition tCabinet0602Clamping_MovingToCB2HomeClampedFromCabinet0602 = NewTransition(sCabinet0602Clamping, sMovingToCB2HomeClampedFromCabinet0602, EnumMacMsBoxTransferTransition.MoveToCB2HomeClampedFromCB0602);
            MacTransition tCabinet0603Clamping_MovingToCB2HomeClampedFromCabinet0603 = NewTransition(sCabinet0603Clamping, sMovingToCB2HomeClampedFromCabinet0603, EnumMacMsBoxTransferTransition.MoveToCB2HomeClampedFromCB0603);
            MacTransition tCabinet0604Clamping_MovingToCB2HomeClampedFromCabinet0604 = NewTransition(sCabinet0604Clamping, sMovingToCB2HomeClampedFromCabinet0604, EnumMacMsBoxTransferTransition.MoveToCB2HomeClampedFromCB0604);
            MacTransition tCabinet0605Clamping_MovingToCB2HomeClampedFromCabinet0605 = NewTransition(sCabinet0605Clamping, sMovingToCB2HomeClampedFromCabinet0605, EnumMacMsBoxTransferTransition.MoveToCB2HomeClampedFromCB0605);
            MacTransition tCabinet0701Clamping_MovingToCB2HomeClampedFromCabinet0701 = NewTransition(sCabinet0701Clamping, sMovingToCB2HomeClampedFromCabinet0701, EnumMacMsBoxTransferTransition.MoveToCB2HomeClampedFromCB0701);
            MacTransition tCabinet0702Clamping_MovingToCB2HomeClampedFromCabinet0702 = NewTransition(sCabinet0702Clamping, sMovingToCB2HomeClampedFromCabinet0702, EnumMacMsBoxTransferTransition.MoveToCB2HomeClampedFromCB0702);
            MacTransition tCabinet0703Clamping_MovingToCB2HomeClampedFromCabinet0703 = NewTransition(sCabinet0703Clamping, sMovingToCB2HomeClampedFromCabinet0703, EnumMacMsBoxTransferTransition.MoveToCB2HomeClampedFromCB0703);
            MacTransition tCabinet0704Clamping_MovingToCB2HomeClampedFromCabinet0704 = NewTransition(sCabinet0704Clamping, sMovingToCB2HomeClampedFromCabinet0704, EnumMacMsBoxTransferTransition.MoveToCB2HomeClampedFromCB0704);
            MacTransition tCabinet0705Clamping_MovingToCB2HomeClampedFromCabinet0705 = NewTransition(sCabinet0705Clamping, sMovingToCB2HomeClampedFromCabinet0705, EnumMacMsBoxTransferTransition.MoveToCB2HomeClampedFromCB0705);

            MacTransition tMovingToCB2HomeClampedFromCabinet0401_CB2HomeClamped = NewTransition(sMovingToCB2HomeClampedFromCabinet0401, sCB2HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB2HomeClampedFromCB0401);
            MacTransition tMovingToCB2HomeClampedFromCabinet0402_CB2HomeClamped = NewTransition(sMovingToCB2HomeClampedFromCabinet0402, sCB2HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB2HomeClampedFromCB0402);
            MacTransition tMovingToCB2HomeClampedFromCabinet0403_CB2HomeClamped = NewTransition(sMovingToCB2HomeClampedFromCabinet0403, sCB2HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB2HomeClampedFromCB0403);
            MacTransition tMovingToCB2HomeClampedFromCabinet0404_CB2HomeClamped = NewTransition(sMovingToCB2HomeClampedFromCabinet0404, sCB2HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB2HomeClampedFromCB0404);
            MacTransition tMovingToCB2HomeClampedFromCabinet0405_CB2HomeClamped = NewTransition(sMovingToCB2HomeClampedFromCabinet0405, sCB2HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB2HomeClampedFromCB0405);
            MacTransition tMovingToCB2HomeClampedFromCabinet0501_CB2HomeClamped = NewTransition(sMovingToCB2HomeClampedFromCabinet0501, sCB2HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB2HomeClampedFromCB0501);
            MacTransition tMovingToCB2HomeClampedFromCabinet0502_CB2HomeClamped = NewTransition(sMovingToCB2HomeClampedFromCabinet0502, sCB2HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB2HomeClampedFromCB0502);
            MacTransition tMovingToCB2HomeClampedFromCabinet0503_CB2HomeClamped = NewTransition(sMovingToCB2HomeClampedFromCabinet0503, sCB2HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB2HomeClampedFromCB0503);
            MacTransition tMovingToCB2HomeClampedFromCabinet0504_CB2HomeClamped = NewTransition(sMovingToCB2HomeClampedFromCabinet0504, sCB2HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB2HomeClampedFromCB0504);
            MacTransition tMovingToCB2HomeClampedFromCabinet0505_CB2HomeClamped = NewTransition(sMovingToCB2HomeClampedFromCabinet0505, sCB2HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB2HomeClampedFromCB0505);
            MacTransition tMovingToCB2HomeClampedFromCabinet0601_CB2HomeClamped = NewTransition(sMovingToCB2HomeClampedFromCabinet0601, sCB2HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB2HomeClampedFromCB0601);
            MacTransition tMovingToCB2HomeClampedFromCabinet0602_CB2HomeClamped = NewTransition(sMovingToCB2HomeClampedFromCabinet0602, sCB2HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB2HomeClampedFromCB0602);
            MacTransition tMovingToCB2HomeClampedFromCabinet0603_CB2HomeClamped = NewTransition(sMovingToCB2HomeClampedFromCabinet0603, sCB2HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB2HomeClampedFromCB0603);
            MacTransition tMovingToCB2HomeClampedFromCabinet0604_CB2HomeClamped = NewTransition(sMovingToCB2HomeClampedFromCabinet0604, sCB2HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB2HomeClampedFromCB0604);
            MacTransition tMovingToCB2HomeClampedFromCabinet0605_CB2HomeClamped = NewTransition(sMovingToCB2HomeClampedFromCabinet0605, sCB2HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB2HomeClampedFromCB0605);
            MacTransition tMovingToCB2HomeClampedFromCabinet0701_CB2HomeClamped = NewTransition(sMovingToCB2HomeClampedFromCabinet0701, sCB2HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB2HomeClampedFromCB0701);
            MacTransition tMovingToCB2HomeClampedFromCabinet0702_CB2HomeClamped = NewTransition(sMovingToCB2HomeClampedFromCabinet0702, sCB2HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB2HomeClampedFromCB0702);
            MacTransition tMovingToCB2HomeClampedFromCabinet0703_CB2HomeClamped = NewTransition(sMovingToCB2HomeClampedFromCabinet0703, sCB2HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB2HomeClampedFromCB0703);
            MacTransition tMovingToCB2HomeClampedFromCabinet0704_CB2HomeClamped = NewTransition(sMovingToCB2HomeClampedFromCabinet0704, sCB2HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB2HomeClampedFromCB0704);
            MacTransition tMovingToCB2HomeClampedFromCabinet0705_CB2HomeClamped = NewTransition(sMovingToCB2HomeClampedFromCabinet0705, sCB2HomeClamped, EnumMacMsBoxTransferTransition.StandbyAtCB2HomeClampedFromCB0705);
            #endregion Get

            #region Release
            MacTransition tCB2HomeClamped_MovingToCabinet0401ForRelease = NewTransition(sCB2HomeClamped, sMovingToCabinet0401ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0401ForRelease);
            MacTransition tCB2HomeClamped_MovingToCabinet0402ForRelease = NewTransition(sCB2HomeClamped, sMovingToCabinet0402ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0402ForRelease);
            MacTransition tCB2HomeClamped_MovingToCabinet0403ForRelease = NewTransition(sCB2HomeClamped, sMovingToCabinet0403ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0403ForRelease);
            MacTransition tCB2HomeClamped_MovingToCabinet0404ForRelease = NewTransition(sCB2HomeClamped, sMovingToCabinet0404ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0404ForRelease);
            MacTransition tCB2HomeClamped_MovingToCabinet0405ForRelease = NewTransition(sCB2HomeClamped, sMovingToCabinet0405ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0405ForRelease);
            MacTransition tCB2HomeClamped_MovingToCabinet0501ForRelease = NewTransition(sCB2HomeClamped, sMovingToCabinet0501ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0501ForRelease);
            MacTransition tCB2HomeClamped_MovingToCabinet0502ForRelease = NewTransition(sCB2HomeClamped, sMovingToCabinet0502ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0502ForRelease);
            MacTransition tCB2HomeClamped_MovingToCabinet0503ForRelease = NewTransition(sCB2HomeClamped, sMovingToCabinet0503ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0503ForRelease);
            MacTransition tCB2HomeClamped_MovingToCabinet0504ForRelease = NewTransition(sCB2HomeClamped, sMovingToCabinet0504ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0504ForRelease);
            MacTransition tCB2HomeClamped_MovingToCabinet0505ForRelease = NewTransition(sCB2HomeClamped, sMovingToCabinet0505ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0505ForRelease);
            MacTransition tCB2HomeClamped_MovingToCabinet0601ForRelease = NewTransition(sCB2HomeClamped, sMovingToCabinet0601ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0601ForRelease);
            MacTransition tCB2HomeClamped_MovingToCabinet0602ForRelease = NewTransition(sCB2HomeClamped, sMovingToCabinet0602ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0602ForRelease);
            MacTransition tCB2HomeClamped_MovingToCabinet0603ForRelease = NewTransition(sCB2HomeClamped, sMovingToCabinet0603ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0603ForRelease);
            MacTransition tCB2HomeClamped_MovingToCabinet0604ForRelease = NewTransition(sCB2HomeClamped, sMovingToCabinet0604ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0604ForRelease);
            MacTransition tCB2HomeClamped_MovingToCabinet0605ForRelease = NewTransition(sCB2HomeClamped, sMovingToCabinet0605ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0605ForRelease);
            MacTransition tCB2HomeClamped_MovingToCabinet0701ForRelease = NewTransition(sCB2HomeClamped, sMovingToCabinet0701ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0701ForRelease);
            MacTransition tCB2HomeClamped_MovingToCabinet0702ForRelease = NewTransition(sCB2HomeClamped, sMovingToCabinet0702ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0702ForRelease);
            MacTransition tCB2HomeClamped_MovingToCabinet0703ForRelease = NewTransition(sCB2HomeClamped, sMovingToCabinet0703ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0703ForRelease);
            MacTransition tCB2HomeClamped_MovingToCabinet0704ForRelease = NewTransition(sCB2HomeClamped, sMovingToCabinet0704ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0704ForRelease);
            MacTransition tCB2HomeClamped_MovingToCabinet0705ForRelease = NewTransition(sCB2HomeClamped, sMovingToCabinet0705ForRelease, EnumMacMsBoxTransferTransition.MoveToCB0705ForRelease);

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

            MacTransition tCabinet0401Releasing_MovingToCB2HomeFromCabinet0401 = NewTransition(sCabinet0401Releasing, sMovingToCB2HomeFromCabinet0401, EnumMacMsBoxTransferTransition.MoveToCB2HomeFromCB0401);
            MacTransition tCabinet0402Releasing_MovingToCB2HomeFromCabinet0402 = NewTransition(sCabinet0402Releasing, sMovingToCB2HomeFromCabinet0402, EnumMacMsBoxTransferTransition.MoveToCB2HomeFromCB0402);
            MacTransition tCabinet0403Releasing_MovingToCB2HomeFromCabinet0403 = NewTransition(sCabinet0403Releasing, sMovingToCB2HomeFromCabinet0403, EnumMacMsBoxTransferTransition.MoveToCB2HomeFromCB0403);
            MacTransition tCabinet0404Releasing_MovingToCB2HomeFromCabinet0404 = NewTransition(sCabinet0404Releasing, sMovingToCB2HomeFromCabinet0404, EnumMacMsBoxTransferTransition.MoveToCB2HomeFromCB0404);
            MacTransition tCabinet0405Releasing_MovingToCB2HomeFromCabinet0405 = NewTransition(sCabinet0405Releasing, sMovingToCB2HomeFromCabinet0405, EnumMacMsBoxTransferTransition.MoveToCB2HomeFromCB0405);
            MacTransition tCabinet0501Releasing_MovingToCB2HomeFromCabinet0501 = NewTransition(sCabinet0501Releasing, sMovingToCB2HomeFromCabinet0501, EnumMacMsBoxTransferTransition.MoveToCB2HomeFromCB0501);
            MacTransition tCabinet0502Releasing_MovingToCB2HomeFromCabinet0502 = NewTransition(sCabinet0502Releasing, sMovingToCB2HomeFromCabinet0502, EnumMacMsBoxTransferTransition.MoveToCB2HomeFromCB0502);
            MacTransition tCabinet0503Releasing_MovingToCB2HomeFromCabinet0503 = NewTransition(sCabinet0503Releasing, sMovingToCB2HomeFromCabinet0503, EnumMacMsBoxTransferTransition.MoveToCB2HomeFromCB0503);
            MacTransition tCabinet0504Releasing_MovingToCB2HomeFromCabinet0504 = NewTransition(sCabinet0504Releasing, sMovingToCB2HomeFromCabinet0504, EnumMacMsBoxTransferTransition.MoveToCB2HomeFromCB0504);
            MacTransition tCabinet0505Releasing_MovingToCB2HomeFromCabinet0505 = NewTransition(sCabinet0505Releasing, sMovingToCB2HomeFromCabinet0505, EnumMacMsBoxTransferTransition.MoveToCB2HomeFromCB0505);
            MacTransition tCabinet0601Releasing_MovingToCB2HomeFromCabinet0601 = NewTransition(sCabinet0601Releasing, sMovingToCB2HomeFromCabinet0601, EnumMacMsBoxTransferTransition.MoveToCB2HomeFromCB0601);
            MacTransition tCabinet0602Releasing_MovingToCB2HomeFromCabinet0602 = NewTransition(sCabinet0602Releasing, sMovingToCB2HomeFromCabinet0602, EnumMacMsBoxTransferTransition.MoveToCB2HomeFromCB0602);
            MacTransition tCabinet0603Releasing_MovingToCB2HomeFromCabinet0603 = NewTransition(sCabinet0603Releasing, sMovingToCB2HomeFromCabinet0603, EnumMacMsBoxTransferTransition.MoveToCB2HomeFromCB0603);
            MacTransition tCabinet0604Releasing_MovingToCB2HomeFromCabinet0604 = NewTransition(sCabinet0604Releasing, sMovingToCB2HomeFromCabinet0604, EnumMacMsBoxTransferTransition.MoveToCB2HomeFromCB0604);
            MacTransition tCabinet0605Releasing_MovingToCB2HomeFromCabinet0605 = NewTransition(sCabinet0605Releasing, sMovingToCB2HomeFromCabinet0605, EnumMacMsBoxTransferTransition.MoveToCB2HomeFromCB0605);
            MacTransition tCabinet0701Releasing_MovingToCB2HomeFromCabinet0701 = NewTransition(sCabinet0701Releasing, sMovingToCB2HomeFromCabinet0701, EnumMacMsBoxTransferTransition.MoveToCB2HomeFromCB0701);
            MacTransition tCabinet0702Releasing_MovingToCB2HomeFromCabinet0702 = NewTransition(sCabinet0702Releasing, sMovingToCB2HomeFromCabinet0702, EnumMacMsBoxTransferTransition.MoveToCB2HomeFromCB0702);
            MacTransition tCabinet0703Releasing_MovingToCB2HomeFromCabinet0703 = NewTransition(sCabinet0703Releasing, sMovingToCB2HomeFromCabinet0703, EnumMacMsBoxTransferTransition.MoveToCB2HomeFromCB0703);
            MacTransition tCabinet0704Releasing_MovingToCB2HomeFromCabinet0704 = NewTransition(sCabinet0704Releasing, sMovingToCB2HomeFromCabinet0704, EnumMacMsBoxTransferTransition.MoveToCB2HomeFromCB0704);
            MacTransition tCabinet0705Releasing_MovingToCB2HomeFromCabinet0705 = NewTransition(sCabinet0705Releasing, sMovingToCB2HomeFromCabinet0705, EnumMacMsBoxTransferTransition.MoveToCB2HomeFromCB0705);

            MacTransition tMovingToCB2HomeFromCabinet0401_CB2Home = NewTransition(sMovingToCB2HomeFromCabinet0401, sCB2Home, EnumMacMsBoxTransferTransition.StandbyAtCB2HomeFromCB0401);
            MacTransition tMovingToCB2HomeFromCabinet0402_CB2Home = NewTransition(sMovingToCB2HomeFromCabinet0402, sCB2Home, EnumMacMsBoxTransferTransition.StandbyAtCB2HomeFromCB0402);
            MacTransition tMovingToCB2HomeFromCabinet0403_CB2Home = NewTransition(sMovingToCB2HomeFromCabinet0403, sCB2Home, EnumMacMsBoxTransferTransition.StandbyAtCB2HomeFromCB0403);
            MacTransition tMovingToCB2HomeFromCabinet0404_CB2Home = NewTransition(sMovingToCB2HomeFromCabinet0404, sCB2Home, EnumMacMsBoxTransferTransition.StandbyAtCB2HomeFromCB0404);
            MacTransition tMovingToCB2HomeFromCabinet0405_CB2Home = NewTransition(sMovingToCB2HomeFromCabinet0405, sCB2Home, EnumMacMsBoxTransferTransition.StandbyAtCB2HomeFromCB0405);
            MacTransition tMovingToCB2HomeFromCabinet0501_CB2Home = NewTransition(sMovingToCB2HomeFromCabinet0501, sCB2Home, EnumMacMsBoxTransferTransition.StandbyAtCB2HomeFromCB0501);
            MacTransition tMovingToCB2HomeFromCabinet0502_CB2Home = NewTransition(sMovingToCB2HomeFromCabinet0502, sCB2Home, EnumMacMsBoxTransferTransition.StandbyAtCB2HomeFromCB0502);
            MacTransition tMovingToCB2HomeFromCabinet0503_CB2Home = NewTransition(sMovingToCB2HomeFromCabinet0503, sCB2Home, EnumMacMsBoxTransferTransition.StandbyAtCB2HomeFromCB0503);
            MacTransition tMovingToCB2HomeFromCabinet0504_CB2Home = NewTransition(sMovingToCB2HomeFromCabinet0504, sCB2Home, EnumMacMsBoxTransferTransition.StandbyAtCB2HomeFromCB0504);
            MacTransition tMovingToCB2HomeFromCabinet0505_CB2Home = NewTransition(sMovingToCB2HomeFromCabinet0505, sCB2Home, EnumMacMsBoxTransferTransition.StandbyAtCB2HomeFromCB0505);
            MacTransition tMovingToCB2HomeFromCabinet0601_CB2Home = NewTransition(sMovingToCB2HomeFromCabinet0601, sCB2Home, EnumMacMsBoxTransferTransition.StandbyAtCB2HomeFromCB0601);
            MacTransition tMovingToCB2HomeFromCabinet0602_CB2Home = NewTransition(sMovingToCB2HomeFromCabinet0602, sCB2Home, EnumMacMsBoxTransferTransition.StandbyAtCB2HomeFromCB0602);
            MacTransition tMovingToCB2HomeFromCabinet0603_CB2Home = NewTransition(sMovingToCB2HomeFromCabinet0603, sCB2Home, EnumMacMsBoxTransferTransition.StandbyAtCB2HomeFromCB0603);
            MacTransition tMovingToCB2HomeFromCabinet0604_CB2Home = NewTransition(sMovingToCB2HomeFromCabinet0604, sCB2Home, EnumMacMsBoxTransferTransition.StandbyAtCB2HomeFromCB0604);
            MacTransition tMovingToCB2HomeFromCabinet0605_CB2Home = NewTransition(sMovingToCB2HomeFromCabinet0605, sCB2Home, EnumMacMsBoxTransferTransition.StandbyAtCB2HomeFromCB0605);
            MacTransition tMovingToCB2HomeFromCabinet0701_CB2Home = NewTransition(sMovingToCB2HomeFromCabinet0701, sCB2Home, EnumMacMsBoxTransferTransition.StandbyAtCB2HomeFromCB0701);
            MacTransition tMovingToCB2HomeFromCabinet0702_CB2Home = NewTransition(sMovingToCB2HomeFromCabinet0702, sCB2Home, EnumMacMsBoxTransferTransition.StandbyAtCB2HomeFromCB0702);
            MacTransition tMovingToCB2HomeFromCabinet0703_CB2Home = NewTransition(sMovingToCB2HomeFromCabinet0703, sCB2Home, EnumMacMsBoxTransferTransition.StandbyAtCB2HomeFromCB0703);
            MacTransition tMovingToCB2HomeFromCabinet0704_CB2Home = NewTransition(sMovingToCB2HomeFromCabinet0704, sCB2Home, EnumMacMsBoxTransferTransition.StandbyAtCB2HomeFromCB0704);
            MacTransition tMovingToCB2HomeFromCabinet0705_CB2Home = NewTransition(sMovingToCB2HomeFromCabinet0705, sCB2Home, EnumMacMsBoxTransferTransition.StandbyAtCB2HomeFromCB0705);
            #endregion Release
            #endregion CB2
            #endregion Transition

            #region State Register OnEntry OnExit
            sStart.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);

                CheckEquipmentStatus();
                CheckAssemblyAlarmSignal();
                CheckAssemblyWarningSignal();

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
            }; sStart.OnExit += (sender, e) => { };
            sInitial.OnEntry += (sender, e) =>
            {
                SetCurrentState((MacState)sender);
                try
                {
                    HalBoxTransfer.Initial();
                }
                catch (Exception ex)
                {
                    throw new BoxTransferInitialFailException(ex.Message);
                }

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
            }; sInitial.OnExit += (sender, e) => { };

            sCB1Home.OnEntry += (sender, e) => { }; sCB1Home.OnExit += (sender, e) => { };
            sCB2Home.OnEntry += (sender, e) => { }; sCB2Home.OnExit += (sender, e) => { };
            sCB1HomeClamped.OnEntry += (sender, e) => { }; sCB1HomeClamped.OnExit += (sender, e) => { };
            sCB2HomeClamped.OnEntry += (sender, e) => { }; sCB2HomeClamped.OnExit += (sender, e) => { };

            #region Change Direction
            sChangingDirectionToCB1Home.OnEntry += (sender, e) => { }; sChangingDirectionToCB1Home.OnExit += (sender, e) => { };
            sChangingDirectionToCB2Home.OnEntry += (sender, e) => { }; sChangingDirectionToCB2Home.OnExit += (sender, e) => { };
            sChangingDirectionToCB1HomeClamped.OnEntry += (sender, e) => { }; sChangingDirectionToCB1HomeClamped.OnExit += (sender, e) => { };
            sChangingDirectionToCB2HomeClamped.OnEntry += (sender, e) => { }; sChangingDirectionToCB2HomeClamped.OnExit += (sender, e) => { };
            #endregion Change Direction

            #region OS
            sMovingToOpenStage.OnEntry += (sender, e) => { }; sMovingToOpenStage.OnExit += (sender, e) => { };
            sOpenStageClamping.OnEntry += (sender, e) => { }; sOpenStageClamping.OnExit += (sender, e) => { };
            sMovingToCB1HomeClampedFromOpenStage.OnEntry += (sender, e) => { }; sMovingToCB1HomeClampedFromOpenStage.OnExit += (sender, e) => { };

            sMovingToOpenStageForRelease.OnEntry += (sender, e) => { }; sMovingToOpenStageForRelease.OnExit += (sender, e) => { };
            sOpenStageReleasing.OnEntry += (sender, e) => { }; sOpenStageReleasing.OnExit += (sender, e) => { };
            sMovingToCB1HomeFromOpenStage.OnEntry += (sender, e) => { }; sMovingToCB1HomeFromOpenStage.OnExit += (sender, e) => { };
            #endregion OS

            #region Lock & Unlock
            sLocking.OnEntry += (sender, e) => { }; sLocking.OnExit += (sender, e) => { };
            sUnlocking.OnEntry += (sender, e) => { }; sUnlocking.OnExit += (sender, e) => { };
            #endregion Lock & Unlock

            #region CB1
            #region Move To Cabinet
            sMovingToCabinet0101.OnEntry += (sender, e) => { }; sMovingToCabinet0101.OnExit += (sender, e) => { };
            sMovingToCabinet0102.OnEntry += (sender, e) => { }; sMovingToCabinet0102.OnExit += (sender, e) => { };
            sMovingToCabinet0103.OnEntry += (sender, e) => { }; sMovingToCabinet0103.OnExit += (sender, e) => { };
            sMovingToCabinet0104.OnEntry += (sender, e) => { }; sMovingToCabinet0104.OnExit += (sender, e) => { };
            sMovingToCabinet0105.OnEntry += (sender, e) => { }; sMovingToCabinet0105.OnExit += (sender, e) => { };
            sMovingToCabinet0201.OnEntry += (sender, e) => { }; sMovingToCabinet0201.OnExit += (sender, e) => { };
            sMovingToCabinet0202.OnEntry += (sender, e) => { }; sMovingToCabinet0202.OnExit += (sender, e) => { };
            sMovingToCabinet0203.OnEntry += (sender, e) => { }; sMovingToCabinet0203.OnExit += (sender, e) => { };
            sMovingToCabinet0204.OnEntry += (sender, e) => { }; sMovingToCabinet0204.OnExit += (sender, e) => { };
            sMovingToCabinet0205.OnEntry += (sender, e) => { }; sMovingToCabinet0205.OnExit += (sender, e) => { };
            sMovingToCabinet0301.OnEntry += (sender, e) => { }; sMovingToCabinet0301.OnExit += (sender, e) => { };
            sMovingToCabinet0302.OnEntry += (sender, e) => { }; sMovingToCabinet0302.OnExit += (sender, e) => { };
            sMovingToCabinet0303.OnEntry += (sender, e) => { }; sMovingToCabinet0303.OnExit += (sender, e) => { };
            sMovingToCabinet0304.OnEntry += (sender, e) => { }; sMovingToCabinet0304.OnExit += (sender, e) => { };
            sMovingToCabinet0305.OnEntry += (sender, e) => { }; sMovingToCabinet0305.OnExit += (sender, e) => { };
            #endregion Move To Cabinet

            #region Clamping At Cabinet
            sCabinet0101Clamping.OnEntry += (sender, e) => { }; sCabinet0101Clamping.OnExit += (sender, e) => { };
            sCabinet0102Clamping.OnEntry += (sender, e) => { }; sCabinet0102Clamping.OnExit += (sender, e) => { };
            sCabinet0103Clamping.OnEntry += (sender, e) => { }; sCabinet0103Clamping.OnExit += (sender, e) => { };
            sCabinet0104Clamping.OnEntry += (sender, e) => { }; sCabinet0104Clamping.OnExit += (sender, e) => { };
            sCabinet0105Clamping.OnEntry += (sender, e) => { }; sCabinet0105Clamping.OnExit += (sender, e) => { };
            sCabinet0201Clamping.OnEntry += (sender, e) => { }; sCabinet0201Clamping.OnExit += (sender, e) => { };
            sCabinet0202Clamping.OnEntry += (sender, e) => { }; sCabinet0202Clamping.OnExit += (sender, e) => { };
            sCabinet0203Clamping.OnEntry += (sender, e) => { }; sCabinet0203Clamping.OnExit += (sender, e) => { };
            sCabinet0204Clamping.OnEntry += (sender, e) => { }; sCabinet0204Clamping.OnExit += (sender, e) => { };
            sCabinet0205Clamping.OnEntry += (sender, e) => { }; sCabinet0205Clamping.OnExit += (sender, e) => { };
            sCabinet0301Clamping.OnEntry += (sender, e) => { }; sCabinet0301Clamping.OnExit += (sender, e) => { };
            sCabinet0302Clamping.OnEntry += (sender, e) => { }; sCabinet0302Clamping.OnExit += (sender, e) => { };
            sCabinet0303Clamping.OnEntry += (sender, e) => { }; sCabinet0303Clamping.OnExit += (sender, e) => { };
            sCabinet0304Clamping.OnEntry += (sender, e) => { }; sCabinet0304Clamping.OnExit += (sender, e) => { };
            sCabinet0305Clamping.OnEntry += (sender, e) => { }; sCabinet0305Clamping.OnExit += (sender, e) => { };
            #endregion Clamping At Cabinet

            #region Return To CB Home Clamped From Cabinet
            sMovingToCB1HomeClampedFromCabinet0101.OnEntry += (sender, e) => { }; sMovingToCB1HomeClampedFromCabinet0101.OnExit += (sender, e) => { };
            sMovingToCB1HomeClampedFromCabinet0102.OnEntry += (sender, e) => { }; sMovingToCB1HomeClampedFromCabinet0102.OnExit += (sender, e) => { };
            sMovingToCB1HomeClampedFromCabinet0103.OnEntry += (sender, e) => { }; sMovingToCB1HomeClampedFromCabinet0103.OnExit += (sender, e) => { };
            sMovingToCB1HomeClampedFromCabinet0104.OnEntry += (sender, e) => { }; sMovingToCB1HomeClampedFromCabinet0104.OnExit += (sender, e) => { };
            sMovingToCB1HomeClampedFromCabinet0105.OnEntry += (sender, e) => { }; sMovingToCB1HomeClampedFromCabinet0105.OnExit += (sender, e) => { };
            sMovingToCB1HomeClampedFromCabinet0201.OnEntry += (sender, e) => { }; sMovingToCB1HomeClampedFromCabinet0201.OnExit += (sender, e) => { };
            sMovingToCB1HomeClampedFromCabinet0202.OnEntry += (sender, e) => { }; sMovingToCB1HomeClampedFromCabinet0202.OnExit += (sender, e) => { };
            sMovingToCB1HomeClampedFromCabinet0203.OnEntry += (sender, e) => { }; sMovingToCB1HomeClampedFromCabinet0203.OnExit += (sender, e) => { };
            sMovingToCB1HomeClampedFromCabinet0204.OnEntry += (sender, e) => { }; sMovingToCB1HomeClampedFromCabinet0204.OnExit += (sender, e) => { };
            sMovingToCB1HomeClampedFromCabinet0205.OnEntry += (sender, e) => { }; sMovingToCB1HomeClampedFromCabinet0205.OnExit += (sender, e) => { };
            sMovingToCB1HomeClampedFromCabinet0301.OnEntry += (sender, e) => { }; sMovingToCB1HomeClampedFromCabinet0301.OnExit += (sender, e) => { };
            sMovingToCB1HomeClampedFromCabinet0302.OnEntry += (sender, e) => { }; sMovingToCB1HomeClampedFromCabinet0302.OnExit += (sender, e) => { };
            sMovingToCB1HomeClampedFromCabinet0303.OnEntry += (sender, e) => { }; sMovingToCB1HomeClampedFromCabinet0303.OnExit += (sender, e) => { };
            sMovingToCB1HomeClampedFromCabinet0304.OnEntry += (sender, e) => { }; sMovingToCB1HomeClampedFromCabinet0304.OnExit += (sender, e) => { };
            sMovingToCB1HomeClampedFromCabinet0305.OnEntry += (sender, e) => { }; sMovingToCB1HomeClampedFromCabinet0305.OnExit += (sender, e) => { };
            #endregion Return To CB Home Clamped From Cabinet

            #region Move To Cabinet Fro Release
            sMovingToCabinet0101ForRelease.OnEntry += (sender, e) => { }; sMovingToCabinet0101ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0102ForRelease.OnEntry += (sender, e) => { }; sMovingToCabinet0102ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0103ForRelease.OnEntry += (sender, e) => { }; sMovingToCabinet0103ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0104ForRelease.OnEntry += (sender, e) => { }; sMovingToCabinet0104ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0105ForRelease.OnEntry += (sender, e) => { }; sMovingToCabinet0105ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0201ForRelease.OnEntry += (sender, e) => { }; sMovingToCabinet0201ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0202ForRelease.OnEntry += (sender, e) => { }; sMovingToCabinet0202ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0203ForRelease.OnEntry += (sender, e) => { }; sMovingToCabinet0203ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0204ForRelease.OnEntry += (sender, e) => { }; sMovingToCabinet0204ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0205ForRelease.OnEntry += (sender, e) => { }; sMovingToCabinet0205ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0301ForRelease.OnEntry += (sender, e) => { }; sMovingToCabinet0301ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0302ForRelease.OnEntry += (sender, e) => { }; sMovingToCabinet0302ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0303ForRelease.OnEntry += (sender, e) => { }; sMovingToCabinet0303ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0304ForRelease.OnEntry += (sender, e) => { }; sMovingToCabinet0304ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0305ForRelease.OnEntry += (sender, e) => { }; sMovingToCabinet0305ForRelease.OnExit += (sender, e) => { };
            #endregion Move To Cabinet Fro Release

            #region Releasing At Cabinet
            sCabinet0101Releasing.OnEntry += (sender, e) => { }; sCabinet0101Releasing.OnExit += (sender, e) => { };
            sCabinet0102Releasing.OnEntry += (sender, e) => { }; sCabinet0102Releasing.OnExit += (sender, e) => { };
            sCabinet0103Releasing.OnEntry += (sender, e) => { }; sCabinet0103Releasing.OnExit += (sender, e) => { };
            sCabinet0104Releasing.OnEntry += (sender, e) => { }; sCabinet0104Releasing.OnExit += (sender, e) => { };
            sCabinet0105Releasing.OnEntry += (sender, e) => { }; sCabinet0105Releasing.OnExit += (sender, e) => { };
            sCabinet0201Releasing.OnEntry += (sender, e) => { }; sCabinet0201Releasing.OnExit += (sender, e) => { };
            sCabinet0202Releasing.OnEntry += (sender, e) => { }; sCabinet0202Releasing.OnExit += (sender, e) => { };
            sCabinet0203Releasing.OnEntry += (sender, e) => { }; sCabinet0203Releasing.OnExit += (sender, e) => { };
            sCabinet0204Releasing.OnEntry += (sender, e) => { }; sCabinet0204Releasing.OnExit += (sender, e) => { };
            sCabinet0205Releasing.OnEntry += (sender, e) => { }; sCabinet0205Releasing.OnExit += (sender, e) => { };
            sCabinet0301Releasing.OnEntry += (sender, e) => { }; sCabinet0301Releasing.OnExit += (sender, e) => { };
            sCabinet0302Releasing.OnEntry += (sender, e) => { }; sCabinet0302Releasing.OnExit += (sender, e) => { };
            sCabinet0303Releasing.OnEntry += (sender, e) => { }; sCabinet0303Releasing.OnExit += (sender, e) => { };
            sCabinet0304Releasing.OnEntry += (sender, e) => { }; sCabinet0304Releasing.OnExit += (sender, e) => { };
            sCabinet0305Releasing.OnEntry += (sender, e) => { }; sCabinet0305Releasing.OnExit += (sender, e) => { };
            #endregion Releasing At Cabinet

            #region Return To CB Home From Cabinet
            sMovingToCB1HomeFromCabinet0101.OnEntry += (sender, e) => { }; sMovingToCB1HomeFromCabinet0101.OnExit += (sender, e) => { };
            sMovingToCB1HomeFromCabinet0102.OnEntry += (sender, e) => { }; sMovingToCB1HomeFromCabinet0102.OnExit += (sender, e) => { };
            sMovingToCB1HomeFromCabinet0103.OnEntry += (sender, e) => { }; sMovingToCB1HomeFromCabinet0103.OnExit += (sender, e) => { };
            sMovingToCB1HomeFromCabinet0104.OnEntry += (sender, e) => { }; sMovingToCB1HomeFromCabinet0104.OnExit += (sender, e) => { };
            sMovingToCB1HomeFromCabinet0105.OnEntry += (sender, e) => { }; sMovingToCB1HomeFromCabinet0105.OnExit += (sender, e) => { };
            sMovingToCB1HomeFromCabinet0201.OnEntry += (sender, e) => { }; sMovingToCB1HomeFromCabinet0201.OnExit += (sender, e) => { };
            sMovingToCB1HomeFromCabinet0202.OnEntry += (sender, e) => { }; sMovingToCB1HomeFromCabinet0202.OnExit += (sender, e) => { };
            sMovingToCB1HomeFromCabinet0203.OnEntry += (sender, e) => { }; sMovingToCB1HomeFromCabinet0203.OnExit += (sender, e) => { };
            sMovingToCB1HomeFromCabinet0204.OnEntry += (sender, e) => { }; sMovingToCB1HomeFromCabinet0204.OnExit += (sender, e) => { };
            sMovingToCB1HomeFromCabinet0205.OnEntry += (sender, e) => { }; sMovingToCB1HomeFromCabinet0205.OnExit += (sender, e) => { };
            sMovingToCB1HomeFromCabinet0301.OnEntry += (sender, e) => { }; sMovingToCB1HomeFromCabinet0301.OnExit += (sender, e) => { };
            sMovingToCB1HomeFromCabinet0302.OnEntry += (sender, e) => { }; sMovingToCB1HomeFromCabinet0302.OnExit += (sender, e) => { };
            sMovingToCB1HomeFromCabinet0303.OnEntry += (sender, e) => { }; sMovingToCB1HomeFromCabinet0303.OnExit += (sender, e) => { };
            sMovingToCB1HomeFromCabinet0304.OnEntry += (sender, e) => { }; sMovingToCB1HomeFromCabinet0304.OnExit += (sender, e) => { };
            sMovingToCB1HomeFromCabinet0305.OnEntry += (sender, e) => { }; sMovingToCB1HomeFromCabinet0305.OnExit += (sender, e) => { };
            #endregion Return To CB Home From Cabinet
            #endregion CB1

            #region CB2
            #region Move To Cabinet
            sMovingToCabinet0401.OnEntry += (sender, e) => { }; sMovingToCabinet0401.OnExit += (sender, e) => { };
            sMovingToCabinet0402.OnEntry += (sender, e) => { }; sMovingToCabinet0402.OnExit += (sender, e) => { };
            sMovingToCabinet0403.OnEntry += (sender, e) => { }; sMovingToCabinet0403.OnExit += (sender, e) => { };
            sMovingToCabinet0404.OnEntry += (sender, e) => { }; sMovingToCabinet0404.OnExit += (sender, e) => { };
            sMovingToCabinet0405.OnEntry += (sender, e) => { }; sMovingToCabinet0405.OnExit += (sender, e) => { };
            sMovingToCabinet0501.OnEntry += (sender, e) => { }; sMovingToCabinet0501.OnExit += (sender, e) => { };
            sMovingToCabinet0502.OnEntry += (sender, e) => { }; sMovingToCabinet0502.OnExit += (sender, e) => { };
            sMovingToCabinet0503.OnEntry += (sender, e) => { }; sMovingToCabinet0503.OnExit += (sender, e) => { };
            sMovingToCabinet0504.OnEntry += (sender, e) => { }; sMovingToCabinet0504.OnExit += (sender, e) => { };
            sMovingToCabinet0505.OnEntry += (sender, e) => { }; sMovingToCabinet0505.OnExit += (sender, e) => { };
            sMovingToCabinet0601.OnEntry += (sender, e) => { }; sMovingToCabinet0601.OnExit += (sender, e) => { };
            sMovingToCabinet0602.OnEntry += (sender, e) => { }; sMovingToCabinet0602.OnExit += (sender, e) => { };
            sMovingToCabinet0603.OnEntry += (sender, e) => { }; sMovingToCabinet0603.OnExit += (sender, e) => { };
            sMovingToCabinet0604.OnEntry += (sender, e) => { }; sMovingToCabinet0604.OnExit += (sender, e) => { };
            sMovingToCabinet0605.OnEntry += (sender, e) => { }; sMovingToCabinet0605.OnExit += (sender, e) => { };
            sMovingToCabinet0701.OnEntry += (sender, e) => { }; sMovingToCabinet0701.OnExit += (sender, e) => { };
            sMovingToCabinet0702.OnEntry += (sender, e) => { }; sMovingToCabinet0702.OnExit += (sender, e) => { };
            sMovingToCabinet0703.OnEntry += (sender, e) => { }; sMovingToCabinet0703.OnExit += (sender, e) => { };
            sMovingToCabinet0704.OnEntry += (sender, e) => { }; sMovingToCabinet0704.OnExit += (sender, e) => { };
            sMovingToCabinet0705.OnEntry += (sender, e) => { }; sMovingToCabinet0705.OnExit += (sender, e) => { };
            #endregion Move To Cabinet

            #region Clamping At Cabinet
            sCabinet0401Clamping.OnEntry += (sender, e) => { }; sCabinet0401Clamping.OnExit += (sender, e) => { };
            sCabinet0402Clamping.OnEntry += (sender, e) => { }; sCabinet0402Clamping.OnExit += (sender, e) => { };
            sCabinet0403Clamping.OnEntry += (sender, e) => { }; sCabinet0403Clamping.OnExit += (sender, e) => { };
            sCabinet0404Clamping.OnEntry += (sender, e) => { }; sCabinet0404Clamping.OnExit += (sender, e) => { };
            sCabinet0405Clamping.OnEntry += (sender, e) => { }; sCabinet0405Clamping.OnExit += (sender, e) => { };
            sCabinet0501Clamping.OnEntry += (sender, e) => { }; sCabinet0501Clamping.OnExit += (sender, e) => { };
            sCabinet0502Clamping.OnEntry += (sender, e) => { }; sCabinet0502Clamping.OnExit += (sender, e) => { };
            sCabinet0503Clamping.OnEntry += (sender, e) => { }; sCabinet0503Clamping.OnExit += (sender, e) => { };
            sCabinet0504Clamping.OnEntry += (sender, e) => { }; sCabinet0504Clamping.OnExit += (sender, e) => { };
            sCabinet0505Clamping.OnEntry += (sender, e) => { }; sCabinet0505Clamping.OnExit += (sender, e) => { };
            sCabinet0601Clamping.OnEntry += (sender, e) => { }; sCabinet0601Clamping.OnExit += (sender, e) => { };
            sCabinet0602Clamping.OnEntry += (sender, e) => { }; sCabinet0602Clamping.OnExit += (sender, e) => { };
            sCabinet0603Clamping.OnEntry += (sender, e) => { }; sCabinet0603Clamping.OnExit += (sender, e) => { };
            sCabinet0604Clamping.OnEntry += (sender, e) => { }; sCabinet0604Clamping.OnExit += (sender, e) => { };
            sCabinet0605Clamping.OnEntry += (sender, e) => { }; sCabinet0605Clamping.OnExit += (sender, e) => { };
            sCabinet0701Clamping.OnEntry += (sender, e) => { }; sCabinet0701Clamping.OnExit += (sender, e) => { };
            sCabinet0702Clamping.OnEntry += (sender, e) => { }; sCabinet0702Clamping.OnExit += (sender, e) => { };
            sCabinet0703Clamping.OnEntry += (sender, e) => { }; sCabinet0703Clamping.OnExit += (sender, e) => { };
            sCabinet0704Clamping.OnEntry += (sender, e) => { }; sCabinet0704Clamping.OnExit += (sender, e) => { };
            sCabinet0705Clamping.OnEntry += (sender, e) => { }; sCabinet0705Clamping.OnExit += (sender, e) => { };
            #endregion Clamping At Cabinet

            #region Return To CB Home Clamped From Cabinet
            sMovingToCB2HomeClampedFromCabinet0401.OnEntry += (sender, e) => { }; sMovingToCB2HomeClampedFromCabinet0401.OnExit += (sender, e) => { };
            sMovingToCB2HomeClampedFromCabinet0402.OnEntry += (sender, e) => { }; sMovingToCB2HomeClampedFromCabinet0402.OnExit += (sender, e) => { };
            sMovingToCB2HomeClampedFromCabinet0403.OnEntry += (sender, e) => { }; sMovingToCB2HomeClampedFromCabinet0403.OnExit += (sender, e) => { };
            sMovingToCB2HomeClampedFromCabinet0404.OnEntry += (sender, e) => { }; sMovingToCB2HomeClampedFromCabinet0404.OnExit += (sender, e) => { };
            sMovingToCB2HomeClampedFromCabinet0405.OnEntry += (sender, e) => { }; sMovingToCB2HomeClampedFromCabinet0405.OnExit += (sender, e) => { };
            sMovingToCB2HomeClampedFromCabinet0501.OnEntry += (sender, e) => { }; sMovingToCB2HomeClampedFromCabinet0501.OnExit += (sender, e) => { };
            sMovingToCB2HomeClampedFromCabinet0502.OnEntry += (sender, e) => { }; sMovingToCB2HomeClampedFromCabinet0502.OnExit += (sender, e) => { };
            sMovingToCB2HomeClampedFromCabinet0503.OnEntry += (sender, e) => { }; sMovingToCB2HomeClampedFromCabinet0503.OnExit += (sender, e) => { };
            sMovingToCB2HomeClampedFromCabinet0504.OnEntry += (sender, e) => { }; sMovingToCB2HomeClampedFromCabinet0504.OnExit += (sender, e) => { };
            sMovingToCB2HomeClampedFromCabinet0505.OnEntry += (sender, e) => { }; sMovingToCB2HomeClampedFromCabinet0505.OnExit += (sender, e) => { };
            sMovingToCB2HomeClampedFromCabinet0601.OnEntry += (sender, e) => { }; sMovingToCB2HomeClampedFromCabinet0601.OnExit += (sender, e) => { };
            sMovingToCB2HomeClampedFromCabinet0602.OnEntry += (sender, e) => { }; sMovingToCB2HomeClampedFromCabinet0602.OnExit += (sender, e) => { };
            sMovingToCB2HomeClampedFromCabinet0603.OnEntry += (sender, e) => { }; sMovingToCB2HomeClampedFromCabinet0603.OnExit += (sender, e) => { };
            sMovingToCB2HomeClampedFromCabinet0604.OnEntry += (sender, e) => { }; sMovingToCB2HomeClampedFromCabinet0604.OnExit += (sender, e) => { };
            sMovingToCB2HomeClampedFromCabinet0605.OnEntry += (sender, e) => { }; sMovingToCB2HomeClampedFromCabinet0605.OnExit += (sender, e) => { };
            sMovingToCB2HomeClampedFromCabinet0701.OnEntry += (sender, e) => { }; sMovingToCB2HomeClampedFromCabinet0701.OnExit += (sender, e) => { };
            sMovingToCB2HomeClampedFromCabinet0702.OnEntry += (sender, e) => { }; sMovingToCB2HomeClampedFromCabinet0702.OnExit += (sender, e) => { };
            sMovingToCB2HomeClampedFromCabinet0703.OnEntry += (sender, e) => { }; sMovingToCB2HomeClampedFromCabinet0703.OnExit += (sender, e) => { };
            sMovingToCB2HomeClampedFromCabinet0704.OnEntry += (sender, e) => { }; sMovingToCB2HomeClampedFromCabinet0704.OnExit += (sender, e) => { };
            sMovingToCB2HomeClampedFromCabinet0705.OnEntry += (sender, e) => { }; sMovingToCB2HomeClampedFromCabinet0705.OnExit += (sender, e) => { };
            #endregion Return To CB Home Clamped From Cabinet

            #region Move To Cabinet Fro Release
            sMovingToCabinet0401ForRelease.OnEntry += (sender, e) => { }; sMovingToCabinet0401ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0402ForRelease.OnEntry += (sender, e) => { }; sMovingToCabinet0402ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0403ForRelease.OnEntry += (sender, e) => { }; sMovingToCabinet0403ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0404ForRelease.OnEntry += (sender, e) => { }; sMovingToCabinet0404ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0405ForRelease.OnEntry += (sender, e) => { }; sMovingToCabinet0405ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0501ForRelease.OnEntry += (sender, e) => { }; sMovingToCabinet0501ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0502ForRelease.OnEntry += (sender, e) => { }; sMovingToCabinet0502ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0503ForRelease.OnEntry += (sender, e) => { }; sMovingToCabinet0503ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0504ForRelease.OnEntry += (sender, e) => { }; sMovingToCabinet0504ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0505ForRelease.OnEntry += (sender, e) => { }; sMovingToCabinet0505ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0601ForRelease.OnEntry += (sender, e) => { }; sMovingToCabinet0601ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0602ForRelease.OnEntry += (sender, e) => { }; sMovingToCabinet0602ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0603ForRelease.OnEntry += (sender, e) => { }; sMovingToCabinet0603ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0604ForRelease.OnEntry += (sender, e) => { }; sMovingToCabinet0604ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0605ForRelease.OnEntry += (sender, e) => { }; sMovingToCabinet0605ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0701ForRelease.OnEntry += (sender, e) => { }; sMovingToCabinet0701ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0702ForRelease.OnEntry += (sender, e) => { }; sMovingToCabinet0702ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0703ForRelease.OnEntry += (sender, e) => { }; sMovingToCabinet0703ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0704ForRelease.OnEntry += (sender, e) => { }; sMovingToCabinet0704ForRelease.OnExit += (sender, e) => { };
            sMovingToCabinet0705ForRelease.OnEntry += (sender, e) => { }; sMovingToCabinet0705ForRelease.OnExit += (sender, e) => { };
            #endregion Move To Cabinet Fro Release

            #region Releasing At Cabinet
            sCabinet0401Releasing.OnEntry += (sender, e) => { }; sCabinet0401Releasing.OnExit += (sender, e) => { };
            sCabinet0402Releasing.OnEntry += (sender, e) => { }; sCabinet0402Releasing.OnExit += (sender, e) => { };
            sCabinet0403Releasing.OnEntry += (sender, e) => { }; sCabinet0403Releasing.OnExit += (sender, e) => { };
            sCabinet0404Releasing.OnEntry += (sender, e) => { }; sCabinet0404Releasing.OnExit += (sender, e) => { };
            sCabinet0405Releasing.OnEntry += (sender, e) => { }; sCabinet0405Releasing.OnExit += (sender, e) => { };
            sCabinet0501Releasing.OnEntry += (sender, e) => { }; sCabinet0501Releasing.OnExit += (sender, e) => { };
            sCabinet0502Releasing.OnEntry += (sender, e) => { }; sCabinet0502Releasing.OnExit += (sender, e) => { };
            sCabinet0503Releasing.OnEntry += (sender, e) => { }; sCabinet0503Releasing.OnExit += (sender, e) => { };
            sCabinet0504Releasing.OnEntry += (sender, e) => { }; sCabinet0504Releasing.OnExit += (sender, e) => { };
            sCabinet0505Releasing.OnEntry += (sender, e) => { }; sCabinet0505Releasing.OnExit += (sender, e) => { };
            sCabinet0601Releasing.OnEntry += (sender, e) => { }; sCabinet0601Releasing.OnExit += (sender, e) => { };
            sCabinet0602Releasing.OnEntry += (sender, e) => { }; sCabinet0602Releasing.OnExit += (sender, e) => { };
            sCabinet0603Releasing.OnEntry += (sender, e) => { }; sCabinet0603Releasing.OnExit += (sender, e) => { };
            sCabinet0604Releasing.OnEntry += (sender, e) => { }; sCabinet0604Releasing.OnExit += (sender, e) => { };
            sCabinet0605Releasing.OnEntry += (sender, e) => { }; sCabinet0605Releasing.OnExit += (sender, e) => { };
            sCabinet0701Releasing.OnEntry += (sender, e) => { }; sCabinet0701Releasing.OnExit += (sender, e) => { };
            sCabinet0702Releasing.OnEntry += (sender, e) => { }; sCabinet0702Releasing.OnExit += (sender, e) => { };
            sCabinet0703Releasing.OnEntry += (sender, e) => { }; sCabinet0703Releasing.OnExit += (sender, e) => { };
            sCabinet0704Releasing.OnEntry += (sender, e) => { }; sCabinet0704Releasing.OnExit += (sender, e) => { };
            sCabinet0705Releasing.OnEntry += (sender, e) => { }; sCabinet0705Releasing.OnExit += (sender, e) => { };
            #endregion Releasing At Cabinet

            #region Return To CB Home From Cabinet
            sMovingToCB2HomeFromCabinet0401.OnEntry += (sender, e) => { }; sMovingToCB2HomeFromCabinet0401.OnExit += (sender, e) => { };
            sMovingToCB2HomeFromCabinet0402.OnEntry += (sender, e) => { }; sMovingToCB2HomeFromCabinet0402.OnExit += (sender, e) => { };
            sMovingToCB2HomeFromCabinet0403.OnEntry += (sender, e) => { }; sMovingToCB2HomeFromCabinet0403.OnExit += (sender, e) => { };
            sMovingToCB2HomeFromCabinet0404.OnEntry += (sender, e) => { }; sMovingToCB2HomeFromCabinet0404.OnExit += (sender, e) => { };
            sMovingToCB2HomeFromCabinet0405.OnEntry += (sender, e) => { }; sMovingToCB2HomeFromCabinet0405.OnExit += (sender, e) => { };
            sMovingToCB2HomeFromCabinet0501.OnEntry += (sender, e) => { }; sMovingToCB2HomeFromCabinet0501.OnExit += (sender, e) => { };
            sMovingToCB2HomeFromCabinet0502.OnEntry += (sender, e) => { }; sMovingToCB2HomeFromCabinet0502.OnExit += (sender, e) => { };
            sMovingToCB2HomeFromCabinet0503.OnEntry += (sender, e) => { }; sMovingToCB2HomeFromCabinet0503.OnExit += (sender, e) => { };
            sMovingToCB2HomeFromCabinet0504.OnEntry += (sender, e) => { }; sMovingToCB2HomeFromCabinet0504.OnExit += (sender, e) => { };
            sMovingToCB2HomeFromCabinet0505.OnEntry += (sender, e) => { }; sMovingToCB2HomeFromCabinet0505.OnExit += (sender, e) => { };
            sMovingToCB2HomeFromCabinet0601.OnEntry += (sender, e) => { }; sMovingToCB2HomeFromCabinet0601.OnExit += (sender, e) => { };
            sMovingToCB2HomeFromCabinet0602.OnEntry += (sender, e) => { }; sMovingToCB2HomeFromCabinet0602.OnExit += (sender, e) => { };
            sMovingToCB2HomeFromCabinet0603.OnEntry += (sender, e) => { }; sMovingToCB2HomeFromCabinet0603.OnExit += (sender, e) => { };
            sMovingToCB2HomeFromCabinet0604.OnEntry += (sender, e) => { }; sMovingToCB2HomeFromCabinet0604.OnExit += (sender, e) => { };
            sMovingToCB2HomeFromCabinet0605.OnEntry += (sender, e) => { }; sMovingToCB2HomeFromCabinet0605.OnExit += (sender, e) => { };
            sMovingToCB2HomeFromCabinet0701.OnEntry += (sender, e) => { }; sMovingToCB2HomeFromCabinet0701.OnExit += (sender, e) => { };
            sMovingToCB2HomeFromCabinet0702.OnEntry += (sender, e) => { }; sMovingToCB2HomeFromCabinet0702.OnExit += (sender, e) => { };
            sMovingToCB2HomeFromCabinet0703.OnEntry += (sender, e) => { }; sMovingToCB2HomeFromCabinet0703.OnExit += (sender, e) => { };
            sMovingToCB2HomeFromCabinet0704.OnEntry += (sender, e) => { }; sMovingToCB2HomeFromCabinet0704.OnExit += (sender, e) => { };
            sMovingToCB2HomeFromCabinet0705.OnEntry += (sender, e) => { }; sMovingToCB2HomeFromCabinet0705.OnExit += (sender, e) => { };
            #endregion Return To CB Home From Cabinet
            #endregion CB2
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
    }
}
