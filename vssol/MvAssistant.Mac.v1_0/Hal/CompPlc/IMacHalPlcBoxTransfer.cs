using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.CompPlc
{
    public interface IMacHalPlcBoxTransfer : IMacHalComponent
    {
        string Clamp(uint BoxType);
        string Unclamp();
        string Initial();
        bool LevelReset();
        string ReadBTRobotStatus();
        void RobotMoving(bool isMoving);

        #region Set Parameter
        void SetSpeed(double ClampSpeed);
        void SetHandSpaceLimit(double? Minimum, double? Maximum);
        void SetClampToCabinetSpaceLimit(double Minimum);
        void SetLevelSensorLimit(double? Level_X, double? Level_Y);
        void SetSixAxisSensorUpperLimit(double? Fx, double? Fy, double? Fz, double? Mx, double? My, double? Mz);
        void SetSixAxisSensorLowerLimit(double? Fx, double? Fy, double? Fz, double? Mx, double? My, double? Mz);
        #endregion

        #region Read Parameter
        double ReadSpeedSetting();
        Tuple<double, double> ReadHandSpaceLimitSetting();
        double ReadClampToCabinetSpaceLimitSetting();
        Tuple<double, double> ReadLevelSensorLimitSetting();
        Tuple<double, double, double, double, double, double> ReadSixAxisSensorUpperLimitSetting();
        Tuple<double, double, double, double, double, double> ReadSixAxisSensorLowerLimitSetting();
        #endregion

        #region Read Component Value
        double ReadHandPos();
        bool ReadBoxDetect();
        double ReadHandPosByLSR();
        double ReadClampDistance();
        Tuple<double, double> ReadLevelSensor();
        Tuple<int, int, int, int, int, int> ReadSixAxisSensor();
        bool ReadHandVacuum();
        bool ReadBT_FrontLimitSenser();
        bool ReadBT_RearLimitSenser();
        #endregion
    }
}
