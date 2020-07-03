using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.JSon.RobotTransferFile
{
    /// <summary>Boxrobot 移動方向</summary>
    public enum BoxrobotTransferDirection
    {
        Dontcare=0,
        /// <summary>前進到 Cabinet</summary>
        Forward,
        /// <summary>回到Home</summary>
        Backward        
    }

    public static class BoxrobotTransferDirectionExtends
    {
        public static string ToDefaultText(this BoxrobotTransferDirection inst)
        {
            return default(string);

        } 

        public static string ToText(this BoxrobotTransferDirection inst)
        {
            var rtnV = inst.ToDefaultText();
            if(inst != BoxrobotTransferDirection.Dontcare)
            {
                rtnV = inst.ToString();
            }
            return rtnV;
        }
    }
}
