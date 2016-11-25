using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class ContainerManager
    {
        private readonly IContainer _container;

        public ContainerManager(IContainer container)
        {
            this._container = container;
        }
    }
}
