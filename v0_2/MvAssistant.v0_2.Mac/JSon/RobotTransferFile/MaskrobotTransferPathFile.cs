using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MvAssistant.v0_2.Mac.JSon.RobotTransferFile
{
    public class MaskrobotTransferPathFile : BaserobotTransferPathFile
    {
        public MaskrobotTransferPathFile() : base() { }
        public MaskrobotTransferPathFile(string path) : base(path) { }
        public MaskrobotTransferPathFile(string path, string extendedName) : base(path, extendedName) { }
        public const string FileConnectionString = "To";
        private string HomeFile(MaskrobotTransferLocation home)
        {
             // vs 2013
            //string fileName = $"{this.FilePath}{home.ToText()}{this.ExetendedFileName}";
            string fileName =this.FilePath + home.ToText() + this.ExetendedFileName;
            return fileName;
        }

        private string FromStartPointToDestinationPathFile(MaskrobotTransferLocation startPoint, MaskrobotTransferLocation destination)
        {
            // vs 2013
            // string fileName = $"{this.FilePath}{startPoint.ToText()}{MaskrobotTransferPathFile.FileConnectionString}{destination.ToText()}{this.ExetendedFileName}";
            string fileName = this.FilePath + startPoint.ToText()+ MaskrobotTransferPathFile.FileConnectionString + destination.ToText()+ this.ExetendedFileName ;
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

        public string FromOSToIronBoxPathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.OS, MaskrobotTransferLocation.IronBox);
        }

        public string FromOSToCrystalBoxPathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.OS, MaskrobotTransferLocation.CrystalBox);
        }

        public string FromIronBoxToOSPathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.IronBox, MaskrobotTransferLocation.OS);
        }

        public string FromCrystalBoxToOSPathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.CrystalBox, MaskrobotTransferLocation.OS);
        }

        public string FromICHomeToDeformInspPathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.ICHome, MaskrobotTransferLocation.DeformInsp);
        }

        public string FromDeformInspTICHomeoPathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.DeformInsp, MaskrobotTransferLocation.ICHome);
        }


        public string FromICHomeToICFrontSidePathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.ICHome, MaskrobotTransferLocation.ICFrontSide);
        }
        public string FromICHomeToICBackSidePathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.ICHome, MaskrobotTransferLocation.ICBackSide);
        }
        public string FromICFrontSideToICStagePathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.ICFrontSide, MaskrobotTransferLocation.ICStage);
        }
        public string FromICBackSideToICStagePathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.ICBackSide, MaskrobotTransferLocation.ICStage);
        }
        public string FromICStageToICFrontSidePathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.ICStage, MaskrobotTransferLocation.ICFrontSide);
        }
        public string FromICStageToICBackSidePathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.ICStage, MaskrobotTransferLocation.ICBackSide);
        }
        public string FromICFrontSideToICHomePathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.ICFrontSide, MaskrobotTransferLocation.ICHome);
        }
        public string FromICBackSideToICHomePathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.ICBackSide, MaskrobotTransferLocation.ICHome);
        }
        public string FromCCHomeToCCFrontSidePathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.CCHome, MaskrobotTransferLocation.CCFrontSide);
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
        public string FromCCHomeToCCBackSidePathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.CCHome, MaskrobotTransferLocation.CCBackSide);
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
        public string FromLPHomeToBarcodeReaderPathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.LPHome, MaskrobotTransferLocation.BarcodeReader);
        }
        public string FromBarcodeReaderToLPHomePathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.BarcodeReader, MaskrobotTransferLocation.LPHome);
        }
        public string FromICHomeToInspDeformPathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.ICHome, MaskrobotTransferLocation.InspDeform);
        }
        public string FromInspDeformToICHomePathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.InspDeform, MaskrobotTransferLocation.ICHome);
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
