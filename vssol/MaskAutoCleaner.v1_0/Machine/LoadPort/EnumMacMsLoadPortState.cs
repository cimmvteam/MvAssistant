using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.LoadPort
{
    /// <summary>狀態</summary>
    public enum  EnumMacMsLoadPortState
    {
        /** Postfix
         * 完成: Complete,
         * 無法完成: Failed
         */ 


        AlarmReset=0,
        AlarmResetComplete,
        AlarmResetFailed,

        Initial=100,
        InitialComplete,
        InitialFailed,

        IdleLoadOK=200,
        DockStart=201,
        ExecuteAlarmResetFirstWhenDocking,
        ExecuteInitialFirstWhenDocking,
        Docking,
        DockFailed,
        DockComplete,


        IdleReadyToUndock=300,
        UnDockStart =301,
        ExecuteAlarmResetFirstWhenUndocking,
        ExecuteInitialFirstWhenUndocking,
        Undocking,
        UndockFiled,
        UnDockComplete,
        IdleReadyToUnload,






    }
}
