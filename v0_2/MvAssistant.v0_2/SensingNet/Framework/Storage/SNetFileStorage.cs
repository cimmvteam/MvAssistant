using CToolkit;
using CToolkit.v1_1;
using SensingNet.v0_2.DvcSensor.SignalTrans;
using SensingNet.v0_2.Framework.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace SensingNet.v0_2.Framework.Storage
{

    /// <summary>
    /// 提供簡易的儲存機制
    /// </summary>
    public class SNetFileStorage : IDisposable
    {
        public SNetFileStorageCfg Config = new SNetFileStorageCfg();

        FileInfo fi;
        //不採用SQLite/EntityFramework, 避免User還要安裝 SQLite.net provider
        StreamWriter fwriter;
        SNetFileStorageInfo fsInfo = new SNetFileStorageInfo();

        public SNetFileStorage() { }
        public SNetFileStorage(string dir) { this.Config.DirectoryPath = dir; }
        ~SNetFileStorage() { this.Dispose(false); }



        public void Write(SNetSignalTransEventArgs ea)
        {
            this.Write(ea.CalibrateData);
        }



        public void Write(List<double> dataList, DateTime? time = null)
        {
            if (dataList.Count <= 0) return;
            var now = DateTime.Now;
            if (time != null) now = time.Value;
            var nowUtc = now.ToUniversalTime();//寫資料, 要求傳入Utc

            //每分鐘 -> 實際儲存
            var fn = string.Format("dt{0}.signal.lock", now.ToString("yyyyMMddHHmm"));
            var dir = Path.Combine(this.Config.DirectoryPath, "dt" + now.ToString("yyyyMMdd"));
            var currfp = Path.Combine(dir, fn);
            var currfi = new FileInfo(currfp);


            try
            {
                //一秒內要進入
                if (!Monitor.TryEnter(this, 1000))
                {
                    CtkLog.WarnNs(this, "時限內無法進入Signal file create");
                    return;
                }
                //檔名是否需要置換了
                if (this.fwriter == null || this.fi.FullName != currfi.FullName)
                {
                    var lockFp = this.fi == null ? null : this.fi.FullName;
                    var nonLockFp = lockFp == null ? null : Regex.Replace(lockFp, @"\.lock$", "");

                    //先關掉舊檔
                    if (this.fwriter != null)
                        this.CloseStream(ref this.fwriter);

                    //再Reanme, 將檔案的.lock移除
                    if (lockFp != nonLockFp)
                        File.Move(lockFp, nonLockFp);

                    //不等待Event的工作, 避免來不及寫入
                    Task.Factory.StartNew(() =>
                    {
                        this.OnFileChanged(new SNetFileStorageEventArgs()
                        {
                            PrevFilePath = nonLockFp,
                            CurrFilePath = currfi.FullName,
                        });
                    });

                    this.fi = currfi;
                    if (!this.fi.Directory.Exists) this.fi.Directory.Create();
                    this.fwriter = new StreamWriter(currfi.FullName, false, Encoding.UTF8);//操作用 lock 檔
                    this.fsInfo.WriteHeader(this.fwriter);
                }
                //檔案是當前時區
                this.fsInfo.WriteValues(this.fwriter, nowUtc, dataList);

                this.fwriter.Flush();
            }
            finally { Monitor.Exit(this); }


        }




        void CloseStream(ref StreamWriter sw)
        {
            if (sw == null) return;
            try
            {
                using (sw)
                    sw.Close();
            }
            catch (System.ObjectDisposedException) { }
            sw = null;
        }

        void DeleteOld()
        {
            if (this.Config.PurgeIntervalSecond == 0) return;//沒設定不進行Purge

            var now = DateTime.Now;
            var purgeDt = now.AddSeconds(-this.Config.PurgeIntervalSecond);
            var roobtDi = new DirectoryInfo(this.Config.DirectoryPath);

            var yyyymmdd = purgeDt.ToString("yyyyMMdd");
            var yyyymmddhhmm = purgeDt.ToString("yyyyMMddHHmm");

            //找出超過時間的目錄, 直接刪除目錄
            var diList = (from row in roobtDi.GetDirectories()
                          where string.Compare(row.Name, "dt" + yyyymmdd) < 0
                          select row).ToList();
            foreach (var di in diList)
                di.Delete(true);



            //找出Purge那天的目錄
            var purgeDayDi = (from row in roobtDi.GetDirectories()
                              where string.Compare(row.Name, "dt" + yyyymmdd) == 0
                              select row).FirstOrDefault();
            if (purgeDayDi == null) return;

            //找出Purge那天, 需要被Purge的檔案
            var fiList = (from row in roobtDi.GetFiles()
                          where string.Compare(row.Name, "dt" + yyyymmddhhmm + ".signal") < 0
                          select row).ToList();
            foreach (var fi in fiList)
                fi.Delete();


        }





        #region Event

        public event EventHandler<SNetFileStorageEventArgs> EhFileChanged;
        public void OnFileChanged(SNetFileStorageEventArgs ea)
        {
            if (EhFileChanged == null) return;
            this.EhFileChanged(this, ea);
        }


        #endregion





        #region IDisposable
        // Flag: Has Dispose already been called?
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            this.DisposeSelf();
            disposed = true;
        }





        void DisposeSelf()
        {
            this.CloseStream(ref this.fwriter);

            CtkEventUtil.RemoveEventHandlersOfOwnerByFilter(this, (dlgt) => true);
        }

        #endregion


    }
}
