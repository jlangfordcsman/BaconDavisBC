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

namespace Employees.ViewModels
{
    public class MainViewModel : ObservableObject, INavigationAware
    {
        private EmployeeRepository employeeRepository;
        private ObservableCollection<Employee> employees;
        private Employee selectedEmployee;
        private bool showAdd;
        private string newEmployeeName;

        public MainViewModel(EmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
            this.employees = new ObservableCollection<Employee>();

            this.AddCommand = new DelegateCommand(this.AddEmployee, this.CanAddEmployee);
            this.NewCommand = new DelegateCommand(this.ClearSelected);
        }

        public DelegateCommand NewCommand { get; set; }

        public DelegateCommand AddCommand { get; set; }

        public string NewEmployeeName
        {
            get
            {
                return this.newEmployeeName;
            }
            set
            {
                this.newEmployeeName = value;
                this.RaisePropertyChanged(() => this.NewEmployeeName);
                this.AddCommand.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<Employee> Employees
        {
            get
            {
                return this.employees;
            }
        }

        public Employee SelectedEmployee
        {
            get
            {
                return this.selectedEmployee;
            }
            set
            {
                this.selectedEmployee = value;
                this.RaisePropertyChanged(() => this.SelectedEmployee);
            }
        }

        public bool ShowAdd
        {
            get
            {
                return this.showAdd;
            }
            set
            {
                this.showAdd = value;
                this.RaisePropertyChanged(() => this.ShowAdd);
            }

        }

        public bool IsNavigationTarget(NavigationContext context)
        {
            return true;
        }

        public void OnNavigatedTo(NavigationContext context)
        {
            LoadEmployees();
            this.SelectedEmployee = this.Employees.FirstOrDefault();
        }

        public void OnNavigatedFrom(NavigationContext context)
        {
            this.employeeRepository.SaveActiveEmployees();
        }

        private void LoadEmployees()
        {
            this.Employees.Clear();

            foreach (var employee in this.employeeRepository.ActiveEmployees)
            {
                this.Employees.Add(employee);
            }
        }

        private void ClearSelected()
        {
            this.SelectedEmployee = null;
        }

        private bool CanAddEmployee()
        {
            return !string.IsNullOrEmpty(this.NewEmployeeName);
        }

        private void AddEmployee()
        {
            if (this.CanAddEmployee())
            {
                var newEmployee = this.employeeRepository.AddEmployee(this.NewEmployeeName);
                LoadEmployees();

                this.SelectedEmployee = newEmployee;
            }
        }
    }
}
