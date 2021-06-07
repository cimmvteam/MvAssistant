using MvAssistant.v0_2.Mac.JSon.RobotTransferFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.Mac.Manifest
{
    public enum MacEnumDevice
    {
        /// 編碼格式 ABCDEF
        /// AB: Assembly 
        ///  11 -> Load Port
        ///  12 -> Inspection Chamber
        ///  13 -> Clean Chamber
        ///  14 -> Open Stage
        ///  15 -> Drawer
        ///  16 -> Mask Transfer
        ///  17 -> Box Transfer
        ///  18 -> PLC
        ///  19 -> Drawer(BoxSlot)
        ///  
        /// CD: Component
        ///  11 -> Stage
        ///  12 -> 紅外線Sensor
        ///  13 -> Laser
        ///  14 -> Camera
        ///  15 -> 光源
        ///  16 -> Particle Counter
        ///  17 -> Air Pressure
        ///  18 -> Ionizer
        ///  19 -> Fiber Optical
        ///  20 -> Cylinder
        ///  21 -> Auto Switch
        ///  22 -> Door
        ///  23 -> Slot
        ///  24 -> Robot
        ///  25 -> Force6Axis
        ///  26 -> Motor
        ///  27 -> Optical Ruler
        ///  28 -> Tactile Sensor
        ///  29 -> Gradienter
        ///  30 -> Vibration
        ///  31 -> Gripper
        ///  32 -> Light Interruption (光遮斷)
        ///  33 -> Robot Skin
        ///  34 -> Static Electricity
        ///  35 -> E84 Sensor
        ///  36 -> RFID/Barcode (identifier) Sensor
        ///  37 -> Plunger
        ///  38 -> Button
        ///  39 -> PresentDetector(在席檢知)
        ///  
        /// EF: 流水號, 從00開始, 如果有第二組sensor, 則為10
        ///  00 -> 第一組component
        ///  10 -> 第二組component
        ///  20 -> 第三組component
        ///  ...依此類推
        ///  如果超過10組
        ///  00 -> 第一組component
        ///  01 -> 第二組component
        ///  02 -> 第三組component
        ///  ...依此類推

        #region Box Transfer

        [MacManifestDeviceProgKpi("Box Transfer", "組件", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.Driver)]
        boxtransfer_assembly,
        [MacManifestDeviceProgKpi("Box Transfer", "PLC", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        boxtransfer_plc,

        [MacManifestDeviceProgKpi("Box Transfer", "雷射測距", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.Install)]
        boxtransfer_laser_1,

        [MacManifestDeviceProgKpi("Box Transfer", "夾爪Camera", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        boxtransfer_camera_gripper_1,

        /// <summary>
        /// DE_BT_03: 環形光
        /// </summary>
        [MacManifestDeviceProgKpi("Box Transfer", "夾爪環形光", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        boxtransfer_light_1,

        /// <summary>
        /// DE_BT_12: Robot
        /// </summary>
        [MacManifestDeviceProgKpi("Box Transfer", "Robot", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        boxtransfer_robot_1,

        /// <summary>
        /// DE_BT_01: 六軸力覺感測
        /// </summary>
        [MacManifestDeviceProgKpi("Box Transfer", "六軸力感", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.Driver)]
        boxtransfer_force_6axis_sensor_1,

        /// <summary>
        /// DE_BT_10: Tactile Sensor (for 開盒PIN腳)
        /// </summary>
        [MacManifestDeviceProgKpi("Box Transfer", "開盒觸感1", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.DontInstall)]
        boxtransfer_tactile_1,

        /// <summary>
        /// DE_BT_13: Tactile Sensor (for Gripper夾取Box)
        /// </summary>
        [MacManifestDeviceProgKpi("Box Transfer", "開盒觸感2", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.DontInstall)]
        boxtransfer_tactile_2,

        /// <summary>
        /// DE_BT_09: IC水平儀
        /// </summary>
        [MacManifestDeviceProgKpi("Box Transfer", "水平儀", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.Install)]
        boxtransfer_gradienter_1,

        /// <summary>
        /// DE_BT_07: Box Gripper (for 15,000 run)
        /// </summary>
        [MacManifestDeviceProgKpi("Box Transfer", "夾爪", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        boxtransfer_gripper_1,



        #endregion Box Transfer



        #region Cabinet
        /// <summary>
        /// DE_DR_ASMBLY: Cabinet Assembly 整機
        /// </summary>
        [MacManifestDeviceProgKpi("Cabinet", "組件", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.Driver)]
        cabinet_assembly,
        [MacManifestDeviceProgKpi("Cabinet", "PLC", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        cabinet_plc,


        cabinet_drawer,//無編號版
        [MacManifestDeviceProgKpi("Cabinet", "Drawer 1-1", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.DontInstall)]
        cabinet_drawer_01_01,
        [MacManifestDeviceProgKpi("Cabinet", "Drawer 1-2", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        cabinet_drawer_01_02,
        [MacManifestDeviceProgKpi("Cabinet", "Drawer 1-3", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        cabinet_drawer_01_03,
        [MacManifestDeviceProgKpi("Cabinet", "Drawer 1-4", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        cabinet_drawer_01_04,
        [MacManifestDeviceProgKpi("Cabinet", "Drawer 1-5", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.DontInstall)]
        cabinet_drawer_01_05,
        [MacManifestDeviceProgKpi("Cabinet", "Drawer 2-1", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.DontInstall)]
        cabinet_drawer_02_01,
        [MacManifestDeviceProgKpi("Cabinet", "Drawer 2-2", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        cabinet_drawer_02_02,
        [MacManifestDeviceProgKpi("Cabinet", "Drawer 2-3", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        cabinet_drawer_02_03,
        [MacManifestDeviceProgKpi("Cabinet", "Drawer 2-4", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        cabinet_drawer_02_04,
        [MacManifestDeviceProgKpi("Cabinet", "Drawer 2-5", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.DontInstall)]
        cabinet_drawer_02_05,
        [MacManifestDeviceProgKpi("Cabinet", "Drawer 3-1", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.DontInstall)]
        cabinet_drawer_03_01,
        [MacManifestDeviceProgKpi("Cabinet", "Drawer 3-2", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        cabinet_drawer_03_02,
        [MacManifestDeviceProgKpi("Cabinet", "Drawer 3-3", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        cabinet_drawer_03_03,
        [MacManifestDeviceProgKpi("Cabinet", "Drawer 3-4", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        cabinet_drawer_03_04,
        [MacManifestDeviceProgKpi("Cabinet", "Drawer 3-5", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.DontInstall)]
        cabinet_drawer_03_05,
        [MacManifestDeviceProgKpi("Cabinet", "Drawer 4-1", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.DontInstall)]
        cabinet_drawer_04_01,
        [MacManifestDeviceProgKpi("Cabinet", "Drawer 4-2", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        cabinet_drawer_04_02,
        [MacManifestDeviceProgKpi("Cabinet", "Drawer 4-3", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        cabinet_drawer_04_03,
        [MacManifestDeviceProgKpi("Cabinet", "Drawer 4-4", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        cabinet_drawer_04_04,
        [MacManifestDeviceProgKpi("Cabinet", "Drawer 4-5", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.DontInstall)]
        cabinet_drawer_04_05,
        [MacManifestDeviceProgKpi("Cabinet", "Drawer 5-1", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.DontInstall)]
        cabinet_drawer_05_01,
        [MacManifestDeviceProgKpi("Cabinet", "Drawer 5-2", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        cabinet_drawer_05_02,
        [MacManifestDeviceProgKpi("Cabinet", "Drawer 5-3", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        cabinet_drawer_05_03,
        [MacManifestDeviceProgKpi("Cabinet", "Drawer 5-4", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        cabinet_drawer_05_04,
        [MacManifestDeviceProgKpi("Cabinet", "Drawer 5-5", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.DontInstall)]
        cabinet_drawer_05_05,
        [MacManifestDeviceProgKpi("Cabinet", "Drawer 6-1", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.DontInstall)]
        cabinet_drawer_06_01,
        [MacManifestDeviceProgKpi("Cabinet", "Drawer 6-2", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        cabinet_drawer_06_02,
        [MacManifestDeviceProgKpi("Cabinet", "Drawer 6-3", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        cabinet_drawer_06_03,
        [MacManifestDeviceProgKpi("Cabinet", "Drawer 6-4", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        cabinet_drawer_06_04,
        [MacManifestDeviceProgKpi("Cabinet", "Drawer 6-5", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.DontInstall)]
        cabinet_drawer_06_05,
        [MacManifestDeviceProgKpi("Cabinet", "Drawer 7-1", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.DontInstall)]
        cabinet_drawer_07_01,
        [MacManifestDeviceProgKpi("Cabinet", "Drawer 7-2", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        cabinet_drawer_07_02,
        [MacManifestDeviceProgKpi("Cabinet", "Drawer 7-3", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        cabinet_drawer_07_03,
        [MacManifestDeviceProgKpi("Cabinet", "Drawer 7-4", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.DontInstall)]
        cabinet_drawer_07_04,
        [MacManifestDeviceProgKpi("Cabinet", "Drawer 7-5", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.DontInstall)]
        cabinet_drawer_07_05,

        #endregion



        #region Clean Chamber
        /// <summary>
        /// DE_CC_ASMBLY: Clean Chamber Assembly 整機
        /// </summary>
        [MacManifestDeviceProgKpi("Claen Ch.", "組件", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.Driver)]
        clean_assembly,
        [MacManifestDeviceProgKpi("Claen Ch.", "PLC", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        cleanch_plc,


        /// <summary>
        /// DE_CC_06: Particle Inspection CCD
        /// </summary>
        [MacManifestDeviceProgKpi("Claen Ch.", "Particle", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        clean_camera_particle_1,

        /// <summary>
        /// DE_CC_08: 線光源
        /// </summary>
        [MacManifestDeviceProgKpi("Claen Ch.", "線光源", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        cleanch_inspection_spot_light_001,

        /// <summary>
        /// DE_CC_04: Pressure Controller
        /// </summary>
        [MacManifestDeviceProgKpi("Claen Ch.", "線光源", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        clean_air_pressure_controller_1,

        /// <summary>
        /// DE_CC_23: Ionizer
        /// </summary>
        [MacManifestDeviceProgKpi("Claen Ch.", "離子靜電消除器", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.None)]
        clean_ionizer_1,

        #endregion Clean Chamber



        #region Inspection Chamber
        /// <summary>
        /// DE_IC_ASMBLY: Inspection Chamber Assembly 整機
        /// </summary>
        [MacManifestDeviceProgKpi("Inspection Ch.", "組件", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.Driver)]
        inspectionch_assembly,
        [MacManifestDeviceProgKpi("Inspection Ch.", "PLC", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        inspectionch_plc,

        /// <summary>
        /// DE_IC_02: Inspection_CCD (Side)
        /// </summary>
        [MacManifestDeviceProgKpi("Inspection Ch.", "Camera-Inspect-Side", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        inspectionch_camera_inspect_side_001,

        /// <summary>
        /// DE_IC_03: Inspection_CCD (Top)
        /// </summary>
        [MacManifestDeviceProgKpi("Inspection Ch.", "Camera-Inspect-Top", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.Install)]
        inspectionch_camera_inspect_top_001,

        /// <summary>
        /// DE_IC_15: Defense_CCD (Top)
        /// </summary>
        [MacManifestDeviceProgKpi("Inspection Ch.", "Camera-Defense-Side", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        inspectionch_camera_defense_side_001,

        /// <summary>
        /// DE_IC_21: Defense_CCD (Side)
        /// </summary>
        [MacManifestDeviceProgKpi("Inspection Ch.", "Camera-Defense-Top", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        inspectionch_camera_defense_top_001,

        [MacManifestDeviceProgKpi("Inspection Ch.", "Light-Circle-Defense-Top", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        inspectionch_light_circle_defense_top_001,
        [MacManifestDeviceProgKpi("Inspection Ch.", "Light-Spot-Inspect-Left", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        inspectionch_light_line_left_001,
        [MacManifestDeviceProgKpi("Inspection Ch.", "Light-Spot-Inspect-Back", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        inspectionch_light_line_back_001,
        [MacManifestDeviceProgKpi("Inspection Ch.", "Light-Circle-Inspect-Top", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        inspectionch_light_circle_inspection_top_001,
        [MacManifestDeviceProgKpi("Inspection Ch.", "Light-Bar-Env-Left", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        inspectionch_light_bar_left_001,
        [MacManifestDeviceProgKpi("Inspection Ch.", "Light-Bar-Env-Right", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        inspectionch_light_bar_right_001,


        /// <summary>
        /// DE_IC_10: Particle Counter
        /// </summary>
        [MacManifestDeviceProgKpi("Inspection Ch.", "Particle Counter", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.Install)]
        inspectionch_particle_counter_1,
        #endregion Inspection Chamber



        #region Load Port
        /// <summary>
        /// DE_LP_ASMBLY: Load Port Assembly 整機
        /// </summary>
        [MacManifestDeviceProgKpi("Load Port", "組件A", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.Driver)]
        loadportA_assembly,
        [MacManifestDeviceProgKpi("Load Port", "組件B", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.Driver)]
        loadportB_assembly,
        [MacManifestDeviceProgKpi("Load Port", "PLC", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        loadport_plc,



        [MacManifestDeviceProgKpi("Load Port", "Camera-Inspect-A", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        loadportA_camera_inspect,
        [MacManifestDeviceProgKpi("Load Port", "Camera-Inspect-B", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        loadportB_camera_inspect,
        [MacManifestDeviceProgKpi("Load Port", "Camera-Inspect-Barcode", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        loadport_camera_barcode_inspect,





        /// <summary>
        /// Use of inspection
        /// </summary>
        [MacManifestDeviceProgKpi("Load Port", "Light-Bar-Env1", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        loadport_light_bar_001,
        [MacManifestDeviceProgKpi("Load Port", "Light-Bar-Env2", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        loadport_light_bar_002,
        [MacManifestDeviceProgKpi("Load Port", "Light-Bar-Env3", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        loadport_light_bar_003,//用於Barcode Reader



        loadport_1,
        loadport_2,

        #endregion Load Port



        #region Mask Transfer
        /// <summary>
        /// DE_MT_ASMBLY: Mask Transfer Assembly 整機
        /// </summary>
        [MacManifestDeviceProgKpi("Mask Transfer", "組件", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.Driver)]
        masktransfer_assembly,
        [MacManifestDeviceProgKpi("Mask Transfer", "PLC", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        masktransfer_plc,


        [MacManifestDeviceProgKpi("Mask Transfer", "Barcode Reader", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.Driver)]
        masktransfer_barcode_reader_1,
        [MacManifestDeviceProgKpi("Mask Transfer", "Light-Bar-Barcode", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.Driver)]
        masktransfer_light_barcode_1,
        /// <summary>
        /// DE_MT_02: Robot
        /// </summary>
        [MacManifestDeviceProgKpi("Mask Transfer", "Robot", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        masktransfer_robot_1,

        /// <summary>
        /// DE_MT_03: 六軸力覺感測
        /// </summary>
        [MacManifestDeviceProgKpi("Mask Transfer", "六軸力感", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        masktransfer_force_6axis_sensor_1,


        /// <summary>
        /// DE_MT_10: IC水平儀
        /// </summary>
        [MacManifestDeviceProgKpi("Mask Transfer", "傾角儀", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.Driver)]
        masktransfer_inclinometer01,


        #endregion Mask Transfer



        #region Open Stage
        /// <summary>
        /// DE_OS_ASMBLY: Open Stage Assembly 整機
        /// </summary>
        [MacManifestDeviceProgKpi("Open Stage", "組件", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.Driver)]
        openstage_assembly,
        [MacManifestDeviceProgKpi("Open Stage", "PLC", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        openstage_plc,

        [MacManifestDeviceProgKpi("Open Stage", "Camera-Defense-Side", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        openstage_camera_side_1,
        [MacManifestDeviceProgKpi("Open Stage", "Camera-Defense-Top", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        openstage_camera_top_1,
        [MacManifestDeviceProgKpi("Open Stage", "Camera-Defense-Left", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        openstage_camera_left_1,
        [MacManifestDeviceProgKpi("Open Stage", "Camera-Defense-Right", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        openstage_camera_right_1,

        [MacManifestDeviceProgKpi("Open Stage", "Light-Bar-Defense-Top", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        openstage_light_bar_defense_top_001,
        [MacManifestDeviceProgKpi("Open Stage", "Light-Bar-Defense-Side", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        openstage_light_bar_defense_side_001,
        [MacManifestDeviceProgKpi("Open Stage", "Light-Bar-Defense-Front", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        openstage_light_bar_defense_front_001,

        /// <summary>
        /// DE_OS_08: Particle Counter
        /// </summary>
        [MacManifestDeviceProgKpi("Open Stage", "Particle Counter", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.Driver)]
        openstage_particle_counter_1,



        #endregion Open Stage



        #region Eqp

        [MacManifestDeviceProgKpi("EQP", "組件", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        eqp_assembly,
        [MacManifestDeviceProgKpi("EQP", "PLC", MacManifestDeviceProgKpiAttribute.EnumManifestDeviceProgStatus.SoftTest)]
        eqp_plc_01,

        #endregion Universal




    }

    public static class MacEnumDeviceExtends
    {



        public static MacEnumDeviceDrawerRange GetDrawerRange(this MacEnumDevice instance)
        {
            return new MacEnumDeviceDrawerRange();
        }

        public static BoxrobotTransferLocation ToBoxrobotTransferLocation(this MacEnumDevice instance)
        {
            var idRange = instance.GetDrawerRange();
            var drawerLocationRange = BoxrobotTransferLocation.Dontcare.GetDrawerRange();
            var diff = instance - idRange.StartID;
            var rtnV = drawerLocationRange.Start + diff;
            return rtnV;
        }

    }
}
