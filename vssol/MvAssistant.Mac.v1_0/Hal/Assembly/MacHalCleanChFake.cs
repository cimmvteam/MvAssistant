using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Assembly
{
    [Guid("C19F5058-1CE7-47AD-810C-418746779F6A")]
    public class MacHalCleanChFake : MacHalFakeComponentBase, IMacHalCleanCh
    {
        public Bitmap Camera_Cap()
        {
            Bitmap bmp = null;
            return bmp;
        }

        public void Camera_SideInsp_CapToSave(string SavePath, string FileType)
        {
            return;
        }

        public string GasValveBlow(uint BlowTime)
        {
            return "OK";
        }

        public void LightSetValue(int value)
        {
            return;
        }

        public float ReadBlowPressure()
        {
            return (float)0.1;
        }

        public Tuple<bool, bool, bool> ReadLightCurtain()
        {
            return new Tuple<bool, bool, bool>(false, false, false);
        }

        public Tuple<double, double, double> ReadMaskLevel()
        {
            return new Tuple<double, double, double>(0, 0, 0);
        }

        public Tuple<int, int, int> ReadParticleCntLimitSetting()
        {
            return new Tuple<int, int, int>(50, 50, 50);
        }

        public Tuple<int, int, int> ReadParticleCount()
        {
            return new Tuple<int, int, int>(20, 20, 20);
        }

        public double ReadPressure()
        {
            return 0;
        }

        public double ReadPressureCtrlSetting()
        {
            return 0;
        }

        public int ReadPressureDiff()
        {
            return 1;
        }

        public int ReadPressureDiffLimitSetting()
        {
            return 1;
        }

        public Tuple<double, double> ReadRobotAboutLimitSetting()
        {
            return new Tuple<double, double>(1, 1);
        }

        public double ReadRobotPosAbout()
        {
            return 0;
        }

        public double ReadRobotPosUpDown()
        {
            return 0;
        }

        public Tuple<double, double> ReadRobotUpDownLimitSetting()
        {
            return new Tuple<double, double>(1,0);
        }

        public void SetParticleCntLimit(uint? L_Limit, uint? M_Limit, uint? S_Limit)
        {
            return;
        }

        public void SetPressureCtrl(double AirPressure)
        {
            return;
        }

        public void SetPressureDiffLimit(uint PressureLimit)
        {
            return;
        }

        public void SetRobotAboutLimit(double? Limit_L, double? Limit_R)
        {
            return;
        }

        public void SetRobotUpDownLimit(double? Limit_U, double? Limit_D)
        {
            return;
        }
    }
}
