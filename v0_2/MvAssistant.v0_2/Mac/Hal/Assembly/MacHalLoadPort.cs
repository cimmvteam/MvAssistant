using MvAssistant.v0_2.DeviceDrive.GudengLoadPort.ReplyCode;
using MvAssistant.v0_2.Mac.Hal.CompCamera;
using MvAssistant.v0_2.Mac.Hal.CompLight;
using MvAssistant.v0_2.Mac.Hal.CompLoadPort;
using MvAssistant.v0_2.Mac.Hal.CompPlc;
using MvAssistant.v0_2.Mac.Manifest;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;

namespace MvAssistant.v0_2.Mac.Hal.Assembly
{
    [GuidAttribute("35E28A32-12E1-413A-8783-8A8018D512F1")]
    public class MacHalLoadPort : MacHalAssemblyBase, IMacHalLoadPort
    {
        #region Device Components

        public MacHalLoadPort()
        {

            // Units.Add((IMacHalLoadPortUnit)this.GetHalDevice(MacEnumDevice.loadport_1));
            // Units.Add((IMacHalLoadPortUnit)this.GetHalDevice(MacEnumDevice.loadport_2));
        }
        public IMacHalPlcLoadPort Plc { get { return (IMacHalPlcLoadPort)this.GetHalDevice(EnumMacDeviceId.loadport_plc); } }
        public IHalCamera CameraLoadPortA { get { return (IHalCamera)this.GetHalDevice(EnumMacDeviceId.loadportA_camera_inspect); } }
        public IHalCamera CameraLoadPortB { get { return (IHalCamera)this.GetHalDevice(EnumMacDeviceId.loadportB_camera_inspect); } }
        public IHalCamera CameraBarcodeInsp { get { return (IHalCamera)this.GetHalDevice(EnumMacDeviceId.loadport_camera_barcode_inspect); } }
        public IMacHalLight LightBarLoadPortA { get { return (IMacHalLight)this.GetHalDevice(EnumMacDeviceId.loadport_light_bar_001); } }
        public IMacHalLight LightBarLoadPortB { get { return (IMacHalLight)this.GetHalDevice(EnumMacDeviceId.loadport_light_bar_002); } }
        public IMacHalLight LightBarBarcodeReader { get { return (IMacHalLight)this.GetHalDevice(EnumMacDeviceId.loadport_light_bar_003); } }

        public IMacHalLoadPortUnit LoadPortUnit
        {
            get
            {
                for (var i = (int)EnumMacDeviceId.loadport_1; i <= (int)EnumMacDeviceId.loadport_2; i++)
                {
                    var did = (EnumMacDeviceId)i;
                    if (!this.IsContainDevice(did)) continue;
                    return (IMacHalLoadPortUnit)this.GetHalDevice(did);
                }
                return null;

            }

        }

        #endregion

        #region Timeout Function
        public virtual bool IsTimeOut(DateTime startTime, int targetDiffSecs = 20)
        {
            var thisTime = DateTime.Now;
            var diff = thisTime.Subtract(startTime).TotalSeconds;
            if (diff >= targetDiffSecs)
                return true;
            else
                return false;
        }
        #endregion Timeout Function

        public string Dock()
        {
            DateTime startTime = DateTime.Now;
            String loadportNum = LoadPortUnit.DeviceId == EnumMacDeviceId.loadport_1.ToString() ? "Load Port A" : LoadPortUnit.DeviceId == EnumMacDeviceId.loadport_2.ToString() ? "Load Port B" : "Unknown Device ID";
            LoadPortUnit.CommandDockRequest();

            while (!IsTimeOut(startTime))
            {
                if (this.LoadPortUnit.CurrentWorkState == EnumLoadPortWorkState.DockComplete)
                    return loadportNum + " Dock complete.";
                else if (this.LoadPortUnit.CurrentWorkState == EnumLoadPortWorkState.MustInitialFirst)
                    return loadportNum + " Dock must initial.";
                else if (this.LoadPortUnit.CurrentWorkState == EnumLoadPortWorkState.MustResetFirst)
                    return loadportNum + " Dock must reset.";
                else if (this.LoadPortUnit.CurrentWorkState == EnumLoadPortWorkState.PODNotPutProperly)
                    return loadportNum + " PODNotPutProperly.";
            }

            return loadportNum + " Dock timeout.";
        }

        public string Undock()
        {
            DateTime startTime = DateTime.Now;
            String loadportNum = LoadPortUnit.DeviceId == EnumMacDeviceId.loadport_1.ToString() ? "Load Port A" : LoadPortUnit.DeviceId == EnumMacDeviceId.loadport_2.ToString() ? "Load Port B" : "Unknown Device ID";
            LoadPortUnit.CommandUndockRequest();

            while (!IsTimeOut(startTime))
            {
                if (this.LoadPortUnit.CurrentWorkState == EnumLoadPortWorkState.UndockComplete)
                    return loadportNum + " Undock complete.";
                else if (this.LoadPortUnit.CurrentWorkState == EnumLoadPortWorkState.MustInitialFirst)
                    return loadportNum + " Undock must initial.";
                else if (this.LoadPortUnit.CurrentWorkState == EnumLoadPortWorkState.MustResetFirst)
                    return loadportNum + " Undock must reset.";
                else if (this.LoadPortUnit.CurrentWorkState == EnumLoadPortWorkState.PODNotPutProperly)
                    return loadportNum + " PODNotPutProperly.";
            }

            return loadportNum + " Undock timeout.";
        }

