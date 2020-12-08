using MvAssistant.v0_2.DeviceDrive.OmronSentechCamera;
using System;
using System.Drawing;
//using Sentech.StApiDotNET;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace MvAssistant.v0_2.Mac.Hal.CompCamera
{
    [GuidAttribute("90BA4CDC-7A82-454A-8F3F-6FE6413AEF41")]
    public class MacHalCameraSenTech : MacHalComponentBase, IHalCamera
    {
        #region Const
        public const string DevConnStr_Id = "id";
        #endregion

        #region DevConnStr
        string id;
        string resourceKey { get { return "resource://" + typeof(MvOmronSentechCameraScanner).Name; } }
        #endregion

        MvOmronSentechCameraLdd ldd;



        #region HAL

        public override int HalConnect()
        {
            this.id = this.GetDevConnStr(DevConnStr_Id);

            var scanner = this.HalContext.ResourceGetOrDefault<MvOmronSentechCameraScanner>(this.resourceKey);
            if (scanner == null)
            {
                scanner = new MvOmronSentechCameraScanner();
                scanner.Connect();
                scanner.ScanAlldevice();
                this.HalContext.ResourceRegister(this.resourceKey, scanner);
            }
            foreach (var camera in scanner.cameras)
            {
                if (camera.Key == id)
                {
                    ldd = camera.Value;
                    ldd.Connect();
                    break;
                }
            }

            if (this.ldd == null)
            {
                if (scanner.cameras.ContainsKey(this.id))
                {
                    this.ldd = scanner.cameras[this.id];
                    ldd.Connect();
                }
                //this.HalContext.ResourceRegister(this.resourceKey, this.ldd);
            }
            return 0;
        }

        public override int HalClose()
        {
            //可能有其它人在使用 Resource, 不在個別 HAL 裡釋放, 由 HalContext 統一釋放
            return 0;
        }


        #endregion


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
            return ldd.CaptureToImageSyn();
            //return ldd.CaptureToImageAsyn();
        }

        //public void Capture()
        //{
        //    IStImage img = ldd.CaptureRaw();
        //    ldd.SaveImage(img, Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + @"\" + ldd.getDeviceName() + @"\" + DateTime.Now.ToString("yyyyMMdd_HHmmss"), "jpg");
        //}

        public int ShotToSaveImage(string strSavePath, string strFileType)
        {
            Bitmap bmp = Shot();
            int intSavedCnt = 0;
            string sFT = strFileType.ToLower();
            try
            {
                if (!Directory.Exists(strSavePath + "/" + ldd.getDeviceName()))
                    Directory.CreateDirectory(strSavePath + "/" + ldd.getDeviceName());
                strSavePath += ("/" + ldd.getDeviceName() + "/" + DateTime.Now.ToString("yyyyMMdd_HHmmssfff"));
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
