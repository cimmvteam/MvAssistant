using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CToolkitCs.v1_2.Mathing
{
    /*Q*/

    public class CtkRealF : CtkOperator<CtkRealF>
    {

        public float Value;

        public CtkRealF() { }
        public CtkRealF(float val) { this.Value = val; }
        public CtkRealF(CtkRealF field) { this.Value = field.Value; }


        #region Operator
        public static CtkRealF operator *(CtkRealF left, CtkRealF right) { return left.Multiply(right); }


        public static implicit operator CtkRealF(float val) { return new CtkRealF(val); }
        #endregion




        #region CtkOperator

        public override CtkRealF Multiply(CtkRealF right)
        { return new CtkRealF(this.Value * right.Value); }

        public override CtkRealF Divide(CtkRealF right)
        { return new CtkRealF(this.Value / right.Value); }

        public override CtkRealF Add(CtkRealF right)
        { return new CtkRealF(this.Value + right.Value); }

        public override CtkRealF Subtract(CtkRealF right)
        { return new CtkRealF(this.Value - right.Value); }

        public override CtkRealF Negative()
        { return new CtkRealF(-this.Value); }






        public override CtkRealF Inverse()
        { return new CtkRealF(1 / this.Value); }


        public override CtkRealF Conjugation() { return new CtkRealF(this); }



        public override CtkRealF GetIdentity()
        { return 1.0f; }
        public override CtkRealF GetZero()
        { return 0.0f; }



        public override bool Equal(CtkRealF right)
        { return this.Value == right.Value; }


        public override void Normalization()
        { return; }
        public override CtkRealF GetNormalization()
        { return new CtkRealF(this.Value); }


        public override string ToOperatorString()
        { return this.Value.ToString(); }




        public override CtkRealF CastFromInt32(int val)
        { return new CtkRealF(val); }

        public override CtkRealF CastFromFloat(float val)
        { return new CtkRealF(val); }

        public override CtkRealF CastFromDouble(double val)
        { return new CtkRealF(Convert.ToSingle(val)); }

        public override CtkRealF CastFromDecimal(decimal val)
        { return new CtkRealF(Convert.ToSingle(val)); }

        #endregion







    }
}
