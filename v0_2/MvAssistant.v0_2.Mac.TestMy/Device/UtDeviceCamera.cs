using System;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.v0_2.DeviceDrive.OmronSentechCamera;


namespace MvAssistant.v0_2.Mac.TestMy.Device
{
    [TestClass]
    public class UtDeviceCamera
    {
        [TestMethod]
        public void TestSentech()
        {
            try
            {
                using (var scanner = new MvOmronSentechCameraScanner())
                {
                    scanner.Connect();
                    scanner.ScanAlldevice();
                    var camera = scanner.cameras[""];
                    //camera.CaptureSaveSyn("D:/","jpg");


                    scanner.Close();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
