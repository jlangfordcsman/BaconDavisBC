using Domain;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlRepository;

namespace Employees.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private EmployeeRepository employeeRepository;

        public MainViewModel(EmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public ObservableCollection<Employee> Employees { get; }
    
    }
}
