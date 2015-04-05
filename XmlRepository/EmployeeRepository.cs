using Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XmlRepository
{
    public class EmployeeRepository
    {
        private static ICollection<Employee> activeEmployees;
        private RoleRepository roleRepo;

        public EmployeeRepository()
        {
            roleRepo = new RoleRepository();
        }

        public ICollection<Employee> ActiveEmployees 
        {
            get
            {
                if (activeEmployees == null)
                {
                    activeEmployees = new List<Employee>();
                    LoadActiveEmployees();
                }

                return activeEmployees;
            }
        }

        public Employee GetEmployee(Guid id)
        {
            Employee employee = new Employee();

            using (var file = new FileStream(Paths.GetEmployeeXmlFile(id), FileMode.Open))
            {
                using (var reader = XmlReader.Create(file))
                {
                    reader.MoveToContent();
                    employee.ReadFromXml(reader);
                }
            }

            return employee;
        }

        public void SaveEmployee(Employee employee)
        {
            using (var file = new FileStream(Paths.GetEmployeeXmlFile(employee.Id), FileMode.Truncate))
            {
                using (var writer = XmlWriter.Create(file))
                {
                    employee.WriteToXml(writer);
                }
            }
        }

        private void LoadActiveEmployees()
        {
            activeEmployees.Clear();

            using (var file = new FileStream(Paths.EmployeeXml, FileMode.Open))
            {
                if (file.Length > 0)
                {
                    var reader = XmlReader.Create(file);

                    reader.ReadStartElement("ActiveEmployees");

                    while (reader.IsStartElement("Employee"))
                    {
                        var employee = this.GetEmployee(Guid.Parse(reader.GetAttribute("Id")));
                        this.ActiveEmployees.Add(employee);
                        reader.ReadStartElement("Employee");
                    }
                }
            }
        }

        public void SaveActiveEmployees()
        {
            using (var file = new FileStream(Paths.EmployeeXml, FileMode.Truncate))
            {
                using (var writer = XmlWriter.Create(file))
                {

                    writer.WriteStartElement("ActiveEmployees");

                    foreach (var employee in this.ActiveEmployees)
                    {
                        writer.WriteStartElement("Employee");
                        writer.WriteAttributeString("Id", employee.Id.ToString());
                        writer.WriteAttributeString("Name", employee.Name);
                        writer.WriteEndElement();

                        SaveEmployee(employee);
                    }

                    writer.WriteEndElement();
                }
            }
        }

        public Employee AddEmployee(string name)
        {
            Employee newEmployee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = name
            };

            this.ActiveEmployees.Add(newEmployee);

            this.SaveActiveEmployees();
            return newEmployee;
        }

        public void RemoveEmployee(Employee employee)
        {
            this.ActiveEmployees.Remove(employee);
            this.SaveActiveEmployees();
        }
    }
}
