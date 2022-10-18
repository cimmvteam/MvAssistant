using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CToolkitCs.v1_2.Mathing
{
    /*R*/

    public class CtkRational : CtkOperator<CtkRational>
    {

        public int Molecular;
        public int Denominator;

        public CtkRational() { }
        public CtkRational(int molecular, int denominator) { this.Molecular = molecular; this.Denominator = denominator; }
        public CtkRational(int molecular) { this.Molecular = molecular; this.Denominator = 1; }
        public CtkRational(CtkRational rational) { this.Molecular = rational.Molecular; this.Denominator = rational.Denominator; }



        #region Operator
        public static CtkRational operator *(CtkRational left, CtkRational right) { return left.Multiply(right); }
        public static CtkRational operator +(CtkRational left, CtkRational right) { return left.Add(right); }
        public static CtkRational operator -(CtkRational left, CtkRational right) { return left.Subtract (right); }
        


        public static implicit operator CtkRational(int val) { return new CtkRational(val); }
        #endregion


        public static CtkRational GenerateZero() { return new CtkRational(0, 1); }
        public static CtkRational GenerateIdentity() { return new CtkRational(1, 1); }





        #region CtkOperator

        public override CtkRational Multiply(CtkRational right)
        { return new CtkRational(this.Molecular * right.Molecular, this.Denominator * right.Denominator); }

        public override CtkRational Divide(CtkRational right)
        { return new CtkRational(this.Molecular * right.Denominator, this.Denominator * right.Molecular); }

        public override CtkRational Add(CtkRational right)
        { return new CtkRational(this.Molecular * right.Denominator + this.Denominator * right.Molecular, this.Denominator * right.Denominator); }


        public override CtkRational Subtract(CtkRational right)
        { return new CtkRational(this.Molecular * right.Denominator - this.Denominator * right.Molecular, this.Denominator * right.Denominator); }



        public override CtkRational Inverse()
        { return new CtkRational(this.Denominator, this.Molecular); }

        public override CtkRational Negative()
        { return new CtkRational(-this.Molecular, this.Denominator); }



        public override CtkRational Conjugation() { return new CtkRational(this); }

        public override CtkRational GetIdentity()
        { return new CtkRational(1, 1); }
        public override CtkRational GetZero()
        { return new CtkRational(0, 1); }







        public override bool Equal(CtkRational right)
        {
            var a = this.GetNormalization();
            var b = right.GetNormalization();
            return (a.Molecular == b.Molecular && a.Denominator == b.Denominator);
        }


        public override void Normalization()
        {
            int gcd = CtkMathOpUtil.GreatestCommonDivisor(this.Molecular, this.Denominator);
            if (gcd == 0) { return; }
            this.Molecular /= gcd;
            this.Denominator /= gcd;

            if (this.Denominator < 0)
            {
                this.Molecular = -this.Molecular;
                this.Denominator = -this.Denominator;
            }
        }
        public override CtkRational GetNormalization()
        {
            var rs = new CtkRational(this.Molecular, this.Denominator);
            rs.Normalization();
            return rs;
        }


        public override string ToOperatorString()
        {
            if (this.Denominator == 1 || this.Molecular == 0) { return this.Molecular.ToString(); }
            else { return this.Molecular + "/" + this.Denominator; }
        }





        public override CtkRational CastFromInt32(int val)
        { return new CtkRational(val, 1); }








        public override CtkRational CastFromFloat(float val)
        {
            throw new NotImplementedException();
        }

        public override CtkRational CastFromDouble(double val)
        {
            throw new NotImplementedException();
        }

        public override CtkRational CastFromDecimal(decimal val)
        {
            throw new NotImplementedException();
        }
        #endregion










    }
}
