﻿using Autofac;
using Service;
using System.Linq;
using Autofac.Integration.Mvc;
using Repository;

namespace Common
{
    /// <summary>
    /// 依赖注入
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            //控制器注入
            builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());

            //注册服务
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();

            builder.Register<IDbContext>(c => new DataContext(ConfigHelper.GetConnectionString("DefaultConnection"))).InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            builder.RegisterType<IISCache>().As<ICache>().InstancePerLifetimeScope();
        }
    }
}