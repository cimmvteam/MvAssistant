using System;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.DeviceDrive.OmronSentechCamera;


namespace MvAssistant.Mac.TestMy.Device
{
    [TestClass]
    public class UtDeviceCamera
    {
        [TestMethod]
        public void TestSentech()
        {
            try
            {
                using (var camera = new MvCameraDeviceScanner())
                {
                    camera.Connect();
                    camera.ScanAlldevice();
                    camera.BT_ccd_gripper_1.SingleDeviceCapture();
                    camera.BT_ccd_gripper_1.SaveImage("jpg");
                    camera.Close();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
