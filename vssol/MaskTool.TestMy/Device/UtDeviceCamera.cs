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
                using (var camera = new MvOmronSentechCameraLdd())
                {
                    camera.Connect();
                    //camera.cameraSample();
                    int intCamCnt = camera.SearchAlldevice().Length;
                    for (int i = 0; i < intCamCnt; i++)
                    {
                        camera.Capture(i);
                    }
                    camera.SaveImage("jpg");
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
