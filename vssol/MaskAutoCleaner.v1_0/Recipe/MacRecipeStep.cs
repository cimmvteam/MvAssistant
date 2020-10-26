using MvAssistant;
using MvAssistant.Mac.v1_0;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MaskAutoCleaner.v1_0.Recipe
{
    public class MacRecipeStep
    {
        [XmlAttribute]
        public string StepName;
        public List<MacRecipeMachineState> StatesBefore
        {
            get { return this.m_statesBefore.Select(x => new MacRecipeMachineState(x.Key, x.Value)).ToList(); }
            set { this.m_statesBefore = value.ToDictionary(x => x.Key, x => x.Value); }
        }
        public List<MacRecipeMachineState> StatesAfter
        {
            get { return this.m_statesAfter.Select(x => new MacRecipeMachineState(x.Key, x.Value)).ToList(); }
            set { this.m_statesAfter = value.ToDictionary(x => x.Key, x => x.Value); }
        }

        public List<MacRecipeMachineState> StatesCmd
        {
            get { return this.m_statesAfter.Select(x => new MacRecipeMachineState(x.Key, x.Value)).ToList(); }
            set { this.m_statesAfter = value.ToDictionary(x => x.Key, x => x.Value); }
        }


        Dictionary<string, string> m_statesBefore = new Dictionary<string, string>();
        Dictionary<string, string> m_statesAfter = new Dictionary<string, string>();
        Dictionary<string, string> m_statesCmd = new Dictionary<string, string>();



        public void AddBeforeState(Enum mid, Enum state)
        {
            if (this.m_statesBefore.ContainsKey(mid.ToString())) throw new MacException("Exist machine id");
            this.m_statesBefore[mid.ToString()] = state.ToString();
        }
        public void AddAfterState(Enum mid, Enum state)
        {
            if (this.m_statesAfter.ContainsKey(mid.ToString())) throw new MacException("Exist machine id");
            this.m_statesAfter[mid.ToString()] = state.ToString();
        }



    }
}
