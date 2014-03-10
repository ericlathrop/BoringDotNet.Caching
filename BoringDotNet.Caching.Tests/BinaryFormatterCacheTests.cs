using NUnit.Framework;
using System;
using System.Runtime.Serialization;

namespace BoringDotNet.Caching
{
    [TestFixture]
    public class BinaryFormatterCacheTests
    {
        private class Unserializable { }

        [Serializable]
        private class IsSerializable { }

        [Test]
        [ExpectedException(typeof(SerializationException))]
        public void Put_WithUnserializableObject_ThrowsException()
        {
            var cache = new BinaryFormatterCache();
            cache.Put("test", new Unserializable());
        }

        [Test]
        public void Get_WithObject_ReturnsACopy()
        {
            var obj = new IsSerializable();
            var cache = new BinaryFormatterCache();
            cache.Put("key", obj);
            var copy = cache.Get<IsSerializable>("key");

            Assert.AreNotEqual(obj, copy);
        }

        [Test]
        public void IsCached_WithCacheMiss_ReturnsFalse()
        {
            var cache = new BinaryFormatterCache();
            Assert.IsFalse(cache.IsCached("key"));
        }

        [Test]
        public void IsCached_WithCacheHit_ReturnsTrue()
        {
            var cache = new BinaryFormatterCache();
            cache.Put("key", new IsSerializable());
            Assert.IsTrue(cache.IsCached("key"));
        }
    }
}
