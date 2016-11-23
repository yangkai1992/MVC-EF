using Autofac;
using Common;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac.Integration.Mvc;
using Repository;

namespace Common
{
    /// <summary>
    /// 依赖注入
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, CustomerConfig config)
        {
            //控制器注入
            builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());

            //注册服务
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();

            builder.Register<IDbContext>(c => new DataContext("Password=123456;Persist Security Info=True;User ID=sa;Initial Catalog=Test;Data Source=127.0.0.1")).InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

        }
    }
}