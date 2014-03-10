using System;

namespace BoringDotNet.Caching.Memcached
{
    public class MemcachedConfiguration : IMemcachedConfiguration
    {
        public string PoolName { get; set; }
        public string KeyNamespace { get; set; }
        public string[] Servers { get; set; }
        public int InitialConnections { get; set; }
        public int MinConnections { get; set; }
        public int MaxConnections { get; set; }
        public TimeSpan MaintenanceSleep { get; set; }
    }
}