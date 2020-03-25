using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MaskTool.TestMy.Device
{
    [TestClass]
    public class UtRobotHandler
    {
        [TestMethod]
        public void TestMethod1()
        {
            RobotHandler robotHandler = new RobotHandler();
            int connectRes = robotHandler.ConnectIfNO();
            if (robotHandler != null)
            {
                robotHandler.ldd.StopProgram();
                robotHandler.ldd.AlarmReset();
            }

            robotHandler.getCurrentPOS();


            robotHandler.ldd.ExecutePNS("PNS0101");
            float[] target = new float[6];
            target[0] = robotHandler.curPos.CurrentX;
            target[1] = robotHandler.curPos.CurrentY;
            target[2] = robotHandler.curPos.CurrentZ+5;
            target[3] = robotHandler.curPos.CurrentW;
            target[4] = robotHandler.curPos.CurrentP;
            target[5] = robotHandler.curPos.CurrentR;
            robotHandler.ExecuteMove(target);
        }
    }
}
