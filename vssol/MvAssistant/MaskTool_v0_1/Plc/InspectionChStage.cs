using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvAssistant.MaskTool_v0_1.Plc
{
    public class InspectionChStage
    {

        PlcDeviceContext m_PlcContext;

        public InspectionChStage() { }
        public InspectionChStage(PlcDeviceContext plc)
        {
            this.m_PlcContext = plc;
        }


        public int MoveToXyz(float x, float y, float z)
        {
            var plc = this.m_PlcContext;

            plc.Write(EnumPlcVariable.ic_stage_pos_x, x);
            plc.Write(EnumPlcVariable.ic_stage_pos_y, y);
            plc.Write(EnumPlcVariable.ic_stage_pos_z, z);

            plc.Write(EnumPlcVariable.ic_stage_trigger, true);


            return 0;
        }




    }
}
