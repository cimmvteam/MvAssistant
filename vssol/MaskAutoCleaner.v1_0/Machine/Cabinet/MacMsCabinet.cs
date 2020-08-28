using MaskAutoCleaner.v1_0.Machine.Drawer;
using MaskAutoCleaner.v1_0.StateMachineBeta;
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
        private IDictionary<string,MacMsDrawer> _dicDrawerStates;

        public MacMsCabinet()
        {
            _dicDrawerStates = new Dictionary<string, MacMsDrawer>();
        }

        public MacMsCabinet(IList<MacMsDrawer> drawerStates):this()
        {
            foreach(var drawerState in drawerStates)
            {
                if (_dicDrawerStates.ContainsKey(drawerState.Index))
                {
                    _dicDrawerStates.Remove(drawerState.Index);
                }
                _dicDrawerStates.Add(drawerState.Index, drawerState);
            } 
        }

        public override void LoadStateMachine()
        {
            MacState sIdleEmpty = NewState(EnumMacCabinetState.IdleEmpty);
        }
    }
}
