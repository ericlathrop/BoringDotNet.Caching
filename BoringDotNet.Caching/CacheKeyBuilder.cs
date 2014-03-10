using System;

namespace BoringDotNet.Caching
{
    public class CacheKeyBuilder<T>
    {
        private readonly ICache _cache;

        public CacheKeyBuilder(ICache cache)
        {
            _cache = cache;
        }

        internal string GetVersionKey()
        {
            return typeof(T).FullName + "|Version";
        }

        internal string GetCacheVersion(DateTime dateTime)
        {
            return _cache.GetOrCreate(GetVersionKey(), () => CacheKeyVersionBuilder.MakeVersionNumber(dateTime));
        }

        public string GetKey(string cacheName, string queryParams)
        {
            return GetKey(cacheName, queryParams, DateTime.UtcNow);
        }

        // for tests to specify the time
        internal string GetKey(string cacheName, string queryParams, DateTime dateTime)
        {
            return typeof(T).FullName + "|" + GetCacheVersion(dateTime) + "|" + cacheName + "|" + queryParams;
        }

        private string SafelyGetVersionFromCache(string key)
        {
            if (!_cache.IsCached(key))
                return null;
            return _cache.Get<string>(key);
        }

        public void InvalidateKeys()
        {
            InvalidateKeys(DateTime.UtcNow);
        }

        // for tests to specify time
        internal void InvalidateKeys(DateTime dateTime)
        {
            var key = GetVersionKey();

            var version = SafelyGetVersionFromCache(key);
            version = version == null ?
                CacheKeyVersionBuilder.MakeVersionNumber(dateTime) :
                CacheKeyVersionBuilder.IncrementVersionNumber(version);
            _cache.Put(key, version);
        }
    }
}
