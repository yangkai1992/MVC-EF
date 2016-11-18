using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace Common
{
    public class EngineContext
    {
        /// <summary>
        /// 初始化一个静态实例工厂
        /// </summary>
        /// <param name="forceRecreate">是否强制创建</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IEngine Initialize(bool forceRecreate)
        {
            if (Singleton<IEngine>.Instance == null || forceRecreate)
            {
                Singleton<IEngine>.Instance = new Engine();

                var config = ConfigurationManager.GetSection("CustomerConfig") as CustomerConfig;
                Singleton<IEngine>.Instance.Initialize(config);
            }

            return Singleton<IEngine>.Instance;
        }
    }
}
