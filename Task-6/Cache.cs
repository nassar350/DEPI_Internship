using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_6
{
    internal class Cache<TKey, TValue> where TKey : notnull
    {
        Dictionary<TKey, TValue> _cache;

        public Cache()
        {
            _cache = new Dictionary<TKey, TValue>();
        }
        public void Add(TKey key, TValue value) 
        {
            _cache.Add(key, value);
        }

        public TValue Get(TKey key) 
        {
            return _cache[key];
        }
    }
}
