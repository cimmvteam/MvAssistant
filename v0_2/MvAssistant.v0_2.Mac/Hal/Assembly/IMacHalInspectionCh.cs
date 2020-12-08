using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace MvAssistant.Mac.v1_0.Hal.Assembly
{
    [GuidAttribute("42CBD958-4DB6-4C5A-852C-3265A3B7F794")]
    public interface IMacHalInspectionCh : IMacHalAssembly
    {
        /// <summary>
        /// Stage XY軸移動，X:300~-10,Y:250~-10，X軸位置、Y軸位置
        /// </summary>
        /// <param name="X_Position">X軸位置</param>
        /// <param name="Y_Position">Y軸位置</param>
        /// <returns></returns>
        string XYPosition(double? X_Position, double? Y_Position);

        /// <summary>
        /// CCD高度調整，1~-85
        /// </summary>
        /// <param name="Z_Position"></param>
        /// <returns></returns>
        string ZPosition(double Z_Position);

        /// <summary>
        /// Mask載台方向旋轉，0~359
        /// </summary>
        /// <param name="W_Position"></param>
        /// <returns></returns>
        string WPosition(double W_Position);

        string Initial();

        string ReadInspChStatus();

        /// <summary>
        /// 設定速度，Stage XY軸移動速度(mm/S)、CCD Z軸移動速度(mm/S)、Mask W軸旋轉速度(Deg/S)
        /// </summary>
        /// <param name="StageXYSpeed">Stage XY軸移動速度(mm/S)</param>
        /// <param name="CcdZSpeed">CCD Z軸移動速度(mm/S)</param>
        /// <param name="MaskWSpeed">Mask W軸旋轉速度(Deg/S)</param>
        void SetSpeed(double? StageXYSpeed, double? CcdZSpeed, double? MaskWSpeed);

        /// <summary>
        /// 設定手臂入侵的左右區間極限，左極限、右極限
        /// </summary>
        /// <param name="AboutLimit_L">左極限</param>
        /// <param name="AboutLimit_R">右極限</param>
        void SetRobotAboutLimit(double? AboutLimit_L, double? AboutLimit_R);

        /// <summary>
        /// 設定手臂入侵的上下區間極限，上極限、下極限
        /// </summary>
        /// <param name="UpDownLimit_U"></param>
        /// <param name="UpDownLimit_D"></param>
        void SetRobotUpDownLimit(double? UpDownLimit_U, double? UpDownLimit_D);

        /// <summary>
        /// 設定各種大小Particle的數量限制
        /// </summary>
        /// <param name="L_Limit">Large Particle Qty</param>
        /// <param name="M_Limit">Medium Particle Qty</param>
        /// <param name="S_Limit">Small Particle Qty</param>
        void SetParticleCntLimit(uint? L_Limit, uint? M_Limit, uint? S_Limit);

        /// <summary>
        /// 讀取各種大小Particle的數量限制設定，大Particle、中Particle、小Particle的數量
        /// </summary>
        /// <returns></returns>
        Tuple<int, int, int> ReadParticleCntLimitSetting();

        /// <summary>
        /// 讀取各種大小Particle的數量，大Particle、中Particle、小Particle的數量
        /// </summary>
        /// <returns></returns>
        Tuple<int, int, int> ReadParticleCount();

        /// <summary>
        /// 設定Inspection Chamber內部與外部環境最大壓差限制
        /// </summary>
        /// <param name="GaugeLimit">錶壓差限制</param>
        void SetPressureDiffLimit(uint? GaugeLimit);

        /// <summary>
        /// 讀取Inspection Chamber內部與外部環境最大壓差限制設定
        /// </summary>
        /// <returns>錶壓差限制</returns>
        int ReadPressureDiffLimitSrtting();

        /// <summary>
        /// 讀取Inspection Chamber內部與外部環境壓差
        /// </summary>
        /// <returns>錶壓差</returns>
        int ReadPressureDiff();

        /// <summary>
        /// 讀取速度設定，Stage XY軸移動速度(mm/S)、CCD Z軸移動速度(mm/S)、Mask W軸旋轉速度(Deg/S)
        /// </summary>
        /// <returns>Stage XY軸移動速度(mm/S)、CCD Z軸移動速度(mm/S)、Mask W軸旋轉速度(Deg/S)</returns>
        Tuple<double, double, double> ReadSpeedSetting();

        /// <summary>
        /// 讀取手臂入侵的左右區間極限設定，左極限、右極限
        /// </summary>
        /// <returns>左極限、右極限</returns>
        Tuple<double, double> ReadRobotAboutLimitSetting();

        /// <summary>
        /// 讀取手臂入侵的上下區間極限設定，上極限、下極限
        /// </summary>
        /// <returns>上極限、下極限</returns>
        Tuple<double, double> ReadRobotUpDownLimitSetting();

        /// <summary>
        /// 設定Robot是否要入侵，讀取Mask Robot可否入侵
        /// </summary>
        /// <param name="isIntrude"></param>
        /// <returns></returns>
        bool ReadRobotIntrude(bool isIntrude);

        /// <summary>
        /// 讀取Stage XY軸位置，X軸位置、Y軸位置
        /// </summary>
        /// <returns>X軸位置、Y軸位置</returns>
        Tuple<double, double> ReadXYPosition();

        /// <summary>
        /// 讀取 CCD Z軸位置
        /// </summary>
        /// <returns>Z軸位置</returns>
        double ReadZPosition();

        /// <summary>
        /// 讀取Mask旋轉W軸位置(旋轉角度)
        /// </summary>
        /// <returns>W軸位置</returns>
        double ReadWPosition();

        /// <summary>
        /// 讀取手臂橫向位置(左右區間)
        /// </summary>
        /// <returns></returns>
        double ReadRobotPosAbout();

        /// <summary>
        /// 讀取手臂直向位置(上下區間)
        /// </summary>
        /// <returns></returns>
        double ReadRobotPosUpDown();

        Bitmap Camera_TopInsp_Cap();

        void Camera_TopInsp_CapToSave(string SavePath, string FileType);

        Bitmap Camera_TopDfs_Cap();

        void Camera_TopDfs_CapToSave(string SavePath, string FileType);

        Bitmap Camera_SideDfs_Cap();

        void Camera_SideDfs_CapToSave(string SavePath, string FileType);

        Bitmap Camera_SideInsp_Cap();

        void Camera_SideInsp_CapToSave(string SavePath, string FileType);

        void LightForLeftLineSetValue(int value);

        void LightForBackLineSetValue(int value);

        void LightForTopCrlDfsSetValue(int value);

        void LightForTopCrlInspSetValue(int value);

        void LightForLeftBarSetValue(int value);

        void LightForRightBarSetValue(int value);
    }
}
