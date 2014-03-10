using Memcached.ClientLibrary;
using System.Net;
using System.Text.RegularExpressions;

namespace BoringDotNet.Caching.Memcached
{
    public class MemcachedClientFactory
    {
        private readonly string _poolName;

        public MemcachedClientFactory(IMemcachedConfiguration config)
        {
            _poolName = config.PoolName;

            SockIOPool pool = SockIOPool.GetInstance(config.PoolName);
            if (!pool.Initialized)
            {
                InitializePool(config, pool);
            }
        }

        private void InitializePool(IMemcachedConfiguration config, SockIOPool pool)
        {
            var servers = ConvertServersToIpAddress(config.Servers);
            pool.SetServers(servers);

            pool.InitConnections = config.InitialConnections;
            pool.MinConnections = config.MinConnections;
            pool.MaxConnections = config.MaxConnections;

            pool.SocketConnectTimeout = 1000;
            pool.SocketTimeout = 3000;
            pool.MaintenanceSleep = (long)config.MaintenanceSleep.TotalMilliseconds;
            pool.Failover = true;

            pool.Nagle = false;
            pool.Initialize();
        }

        private string[] ConvertServersToIpAddress(string[] servers)
        {
            var retServers = new string[servers.Length];
            for (int i = 0; i < servers.Length; i++)
            {
                string server = servers[i];

                string[] serverSplit = server.Split(':');
                string host = serverSplit[0];
                string port = serverSplit[1];

                if (!IsIpAddress(host))
                {
                    host = GetIpFromHost(host);
                }

                retServers[i] = host + ':' + port;
            }

            return retServers;
        }

        private string GetIpFromHost(string host)
        {
            IPHostEntry entry = Dns.GetHostEntry(host);
            return entry.AddressList[0].ToString();
        }

        private bool IsIpAddress(string host)
        {
            return Regex.IsMatch(host, "^[0-9.]+$");
        }

        public MemcachedClient GetClient()
        {
            return new MemcachedClient
            {
                PoolName = _poolName,
                EnableCompression = false
            };
        }
    }
}
