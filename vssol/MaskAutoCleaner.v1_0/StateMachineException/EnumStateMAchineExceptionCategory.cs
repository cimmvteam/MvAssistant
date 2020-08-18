using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineException
{
    /// <summary>State machine例外分類</summary>
    public enum EnumStateMAchineExceptionCategory
    {
        /// <summary>系統類</summary>
        System = 0,
        /// <summary>Mask Transfer</summary>
        MaskTransfer = 1,
        /// <summary>Box Transfer</summary>
        BoxTransfer = 2,
        /// <summary>Load port</summary>
        Loadport = 3,
        /// <summary>Drawer</summary>
        Drawer = 4,
    }
}
