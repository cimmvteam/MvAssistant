//using Microsoft.Analytics.Interfaces;
//using Microsoft.Analytics.Types.Sql;
using MvAssistant.Mac.v1_0;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.JSon;
using MvAssistant.Mac.v1_0.JSon.RobotTransferFile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MvAssistant.Mac.TestMy.MachineRealHal.Hirata_ScenarioTest.Extends
{
   public static class MacHalBoxTransferExtends
    {

        /// <summary>單一路徑移動</summary>
        /// <param name="instance"></param>
        /// <param name="path">路徑檔名稱</param>
        public static void Move(this MacHalBoxTransfer instance,string path)
        {
            instance.Move((new string[] { path }).ToList());
        }


        /// <summary>多路徑移動</summary>
        /// <param name="instance"></param>
        /// <param name="path">路徑檔名稱的集合</param>
        public static void Move(this MacHalBoxTransfer instance, List<string> paths)
        {
            instance.RobotMoving(true);
            foreach(var path in paths)
            {
                instance.ExePathMove(path);
            }
            instance.RobotMoving(false);
        }


        /// <summary>回到 CB1 HOME </summary>
        /// <param name="instance"></param>
        public static void TurnToCB1Home(this MacHalBoxTransfer instance)
        {
            var pathFileObj = new BoxrobotTransferPathFile(PositionInstance.BTR_Path);
            string path = pathFileObj.Cabinet01HomePathFile();
            instance.Move(path);
        }

        /// <summary> 回到 CB1 HOME</summary>
        /// <param name="instance"></param>
        public static void TurnToCB2Home(this MacHalBoxTransfer instance)
        {
            var pathFileObj = new BoxrobotTransferPathFile(PositionInstance.BTR_Path);
            string path = pathFileObj.Cabinet02HomePathFile();
            instance.Move(path);
        }

        public static void TurnOnCameraLight(this MacHalBoxTransfer instance, int lightValue=200)
        {

            instance.LightForGripper(lightValue);
        }

        public static void TurnOffCameraLight(this MacHalBoxTransfer instance)
        {

            instance.LightForGripper(0);
        }

        public static  int GetCameraLightValue(this MacHalBoxTransfer instance,BoxType boxType)
        {
            if (boxType == BoxType.IronBox)
            { return 5; }
            else if (boxType == BoxType.CrystalBox)
            {
                return 100;
            }
            else
            {
                return 200;
            }

        }


        /// <summary></summary>
        /// <param name="instance"></param>
        /// <param name="picName"></param>
        /// <param name="lightValue"></param>
        public static string CameraShot(this MacHalBoxTransfer instance, string pathName,string picType,int lightValue=200)
        {
            string rtnV = "";
            try
            {


                // 開啟 光源 
                instance.TurnOnCameraLight(lightValue);
                // 照相
                instance.Camera_CapToSave(pathName, picType);

                // 關閉
                instance.TurnOffCameraLight();
                rtnV = "Boxtransfer camera shot [OK]";

            }
            catch(Exception ex)
            {
                rtnV ="Boxtransfer camera shot error:" + ex.Message + ",  lightValue=" + lightValue;
            }
            finally
            {
                instance.TurnOffCameraLight();
            }
            return rtnV;
        }

       
    }
}