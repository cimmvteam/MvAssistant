using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.WacohForce
{
    public class WacohForceMessageEventArgs : EventArgs
    {

        public WacohForceVector centerForceVector;
        public WacohForceVector rawForceVector;
        public WacohForceVector correctForceVector { get { return new WacohForceVector(rawForceVector - centerForceVector); } }



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
