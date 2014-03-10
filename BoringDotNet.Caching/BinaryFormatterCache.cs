using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BoringDotNet.Caching
{
    public class BinaryFormatterCache : ICache
    {
        private readonly Dictionary<string, byte[]> _data = new Dictionary<string, byte[]>();

        public T Get<T>(string key)
        {
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream(_data[key]))
            {
                return (T)formatter.Deserialize(stream);
            }
        }

        public void Put<T>(string key, T data)
        {
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, data);
                _data[key] = stream.ToArray();
            }
        }

        public bool IsCached(string key)
        {
            return _data.ContainsKey(key);
        }
    }
}
