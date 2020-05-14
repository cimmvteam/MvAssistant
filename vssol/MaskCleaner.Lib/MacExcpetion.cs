using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner
{
    public class MacExcpetion : Exception
    {

        public MacExcpetion() { }
        public MacExcpetion(string message) : base(message) { }
        public MacExcpetion(string message, Exception innerException) : base(message,innerException) { }

    }

}
