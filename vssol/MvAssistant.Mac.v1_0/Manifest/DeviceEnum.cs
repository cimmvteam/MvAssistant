using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Manifest
{
    public enum DeviceEnum
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

        #region Load Port
        /// <summary>
        /// DE_LP_ASMBLY: Load Port Assembly 整機
        /// </summary>
        loadport_assembly = 110000,

        /// <summary>
        /// DE_LP_23: LoadPort Stage
        /// </summary>
        loadport_stage_1 = 111100,

        /// <summary>
        /// DE_LP_01: Entry Point紅外線(Side)
        /// </summary>
        loadport_infrared_entry_1 = 111200,

        /// <summary>
        /// DE_LP_02: Entry Point紅外線(Front)
        /// </summary>
        loadport_infrared_entry_2 = 111210,

        /// <summary>
        /// DE_LP_10: Mask Row CCD (Front)
        /// </summary>
        loadport_ccd_front_1 = 111400,

        /// <summary>
        /// DE_LP_11: Mask Pitch CCD (Side)
        /// </summary>
        loadport_ccd_side_1 = 111410,

        /// <summary>
        /// DE_LP_12: Mask Direction CCD (Top)
        /// </summary>
        loadport_ccd_top_1 = 111420,

        /// <summary>
        /// DE_LP_13: Light Bar
        /// </summary>
        loadport_lightbar_1 = 111500,

        /// <summary>
        /// DE_LP_25: Clamper
        /// </summary>
        loadport_clamper_1 = 112600,

        /// <summary>
        /// DE_LP_24: E84 Sensor
        /// </summary>
        loadport_e84_1 = 113500,

        /// <summary>
        /// DE_LP_26: POD RFID Reader
        /// </summary>
        loadport_rfid_reader_1 = 113600,

        /// <summary>
        /// DE_LP_27: Plunger
        /// </summary>
        loadport_plunger_1 = 113700,
        #endregion Load Port

        #region Inspection Chamber
        /// <summary>
        /// DE_IC_ASMBLY: Inspection Chamber Assembly 整機
        /// </summary>
        inspection_assembly = 120000,

        /// <summary>
        /// DE_IC_09: XYZR Stage
        /// </summary>
        inspection_stage_1 = 121100,

        /// <summary>
        /// DE_IC_11: 雷射測距sensor (Side)
        /// </summary>
        inspection_laser_entry_1 = 121300,

        /// <summary>
        /// DE_IC_20: 雷射測距sensor (Bottom)
        /// </summary>
        inspection_laser_entry_2 = 121310,

        /// <summary>
        /// DE_IC_02: Inspection_CCD (Side)
        /// </summary>
        inspection_ccd_inspect_side_1 = 121400,

        /// <summary>
        /// DE_IC_03: Inspection_CCD (Top)
        /// </summary>
        inspection_ccd_inspect_top_1 = 121410,

        /// <summary>
        /// DE_IC_15: Defense_CCD (Top)
        /// </summary>
        inspection_ccd_defense_side_1 = 121420,

        /// <summary>
        /// DE_IC_21: Defense_CCD (Side)
        /// </summary>
        inspection_ccd_defense_top_1 = 121430,

        /// <summary>
        /// DE_IC_05: 環形光
        /// </summary>
        inspection_ringlight_1 = 121500,

        /// <summary>
        /// DE_IC_06: Light Bar
        /// </summary>
        inspection_lightbar_1 = 121510,

        /// <summary>
        /// DE_IC_07: 線光源-1 (x,y)
        /// </summary>
        inspection_linesource_1 = 121520,

        /// <summary>
        /// DE_IC_17: 線光源-2 (-x,y)
        /// </summary>
        inspection_linesource_2 = 121530,

        /// <summary>
        /// DE_IC_18: 線光源-3 (-x,-y)
        /// </summary>
        inspection_linesource_3 = 121540,

        /// <summary>
        /// DE_IC_19: 線光源-4 (x,-y)
        /// </summary>
        inspection_linesource_4 = 121560,

        /// <summary>
        /// DE_IC_10: Particle Counter
        /// </summary>
        inspection_particle_counter_1 = 121600,
        #endregion Inspection Chamber

        #region Clean Chamber
        /// <summary>
        /// DE_CC_ASMBLY: Clean Chamber Assembly 整機
        /// </summary>
        clean_assembly = 130000,

        /// <summary>
        /// DE_CC_33: Laser Sensor (Side)
        /// </summary>
        clean_laser_entry_1 = 131300,

        /// <summary>
        /// DE_CC_34: Laser Sensor (Bottom)
        /// </summary>
        clean_laser_entry_2 = 131301,

        /// <summary>
        /// DE_CC_35: 防碰撞 - 點雷射1
        /// </summary>
        clean_laser_prevent_collision_1 = 131302,

        /// <summary>
        /// DE_CC_36: 防碰撞 - 點雷射2
        /// </summary>
        clean_laser_prevent_collision_2 = 131303,

        /// <summary>
        /// DE_CC_37: 防碰撞 - 點雷射3
        /// </summary>
        clean_laser_prevent_collision_3 = 131304,

        /// <summary>
        /// DE_CC_06: Particle Inspection CCD
        /// </summary>
        clean_ccd_particle_1 = 131400,

        /// <summary>
        /// DE_CC_08: 線光源
        /// </summary>
        clean_linesource_1 = 131500,

        /// <summary>
        /// DE_CC_16: PA Counter Sensor
        /// </summary>
        clean_particle_counter_1 = 131600,

        /// <summary>
        /// DE_CC_04: Pressure Controller
        /// </summary>
        clean_air_pressure_controller_1 = 131700,

        /// <summary>
        /// DE_CC_05: 氣體壓力計
        /// </summary>
        clean_air_pressure_sensor_1 = 131710,

        /// <summary>
        /// DE_CC_17: 壓差計
        /// </summary>
        clean_air_pressure_diff_sensor_1 = 131720,

        /// <summary>
        /// DE_CC_50: GasValve
        /// </summary>
        clean_gas_valve_1 = 131730,

        /// <summary>
        /// DE_CC_23: Ionizer
        /// </summary>
        clean_ionizer_1 = 131800,
        #endregion Clean Chamber

        #region Open Stage
        /// <summary>
        /// DE_OS_ASMBLY: Open Stage Assembly 整機
        /// </summary>
        openstage_assembly = 140000,

        /// <summary>
        /// DE_OS_01: Side CCD
        /// </summary>
        openstage_ccd_side_1 = 141400,

        /// <summary>
        /// DE_OS_03: Top CCD
        /// </summary>
        openstage_ccd_top_1 = 141410,

        /// <summary>
        /// DE_OS_16: Front CCD
        /// </summary>
        openstage_ccd_front_1 = 141420,

        /// <summary>
        /// DE_OS_20: Barcode CCD
        /// </summary>
        openstage_ccd_barcode_1 = 141430,

        /// <summary>
        /// DE_OS_18: Light Bar (Top)
        /// </summary>
        openstage_lightbar_top_1 = 141500,

        /// <summary>
        /// DE_OS_19: Light Bar (Barcode)
        /// </summary>
        openstage_lightbar_barcode_1 = 141510,

        /// <summary>
        /// DE_OS_08: Particle Counter
        /// </summary>
        openstage_particle_counter_1 = 141600,

        /// <summary>
        /// DE_OS_12: 光纖單位 - 測開盒
        /// </summary>
        openstage_fiber_optic_open_box_1 = 141900,

        /// <summary>
        /// DE_OS_25: 光纖單位 - 測Box種類
        /// </summary>
        openstage_fiber_optic_box_category_1 = 141910,

        /// <summary>
        /// DE_OS_24: Open Stage底座固定器-1
        /// </summary>
        openstage_box_holder_1 = 142000,

        /// <summary>
        /// DE_OS_28: Open Stage底座固定器-2
        /// </summary>
        openstage_box_holder_2 = 142010,

        /// <summary>
        /// DE_OS_22: Auto Switch,NPN,三線式-1
        /// </summary>
        openstage_auto_switch_1 = 142100,

        /// <summary>
        /// DE_OS_23: Auto Switch,NPN,三線式-2
        /// </summary>
        openstage_auto_switch_2 = 142110,
        #endregion Open Stage

        #region Cabinet
        /// <summary>
        /// DE_DR_ASMBLY: Cabinet Assembly 整機
        /// </summary>
        cabinet_assembly = 150000,
        //cabinet_inner_door_1 = 152200,
        //cabinet_inner_door_2 = 152201,
        //cabinet_inner_door_3 = 152202,
        //cabinet_inner_door_4 = 152203,
        //cabinet_inner_door_5 = 152204,
        //cabinet_inner_door_6 = 152205,

        //drawer_outer_door_1 = 152206,
        //drawer_outer_door_2 = 152207,
        //drawer_outer_door_3 = 152208,
        //drawer_outer_door_4 = 152209,
        //drawer_outer_door_5 = 152210,
        //drawer_outer_door_6 = 152211,
        //drawer_outer_door_7 = 152212,
        //drawer_outer_door_8 = 152213,
        //drawer_outer_door_9 = 152214,
        //drawer_outer_door_10 = 152215,
        //drawer_outer_door_11 = 152216,
        //drawer_outer_door_12 = 152217,
        //drawer_outer_door_13 = 152218,
        //drawer_outer_door_14 = 152219,
        //drawer_outer_door_15 = 152220,
        //drawer_outer_door_16 = 152221,
        //drawer_outer_door_17 = 152222,
        //drawer_outer_door_18 = 152223,
        //drawer_outer_door_19 = 152224,
        //drawer_outer_door_20 = 152225,
        //drawer_outer_door_21 = 152226,
        //drawer_outer_door_22 = 152227,
        //drawer_outer_door_23 = 152228,
        //drawer_outer_door_24 = 152229,
        //drawer_outer_door_25 = 152230,
        //drawer_outer_door_26 = 152231,
        //drawer_outer_door_27 = 152232,
        //drawer_outer_door_28 = 152233,
        //drawer_outer_door_29 = 152234,
        //drawer_outer_door_30 = 152235,

        //drawer_InPlace_1 = 153200,
        //drawer_InPlace_2 = 153201,
        //drawer_InPlace_3 = 153202,
        //drawer_InPlace_4 = 153203,
        //drawer_InPlace_5 = 153204,
        //drawer_InPlace_6 = 153205,
        //drawer_InPlace_7 = 153206,
        //drawer_InPlace_8 = 153207,
        //drawer_InPlace_9 = 153208,
        //drawer_InPlace_10 = 153209,
        //drawer_InPlace_11 = 153210,
        //drawer_InPlace_12 = 153211,
        //drawer_InPlace_13 = 153212,
        //drawer_InPlace_14 = 153213,
        //drawer_InPlace_15 = 153214,
        //drawer_InPlace_16 = 153215,
        //drawer_InPlace_17 = 153216,
        //drawer_InPlace_18 = 153217,
        //drawer_InPlace_19 = 153218,
        //drawer_InPlace_20 = 153219,
        //drawer_InPlace_21 = 153220,
        //drawer_InPlace_22 = 153221,
        //drawer_InPlace_23 = 153222,
        //drawer_InPlace_24 = 153223,
        //drawer_InPlace_25 = 153224,
        //drawer_InPlace_26 = 153225,
        //drawer_InPlace_27 = 153226,
        //drawer_InPlace_28 = 153227,
        //drawer_InPlace_29 = 153228,
        //drawer_InPlace_30 = 153229,
        #endregion

        #region Mask Transfer
        /// <summary>
        /// DE_MT_ASMBLY: Mask Transfer Assembly 整機
        /// </summary>
        masktransfer_assembly = 160000,

        /// <summary>
        /// DE_MT_48: Pellicle型變CCD載台
        /// </summary>
        masktransfer_stage_pellicle_deform_1 = 161100,

        /// <summary>
        /// DE_MT_12: Pellicle型變CCD
        /// </summary>
        masktransfer_ccd_pellicle_deform_1 = 161400,

        /// <summary>
        /// 
        /// </summary>
        masktransfer_ccd_barcode_reader_1 = 161500,
        /// <summary>
        /// 
        /// </summary>
        masktransfer_light_barcode_1 = 161600,
        /// <summary>
        /// DE_MT_02: Robot
        /// </summary>
        masktransfer_robot_1 = 162400,

        /// <summary>
        /// DE_MT_03: 六軸力覺感測
        /// </summary>
        masktransfer_force_6axis_sensor_1 = 162500,

        /// <summary>
        /// DE_MT_23: [Gripper] Stepper Motor (伺服馬達-1)
        /// </summary>
        masktransfer_gripper_01 = 162600,

        /// <summary>
        /// DE_MT_24: [Gripper] Stepper Motor (伺服馬達-2)
        /// </summary>
        masktransfer_gripper_02 = 162610,

        /// <summary>
        /// DE_MT_25: [Gripper] Stepper Motor (伺服馬達-3)
        /// </summary>
        masktransfer_gripper_03 = 162620,

        /// <summary>
        /// DE_MT_26: [Gripper] Stepper Motor (伺服馬達-4)
        /// </summary>
        masktransfer_gripper_04 = 162630,

        /// <summary>
        /// DE_MT_19: Tactile Sensor-1
        /// </summary>
        masktransfer_tactile_1 = 162800,

        /// <summary>
        /// DE_MT_20: Tactile Sensor-2
        /// </summary>
        masktransfer_tactile_2 = 162810,

        /// <summary>
        /// DE_MT_21: Tactile Sensor-3
        /// </summary>
        masktransfer_tactile_3 = 162820,

        /// <summary>
        /// DE_MT_22: Tactile Sensor-4
        /// </summary>
        masktransfer_tactile_4 = 162830,

        /// <summary>
        /// DE_MT_10: IC水平儀
        /// </summary>
        masktransfer_inclinometer01 = 162900,


        /// <summary>
        /// DE_MT_39: 光遮斷Sensor
        /// </summary>
        masktransfer_light_interrupt_1 = 163200,

        /// <summary>
        /// DE_MT_42: Robot Skin
        /// </summary>
        masktransfer_robot_skin_1 = 163300,

        /// <summary>
        /// DE_MT_43: 靜電量測儀
        /// </summary>
        masktransfer_static_electricity_detector_1 = 163400,
        #endregion Mask Transfer

        #region Box Transfer
        /// <summary>
        /// DE_BT_ASMBLY: Box Transfer Assembly 整機
        /// </summary>
        boxtransfer_assembly = 170000,

        /// <summary>
        /// DE_BT_14: 雷射測距Sensor
        /// </summary>
        boxtransfer_laser_gripper_1 = 171300,

        /// <summary>
        /// DE_BT_02: Robot CCD
        /// </summary>
        boxtransfer_ccd_gripper_1 = 171400,

        /// <summary>
        /// DE_BT_03: 環形光
        /// </summary>
        boxtransfer_ringlight_1 = 171500,

        /// <summary>
        /// DE_BT_12: Robot
        /// </summary>
        boxtransfer_robot_1 = 172400,

        /// <summary>
        /// DE_BT_01: 六軸力覺感測
        /// </summary>
        boxtransfer_force_6axis_sensor_1 = 172500,

        /// <summary>
        /// DE_BT_10: Tactile Sensor (for 開盒PIN腳)
        /// </summary>
        boxtransfer_tactile_1 = 172800,

        /// <summary>
        /// DE_BT_13: Tactile Sensor (for Gripper夾取Box)
        /// </summary>
        boxtransfer_tactile_2 = 172810,

        /// <summary>
        /// DE_BT_09: IC水平儀
        /// </summary>
        boxtransfer_gradienter_1 = 172900,

        /// <summary>
        /// DE_BT_07: Box Gripper (for 15,000 run)
        /// </summary>
        boxtransfer_gripper_1 = 173100,

        /// <summary>
        /// DE_BT_15: Robot SkinSensor
        /// </summary>
        boxtransfer_robot_skin_1 = 173300,


        boxtransfer_vibration = 173400,


        #endregion Box Transfer

        #region Universal
        universal_assembly = 180000,

        universal_plc_01 = 181110,
        universal_plc_02 = 181120,

        #endregion Universal

        #region Drawer(BoxSlot)
        drawer_assembly = 190000,
        //boxslot_outdoor_1 = 192200,
        //boxslot_laser_1 = 191300,
        //boxslot_fiber_optical_1 = 191900

        drawer_Interupter_PeopleSide = 193201,
        drawer_Interupter_RobotSide = 193202,
        drawer_Interupter_Home = 193203,
        drawer_Interupter_PeopleSideLimit = 193204,
        drawer_Interupter_RobotSideLimit = 193205,
        drawer_Button_DrawerLoadControl = 193801, 
        drawer_PresentDetector = 193901,
        #endregion 
    }
}
