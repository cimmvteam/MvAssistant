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

        void SetBoxType(uint BoxType);

        int ReadBoxTypeSetting();

        void SetSpeed(uint Speed);

        int ReadSpeedSetting();

        Tuple<bool, bool> ReadRobotIntrude(bool? isBTIntrude, bool? isMTIntrude);

        string ReadClampStatus();

        Tuple<long, long> ReadSortClampPosition();

        Tuple<long, long> ReadSliderPosition();

        Tuple<double, double> ReadCoverPos();

        void SetParticleCntLimit(uint? L_Limit, uint? M_Limit, uint? S_Limit);

        Tuple<int, int, int> ReadParticleCntLimitSetting();

        Tuple<int, int, int> ReadParticleCount();

        Tuple<bool, bool> ReadCoverSensor();

        double ReadBoxDeform();

        double ReadWeightOnStage();

        bool ReadBoxExist();

        string ReadOpenStageStatus();

        bool ReadBeenIntruded();
    }
}
