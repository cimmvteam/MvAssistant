using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Hal.Component;
using MvAssistant.Mac.v1_0.Hal.Component.Camera;
using MvAssistant.Mac.v1_0.Hal.Component.Stage;

using MvAssistant.Manifest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.AssemblyFake
{    
    [MachineManifest(DeviceEnum.inspection_assembly)]
    [GuidAttribute("58130CF0-A31E-4502-87A4-EB43FF78DFE8")]
    public class HalInspectionChamberFake : HalFakeBase, IHalInspectionChamber
    {
        #region Device Components (請看範例說明)
        /// 範例說明
        ///private IHalCamera topCamera;
        ///private IHalCamera sideCamera;
        ///
        ///[MachineManifest(DeviceEnum.loadport_ccd_top_1)]
        ///public IHalCamera TopCamera
        ///{
        ///    set { topCamera = value; }
        ///    get { return topCamera; }
        ///}
        ///
        ///[MachineManifest(DeviceEnum.loadport_ccd_side_1)]
        ///public IHalCamera SideCamera
        ///{
        ///    set { sideCamera = value; }
        ///    get { return sideCamera; }
        ///}
        

        private IHalCamera inspection_ccd_defense_side_1;
        [MachineManifest(DeviceEnum.inspection_ccd_defense_side_1)]
        public IHalCamera Inspection_ccd_defense_side_1
        {
            get { return inspection_ccd_defense_side_1; }
            set { inspection_ccd_defense_side_1 = value; }
        }

        private IHalCamera inspection_ccd_defense_top_1;
        [MachineManifest(DeviceEnum.inspection_ccd_defense_top_1)]
        public IHalCamera Inspection_ccd_defense_top_1
        {
            get { return inspection_ccd_defense_top_1; }
            set { inspection_ccd_defense_top_1 = value; }
        }

        private IHalCamera inspection_ccd_inspect_side_1;
        [MachineManifest(DeviceEnum.inspection_ccd_inspect_side_1)]
        public IHalCamera Inspection_ccd_inspect_side_1
        {
            get { return inspection_ccd_inspect_side_1; }
            set { inspection_ccd_inspect_side_1 = value; }
        }
        private IHalCamera inspection_ccd_inspect_top_1;
        [MachineManifest(DeviceEnum.inspection_ccd_inspect_top_1)]
        public IHalCamera Inspection_ccd_inspect_top_1
        {
            get { return inspection_ccd_inspect_top_1; }
            set { inspection_ccd_inspect_top_1 = value; }
        }


        private IHalLaser inspection_laser_entry_1;
        [MachineManifest(DeviceEnum.inspection_laser_entry_1)]
        public IHalLaser Inspection_laser_entry_1
        {
            get { return inspection_laser_entry_1; }
            set { inspection_laser_entry_1 = value; }
        }

        private IHalLaser inspection_laser_entry_2;
        [MachineManifest(DeviceEnum.inspection_laser_entry_2)]
        public IHalLaser Inspection_laser_entry_2
        {
            get { return inspection_laser_entry_2; }
            set { inspection_laser_entry_2 = value; }
        }


        private IHalLight inspection_lightbar_1;
        [MachineManifest(DeviceEnum.inspection_lightbar_1)]
        public IHalLight Inspection_lightbar_1
        {
            get { return inspection_lightbar_1; }
            set { inspection_lightbar_1 = value; }
        }


        private IHalLight inspection_linesource_1;
        [MachineManifest(DeviceEnum.inspection_linesource_1)]
        public IHalLight Inspection_linesource_1
        {
            get { return inspection_linesource_1; }
            set { inspection_linesource_1 = value; }
        }

        private IHalLight inspection_linesource_2;
        [MachineManifest(DeviceEnum.inspection_linesource_2)]
        public IHalLight Inspection_linesource_2
        {
            get { return inspection_linesource_2; }
            set { inspection_linesource_2 = value; }
        }

        private IHalLight inspection_linesource_3;
        [MachineManifest(DeviceEnum.inspection_linesource_3)]
        public IHalLight Inspection_linesource_3
        {
            get { return inspection_linesource_3; }
            set { inspection_linesource_3 = value; }
        }


        private IHalLight inspection_linesource_4;
        [MachineManifest(DeviceEnum.inspection_linesource_4)]
        public IHalLight Inspection_linesource_4
        {
            get { return inspection_linesource_4; }
            set { inspection_linesource_4 = value; }
        }

        private IHalParticleCounter inspection_particle_counter_1;
        [MachineManifest(DeviceEnum.inspection_particle_counter_1)]
        public IHalParticleCounter Inspection_particle_counter_1
        {
            get { return inspection_particle_counter_1; }
            set { inspection_particle_counter_1 = value; }
        }
        private IHalLight inspection_ringlight_1;
        [MachineManifest(DeviceEnum.inspection_ringlight_1)]
        public IHalLight Inspection_ringlight_1
        {
            get { return inspection_ringlight_1; }
            set { inspection_ringlight_1 = value; }
        }

        private IHalInspectionStage inspection_stage_1;
        [MachineManifest(DeviceEnum.inspection_stage_1)]
        public IHalInspectionStage Inspection_stage_1
        {
            get { return inspection_stage_1; }
            set { inspection_stage_1 = value; }
        }
        
        
        
        
        
        
        
        #endregion Device Components

        #region HAL Interface Functions
        public int HalStop()
        {
            throw new NotImplementedException();
        }
        #endregion HAL Interface Functions
    }
}
