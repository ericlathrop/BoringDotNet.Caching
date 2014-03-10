

namespace BoringDotNet.Caching
{
    public interface ICache
    {
        T Get<T>(string key);
        void Put<T>(string key, T data);
        bool IsCached(string key);
    }
}