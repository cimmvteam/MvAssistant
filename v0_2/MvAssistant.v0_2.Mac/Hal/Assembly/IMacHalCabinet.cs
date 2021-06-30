using MvAssistant.v0_2.DeviceDrive.KjMachineDrawer;
using MvAssistant.v0_2.Mac.Hal.CompDrawer;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace MvAssistant.v0_2.Mac.Hal.Assembly
{
    [GuidAttribute("002CE873-5909-4874-96BF-6CD3971DAB39")]
    public interface IMacHalCabinet : IMacHalAssembly
    {
        IMacHalDrawer MacHalDrawer { get; }

        /** To Drop
         /// <summary>Cabinet 存放 Drawer 的容器</summary>
         Dictionary<string, IMacHalDrawer> Drawers { get; set; }
       
         /// <summary>取出某一特定Index 的 Drawer</summary>
         /// <param name="index"></param>
         /// <returns></returns>
         IMacHalDrawer GetDrawer(string index);
        
        
         /// <summary>產生 Drawer的集合</summary>
         void CreateDrawers<T>(Dictionary<string,HalBase> hals) where T :HalBase, IMacHalDrawer, new();
         */


        /// <summary> 讀取Cabinet內部與外部環境壓差，錶1壓差、錶2壓差 </summary>
        /// <returns></returns>
        Tuple<int, int> ReadChamberPressureDiff();

        /// <summary> 讀取Cabinet內部與外部環境最大壓差限制設定，錶1壓差限制、錶2壓差限制 </summary>
        /// <returns>錶1壓差限制、錶2壓差限制</returns>
        Tuple<int, int> ReadChamberPressureDiffLimitSetting();

        /// <summary> 讀取節流閥開啟大小設定，節流閥1、節流閥2 </summary>
        /// <returns>節流閥1、節流閥2</returns>
        Tuple<int, int> ReadExhaustFlowSetting();

        /// <summary> 讀取光閘是否遮斷，一排一個 各自獨立，遮斷時True，Reset time 500ms </summary>
        /// <returns></returns>
        Tuple<bool, bool, bool, bool, bool, bool, bool> ReadLightCurtain();

        /// <summary> 設定Cabinet內部與外部環境最大壓差限制，錶1壓差限制、錶2壓差限制 </summary>
        /// <param name="Gauge1Limit">錶1壓差限制</param>
        /// <param name="Gauge2Limit">錶2壓差限制</param>
        void SetChamberPressureDiffLimit(uint? Gauge1Limit, uint? Gauge2Limit);

        /// <summary> 設定節流閥開啟大小，節流閥1、節流閥2 </summary>
        /// <param name="Valve1">節流閥1</param>
        /// <param name="Valve2">節流閥2</param>
        void SetExhaustFlow(int? Valve1, int? Valve2);
    }
}
