using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvAssistant.MaskTool_v0_1.Plc
{
    public class MvPlcOpenStage
    {
        private MvPlcContext m_PlcContext;

        public MvPlcOpenStage(MvPlcContext plc)
        {
            this.m_PlcContext = plc;
        }
        public string BoxOpen()//盒子開蓋的所有流程
        {
            string Result = "";

            return Result;
        }
        public string BoxClose()//盒子關蓋的所有流程
        {
            string Result = "";

            return Result;
        }
        public string BoxClamp()//盒蓋的夾爪
        {
            string Result = "";

            return Result;
        }
    }
}
