using SensingNet.v0_2.TimeSignal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SensingNet.v0_2.Framework.Storage
{
    public class SNetFileStorageFormat
    {
        public String FormatName = typeof(SNetFileStorageFormat).Name;
        public String Remark;



        public virtual void WriteValues(StreamWriter sw, DateTime utc, IEnumerable<double> values)
        {
            throw new NotImplementedException();
        }


        //public virtual void ReadStream(StreamReader sr, SNetSignalCollector collector)
        //{
        //    throw new NotImplementedException();
        //}

        public virtual void ReadTSignal(StreamReader sr, SNetTSignalSecSetF8 tSignal)
        {
            throw new NotImplementedException();
        }
    }
}
