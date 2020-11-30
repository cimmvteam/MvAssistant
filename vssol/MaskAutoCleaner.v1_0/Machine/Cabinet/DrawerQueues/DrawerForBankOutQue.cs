using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.Cabinet.DrawerQueues
{
    /// <summary>記錄 在 Drawer 上等待 Bank Out MaskBox 的Queue</summary>
    /// <remarks>
    /// <para>2020/11/26 King Liu [C]</para>
    /// </remarks>
    public  class DrawerForBankOutQue:Queue<DrawerSatusInfo>
    {
        Thread ScanThread;

        /// <summary>Lock object  of  creating unique Instance</summary>
        /// <remarks>
        /// <para>2020/11/26 King Liu[C]</para>
        /// </remarks>
        private static  readonly object lockObjectForGetInstance=new Object();

        /// <summary>instace of singleton</summary>
        /// <remarks>
        /// <para>2020/11/26 King Liu[c]</para>
        /// </remarks>
        private static DrawerForBankOutQue _instance = null;

        /// <summary>to get a singleton instance</summary>
        /// <returns></returns>
        public static DrawerForBankOutQue GetInstance()
        {
            if (_instance == null)
            {
                lock (lockObjectForGetInstance)
                {
                    if (_instance == null)
                    {

                        _instance = new DrawerForBankOutQue();

                    }
                }
            }

            return _instance;
        }

        private DrawerForBankOutQue()
        {

            
          //  ScanThread.IsBackground = true;
        //    ScanThread.Start();

        }


        /// <summary>Enqueue an unique MaskBoxInfo instance</summary>
        /// <param name="maskBoxInfo">enqueue itenm</param>
        /// <param name="message">enqueue result message</param>
        /// <returns>value of  EnqueueUniqueMaskBoxForBankOutQueResult(enum)</returns>
        public EnqueueUniqueMaskBoxForBankOutQueResult EnqueueUnique(DrawerSatusInfo maskBoxInfo,out string message )
        {
            EnqueueUniqueMaskBoxForBankOutQueResult rtnV;
          
            if (this.Where(m => m.BoxBarcode == maskBoxInfo.BoxBarcode).FirstOrDefault() != default(DrawerSatusInfo))
            {
                message = maskBoxInfo.BoxBarcode;
                rtnV = EnqueueUniqueMaskBoxForBankOutQueResult.DuplicateBoxBarcode;
            }
           else  if(this.Where(m => m.DrawerMachineID == maskBoxInfo.DrawerMachineID).FirstOrDefault() != default(DrawerSatusInfo))
            {
                message = maskBoxInfo.DrawerMachineID.ToString();
                rtnV = EnqueueUniqueMaskBoxForBankOutQueResult.DuplicateDrawerMachineID;
            }
            else
            {
                rtnV = EnqueueUniqueMaskBoxForBankOutQueResult.EnqueOK;
                this.Enqueue(maskBoxInfo);
                // TODO: 在這裏更新 JSON 檔
                message = string.Empty;
            }

            return rtnV;
        }

      
    }
    
   
    
}
