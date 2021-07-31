using MvAssistant.v0_2.Mac.Hal.CompCamera;
using MvAssistant.v0_2.Mac.Hal.CompLight;
using MvAssistant.v0_2.Mac.Hal.CompPlc;
using MvAssistant.v0_2.Mac.Manifest;
using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace MvAssistant.v0_2.Mac.Hal.Assembly
{
    [GuidAttribute("F6FF301E-55DB-4861-AF19-A90F5A70FDA6")]
    public class MacHalInspectionCh : MacHalAssemblyBase, IMacHalInspectionCh
    {
        #region Device Components


        public IMacHalPlcInspectionCh Plc { get { return (IMacHalPlcInspectionCh)this.GetHalDevice(EnumMacDeviceId.inspectionch_plc); } }
        public IMacHalLight LightLineLeft { get { return (IMacHalLight)this.GetHalDevice(EnumMacDeviceId.inspectionch_light_line_left_001); } }
        public IMacHalLight LightLineBack { get { return (IMacHalLight)this.GetHalDevice(EnumMacDeviceId.inspectionch_light_line_back_001); } }
        public IMacHalLight LightCrlDefenseTop { get { return (IMacHalLight)this.GetHalDevice(EnumMacDeviceId.inspectionch_light_circle_defense_top_001); } }
        public IMacHalLight LightCrlInspTop { get { return (IMacHalLight)this.GetHalDevice(EnumMacDeviceId.inspectionch_light_circle_inspection_top_001); } }
        public IMacHalLight LightBarLeft { get { return (IMacHalLight)this.GetHalDevice(EnumMacDeviceId.inspectionch_light_bar_left_001); } }
        public IMacHalLight LightBarRight { get { return (IMacHalLight)this.GetHalDevice(EnumMacDeviceId.inspectionch_light_bar_right_001); } }
        public IHalCamera CameraSideInsp { get { return (IHalCamera)this.GetHalDevice(EnumMacDeviceId.inspectionch_camera_inspect_side_001); } }
        public IHalCamera CameraSideDfs { get { return (IHalCamera)this.GetHalDevice(EnumMacDeviceId.inspectionch_camera_defense_side_001); } }
        public IHalCamera CameraTopDfs { get { return (IHalCamera)this.GetHalDevice(EnumMacDeviceId.inspectionch_camera_defense_top_001); } }
        public IHalCamera CameraLink { get { return (IHalCamera)this.GetHalDevice(EnumMacDeviceId.inspectionch_camera_inspect_top_001); } }


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
            this.LightLineLeft.TurnOn(255);
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

        /// <summary>
        /// 設定Robot是否要入侵，讀取Mask Robot可否入侵
        /// </summary>
        /// <param name="isIntrude"></param>
        /// <returns></returns>
        public bool SetRobotIntrude(bool isIntrude)
        { return Plc.SetRobotIntrude(isIntrude); }

        public bool ReadRobotIntruded()
        { return Plc.ReadRobotIntruded(); }

        #region Set Parameter
        /// <summary>
        /// 設定速度，Stage XY軸移動速度(mm/S)、CCD Z軸移動速度(mm/S)、Mask W軸旋轉速度(Deg/S)
        /// </summary>
        /// <param name="StageXYSpeed">Stage XY軸移動速度(mm/S)</param>
        /// <param name="CcdZSpeed">CCD Z軸移動速度(mm/S)</param>
        /// <param name="MaskWSpeed">Mask W軸旋轉速度(Deg/S)</param>
        public void SetSpeedVar(double? StageXYSpeed, double? CcdZSpeed, double? MaskWSpeed)
        { Plc.SetSpeedVar(StageXYSpeed, CcdZSpeed, MaskWSpeed); }

        /// <summary>
        /// 設定手臂入侵的左右區間極限，左極限、右極限
        /// </summary>
        /// <param name="AboutLimit_L">左極限</param>
        /// <param name="AboutLimit_R">右極限</param>
        public void SetRobotPosLeftRightLimit(double? AboutLimit_L, double? AboutLimit_R)
        { Plc.SetRobotPosLeftRightLimit(AboutLimit_L, AboutLimit_R); }

        /// <summary>
        /// 設定手臂入侵的上下區間極限，上極限、下極限
        /// </summary>
        /// <param name="UpDownLimit_U"></param>
        /// <param name="UpDownLimit_D"></param>
        public void SetRobotPosUpDownLimit(double? UpDownLimit_U, double? UpDownLimit_D)
        { Plc.SetRobotPosUpDownLimit(UpDownLimit_U, UpDownLimit_D); }

        public void SetParticleCntLimit(uint? L_Limit, uint? M_Limit, uint? S_Limit)
        { Plc.SetParticleCntLimit(L_Limit, M_Limit, S_Limit); }

        /// <summary>
        /// 設定Inspection Chamber內部與外部環境最大壓差限制
        /// </summary>
        /// <param name="GaugeLimit">壓差限制</param>
        public void SetPressureDiffLimit(uint? GaugeLimit)
        { Plc.SetChamberPressureDiffLimit(GaugeLimit); }
        #endregion

        #region Read Parameter
        /// <summary>
        /// 讀取速度設定，Stage XY軸移動速度(mm/S)、CCD Z軸移動速度(mm/S)、Mask W軸旋轉速度(Deg/S)
        /// </summary>
        /// <returns>Stage XY軸移動速度(mm/S)、CCD Z軸移動速度(mm/S)、Mask W軸旋轉速度(Deg/S)</returns>
        public Tuple<double, double, double> ReadSpeedVar()
        { return Plc.ReadSpeedVar(); }

        /// <summary>
        /// 讀取手臂入侵的左右區間極限設定，左極限、右極限
        /// </summary>
        /// <returns>左極限、右極限</returns>
        public Tuple<double, double> ReadRobotPosLeftRightLimit()
        { return Plc.ReadRobotPosLeftRightLimit(); }

        /// <summary>
        /// 讀取手臂入侵的上下區間極限設定，上極限、下極限
        /// </summary>
        /// <returns>上極限、下極限</returns>
        public Tuple<double, double> ReadRobotPosUpDownLimit()
        { return Plc.ReadRobotPosUpDownLimit(); }

        public Tuple<int, int, int> ReadParticleCntLimit()
        { return Plc.ReadParticleCntLimit(); }

        public Tuple<int, int, int> ReadParticleCount()
        { return Plc.ReadParticleCount(); }

        /// <summary>
        /// 讀取Inspection Chamber內部與外部環境最大壓差限制設定，錶1壓差限制、錶2壓差限制
        /// </summary>
        /// <returns>錶1壓差限制、錶2壓差限制</returns>
        public int ReadChamberPressureDiffLimit()
        { return Plc.ReadChamberPressureDiffLimit(); }
        #endregion

        #region Read Component Value
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
        public double ReadRobotPosLeftRight()
        { return Plc.ReadRobotPosLeftRight(); }

        /// <summary>
        /// 讀取手臂直向位置(上下區間)
        /// </summary>
        /// <returns></returns>
        public double ReadRobotPosUpDown()
        { return Plc.ReadRobotPosUpDown(); }

        /// <summary>
        /// 讀取Inspection Chamber內部與外部環境壓差
        /// </summary>
        /// <returns>錶壓差</returns>
        public int ReadChamberPressureDiff()
        { return Plc.ReadChamberPressureDiff(); }
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

        public void LightForLeftLineSetValue(int value)
        {
            LightLineLeft.TurnOn(value);
        }

        public void LightForBackLineSetValue(int value)
        {
            LightLineBack.TurnOn(value);
        }

        public void LightForTopCrlDfsSetValue(int value)
        {
            LightCrlDefenseTop.TurnOn(value);
        }

        public void LightForTopCrlInspSetValue(int value)
        {
            LightCrlInspTop.TurnOn(value);
        }

        public void LightForLeftBarSetValue(int value)
        {
            LightBarLeft.TurnOn(value);
        }

        public void LightForRightBarSetValue(int value)
        {
            LightBarRight.TurnOn(value);
        }

        public int ReadLightForLeftLine()
        { return LightLineLeft.GetValue(); }
               
        public int ReadLightForBackLine()
        { return LightLineBack.GetValue(); }
               
        public int ReadLightForTopCrlDfs()
        { return LightCrlDefenseTop.GetValue(); }
               
        public int ReadLightForTopCrlInsp()
        { return LightCrlInspTop.GetValue(); }
               
        public int ReadLightForLeftBar()
        { return LightBarLeft.GetValue(); }
               
        public int ReadLightForRightBar()
        { return LightBarRight.GetValue(); }
    }
}
