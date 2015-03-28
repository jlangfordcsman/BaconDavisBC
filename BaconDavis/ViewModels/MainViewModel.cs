using Domain;
using Infrastructure;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlRepository;

namespace BaconDavis.ViewModels
{
    public class MainViewModel : ObservableObject, INavigationAware
    {
        private ProjectRepository projectRepository;
        private IRegionManager regionManager;
        private ProjectViewModel selectedProject;

        public MainViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;

            this.projectRepository = new ProjectRepository();
            this.ManageProjectsCommand = new DelegateCommand(RequestProjectManager);

        }

        public DelegateCommand ManageProjectsCommand { get; set; }

        public Period CurrentWageDay { get; set; }

        public Period AvailableWageDays { get; set; }

        public ICollection<Project> ActiveProjects
        {
            get
            {
                return projectRepository.ActiveProjects;
            }
        }

        public ProjectViewModel SelectedProject
        {
            get
            {
                return this.selectedProject;
            }

            set
            {
                this.selectedProject = value;
                this.RaisePropertyChanged(() => this.SelectedProject);
            }
        }

        private void RequestProjectManager()
        {
            regionManager.RequestNavigate("MainRegion", typeof(Views.ManageProjectView).FullName);
        }

        public void OnNavigatedTo(NavigationContext context)
        {
            SelectedProject = new ProjectViewModel() 
            {
                Project = projectRepository.GetSelectedProject()
            };
        }

        public void OnNavigatedFrom(NavigationContext context)
        {
        }

        public bool IsNavigationTarget(NavigationContext context)
        {
            return true;
        }
    }
}
