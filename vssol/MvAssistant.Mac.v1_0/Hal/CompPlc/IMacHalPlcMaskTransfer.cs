using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.CompPlc
{
    public interface IMacHalPlcMaskTransfer : IMacHalComponent
    {

        string Clamp(uint MaskType);

        string Unclamp();

        string Initial();

        void SetSpeed(double? ClampSpeed, int? CCDSpinSpeed);

        Tuple<double, int> ReadSpeedSetting();

        Tuple<double, double, double, double> ReadClampGripPos();

        string CCDSpin(int SpinDegree);

        long ReadCCDSpinDegree();

        void SetSixAxisSensorLimit(uint? Fx, uint? Fy, uint? Fz, uint? Mx, uint? My, uint? Mz);

        Tuple<int, int, int, int, int, int> ReadSixAxisSensorLimitSetting();

        Tuple<int, int, int, int, int, int> ReadSixAxisSensor();

        void SetClampTactileLim(int? TactileLimit_Up, int? TactileLimit_Down);

        Tuple<int, int> ReadClampTactileLimSetting();

        Tuple<int, int, int> ReadClampTactile_FrontSide();

        Tuple<int, int, int> ReadClampTactile_BehindSide();

        Tuple<int, int, int, int, int, int> ReadClampTactile_LeftSide();

        Tuple<int, int, int, int, int, int> ReadClampTactile_RightSide();

        void SetLevelLimit(int? Level_X, int? Level_Y, int? Level_Z);

        Tuple<int, int, int> ReadLevelLimitSetting();

        Tuple<int, int, int> ReadLevel();

        void SetStaticElecLimit(double? Maximum, double? Minimum);

        Tuple<double, double> ReadStaticElecLimitSetting();

        double ReadStaticElec();

        string ReadMTRobotStatus();

        Tuple<double, double, double, double, double, double> ReadHandInspection();

        void RobotMoving(bool isMoving);
    }
}
