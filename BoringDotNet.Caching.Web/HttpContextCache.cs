using System.Web;

namespace BoringDotNet.Caching.Web
{
    public class HttpContextCache : ICache
    {
        public T Get<T>(string key)
        {
            return (T)HttpContext.Current.Items[key];
        }

        public void Put<T>(string key, T data)
        {
            HttpContext.Current.Items[key] = data;
        }

        public bool IsCached(string key)
        {
            return HttpContext.Current.Items.Contains(key);
        }
    }
}
