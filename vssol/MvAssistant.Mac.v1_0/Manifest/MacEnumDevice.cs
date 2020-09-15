using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Manifest
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
        /// <summary>
        /// DE_BT_ASMBLY: Box Transfer Assembly 整機
        /// </summary>
        boxtransfer_assembly,
        boxtransfer_plc,

        /// <summary>
        /// DE_BT_14: 雷射測距Sensor
        /// </summary>
        boxtransfer_laser_gripper_1,

        /// <summary>
        /// DE_BT_02: Robot CCD
        /// </summary>
        boxtransfer_camera_gripper_1,

        /// <summary>
        /// DE_BT_03: 環形光
        /// </summary>
        boxtransfer_ringlight_1,

        /// <summary>
        /// DE_BT_12: Robot
        /// </summary>
        boxtransfer_robot_1,

        /// <summary>
        /// DE_BT_01: 六軸力覺感測
        /// </summary>
        boxtransfer_force_6axis_sensor_1,

        /// <summary>
        /// DE_BT_10: Tactile Sensor (for 開盒PIN腳)
        /// </summary>
        boxtransfer_tactile_1,

        /// <summary>
        /// DE_BT_13: Tactile Sensor (for Gripper夾取Box)
        /// </summary>
        boxtransfer_tactile_2,

        /// <summary>
        /// DE_BT_09: IC水平儀
        /// </summary>
        boxtransfer_gradienter_1,

        /// <summary>
        /// DE_BT_07: Box Gripper (for 15,000 run)
        /// </summary>
        boxtransfer_gripper_1,

        /// <summary>
        /// DE_BT_15: Robot SkinSensor
        /// </summary>
        boxtransfer_robot_skin_1,


        boxtransfer_vibration,


        #endregion Box Transfer



        #region Cabinet
        /// <summary>
        /// DE_DR_ASMBLY: Cabinet Assembly 整機
        /// </summary>
        cabinet_assembly,
        cabinet_plc,

        cabinet_drawer,//無編號版
        cabinet_drawer_01_01,
        cabinet_drawer_01_02,
        cabinet_drawer_01_03,
        cabinet_drawer_01_04,
        cabinet_drawer_01_05,
        cabinet_drawer_02_01,
        cabinet_drawer_02_02,
        cabinet_drawer_02_03,
        cabinet_drawer_02_04,
        cabinet_drawer_02_05,
        cabinet_drawer_03_01,
        cabinet_drawer_03_02,
        cabinet_drawer_03_03,
        cabinet_drawer_03_04,
        cabinet_drawer_03_05,
        cabinet_drawer_04_01,
        cabinet_drawer_04_02,
        cabinet_drawer_04_03,
        cabinet_drawer_04_04,
        cabinet_drawer_04_05,
        cabinet_drawer_05_01,
        cabinet_drawer_05_02,
        cabinet_drawer_05_03,
        cabinet_drawer_05_04,
        cabinet_drawer_05_05,
        cabinet_drawer_06_01,
        cabinet_drawer_06_02,
        cabinet_drawer_06_03,
        cabinet_drawer_06_04,
        cabinet_drawer_06_05,
        cabinet_drawer_07_01,
        cabinet_drawer_07_02,
        cabinet_drawer_07_03,
        cabinet_drawer_07_04,
        cabinet_drawer_07_05,

        #endregion



        #region Clean Chamber
        /// <summary>
        /// DE_CC_ASMBLY: Clean Chamber Assembly 整機
        /// </summary>
        clean_assembly,
        cleanch_plc,

        /// <summary>
        /// DE_CC_33: Laser Sensor (Side)
        /// </summary>
        clean_laser_entry_1,

        /// <summary>
        /// DE_CC_34: Laser Sensor (Bottom)
        /// </summary>
        clean_laser_entry_2,

        /// <summary>
        /// DE_CC_35: 防碰撞 - 點雷射1
        /// </summary>
        clean_laser_prevent_collision_1,

        /// <summary>
        /// DE_CC_36: 防碰撞 - 點雷射2
        /// </summary>
        clean_laser_prevent_collision_2,

        /// <summary>
        /// DE_CC_37: 防碰撞 - 點雷射3
        /// </summary>
        clean_laser_prevent_collision_3,

        /// <summary>
        /// DE_CC_06: Particle Inspection CCD
        /// </summary>
        clean_camera_particle_1,

        /// <summary>
        /// DE_CC_08: 線光源
        /// </summary>
        cleanch_inspection_spot_light_001,

        /// <summary>
        /// DE_CC_16: PA Counter Sensor
        /// </summary>
        clean_particle_counter_1,

        /// <summary>
        /// DE_CC_04: Pressure Controller
        /// </summary>
        clean_air_pressure_controller_1,

        /// <summary>
        /// DE_CC_05: 氣體壓力計
        /// </summary>
        clean_air_pressure_sensor_1,

        /// <summary>
        /// DE_CC_17: 壓差計
        /// </summary>
        clean_air_pressure_diff_sensor_1,

        /// <summary>
        /// DE_CC_50: GasValve
        /// </summary>
        clean_gas_valve_1,

        /// <summary>
        /// DE_CC_23: Ionizer
        /// </summary>
        clean_ionizer_1,
        #endregion Clean Chamber



        #region Inspection Chamber
        /// <summary>
        /// DE_IC_ASMBLY: Inspection Chamber Assembly 整機
        /// </summary>
        inspection_assembly,
        inspectionch_plc,
        /// <summary>
        /// DE_IC_09: XYZR Stage
        /// </summary>
        inspection_stage_1,

        /// <summary>
        /// DE_IC_11: 雷射測距sensor (Side)
        /// </summary>
        inspection_laser_entry_1,

        /// <summary>
        /// DE_IC_20: 雷射測距sensor (Bottom)
        /// </summary>
        inspection_laser_entry_2,

        /// <summary>
        /// DE_IC_02: Inspection_CCD (Side)
        /// </summary>
        inspectionch_camera_inspect_side_1,

        /// <summary>
        /// DE_IC_03: Inspection_CCD (Top)
        /// </summary>
        inspection_camera_inspect_top_1,

        /// <summary>
        /// DE_IC_15: Defense_CCD (Top)
        /// </summary>
        inspection_camera_defense_side_1,

        /// <summary>
        /// DE_IC_21: Defense_CCD (Side)
        /// </summary>
        inspection_camera_defense_top_1,

        inspectionch_light_circle_defense_top_001,
        inspectionch_light_bar_inspection_side_001,
        inspectionch_light_bar_denfese_side_001,
        inspectionch_light_circle_inspection_top_001,
        inspectionch_light_spot_inspection_left_001,
        inspectionch_light_spot_inspection_right_001,




        /// <summary>
        /// DE_IC_10: Particle Counter
        /// </summary>
        inspection_particle_counter_1,
        #endregion Inspection Chamber



        #region Load Port
        /// <summary>
        /// DE_LP_ASMBLY: Load Port Assembly 整機
        /// </summary>
        loadportA_assembly,
        loadportB_assembly,
        loadport_plc,

        /// <summary>
        /// DE_LP_23: LoadPort Stage
        /// </summary>
        loadport_cell_001,

        /// <summary>
        /// DE_LP_01: Entry Point紅外線(Side)
        /// </summary>
        loadport_infrared_entry_1,

        /// <summary>
        /// DE_LP_02: Entry Point紅外線(Front)
        /// </summary>
        loadport_infrared_entry_2,

        /// <summary>
        /// DE_LP_10: Mask Row CCD (Front)
        /// </summary>
        loadport_ccd_front_1,

        /// <summary>
        /// DE_LP_11: Mask Pitch CCD (Side)
        /// </summary>
        loadport_ccd_side_1,

        /// <summary>
        /// DE_LP_12: Mask Direction CCD (Top)
        /// </summary>
        loadport_ccd_top_1,

        /// <summary>
        /// Use of inspection
        /// </summary>
        loadport_light_bar_001,

        /// <summary>
        /// DE_LP_25: Clamper
        /// </summary>
        loadport_clamper_1,


        /// <summary>
        /// DE_LP_26: POD RFID Reader
        /// </summary>
        loadport_rfid_reader_1,

        /// <summary>
        /// DE_LP_27: Plunger
        /// </summary>
        loadport_plunger_1,

        loadport_1,
        loadport_2,

        #endregion Load Port



        #region Mask Transfer
        /// <summary>
        /// DE_MT_ASMBLY: Mask Transfer Assembly 整機
        /// </summary>
        masktransfer_assembly,
        masktransfer_plc,
        /// <summary>
        /// DE_MT_48: Pellicle型變CCD載台
        /// </summary>
        masktransfer_stage_pellicle_deform_1,

        /// <summary>
        /// DE_MT_12: Pellicle型變CCD
        /// </summary>
        masktransfer_ccd_pellicle_deform_1,

        /// <summary>
        /// 
        /// </summary>
        masktransfer_ccd_barcode_reader_1,
        /// <summary>
        /// 
        /// </summary>
        masktransfer_light_barcode_1,
        /// <summary>
        /// DE_MT_02: Robot
        /// </summary>
        masktransfer_robot_1,

        /// <summary>
        /// DE_MT_03: 六軸力覺感測
        /// </summary>
        masktransfer_force_6axis_sensor_1,

        /// <summary>
        /// DE_MT_23: [Gripper] Stepper Motor (伺服馬達-1)
        /// </summary>
        masktransfer_gripper_01,

        /// <summary>
        /// DE_MT_24: [Gripper] Stepper Motor (伺服馬達-2)
        /// </summary>
        masktransfer_gripper_02,

        /// <summary>
        /// DE_MT_25: [Gripper] Stepper Motor (伺服馬達-3)
        /// </summary>
        masktransfer_gripper_03,

        /// <summary>
        /// DE_MT_26: [Gripper] Stepper Motor (伺服馬達-4)
        /// </summary>
        masktransfer_gripper_04,

        /// <summary>
        /// DE_MT_19: Tactile Sensor-1
        /// </summary>
        masktransfer_tactile_1,

        /// <summary>
        /// DE_MT_20: Tactile Sensor-2
        /// </summary>
        masktransfer_tactile_2,

        /// <summary>
        /// DE_MT_21: Tactile Sensor-3
        /// </summary>
        masktransfer_tactile_3,

        /// <summary>
        /// DE_MT_22: Tactile Sensor-4
        /// </summary>
        masktransfer_tactile_4,

        /// <summary>
        /// DE_MT_10: IC水平儀
        /// </summary>
        masktransfer_inclinometer01,


        /// <summary>
        /// DE_MT_39: 光遮斷Sensor
        /// </summary>
        masktransfer_light_interrupt_1,

        /// <summary>
        /// DE_MT_42: Robot Skin
        /// </summary>
        masktransfer_robot_skin_1,

        /// <summary>
        /// DE_MT_43: 靜電量測儀
        /// </summary>
        masktransfer_static_electricity_detector_1,
        #endregion Mask Transfer



        #region Open Stage
        /// <summary>
        /// DE_OS_ASMBLY: Open Stage Assembly 整機
        /// </summary>
        openstage_assembly,
        openstage_plc,

        /// <summary>
        /// DE_OS_01: Side CCD
        /// </summary>
        openstage_camera_side_1,

        /// <summary>
        /// DE_OS_03: Top CCD
        /// </summary>
        openstage_camera_top_1,

        /// <summary>
        /// DE_OS_16: Front CCD
        /// </summary>
        openstage_camera_front_1,

        /// <summary>
        /// DE_OS_20: Barcode CCD
        /// </summary>
        openstage_camera_barcode_1,

        /// <summary>
        /// DE_OS_18: Light Bar (Top)
        /// </summary>
        openstage_light_bar_defense_top_001,

        /// <summary>
        /// DE_OS_19: Light Bar (Barcode)
        /// </summary>
        openstage_light_bar_defense_side_001,

        /// <summary>
        /// DE_OS_08: Particle Counter
        /// </summary>
        openstage_particle_counter_1,

        /// <summary>
        /// DE_OS_12: 光纖單位 - 測開盒
        /// </summary>
        openstage_fiber_optic_open_box_1,

        /// <summary>
        /// DE_OS_25: 光纖單位 - 測Box種類
        /// </summary>
        openstage_fiber_optic_box_category_1,

        /// <summary>
        /// DE_OS_24: Open Stage底座固定器-1
        /// </summary>
        openstage_box_holder_1,

        /// <summary>
        /// DE_OS_28: Open Stage底座固定器-2
        /// </summary>
        openstage_box_holder_2,

        /// <summary>
        /// DE_OS_22: Auto Switch,NPN,三線式-1
        /// </summary>
        openstage_auto_switch_1,

        /// <summary>
        /// DE_OS_23: Auto Switch,NPN,三線式-2
        /// </summary>
        openstage_auto_switch_2,

        #endregion Open Stage



        #region Universal
        universal_assembly,
        universal_plc_01,

        #endregion Universal




    }
}
