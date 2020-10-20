using MvAssistant.Mac.v1_0.Hal.CompRobot;
using System;
using System.Reflection;

namespace MvAssistant.Mac.v1_0.JSon
{
    public class PositionInfo
    {

        /// <summary>Poosition ID</summary>
        public string PositionID { get; set; }
        /// <summary>Serial No </summary>
        public int Sn { get; set; }

        private HalRobotMotion position;

        public HalRobotMotion GetPosition()
        {
            return position;
        }

        public void SetPosition(HalRobotMotion value)
        {
            position = value;
        }

        public static string GetNewInstID()
        {
            DateTime thisTime = DateTime.Now;
            var rtnV = thisTime.ToString("yyyyMMddHHmmssfff");
            return rtnV;
        }
        public override string ToString()
        {
            string text = Sn.ToString("00000") + " | ";
            PropertyInfo[] properties = typeof(HalRobotMotion).GetProperties();
            foreach (var property in properties)
            {
                if (property.CanRead)
                {
                    text = text + property.Name + ": " + property.GetValue(this.GetPosition()).ToString() + ", ";
                }
            }
            text += "Speed: " + this.GetPosition().Speed + ", MotionType: " + this.GetPosition().MotionType.ToString();
            return text;
        }
    }
}
