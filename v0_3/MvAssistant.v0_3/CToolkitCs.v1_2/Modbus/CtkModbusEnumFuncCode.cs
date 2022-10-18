using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CToolkitCs.v1_2.Modbus
{
    //值為byte
    public enum CtkModbusEnumFuncCode
    {
        fctReadCoil = 1,
        fctReadDiscreteInputs = 2,
        fctReadHoldingRegister = 3,
        fctReadInputRegister = 4,
        fctWriteSingleCoil = 5,
        fctWriteSingleRegister = 6,
        fctWriteMultipleCoils = 15,
        fctWriteMultipleRegister = 16,
        fctReadWriteMultipleRegister = 23,

    }
}
