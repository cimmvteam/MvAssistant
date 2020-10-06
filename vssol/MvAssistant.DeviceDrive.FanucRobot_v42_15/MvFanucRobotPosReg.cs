using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MvAssistant.DeviceDrive.FanucRobot_v42_15
{
    /// <summary>
    /// 存放Robot暫存器中取得的資料
    /// </summary>
    [Serializable]
    public class MvFanucRobotPosReg
    {
        public float x { get { return (float)this.XyzwpreArrary.GetValue(0); } set { this.XyzwpreArrary.SetValue(value, 0); } }
        public float y { get { return (float)this.XyzwpreArrary.GetValue(1); } set { this.XyzwpreArrary.SetValue(value, 1); } }
        public float z { get { return (float)this.XyzwpreArrary.GetValue(2); } set { this.XyzwpreArrary.SetValue(value, 2); } }
        public float w { get { return (float)this.XyzwpreArrary.GetValue(3); } set { this.XyzwpreArrary.SetValue(value, 3); } }
        public float p { get { return (float)this.XyzwpreArrary.GetValue(4); } set { this.XyzwpreArrary.SetValue(value, 4); } }
        public float r { get { return (float)this.XyzwpreArrary.GetValue(5); } set { this.XyzwpreArrary.SetValue(value, 5); } }
        public float e1 { get { return (float)this.XyzwpreArrary.GetValue(6); } set { this.XyzwpreArrary.SetValue(value, 6); } }
        public float e2 { get { return (float)this.XyzwpreArrary.GetValue(7); } set { this.XyzwpreArrary.SetValue(value, 7); } }
        public float e3 { get { return (float)this.XyzwpreArrary.GetValue(8); } set { this.XyzwpreArrary.SetValue(value, 8); } }

        public float j1 { get { return (float)this.JointArray.GetValue(0); } set { this.JointArray.SetValue(value, 0); } }
        public float j2 { get { return (float)this.JointArray.GetValue(1); } set { this.JointArray.SetValue(value, 1); } }
        public float j3 { get { return (float)this.JointArray.GetValue(2); } set { this.JointArray.SetValue(value, 2); } }
        public float j4 { get { return (float)this.JointArray.GetValue(3); } set { this.JointArray.SetValue(value, 3); } }
        public float j5 { get { return (float)this.JointArray.GetValue(4); } set { this.JointArray.SetValue(value, 4); } }
        public float j6 { get { return (float)this.JointArray.GetValue(5); } set { this.JointArray.SetValue(value, 5); } }
        public float j7 { get { return (float)this.JointArray.GetValue(6); } set { this.JointArray.SetValue(value, 6); } }
        public float j8 { get { return (float)this.JointArray.GetValue(7); } set { this.JointArray.SetValue(value, 7); } }
        public float j9 { get { return (float)this.JointArray.GetValue(8); } set { this.JointArray.SetValue(value, 8); } }

        public short c1 { get { return (short)this.ConfigArray.GetValue(0); } set { this.ConfigArray.SetValue(value, 0); } }
        public short c2 { get { return (short)this.ConfigArray.GetValue(1); } set { this.ConfigArray.SetValue(value, 1); } }
        public short c3 { get { return (short)this.ConfigArray.GetValue(2); } set { this.ConfigArray.SetValue(value, 2); } }
        public short c4 { get { return (short)this.ConfigArray.GetValue(3); } set { this.ConfigArray.SetValue(value, 3); } }
        public short c5 { get { return (short)this.ConfigArray.GetValue(4); } set { this.ConfigArray.SetValue(value, 4); } }
        public short c6 { get { return (short)this.ConfigArray.GetValue(5); } set { this.ConfigArray.SetValue(value, 5); } }
        public short c7 { get { return (short)this.ConfigArray.GetValue(6); } set { this.ConfigArray.SetValue(value, 6); } }


        [XmlIgnore]
        public Array XyzwpreArrary = new float[9];
        [XmlIgnore]
        public Array ConfigArray = new short[7];
        [XmlIgnore]
        public Array JointArray = new float[9];
        public short UserFrame = 0;
        public short UserTool = 0;
        /// <summary>
        /// 是否為有效的Coordinate.
        /// 跟Robot取得座標時, 會附帶此旗標
        /// </summary>
        public short ValidC = 0;
        /// <summary>
        /// 是否為有效的Joint
        /// 跟Robot取得軸向時, 會附帶此旗標
        /// </summary>
        public short ValidJ = 0;



        public MvFanucRobotPosReg() { }
        public MvFanucRobotPosReg(MvFanucRobotPosReg source)
        {
            source.Clone(this);
        }


        public void Clone(MvFanucRobotPosReg target)
        {
            this.XyzwpreArrary.CopyTo(target.XyzwpreArrary, 0);
            this.ConfigArray.CopyTo(target.ConfigArray, 0);
            this.JointArray.CopyTo(target.JointArray, 0);
            target.UserFrame = this.UserFrame;
            target.UserTool = this.UserTool;
            target.ValidC = this.ValidC;
            target.ValidJ = this.ValidJ;
        }





    }




}
