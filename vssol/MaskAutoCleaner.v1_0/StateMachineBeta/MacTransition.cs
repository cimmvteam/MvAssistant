using MaskAutoCleaner.v1_0.StateMachineExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineBeta
{
    public class MacTransition
    {
        public string Name;
        public MacTransition() { }
        public MacTransition(string name, MacState from, MacState to)
        {
            this.Name = name;
            this.StateFrom = from;
            this.StateTo = to;
        }

        public MacState StateFrom { get; protected set; }
        public MacState StateTo { get; protected set; }

        /// <summary>Trigger 所需的成員</summary>
        public TriggerMemberBase TriggerMembers{ get; private set; }
        /// <summary>設定Trigger 所需成員的函式</summary>
        /// <param name="triggerMembers"></param>
        public void SetTriggerMembers(TriggerMemberBase triggerMembers)
        {
            TriggerMembers = triggerMembers;
        }
    }

}
