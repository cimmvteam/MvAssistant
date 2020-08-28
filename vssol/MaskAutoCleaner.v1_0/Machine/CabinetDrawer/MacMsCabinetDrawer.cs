using MaskAutoCleaner.v1_0.StateMachineBeta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.CabinetDrawer
{
    [Guid("11111111-1111-1111-1111-111111111111")]// TODO: UPdate this Guid
    public class MacMsCabinetDrawer : MacMachineStateBase
    {
        public override void LoadStateMachine()
        {

            #region State
            MacState sInitialStart = NewState(EnumMacCabinetDrawerState.InitialStart);
            MacState sInitialIng = NewState(EnumMacCabinetDrawerState.InitialIng);
            MacState sInitialComplete = NewState(EnumMacCabinetDrawerState.InitialComplete);
            
            MacState sMoveTrayToHomeStart= NewState(EnumMacCabinetDrawerState.MoveTrayToHomeStart);
            MacState sMoveTrayToHomeIng = NewState(EnumMacCabinetDrawerState.MoveTrayToHomeIng);
            MacState sMoveTrayToHomeComplete = NewState(EnumMacCabinetDrawerState.MoveTrayToInComplete);

            MacState sMoveTrayToOutStart = NewState(EnumMacCabinetDrawerState.MoveTrayToOutStart);
            MacState sMoveTrayToOutIng = NewState(EnumMacCabinetDrawerState.MoveTrayToInIng);
            MacState sMoveTrayToOutComplete = NewState(EnumMacCabinetDrawerState.MoveTrayToOutComplete);

            MacState sMoveTrayToInStart = NewState(EnumMacCabinetDrawerState.MoveTrayToInStart);
            MacState sMoveTrayToInIng = NewState(EnumMacCabinetDrawerState.MoveTrayToInIng);
            MacState sMoveTrayToInComplete = NewState(EnumMacCabinetDrawerState.MoveTrayToInComplete);
            #endregion State

            #region Transition
            MacTransition tInitialStart_InitialIng = NewTransition(sInitialStart, sInitialIng, EnumMacCabinetDrawerTransition.InitialStart_InitialIng);
            MacTransition tInitialIng_InitialComplete = NewTransition(sInitialIng,sInitialComplete, EnumMacCabinetDrawerTransition.InitialIng_InitialComplete);
            MacTransition tInitialComplete_NULL = NewTransition(sInitialComplete,null, EnumMacCabinetDrawerTransition.InitialComplete_NULL);

            MacTransition tMoveTrayToHomeStart_MoveTrayToHomeIng = NewTransition(sMoveTrayToHomeStart,sMoveTrayToHomeIng, EnumMacCabinetDrawerTransition.MoveTrayToHomeStart_MoveTrayToHomeIng);
            MacTransition tMoveTrayToHomeIng_MoveTrayToHomeComplete = NewTransition(sMoveTrayToHomeIng, sMoveTrayToHomeComplete, EnumMacCabinetDrawerTransition.MoveTrayToHomeIng_MoveTrayToHomeComplete);
            MacTransition tMoveTrayToHomeComplete_NULL = NewTransition(sMoveTrayToHomeComplete, null, EnumMacCabinetDrawerTransition.MoveTrayToInComplete_NULL);

            MacTransition tMoveTrayToOutStart_MoveTrayToOutIng = NewTransition(sMoveTrayToOutStart, sMoveTrayToOutIng, EnumMacCabinetDrawerTransition.MoveTrayToOutStart_MoveTrayToOutIng);
            MacTransition tMoveTrayToOutIng_MoveTrayToOutComplete = NewTransition(sMoveTrayToOutIng, sMoveTrayToOutComplete, EnumMacCabinetDrawerTransition.MoveTrayToOutIng_MoveTrayToOutComplete);
            MacTransition tMoveTrayToOutComplete_NULL = NewTransition(sMoveTrayToOutComplete, null, EnumMacCabinetDrawerTransition.MoveTrayToOutComplete_NULL);

            MacTransition tMoveTrayToInStart_MoveTrayToInIng = NewTransition(sMoveTrayToInStart, sMoveTrayToInIng, EnumMacCabinetDrawerTransition.MoveTrayToInStart_MoveTrayToInIng);
            MacTransition tMoveTrayToInIng_MoveTrayToInComplete = NewTransition(sMoveTrayToInIng, sMoveTrayToInComplete, EnumMacCabinetDrawerTransition.MoveTrayToInIng_MoveTrayToInComplete);
            MacTransition tMoveTrayToInComplete_NULL = NewTransition(sMoveTrayToInComplete, null, EnumMacCabinetDrawerTransition.MoveTrayToInComplete_NULL);
            #endregion Transition




        }
    }


}
