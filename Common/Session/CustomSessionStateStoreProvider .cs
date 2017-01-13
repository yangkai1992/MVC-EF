using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace Common.Session
{
    /// <summary>
    /// 自定义session状态驱动
    /// </summary>
    public sealed class CustomSessionStateStoreProvider : SessionStateStoreProviderBase
    {
        private ICache _cache;

        public CustomSessionStateStoreProvider()
        {
            _cache = new RedisCache();
        }

        /// <summary>
        /// 创建新的存储数据
        /// </summary>
        /// <param name="context"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public override SessionStateStoreData CreateNewStoreData(HttpContext context, int timeout)
        {
            return new SessionStateStoreData(new SessionStateItemCollection(),
                SessionStateUtility.GetSessionStaticObjects(context), timeout);
        }

        private static SessionStateStoreData CreateLegitStoreData(HttpContext context,ISessionStateItemCollection sessionItems,HttpStaticObjectsCollection staticObject,int timeout)
        {
            if(sessionItems==null)
                sessionItems=new SessionStateItemCollection();
            if(staticObject==null&&context!=null)
                staticObject=SessionStateUtility.GetSessionStaticObjects(context);
            return new SessionStateStoreData(sessionItems,staticObject,timeout);
        }

        private SessionStateStoreData DoGet(HttpContext context, string id, bool exclusive, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actionFlags)
        {
            locked = false;
            lockId = null;
            lockAge = TimeSpan.Zero;
            actionFlags = SessionStateActions.None;
            RedisSessionState state = RedisSessionState.FromJson(RedisBase.Item_Get<string>(id));
            if (state == null)
            {
                return null;
            }
            RedisBase.Item_SetExpire(id, state._timeout);
            return CreateLegitStoreData(context, state._sessionItems, state._staticObjects, state._timeout);
        }

        /// <summary>
        /// 创建未初始化的项
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        /// <param name="timeout"></param>
        public override void CreateUninitializedItem(HttpContext context, string id, int timeout)
        {
            _cache.Set(id, null, timeout);
        }

        /// <summary>
        /// 获取项
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        /// <param name="locked"></param>
        /// <param name="lockAge"></param>
        /// <param name="lockId"></param>
        /// <param name="actions"></param>
        /// <returns></returns>
        public override SessionStateStoreData GetItem(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actions)
        {
            return sessionStateStoreBehavior.GetItem(false, context, id, out locked, out lockAge, out lockId, out actions);
        }

        /// <summary>
        /// 获取独占项
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        /// <param name="locked"></param>
        /// <param name="lockAge"></param>
        /// <param name="lockId"></param>
        /// <param name="actions"></param>
        /// <returns></returns>
        public override SessionStateStoreData GetItemExclusive(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actions)
        {
            return sessionStateStoreBehavior.GetItem(true, context, id, out locked, out lockAge, out lockId, out actions);
        }

        /// <summary>
        /// 移除项
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        /// <param name="lockId"></param>
        /// <param name="item"></param>
        public override void RemoveItem(HttpContext context, string id, object lockId, SessionStateStoreData item)
        {
            sessionStateStoreBehavior.RemoveItem(context, id, lockId);
        }

        /// <summary>
        /// 重设项的超时时间
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        public override void ResetItemTimeout(HttpContext context, string id)
        {
            sessionStateStoreBehavior.ResetItemTimeout(context, id);
        }

        /// <summary>
        /// 独占设置并释放项
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <param name="lockId"></param>
        /// <param name="newItem"></param>
        public override void SetAndReleaseItemExclusive(HttpContext context, string id, SessionStateStoreData item, object lockId, bool newItem)
        {
            sessionStateStoreBehavior.SetAndReleaseItem(context, id, item, lockId, newItem);
        }

        /// <summary>
        /// 回收
        /// </summary>
        public override void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 结束请求
        /// </summary>
        /// <param name="context"></param>
        public override void EndRequest(HttpContext context)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 初始化请求
        /// </summary>
        /// <param name="context"></param>
        public override void InitializeRequest(System.Web.HttpContext context)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 设置项过期回掉
        /// </summary>
        /// <param name="expireCallback"></param>
        /// <returns></returns>
        public override bool SetItemExpireCallback(SessionStateItemExpireCallback expireCallback)
        {
            return false;
        }
    }
}
