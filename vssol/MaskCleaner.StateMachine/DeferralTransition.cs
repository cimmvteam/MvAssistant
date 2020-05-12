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
    public class DeferralTransition : Transition
    {
       
        public override bool RunGuard(StateMachine sm)
        {
            return base.RunGuard(sm);
        }
        public override bool RunTrigger(IStateParam param, StateMachine sm)
        {
            try
            {
                StateExitEventArgs seea = new StateExitEventArgs();
                FromState.DoExit(seea);
            }
            catch
            {
                throw new StateMachineFatelException(FromState.StateName + ": get exit fail event");
            }
            runAct.Invoke(sm, null);
            try
            {
                StateEntryEventArgs seea = new StateEntryEventArgs(sm);
                ToState.ExecDoEntry(seea);
            }
            catch
            {
                throw new StateMachineFatelException(ToState.StateName + ": get entry fail event");
            }
            return true;
        }

        public DeferralTransition(State fromState, State toState, string transName)
            : base(fromState, toState, transName)
        {
        }

        public DeferralTransition(State fromState, State toState, MethodInfo actionGuard, MethodInfo actionAction, string transName)
            : base(fromState, toState, actionGuard, actionAction, transName)
        {
        }
        public DeferralTransition(State fromState, State toState, Func<bool> actionGuard, Action actionAction, string transName)
            : base(fromState, toState, actionGuard, actionAction, transName)
        {
        }
        public DeferralTransition(StateMachine sm, State fromState, State toState, string transName)
            : base(sm, fromState, toState, transName)
        {
        }

        public DeferralTransition(StateMachine sm, State fromState, State toState, MethodInfo actionGuard, MethodInfo actionAction, string transName)
            : base(sm, fromState, toState, actionGuard, actionAction, transName)
        {
        }
        public DeferralTransition(StateMachine sm, State fromState, State toState, Func<bool> actionGuard, Action actionAction, string transName)
            : base(sm, fromState, toState, actionGuard, actionAction, transName)
        {
        }
        internal override void GetElements(ref XmlDocument doc, ref XmlElement xStateMachine)
        {
            XmlElement xTrans = doc.CreateElement("Transition");
            xTrans.SetAttribute("Name", transName.ToString());
            xTrans.SetAttribute("Type", "Deferral");
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
