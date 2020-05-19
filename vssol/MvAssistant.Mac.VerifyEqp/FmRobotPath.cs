using MvAssistant.DeviceDrive.FanucRobot_v42_15;
using MvAssistant.Mac.v1_0.Hal.Component.Robot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaskCleanerVerify
{
    public partial class FmRobotPath : Form
    {

        MvFanucRobotLdd ldd = new MvFanucRobotLdd();
        public FmRobotPath()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MvFanucRobotPosReg curr = ldd.ReadCurPosUf();
            HalRobotMotion motion = new HalRobotMotion();

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void FmRobotPath_Load(object sender, EventArgs e)
        {

            ldd.RobotIp = "192.168.0.51";
            System.Diagnostics.Debug.Assert(ldd.ConnectIfNo() == 0);

            System.Diagnostics.Debug.Assert(ldd.AlarmReset());

            System.Diagnostics.Debug.Assert(ldd.StopProgram() == 0);
            System.Diagnostics.Debug.Assert(ldd.ExecutePNS("PNS0101"));





        }
    }

    public class PositionSaver
    {
        public HalRobotMotion GetHalRobotMotionFromMvFanucRobotPosReg(MvFanucRobotPosReg sourceObj)
        {
            Func<string,object> GetValue = (propertyName) =>
            {
                PropertyInfo[] property = sourceObj.GetType().GetProperties();
                var propertyNames = property.Select(m=>m.Name).ToList();
                var srcPropertyName = propertyNames.Where(m => m == propertyName).FirstOrDefault();
                if (srcPropertyName == null)
                {
                    srcPropertyName = propertyNames.Where(m => m == propertyName.ToUpper()).FirstOrDefault();
                    if (srcPropertyName == null)
                    {
                        srcPropertyName = propertyNames.Where(m => m == propertyName.ToLower()).FirstOrDefault();
                    }
                }
                if(srcPropertyName!=null)
                {

                }
            };
            var properties = typeof(HalRobotMotion).GetProperties();
            HalRobotMotion motion = new HalRobotMotion();
            foreach (PropertyInfo property in properties)
            {
                if (property.CanWrite)
                {
                    property.SetValue(motion, null);
                }
            }
            return null;
        } 
    }
}