        public string Initial()
        {
            DateTime startTime = DateTime.Now;
            String loadportNum = LoadPortUnit.DeviceId == EnumMacDeviceId.loadport_1.ToString() ? "Load Port A" : LoadPortUnit.DeviceId == EnumMacDeviceId.loadport_2.ToString() ? "Load Port B" : "Unknown Device ID";
            LoadPortUnit.CommandInitialRequest();

            while (!IsTimeOut(startTime))
            {
                if (this.LoadPortUnit.CurrentWorkState == EnumLoadPortWorkState.InitialComplete)
                    return loadportNum + " Initial complete.";
                else if (LoadPortUnit.CurrentWorkState == EnumLoadPortWorkState.MustResetFirst)
                    return loadportNum + " Initial must reset.";
            }

            return loadportNum + " Initial timeout.";
        }

        public string AlarmReset()
        {
            DateTime startTime = DateTime.Now;
            String loadportNum = LoadPortUnit.DeviceId == EnumMacDeviceId.loadport_1.ToString() ? "Load Port A" : LoadPortUnit.DeviceId == EnumMacDeviceId.loadport_2.ToString() ? "Load Port B" : "Unknown Device ID";
            LoadPortUnit.CommandAlarmReset();

            while (!IsTimeOut(startTime))
            {
                if (this.LoadPortUnit.CurrentWorkState == EnumLoadPortWorkState.AlarmResetComplete)
                    return loadportNum + " AlarmReset complete.";
                else if (this.LoadPortUnit.CurrentWorkState == EnumLoadPortWorkState.AlarmResetFail)
                    return loadportNum + " AlarmReset fail.";
            }

            return loadportNum + " AlarmReset timeout.";
        }

        public bool IsDock()
        {
            return LoadPortUnit.StagePosition == EventStagePositionCode.OnDescentPosition;//Stage處於Dock位置
        }

        public bool IsUndock()
        {
            return LoadPortUnit.StagePosition == EventStagePositionCode.OnAscentPosition;//Stage處於Undock位置
        }

        #region Set Parameter
        /// <summary>
        /// 設定LoadPort內部與外部環境最大壓差限制，錶1壓差限制、錶2壓差限制
        /// </summary>
        /// <param name="Gauge1Limit">錶1壓差限制</param>
        /// <param name="Gauge2Limit">錶2壓差限制</param>
        public void SetPressureDiffLimit(uint? Gauge1Limit, uint? Gauge2Limit)
        { Plc.SetPressureDiffLimit(Gauge1Limit, Gauge2Limit); }
        #endregion

        #region Read Parameter
        /// <summary>
        /// 讀取LoadPort內部與外部環境最大壓差限制設定，錶1壓差限制、錶2壓差限制
        /// </summary>
        /// <returns>錶1壓差限制、錶2壓差限制</returns>
        public Tuple<int, int> ReadChamberPressureDiffLimit()
        { return Plc.ReadChamberPressureDiffLimit(); }
        #endregion

        #region Read Component Value
        /// <summary>
        /// 讀取LoadPort內部與外部環境壓差，錶1壓差、錶2壓差
        /// </summary>
        /// <returns>錶1壓差、錶2壓差</returns>
        public Tuple<int, int> ReadPressureDiff()
        { return Plc.ReadPressureDiff(); }

        /// <summary>
        /// 讀取Load Port光閘，True：遮斷 、 False：Normal
        /// </summary>
        /// <returns>True：遮斷、False：Normal</returns>
        public bool ReadLP_Light_Curtain()
        { return Plc.ReadLP_Light_Curtain(); }
        #endregion

        public void LightForLoadPortASetValue(int value)
        {
            LightBarLoadPortA.TurnOn(value);
        }

        public int ReadLightForLoadPortA()
        { return LightBarLoadPortA.GetValue(); }

        public void LightForLoadPortBSetValue(int value)
        {
            LightBarLoadPortB.TurnOn(value);
        }

        public int ReadLightForLoadPortB()
        { return LightBarLoadPortB.GetValue(); }

        public void LightForBarcodeReaderSetValue(int value)
        {
            LightBarBarcodeReader.TurnOn(value);
        }

        public int ReadLightForBarcodeReader()
        { return LightBarBarcodeReader.GetValue(); }

        public Bitmap Camera_LoadPortA_Cap()
        {
            return CameraLoadPortA.Shot();
        }

        public void Camera_LoadPortA_CapToSave(string SavePath, string FileType)
        {
            CameraLoadPortA.ShotToSaveImage(SavePath, FileType);
        }

        public Bitmap Camera_LoadPortB_Cap()
        {
            return CameraLoadPortB.Shot();
        }

        public void Camera_LoadPortB_CapToSave(string SavePath, string FileType)
        {
            CameraLoadPortB.ShotToSaveImage(SavePath, FileType);
        }

        public Bitmap Camera_Barcode_Cap()
        {
            return CameraBarcodeInsp.Shot();
        }

        public void Camera_Barcode_CapToSave(string SavePath, string FileType)
        {
            CameraBarcodeInsp.ShotToSaveImage(SavePath, FileType);
        }

        public string CommandAlarmReset()
        {
            //           return Unit.CommandAlarmReset();
            return null;
        }

    }
}
