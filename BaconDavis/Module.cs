using Infrastructure;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaconDavis
{
    public class Module : BaseModule
    {
        private IRegionManager regionManager;

        public Module(IUnityContainer container, IRegionManager regionManager) 
            : base(container)
        {
            this.regionManager = regionManager;
        }

        protected override void RegisterServices()
        {
            this.container.RegisterType(typeof(object), typeof(Views.MainView), typeof(Views.MainView).FullName);
            this.container.RegisterType(typeof(object), typeof(Views.ManageProjectView), typeof(Views.ManageProjectView).FullName);

            this.regionManager.RequestNavigate("MainRegion", typeof(Views.MainView).FullName);
        }
    }
}
