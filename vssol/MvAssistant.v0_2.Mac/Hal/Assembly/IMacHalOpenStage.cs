using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace MvAssistant.Mac.v1_0.Hal.Assembly
{
    [GuidAttribute("BE05B67D-DB09-4358-81C2-D8DC9F76208A")]
    public interface IMacHalOpenStage : IMacHalAssembly
    {
        string Open();

        string Close();

        string Clamp();

        string Unclamp();

        string SortClamp();

        string SortUnclamp();

        string Lock();

        string Vacuum(bool isSuck);

        string Initial();

        string ReadOpenStageStatus();

        /// <summary>
        /// 設定盒子種類，1：鐵盒 , 2：水晶盒
        /// </summary>
        /// <param name="BoxType">1：鐵盒 , 2：水晶盒</param>
        void SetBoxType(uint BoxType);

        /// <summary>
        /// 設定速度(%)
        /// </summary>
        /// <param name="Speed">(%)</param>
        void SetSpeed(uint Speed);

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

        int ReadBoxTypeSetting();

        int ReadSpeedSetting();

        /// <summary>
        /// 發送入侵訊號，確認Robot能否入侵
        /// </summary>
        /// <param name="isBTIntrude">BT Robot是否要入侵</param>
        /// <param name="isMTIntrude">MT Robot是否要入侵</param>
        /// <returns></returns>
        Tuple<bool, bool> ReadRobotIntrude(bool? isBTIntrude, bool? isMTIntrude);

        /// <summary>
        /// 讀取目前是否被Robot侵入
        /// </summary>
        /// <returns></returns>
        bool ReadBeenIntruded();

        /// <summary>
        /// 讀取開盒夾爪狀態
        /// </summary>
        /// <returns></returns>
        string ReadClampStatus();

        /// <summary>
        /// 讀取Stage上固定Box的夾具位置
        /// </summary>
        /// <returns></returns>
        Tuple<long, long> ReadSortClampPosition();

        /// <summary>
        /// 讀取Slider的位置
        /// </summary>
        /// <returns></returns>
        Tuple<long, long> ReadSliderPosition();

        /// <summary>
        /// 讀取盒蓋位置
        /// </summary>
        /// <returns></returns>
        Tuple<double, double> ReadCoverPos();

        /// <summary>
        /// 讀取盒蓋開闔， Open ; Close
        /// </summary>
        /// <returns></returns>
        Tuple<bool, bool> ReadCoverSensor();

        /// <summary>
        /// 讀取盒子是否變形
        /// </summary>
        /// <returns></returns>
        double ReadBoxDeform();

        /// <summary>
        /// 讀取平台上的重量
        /// </summary>
        /// <returns></returns>
        double ReadWeightOnStage();

        /// <summary>
        /// 讀取是否有Box
        /// </summary>
        /// <returns></returns>
        bool ReadBoxExist();

        void LightForSideBarDfsSetValue(int value);

        void LightForTopBarDfsSetValue(int value);

        void LightForFrontBarDfsSetValue(int value);

        Bitmap Camera_Top_Cap();

        void Camera_Top_CapToSave(string SavePath, string FileType);

        Bitmap Camera_Side_Cap();

        void Camera_Side_CapToSave(string SavePath, string FileType);

        Bitmap Camera_Left_Cap();

        void Camera_Left_CapToSave(string SavePath, string FileType);

        Bitmap Camera_Right_Cap();

        void Camera_Right_CapToSave(string SavePath, string FileType);
    }
}
