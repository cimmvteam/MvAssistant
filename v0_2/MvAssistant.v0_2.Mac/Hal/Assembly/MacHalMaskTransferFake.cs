using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MvAssistant.v0_2.Mac.Hal.CompRobot;

namespace MvAssistant.v0_2.Mac.Hal.Assembly
{
    [Guid("2CB5E7D6-970D-4550-8821-E4DAB03324EF")]
    public class MacHalMaskTransferFake : MacHalAssemblyBase, IMacHalMaskTransfer
    {
        public int BacktrackPathMove(string PathFileLocation)
        {
            return 0;
        }

        public string CCDSpin(int SpinDegree)
        {
            return "OK";
        }

        public void ChangeDirection(string EndPosFileLocation)
        {
            return;
        }

        public bool CheckPosition(string PosFileLocation)
        {
            return true;
        }

        public string Clamp(uint MaskType)
        {
            return "OK";
        }

        public int ExePathMove(string PathFileLocation)
        {
            return 0;
        }

        public int ExePathMove(List<HalRobotMotion> MovePosition)
        {
            return 0;
        }

        public void Initial()
        {
            return;
        }

        public long ReadCCDSpinDegree()
        {
            return 0;
        }

        public Tuple<double, double, double, double> ReadClampGripPos()
        {
            return new Tuple<double, double, double, double>(0, 0, 0, 0);
        }

        public Tuple<int, int> ReadClampTactileLimSetting()
        {
            return new Tuple<int, int>(50, 0);
        }

        public Tuple<int, int, int> ReadClampTactile_BehindSide()
        {
            return new Tuple<int, int, int>(1,1,1);
        }

        public Tuple<int, int, int> ReadClampTactile_FrontSide()
        {
            return new Tuple<int, int, int>(1,1,1);
        }

        public Tuple<int, int, int, int, int, int> ReadClampTactile_LeftSide()
        {
            return new Tuple<int, int, int, int, int, int>(1,1,1,1,1,1);
        }

        public Tuple<int, int, int, int, int, int> ReadClampTactile_RightSide()
        {
            return new Tuple<int, int, int, int, int, int>(1,1,1,1,1,1);
        }

        public List<HalRobotMotion> ReadFilePosition(string PathFileLocation)
        {
            return new List<HalRobotMotion>();
        }

        public Tuple<double, double, double, double, double, double> ReadHandInspection()
        {
            return new Tuple<double, double, double, double, double, double>(0,0,0,0,0,0);
        }

        public Tuple<int, int, int> ReadLevel()
        {
            return new Tuple<int, int, int>(0,0,0);
        }

        public Tuple<int, int, int> ReadLevelLimitSetting()
        {
            return new Tuple<int, int, int>(0,0,0);
        }

        public string ReadMTRobotStatus()
        {
            return "";
        }

        public Tuple<double, double, double, double, double, double> ReadSixAxisSensor()
        {
            return new Tuple<double, double, double, double, double, double>(1,1,1,1,1,1);
        }

        public Tuple<double, double, double, double, double, double> ReadSixAxisSensorLowerLimitSetting()
        {
            return new Tuple<double, double, double, double, double, double>(0,0,0,0,0,0);
        }

        public Tuple<double, double, double, double, double, double> ReadSixAxisSensorUpperLimitSetting()
        {
            return new Tuple<double, double, double, double, double, double>(10,10,10,10,10,10);
        }

        public Tuple<double, long> ReadSpeedSetting()
        {
            return new Tuple<double, long>(1,1);
        }

        public double ReadStaticElec()
        {
            return 0;
        }

        public Tuple<double, double> ReadStaticElecLimitSetting()
        {
            return new Tuple<double, double>(1,0);
        }

        public void Recover()
        {
            return;
        }

        public void Reset()
        {
            return;
        }

        public void RobotMoving(bool isMoving)
        {
            return;
        }

        public void SetClampTactileLim(int? TactileMaxLimit, int? TactileMinLimit)
        {
            return;
        }

        public void SetLevelLimit(int? Level_X, int? Level_Y, int? Level_Z)
        {
            return;
        }

        public void SetSixAxisSensorLowerLimit(double? Fx, double? Fy, double? Fz, double? Mx, double? My, double? Mz)
        {
            return;
        }

        public void SetSixAxisSensorUpperLimit(double? Fx, double? Fy, double? Fz, double? Mx, double? My, double? Mz)
        {
            return;
        }

        public void SetSpeed(double? ClampSpeed, long? CCDSpinSpeed)
        {
            return;
        }

        public void SetStaticElecLimit(double? Maximum, double? Minimum)
        {
            return;
        }

        public string Unclamp()
        {
            return "OK";
        }
    }
}
