using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CToolkitCs.v1_2.Threading
{
    /// <summary>
    /// 有限制全部或延遲等待時間之類的需求, 才需要用此類別.
    /// 否則可以考慮用SpiWait
    /// </summary>
    public class CtkSpinWait
    {
        public DateTime? SpecifiedStartTime = null;

        /// <summary> 等待期間的每次判定間隔 </summary>
        public int WaitCheckIntervalMs = -1;
        /// <summary> 延遲判定時間(milli seconds)=不立刻執行. -1代表不延遲 </summary>
        public int WaitCheckDelayMs = -1;
        /// <summary> 全部等待時間(milli seconds)=超過就不再等待. -1代表無限等待 </summary>
        public int WaitLimitTotalMs = -1;

        public CtkSpinWait() { }


        public void SetWaitTime(int? waitLimitTotalMs = null, int? waitCheckIntervalMs = null, int? waitCheckDelayMs = null)
        {
            if (waitLimitTotalMs.HasValue) this.WaitLimitTotalMs = waitLimitTotalMs.Value;
            if (waitCheckIntervalMs.HasValue) this.WaitCheckIntervalMs = waitCheckIntervalMs.Value;
            if (waitCheckDelayMs.HasValue) this.WaitCheckDelayMs = waitCheckDelayMs.Value;

            if (this.WaitLimitTotalMs < this.WaitCheckIntervalMs) throw new ArgumentException("全部等待時間不能低於判定間隔時間");
            if (this.WaitLimitTotalMs < this.WaitCheckDelayMs) throw new ArgumentException("全部等待時間不能低於判定延遲時間");
        }

        public bool WaitSleep(Func<bool> condition, int? waitLimitTotalMs = null, int? waitCheckIntervalMs = null, int? waitCheckDelayMs = null)
        {
            this.SetWaitTime(waitLimitTotalMs, waitCheckIntervalMs, waitCheckDelayMs);
            var start = this.SpecifiedStartTime.HasValue ? this.SpecifiedStartTime.Value : DateTime.Now;

            //延遲開始
            if (this.WaitCheckDelayMs > 0)
            {//小於0不等待
                var waitMs = (int)(DateTime.Now - start).TotalMilliseconds;
                var delayMs = this.WaitCheckDelayMs - waitMs;//剩餘等待時間
                if (delayMs > 0) Thread.Sleep(delayMs);
            }

            //先行判定
            var flag = condition(); //先執行一次, 避免來不及/沒時間執行, 已滿足就return
            if (flag) return true;

            if (this.WaitCheckIntervalMs > 0)
            {// =0代表無間隔 或 小於0也是
                while ((DateTime.Now - start).TotalMilliseconds < this.WaitLimitTotalMs || this.WaitLimitTotalMs < 0)
                {//持續確認到滿足限制時間, 若沒設置限制 無限等待
                    flag = condition();
                    if (flag) return true;
                    Thread.Sleep(this.WaitCheckIntervalMs);
                }
            }
            else
            {// 若沒設置間隔時間, 視為 SpinUnit
                if (this.WaitLimitTotalMs >= 0)
                {//但有設置限制時間
                    var waitMs = (int)(DateTime.Now - start).TotalMilliseconds;
                    var remaindMs = this.WaitLimitTotalMs - waitMs;//剩餘等待時間
                    if (remaindMs > 0)
                        flag = SpinWait.SpinUntil(condition, remaindMs);
                    // == 0 代表沒剩餘時間等待了, 下面flag會return false
                }
                else
                    flag = SpinWait.SpinUntil(condition, -1);
            }
            return flag;
        }
        public bool WaitUntil(Func<bool> condition, int? waitLimitTotalMs = null, int? waitCheckIntervalMs = null, int? waitCheckDelayMs = null)
        {
            this.SetWaitTime(waitLimitTotalMs, waitCheckIntervalMs, waitCheckDelayMs);
            var start = this.SpecifiedStartTime.HasValue ? this.SpecifiedStartTime.Value : DateTime.Now;

            //延遲開始
            if (this.WaitCheckDelayMs > 0)
            {//小於0不等待
                var waitMs = (int)(DateTime.Now - start).TotalMilliseconds;
                var delayMs = this.WaitCheckDelayMs - waitMs;//剩餘等待時間
                if (delayMs > 0) Thread.Sleep(delayMs);
            }

            //先行判定
            var flag = condition(); //先執行一次, 避免來不及/沒時間執行, 已滿足就return
            if (flag) return true;


            if (this.WaitCheckIntervalMs > 0)
            {//若有設定 判定間隔
                while ((DateTime.Now - start).TotalMilliseconds < this.WaitLimitTotalMs || this.WaitLimitTotalMs < 0)
                {
                    //每隔段時間會出來
                    flag = SpinWait.SpinUntil(condition, this.WaitCheckIntervalMs);
                    if (flag) return true;
                }
            }
            else
            {
                if (this.WaitLimitTotalMs >= 0)
                {
                    var waitMs = (int)(DateTime.Now - start).TotalMilliseconds;
                    var remaindMs = this.WaitLimitTotalMs - waitMs;//剩餘等待時間
                    if (remaindMs > 0)
                        flag = SpinWait.SpinUntil(condition, remaindMs);
                    // == 0 代表沒剩餘時間等待了, 下面flag會return false
                }
                else
                    flag = SpinWait.SpinUntil(condition, -1);
            }

            return flag;
        }



        #region Static

        public static bool SpinSleep(Func<bool> condition, Action success = null, Action fail = null) { return SpinSleep(condition, -1, -1, -1, success, fail); }
        public static bool SpinSleep(Func<bool> condition, int waitLimitTotalMs, Action success = null, Action fail = null) { return SpinSleep(condition, waitLimitTotalMs, -1, -1, success, fail); }
        public static bool SpinSleep(Func<bool> condition, int waitLimitTotalMs, int waitCheckIntervalMs, Action success = null, Action fail = null) { return SpinSleep(condition, waitLimitTotalMs, waitCheckIntervalMs, -1, success, fail); }
        public static bool SpinSleep(Func<bool> condition, int waitLimitTotalMs, int waitCheckIntervalMs, int waitCheckDelayMs, Action success = null, Action fail = null)
        {
            var mvSpinWait = new CtkSpinWait();
            var flag = mvSpinWait.WaitSleep(condition, (int?)waitLimitTotalMs, waitCheckIntervalMs, waitCheckDelayMs);

            if (flag)
            { if (success != null) success(); }
            else
            { if (fail != null) fail(); }
            return flag;
        }

        public static bool SpinUntil(Func<bool> condition, Action success = null, Action fail = null) { return SpinUntil(condition, -1, -1, success, fail); }
        public static bool SpinUntil(Func<bool> condition, int waitLimitTotalMs, Action success = null, Action fail = null) { return SpinUntil(condition, waitLimitTotalMs, -1, success, fail); }
        public static bool SpinUntil(Func<bool> condition, int waitLimitTotalMs, int waitCheckIntervalMs, Action success = null, Action fail = null) { return SpinUntil(condition, waitLimitTotalMs, waitCheckIntervalMs, -1, success, fail); }
        public static bool SpinUntil(Func<bool> condition, int waitLimitTotalMs, int waitCheckIntervalMs, int waitCheckDelayMs, Action success = null, Action fail = null)
        {
            var mvSpinWait = new CtkSpinWait();
            var flag = mvSpinWait.WaitUntil(condition, (int?)waitLimitTotalMs, waitCheckIntervalMs, waitCheckDelayMs);

            if (flag)
            { if (success != null) success(); }
            else
            { if (fail != null) fail(); }
            return flag;
        }

        #endregion


    }
}
