using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace Common.Session
{
    /// <summary>
    /// 自定义session状态驱动
    /// </summary>
    public class CustomSessionStateStoreProvider : SessionStateStoreProviderBase
    {
        private ICache _cache;

        public CustomSessionStateStoreProvider()
        {
            _cache = EngineContext.Current.Resolve<ICache>();

        }

        internal static SessionStateStoreData CreateLegitStoreData(HttpContext context,
                                            ISessionStateItemCollection sessionItems,
                                            HttpStaticObjectsCollection staticObjects,
                                            int timeout)
        {
            if (sessionItems == null)
                sessionItems = new SessionStateItemCollection();
            if (staticObjects == null && context != null)
                staticObjects = SessionStateUtility.GetSessionStaticObjects(context);
            return new SessionStateStoreData(sessionItems, staticObjects, timeout);
        }

        SessionStateStoreData DoGet(HttpContext context,
                                        String id,
                                        bool exclusive,
                                        out bool locked,
                                        out TimeSpan lockAge,
                                        out object lockId,
                                        out SessionStateActions actionFlags)
        {
            locked = false;
            lockId = null;
            lockAge = TimeSpan.Zero;
            actionFlags = SessionStateActions.None;
            SessionState state = SessionState.FromJson(_cache.Get<string>(id));
            if (state == null)
            {
                return null;
            }
            _cache.Set(id, state, state._timeout);
            return CreateLegitStoreData(context, state._sessionItems, state._staticObjects, state._timeout);
        }

        public override SessionStateStoreData GetItem(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actions)
        {
            return DoGet(context, id, true, out locked, out lockAge, out lockId, out actions);
        }

        public override SessionStateStoreData GetItemExclusive(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actions)
        {
            return DoGet(context, id, true, out locked, out lockAge, out lockId, out actions);
        }

        public override void SetAndReleaseItemExclusive(HttpContext context, string id, SessionStateStoreData item, object lockId, bool newItem)
        {
            ISessionStateItemCollection sessionItems = null;
            HttpStaticObjectsCollection staticObjects = null;

            if (item.Items.Count > 0)
                sessionItems = item.Items;
            if (!item.StaticObjects.NeverAccessed)
                staticObjects = item.StaticObjects;

            SessionState state2 = new SessionState(sessionItems, staticObjects, item.Timeout);
            _cache.Set(id, state2.ToJson(), item.Timeout);
        }

        public override void CreateUninitializedItem(HttpContext context, string id, int timeout)
        {
            SessionState state = new SessionState(null, null, timeout);
            _cache.Set(id, state.ToJson(), timeout);
        }
        public override SessionStateStoreData CreateNewStoreData(HttpContext context, int timeout)
        {
            return CreateLegitStoreData(context, null, null, timeout);
        }

        #region 未实现的方法
        public override void RemoveItem(HttpContext context, string id, object lockId, SessionStateStoreData item)
        {
            _cache.Remove(id);
        }
        public override void ReleaseItemExclusive(HttpContext context, string id, object lockId)
        {
        }
        public override void ResetItemTimeout(HttpContext context, string id)
        {
        }
        public override void EndRequest(HttpContext context)
        {
        }
        public override bool SetItemExpireCallback(SessionStateItemExpireCallback expireCallback)
        {
            return true;
        }
        public override void Dispose()
        {
        }
        public override void InitializeRequest(HttpContext context)
        {
        }
        #endregion
    }
}
