using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SensingNet.v0_2.DvcSensor;
using SensingNet.v0_2.DvcSensor.Protocol;

namespace MvAssistant.v0_2.Mac.TestMy.SensingNet
{
    [TestClass]
    public class UtVibration
    {
        [TestMethod]
        public void TestMethod1()
        {

            using (var handler = new SNetDvcSensorHandler())
            {
                var config = handler.Config = new SNetDvcSensorCfg();
                config.IsActivelyConnect = false;
                config.IsActivelyTx = true;
                config.RemoteUri = "net.tcp://192.168.125.203:5000";

                config.ProtoConnect = SNetEnumProtoConnect.Tcp;
                config.ProtoFormat = SNetEnumProtoFormat.SNetCmd;
                config.ProtoSession = SNetEnumProtoSession.SNetCmd;


                handler.EhSignalCapture += (ss, ee) =>
                {
                    System.Diagnostics.Debug.WriteLine("Data[0]={0} ; Lenght={1}", ee.Data[0], ee.Data.Count);
                };

                handler.CfInit();
                handler.CfLoad();


                handler.CfRunLoop();


                handler.CfUnLoad();
                handler.CfFree();

            }

        }
    }
}
