using MvAssistant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Recipe
{
    public class MacRecipe : List<MacRecipeStep>
    {

        public MacRecipeParam Param;
        public EnumMacRecipeType RecipeType = EnumMacRecipeType.None;//未設定


        public MacRecipeStep AddStep(string name)
        {
            var step = new MacRecipeStep() { StepName = name };
            this.Add(step);
            return step;
        }

        public void SaveToXmlFile(string fn) { MvUtil.SaveToXmlFile(this, fn); }




    }
}
