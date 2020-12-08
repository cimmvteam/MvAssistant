using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.JSon.RobotTransferFile
{
    /// <summary>Box Robot 動作分類</summary>
    public  enum BoxrobotTransferActionType
    {
        Dontcare=0,
        /// <summary>取</summary>
       GET,
        /// <summary>放</summary>
        PUT
    }
    public static class BoxrobotTransferActionTypeExtends
    {
        public static string ToDefaultText(this BoxrobotTransferActionType inst)
        {
            return default(string);
        }

        /// <summary></summary>
        /// <param name="inst"></param>
        /// <returns></returns>
        public static string ToText(this BoxrobotTransferActionType inst)
        {
            string rtnV = inst.ToDefaultText();
            if (inst != BoxrobotTransferActionType.Dontcare)
            {
                rtnV = inst.ToString();
            }
            return rtnV;
        }
    }
}
