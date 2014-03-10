
namespace BoringDotNet.Caching
{
    public class NullCache : ICache
    {
        public bool IsCached(string key)
        {
            return false;
        }

        public T Get<T>(string key)
        {
            return default(T);
        }

        public void Put<T>(string key, T data)
        {
        }
    }
}