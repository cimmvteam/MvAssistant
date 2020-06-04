using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.CompPlc
{
    public interface IMacHalPlcInspectionCh : IMacHalComponent
    {
        string XYPosition(double? X_Position, double? Y_Position);

        string ZPosition(double Z_Position);

        string WPosition(double W_Position);

        string Initial();

        string ReadInspChStatus();

        #region Set Parameter
        void SetSpeed(double? StageXYSpeed, double? CcdZSpeed, double? MaskWSpeed);

        void SetRobotAboutLimit(double? AboutLimit_L, double? AboutLimit_R);

        void SetRobotUpDownLimit(double? UpDownLimit_U, double? UpDownLimit_D);
        #endregion

        #region Read Parameter
        Tuple<double, double, double> ReadSpeedSetting();

        Tuple<double, double> ReadRobotAboutLimitSetting();

        Tuple<double, double> ReadRobotUpDownLimitSetting();
        #endregion

        #region Read Component Value
        bool ReadRobotIntrude(bool isIntrude);

        Tuple<double, double> ReadXYPosition();

        double ReadZPosition();

        double ReadWPosition();

        double ReadRobotPosAbout();

        double ReadRobotPosUpDown();
        #endregion
    }
}
