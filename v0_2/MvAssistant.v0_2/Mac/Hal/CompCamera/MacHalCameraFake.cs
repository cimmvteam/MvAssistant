using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;

namespace MvAssistant.v0_2.Mac.Hal.CompCamera
{
    [Guid("6559A888-F8D8-4CD6-8F18-B72E0252EB85")]
    public class MacHalCameraFake : MacHalFakeComponentBase, IHalCamera
    {
        public void SetExposureTime(double mseconds)
        {
            return;
        }

        public void SetFocus(double percentage)
        {
            return;
        }

        public Bitmap Shot()
        {
            Bitmap bmp=null;
            return bmp;
        }

        public int ShotToSaveImage(string SavePath, string FileType)
        {
            return 0;
        }
    }
}
