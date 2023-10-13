using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Windows.Utils {
    public class ReferenceDictionary<TKey, TValue> : Dictionary<TKey, TValue> where TKey : class {
        public ReferenceDictionary() : base(ReferenceEqualityComparer.Instance) {

        }

        private sealed class ReferenceEqualityComparer : IEqualityComparer<TKey> {
            public static readonly ReferenceEqualityComparer Instance = new ReferenceEqualityComparer();
            public bool Equals(TKey x, TKey y) => ReferenceEquals(x, y);
            public int GetHashCode(TKey obj) => RuntimeHelpers.GetHashCode(obj);
        }
    }
}