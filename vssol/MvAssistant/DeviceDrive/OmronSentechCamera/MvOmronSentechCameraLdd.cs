// 如果要使用GUI功能，請取消註解以下內容，以定義ENABLED_ST_GUI並進行進一步的操作
//#define ENABLED_ST_GUI
using Sentech.StApiDotNET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace MvAssistant.DeviceDrive.OmronSentechCamera
{
    public class MvOmronSentechCameraLdd : IDisposable
    {
        public MvOmronSentechCameraLdd(CStDevice device = null, CStDataStream dataStream = null)
        {
            this.device = device;
            this.dataStream = dataStream;
        }
        ~MvOmronSentechCameraLdd() { this.Dispose(false); }

        // 要取得的影像數量
        const int nCountOfImagesToGrab = 1;

        //CStApiAutoInit api;
        //CStSystem system;
        //CStDevice[] StDevice;
        //CStDataStream[] StDataStream;
        CStImageBuffer imageBuffer;
        CStStillImageFiler stillImageFiler;
        uint uInterface = 0;
        uint uCamCnt = 0;
        IStInterface StInterface;
        string[] cameraIDStringArray;
        string[] cameraNameStringArray;
        List<IStImage> ImageList;
        List<string> imgFileNameList;
        CStDevice device;
        CStDataStream dataStream;

        public void Connect()
        {
            //api = new CStApiAutoInit();
            //system = new CStSystem();
            //StDevice = new CStDevice[0];
            //StDataStream = new CStDataStream[0];
            // 建立一個站存區儲存來自StApiRaw檔案的影像資料
            imageBuffer = CStApiDotNet.CreateStImageBuffer();

            // 建立一個靜止影像的物件來處理靜止影像
            stillImageFiler = new CStStillImageFiler();
            cameraIDStringArray = new string[0];
            cameraNameStringArray = new string[0];
            ImageList = new List<IStImage>();
            imgFileNameList = new List<string>();
        }
        public void Close()
        {
            if (dataStream != null)
            {
                // 停止主機取像
                for (int i = 0; i < uCamCnt; i++)
                {
                    dataStream.StopAcquisition();
                    dataStream.Dispose();
                }
            }

            if (device != null)
            {
                // 停止相機取像
                for (int i = 0; i < uCamCnt; i++)
                {
                    device.AcquisitionStop();
                    device.Dispose();
                }
            }

            //if (system != null)
            //    system.Dispose();

            //if (api != null)
            //    api.Dispose();

            if (imageBuffer != null)
                imageBuffer.Dispose();

            if (stillImageFiler != null)
                stillImageFiler.Dispose();
        }

        //public string[] SearchAlldevice()
        //{

        //    uInterface = system.InterfaceCount;
        //    for (uint i = 0; i < uInterface; i++)
        //    {
        //        StInterface = system.GetIStInterface(i);
        //        IStInterfaceInfo tmpInterFaceInfoPtr = StInterface.GetIStInterfaceInfo();
        //        uint uintDeviceCnt = StInterface.DeviceCount;

        //        Array.Resize(ref cameraIDStringArray, (int)uCamCnt + (int)uintDeviceCnt);
        //        Array.Resize(ref cameraNameStringArray, (int)uCamCnt + (int)uintDeviceCnt);
        //        Array.Resize(ref StDevice, (int)uCamCnt + (int)uintDeviceCnt);
        //        Array.Resize(ref StDataStream, (int)uCamCnt + (int)uintDeviceCnt);

        //        for (uint j = 0; j < uintDeviceCnt; j++)
        //        {
        //            IStDeviceInfo tmpDeviceInfoPtr = StInterface.GetIStDeviceInfo(j);
        //            cameraIDStringArray[uCamCnt] = tmpDeviceInfoPtr.ID;
        //            cameraNameStringArray[uCamCnt] = tmpDeviceInfoPtr.DisplayName;

        //            eDeviceAccessFlags deviceAccessFlags = eDeviceAccessFlags.CONTROL;
        //            if (tmpDeviceInfoPtr.AccessStatus == eDeviceAccessStatus.READONLY)
        //            {
        //                deviceAccessFlags = eDeviceAccessFlags.READONLY;
        //            }
        //            StDevice[uCamCnt] = StInterface.CreateStDevice(cameraIDStringArray[uCamCnt], deviceAccessFlags);

        //            StDataStream[uCamCnt] = StDevice[uCamCnt].CreateStDataStream(0);

        //            uCamCnt++;
        //        }
        //    }
        //    return cameraNameStringArray;
        //}

        public void Capture()
        {
            try
            {
                // 顯示裝置名稱
                Console.WriteLine("Device=" + device.GetIStDeviceInfo().DisplayName);

                // 取得影像資料夾的路徑
                string fileNameHeader = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                fileNameHeader += @"\" + device.GetIStDeviceInfo().DisplayName + @"\" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
                //string fileNameHeader = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                //fileNameHeader += @"\" + StDevice[intDvcIdx].GetIStDeviceInfo().DisplayName + @"\" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
                imgFileNameList.Add(fileNameHeader);

                //object[] param = { intDvcIdx };
                //dataStream[intDvcIdx].RegisterCallbackMethod(OnCallback, param);
                dataStream.RegisterCallbackMethod(OnCallback);

                // 主機端獲取影像
                dataStream.StartAcquisition(1);
                //dataStream[intDvcIdx].StartAcquisition(1);

                // 開始由相機取得影像
                device.AcquisitionStart();
                //StDevice[intDvcIdx].AcquisitionStart();
                Thread.Sleep(1000);

                //// 循環取得資料並檢查狀態
                //// 持續執行取得影像直到足夠的幀數
                ////while (dataStream.IsGrabbing)
                //{
                //    // 逾時超過5000ms後，回收儲存影像資料的暫存區
                //    // 使用 'using' 語法可在不需使用時，自動管理暫存區重新排隊操作
                //    using (CStStreamBuffer streamBuffer = dataStream[intDvcIdx].RetrieveBuffer(5000))
                //    {
                //        // 檢查取得的資料是否包含影像資料
                //        if (streamBuffer.GetIStStreamBufferInfo().IsImagePresent)
                //        {
                //            // 若是，建立IStImage物件已進行進一步的影像處理
                //            //IStImage[] stImage=new IStImage[1];
                //            ImageList.Add(streamBuffer.GetIStImage());

                //            {
                //                // JPEG file extension.
                //                string imageFileName = fileNameHeader + ".jpg";

                //                // Save the image file in JPEG format.
                //                stillImageFiler.Quality = 75;
                //                Console.Write(Environment.NewLine + "Saving " + imageFileName + "... ");
                //                stillImageFiler.Save(ImageList[0], eStStillImageFileFormat.JPEG, imgFileNameList[0]);
                //                Console.Write("done" + Environment.NewLine);
                //                ImageList.RemoveAt(0);
                //                imgFileNameList.RemoveAt(0);
                //            }

                //            //// 顯示接收影像的詳細資訊
                //            //Byte[] imageData = stImage.GetByteArray();
                //            //Console.Write("BlockId=" + streamBuffer.GetIStStreamBufferInfo().FrameID);
                //            //Console.Write(" Size:" + stImage.ImageWidth + " x " + stImage.ImageHeight);
                //            //Console.Write(" First byte =" + imageData[0] + Environment.NewLine);


                //            //var width = (int)stImage.ImageWidth;
                //            //var height = (int)stImage.ImageHeight;
                //            //myimg = new Bitmap(width, height);

                //            ////TODO: https://stackoverflow.com/questions/3474434/set-individual-pixels-in-net-format16bppgrayscale-image

                //            //for (var idx = 0; idx < imageData.Length; idx++)
                //            //{
                //            //    var val = imageData[idx];
                //            //    var color = Color.FromArgb(val, val, val);
                //            //    myimg.SetPixel(idx % width, idx / width, color);
                //            //}
                //        }
                //        else
                //        {
                //            // 如果取得的資料不含影像資料
                //            Console.WriteLine("Image data does not exist.");
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                // 顯示例外訊息
                throw ex;
            }
            finally
            {
                // 停止相機取像
                //StDevice[intDvcIdx].AcquisitionStop();
                device.AcquisitionStop();
                // 停止主機取像
                //dataStream[intDvcIdx].StopAcquisition();
                dataStream.StopAcquisition();
            }
        }

        public Image CaptureToImage() { return null; }
        public IStImage CaptureRaw() { return null; }

        public void CaputreAndSave(string fn, string fType)
        {
            var img = this.CaptureRaw();
            this.SaveImage(img, fn, fType);

        }




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
                                ImageList.Add(stImage); ;
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



        public int SaveImage(string sFileType)
        {
            int intSavedCnt = 0;
            string sFT = sFileType.ToLower();
            try
            {
                if (ImageList.Count > 0)
                {
                    while (ImageList.Count > 0)
                    {
                        string imageFileName = imgFileNameList[0];
                        if (sFT == "bitmap" || sFT == "bmp" || sFT == ".bmp")
                        {
                             imageFileName += ".bmp";
                        }
                        else if (sFT == "tiff" || sFT == "tif" || sFT == ".tif")
                        {
                            imageFileName +=".tif";
                        }
                        else if (sFT == "png" || sFT == ".png")
                        {
                            imageFileName += ".png";
                        }
                        else if (sFT == "jpeg" || sFT == "jpg" || sFT == ".jpg")
                        {
                            imageFileName+= ".jpg";
                            stillImageFiler.Quality = 75;
                        }
                        else if (sFT == "csv" || sFT == ".csv")
                        {
                            imageFileName += ".csv";
                        }
                        else //要轉換的檔案格式(副檔名)錯誤
                            return 0;

                        this.SaveImage(ImageList[0], imageFileName, sFT);

                        imgFileNameList.RemoveAt(0);
                        ImageList.RemoveAt(0);
                        intSavedCnt++;
                    }
                }
                else //沒有照片可以儲存
                    return -1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return intSavedCnt;
        }


        public int SaveImage(IStImage img, string fn, string sFileType)
        {
            int intSavedCnt = 0;
            string sFT = sFileType.ToLower();
            try
            {
                if (sFT == "bitmap" || sFT == "bmp" || sFT == ".bmp")
                {
                    stillImageFiler.Save(img, eStStillImageFileFormat.Bitmap, fn);
                }
                else if (sFT == "tiff" || sFT == "tif" || sFT == ".tif")
                {
                    stillImageFiler.Save(img, eStStillImageFileFormat.TIFF, fn);
                }
                else if (sFT == "png" || sFT == ".png")
                {
                    stillImageFiler.Save(img, eStStillImageFileFormat.PNG, fn);
                }
                else if (sFT == "jpeg" || sFT == "jpg" || sFT == ".jpg")
                {
                    stillImageFiler.Quality = 75;
                    stillImageFiler.Save(img, eStStillImageFileFormat.JPEG, fn);
                }
                else if (sFT == "csv" || sFT == ".csv")
                {
                    stillImageFiler.Save(img, eStStillImageFileFormat.CSV, fn);

                }
                else //要轉換的檔案格式(副檔名)錯誤
                    return 0;
                imgFileNameList.RemoveAt(0);
                ImageList.RemoveAt(0);
                intSavedCnt++;


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return intSavedCnt;
        }


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
