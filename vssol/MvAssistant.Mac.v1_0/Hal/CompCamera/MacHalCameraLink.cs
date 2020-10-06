using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MvAssistant.DeviceDrive.CameraLink;
using MvAssistant.DeviceDrive.OmronSentechCamera;

namespace MvAssistant.Mac.v1_0.Hal.CompCamera
{
    [GuidAttribute("BF1D88C3-AEF5-4B2E-B161-336623A21804")]
    public class MacHalCameraLink : MacHalComponentBase, IHalCamera
    {
        #region Const
        public const string DevConnStr_Id = "id";
        #endregion

        #region Device Connection Str
        string id;
        string resourceKey { get { return "resource://" + typeof(MvCameraLinkLdd).Name; } }
        #endregion

        public MvCameraLinkLdd ldd;

        public MacHalCameraLink()
        {
        }

        public override int HalClose()
        {
            //throw new NotImplementedException();
            return 0;
        }

        public override int HalConnect()
        {
            this.id = this.GetDevConnStr(DevConnStr_Id);

            this.ldd = new MvCameraLinkLdd();
            ldd.Connect();

            return 0;
        }

        public void SetExposureTime(double mseconds)
        {
            throw new NotImplementedException();
        }

        public void SetFocus(double percentage)
        {
            throw new NotImplementedException();
        }

        public Bitmap Shot()
        {
            return ldd.Capture();
        }

        public int ShotToSaveImage(string strSavePath, string strFileType)
        {
            Bitmap bmp = Shot();
            int intSavedCnt = 0;
            string sFT = strFileType.ToLower();
            try
            {
                if (!Directory.Exists(strSavePath + "/CameraLink"))
                    Directory.CreateDirectory(strSavePath + "/CameraLink");
                strSavePath += ("/CameraLink/" + DateTime.Now.ToString("yyyyMMdd_HHmmss"));
                if (sFT == "bitmap" || sFT == "bmp" || sFT == ".bmp")
                {
                    strSavePath += ".bmp";
                    bmp.Save(strSavePath, ImageFormat.Bmp);
                    intSavedCnt++;
                }
                else if (sFT == "tiff" || sFT == "tif" || sFT == ".tif")
                {
                    strSavePath += ".tif";
                    bmp.Save(strSavePath, ImageFormat.Tiff);
                    intSavedCnt++;
                }
                else if (sFT == "png" || sFT == ".png")
                {
                    strSavePath += ".png";
                    bmp.Save(strSavePath, ImageFormat.Png);
                    intSavedCnt++;
                }
                else if (sFT == "jpeg" || sFT == "jpg" || sFT == ".jpg")
                {
                    strSavePath += ".jpg";
                    bmp.Save(strSavePath, ImageFormat.Jpeg);
                    intSavedCnt++;
                }
                else //要轉換的檔案格式(副檔名)錯誤
                    throw new Exception("File Type was wrong.");

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return intSavedCnt;
        }
    }
}
