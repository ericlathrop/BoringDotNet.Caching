using NUnit.Framework;
using System;

namespace BoringDotNet.Caching
{
    [TestFixture]
    public class CacheKeyVersionBuilderTests
    {
        [Test]
        public void MakeVersionNumber_WithEpoch_ReturnsBase64String()
        {
            var version = CacheKeyVersionBuilder.MakeVersionNumber(new DateTime(1970, 1, 1));
            Assert.AreEqual("AAAAAAAAAAA=", version);
        }

        [Test]
        public void MakeVersionNumber_WithYearAfterEpoch_ReturnsBase64String()
        {
            var version = CacheKeyVersionBuilder.MakeVersionNumber(new DateTime(1971, 1, 1));
            Assert.AreEqual("ACyxVwcAAAA=", version);
        }

        [Test]
        public void VersionNumberToMillis_WithEpoch_ReturnsZero()
        {
            var version = CacheKeyVersionBuilder.MakeVersionNumber(new DateTime(1970, 1, 1));
            var millis = CacheKeyVersionBuilder.VersionNumberToMillis(version);
            Assert.AreEqual(0, millis);
        }

        [Test]
        public void VersionNumberToMillis_WithYearAfterEpoch_ReturnsCorrectMillis()
        {
            var version = CacheKeyVersionBuilder.MakeVersionNumber(new DateTime(1971, 1, 1));
            var millis = CacheKeyVersionBuilder.VersionNumberToMillis(version);
            Assert.AreEqual(31536000000, millis);
        }

        [Test]
        public void IncrementVersion_WithEpoch_ReturnsBase64String()
        {
            var version = CacheKeyVersionBuilder.IncrementVersionNumber("AAAAAAAAAAA=");
            Assert.AreEqual("AQAAAAAAAAA=", version);
        }
    }
}
