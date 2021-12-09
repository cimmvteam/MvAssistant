using MvAssistant.v0_2.Mac.Hal.CompPlc;
using MvAssistant.v0_2.Mac.Manifest;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.Mac.Hal.Assembly
{
    [Guid("18DB892D-D5A9-42B7-B101-CA70EF238753")]
    public class MacHalInspectionChFake : MacHalAssemblyBase, IMacHalInspectionCh
    {
        public IMacHalPlcInspectionCh Plc { get { return (IMacHalPlcInspectionCh)this.GetHalDevice(EnumMacDeviceId.inspectionch_plc); } }
        public Bitmap Camera_SideDfs_Cap()
        {
            Bitmap bmp = null;
            return bmp;
        }

        public void Camera_SideDfs_CapToSave(string SavePath, string FileType)
        {
            return;
        }

        public Bitmap Camera_SideInsp_Cap()
        {
            Bitmap bmp = null;
            return bmp;
        }

        public void Camera_SideInsp_CapToSave(string SavePath, string FileType)
        {
            return;
        }

        public Bitmap Camera_TopDfs_Cap()
        {
            Bitmap bmp = null;
            return bmp;
        }

        public void Camera_TopDfs_CapToSave(string SavePath, string FileType)
        {
            return;
        }

        public Bitmap Camera_TopInsp_Cap()
        {
            Bitmap bmp = null;
            return bmp;
        }

        public void Camera_TopInsp_CapToSave(string SavePath, string FileType)
        {
            return;
        }

        public string Initial()
        {
            return "OK";
        }

        public void LightForBackLineSetValue(int value)
        {
            return;
        }

        public void LightForLeftBarSetValue(int value)
        {
            return;
        }

        public void LightForLeftLineSetValue(int value)
        {
            return;
        }

        public void LightForRightBarSetValue(int value)
        {
            return;
        }

        public void LightForTopCrlDfsSetValue(int value)
        {
            return;
        }

        public void LightForTopCrlInspSetValue(int value)
        {
            return;
        }
        public int ReadChamberPressureDiff()
        {
            return 1;
        }

        public int ReadChamberPressureDiffLimit()
        {
            return 1;
        }

        public EnumMacPlcAssemblyStatus ReadICStatus()
        {
            return EnumMacPlcAssemblyStatus.None;
        }

        public int ReadLightForBackLine()
        { return 1; }

        public int ReadLightForLeftBar()
        { return 1; }

        public int ReadLightForLeftLine()
        { return 1; }

        public int ReadLightForRightBar()
        { return 1; }

        public int ReadLightForTopCrlDfs()
        { return 1; }

        public int ReadLightForTopCrlInsp()
        { return 1; }

        public Tuple<int, int, int> ReadParticleCntLimit()
        {
            return new Tuple<int, int, int>(2, 2, 2);
        }

        public Tuple<int, int, int> ReadParticleCount()
        {
            return new Tuple<int, int, int>(1, 1, 1);
        }

        public bool ReadRobotIntruded()
        {
            return false;
        }

        public double ReadRobotPosLeftRight()
        {
            return 0;
        }

        public Tuple<double, double> ReadRobotPosLeftRightLimit()
        {
            return new Tuple<double, double>(1, 0);
        }

        public double ReadRobotPosUpDown()
        {
            return 0;
        }

        public Tuple<double, double> ReadRobotPosUpDownLimit()
        {
            return new Tuple<double, double>(1, 0);
        }

        public Tuple<double, double, double> ReadSpeedVar()
        {
            return new Tuple<double, double, double>(1, 1, 1);
        }

        public double ReadWPosition()
        {
            return 1;
        }

        public Tuple<double, double> ReadXYPosition()
        {
            return new Tuple<double, double>(1, 1);
        }

        public double ReadZPosition()
        {
            return 1;
        }

        public void SetParticleCntLimit(uint? L_Limit, uint? M_Limit, uint? S_Limit)
        {
            return;
        }

        public void SetPressureDiffLimit(uint? GaugeLimit)
        {
            return;
        }

        public bool SetRobotIntrude(bool isIntrude)
        {
            return isIntrude;
        }
        public void SetRobotPosLeftRightLimit(double? AboutLimit_L, double? AboutLimit_R)
        {
            return;
        }

        public void SetRobotPosUpDownLimit(double? UpDownLimit_U, double? UpDownLimit_D)
        {
            return;
        }

        public void SetSpeedVar(double? StageXYSpeed, double? CcdZSpeed, double? MaskWSpeed)
        {
            return;
        }

        public string WPosition(double W_Position)
        {
            return "OK";
        }

        public string XYPosition(double? X_Position, double? Y_Position)
        {
            return "OK";
        }

        public string ZPosition(double Z_Position)
        {
            return "OK";
        }
    }
}
