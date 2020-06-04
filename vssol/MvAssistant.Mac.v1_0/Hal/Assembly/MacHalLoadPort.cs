using MvAssistant.Mac.v1_0.Hal.CompPlc;
using MvAssistant.Mac.v1_0.Manifest;
using System;
using System.Runtime.InteropServices;

namespace MvAssistant.Mac.v1_0.Hal.Assembly
{
    [GuidAttribute("35E28A32-12E1-413A-8783-8A8018D512F1")]
    public class MacHalLoadPort : MacHalAssemblyBase, IMacHalLoadPort
    {
        #region Device Components


        public IMacHalPlcLoadPort Plc { get { return (IMacHalPlcLoadPort)this.GetHalDevice(MacEnumDevice.loadport_plc); } }



        #endregion

        #region Set Parameter
        /// <summary>
        /// 設定LoadPort內部與外部環境最大壓差限制，錶1壓差限制、錶2壓差限制
        /// </summary>
        /// <param name="Gauge1Limit">錶1壓差限制</param>
        /// <param name="Gauge2Limit">錶2壓差限制</param>
        public void SetPressureDiffLimit(uint? Gauge1Limit, uint? Gauge2Limit)
        { Plc.SetPressureDiffLimit(Gauge1Limit, Gauge2Limit); }
        #endregion

        #region Read Parameter
        /// <summary>
        /// 讀取LoadPort內部與外部環境最大壓差限制設定，錶1壓差限制、錶2壓差限制
        /// </summary>
        /// <returns>錶1壓差限制、錶2壓差限制</returns>
        public Tuple<int, int> ReadPressureDiffLimitSrtting()
        { return Plc.ReadPressureDiffLimitSrtting(); }
        #endregion

        #region Read Component Value
        /// <summary>
        /// 讀取LoadPort內部與外部環境壓差，錶1壓差、錶2壓差
        /// </summary>
        /// <returns>錶1壓差、錶2壓差</returns>
        public Tuple<int, int> ReadPressureDiff()
        { return Plc.ReadPressureDiff(); }

        /// <summary>
        /// 讀取Load Port光閘，True：遮斷 、 False：Normal
        /// </summary>
        /// <returns>True：遮斷、False：Normal</returns>
        public bool ReadLP_Light_Curtain()
        { return Plc.ReadLP_Light_Curtain(); }
        #endregion

    }
}
