using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.Cabinet
{
    /// <summary>Cabinet 的工作階段</summary>
    public enum CabinetDuration
    {
        /// <summary>Nothing</summary>
        DontCare,
        
        
        /// <summary>System Boot UP 的Initial</summary>
        SystemBootUp,

        /// <summary>一般的 Initial</summary>
        Initial, 
        
        /// <summary>Bank In</summary>
        BankIn,
        /// <summary>Bank Out</summary>
        BankOut,

    }

    public static class CabinetDurationExtends
    {
        public static CabinetDuration Reset(this CabinetDuration Instance)
        {
             return CabinetDuration.DontCare;
        }
        public static CabinetDuration ToIdle(this CabinetDuration Instance)
        {
            return Instance.Reset();
        }

        public static CabinetDuration ToBankIn(this CabinetDuration Instance)
        {
            return CabinetDuration.BankIn;
        }

        public static CabinetDuration ToBankOut(this CabinetDuration Instance)
        {
           return CabinetDuration.BankOut;
        }

        public static CabinetDuration ToSystemBootUp(this CabinetDuration Instance)
        {
            return CabinetDuration.SystemBootUp;
        }

        public static CabinetDuration ToInitial(this CabinetDuration Instance)
        {
            return CabinetDuration.Initial;
        }

        public static bool IsSystemBootupDuration(this CabinetDuration Instance)
        {
           if(Instance== CabinetDuration.SystemBootUp)
            {
                return true;
            }
           else
            {
                return false;
            }
        }

        public static bool IsInitialDuration(this CabinetDuration Instance)
        {
            if (Instance == CabinetDuration.Initial)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsBankInDuration(this CabinetDuration Instance)
        {
            if (Instance == CabinetDuration.BankIn)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsBankOutDuration(this CabinetDuration Instance)
        {
            if (Instance == CabinetDuration.BankOut)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsIdle(this CabinetDuration Instance)
        {
            if (Instance == CabinetDuration.DontCare)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        
    }
}
