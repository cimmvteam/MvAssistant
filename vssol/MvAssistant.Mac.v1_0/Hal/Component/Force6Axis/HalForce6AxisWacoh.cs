using MvAssistant.Mac.v1_0.Hal.Component;
using MvAssistant.Mac.v1_0.Hal.Component.Force6Axis;
using MvAssistant.DeviceDrive;
using MvAssistant.DeviceDrive.WacohForce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component.Force6Axis
{
    [GuidAttribute("DE783AF6-C770-4DCA-B271-E0EEBF5787F6")]
    public class HalForce6AxisWacoh : MacHalComponentBase, IHalForce6Axis
    {
        WacohForceLdd ldd;

        public HalForce6AxisWacoh()
        {
            this.ldd = new WacohForceLdd();
            this.ldd.evtDataReceive += ldd_evtDataReceive;
        }


        #region IHal

        int IHal.HalConnect()
        {
            return this.ldd.ConnectIfNo();
        }

        int IHal.HalClose()
        {
            return this.ldd.Close();
        }

        bool IHal.HalIsConnected()
        {
            return this.ldd.IsConnect();
        }

        #endregion


        #region Event Declare
        public event EventHandler<IHalForce6AxisEventArgs> evtDataReceive;
        void OnDataReceive(IHalForce6AxisEventArgs ea)
        {
            if (this.evtDataReceive == null) return;
            this.evtDataReceive(this, ea);
        }

        #endregion

        #region Event Implementation
        void ldd_evtDataReceive(object sender, MvAssistant.DeviceDrive.WacohForce.WacohForceMessageEventArgs e)
        {
            var ea = new IHalForce6AxisEventArgs();
            ea.centerForceVector = new HalForce6AxisVector(e.centerForceVector);
            ea.rawForceVector = new HalForce6AxisVector(e.rawForceVector);
            this.OnDataReceive(ea);
        }
        #endregion



        #region IHalForce6Axis

        event EventHandler<IHalForce6AxisEventArgs> IHalForce6Axis.evtDataReceive
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }



        public HalForce6AxisVector GetVector()
        {
            throw new NotImplementedException();
        }

        #endregion



    }
}
