using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SensingNet.v0_2
{
    public class SNetUtil
    {

        //test

        public static int DoBgWorkerAsync(DoWorkEventHandler method)
        {
            var bg = new BackgroundWorker();
            bg.DoWork += method;
            bg.WorkerSupportsCancellation = true;
            bg.RunWorkerAsync();



            return 0;
        }



        public string MyNameIs()
        {
            return this.GetType().Namespace;
        }


    }
}
