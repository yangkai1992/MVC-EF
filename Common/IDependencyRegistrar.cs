using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 依赖注入接口
    /// </summary>
    public interface IDependencyRegistrar
    {
        /// <summary>
        /// 注册服务和接口
        /// </summary>
        /// <param name="builder">容器</param>
        /// <param name="typeFinder">类型查找器</param>
        /// <param name="config">配置文件</param>
        void Register(ContainerBuilder builder, ITypeFinder typeFinder, CustomerConfig config);
    }
}
