using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XmlRepository
{
    internal static class EmployeeInRoleExtensions
    {
        internal static void WriteToXml(this EmployeeInRole employeeInRole, XmlWriter writer)
        {
            writer.WriteStartElement("EmployeeInRole");

            employeeInRole.Employee.WriteToXml(writer);

            employeeInRole.Role.WriteToXml(writer);

            writer.WriteEndElement();
        }
    }
}
