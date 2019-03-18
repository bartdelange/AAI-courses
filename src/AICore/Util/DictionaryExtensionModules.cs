using System.Collections.Generic;

namespace AICore.Util
{
    public static class DictionaryExtensionModules
    {
        public static TValue GetOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (dict.TryGetValue(key, out var val)) return val;
            
            val = value;
            dict.Add(key, val);

            return val;
        }
    }
}