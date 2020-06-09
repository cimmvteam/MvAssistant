using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Xml;

namespace MaskAutoCleaner.StateMachine_v1_0
{
    /// <summary>
    /// State類別，名字在SM需要唯一
    /// </summary>
    public class State
    {
        public readonly string rt;
        public List<Transition> TransitionsInState = new List<Transition>(4);

        public Transition timeoutExecuteTransition { get; set; }
        public int TimeoutLimit { get; set; }
        #region Event Provide



        public event EventHandler<StateEntryEventArgs> OnEntry;
        //public string EntryName { get; private set; }
        public event EventHandler<StateExitEventArgs> OnExit;

        public virtual void DoExit(StateExitEventArgs args)
        {
            if (OnExit != null) OnExit(this, args);
        }

        public virtual void ExecDoEntry(StateEntryEventArgs seea)
        {
            StateMachine sm = seea.Sender;
            if (sm.FlageNormalStatus)
            {
                sm.ChangeState(this);
                DoEntry(seea);
            }
        }
        protected void DoEntry(StateEntryEventArgs seea)
        {
            if (OnEntry == null) return;
            //if (OnEntry.GetInvocationList().Count() > 1) throw new ArgumentException("不應有2個以上的OnEntry");

            this.OnEntry(this, seea);

            //foreach (var invo in OnEntry.GetInvocationList())
            //{
            //    var loopSeea = new StateEntryEventArgs(seea.sm);
            //    invo.DynamicInvoke(this, loopSeea);
            //}
        }
        #endregion

        #region 建構Function
        public State() { }
        public State(string stateName)
        {
            //EntryName = "";
            this.StateName = stateName;

        }
        public State(StateMachine sm, string stateName)
        {
            //EntryName = "";
            this.StateName = stateName;
            sm.AddState(this);

        }

        #endregion

        public virtual void GetElements(ref XmlDocument doc, ref XmlElement xStateMachine)
        {
            XmlElement xState = doc.CreateElement("State");
            xState.SetAttribute("Name", StateName.ToString());
            xState.SetAttribute("Type", "State");
            GetEntryElements(ref doc, ref xState);
            GetExitElements(ref doc, ref xState);
            xStateMachine.AppendChild(xState);
        }

        public virtual void GetEntryElements(ref XmlDocument doc, ref XmlElement xState)
        {
            if (OnEntry != null)
            {
                Delegate[] d = OnEntry.GetInvocationList();
                for (int i = 0; i < d.Count(); i++)
                {
                    string name = d[i].Target.GetType().ToString();
                    XmlElement xEntry = doc.CreateElement("Entry");
                    xEntry.SetAttribute("ClassName", name);
                    name = d[i].Method.Name;
                    xEntry.SetAttribute("EventName", name);
                    xState.AppendChild(xEntry);
                }
            }
        }
        public virtual void GetExitElements(ref XmlDocument doc, ref XmlElement xState)
        {
            if (OnExit != null)
            {
                Delegate[] d = OnExit.GetInvocationList();
                for (int i = 0; i < d.Count(); i++)
                {
                    string name = d[i].Target.GetType().ToString();
                    XmlElement xExit = doc.CreateElement("Exit");
                    xExit.SetAttribute("ClassName", name);
                    name = d[i].Method.Name;
                    xExit.SetAttribute("EventName", name);
                    xState.AppendChild(xExit);
                }
            }
        }
    }
}
