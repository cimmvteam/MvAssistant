using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.JSon.RobotTransferFile
{
   public class BoxrobotTransferPathFile:BaserobotTransferPathFile
    {
        /// <summary>檔案名稱連接符號</summary>
        private const string FileNameConnectSign = "_";
        public BoxrobotTransferPathFile() : base()
        {

        }
        public BoxrobotTransferPathFile(string path) : base(path)
        {

        }
        public BoxrobotTransferPathFile(string path, string extendedName) : base(path, extendedName)
        {

        }


         
       /// <summary>由起點到終點的檔案名稱</summary>
       /// <param name="startPoint">起點</param>
       /// <param name="destination">終點</param>
       /// <param name="direction">方向(去、 回)</param>
       /// <param name="actionType">動作方式(取、 放)</param>
       /// <returns></returns>
        private string FromStartPointToDestinationPathFile(BoxrobotTransferLocation startPoint, BoxrobotTransferLocation destination,BoxrobotTransferDirection direction, BoxrobotTransferActionType actionType)
        {
            string fullFileName,fileName;
            const string connetSign= FileNameConnectSign;
            /** // vs 2013
            fileName = $"{startPoint.ToText()}{connetSign}{direction.ToText()}{connetSign}{destination.ToText()}{connetSign}{actionType}{this.ExetendedFileName}";
            fullFileName = $"{this.FilePath}{fileName}";
                         */
            fileName = startPoint.ToText() +connetSign+direction.ToText()+connetSign+destination.ToText()+connetSign + actionType + this.ExetendedFileName;
            fullFileName = this.FilePath + fileName;
             return fullFileName;
        }
        private string CabinetHomePathFile(BoxrobotTransferLocation cabinetHome)
        {
            string fullFileName, fileName;
            //const string connetSign = FileNameConnectSign;
            /** // vs 2013
            fileName = $"{cabinetHome.ToText()}{this.ExetendedFileName}";
            fullFileName = $"{this.FilePath}{fileName}";
            */
            fileName = cabinetHome.ToText()+ this.ExetendedFileName;
            fullFileName = this.FilePath+ fileName;
            return fullFileName;
        }

        /// <summary>LockCrystalBox(水晶盒) 的點位檔案</summary>
        /// <returns></returns>
        public string LockCrystalBoxPathFile()
        {
            /** // vs 2013
            var fileName = $"LockCrystalBox{this.ExetendedFileName}";
            var fullFileName = $"{this.FilePath}{fileName}";
             */ 
            var fileName = "LockCrystalBox" + this.ExetendedFileName;
            var fullFileName =this.FilePath + fileName;
            return fullFileName;
        }
        /// <summary>UnlockCrystalBox(水晶盒)的點位檔案</summary>
        /// <returns></returns>
        public string UnlockCrystalBoxPathFile()
        {
            var fileName = $"UnlockCrystalBox{this.ExetendedFileName}";
            var fullFileName = $"{this.FilePath}{fileName}";
            return fullFileName;
        }
        /// <summary>LockCrystalBox(鐵盒) 的點位檔案</summary>
        /// <returns></returns>
        public string LockIronBoxPathFile()
        {
            var fileName = $"LockIronBox{this.ExetendedFileName}";
            var fullFileName = $"{this.FilePath}{fileName}";
            return fullFileName;
        }
        /// <summary>UnlockCrystalBox(鐵盒)的點位檔案</summary>
        /// <returns></returns>
        public string UnlockIronBoxPathFile()
        {
            var fileName = $"UnlockIronBox{this.ExetendedFileName}";
            var fullFileName = $"{this.FilePath}{fileName}";
            return fullFileName;
        }



        /// <summary>Cabinet 01 Home 的點位檔案</summary>
        /// <returns></returns>
        public string Cabinet01HomePathFile()
        {
            var rtnV = this.CabinetHomePathFile(BoxrobotTransferLocation.Cabinet_01_Home);
            return rtnV;
        }

        /// <summary>cabinet 02 Home 的點位檔案</summary>
        /// <returns></returns>
        public string Cabinet02HomePathFile()
        {
            var rtnV = this.CabinetHomePathFile(BoxrobotTransferLocation.Cabinet_02_Home);
            return rtnV;
        }

        /// <summary>從Cabinet 1 Home 到 指定的 Drawer (Get)</summary>
        /// <param name="drawer"></param>
        /// <returns></returns>
        public string FromCabinet01HomeToDrawer_GET_PathFile(BoxrobotTransferLocation drawer)
        {  //[V]
            var fullFileName = FromStartPointToDestinationPathFile(BoxrobotTransferLocation.Cabinet_01_Home, drawer, BoxrobotTransferDirection.Forward, BoxrobotTransferActionType.GET);
            return fullFileName;
        }
        /// <summary>從Cabinet 2 Home 到 指定的 Drawer (Get)</summary>
        /// <param name="drawer"></param>
        /// <returns></returns>
        public string FromCabinet02HomeToDrawer_GET_PathFile(BoxrobotTransferLocation drawer)
        {  //[V]
            var fullFileName = FromStartPointToDestinationPathFile(BoxrobotTransferLocation.Cabinet_02_Home, drawer, BoxrobotTransferDirection.Forward, BoxrobotTransferActionType.GET);
            return fullFileName;
        }

        /// <summary>從Cabinet 1 Home 到指定的 Drawer (PUT)</summary>
        /// <param name="drawer"></param>
        /// <returns></returns>
        public string FromCabinet01HomeToDrawer_PUT_PathFile(BoxrobotTransferLocation drawer)
        {//[V]
            var fullFileName = FromStartPointToDestinationPathFile(BoxrobotTransferLocation.Cabinet_01_Home, drawer, BoxrobotTransferDirection.Forward, BoxrobotTransferActionType.PUT);
            return fullFileName;
        }

        /// <summary>從Cabinet 2 Home 到指定的 Drawer (PUT)</summary>
        /// <param name="drawer">指定的Drawer</param>
        /// <returns></returns>
        public string FromCabinet02HomeToDrawer_PUT_PathFile(BoxrobotTransferLocation drawer)
        {
            var fullFileName = FromStartPointToDestinationPathFile(BoxrobotTransferLocation.Cabinet_02_Home, drawer, BoxrobotTransferDirection.Forward, BoxrobotTransferActionType.PUT);
            return fullFileName;
        }

        /// <summary>從 指定的Drawer 到 Cabinet 1 Home(Get)</summary>
        /// <param name="drawer">指定的 Drawer</param>
        /// <returns></returns>
        public string FromDrawerToCabinet01Home_GET_PathFile(BoxrobotTransferLocation drawer)
        {  
            var fullFileName = FromStartPointToDestinationPathFile(drawer,BoxrobotTransferLocation.Cabinet_01_Home,  BoxrobotTransferDirection.Backward, BoxrobotTransferActionType.GET);
            return fullFileName;
        }
        /// <summary>從 指定的Drawer 到 Cabinet 2 Home(Get)</summary>
        /// <param name="drawer">指定的 Drawer</param>
        /// <returns></returns>
        public string FromDrawerToCabinet02Home_GET_PathFile(BoxrobotTransferLocation drawer)
        {
            var fullFileName = FromStartPointToDestinationPathFile(drawer, BoxrobotTransferLocation.Cabinet_02_Home, BoxrobotTransferDirection.Backward, BoxrobotTransferActionType.GET);
            return fullFileName;
        }

        /// <summary>從 指定的Drawer 到 Cabinet 1 Home(PUT)</summary>
        /// <param name="drawer">指定的 Drawer</param>
        /// <returns></returns>
        public string FromDrawerToCabinet01Home_PUT_PathFile(BoxrobotTransferLocation drawer)
        {   
            var fullFileName = FromStartPointToDestinationPathFile(drawer, BoxrobotTransferLocation.Cabinet_01_Home, BoxrobotTransferDirection.Backward, BoxrobotTransferActionType.PUT);
            return fullFileName;
        }
        /// <summary>從 指定的Drawer 到 Cabinet 2 Home(PUT)</summary>
        /// <param name="drawer">指定的 Drawer</param>
        /// <returns></returns>
        public string FromDrawerToCabinet02Home_PUT_PathFile(BoxrobotTransferLocation drawer)
        {
            var fullFileName = FromStartPointToDestinationPathFile(drawer, BoxrobotTransferLocation.Cabinet_02_Home, BoxrobotTransferDirection.Backward, BoxrobotTransferActionType.PUT);
            return fullFileName;
        }

        /// <summary>從Cabinet 1 Home 到 OpenStage  Get</summary>
        /// <returns></returns>
        public string FromCabinet01HomeToOpenStage_GET_PathFile()
        {
            var fileName = FromStartPointToDestinationPathFile(BoxrobotTransferLocation.Cabinet_01_Home, BoxrobotTransferLocation.OpenStage, BoxrobotTransferDirection.Forward, BoxrobotTransferActionType.GET);
            return fileName;
        }

        /// <summary>從Cabinet 1 Home 到 OpenStage  Get</summary>
        /// <returns></returns>
        public string FromCabinet01HomeToOpenStage_PUT_PathFile()
        {
            var fileName = FromStartPointToDestinationPathFile(BoxrobotTransferLocation.Cabinet_01_Home, BoxrobotTransferLocation.OpenStage, BoxrobotTransferDirection.Forward, BoxrobotTransferActionType.PUT);
            return fileName;
        }


        public string FromOpenStageToCabinet01Home_GET_PathFile()
        {
            var fileName = FromStartPointToDestinationPathFile(BoxrobotTransferLocation.OpenStage, BoxrobotTransferLocation.Cabinet_01_Home, BoxrobotTransferDirection.Backward, BoxrobotTransferActionType.GET);
            return fileName;
        }

        public string FromOpenStageToCabinet01Home_PUT_PathFile()
        {
            var fileName = FromStartPointToDestinationPathFile(BoxrobotTransferLocation.OpenStage, BoxrobotTransferLocation.Cabinet_01_Home, BoxrobotTransferDirection.Backward, BoxrobotTransferActionType.PUT);
            return fileName;
        }
    }
}
