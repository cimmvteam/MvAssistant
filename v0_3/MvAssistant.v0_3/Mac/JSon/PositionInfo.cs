﻿using MvAssistant.v0_3.Mac.Hal.CompRobot;
using System;
using System.Reflection;

namespace MvAssistant.v0_3.Mac.JSon
{
    public class PositionInfo
    {

        /// <summary>Poosition ID</summary>
        public string PositionID { get; set; }
        /// <summary>Serial No </summary>
        public int Sn { get; set; }

        public MacHalRobotMotion Position;

        public MacHalRobotMotion GetPosition()
        {
            return Position;
        }

        public void SetPosition(MacHalRobotMotion value)
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
            PropertyInfo[] properties = typeof(MacHalRobotMotion).GetProperties();
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
