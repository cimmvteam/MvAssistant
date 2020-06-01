// 如果要使用GUI功能，請取消註解以下內容，以定義ENABLED_ST_GUI並進行進一步的操作
#define ENABLED_ST_GUI
using Sentech.StApiDotNET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.OmronSentechCamera
{
    public class MvOmronSentechCameraLdd : IDisposable
    {

        ~MvOmronSentechCameraLdd() { this.Dispose(false); }

        // 要取得的影像數量
        const int nCountOfImagesToGrab = 100;

        CStApiAutoInit api;
        CStSystem system;
        CStDevice device;
        CStDataStream dataStream;

        public void Connect()
        {

            // 使用前先初始化 StApi
            api = new CStApiAutoInit();
            // 建立系統物件來瀏覽影像和連線
            system = new CStSystem();
            // 建立一個裝置物件，並使用系統物件的功能連接到第一次檢測的設備
            device = system.CreateFirstStDevice();
            // 建立資料串流的物件以處理影像串流資訊
            dataStream = device.CreateStDataStream(0);

        }
        public void Close()
        {
            if (dataStream != null)
            { 
                // 停止主機取像
                dataStream.StopAcquisition();
                dataStream.Dispose();
            }
            if (device != null)
            {
                // 停止相機取像
                device.AcquisitionStop();
                device.Dispose();
            }
            if (system != null)
                system.Dispose();
            if (api != null)
                api.Dispose();

        }
        public Image Capture()
        {
            Bitmap myimg = null;

            try
            {
                // 顯示裝置名稱
                Console.WriteLine("Device=" + device.GetIStDeviceInfo().DisplayName);

                // 主機端獲取影像
                dataStream.StartAcquisition(nCountOfImagesToGrab);

                // 開始由相機取得影像
                device.AcquisitionStart();

                // 循環取得資料並檢查狀態
                // 持續執行取得影像直到足夠的幀數
                //while (dataStream.IsGrabbing)
                {
                    // 逾時超過5000ms後，回收儲存影像資料的暫存區
                    // 使用 'using' 語法可在不需使用時，自動管理暫存區重新排隊操作
                    using (CStStreamBuffer streamBuffer = dataStream.RetrieveBuffer(5000))
                    {
                        // 檢查取得的資料是否包含影像資料
                        if (streamBuffer.GetIStStreamBufferInfo().IsImagePresent)
                        {
                            // 若是，建立IStImage物件已進行進一步的影像處理
                            IStImage stImage = streamBuffer.GetIStImage();

                            // 顯示接收影像的詳細資訊
                            Byte[] imageData = stImage.GetByteArray();
                            //File.WriteAllBytes("Foo.txt",imageData);
                            Console.Write("BlockId=" + streamBuffer.GetIStStreamBufferInfo().FrameID);
                            Console.Write(" Size:" + stImage.ImageWidth + " x " + stImage.ImageHeight);
                            Console.Write(" First byte =" + imageData[0] + Environment.NewLine);


                            var width = (int)stImage.ImageWidth;
                            var height = (int)stImage.ImageHeight;
                            myimg = new Bitmap(width, height);




                            //TODO: https://stackoverflow.com/questions/3474434/set-individual-pixels-in-net-format16bppgrayscale-image

                            for (var idx = 0; idx < imageData.Length; idx++)
                            {
                                var val = imageData[idx];
                                var color = Color.FromArgb(val, val, val);
                                myimg.SetPixel(idx % width, idx / width, color);
                            }

                            myimg.Save("output.jpg");

                            //using (MemoryStream ms = new MemoryStream(imageData, 0, imageData.Length))
                            //{
                            //    ms.Seek(0, SeekOrigin.Begin);
                            //    //Bitmap bmp = new Bitmap(memoryStream);
                            //    //bmp.Save(memoryStream, ImageFormat.Jpeg);
                            //    myimg = Image.FromStream(ms);
                            //    img.Save("output.jpg", ImageFormat.Jpeg);
                            //    //ms.Flush();
                            //}


                        }
                        else
                        {
                            // 如果取得的資料不含影像資料
                            Console.WriteLine("Image data does not exist.");
                        }
                    }
                }



            }
            catch (Exception ex)
            {
                // 顯示例外訊息
                throw ex;
            }
            finally
            {
                // 停止相機取像
                device.AcquisitionStop();
                // 停止主機取像
                dataStream.StopAcquisition();



                // 等待直到按下Enter鍵
                Console.WriteLine("\r\nPress Enter to exit.");
                //Console.ReadLine();
            }
            return myimg;
        }

        public void cameraSample()
        {
            try
            {
                // 使用前先初始化 StApi
                CStApiAutoInit api = new CStApiAutoInit();

                // 建立系統物件來瀏覽影像和連線
                CStSystem system = new CStSystem();

                // 建立一個裝置物件，並使用系統物件的功能連接到第一次檢測的設備
                CStDevice device = system.CreateFirstStDevice();

#if ENABLED_ST_GUI
                // 如要用圖形使用者介面顯示，從這裡建立一個顯示視窗
                CStImageDisplayWnd wnd = new CStImageDisplayWnd();
#endif
                // 建立資料串流的物件以處理影像串流資訊
                CStDataStream dataStream = device.CreateStDataStream(0);

                // 顯示裝置名稱
                Console.WriteLine("Device=" + device.GetIStDeviceInfo().DisplayName);

                // 主機端獲取影像
                dataStream.StartAcquisition(nCountOfImagesToGrab);

                // 開始由相機取得影像
                device.AcquisitionStart();

                // 循環取得資料並檢查狀態
                // 持續執行取得影像直到足夠的幀數
                while (dataStream.IsGrabbing)
                {
                    // 逾時超過5000ms後，回收儲存影像資料的暫存區
                    // 使用 'using' 語法可在不需使用時，自動管理暫存區重新排隊操作
                    using (CStStreamBuffer streamBuffer = dataStream.RetrieveBuffer(5000))
                    {
                        // 檢查取得的資料是否包含影像資料
                        if (streamBuffer.GetIStStreamBufferInfo().IsImagePresent)
                        {
                            // 若是，建立IStImage物件已進行進一步的影像處理
                            IStImage stImage = streamBuffer.GetIStImage();
#if ENABLED_ST_GUI
                            // 取得接收影像的詳細資訊，並顯示在視窗的狀態欄上
                            string strText = device.GetIStDeviceInfo().DisplayName + " ";
                            strText += stImage.ImageWidth + " x " + stImage.ImageHeight + " ";
                            strText += string.Format("{0:F2}[fps]", dataStream.CurrentFPS);
                            wnd.SetUserStatusBarText(strText);

                            // 檢查視窗是否可視
                            if (!wnd.IsVisible)
                            {
                                // 設定視窗的位置及大小
                                wnd.SetPosition(0, 0, (int)stImage.ImageWidth, (int)stImage.ImageHeight);

                                // 建立一個新執行緒顯示視窗
                                wnd.Show(eStWindowMode.ModalessOnNewThread);
                            }

                            // 儲存顯示的影像
                            // 必要時存取影像釋放暫存區
                            wnd.RegisterIStImage(stImage);
#else
                            // 顯示接收影像的詳細資訊
                            Byte[] imageData = stImage.GetByteArray();
                            Console.Write("BlockId=" + streamBuffer.GetIStStreamBufferInfo().FrameID);
                            Console.Write(" Size:" + stImage.ImageWidth + " x " + stImage.ImageHeight);
                            Console.Write(" First byte =" + imageData[0] + Environment.NewLine);
#endif
                        }
                        else
                        {
                            // 如果取得的資料不含影像資料
                            Console.WriteLine("Image data does not exist.");
                        }
                    }
                }

                // 停止相機取像
                device.AcquisitionStop();

                // 停止主機取像
                dataStream.StopAcquisition();

            }
            catch (Exception ex)
            {
                // 顯示例外訊息
                throw ex;
            }
            finally
            {
                // 等待直到按下Enter鍵
                Console.WriteLine("\r\nPress Enter to exit.");
                Console.ReadLine();
            }
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
