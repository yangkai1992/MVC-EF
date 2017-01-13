using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IEngine
    {
        ContainerManager ContainerManager { get; }

        void Initialize();

        /// <summary>
        /// 解析依赖
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Resolve<T>() where T : class;
    }
}
