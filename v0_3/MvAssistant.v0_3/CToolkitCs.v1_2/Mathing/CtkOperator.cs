using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CToolkitCs.v1_2.Mathing
{
    public abstract class CtkOperator<T> : ICtkOperator<T> where T : CtkOperator<T>, new()
    {



        public virtual T Multiply(T right) { throw new NotImplementedException(); }
        public virtual T Divide(T right) { throw new NotImplementedException(); }
        public virtual T Add(T right) { throw new NotImplementedException(); }
        public virtual T Subtract(T right) { throw new NotImplementedException(); }
        public virtual T Inverse() { throw new NotImplementedException(); }
        public virtual T Negative() { throw new NotImplementedException(); }
        public virtual bool Equal(T right) { throw new NotImplementedException(); }
        public virtual T GetIdentity() { throw new NotImplementedException(); }
        public virtual T GetZero() { throw new NotImplementedException(); }
        public virtual string ToOperatorString() { throw new NotImplementedException(); }
        public virtual T CastFromInt32(int val) { throw new NotImplementedException(); }
        public virtual T CastFromFloat(float val) { throw new NotImplementedException(); }
        public virtual T CastFromDouble(double val) { throw new NotImplementedException(); }
        public virtual T CastFromDecimal(decimal val) { throw new NotImplementedException(); }
        public virtual void Normalization() { throw new NotImplementedException(); }
        public virtual T GetNormalization() { throw new NotImplementedException(); }
        public virtual T Conjugation() { throw new NotImplementedException(); }


        
    }
}
