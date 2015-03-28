using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XmlRepository
{
    internal static class EmployeeRoleExtensions
    {
        internal static void ReadFromXml(this EmployeeRole role, XmlReader reader)
        {
            reader.ReadStartElement("Role");

            role.Id = Guid.Parse(reader.GetAttribute("Id"));
            role.Name = reader.GetAttribute("Name");

            reader.ReadEndElement();
        }

        internal static void WriteToXml(this EmployeeRole role, XmlWriter writer)
        {
            writer.WriteStartElement("Role");

            writer.WriteAttributeString("Id", role.Id.ToString());
            writer.WriteAttributeString("Name", role.Name);

            writer.WriteEndElement();
        }
    }
}
