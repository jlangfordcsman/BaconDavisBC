using Domain;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaconDavis.ViewModels
{
    public class ProjectViewModel : ObservableObject
    {
        private Project project;

        public Project Project 
        {
            get
            {
                return this.project;
            }

            set
            {
                this.project = value;
                RaisePropertyChanged(() => this.Project);
                RaisePropertyChanged(() => this.ProjectName);
            }
        }

        public string ProjectName
        {
            get
            {
                return this.Project == null ? string.Empty : this.Project.Name;
            }
        }
    }
}
