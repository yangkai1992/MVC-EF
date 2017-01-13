using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class RedisConnectionWrapper
    {
        private readonly Lazy<string> _connectionString;
        private volatile ConnectionMultiplexer _connection;
        private readonly object _lock = new object();

        public RedisConnectionWrapper()
        {
            this._connectionString = new Lazy<string>(GetConnectionString);
        }

        private string GetConnectionString()
        {
            return "localhost";
        }

        private ConnectionMultiplexer GetConnection()
        {
            if (_connection != null && _connection.IsConnected) return _connection;

            lock (_lock)
            {
                if (_connection != null && _connection.IsConnected) return _connection;

                if (_connection != null)
                {
                    _connection.Dispose();
                }

                _connection = ConnectionMultiplexer.Connect(_connectionString.Value);
            }

            return _connection;
        }

        public IDatabase Database(int? db = null)
        {
            return GetConnection().GetDatabase(db ?? -1);
        }

        public IServer Server(EndPoint endPoint)
        {
            return GetConnection().GetServer(endPoint);
        }

        public EndPoint[] GetEndpoints()
        {
            return GetConnection().GetEndPoints();
        }

        public void FlushDb(int? db = null)
        {
            EndPoint[] endPoints = GetEndpoints();

            foreach (var endPoint in endPoints)
            {
                Server(endPoint).FlushDatabase(db ?? -1);
            }
        }
    }
}
