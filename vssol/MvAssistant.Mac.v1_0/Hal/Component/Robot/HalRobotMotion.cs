using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MvAssistant.Mac.v1_0.Hal.Component.Robot
{
    [GuidAttribute("0F7340AE-4295-4659-A626-6F10471152A9")]
    public class HalRobotMotion
    {
        public HalRobotEnumMotionType MotionType = HalRobotEnumMotionType.None;
        public int IsTcpMove = 0;
        public int Speed = 60;//mm per second

        public int UserFrame = 9;
        public int UserTool = 0;


        public HalRobotPose Pose = new HalRobotPose();

        [XmlIgnore] public float X { get { return this.Pose.X; } set { this.Pose.X = value; } }
        [XmlIgnore]
        public float Y { get { return this.Pose.Y; } set { this.Pose.Y = value; } }
        [XmlIgnore]
        public float Z { get { return this.Pose.Z; } set { this.Pose.Z = value; } }
        [XmlIgnore]
        public float W { get { return this.Pose.W; } set { this.Pose.W = value; } }
        [XmlIgnore]
        public float P { get { return this.Pose.P; } set { this.Pose.P = value; } }
        [XmlIgnore]
        public float R { get { return this.Pose.R; } set { this.Pose.R = value; } }
        [XmlIgnore]
        public float E1 { get { return this.Pose.E1; } set { this.Pose.E1 = value; } }
        [XmlIgnore]
        public float J1 { get { return this.Pose.J1; } set { this.Pose.J1 = value; } }
        [XmlIgnore]
        public float J2 { get { return this.Pose.J2; } set { this.Pose.J2 = value; } }
        [XmlIgnore]
        public float J3 { get { return this.Pose.J3; } set { this.Pose.J3 = value; } }
        [XmlIgnore]
        public float J4 { get { return this.Pose.J4; } set { this.Pose.J4 = value; } }
        [XmlIgnore]
        public float J5 { get { return this.Pose.J5; } set { this.Pose.J5 = value; } }
        [XmlIgnore]
        public float J6 { get { return this.Pose.J6; } set { this.Pose.J6 = value; } }
        [XmlIgnore]
        public float J7 { get { return this.Pose.J7; } set { this.Pose.J7 = value; } }




        public float[] ToPoseArray() { return this.Pose.Vector.ToArray(); }




        public bool IntersectRange(HalRobotPose start, HalRobotPose end)
        {
            var func = new Func<float, float, float, bool>(delegate (float s, float x, float e) { return s <= x && x <= e; });
            var flag = true;
            flag &= func(start.X, this.Pose.X, end.X);
            flag &= func(start.Y, this.Pose.Y, end.Y);
            flag &= func(start.Z, this.Pose.Z, end.Z);
            flag &= func(start.W, this.Pose.X, end.W);
            flag &= func(start.P, this.Pose.X, end.P);
            flag &= func(start.R, this.Pose.X, end.R);

            return flag;
        }




        public float Distance(HalRobotMotion target)
        {
            var vec = target.Pose.Vector - this.Pose.Vector;
            var distnace = vec[0] * vec[0] + vec[1] * vec[1] + vec[2] * vec[2];
            return (float)Math.Sqrt(distnace);
        }
        public float Distance(HalRobotPose target)
        {
            var vec = target.Vector - this.Pose.Vector;
            var distnace = vec[0] * vec[0] + vec[1] * vec[1] + vec[2] * vec[2];
            return (float)Math.Sqrt(distnace);
        }



        public void From(mvfa)
        {

        }

    }
}
