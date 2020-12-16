using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MvAssistant.v0_2.Tasking
{
    public class MvaSpinWait
    {
        public DateTime LastSpinTime = DateTime.Now;
        public int WaitMillisecondsMax = -1;//-1代表無上限
        public int WaitMillisecondsMin = -1;//-1代表不等待
        Action _fail;
        Action _success;
        public MvaSpinWait() { }
        public MvaSpinWait(int maxWaitMs, int minWaitMs) { this.SetWait(maxWaitMs, minWaitMs); }


        public MvaSpinWait OnFail(Action fail) { this._fail = fail; return this; }

        public MvaSpinWait OnSuccess(Action success) { this._success = success; return this; }

        public void SetWait(int? maxWaitMs = null, int? minWaitMs = null)
        {
            if (maxWaitMs.HasValue) this.WaitMillisecondsMax = maxWaitMs.Value;
            if (minWaitMs.HasValue) this.WaitMillisecondsMin = minWaitMs.Value;

            if (this.WaitMillisecondsMax < this.WaitMillisecondsMin) throw new ArgumentException("最大等待時間不能低於最小等待時間");
        }
        public bool WaitUntil(Func<bool> condition, int? maxWaitMs = null, int? minWaitMs = null)
        {
            this.SetWait(maxWaitMs, minWaitMs);

            if (this.WaitMillisecondsMin > 0)
                Thread.Sleep(this.WaitMillisecondsMin);

            var flag = false;
            if (this.WaitMillisecondsMax >= 0)
                flag = SpinWait.SpinUntil(condition, this.WaitMillisecondsMax - this.WaitMillisecondsMin);
            else
                flag = SpinWait.SpinUntil(condition, -1);//小於0就無限等待

            if (flag && this._success != null) this._success();
            if (!flag && this._fail != null) this._fail();
            return flag;

        }


        public bool WaitUntilWithLast(Func<bool> condition, int? maxWaitMs = null, int? minWaitMs = null)
        {

            this.SetWait(maxWaitMs, minWaitMs);


            //小於0不等待
            if (this.WaitMillisecondsMin > 0)
            {
                var elapseMs = (int)(DateTime.Now - this.LastSpinTime).TotalMilliseconds;
                var ms = this.WaitMillisecondsMin - elapseMs;//剩餘等待時間
                if (ms > 0)
                    Thread.Sleep(ms);
            }

            //是否應該 減去已經等待的時間? 但有可能會造成來不及判斷條件

            var flag = condition();
            if (flag) return true;//先執行一次, 避免來不及/沒時間執行, "不"置於最小等待時間前面

            if (this.WaitMillisecondsMax >= 0)
            {
                var elapseMs = (int)(DateTime.Now - this.LastSpinTime).TotalMilliseconds;
                var ms = this.WaitMillisecondsMax - elapseMs;//剩餘等待時間
                if (ms > 0)
                    flag = SpinWait.SpinUntil(condition, ms);
                // ms == 0 代表沒剩餘時間等待了, 下面flag會return false
            }
            else
                flag = SpinWait.SpinUntil(condition, -1);


            this.LastSpinTime = DateTime.Now;

            if (flag && this._success != null) this._success();
            if (!flag && this._fail != null) this._fail();
            return flag;
        }
        #region Static

        public static MvaSpinWait New(int? maxWaitMs = null, int? minWaitMs = null)
        {
            var mv = new MvaSpinWait();
            mv.SetWait(maxWaitMs, minWaitMs);
            return mv;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="success"></param>
        /// <param name="fail"></param>
        /// <returns></returns>
        public static bool SpinUntil(Func<bool> condition, Action success = null, Action fail = null) { return SpinUntil(condition, -1, -1, success, fail); }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="maxWaitMs">-1 無限等待</param>
        /// <param name="success"></param>
        /// <param name="fail"></param>
        /// <returns></returns>
        public static bool SpinUntil(Func<bool> condition, int maxWaitMs, Action success = null, Action fail = null) { return SpinUntil(condition, maxWaitMs, -1, success, fail); }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="maxWaitMs">-1 無限等待</param>
        /// <param name="minWaitMs">-1 不等待</param>
        /// <param name="success"></param>
        /// <param name="fail"></param>
        /// <returns></returns>
        public static bool SpinUntil(Func<bool> condition, int maxWaitMs, int minWaitMs, Action success = null, Action fail = null)
        {
            var mvSpin = new MvaSpinWait();
            var flag = mvSpin.WaitUntil(condition, maxWaitMs, minWaitMs);

            if (flag)
            { if (success != null) success(); }
            else
            { if (fail != null) fail(); }
            return flag;
        }
        #endregion




    }
}
