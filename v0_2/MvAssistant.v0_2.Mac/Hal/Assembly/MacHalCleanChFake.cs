using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.Mac.Hal.Assembly
{
    [Guid("C19F5058-1CE7-47AD-810C-418746779F6A")]
    public class MacHalCleanChFake : MacHalAssemblyBase, IMacHalCleanCh
    {
        public Bitmap Camera_Cap()
        {
            Bitmap bmp = null;
            return bmp;
        }

        public void Camera_Insp_CapToSave(string SavePath, string FileType)
        {
            return;
        }

        public string SetGasValveTime(uint BlowTime)
        {
            return "OK";
        }

        public void LightForInspSetValue(int value)
        {
            return;
        }

        public int ReadLightForInsp()
        { return 1; }

        public float ReadGasValvePressure()
        {
            return (float)0.1;
        }

        public Tuple<bool, bool, bool, bool> ReadLightCurtain()
        {
            return new Tuple<bool, bool, bool, bool>(false, false, false, false);
        }

        public Tuple<double, double, double> ReadMaskLevel()
        {
            return new Tuple<double, double, double>(0, 0, 0);
        }

        public Tuple<int, int, int> ReadParticleCntLimit()
        {
            return new Tuple<int, int, int>(50, 50, 50);
        }

        public Tuple<int, int, int> ReadParticleCount()
        {
            return new Tuple<int, int, int>(20, 20, 20);
        }

        public double ReadManometerPressure()
        {
            return 0;
        }

        public double ReadGasValvePressureVar()
        {
            return 0;
        }

        public int ReadChamberPressureDiff()
        {
            return 1;
        }

        public int ReadManometerPressureLimit()
        {
            return 1;
        }

        public Tuple<double, double> ReadRobotPosLeftRightLimit()
        {
            return new Tuple<double, double>(1, 1);
        }

        public double ReadRobotPosLeftRight()
        {
            return 0;
        }

        public double ReadRobotPosUpDown()
        {
            return 0;
        }

        public Tuple<double, double> ReadRobotPosUpDownLimit()
        {
            return new Tuple<double, double>(1, 0);
        }

        public void SetParticleCntLimit(uint? L_Limit, uint? M_Limit, uint? S_Limit)
        {
            return;
        }

        public void SetGasValvePressurVar(double AirPressure)
        {
            return;
        }

        public void SetManometerPressureLimit(uint PressureLimit)
        {
            return;
        }

        public void SetRobotLeftRightLimit(double? Limit_L, double? Limit_R)
        {
            return;
        }

        public void SetRobotUpDownLimit(double? Limit_U, double? Limit_D)
        {
            return;
        }
    }
}
