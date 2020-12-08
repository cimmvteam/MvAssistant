using MvAssistant.v0_2;
using MvAssistant.v0_2.Mac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MaskAutoCleaner.v1_0.Recipe
{
    [Serializable]
    public class MacRecipeStep
    {
        [XmlAttribute]
        public string StepName;
        public List<MacRecipeMachineState> StatesBefore = new List<MacRecipeMachineState>();
        public List<MacRecipeMachineState> StatesAfter = new List<MacRecipeMachineState>();
        public List<MacRecipeMachineState> StatesCmd = new List<MacRecipeMachineState>();
        //{
        //    get { return this.m_statesCmd.Select(x => new MacRecipeMachineState(x.Key, x.Value)).ToList(); }
        //    set { this.m_statesCmd = value.ToDictionary(x => x.Key == null ? "" : x.Key, x => x.Value); }
        //}


        //Dictionary<string, string> m_statesBefore = new Dictionary<string, string>();
        //Dictionary<string, string> m_statesAfter = new Dictionary<string, string>();
        //Dictionary<string, string> m_statesCmd = new Dictionary<string, string>();



        public void AddBeforeState(Enum mid, Enum state)
        {
            var obj = this.StatesBefore.Where(x => x.Key == mid.ToString()).FirstOrDefault();
            if (obj != null) throw new MacException("Exist machine id");
            this.StatesBefore.Add(new MacRecipeMachineState(mid, state));
        }
        public void AddAfterState(Enum mid, Enum state)
        {
            var obj = this.StatesAfter.Where(x => x.Key == mid.ToString()).FirstOrDefault();
            if (obj != null) throw new MacException("Exist machine id");
            this.StatesAfter.Add(new MacRecipeMachineState(mid, state));
        }

        public void AddCmd(Enum mid, Enum command)
        {
            var obj = this.StatesCmd.Where(x => x.Key == mid.ToString()).FirstOrDefault();
            if (obj != null) throw new MacException("Exist machine id");
            this.StatesCmd.Add(new MacRecipeMachineState(mid, command));
        }


    }
}
