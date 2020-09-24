using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Recipe
{
    public class MacRecipeMgr
    {

        //只有一個Recipe, 先求有, 再求好, 之後再追加 Multi-Recipe Run
        public MacRecipe Recipe;




        public void LoaddRecipe(string recipeName)
        {

        }


        public void Execute()
        {

            foreach(var step in this.Recipe)
            {


            }

            
        }





        bool CheckBeforeStates(MacRecipeStep step)
        {





            return true;
        }


    }
}
