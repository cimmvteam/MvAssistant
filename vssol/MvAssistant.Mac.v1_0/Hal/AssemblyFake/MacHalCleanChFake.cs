
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Hal.Component;
using MvAssistant.Mac.v1_0.Hal.Component.AirPressure;
using MvAssistant.Mac.v1_0.Hal.Component.Camera;
using MvAssistant.Mac.v1_0.Manifest;
using System;
using System.Runtime.InteropServices;

namespace MvAssistant.Mac.v1_0.Hal.AssemblyFake
{
    [MacMachineManifest(MacEnumDevice.clean_assembly)]
    [GuidAttribute("E3CF5050-6C3A-4A0A-85D5-FB8C56456731")]
    public class MacHalCleanChFake : HalFakeBase, IMacHalCleanCh
    {
        #region Device Components
      

        private IHalCamera clean_ccd_particle_1;
        [MacMachineManifest(MacEnumDevice.clean_ccd_particle_1)]
        public IHalCamera Clean_ccd_particle_1 { get { return clean_ccd_particle_1; } set { clean_ccd_particle_1 = value; } }

        private IHalLaser clean_laser_entry_1;
        [MacMachineManifest(MacEnumDevice.clean_laser_entry_1)]
        public IHalLaser Clean_laser_entry_1 { get { return clean_laser_entry_1; } set { clean_laser_entry_1 = value; } }

        private IHalLaser clean_laser_entry_2;
        [MacMachineManifest(MacEnumDevice.clean_laser_entry_2)]
        public IHalLaser Clean_laser_entry_2 { get { return clean_laser_entry_2; } set { clean_laser_entry_2 = value; } }
        private IHalPressureCtrl clean_air_pressure_controller_1;
        [MacMachineManifest(MacEnumDevice.clean_air_pressure_controller_1)]
        public IHalPressureCtrl Clean_air_pressure_controller_1
        {
            get { return clean_air_pressure_controller_1; }
            set { clean_air_pressure_controller_1 = value; }
        }

        private IHalPressureSensor clean_air_pressure_sensor_1;
        [MacMachineManifest(MacEnumDevice.clean_air_pressure_sensor_1)]
        public IHalPressureSensor Clean_air_pressure_sensor_1
        {
            get { return clean_air_pressure_sensor_1; }
            set { clean_air_pressure_sensor_1 = value; }
        }

        private IHalDiffPressure clean_air_pressure_diff_sensor_1;
        [MacMachineManifest(MacEnumDevice.clean_air_pressure_diff_sensor_1)]
        public IHalDiffPressure Clean_air_pressure_diff_sensor_1
        {
            get { return clean_air_pressure_diff_sensor_1; }
            set { clean_air_pressure_diff_sensor_1 = value; }
        }

        private IHalLight clean_linesource_1;
        [MacMachineManifest(MacEnumDevice.clean_linesource_1)]
        public IHalLight Clean_linesource_1
        {
            get { return clean_linesource_1; }
            set { clean_linesource_1 = value; }
        }

        private IHalIonizer clean_iozonier_1;
        [MacMachineManifest(MacEnumDevice.clean_ionizer_1)]
        public IHalIonizer Clean_iozonier_1
        {
            get { return clean_iozonier_1; }
            set { clean_iozonier_1 = value; }
        }

        private IHalParticleCounter clean_particle_counter_1;
        [MacMachineManifest(MacEnumDevice.clean_particle_counter_1)]
        public IHalParticleCounter Clean_particle_counter_1
        {
            get { return clean_particle_counter_1; }
            set { clean_particle_counter_1 = value; }
        }

        private IHalLaser clean_laser_prevent_collision_1;
        [MacMachineManifest(MacEnumDevice.clean_laser_prevent_collision_1)]
        public IHalLaser Clean_laser_prevent_collision_1
        {
            get { return clean_laser_prevent_collision_1; }
            set { clean_laser_prevent_collision_1 = value; }
        }

        private IHalLaser clean_laser_prevent_collision_2;
        [MacMachineManifest(MacEnumDevice.clean_laser_prevent_collision_2)]
        public IHalLaser Clean_laser_prevent_collision_2
        {
            get { return clean_laser_prevent_collision_2; }
            set { clean_laser_prevent_collision_2 = value; }
        }

        private IHalLaser clean_laser_prevent_collision_3;
        [MacMachineManifest(MacEnumDevice.clean_laser_prevent_collision_3)]
        public IHalLaser Clean_laser_prevent_collision_3
        {
            get { return clean_laser_prevent_collision_3; }
            set { clean_laser_prevent_collision_3 = value; }
        }


        private IHalGasValve clean_gas_valve_1;
        [MacMachineManifest(MacEnumDevice.clean_gas_valve_1)]
        public IHalGasValve Clean_gas_valve_1
        {
            get { return clean_gas_valve_1; }
            set { clean_gas_valve_1 = value; }
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
