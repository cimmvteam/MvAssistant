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

        public string BTClamp(string BoxType, bool IsClamp)
        {
            var Result = "";
            var plc = this.m_PlcContext;
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
                switch (plc.Read<string>(MvEnumPlcVariable.BT_TO_PC_ClampCmd_Result))
                {
                    case "0":
                        Result = "Invalid";
                        break;
                    case "1":
                        Result = "OK";
                        break;
                    case "2":
                        Result = "Clamp no box type";
                        break;
                    case "3":
                        Result = "Tactile out range";
                        break;
                    case "4":
                        Result = "Motor error";
                        break;
                    case "5":
                        Result = "Please initial";
                        break;
                    case "6":
                        Result = "System not ready";
                        break;
                }
            }
            else
                throw new MvException("PLC connection fail");

            return Result;
        }
    }
}
