using MvAssistant.Mac.v1_0;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace MvAssistant.Mac
{
    /// <summary>
    /// abstract fake HAL (Hardware Abstract Layer) class
    /// </summary>
    [GuidAttribute("A0D5CD14-8A44-46EA-AF8C-DF2724A7517B")]
    public abstract class HalFakeBase : IHalFake
    {


        #region IHal



        public virtual int HalClose()
        {
            throw new NotImplementedException();
        }

        public virtual int HalConnect()
        {
            throw new NotImplementedException();
        }

        public virtual bool HalIsConnected()
        {
            throw new NotImplementedException();
        }

        public virtual int HalStop()
        {
            throw new NotImplementedException();
        }


        #endregion



    }
}