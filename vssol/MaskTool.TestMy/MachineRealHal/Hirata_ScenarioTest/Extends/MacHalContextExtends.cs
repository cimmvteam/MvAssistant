using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Hal.CompDrawer;
using MvAssistant.Mac.v1_0.Manifest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MvAssistant.Mac.TestMy.MachineRealHal.Hirata_ScenarioTest.Extends
{
   public static class MacHalContextExtends
    {
        /// <summary>初始化及載入 MacHalContext</summary>
        /// <param name="instance"></param>
        public static void InitialAndLoad(this MacHalContext instance)
        {
            instance.MvCfInit();
            instance.MvCfLoad();
        }

        /// <summary>取得 universal Assembly instance</summary>
        /// <param name="instance"></param>
        /// <param name="autoConnect">
        /// <para>true: 自動   Connect </para>
        /// <para>false: 另行 Connect
        /// </param>
        /// <returns></returns>
        public static MacHalUniversal GetUniversalAssembly(this MacHalContext instance,bool autoConnect=false)
        {
          var rtnV=  instance.HalDevices[MacEnumDevice.universal_assembly.ToString()] as MacHalUniversal;
            if (autoConnect)
            {
                if (rtnV != null)
                {
                    rtnV.HalConnect();
                }
            }
            return rtnV;

        }

        /// <summary>取得 MacHalBoxTransfer Assembly Instance</summary>
        /// <param name="instance"></param>
        /// <param name="autoConnect">
        /// <para>true: 自動 Connect</para>
        /// <para>false: 另行連線</para>
        /// </param>
        /// <returns></returns>
        public static MacHalBoxTransfer GetBoxTransferAssembly(this MacHalContext instance, bool autoConnect=false)
        {
            var rtnV = instance.HalDevices[MacEnumDevice.boxtransfer_assembly.ToString()] as MacHalBoxTransfer;
            if (autoConnect)
            {
                if (rtnV != null)
                {
                    rtnV.HalConnect();
                }
            }
            return rtnV;
        }

        /// <summary>取得 MacHalOpenStage Assembly Instance</summary>
        /// <param name="instance"></param>
        /// <param name="autoConnect">
        /// <para>true: 自動 Connect</para>
        /// <para>false: 另行連線</para>
        /// </param>
        /// <returns></returns>
        public static MacHalOpenStage GetOpenStageAssembly(this MacHalContext instance,bool autoConnect)
        {
            var rtnV = instance.HalDevices[MacEnumDevice.openstage_assembly.ToString()] as MacHalOpenStage;
            if (autoConnect)
            {
                if (rtnV != null)
                {
                    rtnV.HalConnect();
                }
            }
            return rtnV;
        }

        /// <summary>取得 MacHalCabinet assembly Instance</summary>
        /// <param name="instance"></param>
        /// <param name="autoConnect">
        /// <para>true: 自動 Connect</para>
        /// <para>false: 另行連線</para>
        /// </param>
        /// <returns></returns>
        public static MacHalCabinet GetCabinetAssembly(this MacHalContext instance,bool autoConnect)
        {
            var rtnV = instance.HalDevices[MacEnumDevice.cabinet_assembly.ToString()] as MacHalCabinet;
            if (autoConnect)
            {
                if (rtnV != null)
                {
                    rtnV.HalConnect();
                }
            }
            return rtnV;
        }

        /// <summary>Connect 所有 Drawer</summary>
        /// <param name="instance"></param>
        /// <returns>
        /// Connect 失敗的 Drawer 的 Key 值
        /// </returns>
        public static List<MacEnumDevice> DrawersConnect(this MacHalContext instance)
        {
            List<MacEnumDevice> connectFailed = new List<MacEnumDevice>();
            foreach (var key in HalDrawerExtends.DrawerKeys)
            {
                try
                {
                    var drawer = instance.GetDrawer(key);
                    drawer.HalConnect();
                }
                catch(Exception e)
                {
                    connectFailed.Add(key);
                }
            }
            return connectFailed;
        }

        /// <summary>取得 Drawer Assembly intance</summary>
        /// <param name="instance"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IMacHalDrawer GetDrawer(this MacHalContext instance, MacEnumDevice key)
        {
            var cabinet = instance.HalDevices[key.ToString()] as MacHalCabinet;
            var rtnV = cabinet.MacHalDrawer;
            return rtnV;
        }
    }
}