using MaskAutoCleaner.v1_0.StateMachineExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineBeta
{
    public class MacState
    {
        public string Name;

        public MacState() { }
        public MacState(string name) { this.Name = name; }


        #region Event Provide

        public event EventHandler<MacStateEntryEventArgs> OnEntry;
        public event EventHandler<MacStateExitEventArgs> OnExit;

        public virtual void DoExit(MacStateExitEventArgs args)
        {
            if (OnExit == null) return;
            this.OnExit(this, args);
        }

        public void DoEntry(MacStateEntryEventArgs seea)
        {

            if (OnEntry == null) return;
            this.OnEntry(this, seea);
        }

        [Obsolete]
        public void ExecuteCommand(MacStateEntryEventArgs seea)
        {
               DoEntry(seea);
        }

        public void ExecuteCommandAtEntry(MacStateEntryEventArgs seea)
        {
            DoEntry(seea);
        }

        public void ExecuteCommandAtExit(MacTransition transition ,MacStateExitEventArgs exitEventArgs,MacStateEntryEventArgs entryEventArgs)
        {
            var stateTo = transition.StateTo;
            DoExit(exitEventArgs);
            stateTo.DoEntry(entryEventArgs);
        }
       

        #endregion

        /// <summary>
        /// <para>是不是 StateMachine 發出的例外</para>
        /// <para>true: 是</para><para>false: 不是</para>
        /// <para>null: 没有列外</para>
        /// </summary>
        public bool? IsStateMachineException
        {
            get
            {
                if (StateException == null)
                {
                    return default(bool?);
                }
                else if (StateException.GetType().IsSubclassOf(typeof(StateMachineExceptionBase)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
          }
        /// <summary>發出的例外</summary>
        public Exception StateException { get; set; }
        /// <summary>清除例外資料</summary>
        public void ClearException()
        {
            StateException = null;
        }
        /// <summary>設定例外物件</summary>
        /// <param name="ex">例外的物件</param>
        public void SetException(Exception ex){ this.StateException = ex; }
    }
}
