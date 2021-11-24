using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.Mac.Hal.CompPlc
{
    public interface IMacHalPlcMaskTransfer : IMacHalPlcBase
    {

        string Clamp(uint MaskType);

        string Unclamp();

        string Initial();

        void SetSpeedVar(double? ClampSpeed, long? CCDSpinSpeed);

        Tuple<double, long> ReadSpeedVar();

        Tuple<double, double, double, double> ReadClampGripPos();

        string CCDSpin(int SpinDegree);

        long ReadCCDSpinDegree();

        void SetSixAxisSensorUpperLimit(double? Fx, double? Fy, double? Fz, double? Mx, double? My, double? Mz);

        void SetSixAxisSensorLowerLimit(double? Fx, double? Fy, double? Fz, double? Mx, double? My, double? Mz);

        Tuple<double, double, double, double, double, double> ReadSixAxisSensorUpperLimit();

        Tuple<double, double, double, double, double, double> ReadSixAxisSensorLowerLimit();

        Tuple<double, double, double, double, double, double> ReadSixAxisSensor();

        void SetClampTactileLim(int? TactileLimit_Up, int? TactileLimit_Down);

        Tuple<int, int> ReadClampTactileLimit();

        Tuple<int, int, int> ReadClampTactile_FrontSide();

        Tuple<int, int, int> ReadClampTactile_BehindSide();

        Tuple<int, int, int, int, int, int> ReadClampTactile_LeftSide();

        Tuple<int, int, int, int, int, int> ReadClampTactile_RightSide();

        void SetLevelLimit(int? Level_X, int? Level_Y, int? Level_Z);

        Tuple<int, int, int> ReadLevelLimit();

        Tuple<int, int, int> ReadLevel();

        void SetStaticElecLimit(double? Maximum, double? Minimum);

        Tuple<double, double> ReadStaticElecLimit();

        double ReadStaticElec();

        string ReadMTRobotStatus();

        Tuple<double, double, double, double, double, double> ReadHandInspection();

        void RobotMoving(bool isMoving);
    }
}
