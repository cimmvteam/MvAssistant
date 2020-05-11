using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Hal.Component;
using MvAssistant.Mac.v1_0.Hal.Component.Camera;
using MvAssistant.Mac.v1_0.Hal.Component.Stage;
using MvAssistant.Mac.v1_0.Hal.CompPlc;
using MvAssistant.Mac.v1_0.Manifest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Assembly
{
    [GuidAttribute("F6FF301E-55DB-4861-AF19-A90F5A70FDA6")]
    public class MacHalInspectionCh : MacHalAssemblyBase, IMacHalInspectionCh
    {
        #region Device Components


        public IMacHalPlcInspectionCh Plc { get { return (IMacHalPlcInspectionCh)this.GetMachine(MacEnumDevice.inspectionch_plc); } }



        public IHalCamera Inspection_ccd_defense_side_1 { get { return (IHalCamera)this.GetMachine(MacEnumDevice.inspection_ccd_defense_side_1); } }
        public IHalCamera Inspection_ccd_defense_top_1 { get { return (IHalCamera)this.GetMachine(MacEnumDevice.inspection_ccd_defense_top_1); } }
        public IHalCamera Inspection_ccd_inspect_side_1 { get { return (IHalCamera)this.GetMachine(MacEnumDevice.inspection_ccd_inspect_side_1); } }
        public IHalCamera Inspection_ccd_inspect_top_1 { get { return (IHalCamera)this.GetMachine(MacEnumDevice.inspection_ccd_inspect_top_1); } }
        public IHalLaser Inspection_laser_entry_1 { get { return (IHalLaser)this.GetMachine(MacEnumDevice.inspection_laser_entry_1); } }
        public IHalLaser Inspection_laser_entry_2 { get { return (IHalLaser)this.GetMachine(MacEnumDevice.inspection_laser_entry_2); } }
        public IHalLight Inspection_lightbar_1 { get { return (IHalLight)this.GetMachine(MacEnumDevice.inspection_lightbar_1); } }
        public IHalLight Inspection_linesource_1 { get { return (IHalLight)this.GetMachine(MacEnumDevice.inspection_linesource_1); } }
        public IHalLight Inspection_linesource_2 { get { return (IHalLight)this.GetMachine(MacEnumDevice.inspection_linesource_2); } }
        public IHalLight Inspection_linesource_3 { get { return (IHalLight)this.GetMachine(MacEnumDevice.inspection_linesource_3); } }
        public IHalLight Inspection_linesource_4 { get { return (IHalLight)this.GetMachine(MacEnumDevice.inspection_linesource_4); } }
        public IHalParticleCounter Inspection_particle_counter_1 { get { return (IHalParticleCounter)this.GetMachine(MacEnumDevice.inspection_particle_counter_1); } }
        public IHalLight Inspection_ringlight_1 { get { return (IHalLight)this.GetMachine(MacEnumDevice.inspection_ringlight_1); } }
        public IHalInspectionStage Inspection_stage_1 { get { return (IHalInspectionStage)this.GetMachine(MacEnumDevice.inspection_stage_1); } }


        #endregion Device Components

        /// <summary>
        /// Stage XY軸移動，X:300~-10,Y:250~-10，X軸位置、Y軸位置
        /// </summary>
        /// <param name="X_Position">X軸位置</param>
        /// <param name="Y_Position">Y軸位置</param>
        /// <returns></returns>
        public string ICXYPosition(double X_Position, double Y_Position)
        { return Plc.XYPosition(X_Position, Y_Position); }

        /// <summary>
        /// CCD高度調整，1~-85
        /// </summary>
        /// <param name="Z_Position"></param>
        /// <returns></returns>
        public string ICZPosition(double Z_Position)
        { return Plc.ZPosition(Z_Position); }

        /// <summary>
        /// Mask載台方向旋轉，0~359
        /// </summary>
        /// <param name="W_Position"></param>
        /// <returns></returns>
        public string ICWPosition(double W_Position)
        { return Plc.WPosition(W_Position); }

        public string ICInitial()
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
        public void SetSpeed(double StageXYSpeed, double CcdZSpeed, double MaskWSpeed)
        { Plc.SetSpeed(StageXYSpeed, CcdZSpeed, MaskWSpeed); }

        /// <summary>
        /// 設定手臂入侵的左右區間極限，左極限、右極限
        /// </summary>
        /// <param name="AboutLimit_L">左極限</param>
        /// <param name="AboutLimit_R">右極限</param>
        public void SetRobotAboutLimit(double AboutLimit_L, double AboutLimit_R)
        { Plc.SetRobotAboutLimit(AboutLimit_L, AboutLimit_R); }

        /// <summary>
        /// 設定手臂入侵的上下區間極限，上極限、下極限
        /// </summary>
        /// <param name="UpDownLimit_U"></param>
        /// <param name="UpDownLimit_D"></param>
        void SetRobotUpDownLimit(double UpDownLimit_U, double UpDownLimit_D)
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
    }
}
