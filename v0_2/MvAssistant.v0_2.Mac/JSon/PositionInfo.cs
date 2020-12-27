using MvAssistant.v0_2.Mac.Hal.CompRobot;
using System;
using System.Reflection;

namespace MvAssistant.v0_2.Mac.JSon
{
    public class PositionInfo
    {

        /// <summary>Poosition ID</summary>
        public string PositionID { get; set; }
        /// <summary>Serial No </summary>
        public int Sn { get; set; }

        public HalRobotMotion Position;

        public HalRobotMotion GetPosition()
        {
            return Position;
        }

        public void SetPosition(HalRobotMotion value)
        {
            Position = value;
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
