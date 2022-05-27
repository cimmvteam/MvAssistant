using MvAssistant.v0_2.Mac.Hal.CompPlc;
using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace MvAssistant.v0_2.Mac.Hal.Assembly
{
    [GuidAttribute("442DC2E7-1076-4B1F-8F73-7B865ED08771")]
    public interface IMacHalBoxTransfer : IMacHalAssembly
    {
        IMacHalPlcBoxTransfer Plc { get; }

        /// <summary> 給點位清單，回朔移動路徑，從最後一個點位返回依序移動至清單起始點位 </summary>
        /// <param name="PathFileLocation"></param>
        /// <returns></returns>
        int BacktrackPathMove(string PathFileLocation);

        Bitmap Camera_Cap();

        void Camera_CapToSave(string SavePath, string FileType);

        /// <summary> 檢查當前位置與目標位置是否一致，點位允許誤差 ±50mm  </summary>
        /// <param name="PosFileLocation"></param>
        /// <returns></returns>
        bool CheckPosition(string PosFileLocation);

        /// <summary> 夾取，1：鐵盒、2：水晶盒 </summary>
        /// <param name="BoxType">1：鐵盒、2：水晶盒</param>
        /// <returns></returns>
        string Clamp(uint BoxType);

        /// <summary> 給點位清單，依序移動 </summary>
        /// <param name="PathPosition"></param>
        int ExePathMove(string PathFileLocation);
        /// <summary> Initial指令只針對PLC動作 </summary>
        /// <returns></returns>
        string Initial();

        /// <summary> 重置夾爪XY軸水平 </summary>
        /// <returns></returns>
        bool LevelReset();

        void LightForGripperSetValue(int value);

        /// <summary> 讀取夾爪前方是否有Box </summary>
        /// <returns></returns>
        bool ReadBoxDetect();

        /// <summary> 讀取BoxTransfer第七軸前端的極限Sensor， True：Nomal, False：Error </summary>
        /// <returns>True：Nomal, False：Error</returns>
        bool ReadBT_FrontLimitSenser();

        /// <summary> 讀取BoxTransfer第七軸後端的極限Sensor， True：Nomal, False：Error </summary>
        /// <returns>True：Nomal, False：Error</returns>
        bool ReadBT_RearLimitSenser();

        EnumMacPlcAssemblyStatus ReadBTStatus();

        /// <summary> 讀取Clamp與Cabinet的最小間距限制 </summary>
        /// <returns>最小間</returns>
        double ReadClampAndCabinetSpacingLimit();

        /// <summary> 讀取Clamp前方物體距離 </summary>
        /// <returns></returns>
        double ReadClampDistance();

        /// <summary> 讀取夾爪間距的極限值設定，最小夾距、最大夾距 </summary>
        /// <returns>最小夾距、最大夾距</returns>
        Tuple<double, double> ReadClampSpacingLimit();

        /// <summary> 讀取夾爪速度設定 </summary>
        /// <returns></returns>
        double ReadClampSpeedVar();

        /// <summary> 確認Clamp吸塵狀態 </summary>
        /// <returns></returns>
        bool ReadClampVacuum();

        /// <summary> 讀取軟體記憶的夾爪位置 </summary>
        /// <returns></returns>
        double ReadHandPos();

        /// <summary> 讀取由雷射檢測的夾爪位置 </summary>
        /// <returns></returns>
        double ReadHandPosByLSR();

        /// <summary> 讀取XY軸水平Sensor目前數值，X軸水平數值、Y軸水平數值 </summary>
        /// <returns>X軸水平數值、Y軸水平數值</returns>
        Tuple<double, double> ReadLevelSensor();

        /// <summary> 設定XY軸水平Sensor限制，X軸水平限制、Y軸水平限制 </summary>
        /// <returns>X軸水平限制、Y軸水平限制</returns>
        Tuple<double, double> ReadLevelSensorLimit();

        int ReadLightForGripper();

        /// <summary> 讀取六軸力覺Sensor數值 </summary>
        /// <returns></returns>
        Tuple<double, double, double, double, double, double> ReadSixAxisSensor();

        /// <summary> 讀取六軸力覺Sensor的壓力值下限設定 </summary>
        /// <returns></returns>
        Tuple<double, double, double, double, double, double> ReadSixAxisSensorLowerLimit();

        /// <summary> 讀取六軸力覺Sensor的壓力值上限設定 </summary>
        /// <returns></returns>
        Tuple<double, double, double, double, double, double> ReadSixAxisSensorUpperLimit();
        /// <summary> 當手臂作動或停止時，需要下指令讓PLC知道目前Robot是移動或靜止狀態 </summary>
        /// <param name="isMoving">手臂是否要移動</param>
        void RobotMoving(bool isMoving);

        /// <summary> 手臂狀態收復 </summary>
        /// <returns></returns>
        int RobotRecover();

        /// <summary> 手臂重啟 </summary>
        /// <returns></returns>
        int RobotReset();
        /// <summary> 手臂停止程序 </summary>
        /// <returns></returns>
        int RobotStopProgram();
        /// <summary> 設定Clamp與Cabinet的最小間距限制 </summary>
        /// <param name="Minimum">最小間距</param>
        void SetClampAndCabinetSpacingLimit(double Minimum);

        /// <summary> 設定夾爪間距的極限值，最小間距、最大間距 </summary>
        /// <param name="Minimum">最小間距</param>
        /// <param name="Maximum">最大間距</param>
        void SetClampSpacingLimit(double? Minimum, double? Maximum);

        /// <summary> 夾爪速度設定，單位(mm/sec) </summary>
        /// <param name="ClampSpeed">夾爪速度</param>
        void SetClampSpeedVar(double ClampSpeed);

        /// <summary> 設定水平Sensor的XY軸極限值，X軸水平極限、Y軸水平極限 </summary>
        /// <param name="Level_X">X軸水平極限</param>
        /// <param name="Level_Y">Y軸水平極限</param>
        void SetLevelSensorLimit(double? Level_X, double? Level_Y);

        /// <summary> 設定六軸力覺Sensor的壓力值下限 </summary>
        /// <param name="Fx"></param>
        /// <param name="Fy"></param>
        /// <param name="Fz"></param>
        /// <param name="Mx"></param>
        /// <param name="My"></param>
        /// <param name="Mz"></param>
        void SetSixAxisSensorLowerLimit(double? Fx, double? Fy, double? Fz, double? Mx, double? My, double? Mz);

        /// <summary> 設定六軸力覺Sensor的壓力值上限 </summary>
        /// <param name="Fx"></param>
        /// <param name="Fy"></param>
        /// <param name="Fz"></param>
        /// <param name="Mx"></param>
        /// <param name="My"></param>
        /// <param name="Mz"></param>
        void SetSixAxisSensorUpperLimit(double? Fx, double? Fy, double? Fz, double? Mx, double? My, double? Mz);
        string Unclamp();
    }
}
