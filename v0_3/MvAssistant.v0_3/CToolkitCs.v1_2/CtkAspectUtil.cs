using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CToolkitCs.v1_2
{
    public class CtkAspectUtil
    {
        public Action<Action> Chain { get; set; }
        public CtkAspectUtil Combine(Action<Action> newAspectDelegate)
        {
            if (Chain == null)
            {
                Chain = newAspectDelegate;
            }
            else
            {
                Action<Action> existingChain = Chain;
                Action<Action> callAnother = work => existingChain(() => newAspectDelegate(work));
                Chain = callAnother;
            }
            return this;
        }
        public void Do(Action work)
        {
            if (Chain == null)
            {
                work();
            }
            else
            {
                Chain(work);
            }
        }



        public CtkAspectUtil RetryCache(int cnt, Action<Exception> catchHandler, Action retryFailHandler)
        {
            this.Combine(work =>
            {
                for(int idx=1; idx<=cnt; idx++)
                {
                    try
                    {
                        work();
                        return;
                    }catch(Exception ex)
                    {
                        catchHandler(ex);
                    }
                }
                retryFailHandler();
            });


            return this;
        }


        //--- Static ---

        public static CtkAspectUtil New { get { return new CtkAspectUtil(); } }

    }
}
