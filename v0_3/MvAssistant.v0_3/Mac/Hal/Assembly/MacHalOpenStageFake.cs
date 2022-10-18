using MvAssistant.v0_3.DeviceDrive.OmronSentechCamera;
using MvAssistant.v0_3.Mac.Hal.CompCamera;
using MvAssistant.v0_3.Mac.Hal.CompPlc;
using MvAssistant.v0_3.Mac.Manifest;
using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace MvAssistant.v0_3.Mac.Hal.Assembly
{
    [Guid("5F714250-6215-40B1-A6AB-FD6BA8C7A27A")]
    public class MacHalOpenStageFake : MacHalAssemblyBase, IMacHalOpenStage
    {


        #region Device Components


        public IHalCamera CameraNearCC { get { return (IHalCamera)this.GetHalDevice(EnumMacDeviceId.openstage_camera_right_1); } }
        public IHalCamera CameraNearLP { get { return (IHalCamera)this.GetHalDevice(EnumMacDeviceId.openstage_camera_left_1); } }
        public IHalCamera CameraSide { get { return (IHalCamera)this.GetHalDevice(EnumMacDeviceId.openstage_camera_side_1); } }
        public IHalCamera CameraTop { get { return (IHalCamera)this.GetHalDevice(EnumMacDeviceId.openstage_camera_top_1); } }
        public IMacHalPlcOpenStage Plc { get { return (IMacHalPlcOpenStage)this.GetHalDevice(EnumMacDeviceId.openstage_plc); } }
        #endregion Device Components


        public Bitmap Camera_Left_Cap()
        {
            Bitmap bmp = null;
            return bmp;
        }

        public void Camera_Left_CapToSave(string SavePath, string FileType)
        {
            return;
        }

        public Bitmap Camera_Right_Cap()
        {
            Bitmap bmp = null;
            return bmp;
        }

        public void Camera_Right_CapToSave(string SavePath, string FileType)
        {
            return;
        }

        public Bitmap Camera_Side_Cap()
        {
            Bitmap bmp = null;
            return bmp;
        }

        public void Camera_Side_CapToSave(string SavePath, string FileType)
        {
            return;
        }

        public Bitmap Camera_Top_Cap()
        {
            Bitmap bmp = null;
            return bmp;
        }

        public void Camera_Top_CapToSave(string SavePath, string FileType)
        {
            return;
        }

        public string Clamp()
        {
            return "OK";
        }

        public string Close()
        {
            return "OK";
        }

        public string Initial()
        {
            return "OK";
        }

        public void LightForFrontBarDfsSetValue(int value)
        {
            return;
        }

        public void LightForSideBarDfsSetValue(int value)
        {
            return;
        }

        public void LightForTopBarDfsSetValue(int value)
        {
            return;
        }

        public string Lock()
        {
            return "OK";
        }

        /// <summary>
        /// 開盒
        /// </summary>
        /// <returns></returns>
        public string Open()
        {
            return "OK";
        }
        public int ReadLightForFrontBarDfs()
        { return 1; }

        public int ReadLightForSideBarDfs()
        { return 1; }

        public int ReadLightForTopBarDfs()
        { return 1; }

        public EnumMacPlcAssemblyStatus ReadOSStatus()
        { return EnumMacPlcAssemblyStatus.Busy; }

        public Tuple<int, int, int> ReadParticleCntLimit()
        {
            return new Tuple<int, int, int>(2, 2, 2);
        }

        public Tuple<int, int, int> ReadParticleCount()
        {
            return new Tuple<int, int, int>(1, 1, 1);
        }

        public void SetParticleCntLimit(uint? L_Limit, uint? M_Limit, uint? S_Limit)
        {
            return;
        }

        public string SortClamp()
        {
            return "OK";
        }

        public string SortUnclamp()
        {
            return "OK";
        }

        public string Unclamp()
        {
            return "OK";
        }
        public string Vacuum(bool isSuck)
        {
            return "OK";
        }
        #region Set Parameter
        /// <summary>
        /// 設定盒子種類，1：鐵盒 , 2：水晶盒
        /// </summary>
        /// <param name="BoxType">1：鐵盒 , 2：水晶盒</param>
        public void SetBoxType(uint BoxType)
        { return; }

        /// <summary>
        /// 設定速度(%)
        /// </summary>
        /// <param name="Speed">(%)</param>
        public void SetSpeedVar(uint Speed)
        { return; }
        #endregion

        #region Read Parameter

        public int ReadBoxTypeVar()
        { return 1; }

        public int ReadSpeedVar()
        { return 1; }
        #endregion

        #region Read Component Value

        /// <summary>
        /// 讀取盒子是否變形
        /// </summary>
        /// <returns></returns>
        public double ReadBoxDeform()
        { return 1; }

        /// <summary>
        /// 讀取是否有Box
        /// </summary>
        /// <returns></returns>
        public bool ReadBoxExist()
        { return true; }

        /// <summary>
        /// 讀取開盒夾爪狀態
        /// </summary>
        /// <returns></returns>
        public string ReadClampStatus()
        { return "Clamp"; }

        /// <summary>
        /// 讀取盒蓋位置
        /// </summary>
        /// <returns></returns>
        public Tuple<double, double> ReadCoverPos()
        { return new Tuple<double, double>(1, 1); }

        /// <summary>
        /// 讀取盒蓋開闔， Open ; Close
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, bool> ReadCoverSensor()
        { return new Tuple<bool, bool>(true, true); }

        /// <summary>
        /// 讀取目前是否被Robot侵入
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, bool> ReadRobotIntruded()
        { return new Tuple<bool, bool>(false, false); }

        /// <summary>
        /// 讀取Slider的位置
        /// </summary>
        /// <returns></returns>
        public Tuple<long, long> ReadSliderPosition()
        { return new Tuple<long, long>(1, 1); }

        /// <summary>
        /// 讀取Stage上固定Box的夾具位置
        /// </summary>
        /// <returns></returns>
        public Tuple<long, long> ReadSortClampPosition()
        { return new Tuple<long, long>(1, 1); }

        /// <summary>
        /// 讀取平台上的重量
        /// </summary>
        /// <returns></returns>
        public double ReadWeightOnStage()
        { return 285; }

        /// <summary>
        /// 發送入侵訊號，確認Robot能否入侵
        /// </summary>
        /// <param name="isBTIntrude">BT Robot是否要入侵</param>
        /// <param name="isMTIntrude">MT Robot是否要入侵</param>
        /// <returns></returns>
        public Tuple<bool, bool> SetRobotIntrude(bool? isBTIntrude, bool? isMTIntrude)
        {
            /**real           
            return Plc.ReadRobotIntrude(isBTIntrude, isMTIntrude);
           */
            return new Tuple<bool, bool>((isBTIntrude ?? false), (isMTIntrude ?? false));

        }
        #endregion
    }
}
