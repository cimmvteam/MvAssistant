using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Hal.Component;
using MvAssistant.Mac.v1_0.Hal.Component.AirPressure;
using MvAssistant.Mac.v1_0.Hal.Component.Camera;
using MvAssistant.Mac.v1_0.Manifest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Assembly
{
    [MacMachineManifest(MacEnumDevice.clean_assembly)]
    [GuidAttribute("25B4A570-8696-4726-AB5A-CF22161DFA19")]
    public class MacHalCleanCh : MacHalAssemblyBase, IMacHalCleanCh
    {
        #region Device Components (請看範例說明)


        [MacMachineManifest(MacEnumDevice.clean_air_pressure_controller_1)]
        public IHalPressureCtrl Clean_air_pressure_controller_1 { get; set; }
        [MacMachineManifest(MacEnumDevice.clean_air_pressure_diff_sensor_1)]
        public IHalDiffPressure Clean_air_pressure_diff_sensor_1 { get; set; }
        [MacMachineManifest(MacEnumDevice.clean_air_pressure_sensor_1)]
        public IHalPressureSensor Clean_air_pressure_sensor_1 { get; set; }
        [MacMachineManifest(MacEnumDevice.clean_ccd_particle_1)]
        public IHalCamera Clean_ccd_particle_1 { get; set; }
        [MacMachineManifest(MacEnumDevice.clean_gas_valve_1)]
        public IHalGasValve Clean_gas_valve_1 { get; set; }
        [MacMachineManifest(MacEnumDevice.clean_ionizer_1)]
        public IHalIonizer Clean_iozonier_1 { get; set; }
        [MacMachineManifest(MacEnumDevice.clean_laser_entry_1)]
        public IHalLaser Clean_laser_entry_1 { get; set; }
        [MacMachineManifest(MacEnumDevice.clean_laser_entry_2)]
        public IHalLaser Clean_laser_entry_2 { get; set; }
        [MacMachineManifest(MacEnumDevice.clean_laser_prevent_collision_1)]
        public IHalLaser Clean_laser_prevent_collision_1 { get; set; }
        [MacMachineManifest(MacEnumDevice.clean_laser_prevent_collision_2)]
        public IHalLaser Clean_laser_prevent_collision_2 { get; set; }
        [MacMachineManifest(MacEnumDevice.clean_laser_prevent_collision_3)]
        public IHalLaser Clean_laser_prevent_collision_3 { get; set; }
        [MacMachineManifest(MacEnumDevice.clean_linesource_1)]
        public IHalLight Clean_linesource_1 { get; set; }
        [MacMachineManifest(MacEnumDevice.clean_particle_counter_1)]
        public IHalParticleCounter Clean_particle_counter_1 { get; set; }



        #endregion Device Components


        #region HAL Interface Functions
        int IHal.HalClose()
        {
            throw new NotImplementedException();
        }

        int IHal.HalConnect()
        {
            throw new NotImplementedException();
        }

        bool IHal.HalIsConnected()
        {
            throw new NotImplementedException();
        }

        int IMacHalAssembly.HalStop()
        {
            throw new NotImplementedException();
        }
        #endregion HAL Interface Functions


    }
}