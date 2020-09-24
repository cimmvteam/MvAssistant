using MvAssistant.Mac.v1_0;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Recipe
{
    public class MacRecipeStep
    {

        public string StepName;

        public Dictionary<string, string> StatesBefore = new Dictionary<string, string>();
        public Dictionary<string, string> StatesAfter = new Dictionary<string, string>();



        public void AddBeforeState(Enum mid, Enum state)
        {
            if (this. StatesBefore.ContainsKey(mid.ToString())) throw new MacException("Exist machine id");
            this.StatesBefore[mid.ToString()] = state.ToString();
        }
        public void AddAfterState(Enum mid, Enum state)
        {
            if (this.StatesAfter.ContainsKey(mid.ToString())) throw new MacException("Exist machine id");
            this.StatesAfter[mid.ToString()] = state.ToString();
        }



    }
}
