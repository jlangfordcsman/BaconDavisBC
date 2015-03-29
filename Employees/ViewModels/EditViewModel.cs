using Domain;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlRepository;

namespace Employees.ViewModels
{
    public class EditViewModel : ObservableObject
    {
        private Employee employee;

        public Employee Employee 
        { 
            get
            {
                return this.employee;
            }

            set
            {
                this.employee = value;
                RaisePropertyChanged(() => this.Employee);
            }
        }
    }
}
