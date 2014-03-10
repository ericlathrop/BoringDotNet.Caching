using System.Collections.Generic;

namespace BoringDotNet.Caching
{
    public class DictionaryCache : ICache
    {
        private readonly Dictionary<string, object> _data;

        public DictionaryCache()
        {
            _data = new Dictionary<string, object>();
        }

        public DictionaryCache(Dictionary<string, object> data)
        {
            _data = data;
        }

        public bool IsCached(string key)
        {
            return _data.ContainsKey(key);
        }

        public T Get<T>(string key)
        {
            return (T)_data[key];
        }

        public void Put<T>(string key, T data)
        {
            _data[key] = data;
        }
    }
}