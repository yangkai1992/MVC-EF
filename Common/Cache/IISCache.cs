using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Common
{
    public class IISCache:ICache
    {
        private Cache _cache;

        public IISCache()
        {
            _cache = HttpContext.Current.Cache;
        }


        public T Get<T>(string key)
        {
            object obj = _cache.Get(key);
            if (obj == null)
            {
                return default(T);
            }

            return (T)obj;
        }

        public void Set(string key, object val, int cacheTime)
        {
            if (val == null)
                return;
            var expiry=DateTime.UtcNow.AddMinutes(cacheTime);
            _cache.Insert(key, val, null, expiry, Cache.NoSlidingExpiration);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public void Clear()
        {
            IDictionaryEnumerator cacheEnum = _cache.GetEnumerator();
            while (cacheEnum.MoveNext())
            {
                _cache.Remove(cacheEnum.Key.ToString());
            }
        }
    }
}
