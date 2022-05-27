using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.Mac.Hal.CompPlc
{
    public interface IMacHalPlcOpenStage : IMacHalPlcBase
    {
        string Clamp();

        string Close();

        string Initial();

        string Lock();

        string Open();
        double ReadBoxDeform();

        bool ReadBoxExist();

        int ReadBoxTypeVar();

        string ReadClampStatus();

        Tuple<double, double> ReadCoverPos();

        Tuple<bool, bool> ReadCoverSensor();

        EnumMacPlcAssemblyStatus ReadOSStatus();

        Tuple<int, int, int> ReadParticleCntLimit();

        Tuple<int, int, int> ReadParticleCount();

        Tuple<bool, bool> ReadRobotIntruded();

        Tuple<long, long> ReadSliderPosition();

        Tuple<long, long> ReadSortClampPosition();

        int ReadSpeedVar();

        double ReadWeightOnStage();

        void SetBoxTypeVar(uint BoxType);

        void SetParticleCntLimit(uint? L_Limit, uint? M_Limit, uint? S_Limit);

        Tuple<bool, bool> SetRobotIntrude(bool? isBTIntrude, bool? isMTIntrude);

        void SetSpeedVar(uint Speed);

        string SortClamp();

        string SortUnclamp();

        string Unclamp();
        string Vacuum(bool isSuck);
    }
}
