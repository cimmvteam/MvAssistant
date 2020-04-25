using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Hal.Component;
using MvAssistant.Mac.v1_0.Hal.Component.AutoSwitch;
using MvAssistant.Mac.v1_0.Hal.Component.Camera;
using MvAssistant.Mac.v1_0.Hal.Component.FiberOptic;
using MvAssistant.Mac.v1_0.Hal.Component.Motor;

using MvAssistant.Mac.v1_0.Manifest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.AssemblyFake
{    
    [MacMachineManifest(MacEnumDevice.openstage_assembly)]
    [GuidAttribute("C6D0645E-6112-416F-B694-47D504356717")]
    public class MacHalOpenStageFake : HalFakeBase, IMacHalOpenStage
    {
        #region Device Components (請看範例說明)
      
        private IHalCamera openstage_ccd_side_1;
        [MacMachineManifest(MacEnumDevice.openstage_ccd_side_1)]
        public IHalCamera Openstage_ccd_side_1
        {
            get { return openstage_ccd_side_1; }
            set { openstage_ccd_side_1 = value; }
        }

        private IHalCamera openstage_ccd_top_1;
        [MacMachineManifest(MacEnumDevice.openstage_ccd_top_1)]
        public IHalCamera Openstage_ccd_top_1
        {
            get { return openstage_ccd_top_1; }
            set { openstage_ccd_top_1 = value; }
        }
        private IHalCamera openstage_ccd_front_1;
        [MacMachineManifest(MacEnumDevice.openstage_ccd_front_1)]
        public IHalCamera Openstage_ccd_front_1
        {
            get { return openstage_ccd_front_1; }
            set { openstage_ccd_front_1 = value; }
        }
        private IHalCamera openstage_ccd_barcode_1;
        [MacMachineManifest(MacEnumDevice.openstage_ccd_barcode_1)]
        public IHalCamera Openstage_ccd_barcode_1
        {
            get { return openstage_ccd_barcode_1; }
            set { openstage_ccd_barcode_1 = value; }
        }
        private IHalLight openstage_lightbar_top_1;
        [MacMachineManifest(MacEnumDevice.openstage_lightbar_top_1)]
        public IHalLight Openstage_lightbar_top_1
        {
            get { return openstage_lightbar_top_1; }
            set { openstage_lightbar_top_1 = value; }
        }
        private IHalLight openstage_lightbar_barcode_1;
        [MacMachineManifest(MacEnumDevice.openstage_lightbar_barcode_1)]
        public IHalLight Openstage_lightbar_barcode_1
        {
            get { return openstage_lightbar_barcode_1; }
            set { openstage_lightbar_barcode_1 = value; }
        }
        private IHalParticleCounter openstage_particle_counter_1;
        [MacMachineManifest(MacEnumDevice.openstage_particle_counter_1)]
        public IHalParticleCounter Openstage_particle_counter_1
        {
            get { return openstage_particle_counter_1; }
            set { openstage_particle_counter_1 = value; }
        }
        private IHalFiberOptic openstage_fiber_optic_open_box_1;
        [MacMachineManifest(MacEnumDevice.openstage_fiber_optic_open_box_1)]
        public IHalFiberOptic Openstage_fiber_optic_open_box_1
        {
            get { return openstage_fiber_optic_open_box_1; }
            set { openstage_fiber_optic_open_box_1 = value; }
        }
        private IHalFiberOptic openstage_fiber_optic_box_category_1;
        [MacMachineManifest(MacEnumDevice.openstage_fiber_optic_box_category_1)]
        public IHalFiberOptic Openstage_fiber_optic_box_category_1
        {
            get { return openstage_fiber_optic_box_category_1; }
            set { openstage_fiber_optic_box_category_1 = value; }
        }
        private IHalCylinder openstage_box_holder_1;
        [MacMachineManifest(MacEnumDevice.openstage_box_holder_1)]
        public IHalCylinder Openstage_box_holder_1
        {
            get { return openstage_box_holder_1; }
            set { openstage_box_holder_1 = value; }
        }
        private IHalCylinder openstage_box_holder_2;
        [MacMachineManifest(MacEnumDevice.openstage_box_holder_2)]
        public IHalCylinder Openstage_box_holder_2
        {
            get { return openstage_box_holder_2; }
            set { openstage_box_holder_2 = value; }
        }
        private IHalAutoSwitch openstage_auto_switch_1;
        [MacMachineManifest(MacEnumDevice.openstage_auto_switch_1)]
        public IHalAutoSwitch Openstage_auto_switch_1
        {
            get { return openstage_auto_switch_1; }
            set { openstage_auto_switch_1 = value; }
        }
        private IHalAutoSwitch openstage_auto_switch_2;
        [MacMachineManifest(MacEnumDevice.openstage_auto_switch_2)]
        public IHalAutoSwitch Openstage_auto_switch_2
        {
            get { return openstage_auto_switch_2; }
            set { openstage_auto_switch_2 = value; }
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
