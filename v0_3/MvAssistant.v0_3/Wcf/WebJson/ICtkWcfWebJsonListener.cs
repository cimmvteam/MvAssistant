using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MvaCToolkitCs.v1_2.Wcf.WebJson
{

    [ServiceContract(SessionMode = SessionMode.Allowed)]
    public interface ICtkWcfWebJsonListener
    {
        [OperationContract()]
        void Capture();

    }



}
