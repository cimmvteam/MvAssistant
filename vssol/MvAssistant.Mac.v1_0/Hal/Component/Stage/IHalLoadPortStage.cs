using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component.Stage
{
    [GuidAttribute("FAACAFC5-5B69-4782-824B-10AEB3B4BFF6")]
    public interface IHalLoadPortStage : IHalComponent
    {
        /// <summary>
        /// 設定[LoadPort Stage]移動速度
        /// </summary>
        /// <param name="speed"></param>
        /// <returns></returns>
        bool SetMoveSpeed(int speed);

        /// <summary>
        /// 移動[LoadPort Stage]
        /// </summary>
        /// <param name="moveinfo"></param>
        /// <param name="arriveFlag"></param>
        void Move(double moveinfo, out bool arriveFlag);

        /// <summary>
        /// Emergence Active
        /// </summary>
        void EmergenceActive();
    }
}
