using System;
using System.Collections.Generic;
using System.Text;

namespace CToolkitCs.v1_2
{
    public static class CtkEquality<T>
    {
        public static IEqualityComparer<T> CreateComparer<V>(Func<T, V> keySelector)
        {
            return new CtkCommonEqualityComparer<V>(keySelector);
        }
        public static IEqualityComparer<T> CreateComparer<V>(Func<T, V> keySelector, IEqualityComparer<V> comparer)
        {
            return new CtkCommonEqualityComparer<V>(keySelector, comparer);
        }

        class CtkCommonEqualityComparer<V> : IEqualityComparer<T>
        {
            private Func<T, V> keySelector;
            private IEqualityComparer<V> comparer;

            public CtkCommonEqualityComparer(Func<T, V> keySelector, IEqualityComparer<V> comparer)
            {
                this.keySelector = keySelector;
                this.comparer = comparer;
            }
            public CtkCommonEqualityComparer(Func<T, V> keySelector)
                : this(keySelector, EqualityComparer<V>.Default)
            { }

            public bool Equals(T x, T y)
            {
                return comparer.Equals(keySelector(x), keySelector(y));
            }
            public int GetHashCode(T obj)
            {
                return comparer.GetHashCode(keySelector(obj));
            }
        }
    }
}
