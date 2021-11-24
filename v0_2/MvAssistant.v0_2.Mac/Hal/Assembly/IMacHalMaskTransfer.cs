using MvAssistant.v0_2.Mac.Hal.CompPlc;
using MvAssistant.v0_2.Mac.Hal.CompRobot;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace MvAssistant.v0_2.Mac.Hal.Assembly
{
    [GuidAttribute("6412D4A0-41F3-4659-B12B-7A8BF9399BAE")]
    public interface IMacHalMaskTransfer : IMacHalAssembly
    {
        IMacHalPlcMaskTransfer Plc { get; }

        /// <summary> 讀取檔案的路徑點位 </summary>
        /// <param name="PathFileLocation"></param>
        /// <returns></returns>
        List<HalRobotMotion> ReadFilePosition(string PathFileLocation);

        /// <summary> 帶入點位資訊，移動Robot </summary>
        /// <param name="MovePosition"></param>
        /// <returns></returns>
        int ExePathMove(List<HalRobotMotion> MovePosition);

        /// <summary> 給點位清單，依序移動 </summary>
        /// <param name="PathPosition"></param>
        int ExePathMove(string PathFileLocation);

        /// <summary> 給點位清單，回朔移動路徑，從最後一個點位返回依序移動至清單起始點位 </summary>
        /// <param name="PathFileLocation"></param>
        /// <returns></returns>
        int BacktrackPathMove(string PathFileLocation);

        /// <summary> 調整手臂到其他進入Assembly的Home點 </summary>
        /// <param name="EndPosFileLocation">Jason檔的儲存目錄</param>
        /// <param name="EndPosition"></param>
        void ChangeDirection(string EndPosFileLocation);

        /// <summary> 檢查當前位置與目標位置是否一致，點位允許誤差 ±50mm </summary>
        /// <param name="PosFileLocation"></param>
        /// <returns></returns>
        bool CheckPosition(string PosFileLocation);

        /// <summary> 手臂重啟 </summary>
        /// <returns></returns>
        int RobotReset();

        /// <summary> 手臂狀態收復 </summary>
        /// <returns></returns>
        int RobotRecover();

        /// <summary> 手臂停止程序 </summary>
        /// <returns></returns>
        int RobotStopProgram();
        string Clamp(uint MaskType);
        string Unclamp();
        /// <summary> CCD旋轉(單位 0.01 Degree) </summary>
        /// <param name="SpinDegree"></param>
        /// <returns></returns>
        string CCDSpin(int SpinDegree);

        /// <summary> Initial指令只針對PLC動作 </summary>
        void Initial();
        string ReadMTRobotStatus();
        /// <summary> 當手臂作動或停止時，需要下指令讓PLC知道目前Robot是移動或靜止狀態 </summary>
        /// <param name="isMoving">手臂是否要移動</param>
        void RobotMoving(bool isMoving);


        /// <summary> 設定夾爪觸覺極限 </summary>
        /// <param name="TactileMaxLimit">上限</param>
        /// <param name="TactileMinLimit">下限</param>
        //void SetClampTactileLim(int? TactileMaxLimit, int? TactileMinLimit);

        /// <summary> 設定三軸水平極限值 </summary>
        /// <param name="Level_X"></param>
        /// <param name="Level_Y"></param>
        /// <param name="Level_Z"></param>
        void SetLevelLimit(int? Level_X, int? Level_Y, int? Level_Z);
        /// <summary> 設定六軸力覺Sensor的壓力值上限 </summary>
        /// <param name="Fx"></param>
        /// <param name="Fy"></param>
        /// <param name="Fz"></param>
        /// <param name="Mx"></param>
        /// <param name="My"></param>
        /// <param name="Mz"></param>
        void SetSixAxisSensorUpperLimit(double? Fx, double? Fy, double? Fz, double? Mx, double? My, double? Mz);
        /// <summary> 設定六軸力覺Sensor的壓力值下限 </summary>
        /// <param name="Fx"></param>
        /// <param name="Fy"></param>
        /// <param name="Fz"></param>
        /// <param name="Mx"></param>
        /// <param name="My"></param>
        /// <param name="Mz"></param>
        void SetSixAxisSensorLowerLimit(double? Fx, double? Fy, double? Fz, double? Mx, double? My, double? Mz);
        /// <summary> 夾速度設定，單位(mm/sec)，CCD旋轉速度設定，單位(0.01 deg/sec) </summary>
        /// <param name="ClampSpeed">(mm/sec)</param>
        /// <param name="CCDSpinSpeed">(0.01 deg/sec)</param>
        void SetSpeed(double? ClampSpeed, long? CCDSpinSpeed);
        /// <summary> 設定靜電感測的區間限制 </summary>
        /// <param name="Maximum">上限</param>
        /// <param name="Minimum">下限</param>
        void SetStaticElecLimit(double? Maximum, double? Minimum);


        /// <summary> 讀取夾爪觸覺極限設定值，上限、下限 </summary>
        /// <returns>Maximum、Minimum</returns>
        Tuple<int, int> ReadClampTactileLimit();
        /// <summary> 讀取三軸水平極限值設定，X軸、Y軸、Z軸 </summary>
        /// <returns>X軸、Y軸、Z軸</returns>
        Tuple<int, int, int> ReadLevelLimit();
        /// <summary> 讀取六軸力覺Sensor的壓力值上限設定，Fx、Fy、Fz、Mx、My、Mz </summary>
        /// <returns>Fx、Fy、Fz、Mx、My、Mz</returns>
        Tuple<double, double, double, double, double, double> ReadSixAxisSensorUpperLimit();
        /// <summary> 讀取六軸力覺Sensor的壓力值下限設定，Fx、Fy、Fz、Mx、My、Mz </summary>
        /// <returns></returns>
        Tuple<double, double, double, double, double, double> ReadSixAxisSensorLowerLimit();
        /// <summary> 讀取速度設定，夾爪速度、CCD旋轉速度 </summary>
        /// <returns>夾爪速度、CCD旋轉速度</returns>
        Tuple<double, long> ReadSpeedVar();
        /// <summary> 讀取靜電感測的區間限制設定值，上限、下限 </summary>
        /// <returns>上限、下限</returns>
        Tuple<double, double> ReadStaticElecLimit();


        /// <summary> 讀取CCD旋轉角度(單位 0.01 Degree) </summary>
        /// <returns></returns>
        //long ReadCCDSpinDegree();

        /// <summary> 讀取夾爪鉗四邊伸出長度的位置，夾爪前端、夾爪後端、夾爪左邊、夾爪右邊 </summary>
        /// <returns>夾爪前端、夾爪後端、夾爪左邊、夾爪右邊</returns>
        Tuple<double, double, double, double> ReadClampGripPos();

        /// <summary> 讀取夾爪前端觸覺數值(前端Sensor會有三個數值) </summary>
        /// <returns></returns>
        //Tuple<int, int, int> ReadClampTactile_FrontSide();

        /// <summary> 讀取夾爪後端觸覺數值(後端Sensor會有三個數值) </summary>
        /// <returns></returns>
        //Tuple<int, int, int> ReadClampTactile_BehindSide();

        /// <summary> 讀取夾爪左側觸覺數值(左側Sensor會有六個數值) </summary>
        /// <returns></returns>
        //Tuple<int, int, int, int, int, int> ReadClampTactile_LeftSide();

        /// <summary> 讀取夾爪右側觸覺數值(右側Sensor會有六個數值) </summary>
        /// <returns></returns>
        //Tuple<int, int, int, int, int, int> ReadClampTactile_RightSide();

        /// <summary> 讀取夾爪變形檢測數值(需要先將手臂伸到檢測平台) </summary>
        /// <returns></returns>
        Tuple<double, double, double, double, double, double> ReadHandInspection();
        /// <summary> 讀取三軸水平數值，X軸、Y軸、Z軸 </summary>
        /// <returns></returns>
        Tuple<int, int, int> ReadLevel();
        /// <summary> 讀取六軸Sensor數值，Fx、Fy、Fz、Mx、My、Mz </summary>
        /// <returns>Fx、Fy、Fz、Mx、My、Mz</returns>
        Tuple<double, double, double, double, double, double> ReadSixAxisSensor();
        /// <summary> 讀取靜電測量值 </summary>
        /// <returns></returns>
        double ReadStaticElec();
    }
}
