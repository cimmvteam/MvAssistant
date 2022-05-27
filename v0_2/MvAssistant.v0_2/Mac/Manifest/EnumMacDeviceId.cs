using MvAssistant.v0_2.Mac.JSon.RobotTransferFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.Mac.Manifest
{
    public enum EnumMacDeviceId
    {
        None,


        #region Box Transfer

        boxtransfer_assembly,
        boxtransfer_plc,
        boxtransfer_laser_1,
        boxtransfer_camera_gripper_1,
        /// <summary> 環形光 </summary>
        boxtransfer_light_1,
        boxtransfer_robot_1,
        boxtransfer_force_6axis_sensor_1,
        boxtransfer_tactile_1,
        boxtransfer_tactile_2,
        boxtransfer_gradienter_1,
        boxtransfer_gripper_1,

        #endregion Box Transfer



        #region Cabinet

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

        clean_assembly,
        cleanch_plc,
        clean_camera_particle_1,
        cleanch_inspection_spot_light_001,
        clean_air_pressure_controller_1,
        clean_ionizer_1,

        #endregion Clean Chamber



        #region Inspection Chamber

        inspectionch_assembly,
        inspectionch_plc,
        inspectionch_camera_inspect_side_001,
        inspectionch_camera_inspect_top_001,
        inspectionch_camera_defense_side_001,
        inspectionch_camera_defense_top_001,
        inspectionch_light_circle_defense_top_001,
        inspectionch_light_line_left_001,
        inspectionch_light_line_back_001,
        inspectionch_light_circle_inspection_top_001,
        inspectionch_light_bar_left_001,
        inspectionch_light_bar_right_001,
        inspectionch_particle_counter_1,

        #endregion Inspection Chamber



        #region Load Port

        loadportA_assembly,
        loadportB_assembly,
        loadport_plc,
        loadportA_camera_inspect,
        loadportB_camera_inspect,
        loadport_camera_barcode_inspect,
        loadport_light_bar_001,
        loadport_light_bar_002,
        loadport_light_bar_003,//用於Barcode Reader

        loadport_1,
        loadport_2,

        #endregion Load Port



        #region Mask Transfer

        masktransfer_assembly,
        masktransfer_plc,
        masktransfer_barcode_reader_1,
        masktransfer_light_barcode_1,
        masktransfer_robot_1,
        masktransfer_force_6axis_sensor_1,
        masktransfer_inclinometer01,


        #endregion Mask Transfer



        #region Open Stage
        openstage_assembly,
        openstage_plc,
        openstage_camera_side_1,
        openstage_camera_top_1,
        openstage_camera_left_1,
        openstage_camera_right_1,
        openstage_light_bar_defense_top_001,
        openstage_light_bar_defense_side_001,
        openstage_light_bar_defense_front_001,
        openstage_particle_counter_1,

        #endregion Open Stage



        #region Eqp

        eqp_assembly,
        eqp_plc_01,

        #endregion Eqp




    }

    public static class MacEnumDeviceExtends
    {



        public static MacEnumDeviceDrawerRange GetDrawerRange(this EnumMacDeviceId instance)
        {
            return new MacEnumDeviceDrawerRange();
        }

        public static BoxrobotTransferLocation ToBoxrobotTransferLocation(this EnumMacDeviceId instance)
        {
            var idRange = instance.GetDrawerRange();
            var drawerLocationRange = BoxrobotTransferLocation.Dontcare.GetDrawerRange();
            var diff = instance - idRange.StartID;
            var rtnV = drawerLocationRange.Start + diff;
            return rtnV;
        }

    }
}
