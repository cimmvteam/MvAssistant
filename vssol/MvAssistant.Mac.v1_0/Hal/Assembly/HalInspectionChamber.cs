using MaskAutoCleaner.Hal.Intf.Assembly;
using MaskAutoCleaner.Hal.Intf.Component;
using MaskAutoCleaner.Hal.Intf.Component.Stage;
using MaskAutoCleaner.Manifest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Hal.Imp.Assembly
{
    [MachineManifest(DeviceEnum.inspection_assembly)]
    [GuidAttribute("F6FF301E-55DB-4861-AF19-A90F5A70FDA6")]
    public class HalInspectionChamber : HalAssemblyBase, IHalInspectionChamber
    {
        #region Device Components (請看範例說明)




        [MachineManifest(DeviceEnum.inspection_ccd_defense_side_1)]
        public IHalCamera Inspection_ccd_defense_side_1 { get; set; }
        [MachineManifest(DeviceEnum.inspection_ccd_defense_top_1)]
        public IHalCamera Inspection_ccd_defense_top_1 { get; set; }
        [MachineManifest(DeviceEnum.inspection_ccd_inspect_side_1)]
        public IHalCamera Inspection_ccd_inspect_side_1 { get; set; }
        [MachineManifest(DeviceEnum.inspection_ccd_inspect_top_1)]
        public IHalCamera Inspection_ccd_inspect_top_1 { get; set; }
        [MachineManifest(DeviceEnum.inspection_laser_entry_1)]
        public IHalLaser Inspection_laser_entry_1 { get; set; }
        [MachineManifest(DeviceEnum.inspection_laser_entry_2)]
        public IHalLaser Inspection_laser_entry_2 { get; set; }
        [MachineManifest(DeviceEnum.inspection_lightbar_1)]
        public IHalLight Inspection_lightbar_1 { get; set; }
        [MachineManifest(DeviceEnum.inspection_linesource_1)]
        public IHalLight Inspection_linesource_1 { get; set; }
        [MachineManifest(DeviceEnum.inspection_linesource_2)]
        public IHalLight Inspection_linesource_2 { get; set; }
        [MachineManifest(DeviceEnum.inspection_linesource_3)]
        public IHalLight Inspection_linesource_3 { get; set; }
        [MachineManifest(DeviceEnum.inspection_linesource_4)]
        public IHalLight Inspection_linesource_4 { get; set; }
        [MachineManifest(DeviceEnum.inspection_particle_counter_1)]
        public IHalParticleCounter Inspection_particle_counter_1 { get; set; }
        [MachineManifest(DeviceEnum.inspection_ringlight_1)]
        public IHalLight Inspection_ringlight_1 { get; set; }
        [MachineManifest(DeviceEnum.inspection_stage_1)]
        public IHalInspectionStage Inspection_stage_1 { get; set; }

        
        
        
        
        
        
        #endregion Device Components

        #region HAL Interface Functions
        int IHalAssembly.HalStop()
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
