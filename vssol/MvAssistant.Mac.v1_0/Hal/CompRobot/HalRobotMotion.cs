using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MvAssistant.Mac.v1_0.Hal.Component.Robot
{
    [Guid("0F7340AE-4295-4659-A626-6F10471152A9")]
    [Serializable]
    public class HalRobotMotion
    {

        [XmlAttribute] public HalRobotEnumMotionType MotionType = HalRobotEnumMotionType.None;
        [XmlAttribute] public int IsTcpMove = 0;
        [XmlAttribute] public int Speed = 60;//mm per second
        [XmlAttribute] public int UserFrame = 9;
        [XmlAttribute] public int UserTool = 0;
        public HalRobotPose Pose = new HalRobotPose();







        public float[] ToXyzwprArray() { return this.Pose.Xyzwpr.ToArray(); }
        public float[] ToJointArray() { return this.Pose.Joint.ToArray(); }
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
            var vec = target.Pose.Xyzwpr - this.Pose.Xyzwpr;
            var distnace = vec[0] * vec[0] + vec[1] * vec[1] + vec[2] * vec[2];
            return (float)Math.Sqrt(distnace);
        }
        public float Distance(HalRobotPose target)
        {
            var vec = target.Xyzwpr - this.Pose.Xyzwpr;
            var distnace = vec[0] * vec[0] + vec[1] * vec[1] + vec[2] * vec[2];
            return (float)Math.Sqrt(distnace);
        }

        public HalRobotMotion Clone()
        {
            var buffer = MvUtil.SerializeBinary(this);
            var rtn = MvUtil.DeserializeBinary<HalRobotMotion>(buffer);
            return rtn;
        }






        #region Pose Properties
        [XmlIgnore] public float X { get { return this.Pose.X; } set { this.Pose.X = value; } }
        [XmlIgnore] public float Y { get { return this.Pose.Y; } set { this.Pose.Y = value; } }
        [XmlIgnore] public float Z { get { return this.Pose.Z; } set { this.Pose.Z = value; } }
        [XmlIgnore] public float W { get { return this.Pose.W; } set { this.Pose.W = value; } }
        [XmlIgnore] public float P { get { return this.Pose.P; } set { this.Pose.P = value; } }
        [XmlIgnore] public float R { get { return this.Pose.R; } set { this.Pose.R = value; } }
        [XmlIgnore] public float E1 { get { return this.Pose.E1; } set { this.Pose.E1 = value; } }
        [XmlIgnore] public float E2 { get { return this.Pose.E2; } set { this.Pose.E2 = value; } }
        [XmlIgnore] public float E3 { get { return this.Pose.E3; } set { this.Pose.E3 = value; } }
        [XmlIgnore] public float J1 { get { return this.Pose.J1; } set { this.Pose.J1 = value; } }
        [XmlIgnore] public float J2 { get { return this.Pose.J2; } set { this.Pose.J2 = value; } }
        [XmlIgnore] public float J3 { get { return this.Pose.J3; } set { this.Pose.J3 = value; } }
        [XmlIgnore] public float J4 { get { return this.Pose.J4; } set { this.Pose.J4 = value; } }
        [XmlIgnore] public float J5 { get { return this.Pose.J5; } set { this.Pose.J5 = value; } }
        [XmlIgnore] public float J6 { get { return this.Pose.J6; } set { this.Pose.J6 = value; } }
        [XmlIgnore] public float J7 { get { return this.Pose.J7; } set { this.Pose.J7 = value; } }
        [XmlIgnore] public float J8 { get { return this.Pose.J8; } set { this.Pose.J8 = value; } }
        [XmlIgnore] public float J9 { get { return this.Pose.J9; } set { this.Pose.J9 = value; } }
        #endregion




    }
}
