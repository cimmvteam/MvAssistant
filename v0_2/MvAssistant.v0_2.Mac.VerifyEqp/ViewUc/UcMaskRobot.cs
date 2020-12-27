using MvAssistant.v0_2.Mac.Hal.CompRobotTest;
using MvAssistant.v0_2.Threading;
using System;
using System.Windows.Forms;

namespace MvAssistantMacVerifyEqp.ViewUc
{
    public partial class UcMaskRobot : UserControl
    {
        MacHalMaskRobotFanuc robotHandler;

        MvaCancelTask task;


        public UcMaskRobot()
        {
            InitializeComponent();
        }
        ~UcMaskRobot()
        {
            this.TaskClose();
        }


        private void btnConnect_Click(object sender, EventArgs e)
        {
            robotHandler = new MacHalMaskRobotFanuc();
            robotHandler.ldd.RobotIp = "192.168.0.50";
            if (robotHandler.ConnectIfNO() == 0)
            {
                this.LogWrite("Connection Success");
            }
            else
            {
                this.LogWrite("Connection Fail");
                this.robotHandler.Close();
                return;
            }

            //var currPos = robotHandler.getCurrentPOS();


        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (robotHandler == null) return;
            robotHandler.ldd.StopProgram();


            if (robotHandler.ldd.AlarmReset())
            {
                this.LogWrite("Reset Done");
            }
            else
            {
                this.LogWrite("Reset Fail");
                return;
            }


        }

        private void btnStartPns0101_Click(object sender, EventArgs e)
        {
            try
            {
                robotHandler.MaskRobotMove(robotHandler.LPUpsideToOSPutMask()); // Add by LingChengYeh
                //robotHandler.StartPns0101SgsVerify();
                //robotHandler.MaskRobotMove()
            }
            catch (Exception ex)
            {
                this.LogWrite(ex.Message);
            }

        }

        private void btnStartPns0102_Click(object sender, EventArgs e)
        {
            if (this.task != null) return;

            this.task = MvaCancelTask.RunLoop(() =>
            {
                //大迴圈, 來回一次
                try
                {
                    robotHandler.SgsVerifyStartPns0102(ri =>
                    {
                        this.Invoke(new Action(() =>
                        {
                            tbPoseX.Text = ri.x + "";
                            tbPoseY.Text = ri.y + "";
                            tbPoseZ.Text = ri.z + "";
                            tbPoseW.Text = ri.w + "";
                            tbPoseP.Text = ri.p + "";
                            tbPoseR.Text = ri.r + "";
                        }));
                    });
                    return true;
                }
                catch (Exception ex)
                {
                    this.LogWrite(ex.Message);
                    return false;
                }
            }, 1000);
        }

        private void btnStopPns0101_Click(object sender, EventArgs e)
        {

        }

        private void btnStopPns0102_Click(object sender, EventArgs e)
        {
            this.TaskClose();
        }


        void LogWrite(string msg)
        {
            this.Invoke(new Action(() =>
            {
                var now = DateTime.Now;
                this.rtbLog.AppendText(string.Format("{0} {1}\r\n", now.ToString("yyyyMMdd HH:mm:ss"), msg));
            }));
        }

        void TaskClose()
        {
            if (this.task != null)
            {
                using (var obj = this.task)
                    obj.Cancel();
                this.task = null;
            }
        }


        private void UcMaskRobot_Load(object sender, EventArgs e)
        {

        }
    }
}
