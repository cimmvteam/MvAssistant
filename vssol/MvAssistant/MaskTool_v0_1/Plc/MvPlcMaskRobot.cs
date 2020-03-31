using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvAssistant.MaskTool_v0_1.Plc
{
    public class MvPlcMaskRobot
    {
        private MvPlcContext m_PlcContext;

        public MvPlcMaskRobot(MvPlcContext plc)
        {
            this.m_PlcContext = plc;
        }
        public string MTClamp(string MaskType, bool IsClamp)
        {
            string Result = "";
<<<<<<< HEAD
            var plc = m_PlcContext;
            if (plc.IsConnected)
            {

            }
            else
                throw new MvException("PLC connection fail");
            return Result;
        }
        public string MTClampCheck()//檢查MaskClamp目前夾距、夾取角度及各項狀態與設定值是否吻合
        {
            string Result = "";

            return Result;
        }
        public string MaskCheck()//檢查夾爪有無夾取Mask
        {
            string Result = "";

=======
            var plc = this.m_PlcContext;
            try
            {
                plc.Write(MvEnumPlcVariable.PC_TO_MT_Initial_A04, false);
                Thread.Sleep(100);
                plc.Write(MvEnumPlcVariable.PC_TO_MT_Initial_A04, true);

                if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.MT_TO_PC_Initial_A04_Reply), 1000))
                    throw new MvException("Open Stage Initial T0 timeout");
                else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.MT_TO_PC_Initial_A04_Complete), 5000))
                    throw new MvException("Open Stage Initial T2 timeout");

                switch (plc.Read<int>(MvEnumPlcVariable.MT_TO_PC_Initial_A04_Result))
                {
                    case 0:
                        Result = "Invalid";
                        break;
                    case 1:
                        Result = "Idle";
                        break;
                    case 2:
                        Result = "Busy";
                        break;
                    case 3:
                        Result = "Error";
                        break;
                }

                plc.Write(MvEnumPlcVariable.PC_TO_MT_Initial_A04, false);

                if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.MT_TO_PC_Initial_A04_Complete), 1000))
                    throw new MvException("Open Stage Initial T4 timeout");
            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                plc.Write(MvEnumPlcVariable.PC_TO_MT_Initial_A04, false);
            }
            
>>>>>>> 7d76a0e8abe70a0aea1c750ff12f3c502bf5a150
            return Result;
        }

        //將夾爪伸到LoadPort，讀取感測器的值，確認夾爪有無變形
        public Tuple<double, double, double, double, double, double> ReadHandInspection()
        {
            var plc = this.m_PlcContext;
            return new Tuple<double, double, double, double, double, double>(
            plc.Read<double>(MvEnumPlcVariable.LD_TO_PC_Laser1),
            plc.Read<double>(MvEnumPlcVariable.LD_TO_PC_Laser2),
            plc.Read<double>(MvEnumPlcVariable.LD_TO_PC_Laser3),
            plc.Read<double>(MvEnumPlcVariable.LD_TO_PC_Laser4),
            plc.Read<double>(MvEnumPlcVariable.LD_TO_PC_Laser5),
            plc.Read<double>(MvEnumPlcVariable.LD_TO_PC_Laser6)
            );
        }
    }
       
}
