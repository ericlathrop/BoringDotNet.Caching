using System;
using System.Configuration;
using System.Linq;

namespace BoringDotNet.Caching.Memcached
{
    public class AppSettingsMemcachedConfiguration : IMemcachedConfiguration
    {
        public AppSettingsMemcachedConfiguration(string poolName)
        {
            PoolName = poolName;
        }

        public string PoolName { get; private set; }

        public string[] Servers
        {
            get
            {
                return ConfigurationManager.AppSettings["memcached_servers"]
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(server => server.Trim())
                    .ToArray();
            }
        }

        public int InitialConnections
        {
            get { return int.Parse(GetAppSettings("memcached.data.pool.InitConnections", "3")); }
        }

        public int MinConnections
        {
            get { return int.Parse(GetAppSettings("memcached.data.pool.MinConnections", "3")); }
        }

        public int MaxConnections
        {
            get { return int.Parse(GetAppSettings("memcached.data.pool.MaxConnections", "20")); }
        }

        public TimeSpan MaintenanceSleep
        {
            get { return TimeSpan.FromMilliseconds(int.Parse(ConfigurationManager.AppSettings["memcached_maintenance_sleep"])); }
        }

        public string GetAppSettings(string key, string defaultValue)
        {
            var val = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrEmpty(val))
                val = defaultValue;
            return val;
        }
    }
}
