using MvAssistant.v0_2.Mac.Hal;
using MvAssistant.v0_2.Mac.Hal.Assembly;
using MvAssistant.v0_2.Mac.Hal.CompDrawer;
using MvAssistant.v0_2.Mac.Manifest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MvAssistant.v0_2.Mac.TestMy.ToolHal.Hirata_ScenarioTest.Extends
{
   public static class MacHalContextExtends
    {
        public const string configPath= "UserData/Manifest/Manifest.xml.real";



        public static MacHalContext Create_MacHalContext_Instance()
        {
            var rtnInst = new MacHalContext(configPath);
            rtnInst.InitialAndLoad();
            return rtnInst;
        }
        
        /// <summary>初始化及載入 MacHalContext</summary>
        /// <param name="instance"></param>
        public static void InitialAndLoad(this MacHalContext instance)
        {
            instance.MvaCfBootup();
            instance.MvaCfLoad();
        }

        /// <summary>取得 universal Assembly instance</summary>
        /// <param name="instance"></param>
        /// <param name="autoConnect">
        /// <para>true: 自動   Connect </para>
        /// <para>false: 另行 Connect
        /// </param>
        /// <returns></returns>
        public static MacHalEqp GetUniversalAssembly(this MacHalContext instance,bool autoConnect=false)
        {
          var rtnV=  instance.HalDevices[EnumMacDeviceId.eqp_assembly.ToString()] as MacHalEqp;
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
            var rtnV = instance.HalDevices[EnumMacDeviceId.boxtransfer_assembly.ToString()] as MacHalBoxTransfer;
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
        public static MacHalOpenStage GetOpenStageAssembly(this MacHalContext instance,bool autoConnect=false)
        {
            var rtnV = instance.HalDevices[EnumMacDeviceId.openstage_assembly.ToString()] as MacHalOpenStage;
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
            var rtnV = instance.HalDevices[EnumMacDeviceId.cabinet_assembly.ToString()] as MacHalCabinet;
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
        public static List<EnumMacDeviceId> DrawersConnect(this MacHalContext instance)
        {
            List<EnumMacDeviceId> connectFailed = new List<EnumMacDeviceId>();
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
        /// <param name="autoConnect"></param>
        /// <returns></returns>
        public static IMacHalDrawer GetDrawer(this MacHalContext instance, EnumMacDeviceId key, bool autoConnect = false)
        {
            var cabinet = instance.HalDevices[key.ToString()] as MacHalCabinet;
            var drawer = cabinet.MacHalDrawer;
            if(autoConnect)
            {
                drawer.HalConnect();
            }
            return drawer;
        }
    }
}