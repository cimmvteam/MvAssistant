using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MvAssistant.MaskTool_v0_1.Plc
{
    public class MvPlcBoxRobot
    {
        MvPlcContext m_PlcContext;

        public MvPlcBoxRobot(MvPlcContext plc)
        {
            this.m_PlcContext = plc;
        }

        public string BrClamp(char BoxType, bool IsClamp)
        {
            var plc = this.m_PlcContext;
            var Result = "";
            if (plc.IsConnected)
            {
                plc.Write(MvEnumPlcVariable.PC_TO_BT_Box_Type, BoxType);
                plc.Write(MvEnumPlcVariable.PC_TO_BT_Clamp, IsClamp);

                if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.BT_TO_PC_ClampCmd_Reply), 1000))
                    throw new MvException("Box Hand T0 timeout");
                else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.BT_TO_PC_ClampCmd_Complete), 5000))
                    throw new MvException("Box Hand T2 timeout");

                SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.PC_TO_BT_Clamp), 1000);

                if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.BT_TO_PC_ClampCmd_Reply), 1000))
                    throw new MvException("Box Hand T4 timeout");
                Result = plc.Read<string>(MvEnumPlcVariable.BT_TO_PC_ClampCmd_Result);
            }
            else
                throw new MvException("PLC connection fail");

            return Result;
        }
    }
}
