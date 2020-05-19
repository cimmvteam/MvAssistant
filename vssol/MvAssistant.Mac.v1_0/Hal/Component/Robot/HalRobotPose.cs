using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MvAssistant.Mac.v1_0.Hal.Component.Robot
{
    [GuidAttribute("27EEE4BE-B9A4-436D-A0F2-4DAF4319BE09")]
    public class HalRobotPose
    {
        [XmlIgnore]
        public MathNet.Numerics.LinearAlgebra.Single.DenseVector Vector = new MathNet.Numerics.LinearAlgebra.Single.DenseVector(9);

        public float X { get { return this.Vector[0]; } set { this.Vector[0] = value; } }
        public float Y { get { return this.Vector[1]; } set { this.Vector[1] = value; } }
        public float Z { get { return this.Vector[2]; } set { this.Vector[2] = value; } }
        public float W { get { return this.Vector[3]; } set { this.Vector[3] = value; } }
        public float P { get { return this.Vector[4]; } set { this.Vector[4] = value; } }
        public float R { get { return this.Vector[5]; } set { this.Vector[5] = value; } }
        public float E1 { get { return this.Vector[6]; } set { this.Vector[6] = value; } }

        [XmlIgnore]
        public MathNet.Numerics.LinearAlgebra.Single.DenseVector joint = new MathNet.Numerics.LinearAlgebra.Single.DenseVector(9);
        public float J1 { get { return this.joint[0]; } set { this.joint[0] = value; } }
        public float J2 { get { return this.joint[1]; } set { this.joint[1] = value; } }
        public float J3 { get { return this.joint[2]; } set { this.joint[2] = value; } }
        public float J4 { get { return this.joint[3]; } set { this.joint[3] = value; } }
        public float J5 { get { return this.joint[4]; } set { this.joint[4] = value; } }
        public float J6 { get { return this.joint[5]; } set { this.joint[5] = value; } }
        public float J7 { get { return this.joint[6]; } set { this.joint[6] = value; } }




    }
}
