using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MvAssistant.DeviceDrive.FanucRobot_v42_15
{
    /// <summary>
    /// 除Robot暫存器資料以外, 可存放其它類型資料
    /// </summary>
    [Serializable]
    public class MvFanucRobotInfo
    {
        public bool IsReachTarget = false;
        /// <summary>
        /// 0:Offset ; 1:Postion ; 2:Joint
        /// </summary>
        public int MotionType = 0;

        public MvFanucRobotPosReg PosReg = new MvFanucRobotPosReg();
        public DateTime RobotTime = DateTime.Now;
        /// <summary>
        /// (mm/sec)
        /// </summary>
        public int Speed = 60;

        public MvFanucRobotInfo() { }

        public MvFanucRobotInfo(MvFanucRobotInfo source)
        {
            source.Clone(this);
        }
        public MvFanucRobotInfo(MvFanucRobotPosReg source)
        {
            source.Clone(this.PosReg);
        }


        public void Clone(MvFanucRobotInfo target)
        {
            this.PosReg.Clone(target.PosReg);

            target.Speed = this.Speed;
            target.MotionType = this.MotionType;

            target.RobotTime = this.RobotTime;
            target.IsReachTarget = this.IsReachTarget;
        }



        #region Position Register Properties

        [XmlIgnore] public short c1 { get { return this.PosReg.c1; } set { this.PosReg.c1 = value; } }
        [XmlIgnore] public short c2 { get { return this.PosReg.c2; } set { this.PosReg.c2 = value; } }
        [XmlIgnore] public short c3 { get { return this.PosReg.c3; } set { this.PosReg.c3 = value; } }
        [XmlIgnore] public short c4 { get { return this.PosReg.c4; } set { this.PosReg.c4 = value; } }
        [XmlIgnore] public short c5 { get { return this.PosReg.c5; } set { this.PosReg.c5 = value; } }
        [XmlIgnore] public short c6 { get { return this.PosReg.c6; } set { this.PosReg.c6 = value; } }
        [XmlIgnore] public short c7 { get { return this.PosReg.c7; } set { this.PosReg.c7 = value; } }
        [XmlIgnore] public float e1 { get { return this.PosReg.e1; } set { this.PosReg.e1 = value; } }
        [XmlIgnore] public float e2 { get { return this.PosReg.e2; } set { this.PosReg.e2 = value; } }
        [XmlIgnore] public float e3 { get { return this.PosReg.e3; } set { this.PosReg.e3 = value; } }
        [XmlIgnore] public float j1 { get { return this.PosReg.j1; } set { this.PosReg.j1 = value; } }
        [XmlIgnore] public float j2 { get { return this.PosReg.j2; } set { this.PosReg.j2 = value; } }
        [XmlIgnore] public float j3 { get { return this.PosReg.j3; } set { this.PosReg.j3 = value; } }
        [XmlIgnore] public float j4 { get { return this.PosReg.j4; } set { this.PosReg.j4 = value; } }
        [XmlIgnore] public float j5 { get { return this.PosReg.j5; } set { this.PosReg.j5 = value; } }
        [XmlIgnore] public float j6 { get { return this.PosReg.j6; } set { this.PosReg.j6 = value; } }
        [XmlIgnore] public float j7 { get { return this.PosReg.j7; } set { this.PosReg.j7 = value; } }
        [XmlIgnore] public float j8 { get { return this.PosReg.j8; } set { this.PosReg.j8 = value; } }
        [XmlIgnore] public float j9 { get { return this.PosReg.j9; } set { this.PosReg.j9 = value; } }
        [XmlIgnore] public float p { get { return this.PosReg.p; } set { this.PosReg.p = value; } }
        [XmlIgnore] public float r { get { return this.PosReg.r; } set { this.PosReg.r = value; } }
        [XmlIgnore] public short UserFrame { get { return this.PosReg.UserFrame; } set { this.PosReg.UserFrame = value; } }
        [XmlIgnore] public short UserTool { get { return this.PosReg.UserTool; } set { this.PosReg.UserTool = value; } }
        [XmlIgnore] public short ValidC { get { return this.PosReg.ValidC; } set { this.PosReg.ValidC = value; } }
        [XmlIgnore] public short ValidJ { get { return this.PosReg.ValidJ; } set { this.PosReg.ValidJ = value; } }
        [XmlIgnore] public float w { get { return this.PosReg.w; } set { this.PosReg.w = value; } }
        [XmlIgnore] public float x { get { return this.PosReg.x; } set { this.PosReg.x = value; } }
        [XmlIgnore] public float y { get { return this.PosReg.y; } set { this.PosReg.y = value; } }
        [XmlIgnore] public float z { get { return this.PosReg.z; } set { this.PosReg.z = value; } }

        #endregion


    }




}
