﻿using System;
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
            using (var camera = new MvOmronSentechCameraLdd())
            {
                camera.Connect();
                var img = camera.Capture();


            }





        }
    }
}
