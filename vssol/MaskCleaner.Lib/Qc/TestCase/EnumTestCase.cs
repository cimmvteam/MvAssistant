using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Qc.TestCase
{
    public enum TestCaseCategory
    {
        [Description("未指定")]
        NonSpecified = 0,

        [Description("App-Level WinService")]
        AppService = 10001,

        [Description("App-Level Main GUI")]
        AppMainGui = 10002,

        [Description("App-Level Recipe Editor")]
        AppRecipeEditor = 10003,

        [Description("App-Level State Machine Editor")]
        AppStateMachineEditor = 10004,

        [Description("App-Level State Machine Log Viewer")]
        AppStateMachineLogViewer = 10005,

        [Description("Framework-Level Recipe Agent")]
        FrameworkRecipeAgent = 20001,

        [Description("Framework-Level State Machine")]
        FrameworkStateMachine = 20002,

        [Description("Framework-Level Eqp Manger")]
        FrameworkEqpManager = 20003,

        [Description("Framework-Level SECS Manager")]
        FrameworkSecsManager = 20004,

        [Description("Framework-Level HAL & HDL")]
        FrameworkHalAndHdl = 20005,

        [Description("Function-Level Alarm Architecture")]
        FuncAlarm = 90001,
    }
}
