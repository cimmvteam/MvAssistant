using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SensingNet.v0_2.TimeSignal
{
    public interface ISNetTdTSignalSet<T, S>
    {
        void Add(T key, IEnumerable<S> signals);
        bool ContainKey(T key);
        List<S> GetOrCreate(T key);
        void Set(T key, List<S> signals);
        void Set(T key, S signals);

    }
}
