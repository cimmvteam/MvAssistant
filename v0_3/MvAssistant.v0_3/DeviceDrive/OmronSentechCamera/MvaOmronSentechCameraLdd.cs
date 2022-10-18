// 如果要使用GUI功能，請取消註解以下內容，以定義ENABLED_ST_GUI並進行進一步的操作
//#define ENABLED_ST_GUI
using Sentech.StApiDotNET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace MvAssistant.v0_3.DeviceDrive.OmronSentechCamera
{
    public class MvaOmronSentechCameraLdd : IDisposable
    {
        public MvaOmronSentechCameraLdd(CStDevice device = null, CStDataStream dataStream = null)
        {
            this.device = device;
            this.dataStream = dataStream;
        }
        ~MvaOmronSentechCameraLdd() { this.Dispose(false); }

        // 要取得的影像數量
        const int nCountOfImagesToGrab = 1;

        //CStApiAutoInit api;
        //CStSystem system;
        //CStDevice[] StDevice;
        //CStDataStream[] StDataStream;
        CStImageBuffer imageBuffer;
        CStStillImageFiler stillImageFiler;
        IStImage stImage;
        Image image;
        Bitmap bmp;
        string FileName;
        //List<IStImage> ImageList;
        //List<string> imgFileNameList;
        CStDevice device;
        CStDataStream dataStream;

        public void Connect()
        {
            // 建立一個站存區儲存來自StApiRaw檔案的影像資料
            imageBuffer = CStApiDotNet.CreateStImageBuffer();

            // 建立一個靜止影像的物件來處理靜止影像
            stillImageFiler = new CStStillImageFiler();
            stImage = null;
            image = null;
            bmp = null;
            FileName = "";
        }
        public void Close()
        {
            if (dataStream != null)
                dataStream.Dispose();

            if (device != null)
                device.Dispose();

            if (imageBuffer != null)
                imageBuffer.Dispose();

            if (stillImageFiler != null)
                stillImageFiler.Dispose();
        }

        public string getDeviceName()
        { return device.GetIStDeviceInfo().DisplayName; }

        public string getDeviceID()
        { return device.GetIStDeviceInfo().ID; }

        //public void CaptureSaveSyn(string SavePath, string FileType)
        //{
        //    try
        //    {
        //        stImage = null;
        //        // 顯示裝置名稱
        //        Console.WriteLine("Device=" + device.GetIStDeviceInfo().DisplayName);

        //        //dataStream.RegisterCallbackMethod(OnCallback);

        //        // 主機端獲取影像
        //        dataStream.StartAcquisition(1);

        //        // 開始由相機取得影像
        //        device.AcquisitionStart();

        //        // 逾時超過5000ms後，回收儲存影像資料的暫存區
        //        // 使用 'using' 語法可在不需使用時，自動管理暫存區重新排隊操作
        //        using (CStStreamBuffer streamBuffer = dataStream.RetrieveBuffer(5000))
        //        {
        //            // 檢查取得的資料是否包含影像資料
        //            if (streamBuffer.GetIStStreamBufferInfo().IsImagePresent)
        //            {
        //                string sFT = FileType.ToLower();
        //                stImage = streamBuffer.GetIStImage();
        //                SavePath += (@"\" + device.GetIStDeviceInfo().DisplayName + @"\" + DateTime.Now.ToString("yyyyMMdd_HHmmss"));
        //                if (sFT == "bitmap" || sFT == "bmp" || sFT == ".bmp")
        //                {
        //                    SavePath += ".jpg";
        //                }
        //                else if (sFT == "tiff" || sFT == "tif" || sFT == ".tif")
        //                {
        //                    SavePath += ".tif";
        //                }
        //                else if (sFT == "png" || sFT == ".png")
        //                {
        //                    SavePath += ".png";
        //                }
        //                else if (sFT == "jpeg" || sFT == "jpg" || sFT == ".jpg")
        //                {
        //                    SavePath += ".jpg";
        //                    stillImageFiler.Quality = 75;
        //                }
        //                else if (sFT == "csv" || sFT == ".csv")
        //                {
        //                    SavePath += ".csv";
        //                }
        //                else //要轉換的檔案格式(副檔名)錯誤
        //                    throw new Exception("File Type was wrong.");

        //                this.SaveImage(stImage, SavePath, sFT);

        //                device.AcquisitionStop();
        //                dataStream.StopAcquisition();
        //                //// 顯示接收影像的詳細資訊
        //                //Byte[] imageData = stImage.GetByteArray();
        //                //Console.Write("BlockId=" + streamBuffer.GetIStStreamBufferInfo().FrameID);
        //                //Console.Write(" Size:" + stImage.ImageWidth + " x " + stImage.ImageHeight);
        //                //Console.Write(" First byte =" + imageData[0] + Environment.NewLine);


        //                //var width = (int)stImage.ImageWidth;
        //                //var height = (int)stImage.ImageHeight;
        //                //myimg = new Bitmap(width, height);

        //                ////TODO: https://stackoverflow.com/questions/3474434/set-individual-pixels-in-net-format16bppgrayscale-image

        //                //for (var idx = 0; idx < imageData.Length; idx++)
        //                //{
        //                //    var val = imageData[idx];
        //                //    var color = Color.FromArgb(val, val, val);
        //                //    myimg.SetPixel(idx % width, idx / width, color);
        //                //}
        //            }
        //            else
        //            {
        //                // 如果取得的資料不含影像資料
        //                Console.WriteLine("Image data does not exist.");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        device.AcquisitionStop();
        //        dataStream.StopAcquisition();

        //        throw ex;
        //    }
        //}

        public Bitmap CaptureToImageSyn()
        {
            try
            {
                stImage = null;
                bmp = null;
                CStPixelFormatConverter m_Converter = new CStPixelFormatConverter();
                // 顯示裝置名稱
                Console.WriteLine("Device=" + device.GetIStDeviceInfo().DisplayName);

                //dataStream.RegisterCallbackMethod(OnCallback);

                // 主機端獲取影像
                dataStream.StartAcquisition(1);

                // 開始由相機取得影像
                device.AcquisitionStart();

                // 逾時超過5000ms後，回收儲存影像資料的暫存區
                // 使用 'using' 語法可在不需使用時，自動管理暫存區重新排隊操作
                using (CStStreamBuffer streamBuffer = dataStream.RetrieveBuffer(5000))
                {
                    // 檢查取得的資料是否包含影像資料
                    if (streamBuffer.GetIStStreamBufferInfo().IsImagePresent)
                    {
                        stImage = streamBuffer.GetIStImage();

                        #region 如果取得的影像是彩色，需經過轉換才能得到每Pixel以RGB排列的Byte[]
                        bool isColor = CStApiDotNet.GetIStPixelFormatInfo(stImage.ImagePixelFormat).IsColor;
                        if (isColor)
                        {
                            // Convert the image data to BGR8 format.
                            m_Converter.DestinationPixelFormat = eStPixelFormatNamingConvention.BGR8;
                        }
                        else
                        {
                            // Convert the image data to Mono8 format.
                            m_Converter.DestinationPixelFormat = eStPixelFormatNamingConvention.Mono8;
                        }
                        m_Converter.Convert(stImage, imageBuffer);
                        #endregion
                        //Byte[] imageData = stImage.GetByteArray();
                        byte[] imageData = imageBuffer.GetIStImage().GetByteArray();

                        var width = (int)stImage.ImageWidth;
                        var height = (int)stImage.ImageHeight;

                        //如果影像是彩色的
                        if (isColor)
                            bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                        else//灰階影像
                        {
                            bmp = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
                            //// 下面的代碼是為了修改生成位圖的索引表，從偽彩修改為灰度  
                            ColorPalette palette = bmp.Palette;
                            for (int i = 0; i < 256; i++) palette.Entries[i] = Color.FromArgb(i, i, i);
                            bmp.Palette = palette;
                        }
                        // Lock the bits of the bitmap.
                        BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, bmp.PixelFormat);
                        // Place the pointer to the buffer of the bitmap.
                        IntPtr ptr = bmpData.Scan0;

                        // fill in rgbValues
                        Marshal.Copy(imageData, 0, ptr, imageData.Length);
                        bmp.UnlockBits(bmpData);

                        //bmp.Save(@"D:/" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".jpg", ImageFormat.Jpeg);
                    }
                    else
                    {
                        // 如果取得的資料不含影像資料
                        Console.WriteLine("Image data does not exist.");
                    }
                }
                return bmp;
            }
            catch (Exception ex)
            {
                device.AcquisitionStop();
                dataStream.StopAcquisition();

                throw ex;
            }
            finally
            {
                device.AcquisitionStop();
                dataStream.StopAcquisition();
                GC.Collect();
            }
        }

        /// <summary>
        /// 非同步取得影像的OnCallback函數無法順利執行(目前不使用)
        /// </summary>
        /// <returns></returns>
        public Bitmap CaptureToImageAsyn()
        {
            try
            {
                stImage = null;
                bmp = null;
                CStPixelFormatConverter m_Converter = new CStPixelFormatConverter();

                dataStream.RegisterCallbackMethod(OnCallback);
                dataStream.StartAcquisition(1);
                device.AcquisitionStart();

                if (SpinWait.SpinUntil(() => stImage != null, 10000))
                {
                    device.AcquisitionStop();
                    dataStream.StopAcquisition();

                    bool isColor = CStApiDotNet.GetIStPixelFormatInfo(stImage.ImagePixelFormat).IsColor;
                    if (isColor)
                    {
                        // Convert the image data to BGR8 format.
                        m_Converter.DestinationPixelFormat = eStPixelFormatNamingConvention.BGR8;
                    }
                    else
                    {
                        // Convert the image data to Mono8 format.
                        m_Converter.DestinationPixelFormat = eStPixelFormatNamingConvention.Mono8;
                    }
                    m_Converter.Convert(stImage, imageBuffer);
                    byte[] imageData = imageBuffer.GetIStImage().GetByteArray();
                    MemoryStream ms = new MemoryStream(imageData);
                    bmp = (Bitmap)Bitmap.FromStream(ms);
                }
                else
                    throw new Exception("Camera(" + device.GetIStDeviceInfo().DisplayName + ") can not capture image!!");


            }
            catch (Exception ex)
            {
                device.AcquisitionStop();
                dataStream.StopAcquisition();

                throw ex;
            }
            return bmp;
        }

        public IStImage CaptureRaw()
        {
            try
            {
                stImage = null;

                dataStream.RegisterCallbackMethod(OnCallback);
                dataStream.StartAcquisition(1);
                device.AcquisitionStart();
                Thread.Sleep(1000);
                if (SpinWait.SpinUntil(() => stImage != null, 10000))
                {
                    device.AcquisitionStop();
                    dataStream.StopAcquisition();
                }
                else
                    throw new Exception("Camera(" + device.GetIStDeviceInfo().DisplayName + ") can not capture image!!");
            }
            catch (Exception ex)
            {
                device.AcquisitionStop();
                dataStream.StopAcquisition();

                throw ex;
            }
            return stImage;
        }

        //public void CaputreAndSave(string fn, string fType)
        //{
        //    var img = this.CaptureRaw();
        //    this.SaveImage(img, fn, fType);

        //}




        //public void Capture(int intDvcIdx)
        //{
        //    try
        //    {
        //        // 顯示裝置名稱
        //        Console.WriteLine("Device=" + StDevice[intDvcIdx].GetIStDeviceInfo().DisplayName);

        //        // 取得影像資料夾的路徑
        //        string fileNameHeader = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        //        fileNameHeader += @"\" + StDevice[intDvcIdx].GetIStDeviceInfo().DisplayName + @"\" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
        //        imgFileNameList.Add(fileNameHeader);

        //        object[] param = { intDvcIdx };
        //        StDataStream[intDvcIdx].RegisterCallbackMethod(OnCallback, param);

        //        // 主機端獲取影像
        //        StDataStream[intDvcIdx].StartAcquisition(1);

        //        // 開始由相機取得影像
        //        StDevice[intDvcIdx].AcquisitionStart();

        //        //// 循環取得資料並檢查狀態
        //        //// 持續執行取得影像直到足夠的幀數
        //        ////while (dataStream.IsGrabbing)
        //        //{
        //        //    // 逾時超過5000ms後，回收儲存影像資料的暫存區
        //        //    // 使用 'using' 語法可在不需使用時，自動管理暫存區重新排隊操作
        //        //    using (CStStreamBuffer streamBuffer = dataStream[intDvcIdx].RetrieveBuffer(5000))
        //        //    {
        //        //        // 檢查取得的資料是否包含影像資料
        //        //        if (streamBuffer.GetIStStreamBufferInfo().IsImagePresent)
        //        //        {
        //        //            // 若是，建立IStImage物件已進行進一步的影像處理
        //        //            //IStImage[] stImage=new IStImage[1];
        //        //            ImageList.Add(streamBuffer.GetIStImage());

        //        //            {
        //        //                // JPEG file extension.
        //        //                string imageFileName = fileNameHeader + ".jpg";

        //        //                // Save the image file in JPEG format.
        //        //                stillImageFiler.Quality = 75;
        //        //                Console.Write(Environment.NewLine + "Saving " + imageFileName + "... ");
        //        //                stillImageFiler.Save(ImageList[0], eStStillImageFileFormat.JPEG, imgFileNameList[0]);
        //        //                Console.Write("done" + Environment.NewLine);
        //        //                ImageList.RemoveAt(0);
        //        //                imgFileNameList.RemoveAt(0);
        //        //            }

        //        //            //// 顯示接收影像的詳細資訊
        //        //            //Byte[] imageData = stImage.GetByteArray();
        //        //            //Console.Write("BlockId=" + streamBuffer.GetIStStreamBufferInfo().FrameID);
        //        //            //Console.Write(" Size:" + stImage.ImageWidth + " x " + stImage.ImageHeight);
        //        //            //Console.Write(" First byte =" + imageData[0] + Environment.NewLine);


        //        //            //var width = (int)stImage.ImageWidth;
        //        //            //var height = (int)stImage.ImageHeight;
        //        //            //myimg = new Bitmap(width, height);

        //        //            ////TODO: https://stackoverflow.com/questions/3474434/set-individual-pixels-in-net-format16bppgrayscale-image

        //        //            //for (var idx = 0; idx < imageData.Length; idx++)
        //        //            //{
        //        //            //    var val = imageData[idx];
        //        //            //    var color = Color.FromArgb(val, val, val);
        //        //            //    myimg.SetPixel(idx % width, idx / width, color);
        //        //            //}
        //        //        }
        //        //        else
        //        //        {
        //        //            // 如果取得的資料不含影像資料
        //        //            Console.WriteLine("Image data does not exist.");
        //        //        }
        //        //    }
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        // 停止相機取像
        //        StDevice[intDvcIdx].AcquisitionStop();
        //        // 停止主機取像
        //        StDataStream[intDvcIdx].StopAcquisition();
        //    }
        //}

        // Method for handling callback action
        public void OnCallback(IStCallbackParamBase paramBase, object[] param)
        {
            int DeviceIdx = (int)param[0];
            // Check callback type. Only NewBuffer event is handled in here
            if (paramBase.CallbackType == eStCallbackType.TL_DataStreamNewBuffer)
            {
                // In case of receiving a NewBuffer events:
                // Convert received callback parameter into IStCallbackParamGenTLEventNewBuffer for acquiring additional information.
                IStCallbackParamGenTLEventNewBuffer callbackParam = paramBase as IStCallbackParamGenTLEventNewBuffer;

                if (callbackParam != null)
                {
                    try
                    {
                        // Get the IStDataStream interface object from the received callback parameter.
                        IStDataStream dataStream = callbackParam.GetIStDataStream();

                        // Retrieve the buffer of image data for that callback indicated there is a buffer received.
                        using (CStStreamBuffer streamBuffer = dataStream.RetrieveBuffer(0))
                        {
                            // Check if the acquired data contains image data.
                            if (streamBuffer.GetIStStreamBufferInfo().IsImagePresent)
                            {
                                // If yes, we create a IStImage object for further image handling.
                                IStImage stImage = streamBuffer.GetIStImage();
                                //ImageList.Add(stImage);
                                this.stImage = stImage;
                            }
                            else
                            {
                                // If the acquired data contains no image data.
                                Console.WriteLine("Image data does not exist.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // If any exception occurred, display the description of the error here.
                        throw ex;
                    }
                }
            }
        }


        //public int SaveImage(Bitmap bmp, string strSavePath, string strFileType)
        //{
        //    int intSavedCnt = 0;
        //    string sFT = strFileType.ToLower();
        //    try
        //    {
        //        strSavePath += (@"\" + device.GetIStDeviceInfo().DisplayName + @"\" + DateTime.Now.ToString("yyyyMMdd_HHmmss"));
        //        if (sFT == "bitmap" || sFT == "bmp" || sFT == ".bmp")
        //        {
        //            strSavePath += ".bmp";
        //            bmp.Save(strSavePath, ImageFormat.Bmp);
        //            intSavedCnt++;
        //        }
        //        else if (sFT == "tiff" || sFT == "tif" || sFT == ".tif")
        //        {
        //            strSavePath += ".tif";
        //            bmp.Save(strSavePath, ImageFormat.Tiff);
        //            intSavedCnt++;
        //        }
        //        else if (sFT == "png" || sFT == ".png")
        //        {
        //            strSavePath += ".png";
        //            bmp.Save(strSavePath, ImageFormat.Png);
        //            intSavedCnt++;
        //        }
        //        else if (sFT == "jpeg" || sFT == "jpg" || sFT == ".jpg")
        //        {
        //            strSavePath += ".jpg";
        //            bmp.Save(strSavePath, ImageFormat.Jpeg);
        //            intSavedCnt++;
        //        }
        //        else //要轉換的檔案格式(副檔名)錯誤
        //            throw new Exception("File Type was wrong.");

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return intSavedCnt;
        //}

        //public void SaveImage(IStImage img, string fn, string sFileType)
        //{
        //    string sFT = sFileType.ToLower();
        //    try
        //    {
        //        if (sFT == "bitmap" || sFT == "bmp" || sFT == ".bmp")
        //        {
        //            stillImageFiler.Save(img, eStStillImageFileFormat.Bitmap, fn);
        //        }
        //        else if (sFT == "tiff" || sFT == "tif" || sFT == ".tif")
        //        {
        //            stillImageFiler.Save(img, eStStillImageFileFormat.TIFF, fn);
        //        }
        //        else if (sFT == "png" || sFT == ".png")
        //        {
        //            stillImageFiler.Save(img, eStStillImageFileFormat.PNG, fn);
        //        }
        //        else if (sFT == "jpeg" || sFT == "jpg" || sFT == ".jpg")
        //        {
        //            stillImageFiler.Quality = 75;
        //            stillImageFiler.Save(img, eStStillImageFileFormat.JPEG, fn);
        //        }
        //        else if (sFT == "csv" || sFT == ".csv")
        //        {
        //            stillImageFiler.Save(img, eStStillImageFileFormat.CSV, fn);

        //        }
        //        else //要轉換的檔案格式(副檔名)錯誤
        //            throw new Exception("File Type was wrong.");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        #region IDisposable
        // Flag: Has Dispose already been called?
        protected bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //

            this.DisposeSelf();

            disposed = true;
        }


        protected virtual void DisposeSelf()
        {
            this.Close();
        }



        #endregion

    }
}
