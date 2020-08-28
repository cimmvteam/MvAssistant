using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineBeta
{
    /// <summary>控制是否逾時類別</summary>
    public  class MacMsTimeOutController
    {
        /// <summary>逾時秒數</summary>
        private int _timeoutSecond { get;  set; }
        /// <summary>建構式</summary>
        public MacMsTimeOutController()
        {
            _timeoutSecond = 20;
        }

        /// <summary>建構式</summary>
        /// <param name="timeoutSecond">逾時秒數</param>
        public MacMsTimeOutController(int timeoutSecond)
        {
            _timeoutSecond = timeoutSecond;
        }

        /// <summary>判斷是否逾時</summary>
        /// <param name="startTime">開始計算時間</param>
        /// <param name="targetDiffSecs">逾時秒數</param>
        /// <returns></returns>
        public virtual bool IsTimeOut(DateTime startTime, int targetDiffSecs)
        {
            var thisTime = DateTime.Now;
            var diff = thisTime.Subtract(startTime).TotalSeconds;
            if (diff >= targetDiffSecs)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>判斷是否逾時</summary>
        /// <param name="startTime">開始計算的時間</param>
        /// <returns></returns>
        public virtual bool IsTimeOut(DateTime startTime)
        {
            return IsTimeOut(startTime, _timeoutSecond);
        }
    }
}
