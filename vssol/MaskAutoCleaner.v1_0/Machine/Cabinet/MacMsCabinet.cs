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
        /// <param name="drawers"> Drawer 數量</param>
        public void Load(int drawers)
        {
            if (SumOfCabinetDrawerStates == 0)
            {

            }
            else
            {
                int loadDrawers = 0;
                foreach (var workState in _dicCabinetDrawerStates)
                {
                    var mState = workState.Value;
                    if (mState.CanLoad())
                    {
                        loadDrawers++;
                        mState.Load_MoveTrayToOut();
                        if(loadDrawers == drawers)
                        {
                            break;
                        }
                    }
                }
            }
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

            MacState sAnyState = NewState(EnumMacCabinetState.AnyState);
            MacState sStateMachineLoadAllDrawersStateMchineStart = NewState(EnumMacCabinetState.StateMachineLoadAllDrawersStateMchineStart);
            MacState sStateMachineLoadAllDrawersStateMchineIng = NewState(EnumMacCabinetState.StateMachineLoadAllDrawersStateMchineIng);
            MacState sStateMachineLoadAllDrawersStateMchineComplete = NewState(EnumMacCabinetState.StateMachineLoadAllDrawersStateMchineComplete);

            MacState sLoadMoveDrawerTraysToOutStart = NewState(EnumMacCabinetState.LoadMoveDrawerTraysToOutStart);
            MacState sLoadMoveDrawerTraysToOutIng = NewState(EnumMacCabinetState.LoadMoveDrawerTraysToOutIng);
            MacState sLoadMoveDrawerTraysToOutComplete = NewState(EnumMacCabinetState.LoadMoveDrawerTraysToOutComplete);

            MacState sInitialDrawersStart = NewState(EnumMacCabinetState.InitialDrawersStart);
            MacState sInitialDrawersIng = NewState(EnumMacCabinetState.InitialDrawersIng);
            MacState sInitialDrawersComplete = NewState(EnumMacCabinetState.InitialDrawersComplete);

            MacState sSynchronousDrawerStatesStart = NewState(EnumMacCabinetState.SynchronousDrawerStatesStart);
            MacState sSynchronousDrawerStatesIng = NewState(EnumMacCabinetState.SynchronousDrawerStatesIng);
            MacState sSynchronousDrawerStatesComplete = NewState(EnumMacCabinetState.SynchronousDrawerStatesComplete);


        }




    }
}
