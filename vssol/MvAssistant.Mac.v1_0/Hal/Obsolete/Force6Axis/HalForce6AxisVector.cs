using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra.Single;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component.Force6Axis
{
    [GuidAttribute("F5B9C254-4551-453D-AF48-68DE104AD9B8")]
    public class HalForce6AxisVector : MathNet.Numerics.LinearAlgebra.Single.DenseVector
    {


        public float fx { get { return this[0]; } set { this[0] = value; } }
        public float fy { get { return this[1]; } set { this[1] = value; } }
        public float fz { get { return this[2]; } set { this[2] = value; } }
        public float mx { get { return this[3]; } set { this[3] = value; } }
        public float my { get { return this[4]; } set { this[4] = value; } }
        public float mz { get { return this[5]; } set { this[5] = value; } }


        public HalForce6AxisVector()
            : base(6)
        {

        }

        public HalForce6AxisVector(MathNet.Numerics.LinearAlgebra.Single.DenseVector vec)
            : base(vec.ToArray())
        {

        }







        public string GetVectorString()
        {
            return string.Format("{0:00000}, {1:00000}, {2:00000}, {3:00000}, {4:00000}, {5:00000}",
                this.fx, this.fy, this.fz, this.mx, this.my, this.mz);
        }



    }
}
