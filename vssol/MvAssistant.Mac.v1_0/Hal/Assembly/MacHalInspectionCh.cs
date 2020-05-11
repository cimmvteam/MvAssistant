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



        public IHalCamera Inspection_ccd_defense_side_1 { get{ return (IHalCamera)this.GetMachine(MacEnumDevice.inspection_ccd_defense_side_1); } }
        public IHalCamera Inspection_ccd_defense_top_1 { get{ return (IHalCamera)this.GetMachine(MacEnumDevice.inspection_ccd_defense_top_1); } }
        public IHalCamera Inspection_ccd_inspect_side_1 { get{ return (IHalCamera)this.GetMachine(MacEnumDevice.inspection_ccd_inspect_side_1); } }
        public IHalCamera Inspection_ccd_inspect_top_1 { get{ return (IHalCamera)this.GetMachine(MacEnumDevice.inspection_ccd_inspect_top_1); } }
        public IHalLaser Inspection_laser_entry_1 { get{ return (IHalLaser)this.GetMachine(MacEnumDevice.inspection_laser_entry_1); } }
        public IHalLaser Inspection_laser_entry_2 { get{ return (IHalLaser)this.GetMachine(MacEnumDevice.inspection_laser_entry_2); } }
        public IHalLight Inspection_lightbar_1 { get{ return (IHalLight)this.GetMachine(MacEnumDevice.inspection_lightbar_1); } }
        public IHalLight Inspection_linesource_1 { get{ return (IHalLight)this.GetMachine(MacEnumDevice.inspection_linesource_1); } }
        public IHalLight Inspection_linesource_2 { get{ return (IHalLight)this.GetMachine(MacEnumDevice.inspection_linesource_2); } }
        public IHalLight Inspection_linesource_3 { get{ return (IHalLight)this.GetMachine(MacEnumDevice.inspection_linesource_3); } }
        public IHalLight Inspection_linesource_4 { get{ return (IHalLight)this.GetMachine(MacEnumDevice.inspection_linesource_4); } }
        public IHalParticleCounter Inspection_particle_counter_1 { get{ return (IHalParticleCounter)this.GetMachine(MacEnumDevice.inspection_particle_counter_1); } }
        public IHalLight Inspection_ringlight_1 { get{ return (IHalLight)this.GetMachine(MacEnumDevice.inspection_ringlight_1); } }
        public IHalInspectionStage Inspection_stage_1 { get{ return (IHalInspectionStage)this.GetMachine(MacEnumDevice.inspection_stage_1); } }


        #endregion Device Components

        /// <summary>
        /// Stage XY軸移動，X:300~-10,Y:250~-10，X軸位置、Y軸位置
        /// </summary>
        /// <param name="X_Position">X軸位置</param>
        /// <param name="Y_Position">Y軸位置</param>
        /// <returns></returns>
        public string XYPosition(double X_Position, double Y_Position)
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
    }
}
