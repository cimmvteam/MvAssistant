using MaskAutoCleaner.v1_0.StateMachineBeta;
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
            MacTransition tCB2HomeClamped_ChangingDirectionToCB1HomeClamped
 = NewTransition(sCB2HomeClamped, sChangingDirectionToCB1HomeClamped, EnumMacMsBoxTransferTransition.ChangeDirectionToCB1HomeClampedFromCB2HomeClamped);
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

            #endregion Lock & Unlock

            #region CB1

            #endregion CB1

            #region CB2

            #endregion CB2
            #endregion Transition

            #region State Register OnEntry OnExit

            #endregion State Register OnEntry OnExit
        }
    }
}
