using MvAssistant.v0_2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Recipe
{
    [Serializable]
    public class MacRecipe : List<MacRecipeStep>
    {
        public String RecipeName;

        public MacRecipeParam Param;
        public EnumMacRecipeType RecipeType = EnumMacRecipeType.None;//未設定


        public MacRecipeStep AddStep(string name)
        {
            var step = new MacRecipeStep() { StepName = name };
            this.Add(step);
            return step;
        }

        public void SaveToXmlFile(string fn) { MvaUtil.SaveToXmlFile(this, fn); }




    }
}
