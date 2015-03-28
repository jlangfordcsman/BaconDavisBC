using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XmlRepository
{
    internal static class PeriodExtensions
    {
        internal static void WriteToXml(this Period period, XmlWriter writer)
        {
            writer.WriteStartElement("Period");

            if (period.Start.HasValue)
            {
                writer.WriteAttributeString("Start", period.Start.ToString());
            }

            if (period.End.HasValue)
            {
                writer.WriteAttributeString("End", period.End.ToString());
            }

            writer.WriteStartElement("EmployeesInRoles");

            foreach (var employeeInRole in period.EmployeesInRoles)
            {
                employeeInRole.WriteToXml(writer);
            }

            writer.WriteEndElement();

            writer.WriteEndElement();
        }

        internal static void ReadFromXml(this Period period, XmlReader reader)
        {
            string buf = reader.GetAttribute("Start");

            if (!string.IsNullOrEmpty(buf))
            {
                period.Start = DateTime.Parse(buf);
            }

            buf = reader.GetAttribute("End");

            if (!string.IsNullOrEmpty(buf))
            {
                period.End = DateTime.Parse(buf);
            }

            reader.ReadStartElement("Period");
            reader.ReadEndElement();
        }
    }
}
