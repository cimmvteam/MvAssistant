using MvAssistant.Mac.v1_0.Hal.CompLoadPort;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace MvAssistant.Mac.v1_0.Hal.Assembly
{
    [GuidAttribute("8E7C81C2-3074-43AA-867E-E3F3700668E0")]
    public interface IMacHalLoadPort : IMacHalAssembly
    {
        /// <summary>Load Port 單元 (執行 Dock/Undock 的單元)</summary>
        IMacHalLoadPortUnit LoadPortUnit{ get; }

        string Dock();

        string Undock();

        /// <summary>
        /// 設定LoadPort內部與外部環境最大壓差限制，錶1壓差限制、錶2壓差限制
        /// </summary>
        /// <param name="Gauge1Limit">錶1壓差限制</param>
        /// <param name="Gauge2Limit">錶2壓差限制</param>
        void SetPressureDiffLimit(uint? Gauge1Limit, uint? Gauge2Limit);

        /// <summary>
        /// 讀取LoadPort內部與外部環境最大壓差限制設定，錶1壓差限制、錶2壓差限制
        /// </summary>
        /// <returns>錶1壓差限制、錶2壓差限制</returns>
        Tuple<int, int> ReadPressureDiffLimitSrtting();

        /// <summary>
        /// 讀取LoadPort內部與外部環境壓差，錶1壓差、錶2壓差
        /// </summary>
        /// <returns>錶1壓差、錶2壓差</returns>
        Tuple<int, int> ReadPressureDiff();

        /// <summary>
        /// 讀取Load Port光閘，True：遮斷 、 False：Normal
        /// </summary>
        /// <returns>True：遮斷、False：Normal</returns>
        bool ReadLP_Light_Curtain();

        
    }
}
