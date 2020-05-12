using MaskAutoCleaner.Tasking;
using MvLib.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MaskAutoCleaner.StateOp
{
    public class MacState : State
    {
        public Timer TimerStateTimeout { get; set; }
        public MacTask s;
        public MacState() { }
        public MacState(string stateName)
            : base(stateName)
        {
        }

        public MacState(StateMachine sm, string stateName)
            : base(sm, stateName)
        {
        }





        public override void ExecDoEntry(StateEntryEventArgs seea)
        {
            var sm = seea.Sender as MacMachineBase;
            if (sm == null) throw new MacExcpetion("不支援非此專案的State Machine");
            if (sm.FlageNormalStatus)
            {
                if (sm.ChangeState(this))
                {
                    sm.SnapShot();
                }
                if (TimerStateTimeout != null && timeoutExecuteTransition != null)
                {
                    sm.SetCurrentTimeoutTranisition(timeoutExecuteTransition);
                    TimerStateTimeout.Interval = TimeoutLimit;
                    TimerStateTimeout.BeginInit();
                    TimerStateTimeout.Start();
                }
                DoEntry(seea);
            }
        }

        public override void DoExit(StateExitEventArgs args)
        {
            if (TimerStateTimeout != null) TimerStateTimeout.Stop();
            base.DoExit(args);
        }
       
    }
}
