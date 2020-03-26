using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MvAssistant.MaskTool_v0_1.Plc
{
    public class MvPlcMaskRobot
    {
        private MvPlcContext m_PlcContext;

        public MvPlcMaskRobot(MvPlcContext plc)
        {
            this.m_PlcContext = plc;
        }

        public string Initial()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            plc.Write(MvEnumPlcVariable.PC_TO_MT_Initial_A04, false);
            Thread.Sleep(100);
            plc.Write(MvEnumPlcVariable.PC_TO_MT_Initial_A04, true);

            if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.MT_TO_PC_Initial_A04_Reply), 1000))
                throw new MvException("Open Stage Initial T0 timeout");
            else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.MT_TO_PC_Initial_A04_Complete), 5000))
                throw new MvException("Open Stage Initial T2 timeout");

            plc.Write(MvEnumPlcVariable.PC_TO_MT_Initial_A04, false);

            if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.MT_TO_PC_Initial_A04_Complete), 1000))
                throw new MvException("Open Stage Initial T4 timeout");
            switch (plc.Read<int>(MvEnumPlcVariable.MT_TO_PC_Initial_A04_Result))
            {
                //case 0:
                //    Result = "Invalid";
                //    break;
                //case 1:
                //    Result = "Idle";
                //    break;
                //case 2:
                //    Result = "Busy";
                //    break;
                //case 3:
                //    Result = "Error";
                //    break;
            }
            return Result;
        }
    }
}
