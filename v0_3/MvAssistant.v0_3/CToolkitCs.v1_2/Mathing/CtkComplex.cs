using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CToolkitCs.v1_2.Mathing
{
    /*Q*/

    public class CtkComplex : CtkOperator<CtkComplex>
    {

        public CtkRational Real;
        public CtkRational Imaginary;

        public CtkComplex() { }
        public CtkComplex(CtkRational real, CtkRational denominator) { this.Real = real; this.Imaginary = denominator; }
        public CtkComplex(CtkRational real) { this.Real = real; this.Imaginary = 0; }
        public CtkComplex(CtkComplex field) { this.Real = field.Real; this.Imaginary = field.Imaginary; }


        #region Operator
        public static CtkComplex operator *(CtkComplex left, CtkComplex right) { return left.Multiply(right); }


        public static implicit operator CtkComplex(int val) { return new CtkComplex(val); }
        public static implicit operator CtkComplex(CtkRational val) { return new CtkComplex(val); }
        public static implicit operator CtkComplex(CtkRational[] val)
        {
            return new CtkComplex(val[0], val[1]);
        }
        #endregion




        #region CtkOperator

        public override CtkComplex Multiply(CtkComplex right)
        { return new CtkComplex(this.Real * right.Real - this.Imaginary * right.Imaginary, this.Real * right.Imaginary + this.Imaginary * right.Real); }

        public override CtkComplex Divide(CtkComplex right)
        { return this.Multiply(right.Inverse()); }

        public override CtkComplex Add(CtkComplex right)
        { return new CtkComplex(this.Real + right.Real, this.Imaginary + right.Imaginary); }


        public override CtkComplex Subtract(CtkComplex right)
        { return new CtkComplex(this.Real - right.Real, this.Imaginary - right.Imaginary); }

        public override CtkComplex Inverse()
        {
            var tmp = this.Real * this.Real + this.Imaginary * this.Imaginary;
            return new CtkComplex(this.Real.Divide(tmp), this.Imaginary.Divide(tmp).Negative());
        }

        public override CtkComplex Negative()
        { return new CtkComplex(this.Real.Negative(), this.Imaginary.Negative()); }

        public override CtkComplex Conjugation() { return new CtkComplex(this.Real, this.Imaginary.Negative()); }


        public override CtkComplex GetIdentity()
        { return new CtkComplex(1, 0); }
        public override CtkComplex GetZero()
        { return new CtkComplex(0, 0); }







        public override bool Equal(CtkComplex right)
        {
            var a = this.GetNormalization();
            var b = right.GetNormalization();
            return (a.Real == b.Real && a.Imaginary == b.Imaginary);
        }


        public override void Normalization()
        { this.Real.Normalization(); this.Imaginary.Normalization(); }
        public override CtkComplex GetNormalization()
        {
            var rs = new CtkComplex(this);
            rs.Normalization();
            return rs;
        }


        public override string ToOperatorString()
        {
            return this.Real.ToOperatorString() + "+" + this.Imaginary.ToOperatorString() + "i";
        }






        public override CtkComplex CastFromInt32(int val)
        {
            throw new NotImplementedException();
        }

        public override CtkComplex CastFromFloat(float val)
        {
            throw new NotImplementedException();
        }

        public override CtkComplex CastFromDouble(double val)
        {
            throw new NotImplementedException();
        }

        public override CtkComplex CastFromDecimal(decimal val)
        {
            throw new NotImplementedException();
        }

        #endregion






    }
}
