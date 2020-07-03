using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.JSon.RobotTransferFile
{
   public class BoxRobotTransferPathFile:BaserobotTransferPathFile
    {
        /// <summary>檔案名稱連接符號</summary>
        private const string FileNameConnectSign = "_";
        public BoxRobotTransferPathFile() : base()
        {

        }
        public BoxRobotTransferPathFile(string path) : base(path)
        {

        }
        public BoxRobotTransferPathFile(string path, string extendedName) : base(path, extendedName)
        {

        }


        /// <summary>檔案名稱</summary>
        /// <param name="startLoc"></param>
        /// <param name="target"></param>
        /// <param name="direction"></param>
        /// <param name="actionType"></param>
        /// <returns></returns>
        public string GetFromStartPointToDestinationPathFile(BoxrobotTransferLocation startPoint, BoxrobotTransferLocation destination,BoxrobotTransferDirection direction, BoxrobotTransferActionType actionType)
        {
            string fullFileName,fileName;
            const string connetSign= FileNameConnectSign;
            fileName = $"{startPoint.ToText()}{connetSign}{destination.ToText()}{connetSign}{actionType}{this.ExetendedFileName}";
            fullFileName = $"{this.FilePath}{fileName}";
            return fullFileName;
        }

        /// <summary>從Cabinet 1 Home 到 指定的 Drawer (Get)</summary>
        /// <param name="drawer"></param>
        /// <returns></returns>
        public string GetFromCabinet01HomeToDrawerGetPathFile(BoxrobotTransferLocation drawer)
        {
            var fullFileName = GetFromStartPointToDestinationPathFile(BoxrobotTransferLocation.Cabinet_01_Home, drawer, BoxrobotTransferDirection.Forward, BoxrobotTransferActionType.Get);
            return fullFileName;
        }
        /// <summary>從Cabinet 2 Home 到 指定的 Drawer (Get)</summary>
        /// <param name="drawer"></param>
        /// <returns></returns>
        public string GetFromCabinet02HomeToDrawerGetPathFile(BoxrobotTransferLocation drawer)
        {
            var fullFileName = GetFromStartPointToDestinationPathFile(BoxrobotTransferLocation.Cabinet_02_Home, drawer, BoxrobotTransferDirection.Forward, BoxrobotTransferActionType.Get);
            return fullFileName;
        }

        /// <summary>從Cabinet 1 Home 到指定的 Drawer (PUT)</summary>
        /// <param name="drawer"></param>
        /// <returns></returns>
        public string GetFromCabinet01HomeToDrawerPutPathFile(BoxrobotTransferLocation drawer)
        {
            var fullFileName = GetFromStartPointToDestinationPathFile(BoxrobotTransferLocation.Cabinet_01_Home, drawer, BoxrobotTransferDirection.Forward, BoxrobotTransferActionType.Put);
            return fullFileName;
        }

        /// <summary>從Cabinet 2 Home 到指定的 Drawer (PUT)</summary>
        /// <param name="drawer">指定的Drawer</param>
        /// <returns></returns>
        public string GetFromCabinet02HomeToDrawerPutPathFile(BoxrobotTransferLocation drawer)
        {
            var fullFileName = GetFromStartPointToDestinationPathFile(BoxrobotTransferLocation.Cabinet_02_Home, drawer, BoxrobotTransferDirection.Forward, BoxrobotTransferActionType.Put);
            return fullFileName;
        }

        /// <summary>從 指定的Drawer 到 Cabinet 1 Home(Get)</summary>
        /// <param name="drawer">指定的 Drawer</param>
        /// <returns></returns>
        public string GetFromDrawerToCabinet01HomeGetPathFile(BoxrobotTransferLocation drawer)
        {
            var fullFileName = GetFromStartPointToDestinationPathFile(drawer,BoxrobotTransferLocation.Cabinet_01_Home,  BoxrobotTransferDirection.Backword, BoxrobotTransferActionType.Get);
            return fullFileName;
        }
        /// <summary>從 指定的Drawer 到 Cabinet 2 Home(Get)</summary>
        /// <param name="drawer">指定的 Drawer</param>
        /// <returns></returns>
        public string GetFromDrawerToCabinet02HomeGetPathFile(BoxrobotTransferLocation drawer)
        {
            var fullFileName = GetFromStartPointToDestinationPathFile(drawer, BoxrobotTransferLocation.Cabinet_02_Home, BoxrobotTransferDirection.Backword, BoxrobotTransferActionType.Get);
            return fullFileName;
        }

        /// <summary>從 指定的Drawer 到 Cabinet 1 Home(Get)</summary>
        /// <param name="drawer">指定的 Drawer</param>
        /// <returns></returns>
        public string GetFromDrawerToCabinet01HomePutPathFile(BoxrobotTransferLocation drawer)
        {
            var fullFileName = GetFromStartPointToDestinationPathFile(drawer, BoxrobotTransferLocation.Cabinet_01_Home, BoxrobotTransferDirection.Backword, BoxrobotTransferActionType.Put);
            return fullFileName;
        }
        /// <summary>從 指定的Drawer 到 Cabinet 2 Home(Get)</summary>
        /// <param name="drawer">指定的 Drawer</param>
        /// <returns></returns>
        public string GetFromDrawerToCabinet02HomePutPathFile(BoxrobotTransferLocation drawer)
        {
            var fullFileName = GetFromStartPointToDestinationPathFile(drawer, BoxrobotTransferLocation.Cabinet_02_Home, BoxrobotTransferDirection.Backword, BoxrobotTransferActionType.Put);
            return fullFileName;
        }

    }
}
