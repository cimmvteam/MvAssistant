﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.Cabinet.DrawerQueues
{
    /// <summary>Result of EnqueueUniqu  </summary>
    public enum EnqueueUniqueMaskBoxForBankOutQueResult
    {
        EnqueOK,
        DuplicateBoxBarcode,
        DuplicateDrawerMachineID
    }
}
