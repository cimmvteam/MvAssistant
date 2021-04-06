using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.Mac.Hal.CompPlc
{
    public interface IMacHalPlcInspectionCh : IMacHalComponent
    {
        string XYPosition(double? X_Position, double? Y_Position);

        string ZPosition(double Z_Position);

        string WPosition(double W_Position);

        string Initial();

        string ReadInspChStatus();

        bool SetRobotIntrude(bool isIntrude);

        bool ReadRobotIntruded();

        #region Set Parameter
        void SetSpeed(double? StageXYSpeed, double? CcdZSpeed, double? MaskWSpeed);

        void SetRobotAboutLimit(double? AboutLimit_L, double? AboutLimit_R);

        void SetRobotUpDownLimit(double? UpDownLimit_U, double? UpDownLimit_D);

        void SetParticleCntLimit(uint? L_Limit, uint? M_Limit, uint? S_Limit);

        void SetPressureDiffLimit(uint? GaugeLimit);
        #endregion

        #region Read Parameter
        Tuple<double, double, double> ReadSpeedSetting();

        Tuple<double, double> ReadRobotAboutLimitSetting();

        Tuple<double, double> ReadRobotUpDownLimitSetting();

        Tuple<int, int, int> ReadParticleCntLimitSetting();

        Tuple<int, int, int> ReadParticleCount();

        int ReadPressureDiffLimitSrtting();

        int ReadPressureDiff();
        #endregion

        #region Read Component Value
        Tuple<double, double> ReadXYPosition();

        double ReadZPosition();

        double ReadWPosition();

        double ReadRobotPosAbout();

        double ReadRobotPosUpDown();
        #endregion
    }
}
