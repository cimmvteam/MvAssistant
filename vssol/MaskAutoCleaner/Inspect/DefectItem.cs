using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner
{
    public class DefectItem
    {
        public double X;
        public double Y;
        public EnumDefectType DefectType;
        public MacEnumMaskSide Side;

        public double Frame_Height;
        public int PurgeTimesLimit;
        public int PurgeCount = 0;
        public int InspectionTimes = 0;

        public EnumDefectStatus DefectStatus;



        public bool IsParticleClear = false;
        public bool DefectProcessEnd = false;

    }
}
