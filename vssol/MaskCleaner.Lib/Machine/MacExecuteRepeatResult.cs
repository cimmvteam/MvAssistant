using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Machine
{
    public class MacExecuteRepeatResult
    {

        public bool IsKeepRepeat = true;

        //millisecond
        public int NextExecuteInterval = 50;


        public static implicit operator MacExecuteRepeatResult(bool val) { return new MacExecuteRepeatResult() { IsKeepRepeat = val }; }

    }
}
