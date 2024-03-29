﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.Mac.Manifest.Exceptions
{
    public class MacGenCheckSumException : MacException
    {
        public MacGenCheckSumException()
        {
        }

        public MacGenCheckSumException(string message)
            : base(message)
        {
        }

        public MacGenCheckSumException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
