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
    [Serializable]
    public class HalRobotPose
    {
        [XmlIgnore] public MathNet.Numerics.LinearAlgebra.Single.DenseVector Xyzwpr = new MathNet.Numerics.LinearAlgebra.Single.DenseVector(9);

        [XmlAttribute] public float X { get { return this.Xyzwpr[0]; } set { this.Xyzwpr[0] = value; } }
        [XmlAttribute] public float Y { get { return this.Xyzwpr[1]; } set { this.Xyzwpr[1] = value; } }
        [XmlAttribute] public float Z { get { return this.Xyzwpr[2]; } set { this.Xyzwpr[2] = value; } }
        [XmlAttribute] public float W { get { return this.Xyzwpr[3]; } set { this.Xyzwpr[3] = value; } }
        [XmlAttribute] public float P { get { return this.Xyzwpr[4]; } set { this.Xyzwpr[4] = value; } }
        [XmlAttribute] public float R { get { return this.Xyzwpr[5]; } set { this.Xyzwpr[5] = value; } }
        [XmlAttribute] public float E1 { get { return this.Xyzwpr[6]; } set { this.Xyzwpr[6] = value; } }
        [XmlAttribute] public float E2 { get { return this.Xyzwpr[7]; } set { this.Xyzwpr[7] = value; } }
        [XmlAttribute] public float E3 { get { return this.Xyzwpr[8]; } set { this.Xyzwpr[8] = value; } }


        [XmlIgnore] public MathNet.Numerics.LinearAlgebra.Single.DenseVector Joint = new MathNet.Numerics.LinearAlgebra.Single.DenseVector(9);

        [XmlAttribute] public float J1 { get { return this.Joint[0]; } set { this.Joint[0] = value; } }
        [XmlAttribute] public float J2 { get { return this.Joint[1]; } set { this.Joint[1] = value; } }
        [XmlAttribute] public float J3 { get { return this.Joint[2]; } set { this.Joint[2] = value; } }
        [XmlAttribute] public float J4 { get { return this.Joint[3]; } set { this.Joint[3] = value; } }
        [XmlAttribute] public float J5 { get { return this.Joint[4]; } set { this.Joint[4] = value; } }
        [XmlAttribute] public float J6 { get { return this.Joint[5]; } set { this.Joint[5] = value; } }
        [XmlAttribute] public float J7 { get { return this.Joint[6]; } set { this.Joint[6] = value; } }
        [XmlAttribute] public float J8 { get { return this.Joint[7]; } set { this.Joint[7] = value; } }
        [XmlAttribute] public float J9 { get { return this.Joint[8]; } set { this.Joint[8] = value; } }

    }
}
