﻿using MvAssistant.Mac.v1_0.Hal.Assembly;
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
    [GuidAttribute("F6FF301E-55DB-4861-AF19-A90F5A70FDA6")]
    public class MacHalInspectionCh : MacHalAssemblyBase, IMacHalInspectionCh
    {
        #region Device Components

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

   

  
    }
}
