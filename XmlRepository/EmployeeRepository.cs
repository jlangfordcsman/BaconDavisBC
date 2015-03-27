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
                    LoadEmployeesFromXml();
                }

                return employees;
            }
        }

        private void LoadEmployeesFromXml()
        {
            using (var file = new FileStream(Paths.EmployeeXml, FileMode.Open))
            {
                var reader = XmlReader.Create(file);

                reader.ReadStartElement("Employees");

                while (reader.IsStartElement("Employee"))
                {
                    reader.ReadStartElement("Employee");

                    Employee employee = new Employee();
                    employee.Id = Guid.Parse(reader.GetAttribute("Id"));
                    employee.Name = reader.GetAttribute("Name");
                    employee.PrimaryRole = roleRepo.Roles.FirstOrDefault(r => r.Id == Guid.Parse(reader.GetAttribute("PrimaryRole")));

                    reader.ReadEndElement();
                }

                reader.ReadEndElement();
            }
        }

        private void SaveEmployeesToXml()
        {
            using (var file = new FileStream(Paths.EmployeeXml, FileMode.Truncate))
            {
                var writer = XmlWriter.Create(file);

                writer.WriteStartElement("Employees");

                foreach (var employee in this.Employees)
                {
                    writer.WriteStartElement("Employee");

                    writer.WriteAttributeString("Id", employee.Id.ToString());
                    writer.WriteAttributeString("Name", employee.Name);

                    if(employee.PrimaryRole != null)
                    {
                        writer.WriteAttributeString("PrimaryRole", employee.PrimaryRole.Id.ToString());
                    }

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
            }
        }
    }
}
