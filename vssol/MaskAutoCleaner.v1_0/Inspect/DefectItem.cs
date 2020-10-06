using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0
{
    public class DefectItem
    {
        public double X;
        public double Y;
        public EnumDefectType DefectType;
        public EnumMacMaskSide Side;

        public double Frame_Height;
        public int PurgeTimesLimit;
        public int PurgeCount = 0;
        public int InspectionTimes = 0;

        public EnumDefectStatus DefectStatus;



        public bool IsParticleClear = false;
        public bool DefectProcessEnd = false;

    }
}
