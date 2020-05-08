using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Hal.Component;
using MvAssistant.Mac.v1_0.Hal.Component.AutoSwitch;
using MvAssistant.Mac.v1_0.Hal.Component.Camera;
using MvAssistant.Mac.v1_0.Hal.Component.FiberOptic;
using MvAssistant.Mac.v1_0.Hal.Component.Motor;
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
    [GuidAttribute("66F97306-5CB8-4188-B835-CDC87DF1EF23")]
    public class MacHalOpenStage : MacHalAssemblyBase, IMacHalOpenStage
    {


        #region Device Components


        public IMacHalPlcOpenStage Plc { get { return (IMacHalPlcOpenStage)this.GetMachine(MacEnumDevice.openstage_plc); } }

        public IHalCamera Openstage_ccd_side_1 { get { return (IHalCamera)this.GetMachine(MacEnumDevice.openstage_ccd_side_1); } }
        public IHalCamera Openstage_ccd_top_1 { get { return (IHalCamera)this.GetMachine(MacEnumDevice.openstage_ccd_top_1); } }
        public IHalCamera Openstage_ccd_front_1 { get { return (IHalCamera)this.GetMachine(MacEnumDevice.openstage_ccd_front_1); } }
        public IHalCamera Openstage_ccd_barcode_1 { get { return (IHalCamera)this.GetMachine(MacEnumDevice.openstage_ccd_barcode_1); } }
        public IHalLight Openstage_lightbar_top_1 { get { return (IHalLight)this.GetMachine(MacEnumDevice.openstage_lightbar_top_1); } }
        public IHalLight Openstage_lightbar_barcode_1 { get { return (IHalLight)this.GetMachine(MacEnumDevice.openstage_lightbar_barcode_1); } }
        public IHalParticleCounter Openstage_particle_counter_1 { get { return (IHalParticleCounter)this.GetMachine(MacEnumDevice.openstage_particle_counter_1); } }
        public IHalFiberOptic Openstage_fiber_optic_open_box_1 { get { return (IHalFiberOptic)this.GetMachine(MacEnumDevice.openstage_fiber_optic_open_box_1); } }
        public IHalFiberOptic Openstage_fiber_optic_box_category_1 { get { return (IHalFiberOptic)this.GetMachine(MacEnumDevice.openstage_fiber_optic_box_category_1); } }
        public IHalCylinder Openstage_box_holder_1 { get { return (IHalCylinder)this.GetMachine(MacEnumDevice.openstage_box_holder_1); } }
        public IHalCylinder Openstage_box_holder_2 { get { return (IHalCylinder)this.GetMachine(MacEnumDevice.openstage_box_holder_2); } }
        public IHalAutoSwitch Openstage_auto_switch_1 { get { return (IHalAutoSwitch)this.GetMachine(MacEnumDevice.openstage_auto_switch_1); } }
        public IHalAutoSwitch Openstage_auto_switch_2 { get { return (IHalAutoSwitch)this.GetMachine(MacEnumDevice.openstage_auto_switch_2); } }


        #endregion Device Components

        /// <summary>
        /// 開盒
        /// </summary>
        /// <returns></returns>
        public string OSOpen()
        {
            string result = "";
            result=Plc.Open();
            return result;
        }

        public string OSClose()
        {
            string result = "";
            result = Plc.Close();
            return result;
        }

        public string OSClamp()
        {
            string result = "";
            result = Plc.Clamp();
            return result;
        }

        public string OSUnclamp()
        {
            string result = "";
            result = Plc.Unclamp();
            return result;
        }

        public string OSSortClamp()
        {
            string result = "";
            result = Plc.SortClamp();
            return result;
        }

        public string OSSortUnclamp()
        {
            string result = "";
            result = Plc.SortUnclamp();
            return result;
        }

        public string OSLock()
        {
            string result = "";
            result = Plc.Lock();
            return result;
        }

        public string OSVacuum(bool isSuck)
        {
            string result = "";
            result = Plc.Vacuum(isSuck);
            return result;
        }

        public string OSInitial()
        {
            string result = "";
            result = Plc.Initial();
            return result;
        }

    }
}
