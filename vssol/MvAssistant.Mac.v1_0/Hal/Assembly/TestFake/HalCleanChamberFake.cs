
using MaskAutoCleaner.Hal.Intf.Assembly;
using MaskAutoCleaner.Hal.Intf.Component;
using MaskAutoCleaner.Hal.Intf.Component.AirPressure;
using MaskAutoCleaner.Manifest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace MaskAutoCleaner.Hal.ImpFake.Assembly
{
    [MachineManifest(DeviceEnum.clean_assembly)]
    [GuidAttribute("E3CF5050-6C3A-4A0A-85D5-FB8C56456731")]
    public class HalCleanChamberFake : HalFakeBase, IHalCleanChamber
    {
        #region Device Components (請看範例說明)
        /// 範例說明
        /// private IHalCamera topCamera;
        /// private IHalCamera sideCamera;
        /// 
        /// [MachineManifest(DeviceEnum.loadport_ccd_top_1)]
        /// public IHalCamera TopCamera
        /// {
        ///     set { topCamera = value; }
        ///     get { return topCamera; }
        /// }
        /// 
        /// [MachineManifest(DeviceEnum.loadport_ccd_side_1)]
        /// public IHalCamera SideCamera
        /// {
        ///     set { sideCamera = value; }
        ///     get { return sideCamera; }
        /// }


        private IHalCamera clean_ccd_particle_1;
        [MachineManifest(DeviceEnum.clean_ccd_particle_1)]
        public IHalCamera Clean_ccd_particle_1 { get { return clean_ccd_particle_1; } set { clean_ccd_particle_1 = value; } }

        private IHalLaser clean_laser_entry_1;
        [MachineManifest(DeviceEnum.clean_laser_entry_1)]
        public IHalLaser Clean_laser_entry_1 { get { return clean_laser_entry_1; } set { clean_laser_entry_1 = value; } }

        private IHalLaser clean_laser_entry_2;
        [MachineManifest(DeviceEnum.clean_laser_entry_2)]
        public IHalLaser Clean_laser_entry_2 { get { return clean_laser_entry_2; } set { clean_laser_entry_2 = value; } }
        private IHalPressureCtrl clean_air_pressure_controller_1;
        [MachineManifest(DeviceEnum.clean_air_pressure_controller_1)]
        public IHalPressureCtrl Clean_air_pressure_controller_1
        {
            get { return clean_air_pressure_controller_1; }
            set { clean_air_pressure_controller_1 = value; }
        }

        private IHalPressureSensor clean_air_pressure_sensor_1;
        [MachineManifest(DeviceEnum.clean_air_pressure_sensor_1)]
        public IHalPressureSensor Clean_air_pressure_sensor_1
        {
            get { return clean_air_pressure_sensor_1; }
            set { clean_air_pressure_sensor_1 = value; }
        }

        private IHalDiffPressure clean_air_pressure_diff_sensor_1;
        [MachineManifest(DeviceEnum.clean_air_pressure_diff_sensor_1)]
        public IHalDiffPressure Clean_air_pressure_diff_sensor_1
        {
            get { return clean_air_pressure_diff_sensor_1; }
            set { clean_air_pressure_diff_sensor_1 = value; }
        }

        private IHalLight clean_linesource_1;
        [MachineManifest(DeviceEnum.clean_linesource_1)]
        public IHalLight Clean_linesource_1
        {
            get { return clean_linesource_1; }
            set { clean_linesource_1 = value; }
        }

        private IHalIonizer clean_iozonier_1;
        [MachineManifest(DeviceEnum.clean_ionizer_1)]
        public IHalIonizer Clean_iozonier_1
        {
            get { return clean_iozonier_1; }
            set { clean_iozonier_1 = value; }
        }

        private IHalParticleCounter clean_particle_counter_1;
        [MachineManifest(DeviceEnum.clean_particle_counter_1)]
        public IHalParticleCounter Clean_particle_counter_1
        {
            get { return clean_particle_counter_1; }
            set { clean_particle_counter_1 = value; }
        }

        private IHalLaser clean_laser_prevent_collision_1;
        [MachineManifest(DeviceEnum.clean_laser_prevent_collision_1)]
        public IHalLaser Clean_laser_prevent_collision_1
        {
            get { return clean_laser_prevent_collision_1; }
            set { clean_laser_prevent_collision_1 = value; }
        }

        private IHalLaser clean_laser_prevent_collision_2;
        [MachineManifest(DeviceEnum.clean_laser_prevent_collision_2)]
        public IHalLaser Clean_laser_prevent_collision_2
        {
            get { return clean_laser_prevent_collision_2; }
            set { clean_laser_prevent_collision_2 = value; }
        }

        private IHalLaser clean_laser_prevent_collision_3;
        [MachineManifest(DeviceEnum.clean_laser_prevent_collision_3)]
        public IHalLaser Clean_laser_prevent_collision_3
        {
            get { return clean_laser_prevent_collision_3; }
            set { clean_laser_prevent_collision_3 = value; }
        }


        private IHalGasValve clean_gas_valve_1;
        [MachineManifest(DeviceEnum.clean_gas_valve_1)]
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
