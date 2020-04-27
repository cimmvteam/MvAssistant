using MvAssistant.Mac.v1_0.Hal.Component;
using MvAssistant.Mac.v1_0.Hal.Component.Stage;
using MvAssistant.Mac.v1_0.Hal.CompPlc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace MvAssistant.Mac.v1_0.Hal.Component.Stage
{
    [GuidAttribute("04BCF46C-83FF-49FA-A13B-10708C48B57F")]
    public class HalStageHirata : MacHalComponentBase, IHalInspectionStage
    {



        MacHalPlcContext plcContext;



       public bool HalMoveIsComplete()
        {
            return true;
        }
        bool IHalInspectionStage.HalMoveIsComplete()
        {
            throw new NotImplementedException();
        }

        bool IHalInspectionStage.HalMoveRel(HalStageMotion p)
        {
            
            throw new NotImplementedException();
        }

        public bool HalMoveAbs(HalStageMotion p)
        {
            throw new NotImplementedException();
        }


     

        public int HalConnect()
        {
            throw new NotImplementedException();
        }

        public int HalClose()
        {
            throw new NotImplementedException();
        }

        public bool HalIsConnected()
        {
            throw new NotImplementedException();
        }
    }
}
