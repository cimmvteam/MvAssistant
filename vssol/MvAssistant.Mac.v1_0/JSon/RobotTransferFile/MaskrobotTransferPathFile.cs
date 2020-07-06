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

        public string FromICHomeToDeformInspPathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.ICHome, MaskrobotTransferLocation.DeformInsp);
        }

        public string FromDeformInspTICHomeoPathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.DeformInsp, MaskrobotTransferLocation.ICHome);
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
        public string FromCCHomeFrontSideToCCPathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.CCHomeFrontSide, MaskrobotTransferLocation.CC);
        }

        public string FromCCFrontSideToCCHomePathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.CCFrontSide, MaskrobotTransferLocation.CCHome);
        }

        public string FromCCFrontSideToCleanPathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.CCFrontSide, MaskrobotTransferLocation.Clean);
        }

        public string FromFrontSideCleanFinishToCCPathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.FrontSideCleanFinish, MaskrobotTransferLocation.CC);
        }
        public string FromCCFrontSideToCapturePathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.CCFrontSide, MaskrobotTransferLocation.Capture);
        }
        public string FromFrontSideCaptureFinishToCCPathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.FrontSideCaptureFinish, MaskrobotTransferLocation.CC);
        }
        public string FromCCHomeBackSideToCCPathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.CCHomeBackSide, MaskrobotTransferLocation.CC);
        }
        public string FromCCBackSideToCCHomePathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.CCBackSide, MaskrobotTransferLocation.CCHome);
        }
        public string FromCCBackSideToCleanPathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.CCBackSide, MaskrobotTransferLocation.Clean);
        }
        public string FromBackSideCleanFinishToCCPathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.BackSideCleanFinish, MaskrobotTransferLocation.CC);
        }
        public string FromCCBackSideToCapturePathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.CCBackSide, MaskrobotTransferLocation.Capture);
        }
         public string FromBackSideCaptureFinishToCCPathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.BackSideCapture, MaskrobotTransferLocation.CC);
        }

        /**
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
    */
    }
}
