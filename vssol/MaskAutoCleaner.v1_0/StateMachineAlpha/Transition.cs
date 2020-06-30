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
    /// StateMachine的Transition實際作動層
    /// </summary>
    public class Transition
    {
        //private Func<bool> runGuard;
        //private Action<Object> runAct;
        protected MethodInfo runGuard;
        protected MethodInfo runAct;
        public State FromState { get; protected set; }
        public State ToState { get; protected set; }
        protected string transName;
        public string TransName
        {
            get { return transName; }
        }

        public virtual bool RunGuard(StateMachine sm)
        {

            try
            {
                return (bool)runGuard.Invoke(sm, null);
            }
            catch (Exception ex) { throw ex; }
        }
        public virtual bool RunTrigger(IStateParam param, StateMachine sm)
        {
            //if (!sm.CheckState(FromState))
            //{
            //    throw new NotCurrentStateException(sm.GetType().ToString() + " : current is " + sm.CurrentState.StateName + ",not Transition from: " + FromState.StateName);
            //}

            StateExitEventArgs seea = new StateExitEventArgs();
            try { FromState.DoExit(seea); }
            catch (Exception ex) { throw ex; }

            try { runAct.Invoke(sm, null); }
            catch (Exception ex) { throw ex; }

            StateEntryEventArgs senea = new StateEntryEventArgs(sm);
            try { ToState.ExecDoEntry(senea); }
            catch (Exception ex) { throw ex; }


            return true;
        }
        public virtual bool RunTrigger(string transName, StateMachine sm)
        {
            return RunTrigger(new StateParameter(transName), sm);
        }

        private void commonBuild(State fromState, State toState, string transName)
        {
            if (fromState == null)
            {
                throw new NullReferenceException("Reference FromState is null");
            }
            if (toState == null)
            {
                throw new NullReferenceException("Reference ToState is null");
            }
            // TODO: Complete member initialization
            this.FromState = fromState;
            FromState.TransitionsInState.Add(this);
            this.ToState = toState;
            this.transName = transName;
        }


        public bool IsInCurrentState(StateMachine sm)
        {
            return (sm.IsInCurrentState(FromState));
        }

        public Transition(State fromState, State toState, string transName)
        {
            commonBuild(fromState, toState, transName);
            runGuard = typeof(StateMachine).GetMethod("PassGuard");
            runAct = typeof(StateMachine).GetMethod("NoAction");
        }

        public Transition(StateMachine sm, State fromState, State toState, string transName)
        {
            commonBuild(fromState, toState, transName);
            runGuard = sm.GetType().GetMethod("PassGuard");
            runAct = sm.GetType().GetMethod("NoAction");
            sm.AddTransition(this);
        }

        public Transition(State fromState, State toState, MethodInfo actionGuard, MethodInfo actionAction, string transName)
        {
            commonBuild(fromState, toState, transName);
            if (actionGuard != null)
            {
                runGuard = actionGuard;
            }
            else
            {
                runGuard = typeof(StateMachine).GetMethod("PassGuard");
            }
            if (actionAction != null)
            {
                runAct = actionAction;
            }
            else
            {
                runAct = typeof(StateMachine).GetMethod("NoAction");
            }
        }

        public Transition(StateMachine sm, State fromState, State toState, MethodInfo actionGuard, MethodInfo actionAction, string transName)
        {
            commonBuild(fromState, toState, transName);
            if (actionGuard != null)
            {
                runGuard = actionGuard;
            }
            else
            {
                runGuard = typeof(StateMachine).GetMethod("PassGuard");
            }
            if (actionAction != null)
            {
                runAct = actionAction;
            }
            else
            {
                runAct = typeof(StateMachine).GetMethod("NoAction");
            }
            sm.AddTransition(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromState"></param>
        /// <param name="toState"></param>
        /// <param name="actionGuard"></param>
        /// <param name="actionAction">State machine action after guard condition</param>
        /// <param name="transName"></param>
        public Transition(State fromState, State toState, Func<bool> actionGuard, Action actionAction, string transName)
        {
            commonBuild(fromState, toState, transName);
            if (actionGuard != null)
            {
                runGuard = actionGuard.Method;
            }
            else
            {
                runGuard = typeof(StateMachine).GetMethod("PassGuard");
            }
            if (actionAction != null)
            {
                runAct = actionAction.Method;
            }
            else
            {
                runAct = typeof(StateMachine).GetMethod("NoAction");
            }
        }
        public Transition(StateMachine sm, State fromState, State toState, Func<bool> actionGuard, Action actionAction, string transName)
        {
            commonBuild(fromState, toState, transName);
            if (actionGuard != null)
            {
                runGuard = actionGuard.Method;
            }
            else
            {
                runGuard = typeof(StateMachine).GetMethod("PassGuard");
            }
            if (actionAction != null)
            {
                runAct = actionAction.Method;
            }
            else
            {
                runAct = typeof(StateMachine).GetMethod("NoAction");
            }
            this.transName = transName;
            sm.AddTransition(this);
        }

        internal virtual void GetElements(ref XmlDocument doc, ref XmlElement xStateMachine)
        {
            XmlElement xTrans = doc.CreateElement("Transition");
            xTrans.SetAttribute("Name", transName.ToString());
            xTrans.SetAttribute("Type", "Normal");
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
