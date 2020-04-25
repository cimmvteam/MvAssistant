﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvAssistant.MaskTool_v0_1.Plc
{
    public enum MvEnumPlcVariable
    {
        //PLC Connection & Public Area
        PC_TO_PLC_CheckClock,//PLC軟體狀態檢查
        PC_TO_PLC_CheckClock_Reply,
        PC_TO_DR_Red,
        PC_TO_DR_Orange,
        PC_TO_DR_Blue,
        PC_TO_DR_Buzzer,//0: None 1~4:Have
        Reset_ALL,
        Reset_ALL_Complete,
        PC_TO_BT_EMS,
        PC_TO_MT_EMS,
        PC_TO_OS_EMS,
        PC_TO_IC_EMS,

        //Cabinet(A01)
        PC_TO_DB_DP1Limit, //壓差設定數值寫入 AWord
        PC_TO_DB_DP2Limit,
        DB_TO_PC_DP1,//實際壓差數值讀取 BWord 壓差是否達標讀取 Bit
        DB_TO_PC_DP2,
        PC_TO_DB_Exhaust1, //節流閥開啟大小寫入 Word
        PC_TO_DB_Exhaust2,
        DR_TO_PC_Area1,//一排一個 各自獨立，遮斷時True，Reset time 500ms
        DR_TO_PC_Area2,
        DR_TO_PC_Area3,
        DR_TO_PC_Area4,
        DR_TO_PC_Area5,
        DR_TO_PC_Area6,
        DR_TO_PC_Area7,

        //Clean Chamber(A02)
        PC_TO_CC_PD_L_Limit,
        PC_TO_CC_PD_M_Limit,
        PC_TO_CC_PD_S_Limit,
        CC_TO_PC_PD_L,//偵測到Particle count
        CC_TO_PC_PD_M,
        CC_TO_PC_PD_S,
        CC_TO_PC_MaskLevel1,//檢測Mask 放置水平度
        CC_TO_PC_MaskLevel2,
        CC_TO_PC_MaskLevel3,
        PC_TO_CC_Robot_AboutLimit_R,
        PC_TO_CC_Robot_AboutLimit_L,
        CC_TO_PC_RobotPosition_About,//檢測Robot侵入位置(左右)
        PC_TO_CC_Robot_UpDownLimit_U,
        PC_TO_CC_Robot_UpDownLimit_D,
        CC_TO_PC_RobotPosition_UpDown,//檢測Robot侵入位置(上下)
        PC_TO_CC_DP_Limit,//壓差設定數值寫入 AWord
        CC_TO_PC_DP,//實際壓差數值讀取 BWord 壓差是否達標讀取 Bit
        PC_TO_CC_Blow,
        PC_TO_CC_BlowTime,//Time單位為100ms
        CC_TO_PC_Blow_Result,//吹氣結果
        CC_TO_PC_Blow_Reply,
        CC_TO_PC_Blow_Complete,//吹氣完成
        PC_TO_CC_PressureControl,
        CC_TO_PC_PressureControl,
        CC_TO_PC_Pressure,//壓力表數值
        CC_TO_PC_Area1,//一排一個 各自獨立，遮斷時True，Reset time 500ms
        CC_TO_PC_Area2,
        CC_TO_PC_Area3,

        //Box Robot Hand(A03)
        PC_TO_BT_Clamp,//會依設定位置進行夾取mask box
        PC_TO_BT_Box_Type,
        BT_TO_PC_ClampCmd_Reply,
        BT_TO_PC_ClampCmd_Complete,
        BT_TO_PC_ClampCmd_Result,
        PC_TO_BT_Unclamp,//放開mask box
        BT_TO_PC_UnclampCmd_Reply,
        BT_TO_PC_UnclampCmd_Complete,
        BT_TO_PC_UnclampCmd_Result,
        PC_TO_BT_Initial_A03,//機台狀態Initial
        BT_TO_PC_Initial_A03_Reply,
        BT_TO_PC_Initial_A03_Complete,
        BT_TO_PC_Initial_A03_Result,
        PC_TO_BT_Speed,
        BT_TO_PC_HandPosition,
        BT_TO_PC_LoadSensor,//判有無Box
        PC_TO_BT_Laser1_FLS,
        PC_TO_BT_Laser1_RLS,
        BT_TO_PC_LaserPosition1,//Hand夾爪目前位置
        PC_TO_BT_Laser2_Limit,
        BT_TO_PC_LaserPosition2,//偵測Robot Hand前方物體距離
        PC_TO_BT_Level_Limit_X,
        PC_TO_BT_Level_Limit_Y,
        PC_TO_BT_Level_Reset,
        BT_TO_PC_Level_X,
        BT_TO_PC_Level_Y,
        PC_TO_BT_Level_Reset_Complete,
        PC_TO_BT_ForceLimit_Fx,
        PC_TO_BT_ForceLimit_Fy,
        PC_TO_BT_ForceLimit_Fz,
        PC_TO_BT_ForceLimit_Mx,
        PC_TO_BT_ForceLimit_My,
        PC_TO_BT_ForceLimit_Mz,
        BT_TO_PC_ForceFx,//Robot力覺數值
        BT_TO_PC_ForceFy,
        BT_TO_PC_ForceFz,
        BT_TO_PC_ForceMx,
        BT_TO_PC_ForceMy,
        BT_TO_PC_ForceMz,
        BT_TO_PC_Vacuum,
        BT_TO_PC_A03Status,
        PC_TO_BT_RobotMoving,

        //Mask Robot Hand(A04)
        PC_TO_MT_Clamp,
        PC_TO_MT_MaskType,
        MT_TO_PC_ClampCmd_Reply,
        MT_TO_PC_ClampCmd_Complete,
        MT_TO_PC_ClampCmd_Result,
        PC_TO_MT_Unclamp,
        MT_TO_PC_UnclampCmd_Reply,
        MT_TO_PC_UnclampCmd_Complete,
        MT_TO_PC_UnclampCmd_Result,
        PC_TO_MT_Initial_A04,
        MT_TO_PC_Initial_A04_Reply,
        MT_TO_PC_Initial_A04_Complete,
        MT_TO_PC_Initial_A04_Result,
        PC_TO_MT_Speed,
        PC_TO_MT_Spin_Speed,
        PC_TO_MT_Tactile_Limit,
        MT_TO_PC_Position_Up,
        MT_TO_PC_Position_Down,
        MT_TO_PC_Position_Left,
        MT_TO_PC_Position_Right,
        PC_TO_MT_Spin_Point,
        PC_TO_MT_Spin,
        MT_TO_PC_Spin_Reply,
        MT_TO_PC_Spin_Complete,
        MT_TO_PC_Spin_Result,
        MT_TO_PC_Position_Spin,
        PC_TO_MT_ForceLimit_Fx,
        PC_TO_MT_ForceLimit_Fy,
        PC_TO_MT_ForceLimit_Fz,
        PC_TO_MT_ForceLimit_Mx,
        PC_TO_MT_ForceLimit_My,
        PC_TO_MT_ForceLimit_Mz,
        MT_TO_PC_ForceFx,
        MT_TO_PC_ForceFy,
        MT_TO_PC_ForceFz,
        MT_TO_PC_ForceMx,
        MT_TO_PC_ForceMy,
        MT_TO_PC_ForceMz,
        MT_TO_PC_StaticElectricity_Limit_UP,
        MT_TO_PC_StaticElectricity_Limit_Down,
        MT_TO_PC_StaticElectricity_Value,
        MT_TO_PC_A04Status,
        PC_TO_MT_RobotMoving,

        //Open Stage(A05)
        PC_TO_OS_Open,//開盒
        OS_TO_PC_Open_Reply,
        OS_TO_PC_Open_Complete,
        OS_TO_PC_Open_Result,
        PC_TO_OS_Close,//關盒
        OS_TO_PC_Close_Reply,
        OS_TO_PC_Close_Complete,
        OS_TO_PC_Close_Result,
        PC_TO_OS_Clamp,//開盒夾爪閉合
        OS_TO_PC_Clamp_Reply,
        OS_TO_PC_Clamp_Complete,
        OS_TO_PC_Clamp_Result,//Result(1:OK 2:NoBox 3:NoClose 4:)
        PC_TO_OS_Unclamp,//開盒夾爪鬆開
        OS_TO_PC_Unclamp_Reply,
        OS_TO_PC_Unclamp_Complete,
        OS_TO_PC_Unclamp_Result,
        PC_TO_OS_SortClamp,//Stage上固定Box的夾具閉合
        OS_TO_PC_SortClamp_Reply,
        OS_TO_PC_SortClamp_Complete,
        OS_TO_PC_SortClamp_Result,
        PC_TO_OS_SortUnclamp,//Stage上固定Box的夾具鬆開
        OS_TO_PC_SortUnclamp_Reply,
        OS_TO_PC_SortUnclamp_Complete,
        OS_TO_PC_SortUnclamp_Result,
        PC_TO_OS_Lock,//開關盒鎖
        OS_TO_PC_Lock_Reply,
        OS_TO_PC_Lock_Complete,
        OS_TO_PC_Lock_Result,
        PC_TO_OS_Initial_A05,//Initial
        OS_TO_PC_Initial_A05_Reply,
        OS_TO_PC_Initial_A05_Complete,
        OS_TO_PC_Initial_A05_Result,
        PC_TO_OS_BoxType,//設定內容 1.Box type－鐵盒：1、水晶盒：2      2.作動速度%
        PC_TO_OS_BTIntrude,//Robot侵入A05
        PC_TO_OS_MTIntrude,
        OS_TO_PC_BTLicence,//Robot侵入A05許可
        OS_TO_PC_MTLicence,
        OS_TO_PC_ClampStatus,
        OS_TO_PC_SortClamp1_Position,
        OS_TO_PC_SortClamp2_Position,
        OS_TO_PC_Slider1_Position,
        OS_TO_PC_Slider2_Position,
        OS_TO_PC_Cover1_Position,
        OS_TO_PC_Cover2_Position,
        OS_TO_PC_CoverSensor_Open,
        OS_TO_PC_CoverSensor_Close,
        OS_TO_PC_SoundWave,
        OS_TO_PC_Weight_Cruuent,
        OS_TO_PC_BoxCheckOK,
        OS_TO_PC_A05Status,

        //Inspection Chamber(A06)
        PC_TO_IC_XYCmd,//Stage進行XY移動
        PC_TO_IC_XPoint,
        PC_TO_IC_YPoint,
        IC_TO_PC_XYReply,
        IC_TO_PC_XYComplete,
        IC_TO_PC_XYResult,
        PC_TO_IC_ZCmd,//CCD高度變更
        PC_TO_IC_ZPoint,
        IC_TO_PC_ZReply,
        IC_TO_PC_ZComplete,
        IC_TO_PC_ZResult,
        PC_TO_IC_WCmd,//Mask方向旋轉
        PC_TO_IC_WPoint,
        IC_TO_PC_WReply,
        IC_TO_PC_WComplete,
        IC_TO_PC_WResult,
        PC_TO_IC_Initial_A06,
        IC_TO_PC_Initial_A06_Reply,
        IC_TO_PC_Initial_A06_Complete,
        IC_TO_PC_Initial_A06_Result,
        PC_TO_IC_Z_Speed,
        PC_TO_IC_XY_Speed,
        PC_TO_IC_W_Speed,
        PC_TO_IC_RobotIntrude,
        IC_TO_PC_RobotLicence,
        IC_TO_PC_Positon_X,//XY Stage位置
        IC_TO_PC_Positon_Y,
        IC_TO_PC_Positon_Z,//CCD Z軸位置
        IC_TO_PC_Positon_W,//旋轉位置
        PC_TO_IC_Robot_AboutLimit_R,//檢測Robot侵入位置(左右)
        PC_TO_IC_Robot_AboutLimit_L,
        IC_TO_PC_RobotPosition_About,
        PC_TO_IC_Robot_UpDownLimit_U,//檢測Robot侵入位置(上下)
        PC_TO_IC_Robot_UpDownLimit_D,
        IC_TO_PC_RobotPosition_UpDown,
        IC_TO_PC_A06Status,
        A06_Alarm,

        //Load Port(A07)
        PC_TO_LP_DP1Limit,//壓差極限數值寫入 AWord
        PC_TO_LP_DP2Limit,
        LP_TO_PC_DP1,//實際壓差數值
        LP_TO_PC_DP2,

        //外罩(A08)
        PC_TO_FFU_SetSpeed,
        PC_TO_FFU_Address,
        PC_TO_FFU_Write,
        FFU_TO_PC_Write_Reply,
        FFU_TO_PC_Write_Complete,
        FFU_TO_PC_Write_Result,
        FFU_TO_PC_FFUCurrentSpeed_1,
        FFU_TO_PC_FFUCurrentSpeed_2,
        FFU_TO_PC_FFUCurrentSpeed_3,
        FFU_TO_PC_FFUCurrentSpeed_4,
        FFU_TO_PC_FFUCurrentSpeed_5,
        FFU_TO_PC_FFUCurrentSpeed_6,
        FFU_TO_PC_FFUCurrentSpeed_7,
        FFU_TO_PC_FFUCurrentSpeed_8,
        FFU_TO_PC_FFUCurrentSpeed_9,
        FFU_TO_PC_FFUCurrentSpeed_10,
        FFU_TO_PC_FFUCurrentSpeed_11,
        FFU_TO_PC_FFUCurrentSpeed_12,

        //Hand Inspection(A09)
        LD_TO_PC_Laser1,//測距Sensor數值，4個下方 2個側邊
        LD_TO_PC_Laser2,
        LD_TO_PC_Laser3,
        LD_TO_PC_Laser4,
        LD_TO_PC_Laser5,
        LD_TO_PC_Laser6

    }
}