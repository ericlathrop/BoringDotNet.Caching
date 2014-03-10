using System;

namespace BoringDotNet.Caching
{
    public class CacheKeyVersionBuilder
    {
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1);
        private const long TicksInMillisecond = 10000;

        private CacheKeyVersionBuilder() { } // prevent instantiation

        public static string MakeVersionNumber(DateTime dateTime)
        {
            return MakeVersionNumber(MillisSinceUnixEpoch(dateTime));
        }

        public static string MakeVersionNumber(long timeMillis)
        {
            var timeMillisBytes = BitConverter.GetBytes(timeMillis);
            return Convert.ToBase64String(timeMillisBytes);
        }

        private static long MillisSinceUnixEpoch(DateTime dateTime)
        {
            return (dateTime.Ticks - UnixEpoch.Ticks) / TicksInMillisecond;
        }

        public static long VersionNumberToMillis(string version)
        {
            var timeMillisBytes = Convert.FromBase64String(version);
            return BitConverter.ToInt64(timeMillisBytes, 0);
        }

        public static string IncrementVersionNumber(string version)
        {
            var millis = VersionNumberToMillis(version);
            return MakeVersionNumber(millis + 1);
        }
    }
}
