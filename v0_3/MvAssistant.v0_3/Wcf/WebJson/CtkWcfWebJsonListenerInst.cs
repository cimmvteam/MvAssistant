using MvaCToolkitCs.v1_2.Protocol;
using MvaCToolkitCs.v1_2.Threading;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MvaCToolkitCs.v1_2.Wcf.WebJson
{




    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class CtkWcfWebJsonListenerInst : ICtkWcfWebJsonListener
    {
        //[ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(ICTkWcfDuplexOpCallback))]
        //無法同時繼承並宣告ServiceContract




        public static CtkWcfWebJsonListener<ICtkWcfWebJsonListener> NewDefault(WebHttpBinding _binding = null)
        {
            var svrInst = new CtkWcfWebJsonListenerInst();
            if (_binding == null) _binding = new WebHttpBinding()
            {
            };
            return NewInst<ICtkWcfWebJsonListener>(svrInst, _binding);
        }

        public static CtkWcfWebJsonListener<T> NewInst<T>(T svrInst, WebHttpBinding _binding = null) where T : class, ICtkWcfWebJsonListener
        {
            if (_binding == null) _binding = new WebHttpBinding();
            return new CtkWcfWebJsonListener<T>(svrInst, _binding);
        }



        #region ICdStockCaptureWcfListener
        public void Capture()
        {
        }

        #endregion

    }


}
