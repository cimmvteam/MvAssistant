using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions
{
    /// <summary>State machine例外分類</summary>
    public enum EnumStateMAchineExceptionCategory
    {
        /// <summary>系統類</summary>
        System = 0,
        /// <summary>Drawer</summary>
        Drawer = 1,

        CleanCh = 2,
        /// <summary>Box Transfer</summary>
        BoxTransfer = 3,
        /// <summary>Mask Transfer</summary>
        MaskTransfer = 4,

        OpenStage = 5,

        InspectionCh = 6,
        /// <summary>Load port</summary>
        Loadport = 7,

    }
}
