using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace MvAssistant.Mac.v1_0.Hal.Assembly
{
    [GuidAttribute("442DC2E7-1076-4B1F-8F73-7B865ED08771")]
    public interface IMacHalBoxTransfer : IMacHalAssembly
    {
        int ExePathMove(string PathFileLocation);

        /// <summary>
        /// 夾取，1：鐵盒、2：水晶盒
        /// </summary>
        /// <param name="BoxType">1：鐵盒、2：水晶盒</param>
        /// <returns></returns>
        string Clamp(uint BoxType);

        string Unclamp();

        string Initial();

        /// <summary>
        /// 重置夾爪XY軸水平
        /// </summary>
        /// <returns></returns>
        bool LevelReset();

        string ReadBTRobotStatus();

        /// <summary>
        /// 當手臂作動或停止時，需要下指令讓PLC知道目前Robot是移動或靜止狀態
        /// </summary>
        /// <param name="isMoving">手臂是否要移動</param>
        void RobotMoving(bool isMoving);

        /// <summary>
        /// 夾爪速度設定，單位(mm/sec)
        /// </summary>
        /// <param name="ClampSpeed">夾爪速度</param>
        void SetSpeed(double ClampSpeed);

        /// <summary>
        /// 設定夾爪間距的極限值，最小間距、最大間距
        /// </summary>
        /// <param name="Minimum">最小間距</param>
        /// <param name="Maximum">最大間距</param>
        void SetHandSpaceLimit(double? Minimum, double? Maximum);

        /// <summary>
        /// 設定Clamp與Cabinet的最小間距限制
        /// </summary>
        /// <param name="Minimum">最小間距</param>
        void SetClampToCabinetSpaceLimit(double Minimum);

        /// <summary>
        /// 設定水平Sensor的XY軸極限值，X軸水平極限、Y軸水平極限
        /// </summary>
        /// <param name="Level_X">X軸水平極限</param>
        /// <param name="Level_Y">Y軸水平極限</param>
        void SetLevelSensorLimit(double? Level_X, double? Level_Y);

        /// <summary>
        /// 設定六軸力覺Sensor的壓力值上限
        /// </summary>
        /// <param name="Fx"></param>
        /// <param name="Fy"></param>
        /// <param name="Fz"></param>
        /// <param name="Mx"></param>
        /// <param name="My"></param>
        /// <param name="Mz"></param>
        void SetSixAxisSensorUpperLimit(double? Fx, double? Fy, double? Fz, double? Mx, double? My, double? Mz);

        /// <summary>
        /// 設定六軸力覺Sensor的壓力值下限
        /// </summary>
        /// <param name="Fx"></param>
        /// <param name="Fy"></param>
        /// <param name="Fz"></param>
        /// <param name="Mx"></param>
        /// <param name="My"></param>
        /// <param name="Mz"></param>
        void SetSixAxisSensorLowerLimit(double? Fx, double? Fy, double? Fz, double? Mx, double? My, double? Mz);

        /// <summary>
        /// 讀取夾爪速度設定
        /// </summary>
        /// <returns></returns>
        double ReadSpeedSetting();

        /// <summary>
        /// 讀取夾爪間距的極限值設定，最小夾距、最大夾距
        /// </summary>
        /// <returns>最小夾距、最大夾距</returns>
        Tuple<double, double> ReadHandSpaceLimitSetting();

        /// <summary>
        /// 讀取Clamp與Cabinet的最小間距限制
        /// </summary>
        /// <returns>最小間</returns>
        double ReadClampToCabinetSpaceLimitSetting();

        /// <summary>
        /// 設定XY軸水平Sensor限制，X軸水平限制、Y軸水平限制
        /// </summary>
        /// <returns>X軸水平限制、Y軸水平限制</returns>
        Tuple<double, double> ReadLevelSensorLimitSetting();

        /// <summary>
        /// 讀取六軸力覺Sensor的壓力值上限設定
        /// </summary>
        /// <returns></returns>
        Tuple<double, double, double, double, double, double> ReadSixAxisSensorUpperLimitSetting();

        /// <summary>
        /// 讀取六軸力覺Sensor的壓力值下限設定
        /// </summary>
        /// <returns></returns>
        Tuple<double, double, double, double, double, double> ReadSixAxisSensorLowerLimitSetting();

        /// <summary>
        /// 讀取軟體記憶的夾爪位置
        /// </summary>
        /// <returns></returns>
        double ReadHandPos();

        /// <summary>
        /// 讀取夾爪前方是否有Box
        /// </summary>
        /// <returns></returns>
        bool ReadBoxDetect();

        /// <summary>
        /// 讀取由雷射檢測的夾爪位置
        /// </summary>
        /// <returns></returns>
        double ReadHandPosByLSR();

        /// <summary>
        /// 讀取Clamp前方物體距離
        /// </summary>
        /// <returns></returns>
        double ReadClampDistance();

        /// <summary>
        /// 讀取XY軸水平Sensor目前數值，X軸水平數值、Y軸水平數值
        /// </summary>
        /// <returns>X軸水平數值、Y軸水平數值</returns>
        Tuple<double, double> ReadLevelSensor();

        /// <summary>
        /// 讀取六軸力覺Sensor數值
        /// </summary>
        /// <returns></returns>
        Tuple<int, int, int, int, int, int> ReadSixAxisSensor();

        /// <summary>
        /// 確認Hand吸塵狀態
        /// </summary>
        /// <returns></returns>
        bool ReadHandVacuum();

        Bitmap Camera_Cap();

        void Camera_CapToSave(string SavePath, string FileType);
    }
}
