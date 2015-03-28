using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XmlRepository
{
    internal static class EmployeeExtensions
    {
        internal static void WriteToXml(this Employee employee, XmlWriter writer)
        {
            writer.WriteStartElement("Employee");

            writer.WriteAttributeString("Id", employee.Id.ToString());
            writer.WriteAttributeString("Name", employee.Name);

            if (employee.PrimaryRole != null)
            {
                writer.WriteAttributeString("PrimaryRole", employee.PrimaryRole.Id.ToString());
            }

            writer.WriteEndElement();
        }

        internal static void ReadFromXml(this Employee employee, XmlReader reader)
        {
            employee.Id = Guid.Parse(reader.GetAttribute("Id"));
            employee.Name = reader.GetAttribute("Name");

            reader.ReadStartElement("Employee");

            employee.PrimaryRole = RoleRepository.Default.Roles.FirstOrDefault(r => r.Id == Guid.Parse(reader.GetAttribute("PrimaryRole")));

            reader.ReadEndElement();
        }
    }
}
