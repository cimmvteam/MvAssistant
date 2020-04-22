using MvAssistant.Mac.v1_0.Hal.Component;
using MvAssistant.Mac.v1_0.Hal.Component.Camera;
using MvAssistant.Mac.v1_0.Hal.Component.Stage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Assembly
{    
    [GuidAttribute("42CBD958-4DB6-4C5A-852C-3265A3B7F794")]
    public interface IHalInspectionChamber : IHalAssembly
    {

        IHalCamera Inspection_ccd_defense_side_1 { get; set; }
        IHalCamera Inspection_ccd_defense_top_1 { get; set; }
        IHalCamera Inspection_ccd_inspect_side_1 { get; set; }
        IHalCamera Inspection_ccd_inspect_top_1 { get; set; }
        IHalLaser Inspection_laser_entry_1 { get; set; }
        IHalLaser Inspection_laser_entry_2 { get; set; }
        IHalLight Inspection_lightbar_1 { get; set; }
        IHalLight Inspection_linesource_1 { get; set; }
        IHalLight Inspection_linesource_2 { get; set; }
        IHalLight Inspection_linesource_3 { get; set; }
        IHalLight Inspection_linesource_4 { get; set; }
        IHalParticleCounter Inspection_particle_counter_1 { get; set; }
        IHalLight Inspection_ringlight_1 { get; set; }
        IHalInspectionStage Inspection_stage_1 { get; set; }
    }
}
