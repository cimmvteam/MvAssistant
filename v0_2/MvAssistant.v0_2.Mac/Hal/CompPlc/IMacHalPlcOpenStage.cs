using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.Mac.Hal.CompPlc
{
    public interface IMacHalPlcOpenStage : IMacHalComponent
    {
        string Open();

        string Close();

        string Clamp();

        string Unclamp();

        string SortClamp();

        string SortUnclamp();

        string Lock();

        string Vacuum(bool isSuck);

        string Initial();

        void SetBoxTypeVar(uint BoxType);

        int ReadBoxTypeVar();

        void SetSpeedVar(uint Speed);

        int ReadSpeedVar();

        Tuple<bool, bool> SetRobotIntrude(bool? isBTIntrude, bool? isMTIntrude);

        string ReadClampStatus();

        Tuple<long, long> ReadSortClampPosition();

        Tuple<long, long> ReadSliderPosition();

        Tuple<double, double> ReadCoverPos();

        void SetParticleCntLimit(uint? L_Limit, uint? M_Limit, uint? S_Limit);

        Tuple<int, int, int> ReadParticleCntLimit();

        Tuple<int, int, int> ReadParticleCount();

        Tuple<bool, bool> ReadCoverSensor();

        double ReadBoxDeform();

        double ReadWeightOnStage();

        bool ReadBoxExist();

        string ReadOpenStageStatus();

        Tuple<bool, bool> ReadRobotIntruded();
    }
}
