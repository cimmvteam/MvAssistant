using MaskCleaner.StateMachine.SmExp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MaskCleaner.StateMachine
{
    public class InternalTransition : Transition
    {
        public override bool RunGuard(StateMachine sm)
        {
            return base.RunGuard(sm);
        }
        public override bool RunTrigger(IStateParam param, StateMachine sm)
        {
            runAct.Invoke(sm, null);
            return true;
        }

        public InternalTransition(State fromState, string transName)
            : base(fromState, fromState, transName)
        {
        }

        public InternalTransition(State fromState, MethodInfo actionGuard, MethodInfo actionAction, string transName)
            : base(fromState, fromState, actionGuard, actionAction, transName)
        {
        }
        public InternalTransition(State fromState, Func<bool> actionGuard, Action actionAction, string transName)
            : base(fromState, fromState, actionGuard, actionAction, transName)
        {
        }
        public InternalTransition(StateMachine sm, State fromState, string transName)
            : base(sm, fromState, fromState, transName)
        {
        }

        public InternalTransition(StateMachine sm, State fromState, MethodInfo actionGuard, MethodInfo actionAction, string transName)
            : base(sm, fromState, fromState, actionGuard, actionAction, transName)
        {
        }
        public InternalTransition(StateMachine sm, State fromState, Func<bool> actionGuard, Action actionAction, string transName)
            : base(sm, fromState, fromState, actionGuard, actionAction, transName)
        {
        }
        internal override void GetElements(ref XmlDocument doc, ref XmlElement xStateMachine)
        {
            XmlElement xTrans = doc.CreateElement("Transition");
            xTrans.SetAttribute("Name", transName.ToString());
            xTrans.SetAttribute("Type", "Internal");
            XmlElement xTemp = doc.CreateElement("FromState");
            xTemp.SetAttribute("Name", FromState.StateName.ToString());
            xTrans.AppendChild(xTemp);
            xTemp = doc.CreateElement("ToState");
            xTemp.SetAttribute("Name", ToState.StateName.ToString());
            xTrans.AppendChild(xTemp);
            xTemp = doc.CreateElement("Guard");
            string tempN = /*runGuard.DeclaringType.ToString() + "." + */runGuard.Name;
            xTemp.SetAttribute("Name", tempN);
            xTrans.AppendChild(xTemp);
            xTemp = doc.CreateElement("Action");
            tempN = /*runAct.DeclaringType.ToString() + "." +*/ runAct.Name;
            xTemp.SetAttribute("Name", tempN);
            xTrans.AppendChild(xTemp);
            xStateMachine.AppendChild(xTrans);
        }
    }
}
