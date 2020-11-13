using MvAssistant.Mac.v1_0.Hal.CompCamera;
using MvAssistant.Mac.v1_0.Hal.CompLoadPort;
using MvAssistant.Mac.v1_0.Hal.CompPlc;
using MvAssistant.Mac.v1_0.Manifest;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;

namespace MvAssistant.Mac.v1_0.Hal.Assembly
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
                throw new MvException("Load Port Dock Timeout !!");
            else
                return "OK";
        }

        public string Undock()
        {
            LoadPortUnit.CommandUndockRequest();

            if (!SpinWait.SpinUntil(() => LoadPortUnit.CurrentWorkState == LoadPortWorkState.UndockComplete, 20 * 1000))
                throw new MvException("Load Port Undock Timeout !!");
            else
                return "OK";
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
        public Tuple<int, int> ReadPressureDiffLimitSrtting()
        { return Plc.ReadPressureDiffLimitSrtting(); }
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
