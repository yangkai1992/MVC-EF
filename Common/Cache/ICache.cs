using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface ICache
    {
        T Get<T>(string key);
        void Set(string key, object val, int cacheTime);
        void Remove(string key);
        /// <summary>
        /// 清空所有缓存
        /// </summary>
        void Clear();
    }
}
