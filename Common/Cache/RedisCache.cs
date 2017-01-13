using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class RedisCache:ICache
    {
        private readonly RedisConnectionWrapper _connectionWrapper;
        private readonly IDatabase _db;

        public RedisCache()
        {
            this._connectionWrapper = new RedisConnectionWrapper();
            this._db = _connectionWrapper.Database();
        }


        public T Get<T>(string key)
        {
            RedisValue rValue = _db.StringGet(key);
            if (!rValue.HasValue)
                return default(T);

            string jsonString = Encoding.UTF8.GetString(rValue);
            return JsonUtls.FromJson<T>(jsonString);
        }

        public void Set(string key, object val, int cacheTime)
        {
            string jsonString = JsonUtls.ToJson(val);
            byte[] entryBytes = Encoding.UTF8.GetBytes(jsonString);

            TimeSpan expiresIn = TimeSpan.FromMinutes(cacheTime);
            _db.StringSet(key, entryBytes, expiresIn);
        }

        public void Remove(string key)
        {
            _db.KeyDelete(key);
        }

        public void Clear()
        {
            foreach (var ep in _connectionWrapper.GetEndpoints())
            {
                IServer server = _connectionWrapper.Server(ep);
                IEnumerable<RedisKey> keys = server.Keys();
                foreach (var key in keys)
                    _db.KeyDelete(key);
            }
        }
    }
}
