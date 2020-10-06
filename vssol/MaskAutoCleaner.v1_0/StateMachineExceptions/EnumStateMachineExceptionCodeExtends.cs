using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineExceptions
{


    /// <summary>EnumStateMachineExceptionCode 的擴充方法</summary>
    public static class EnumStateMachineExceptionCodeExtends
    {
        /// <summary>轉成錯誤代碼</summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static string ToErroeCode(this EnumStateMachineExceptionCode instance)
        {
            var intCode = (int)instance;
            var stringCode = intCode.ToString("000000");
            return stringCode;
        }
    }
}
