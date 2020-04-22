using MvAssistant.Mac.v1_0.Hal.Component;
using MvAssistant.Mac.v1_0.Hal.Component.AirPressure;
using MvAssistant.Mac.v1_0.Hal.Component.Camera;
using MvAssistant.Manifest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Assembly
{
    [GuidAttribute("D86FA618-FDA6-4B8C-B8D2-E1FBF5F824A6")]
    public interface IHalCleanChamber : IHalAssembly
    {
        IHalPressureCtrl Clean_air_pressure_controller_1 { get; set; }
        IHalDiffPressure Clean_air_pressure_diff_sensor_1 { get; set; }
        IHalPressureSensor Clean_air_pressure_sensor_1 { get; set; }
        IHalCamera Clean_ccd_particle_1 { get; set; }
        IHalGasValve Clean_gas_valve_1 { get; set; }
        IHalIonizer Clean_iozonier_1 { get; set; }
        IHalLaser Clean_laser_entry_1 { get; set; }
        IHalLaser Clean_laser_entry_2 { get; set; }
        IHalLaser Clean_laser_prevent_collision_1 { get; set; }
        IHalLaser Clean_laser_prevent_collision_2 { get; set; }
        IHalLaser Clean_laser_prevent_collision_3 { get; set; }
        IHalLight Clean_linesource_1 { get; set; }
        IHalParticleCounter Clean_particle_counter_1 { get; set; }
    }
}
