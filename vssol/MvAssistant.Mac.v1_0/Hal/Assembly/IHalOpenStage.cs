using MaskAutoCleaner.Hal.Intf.Component;
using MaskAutoCleaner.Hal.Intf.Component.Motor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Hal.Intf.Assembly
{    
    [GuidAttribute("BE05B67D-DB09-4358-81C2-D8DC9F76208A")]
    public interface IHalOpenStage : IHalAssembly
    {
        IHalAutoSwitch Openstage_auto_switch_1 { get; set; }
        IHalAutoSwitch Openstage_auto_switch_2 { get; set; }
        IHalCylinder Openstage_box_holder_1 { get; set; }
        IHalCylinder Openstage_box_holder_2 { get; set; }
        IHalCamera Openstage_ccd_barcode_1 { get; set; }
        IHalCamera Openstage_ccd_front_1 { get; set; }
        IHalCamera Openstage_ccd_side_1 { get; set; }
        IHalCamera Openstage_ccd_top_1 { get; set; }
        IHalFiberOptic Openstage_fiber_optic_box_category_1 { get; set; }
        IHalFiberOptic Openstage_fiber_optic_open_box_1 { get; set; }
        IHalLight Openstage_lightbar_barcode_1 { get; set; }
        IHalLight Openstage_lightbar_top_1 { get; set; }
        IHalParticleCounter Openstage_particle_counter_1 { get; set; }
    }
}
