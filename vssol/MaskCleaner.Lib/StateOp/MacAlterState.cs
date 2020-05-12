using MvLib.StateMachine;
using MvLib.TaskDispatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.StateOp
{

    [TaskPlan(DispatchTime = "20181206", Sponsor = EnumSponsor.MHTSAIZW, Version = "0.1.0", TaskType = EnumTaskType.NewFunction, Name = "Implement alter state", Description = "提供多個traisit目標的state, 需要實作EvalList->評估需使用哪個Transition", Status = EnumTaskStatus.ONGOING)]
    public class MacAlterState: MacState
    {
        #region 建構Function
        public MacAlterState() { }
        public MacAlterState(string stateName) : base(stateName)
        {
        }
        public MacAlterState(StateMachine sm, string stateName) : base(sm,stateName)
        {
        }

        #endregion
    }
}
