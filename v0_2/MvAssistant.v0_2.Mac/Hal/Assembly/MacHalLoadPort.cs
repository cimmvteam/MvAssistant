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
        public IMacHalPlcLoadPort Plc { get { return (IMacHalPlcLoadPort)this.GetHalDevice(MacEnumDevice.loadport_plc); } }
        public IHalCamera CameraLoadPortA { get { return (IHalCamera)this.GetHalDevice(MacEnumDevice.loadportA_camera_inspect); } }
        public IHalCamera CameraLoadPortB { get { return (IHalCamera)this.GetHalDevice(MacEnumDevice.loadportB_camera_inspect); } }
        public IHalCamera CameraBarcodeInsp { get { return (IHalCamera)this.GetHalDevice(MacEnumDevice.loadport_camera_barcode_inspect); } }
        public IMacHalLight LightBarLoadPortA { get { return (IMacHalLight)this.GetHalDevice(MacEnumDevice.loadport_light_bar_001); } }
        public IMacHalLight LightBarLoadPortB { get { return (IMacHalLight)this.GetHalDevice(MacEnumDevice.loadport_light_bar_002); } }
        public IMacHalLight LightBarBarcodeReader { get { return (IMacHalLight)this.GetHalDevice(MacEnumDevice.loadport_light_bar_003); } }

        public IMacHalLoadPortUnit LoadPortUnit
        {
            get
            {
                IMacHalLoadPortUnit rtnV = null;
                for (var i = (int)MacEnumDevice.loadport_1; i <= (int)MacEnumDevice.loadport_2; i++)
                {
                    try
                    {
                        rtnV = (IMacHalLoadPortUnit)this.GetHalDevice((MacEnumDevice)i);
                        if (rtnV != null)
                        {
                            break;
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
                return rtnV;

            }

        }

        #endregion

        public string Dock()
        {
            LoadPortUnit.CommandDockRequest();

            if (!SpinWait.SpinUntil(() => LoadPortUnit.CurrentWorkState == LoadPortWorkState.DockComplete, 20 * 1000))
                return "Load Port Dock Timeout";
            else
                return "OK";
        }

        public string Undock()
        {
            LoadPortUnit.CommandUndockRequest();

            if (!SpinWait.SpinUntil(() => LoadPortUnit.CurrentWorkState == LoadPortWorkState.UndockComplete, 20 * 1000))
                return "Load Port Undock Timeout";
            else
                return "OK";
        }

        public string Initial()
        {
            LoadPortUnit.CommandInitialRequest();

            if (SpinWait.SpinUntil(() => LoadPortUnit.CurrentWorkState == LoadPortWorkState.InitialComplete, 20 * 1000))
                return "OK";
            else if (LoadPortUnit.CurrentWorkState == LoadPortWorkState.MustResetFirst)
                return "Load Port must reset first";
            else
                return "Load Port Initial timeout";
        }

        public string AlarmReset()
        {
            LoadPortUnit.CommandAlarmReset();

            if (SpinWait.SpinUntil(() => LoadPortUnit.CurrentWorkState == LoadPortWorkState.AlarmResetComplete, 20 * 1000))
                return "OK";
            else if (LoadPortUnit.CurrentWorkState == LoadPortWorkState.AlarmResetFail)
                return "Load Port AlarmReset failed";
            else
                return "Load Port AlarmReset timeout";
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
