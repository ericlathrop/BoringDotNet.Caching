using System;

namespace BoringDotNet.Caching
{
    public static class CacheExtensions
    {
        public static T GetOrCreate<T>(this ICache cache, string key, Func<T> creator)
        {
            if (cache.IsCached(key))
                return cache.Get<T>(key);

            var data = creator();
            cache.Put(key, data);
            return data;
        }
    }
}
