using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.Mac.Hal.CompPlc
{
    public interface IMacHalPlcInspectionCh : IMacHalPlcBase
    {
        string XYPosition(double? X_Position, double? Y_Position);

        string ZPosition(double Z_Position);

        string WPosition(double W_Position);

        string Initial();

        EnumMacPlcAssemblyStatus ReadICStatus();

        bool SetRobotIntrude(bool isIntrude);

        bool ReadRobotIntruded();

        #region Set Parameter
        void SetSpeedVar(double? StageXYSpeed, double? CcdZSpeed, double? MaskWSpeed);

        void SetRobotPosLeftRightLimit(double? AboutLimit_L, double? AboutLimit_R);

        void SetRobotPosUpDownLimit(double? UpDownLimit_U, double? UpDownLimit_D);

        void SetParticleCntLimit(uint? L_Limit, uint? M_Limit, uint? S_Limit);

        void SetChamberPressureDiffLimit(uint? GaugeLimit);
        #endregion

        #region Read Parameter
        Tuple<double, double, double> ReadSpeedVar();

        Tuple<double, double> ReadRobotPosLeftRightLimit();

        Tuple<double, double> ReadRobotPosUpDownLimit();

        Tuple<int, int, int> ReadParticleCntLimit();

        Tuple<int, int, int> ReadParticleCount();

        int ReadChamberPressureDiffLimit();

        int ReadChamberPressureDiff();
        #endregion

        #region Read Component Value
        Tuple<double, double> ReadXYPosition();

        double ReadZPosition();

        double ReadWPosition();

        double ReadRobotPosLeftRight();

        double ReadRobotPosUpDown();
        #endregion
    }
}
