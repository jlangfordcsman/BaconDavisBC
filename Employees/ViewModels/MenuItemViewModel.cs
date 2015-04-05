using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.ViewModels
{
    public class MenuItemViewModel
    {
        private IRegionManager regionManager;

        public MenuItemViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            this.NavigateCommand = new DelegateCommand(this.Navigate);
        }

        public DelegateCommand NavigateCommand { get; set; }

        private void Navigate()
        {
            this.regionManager.RequestNavigate("MainRegion", typeof(Views.MainView).FullName);
        }
    }
}
