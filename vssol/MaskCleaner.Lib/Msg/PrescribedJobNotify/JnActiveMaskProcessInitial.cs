using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Msg.PrescribedJobNotify
{
    public class JnActiveMaskProcessInitial : PrescribedJobNotifyBase
    {
        public EnumRecipeType RecipeType;
        public string LoadPortSource;
        public string DrawerSource;
        public string RecipeName;

        public JnActiveMaskProcessInitial(EnumRecipeType type, string loadport, string drawer, string recipeName)
        {
            this.RecipeType = type;
            this.LoadPortSource = loadport;
            this.DrawerSource = drawer;
            this.RecipeName = recipeName;
            
        }
    }
}
