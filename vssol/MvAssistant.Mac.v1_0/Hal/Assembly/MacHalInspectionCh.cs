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

namespace MvAssistant.Mac.v1_0.Hal.Assembly
{
    [MacMachineManifest(MacEnumDevice.inspection_assembly)]
    [GuidAttribute("F6FF301E-55DB-4861-AF19-A90F5A70FDA6")]
    public class MacHalInspectionCh : MacHalAssemblyBase, IMacHalInspectionCh
    {
        #region Device Components




        [MacMachineManifest(MacEnumDevice.inspection_ccd_defense_side_1)]
        public IHalCamera Inspection_ccd_defense_side_1 { get; set; }
        [MacMachineManifest(MacEnumDevice.inspection_ccd_defense_top_1)]
        public IHalCamera Inspection_ccd_defense_top_1 { get; set; }
        [MacMachineManifest(MacEnumDevice.inspection_ccd_inspect_side_1)]
        public IHalCamera Inspection_ccd_inspect_side_1 { get; set; }
        [MacMachineManifest(MacEnumDevice.inspection_ccd_inspect_top_1)]
        public IHalCamera Inspection_ccd_inspect_top_1 { get; set; }
        [MacMachineManifest(MacEnumDevice.inspection_laser_entry_1)]
        public IHalLaser Inspection_laser_entry_1 { get; set; }
        [MacMachineManifest(MacEnumDevice.inspection_laser_entry_2)]
        public IHalLaser Inspection_laser_entry_2 { get; set; }
        [MacMachineManifest(MacEnumDevice.inspection_lightbar_1)]
        public IHalLight Inspection_lightbar_1 { get; set; }
        [MacMachineManifest(MacEnumDevice.inspection_linesource_1)]
        public IHalLight Inspection_linesource_1 { get; set; }
        [MacMachineManifest(MacEnumDevice.inspection_linesource_2)]
        public IHalLight Inspection_linesource_2 { get; set; }
        [MacMachineManifest(MacEnumDevice.inspection_linesource_3)]
        public IHalLight Inspection_linesource_3 { get; set; }
        [MacMachineManifest(MacEnumDevice.inspection_linesource_4)]
        public IHalLight Inspection_linesource_4 { get; set; }
        [MacMachineManifest(MacEnumDevice.inspection_particle_counter_1)]
        public IHalParticleCounter Inspection_particle_counter_1 { get; set; }
        [MacMachineManifest(MacEnumDevice.inspection_ringlight_1)]
        public IHalLight Inspection_ringlight_1 { get; set; }
        [MacMachineManifest(MacEnumDevice.inspection_stage_1)]
        public IHalInspectionStage Inspection_stage_1 { get; set; }

        
        
        
        
        
        
        #endregion Device Components

        #region HAL Interface Functions
        int IMacHalAssembly.HalStop()
        {
            throw new NotImplementedException();
        }

        int IHal.HalConnect()
        {
            throw new NotImplementedException();
        }

        int IHal.HalClose()
        {
            throw new NotImplementedException();
        }

        bool IHal.HalIsConnected()
        {
            throw new NotImplementedException();
        }
        #endregion HAL Interface Functions

  
    }
}
