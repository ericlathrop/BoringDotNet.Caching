using System;

namespace BoringDotNet.Caching.Memcached
{
    public interface IMemcachedConfiguration
    {
        string PoolName { get; }
        string[] Servers { get; }
        int InitialConnections { get; }
        int MinConnections { get; }
        int MaxConnections { get; }
        TimeSpan MaintenanceSleep { get; }
    }
}