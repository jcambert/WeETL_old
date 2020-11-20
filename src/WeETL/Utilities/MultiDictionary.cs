using System.Collections.Generic;

namespace WeETL
{
    public class MultiDictionary<Key, Key2, Value> : Dictionary<Key, Dictionary<Key2, Value>>
    {
        public MultiDictionary(IEqualityComparer<Key> comparer) : base(comparer)
        {
        }

        public void Add(Key key, Key2 key2, Value value)
        {
            if (!this.TryGetValue(key, out var values))
            {
                values = new Dictionary<Key2, Value>();
                this.Add(key, values);
            }
            values[key2] = value;
        }
    }
}
