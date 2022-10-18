using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.Mac.Hal.CompPlc
{
    public interface IMacHalPlcBoxTransfer : IMacHalPlcBase
    {
        string Clamp(uint BoxType);
        string Initial();

        bool LevelReset();

        EnumMacPlcAssemblyStatus ReadBTStatus();

        void RobotMoving(bool isMoving);

        string Unclamp();
        #region Set Parameter
        void SetClampToCabinetSpaceLimit(double Minimum);

        void SetHandSpaceLimit(double? Minimum, double? Maximum);

        void SetLevelSensorLimit(double? Level_X, double? Level_Y);

        void SetSixAxisSensorLowerLimit(double? Fx, double? Fy, double? Fz, double? Mx, double? My, double? Mz);

        void SetSixAxisSensorUpperLimit(double? Fx, double? Fy, double? Fz, double? Mx, double? My, double? Mz);

        void SetSpeedVar(double ClampSpeed);
        #endregion

        #region Read Parameter
        double ReadClampToCabinetSpaceLimitSetting();

        Tuple<double, double> ReadHandSpaceLimitSetting();

        Tuple<double, double> ReadLevelSensorLimitSetting();

        Tuple<double, double, double, double, double, double> ReadSixAxisSensorLowerLimit();

        Tuple<double, double, double, double, double, double> ReadSixAxisSensorUpperLimit();

        double ReadSpeedVar();
        #endregion

        #region Read Component Value
        bool ReadBoxDetect();

        bool ReadBT_FrontLimitSenser();

        bool ReadBT_RearLimitSenser();

        double ReadClampDistance();

        double ReadHandPos();
        double ReadHandPosByLSR();
        bool ReadHandVacuum();

        Tuple<double, double> ReadLevelSensor();
        Tuple<double, double, double, double, double, double> ReadSixAxisSensor();
        #endregion
    }
}
