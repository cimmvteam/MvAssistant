using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.Cabinet
{
    public enum BankType
    {
        DontCare,
        BankIn,
        BankOut,
    }

    public static class BankTypeExtends
    {
        public static void Reset(this BankType Instance)
        {
            Instance = BankType.DontCare;
        }
        public static void ToBankIn(this BankType Instance)
        {
            Instance = BankType.BankIn;
        }

        public static void ToBankOut(this BankType Instance)
        {
            Instance = BankType.BankOut;
        }

    }
}
