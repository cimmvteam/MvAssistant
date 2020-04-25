using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Hal.Component;
using MvAssistant.Mac.v1_0.Hal.Component.Camera;
using MvAssistant.Mac.v1_0.Hal.Component.Stage;

using MvAssistant.Mac.v1_0.Manifest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.AssemblyFake
{    
    [MacMachineManifest(MacEnumDevice.inspection_assembly)]
    [GuidAttribute("58130CF0-A31E-4502-87A4-EB43FF78DFE8")]
    public class MacHalInspectionChFake : HalFakeBase, IMacHalInspectionCh
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
        [MacMachineManifest(MacEnumDevice.inspection_ccd_defense_side_1)]
        public IHalCamera Inspection_ccd_defense_side_1
        {
            get { return inspection_ccd_defense_side_1; }
            set { inspection_ccd_defense_side_1 = value; }
        }

        private IHalCamera inspection_ccd_defense_top_1;
        [MacMachineManifest(MacEnumDevice.inspection_ccd_defense_top_1)]
        public IHalCamera Inspection_ccd_defense_top_1
        {
            get { return inspection_ccd_defense_top_1; }
            set { inspection_ccd_defense_top_1 = value; }
        }

        private IHalCamera inspection_ccd_inspect_side_1;
        [MacMachineManifest(MacEnumDevice.inspection_ccd_inspect_side_1)]
        public IHalCamera Inspection_ccd_inspect_side_1
        {
            get { return inspection_ccd_inspect_side_1; }
            set { inspection_ccd_inspect_side_1 = value; }
        }
        private IHalCamera inspection_ccd_inspect_top_1;
        [MacMachineManifest(MacEnumDevice.inspection_ccd_inspect_top_1)]
        public IHalCamera Inspection_ccd_inspect_top_1
        {
            get { return inspection_ccd_inspect_top_1; }
            set { inspection_ccd_inspect_top_1 = value; }
        }


        private IHalLaser inspection_laser_entry_1;
        [MacMachineManifest(MacEnumDevice.inspection_laser_entry_1)]
        public IHalLaser Inspection_laser_entry_1
        {
            get { return inspection_laser_entry_1; }
            set { inspection_laser_entry_1 = value; }
        }

        private IHalLaser inspection_laser_entry_2;
        [MacMachineManifest(MacEnumDevice.inspection_laser_entry_2)]
        public IHalLaser Inspection_laser_entry_2
        {
            get { return inspection_laser_entry_2; }
            set { inspection_laser_entry_2 = value; }
        }


        private IHalLight inspection_lightbar_1;
        [MacMachineManifest(MacEnumDevice.inspection_lightbar_1)]
        public IHalLight Inspection_lightbar_1
        {
            get { return inspection_lightbar_1; }
            set { inspection_lightbar_1 = value; }
        }


        private IHalLight inspection_linesource_1;
        [MacMachineManifest(MacEnumDevice.inspection_linesource_1)]
        public IHalLight Inspection_linesource_1
        {
            get { return inspection_linesource_1; }
            set { inspection_linesource_1 = value; }
        }

        private IHalLight inspection_linesource_2;
        [MacMachineManifest(MacEnumDevice.inspection_linesource_2)]
        public IHalLight Inspection_linesource_2
        {
            get { return inspection_linesource_2; }
            set { inspection_linesource_2 = value; }
        }

        private IHalLight inspection_linesource_3;
        [MacMachineManifest(MacEnumDevice.inspection_linesource_3)]
        public IHalLight Inspection_linesource_3
        {
            get { return inspection_linesource_3; }
            set { inspection_linesource_3 = value; }
        }


        private IHalLight inspection_linesource_4;
        [MacMachineManifest(MacEnumDevice.inspection_linesource_4)]
        public IHalLight Inspection_linesource_4
        {
            get { return inspection_linesource_4; }
            set { inspection_linesource_4 = value; }
        }

        private IHalParticleCounter inspection_particle_counter_1;
        [MacMachineManifest(MacEnumDevice.inspection_particle_counter_1)]
        public IHalParticleCounter Inspection_particle_counter_1
        {
            get { return inspection_particle_counter_1; }
            set { inspection_particle_counter_1 = value; }
        }
        private IHalLight inspection_ringlight_1;
        [MacMachineManifest(MacEnumDevice.inspection_ringlight_1)]
        public IHalLight Inspection_ringlight_1
        {
            get { return inspection_ringlight_1; }
            set { inspection_ringlight_1 = value; }
        }

        private IHalInspectionStage inspection_stage_1;
        [MacMachineManifest(MacEnumDevice.inspection_stage_1)]
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
