using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;

namespace MvAssistant.Mac.v1_0.Hal.CompCamera
{
    [GuidAttribute("FC96E705-38E6-4120-98BB-85670EF81AAF")]
    public class HalCameraFake : HalFakeBase, IHalCamera
    {
        //Image image = null;
        private string imagePath;
        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; }
        }

        public string Image { get; set; }


        public Image Shot()
        {
            return Shot(imagePath);
        }

        public void SetExposureTime(double mseconds)
        {

        }

        public void SetFocus(double percentage)
        {
            throw new NotImplementedException();
        }

        public Image Shot(string imgFilePath)
        {
            /// randomly pick up image from folder
            //var ext = new List<string> { ".jpg", ".bmp", ".png" };
            //var imgFiles = Directory.GetFiles(imgFolderPath, "*.*", SearchOption.AllDirectories).
            //    Where(s => ext.Contains(Path.GetExtension(s)));

            //if (imgFiles == null || imgFiles.Count() == 0)
            //    return new System.Drawing.Bitmap(10, 10);

            //Random rnd = new Random(Guid.NewGuid().GetHashCode());
            //int rndNum = rnd.Next(imgFiles.Count() - 1);
            //return new System.Drawing.Bitmap(imgFiles.ElementAt(rndNum).ToString());


            var imageOffPath = @"UserData\Image\off.png";
            var imageOnPath = @"UserData\Image\on.png";
            var resultPath = imageOffPath;//預設為off image

            if (!string.IsNullOrEmpty(imgFilePath)) resultPath = imgFilePath;//有帶file path, 就設置成該檔案




            //TriggerCondition();

            if (resultPath.Substring(0, 7).ToUpper().Equals("HTTP://") ||
                resultPath.Substring(0, 2).ToUpper().Equals("\\\\"))
            {
                //System.Net.WebRequest request = System.Net.WebRequest.Create(imgFilePath);
                //System.Net.WebResponse response = request.GetResponse();
                //System.IO.Stream responseStream = response.GetResponseStream();
                //Bitmap img = new Bitmap(responseStream);
                //return img;
                WebClient wc = new WebClient();
                Stream s = wc.OpenRead(resultPath);
                Bitmap img = new Bitmap(s);
                return img;
            }
            else
            {
                resultPath = resultPath.Replace("\\", "\\\\");
                Bitmap img = new Bitmap(resultPath);
                return img;
            }
        }

        Bitmap IHalCamera.Shot()
        {
            throw new NotImplementedException();
        }

        public int ShotToSaveImage( string SavePath, string FileType)
        {
            throw new NotImplementedException();
        }
    }
}
