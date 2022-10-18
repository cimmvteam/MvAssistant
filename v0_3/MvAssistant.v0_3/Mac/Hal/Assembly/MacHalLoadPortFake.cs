using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MvAssistant.v0_3.Mac.Hal.CompLoadPort;
using MvAssistant.v0_3.Mac.Hal.CompPlc;
using MvAssistant.v0_3.Mac.Manifest;

namespace MvAssistant.v0_3.Mac.Hal.Assembly
{
    [Guid("24DF35C8-AF37-4103-BE97-770294647EEF")]
    public class MacHalLoadPortFake : MacHalAssemblyBase, IMacHalLoadPort
    {
        public IMacHalPlcLoadPort Plc { get { return (IMacHalPlcLoadPort)this.GetHalDevice(EnumMacDeviceId.loadport_plc); } }
        public IMacHalLoadPortUnit LoadPortUnit
        {
            get
            {
                for (var idx = (int)EnumMacDeviceId.loadport_1; idx <= (int)EnumMacDeviceId.loadport_2; idx++)
                {
                    var did = (EnumMacDeviceId)idx;
                    if (!this.IsContainDevice(did)) continue;//先確認是否有此裝置再 Return,避免拋出 Exception
                    return (IMacHalLoadPortUnit)this.GetHalDevice(did);

                }
                return null;
            }

        }

        public string Dock()
        { return "OK"; }

        public string Undock()
        { return "OK"; }

        public string Initial()
        { return "OK"; }

        public string AlarmReset()
        { return "OK"; }

        public bool IsDock()
        { return true; }

        public bool IsUndock()
        { return true; }

        public bool ReadLP_Light_Curtain()
        {
            return false;
        }

        public Tuple<int, int> ReadPressureDiff()
        {
            return new Tuple<int, int>(0, 0);
        }

        public Tuple<int, int> ReadChamberPressureDiffLimit()
        {
            return new Tuple<int, int>(1, 1);
        }

        public void SetPressureDiffLimit(uint? Gauge1Limit, uint? Gauge2Limit)
        {
            return;
        }

        public Bitmap Camera_LoadPortA_Cap()
        {
            Bitmap bmp = null;
            return bmp;
        }

        public void Camera_LoadPortA_CapToSave(string SavePath, string FileType)
        {
            return;
        }

        public Bitmap Camera_LoadPortB_Cap()
        {
            Bitmap bmp = null;
            return bmp;
        }

        public void Camera_LoadPortB_CapToSave(string SavePath, string FileType)
        {
            return;
        }

        public Bitmap Camera_Barcode_Cap()
        {
            Bitmap bmp = null;
            return bmp;
        }

        public void Camera_Barcode_CapToSave(string SavePath, string FileType)
        {
            return;
        }

        public void LightForLoadPortASetValue(int value)
        {
            return;
        }

        public void LightForLoadPortBSetValue(int value)
        {
            return;
        }

        public void LightForBarcodeReaderSetValue(int value)
        {
            return;
        }

        public int ReadLightForLoadPortA()
        {
            return 1;
        }

        public int ReadLightForLoadPortB()
        {
            return 1;
        }

        public int ReadLightForBarcodeReader()
        {
            return 1;
        }
    }
}
