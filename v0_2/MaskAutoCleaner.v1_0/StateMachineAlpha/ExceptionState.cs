using MaskAutoCleaner.v1_0.StateMachineAlpha.SmExp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MaskAutoCleaner.v1_0.StateMachineAlpha
{

    /// <summary>
    /// State類別，名字在SM需要唯一
    /// </summary>
    public class ExceptionState : State
    {
        public IStateErrorInfo ReasonOfState;
        public Enum AlarmId
        {
            get
            {
                if (this.ReasonOfState == null) return null;
                return ReasonOfState.StateAlarmId;
            }
            set
            {
                ReasonOfState.StateAlarmId = value;
            }
        }

        public ExceptionState() { }
        public ExceptionState(string stateName)
            : base(stateName)
        {
            OnEntry += SECSCommucation;
        }

        public ExceptionState(StateMachine sm, string stateName)
            : base(sm, stateName)
        {
            OnEntry += SECSCommucation;
        }

        public ExceptionState(string name, IStateErrorInfo ex)
            : base(name)
        {
            // TODO: Complete member initialization
            this.ReasonOfState = ex;
        }

        public void SECSCommucation(object sender, EventArgs e)
        {
            
        }

        //public Dictionary<string, State> substates = new Dictionary<string, State>();
        public override void GetElements(ref XmlDocument doc, ref XmlElement xStateMachine)
        {
            XmlElement xState = doc.CreateElement("State");
            xState.SetAttribute("Name", StateName.ToString());
            xState.SetAttribute("Type", "ExceptionState");
            GetEntryElements(ref doc, ref xState);
            GetExitElements(ref doc, ref xState);
            xStateMachine.AppendChild(xState);
        }

        //override do entry function
        public override void ExecDoEntry(StateEntryEventArgs senea)
        {
            StateMachine sm = senea.Sender;
            sm.ChangeState(this);
            //sm.SmSnapShot();TODO: 要不要Snap應由專案決定, 不由Library決定

            DoEntry(senea);
            if (ReasonOfState.StateAlarmId != null)
            {
                sm.PopupAlarm(sm, EnumStateMachineMsgType.StateMachineException, ReasonOfState.StateAlarmId.ToString());
            }
            else
            {
                throw new StateMachineException("AlarmId is fail string");
            }

        }
    }
}