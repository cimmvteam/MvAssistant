//using Microsoft.Analytics.Interfaces;
//using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MaskAutoCleaner.v1_0.Machine.CabinetDrawer
{
    public enum  EnumMacMcCabinetDrawerCmd
    {
        SystemBootup,
        SystemBootupInitial,
        Load_MoveTrayToOut,
        Load_MoveTrayToHome,
        Load_MoveTrayToIn,
    }
}