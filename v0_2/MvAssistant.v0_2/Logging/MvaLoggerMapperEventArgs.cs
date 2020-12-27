using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvAssistant.v0_2.Logging
{

    public class MvaLoggerMapperEventArgs : EventArgs
    {
        public MvaLogger Logger;
        public string LoggerId;
    }
}
