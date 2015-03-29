using Domain;
using Infrastructure;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlRepository;

namespace BaconDavis.ViewModels
{
    public class ManageProjectsViewModel : ObservableObject, INavigationAware
    {
        private IRegionManager regionManager;
        private ProjectRepository projectRepository;
        private Project selectedProject;
        private string newProjectName;

        private ObservableCollection<Project> activeProjects;

        public ManageProjectsViewModel(IRegionManager regionManager)
        {
            activeProjects = new ObservableCollection<Project>();

            projectRepository = new ProjectRepository();

            DoneCommand = new DelegateCommand(CompleteProjectManagement);

            AddCommand = new DelegateCommand(AddProject, CanAddProject);

            DeleteCommand = new DelegateCommand<Project>(DeleteProject, CanDeleteProject);

            this.regionManager = regionManager;
        }

        public DelegateCommand DoneCommand { get; set; }

        public DelegateCommand AddCommand { get; set; }

        public DelegateCommand<Project> DeleteCommand { get; set; }

        public Project SelectedProject
        {
            get
            {
                return this.selectedProject;
            }
            set
            {
                this.selectedProject = value;
                RaisePropertyChanged(() => this.SelectedProject);
            }
        }

        public string NewProjectName
        {
            get
            {
                return newProjectName;
            }
            set
            {
                this.newProjectName = value;
                RaisePropertyChanged(() => this.NewProjectName);
                AddCommand.RaiseCanExecuteChanged();
            }

        }

        public ObservableCollection<Project> ActiveProjects
        {
            get
            {
                return this.activeProjects;
            }
        }

        public void CompleteProjectManagement()
        {
            if (selectedProject != null)
            {
                projectRepository.SetSelectedProject(selectedProject);
            }

            this.regionManager.RequestNavigate("MainRegion", typeof(Views.MainView).FullName);
        }

        public void OnNavigatedTo(NavigationContext context)
        {
            SelectedProject = projectRepository.GetSelectedProject();
            LoadActiveProjects();
        }

        private void LoadActiveProjects()
        {
            activeProjects.Clear();

            foreach (var project in projectRepository.ActiveProjects)
            {
                activeProjects.Add(project);
            }

            RaisePropertyChanged(() => ActiveProjects);
        }

        public void OnNavigatedFrom(NavigationContext context)
        {
            projectRepository.SaveActiveProjects();
        }

        public bool IsNavigationTarget(NavigationContext context)
        {
            return true;
        }

        private void AddProject()
        {
            if (!string.IsNullOrEmpty(this.newProjectName))
            {
                projectRepository.AddProject(this.newProjectName);
                LoadActiveProjects();
            }
        }

        private bool CanAddProject()
        {
            return !string.IsNullOrEmpty(this.newProjectName);
        }

        private void DeleteProject(Project project)
        {
            projectRepository.ActiveProjects.Remove(project);
            projectRepository.SaveActiveProjects();
            LoadActiveProjects();

            this.SelectedProject = this.ActiveProjects.FirstOrDefault();

            this.RaisePropertyChanged(() => this.ActiveProjects);
        }

        private bool CanDeleteProject(Project project)
        {
            return true;
        }
    }
}
