BoringDotNet.Caching
=================
Caching interfaces, implementations, and cache key generation.

This small set of libraries provide a ICache interface with several implementations.

* NullCache - For when you want to switch off caching
* DictionaryCache - Simple in-memory cache.
* BinaryFormatterCache - Serializes object, and stores in-memory
* HttpContextCache - Adapter for System.Web.HttpContext
* MemcachedCache - Adapter for Memcached.

CacheKeyBuilder implements [key-based cache expiration](http://signalvnoise.com/posts/3113-how-key-based-cache-expiration-works).
