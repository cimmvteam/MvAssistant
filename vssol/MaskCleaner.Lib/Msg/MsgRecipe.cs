using MvLib.StateMachine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Msg
{
    public class MsgRecipe : MsgBase
    {

        public object TriggerRef;


        public MsgRecipe() { }
        public MsgRecipe(string name) { this.TransName = name; }



        //=== Static ==========================================================================================================


        public static implicit operator MsgRecipe(string d) { return new MsgRecipe(d); }

    }
}
