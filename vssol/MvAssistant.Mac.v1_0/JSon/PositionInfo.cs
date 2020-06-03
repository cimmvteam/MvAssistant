using MvAssistant.Mac.v1_0.Hal.Component.Robot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.JSon
{
    public class PositionInfo
    {

        /// <summary>Poosition ID</summary>
        public string PositionID { get; set; }
        /// <summary>Serial No </summary>
        public int Sn { get; set; }
        public HalRobotMotion Position { get; set; }
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
                    text = text + property.Name + ": " + property.GetValue(this.Position).ToString() + ", ";
                }
            }
            text += "Speed: " + this.Position.Speed + ", MotionType: " + this.Position.MotionType.ToString();
            return text;
        }
    }
}
