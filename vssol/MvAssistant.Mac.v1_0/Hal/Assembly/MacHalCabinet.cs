using MvAssistant.Mac.v1_0.Hal.CompDrawer;
using MvAssistant.Mac.v1_0.Hal.CompPlc;
using MvAssistant.Mac.v1_0.Manifest;
using System;
using System.Runtime.InteropServices;
using MvAssistant.DeviceDrive.KjMachineDrawer;
using System.Collections.Generic;
using System.Linq;

namespace MvAssistant.Mac.v1_0.Hal.Assembly
{
    [GuidAttribute("DBCB4F3E-0405-450E-80D5-F2D1401975F1")]
    public class MacHalCabinet : MacHalAssemblyBase, IMacHalCabinet
    {

      //  public MvKjMachineDrawerManager LddManager = null;

        #region Device Components
        public IMacHalPlcCabinet Plc { get { return (IMacHalPlcCabinet)this.GetHalDevice(MacEnumDevice.cabinet_plc); } }

        public IMacHalDrawer MacHalDrawer
        {
            get
            {
                //return this.
                IMacHalDrawer drawer = null;
                for (var i = (int)MacEnumDevice.cabinet_drawer_01_01; i <= (int)MacEnumDevice.cabinet_drawer_07_05; i++)
                {
                    try
                    {
                        drawer = (IMacHalDrawer)this.GetHalDevice((MacEnumDevice)i);
                        if (drawer != null)
                        {
                            break;
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
                return drawer;
               ;
            }
        }

      //  public Dictionary<string, IMacHalDrawer> Drawers { get; set; }
      /*
        public IMacHalDrawer Drawer(int index)
        {
            var key = string.Format("{0}_{1:000}", MacEnumDevice.cabinet_drawer, index);
            return (IMacHalDrawer)this.GetHalDevice(MacEnumDevice.cabinet_drawer);
        }
        */
        /*
        public IMacHalDrawer GetDrawer(string index)
        {
            //  var key = string.Format("{0}_{1}", MacEnumDevice.cabinet_drawer, index);
            // return (IMacHalDrawer)this.GetHalDevice(MacEnumDevice.cabinet_drawer);
            try
            {
                var drawer = Drawers[index];
                return drawer;
            }
            catch(Exception ex)
            {
                return default(IMacHalDrawer);
            }
        }
        */
       /*
        public void CreateDrawers<T>(Dictionary<string, HalBase> hals) where T: HalBase,IMacHalDrawer, new()
        {
            Drawers = new Dictionary<string, IMacHalDrawer>();
            var keys = hals.Keys;
            var index = 0;
            foreach (string key in keys)
            {
                if (index != 0)
                {
                    HalBase drawer;
                   var isValue= hals.TryGetValue(key, out drawer);
                    var inst = (T)drawer;
                    inst.Index = key;
                    Drawers.Add(key,inst);

                }
                index++;
            }
        }*/
        #endregion Device Components



        #region Set Parameter
        /// <summary>
        /// 設定Cabinet內部與外部環境最大壓差限制，錶1壓差限制、錶2壓差限制
        /// </summary>
        /// <param name="Gauge1Limit">錶1壓差限制</param>
        /// <param name="Gauge2Limit">錶2壓差限制</param>
        public void SetPressureDiffLimit(uint? Gauge1Limit, uint? Gauge2Limit)
        { Plc.SetPressureDiffLimit(Gauge1Limit, Gauge2Limit); }

        /// <summary>
        /// 設定節流閥開啟大小，節流閥1、節流閥2
        /// </summary>
        /// <param name="Valve1">節流閥1</param>
        /// <param name="Valve2">節流閥2</param>
        public void SetExhaustFlow(int? Valve1, int? Valve2)
        { Plc.SetExhaustFlow(Valve1, Valve2); }
        #endregion

        #region Read Parameter
        /// <summary>
        /// 讀取Cabinet內部與外部環境最大壓差限制設定，錶1壓差限制、錶2壓差限制
        /// </summary>
        /// <returns>錶1壓差限制、錶2壓差限制</returns>
        public Tuple<int, int> ReadPressureDiffLimitSetting()
        { return Plc.ReadPressureDiffLimitSetting(); }

        /// <summary>
        /// 讀取節流閥開啟大小設定，節流閥1、節流閥2
        /// </summary>
        /// <returns>節流閥1、節流閥2</returns>
        public Tuple<int, int> ReadExhaustFlowSetting()
        { return Plc.ReadExhaustFlowSetting(); }
        #endregion

        #region Read Component Value
        /// <summary>
        /// 讀取Cabinet內部與外部環境壓差，錶1壓差、錶2壓差
        /// </summary>
        /// <returns></returns>
        public Tuple<int, int> ReadPressureDiff()
        { return Plc.ReadPressureDiff(); }

        /// <summary>
        /// 讀取光閘是否遮斷，一排一個 各自獨立，遮斷時True，Reset time 500ms
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, bool, bool, bool, bool, bool, bool> ReadLightCurtain()
        { return Plc.ReadLightCurtain(); }

        








        #endregion


    }
}
