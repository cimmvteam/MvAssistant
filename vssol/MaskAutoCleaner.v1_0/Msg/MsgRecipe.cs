using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Msg
{
    public class MsgRecipe : MsgBase
    {

        public object TriggerRef;

        public MsgRecipe() { }



        //=== Static ==========================================================================================================


        //public static implicit operator MsgRecipe(string d) { return new MsgRecipe(d); }

    }
}
