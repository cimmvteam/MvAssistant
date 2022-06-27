using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.Mac.Hal.CompPlc
{
    public interface IMacHalPlcMaskTransfer : IMacHalPlcBase
    {

        string CCDSpin(int SpinDegree);

        string Clamp(uint MaskType);

        string Initial();

        long ReadCCDSpinDegree();

        Tuple<double, double, double, double> ReadClampGripPos();

        Tuple<int, int, int> ReadClampTactile_BehindSide();

        Tuple<int, int, int> ReadClampTactile_FrontSide();

        Tuple<int, int, int, int, int, int> ReadClampTactile_LeftSide();

        Tuple<int, int, int, int, int, int> ReadClampTactile_RightSide();

        Tuple<int, int> ReadClampTactileLimit();

        Tuple<double, double, double, double, double, double> ReadHandInspection();

        Tuple<int, int, int> ReadLevel();

        Tuple<int, int, int> ReadLevelLimit();

        bool ReadMaskPresentVar();
        EnumMacPlcAssemblyStatus ReadMTStatus();

        Tuple<double, double, double, double, double, double> ReadSixAxisSensor();

        Tuple<double, double, double, double, double, double> ReadSixAxisSensorLowerLimit();

        Tuple<double, double, double, double, double, double> ReadSixAxisSensorUpperLimit();

        Tuple<double, long> ReadSpeedVar();

        double ReadStaticElec();

        Tuple<double, double> ReadStaticElecLimit();

        void RobotMoving(bool isMoving);

        void SetClampTactileLim(int? TactileLimit_Up, int? TactileLimit_Down);

        void SetLevelLimit(int? Level_X, int? Level_Y, int? Level_Z);

        void SetSixAxisSensorLowerLimit(double? Fx, double? Fy, double? Fz, double? Mx, double? My, double? Mz);

        void SetSixAxisSensorUpperLimit(double? Fx, double? Fy, double? Fz, double? Mx, double? My, double? Mz);

        void SetSpeedVar(double? ClampSpeed, long? CCDSpinSpeed);

        void SetStaticElecLimit(double? Maximum, double? Minimum);

        string Unclamp();
    }
}
