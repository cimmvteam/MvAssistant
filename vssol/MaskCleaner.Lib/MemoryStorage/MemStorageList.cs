using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaskAutoCleaner.MemoryStorage
{
    public class MemStorageList:List<MemStorageItem>
    {

        public int ReverseCount = 1024;

        public void AddAndCheck(MemStorageItem item)
        {
            this.Add(item);
            while (this.Count > ReverseCount)
                this.RemoveAt(0);
        }

    }
}
