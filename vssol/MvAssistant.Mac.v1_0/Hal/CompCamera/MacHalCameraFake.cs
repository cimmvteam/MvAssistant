using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;

namespace MvAssistant.Mac.v1_0.Hal.CompCamera
{
    [Guid("6559A888-F8D8-4CD6-8F18-B72E0252EB85")]
    public class MacHalCameraFake : MacHalFakeComponentBase, IHalCamera
    {
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
            throw new NotImplementedException();
        }

        public int ShotToSaveImage(string SavePath, string FileType)
        {
            throw new NotImplementedException();
        }
    }
}
