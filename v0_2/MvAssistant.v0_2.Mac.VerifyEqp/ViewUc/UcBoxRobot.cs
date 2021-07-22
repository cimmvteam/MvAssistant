using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MvAssistant.v0_2.Threading;
using MvAssistant.v0_2.Mac.Hal.CompRobotTest;

namespace MvAssistantMacVerifyEqp.ViewUc
{
    public partial class UcBoxRobot : UserControl
    {
        MacHalBoxRobotFanuc robotHandler;

        MvaTask task;
        public UcBoxRobot()
        {
            InitializeComponent();
        }
        ~UcBoxRobot()
        {
            this.TaskClose();
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

        private void Button4_Click(object sender, EventArgs e)
        {
            if (this.task != null) return;
            if (this.task != null && !this.task.IsEnd()) return;

            this.task = MvaTask.RunLoop(() =>
            {
                //大迴圈, 來回一次
                try
                {
                    robotHandler.SgsVerifyStartPns0102(ri =>
                    {
                        this.Invoke(new Action(() =>
                        {
                            X_VALUE.Text = ri.x + "";
                            Y_VALUE.Text = ri.y + "";
                            Z_VALUE.Text = ri.z + "";
                            W_VALUE.Text = ri.w + "";
                            P_VALUE.Text = ri.p + "";
                            R_VALUE.Text = ri.r + "";
                            E1_VALUE.Text = ri.e1 + "";
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

        void LogWrite(string msg)
        {
            var now = DateTime.Now;
            this.richTextBox1.AppendText(string.Format("{0} {1}\r\n", now.ToString("yyyyMMdd HH:mm:ss"), msg));
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            robotHandler = new MacHalBoxRobotFanuc();
            robotHandler.ldd.RobotIp = "192.168.0.51";
            if (robotHandler.ConnectTry() == 0)
            {
                this.LogWrite("Connection Success");
            }
            else
            {
                this.LogWrite("Connection Fail");
                this.robotHandler.Close();
                return;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
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

        private void Button5_Click(object sender, EventArgs e)
        {
            this.TaskClose();
        }
    }
}
