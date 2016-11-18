using Autofac;
using Common;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common
{
    /// <summary>
    /// 依赖注入
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
        }

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, CustomerConfig config)
        {
            throw new NotImplementedException();
        }
    }
}