using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.DeviceDrive.WacohForce
{
    public class MvaWacohForceMessageEventArgs : EventArgs
    {

        public MvaWacohForceVector centerForceVector;
        public MvaWacohForceVector rawForceVector;
        public MvaWacohForceVector correctForceVector { get { return new MvaWacohForceVector(rawForceVector - centerForceVector); } }



        public int iChk
        {
            get
            {
                return Convert.ToInt32(
                    Math.Sqrt(
                    Math.Pow(
                (float)((this.rawForceVector.fx - this.centerForceVector.fx) / 3.2)
                    , 2)
                    + Math.Pow(
                (float)((this.rawForceVector.fy - this.centerForceVector.fy) / 3.2)
                    , 2)
                    + Math.Pow(
                (float)((this.rawForceVector.fz - this.centerForceVector.fz) / 3.2)
                    , 2)));
            }
        }





    }
}
