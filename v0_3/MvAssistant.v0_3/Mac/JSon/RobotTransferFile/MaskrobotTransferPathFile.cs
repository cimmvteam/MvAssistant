﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MvAssistant.v0_3.Mac.JSon.RobotTransferFile
{
    public class MaskrobotTransferPathFile : BaserobotTransferPathFile
    {
        public MaskrobotTransferPathFile() : base() { }
        public MaskrobotTransferPathFile(string path) : base(path) { }
        public MaskrobotTransferPathFile(string path, string extendedName) : base(path, extendedName) { }
        public const string FileConnectionString = "To";
        private string SinglePositionFile(MaskrobotTransferLocation PositionName)
        {
            // vs 2013
            //string fileName = $"{this.FilePath}{home.ToText()}{this.ExetendedFileName}";
            string fileName = this.FilePath + PositionName.ToText() + this.ExetendedFileName;
            return fileName;
        }

        private string FromStartPointToDestinationPathFile(MaskrobotTransferLocation startPoint, MaskrobotTransferLocation destination)
        {
            // vs 2013
            // string fileName = $"{this.FilePath}{startPoint.ToText()}{MaskrobotTransferPathFile.FileConnectionString}{destination.ToText()}{this.ExetendedFileName}";
            string fileName = this.FilePath + startPoint.ToText() + MaskrobotTransferPathFile.FileConnectionString + destination.ToText() + this.ExetendedFileName;
            return fileName;
        }

        public string LoadPortHomePathFile()
        {
            return SinglePositionFile(MaskrobotTransferLocation.LoadPortHome);
        }
        public string InspChHomePathFile()
        {
            return SinglePositionFile(MaskrobotTransferLocation.InspChHome);
        }
        public string CleanChHomePathFile()
        {
            return SinglePositionFile(MaskrobotTransferLocation.CleanChHome);
        }

        public string CCFrontSideCleanMaskCenterFile()
        { return SinglePositionFile(MaskrobotTransferLocation.CCFrontSideCleanMaskCenter); }
        public string CCFrontSideCaptureMaskCenterFile()
        { return SinglePositionFile(MaskrobotTransferLocation.CCFrontSideCaptureMaskCenter); }
        public string CCBackSideCleanMaskCenterFile()
        { return SinglePositionFile(MaskrobotTransferLocation.CCBackSideCleanMaskCenter); }
        public string CCBackSideCaptureMaskCenterFile()
        { return SinglePositionFile(MaskrobotTransferLocation.CCBackSideCaptureMaskCenter); }

        public string ReadBarcodeFile()
        { return SinglePositionFile(MaskrobotTransferLocation.ReadBarcode); }

        public string ReadT7codeFile()
        { return SinglePositionFile(MaskrobotTransferLocation.ReadT7code); }

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

        /// <summary> Glass=Front Side </summary>
        public string FromICHomeToICFrontSidePathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.ICHome, MaskrobotTransferLocation.ICFrontSide);
        }
        /// <summary> Pellicle=Back Side </summary>
        public string FromICHomeToICBackSidePathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.ICHome, MaskrobotTransferLocation.ICBackSide);
        }
        /// <summary> Glass=Front Side </summary>
        public string FromICFrontSideToICStagePathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.ICFrontSide, MaskrobotTransferLocation.ICStage);
        }
        /// <summary> Pellicle=Back Side </summary>
        public string FromICBackSideToICStagePathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.ICBackSide, MaskrobotTransferLocation.ICStage);
        }
        /// <summary> Glass=Front Side </summary>
        public string FromICStageToICFrontSidePathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.ICStage, MaskrobotTransferLocation.ICFrontSide);
        }
        /// <summary> Pellicle=Back Side </summary>
        public string FromICStageToICBackSidePathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.ICStage, MaskrobotTransferLocation.ICBackSide);
        }
        /// <summary> Glass=Front Side </summary>
        public string FromICFrontSideToICHomePathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.ICFrontSide, MaskrobotTransferLocation.ICHome);
        }
        /// <summary> Pellicle=Back Side </summary>
        public string FromICBackSideToICHomePathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.ICBackSide, MaskrobotTransferLocation.ICHome);
        }
        /// <summary> Glass=Front Side </summary>
        public string FromCCHomeToCCFrontSidePathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.CCHome, MaskrobotTransferLocation.CCFrontSide);
        }
        /// <summary> Glass=Front Side </summary>
        public string FromCCFrontSideToCCHomePathFile()
        {
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.CCFrontSide, MaskrobotTransferLocation.CCHome);
        }
        /// <summary> Glass=Front Side </summary>
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
            return FromStartPointToDestinationPathFile(MaskrobotTransferLocation.BackSideCaptureFinish, MaskrobotTransferLocation.CC);
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
