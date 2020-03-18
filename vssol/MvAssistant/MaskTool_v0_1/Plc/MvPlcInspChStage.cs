using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvAssistant.MaskTool_v0_1.Plc
{
    public class MvPlcInspChStage
    {

        MvPlcContext m_PlcContext;


        public MvPlcInspChStage(MvPlcContext plc)
        {
            this.m_PlcContext = plc;
        }


        public int MoveToXyz(float x, float y, float z)
        {
            var plc = this.m_PlcContext;

            plc.Write(MvEnumPlcVariable.ic_stage_pos_x, x);
            plc.Write(MvEnumPlcVariable.ic_stage_pos_y, y);
            plc.Write(MvEnumPlcVariable.ic_stage_pos_z, z);

            plc.Write(MvEnumPlcVariable.ic_stage_trigger, true);


            return 0;
        }




    }
}
