using System.Collections.Generic;
using System.Linq;

namespace pocker_backend_core.helpers
{
    internal static class CollectionHelper
    {
        public static void AddToStart<TK, TV>(IDictionary<TK, TV> dictionary, TK key, TV value)
        {
            var kv = dictionary.Keys.Zip(dictionary.Values).ToList();
            dictionary.Clear();
            dictionary.Add(key, value);
            kv.ForEach(tuple => dictionary.Add(tuple.First!, tuple.Second));
        }

        public static TK GetByValue<TK, TV>(IDictionary<TK, TV> dictionary, TV value)
        {
            return dictionary.First(kv => value.Equals(kv.Value)).Key;
        }
    }
}