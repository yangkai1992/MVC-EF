using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Common
{
    public class Engine:IEngine
    {
        private ContainerManager _containerManager;

        public ContainerManager ContainerManager
        {
            get { return _containerManager; }
        }

        /// <summary>
        /// 注册组件和插件
        /// </summary>
        /// <param name="config">配置</param>
        public void Initialize(CustomerConfig config)
        {
            //注册依赖
            RegisterDependencies(config);
        }

        private void RegisterDependencies(CustomerConfig config)
        {
            var builder = new ContainerBuilder();
            var container = builder.Build();
            this._containerManager = new ContainerManager(container);

            var typeFinder = new WebAppTypeFinder();
            //创建一个新的ContainerBuilder实例，因为Build()和Update()方法在每个实例中是只能被调用一次
            builder = new ContainerBuilder();

            builder.RegisterInstance(config).As<CustomerConfig>().SingleInstance();
            builder.RegisterInstance(this).As<IEngine>().SingleInstance();
            builder.RegisterInstance(typeFinder).As<ITypeFinder>().SingleInstance();
            builder.Update(container);

            builder = new ContainerBuilder();
            var drTypes = typeFinder.FindClassesOfType<IDependencyRegistrar>();
            var drInstances = new List<IDependencyRegistrar>();

            foreach (var drType in drTypes)
                drInstances.Add((IDependencyRegistrar)Activator.CreateInstance(drType));

            foreach (var dependencyRegistrar in drInstances)
                dependencyRegistrar.Register(builder, typeFinder, config);

            builder.Update(container);

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
