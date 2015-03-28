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
        private static ICollection<Employee> employees;
        private RoleRepository roleRepo;

        public EmployeeRepository()
        {
            roleRepo = new RoleRepository();
        }

        public ICollection<Employee> Employees 
        {
            get
            {
                if (employees == null)
                {
                    employees = new List<Employee>();
                    LoadEmployees();
                }

                return employees;
            }
        }

        private void LoadEmployees()
        {
            using (var file = new FileStream(Paths.EmployeeXml, FileMode.Open))
            {
                var reader = XmlReader.Create(file);

                reader.ReadStartElement("Employees");

                while (reader.IsStartElement("Employee"))
                {
                    var employee = new Employee();
                    employee.ReadFromXml(reader);
                }

                reader.ReadEndElement();
            }
        }

        private void SaveEmployees()
        {
            using (var file = new FileStream(Paths.EmployeeXml, FileMode.Truncate))
            {
                using (var writer = XmlWriter.Create(file))
                {

                    writer.WriteStartElement("Employees");

                    foreach (var employee in this.Employees)
                    {
                        employee.WriteToXml(writer);
                    }

                    writer.WriteEndElement();
                }
            }
        }
    }
}
