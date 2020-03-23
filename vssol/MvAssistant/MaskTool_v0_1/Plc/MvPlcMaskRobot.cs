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

            return Result;
        }
    }
}
