using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.Cabinet
{
    public enum EnumMacMcDrawerCmd
    {
        Initial,
        Load_TrayGotoIn,
        Load_PutBox,
        Load_TrayGotoOut,
        Load_GetBox,
        Unload_TrayGotoOut,
        Unload_PutBox,
        Unload_TrayGotoIn,
        UnLoadPostWork
    }
}
