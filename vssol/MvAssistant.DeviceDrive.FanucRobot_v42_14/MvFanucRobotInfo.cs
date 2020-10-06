using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MvAssistant.DeviceDrive.FanucRobot_v42_14
{
    [Serializable]
    public class MvFanucRobotInfo
    {
        public float x { get { return (float)this.posArray.GetValue(0); } set { this.posArray.SetValue(value, 0); } }
        public float y { get { return (float)this.posArray.GetValue(1); } set { this.posArray.SetValue(value, 1); } }
        public float z { get { return (float)this.posArray.GetValue(2); } set { this.posArray.SetValue(value, 2); } }
        public float w { get { return (float)this.posArray.GetValue(3); } set { this.posArray.SetValue(value, 3); } }
        public float p { get { return (float)this.posArray.GetValue(4); } set { this.posArray.SetValue(value, 4); } }
        public float r { get { return (float)this.posArray.GetValue(5); } set { this.posArray.SetValue(value, 5); } }
        public float e1 { get { return (float)this.posArray.GetValue(6); } set { this.posArray.SetValue(value, 6); } }


        public float j1 { get { return (float)this.jointArray.GetValue(0); } set { this.jointArray.SetValue(value, 0); } }
        public float j2 { get { return (float)this.jointArray.GetValue(1); } set { this.jointArray.SetValue(value, 1); } }
        public float j3 { get { return (float)this.jointArray.GetValue(2); } set { this.jointArray.SetValue(value, 2); } }
        public float j4 { get { return (float)this.jointArray.GetValue(3); } set { this.jointArray.SetValue(value, 3); } }
        public float j5 { get { return (float)this.jointArray.GetValue(4); } set { this.jointArray.SetValue(value, 4); } }
        public float j6 { get { return (float)this.jointArray.GetValue(5); } set { this.jointArray.SetValue(value, 5); } }
        public float j7 { get { return (float)this.jointArray.GetValue(6); } set { this.jointArray.SetValue(value, 6); } }


        [XmlIgnore]
        public Array posArray = new float[10];
        [XmlIgnore]
        public Array configArray = new short[7];
        [XmlIgnore]
        public Array jointArray = new float[10];
        public short userFrame = 0;
        public short userTool = 0;
        public short validC = 0;
        public short validJ = 0;
        /// <summary>
        /// 0:Offset ; 1:Postion ; 2:Joint
        /// </summary>
        public int MotionType = 0;
        /// <summary>
        /// (mm/sec)
        /// </summary>
        public int speed = 100;



        //非原生支援欄位 - 應該算是應用欄位
        public DateTime robotTime;
        public bool isReachTarget = false;



        public MvFanucRobotInfo() { }
        public MvFanucRobotInfo(MvFanucRobotInfo newone)
        {
            newone.posArray.CopyTo(posArray, 0);
            newone.configArray.CopyTo(configArray, 0);
            newone.jointArray.CopyTo(jointArray, 0);
            userFrame = newone.userFrame;
            userTool = newone.userTool;
            validC = newone.validC;
            validJ = newone.validJ;
            robotTime = newone.robotTime;
        }



    }




}
