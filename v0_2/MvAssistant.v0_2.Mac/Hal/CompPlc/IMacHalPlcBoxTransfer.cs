using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.Mac.Hal.CompPlc
{
    public interface IMacHalPlcBoxTransfer : IMacHalPlcBase
    {
        string Clamp(uint BoxType);
        string Unclamp();
        string Initial();
        bool LevelReset();
        string ReadBTRobotStatus();
        void RobotMoving(bool isMoving);

        #region Set Parameter
        void SetSpeedVar(double ClampSpeed);
        void SetHandSpaceLimit(double? Minimum, double? Maximum);
        void SetClampToCabinetSpaceLimit(double Minimum);
        void SetLevelSensorLimit(double? Level_X, double? Level_Y);
        void SetSixAxisSensorUpperLimit(double? Fx, double? Fy, double? Fz, double? Mx, double? My, double? Mz);
        void SetSixAxisSensorLowerLimit(double? Fx, double? Fy, double? Fz, double? Mx, double? My, double? Mz);
        #endregion

        #region Read Parameter
        double ReadSpeedVar();
        Tuple<double, double> ReadHandSpaceLimitSetting();
        double ReadClampToCabinetSpaceLimitSetting();
        Tuple<double, double> ReadLevelSensorLimitSetting();
        Tuple<double, double, double, double, double, double> ReadSixAxisSensorUpperLimit();
        Tuple<double, double, double, double, double, double> ReadSixAxisSensorLowerLimit();
        #endregion

        #region Read Component Value
        double ReadHandPos();
        bool ReadBoxDetect();
        double ReadHandPosByLSR();
        double ReadClampDistance();
        Tuple<double, double> ReadLevelSensor();
        Tuple<double, double, double, double, double, double> ReadSixAxisSensor();
        bool ReadHandVacuum();
        bool ReadBT_FrontLimitSenser();
        bool ReadBT_RearLimitSenser();
        #endregion
    }
}
