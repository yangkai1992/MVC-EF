using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Cache
{
    public class RedisCache:ICache
    {


        public T Get<T>(string key)
        {
            throw new NotImplementedException();
        }

        public void Set<T>(string key, T val, int cacheTime)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }
    }
}
