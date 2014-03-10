using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BoringDotNet.Caching
{
    [TestFixture]
    public class CacheKeyBuilderTests
    {
        private const string VersionKey = "BoringDotNet.Caching.CacheKeyBuilderTests|Version";

        [Test]
        public void GetVersionKey_WithType_ReturnsKey()
        {
            var ckb = new CacheKeyBuilder<CacheKeyBuilderTests>(null);
            var key = ckb.GetVersionKey();
            Assert.AreEqual(VersionKey, key);
        }

        [Test]
        public void GetCacheVersion_WithNothingInCache_GeneratesNewKey()
        {
            var stubCache = new DictionaryCache();
            var ckb = new CacheKeyBuilder<CacheKeyBuilderTests>(stubCache);

            var version = ckb.GetCacheVersion(new DateTime(1970, 1, 1));

            Assert.AreEqual("AAAAAAAAAAA=", version);
        }

        [Test]
        public void GetCacheVersion_WithNothingInCache_StoresNewVersionInCache()
        {
            var cacheData = new Dictionary<string, object>();
            var cache = new DictionaryCache(cacheData);
            var ckb = new CacheKeyBuilder<CacheKeyBuilderTests>(cache);

            ckb.GetCacheVersion(new DateTime(1970, 1, 1));

            Assert.AreEqual("AAAAAAAAAAA=", cacheData[VersionKey]);
        }

        [Test]
        public void GetCacheVersion_WithCachedVersion_ReturnsVersionFromCache()
        {
            var mockCache = Substitute.For<ICache>();
            const string fakeVersion = "12345";
            mockCache.IsCached(VersionKey).Returns(true);
            mockCache.Get<string>(VersionKey).Returns(fakeVersion);
            var ckb = new CacheKeyBuilder<CacheKeyBuilderTests>(mockCache);

            var version = ckb.GetCacheVersion(new DateTime(1970, 1, 1));

            Assert.AreEqual(fakeVersion, version);
        }

        [TestCase("BoringDotNet.Caching.CacheKeyBuilderTests")]
        [TestCase("AAAAAAAAAAA=")]
        [TestCase("someQuery")]
        [TestCase("someParam")]
        public void GetKey_WithNothingInCache_ContainsString(string existsInKey)
        {
            var stubCache = Substitute.For<ICache>();
            var ckb = new CacheKeyBuilder<CacheKeyBuilderTests>(stubCache);

            var key = ckb.GetKey("someQuery", "someParam", new DateTime(1970, 1, 1));

            Assert.IsTrue(key.Contains(existsInKey));
        }

        [Test]
        public void InvalidateKeys_WithNothingInCache_StoresNewVersion()
        {
            var cacheData = new Dictionary<string, object>();
            var cache = new DictionaryCache(cacheData);
            var ckb = new CacheKeyBuilder<CacheKeyBuilderTests>(cache);

            ckb.InvalidateKeys(new DateTime(1970, 1, 1));

            Assert.AreEqual("AAAAAAAAAAA=", cacheData[VersionKey]);
        }

        [Test]
        public void InvalidateKeys_WithVersionInCache_IncrementsVersion()
        {
            var cacheData = new Dictionary<string, object>();
            cacheData[VersionKey] = "AAAAAAAAAAA=";
            var cache = new DictionaryCache(cacheData);
            var ckb = new CacheKeyBuilder<CacheKeyBuilderTests>(cache);

            ckb.InvalidateKeys(new DateTime(1970, 1, 1));

            Assert.AreEqual("AQAAAAAAAAA=", cacheData[VersionKey]);
        }
    }
}
