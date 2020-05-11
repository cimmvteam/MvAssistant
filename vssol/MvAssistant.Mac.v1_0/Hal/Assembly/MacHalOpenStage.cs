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
        public string Open()
        {
            string result = "";
            result = Plc.Open();
            return result;
        }

        public string Close()
        {
            string result = "";
            result = Plc.Close();
            return result;
        }

        public string Clamp()
        {
            string result = "";
            result = Plc.Clamp();
            return result;
        }

        public string Unclamp()
        {
            string result = "";
            result = Plc.Unclamp();
            return result;
        }

        public string SortClamp()
        {
            string result = "";
            result = Plc.SortClamp();
            return result;
        }

        public string SortUnclamp()
        {
            string result = "";
            result = Plc.SortUnclamp();
            return result;
        }

        public string Lock()
        {
            string result = "";
            result = Plc.Lock();
            return result;
        }

        public string Vacuum(bool isSuck)
        {
            string result = "";
            result = Plc.Vacuum(isSuck);
            return result;
        }

        public string Initial()
        {
            string result = "";
            result = Plc.Initial();
            return result;
        }

        public string ReadOpenStageStatus()
        { return Plc.ReadOpenStageStatus(); }

        #region Set Parameter
        /// <summary>
        /// 設定盒子種類，1：鐵盒 , 2：水晶盒
        /// </summary>
        /// <param name="BoxType">1：鐵盒 , 2：水晶盒</param>
        public void SetBoxType(uint BoxType)
        { Plc.SetBoxType(BoxType); }

        /// <summary>
        /// 設定速度(%)
        /// </summary>
        /// <param name="Speed">(%)</param>
        public void SetSpeed(uint Speed)
        { Plc.SetSpeed(Speed); }
        #endregion

        #region Read Parameter

        public int ReadBoxTypeSetting()
        { return Plc.ReadBoxTypeSetting(); }

        public uint ReadSpeedSetting()
        { return Plc.ReadSpeedSetting(); }
        #endregion

        #region Read Component Value

        /// <summary>
        /// 發送入侵訊號，確認Robot能否入侵
        /// </summary>
        /// <param name="isBTIntrude">BT Robot是否要入侵</param>
        /// <param name="isMTIntrude">MT Robot是否要入侵</param>
        /// <returns></returns>
        public Tuple<bool, bool> ReadRobotIntrude(bool isBTIntrude, bool isMTIntrude)
        { return Plc.ReadRobotIntrude(isBTIntrude, isMTIntrude); }

        /// <summary>
        /// 讀取開盒夾爪狀態
        /// </summary>
        /// <returns></returns>
        public string ReadClampStatus()
        { return Plc.ReadClampStatus(); }

        /// <summary>
        /// 讀取Stage上固定Box的夾具位置
        /// </summary>
        /// <returns></returns>
        public Tuple<long, long> ReadSortClampPosition()
        { return Plc.ReadSortClampPosition(); }

        /// <summary>
        /// 讀取Slider的位置
        /// </summary>
        /// <returns></returns>
        public Tuple<long, long> ReadSliderPosition()
        { return Plc.ReadSliderPosition(); }

        /// <summary>
        /// 讀取盒蓋位置
        /// </summary>
        /// <returns></returns>
        public Tuple<double, double> ReadCoverPos()
        { return Plc.ReadCoverPos(); }

        /// <summary>
        /// 讀取盒蓋開闔
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, bool> ReadCoverSensor()
        { return Plc.ReadCoverSensor(); }

        /// <summary>
        /// 讀取盒子是否變形
        /// </summary>
        /// <returns></returns>
        public double ReadBoxDeform()
        { return Plc.ReadBoxDeform(); }

        /// <summary>
        /// 讀取平台上的重量
        /// </summary>
        /// <returns></returns>
        public double ReadWeightOnStage()
        { return Plc.ReadWeightOnStage(); }

        /// <summary>
        /// 讀取是否有Box
        /// </summary>
        /// <returns></returns>
        public bool ReadBoxExist()
        { return Plc.ReadBoxExist(); }
        
        #endregion
    }
}
