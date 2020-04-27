using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Hal.Component;
using MvAssistant.Mac.v1_0.Hal.Component.AirPressure;
using MvAssistant.Mac.v1_0.Hal.Component.Camera;
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
    [GuidAttribute("25B4A570-8696-4726-AB5A-CF22161DFA19")]
    public class MacHalCleanCh : MacHalAssemblyBase, IMacHalCleanCh
    {
        #region Device Components


        public IMacHalPlcCleanCh Plc { get { return (IMacHalPlcCleanCh)this.GetMachine(MacEnumDevice.cleanch_plc); } }


        public IHalPressureCtrl Clean_air_pressure_controller_1 { get { return (IHalPressureCtrl)this.GetMachine(MacEnumDevice.clean_air_pressure_controller_1); } }
        public IHalDiffPressure Clean_air_pressure_diff_sensor_1 { get { return (IHalDiffPressure)this.GetMachine(MacEnumDevice.clean_air_pressure_diff_sensor_1); } }
        public IHalPressureSensor Clean_air_pressure_sensor_1 { get { return (IHalPressureSensor)this.GetMachine(MacEnumDevice.clean_air_pressure_sensor_1); } }
        public IHalCamera Clean_ccd_particle_1 { get { return (IHalCamera)this.GetMachine(MacEnumDevice.clean_ccd_particle_1); } }
        public IHalGasValve Clean_gas_valve_1 { get { return (IHalGasValve)this.GetMachine(MacEnumDevice.clean_gas_valve_1); } }
        public IHalIonizer Clean_iozonier_1 { get { return (IHalIonizer)this.GetMachine(MacEnumDevice.clean_ionizer_1); } }
        public IHalLaser Clean_laser_entry_1 { get { return (IHalLaser)this.GetMachine(MacEnumDevice.clean_laser_entry_1); } }
        public IHalLaser Clean_laser_entry_2 { get { return (IHalLaser)this.GetMachine(MacEnumDevice.clean_laser_entry_2); } }
        public IHalLaser Clean_laser_prevent_collision_1 { get { return (IHalLaser)this.GetMachine(MacEnumDevice.clean_laser_prevent_collision_1); } }
        public IHalLaser Clean_laser_prevent_collision_2 { get { return (IHalLaser)this.GetMachine(MacEnumDevice.clean_laser_prevent_collision_2); } }
        public IHalLaser Clean_laser_prevent_collision_3 { get { return (IHalLaser)this.GetMachine(MacEnumDevice.clean_laser_prevent_collision_3); } }
        public IHalLight Clean_linesource_1 { get { return (IHalLight)this.GetMachine(MacEnumDevice.clean_linesource_1); } }
        public IHalParticleCounter Clean_particle_counter_1 { get { return (IHalParticleCounter)this.GetMachine(MacEnumDevice.clean_particle_counter_1); } }



        #endregion Device Components



    }
}