using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.JSon.RobotTransferFile
{
    public  class MaskrobotTransferPathFile : BaserobotTransferPathFile
    {
        public MaskrobotTransferPathFile() : base() { }
        public MaskrobotTransferPathFile(string path) : base(path) {  }
        public MaskrobotTransferPathFile(string path,string extendedName):base(path,extendedName){ }
        public const string FileConnectionString = "To";
        private string HomeFile(MaskrobotTransferLocation home)
        {

            string fileName = $"{this.FilePath}{home.ToText()}{this.ExetendedFileName}";
            return fileName;
        }

        private string FromStartPointToDestinationPathFile(MaskrobotTransferLocation startPoint, MaskrobotTransferLocation destination)
        {
            string fileName = $"{this.FilePath}{startPoint.ToText()}{MaskrobotTransferPathFile.FileConnectionString}{destination.ToText()}{this.ExetendedFileName}";
            return fileName;
        }

        public string LoadPortHomePathFile()
        {
            return HomeFile(MaskrobotTransferLocation.LoadPortHome);
        }
        public string InspChHomePathFile()
        {
            return HomeFile(MaskrobotTransferLocation.InspChHome);
        }
        public string CleanChHomePathFile()
        {
            return HomeFile(MaskrobotTransferLocation.CleanChHome);
        }

        public string FromLPHomeToLP1PathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.LPHome, MaskrobotTransferLocation.LP1);
        }
        public string FromLPHomeToLP2PathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.LPHome, MaskrobotTransferLocation.LP2);
        }
        public string FromLP1ToLPHomePathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.LP1, MaskrobotTransferLocation.LPHome);
        }
        public string FromLP2ToLPHomePathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.LP2, MaskrobotTransferLocation.LPHome);
        }
        public string FromLPHomeToOSPathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.LPHome, MaskrobotTransferLocation.OS);
        }
        public string FromOSToLPHomePathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.OS, MaskrobotTransferLocation.LPHome);
        }
        public string FromICHomeFrontSideToICPathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.ICHomeFrontSide, MaskrobotTransferLocation.IC);
        }
        public string FromICHomeBackSideToICPathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.ICHomeBackSide, MaskrobotTransferLocation.IC);
        }
        public string FromICFrontSideToICHomePathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.ICFrontSide, MaskrobotTransferLocation.ICHome);
        }
        public string FromICBackSideToICHomePathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.ICBackSide, MaskrobotTransferLocation.ICHome);
        }
        public string FromCCHomeFrontSideToCleanPathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.CCHomeFrontSide, MaskrobotTransferLocation.Clean);
        }

        public string FromCCHomeFrontSideToCameraPathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.CCHomeFrontSide, MaskrobotTransferLocation.Camera);
        }
        public string FromCCHomeBackSideToCleanPathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.CCHomeBackSide, MaskrobotTransferLocation.Clean);
        }
        public string FromCCHomeBackSideToCameraPathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.CCHomeBackSide, MaskrobotTransferLocation.Camera);
        }
    }
}
