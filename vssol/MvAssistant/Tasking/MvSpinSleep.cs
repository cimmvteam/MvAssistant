using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MvAssistant.Tasking
{
    public class MvSpinSleep
    {
        public DateTime LastSpinTime = DateTime.Now;
        public int WaitMillisecondsTotal = -1;//-1代表無上限
        public int WaitMillisecondsEach = -1;//-1代表不等待


        public MvSpinSleep() { }
        public MvSpinSleep(int totalMs, int eachMs) { this.SetTime(totalMs, eachMs); }


        public void SetTime(int? totalMs = null, int? eachMs = null)
        {
            if (totalMs.HasValue) this.WaitMillisecondsTotal = totalMs.Value;
            if (eachMs.HasValue) this.WaitMillisecondsEach = eachMs.Value;
        }



        public bool SleepLoopUntil(Func<bool> condition, int? totalMs = null, int? eachMs = null)
        {
            this.SetTime(totalMs, eachMs);

            var start = DateTime.Now;

            var flag = condition();//避免沒時間/來不及執行
            if (flag) return true;

            var forever = this.WaitMillisecondsTotal < 0;
            while (forever || (DateTime.Now - start).TotalMilliseconds < this.WaitMillisecondsTotal)
            {
                flag = condition();
                if (flag) return true;
                if (this.WaitMillisecondsEach > 0)
                    Thread.Sleep(this.WaitMillisecondsEach);
            }

            return flag;//一般為false
        }


        public bool SleepLoopUntilWithLast(Func<bool> condition, int? totalMs = null, int? eachMs = null)
        {
            this.SetTime(totalMs, eachMs);

            var start = this.LastSpinTime;
            //var alreadyWaitTime = DateTime.Now - this.LastSpinTime;

            var flag = condition();//避免沒時間/來不及執行
            if (flag) return true;
            var forever = this.WaitMillisecondsTotal < 0;
            while (forever || (DateTime.Now - start).TotalMilliseconds < this.WaitMillisecondsTotal)
            {
                flag = condition();
                if (flag) return true;
                if (this.WaitMillisecondsEach > 0)
                    Thread.Sleep(this.WaitMillisecondsEach);
            }
            return flag;
        }








        public static bool SpinUntil(Func<bool> condition, Action success = null, Action fail = null) { return SpinUntil(condition, -1, -1, success, fail); }
        public static bool SpinUntil(Func<bool> condition, int maxWaitMs, Action success = null, Action fail = null) { return SpinUntil(condition, maxWaitMs, -1, success, fail); }
        public static bool SpinUntil(Func<bool> condition, int maxWaitMs, int minWaitMs, Action success = null, Action fail = null)
        {
            var mvSpin = new MvSpinSleep();
            var flag = mvSpin.SleepLoopUntil(condition, maxWaitMs, minWaitMs);

            if (flag)
            { if (success != null) success(); }
            else
            { if (fail != null) fail(); }
            return flag;
        }





    }
}
