using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaskAutoCleaner.MemoryStorage
{
    public class MemStorageItem
    {
        public DateTime dt { get; set; }
        public object value;

        public float fValue { get { return Convert.ToSingle(this.value); } }







    }
}
