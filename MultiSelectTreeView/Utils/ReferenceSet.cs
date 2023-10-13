using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Windows.Utils {
    public class ReferenceSet<T> : HashSet<T> where T : class {
        public ReferenceSet() : base(ReferenceEqualityComparer.Instance) {

        }

        public ReferenceSet(IEnumerable<T> items) : base(items, ReferenceEqualityComparer.Instance) {

        }

        private sealed class ReferenceEqualityComparer : IEqualityComparer<T> {
            public static readonly ReferenceEqualityComparer Instance = new ReferenceEqualityComparer();
            public bool Equals(T x, T y) => ReferenceEquals(x, y);
            public int GetHashCode(T obj) => RuntimeHelpers.GetHashCode(obj);
        }
    }
}