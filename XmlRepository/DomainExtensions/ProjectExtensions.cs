using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XmlRepository
{
    internal static class ProjectExtensions
    {
        internal static void WriteToXml(this Project project, XmlWriter writer)
        {
            writer.WriteStartElement("Project");

            writer.WriteAttributeString("Id", project.Id.ToString());
            writer.WriteAttributeString("Name", project.Name);

            writer.WriteStartElement("Periods");

            foreach (var period in project.Periods)
            {
                period.WriteToXml(writer);
            }

            writer.WriteEndElement();

            writer.WriteEndElement();
        }

        internal static void ReadFromXml(this Project project, XmlReader reader)
        {
            reader.MoveToContent();

            project.Id = Guid.Parse(reader.GetAttribute("Id"));
            project.Name = reader.GetAttribute("Name");

            reader.ReadStartElement("Project");

            reader.ReadStartElement("Periods");

            while (reader.IsStartElement("Period"))
            {
                var period = new Period();
                period.ReadFromXml(reader);
                project.Periods.Add(period);
            }


            reader.ReadEndElement();
        }
    }
}
