using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace Common.Session
{
    internal class SessionState
    {
        internal ISessionStateItemCollection _sessionItems;
        internal HttpStaticObjectsCollection _staticObjects;
        internal int _timeout;

        internal SessionState(ISessionStateItemCollection sessionItems, HttpStaticObjectsCollection staticObjects, int timeout)
        {
            this.Copy(sessionItems, staticObjects, timeout);
        }

        internal void Copy(ISessionStateItemCollection sessionItems, HttpStaticObjectsCollection staticObjects, int timeout)
        {
            this._sessionItems = sessionItems;
            this._staticObjects = staticObjects;
            this._timeout = timeout;
        }

        public string ToJson()
        {
            // 这里忽略_staticObjects这个成员。
            if (_sessionItems == null || _sessionItems.Count == 0)
            {
                return null;
            }

            Dictionary<string, SaveValue> dict = new Dictionary<string, SaveValue>(_sessionItems.Count);

            NameObjectCollectionBase.KeysCollection keys = _sessionItems.Keys;
            string key;
            object objectValue = string.Empty;
            Type type = null;
            for (int i = 0; i < keys.Count; i++)
            {
                key = keys[i];
                objectValue = _sessionItems[key];
                if (objectValue != null)
                {
                    type = objectValue.GetType();
                }
                else
                {
                    type = typeof(object);
                }
                dict.Add(key, new SaveValue { Value = objectValue, Type = type });
            }

            SessionStateItem item = new SessionStateItem { Dict = dict, Timeout = this._timeout };

            return JsonConvert.SerializeObject(item);
        }

        public static SessionState FromJson(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }
            try
            {
                SessionStateItem item = JsonConvert.DeserializeObject<SessionStateItem>(json);
                SessionStateItemCollection collections = new SessionStateItemCollection();

                SaveValue objectValue = null;
                JsonSerializer serializer = new JsonSerializer();
                StringReader sr = null;
                JsonTextReader tReader = null;

                foreach (KeyValuePair<string, SaveValue> kvp in item.Dict)
                {
                    objectValue = kvp.Value as SaveValue;
                    if (objectValue.Value == null)
                    {
                        collections[kvp.Key] = null;
                    }
                    else
                    {
                        if (!IsValueType(objectValue.Type))
                        {
                            sr = new StringReader(objectValue.Value.ToString());
                            tReader = new JsonTextReader(sr);
                            collections[kvp.Key] = serializer.Deserialize(tReader, objectValue.Type);
                        }
                        else
                        {
                            collections[kvp.Key] = objectValue.Value;
                        }
                    }
                }

                return new SessionState(collections, null, item.Timeout);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 判断是否为值类型
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns></returns>
        public static bool IsValueType(Type type)
        {
            if (type.IsValueType)
            {
                return true;
            }
            //基础数据类型
            if (type == typeof(string) || type == typeof(char)
                || type == typeof(ushort) || type == typeof(short) || type == typeof(uint) || type == typeof(int)
                || type == typeof(ulong) || type == typeof(long) || type == typeof(double) || type == typeof(decimal)
                || type == typeof(bool)
                || type == typeof(byte))
            {
                return true;
            }
            //可为null的基础数据类型
            if (type.IsGenericType && !type.IsGenericTypeDefinition)
            {
                Type genericType = type.GetGenericTypeDefinition();

                if (Object.ReferenceEquals(genericType, typeof(Nullable<>)))
                {
                    var actualType = type.GetGenericArguments()[0];
                    return IsValueType(actualType);

                }
            }
            return false;
        }
    }

    internal sealed class SessionStateItem
    {
        public Dictionary<string, SaveValue> Dict;
        public int Timeout;
    }

    internal sealed class SaveValue
    {
        public object Value { get; set; }

        public Type Type { get; set; }
    }
}
