
using MvLib.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Msg
{
    public class MsgRisk : MsgBase
    {
        //Assign in create period
        public Object Sender;
        public Qc.DifficultScenario.DifficultScenarioID RiskId;
        public DateTime RecordTime;





    }
}
