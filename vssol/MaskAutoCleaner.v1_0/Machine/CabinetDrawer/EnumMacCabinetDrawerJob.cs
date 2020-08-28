using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.CabinetDrawer
{
    /// <summary>Drawer 的工作</summary>
    public enum EnumMacCabinetDrawerJob
    {
        /// <summary>初Control Console開機時還没指定工作</summary>
        None,
        /// <summary>Control Console 開機後 Initial</summary>
        FirstInitial,
        /// <summary>等待 Load</summary>
        WaitingLoad,
        /// <summary>Load</summary>
        Load,
        /// <summary>等待 Unload</summary>
        WaitingUnload,
        /// <summary>Unload</summary>
        Unload
    }
}
