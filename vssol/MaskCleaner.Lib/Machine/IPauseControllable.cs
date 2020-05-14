using MaskAutoCleaner.Machine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Machine
{
    public interface IPauseControllable
    {
        /// <summary>
        /// 控制State machine要停止或開啟自動執行trigger的功能
        /// </summary>
        /// <param name="emrm"></param>
        void SetMachineRunningMode(EnumMachineRunningMode emrm);
        void SetStateMAchinePause();
        void SetStateMachineRun();
    }
}
