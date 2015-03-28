using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public abstract class BaseModule : IModule
    {
        protected IUnityContainer container;

        public BaseModule(IUnityContainer container)
        {
            this.container = container;
        }

        public void Initialize()
        {
            this.RegisterServices();
        }

        protected abstract void RegisterServices();
    }
}
