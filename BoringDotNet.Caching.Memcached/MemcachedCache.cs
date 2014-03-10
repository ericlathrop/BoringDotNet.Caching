using Memcached.ClientLibrary;

namespace BoringDotNet.Caching.Memcached
{
    public class MemcachedCache : ICache
    {
        private readonly MemcachedClient _client;

        public MemcachedCache(MemcachedClientFactory clientFactory)
        {
            _client = clientFactory.GetClient();
        }

        public T Get<T>(string key)
        {
            return (T)_client.Get(key);
        }

        public void Put<T>(string key, T data)
        {
            _client.Set(key, data);
        }

        public bool IsCached(string key)
        {
            return _client.KeyExists(key);
        }
    }
}
