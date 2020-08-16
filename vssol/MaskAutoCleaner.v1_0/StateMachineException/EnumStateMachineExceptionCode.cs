using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.StateMachineException
{
    public enum EnumStateMachineExceptionCode
    {
        MaskTransferException = 10000,
        MaskTransferInitialFailException = 10001,
        MaskTransferInitialTimeOutException = 10002,
        MaskTransferMustResetException = 10003,
        MaskTransferMustInitialException = 10004,


        BoxTransferException = 20000,
        BoxTransferInitialFailException = 20001,
        BoxTransferInitialTimeOutException = 20002,
        BoxTransferMustResetException = 20003,
        BoxTransferMustInitialException = 20004,
       

        LoadportException =30000,
        LoadportInitialException=30001,
        LoadportInitialTimeoutEXception=30002,
        LoadportMustResetException=30003,
        LoadportMustInitialException =30004,

        DrawerException =40000,
        DrawerInitialFailException =40001,
        DrawerInitialTimeOutException=40002,
        DrawerMustResetException=40003,
        DrawerMustInitialException=40004,

    }

    public enum EnumEaceptionCategory
    {
        System=0,
        MaskTransfer=1,
        BoxTransfer=2,
        Loadport=3,
        Drawer=4,
    }

    public static class EnumExceptionCodeExrends
    {
        public static string ToErroeCode(this EnumStateMachineExceptionCode instance)
        {
            var intCode = (int)instance;
            var stringCode = intCode.ToString("000000");
            return stringCode;
        }
    }
}
