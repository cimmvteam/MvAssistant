﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace MvAssistant.v0_3.Mac.Hal.CompPlc
{
    [Guid("17793D56-17D7-4DFD-850B-EEB443CE9231")]
    public class MacHalPlcLoadPort : MacHalPlcBase, IMacHalPlcLoadPort
    {
    

        public MacHalPlcLoadPort()
        {

        }
        public MacHalPlcLoadPort(MacHalPlcContext plc = null)
        {
            this.plcContext = plc;
        }



        //設定壓差極限值
        public void SetPressureDiffLimit(uint? Gauge1Limit, uint? Gauge2Limit)
        {
            var plc = this.plcContext;

            if (Gauge1Limit != null)
                plc.Write(EnumMacHalPlcVariable.PC_TO_LP_DP1Limit, Gauge1Limit);
            if (Gauge2Limit != null)
                plc.Write(EnumMacHalPlcVariable.PC_TO_LP_DP2Limit, Gauge2Limit);
        }

        //讀取壓差極限值
        public Tuple<int, int> ReadChamberPressureDiffLimit()
        {
            var plc = this.plcContext;

            return new Tuple<int, int>(
                plc.Read<int>(EnumMacHalPlcVariable.PC_TO_LP_DP1Limit),
                plc.Read<int>(EnumMacHalPlcVariable.PC_TO_LP_DP2Limit)
                );
        }

        //讀取實際壓差
        public Tuple<int, int> ReadPressureDiff()
        {
            var plc = this.plcContext;

            return new Tuple<int, int>(
                plc.Read<int>(EnumMacHalPlcVariable.LP_TO_PC_DP1),
                plc.Read<int>(EnumMacHalPlcVariable.LP_TO_PC_DP2)
                );
        }

        public bool ReadLP_Light_Curtain()
        {
            var plc = this.plcContext;
            return plc.Read<bool>(EnumMacHalPlcVariable.PLC_TO_PC_LP_Light_Curtain);
        }

    }
}
