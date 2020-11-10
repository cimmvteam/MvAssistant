using MvAssistant.Mac.v1_0.Hal.CompCamera;
using MvAssistant.Mac.v1_0.Hal.CompLight;
using MvAssistant.Mac.v1_0.Hal.CompPlc;
using MvAssistant.Mac.v1_0.Manifest;
using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace MvAssistant.Mac.v1_0.Hal.Assembly
{
    [GuidAttribute("F6FF301E-55DB-4861-AF19-A90F5A70FDA6")]
    public class MacHalInspectionCh : MacHalAssemblyBase, IMacHalInspectionCh
    {
        #region Device Components


        public IMacHalPlcInspectionCh Plc { get { return (IMacHalPlcInspectionCh)this.GetHalDevice(MacEnumDevice.inspectionch_plc); } }
        public IMacHalLight LightBarInspSide { get { return (IMacHalLight)this.GetHalDevice(MacEnumDevice.inspectionch_light_bar_inspection_side_001); } }
        public IMacHalLight LightBarDefenseSide { get { return (IMacHalLight)this.GetHalDevice(MacEnumDevice.inspectionch_light_bar_denfese_side_001); } }
        public IMacHalLight LightCrlDefenseTop { get { return (IMacHalLight)this.GetHalDevice(MacEnumDevice.inspectionch_light_circle_defense_top_001); } }
        public IMacHalLight LightCrlInspTop { get { return (IMacHalLight)this.GetHalDevice(MacEnumDevice.inspectionch_light_circle_inspection_top_001); } }
        public IMacHalLight LightSpotInspLeft { get { return (IMacHalLight)this.GetHalDevice(MacEnumDevice.inspectionch_light_spot_inspection_left_001); } }
        public IMacHalLight LightSpotInspRight { get { return (IMacHalLight)this.GetHalDevice(MacEnumDevice.inspectionch_light_spot_inspection_right_001); } }
        public IHalCamera CameraSideInsp { get { return (IHalCamera)this.GetHalDevice(MacEnumDevice.inspectionch_camera_inspect_side_1); } }
        public IHalCamera CameraSideDfs { get { return (IHalCamera)this.GetHalDevice(MacEnumDevice.inspection_camera_defense_side_1); } }
        public IHalCamera CameraTopDfs { get { return (IHalCamera)this.GetHalDevice(MacEnumDevice.inspection_camera_defense_top_1); } }
        public IHalCamera CameraLink { get { return (IHalCamera)this.GetHalDevice(MacEnumDevice.inspection_camera_inspect_top_1); } }


        #endregion Device Components
        

        public bool DefenseCheck()
        {
            //TODO: Light tun on
            //  Camera capture
            // Algo
            // Light turn off


            return false;
        }

        public void InspectTop()
        {

        }


        public void InspectionSide()
        {

            this.Plc.XYPosition(200, 100);
            this.Plc.ZPosition(-50);

            this.Plc.WPosition(0);
            this.LightBarInspSide.TurnOn(255);
            //TODO: Camera

            this.Plc.WPosition(90);
            //TODO: Camera

            this.Plc.WPosition(180);
            //TODO: Camera

            this.Plc.WPosition(270);
            //TODO: Camera


        }







        /// <summary>
        /// Stage XY軸移動，X:300~-10,Y:250~-10，X軸位置、Y軸位置
        /// </summary>
        /// <param name="X_Position">X軸位置</param>
        /// <param name="Y_Position">Y軸位置</param>
        /// <returns></returns>
        public string XYPosition(double? X_Position, double? Y_Position)
        { return Plc.XYPosition(X_Position, Y_Position); }

        /// <summary>
        /// CCD高度調整，1~-85
        /// </summary>
        /// <param name="Z_Position"></param>
        /// <returns></returns>
        public string ZPosition(double Z_Position)
        { return Plc.ZPosition(Z_Position); }

        /// <summary>
        /// Mask載台方向旋轉，0~359
        /// </summary>
        /// <param name="W_Position"></param>
        /// <returns></returns>
        public string WPosition(double W_Position)
        { return Plc.WPosition(W_Position); }

        public string Initial()
        { return Plc.Initial(); }

        public string ReadInspChStatus()
        { return Plc.ReadInspChStatus(); }

        #region Set Parameter
        /// <summary>
        /// 設定速度，Stage XY軸移動速度(mm/S)、CCD Z軸移動速度(mm/S)、Mask W軸旋轉速度(Deg/S)
        /// </summary>
        /// <param name="StageXYSpeed">Stage XY軸移動速度(mm/S)</param>
        /// <param name="CcdZSpeed">CCD Z軸移動速度(mm/S)</param>
        /// <param name="MaskWSpeed">Mask W軸旋轉速度(Deg/S)</param>
        public void SetSpeed(double? StageXYSpeed, double? CcdZSpeed, double? MaskWSpeed)
        { Plc.SetSpeed(StageXYSpeed, CcdZSpeed, MaskWSpeed); }

        /// <summary>
        /// 設定手臂入侵的左右區間極限，左極限、右極限
        /// </summary>
        /// <param name="AboutLimit_L">左極限</param>
        /// <param name="AboutLimit_R">右極限</param>
        public void SetRobotAboutLimit(double? AboutLimit_L, double? AboutLimit_R)
        { Plc.SetRobotAboutLimit(AboutLimit_L, AboutLimit_R); }

        /// <summary>
        /// 設定手臂入侵的上下區間極限，上極限、下極限
        /// </summary>
        /// <param name="UpDownLimit_U"></param>
        /// <param name="UpDownLimit_D"></param>
        public void SetRobotUpDownLimit(double? UpDownLimit_U, double? UpDownLimit_D)
        { Plc.SetRobotUpDownLimit(UpDownLimit_U, UpDownLimit_D); }
        #endregion

        #region Read Parameter
        /// <summary>
        /// 讀取速度設定，Stage XY軸移動速度(mm/S)、CCD Z軸移動速度(mm/S)、Mask W軸旋轉速度(Deg/S)
        /// </summary>
        /// <returns>Stage XY軸移動速度(mm/S)、CCD Z軸移動速度(mm/S)、Mask W軸旋轉速度(Deg/S)</returns>
        public Tuple<double, double, double> ReadSpeedSetting()
        { return Plc.ReadSpeedSetting(); }

        /// <summary>
        /// 讀取手臂入侵的左右區間極限設定，左極限、右極限
        /// </summary>
        /// <returns>左極限、右極限</returns>
        public Tuple<double, double> ReadRobotAboutLimitSetting()
        { return Plc.ReadRobotAboutLimitSetting(); }

        /// <summary>
        /// 讀取手臂入侵的上下區間極限設定，上極限、下極限
        /// </summary>
        /// <returns>上極限、下極限</returns>
        public Tuple<double, double> ReadRobotUpDownLimitSetting()
        { return Plc.ReadRobotUpDownLimitSetting(); }
        #endregion

        #region Read Component Value
        /// <summary>
        /// 設定Robot是否要入侵，讀取Mask Robot可否入侵
        /// </summary>
        /// <param name="isIntrude"></param>
        /// <returns></returns>
        public bool ReadRobotIntrude(bool isIntrude)
        { return Plc.ReadRobotIntrude(isIntrude); }

        /// <summary>
        /// 讀取Stage XY軸位置，X軸位置、Y軸位置
        /// </summary>
        /// <returns>X軸位置、Y軸位置</returns>
        public Tuple<double, double> ReadXYPosition()
        { return Plc.ReadXYPosition(); }

        /// <summary>
        /// 讀取 CCD Z軸位置
        /// </summary>
        /// <returns>Z軸位置</returns>
        public double ReadZPosition()
        { return Plc.ReadZPosition(); }

        /// <summary>
        /// 讀取Mask旋轉W軸位置(旋轉角度)
        /// </summary>
        /// <returns>W軸位置</returns>
        public double ReadWPosition()
        { return Plc.ReadWPosition(); }

        /// <summary>
        /// 讀取手臂橫向位置(左右區間)
        /// </summary>
        /// <returns></returns>
        public double ReadRobotPosAbout()
        { return Plc.ReadRobotPosAbout(); }

        /// <summary>
        /// 讀取手臂直向位置(上下區間)
        /// </summary>
        /// <returns></returns>
        public double ReadRobotPosUpDown()
        { return Plc.ReadRobotPosUpDown(); }
        #endregion

        public Bitmap Camera_TopInsp_Cap()
        {
            return CameraLink.Shot();
        }

        public void Camera_TopInsp_CapToSave(string SavePath, string FileType)
        {
            CameraLink.ShotToSaveImage(SavePath, FileType);
        }

        public Bitmap Camera_TopDfs_Cap()
        {
            return CameraTopDfs.Shot();
        }

        public void Camera_TopDfs_CapToSave(string SavePath, string FileType)
        {
            CameraTopDfs.ShotToSaveImage(SavePath, FileType);
        }

        public Bitmap Camera_SideDfs_Cap()
        {
            return CameraSideDfs.Shot();
        }

        public void Camera_SideDfs_CapToSave(string SavePath, string FileType)
        {
            CameraSideDfs.ShotToSaveImage(SavePath, FileType);
        }

        public Bitmap Camera_SideInsp_Cap()
        {
            return CameraSideInsp.Shot();
        }

        public void Camera_SideInsp_CapToSave(string SavePath, string FileType)
        {
            CameraSideInsp.ShotToSaveImage(SavePath, FileType);
        }

        public void LightForSideBarInspSetValue(int value)
        {
            LightBarInspSide.TurnOn(value);
        }

        public void LightForSideBarDfsSetValue(int value)
        {
            LightBarDefenseSide.TurnOn(value);
        }
        
        public void LightForTopCrlDefenseSetValue(int value)
        {
            LightCrlDefenseTop.TurnOn(value);
        }

        public void LightForTopCrlInspSetValue(int value)
        {
            LightCrlInspTop.TurnOn(value);
        }

        public void LightForLeftSpotInspSetValue(int value)
        {
            LightSpotInspLeft.TurnOn(value);
        }

        public void LightForRightSpotInspSetValue(int value)
        {
            LightSpotInspRight.TurnOn(value);
        }
    }
}
